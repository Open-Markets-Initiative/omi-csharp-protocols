namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Represents a message contained in Packet, identified by its message type.
/// </summary>
public interface IPacketMessage : IMessage
{
    /// <summary>
    ///  Gets the message type.
    /// </summary>
    MessageCode DataValue { get; }
}
