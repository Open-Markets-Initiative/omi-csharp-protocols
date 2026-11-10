using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  The System Event Message is used to indicate events that apply to the market or the data feed.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  The System Event Message is used to indicate events that apply to the market or the data feed.
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
    ///  The Security Event Message is used to indicate events that apply to a security
    /// </summary>
    SecurityEventMessage = (byte)'E',

    /// <summary>
    ///  A displayed order that has been added to the IEX Book
    /// </summary>
    AddOrderMessage = (byte)'a',

    /// <summary>
    ///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
    /// </summary>
    OrderModifyMessage = (byte)'M',

    /// <summary>
    ///  A displayed order that was removed from the IEX Book
    /// </summary>
    OrderDeleteMessage = (byte)'R',

    /// <summary>
    ///  A displayed order that was executed against
    /// </summary>
    OrderExecutedMessage = (byte)'L',

    /// <summary>
    ///  A non-displayed order on the book that executed against another non-displayed order on the book
    /// </summary>
    TradeMessage = (byte)'T',

    /// <summary>
    ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
    /// </summary>
    TradeBreakMessage = (byte)'B',

    /// <summary>
    ///  This message is used to indicate that the IEX Book for a symbol has been cleared of all orders
    /// </summary>
    ClearBookMessage = (byte)'C',

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
