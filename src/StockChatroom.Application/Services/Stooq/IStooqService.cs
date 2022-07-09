namespace StockChatroom.Application.Services.Stooq;

public interface IStooqService
{
    public Task GetStockQuote(string stockCode);
}
