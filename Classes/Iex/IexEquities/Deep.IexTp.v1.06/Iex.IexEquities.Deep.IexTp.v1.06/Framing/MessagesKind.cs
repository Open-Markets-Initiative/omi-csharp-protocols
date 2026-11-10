namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Branch
/// </summary>
public enum MessagesKind : byte
{
    /// <summary>
    ///  IexTp heartbeat packet
    /// </summary>
    Heartbeat,

    /// <summary>
    ///  IexTp message
    /// </summary>
    Message,
}
