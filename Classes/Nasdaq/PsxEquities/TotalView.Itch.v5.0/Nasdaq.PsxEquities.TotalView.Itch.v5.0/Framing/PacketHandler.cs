namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

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
            case RegShoShortSalePriceTestRestrictedIndicatorMessage received:
                OnRegShoShortSalePriceTestRestrictedIndicatorMessage(received, frame);
                break;
            case MarketParticipantPositionMessage received:
                OnMarketParticipantPositionMessage(received, frame);
                break;
            case MwcbDeclineLevelMessage received:
                OnMwcbDeclineLevelMessage(received, frame);
                break;
            case MwcbStatusLevelMessage received:
                OnMwcbStatusLevelMessage(received, frame);
                break;
            case LuldAuctionCollarMessage received:
                OnLuldAuctionCollarMessage(received, frame);
                break;
            case OperationalHaltMessage received:
                OnOperationalHaltMessage(received, frame);
                break;
            case AddOrderNoMpidAttributionMessage received:
                OnAddOrderNoMpidAttributionMessage(received, frame);
                break;
            case AddOrderWithMpidAttributionMessage received:
                OnAddOrderWithMpidAttributionMessage(received, frame);
                break;
            case OrderExecutedMessage received:
                OnOrderExecutedMessage(received, frame);
                break;
            case OrderExecutedWithPriceMessage received:
                OnOrderExecutedWithPriceMessage(received, frame);
                break;
            case OrderCancelMessage received:
                OnOrderCancelMessage(received, frame);
                break;
            case OrderDeleteMessage received:
                OnOrderDeleteMessage(received, frame);
                break;
            case OrderReplaceMessage received:
                OnOrderReplaceMessage(received, frame);
                break;
            case TradeMessageNonCross received:
                OnTradeMessageNonCross(received, frame);
                break;
            case CrossTradeMessage received:
                OnCrossTradeMessage(received, frame);
                break;
            case BrokenTradeMessage received:
                OnBrokenTradeMessage(received, frame);
                break;
            case NetOrderImbalanceIndicatorMessage received:
                OnNetOrderImbalanceIndicatorMessage(received, frame);
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
    ///  Called for each Reg Sho Short Sale Price Test Restricted Indicator Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Short Sale Price Test Restricted Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Market Participant Position Message.
    /// </summary>
    /// <param name="message">The received Market Participant Position Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Status Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Status Level Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Luld Auction Collar Message.
    /// </summary>
    /// <param name="message">The received Luld Auction Collar Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOperationalHaltMessage(OperationalHaltMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Add Order No Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order No Mpid Attribution Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Add Order With Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order With Mpid Attribution Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedMessage(OrderExecutedMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed With Price Message.
    /// </summary>
    /// <param name="message">The received Order Executed With Price Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Cancel Message.
    /// </summary>
    /// <param name="message">The received Order Cancel Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderCancelMessage(OrderCancelMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderDeleteMessage(OrderDeleteMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Replace Message.
    /// </summary>
    /// <param name="message">The received Order Replace Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderReplaceMessage(OrderReplaceMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trade Message Non Cross.
    /// </summary>
    /// <param name="message">The received Trade Message Non Cross.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeMessageNonCross(TradeMessageNonCross message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Cross Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCrossTradeMessage(CrossTradeMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnBrokenTradeMessage(BrokenTradeMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Net Order Imbalance Indicator Message.
    /// </summary>
    /// <param name="message">The received Net Order Imbalance Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, MessageFrame frame) { }

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
