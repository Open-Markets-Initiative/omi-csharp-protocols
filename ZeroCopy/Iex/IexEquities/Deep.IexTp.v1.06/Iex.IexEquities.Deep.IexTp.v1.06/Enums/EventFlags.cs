using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Identifies event processing by the System
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum EventFlags : byte
{
    /// <summary>
    ///  Order Book Is Processing An Event
    /// </summary>
    OrderBookIsProcessingAnEvent = 0,

    /// <summary>
    ///  Event Processing Complete
    /// </summary>
    EventProcessingComplete = 1,

}

/// <summary>
///  Reads and writes the Event Flags field in place.
/// </summary>
public struct EventFlagsField
{
    /// <summary>
    ///  The size of the Event Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Identifies event processing by the System
    /// </summary>
    public EventFlags Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Event Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly EventFlags Decode()
        => (EventFlags)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Event Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(EventFlags value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
