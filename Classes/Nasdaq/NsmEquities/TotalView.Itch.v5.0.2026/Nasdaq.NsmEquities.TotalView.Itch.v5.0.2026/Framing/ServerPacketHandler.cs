namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

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
        if (frame.ServerPayloadKind == ServerPayloadKind.ServerHeartbeatPacket) OnServerHeartbeatPacket(frame);
        if (frame.ServerPayloadKind == ServerPayloadKind.EndOfSessionPacket) OnEndOfSessionPacket(frame);
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
            case IpoQuotingPeriodUpdate received:
                OnIpoQuotingPeriodUpdate(received, frame);
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
            case NonCrossTradeMessage received:
                OnNonCrossTradeMessage(received, frame);
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
            case RetailPriceImprovementIndicatorMessage received:
                OnRetailPriceImprovementIndicatorMessage(received, frame);
                break;
            case DirectListingWithCapitalRaisePriceDiscoveryMessage received:
                OnDirectListingWithCapitalRaisePriceDiscoveryMessage(received, frame);
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
    ///  Called for each Stock Directory Message.
    /// </summary>
    /// <param name="message">The received Stock Directory Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStockDirectoryMessage(StockDirectoryMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Stock Trading Action Message.
    /// </summary>
    /// <param name="message">The received Stock Trading Action Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnStockTradingActionMessage(StockTradingActionMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Reg Sho Short Sale Price Test Restricted Indicator Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Short Sale Price Test Restricted Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Market Participant Position Message.
    /// </summary>
    /// <param name="message">The received Market Participant Position Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Mwcb Status Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Status Level Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Ipo Quoting Period Update.
    /// </summary>
    /// <param name="message">The received Ipo Quoting Period Update.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Luld Auction Collar Message.
    /// </summary>
    /// <param name="message">The received Luld Auction Collar Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOperationalHaltMessage(OperationalHaltMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Add Order No Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order No Mpid Attribution Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Add Order With Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order With Mpid Attribution Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Executed With Price Message.
    /// </summary>
    /// <param name="message">The received Order Executed With Price Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Cancel Message.
    /// </summary>
    /// <param name="message">The received Order Cancel Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderCancelMessage(OrderCancelMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderDeleteMessage(OrderDeleteMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Order Replace Message.
    /// </summary>
    /// <param name="message">The received Order Replace Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnOrderReplaceMessage(OrderReplaceMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Non Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Non Cross Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnNonCrossTradeMessage(NonCrossTradeMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Cross Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnCrossTradeMessage(CrossTradeMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Net Order Imbalance Indicator Message.
    /// </summary>
    /// <param name="message">The received Net Order Imbalance Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Retail Price Improvement Indicator Message.
    /// </summary>
    /// <param name="message">The received Retail Price Improvement Indicator Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each Direct Listing With Capital Raise Price Discovery Message.
    /// </summary>
    /// <param name="message">The received Direct Listing With Capital Raise Price Discovery Message.</param>
    /// <param name="frame">The entry that carries the message.</param>
    protected virtual void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, ServerSoupBinTcpPacketFrame frame) { }

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
    ///  Called for each Server Heartbeat Packet.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnServerHeartbeatPacket(ServerSoupBinTcpPacketFrame frame) { }

    /// <summary>
    ///  Called for each End Of Session Packet.
    /// </summary>
    /// <param name="frame">The received entry.</param>
    protected virtual void OnEndOfSessionPacket(ServerSoupBinTcpPacketFrame frame) { }
}
