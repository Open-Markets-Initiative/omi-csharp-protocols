namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

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
    ///  NASDAQ TEXAS
    /// </summary>
    NasdaqTexas = (byte)'B',

    /// <summary>
    ///  PSX
    /// </summary>
    Psx = (byte)'X',
}
