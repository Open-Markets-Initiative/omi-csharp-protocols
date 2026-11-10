namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Security event identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityEvent : byte
{
    /// <summary>
    ///  Opening Process Complete
    /// </summary>
    OpeningProcessComplete = (byte)'O',

    /// <summary>
    ///  Closing Process Complete
    /// </summary>
    ClosingProcessComplete = (byte)'C',
}
