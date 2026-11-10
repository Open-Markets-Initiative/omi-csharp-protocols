namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Handles each Server Packet and the messages that it carries.
/// </summary>
public abstract class ServerPacketHandler
{
    /// <summary>
    ///  Gets the packet that the handler is handling.
    /// </summary>
    public ServerPacket Packet { get; private set; } = null!;

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
    public void Handle(ServerPacket packet)
    {
        Packet = packet;
        foreach (var entry in packet.ServerSoupBinTcpPacketFrames)
        {
            Visit(entry);
        }
    }

    private void Visit(ServerSoupBinTcpPacketFrame frame)
    {
        if (frame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeat) OnServerHeartbeat(frame);
        if (frame.ServerPayloadKind == ServerPayloadKind.EndOfSession) OnEndOfSession(frame);
        if (frame.ServerPayloadKind == ServerPayloadKind.Messages && frame.ServerPayload is { } serverPayloadReceived) Handle(serverPayloadReceived, frame);
    }

    private void Handle(IMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        switch (message)
        {
            case DebugPacket received:
                OnDebugPacket(received, frame);
                break;
            case LoginAcceptedPacket received:
                OnLoginAcceptedPacket(received, frame);
                break;
            case LoginRejectedPacket received:
                OnLoginRejectedPacket(received, frame);
                break;
            case SequencedDataPacket received:
                OnSequencedDataPacket(received, frame);
                {
                    var outer = this.Parent;
                    this.Parent = received;
                    try
                    {
                        this.Handle(received.SequencedMessage, frame);
                    }
                    finally
                    {
                        this.Parent = outer;
                    }
                }
                break;
            case SystemEventMessage received:
                OnSystemEventMessage(received, frame);
                break;
            case OrderAcceptedMessage received:
                OnOrderAcceptedMessage(received, frame);
                break;
            case ReplacedMessage received:
                OnReplacedMessage(received, frame);
                break;
            case CanceledMessage received:
                OnCanceledMessage(received, frame);
                break;
            case AiqCanceledMessage received:
                OnAiqCanceledMessage(received, frame);
                break;
            case OrderExecutedMessage received:
                OnOrderExecutedMessage(received, frame);
                break;
            case BrokenTradeMessage received:
                OnBrokenTradeMessage(received, frame);
                break;
            case RejectedOrderMessage received:
                OnRejectedOrderMessage(received, frame);
                break;
            case CancelPendingMessage received:
                OnCancelPendingMessage(received, frame);
                break;
            case CancelRejectMessage received:
                OnCancelRejectMessage(received, frame);
                break;
            case OrderPriorityUpdateMessage received:
                OnOrderPriorityUpdateMessage(received, frame);
                break;
            case OrderModifiedMessage received:
                OnOrderModifiedMessage(received, frame);
                break;
            case OrderRestatedMessage received:
                OnOrderRestatedMessage(received, frame);
                break;
            case AccountQueryResponseMessage received:
                OnAccountQueryResponseMessage(received, frame);
                break;
            case MassCancelResponseMessage received:
                OnMassCancelResponseMessage(received, frame);
                break;
            case DisableOrderEntryResponseMessage received:
                OnDisableOrderEntryResponseMessage(received, frame);
                break;
            case EnableOrderEntryResponseMessage received:
                OnEnableOrderEntryResponseMessage(received, frame);
                break;
            case UnknownSequencedMessage received:
                OnUnknownSequencedMessage(received, frame);
                break;
            case UnknownServerMessage received:
                OnUnknownServerMessage(received, frame);
                break;
        }
    }

    /// <summary>
    ///  Called for each Debug Packet.
    /// </summary>
    /// <param name="message">The received Debug Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnDebugPacket(DebugPacket message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Login Accepted Packet.
    /// </summary>
    /// <param name="message">The received Login Accepted Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnLoginAcceptedPacket(LoginAcceptedPacket message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Login Rejected Packet.
    /// </summary>
    /// <param name="message">The received Login Rejected Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnLoginRejectedPacket(LoginRejectedPacket message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Sequenced Data Packet.
    /// </summary>
    /// <param name="message">The received Sequenced Data Packet.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSequencedDataPacket(SequencedDataPacket message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each System Event Message.
    /// </summary>
    /// <param name="message">The received System Event Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSystemEventMessage(SystemEventMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Accepted Message.
    /// </summary>
    /// <param name="message">The received Order Accepted Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderAcceptedMessage(OrderAcceptedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Replaced Message.
    /// </summary>
    /// <param name="message">The received Replaced Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnReplacedMessage(ReplacedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Canceled Message.
    /// </summary>
    /// <param name="message">The received Canceled Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCanceledMessage(CanceledMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Aiq Canceled Message.
    /// </summary>
    /// <param name="message">The received Aiq Canceled Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAiqCanceledMessage(AiqCanceledMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Rejected Order Message.
    /// </summary>
    /// <param name="message">The received Rejected Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRejectedOrderMessage(RejectedOrderMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Cancel Pending Message.
    /// </summary>
    /// <param name="message">The received Cancel Pending Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCancelPendingMessage(CancelPendingMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Cancel Reject Message.
    /// </summary>
    /// <param name="message">The received Cancel Reject Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCancelRejectMessage(CancelRejectMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Priority Update Message.
    /// </summary>
    /// <param name="message">The received Order Priority Update Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderPriorityUpdateMessage(OrderPriorityUpdateMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Modified Message.
    /// </summary>
    /// <param name="message">The received Order Modified Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderModifiedMessage(OrderModifiedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Restated Message.
    /// </summary>
    /// <param name="message">The received Order Restated Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderRestatedMessage(OrderRestatedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Account Query Response Message.
    /// </summary>
    /// <param name="message">The received Account Query Response Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAccountQueryResponseMessage(AccountQueryResponseMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Mass Cancel Response Message.
    /// </summary>
    /// <param name="message">The received Mass Cancel Response Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMassCancelResponseMessage(MassCancelResponseMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Disable Order Entry Response Message.
    /// </summary>
    /// <param name="message">The received Disable Order Entry Response Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnDisableOrderEntryResponseMessage(DisableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Enable Order Entry Response Message.
    /// </summary>
    /// <param name="message">The received Enable Order Entry Response Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnEnableOrderEntryResponseMessage(EnableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownSequencedMessage(UnknownSequencedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownServerMessage(UnknownServerMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Server Heartbeat.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnServerHeartbeat(ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each End Of Session.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnEndOfSession(ServerSoupBinTcpPacketFrame frame) { }
}
