namespace Databento.MarketData.Normalized.Dbn.v1.Viewer;

/// <summary>
///  Handles parsed packets and writes every field of each message to the console.
/// </summary>
public sealed class GatewayViewer : GatewayStreamHandler
{
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
    public void Receive(GatewayStream packet)
    {
        ++this.packetIndex;
        if (!packet.IsParsed)
        {
            Console.WriteLine($"[packet={packetIndex}] packet did not parse");
            return;
        }
        ++this.Packets;
        this.Messages += packet.Messages.Count;
        Console.WriteLine($"[packet={packetIndex}] valid={packet.IsValid} count={packet.MessageCount} messages={packet.Messages.Count}");
        foreach (var (index, message) in packet.Messages.Index())
        {
            if (message is UnknownGatewayMessage unknown)
                Console.WriteLine($"[packet={packetIndex}] WARNING: message {messageIndex + index + 1} has the unknown message type 0x{unknown.DataValue:X}");
        }

        Handle(packet);
        if (!packet.Trailer.IsEmpty)
            Console.WriteLine($"[packet={packetIndex}] Trailer: {Convert.ToHexString(packet.Trailer.Span)}");
    }

    /// <inheritdoc/>
    protected override void OnTradeMsg(TradeMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpTradeMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnMbp1Msg(Mbp1Msg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpMbp1Msg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnMbp10Msg(Mbp10Msg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpMbp10Msg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnOhlcvMsg(OhlcvMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpOhlcvMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnStatusMsg(StatusMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpStatusMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnInstrumentDefMsg(InstrumentDefMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpInstrumentDefMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnImbalanceMsg(ImbalanceMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpImbalanceMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnErrorMsg(ErrorMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpErrorMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnSymbolMappingMsg(SymbolMappingMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpSymbolMappingMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnSystemMsg(SystemMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpSystemMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnStatMsg(StatMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpStatMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnMboMsg(MboMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpMboMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnCmbp1Msg(Cmbp1Msg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpCmbp1Msg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnCbboMsg(CbboMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpCbboMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnBboMsg(BboMsg message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpBboMsg(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnUnknownGatewayMessage(UnknownGatewayMessage message, GatewayRecordFrame frame)
    {
        ++this.messageIndex;
        DumpUnknownGatewayMessage(message, this.messageIndex);
    }

    /// <summary>
    ///  Writes each field of the Bbo Msg to the console.
    /// </summary>
    void DumpBboMsg(BboMsg message, int index)
    {
        Console.WriteLine($"[message={index}] BboMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Cbbo Msg to the console.
    /// </summary>
    void DumpCbboMsg(CbboMsg message, int index)
    {
        Console.WriteLine($"[message={index}] CbboMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Cmbp 1 Msg to the console.
    /// </summary>
    void DumpCmbp1Msg(Cmbp1Msg message, int index)
    {
        Console.WriteLine($"[message={index}] Cmbp1Msg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Error Msg to the console.
    /// </summary>
    void DumpErrorMsg(ErrorMsg message, int index)
    {
        Console.WriteLine($"[message={index}] ErrorMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Imbalance Msg to the console.
    /// </summary>
    void DumpImbalanceMsg(ImbalanceMsg message, int index)
    {
        Console.WriteLine($"[message={index}] ImbalanceMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Instrument Def Msg to the console.
    /// </summary>
    void DumpInstrumentDefMsg(InstrumentDefMsg message, int index)
    {
        Console.WriteLine($"[message={index}] InstrumentDefMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Mbo Msg to the console.
    /// </summary>
    void DumpMboMsg(MboMsg message, int index)
    {
        Console.WriteLine($"[message={index}] MboMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Mbp 1 Msg to the console.
    /// </summary>
    void DumpMbp1Msg(Mbp1Msg message, int index)
    {
        Console.WriteLine($"[message={index}] Mbp1Msg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Mbp 10 Msg to the console.
    /// </summary>
    void DumpMbp10Msg(Mbp10Msg message, int index)
    {
        Console.WriteLine($"[message={index}] Mbp10Msg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Ohlcv Msg to the console.
    /// </summary>
    void DumpOhlcvMsg(OhlcvMsg message, int index)
    {
        Console.WriteLine($"[message={index}] OhlcvMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Stat Msg to the console.
    /// </summary>
    void DumpStatMsg(StatMsg message, int index)
    {
        Console.WriteLine($"[message={index}] StatMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Status Msg to the console.
    /// </summary>
    void DumpStatusMsg(StatusMsg message, int index)
    {
        Console.WriteLine($"[message={index}] StatusMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Symbol Mapping Msg to the console.
    /// </summary>
    void DumpSymbolMappingMsg(SymbolMappingMsg message, int index)
    {
        Console.WriteLine($"[message={index}] SymbolMappingMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the System Msg to the console.
    /// </summary>
    void DumpSystemMsg(SystemMsg message, int index)
    {
        Console.WriteLine($"[message={index}] SystemMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Trade Msg to the console.
    /// </summary>
    void DumpTradeMsg(TradeMsg message, int index)
    {
        Console.WriteLine($"[message={index}] TradeMsg");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of a message whose message type the protocol does not define to the console.
    /// </summary>
    void DumpUnknownGatewayMessage(UnknownGatewayMessage message, int index)
    {
        Console.WriteLine($"[message={index}] UnknownGatewayMessage");
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
                        case UnknownFileMessage typed:
                            Console.WriteLine($"{indent}  Bytes: {Convert.ToHexString(typed.Bytes.Span)}");
                            break;
                        case UnknownGatewayMessage typed:
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
