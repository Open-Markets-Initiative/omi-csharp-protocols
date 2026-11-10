namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

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
