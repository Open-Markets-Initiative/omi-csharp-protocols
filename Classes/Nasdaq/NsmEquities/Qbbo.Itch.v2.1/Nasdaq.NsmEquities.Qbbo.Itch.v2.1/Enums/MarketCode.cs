namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Identifies the applicable Nasdaq market center
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketCode : byte
{
    /// <summary>
    ///  Nasdaq
    /// </summary>
    Nasdaq = (byte)'Q',

    /// <summary>
    ///  Nasdaq Texas
    /// </summary>
    NasdaqTexas = (byte)'B',

    /// <summary>
    ///  PSX
    /// </summary>
    Psx = (byte)'X',
}
