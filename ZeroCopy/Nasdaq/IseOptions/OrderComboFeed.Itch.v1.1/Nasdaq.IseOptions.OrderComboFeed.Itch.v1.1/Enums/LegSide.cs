using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Indicates the side of the leg
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LegSide : byte
{
    /// <summary>
    ///  Buy
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell
    /// </summary>
    Sell = (byte)'S',

}

/// <summary>
///  Reads and writes the Leg Side field in place.
/// </summary>
public struct LegSideField
{
    /// <summary>
    ///  The size of the Leg Side field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the side of the leg
    /// </summary>
    public LegSide Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Leg Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly LegSide Decode()
        => (LegSide)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Leg Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(LegSide value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
