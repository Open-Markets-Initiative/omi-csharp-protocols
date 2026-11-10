using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PriceVariationIndicator : byte
{
    /// <summary>
    ///  Less than 1%
    /// </summary>
    LessThan1 = (byte)'L',

    /// <summary>
    ///  1 to 1.99%
    /// </summary>
    OneToOnePointNineNinePercent = (byte)'1',

    /// <summary>
    ///  2 to 2.99%
    /// </summary>
    TwoToTwoPointNineNinePercent = (byte)'2',

    /// <summary>
    ///  3 to 3.99%
    /// </summary>
    ThreeToThreePointNineNinePercent = (byte)'3',

    /// <summary>
    ///  4 to 4.99%
    /// </summary>
    FourToFourPointNineNinePercent = (byte)'4',

    /// <summary>
    ///  5 to 5.99%
    /// </summary>
    FiveToFivePointNineNinePercent = (byte)'5',

    /// <summary>
    ///  6 to 6.99%
    /// </summary>
    SixToSixPointNineNinePercent = (byte)'6',

    /// <summary>
    ///  7 to 7.99%
    /// </summary>
    SevenToSevenPointNineNinePercent = (byte)'7',

    /// <summary>
    ///  8 to 8.99%
    /// </summary>
    EightToEightPointNineNinePercent = (byte)'8',

    /// <summary>
    ///  9 to 9.99%
    /// </summary>
    NineToNinePointNineNinePercent = (byte)'9',

    /// <summary>
    ///  10 to 19.99%
    /// </summary>
    TenToNineteenPointNineNinePercent = (byte)'A',

    /// <summary>
    ///  20 to 29.99%
    /// </summary>
    TwentyToTwentyNinePointNineNinePercent = (byte)'B',

    /// <summary>
    ///  30% or greater
    /// </summary>
    ThirtyPercentOrGreater = (byte)'C',

    /// <summary>
    ///  Cannot be calculated
    /// </summary>
    CannotBeCalculated = (byte)' ',

}

/// <summary>
///  Reads and writes the Price Variation Indicator field in place.
/// </summary>
public struct PriceVariationIndicatorField
{
    /// <summary>
    ///  The size of the Price Variation Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
    /// </summary>
    public PriceVariationIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Price Variation Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly PriceVariationIndicator Decode()
        => (PriceVariationIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Price Variation Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(PriceVariationIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
