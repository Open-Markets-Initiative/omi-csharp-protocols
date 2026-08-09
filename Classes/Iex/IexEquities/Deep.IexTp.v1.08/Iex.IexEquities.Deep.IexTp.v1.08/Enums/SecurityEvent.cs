namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Security event identifier
/// </summary>
/// <remarks>
///  Each value corresponds to the ASCII byte value used as the wire-format discriminator.
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
