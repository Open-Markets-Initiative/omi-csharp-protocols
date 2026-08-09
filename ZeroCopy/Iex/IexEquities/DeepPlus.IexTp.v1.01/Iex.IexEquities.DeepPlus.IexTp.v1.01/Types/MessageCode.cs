namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Routing enumeration: maps each discriminator wire code to the message it dispatches to. Switch on this for message routing.
/// </summary>
public enum MessageCode : byte
{
    SystemEventMessage = (byte)'S',
    SecurityDirectoryMessage = (byte)'D',
    TradingStatusMessage = (byte)'H',
    RetailLiquidityIndicatorMessage = (byte)'I',
    OperationalHaltStatusMessage = (byte)'O',
    ShortSalePriceTestStatusMessage = (byte)'P',
    SecurityEventMessage = (byte)'E',
    AddOrderMessage = (byte)'a',
    OrderModifyMessage = (byte)'M',
    OrderDeleteMessage = (byte)'R',
    OrderExecutedMessage = (byte)'L',
    TradeMessage = (byte)'T',
    TradeBreakMessage = (byte)'B',
    ClearBookMessage = (byte)'C',
}
