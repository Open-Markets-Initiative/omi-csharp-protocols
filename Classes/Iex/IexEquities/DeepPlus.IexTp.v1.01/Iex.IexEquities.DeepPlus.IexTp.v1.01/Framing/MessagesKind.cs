namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

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
