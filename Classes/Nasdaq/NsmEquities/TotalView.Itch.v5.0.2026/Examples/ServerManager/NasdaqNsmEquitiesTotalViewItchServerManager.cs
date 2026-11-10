namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  A message sets the next sequence number from its Accepted Sequence Number field. Until then, a sequenced message has no sequence number. Each later sequenced message has the next number. A message that has no sequence number has no partial method. Override its handler method to receive it. Implement the partial methods that you need in another file of this partial class, as file NasdaqNsmEquitiesTotalViewItchServerManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class NasdaqNsmEquitiesTotalViewItchServerManager : ServerPacketHandler
{
    /// <summary>
    ///  The sequence number of the next sequenced message, or null before a message sets it.
    /// </summary>
    private global::System.UInt128? sequence;

    /// <summary>
    ///  The sequence number of the message that contains the nested messages being handled, and of each nested message.
    /// </summary>
    private global::System.UInt128? parentSequence;

    /// <summary>
    ///  Gets the number of packets received that parsed.
    /// </summary>
    public long Packets { get; private set; }

    /// <summary>
    ///  Gets the number of messages in the packets received that parsed.
    /// </summary>
    public long Messages { get; private set; }

    /// <summary>
    ///  Dispatches a parsed packet to the handler methods, with the sequence number of each message.
    /// </summary>
    /// <remarks>
    ///  A packet that failed to parse is ignored. Call this method for each packet. Handle dispatches a packet without this check.
    /// </remarks>
    public void Receive(ServerPacket packet)
    {
        if (!packet.IsParsed)
        {
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        Handle(packet);
    }

    /// <summary>
    ///  Holds the sequence number of one connection.
    /// </summary>
    public sealed class Connection
    {
        /// <summary>
        ///  Gets or sets the number of the next sequenced message of the connection, or <see langword="null"/> before a message sets it.
        /// </summary>
        public global::System.UInt128? Sequence { get; set; }
    }

    /// <summary>
    ///  Receives a packet of one connection, with the sequence numbers of that connection.
    /// </summary>
    /// <remarks>
    ///  Use one Connection for each connection.
    /// </remarks>
    public void Receive(Connection connection, ServerPacket packet)
    {
        this.sequence = connection.Sequence;
        Receive(packet);
        connection.Sequence = this.sequence;
    }

    private readonly Dictionary<ushort, StockDirectoryMessage> stockDirectoryMessageByStockLocate = new();

    /// <summary>
    ///  Gets the Stock Directory Message last received with the specified Stock Locate.
    /// </summary>
    /// <param name="stockLocate">The Stock Locate to look up.</param>
    /// <param name="record">When this method returns, the Stock Directory Message last received with that key, or <see langword="null"/> if none was received.</param>
    /// <returns><see langword="true"/> if a message was received with that key; otherwise, <see langword="false"/>.</returns>
    public bool TryGetStockDirectoryMessage(ushort stockLocate, [global::System.Diagnostics.CodeAnalysis.NotNullWhen(true)] out StockDirectoryMessage? record)
        => this.stockDirectoryMessageByStockLocate.TryGetValue(stockLocate, out record);

    /// <inheritdoc/>
    protected override void OnLoginAcceptedPacket(LoginAcceptedPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        if (global::System.UInt128.TryParse(message.AcceptedSequenceNumber, global::System.Globalization.NumberStyles.Integer, global::System.Globalization.CultureInfo.InvariantCulture, out var number))
        {
            this.sequence = number;
        }
        OnLoginAcceptedPacket(message, this.sequence);
    }

    /// <inheritdoc/>
    protected override void OnSequencedDataPacket(SequencedDataPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        OnSequencedDataPacket(message, this.sequence);
        this.parentSequence = this.sequence;
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSystemEventMessage(SystemEventMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnSystemEventMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnStockDirectoryMessage(StockDirectoryMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        this.stockDirectoryMessageByStockLocate[message.StockLocate] = message;
        OnStockDirectoryMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnStockTradingActionMessage(StockTradingActionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnStockTradingActionMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnRegShoShortSalePriceTestRestrictedIndicatorMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnMarketParticipantPositionMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnMwcbDeclineLevelMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnMwcbStatusLevelMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, ServerSoupBinTcpPacketFrame frame)
    {
        OnIpoQuotingPeriodUpdate(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnLuldAuctionCollarMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltMessage(OperationalHaltMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOperationalHaltMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnAddOrderNoMpidAttributionMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnAddOrderWithMpidAttributionMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderExecutedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderExecutedWithPriceMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderCancelMessage(OrderCancelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderCancelMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderDeleteMessage(OrderDeleteMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderDeleteMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderReplaceMessage(OrderReplaceMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderReplaceMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnNonCrossTradeMessage(NonCrossTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnNonCrossTradeMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCrossTradeMessage(CrossTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnCrossTradeMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnBrokenTradeMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnNetOrderImbalanceIndicatorMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnRetailPriceImprovementIndicatorMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnDirectListingWithCapitalRaisePriceDiscoveryMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnUnknownSequencedMessage(UnknownSequencedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnUnknownSequencedMessage(message, this.parentSequence);
    }

    /// <summary>
    ///  Called for each Login Accepted Packet.
    /// </summary>
    /// <param name="message">The received Login Accepted Packet.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnLoginAcceptedPacket(LoginAcceptedPacket message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Sequenced Data Packet.
    /// </summary>
    /// <param name="message">The received Sequenced Data Packet.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnSequencedDataPacket(SequencedDataPacket message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each System Event Message.
    /// </summary>
    /// <param name="message">The received System Event Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnSystemEventMessage(SystemEventMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Stock Directory Message.
    /// </summary>
    /// <param name="message">The received Stock Directory Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStockDirectoryMessage(StockDirectoryMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Stock Trading Action Message.
    /// </summary>
    /// <param name="message">The received Stock Trading Action Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStockTradingActionMessage(StockTradingActionMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Reg Sho Short Sale Price Test Restricted Indicator Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Short Sale Price Test Restricted Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Market Participant Position Message.
    /// </summary>
    /// <param name="message">The received Market Participant Position Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Mwcb Status Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Status Level Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Ipo Quoting Period Update.
    /// </summary>
    /// <param name="message">The received Ipo Quoting Period Update.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Luld Auction Collar Message.
    /// </summary>
    /// <param name="message">The received Luld Auction Collar Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOperationalHaltMessage(OperationalHaltMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Add Order No Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order No Mpid Attribution Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Add Order With Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order With Mpid Attribution Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedMessage(OrderExecutedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Executed With Price Message.
    /// </summary>
    /// <param name="message">The received Order Executed With Price Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Cancel Message.
    /// </summary>
    /// <param name="message">The received Order Cancel Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderCancelMessage(OrderCancelMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderDeleteMessage(OrderDeleteMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Replace Message.
    /// </summary>
    /// <param name="message">The received Order Replace Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderReplaceMessage(OrderReplaceMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Non Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Non Cross Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnNonCrossTradeMessage(NonCrossTradeMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Cross Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnCrossTradeMessage(CrossTradeMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnBrokenTradeMessage(BrokenTradeMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Net Order Imbalance Indicator Message.
    /// </summary>
    /// <param name="message">The received Net Order Imbalance Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Retail Price Improvement Indicator Message.
    /// </summary>
    /// <param name="message">The received Retail Price Improvement Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Direct Listing With Capital Raise Price Discovery Message.
    /// </summary>
    /// <param name="message">The received Direct Listing With Capital Raise Price Discovery Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnUnknownSequencedMessage(UnknownSequencedMessage message, global::System.UInt128? sequence);

}
