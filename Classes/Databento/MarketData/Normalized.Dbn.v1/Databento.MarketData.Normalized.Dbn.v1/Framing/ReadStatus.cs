namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Specifies why reading the messages of a packet stopped.
/// </summary>
public enum ReadStatus
{
    /// <summary>
    ///  No error stopped reading.
    /// </summary>
    Complete,
    /// <summary>
    ///  The packet ended before a message, or a length or type field that precedes it, was complete.
    /// </summary>
    Truncated,
    /// <summary>
    ///  A message length was zero, or smaller than the bytes that precede the message payload. The message count was too large to hold. The occurrences of a repeated element did not fill its payload. The occurrences of a repeated element took no bytes.
    /// </summary>
    InvalidLength,
    /// <summary>
    ///  The packet contains an enclosing message type that the protocol does not define.
    /// </summary>
    UnknownEnclosingType,
    /// <summary>
    ///  The packet declares a message count and contains bytes after the last message.
    /// </summary>
    TrailingBytes,
}
