namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state. Please note that firms should only show live issues and quoting participants on public quotation displays.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Authenticity : byte
{
    /// <summary>
    ///  Live/Production
    /// </summary>
    LiveProduction = (byte)'P',

    /// <summary>
    ///  Test
    /// </summary>
    Test = (byte)'T',
}
