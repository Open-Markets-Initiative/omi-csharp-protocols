namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0.PacketManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file NasdaqPsxEquitiesTotalViewItchPacketManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class NasdaqPsxEquitiesTotalViewItchPacketManager : PacketHandler
{
    /// <summary>
    ///  The sequence number of the next message.
    /// </summary>
    private ulong sequence;

    /// <summary>
    ///  The sequence number at which the next packet starts, or null before the first packet.
    /// </summary>
    private ulong? expectedSequence;

    /// <summary>
    ///  The session of the last packet, or null before the first packet.
    /// </summary>
    private string? session;

    /// <summary>
    ///  Gets the number of packets received that parsed.
    /// </summary>
    public long Packets { get; private set; }

    /// <summary>
    ///  Gets the number of messages in the packets received that parsed.
    /// </summary>
    public long Messages { get; private set; }

    /// <summary>
    ///  Gets the number of packets received that did not start at the expected sequence number.
    /// </summary>
    public long SequenceGaps { get; private set; }

    /// <summary>
    ///  Dispatches a parsed packet to the handler methods, with the sequence number of each message.
    /// </summary>
    /// <remarks>
    ///  A packet that failed to parse is ignored. Call this method for each packet. Handle dispatches a packet without this check and without assigning sequence numbers. When the session in the header changes, sequence numbering restarts at this packet and no gap is reported.
    /// </remarks>
    public void Receive(Packet packet)
    {
        if (!packet.IsParsed)
        {
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        var current = packet.Header.Session;
        if (this.session is { } previous && current != previous)
        {
            this.expectedSequence = null;
        }
        this.session = current;
        var first = packet.Header.SequenceNumber;
        if (this.expectedSequence is { } expected && first is { } && first != expected)
        {
            ++this.SequenceGaps;
            OnGap(expected, first);
        }
        this.sequence = first;
        this.expectedSequence = (ulong)(first + (ulong)packet.MessageCount);
        Handle(packet);
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
    protected override void OnSystemEventMessage(SystemEventMessage message, MessageFrame frame)
    {
        OnSystemEventMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStockDirectoryMessage(StockDirectoryMessage message, MessageFrame frame)
    {
        this.stockDirectoryMessageByStockLocate[message.StockLocate] = message;
        OnStockDirectoryMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStockTradingActionMessage(StockTradingActionMessage message, MessageFrame frame)
    {
        OnStockTradingActionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, MessageFrame frame)
    {
        OnRegShoShortSalePriceTestRestrictedIndicatorMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, MessageFrame frame)
    {
        OnMarketParticipantPositionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, MessageFrame frame)
    {
        OnMwcbDeclineLevelMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, MessageFrame frame)
    {
        OnMwcbStatusLevelMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, MessageFrame frame)
    {
        OnLuldAuctionCollarMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltMessage(OperationalHaltMessage message, MessageFrame frame)
    {
        OnOperationalHaltMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, MessageFrame frame)
    {
        OnAddOrderNoMpidAttributionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, MessageFrame frame)
    {
        OnAddOrderWithMpidAttributionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, MessageFrame frame)
    {
        OnOrderExecutedMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, MessageFrame frame)
    {
        OnOrderExecutedWithPriceMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderCancelMessage(OrderCancelMessage message, MessageFrame frame)
    {
        OnOrderCancelMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderDeleteMessage(OrderDeleteMessage message, MessageFrame frame)
    {
        OnOrderDeleteMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderReplaceMessage(OrderReplaceMessage message, MessageFrame frame)
    {
        OnOrderReplaceMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeMessageNonCross(TradeMessageNonCross message, MessageFrame frame)
    {
        OnTradeMessageNonCross(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnCrossTradeMessage(CrossTradeMessage message, MessageFrame frame)
    {
        OnCrossTradeMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, MessageFrame frame)
    {
        OnBrokenTradeMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, MessageFrame frame)
    {
        OnNetOrderImbalanceIndicatorMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnUnknownPacketMessage(UnknownPacketMessage message, MessageFrame frame)
    {
        OnUnknownPacketMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <summary>
    ///  Called for each System Event Message.
    /// </summary>
    /// <param name="message">The received System Event Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Stock Directory Message.
    /// </summary>
    /// <param name="message">The received Stock Directory Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStockDirectoryMessage(StockDirectoryMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Stock Trading Action Message.
    /// </summary>
    /// <param name="message">The received Stock Trading Action Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStockTradingActionMessage(StockTradingActionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Reg Sho Short Sale Price Test Restricted Indicator Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Short Sale Price Test Restricted Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Market Participant Position Message.
    /// </summary>
    /// <param name="message">The received Market Participant Position Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Mwcb Status Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Status Level Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Luld Auction Collar Message.
    /// </summary>
    /// <param name="message">The received Luld Auction Collar Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOperationalHaltMessage(OperationalHaltMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Add Order No Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order No Mpid Attribution Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Add Order With Mpid Attribution Message.
    /// </summary>
    /// <param name="message">The received Add Order With Mpid Attribution Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedMessage(OrderExecutedMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Executed With Price Message.
    /// </summary>
    /// <param name="message">The received Order Executed With Price Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Cancel Message.
    /// </summary>
    /// <param name="message">The received Order Cancel Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderCancelMessage(OrderCancelMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderDeleteMessage(OrderDeleteMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Replace Message.
    /// </summary>
    /// <param name="message">The received Order Replace Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderReplaceMessage(OrderReplaceMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trade Message Non Cross.
    /// </summary>
    /// <param name="message">The received Trade Message Non Cross.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradeMessageNonCross(TradeMessageNonCross message, ulong sequence);

    /// <summary>
    ///  Called for each Cross Trade Message.
    /// </summary>
    /// <param name="message">The received Cross Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnCrossTradeMessage(CrossTradeMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnBrokenTradeMessage(BrokenTradeMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Net Order Imbalance Indicator Message.
    /// </summary>
    /// <param name="message">The received Net Order Imbalance Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, ulong sequence);

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnUnknownPacketMessage(UnknownPacketMessage message, ulong sequence);

    /// <summary>
    ///  Called when a packet does not start at the sequence number expected after the previous packet.
    /// </summary>
    /// <param name="expected">The sequence number expected after the last message received.</param>
    /// <param name="received">The sequence number of the first message of the packet.</param>
    partial void OnGap(ulong expected, ulong received);
}
