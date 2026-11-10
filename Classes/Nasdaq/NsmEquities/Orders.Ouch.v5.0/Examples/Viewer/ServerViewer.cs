namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Viewer;

/// <summary>
///  Handles parsed packets and writes every field of each message to the console.
/// </summary>
public sealed class ServerViewer : ServerPacketHandler
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
    ///  The index of the last packet received.
    /// </summary>
    private int packetIndex;

    /// <summary>
    ///  The index of the last message received.
    /// </summary>
    private int messageIndex;

    /// <summary>
    ///  Gets the number of packets received that parsed.
    /// </summary>
    public long Packets { get; private set; }

    /// <summary>
    ///  Gets the number of messages in the packets received that parsed.
    /// </summary>
    public long Messages { get; private set; }

    /// <summary>
    ///  Writes the packet's decoded contents or reports that it did not parse.
    /// </summary>
    public void Receive(ServerPacket packet)
    {
        ++this.packetIndex;
        if (!packet.IsParsed)
        {
            Console.WriteLine($"[packet={packetIndex}] packet did not parse");
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        Console.WriteLine($"[packet={packetIndex}] valid={packet.IsValid} messages={packet.Messages.Count}");
        foreach (var (index, message) in packet.Messages.Index())
        {
            if (message is UnknownServerMessage unknown)
                Console.WriteLine($"[packet={packetIndex}] WARNING: message {messageIndex + index + 1} has the unknown message type 0x{unknown.DataValue:X}");
        }

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

    /// <summary>
    ///  The sequence numbers of each TCP flow, separate from those of the other flows.
    /// </summary>
    private readonly Dictionary<TcpFlow, Connection> connections = new();

    /// <summary>
    ///  Receives a packet of one TCP flow, with the sequence numbers of that flow.
    /// </summary>
    public void Receive(TcpFlow flow, ServerPacket packet)
    {
        if (!this.connections.TryGetValue(flow, out var connection))
        {
            connection = new Connection();
            this.connections.Add(flow, connection);
        }
        Receive(connection, packet);
    }

    /// <summary>
    ///  Releases the sequence numbers of a TCP flow that has ended.
    /// </summary>
    public void Release(TcpFlow flow) => this.connections.Remove(flow);

    /// <inheritdoc/>
    protected override void OnDebugPacket(DebugPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpDebugPacket(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnLoginAcceptedPacket(LoginAcceptedPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        if (global::System.UInt128.TryParse(message.AcceptedSequenceNumber, global::System.Globalization.NumberStyles.Integer, global::System.Globalization.CultureInfo.InvariantCulture, out var number))
        {
            this.sequence = number;
        }
        DumpLoginAcceptedPacket(message, this.messageIndex, this.sequence);
    }

    /// <inheritdoc/>
    protected override void OnLoginRejectedPacket(LoginRejectedPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpLoginRejectedPacket(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnSequencedDataPacket(SequencedDataPacket message, ServerSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpSequencedDataPacket(message, this.messageIndex, this.sequence);
        this.parentSequence = this.sequence;
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSystemEventMessage(SystemEventMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpSystemEventMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderAcceptedMessage(OrderAcceptedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderAcceptedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnReplacedMessage(ReplacedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpReplacedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCanceledMessage(CanceledMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpCanceledMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAiqCanceledMessage(AiqCanceledMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpAiqCanceledMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderExecutedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpBrokenTradeMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRejectedOrderMessage(RejectedOrderMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpRejectedOrderMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCancelPendingMessage(CancelPendingMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpCancelPendingMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCancelRejectMessage(CancelRejectMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpCancelRejectMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderPriorityUpdateMessage(OrderPriorityUpdateMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderPriorityUpdateMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderModifiedMessage(OrderModifiedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderModifiedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderRestatedMessage(OrderRestatedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderRestatedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAccountQueryResponseMessage(AccountQueryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpAccountQueryResponseMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMassCancelResponseMessage(MassCancelResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpMassCancelResponseMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnDisableOrderEntryResponseMessage(DisableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpDisableOrderEntryResponseMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnEnableOrderEntryResponseMessage(EnableOrderEntryResponseMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpEnableOrderEntryResponseMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnUnknownSequencedMessage(UnknownSequencedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpUnknownSequencedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnUnknownServerMessage(UnknownServerMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpUnknownServerMessage(message, this.messageIndex);
    }

    /// <summary>
    ///  Writes each field of the Account Query Response Message to the console.
    /// </summary>
    void DumpAccountQueryResponseMessage(AccountQueryResponseMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] AccountQueryResponseMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Aiq Canceled Message to the console.
    /// </summary>
    void DumpAiqCanceledMessage(AiqCanceledMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] AiqCanceledMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Broken Trade Message to the console.
    /// </summary>
    void DumpBrokenTradeMessage(BrokenTradeMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] BrokenTradeMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Cancel Pending Message to the console.
    /// </summary>
    void DumpCancelPendingMessage(CancelPendingMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] CancelPendingMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Cancel Reject Message to the console.
    /// </summary>
    void DumpCancelRejectMessage(CancelRejectMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] CancelRejectMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Canceled Message to the console.
    /// </summary>
    void DumpCanceledMessage(CanceledMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] CanceledMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Debug Packet to the console.
    /// </summary>
    void DumpDebugPacket(DebugPacket message, int index)
    {
        Console.WriteLine($"[message={index}] DebugPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Disable Order Entry Response Message to the console.
    /// </summary>
    void DumpDisableOrderEntryResponseMessage(DisableOrderEntryResponseMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] DisableOrderEntryResponseMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Enable Order Entry Response Message to the console.
    /// </summary>
    void DumpEnableOrderEntryResponseMessage(EnableOrderEntryResponseMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] EnableOrderEntryResponseMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Login Accepted Packet to the console.
    /// </summary>
    void DumpLoginAcceptedPacket(LoginAcceptedPacket message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] LoginAcceptedPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Login Rejected Packet to the console.
    /// </summary>
    void DumpLoginRejectedPacket(LoginRejectedPacket message, int index)
    {
        Console.WriteLine($"[message={index}] LoginRejectedPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Mass Cancel Response Message to the console.
    /// </summary>
    void DumpMassCancelResponseMessage(MassCancelResponseMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] MassCancelResponseMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Order Accepted Message to the console.
    /// </summary>
    void DumpOrderAcceptedMessage(OrderAcceptedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderAcceptedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Order Executed Message to the console.
    /// </summary>
    void DumpOrderExecutedMessage(OrderExecutedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderExecutedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Order Modified Message to the console.
    /// </summary>
    void DumpOrderModifiedMessage(OrderModifiedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderModifiedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Order Priority Update Message to the console.
    /// </summary>
    void DumpOrderPriorityUpdateMessage(OrderPriorityUpdateMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderPriorityUpdateMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Order Restated Message to the console.
    /// </summary>
    void DumpOrderRestatedMessage(OrderRestatedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderRestatedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Rejected Order Message to the console.
    /// </summary>
    void DumpRejectedOrderMessage(RejectedOrderMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] RejectedOrderMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Replaced Message to the console.
    /// </summary>
    void DumpReplacedMessage(ReplacedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] ReplacedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Sequenced Data Packet to the console.
    /// </summary>
    void DumpSequencedDataPacket(SequencedDataPacket message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SequencedDataPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the System Event Message to the console.
    /// </summary>
    void DumpSystemEventMessage(SystemEventMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SystemEventMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of a message whose message type the protocol does not define to the console.
    /// </summary>
    void DumpUnknownSequencedMessage(UnknownSequencedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] UnknownSequencedMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        Console.WriteLine($"  Bytes: {Convert.ToHexString(message.Bytes.Span)}");
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of a message whose message type the protocol does not define to the console.
    /// </summary>
    void DumpUnknownServerMessage(UnknownServerMessage message, int index)
    {
        Console.WriteLine($"[message={index}] UnknownServerMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        Console.WriteLine($"  Bytes: {Convert.ToHexString(message.Bytes.Span)}");
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    static void WriteFields(IEnumerable<FieldView> fields, string indent, bool writeNested)
    {
        foreach (var field in fields)
        {
            if (field.Message is { } message)
            {
                Console.WriteLine($"{indent}{field.Name}: {message.GetType().Name}");
                if (writeNested)
                {
                    WriteFields(message.Fields, indent + "  ", true);
                    switch (message)
                    {
                        case UnknownClientMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownUnsequencedMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownServerMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownSequencedMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                    }
                }
                continue;
            }
            if (field.Messages is { } messages)
            {
                Console.WriteLine($"{indent}{field.Name}:");
                for (var index = 0; index < messages.Count; index++)
                {
                    Console.WriteLine($"{indent}  [{index}] {messages[index].GetType().Name}");
                    WriteFields(messages[index].Fields, indent + "    ", true);
                }
                continue;
            }
            if (field.Entries is not { } entries)
            {
                Console.WriteLine($"{indent}{field.Name}: {field.Text}");
                continue;
            }
            Console.WriteLine($"{indent}{field.Name}:");
            for (var index = 0; index < entries.Count; index++)
            {
                Console.WriteLine($"{indent}  [{index}]");
                WriteFields(entries[index].Fields, indent + "    ", true);
            }
        }
    }
}
