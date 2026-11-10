using System.Buffers.Binary;
using static Iex.IexEquities.DeepPlus.IexTp.v1_05.Rules;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct PacketFramedMessage
{
    internal PacketFramedMessage(PacketMessageCode dataValue, MessagesKind messageskind, ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IPacketMessage? message)
    {
        DataValue = dataValue;
        MessagesKind = messageskind;
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue { get; }
    /// <summary>
    ///  Gets the kind that the packet carries for this message.
    /// </summary>
    public MessagesKind MessagesKind { get; }
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
    public IPacketMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct PacketMessageReader
{
    private const int Start = 0;
    private const int MessageDataReached = 1;
    private const int MessageOccurrence = 2;
    private const int MessagesContinued = 3;
    private const int Ended = 4;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly Packet target;
    private MessageFrame? messageFrame;
    private const int Unset = -1;
    private IPacketMessage found = null!;
    private int frameStart;
    private int foundStart;
    private int foundFinish = Unset;
    private int payloadFinish;
    private PacketMessageCode foundDataValue;
    private int countedFinish = Unset;
    private ReadOnlySpan<byte> trailerBytes;
    private ulong declaredCount;
    int messagesEntryStart;
    int messagesEnd;
    int MessagesSizeRule0End;
    global::System.Int128 MessagesSizeRule0Width;
    ushort payloadLengthValue;
    int messagesCase;
    ushort messageCountValue;
    global::System.UInt128 MessageCountRule0Entries;
    int messageEntryStart;
    int messageEnd;
    int MessageSizeRule1End;
    global::System.Int128 MessageSizeRule1Width;
    ushort messageLengthValue;
    int messageHeaderEntryStart;
    int messageDataCase;
    ReadOnlySpan<byte> messageTypeValue;
    const int payloadLengthDependencyDistanceFromMessages = -28;
    const int payloadLengthDependencyReadWidth = 2;
    const int messageCountDependencyDistanceFromMessages = -26;
    const int messageCountDependencyReadWidth = 2;
    const int messageLengthDependencyDistanceFromMessage = 0;
    const int messageLengthDependencyReadWidth = 2;
    const int messageTypeDependencyDistanceFromMessageData = -1;
    const int messageTypeDependencyReadWidth = 1;

    /// <summary>
    ///  Initializes a new instance of the <see cref="PacketMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    public PacketMessageReader(ReadOnlySpan<byte> buffer) : this(buffer, new Packet())
    {
    }

    internal PacketMessageReader(ReadOnlySpan<byte> buffer, Packet target)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
        HeaderStart = buffer.Length;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public PacketFramedMessage Current { get; private set; }
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
    ///  Gets the message count declared by the packet, or 0 if the packet carries no messages.
    /// </summary>
    public ulong MessageCount => declaredCount;
    internal ReadOnlySpan<byte> Trailer => trailerBytes;
    internal int HeaderStart { get; private set; }

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
                const int PacketHeaderDistance = 0;
                HeaderStart = int.Min(HeaderStart, position + PacketHeaderDistance);
                const int VersionWidth = 1;
                if (!Take(VersionWidth, buffer.Length)) return false;
                const int ReservedWidth = 1;
                if (!Take(ReservedWidth, buffer.Length)) return false;
                const int MessageProtocolIdWidth = 2;
                if (!Take(MessageProtocolIdWidth, buffer.Length)) return false;
                const int ChannelIdWidth = 4;
                if (!Take(ChannelIdWidth, buffer.Length)) return false;
                const int SessionIdWidth = 4;
                if (!Take(SessionIdWidth, buffer.Length)) return false;
                const int PayloadLengthWidth = 2;
                if (!Take(PayloadLengthWidth, buffer.Length)) return false;
                const int MessageCountWidth = 2;
                if (!Take(MessageCountWidth, buffer.Length)) return false;
                const int StreamOffsetWidth = 8;
                if (!Take(StreamOffsetWidth, buffer.Length)) return false;
                const int FirstMessageSequenceNumberWidth = 8;
                if (!Take(FirstMessageSequenceNumberWidth, buffer.Length)) return false;
                const int SendTimeWidth = 8;
                if (!Take(SendTimeWidth, buffer.Length)) return false;
                messagesEntryStart = position;
                {
                    if (buffer.Length < position + payloadLengthDependencyDistanceFromMessages + payloadLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                    payloadLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(position + payloadLengthDependencyDistanceFromMessages, payloadLengthDependencyReadWidth));
                    MessagesSizeRule0Width = checked((global::System.Int128)(payloadLengthValue));
                }
                if (MessagesSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                if (MessagesSizeRule0Width > (long)buffer.Length - position) return Stop(ReadStatus.Truncated);
                MessagesSizeRule0End = position + (int)MessagesSizeRule0Width;
                messagesEnd = MessagesSizeRule0End;
                {
                    const int MessagesNoCase = 0;
                    const int MessagesBranchRule1Case = 1;
                    const int MessagesBranchRule2Case = 2;
                    messagesCase = MessagesNoCase;
                    {
                        if (messagesEnd < position + messageCountDependencyDistanceFromMessages + messageCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(position + messageCountDependencyDistanceFromMessages, messageCountDependencyReadWidth));
                        const ushort MessagesBranchRule1Data2 = 0;
                        if ((messageCountValue == MessagesBranchRule1Data2)) { messagesCase = MessagesBranchRule1Case; }
                        else { messagesCase = MessagesBranchRule2Case; }
                    }
                    if (messagesCase == MessagesBranchRule1Case)
                    {
                        target.MessagesKind = MessagesKind.Heartbeat;
                        goto case MessagesContinued;
                    }
                    else
                    {
                        target.MessagesKind = MessagesKind.Message;
                        {
                            MessageCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(messageCountValue)));
                            if (MessageCountRule0Entries > ulong.MaxValue - declaredCount) return Stop(ReadStatus.InvalidLength);
                            declaredCount += (ulong)MessageCountRule0Entries;
                        }
                        goto case MessageOccurrence;
                    }
                }
            }
            case MessageDataReached:
            {
                if (position < messageEnd)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..messageEnd];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..messageEnd]);
                        payloadFinish = messageEnd;
                    }
                }
                position = messageEnd;
                countedFinish = Unset;
                foundFinish = Unset;
                MessageCountRule0Entries--;
                Current = new PacketFramedMessage(foundDataValue, target.MessagesKind, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                state = MessageOccurrence;
                return true;
            }
            case MessageOccurrence:
            {
                if (!(MessageCountRule0Entries > RuleCountNone))
                {
                    {
                        if (MessageCountRule0Entries > RuleCountNone) return Stop(ReadStatus.Truncated);
                        countedFinish = position;
                        goto case MessagesContinued;
                    }
                }
                messageEntryStart = position;
                {
                    messageFrame = new MessageFrame();
                    target.MessageFrames.Add(messageFrame);
                    const long MessageSizeRule1Data2 = 2;
                    {
                        if (messagesEnd < position + messageLengthDependencyDistanceFromMessage + messageLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        messageLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(buffer.Slice(position + messageLengthDependencyDistanceFromMessage, messageLengthDependencyReadWidth));
                        MessageSizeRule1Width = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(MessageSizeRule1Data2))));
                    }
                    if (MessageSizeRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (MessageSizeRule1Width > (long)messagesEnd - position) return Stop(ReadStatus.Truncated);
                    MessageSizeRule1End = position + (int)MessageSizeRule1Width;
                    messageEnd = MessageSizeRule1End;
                    messageHeaderEntryStart = position;
                    const int MessageLengthWidth = 2;
                    if (!Take(MessageLengthWidth, messageEnd)) return false;
                    const int MessageTypeWidth = 1;
                    if (!Take(MessageTypeWidth, messageEnd)) return false;
                    {
                        var messageHeaderParsed = new MessageHeader();
                        var messageHeaderOffset = messageHeaderEntryStart;
                        messageHeaderParsed.ParseFrom(buffer[..messageEnd], ref messageHeaderOffset);
                        messageFrame!.SetMessageHeader(messageHeaderParsed);
                    }
                    {
                        const int MessageDataNoCase = 0;
                        const int MessageDataBranchRule0Case = 1;
                        const int MessageDataBranchRule1Case = 2;
                        const int MessageDataBranchRule2Case = 3;
                        const int MessageDataBranchRule3Case = 4;
                        const int MessageDataBranchRule4Case = 5;
                        const int MessageDataBranchRule5Case = 6;
                        const int MessageDataBranchRule6Case = 7;
                        const int MessageDataBranchRule7Case = 8;
                        const int MessageDataBranchRule8Case = 9;
                        const int MessageDataBranchRule9Case = 10;
                        const int MessageDataBranchRule10Case = 11;
                        const int MessageDataBranchRule11Case = 12;
                        const int MessageDataBranchRule12Case = 13;
                        const int MessageDataBranchRule13Case = 14;
                        messageDataCase = MessageDataNoCase;
                        {
                            if (messageEnd < position + messageTypeDependencyDistanceFromMessageData + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            messageTypeValue = buffer.Slice(position + messageTypeDependencyDistanceFromMessageData, messageTypeDependencyReadWidth);
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule0Data2 = "S"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule1Data2 = "D"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule2Data2 = "H"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule3Data2 = "I"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule4Data2 = "O"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule5Data2 = "P"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule6Data2 = "E"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule7Data2 = "a"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule8Data2 = "M"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule9Data2 = "R"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule10Data2 = "L"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule11Data2 = "T"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule12Data2 = "B"u8;
                            global::System.ReadOnlySpan<byte> MessageDataBranchRule13Data2 = "C"u8;
                            if (messageTypeValue.SequenceEqual(MessageDataBranchRule0Data2)) { messageDataCase = MessageDataBranchRule0Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule1Data2)) { messageDataCase = MessageDataBranchRule1Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule2Data2)) { messageDataCase = MessageDataBranchRule2Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule3Data2)) { messageDataCase = MessageDataBranchRule3Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule4Data2)) { messageDataCase = MessageDataBranchRule4Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule5Data2)) { messageDataCase = MessageDataBranchRule5Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule6Data2)) { messageDataCase = MessageDataBranchRule6Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule7Data2)) { messageDataCase = MessageDataBranchRule7Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule8Data2)) { messageDataCase = MessageDataBranchRule8Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule9Data2)) { messageDataCase = MessageDataBranchRule9Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule10Data2)) { messageDataCase = MessageDataBranchRule10Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule11Data2)) { messageDataCase = MessageDataBranchRule11Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule12Data2)) { messageDataCase = MessageDataBranchRule12Case; }
                            else if (messageTypeValue.SequenceEqual(MessageDataBranchRule13Data2)) { messageDataCase = MessageDataBranchRule13Case; }
                        }
                        if (messageDataCase == MessageDataBranchRule0Case)
                        {
                            var parsed = new SystemEventMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule1Case)
                        {
                            var parsed = new SecurityDirectoryMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule2Case)
                        {
                            var parsed = new TradingStatusMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule3Case)
                        {
                            var parsed = new RetailLiquidityIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule4Case)
                        {
                            var parsed = new OperationalHaltStatusMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule5Case)
                        {
                            var parsed = new ShortSalePriceTestStatusMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule6Case)
                        {
                            var parsed = new SecurityEventMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule7Case)
                        {
                            var parsed = new AddOrderMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule8Case)
                        {
                            var parsed = new OrderModifyMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule9Case)
                        {
                            var parsed = new OrderDeleteMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule10Case)
                        {
                            var parsed = new OrderExecutedMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule11Case)
                        {
                            var parsed = new TradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule12Case)
                        {
                            var parsed = new TradeBreakMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else if (messageDataCase == MessageDataBranchRule13Case)
                        {
                            var parsed = new ClearBookMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..messageEnd], ref position);
                            found = parsed;
                        }
                        else
                        {
                            {
                                if (messageEnd < position + messageTypeDependencyDistanceFromMessageData + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (PacketMessageCode)buffer.Slice(position + messageTypeDependencyDistanceFromMessageData, messageTypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownPacketMessage.Parse(buffer[..messageEnd], ref position).SetDataValue(code);
                            }
                        }
                        foundDataValue = ((IPacketMessage)found).DataValue;
                        frameStart = messageEntryStart;
                        foundFinish = position;
                        payloadFinish = position;
                        if (found.Status != ParseStatus.Ok)
                        {
                            Current = new PacketFramedMessage(foundDataValue, target.MessagesKind, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                            state = Ended;
                            return true;
                        }
                        messageFrame!.messageData = (IPacketMessage)found;
                        goto case MessageDataReached;
                    }
                }
            }
            case MessagesContinued:
            {
                if (position < messagesEnd)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..messagesEnd];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..messagesEnd]);
                        payloadFinish = messagesEnd;
                    }
                }
                position = messagesEnd;
                countedFinish = Unset;
                foundFinish = Unset;
                position = buffer.Length;
                if (state == Start)
                {
                    Current = new PacketFramedMessage(default, target.MessagesKind, buffer[0..position], buffer[position..position], position - 0, null);
                    state = Ended;
                    return true;
                }
                state = Ended;
                return false;
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
