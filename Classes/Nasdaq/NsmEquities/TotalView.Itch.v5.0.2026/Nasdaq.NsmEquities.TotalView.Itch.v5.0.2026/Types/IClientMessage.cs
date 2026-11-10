namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Represents a message contained in Client Packet, identified by its message type.
/// </summary>
public interface IClientMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    ClientMessageCode DataValue { get; }
}
