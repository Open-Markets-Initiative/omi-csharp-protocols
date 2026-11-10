using System.Text;
using System.Buffers.Binary;
using static Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Rules;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Nasdaq historical file: every message of the day, each preceded by its length
/// </summary>
public sealed class FileValue : IPacket
{
    IHeader? IPacket.Header => null;

    /// <summary>
    ///  Gets the File Message entries, in wire order.
    /// </summary>
    public List<FileMessageFrame> FileMessageFrames { get; } = [];

    /// <summary>
    ///  Adds a File Message entry that carries the message.
    /// </summary>
    /// <param name="message">The message that the entry carries.</param>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    /// <exception cref="ArgumentNullException">The value is <see langword="null"/>.</exception>
    public FileMessageFrame Add(IFileMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        var entry = new FileMessageFrame { FilePayload = message };
        FileMessageFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public FileFramedMessages Messages { get; }

    IReadOnlyList<IMessage> IPacket.Messages => Messages;

    /// <summary>
    ///  Gets a value indicating whether the header and every message of the packet parsed, with no message missing.
    /// </summary>
    /// <remarks>
    ///  Unlike <see cref="IsValid"/>, this property keeps the result of the parse when a value changes. It does not test whether the protocol defines each value. A packet with an unknown message type or an undefined value can be parsed. The value is <see langword="true"/> for a packet that is created in code rather than parsed.
    /// </remarks>
    public bool IsParsed { get; private set; } = true;

    /// <summary>
    ///  Gets a value indicating whether the protocol specification allows every value of the header and of each message.
    /// </summary>
    /// <remarks>
    ///  The value is computed from the current values each time that it is read. It is <see langword="true"/> when <see cref="Validate"/> reports no failures. It is <see langword="false"/> for a message of a type that the protocol does not define. It does not show whether the parse succeeded. A field that the parse did not reach holds its cleared value, so check <see cref="IsParsed"/> first.
    /// </remarks>
    public bool IsValid => Validate().Count == 0;

    ReadOnlyMemory<byte> IPacket.Trailer => ReadOnlyMemory<byte>.Empty;

    /// <summary>
    ///  Initializes a new instance of the <see cref="FileValue"/> class with default values and no messages.
    /// </summary>
    public FileValue()
    {
        Messages = new FileFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(FileValue other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.FileMessageFrames.Clear();
        this.FileMessageFrames.AddRange(other.FileMessageFrames.Select(static entry => entry.Copy()));
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.FileMessageFrames.Clear();
        Messages.Keep(null);
        this.IsParsed = true;
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static FileValue Parse(ReadOnlySpan<byte> data)
    {
        var packet = new FileValue();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var messages = new FileFramedMessages(data, this);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Gets the length of the File Message frame at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the frame.</param>
    /// <param name="length">The number of bytes of the frame, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state a frame length that is not valid.</exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state a frame length that is not valid.";
        const int messageLengthDependencyDistanceFromFileMessage = 0;
        const int messageLengthDependencyReadWidth = 2;
        global::System.Int128 FileMessageReassembleRule1Width;
        ushort messageLengthValue;
        length = 0;
        const long FileMessageReassembleRule1Data2 = 2;
        {
            if (source.Length < messageLengthDependencyDistanceFromFileMessage + messageLengthDependencyReadWidth) return false;
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(source.Slice(messageLengthDependencyDistanceFromFileMessage, messageLengthDependencyReadWidth));
            FileMessageReassembleRule1Width = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(FileMessageReassembleRule1Data2))));
        }
        if (FileMessageReassembleRule1Width <= RuleEmptyWidth || FileMessageReassembleRule1Width > int.MaxValue) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        length = (int)FileMessageReassembleRule1Width;
        return true;
    }

    internal IEnumerable<IFileMessage> Collect()
    {
        foreach (var entry in this.FileMessageFrames)
        {
            foreach (var message in entry.Collect())
            {
                yield return message;
            }
        }
        yield break;
    }

