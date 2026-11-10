using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The system event message type is used to signal a market or data feed handler event.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class SystemEventMessage : IPacketMessage, IEquatable<SystemEventMessage>, IFieldOwner
{
    internal const int TimestampOffset = 0;

    internal const int TimestampLength = 6;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Timestamp"/>.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of <see cref="Timestamp"/> for a time of one full day.
    /// </summary>
    public const long TimestampUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / TimestampNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that <see cref="Timestamp"/> can hold.
    /// </summary>
    public const ulong TimestampWireMaximum = (1UL << 48) - 1UL;

    internal const int EventCodeOffset = TimestampOffset + TimestampLength;

    internal const int EventCodeLength = 1;

    internal const int CurrentYearOffset = EventCodeOffset + EventCodeLength;

    internal const int CurrentYearLength = 2;

    internal const int CurrentMonthOffset = CurrentYearOffset + CurrentYearLength;

    internal const int CurrentMonthLength = 1;

    internal const int CurrentDayOffset = CurrentMonthOffset + CurrentMonthLength;

    internal const int CurrentDayLength = 1;

    internal const int VersionOffset = CurrentDayOffset + CurrentDayLength;

    internal const int VersionLength = 1;

    internal const int SubVersionOffset = VersionOffset + VersionLength;

    internal const int SubVersionLength = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + EventCodeLength + CurrentYearLength + CurrentMonthLength + CurrentDayLength + VersionLength + SubVersionLength;

    internal ulong timestamp;

    /// <summary>
    ///  The time, expressed as the number of nanoseconds after midnight.
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds a time outside one day. This property has a precision of 100 nanoseconds, and <see cref="TimestampRaw"/> holds the exact value. A time is rounded to the wire unit of the field, with a midpoint away from zero. A time past the largest wire value is set to that value.
    /// </remarks>
    public TimeOnly? Timestamp
    {
        get => Midnight.ToTimeOnly(this.timestamp, TimestampNanosecondsPerUnit, TimestampUnitsPerDay);
        set
        {
            this.timestamp = value is { } present ? Midnight.ToRaw<ulong>(present, TimestampNanosecondsPerUnit, TimestampWireMaximum) : default;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Timestamp"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TimestampRaw
    {
        get => this.timestamp;
        set
        {
            this.timestamp = value;
            Touch();
        }
    }

    internal EventCode eventCode;

    /// <summary>
    ///  Refer to System Event Codes below
    /// </summary>
    public EventCode EventCode
    {
        get => this.eventCode;
        set
        {
            this.eventCode = value;
            Touch();
        }
    }

    internal ushort currentYear;

    /// <summary>
    ///  The current calendar year (example: 2016).
    /// </summary>
    public ushort CurrentYear
    {
        get => this.currentYear;
        set
        {
            this.currentYear = value;
            Touch();
        }
    }

    internal byte currentMonth;

    /// <summary>
    ///  The current calendar month, with values 1 to 12 inclusive, January=1, etc.
    /// </summary>
    public byte CurrentMonth
    {
        get => this.currentMonth;
        set
        {
            this.currentMonth = value;
            Touch();
        }
    }

    internal byte currentDay;

    /// <summary>
    ///  The current calendar day, with values 1 to 31 inclusive.
    /// </summary>
    public byte CurrentDay
    {
        get => this.currentDay;
        set
        {
            this.currentDay = value;
            Touch();
        }
    }

    internal byte version;

    /// <summary>
    ///  Version of this interface. Currently set to 1.
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

    internal byte subVersion;

    /// <summary>
    ///  Sub-version of this interface. Currently set to 0.
    /// </summary>
    public byte SubVersion
    {
        get => this.subVersion;
        set
        {
            this.subVersion = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.SystemEventMessage;

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => MessageByteLength;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.eventCode);

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

    IMessage IMessage.Copy() => new SystemEventMessage(this);

    void IMessage.SetOwner(IFieldOwner? owner) => this.owner = owner;

    int IMessage.EncodedByteLength => MessageByteLength;

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets any extra bytes that follow the message within its declared length.
    /// </summary>
    /// <remarks>
    ///  These bytes are written only when the message is encoded in a packet that declares each message length. Setting this property marks the message as modified.
    /// </remarks>
    public ReadOnlyMemory<byte> Trailer
    {
        get => this.trailer;
        set
        {
            this.trailer = value.ToArray();
            Touch();
        }
    }

    void IMessage.SetTrailer(ReadOnlySpan<byte> bytes) => this.trailer = bytes.ToArray();

    private const int DataValueLength = 1;

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  The message type.
        /// </summary>
        DataValue,
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Refer to System Event Codes below
        /// </summary>
        EventCode,
        /// <summary>
        ///  The current calendar year (example: 2016).
        /// </summary>
        CurrentYear,
        /// <summary>
        ///  The current calendar month, with values 1 to 12 inclusive, January=1, etc.
        /// </summary>
        CurrentMonth,
        /// <summary>
        ///  The current calendar day, with values 1 to 31 inclusive.
        /// </summary>
        CurrentDay,
        /// <summary>
        ///  Version of this interface. Currently set to 1.
        /// </summary>
        Version,
        /// <summary>
        ///  Sub-version of this interface. Currently set to 0.
        /// </summary>
        SubVersion,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly SystemEventMessage owner;

        internal FieldSet(SystemEventMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Refer to System Event Codes below
        /// </summary>
        public FieldView EventCode => this[FieldName.EventCode];

        /// <summary>
        ///  The current calendar year (example: 2016).
        /// </summary>
        public FieldView CurrentYear => this[FieldName.CurrentYear];

        /// <summary>
        ///  The current calendar month, with values 1 to 12 inclusive, January=1, etc.
        /// </summary>
        public FieldView CurrentMonth => this[FieldName.CurrentMonth];

        /// <summary>
        ///  The current calendar day, with values 1 to 31 inclusive.
        /// </summary>
        public FieldView CurrentDay => this[FieldName.CurrentDay];

        /// <summary>
        ///  Version of this interface. Currently set to 1.
        /// </summary>
        public FieldView Version => this[FieldName.Version];

        /// <summary>
        ///  Sub-version of this interface. Currently set to 0.
        /// </summary>
        public FieldView SubVersion => this[FieldName.SubVersion];

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
        new(nameof(FieldName.DataValue), FieldKind.Enum, DataValueLength),
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.EventCode), FieldKind.Enum, EventCodeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.CurrentYear), FieldKind.UnsignedInteger, CurrentYearLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.CurrentMonth), FieldKind.UnsignedInteger, CurrentMonthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.CurrentDay), FieldKind.UnsignedInteger, CurrentDayLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Version), FieldKind.UnsignedInteger, VersionLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.SubVersion), FieldKind.UnsignedInteger, SubVersionLength) { ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="SystemEventMessage"/> class with default values.
    /// </summary>
    public SystemEventMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="SystemEventMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public SystemEventMessage(SystemEventMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(SystemEventMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.eventCode = other.eventCode;
        this.currentYear = other.currentYear;
        this.currentMonth = other.currentMonth;
        this.currentDay = other.currentDay;
        this.version = other.version;
        this.subVersion = other.subVersion;
        this.trailer = other.trailer;
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
        this.timestamp = default;
        this.eventCode = default;
        this.currentYear = default;
        this.currentMonth = default;
        this.currentDay = default;
        this.version = default;
        this.subVersion = default;
        this.trailer = [];
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
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public static SystemEventMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new SystemEventMessage();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public static SystemEventMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new SystemEventMessage();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
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
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
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
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = Padded.ReadBigEndian<ulong>(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, EventCodeLength, FieldName.EventCode)) return;
        this.eventCode = (EventCode)data.Slice(cursor, EventCodeLength)[0];
        cursor += EventCodeLength;
        if (Truncated(data, start, cursor, CurrentYearLength, FieldName.CurrentYear)) return;
        this.currentYear = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, CurrentYearLength));
        cursor += CurrentYearLength;
        if (Truncated(data, start, cursor, CurrentMonthLength, FieldName.CurrentMonth)) return;
        this.currentMonth = data.Slice(cursor, CurrentMonthLength)[0];
        cursor += CurrentMonthLength;
        if (Truncated(data, start, cursor, CurrentDayLength, FieldName.CurrentDay)) return;
        this.currentDay = data.Slice(cursor, CurrentDayLength)[0];
        cursor += CurrentDayLength;
        if (Truncated(data, start, cursor, VersionLength, FieldName.Version)) return;
        this.version = data.Slice(cursor, VersionLength)[0];
        cursor += VersionLength;
        if (Truncated(data, start, cursor, SubVersionLength, FieldName.SubVersion)) return;
        this.subVersion = data.Slice(cursor, SubVersionLength)[0];
        cursor += SubVersionLength;
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
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The value of offset is negative or greater than the length of the span.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < MessageByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IMessage)this).Write(data, offset, trailerFollows: false, out var cursor, out var failure))
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
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public byte[] ToBytes()
    {
        var buffer = new byte[MessageByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the message into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the message was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < MessageByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IMessage)this).Write(data, 0, trailerFollows: false, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IMessage.Write(Span<byte> data, int offset, bool trailerFollows, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        if (this.timestamp > TimestampWireMaximum)
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.Timestamp), EncodeStatus.DoesNotFit);
            return false;
        }
        Padded.WriteBigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        data.Slice(cursor, EventCodeLength)[0] = unchecked((byte)(this.eventCode));
        cursor += EventCodeLength;
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, CurrentYearLength), this.currentYear);
        cursor += CurrentYearLength;
        data.Slice(cursor, CurrentMonthLength)[0] = unchecked((byte)(this.currentMonth));
        cursor += CurrentMonthLength;
        data.Slice(cursor, CurrentDayLength)[0] = unchecked((byte)(this.currentDay));
        cursor += CurrentDayLength;
        data.Slice(cursor, VersionLength)[0] = unchecked((byte)(this.version));
        cursor += VersionLength;
        data.Slice(cursor, SubVersionLength)[0] = unchecked((byte)(this.subVersion));
        cursor += SubVersionLength;
        failure = default;
        return true;
    }

    bool IFieldOwner.TryRaw(out ReadOnlyMemory<byte> raw)
    {
        if (this.status == ParseStatus.None || this.modified)
        {
            if (this.cache is null)
            {
                var buffer = new byte[MessageByteLength];
                if (!((IMessage)this).Write(buffer, 0, trailerFollows: false, out _, out _))
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
        if (!global::System.Enum.IsDefined(this.eventCode))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.EventCode), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(SystemEventMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.eventCode != other.eventCode) return false;
        if (this.currentYear != other.currentYear) return false;
        if (this.currentMonth != other.currentMonth) return false;
        if (this.currentDay != other.currentDay) return false;
        if (this.version != other.version) return false;
        if (this.subVersion != other.subVersion) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is SystemEventMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.eventCode);
        hash.Add(this.currentYear);
        hash.Add(this.currentMonth);
        hash.Add(this.currentDay);
        hash.Add(this.version);
        hash.Add(this.subVersion);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Determines whether the specified bytes equal the result of <see cref="ToBytes"/>.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public bool BytesEqual(ReadOnlySpan<byte> data) => ToBytes().AsSpan().SequenceEqual(data);

    /// <summary>
    ///  Identifies the fields that differ between two instances.
    /// </summary>
    public readonly struct Changes : IEquatable<Changes>, IEnumerable<FieldName>
    {
        internal const int WordShift = 6;
        internal const int WordBitMask = 63;
        private const int WordCount = 1;
        private const int FieldCount = 8;

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
            if (Has(FieldName.DataValue)) names.Add(nameof(FieldName.DataValue));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.EventCode)) names.Add(nameof(FieldName.EventCode));
            if (Has(FieldName.CurrentYear)) names.Add(nameof(FieldName.CurrentYear));
            if (Has(FieldName.CurrentMonth)) names.Add(nameof(FieldName.CurrentMonth));
            if (Has(FieldName.CurrentDay)) names.Add(nameof(FieldName.CurrentDay));
            if (Has(FieldName.Version)) names.Add(nameof(FieldName.Version));
            if (Has(FieldName.SubVersion)) names.Add(nameof(FieldName.SubVersion));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(SystemEventMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.eventCode != other.eventCode) diff = diff.With(FieldName.EventCode);
        if (this.currentYear != other.currentYear) diff = diff.With(FieldName.CurrentYear);
        if (this.currentMonth != other.currentMonth) diff = diff.With(FieldName.CurrentMonth);
        if (this.currentDay != other.currentDay) diff = diff.With(FieldName.CurrentDay);
        if (this.version != other.version) diff = diff.With(FieldName.Version);
        if (this.subVersion != other.subVersion) diff = diff.With(FieldName.SubVersion);
        return diff;
    }

    private void CopyField(FieldName field, SystemEventMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.EventCode:
                this.eventCode = from.eventCode;
                break;
            case FieldName.CurrentYear:
                this.currentYear = from.currentYear;
                break;
            case FieldName.CurrentMonth:
                this.currentMonth = from.currentMonth;
                break;
            case FieldName.CurrentDay:
                this.currentDay = from.currentDay;
                break;
            case FieldName.Version:
                this.version = from.version;
                break;
            case FieldName.SubVersion:
                this.subVersion = from.subVersion;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"SystemEventMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, EventCode: {(Text.Enum(this.eventCode))}, CurrentYear: {(Text.Of(this.currentYear))}, CurrentMonth: {(Text.Of(this.currentMonth))}, CurrentDay: {(Text.Of(this.currentDay))}, Version: {(Text.Of(this.version))}, SubVersion: {(Text.Of(this.subVersion))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Enum(this.eventCode))}] [{(Text.Of(this.currentYear))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.SystemEventMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.EventCode => Text.Enum(this.eventCode),
        FieldName.CurrentYear => Text.Of(this.currentYear),
        FieldName.CurrentMonth => Text.Of(this.currentMonth),
        FieldName.CurrentDay => Text.Of(this.currentDay),
        FieldName.Version => Text.Of(this.version),
        FieldName.SubVersion => Text.Of(this.subVersion),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.EventCode => global::System.Enum.IsDefined(this.eventCode),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Timestamp => TimestampOffset,
        FieldName.EventCode => EventCodeOffset,
        FieldName.CurrentYear => CurrentYearOffset,
        FieldName.CurrentMonth => CurrentMonthOffset,
        FieldName.CurrentDay => CurrentDayOffset,
        FieldName.Version => VersionOffset,
        FieldName.SubVersion => SubVersionOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.EventCode => EventCodeLength,
        FieldName.CurrentYear => CurrentYearLength,
        FieldName.CurrentMonth => CurrentMonthLength,
        FieldName.CurrentDay => CurrentDayLength,
        FieldName.Version => VersionLength,
        FieldName.SubVersion => SubVersionLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is SystemEventMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as SystemEventMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.SystemEventMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
