namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates the quoting participant's registration status in relation to SEC Rules 101 and 104 of Regulation M
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketMakerMode : byte
{
    /// <summary>
    ///  Normal
    /// </summary>
    Normal = (byte)'N',

    /// <summary>
    ///  Passive
    /// </summary>
    Passive = (byte)'P',

    /// <summary>
    ///  Syndicate
    /// </summary>
    Syndicate = (byte)'S',

    /// <summary>
    ///  Pre-Syndicate
    /// </summary>
    PreSyndicate = (byte)'R',

    /// <summary>
    ///  Penalty
    /// </summary>
    Penalty = (byte)'L',
}
