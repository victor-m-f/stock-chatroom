using StockChatroom.Domain.Entities;
using StockChatroom.Shared.ApiResponses;

namespace StockChatroom.Application.Services.Stooq;

public interface IStooqService
{
    public Task<ApiResponseWithResult<List<Stock>>> GetStocksAsync(string stockCode, CancellationToken cancellationToken);
}
