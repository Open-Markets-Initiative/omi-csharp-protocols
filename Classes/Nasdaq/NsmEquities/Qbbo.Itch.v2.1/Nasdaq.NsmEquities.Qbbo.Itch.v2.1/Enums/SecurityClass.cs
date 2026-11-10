namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates the primary listing market for the issue.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityClass : byte
{
    /// <summary>
    ///  Nasdaq-Listed Issue
    /// </summary>
    NasdaqListedIssue = (byte)'Q',

    /// <summary>
    ///  NYSE
    /// </summary>
    Nyse = (byte)'N',

    /// <summary>
    ///  NYSE American
    /// </summary>
    NyseAmerican = (byte)'A',

    /// <summary>
    ///  NYSE Arca
    /// </summary>
    NyseArca = (byte)'P',

    /// <summary>
    ///  NYSE Texas
    /// </summary>
    NyseTexas = (byte)'M',

    /// <summary>
    ///  BATS
    /// </summary>
    Bats = (byte)'Z',

    /// <summary>
    ///  IEXG
    /// </summary>
    Iexg = (byte)'V',
}
