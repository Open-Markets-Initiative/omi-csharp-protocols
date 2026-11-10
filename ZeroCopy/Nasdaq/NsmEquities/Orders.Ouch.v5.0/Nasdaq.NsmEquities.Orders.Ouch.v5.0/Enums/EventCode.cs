using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Event Code
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EventCode : byte
{
    /// <summary>
    ///  Start Of Day, indicating that exchange is open and is ready to accept orders.
    /// </summary>
    StartOfDay = (byte)'S',

    /// <summary>
    ///  End Of Day, indicating that the exchange is closed and will no longer accept new orders. Note that it is still possible to receive breaks and cancels after this event.
    /// </summary>
    EndOfDay = (byte)'E',

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
