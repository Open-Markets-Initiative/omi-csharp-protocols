namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Handles each Client Packet and the messages that it carries.
/// </summary>
public abstract class ClientPacketHandler
{
    /// <summary>
    ///  Gets the packet that the handler is handling.
    /// </summary>
    public ClientPacket Packet { get; private set; } = null!;

    /// <summary>
    ///  Calls the handler method for each message, and for each entry or packet of a kind that carries none.
    /// </summary>
    /// <remarks>
    ///  This method does not check whether the packet is valid.
    /// </remarks>
    public void Handle(ClientPacket packet)
    {
        Packet = packet;
        foreach (var entry in packet.ClientSoupBinTcpPacketFrames)
        {
            Visit(entry);
        }
    }

    private void Visit(ClientSoupBinTcpPacketFrame frame)
    {
        if (frame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeatPacket) OnClientHeartbeatPacket(frame);
        if (frame.ClientPayloadKind == ClientPayloadKind.LogoutRequestPacket) OnLogoutRequestPacket(frame);
        if (frame.ClientPayloadKind == ClientPayloadKind.Messages && frame.ClientPayload is { } clientPayloadReceived) Handle(clientPayloadReceived, frame);
    }

    private void Handle(IMessage message, ClientSoupBinTcpPacketFrame frame)
    {
        switch (message)
        {
            case DebugPacket received:
                OnDebugPacket(received, frame);
                break;
            case LoginRequestPacket received:
                OnLoginRequestPacket(received, frame);
                break;
            case UnsequencedDataPacket received:
                OnUnsequencedDataPacket(received, frame);
                break;
            case UnknownClientMessage received:
                OnUnknownClientMessage(received, frame);
                break;
        }
    }

    /// <summary>
    ///  Called for each Debug Packet.
    /// </summary>
    /// <param name="message">The received Debug Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnDebugPacket(DebugPacket message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Login Request Packet.
    /// </summary>
    /// <param name="message">The received Login Request Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnLoginRequestPacket(LoginRequestPacket message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Unsequenced Data Packet.
    /// </summary>
    /// <param name="message">The received Unsequenced Data Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnsequencedDataPacket(UnsequencedDataPacket message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownClientMessage(UnknownClientMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Client Heartbeat Packet.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnClientHeartbeatPacket(ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Logout Request Packet.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnLogoutRequestPacket(ClientSoupBinTcpPacketFrame frame) { }
}
