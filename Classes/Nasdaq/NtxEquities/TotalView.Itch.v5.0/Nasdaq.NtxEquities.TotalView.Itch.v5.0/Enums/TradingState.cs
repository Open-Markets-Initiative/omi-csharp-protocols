namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the current trading state for the stock.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum TradingState : byte
{
    /// <summary>
    ///  Halted across all U.S. equity markets / SROs
    /// </summary>
    Halted = (byte)'H',

    /// <summary>
    ///  Paused across all U.S. equity markets / SROs (Nasdaq-listed securities only)
    /// </summary>
    Paused = (byte)'P',

    /// <summary>
    ///  Quotation only period for cross-SRO halt or pause
    /// </summary>
    QuotationOnly = (byte)'Q',

    /// <summary>
    ///  Trading on Nasdaq Texas
    /// </summary>
    Trading = (byte)'T',
}