    private const int MessageLengthLength = 2;
    private static readonly FieldDescriptor MessageLengthField = new("MessageLength", FieldKind.UnsignedInteger, MessageLengthLength) { ByteOrder = "Big", IsSigned = false };
    private const int MessageTypeLength = 1;
    private static readonly FieldDescriptor MessageTypeField = new("MessageType", FieldKind.Enum, MessageTypeLength);

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var length = 0;
            var fileMessageMeasuredEntries = this.FileMessageFrames;
            var fileMessageMeasuredFirst = 0;
            while (fileMessageMeasuredFirst < fileMessageMeasuredEntries.Count)
            {
                var fileMessageMeasuredFrame = fileMessageMeasuredEntries[fileMessageMeasuredFirst];
                length += MessageHeader.MessageByteLength;
                if (fileMessageMeasuredFrame.FilePayload is not null)
                {
                    length += ((IMessage)fileMessageMeasuredFrame.FilePayload!).EncodedByteLength + ((IMessage)fileMessageMeasuredFrame.FilePayload!).Trailer.Length;
                }
                fileMessageMeasuredFirst++;
            }
            return length;
        }
    }

    /// <summary>
    ///  Encodes the packet into the start of the span.
    /// </summary>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data)
    {
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!Write(data, length, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the packet into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the reason and the first field whose value cannot be encoded, if any. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes.</param>
    /// <returns><see langword="true"/> if the packet was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  The field is in the header or in a message, or is a length. For a field in a group entry or nested message, the failure also names the group or field holding it. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!Write(data, length, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    private bool Write(Span<byte> data, int length, out int cursor, out EncodeFailure failure)
    {
        cursor = 0;
        var index = 0;
        const int messageTypeDependencyDistanceFromMessageHeader = 2;
        const int messageTypeDependencyReadWidth = 1;
        const int messageLengthDependencyDistanceFromMessageHeader = 0;
        const int messageLengthDependencyReadWidth = 2;
        var fileWriterStart = cursor;
        var fileMessageWriterPosition = cursor;
        var fileMessageMeasuredBytes = 0;
        {
            var fileMessageMeasuredEntries = this.FileMessageFrames;
            var fileMessageMeasuredFirst = 0;
            while (fileMessageMeasuredFirst < fileMessageMeasuredEntries.Count)
            {
                var fileMessageMeasuredFrame = fileMessageMeasuredEntries[fileMessageMeasuredFirst];
                fileMessageMeasuredBytes += MessageHeader.MessageByteLength;
                if (fileMessageMeasuredFrame.FilePayload is not null)
                {
                    fileMessageMeasuredBytes += ((IMessage)fileMessageMeasuredFrame.FilePayload!).EncodedByteLength + ((IMessage)fileMessageMeasuredFrame.FilePayload!).Trailer.Length;
                }
                fileMessageMeasuredFirst++;
            }
        }
        var fileMessageWriterEntries = this.FileMessageFrames;
        var fileMessageIndex = 0;
        while (fileMessageIndex < fileMessageWriterEntries.Count)
        {
            var fileMessageWriterFrame = fileMessageWriterEntries[fileMessageIndex];
            var fileMessageWriterStart = cursor;
            var fileMessageOccurrenceBytes = 0;
            {
                fileMessageOccurrenceBytes += MessageHeader.MessageByteLength;
                if (fileMessageWriterFrame.FilePayload is not null)
                {
                    fileMessageOccurrenceBytes += ((IMessage)fileMessageWriterFrame.FilePayload!).EncodedByteLength + ((IMessage)fileMessageWriterFrame.FilePayload!).Trailer.Length;
                }
            }
            global::System.Int128 messageLengthStampedValue;
            {
                const long FileMessageReassembleRule1Data2 = 2;
                global::System.Int128 measure = fileMessageOccurrenceBytes;
                try
                {
                    messageLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(FileMessageReassembleRule1Data2))))));
                }
                catch (global::System.OverflowException)
                {
                    failure = new EncodeFailure(MessageLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            if (messageLengthStampedValue > ushort.MaxValue)
            {
                failure = new EncodeFailure(MessageLengthField, EncodeStatus.TooLong, index);
                return false;
            }
            if (messageLengthStampedValue < ushort.MinValue)
            {
                failure = new EncodeFailure(MessageLengthField, EncodeStatus.DoesNotFit, index);
                return false;
            }
            var messageHeaderWriterStart = cursor;
            var messageHeaderWriting = fileMessageWriterFrame.MessageHeader;
            if (!((IGroupEntry)messageHeaderWriting).Write(data, cursor, out cursor, out failure))
            {
                failure = failure.InMessage(index);
                return false;
            }
            var filePayloadWriterStart = cursor;
            var filePayloadOccurrenceBytes = 0;
            {
                if (fileMessageWriterFrame.FilePayload is not null)
                {
                    filePayloadOccurrenceBytes += ((IMessage)fileMessageWriterFrame.FilePayload!).EncodedByteLength + ((IMessage)fileMessageWriterFrame.FilePayload!).Trailer.Length;
                }
            }
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
                const int FilePayloadNothingWritten = -1;
                var filePayloadWrittenCase = FilePayloadNothingWritten;
                if (fileMessageWriterFrame.FilePayload is SystemEventMessage)
                {
                    var systemEventMessageWriterStart = cursor;
                    var systemEventMessageWriting = (SystemEventMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)systemEventMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    systemEventMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += systemEventMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)systemEventMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is StockDirectoryMessage)
                {
                    var stockDirectoryMessageWriterStart = cursor;
                    var stockDirectoryMessageWriting = (StockDirectoryMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)stockDirectoryMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    stockDirectoryMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += stockDirectoryMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)stockDirectoryMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is StockTradingActionMessage)
                {
                    var stockTradingActionMessageWriterStart = cursor;
                    var stockTradingActionMessageWriting = (StockTradingActionMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)stockTradingActionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    stockTradingActionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += stockTradingActionMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)stockTradingActionMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is RegShoShortSalePriceTestRestrictedIndicatorMessage)
                {
                    var regShoShortSalePriceTestRestrictedIndicatorMessageWriterStart = cursor;
                    var regShoShortSalePriceTestRestrictedIndicatorMessageWriting = (RegShoShortSalePriceTestRestrictedIndicatorMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)regShoShortSalePriceTestRestrictedIndicatorMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    regShoShortSalePriceTestRestrictedIndicatorMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += regShoShortSalePriceTestRestrictedIndicatorMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)regShoShortSalePriceTestRestrictedIndicatorMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is MarketParticipantPositionMessage)
                {
                    var marketParticipantPositionMessageWriterStart = cursor;
                    var marketParticipantPositionMessageWriting = (MarketParticipantPositionMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)marketParticipantPositionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    marketParticipantPositionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += marketParticipantPositionMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)marketParticipantPositionMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is MwcbDeclineLevelMessage)
                {
                    var mwcbDeclineLevelMessageWriterStart = cursor;
                    var mwcbDeclineLevelMessageWriting = (MwcbDeclineLevelMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)mwcbDeclineLevelMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    mwcbDeclineLevelMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += mwcbDeclineLevelMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)mwcbDeclineLevelMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is MwcbStatusLevelMessage)
                {
                    var mwcbStatusLevelMessageWriterStart = cursor;
                    var mwcbStatusLevelMessageWriting = (MwcbStatusLevelMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)mwcbStatusLevelMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    mwcbStatusLevelMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += mwcbStatusLevelMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)mwcbStatusLevelMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is IpoQuotingPeriodUpdate)
                {
                    var ipoQuotingPeriodUpdateWriterStart = cursor;
                    var ipoQuotingPeriodUpdateWriting = (IpoQuotingPeriodUpdate)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)ipoQuotingPeriodUpdateWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    ipoQuotingPeriodUpdateWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += ipoQuotingPeriodUpdateWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)ipoQuotingPeriodUpdateWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is LuldAuctionCollarMessage)
                {
                    var luldAuctionCollarMessageWriterStart = cursor;
                    var luldAuctionCollarMessageWriting = (LuldAuctionCollarMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)luldAuctionCollarMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    luldAuctionCollarMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += luldAuctionCollarMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)luldAuctionCollarMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OperationalHaltMessage)
                {
                    var operationalHaltMessageWriterStart = cursor;
                    var operationalHaltMessageWriting = (OperationalHaltMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)operationalHaltMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    operationalHaltMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += operationalHaltMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)operationalHaltMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is AddOrderNoMpidAttributionMessage)
                {
                    var addOrderNoMpidAttributionMessageWriterStart = cursor;
                    var addOrderNoMpidAttributionMessageWriting = (AddOrderNoMpidAttributionMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)addOrderNoMpidAttributionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    addOrderNoMpidAttributionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += addOrderNoMpidAttributionMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)addOrderNoMpidAttributionMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is AddOrderWithMpidAttributionMessage)
                {
                    var addOrderWithMpidAttributionMessageWriterStart = cursor;
                    var addOrderWithMpidAttributionMessageWriting = (AddOrderWithMpidAttributionMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)addOrderWithMpidAttributionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    addOrderWithMpidAttributionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += addOrderWithMpidAttributionMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)addOrderWithMpidAttributionMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OrderExecutedMessage)
                {
                    var orderExecutedMessageWriterStart = cursor;
                    var orderExecutedMessageWriting = (OrderExecutedMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)orderExecutedMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    orderExecutedMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += orderExecutedMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)orderExecutedMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OrderExecutedWithPriceMessage)
                {
                    var orderExecutedWithPriceMessageWriterStart = cursor;
                    var orderExecutedWithPriceMessageWriting = (OrderExecutedWithPriceMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)orderExecutedWithPriceMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    orderExecutedWithPriceMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += orderExecutedWithPriceMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)orderExecutedWithPriceMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OrderCancelMessage)
                {
                    var orderCancelMessageWriterStart = cursor;
                    var orderCancelMessageWriting = (OrderCancelMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)orderCancelMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    orderCancelMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += orderCancelMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)orderCancelMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OrderDeleteMessage)
                {
                    var orderDeleteMessageWriterStart = cursor;
                    var orderDeleteMessageWriting = (OrderDeleteMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)orderDeleteMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    orderDeleteMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += orderDeleteMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)orderDeleteMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is OrderReplaceMessage)
                {
                    var orderReplaceMessageWriterStart = cursor;
                    var orderReplaceMessageWriting = (OrderReplaceMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)orderReplaceMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    orderReplaceMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += orderReplaceMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)orderReplaceMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is NonCrossTradeMessage)
                {
                    var nonCrossTradeMessageWriterStart = cursor;
                    var nonCrossTradeMessageWriting = (NonCrossTradeMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)nonCrossTradeMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    nonCrossTradeMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += nonCrossTradeMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)nonCrossTradeMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is CrossTradeMessage)
                {
                    var crossTradeMessageWriterStart = cursor;
                    var crossTradeMessageWriting = (CrossTradeMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)crossTradeMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    crossTradeMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += crossTradeMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)crossTradeMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is BrokenTradeMessage)
                {
                    var brokenTradeMessageWriterStart = cursor;
                    var brokenTradeMessageWriting = (BrokenTradeMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)brokenTradeMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    brokenTradeMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += brokenTradeMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)brokenTradeMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is NetOrderImbalanceIndicatorMessage)
                {
                    var netOrderImbalanceIndicatorMessageWriterStart = cursor;
                    var netOrderImbalanceIndicatorMessageWriting = (NetOrderImbalanceIndicatorMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)netOrderImbalanceIndicatorMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    netOrderImbalanceIndicatorMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += netOrderImbalanceIndicatorMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)netOrderImbalanceIndicatorMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is RetailPriceImprovementIndicatorMessage)
                {
                    var retailPriceImprovementIndicatorMessageWriterStart = cursor;
                    var retailPriceImprovementIndicatorMessageWriting = (RetailPriceImprovementIndicatorMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)retailPriceImprovementIndicatorMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    retailPriceImprovementIndicatorMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += retailPriceImprovementIndicatorMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)retailPriceImprovementIndicatorMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is DirectListingWithCapitalRaisePriceDiscoveryMessage)
                {
                    var directListingWithCapitalRaisePriceDiscoveryMessageWriterStart = cursor;
                    var directListingWithCapitalRaisePriceDiscoveryMessageWriting = (DirectListingWithCapitalRaisePriceDiscoveryMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)directListingWithCapitalRaisePriceDiscoveryMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    directListingWithCapitalRaisePriceDiscoveryMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += directListingWithCapitalRaisePriceDiscoveryMessageWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)directListingWithCapitalRaisePriceDiscoveryMessageWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                }
                else if (fileMessageWriterFrame.FilePayload is UnknownFileMessage)
                {
                    var filePayloadWriting = (UnknownFileMessage)fileMessageWriterFrame.FilePayload!;
                    if (!((IMessage)filePayloadWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    filePayloadWriting.Trailer.Span.CopyTo(data[cursor..]);
                    cursor += filePayloadWriting.Trailer.Length;
                    index++;
                    data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IFileMessage)filePayloadWriting).DataValue));
                    {
                        var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                        if (fileMessageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                        {
                            fileMessageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                            ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                        }
                    }
                    filePayloadWrittenCase = FilePayloadNoCase;
                }
                if (filePayloadWrittenCase != FilePayloadNothingWritten)
                {
                    int filePayloadCase;
                    ReadOnlySpan<byte> messageTypeValue;
                    filePayloadCase = FilePayloadNoCase;
                    {
                        if (length < messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader + messageTypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                        messageTypeValue = data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth);
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
                    if (filePayloadCase != filePayloadWrittenCase)
                    {
                        failure = new EncodeFailure(MessageTypeField, EncodeStatus.RuleMismatch, index);
                        return false;
                    }
                }
            }
            {
                ushort messageLengthValue;
                const long FilePayloadSizeRule0Data2 = 1;
                global::System.Int128 measure = filePayloadOccurrenceBytes;
                bool agrees;
                try
                {
                    messageLengthValue = checked((ushort)(messageLengthStampedValue));
                    agrees = checked((checked((global::System.Int128)(messageLengthValue)) - ((global::System.Int128)(FilePayloadSizeRule0Data2)))) == measure;
                }
                catch (global::System.OverflowException)
                {
                    agrees = false;
                }
                if (!agrees)
                {
                    failure = new EncodeFailure(MessageLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(messageHeaderWriterStart + messageLengthDependencyDistanceFromMessageHeader, messageLengthDependencyReadWidth), unchecked((ushort)(messageLengthStampedValue)));
            {
                var messageLengthStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(messageHeaderWriterStart + messageLengthDependencyDistanceFromMessageHeader, messageLengthDependencyReadWidth));
                if (fileMessageWriterFrame.MessageHeader.messageLength != messageLengthStamped)
                {
                    fileMessageWriterFrame.MessageHeader.messageLength = messageLengthStamped;
                    ((IFieldOwner)fileMessageWriterFrame.MessageHeader).Touch();
                }
            }
            {
                ushort messageLengthValue;
                const long FileMessageSizeRule2Data2 = 2;
                global::System.Int128 measure = fileMessageOccurrenceBytes;
                bool agrees;
                try
                {
                    messageLengthValue = checked((ushort)(messageLengthStampedValue));
                    agrees = checked((checked((global::System.Int128)(messageLengthValue)) + ((global::System.Int128)(FileMessageSizeRule2Data2)))) == measure;
                }
                catch (global::System.OverflowException)
                {
                    agrees = false;
                }
                if (!agrees)
                {
                    failure = new EncodeFailure(MessageLengthField, EncodeStatus.RuleMismatch, index);
                    return false;
                }
            }
            fileMessageIndex++;
        }
        {
            global::System.Int128 measure = fileMessageWriterPosition + fileMessageMeasuredBytes;
            bool agrees;
            agrees = checked((global::System.Int128)(length)) == measure;
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
        }
        failure = default;
        return true;
    }

    /// <summary>
    ///  Checks every field of the header and of each message against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        foreach (var (index, message) in Messages.Index())
        {
            foreach (var failure in message.Validate())
            {
                failures.Add(failure.InMessage(index));
            }
        }
        foreach (var entry in this.FileMessageFrames)
        {
            failures.AddRange(entry.Validate());
        }
        return failures;
    }

    /// <summary>
    ///  Returns the packet formatted on multiple lines.
    /// </summary>
    /// <remarks>
    ///  The text holds each kind and message of the packet, and a line for each frame entry.
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet");
        builder.Append("  FileMessageFrames: ").Append(this.FileMessageFrames.Count).AppendLine();
        foreach (var entry in this.FileMessageFrames)
        {
            builder.Append("  ").AppendLine(entry.ToString());
        }
        return builder.ToString();
    }
}
