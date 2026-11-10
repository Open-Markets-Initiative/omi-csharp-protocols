namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

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
    ServerHeartbeatPacket,

    /// <summary>
    ///  SoupBinTCP server signals end of session; client may now disconnect. Carries no payload
    /// </summary>
    EndOfSessionPacket,
}
