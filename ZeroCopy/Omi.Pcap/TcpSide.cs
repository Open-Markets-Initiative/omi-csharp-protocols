namespace Omi.Pcap;

/// <summary>
///  Identifies the side of a TCP connection that sends on a <see cref="TcpFlow"/>.
/// </summary>
public enum TcpSide
{
    /// <summary>
    ///  No SYN of the connection was seen.
    /// </summary>
    Unknown,

    /// <summary>
    ///  The side that opened the connection with a SYN.
    /// </summary>
    Client,

    /// <summary>
    ///  The side that answered the SYN with a SYN-ACK.
    /// </summary>
    Server,
}
