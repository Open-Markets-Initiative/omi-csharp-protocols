namespace Iex.IexEquities.Tops.IexTp;

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
    QuoteUpdateMessage = (byte)'Q',
    TradeReportMessage = (byte)'T',
    OfficialPriceMessage = (byte)'X',
    TradeBreakMessage = (byte)'B',
    AuctionInformationMessage = (byte)'A',
}
