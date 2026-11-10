using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Event Code
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Start of Messages. Outside of time stamp messages, the start of day message is the first message sent in any trading day
    /// </summary>
    StartOfMessages = (byte)'O',

    /// <summary>
    ///  This message indicates that Nasdaq Texas is open and ready to start accepting orders
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are available for execution
    /// </summary>
    StartOfMarketHours = (byte)'Q',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are no longer available for execution
    /// </summary>
    EndOfMarketHours = (byte)'M',

    /// <summary>
    ///  It indicates that Nasdaq Texas is now closed and will not accept any new orders today. It is still possible to receive Broken Trade messages and Order Delete messages after the End of Day
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  This is always the last message sent in any trading day.
    /// </summary>
    EndOfMessages = (byte)'C',

}

/// <summary>
///  Reads and writes the Event Code field in place.
/// </summary>
public struct EventCodeField
{
    /// <summary>
    ///  The size of the Event Code field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Event Code
    /// </summary>
    public EventCode Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Event Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly EventCode Decode()
        => (EventCode)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Event Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(EventCode value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
