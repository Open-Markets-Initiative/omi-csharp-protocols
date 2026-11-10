namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates if the security is eligible to be released for trading
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OpenEligibilityStatus : byte
{
    /// <summary>
    ///  Not Eligible
    /// </summary>
    NotEligible = (byte)'N',

    /// <summary>
    ///  Eligible
    /// </summary>
    Eligible = (byte)'Y',
}
