using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  The System Event Message is used to indicate events that apply to the market or the data feed
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  IEX disseminates a full pre-market spin of Security Directory Messages for all IEX-listed securities. After the pre-market spin, IEX will use the Security Directory Message to relay changes for an individual security.
    /// </summary>
    SecurityDirectoryMessage = (byte)'D',

    /// <summary>
    ///  The Trading Status Message is used to indicate the current trading status of a security.
    /// </summary>
    TradingStatusMessage = (byte)'H',

    /// <summary>
    ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
    /// </summary>
    RetailLiquidityIndicatorMessage = (byte)'I',

    /// <summary>
    ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
    /// </summary>
    OperationalHaltStatusMessage = (byte)'O',

    /// <summary>
    ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
    /// </summary>
    ShortSalePriceTestStatusMessage = (byte)'P',

    /// <summary>
    ///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
    /// </summary>
    QuoteUpdateMessage = (byte)'Q',

    /// <summary>
    ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
    /// </summary>
    TradeReportMessage = (byte)'T',

    /// <summary>
    ///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
    /// </summary>
    OfficialPriceMessage = (byte)'X',

    /// <summary>
    ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
    /// </summary>
    TradeBreakMessage = (byte)'B',

    /// <summary>
    ///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
    /// </summary>
    AuctionInformationMessage = (byte)'A',

}

/// <summary>
///  Reads and writes the Message Type field in place.
/// </summary>
public struct MessageTypeField
{
    /// <summary>
    ///  The size of the Message Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Code identifying this message type
    /// </summary>
    public MessageType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Message Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MessageType Decode()
        => (MessageType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Message Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MessageType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
