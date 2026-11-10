namespace Iex.IexEquities.DeepPlus.IexTp.v1_01.PacketManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file IexIexEquitiesDeepPlusIexTpPacketManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class IexIexEquitiesDeepPlusIexTpPacketManager : PacketHandler
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
    private uint? session;

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
        var current = packet.Header.SessionId;
        if (this.session is { } previous && current != previous)
        {
            this.expectedSequence = null;
        }
        this.session = current;
        var first = packet.Header.FirstMessageSequenceNumber;
        if (this.expectedSequence is { } expected && first is { } && first != expected)
        {
            ++this.SequenceGaps;
            OnGap(expected, first);
        }
        this.sequence = first;
        this.expectedSequence = (ulong)(first + (ulong)packet.MessageCount);
        Handle(packet);
    }

    /// <inheritdoc/>
    protected override void OnSystemEventMessage(SystemEventMessage message, MessageFrame frame)
    {
        OnSystemEventMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSecurityDirectoryMessage(SecurityDirectoryMessage message, MessageFrame frame)
    {
        OnSecurityDirectoryMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradingStatusMessage(TradingStatusMessage message, MessageFrame frame)
    {
        OnTradingStatusMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage message, MessageFrame frame)
    {
        OnRetailLiquidityIndicatorMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage message, MessageFrame frame)
    {
        OnOperationalHaltStatusMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage message, MessageFrame frame)
    {
        OnShortSalePriceTestStatusMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSecurityEventMessage(SecurityEventMessage message, MessageFrame frame)
    {
        OnSecurityEventMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAddOrderMessage(AddOrderMessage message, MessageFrame frame)
    {
        OnAddOrderMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderModifyMessage(OrderModifyMessage message, MessageFrame frame)
    {
        OnOrderModifyMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderDeleteMessage(OrderDeleteMessage message, MessageFrame frame)
    {
        OnOrderDeleteMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, MessageFrame frame)
    {
        OnOrderExecutedMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeMessage(TradeMessage message, MessageFrame frame)
    {
        OnTradeMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeBreakMessage(TradeBreakMessage message, MessageFrame frame)
    {
        OnTradeBreakMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnClearBookMessage(ClearBookMessage message, MessageFrame frame)
    {
        OnClearBookMessage(message, this.sequence);
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
    ///  Called for each Security Directory Message.
    /// </summary>
    /// <param name="message">The received Security Directory Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnSecurityDirectoryMessage(SecurityDirectoryMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trading Status Message.
    /// </summary>
    /// <param name="message">The received Trading Status Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradingStatusMessage(TradingStatusMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Retail Liquidity Indicator Message.
    /// </summary>
    /// <param name="message">The received Retail Liquidity Indicator Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Operational Halt Status Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Status Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Short Sale Price Test Status Message.
    /// </summary>
    /// <param name="message">The received Short Sale Price Test Status Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Security Event Message.
    /// </summary>
    /// <param name="message">The received Security Event Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnSecurityEventMessage(SecurityEventMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Add Order Message.
    /// </summary>
    /// <param name="message">The received Add Order Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAddOrderMessage(AddOrderMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Modify Message.
    /// </summary>
    /// <param name="message">The received Order Modify Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderModifyMessage(OrderModifyMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Delete Message.
    /// </summary>
    /// <param name="message">The received Order Delete Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderDeleteMessage(OrderDeleteMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedMessage(OrderExecutedMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trade Message.
    /// </summary>
    /// <param name="message">The received Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradeMessage(TradeMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trade Break Message.
    /// </summary>
    /// <param name="message">The received Trade Break Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradeBreakMessage(TradeBreakMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Clear Book Message.
    /// </summary>
    /// <param name="message">The received Clear Book Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnClearBookMessage(ClearBookMessage message, ulong sequence);

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
