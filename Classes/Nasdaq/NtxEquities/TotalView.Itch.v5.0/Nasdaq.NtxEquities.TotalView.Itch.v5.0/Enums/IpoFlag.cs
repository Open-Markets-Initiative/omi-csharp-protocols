namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the Nasdaq security is set up for IPO release.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoFlag : byte
{
    /// <summary>
    ///  Nasdaq system only accepts round lots orders for this security.
    /// </summary>
    IpoSecurity = (byte)'Y',

    /// <summary>
    ///  Nasdaq system does not have any order size restrictions for this security. Odd and mixed lot orders are allowed.
    /// </summary>
    NotIpoSecurity = (byte)'N',

    /// <summary>
    ///  Not available
    /// </summary>
    NotAvailable = (byte)' ',
}
