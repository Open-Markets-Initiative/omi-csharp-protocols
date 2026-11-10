using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ShortSaleThresholdIndicator : byte
{
    /// <summary>
    ///  Issue is restricted under SEC Rule 203(b)(3)
    /// </summary>
    Restricted = (byte)'Y',

    /// <summary>
    ///  Issue is not restricted
    /// </summary>
    NotRestricted = (byte)'N',

    /// <summary>
    ///  Threshold Indicator not available
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Short Sale Threshold Indicator field in place.
/// </summary>
public struct ShortSaleThresholdIndicatorField
{
    /// <summary>
    ///  The size of the Short Sale Threshold Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
    /// </summary>
    public ShortSaleThresholdIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Short Sale Threshold Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ShortSaleThresholdIndicator Decode()
        => (ShortSaleThresholdIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Short Sale Threshold Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ShortSaleThresholdIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
