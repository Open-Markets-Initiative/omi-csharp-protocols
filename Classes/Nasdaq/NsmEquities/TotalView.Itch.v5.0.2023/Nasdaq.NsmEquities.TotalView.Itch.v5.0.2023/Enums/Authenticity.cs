namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Authenticity : byte
{
    /// <summary>
    ///  Live Production
    /// </summary>
    LiveProduction = (byte)'P',

    /// <summary>
    ///  Test
    /// </summary>
    Test = (byte)'T',
}
