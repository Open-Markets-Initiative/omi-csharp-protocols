namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

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
            case StockDirectoryMessage received:
                OnStockDirectoryMessage(received, frame);
                break;
            case StockTradingActionMessage received:
                OnStockTradingActionMessage(received, frame);
                break;
            case RegShoRestrictionMessage received:
                OnRegShoRestrictionMessage(received, frame);
                break;
            case MwcbDeclineLevelMessage received:
                OnMwcbDeclineLevelMessage(received, frame);
                break;
            case MwcbBreachMessage received:
                OnMwcbBreachMessage(received, frame);
                break;
            case OperationalHaltMessage received:
                OnOperationalHaltMessage(received, frame);
                break;
            case BboQuotationMessage received:
                OnBboQuotationMessage(received, frame);
                break;
            case PriceImprovementMessage received:
                OnPriceImprovementMessage(received, frame);
                break;
            case IpoQuotingPeriodUpdateMessage received:
                OnIpoQuotingPeriodUpdateMessage(received, frame);
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
    ///  Called for each Stock Directory Message.
    /// </summary>
    /// <param name="message">The received Stock Directory Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStockDirectoryMessage(StockDirectoryMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Stock Trading Action Message.
    /// </summary>
    /// <param name="message">The received Stock Trading Action Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStockTradingActionMessage(StockTradingActionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Reg Sho Restriction Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Restriction Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRegShoRestrictionMessage(RegShoRestrictionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Breach Message.
    /// </summary>
    /// <param name="message">The received Mwcb Breach Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbBreachMessage(MwcbBreachMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOperationalHaltMessage(OperationalHaltMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Bbo Quotation Message.
    /// </summary>
    /// <param name="message">The received Bbo Quotation Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnBboQuotationMessage(BboQuotationMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Price Improvement Message.
    /// </summary>
    /// <param name="message">The received Price Improvement Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnPriceImprovementMessage(PriceImprovementMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Ipo Quoting Period Update Message.
    /// </summary>
    /// <param name="message">The received Ipo Quoting Period Update Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnIpoQuotingPeriodUpdateMessage(IpoQuotingPeriodUpdateMessage message, MessageFrame frame) { }

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
