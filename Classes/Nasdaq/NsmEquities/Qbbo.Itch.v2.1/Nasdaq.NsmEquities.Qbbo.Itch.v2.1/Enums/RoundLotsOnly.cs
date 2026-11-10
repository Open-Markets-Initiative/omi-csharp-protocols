namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates if Nasdaq system limits order entry for issue
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RoundLotsOnly : byte
{
    /// <summary>
    ///  only round lots are accepted in this stock
    /// </summary>
    RoundLotsOnlyValue = (byte)'Y',

    /// <summary>
    ///  odd/mixed lots are allowed
    /// </summary>
    NoRestrictions = (byte)'N',
}
