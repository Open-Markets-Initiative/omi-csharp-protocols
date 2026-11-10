namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Identifies the type of a message contained in Packet by its message type.
/// </summary>
public enum PacketMessageCode : byte
{
    /// <summary>
    ///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
    /// </summary>
    QuoteUpdateMessage = (byte)'Q',

    /// <summary>
    ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
    /// </summary>
    TradeReportMessage = (byte)'T',

    /// <summary>
    ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
    /// </summary>
    TradeBreakMessage = (byte)'B',
}
