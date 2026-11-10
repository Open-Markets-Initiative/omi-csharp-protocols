namespace Iex.IexEquities.Tops.IexTp.v1_56.PacketManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  Implement the partial methods that you need in another file of this partial class, as file IexIexEquitiesTopsIexTpPacketManager.Handlers.cs does for OnQuoteUpdateMessage.
/// </remarks>
public sealed partial class IexIexEquitiesTopsIexTpPacketManager : PacketHandler
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
    protected override void OnQuoteUpdateMessage(QuoteUpdateMessage message, MessageFrame frame)
    {
        OnQuoteUpdateMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeReportMessage(TradeReportMessage message, MessageFrame frame)
    {
        OnTradeReportMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeBreakMessage(TradeBreakMessage message, MessageFrame frame)
    {
        OnTradeBreakMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnUnknownPacketMessage(UnknownPacketMessage message, MessageFrame frame)
    {
        OnUnknownPacketMessage(message, this.sequence);
        ++this.sequence;
    }

    /// <summary>
    ///  Called for each Quote Update Message.
    /// </summary>
    /// <param name="message">The received Quote Update Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnQuoteUpdateMessage(QuoteUpdateMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trade Report Message.
    /// </summary>
    /// <param name="message">The received Trade Report Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradeReportMessage(TradeReportMessage message, ulong sequence);

    /// <summary>
    ///  Called for each Trade Break Message.
    /// </summary>
    /// <param name="message">The received Trade Break Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnTradeBreakMessage(TradeBreakMessage message, ulong sequence);

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
