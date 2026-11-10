namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Handles each Packet and the messages that it carries.
/// </summary>
public abstract class PacketHandler
{
    /// <summary>
    ///  Gets the packet that the handler is handling.
    /// </summary>
    public Packet Packet { get; private set; } = null!;

    /// <summary>
    ///  Calls the handler method for each message, and for each entry or packet of a kind that carries none.
    /// </summary>
    /// <remarks>
    ///  This method does not check whether the packet is valid.
    /// </remarks>
    public void Handle(Packet packet)
    {
        Packet = packet;
        if (packet.MessagesKind == MessagesKind.Heartbeat) OnHeartbeat(packet);
        if (packet.MessagesKind == MessagesKind.EndOfSession) OnEndOfSession(packet);
        if (packet.MessagesKind == MessagesKind.Message)
        {
            foreach (var entry in packet.MessageFrames)
            {
                Visit(entry);
            }
        }
    }

    private void Visit(MessageFrame frame)
    {
        if (frame.Payload is { } payloadReceived) Handle(payloadReceived, frame);
    }

    private void Handle(IMessage message, MessageFrame frame)
    {
        switch (message)
        {
            case SystemEventMessage received:
                OnSystemEventMessage(received, frame);
                break;
            case ComplexStrategyDirectoryMessage received:
                OnComplexStrategyDirectoryMessage(received, frame);
                break;
            case StrategyTradingActionMessage received:
                OnStrategyTradingActionMessage(received, frame);
                break;
            case StrategyOpenClosedMessage received:
                OnStrategyOpenClosedMessage(received, frame);
                break;
            case ComplexStrategyOrderOnBookMessage received:
                OnComplexStrategyOrderOnBookMessage(received, frame);
                break;
            case ComplexStrategyAuctionMessage received:
                OnComplexStrategyAuctionMessage(received, frame);
                break;
            case UnknownPacketMessage received:
                OnUnknownPacketMessage(received, frame);
                break;
        }
    }

    /// <summary>
    ///  Called for each System Event Message.
    /// </summary>
    /// <param name="message">The received System Event Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSystemEventMessage(SystemEventMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Complex Strategy Directory Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Directory Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnComplexStrategyDirectoryMessage(ComplexStrategyDirectoryMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Strategy Trading Action Message.
    /// </summary>
    /// <param name="message">The received Strategy Trading Action Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStrategyTradingActionMessage(StrategyTradingActionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Strategy Open Closed Message.
    /// </summary>
    /// <param name="message">The received Strategy Open Closed Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStrategyOpenClosedMessage(StrategyOpenClosedMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Complex Strategy Order On Book Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Order On Book Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnComplexStrategyOrderOnBookMessage(ComplexStrategyOrderOnBookMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Complex Strategy Auction Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Auction Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnComplexStrategyAuctionMessage(ComplexStrategyAuctionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnUnknownPacketMessage(UnknownPacketMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Heartbeat.
    /// </summary>
    /// <param name="packet">The received packet.</param>
    protected virtual void OnHeartbeat(Packet packet) { }

    /// <summary>
    ///  Called for each End Of Session.
    /// </summary>
    /// <param name="packet">The received packet.</param>
    protected virtual void OnEndOfSession(Packet packet) { }
}
