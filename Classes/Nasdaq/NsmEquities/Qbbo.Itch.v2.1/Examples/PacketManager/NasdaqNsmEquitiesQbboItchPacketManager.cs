namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file NasdaqNsmEquitiesQbboItchPacketManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class NasdaqNsmEquitiesQbboItchPacketManager : PacketHandler
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

    /// <inheritdoc/>
    protected override void OnSystemEventMessage(SystemEventMessage message, MessageFrame frame)
    {
        OnSystemEventMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStockDirectoryMessage(StockDirectoryMessage message, MessageFrame frame)
    {
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
    protected override void OnRegShoRestrictionMessage(RegShoRestrictionMessage message, MessageFrame frame)
    {
        OnRegShoRestrictionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, MessageFrame frame)
    {
        OnMwcbDeclineLevelMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbBreachMessage(MwcbBreachMessage message, MessageFrame frame)
    {
        OnMwcbBreachMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltMessage(OperationalHaltMessage message, MessageFrame frame)
    {
        OnOperationalHaltMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnBboQuotationMessage(BboQuotationMessage message, MessageFrame frame)
    {
        OnBboQuotationMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnPriceImprovementMessage(PriceImprovementMessage message, MessageFrame frame)
    {
        OnPriceImprovementMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnIpoQuotingPeriodUpdateMessage(IpoQuotingPeriodUpdateMessage message, MessageFrame frame)
    {
        OnIpoQuotingPeriodUpdateMessage(message, this.sequence);
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
    ///  Called for each Reg Sho Restriction Message.
    /// </summary>
    /// <param name="message">The received Reg Sho Restriction Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRegShoRestrictionMessage(RegShoRestrictionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Mwcb Decline Level Message.
    /// </summary>
    /// <param name="message">The received Mwcb Decline Level Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Mwcb Breach Message.
    /// </summary>
    /// <param name="message">The received Mwcb Breach Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMwcbBreachMessage(MwcbBreachMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Operational Halt Message.
    /// </summary>
    /// <param name="message">The received Operational Halt Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOperationalHaltMessage(OperationalHaltMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Bbo Quotation Message.
    /// </summary>
    /// <param name="message">The received Bbo Quotation Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnBboQuotationMessage(BboQuotationMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Price Improvement Message.
    /// </summary>
    /// <param name="message">The received Price Improvement Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnPriceImprovementMessage(PriceImprovementMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Ipo Quoting Period Update Message.
    /// </summary>
    /// <param name="message">The received Ipo Quoting Period Update Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnIpoQuotingPeriodUpdateMessage(IpoQuotingPeriodUpdateMessage message, ulong sequence);

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
