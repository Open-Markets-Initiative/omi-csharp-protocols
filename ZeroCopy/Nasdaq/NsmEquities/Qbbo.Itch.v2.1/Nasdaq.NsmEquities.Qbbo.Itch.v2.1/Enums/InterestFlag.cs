using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates the type of price improvement available
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InterestFlag : byte
{
    /// <summary>
    ///  PI orders available on the buy side
    /// </summary>
    BuySide = (byte)'B',

    /// <summary>
    ///  PI orders available on the sell side
    /// </summary>
    SellSide = (byte)'S',

    /// <summary>
    ///  PI orders available on both sides (buy and sell)
    /// </summary>
    BothSides = (byte)'A',

    /// <summary>
    ///  No PI orders available
    /// </summary>
    NoPiAvailable = (byte)'N',

}

/// <summary>
///  Reads and writes the Interest Flag field in place.
/// </summary>
public struct InterestFlagField
{
    /// <summary>
    ///  The size of the Interest Flag field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the type of price improvement available
    /// </summary>
    public InterestFlag Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Interest Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly InterestFlag Decode()
        => (InterestFlag)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Interest Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(InterestFlag value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
