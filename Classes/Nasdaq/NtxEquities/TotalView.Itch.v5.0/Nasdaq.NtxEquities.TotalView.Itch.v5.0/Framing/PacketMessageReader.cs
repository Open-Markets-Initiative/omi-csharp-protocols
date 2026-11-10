using System.Buffers.Binary;
using static Nasdaq.NtxEquities.TotalView.Itch.v5_0.Rules;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

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
    private const int PayloadReached = 1;
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
    int messagesCase;
    ushort messageCountValue;
    global::System.UInt128 MessageCountRule0Entries;
    int messageEntryStart;
    int messageEnd;
    int MessageSizeRule1End;
    global::System.Int128 MessageSizeRule1Width;
    ushort messageLengthValue;
    int messageHeaderEntryStart;
    int payloadEntryStart;
    int payloadEnd;
    int PayloadSizeRule0End;
    global::System.Int128 PayloadSizeRule0Width;
    int payloadCase;
    ReadOnlySpan<byte> messageTypeValue;
    const int messageCountDependencyDistanceFromMessages = -2;
    const int messageCountDependencyReadWidth = 2;
    const int messageLengthDependencyDistanceFromMessage = 0;
    const int messageLengthDependencyReadWidth = 2;
    const int messageTypeDependencyDistanceFromPayload = -1;
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
                const int SessionWidth = 10;
                if (!Take(SessionWidth, buffer.Length)) return false;
                const int SequenceNumberWidth = 8;
                if (!Take(SequenceNumberWidth, buffer.Length)) return false;
                const int MessageCountWidth = 2;
                if (!Take(MessageCountWidth, buffer.Length)) return false;
                {
                    const int MessagesNoCase = 0;
                    const int MessagesBranchRule0Case = 1;
                    const int MessagesBranchRule1Case = 2;
                    const int MessagesBranchRule2Case = 3;
                    messagesCase = MessagesNoCase;
                    {
                        if (buffer.Length < position + messageCountDependencyDistanceFromMessages + messageCountDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(position + messageCountDependencyDistanceFromMessages, messageCountDependencyReadWidth));
                        const ushort MessagesBranchRule0Data2 = 0;
                        const ushort MessagesBranchRule1Data2 = 65535;
                        if ((messageCountValue == MessagesBranchRule0Data2)) { messagesCase = MessagesBranchRule0Case; }
                        else if ((messageCountValue == MessagesBranchRule1Data2)) { messagesCase = MessagesBranchRule1Case; }
                        else { messagesCase = MessagesBranchRule2Case; }
                    }
                    if (messagesCase == MessagesBranchRule0Case)
                    {
                        target.MessagesKind = MessagesKind.Heartbeat;
                        goto case MessagesContinued;
                    }
                    else if (messagesCase == MessagesBranchRule1Case)
                    {
                        target.MessagesKind = MessagesKind.EndOfSession;
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
            case PayloadReached:
            {
                if (position < payloadEnd)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..payloadEnd];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..payloadEnd]);
                        payloadFinish = payloadEnd;
                    }
                }
                position = payloadEnd;
                countedFinish = Unset;
                foundFinish = Unset;
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
                        if (buffer.Length < position + messageLengthDependencyDistanceFromMessage + messageLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(position + messageLengthDependencyDistanceFromMessage, messageLengthDependencyReadWidth));
                        MessageSizeRule1Width = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(MessageSizeRule1Data2))));
                    }
                    if (MessageSizeRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (MessageSizeRule1Width > (long)buffer.Length - position) return Stop(ReadStatus.Truncated);
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
                    payloadEntryStart = position;
                    const long PayloadSizeRule0Data2 = 1;
                    PayloadSizeRule0Width = checked((checked((global::System.Int128)(messageLengthValue)) - ((global::System.Int128)(PayloadSizeRule0Data2))));
                    if (PayloadSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (PayloadSizeRule0Width > (long)messageEnd - position) return Stop(ReadStatus.Truncated);
                    PayloadSizeRule0End = position + (int)PayloadSizeRule0Width;
                    payloadEnd = PayloadSizeRule0End;
                    {
                        const int PayloadNoCase = 0;
                        const int PayloadBranchRule1Case = 1;
                        const int PayloadBranchRule2Case = 2;
                        const int PayloadBranchRule3Case = 3;
                        const int PayloadBranchRule4Case = 4;
                        const int PayloadBranchRule5Case = 5;
                        const int PayloadBranchRule6Case = 6;
                        const int PayloadBranchRule7Case = 7;
                        const int PayloadBranchRule8Case = 8;
                        const int PayloadBranchRule9Case = 9;
                        const int PayloadBranchRule10Case = 10;
                        const int PayloadBranchRule11Case = 11;
                        const int PayloadBranchRule12Case = 12;
                        const int PayloadBranchRule13Case = 13;
                        const int PayloadBranchRule14Case = 14;
                        const int PayloadBranchRule15Case = 15;
                        const int PayloadBranchRule16Case = 16;
                        const int PayloadBranchRule17Case = 17;
                        const int PayloadBranchRule18Case = 18;
                        const int PayloadBranchRule19Case = 19;
                        const int PayloadBranchRule20Case = 20;
                        const int PayloadBranchRule21Case = 21;
                        payloadCase = PayloadNoCase;
                        {
                            if (payloadEnd < position + messageTypeDependencyDistanceFromPayload + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            messageTypeValue = buffer.Slice(position + messageTypeDependencyDistanceFromPayload, messageTypeDependencyReadWidth);
                            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = "S"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = "R"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = "H"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = "Y"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = "L"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = "V"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = "W"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = "J"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = "h"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = "A"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = "F"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = "E"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = "C"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = "X"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = "D"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = "U"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = "P"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = "Q"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = "B"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = "I"u8;
                            global::System.ReadOnlySpan<byte> PayloadBranchRule21Data2 = "N"u8;
                            if (messageTypeValue.SequenceEqual(PayloadBranchRule1Data2)) { payloadCase = PayloadBranchRule1Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule2Data2)) { payloadCase = PayloadBranchRule2Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule3Data2)) { payloadCase = PayloadBranchRule3Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule4Data2)) { payloadCase = PayloadBranchRule4Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule5Data2)) { payloadCase = PayloadBranchRule5Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule6Data2)) { payloadCase = PayloadBranchRule6Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule7Data2)) { payloadCase = PayloadBranchRule7Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule8Data2)) { payloadCase = PayloadBranchRule8Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule9Data2)) { payloadCase = PayloadBranchRule9Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule10Data2)) { payloadCase = PayloadBranchRule10Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule11Data2)) { payloadCase = PayloadBranchRule11Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule12Data2)) { payloadCase = PayloadBranchRule12Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule13Data2)) { payloadCase = PayloadBranchRule13Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule14Data2)) { payloadCase = PayloadBranchRule14Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule15Data2)) { payloadCase = PayloadBranchRule15Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule16Data2)) { payloadCase = PayloadBranchRule16Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule17Data2)) { payloadCase = PayloadBranchRule17Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule18Data2)) { payloadCase = PayloadBranchRule18Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule19Data2)) { payloadCase = PayloadBranchRule19Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule20Data2)) { payloadCase = PayloadBranchRule20Case; }
                            else if (messageTypeValue.SequenceEqual(PayloadBranchRule21Data2)) { payloadCase = PayloadBranchRule21Case; }
                        }
                        if (payloadCase == PayloadBranchRule1Case)
                        {
                            var parsed = new SystemEventMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule2Case)
                        {
                            var parsed = new StockDirectoryMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule3Case)
                        {
                            var parsed = new StockTradingActionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule4Case)
                        {
                            var parsed = new RegShoShortSalePriceTestRestrictedIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule5Case)
                        {
                            var parsed = new MarketParticipantPositionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule6Case)
                        {
                            var parsed = new MwcbDeclineLevelMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule7Case)
                        {
                            var parsed = new MwcbStatusMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule8Case)
                        {
                            var parsed = new LuldAuctionCollarMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule9Case)
                        {
                            var parsed = new OperationalHaltMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule10Case)
                        {
                            var parsed = new AddOrderNoMpidAttributionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule11Case)
                        {
                            var parsed = new AddOrderMpidAttributionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule12Case)
                        {
                            var parsed = new OrderExecutedMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule13Case)
                        {
                            var parsed = new OrderExecutedWithPriceMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule14Case)
                        {
                            var parsed = new OrderCancelMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule15Case)
                        {
                            var parsed = new OrderDeleteMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule16Case)
                        {
                            var parsed = new OrderReplaceMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule17Case)
                        {
                            var parsed = new NonCrossTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule18Case)
                        {
                            var parsed = new CrossTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule19Case)
                        {
                            var parsed = new BrokenTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule20Case)
                        {
                            var parsed = new NetOrderImbalanceIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else if (payloadCase == PayloadBranchRule21Case)
                        {
                            var parsed = new RetailInterestMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..payloadEnd], ref position);
                            found = parsed;
                        }
                        else
                        {
                            {
                                if (payloadEnd < position + messageTypeDependencyDistanceFromPayload + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (PacketMessageCode)buffer.Slice(position + messageTypeDependencyDistanceFromPayload, messageTypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownPacketMessage.Parse(buffer[..payloadEnd], ref position).SetDataValue(code);
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
                        messageFrame!.payload = (IPacketMessage)found;
                        goto case PayloadReached;
                    }
                }
            }
            case MessagesContinued:
            {
                if (position < buffer.Length)
                {
                    if (countedFinish != Unset && countedFinish >= foundFinish)
                    {
                        trailerBytes = buffer[position..buffer.Length];
                        Status = ReadStatus.TrailingBytes;
                    }
                    else if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..buffer.Length]);
                        payloadFinish = buffer.Length;
                    }
                }
                position = buffer.Length;
                countedFinish = Unset;
                foundFinish = Unset;
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
