namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Represents a message contained in Server Packet, identified by its message type.
/// </summary>
public interface IServerMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    ServerMessageCode DataValue { get; }
}
