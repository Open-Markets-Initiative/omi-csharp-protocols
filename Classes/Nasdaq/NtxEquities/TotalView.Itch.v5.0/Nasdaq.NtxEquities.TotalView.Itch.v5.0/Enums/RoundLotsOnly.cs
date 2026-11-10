namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if Nasdaq system limits order entry for issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RoundLotsOnly : byte
{
    /// <summary>
    ///  Nasdaq system only accepts round lots orders for this security.
    /// </summary>
    RoundLotsOnlyValue = (byte)'Y',

    /// <summary>
    ///  Nasdaq system does not have any order size restrictions for this security. Odd and mixed lot orders are allowed.
    /// </summary>
    NoRestrictions = (byte)'N',
}
