using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Denotes the type of event for which the message is being generated.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Denotes that BBO feed has started its daily transmission schedule
    /// </summary>
    StartOfTransmissions = (byte)'O',

    /// <summary>
    ///  This message indicates that the exchange is open and ready to start accepting orders
    /// </summary>
    StartOfSystemHours = (byte)'S',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are available for execution.
    /// </summary>
    StartOfMarketHours = (byte)'Q',

    /// <summary>
    ///  This message is intended to indicate that Market Hours orders are no longer available for execution.
    /// </summary>
    EndOfMarketHours = (byte)'M',

    /// <summary>
    ///  It indicates that the exchange is now closed and will not accept any new orders today.
    /// </summary>
    EndOfSystemHours = (byte)'E',

    /// <summary>
    ///  Denotes that BBO feed has completed its daily transmission schedule.
    /// </summary>
    EndOfTransmissions = (byte)'C',

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
    ///  Denotes the type of event for which the message is being generated.
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
