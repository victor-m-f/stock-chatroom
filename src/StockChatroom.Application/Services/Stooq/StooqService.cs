using Microsoft.VisualBasic.FileIO;
using StockChatroom.Domain.Entities;
using StockChatroom.Shared.ApiResponses;
using System.Net.Http.Headers;

namespace StockChatroom.Application.Services.Stooq;

public class StooqService : IStooqService
{
    private readonly HttpClient _httpClient;

    public StooqService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ApiResponseWithResult<List<Stock>>> GetStocksAsync(string stockCode, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));

        using var resp = await _httpClient.GetAsync($"?s={stockCode}&f=sd2t2ohlcv&h&e=csv", cancellationToken);

        _ = resp.EnsureSuccessStatusCode();

        using var stream = await resp.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        var parser = new TextFieldParser(streamReader)
        {
            TextFieldType = FieldType.Delimited,
        };
        parser.SetDelimiters(new string[] { "," });

        var stocks = new List<Stock>();
        var errors = new List<ApiError>();

        var firstIteration = true;

        while (!parser.EndOfData)
        {
            if (firstIteration)
            {
                firstIteration = false;
                continue;
            }

            var row = parser.ReadFields();

            if (row == null || row.Length != 8)
            {
                errors.Add(new ApiError("Invalid stock format."));
                break;
            }

            stocks.Add(new Stock(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7]));
        }

        return errors.Any() ? new ApiResponseWithResult<List<Stock>>(errors, 400) : new ApiResponseWithResult<List<Stock>>(stocks, 200);
    }
}
