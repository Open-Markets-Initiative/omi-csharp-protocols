namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the quoting participant's registration status in relation to SEC Rules 101 and 104 of Regulation M
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MarketMakerMode : byte
{
    /// <summary>
    ///  normal
    /// </summary>
    Normal = (byte)'N',

    /// <summary>
    ///  passive
    /// </summary>
    Passive = (byte)'P',

    /// <summary>
    ///  syndicate
    /// </summary>
    Syndicate = (byte)'S',

    /// <summary>
    ///  pre-syndicate
    /// </summary>
    PreSyndicate = (byte)'R',

    /// <summary>
    ///  penalty
    /// </summary>
    Penalty = (byte)'L',
}
