namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerManager;

/// <summary>
///  Handles parsed packets, assigns a number to each message, and calls the partial method for its type.
/// </summary>
/// <remarks>
///  A message sets the next sequence number from its Accepted Sequence Number field. Until then, a sequenced message has no sequence number. Each later sequenced message has the next number. A message that has no sequence number has no partial method. Override its handler method to receive it. Implement the partial methods that you need in another file of this partial class, as file NasdaqNsmEquitiesOrdersOuchServerManager.Handlers.cs does for OnSystemEventMessage.
/// </remarks>
public sealed partial class NasdaqNsmEquitiesOrdersOuchServerManager : ServerPacketHandler
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
    protected override void OnOrderAcceptedMessage(OrderAcceptedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderAcceptedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnReplacedMessage(ReplacedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnReplacedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCanceledMessage(CanceledMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnCanceledMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAiqCanceledMessage(AiqCanceledMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnAiqCanceledMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderExecutedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnBrokenTradeMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRejectedOrderMessage(RejectedOrderMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnRejectedOrderMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCancelPendingMessage(CancelPendingMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnCancelPendingMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCancelRejectMessage(CancelRejectMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnCancelRejectMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderPriorityUpdateMessage(OrderPriorityUpdateMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderPriorityUpdateMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderModifiedMessage(OrderModifiedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderModifiedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderRestatedMessage(OrderRestatedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnOrderRestatedMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAccountQueryResponseMessage(AccountQueryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnAccountQueryResponseMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMassCancelResponseMessage(MassCancelResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnMassCancelResponseMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnDisableOrderEntryResponseMessage(DisableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnDisableOrderEntryResponseMessage(message, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnEnableOrderEntryResponseMessage(EnableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        OnEnableOrderEntryResponseMessage(message, this.parentSequence);
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
    ///  Called for each Order Accepted Message.
    /// </summary>
    /// <param name="message">The received Order Accepted Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderAcceptedMessage(OrderAcceptedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Replaced Message.
    /// </summary>
    /// <param name="message">The received Replaced Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnReplacedMessage(ReplacedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Canceled Message.
    /// </summary>
    /// <param name="message">The received Canceled Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnCanceledMessage(CanceledMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Aiq Canceled Message.
    /// </summary>
    /// <param name="message">The received Aiq Canceled Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAiqCanceledMessage(AiqCanceledMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Executed Message.
    /// </summary>
    /// <param name="message">The received Order Executed Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderExecutedMessage(OrderExecutedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Broken Trade Message.
    /// </summary>
    /// <param name="message">The received Broken Trade Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnBrokenTradeMessage(BrokenTradeMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Rejected Order Message.
    /// </summary>
    /// <param name="message">The received Rejected Order Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnRejectedOrderMessage(RejectedOrderMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Cancel Pending Message.
    /// </summary>
    /// <param name="message">The received Cancel Pending Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnCancelPendingMessage(CancelPendingMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Cancel Reject Message.
    /// </summary>
    /// <param name="message">The received Cancel Reject Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnCancelRejectMessage(CancelRejectMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Priority Update Message.
    /// </summary>
    /// <param name="message">The received Order Priority Update Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderPriorityUpdateMessage(OrderPriorityUpdateMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Modified Message.
    /// </summary>
    /// <param name="message">The received Order Modified Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderModifiedMessage(OrderModifiedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Order Restated Message.
    /// </summary>
    /// <param name="message">The received Order Restated Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnOrderRestatedMessage(OrderRestatedMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Account Query Response Message.
    /// </summary>
    /// <param name="message">The received Account Query Response Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnAccountQueryResponseMessage(AccountQueryResponseMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Mass Cancel Response Message.
    /// </summary>
    /// <param name="message">The received Mass Cancel Response Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnMassCancelResponseMessage(MassCancelResponseMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Disable Order Entry Response Message.
    /// </summary>
    /// <param name="message">The received Disable Order Entry Response Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnDisableOrderEntryResponseMessage(DisableOrderEntryResponseMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for each Enable Order Entry Response Message.
    /// </summary>
    /// <param name="message">The received Enable Order Entry Response Message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnEnableOrderEntryResponseMessage(EnableOrderEntryResponseMessage message, global::System.UInt128? sequence);

    /// <summary>
    ///  Called for a message whose message type the protocol does not define.
    /// </summary>
    /// <param name="message">The received message.</param>
    /// <param name="sequence">The sequence number of the message. A nested message has the number of the message that contains it.</param>
    partial void OnUnknownSequencedMessage(UnknownSequencedMessage message, global::System.UInt128? sequence);

}
