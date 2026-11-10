namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Market code
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
    ///  NASDAQ TEXAS
    /// </summary>
    NasdaqTexas = (byte)'B',

    /// <summary>
    ///  PSX
    /// </summary>
    Psx = (byte)'X',
}
