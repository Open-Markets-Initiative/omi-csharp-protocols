using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  IPO Quotation Release Qualifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoQuotationReleaseQualifier : byte
{
    /// <summary>
    ///  Used when Nasdaq Market Operations initially enters the IPO instrument for release
    /// </summary>
    AnticipatedQuotationReleaseTime = (byte)'A',

    /// <summary>
    ///  Used when Nasdaq Market Operations cancels or postpones the release of the new IPO instrument
    /// </summary>
    IpoReleaseCanceledOrPostponed = (byte)'C',

}

/// <summary>
///  Reads and writes the Ipo Quotation Release Qualifier field in place.
/// </summary>
public struct IpoQuotationReleaseQualifierField
{
    /// <summary>
    ///  The size of the Ipo Quotation Release Qualifier field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  IPO Quotation Release Qualifier
    /// </summary>
    public IpoQuotationReleaseQualifier Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Ipo Quotation Release Qualifier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly IpoQuotationReleaseQualifier Decode()
        => (IpoQuotationReleaseQualifier)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Ipo Quotation Release Qualifier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(IpoQuotationReleaseQualifier value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
