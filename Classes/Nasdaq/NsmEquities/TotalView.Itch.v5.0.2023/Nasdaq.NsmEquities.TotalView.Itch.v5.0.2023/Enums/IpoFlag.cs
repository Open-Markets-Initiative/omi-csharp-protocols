namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates if the NASDAQ security is set up for IPO release
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoFlag : byte
{
    /// <summary>
    ///  Nasdaq listed instrument is set up as a new IPO security
    /// </summary>
    SetUpForIpoRelease = (byte)'Y',

    /// <summary>
    ///  Nasdaq listed instrument is not set up as a new IPO security
    /// </summary>
    NotSetUpForIpoRelease = (byte)'N',

    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)' ',
}
