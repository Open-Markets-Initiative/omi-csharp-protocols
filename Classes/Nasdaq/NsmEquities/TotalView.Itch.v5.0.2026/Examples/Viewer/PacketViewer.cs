namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Viewer;

/// <summary>
///  Handles parsed packets and writes every field of each message to the console.
/// </summary>
public sealed class PacketViewer : PacketHandler
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
    public void Receive(Packet packet)
    {
        ++this.packetIndex;
        if (!packet.IsParsed)
        {
            Console.WriteLine($"[packet={packetIndex}] packet did not parse");
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
            Console.WriteLine($"[packet={packetIndex}] gap: expected sequence {expected}, received sequence {first}");
        }
        this.sequence = first;
        this.expectedSequence = (ulong)(first + (ulong)packet.MessageCount);
        Console.WriteLine($"[packet={packetIndex} sequence={first}] valid={packet.IsValid} count={packet.MessageCount} messages={packet.Messages.Count}");
        WriteFields(packet.Header.Fields, "  ", writeNested: true);
        foreach (var (index, message) in packet.Messages.Index())
        {
            if (message is UnknownPacketMessage unknown)
                Console.WriteLine($"[packet={packetIndex} sequence={first}] WARNING: message {messageIndex + index + 1} has the unknown message type 0x{unknown.DataValue:X}");
        }

        Handle(packet);
        if (!packet.Trailer.IsEmpty)
            Console.WriteLine($"[packet={packetIndex} sequence={first}] Trailer: {Convert.ToHexString(packet.Trailer.Span)}");
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
        ++this.messageIndex;
        DumpSystemEventMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStockDirectoryMessage(StockDirectoryMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        this.stockDirectoryMessageByStockLocate[message.StockLocate] = message;
        DumpStockDirectoryMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnStockTradingActionMessage(StockTradingActionMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpStockTradingActionMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpRegShoShortSalePriceTestRestrictedIndicatorMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMarketParticipantPositionMessage(MarketParticipantPositionMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpMarketParticipantPositionMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpMwcbDeclineLevelMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnMwcbStatusLevelMessage(MwcbStatusLevelMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpMwcbStatusLevelMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpIpoQuotingPeriodUpdate(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnLuldAuctionCollarMessage(LuldAuctionCollarMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpLuldAuctionCollarMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltMessage(OperationalHaltMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOperationalHaltMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpAddOrderNoMpidAttributionMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpAddOrderWithMpidAttributionMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedMessage(OrderExecutedMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOrderExecutedMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOrderExecutedWithPriceMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderCancelMessage(OrderCancelMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOrderCancelMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderDeleteMessage(OrderDeleteMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOrderDeleteMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOrderReplaceMessage(OrderReplaceMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOrderReplaceMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnNonCrossTradeMessage(NonCrossTradeMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpNonCrossTradeMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnCrossTradeMessage(CrossTradeMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpCrossTradeMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnBrokenTradeMessage(BrokenTradeMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpBrokenTradeMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpNetOrderImbalanceIndicatorMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpRetailPriceImprovementIndicatorMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpDirectListingWithCapitalRaisePriceDiscoveryMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnUnknownPacketMessage(UnknownPacketMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpUnknownPacketMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <summary>
    ///  Writes each field of the Add Order No Mpid Attribution Message to the console.
    /// </summary>
    void DumpAddOrderNoMpidAttributionMessage(AddOrderNoMpidAttributionMessage message, int index, ulong sequence)
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
    void DumpAddOrderWithMpidAttributionMessage(AddOrderWithMpidAttributionMessage message, int index, ulong sequence)
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
    void DumpBrokenTradeMessage(BrokenTradeMessage message, int index, ulong sequence)
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
    void DumpCrossTradeMessage(CrossTradeMessage message, int index, ulong sequence)
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
    ///  Writes each field of the Direct Listing With Capital Raise Price Discovery Message to the console.
    /// </summary>
    void DumpDirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage message, int index, ulong sequence)
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
    void DumpIpoQuotingPeriodUpdate(IpoQuotingPeriodUpdate message, int index, ulong sequence)
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
    ///  Writes each field of the Luld Auction Collar Message to the console.
    /// </summary>
    void DumpLuldAuctionCollarMessage(LuldAuctionCollarMessage message, int index, ulong sequence)
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
    void DumpMarketParticipantPositionMessage(MarketParticipantPositionMessage message, int index, ulong sequence)
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
    void DumpMwcbDeclineLevelMessage(MwcbDeclineLevelMessage message, int index, ulong sequence)
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
    void DumpMwcbStatusLevelMessage(MwcbStatusLevelMessage message, int index, ulong sequence)
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
    void DumpNetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage message, int index, ulong sequence)
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
    void DumpNonCrossTradeMessage(NonCrossTradeMessage message, int index, ulong sequence)
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
    void DumpOperationalHaltMessage(OperationalHaltMessage message, int index, ulong sequence)
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
    void DumpOrderCancelMessage(OrderCancelMessage message, int index, ulong sequence)
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
    void DumpOrderDeleteMessage(OrderDeleteMessage message, int index, ulong sequence)
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
    void DumpOrderExecutedMessage(OrderExecutedMessage message, int index, ulong sequence)
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
    void DumpOrderExecutedWithPriceMessage(OrderExecutedWithPriceMessage message, int index, ulong sequence)
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
    void DumpOrderReplaceMessage(OrderReplaceMessage message, int index, ulong sequence)
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
    void DumpRegShoShortSalePriceTestRestrictedIndicatorMessage(RegShoShortSalePriceTestRestrictedIndicatorMessage message, int index, ulong sequence)
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
    void DumpRetailPriceImprovementIndicatorMessage(RetailPriceImprovementIndicatorMessage message, int index, ulong sequence)
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
    ///  Writes each field of the Stock Directory Message to the console.
    /// </summary>
    void DumpStockDirectoryMessage(StockDirectoryMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] StockDirectoryMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Stock Trading Action Message to the console.
    /// </summary>
    void DumpStockTradingActionMessage(StockTradingActionMessage message, int index, ulong sequence)
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
    void DumpSystemEventMessage(SystemEventMessage message, int index, ulong sequence)
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
    void DumpUnknownPacketMessage(UnknownPacketMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] UnknownPacketMessage");
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
