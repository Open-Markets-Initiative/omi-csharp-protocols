namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The Nasdaq Texas cross session for which the message is being generated.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  Nasdaq Texas Opening Cross.
    /// </summary>
    NasdaqTexasOpeningCross = (byte)'O',

    /// <summary>
    ///  Nasdaq Texas Closing Cross.
    /// </summary>
    NasdaqTexasClosingCross = (byte)'C',

    /// <summary>
    ///  Cross for IPO and halted / paused securities.
    /// </summary>
    CrossForIpoAndHaltedPaused = (byte)'H',

    /// <summary>
    ///  Nasdaq Texas Cross Network: Intraday Cross and Post Close Cross
    /// </summary>
    NasdaqTexasCrossNetwork = (byte)'I',
}
