using Microsoft.VisualBasic.FileIO;
using System.Net.Http.Headers;

namespace StockChatroom.Application.Services.Stooq;

public class StooqService : IStooqService
{
    private readonly HttpClient _httpClient;

    public StooqService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetStockQuote(string stockCode)
    {
        using var msg = new HttpRequestMessage(HttpMethod.Get, new Uri($"?s={stockCode}&f=sd2t2ohlcv&h&e=csv"));
        msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
        using var resp = await _httpClient.SendAsync(msg);

        resp.EnsureSuccessStatusCode();

        using var stream = await resp.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(stream);
        var parser = new TextFieldParser(streamReader)
        {
            TextFieldType = FieldType.Delimited,
        };
        parser.SetDelimiters(new string[] { ";" });

        while (!parser.EndOfData)
        {
            var row = parser.ReadFields();
        }
    }
}
