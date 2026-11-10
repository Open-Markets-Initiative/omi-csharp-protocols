namespace Iex.IexEquities.Tops.IexTp.v1_64;

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
