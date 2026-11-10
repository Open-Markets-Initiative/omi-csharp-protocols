namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Reflects the current trading state for the issue.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CurrentTradingState : byte
{
    /// <summary>
    ///  Halted/Paused on Nasdaq and all UTP participants
    /// </summary>
    HaltedPaused = (byte)'H',

    /// <summary>
    ///  Quotation only period for cross-SRO halt or pause
    /// </summary>
    QuotationOnly = (byte)'Q',

    /// <summary>
    ///  Paused across all U.S. equity markets / SROs (Nasdaq-listed securities only)
    /// </summary>
    Paused = (byte)'P',

    /// <summary>
    ///  Trading on Nasdaq Market
    /// </summary>
    Trading = (byte)'T',
}
