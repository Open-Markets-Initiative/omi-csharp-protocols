namespace Databento.MarketData.Normalized.Dbn.v1.GatewayManager;

public sealed partial class DatabentoMarketDataNormalizedDbnGatewayManager
{
    partial void OnTradeMsg(TradeMsg message, int index)
    {
        Console.WriteLine($"TradeMsg {index}: Price {message.Price}");
    }
}
