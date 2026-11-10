namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Viewer;

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
    protected override void OnStockDirectoryMessage(StockDirectoryMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        this.stockDirectoryMessageByStockLocate[message.StockLocate] = message;
        DumpStockDirectoryMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnStockTradingActionMessage(StockTradingActionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpStockTradingActionMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpRegShoShortSalePriceTestRestrictedIndicatorMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpMarketParticipantPositionMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpMwcbDeclineLevelMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpMwcbStatusLevelMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpIpoQuotingPeriodUpdate(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpLuldAuctionCollarMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltMessage(OperationalHaltMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOperationalHaltMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpAddOrderNoMpidAttributionMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpAddOrderWithMpidAttributionMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderExecutedMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderExecutedWithPriceMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderCancelMessage(OrderCancelMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderCancelMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderDeleteMessage(OrderDeleteMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderDeleteMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnOrderReplaceMessage(OrderReplaceMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpOrderReplaceMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnNonCrossTradeMessage(NonCrossTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpNonCrossTradeMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnCrossTradeMessage(CrossTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpCrossTradeMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpBrokenTradeMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpNetOrderImbalanceIndicatorMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpRetailPriceImprovementIndicatorMessage(message, this.messageIndex, this.parentSequence);
    }

    /// <inheritdoc/>
    protected override void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, ServerSoupBinTcpPacketFrame frame)
    {
        DumpDirectListingWithCapitalRaisePriceDiscoveryMessage(message, this.messageIndex, this.parentSequence);
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
    ///  Writes each field of the Add Order No Mpid Attribution Message to the console.
    /// </summary>
    void DumpAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] AddOrderNoMpidAttributionMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteAddOrderNoMpidAttributionMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Add Order With Mpid Attribution Message to the console.
    /// </summary>
    void DumpAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] AddOrderWithMpidAttributionMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteAddOrderWithMpidAttributionMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Broken Trade Message to the console.
    /// </summary>
    void DumpBrokenTradeMessage(BrokenTradeMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] BrokenTradeMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteBrokenTradeMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteBrokenTradeMessage(BrokenTradeMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Cross Trade Message to the console.
    /// </summary>
    void DumpCrossTradeMessage(CrossTradeMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] CrossTradeMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteCrossTradeMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteCrossTradeMessage(CrossTradeMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
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
    ///  Writes each field of the Direct Listing With Capital Raise Price Discovery Message to the console.
    /// </summary>
    void DumpDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] DirectListingWithCapitalRaisePriceDiscoveryMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteDirectListingWithCapitalRaisePriceDiscoveryMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Ipo Quoting Period Update to the console.
    /// </summary>
    void DumpIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] IpoQuotingPeriodUpdate");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteIpoQuotingPeriodUpdate(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
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
    ///  Writes each field of the Luld Auction Collar Message to the console.
    /// </summary>
    void DumpLuldAuctionCollarMessage(LuldAuctionCollarMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] LuldAuctionCollarMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteLuldAuctionCollarMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteLuldAuctionCollarMessage(LuldAuctionCollarMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Market Participant Position Message to the console.
    /// </summary>
    void DumpMarketParticipantPositionMessage(MarketParticipantPositionMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] MarketParticipantPositionMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteMarketParticipantPositionMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteMarketParticipantPositionMessage(MarketParticipantPositionMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Mwcb Decline Level Message to the console.
    /// </summary>
    void DumpMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] MwcbDeclineLevelMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteMwcbDeclineLevelMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Mwcb Status Level Message to the console.
    /// </summary>
    void DumpMwcbStatusLevelMessage(MwcbStatusLevelMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] MwcbStatusLevelMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteMwcbStatusLevelMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteMwcbStatusLevelMessage(MwcbStatusLevelMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Net Order Imbalance Indicator Message to the console.
    /// </summary>
    void DumpNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] NetOrderImbalanceIndicatorMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteNetOrderImbalanceIndicatorMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Non Cross Trade Message to the console.
    /// </summary>
    void DumpNonCrossTradeMessage(NonCrossTradeMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] NonCrossTradeMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteNonCrossTradeMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteNonCrossTradeMessage(NonCrossTradeMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Operational Halt Message to the console.
    /// </summary>
    void DumpOperationalHaltMessage(OperationalHaltMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OperationalHaltMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOperationalHaltMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOperationalHaltMessage(OperationalHaltMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Order Cancel Message to the console.
    /// </summary>
    void DumpOrderCancelMessage(OrderCancelMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderCancelMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOrderCancelMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOrderCancelMessage(OrderCancelMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Order Delete Message to the console.
    /// </summary>
    void DumpOrderDeleteMessage(OrderDeleteMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderDeleteMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOrderDeleteMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOrderDeleteMessage(OrderDeleteMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Order Executed Message to the console.
    /// </summary>
    void DumpOrderExecutedMessage(OrderExecutedMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderExecutedMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOrderExecutedMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOrderExecutedMessage(OrderExecutedMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Order Executed With Price Message to the console.
    /// </summary>
    void DumpOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderExecutedWithPriceMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOrderExecutedWithPriceMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Order Replace Message to the console.
    /// </summary>
    void DumpOrderReplaceMessage(OrderReplaceMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OrderReplaceMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteOrderReplaceMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteOrderReplaceMessage(OrderReplaceMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Reg Sho Short Sale Price Test Restricted Indicator Message to the console.
    /// </summary>
    void DumpRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] RegShoShortSalePriceTestRestrictedIndicatorMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteRegShoShortSalePriceTestRestrictedIndicatorMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.LocateCode.Descriptor && TryGetStockDirectoryMessage(message.LocateCode, out var locateCodeRecord))
            {
                return $" ({locateCodeRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the Retail Price Improvement Indicator Message to the console.
    /// </summary>
    void DumpRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] RetailPriceImprovementIndicatorMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteRetailPriceImprovementIndicatorMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
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
    ///  Writes each field of the Stock Directory Message to the console.
    /// </summary>
    void DumpStockDirectoryMessage(StockDirectoryMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] StockDirectoryMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Stock Trading Action Message to the console.
    /// </summary>
    void DumpStockTradingActionMessage(StockTradingActionMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] StockTradingActionMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteStockTradingActionMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteStockTradingActionMessage(StockTradingActionMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
    }

    /// <summary>
    ///  Writes each field of the System Event Message to the console.
    /// </summary>
    void DumpSystemEventMessage(SystemEventMessage message, int index, global::System.UInt128? sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SystemEventMessage");
        WriteFields(message.Fields, "  ", writeNested: false, field => NoteSystemEventMessage(message, field));
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    string NoteSystemEventMessage(SystemEventMessage message, FieldView field)
    {
        {
            if (field.Descriptor == message.Fields.StockLocate.Descriptor && TryGetStockDirectoryMessage(message.StockLocate, out var stockLocateRecord))
            {
                return $" ({stockLocateRecord.Stock})";
            }
        }
        return string.Empty;
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

    static void WriteFields(IEnumerable<FieldView> fields, string indent, bool writeNested, Func<FieldView, string>? note = null)
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
                        case UnknownServerMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownSequencedMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownPacketMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownFileMessage typed:
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
                Console.WriteLine($"{indent}{field.Name}: {field.Text}{note?.Invoke(field)}");
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
