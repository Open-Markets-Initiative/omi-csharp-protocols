namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

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
