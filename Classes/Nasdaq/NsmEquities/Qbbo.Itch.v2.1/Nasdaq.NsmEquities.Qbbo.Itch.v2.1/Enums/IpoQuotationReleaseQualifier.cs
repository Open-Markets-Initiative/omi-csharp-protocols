namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Denotes the IPO release qualifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoQuotationReleaseQualifier : byte
{
    /// <summary>
    ///  This value would be used when Nasdaq Market Operations initially enters the IPO instrument for release.
    /// </summary>
    AnticipatedQuotationReleaseTime = (byte)'A',

    /// <summary>
    ///  This value would be used when Nasdaq Market Operations cancels or postpones the release of the IPO instrument.
    /// </summary>
    IpoReleaseCanceledPostponed = (byte)'C',
}
