using System.Text;
using System.Buffers.Binary;
using static Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Rules;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Itch Mold Udp 64 Packet
/// </summary>
public sealed class Packet : IPacket
{
    private PacketHeader header = null!;

    /// <summary>
    ///  Gets the header of the packet, whose fields the caller can set.
    /// </summary>
    public PacketHeader Header => this.header;

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
        var entry = new MessageFrame { Payload = message };
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
        this.header = new PacketHeader(other.header);
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
        this.header = new PacketHeader();
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
        var header = PacketHeader.Parse(data[messages.HeaderStart..]);
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
    private const ushort EndOfSessionMessageCount = 65535;
    private const int MessageCountLength = 2;
    private static readonly FieldDescriptor MessageCountField = new("MessageCount", FieldKind.UnsignedInteger, MessageCountLength) { ByteOrder = "Big", IsSigned = false };
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
            length += PacketHeader.HeaderByteLength;
            if (this.MessagesKind == MessagesKind.Heartbeat)
            {
            }
            else if (this.MessagesKind == MessagesKind.EndOfSession)
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
                    if (messageMeasuredFrame.Payload is not null)
                    {
                        length += ((IMessage)messageMeasuredFrame.Payload!).EncodedByteLength + ((IMessage)messageMeasuredFrame.Payload!).Trailer.Length;
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
        const int messageCountDependencyDistanceFromPacketHeader = 18;
        const int messageCountDependencyReadWidth = 2;
        const int messageTypeDependencyDistanceFromMessageHeader = 2;
        const int messageTypeDependencyReadWidth = 1;
        const int messageLengthDependencyDistanceFromMessageHeader = 0;
        const int messageLengthDependencyReadWidth = 2;
        var packetWriterStart = cursor;
        var packetHeaderWriterStart = cursor;
        if (!((IHeader)Header).Write(data, cursor, out cursor, out failure)) return false;
        var messagesWriterStart = cursor;
        {
            const int MessagesNoCase = 0;
            const int MessagesBranchRule0Case = 1;
            const int MessagesBranchRule1Case = 2;
            const int MessagesBranchRule2Case = 3;
            const int MessagesNothingWritten = -1;
            var messagesWrittenCase = MessagesNothingWritten;
            if (this.MessagesKind == MessagesKind.Heartbeat)
            {
                global::System.Int128 messageCountStampedValue;
                {
                    const ushort MessagesBranchRule0Data2 = HeartbeatMessageCount;
                    messageCountStampedValue = ((global::System.Int128)(MessagesBranchRule0Data2));
                }
                var heartbeatWriterStart = cursor;
                BinaryPrimitives.WriteUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth), unchecked((ushort)(messageCountStampedValue)));
                {
                    var messageCountStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth));
                    if (Header.messageCount != messageCountStamped)
                    {
                        Header.messageCount = messageCountStamped;
                        ((IFieldOwner)Header).Touch();
                    }
                }
            }
            else if (this.MessagesKind == MessagesKind.EndOfSession)
            {
                global::System.Int128 messageCountStampedValue;
                {
                    const ushort MessagesBranchRule1Data2 = EndOfSessionMessageCount;
                    messageCountStampedValue = ((global::System.Int128)(MessagesBranchRule1Data2));
                }
                var endOfSessionWriterStart = cursor;
                BinaryPrimitives.WriteUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth), unchecked((ushort)(messageCountStampedValue)));
                {
                    var messageCountStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth));
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
                        if (messageMeasuredFrame.Payload is not null)
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
                        if (messageWriterFrame.Payload is not null)
                        {
                            messageOccurrenceBytes += ((IMessage)messageWriterFrame.Payload!).EncodedByteLength + ((IMessage)messageWriterFrame.Payload!).Trailer.Length;
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
                    var payloadWriterStart = cursor;
                    var payloadOccurrenceBytes = 0;
                    {
                        if (messageWriterFrame.Payload is not null)
                        {
                            payloadOccurrenceBytes += ((IMessage)messageWriterFrame.Payload!).EncodedByteLength + ((IMessage)messageWriterFrame.Payload!).Trailer.Length;
                        }
                    }
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
                        const int PayloadNothingWritten = -1;
                        var payloadWrittenCase = PayloadNothingWritten;
                        if (messageWriterFrame.Payload is SystemEventMessage)
                        {
                            var systemEventMessageWriterStart = cursor;
                            var systemEventMessageWriting = (SystemEventMessage)messageWriterFrame.Payload!;
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
                        else if (messageWriterFrame.Payload is StockDirectoryMessage)
                        {
                            var stockDirectoryMessageWriterStart = cursor;
                            var stockDirectoryMessageWriting = (StockDirectoryMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)stockDirectoryMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            stockDirectoryMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += stockDirectoryMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)stockDirectoryMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is StockTradingActionMessage)
                        {
                            var stockTradingActionMessageWriterStart = cursor;
                            var stockTradingActionMessageWriting = (StockTradingActionMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)stockTradingActionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            stockTradingActionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += stockTradingActionMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)stockTradingActionMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is RegShoRestrictionMessage)
                        {
                            var regShoRestrictionMessageWriterStart = cursor;
                            var regShoRestrictionMessageWriting = (RegShoRestrictionMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)regShoRestrictionMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            regShoRestrictionMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += regShoRestrictionMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)regShoRestrictionMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is MwcbDeclineLevelMessage)
                        {
                            var mwcbDeclineLevelMessageWriterStart = cursor;
                            var mwcbDeclineLevelMessageWriting = (MwcbDeclineLevelMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)mwcbDeclineLevelMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            mwcbDeclineLevelMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += mwcbDeclineLevelMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)mwcbDeclineLevelMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is MwcbBreachMessage)
                        {
                            var mwcbBreachMessageWriterStart = cursor;
                            var mwcbBreachMessageWriting = (MwcbBreachMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)mwcbBreachMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            mwcbBreachMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += mwcbBreachMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)mwcbBreachMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is OperationalHaltMessage)
                        {
                            var operationalHaltMessageWriterStart = cursor;
                            var operationalHaltMessageWriting = (OperationalHaltMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)operationalHaltMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            operationalHaltMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += operationalHaltMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)operationalHaltMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is BboQuotationMessage)
                        {
                            var bboQuotationMessageWriterStart = cursor;
                            var bboQuotationMessageWriting = (BboQuotationMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)bboQuotationMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            bboQuotationMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += bboQuotationMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)bboQuotationMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is PriceImprovementMessage)
                        {
                            var priceImprovementMessageWriterStart = cursor;
                            var priceImprovementMessageWriting = (PriceImprovementMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)priceImprovementMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            priceImprovementMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += priceImprovementMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)priceImprovementMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is IpoQuotingPeriodUpdateMessage)
                        {
                            var ipoQuotingPeriodUpdateMessageWriterStart = cursor;
                            var ipoQuotingPeriodUpdateMessageWriting = (IpoQuotingPeriodUpdateMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)ipoQuotingPeriodUpdateMessageWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            ipoQuotingPeriodUpdateMessageWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += ipoQuotingPeriodUpdateMessageWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)ipoQuotingPeriodUpdateMessageWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                        }
                        else if (messageWriterFrame.Payload is UnknownPacketMessage)
                        {
                            var payloadWriting = (UnknownPacketMessage)messageWriterFrame.Payload!;
                            if (!((IMessage)payloadWriting).Write(data, cursor, trailerFollows: true, out cursor, out failure))
                            {
                                failure = failure.InMessage(index);
                                return false;
                            }
                            payloadWriting.Trailer.Span.CopyTo(data[cursor..]);
                            cursor += payloadWriting.Trailer.Length;
                            index++;
                            data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0] = unchecked((byte)(((IPacketMessage)payloadWriting).DataValue));
                            {
                                var messageTypeStamped = (MessageType)data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth)[0];
                                if (messageWriterFrame.MessageHeader.messageType != messageTypeStamped)
                                {
                                    messageWriterFrame.MessageHeader.messageType = messageTypeStamped;
                                    ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                                }
                            }
                            payloadWrittenCase = PayloadNoCase;
                        }
                        if (payloadWrittenCase != PayloadNothingWritten)
                        {
                            int payloadCase;
                            ReadOnlySpan<byte> messageTypeValue;
                            payloadCase = PayloadNoCase;
                            {
                                if (length < messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader + messageTypeDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                                messageTypeValue = data.Slice(messageHeaderWriterStart + messageTypeDependencyDistanceFromMessageHeader, messageTypeDependencyReadWidth);
                                global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = "S"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = "R"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = "H"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = "Y"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = "V"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = "W"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = "h"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = "Q"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = "N"u8;
                                global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = "K"u8;
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
                            }
                            if (payloadCase != payloadWrittenCase)
                            {
                                failure = new EncodeFailure(MessageTypeField, EncodeStatus.RuleMismatch, index);
                                return false;
                            }
                        }
                    }
                    {
                        ushort messageLengthValue;
                        const long PayloadSizeRule0Data2 = 1;
                        global::System.Int128 measure = payloadOccurrenceBytes;
                        bool agrees;
                        try
                        {
                            messageLengthValue = checked((ushort)(messageLengthStampedValue));
                            agrees = checked((checked((global::System.Int128)(messageLengthValue)) - ((global::System.Int128)(PayloadSizeRule0Data2)))) == measure;
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
                        if (messageWriterFrame.MessageHeader.messageLength != messageLengthStamped)
                        {
                            messageWriterFrame.MessageHeader.messageLength = messageLengthStamped;
                            ((IFieldOwner)messageWriterFrame.MessageHeader).Touch();
                        }
                    }
                    messageIndex++;
                }
                BinaryPrimitives.WriteUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth), unchecked((ushort)(messageCountStampedValue)));
                {
                    var messageCountStamped = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth));
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
                    if (length < packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader + messageCountDependencyReadWidth) { failure = new EncodeFailure(EncodeStatus.RuleMismatch).InMessage(index); return false; }
                    messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(packetHeaderWriterStart + messageCountDependencyDistanceFromPacketHeader, messageCountDependencyReadWidth));
                    const ushort MessagesBranchRule0Data2 = 0;
                    const ushort MessagesBranchRule1Data2 = 65535;
                    if ((messageCountValue == MessagesBranchRule0Data2)) { messagesCase = MessagesBranchRule0Case; }
                    else if ((messageCountValue == MessagesBranchRule1Data2)) { messagesCase = MessagesBranchRule1Case; }
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
        builder.AppendLine("Packet [PacketHeader]");
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
