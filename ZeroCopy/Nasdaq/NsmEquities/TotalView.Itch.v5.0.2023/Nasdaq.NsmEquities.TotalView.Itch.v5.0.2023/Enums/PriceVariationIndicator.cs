using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PriceVariationIndicator : byte
{
    /// <summary>
    ///  Less than 1%
    /// </summary>
    LessThan1Percent = (byte)'L',

    /// <summary>
    ///  1 to 1.99%
    /// </summary>
    LessThan2Percent = (byte)'1',

    /// <summary>
    ///  2 to 2.99%
    /// </summary>
    LessThan3Percent = (byte)'2',

    /// <summary>
    ///  3 to 3.99%
    /// </summary>
    LessThan4Percent = (byte)'3',

    /// <summary>
    ///  4 to 4.99%
    /// </summary>
    LessThan5Percent = (byte)'4',

    /// <summary>
    ///  5 to 5.99%
    /// </summary>
    LessThan6Percent = (byte)'5',

    /// <summary>
    ///  6 to 6.99%
    /// </summary>
    LessThan7Percent = (byte)'6',

    /// <summary>
    ///  7 to 7.99%
    /// </summary>
    LessThan8Percent = (byte)'7',

    /// <summary>
    ///  8 to 8.99%
    /// </summary>
    LessThan9Percent = (byte)'8',

    /// <summary>
    ///  9 to 9.99%
    /// </summary>
    LessThan10Percent = (byte)'9',

    /// <summary>
    ///  10 to 19.99%
    /// </summary>
    LessThan20Percent = (byte)'A',

    /// <summary>
    ///  20 to 29.99%
    /// </summary>
    LessThan30Percent = (byte)'B',

    /// <summary>
    ///  30% or greater
    /// </summary>
    MoreThan30Percent = (byte)'C',

    /// <summary>
    ///  Cannot be calculated
    /// </summary>
    NotAvailable = (byte)' ',

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
    ///  Indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price
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
