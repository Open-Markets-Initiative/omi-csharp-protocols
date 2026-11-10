namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Viewer;

/// <summary>
///  Handles parsed packets and writes every field of each message to the console.
/// </summary>
public sealed class ClientViewer : ClientPacketHandler
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
    public void Receive(ClientPacket packet)
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
            if (message is UnknownClientMessage unknown)
                Console.WriteLine($"[packet={packetIndex}] WARNING: message {messageIndex + index + 1} has the unknown message type 0x{unknown.DataValue:X}");
        }

        Handle(packet);
    }

    /// <inheritdoc/>
    protected override void OnDebugPacket(DebugPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpDebugPacket(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnLoginRequestPacket(LoginRequestPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpLoginRequestPacket(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnUnsequencedDataPacket(UnsequencedDataPacket message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpUnsequencedDataPacket(message, this.messageIndex);
    }

    /// <inheritdoc/>
    protected override void OnUnknownClientMessage(UnknownClientMessage message, ClientSoupBinTcpPacketFrame frame)
    {
        ++this.messageIndex;
        DumpUnknownClientMessage(message, this.messageIndex);
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
    ///  Writes each field of the Login Request Packet to the console.
    /// </summary>
    void DumpLoginRequestPacket(LoginRequestPacket message, int index)
    {
        Console.WriteLine($"[message={index}] LoginRequestPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of the Unsequenced Data Packet to the console.
    /// </summary>
    void DumpUnsequencedDataPacket(UnsequencedDataPacket message, int index)
    {
        Console.WriteLine($"[message={index}] UnsequencedDataPacket");
        WriteFields(message.Fields, "  ", writeNested: false);
        if (!message.Trailer.IsEmpty)
            Console.WriteLine($"  Trailer: {Convert.ToHexString(message.Trailer.Span)}");
    }

    /// <summary>
    ///  Writes each field of a message whose message type the protocol does not define to the console.
    /// </summary>
    void DumpUnknownClientMessage(UnknownClientMessage message, int index)
    {
        Console.WriteLine($"[message={index}] UnknownClientMessage");
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
