namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates whether the security is an exchange traded product
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EtpFlag : byte
{
    /// <summary>
    ///  Instrument is an ETP
    /// </summary>
    Etp = (byte)'Y',

    /// <summary>
    ///  Instrument is not an ETP
    /// </summary>
    NotEtp = (byte)'N',

    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)' ',
}
