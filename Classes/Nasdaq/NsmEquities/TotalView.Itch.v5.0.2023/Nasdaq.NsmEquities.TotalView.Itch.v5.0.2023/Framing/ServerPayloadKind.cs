namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Branch
/// </summary>
public enum ServerPayloadKind : byte
{
    /// <summary>
    ///  A message, which its type identifies.
    /// </summary>
    Messages,

    /// <summary>
    ///  SoupBinTCP server sends a heartbeat to indicate the connection is alive; carries no payload
    /// </summary>
    ServerHeartbeat,

    /// <summary>
    ///  Mold Udp64 end of session
    /// </summary>
    EndOfSession,
}
