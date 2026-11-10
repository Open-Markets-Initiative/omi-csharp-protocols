using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Corresponds to TimeInForce (59) in Nasdaq FIX
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TimeInForce : byte
{
    /// <summary>
    ///  Day (Market Hours)
    /// </summary>
    Day = (byte)'0',

    /// <summary>
    ///  IOC
    /// </summary>
    Ioc = (byte)'3',

    /// <summary>
    ///  GTX (Extended Hours)
    /// </summary>
    GtxExtendedHours = (byte)'5',

    /// <summary>
    ///  GTT (ExpireTime needs to be specified)
    /// </summary>
    Gtt = (byte)'6',

    /// <summary>
    ///  After hours
    /// </summary>
    AfterHours = (byte)'E',

}

/// <summary>
///  Reads and writes the Time In Force field in place.
/// </summary>
public struct TimeInForceField
{
    /// <summary>
    ///  The size of the Time In Force field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Corresponds to TimeInForce (59) in Nasdaq FIX
    /// </summary>
    public TimeInForce Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Time In Force field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TimeInForce Decode()
        => (TimeInForce)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Time In Force field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TimeInForce value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
