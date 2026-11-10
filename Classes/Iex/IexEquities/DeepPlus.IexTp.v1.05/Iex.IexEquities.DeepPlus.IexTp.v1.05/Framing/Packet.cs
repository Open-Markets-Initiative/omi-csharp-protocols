using System.Text;
using System.Buffers.Binary;
using static Iex.IexEquities.DeepPlus.IexTp.v1_05.Rules;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  IexTp Udp Packet
/// </summary>
public sealed class Packet : IPacket
{
    private IextpHeader header = null!;

    /// <summary>
    ///  Gets the header of the packet, whose fields the caller can set.
    /// </summary>
    public IextpHeader Header => this.header;

    IHeader? IPacket.Header => Header;

    /// <summary>
    ///  Gets or sets the kind of the Messages, which is the case that the packet carries.
    /// </summary>
    public MessagesKind MessagesKind { get; set; } = MessagesKind.Message;

    /// <summary>
    ///  Gets the Message entries, in wire order.
    /// </summary>
    public List<MessageFrame> MessageFrames { get; } = [];

    /// <summary>
    ///  Adds a Message entry that carries the message.
    /// </summary>
    /// <param name="message">The message that the entry carries.</param>
    /// <returns>The new entry, whose header fields the caller can set.</returns>
    /// <exception cref="ArgumentNullException">The value is <see langword="null"/>.</exception>
    public MessageFrame Add(IPacketMessage message)
    {
        ArgumentNullException.ThrowIfNull(message);
        var entry = new MessageFrame { MessageData = message };
        MessagesKind = MessagesKind.Message;
        MessageFrames.Add(entry);
        return entry;
    }

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public PacketFramedMessages Messages { get; }

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

