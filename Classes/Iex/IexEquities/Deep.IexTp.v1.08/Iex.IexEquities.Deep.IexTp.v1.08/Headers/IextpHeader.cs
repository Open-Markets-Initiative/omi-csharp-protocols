using System.Buffers.Binary;
using System.Diagnostics;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  IexTp packet header
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class IextpHeader : IHeader, IEquatable<IextpHeader>, IFieldOwner
{
    internal const int VersionOffset = 0;

    internal const int VersionLength = 1;

    /// <summary>
    ///  Version of the IEX-TP transport specification
    /// </summary>
    public const byte VersionStaticValue = (byte)1;

    internal const int ReservedOffset = VersionOffset + VersionLength;

    internal const int ReservedLength = 1;

    internal const int MessageProtocolIdOffset = ReservedOffset + ReservedLength;

    internal const int MessageProtocolIdLength = 2;

    /// <summary>
    ///  Iex Deep Protocol
    /// </summary>
    public const ushort MessageProtocolIdStaticValue = (ushort)32772;

    internal const int ChannelIdOffset = MessageProtocolIdOffset + MessageProtocolIdLength;

    internal const int ChannelIdLength = 4;

    /// <summary>
    ///  The one channel DEEP is published on
    /// </summary>
    public const uint ChannelIdStaticValue = 1U;

    internal const int SessionIdOffset = ChannelIdOffset + ChannelIdLength;

    internal const int SessionIdLength = 4;

    internal const int PayloadLengthOffset = SessionIdOffset + SessionIdLength;

    internal const int PayloadLengthLength = 2;

    internal const int MessageCountOffset = PayloadLengthOffset + PayloadLengthLength;

    internal const int MessageCountLength = 2;

    internal const int StreamOffsetOffset = MessageCountOffset + MessageCountLength;

    internal const int StreamOffsetLength = 8;

    internal const int FirstMessageSequenceNumberOffset = StreamOffsetOffset + StreamOffsetLength;

    internal const int FirstMessageSequenceNumberLength = 8;

    internal const int SendTimeOffset = FirstMessageSequenceNumberOffset + FirstMessageSequenceNumberLength;

    internal const int SendTimeLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="SendTime"/>.
    /// </summary>
    public const long SendTimeNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int HeaderByteLength = VersionLength + ReservedLength + MessageProtocolIdLength + ChannelIdLength + SessionIdLength + PayloadLengthLength + MessageCountLength + StreamOffsetLength + FirstMessageSequenceNumberLength + SendTimeLength;

    internal byte version;

    /// <summary>
    ///  Version of transport specification
    /// </summary>
    public byte Version
    {
        get => this.version;
        set
        {
            this.version = value;
            Touch();
        }
    }

    internal ushort messageProtocolId;

    /// <summary>
    ///  Unique identifier of the higher layer protocol
    /// </summary>
    public ushort MessageProtocolId
    {
        get => this.messageProtocolId;
        set
        {
            this.messageProtocolId = value;
            Touch();
        }
    }

    internal uint channelId;

    /// <summary>
    ///  Identifies the stream of bytes sequenced messages
    /// </summary>
    public uint ChannelId
    {
        get => this.channelId;
        set
        {
            this.channelId = value;
            Touch();
        }
    }

    internal uint sessionId;

    /// <summary>
    ///  Identifies the session
    /// </summary>
    public uint SessionId
    {
        get => this.sessionId;
        set
        {
            this.sessionId = value;
            Touch();
        }
    }

    internal ushort payloadLength;

    /// <summary>
    ///  Byte length of the payload
    /// </summary>
    public ushort PayloadLength => this.payloadLength;

    internal ushort messageCount;

    /// <summary>
    ///  Number of messages in the payload
    /// </summary>
    public ushort MessageCount => this.messageCount;

    internal ulong streamOffset;

    /// <summary>
    ///  Byte offset of the data stream
    /// </summary>
    public ulong StreamOffset
    {
        get => this.streamOffset;
        set
        {
            this.streamOffset = value;
            Touch();
        }
    }

    internal ulong firstMessageSequenceNumber;

    /// <summary>
    ///  Sequence of the first message in the segment
    /// </summary>
    public ulong FirstMessageSequenceNumber
    {
        get => this.firstMessageSequenceNumber;
        set
        {
            this.firstMessageSequenceNumber = value;
            Touch();
        }
    }

    internal ulong sendTime;

    /// <summary>
    ///  Send time of segment
    /// </summary>
    /// <remarks>
    ///  This property has a precision of 100 nanoseconds, and <see cref="SendTimeRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public DateTime SendTime
    {
        get => Instant.ToDateTime(this.sendTime, SendTimeNanosecondsPerUnit);
        set
        {
            this.sendTime = Instant.ToRaw<ulong>(value, SendTimeNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="SendTime"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong SendTimeRaw
    {
        get => this.sendTime;
        set
        {
            this.sendTime = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => HeaderByteLength;

    /// <summary>
    ///  Gets the result of the most recent parse.
    /// </summary>
    public ParseStatus Status => this.status;

    /// <summary>
    ///  Gets the field where the most recent parse stopped, or <see langword="null"/> if it did not stop at a field.
    /// </summary>
    public FieldView? FailedField => this.failed is { } entry ? new FieldView(this, (int)entry) : null;

    /// <summary>
    ///  Gets the byte offset, relative to the start of the parsed bytes, where the most recent parse stopped.
    /// </summary>
    public int FailedOffset => this.failedOffset;

    /// <summary>
    ///  Gets a value indicating whether every message type and every enumerated value is one that the protocol defines.
    /// </summary>
    public bool IsRecognized => true;

    /// <summary>
    ///  Gets the bytes that were parsed, or no bytes if the instance was not parsed.
    /// </summary>
    public ReadOnlyMemory<byte> Raw => this.raw;

    /// <summary>
    ///  Gets a value indicating whether any value has changed since the most recent parse or clear.
    /// </summary>
    public bool IsModified => this.modified;

    private ParseStatus status;
    private FieldName? failed;
    private int failedOffset;
    private byte[] raw = [];
    private byte[]? cache;
    private bool modified;
    private IFieldOwner? owner;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  Version of transport specification
        /// </summary>
        Version,
        /// <summary>
        ///  Reserved byte
        /// </summary>
        Reserved,
        /// <summary>
        ///  Unique identifier of the higher layer protocol
        /// </summary>
        MessageProtocolId,
        /// <summary>
        ///  Identifies the stream of bytes sequenced messages
        /// </summary>
        ChannelId,
        /// <summary>
        ///  Identifies the session
        /// </summary>
        SessionId,
        /// <summary>
        ///  Byte length of the payload
        /// </summary>
        PayloadLength,
        /// <summary>
        ///  Number of messages in the payload
        /// </summary>
        MessageCount,
        /// <summary>
        ///  Byte offset of the data stream
        /// </summary>
        StreamOffset,
        /// <summary>
        ///  Sequence of the first message in the segment
        /// </summary>
        FirstMessageSequenceNumber,
        /// <summary>
        ///  Send time of segment
        /// </summary>
        SendTime,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly IextpHeader owner;

        internal FieldSet(IextpHeader owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  Version of transport specification
        /// </summary>
        public FieldView Version => this[FieldName.Version];

        /// <summary>
        ///  Reserved byte
        /// </summary>
        public FieldView Reserved => this[FieldName.Reserved];

        /// <summary>
        ///  Unique identifier of the higher layer protocol
        /// </summary>
        public FieldView MessageProtocolId => this[FieldName.MessageProtocolId];

        /// <summary>
        ///  Identifies the stream of bytes sequenced messages
        /// </summary>
        public FieldView ChannelId => this[FieldName.ChannelId];

        /// <summary>
        ///  Identifies the session
        /// </summary>
        public FieldView SessionId => this[FieldName.SessionId];

        /// <summary>
        ///  Byte length of the payload
        /// </summary>
        public FieldView PayloadLength => this[FieldName.PayloadLength];

        /// <summary>
        ///  Number of messages in the payload
        /// </summary>
        public FieldView MessageCount => this[FieldName.MessageCount];

        /// <summary>
        ///  Byte offset of the data stream
        /// </summary>
        public FieldView StreamOffset => this[FieldName.StreamOffset];

        /// <summary>
        ///  Sequence of the first message in the segment
        /// </summary>
        public FieldView FirstMessageSequenceNumber => this[FieldName.FirstMessageSequenceNumber];

        /// <summary>
        ///  Send time of segment
        /// </summary>
        public FieldView SendTime => this[FieldName.SendTime];

        /// <summary>
        ///  Gets the specified field.
        /// </summary>
        public FieldView this[FieldName field] => new(this.owner, (int)field);

        /// <summary>
        ///  Returns an enumerator that iterates through the fields in wire order.
        /// </summary>
        public FieldEnumerator GetEnumerator() => new(this.owner);

        IEnumerator<FieldView> IEnumerable<FieldView>.GetEnumerator() => GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private static readonly FieldDescriptor[] Descriptors =
    [
        new(nameof(FieldName.Version), FieldKind.UnsignedInteger, VersionLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Reserved), FieldKind.Padding, ReservedLength),
        new(nameof(FieldName.MessageProtocolId), FieldKind.UnsignedInteger, MessageProtocolIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.ChannelId), FieldKind.UnsignedInteger, ChannelIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SessionId), FieldKind.UnsignedInteger, SessionIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.PayloadLength), FieldKind.UnsignedInteger, PayloadLengthLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.MessageCount), FieldKind.UnsignedInteger, MessageCountLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.StreamOffset), FieldKind.UnsignedInteger, StreamOffsetLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.FirstMessageSequenceNumber), FieldKind.UnsignedInteger, FirstMessageSequenceNumberLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SendTime), FieldKind.DateTime, SendTimeLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IHeader.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="IextpHeader"/> class with default values.
    /// </summary>
    public IextpHeader()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="IextpHeader"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public IextpHeader(IextpHeader other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(IextpHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.version = other.version;
        this.messageProtocolId = other.messageProtocolId;
        this.channelId = other.channelId;
        this.sessionId = other.sessionId;
        this.payloadLength = other.payloadLength;
        this.messageCount = other.messageCount;
        this.streamOffset = other.streamOffset;
        this.firstMessageSequenceNumber = other.firstMessageSequenceNumber;
        this.sendTime = other.sendTime;
        this.status = other.status;
        this.failed = other.failed;
        this.failedOffset = other.failedOffset;
        this.raw = other.raw;
        this.cache = other.cache;
        this.modified = other.modified;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    /// <remarks>
    ///  A field with one fixed value is reset to that value.
    /// </remarks>
    public void Clear()
    {
        this.version = VersionStaticValue;
        this.messageProtocolId = MessageProtocolIdStaticValue;
        this.channelId = ChannelIdStaticValue;
        this.sessionId = default;
        this.payloadLength = default;
        this.messageCount = default;
        this.streamOffset = default;
        this.firstMessageSequenceNumber = default;
        this.sendTime = default;
        this.status = default;
        this.failed = null;
        this.failedOffset = default;
        this.raw = [];
        this.cache = null;
        this.modified = false;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Parses a new instance from the start of the bytes.
    /// </summary>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static IextpHeader Parse(ReadOnlySpan<byte> data)
    {
        var instance = new IextpHeader();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static IextpHeader Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new IextpHeader();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        ParseFrom(data, ref offset);
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    public void ParseFrom(ReadOnlySpan<byte> data, ref int offset)
    {
        Clear();
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        if (Truncated(data, start, cursor, VersionLength, FieldName.Version)) return;
        this.version = data.Slice(cursor, VersionLength)[0];
        cursor += VersionLength;
        if (Truncated(data, start, cursor, ReservedLength, FieldName.Reserved)) return;
        cursor += ReservedLength;
        if (Truncated(data, start, cursor, MessageProtocolIdLength, FieldName.MessageProtocolId)) return;
        this.messageProtocolId = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, MessageProtocolIdLength));
        cursor += MessageProtocolIdLength;
        if (Truncated(data, start, cursor, ChannelIdLength, FieldName.ChannelId)) return;
        this.channelId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, ChannelIdLength));
        cursor += ChannelIdLength;
        if (Truncated(data, start, cursor, SessionIdLength, FieldName.SessionId)) return;
        this.sessionId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, SessionIdLength));
        cursor += SessionIdLength;
        if (Truncated(data, start, cursor, PayloadLengthLength, FieldName.PayloadLength)) return;
        this.payloadLength = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, PayloadLengthLength));
        cursor += PayloadLengthLength;
        if (Truncated(data, start, cursor, MessageCountLength, FieldName.MessageCount)) return;
        this.messageCount = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, MessageCountLength));
        cursor += MessageCountLength;
        if (Truncated(data, start, cursor, StreamOffsetLength, FieldName.StreamOffset)) return;
        this.streamOffset = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, StreamOffsetLength));
        cursor += StreamOffsetLength;
        if (Truncated(data, start, cursor, FirstMessageSequenceNumberLength, FieldName.FirstMessageSequenceNumber)) return;
        this.firstMessageSequenceNumber = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, FirstMessageSequenceNumberLength));
        cursor += FirstMessageSequenceNumberLength;
        if (Truncated(data, start, cursor, SendTimeLength, FieldName.SendTime)) return;
        this.sendTime = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, SendTimeLength));
        cursor += SendTimeLength;
        this.raw = data[start..cursor].ToArray();
        this.status = ParseStatus.Ok;
        offset = cursor;
    }

    private void Fail(ParseStatus status, FieldName field, int offset, ReadOnlySpan<byte> available)
    {
        this.status = status;
        this.failed = field;
        this.failedOffset = offset;
        this.raw = available.ToArray();
    }

    private bool Truncated(ReadOnlySpan<byte> data, int start, int cursor, int length, FieldName field)
    {
        if (data.Length - cursor >= length) return false;
        Fail(ParseStatus.Truncated, field, cursor - start, data[start..]);
        return true;
    }

    /// <summary>
    ///  Encodes this instance into the span, starting at the specified offset.
    /// </summary>
    /// <param name="data">The span that receives the bytes.</param>
    /// <param name="offset">The offset in the span of the first byte to write.</param>
    /// <returns>The offset immediately after the last byte written.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The value of offset is negative or greater than the length of the span.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < HeaderByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IHeader)this).Write(data, offset, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into the start of the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    public byte[] ToBytes()
    {
        var buffer = new byte[HeaderByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the header into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the header was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < HeaderByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IHeader)this).Write(data, 0, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IHeader.Write(Span<byte> data, int offset, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        data.Slice(cursor, VersionLength)[0] = unchecked((byte)(this.version));
        cursor += VersionLength;
        {
            if (this.raw.Length >= ReservedOffset + ReservedLength)
            {
                this.raw.AsSpan(ReservedOffset, ReservedLength).CopyTo(data.Slice(cursor, ReservedLength));
            }
            else
            {
                data.Slice(cursor, ReservedLength).Clear();
            }
            cursor += ReservedLength;
        }
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, MessageProtocolIdLength), this.messageProtocolId);
        cursor += MessageProtocolIdLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, ChannelIdLength), this.channelId);
        cursor += ChannelIdLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, SessionIdLength), this.sessionId);
        cursor += SessionIdLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, PayloadLengthLength), this.payloadLength);
        cursor += PayloadLengthLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, MessageCountLength), this.messageCount);
        cursor += MessageCountLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, StreamOffsetLength), this.streamOffset);
        cursor += StreamOffsetLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, FirstMessageSequenceNumberLength), this.firstMessageSequenceNumber);
        cursor += FirstMessageSequenceNumberLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, SendTimeLength), this.sendTime);
        cursor += SendTimeLength;
        failure = default;
        return true;
    }

    bool IFieldOwner.TryRaw(out ReadOnlyMemory<byte> raw)
    {
        if (this.status == ParseStatus.None || this.modified)
        {
            if (this.cache is null)
            {
                var buffer = new byte[HeaderByteLength];
                if (!((IHeader)this).Write(buffer, 0, out _, out _))
                {
                    raw = ReadOnlyMemory<byte>.Empty;
                    return false;
                }
                this.cache = buffer;
            }
            raw = this.cache;
            return true;
        }
        raw = this.raw;
        return true;
    }

    /// <summary>
    ///  Checks every field against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    /// <remarks>
    ///  Validate checks the current values. After a parse, check Status first, because a field that the parse did not reach holds its cleared value.
    /// </remarks>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        if (this.version is not VersionStaticValue)
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Version), ValidationStatus.NotStaticValue));
        }
        if (this.messageProtocolId is not MessageProtocolIdStaticValue)
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.MessageProtocolId), ValidationStatus.NotStaticValue));
        }
        if (this.channelId is not ChannelIdStaticValue)
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ChannelId), ValidationStatus.NotStaticValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(IextpHeader? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.version != other.version) return false;
        if (this.messageProtocolId != other.messageProtocolId) return false;
        if (this.channelId != other.channelId) return false;
        if (this.sessionId != other.sessionId) return false;
        if (this.payloadLength != other.payloadLength) return false;
        if (this.messageCount != other.messageCount) return false;
        if (this.streamOffset != other.streamOffset) return false;
        if (this.firstMessageSequenceNumber != other.firstMessageSequenceNumber) return false;
        if (this.sendTime != other.sendTime) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is IextpHeader other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.version);
        hash.Add(this.messageProtocolId);
        hash.Add(this.channelId);
        hash.Add(this.sessionId);
        hash.Add(this.payloadLength);
        hash.Add(this.messageCount);
        hash.Add(this.streamOffset);
        hash.Add(this.firstMessageSequenceNumber);
        hash.Add(this.sendTime);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Determines whether the specified bytes equal the result of <see cref="ToBytes"/>.
    /// </summary>
    public bool BytesEqual(ReadOnlySpan<byte> data) => ToBytes().AsSpan().SequenceEqual(data);

    /// <summary>
    ///  Identifies the fields that differ between two instances.
    /// </summary>
    public readonly struct Changes : IEquatable<Changes>, IEnumerable<FieldName>
    {
        internal const int WordShift = 6;
        internal const int WordBitMask = 63;
        private const int WordCount = 1;
        private const int FieldCount = 10;

        [global::System.Runtime.CompilerServices.InlineArray(WordCount)]
        private struct Words
        {
            private ulong word;
        }

        private readonly Words words;

        private Changes(Words words)
        {
            this.words = words;
        }

        /// <summary>
        ///  Gets a value with no fields marked.
        /// </summary>
        public static Changes None => default;

        /// <summary>
        ///  Gets a value indicating whether no fields are marked.
        /// </summary>
        public bool IsNone => ((ReadOnlySpan<ulong>)this.words).IndexOfAnyExcept(0UL) < 0;

        internal ulong[] ToWords() => ((ReadOnlySpan<ulong>)this.words).ToArray();

        internal Changes With(int bit)
        {
            var word = bit >> WordShift;
            if ((uint)word >= WordCount) return this;
            var words = this.words;
            words[word] |= 1UL << (bit & WordBitMask);
            return new(words);
        }

        internal Changes With(FieldName field) => With((int)field);

        internal bool Has(int bit)
        {
            var word = bit >> WordShift;
            return (uint)word < WordCount && (this.words[word] & (1UL << (bit & WordBitMask))) != 0;
        }

        /// <summary>
        ///  Determines whether the specified field is marked.
        /// </summary>
        public bool Has(FieldName field) => Has((int)field);

        /// <summary>
        ///  Determines whether another value marks the same fields.
        /// </summary>
        public bool Equals(Changes other) => ((ReadOnlySpan<ulong>)this.words).SequenceEqual((ReadOnlySpan<ulong>)other.words);

        /// <summary>
        ///  Determines whether the specified object is a value that marks the same fields.
        /// </summary>
        public override bool Equals(object? obj) => obj is Changes other && Equals(other);

        /// <summary>
        ///  Returns a hash code computed from the marked fields.
        /// </summary>
        public override int GetHashCode()
        {
            var hc = new HashCode();
            foreach (var word in this.words)
            {
                hc.Add(word);
            }
            return hc.ToHashCode();
        }

        /// <summary>
        ///  Determines whether two values mark the same fields.
        /// </summary>
        public static bool operator ==(Changes a, Changes b) => a.Equals(b);

        /// <summary>
        ///  Determines whether two values mark different fields.
        /// </summary>
        public static bool operator !=(Changes a, Changes b) => !a.Equals(b);

        /// <summary>
        ///  Returns an enumerator that iterates through the marked fields in field order.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        IEnumerator<FieldName> IEnumerable<FieldName>.GetEnumerator() => GetEnumerator();

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///  Enumerates the marked fields of a value.
        /// </summary>
        public struct Enumerator : IEnumerator<FieldName>
        {
            private const int BeforeFirst = -1;
            private readonly Changes changes;
            private int field;

            internal Enumerator(Changes changes)
            {
                this.changes = changes;
                this.field = BeforeFirst;
            }

            /// <summary>
            ///  Gets the current marked field.
            /// </summary>
            public readonly FieldName Current => (FieldName)this.field;

            readonly object global::System.Collections.IEnumerator.Current => Current;

            /// <summary>
            ///  Advances the enumerator to the next marked field.
            /// </summary>
            public bool MoveNext()
            {
                while (++this.field < FieldCount)
                {
                    if (this.changes.Has(this.field)) return true;
                }
                return false;
            }

            /// <summary>
            ///  Sets the enumerator to its initial position, before the first field.
            /// </summary>
            public void Reset() => this.field = BeforeFirst;

            /// <summary>
            ///  Releases the enumerator.
            /// </summary>
            public readonly void Dispose() { }
        }

        /// <summary>
        ///  Returns the names of the marked fields, or None if no fields are marked.
        /// </summary>
        public override string ToString()
        {
            var names = new List<string>();
            if (Has(FieldName.Version)) names.Add(nameof(FieldName.Version));
            if (Has(FieldName.Reserved)) names.Add(nameof(FieldName.Reserved));
            if (Has(FieldName.MessageProtocolId)) names.Add(nameof(FieldName.MessageProtocolId));
            if (Has(FieldName.ChannelId)) names.Add(nameof(FieldName.ChannelId));
            if (Has(FieldName.SessionId)) names.Add(nameof(FieldName.SessionId));
            if (Has(FieldName.PayloadLength)) names.Add(nameof(FieldName.PayloadLength));
            if (Has(FieldName.MessageCount)) names.Add(nameof(FieldName.MessageCount));
            if (Has(FieldName.StreamOffset)) names.Add(nameof(FieldName.StreamOffset));
            if (Has(FieldName.FirstMessageSequenceNumber)) names.Add(nameof(FieldName.FirstMessageSequenceNumber));
            if (Has(FieldName.SendTime)) names.Add(nameof(FieldName.SendTime));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(IextpHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.version != other.version) diff = diff.With(FieldName.Version);
        if (this.messageProtocolId != other.messageProtocolId) diff = diff.With(FieldName.MessageProtocolId);
        if (this.channelId != other.channelId) diff = diff.With(FieldName.ChannelId);
        if (this.sessionId != other.sessionId) diff = diff.With(FieldName.SessionId);
        if (this.payloadLength != other.payloadLength) diff = diff.With(FieldName.PayloadLength);
        if (this.messageCount != other.messageCount) diff = diff.With(FieldName.MessageCount);
        if (this.streamOffset != other.streamOffset) diff = diff.With(FieldName.StreamOffset);
        if (this.firstMessageSequenceNumber != other.firstMessageSequenceNumber) diff = diff.With(FieldName.FirstMessageSequenceNumber);
        if (this.sendTime != other.sendTime) diff = diff.With(FieldName.SendTime);
        return diff;
    }

    private void CopyField(FieldName field, IextpHeader from)
    {
        switch (field)
        {
            case FieldName.Version:
                this.version = from.version;
                break;
            case FieldName.MessageProtocolId:
                this.messageProtocolId = from.messageProtocolId;
                break;
            case FieldName.ChannelId:
                this.channelId = from.channelId;
                break;
            case FieldName.SessionId:
                this.sessionId = from.sessionId;
                break;
            case FieldName.PayloadLength:
                this.payloadLength = from.payloadLength;
                break;
            case FieldName.MessageCount:
                this.messageCount = from.messageCount;
                break;
            case FieldName.StreamOffset:
                this.streamOffset = from.streamOffset;
                break;
            case FieldName.FirstMessageSequenceNumber:
                this.firstMessageSequenceNumber = from.firstMessageSequenceNumber;
                break;
            case FieldName.SendTime:
                this.sendTime = from.sendTime;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"IextpHeader {{ Version: {(Text.Of(this.version))}, MessageProtocolId: {(Text.Of(this.messageProtocolId))}, ChannelId: {(Text.Of(this.channelId))}, SessionId: {(Text.Of(this.sessionId))}, PayloadLength: {(Text.Of(this.payloadLength))}, MessageCount: {(Text.Of(this.messageCount))}, StreamOffset: {(Text.Of(this.streamOffset))}, FirstMessageSequenceNumber: {(Text.Of(this.firstMessageSequenceNumber))}, SendTime: {(Text.Instant(this.sendTime, SendTimeNanosecondsPerUnit))} }}";

    private string DebuggerDisplay => $"[{(Text.Of(this.version))}] [{(Text.Of(this.messageProtocolId))}] [{(Text.Of(this.channelId))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.Version => Text.Of(this.version),
        FieldName.MessageProtocolId => Text.Of(this.messageProtocolId),
        FieldName.ChannelId => Text.Of(this.channelId),
        FieldName.SessionId => Text.Of(this.sessionId),
        FieldName.PayloadLength => Text.Of(this.payloadLength),
        FieldName.MessageCount => Text.Of(this.messageCount),
        FieldName.StreamOffset => Text.Of(this.streamOffset),
        FieldName.FirstMessageSequenceNumber => Text.Of(this.firstMessageSequenceNumber),
        FieldName.SendTime => Text.Instant(this.sendTime, SendTimeNanosecondsPerUnit),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.Version => VersionOffset,
        FieldName.Reserved => ReservedOffset,
        FieldName.MessageProtocolId => MessageProtocolIdOffset,
        FieldName.ChannelId => ChannelIdOffset,
        FieldName.SessionId => SessionIdOffset,
        FieldName.PayloadLength => PayloadLengthOffset,
        FieldName.MessageCount => MessageCountOffset,
        FieldName.StreamOffset => StreamOffsetOffset,
        FieldName.FirstMessageSequenceNumber => FirstMessageSequenceNumberOffset,
        FieldName.SendTime => SendTimeOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.Version => VersionLength,
        FieldName.Reserved => ReservedLength,
        FieldName.MessageProtocolId => MessageProtocolIdLength,
        FieldName.ChannelId => ChannelIdLength,
        FieldName.SessionId => SessionIdLength,
        FieldName.PayloadLength => PayloadLengthLength,
        FieldName.MessageCount => MessageCountLength,
        FieldName.StreamOffset => StreamOffsetLength,
        FieldName.FirstMessageSequenceNumber => FirstMessageSequenceNumberLength,
        FieldName.SendTime => SendTimeLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is IextpHeader typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as IextpHeader ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
