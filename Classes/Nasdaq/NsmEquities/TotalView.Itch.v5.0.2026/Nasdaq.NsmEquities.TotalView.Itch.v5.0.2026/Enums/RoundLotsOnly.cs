namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates if Nasdaq system limits order entry for issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RoundLotsOnly : byte
{
    /// <summary>
    ///  Nasdaq system only accepts round lot orders for this security
    /// </summary>
    Yes = (byte)'Y',

    /// <summary>
    ///  Nasdaq system does not have any order size restrictions for this security
    /// </summary>
    No = (byte)'N',
}
