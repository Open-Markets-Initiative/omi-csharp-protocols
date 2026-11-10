namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file NasdaqIseOptionsOrderComboFeedItchPacketManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class NasdaqIseOptionsOrderComboFeedItchPacketManager : PacketHandler
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
    protected override void OnComplexStrategyDirectoryMessage(ComplexStrategyDirectoryMessage message, MessageFrame frame)
    {
        OnComplexStrategyDirectoryMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStrategyTradingActionMessage(StrategyTradingActionMessage message, MessageFrame frame)
    {
        OnStrategyTradingActionMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStrategyOpenClosedMessage(StrategyOpenClosedMessage message, MessageFrame frame)
    {
        OnStrategyOpenClosedMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnComplexStrategyOrderOnBookMessage(ComplexStrategyOrderOnBookMessage message, MessageFrame frame)
    {
        OnComplexStrategyOrderOnBookMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnComplexStrategyAuctionMessage(ComplexStrategyAuctionMessage message, MessageFrame frame)
    {
        OnComplexStrategyAuctionMessage(message, this.sequence);
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
    ///  Called for each Complex Strategy Directory Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Directory Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnComplexStrategyDirectoryMessage(ComplexStrategyDirectoryMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Strategy Trading Action Message.
    /// </summary>
    /// <param name="message">The received Strategy Trading Action Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStrategyTradingActionMessage(StrategyTradingActionMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Strategy Open Closed Message.
    /// </summary>
    /// <param name="message">The received Strategy Open Closed Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnStrategyOpenClosedMessage(StrategyOpenClosedMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Complex Strategy Order On Book Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Order On Book Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnComplexStrategyOrderOnBookMessage(ComplexStrategyOrderOnBookMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Complex Strategy Auction Message.
    /// </summary>
    /// <param name="message">The received Complex Strategy Auction Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnComplexStrategyAuctionMessage(ComplexStrategyAuctionMessage message, ulong sequence);

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