    /// <summary>
    ///  Gets the number of messages that the packet declares.
    /// </summary>
    /// <remarks>
    ///  The value is 0 for a packet that was not parsed or that ended before its message count. It is also 0 when the count field holds a value that marks a packet with no messages.
    /// </remarks>
    public ulong MessageCount { get; private set; }

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets the bytes in the packet message area after the last message, or in place of messages.
    /// </summary>
    public ReadOnlyMemory<byte> Trailer
    {
        get => this.trailer;
        set => this.trailer = value.ToArray();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Packet"/> class with default values and no messages.
    /// </summary>
    public Packet()
    {
        Messages = new PacketFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(Packet other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.header = new IextpHeader(other.header);
        this.MessagesKind = other.MessagesKind;
        this.MessageFrames.Clear();
        this.MessageFrames.AddRange(other.MessageFrames.Select(static entry => entry.Copy()));
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
        this.MessageCount = other.MessageCount;
        this.trailer = other.trailer;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.header = new IextpHeader();
        this.MessagesKind = MessagesKind.Message;
        this.MessageFrames.Clear();
        Messages.Keep(null);
        this.IsParsed = true;
        this.MessageCount = default;
        this.trailer = [];
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static Packet Parse(ReadOnlySpan<byte> data)
    {
        var packet = new Packet();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var messages = new PacketFramedMessages(data, this);
        var header = IextpHeader.Parse(data[messages.HeaderStart..]);
        this.header = header;
        this.IsParsed = header.Status == ParseStatus.Ok && messages.Status == ParseStatus.Ok && messages.IsComplete;
        this.Trailer = messages.Trailer;
        this.MessageCount = messages.MessageCount;
        Messages.Keep(messages);
    }

    internal IEnumerable<IPacketMessage> Collect()
    {
        if (this.MessagesKind == MessagesKind.Message)
        {
            foreach (var entry in this.MessageFrames)
            {
                foreach (var message in entry.Collect())
                {
                    yield return message;
                }
            }
        }
        yield break;
    }

    private const ushort HeartbeatMessageCount = 0;
    private const int PayloadLengthLength = 2;
    private static readonly FieldDescriptor PayloadLengthField = new("PayloadLength", FieldKind.UnsignedInteger, PayloadLengthLength) { ByteOrder = "Little", IsSigned = false };
    private const int MessageCountLength = 2;
    private static readonly FieldDescriptor MessageCountField = new("MessageCount", FieldKind.UnsignedInteger, MessageCountLength) { ByteOrder = "Little", IsSigned = false };
    private const int MessageLengthLength = 2;
    private static readonly FieldDescriptor MessageLengthField = new("MessageLength", FieldKind.UnsignedInteger, MessageLengthLength) { ByteOrder = "Little", IsSigned = false };
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
            length += IextpHeader.HeaderByteLength;
            if (this.MessagesKind == MessagesKind.Heartbeat)
            {
            }
            else
            {
                var messageMeasuredEntries = this.MessageFrames;
                var messageMeasuredFirst = 0;
                while (messageMeasuredFirst < messageMeasuredEntries.Count)
                {
                    var messageMeasuredFrame = messageMeasuredEntries[messageMeasuredFirst];
                    length += MessageHeader.MessageByteLength;
                    if (messageMeasuredFrame.MessageData is not null)
                    {
                        length += ((IMessage)messageMeasuredFrame.MessageData!).EncodedByteLength + ((IMessage)messageMeasuredFrame.MessageData!).Trailer.Length;
                    }
                    messageMeasuredFirst++;
                }
            }
            length += Trailer.Length;
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
    ///  The field is in the header or in a message, or is the message count or a length. For a field in a group entry or nested message, the failure also names the group or field holding it. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
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
        const int messageCountDependencyDistanceFromIextpHeader = 14;
        const int messageCountDependencyReadWidth = 2;
        const int messageTypeDependencyDistanceFromMessageHeader = 2;
        const int messageTypeDependencyReadWidth = 1;
        const int messageLengthDependencyDistanceFromMessageHeader = 0;
        const int messageLengthDependencyReadWidth = 2;
        const int payloadLengthDependencyDistanceFromIextpHeader = 12;
        const int payloadLengthDependencyReadWidth = 2;
        var packetWriterStart = cursor;
        var iextpHeaderWriterStart = cursor;
        if (!((IHeader)Header).Write(data, cursor, out cursor, out failure)) return false;
        var messagesWriterStart = cursor;
        var messagesOccurrenceBytes = 0;
        {
            if (this.MessagesKind == MessagesKind.Heartbeat)
            {
            }
            else
            {
                var messageMeasuredEntries = this.MessageFrames;
                var messageMeasuredFirst = 0;
                while (messageMeasuredFirst < messageMeasuredEntries.Count)
                {
                    var messageMeasuredFrame = messageMeasuredEntries[messageMeasuredFirst];
                    messagesOccurrenceBytes += MessageHeader.MessageByteLength;
                    if (messageMeasuredFrame.MessageData is not null)
                    {
                        messagesOccurrenceBytes += ((IMessage)messageMeasuredFrame.MessageData!).EncodedByteLength + ((IMessage)messageMeasuredFrame.MessageData!).Trailer.Length;
                    }
                    messageMeasuredFirst++;
                }
            }
            messagesOccurrenceBytes += Trailer.Length;
        }
        global::System.Int128 payloadLengthStampedValue;
        {
            global::System.Int128 measure = messagesOccurrenceBytes;
            payloadLengthStampedValue = measure;
        }
        if (payloadLengthStampedValue > ushort.MaxValue)
        {
            failure = new EncodeFailure(PayloadLengthField, EncodeStatus.TooLong, null);
            return false;
        }
        if (payloadLengthStampedValue < ushort.MinValue)
        {
            failure = new EncodeFailure(PayloadLengthField, EncodeStatus.DoesNotFit, null);
            return false;
        }
        {
            const int MessagesNoCase = 0;
            const int MessagesBranchRule1Case = 1;
            const int MessagesBranchRule2Case = 2;
            const int MessagesNothingWritten = -1;
            var messagesWrittenCase = MessagesNothingWritten;
            if (this.MessagesKind == MessagesKind.Heartbeat)
            {
                global::System.Int128 messageCountStampedValue;
                {
                    const ushort MessagesBranchRule1Data2 = HeartbeatMessageCount;
                    messageCountStampedValue = ((global::System.Int128)(MessagesBranchRule1Data2));
                }
                var heartbeatWriterStart = cursor;
                BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader, messageCountDependencyReadWidth), unchecked((ushort)(messageCountStampedValue)));
                {
                    var messageCountStamped = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader, messageCountDependencyReadWidth));
                    if (Header.messageCount != messageCountStamped)
                    {
                        Header.messageCount = messageCountStamped;
                        ((IFieldOwner)Header).Touch();
                    }
                }
            }
            else
            {
                var messageWriterPosition = cursor;
                var messageMeasuredCount = 0;
                {
                    var messageMeasuredEntries = this.MessageFrames;
                    var messageMeasuredFirst = 0;
                    while (messageMeasuredFirst < messageMeasuredEntries.Count)
                    {
                        var messageMeasuredFrame = messageMeasuredEntries[messageMeasuredFirst];
                        if (messageMeasuredFrame.MessageData is not null)
                        {
                        }
                        messageMeasuredFirst++;
                        messageMeasuredCount++;
                    }
                }
                global::System.Int128 messageCountStampedValue;
                {
                    global::System.Int128 measure = messageMeasuredCount;
                    messageCountStampedValue = measure;
                }
                if (messageCountStampedValue > ushort.MaxValue)
                {
                    failure = new EncodeFailure(MessageCountField, EncodeStatus.TooLong, null);
                    return false;
                }
                if (messageCountStampedValue < ushort.MinValue)
                {
                    failure = new EncodeFailure(MessageCountField, EncodeStatus.DoesNotFit, null);
                    return false;
                }
                var messageWriterEntries = this.MessageFrames;
                var messageIndex = 0;
                while (messageIndex < messageWriterEntries.Count)
                {
                    var messageWriterFrame = messageWriterEntries[messageIndex];
                    var messageWriterStart = cursor;
                    var messageOccurrenceBytes = 0;
                    {
                        messageOccurrenceBytes += MessageHeader.MessageByteLength;
                        if (messageWriterFrame.MessageData is not null)
                        {
                            messageOccurrenceBytes += ((IMessage)messageWriterFrame.MessageData!).EncodedByteLength + ((IMessage)messageWriterFrame.MessageData!).Trailer.Length;
                        }
                    }
                    global::System.Int128 messageLengthStampedValue;
                    {
                        const long MessageSizeRule1Data2 = 2;
                        global::System.Int128 measure = messageOccurrenceBytes;
                        try
                        {
                            messageLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(MessageSizeRule1Data2))))));
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
                    var messageHeaderWriting = messageWriterFrame.MessageHeader;
                    if (!((IGroupEntry)messageHeaderWriting).Write(data, cursor, out cursor, out failure))
                    {
                        failure = failure.InMessage(index);
                        return false;
                    }
                    var messageDataWriterStart = cursor;
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
                        const int MessageDataNothingWritten = -1;
                        var messageDataWrittenCase = MessageDataNothingWritten;
                        if (messageWriterFrame.MessageData is SystemEventMessage)
                        {
                            var systemEventMessageWriterStart = cursor;
                            var systemEventMessageWriting = (SystemEventMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)systemEventMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            systemEventMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += systemEventMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)systemEventMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is SecurityDirectoryMessage)
                        {
                            var securityDirectoryMessageWriterStart = cursor;
                            var securityDirectoryMessageWriting = (SecurityDirectoryMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)securityDirectoryMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            securityDirectoryMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += securityDirectoryMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)securityDirectoryMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is TradingStatusMessage)
                        {
                            var tradingStatusMessageWriterStart = cursor;
                            var tradingStatusMessageWriting = (TradingStatusMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)tradingStatusMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            tradingStatusMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += tradingStatusMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)tradingStatusMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is RetailLiquidityIndicatorMessage)
                        {
                            var retailLiquidityIndicatorMessageWriterStart = cursor;
                            var retailLiquidityIndicatorMessageWriting = (RetailLiquidityIndicatorMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)retailLiquidityIndicatorMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            retailLiquidityIndicatorMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += retailLiquidityIndicatorMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)retailLiquidityIndicatorMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is OperationalHaltStatusMessage)
                        {
                            var operationalHaltStatusMessageWriterStart = cursor;
                            var operationalHaltStatusMessageWriting = (OperationalHaltStatusMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)operationalHaltStatusMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            operationalHaltStatusMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += operationalHaltStatusMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)operationalHaltStatusMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is ShortSalePriceTestStatusMessage)
                        {
                            var shortSalePriceTestStatusMessageWriterStart = cursor;
                            var shortSalePriceTestStatusMessageWriting = (ShortSalePriceTestStatusMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)shortSalePriceTestStatusMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            shortSalePriceTestStatusMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += shortSalePriceTestStatusMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)shortSalePriceTestStatusMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is SecurityEventMessage)
                        {
                            var securityEventMessageWriterStart = cursor;
                            var securityEventMessageWriting = (SecurityEventMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)securityEventMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            securityEventMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += securityEventMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)securityEventMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is AddOrderMessage)
                        {
                            var addOrderMessageWriterStart = cursor;
                            var addOrderMessageWriting = (AddOrderMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)addOrderMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            addOrderMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += addOrderMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)addOrderMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is OrderModifyMessage)
                        {
                            var orderModifyMessageWriterStart = cursor;
                            var orderModifyMessageWriting = (OrderModifyMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)orderModifyMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            orderModifyMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += orderModifyMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)orderModifyMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is OrderDeleteMessage)
                        {
                            var orderDeleteMessageWriterStart = cursor;
                            var orderDeleteMessageWriting = (OrderDeleteMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)orderDeleteMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            orderDeleteMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += orderDeleteMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)orderDeleteMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is OrderExecutedMessage)
                        {
                            var orderExecutedMessageWriterStart = cursor;
                            var orderExecutedMessageWriting = (OrderExecutedMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)orderExecutedMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            orderExecutedMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += orderExecutedMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)orderExecutedMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is TradeMessage)
                        {
                            var tradeMessageWriterStart = cursor;
                            var tradeMessageWriting = (TradeMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)tradeMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            tradeMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += tradeMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)tradeMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is TradeBreakMessage)
                        {
                            var tradeBreakMessageWriterStart = cursor;
                            var tradeBreakMessageWriting = (TradeBreakMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)tradeBreakMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            tradeBreakMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += tradeBreakMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)tradeBreakMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is ClearBookMessage)
                        {
                            var clearBookMessageWriterStart = cursor;
                            var clearBookMessageWriting = (ClearBookMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)clearBookMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            clearBookMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += clearBookMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)clearBookMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.MessageData is UnknownPacketMessage)
                        {
                            var messageDataWriting = (UnknownPacketMessage)messageWriterFrame.MessageData!;
                            if (!((IMessage)messageDataWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            messageDataWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += messageDataWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)messageDataWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                            messageDataWrittenCase = MessageDataNoCase;
                        }
                        if (messageDataWrittenCase != MessageDataNothingWritten)
                        {
                            int messageDataCase;
                            ReadOnlySpan<byte> messageTypeValue;
                            messageDataCase = MessageDataNoCase;
                            {
                                if (length < messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader + messageTypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                                messageTypeValue = data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth);
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
                            if (messageDataCase != messageDataWrittenCase)
                            {
                                failure = new EncodeFailure(MessageTypeField, EncodeStatus.RuleMismatch, index);
                                return false;
                            }
                        }
                    }
                    BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(messageHeaderWriterStart + messageLengthDependencyDistanceFromMessageHeader, messageLengthDependencyReadWidth), unchecked((ushort)(messageLengthStampedValue)));
                    {
                        var messageLengthStamped = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(messageHeaderWriterStart + messageLengthDependencyDistanceFromMessageHeader, messageLengthDependencyReadWidth));
                        if (messageWriterFrame.MessageHeader.messageLength != messageLengthStamped)
                        {
                            messageWriterFrame.MessageHeader.messageLength = messageLengthStamped;
                            ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                        }
                    }
                    messageIndex++;
                }
                BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader, messageCountDependencyReadWidth), unchecked((ushort)(messageCountStampedValue)));
                {
                    var messageCountStamped = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader, messageCountDependencyReadWidth));
                    if (Header.messageCount != messageCountStamped)
                    {
                        Header.messageCount = messageCountStamped;
                        ((IFieldOwner)Header).Touch();
                    }
                }
                messagesWrittenCase = MessagesBranchRule2Case;
            }
            if (messagesWrittenCase != MessagesNothingWritten)
            {
                int messagesCase;
                ushort messageCountValue;
                messagesCase = MessagesNoCase;
                {
                    if (length < iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader + messageCountDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                    messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + messageCountDependencyDistanceFromIextpHeader, messageCountDependencyReadWidth));
                    const ushort MessagesBranchRule1Data2 = 0;
                    if ((messageCountValue == MessagesBranchRule1Data2)) { messagesCase = MessagesBranchRule1Case; }
                    else { messagesCase = MessagesBranchRule2Case; }
                }
                if (messagesCase != messagesWrittenCase)
                {
                    failure = new EncodeFailure(MessageCountField, EncodeStatus.RuleMismatch, null);
                    return false;
                }
            }
        }
        Trailer.Span.CopyTo(data[cursor..]);
        cursor += Trailer.Length;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + payloadLengthDependencyDistanceFromIextpHeader, payloadLengthDependencyReadWidth), unchecked((ushort)(payloadLengthStampedValue)));
        {
            var payloadLengthStamped = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(iextpHeaderWriterStart + payloadLengthDependencyDistanceFromIextpHeader, payloadLengthDependencyReadWidth));
            if (Header.payloadLength != payloadLengthStamped)
            {
                Header.payloadLength = payloadLengthStamped;
                ((IFieldOwner)Header).Touch();
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
        failures.AddRange(((IHeader)Header).Validate());
        foreach (var (index, message) in Messages.Index())
        {
            foreach (var failure in message.Validate())
            {
                failures.Add(failure.InMessage(index));
            }
        }
        foreach (var entry in this.MessageFrames)
        {
            failures.AddRange(entry.Validate());
        }
        return failures;
    }

    /// <summary>
    ///  Returns the packet formatted on multiple lines.
    /// </summary>
    /// <remarks>
    ///  The text holds the header, each kind and message of the packet, and a line for each frame entry. It also holds the trailer length if the packet has a trailer.
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet [IextpHeader]");
        builder.Append("  ").AppendLine(Header.ToString());
        builder.Append("  MessagesKind: ").AppendLine(Text.Enum(this.MessagesKind));
        builder.Append("  MessageFrames: ").Append(this.MessageFrames.Count).AppendLine();
        foreach (var entry in this.MessageFrames)
        {
            builder.Append("  ").AppendLine(entry.ToString());
        }
        if (Trailer.Length != 0)
        {
            builder.Append("  Trailer: ").Append(Trailer.Length).AppendLine(" bytes");
        }
        return builder.ToString();
    }
}
