using System.Buffers.Binary;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Rules;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct FileFramedMessage
{
    internal FileFramedMessage(MessageCode dataValue, ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IFileMessage? message)
    {
        DataValue = dataValue;
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public MessageCode DataValue { get; }
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
    public IFileMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct FileMessageReader
{
    private const int Start = 0;
    private const int FilePayloadReached = 1;
    private const int FileMessageOccurrence = 2;
    private const int Ended = 3;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly FileValue target;
    private FileMessageFrame? fileMessageFrame;
    private const int Unset = -1;
    private IFileMessage found = null!;
    private int frameStart;
    private int foundStart;
    private int foundFinish = Unset;
    private int payloadFinish;
    private MessageCode foundDataValue;
    int FileMessagePayloadRule0End;
    global::System.Int128 FileMessagePayloadRule0RawEnd;
    int fileMessageEntryStart;
    int fileMessageEnd;
    int FileMessageReassembleRule1End;
    global::System.Int128 FileMessageReassembleRule1Width;
    ushort messageLengthValue;
    int FileMessageSizeRule2End;
    global::System.Int128 FileMessageSizeRule2Width;
    int messageHeaderEntryStart;
    int filePayloadEntryStart;
    int filePayloadEnd;
    int FilePayloadSizeRule0End;
    global::System.Int128 FilePayloadSizeRule0Width;
    int filePayloadCase;
    ReadOnlySpan<byte> messageTypeValue;
    const int messageLengthDependencyDistanceFromFileMessage = 0;
    const int messageLengthDependencyReadWidth = 2;
    const int messageTypeDependencyDistanceFromFilePayload = -1;
    const int messageTypeDependencyReadWidth = 1;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FileMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    public FileMessageReader(ReadOnlySpan<byte> buffer) : this(buffer, new FileValue())
    {
    }

    internal FileMessageReader(ReadOnlySpan<byte> buffer, FileValue target)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public FileFramedMessage Current { get; private set; }
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
                    FileMessagePayloadRule0RawEnd = checked((global::System.Int128)(buffer.Length));
                    if (FileMessagePayloadRule0RawEnd < position || FileMessagePayloadRule0RawEnd > buffer.Length) return Stop(ReadStatus.Truncated);
                    FileMessagePayloadRule0End = (int)FileMessagePayloadRule0RawEnd;
                }
                goto case FileMessageOccurrence;
            }
            case FilePayloadReached:
            {
                if (position < filePayloadEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..filePayloadEnd]);
                        payloadFinish = filePayloadEnd;
                    }
                }
                position = filePayloadEnd;
                foundFinish = Unset;
                if (position < fileMessageEnd)
                {
                    if (position == foundFinish)
                    {
                        found.SetTrailer(buffer[position..fileMessageEnd]);
                        payloadFinish = fileMessageEnd;
                    }
                }
                position = fileMessageEnd;
                foundFinish = Unset;
                if (position <= fileMessageEntryStart || position > FileMessagePayloadRule0End) return Stop(ReadStatus.InvalidLength);
                Current = new FileFramedMessage(foundDataValue, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                state = FileMessageOccurrence;
                return true;
            }
            case FileMessageOccurrence:
            {
                if (!(position < FileMessagePayloadRule0End))
                {
                    {
                        if (position != FileMessagePayloadRule0End) return Stop(ReadStatus.InvalidLength);
                        position = FileMessagePayloadRule0End;
                        foundFinish = Unset;
                        position = buffer.Length;
                        if (state == Start)
                        {
                            Current = new FileFramedMessage(default, buffer[0..position], buffer[position..position], position - 0, null);
                            state = Ended;
                            return true;
                        }
                        state = Ended;
                        return false;
                    }
                }
                fileMessageEntryStart = position;
                {
                    fileMessageFrame = new FileMessageFrame();
                    target.FileMessageFrames.Add(fileMessageFrame);
                    const long FileMessageReassembleRule1Data2 = 2;
                    {
                        if (FileMessagePayloadRule0End < position + messageLengthDependencyDistanceFromFileMessage + messageLengthDependencyReadWidth) return Stop(ReadStatus.Truncated);
                        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(buffer.Slice(position + messageLengthDependencyDistanceFromFileMessage, messageLengthDependencyReadWidth));
                        FileMessageReassembleRule1Width = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(FileMessageReassembleRule1Data2))));
                    }
                    if (FileMessageReassembleRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (FileMessageReassembleRule1Width > (long)FileMessagePayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    FileMessageReassembleRule1End = position + (int)FileMessageReassembleRule1Width;
                    const long FileMessageSizeRule2Data2 = 2;
                    FileMessageSizeRule2Width = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(FileMessageSizeRule2Data2))));
                    if (FileMessageSizeRule2Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (FileMessageSizeRule2Width > (long)FileMessagePayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    FileMessageSizeRule2End = position + (int)FileMessageSizeRule2Width;
                    fileMessageEnd = FileMessageReassembleRule1End;
                    if (FileMessageSizeRule2End != fileMessageEnd) return Stop(ReadStatus.InvalidLength);
                    messageHeaderEntryStart = position;
                    const int MessageLengthWidth = 2;
                    if (!Take(MessageLengthWidth, fileMessageEnd)) return false;
                    const int MessageTypeWidth = 1;
                    if (!Take(MessageTypeWidth, fileMessageEnd)) return false;
                    {
                        var messageHeaderParsed = new MessageHeader();
                        var messageHeaderOffset = messageHeaderEntryStart;
                        messageHeaderParsed.ParseFrom(buffer[..fileMessageEnd], ref messageHeaderOffset);
                        fileMessageFrame!.SetMessageHeader(messageHeaderParsed);
                    }
                    filePayloadEntryStart = position;
                    const long FilePayloadSizeRule0Data2 = 1;
                    FilePayloadSizeRule0Width = checked((checked((global::System.Int128)(messageLengthValue)) - ((global::System.Int128)(FilePayloadSizeRule0Data2))));
                    if (FilePayloadSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (FilePayloadSizeRule0Width > (long)fileMessageEnd - position) return Stop(ReadStatus.Truncated);
                    FilePayloadSizeRule0End = position + (int)FilePayloadSizeRule0Width;
                    filePayloadEnd = FilePayloadSizeRule0End;
                    {
                        const int FilePayloadNoCase = 0;
                        const int FilePayloadBranchRule1Case = 1;
                        const int FilePayloadBranchRule2Case = 2;
                        const int FilePayloadBranchRule3Case = 3;
                        const int FilePayloadBranchRule4Case = 4;
                        const int FilePayloadBranchRule5Case = 5;
                        const int FilePayloadBranchRule6Case = 6;
                        const int FilePayloadBranchRule7Case = 7;
                        const int FilePayloadBranchRule8Case = 8;
                        const int FilePayloadBranchRule9Case = 9;
                        const int FilePayloadBranchRule10Case = 10;
                        const int FilePayloadBranchRule11Case = 11;
                        const int FilePayloadBranchRule12Case = 12;
                        const int FilePayloadBranchRule13Case = 13;
                        const int FilePayloadBranchRule14Case = 14;
                        const int FilePayloadBranchRule15Case = 15;
                        const int FilePayloadBranchRule16Case = 16;
                        const int FilePayloadBranchRule17Case = 17;
                        const int FilePayloadBranchRule18Case = 18;
                        const int FilePayloadBranchRule19Case = 19;
                        const int FilePayloadBranchRule20Case = 20;
                        const int FilePayloadBranchRule21Case = 21;
                        const int FilePayloadBranchRule22Case = 22;
                        const int FilePayloadBranchRule23Case = 23;
                        filePayloadCase = FilePayloadNoCase;
                        {
                            if (filePayloadEnd < position + messageTypeDependencyDistanceFromFilePayload + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                            messageTypeValue = buffer.Slice(position + messageTypeDependencyDistanceFromFilePayload, messageTypeDependencyReadWidth);
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule1Data2 = "S"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule2Data2 = "R"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule3Data2 = "H"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule4Data2 = "Y"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule5Data2 = "L"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule6Data2 = "V"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule7Data2 = "W"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule8Data2 = "K"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule9Data2 = "J"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule10Data2 = "h"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule11Data2 = "A"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule12Data2 = "F"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule13Data2 = "E"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule14Data2 = "C"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule15Data2 = "X"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule16Data2 = "D"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule17Data2 = "U"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule18Data2 = "P"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule19Data2 = "Q"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule20Data2 = "B"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule21Data2 = "I"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule22Data2 = "N"u8;
                            global::System.ReadOnlySpan<byte> FilePayloadBranchRule23Data2 = "O"u8;
                            if (messageTypeValue.SequenceEqual(FilePayloadBranchRule1Data2)) { filePayloadCase = FilePayloadBranchRule1Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule2Data2)) { filePayloadCase = FilePayloadBranchRule2Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule3Data2)) { filePayloadCase = FilePayloadBranchRule3Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule4Data2)) { filePayloadCase = FilePayloadBranchRule4Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule5Data2)) { filePayloadCase = FilePayloadBranchRule5Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule6Data2)) { filePayloadCase = FilePayloadBranchRule6Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule7Data2)) { filePayloadCase = FilePayloadBranchRule7Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule8Data2)) { filePayloadCase = FilePayloadBranchRule8Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule9Data2)) { filePayloadCase = FilePayloadBranchRule9Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule10Data2)) { filePayloadCase = FilePayloadBranchRule10Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule11Data2)) { filePayloadCase = FilePayloadBranchRule11Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule12Data2)) { filePayloadCase = FilePayloadBranchRule12Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule13Data2)) { filePayloadCase = FilePayloadBranchRule13Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule14Data2)) { filePayloadCase = FilePayloadBranchRule14Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule15Data2)) { filePayloadCase = FilePayloadBranchRule15Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule16Data2)) { filePayloadCase = FilePayloadBranchRule16Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule17Data2)) { filePayloadCase = FilePayloadBranchRule17Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule18Data2)) { filePayloadCase = FilePayloadBranchRule18Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule19Data2)) { filePayloadCase = FilePayloadBranchRule19Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule20Data2)) { filePayloadCase = FilePayloadBranchRule20Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule21Data2)) { filePayloadCase = FilePayloadBranchRule21Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule22Data2)) { filePayloadCase = FilePayloadBranchRule22Case; }
                            else if (messageTypeValue.SequenceEqual(FilePayloadBranchRule23Data2)) { filePayloadCase = FilePayloadBranchRule23Case; }
                        }
                        if (filePayloadCase == FilePayloadBranchRule1Case)
                        {
                            var parsed = new SystemEventMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule2Case)
                        {
                            var parsed = new StockDirectoryMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule3Case)
                        {
                            var parsed = new StockTradingActionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule4Case)
                        {
                            var parsed = new RegShoShortSalePriceTestRestrictedIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule5Case)
                        {
                            var parsed = new MarketParticipantPositionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule6Case)
                        {
                            var parsed = new MwcbDeclineLevelMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule7Case)
                        {
                            var parsed = new MwcbStatusLevelMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule8Case)
                        {
                            var parsed = new IpoQuotingPeriodUpdate();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule9Case)
                        {
                            var parsed = new LuldAuctionCollarMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule10Case)
                        {
                            var parsed = new OperationalHaltMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule11Case)
                        {
                            var parsed = new AddOrderNoMpidAttributionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule12Case)
                        {
                            var parsed = new AddOrderWithMpidAttributionMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule13Case)
                        {
                            var parsed = new OrderExecutedMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule14Case)
                        {
                            var parsed = new OrderExecutedWithPriceMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule15Case)
                        {
                            var parsed = new OrderCancelMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule16Case)
                        {
                            var parsed = new OrderDeleteMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule17Case)
                        {
                            var parsed = new OrderReplaceMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule18Case)
                        {
                            var parsed = new NonCrossTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule19Case)
                        {
                            var parsed = new CrossTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule20Case)
                        {
                            var parsed = new BrokenTradeMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule21Case)
                        {
                            var parsed = new NetOrderImbalanceIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule22Case)
                        {
                            var parsed = new RetailPriceImprovementIndicatorMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else if (filePayloadCase == FilePayloadBranchRule23Case)
                        {
                            var parsed = new DirectListingWithCapitalRaisePriceDiscoveryMessage();
                            foundStart = position;
                            parsed.ParseFrom(buffer[..filePayloadEnd], ref position);
                            found = parsed;
                        }
                        else
                        {
                            {
                                if (filePayloadEnd < position + messageTypeDependencyDistanceFromFilePayload + messageTypeDependencyReadWidth) return Stop(ReadStatus.Truncated);
                                var code = (MessageCode)buffer.Slice(position + messageTypeDependencyDistanceFromFilePayload, messageTypeDependencyReadWidth)[0];
                                foundStart = position;
                                found = UnknownFileMessage.Parse(buffer[..filePayloadEnd], ref position).SetDataValue(code);
                            }
                        }
                        foundDataValue = ((IFileMessage)found).DataValue;
                        frameStart = fileMessageEntryStart;
                        foundFinish = position;
                        payloadFinish = position;
                        if (found.Status != ParseStatus.Ok)
                        {
                            Current = new FileFramedMessage(foundDataValue, buffer[frameStart..foundStart], buffer[foundStart..payloadFinish], position - frameStart, found);
                            state = Ended;
                            return true;
                        }
                        fileMessageFrame!.filePayload = (IFileMessage)found;
                        goto case FilePayloadReached;
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
