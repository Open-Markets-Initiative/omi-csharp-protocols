namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

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
        if (frame.MessageData is { } messageDataReceived) Handle(messageDataReceived, frame);
    }

    private void Handle(IMessage message, MessageFrame frame)
    {
        switch (message)
        {
            case SystemEventMessage received:
                OnSystemEventMessage(received, frame);
                break;
            case SecurityDirectoryMessage received:
                OnSecurityDirectoryMessage(received, frame);
                break;
            case TradingStatusMessage received:
                OnTradingStatusMessage(received, frame);
                break;
            case RetailLiquidityIndicatorMessage received:
                OnRetailLiquidityIndicatorMessage(received, frame);
                break;
            case OperationalHaltStatusMessage received:
                OnOperationalHaltStatusMessage(received, frame);
                break;
            case ShortSalePriceTestStatusMessage received:
                OnShortSalePriceTestStatusMessage(received, frame);
                break;
            case SecurityEventMessage received:
                OnSecurityEventMessage(received, frame);
                break;
            case AddOrderMessage received:
                OnAddOrderMessage(received, frame);
                break;
            case OrderModifyMessage received:
                OnOrderModifyMessage(received, frame);
                break;
            case OrderDeleteMessage received:
                OnOrderDeleteMessage(received, frame);
                break;
            case OrderExecutedMessage received:
                OnOrderExecutedMessage(received, frame);
                break;
            case TradeMessage received:
                OnTradeMessage(received, frame);
                break;
            case TradeBreakMessage received:
                OnTradeBreakMessage(received, frame);
                break;
            case ClearBookMessage received:
                OnClearBookMessage(received, frame);
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
    ///  Called for each Security Directory Message.
    /// </summary>
    /// <param name="message">The received Security Directory Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSecurityDirectoryMessage(SecurityDirectoryMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trading Status Message.
    /// </summary>
    /// <param name="message">The received Trading Status Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradingStatusMessage(TradingStatusMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Retail Liquidity Indicator Message.
    /// </summary>
    /// <param name="message">The received Retail Liquidity Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Operational Halt Status Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Status Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Short Sale Price Test Status Message.
    /// </summary>
    /// <param name="message">The received Short Sale Price Test Status Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Security Event Message.
    /// </summary>
    /// <param name="message">The received Security Event Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnSecurityEventMessage(SecurityEventMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Add Order Message.
    /// </summary>
    /// <param name="message">The received Add Order Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAddOrderMessage(AddOrderMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Modify Message.
    /// </summary>
    /// <param name="message">The received Order Modify Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderModifyMessage(OrderModifyMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderDeleteMessage(OrderDeleteMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedMessage(OrderExecutedMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trade Message.
    /// </summary>
    /// <param name="message">The received Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeMessage(TradeMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Trade Break Message.
    /// </summary>
    /// <param name="message">The received Trade Break Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnTradeBreakMessage(TradeBreakMessage message, MessageFrame frame) { }

    /// <summary>
    ///  Called for each Clear Book Message.
    /// </summary>
    /// <param name="message">The received Clear Book Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnClearBookMessage(ClearBookMessage message, MessageFrame frame) { }

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
}
