namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates if the Nasdaq security is set up for IPO release.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoFlag : byte
{
    /// <summary>
    ///  Nasdaq listed instrument is set up as a new IPO security
    /// </summary>
    IpoSecurity = (byte)'Y',

    /// <summary>
    ///  Nasdaq listed instrument is not set up as a new IPO security
    /// </summary>
    NotIpoSecurity = (byte)'N',

    /// <summary>
    ///  Nasdaq listed instrument is a non-IPO new listed security
    /// </summary>
    NonIpoNewListedSecurity = (byte)'Z',

    /// <summary>
    ///  Not available
    /// </summary>
    NotAvailable = (byte)' ',
}
