using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
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
