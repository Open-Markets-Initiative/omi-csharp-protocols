namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Branch
/// </summary>
public enum ClientPayloadKind : byte
{
    /// <summary>
    ///  A message, which its type identifies.
    /// </summary>
    Messages,

    /// <summary>
    ///  SoupBinTCP client sends a heartbeat to indicate the connection is alive; carries no payload
    /// </summary>
    ClientHeartbeatPacket,

    /// <summary>
    ///  SoupBinTCP client requests session logout; server completes any in-flight messages then closes. Carries no payload
    /// </summary>
    LogoutRequestPacket,
}
