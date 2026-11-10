using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Capacity
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Capacity : byte
{
    /// <summary>
    ///  Other
    /// </summary>
    Other = (byte)'O',

    /// <summary>
    ///  agency
    /// </summary>
    Agency = (byte)'A',

    /// <summary>
    ///  principal
    /// </summary>
    Principal = (byte)'P',

    /// <summary>
    ///  riskless
    /// </summary>
    Riskless = (byte)'R',

}

/// <summary>
///  Reads and writes the Capacity field in place.
/// </summary>
public struct CapacityField
{
    /// <summary>
    ///  The size of the Capacity field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Capacity
    /// </summary>
    public Capacity Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Capacity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Capacity Decode()
        => (Capacity)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Capacity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Capacity value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
