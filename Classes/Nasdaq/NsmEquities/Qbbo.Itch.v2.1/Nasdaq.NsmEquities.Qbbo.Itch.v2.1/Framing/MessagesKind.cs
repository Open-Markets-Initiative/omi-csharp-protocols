namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Branch
/// </summary>
public enum MessagesKind : byte
{
    /// <summary>
    ///  Mold Udp64 heartbeat
    /// </summary>
    Heartbeat,

    /// <summary>
    ///  Mold Udp64 end of session
    /// </summary>
    EndOfSession,

    /// <summary>
    ///  Mold Udp64 Message
    /// </summary>
    Message,
}
