namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

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
    ///  SoupBinTCP server signals end of session; client may now disconnect. Carries no payload
    /// </summary>
    EndOfSession,
}
