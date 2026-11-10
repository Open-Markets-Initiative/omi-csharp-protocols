namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file NasdaqNsmEquitiesTotalViewItchClientManager.Handlers.cs does for OnDebugPacket.
/// </remarks>
public sealed partial class NasdaqNsmEquitiesTotalViewItchClientManager : ClientPacketHandler
{
    /// <summary>
    ///  The position of the last message received, counted from 1.
    /// </summary>
    private int index;

    /// <summary>
    ///  Gets the number of packets received that parsed.
    /// </summary>
    public long Packets { get; private set; }

    /// <summary>
    ///  Gets the number of messages in the packets received that parsed.
    /// </summary>
    public long Messages { get; private set; }

    /// <summary>
    ///  Dispatches a parsed packet to the handler methods, with the position of each message in the order received.
    /// </summary>
    /// <remarks>
    ///  A packet that failed to parse is ignored. Call this method for each packet. Handle dispatches a packet without this check.
    /// </remarks>
    public void Receive(ClientPacket packet)
    {
        if (!packet.IsParsed)
        {
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        Handle(packet);
    }

    /// <inheritdoc/>
    protected override void OnDebugPacket(DebugPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.index;
        OnDebugPacket(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnLoginRequestPacket(LoginRequestPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.index;
        OnLoginRequestPacket(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnUnsequencedDataPacket(UnsequencedDataPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.index;
        OnUnsequencedDataPacket(message, this.index);
    }

    /// <inheritdoc/>
    protected override void OnUnknownClientMessage(UnknownClientMessage message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.index;
        OnUnknownClientMessage(message, this.index);
    }

    /// <summary>
    ///  Called for each Debug Packet.
    /// </summary>
    /// <param name="message">The received Debug Packet.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnDebugPacket(DebugPacket message, int index);

    /// <summary>
    ///  Called for each Login Request Packet.
    /// </summary>
    /// <param name="message">The received Login Request Packet.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnLoginRequestPacket(LoginRequestPacket message, int index);

    /// <summary>
    ///  Called for each Unsequenced Data Packet.
    /// </summary>
    /// <param name="message">The received Unsequenced Data Packet.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnUnsequencedDataPacket(UnsequencedDataPacket message, int index);

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="index">The position of the message in the order received, counted from 1. A nested message has the position of the message that contains it.</param>
    partial void OnUnknownClientMessage(UnknownClientMessage message, int index);

}
