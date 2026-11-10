using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The ticker message is used to send real time trade information
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ComplexStrategyTickerMessage : IPacketMessage, IEquatable<ComplexStrategyTickerMessage>, IFieldOwner
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

    internal const int StrategyIdOffset = TimestampOffset + TimestampLength;

    internal const int StrategyIdLength = 4;

    internal const int LastPriceOffset = StrategyIdOffset + StrategyIdLength;

    internal const int LastPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Last Price.
    /// </summary>
    public const byte LastPriceScale = 8;

    /// <summary>
    ///  The factor by which <see cref="LastPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LastPriceFactor = 100000000m;

    internal const int SizeOffset = LastPriceOffset + LastPriceLength;

    internal const int SizeLength = 4;

    internal const int VolumeOffset = SizeOffset + SizeLength;

    internal const int VolumeLength = 4;

    internal const int HighOffset = VolumeOffset + VolumeLength;

    internal const int HighLength = 8;

    internal const int LowOffset = HighOffset + HighLength;

    internal const int LowLength = 8;

    internal const int FirstOffset = LowOffset + LowLength;

    internal const int FirstLength = 8;

    internal const int TradeConditionOffset = FirstOffset + FirstLength;

    internal const int TradeConditionLength = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + StrategyIdLength + LastPriceLength + SizeLength + VolumeLength + HighLength + LowLength + FirstLength + TradeConditionLength;

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

    internal uint strategyId;

    /// <summary>
    ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
    /// </summary>
    public uint StrategyId
    {
        get => this.strategyId;
        set
        {
            this.strategyId = value;
            Touch();
        }
    }

    internal long lastPrice;

    /// <summary>
    ///  Most recent price.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal LastPrice
    {
        get => FixedPoint.ToDecimal(this.lastPrice, LastPriceScale);
        set
        {
            this.lastPrice = FixedPoint.ToRaw<long>(value, LastPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="LastPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long LastPriceRaw
    {
        get => this.lastPrice;
        set
        {
            this.lastPrice = value;
            Touch();
        }
    }

    internal uint size;

    /// <summary>
    ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
    /// </summary>
    public uint Size
    {
        get => this.size;
        set
        {
            this.size = value;
            Touch();
        }
    }

    internal uint volume;

    /// <summary>
    ///  Total traded quantity.
    /// </summary>
    public uint Volume
    {
        get => this.volume;
        set
        {
            this.volume = value;
            Touch();
        }
    }

    internal ulong high;

    /// <summary>
    ///  High price for the day.
    /// </summary>
    public ulong High
    {
        get => this.high;
        set
        {
            this.high = value;
            Touch();
        }
    }

    internal ulong low;

    /// <summary>
    ///  Low price for the day.
    /// </summary>
    public ulong Low
    {
        get => this.low;
        set
        {
            this.low = value;
            Touch();
        }
    }

    internal ulong first;

    /// <summary>
    ///  Opening price for the day
    /// </summary>
    public ulong First
    {
        get => this.first;
        set
        {
            this.first = value;
            Touch();
        }
    }

    internal char tradeCondition;

    /// <summary>
    ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
    /// </summary>
    public char TradeCondition
    {
        get => this.tradeCondition;
        set
        {
            this.tradeCondition = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.ComplexStrategyTickerMessage;

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

    IMessage IMessage.Copy() => new ComplexStrategyTickerMessage(this);

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
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        StrategyId,
        /// <summary>
        ///  Most recent price.
        /// </summary>
        LastPrice,
        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        Size,
        /// <summary>
        ///  Total traded quantity.
        /// </summary>
        Volume,
        /// <summary>
        ///  High price for the day.
        /// </summary>
        High,
        /// <summary>
        ///  Low price for the day.
        /// </summary>
        Low,
        /// <summary>
        ///  Opening price for the day
        /// </summary>
        First,
        /// <summary>
        ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
        /// </summary>
        TradeCondition,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ComplexStrategyTickerMessage owner;

        internal FieldSet(ComplexStrategyTickerMessage owner)
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
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public FieldView StrategyId => this[FieldName.StrategyId];

        /// <summary>
        ///  Most recent price.
        /// </summary>
        public FieldView LastPrice => this[FieldName.LastPrice];

        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        public FieldView Size => this[FieldName.Size];

        /// <summary>
        ///  Total traded quantity.
        /// </summary>
        public FieldView Volume => this[FieldName.Volume];

        /// <summary>
        ///  High price for the day.
        /// </summary>
        public FieldView High => this[FieldName.High];

        /// <summary>
        ///  Low price for the day.
        /// </summary>
        public FieldView Low => this[FieldName.Low];

        /// <summary>
        ///  Opening price for the day
        /// </summary>
        public FieldView First => this[FieldName.First];

        /// <summary>
        ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
        /// </summary>
        public FieldView TradeCondition => this[FieldName.TradeCondition];

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
        new(nameof(FieldName.StrategyId), FieldKind.UnsignedInteger, StrategyIdLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.LastPrice), FieldKind.Decimal, LastPriceLength) { Scale = LastPriceScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.Size), FieldKind.UnsignedInteger, SizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Volume), FieldKind.UnsignedInteger, VolumeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.High), FieldKind.UnsignedInteger, HighLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Low), FieldKind.UnsignedInteger, LowLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.First), FieldKind.UnsignedInteger, FirstLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TradeCondition), FieldKind.Char, TradeConditionLength) { Fill = "Spaces", Justification = "Left" },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyTickerMessage"/> class with default values.
    /// </summary>
    public ComplexStrategyTickerMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyTickerMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ComplexStrategyTickerMessage(ComplexStrategyTickerMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ComplexStrategyTickerMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.strategyId = other.strategyId;
        this.lastPrice = other.lastPrice;
        this.size = other.size;
        this.volume = other.volume;
        this.high = other.high;
        this.low = other.low;
        this.first = other.first;
        this.tradeCondition = other.tradeCondition;
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
        this.strategyId = default;
        this.lastPrice = default;
        this.size = default;
        this.volume = default;
        this.high = default;
        this.low = default;
        this.first = default;
        this.tradeCondition = default;
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
    public static ComplexStrategyTickerMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ComplexStrategyTickerMessage();
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
    public static ComplexStrategyTickerMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ComplexStrategyTickerMessage();
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
        if (Truncated(data, start, cursor, StrategyIdLength, FieldName.StrategyId)) return;
        this.strategyId = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, StrategyIdLength));
        cursor += StrategyIdLength;
        if (Truncated(data, start, cursor, LastPriceLength, FieldName.LastPrice)) return;
        this.lastPrice = BinaryPrimitives.ReadInt64BigEndian(data.Slice(cursor, LastPriceLength));
        cursor += LastPriceLength;
        if (Truncated(data, start, cursor, SizeLength, FieldName.Size)) return;
        this.size = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, SizeLength));
        cursor += SizeLength;
        if (Truncated(data, start, cursor, VolumeLength, FieldName.Volume)) return;
        this.volume = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, VolumeLength));
        cursor += VolumeLength;
        if (Truncated(data, start, cursor, HighLength, FieldName.High)) return;
        this.high = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, HighLength));
        cursor += HighLength;
        if (Truncated(data, start, cursor, LowLength, FieldName.Low)) return;
        this.low = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, LowLength));
        cursor += LowLength;
        if (Truncated(data, start, cursor, FirstLength, FieldName.First)) return;
        this.first = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, FirstLength));
        cursor += FirstLength;
        if (Truncated(data, start, cursor, TradeConditionLength, FieldName.TradeCondition)) return;
        this.tradeCondition = TextEncoding.Ascii.ReadChar(data.Slice(cursor, TradeConditionLength)[0]);
        cursor += TradeConditionLength;
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
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, StrategyIdLength), this.strategyId);
        cursor += StrategyIdLength;
        BinaryPrimitives.WriteInt64BigEndian(data.Slice(cursor, LastPriceLength), this.lastPrice);
        cursor += LastPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, SizeLength), this.size);
        cursor += SizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, VolumeLength), this.volume);
        cursor += VolumeLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, HighLength), this.high);
        cursor += HighLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, LowLength), this.low);
        cursor += LowLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, FirstLength), this.first);
        cursor += FirstLength;
        if (!TextEncoding.Ascii.CanWrite(this.tradeCondition))
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.TradeCondition), EncodeStatus.DoesNotFit);
            return false;
        }
        data.Slice(cursor, TradeConditionLength)[0] = TextEncoding.Ascii.WriteChar(this.tradeCondition);
        cursor += TradeConditionLength;
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
        if (!global::System.Text.Ascii.IsValid(this.tradeCondition))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.TradeCondition), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ComplexStrategyTickerMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.strategyId != other.strategyId) return false;
        if (this.lastPrice != other.lastPrice) return false;
        if (this.size != other.size) return false;
        if (this.volume != other.volume) return false;
        if (this.high != other.high) return false;
        if (this.low != other.low) return false;
        if (this.first != other.first) return false;
        if (this.tradeCondition != other.tradeCondition) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ComplexStrategyTickerMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.strategyId);
        hash.Add(this.lastPrice);
        hash.Add(this.size);
        hash.Add(this.volume);
        hash.Add(this.high);
        hash.Add(this.low);
        hash.Add(this.first);
        hash.Add(this.tradeCondition);
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
            if (Has(FieldName.DataValue)) names.Add(nameof(FieldName.DataValue));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.StrategyId)) names.Add(nameof(FieldName.StrategyId));
            if (Has(FieldName.LastPrice)) names.Add(nameof(FieldName.LastPrice));
            if (Has(FieldName.Size)) names.Add(nameof(FieldName.Size));
            if (Has(FieldName.Volume)) names.Add(nameof(FieldName.Volume));
            if (Has(FieldName.High)) names.Add(nameof(FieldName.High));
            if (Has(FieldName.Low)) names.Add(nameof(FieldName.Low));
            if (Has(FieldName.First)) names.Add(nameof(FieldName.First));
            if (Has(FieldName.TradeCondition)) names.Add(nameof(FieldName.TradeCondition));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ComplexStrategyTickerMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.strategyId != other.strategyId) diff = diff.With(FieldName.StrategyId);
        if (this.lastPrice != other.lastPrice) diff = diff.With(FieldName.LastPrice);
        if (this.size != other.size) diff = diff.With(FieldName.Size);
        if (this.volume != other.volume) diff = diff.With(FieldName.Volume);
        if (this.high != other.high) diff = diff.With(FieldName.High);
        if (this.low != other.low) diff = diff.With(FieldName.Low);
        if (this.first != other.first) diff = diff.With(FieldName.First);
        if (this.tradeCondition != other.tradeCondition) diff = diff.With(FieldName.TradeCondition);
        return diff;
    }

    private void CopyField(FieldName field, ComplexStrategyTickerMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.StrategyId:
                this.strategyId = from.strategyId;
                break;
            case FieldName.LastPrice:
                this.lastPrice = from.lastPrice;
                break;
            case FieldName.Size:
                this.size = from.size;
                break;
            case FieldName.Volume:
                this.volume = from.volume;
                break;
            case FieldName.High:
                this.high = from.high;
                break;
            case FieldName.Low:
                this.low = from.low;
                break;
            case FieldName.First:
                this.first = from.first;
                break;
            case FieldName.TradeCondition:
                this.tradeCondition = from.tradeCondition;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ComplexStrategyTickerMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, StrategyId: {(Text.Of(this.strategyId))}, LastPrice: {(Text.Of(FixedPoint.ToDecimal(this.lastPrice, LastPriceScale)))}, Size: {(Text.Of(this.size))}, Volume: {(Text.Of(this.volume))}, High: {(Text.Of(this.high))}, Low: {(Text.Of(this.low))}, First: {(Text.Of(this.first))}, TradeCondition: {(Text.Of(this.tradeCondition))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.strategyId))}] [{(Text.Of(FixedPoint.ToDecimal(this.lastPrice, LastPriceScale)))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.ComplexStrategyTickerMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.StrategyId => Text.Of(this.strategyId),
        FieldName.LastPrice => Text.Of(FixedPoint.ToDecimal(this.lastPrice, LastPriceScale)),
        FieldName.Size => Text.Of(this.size),
        FieldName.Volume => Text.Of(this.volume),
        FieldName.High => Text.Of(this.high),
        FieldName.Low => Text.Of(this.low),
        FieldName.First => Text.Of(this.first),
        FieldName.TradeCondition => Text.Of(this.tradeCondition),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => true;

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Timestamp => TimestampOffset,
        FieldName.StrategyId => StrategyIdOffset,
        FieldName.LastPrice => LastPriceOffset,
        FieldName.Size => SizeOffset,
        FieldName.Volume => VolumeOffset,
        FieldName.High => HighOffset,
        FieldName.Low => LowOffset,
        FieldName.First => FirstOffset,
        FieldName.TradeCondition => TradeConditionOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.StrategyId => StrategyIdLength,
        FieldName.LastPrice => LastPriceLength,
        FieldName.Size => SizeLength,
        FieldName.Volume => VolumeLength,
        FieldName.High => HighLength,
        FieldName.Low => LowLength,
        FieldName.First => FirstLength,
        FieldName.TradeCondition => TradeConditionLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ComplexStrategyTickerMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ComplexStrategyTickerMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.ComplexStrategyTickerMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
