using System.Buffers.Binary;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Rules;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct ClientFramedMessage
{
    internal ClientFramedMessage(ClientMessageCode dataValue, ClientPayloadKind clientpayloadkind, ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IClientMessage? message)
    {
        DataValue = dataValue;
        ClientPayloadKind = clientpayloadkind;
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public ClientMessageCode DataValue { get; }
    /// <summary>
    ///  Gets the kind that the packet carries for this message.
    /// </summary>
    public ClientPayloadKind ClientPayloadKind { get; }
    /// <summary>
    ///  Gets the bytes that precede the payload of the message.
    /// </summary>
    public ReadOnlySpan<byte> Header { get; }
    /// <summary>
    ///  Gets the payload bytes of the message.
    /// </summary>
    public ReadOnlySpan<byte> Payload { get; }
    /// <summary>
    ///  Gets the number of packet bytes that the message occupies.
    /// </summary>
    public int Consumed { get; }
    /// <summary>
    ///  Gets the parsed message, or <see langword="null"/> if the packet or frame carries no message.
    /// </summary>
    public IClientMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct ClientMessageReader
{
    private const int Start = 0;
    private const int ClientPayloadReached = 1;
    private const int ClientPayloadContinued = 2;
    private const int ClientSoupBinTcpPacketOccurrence = 3;
    private const int Ended = 4;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly ClientPacket target;
    private ClientSoupBinTcpPacketFrame? clientSoupBinTcpPacketFrame;
    private const int Unset = -1;
    private IClientMessage found = null!;
    private int frameStart;
    private int foundStart;
    private int foundFinish = Unset;
    private int payloadFinish;
    private ClientMessageCode foundDataValue;
    int ClientSoupBinTcpPacketPayloadRule0End;
    global::System.Int128 ClientSoupBinTcpPacketPayloadRule0RawEnd;
    int clientSoupBinTcpPacketEntryStart;
    int clientSoupBinTcpPacketEnd;
    int ClientSoupBinTcpPacketReassembleRule1End;
    global::System.Int128 ClientSoupBinTcpPacketReassembleRule1Width;
    ushort packetLengthValue;
    int ClientSoupBinTcpPacketSizeRule2End;
    global::System.Int128 ClientSoupBinTcpPacketSizeRule2Width;
    int clientPacketHeaderEntryStart;
    int clientPayloadCase;
    ReadOnlySpan<byte> clientPacketTypeValue;
    int unsequencedDataPacketEntryStart;
    int unsequencedDataPacketEnd;
    int UnsequencedDataPacketSizeRule0End;
    global::System.Int128 UnsequencedDataPacketSizeRule0Width;
    const int packetLengthDependencyDistanceFromClientSoupBinTcpPacket = 0;
    const int packetLengthDependencyReadWidth = 2;
    const int clientPacketTypeDependencyDistanceFromClientPayload = -1;
    const int clientPacketTypeDependencyReadWidth = 1;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    public ClientMessageReader(ReadOnlySpan<byte> buffer) : this(buffer, new ClientPacket())
    {
    }

    internal ClientMessageReader(ReadOnlySpan<byte> buffer, ClientPacket target)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public ClientFramedMessage Current { get; private set; }
    /// <summary>
    ///  Gets the reason that reading stopped, or <see cref="ReadStatus.Complete"/> if no error stopped it.
    /// </summary>
    public ReadStatus Status { get; private set; }
    /// <summary>
    ///  Gets a value indicating whether no error has stopped reading.
    /// </summary>
    public bool IsComplete => Status == ReadStatus.Complete;
    /// <summary>
    ///  Gets the offset of the next unread byte.
    /// </summary>
    public int Consumed => position;

    /// <summary>
    ///  Advances to the next message.
    /// </summary>
    /// <returns><see langword="true"/> if there is another message, or <see langword="false"/> if the end was reached or an error stopped reading.</returns>
    public bool MoveNext()
    {
        switch (state)
        {
            case Start:
            {
                {
                    ClientSoupBinTcpPacketPayloadRule0RawEnd = checked((global::System.Int128)(buffer.Length));
                    if (ClientSoupBinTcpPacketPayloadRule0RawEnd < position || ClientSoupBinTcpPacketPayloadRule0RawEnd > buffer.Length) return Stop(ReadStatus.Truncated);
                    ClientSoupBinTcpPacketPayloadRule0End = (int)ClientSoupBinTcpPacketPayloadRule0RawEnd;
                }
                goto case ClientSoupBinTcpPacketOccurrence;
            }
            case ClientPayloadReached:
            {
                if (position < clientSoupBinTcpPacketEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..clientSoupBinTcpPacketEnd]);
                        payloadFinish = clientSoupBinTcpPacketEnd;
                    }
                }
                position = clientSoupBinTcpPacketEnd;
                foundFinish = Unset;
                if (position <= clientSoupBinTcpPacketEntryStart || position > ClientSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new ClientFramedMessage(foundDataValue, clientSoupBinTcpPacketFrame?.ClientPayloadKind ?? default, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                state = ClientSoupBinTcpPacketOccurrence;
                return true;
            }
            case ClientPayloadContinued:
            {
                if (position < clientSoupBinTcpPacketEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..clientSoupBinTcpPacketEnd]);
                        payloadFinish = clientSoupBinTcpPacketEnd;
                    }
                }
                position = clientSoupBinTcpPacketEnd;
                foundFinish = Unset;
                if (position <= clientSoupBinTcpPacketEntryStart || position > ClientSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new ClientFramedMessage(default, clientSoupBinTcpPacketFrame?.ClientPayloadKind ?? default, buffer[clientSoupBinTcpPacketEntryStart..position], buffer[position..position], position - clientSoupBinTcpPacketEntryStart, null);
                state = ClientSoupBinTcpPacketOccurrence;
                return true;
            }
            case ClientSoupBinTcpPacketOccurrence:
            {
                if (!(position < ClientSoupBinTcpPacketPayloadRule0End))
                {
                    {
                        if (position != ClientSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                        position = ClientSoupBinTcpPacketPayloadRule0End;
                        foundFinish = Unset;
                        position = buffer.Length;
                        if (state == Start)
                        {
                            Current = new ClientFramedMessage(default, clientSoupBinTcpPacketFrame?.ClientPayloadKind ?? default, buffer[0..position], buffer[position..position], position - 0, null);
                            state = Ended;
                            return true;
                        }
                        state = Ended;
                        return false;
                    }
                }
                clientSoupBinTcpPacketEntryStart = position;
                {
                    clientSoupBinTcpPacketFrame = new ClientSoupBinTcpPacketFrame();
                    target.ClientSoupBinTcpPacketFrames.Add(clientSoupBinTcpPacketFrame);
                    const long ClientSoupBinTcpPacketReassembleRule1Data2 = 2;
                    {
                        if (ClientSoupBinTcpPacketPayloadRule0End < position + packetLengthDependencyDistanceFromClientSoupBinTcpPacket + packetLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(position + packetLengthDependencyDistanceFromClientSoupBinTcpPacket, packetLengthDependencyReadWidth));
                        ClientSoupBinTcpPacketReassembleRule1Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1Data2))));
                    }
                    if (ClientSoupBinTcpPacketReassembleRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ClientSoupBinTcpPacketReassembleRule1Width > (long)ClientSoupBinTcpPacketPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    ClientSoupBinTcpPacketReassembleRule1End = position + (int)ClientSoupBinTcpPacketReassembleRule1Width;
                    const long ClientSoupBinTcpPacketSizeRule2Data2 = 2;
                    ClientSoupBinTcpPacketSizeRule2Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2Data2))));
                    if (ClientSoupBinTcpPacketSizeRule2Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ClientSoupBinTcpPacketSizeRule2Width > (long)ClientSoupBinTcpPacketPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    ClientSoupBinTcpPacketSizeRule2End = position + (int)ClientSoupBinTcpPacketSizeRule2Width;
                    clientSoupBinTcpPacketEnd = ClientSoupBinTcpPacketReassembleRule1End;
                    if (ClientSoupBinTcpPacketSizeRule2End != clientSoupBinTcpPacketEnd) return Stop(ReadStatus.InvalidLength);
                    clientPacketHeaderEntryStart = position;
                    const int PacketLengthWidth = 2;
                    if (!Take(PacketLengthWidth, clientSoupBinTcpPacketEnd)) return false;
                    const int ClientPacketTypeWidth = 1;
                    if (!Take(ClientPacketTypeWidth, clientSoupBinTcpPacketEnd)) return false;
                    {
                        var clientPacketHeaderParsed = new ClientPacketHeader();
                        var clientPacketHeaderOffset = clientPacketHeaderEntryStart;
                        clientPacketHeaderParsed.ParseFrom(buffer[..clientSoupBinTcpPacketEnd], ref clientPacketHeaderOffset);
                        clientSoupBinTcpPacketFrame!.SetClientPacketHeader(clientPacketHeaderParsed);
                    }
                    {
                        const int ClientPayloadNoCase = 0;
                        const int ClientPayloadBranchRule0Case = 1;
                        const int ClientPayloadBranchRule1Case = 2;
                        const int ClientPayloadBranchRule2Case = 3;
                        const int ClientPayloadBranchRule3Case = 4;
                        const int ClientPayloadBranchRule4Case = 5;
                        clientPayloadCase = ClientPayloadNoCase;
                        {
                            if (clientSoupBinTcpPacketEnd < position + clientPacketTypeDependencyDistanceFromClientPayload + clientPacketTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            clientPacketTypeValue = buffer.Slice(position + clientPacketTypeDependencyDistanceFromClientPayload, clientPacketTypeDependencyReadWidth);
                            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = "\u002B"u8;
                            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = "L"u8;
                            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = "U"u8;
                            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = "R"u8;
                            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = "O"u8;
                            if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule0Data2)) { clientPayloadCase = ClientPayloadBranchRule0Case; }
                            else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule1Data2)) { clientPayloadCase = ClientPayloadBranchRule1Case; }
                            else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule2Data2)) { clientPayloadCase = ClientPayloadBranchRule2Case; }
                            else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule3Data2)) { clientPayloadCase = ClientPayloadBranchRule3Case; }
                            else if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule4Data2)) { clientPayloadCase = ClientPayloadBranchRule4Case; }
                        }
                        if (clientPayloadCase == ClientPayloadBranchRule0Case)
                        {
                            clientSoupBinTcpPacketFrame!.ClientPayloadKind = ClientPayloadKind.Messages;
                            var parsed = new DebugPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..clientSoupBinTcpPacketEnd], ref position, checked((global::System.Int128?)(packetLengthValue)));
                            found = parsed;
                        }
                        else if (clientPayloadCase == ClientPayloadBranchRule1Case)
                        {
                            clientSoupBinTcpPacketFrame!.ClientPayloadKind = ClientPayloadKind.Messages;
                            var parsed = new LoginRequestPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..clientSoupBinTcpPacketEnd], ref position);
                            found = parsed;
                        }
                        else if (clientPayloadCase == ClientPayloadBranchRule2Case)
                        {
                            clientSoupBinTcpPacketFrame!.ClientPayloadKind = ClientPayloadKind.Messages;
                            unsequencedDataPacketEntryStart = position;
                            const long UnsequencedDataPacketSizeRule0Data2 = 1;
                            UnsequencedDataPacketSizeRule0Width = checked((checked((global::System.Int128)(packetLengthValue)) - ((global::System.Int128)(UnsequencedDataPacketSizeRule0Data2))));
                            if (UnsequencedDataPacketSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                            if (UnsequencedDataPacketSizeRule0Width > (long)clientSoupBinTcpPacketEnd - position) return Stop(ReadStatus.Truncated);
                            UnsequencedDataPacketSizeRule0End = position + (int)UnsequencedDataPacketSizeRule0Width;
                            unsequencedDataPacketEnd = UnsequencedDataPacketSizeRule0End;
                            var parsed = new UnsequencedDataPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..unsequencedDataPacketEnd], ref position, checked((global::System.Int128?)(packetLengthValue)), checked((global::System.Int128?)(packetLengthValue)));
                            found = parsed;
                        }
                        else if (clientPayloadCase == ClientPayloadBranchRule3Case)
                        {
                            clientSoupBinTcpPacketFrame!.ClientPayloadKind = ClientPayloadKind.ClientHeartbeat;
                            goto case ClientPayloadContinued;
                        }
                        else if (clientPayloadCase == ClientPayloadBranchRule4Case)
                        {
                            clientSoupBinTcpPacketFrame!.ClientPayloadKind = ClientPayloadKind.LogoutRequest;
                            goto case ClientPayloadContinued;
                        }
                        else
                        {
                            {
                                if (clientSoupBinTcpPacketEnd < position + clientPacketTypeDependencyDistanceFromClientPayload + clientPacketTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (ClientMessageCode)buffer.Slice(position + clientPacketTypeDependencyDistanceFromClientPayload, clientPacketTypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownClientMessage.Parse(buffer[..clientSoupBinTcpPacketEnd], ref position).SetDataValue(code);
                            }
                        }
                        foundDataValue = ((IClientMessage)found).DataValue;
                        frameStart = clientSoupBinTcpPacketEntryStart;
                        foundFinish = position;
                        payloadFinish = position;
                        if (found.Status != ParseStatus.Ok)
                        {
                            Current = new ClientFramedMessage(foundDataValue, clientSoupBinTcpPacketFrame?.ClientPayloadKind ?? default, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                            state = Ended;
                            return true;
                        }
                        clientSoupBinTcpPacketFrame!.clientPayload = (IClientMessage)found;
                        if (clientPayloadCase == ClientPayloadBranchRule2Case)
                        {
                            if (position < unsequencedDataPacketEnd)
                            {
                                if (position == foundFinish)
                                {
                                    found.SetTrailer(buffer[position..unsequencedDataPacketEnd]);
                                    payloadFinish = unsequencedDataPacketEnd;
                                }
                            }
                            position = unsequencedDataPacketEnd;
                            foundFinish = Unset;
                            goto case ClientPayloadReached;
                        }
                        else
                        {
                            goto case ClientPayloadReached;
                        }
                    }
                }
            }
            default:
            {
                return false;
            }
        }
    }

    private bool Stop(ReadStatus status) { Status = status; state = Ended; return false; }

    private bool Take(int width, int end)
    {
        if (position + width > end) return Stop(ReadStatus.Truncated);
        position += width;
        return true;
    }
}
