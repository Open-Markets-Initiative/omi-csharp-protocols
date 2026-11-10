namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

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
    ///  Gets the message that contains the message being handled, or null when no message contains it.
    /// </summary>
    protected IMessage? Parent { get; private set; }

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
        if (frame.ClientPayloadKind == ClientPayloadKind.ClientHeartbeat) OnClientHeartbeat(frame);
        if (frame.ClientPayloadKind == ClientPayloadKind.LogoutRequest) OnLogoutRequest(frame);
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
                {
                    var outer = this.Parent;
                    this.Parent = received;
                    try
                    {
                        this.Handle(received.UnsequencedMessage, frame);
                    }
                    finally
                    {
                        this.Parent = outer;
                    }
                }
                break;
            case EnterOrderMessage received:
                OnEnterOrderMessage(received, frame);
                break;
            case ReplaceOrderMessage received:
                OnReplaceOrderMessage(received, frame);
                break;
            case CancelOrderMessage received:
                OnCancelOrderMessage(received, frame);
                break;
            case ModifyOrderMessage received:
                OnModifyOrderMessage(received, frame);
                break;
            case AccountQueryMessage received:
                OnAccountQueryMessage(received, frame);
                break;
            case MassCancelRequestMessage received:
                OnMassCancelRequestMessage(received, frame);
                break;
            case DisableOrderEntryRequestMessage received:
                OnDisableOrderEntryRequestMessage(received, frame);
                break;
            case EnableOrderEntryRequestMessage received:
                OnEnableOrderEntryRequestMessage(received, frame);
                break;
            case UnknownUnsequencedMessage received:
                OnUnknownUnsequencedMessage(received, frame);
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
    ///  Called for each Enter Order Message.
    /// </summary>
    /// <param name="message">The received Enter Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnEnterOrderMessage(EnterOrderMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Replace Order Message.
    /// </summary>
    /// <param name="message">The received Replace Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnReplaceOrderMessage(ReplaceOrderMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Cancel Order Message.
    /// </summary>
    /// <param name="message">The received Cancel Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCancelOrderMessage(CancelOrderMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Modify Order Message.
    /// </summary>
    /// <param name="message">The received Modify Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnModifyOrderMessage(ModifyOrderMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Account Query Message.
    /// </summary>
    /// <param name="message">The received Account Query Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAccountQueryMessage(AccountQueryMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Mass Cancel Request Message.
    /// </summary>
    /// <param name="message">The received Mass Cancel Request Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMassCancelRequestMessage(MassCancelRequestMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Disable Order Entry Request Message.
    /// </summary>
    /// <param name="message">The received Disable Order Entry Request Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnDisableOrderEntryRequestMessage(DisableOrderEntryRequestMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Enable Order Entry Request Message.
    /// </summary>
    /// <param name="message">The received Enable Order Entry Request Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnEnableOrderEntryRequestMessage(EnableOrderEntryRequestMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownUnsequencedMessage(UnknownUnsequencedMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownClientMessage(UnknownClientMessage message, ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Client Heartbeat.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnClientHeartbeat(ClientSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Logout Request.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnLogoutRequest(ClientSoupBinTcpPacketFrame frame) { }
}
