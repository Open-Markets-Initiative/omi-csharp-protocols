using System.Buffers.Binary;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Rules;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct ServerFramedMessage
{
    internal ServerFramedMessage(ServerMessageCode dataValue, ServerPayloadKind serverpayloadkind, ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IServerMessage? message)
    {
        DataValue = dataValue;
        ServerPayloadKind = serverpayloadkind;
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public ServerMessageCode DataValue { get; }
    /// <summary>
    ///  Gets the kind that the packet carries for this message.
    /// </summary>
    public ServerPayloadKind ServerPayloadKind { get; }
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
    public IServerMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct ServerMessageReader
{
    private const int Start = 0;
    private const int ServerPayloadReached = 1;
    private const int ServerPayloadContinued = 2;
    private const int ServerSoupBinTcpPacketOccurrence = 3;
    private const int Ended = 4;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly ServerPacket target;
    private ServerSoupBinTcpPacketFrame? serverSoupBinTcpPacketFrame;
    private const int Unset = -1;
    private IServerMessage found = null!;
    private int frameStart;
    private int foundStart;
    private int foundFinish = Unset;
    private int payloadFinish;
    private ServerMessageCode foundDataValue;
    int ServerSoupBinTcpPacketPayloadRule0End;
    global::System.Int128 ServerSoupBinTcpPacketPayloadRule0RawEnd;
    int serverSoupBinTcpPacketEntryStart;
    int serverSoupBinTcpPacketEnd;
    int ServerSoupBinTcpPacketReassembleRule1End;
    global::System.Int128 ServerSoupBinTcpPacketReassembleRule1Width;
    ushort packetLengthValue;
    int ServerSoupBinTcpPacketSizeRule2End;
    global::System.Int128 ServerSoupBinTcpPacketSizeRule2Width;
    int serverPacketHeaderEntryStart;
    int serverPayloadCase;
    ReadOnlySpan<byte> serverPacketTypeValue;
    int sequencedDataPacketEntryStart;
    int sequencedDataPacketEnd;
    int SequencedDataPacketSizeRule0End;
    global::System.Int128 SequencedDataPacketSizeRule0Width;
    const int packetLengthDependencyDistanceFromServerSoupBinTcpPacket = 0;
    const int packetLengthDependencyReadWidth = 2;
    const int serverPacketTypeDependencyDistanceFromServerPayload = -1;
    const int serverPacketTypeDependencyReadWidth = 1;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ServerMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    public ServerMessageReader(ReadOnlySpan<byte> buffer) : this(buffer, new ServerPacket())
    {
    }

    internal ServerMessageReader(ReadOnlySpan<byte> buffer, ServerPacket target)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public ServerFramedMessage Current { get; private set; }
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
                    ServerSoupBinTcpPacketPayloadRule0RawEnd = checked((global::System.Int128)(buffer.Length));
                    if (ServerSoupBinTcpPacketPayloadRule0RawEnd < position || ServerSoupBinTcpPacketPayloadRule0RawEnd > buffer.Length) return Stop(ReadStatus.Truncated);
                    ServerSoupBinTcpPacketPayloadRule0End = (int)ServerSoupBinTcpPacketPayloadRule0RawEnd;
                }
                goto case ServerSoupBinTcpPacketOccurrence;
            }
            case ServerPayloadReached:
            {
                if (position < serverSoupBinTcpPacketEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..serverSoupBinTcpPacketEnd]);
                        payloadFinish = serverSoupBinTcpPacketEnd;
                    }
                }
                position = serverSoupBinTcpPacketEnd;
                foundFinish = Unset;
                if (position <= serverSoupBinTcpPacketEntryStart || position > ServerSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new ServerFramedMessage(foundDataValue, serverSoupBinTcpPacketFrame?.ServerPayloadKind ?? default, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                state = ServerSoupBinTcpPacketOccurrence;
                return true;
            }
            case ServerPayloadContinued:
            {
                if (position < serverSoupBinTcpPacketEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..serverSoupBinTcpPacketEnd]);
                        payloadFinish = serverSoupBinTcpPacketEnd;
                    }
                }
                position = serverSoupBinTcpPacketEnd;
                foundFinish = Unset;
                if (position <= serverSoupBinTcpPacketEntryStart || position > ServerSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new ServerFramedMessage(default, serverSoupBinTcpPacketFrame?.ServerPayloadKind ?? default, buffer[serverSoupBinTcpPacketEntryStart..position], buffer[position..position], position - serverSoupBinTcpPacketEntryStart, null);
                state = ServerSoupBinTcpPacketOccurrence;
                return true;
            }
            case ServerSoupBinTcpPacketOccurrence:
            {
                if (!(position < ServerSoupBinTcpPacketPayloadRule0End))
                {
                    {
                        if (position != ServerSoupBinTcpPacketPayloadRule0End) return Stop(ReadStatus.InvalidLength);
                        position = ServerSoupBinTcpPacketPayloadRule0End;
                        foundFinish = Unset;
                        position = buffer.Length;
                        if (state == Start)
                        {
                            Current = new ServerFramedMessage(default, serverSoupBinTcpPacketFrame?.ServerPayloadKind ?? default, buffer[0..position], buffer[position..position], position - 0, null);
                            state = Ended;
                            return true;
                        }
                        state = Ended;
                        return false;
                    }
                }
                serverSoupBinTcpPacketEntryStart = position;
                {
                    serverSoupBinTcpPacketFrame = new ServerSoupBinTcpPacketFrame();
                    target.ServerSoupBinTcpPacketFrames.Add(serverSoupBinTcpPacketFrame);
                    const long ServerSoupBinTcpPacketReassembleRule1Data2 = 2;
                    {
                        if (ServerSoupBinTcpPacketPayloadRule0End < position + packetLengthDependencyDistanceFromServerSoupBinTcpPacket + packetLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(position + packetLengthDependencyDistanceFromServerSoupBinTcpPacket, packetLengthDependencyReadWidth));
                        ServerSoupBinTcpPacketReassembleRule1Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1Data2))));
                    }
                    if (ServerSoupBinTcpPacketReassembleRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ServerSoupBinTcpPacketReassembleRule1Width > (long)ServerSoupBinTcpPacketPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    ServerSoupBinTcpPacketReassembleRule1End = position + (int)ServerSoupBinTcpPacketReassembleRule1Width;
                    const long ServerSoupBinTcpPacketSizeRule2Data2 = 2;
                    ServerSoupBinTcpPacketSizeRule2Width = checked((checked((global::System.Int128)(packetLengthValue)) + ((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2Data2))));
                    if (ServerSoupBinTcpPacketSizeRule2Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ServerSoupBinTcpPacketSizeRule2Width > (long)ServerSoupBinTcpPacketPayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    ServerSoupBinTcpPacketSizeRule2End = position + (int)ServerSoupBinTcpPacketSizeRule2Width;
                    serverSoupBinTcpPacketEnd = ServerSoupBinTcpPacketReassembleRule1End;
                    if (ServerSoupBinTcpPacketSizeRule2End != serverSoupBinTcpPacketEnd) return Stop(ReadStatus.InvalidLength);
                    serverPacketHeaderEntryStart = position;
                    const int PacketLengthWidth = 2;
                    if (!Take(PacketLengthWidth, serverSoupBinTcpPacketEnd)) return false;
                    const int ServerPacketTypeWidth = 1;
                    if (!Take(ServerPacketTypeWidth, serverSoupBinTcpPacketEnd)) return false;
                    {
                        var serverPacketHeaderParsed = new ServerPacketHeader();
                        var serverPacketHeaderOffset = serverPacketHeaderEntryStart;
                        serverPacketHeaderParsed.ParseFrom(buffer[..serverSoupBinTcpPacketEnd], ref serverPacketHeaderOffset);
                        serverSoupBinTcpPacketFrame!.SetServerPacketHeader(serverPacketHeaderParsed);
                    }
                    {
                        const int ServerPayloadNoCase = 0;
                        const int ServerPayloadBranchRule0Case = 1;
                        const int ServerPayloadBranchRule1Case = 2;
                        const int ServerPayloadBranchRule2Case = 3;
                        const int ServerPayloadBranchRule3Case = 4;
                        const int ServerPayloadBranchRule4Case = 5;
                        const int ServerPayloadBranchRule5Case = 6;
                        serverPayloadCase = ServerPayloadNoCase;
                        {
                            if (serverSoupBinTcpPacketEnd < position + serverPacketTypeDependencyDistanceFromServerPayload + serverPacketTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            serverPacketTypeValue = buffer.Slice(position + serverPacketTypeDependencyDistanceFromServerPayload, serverPacketTypeDependencyReadWidth);
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = "\u002B"u8;
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = "A"u8;
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = "J"u8;
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = "S"u8;
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = "H"u8;
                            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = "Z"u8;
                            if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule0Data2)) { serverPayloadCase = ServerPayloadBranchRule0Case; }
                            else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule1Data2)) { serverPayloadCase = ServerPayloadBranchRule1Case; }
                            else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule2Data2)) { serverPayloadCase = ServerPayloadBranchRule2Case; }
                            else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule3Data2)) { serverPayloadCase = ServerPayloadBranchRule3Case; }
                            else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule4Data2)) { serverPayloadCase = ServerPayloadBranchRule4Case; }
                            else if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule5Data2)) { serverPayloadCase = ServerPayloadBranchRule5Case; }
                        }
                        if (serverPayloadCase == ServerPayloadBranchRule0Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.Messages;
                            var parsed = new DebugPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..serverSoupBinTcpPacketEnd], ref position, checked((global::System.Int128?)(packetLengthValue)));
                            found = parsed;
                        }
                        else if (serverPayloadCase == ServerPayloadBranchRule1Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.Messages;
                            var parsed = new LoginAcceptedPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..serverSoupBinTcpPacketEnd], ref position);
                            found = parsed;
                        }
                        else if (serverPayloadCase == ServerPayloadBranchRule2Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.Messages;
                            var parsed = new LoginRejectedPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..serverSoupBinTcpPacketEnd], ref position);
                            found = parsed;
                        }
                        else if (serverPayloadCase == ServerPayloadBranchRule3Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.Messages;
                            sequencedDataPacketEntryStart = position;
                            const long SequencedDataPacketSizeRule0Data2 = 1;
                            SequencedDataPacketSizeRule0Width = checked((checked((global::System.Int128)(packetLengthValue)) - ((global::System.Int128)(SequencedDataPacketSizeRule0Data2))));
                            if (SequencedDataPacketSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                            if (SequencedDataPacketSizeRule0Width > (long)serverSoupBinTcpPacketEnd - position) return Stop(ReadStatus.Truncated);
                            SequencedDataPacketSizeRule0End = position + (int)SequencedDataPacketSizeRule0Width;
                            sequencedDataPacketEnd = SequencedDataPacketSizeRule0End;
                            var parsed = new SequencedDataPacket();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..sequencedDataPacketEnd], ref position, checked((global::System.Int128?)(packetLengthValue)), checked((global::System.Int128?)(packetLengthValue)));
                            found = parsed;
                        }
                        else if (serverPayloadCase == ServerPayloadBranchRule4Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.ServerHeartbeatPacket;
                            goto case ServerPayloadContinued;
                        }
                        else if (serverPayloadCase == ServerPayloadBranchRule5Case)
                        {
                            serverSoupBinTcpPacketFrame!.ServerPayloadKind = ServerPayloadKind.EndOfSessionPacket;
                            goto case ServerPayloadContinued;
                        }
                        else
                        {
                            {
                                if (serverSoupBinTcpPacketEnd < position + serverPacketTypeDependencyDistanceFromServerPayload + serverPacketTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (ServerMessageCode)buffer.Slice(position + serverPacketTypeDependencyDistanceFromServerPayload, serverPacketTypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownServerMessage.Parse(buffer[..serverSoupBinTcpPacketEnd], ref position).SetDataValue(code);
                            }
                        }
                        foundDataValue = ((IServerMessage)found).DataValue;
                        frameStart = serverSoupBinTcpPacketEntryStart;
                        foundFinish = position;
                        payloadFinish = position;
                        if (found.Status != ParseStatus.Ok)
                        {
                            Current = new ServerFramedMessage(foundDataValue, serverSoupBinTcpPacketFrame?.ServerPayloadKind ?? default, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                            state = Ended;
                            return true;
                        }
                        serverSoupBinTcpPacketFrame!.serverPayload = (IServerMessage)found;
                        if (serverPayloadCase == ServerPayloadBranchRule3Case)
                        {
                            if (position < sequencedDataPacketEnd)
                            {
                                if (position == foundFinish)
                                {
                                    found.SetTrailer(buffer[position..sequencedDataPacketEnd]);
                                    payloadFinish = sequencedDataPacketEnd;
                                }
                            }
                            position = sequencedDataPacketEnd;
                            foundFinish = Unset;
                            goto case ServerPayloadReached;
                        }
                        else
                        {
                            goto case ServerPayloadReached;
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
