namespace Iex.IexEquities.Deep.IexTp.v1_08.Viewer;

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
    private uint? session;

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
        var current = packet.Header.SessionId;
        if (this.session is { } previous && current != previous)
        {
            this.expectedSequence = null;
        }
        this.session = current;
        var first = packet.Header.FirstMessageSequenceNumber;
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

    /// <inheritdoc/>
    protected override void OnSystemEventMessage(SystemEventMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpSystemEventMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSecurityDirectoryMessage(SecurityDirectoryMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpSecurityDirectoryMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradingStatusMessage(TradingStatusMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpTradingStatusMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpRetailLiquidityIndicatorMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOperationalHaltStatusMessage(OperationalHaltStatusMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOperationalHaltStatusMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpShortSalePriceTestStatusMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnSecurityEventMessage(SecurityEventMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpSecurityEventMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnPriceLevelBuyUpdateMessage(PriceLevelBuyUpdateMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpPriceLevelBuyUpdateMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnPriceLevelSellUpdateMessage(PriceLevelSellUpdateMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpPriceLevelSellUpdateMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeReportMessage(TradeReportMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpTradeReportMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnOfficialPriceMessage(OfficialPriceMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpOfficialPriceMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnTradeBreakMessage(TradeBreakMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpTradeBreakMessage(message, this.messageIndex, this.sequence);
        ++this.sequence;
    }

    /// <inheritdoc/>
    protected override void OnAuctionInformationMessage(AuctionInformationMessage message, MessageFrame frame)
    {
        ++this.messageIndex;
        DumpAuctionInformationMessage(message, this.messageIndex, this.sequence);
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
    ///  Writes each field of the Auction Information Message to the console.
    /// </summary>
    void DumpAuctionInformationMessage(AuctionInformationMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] AuctionInformationMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Official Price Message to the console.
    /// </summary>
    void DumpOfficialPriceMessage(OfficialPriceMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OfficialPriceMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Operational Halt Status Message to the console.
    /// </summary>
    void DumpOperationalHaltStatusMessage(OperationalHaltStatusMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] OperationalHaltStatusMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Price Level Buy Update Message to the console.
    /// </summary>
    void DumpPriceLevelBuyUpdateMessage(PriceLevelBuyUpdateMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] PriceLevelBuyUpdateMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Price Level Sell Update Message to the console.
    /// </summary>
    void DumpPriceLevelSellUpdateMessage(PriceLevelSellUpdateMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] PriceLevelSellUpdateMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Retail Liquidity Indicator Message to the console.
    /// </summary>
    void DumpRetailLiquidityIndicatorMessage(RetailLiquidityIndicatorMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] RetailLiquidityIndicatorMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Security Directory Message to the console.
    /// </summary>
    void DumpSecurityDirectoryMessage(SecurityDirectoryMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SecurityDirectoryMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Security Event Message to the console.
    /// </summary>
    void DumpSecurityEventMessage(SecurityEventMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SecurityEventMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Short Sale Price Test Status Message to the console.
    /// </summary>
    void DumpShortSalePriceTestStatusMessage(ShortSalePriceTestStatusMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] ShortSalePriceTestStatusMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the System Event Message to the console.
    /// </summary>
    void DumpSystemEventMessage(SystemEventMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] SystemEventMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Trade Break Message to the console.
    /// </summary>
    void DumpTradeBreakMessage(TradeBreakMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] TradeBreakMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Trade Report Message to the console.
    /// </summary>
    void DumpTradeReportMessage(TradeReportMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] TradeReportMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Trading Status Message to the console.
    /// </summary>
    void DumpTradingStatusMessage(TradingStatusMessage message, int index, ulong sequence)
    {
        Console.WriteLine($"[message={index} sequence={sequence}] TradingStatusMessage");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
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
                        case UnknownPacketMessage typed:
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
