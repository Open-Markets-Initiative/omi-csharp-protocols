using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  A statistics message; the record of the statistics schema
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class StatMsg : IFileMessage, IGatewayMessage, IEquatable<StatMsg>, IFieldOwner
{
    internal const int TsRecvOffset = 0;

    internal const int TsRecvLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsRecv"/>.
    /// </summary>
    public const long TsRecvNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Ts Recv that means no value.
    /// </summary>
    public const ulong TsRecvNullValue = 18446744073709551615UL;

    internal const int TsRefOffset = TsRecvOffset + TsRecvLength;

    internal const int TsRefLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="TsRef"/>.
    /// </summary>
    public const long TsRefNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Ts Ref that means no value.
    /// </summary>
    public const ulong TsRefNullValue = 18446744073709551615UL;

    internal const int PriceOffset = TsRefOffset + TsRefLength;

    internal const int PriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Price.
    /// </summary>
    public const byte PriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="Price"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Price that means no value.
    /// </summary>
    public const long PriceNullValue = 9223372036854775807L;

    internal const int QuantityOffset = PriceOffset + PriceLength;

    internal const int QuantityLength = 4;

    /// <summary>
    ///  The encoded value of Quantity that means no value.
    /// </summary>
    public const int QuantityNullValue = 2147483647;

    internal const int SequenceOffset = QuantityOffset + QuantityLength;

    internal const int SequenceLength = 4;

    internal const int TsInDeltaOffset = SequenceOffset + SequenceLength;

    internal const int TsInDeltaLength = 4;

    internal const int StatTypeOffset = TsInDeltaOffset + TsInDeltaLength;

    internal const int StatTypeLength = 2;

    internal const int ChannelOffset = StatTypeOffset + StatTypeLength;

    internal const int ChannelLength = 2;

    internal const int UpdateActionOffset = ChannelOffset + ChannelLength;

    internal const int UpdateActionLength = 1;

    internal const int StatFlagsOffset = UpdateActionOffset + UpdateActionLength;

    internal const int StatFlagsLength = 1;

    internal const int Reserved6Offset = StatFlagsOffset + StatFlagsLength;

    internal const int Reserved6Length = 6;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TsRecvLength + TsRefLength + PriceLength + QuantityLength + SequenceLength + TsInDeltaLength + StatTypeLength + ChannelLength + UpdateActionLength + StatFlagsLength + Reserved6Length;

    internal ulong tsRecv;

    /// <summary>
    ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TsRecvNullValue"/>. Setting <see langword="null"/> writes <see cref="TsRecvNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="TsRecvRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TsRecvNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? TsRecv
    {
        get => this.tsRecv == TsRecvNullValue ? (DateTime?)null : Instant.ToDateTime(this.tsRecv, TsRecvNanosecondsPerUnit);
        set
        {
            this.tsRecv = value is { } present ? Instant.ToRaw<ulong>(present, TsRecvNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, TsRecvNullValue) : TsRecvNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TsRecv"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TsRecvRaw
    {
        get => this.tsRecv;
        set
        {
            this.tsRecv = value;
            Touch();
        }
    }

    internal ulong tsRef;

    /// <summary>
    ///  The reference timestamp of the statistic value expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TsRefNullValue"/>. Setting <see langword="null"/> writes <see cref="TsRefNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="TsRefRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TsRefNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? TsRef
    {
        get => this.tsRef == TsRefNullValue ? (DateTime?)null : Instant.ToDateTime(this.tsRef, TsRefNanosecondsPerUnit);
        set
        {
            this.tsRef = value is { } present ? Instant.ToRaw<ulong>(present, TsRefNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, TsRefNullValue) : TsRefNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TsRef"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong TsRefRaw
    {
        get => this.tsRef;
        set
        {
            this.tsRef = value;
            Touch();
        }
    }

    internal long price;

    /// <summary>
    ///  The price where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="PriceNullValue"/>. Setting <see langword="null"/> writes <see cref="PriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="PriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? Price
    {
        get => this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale);
        set
        {
            this.price = value is { } present ? FixedPoint.ToRaw<long>(present, PriceFactor, long.MinValue, long.MaxValue, PriceNullValue) : PriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Price"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long PriceRaw
    {
        get => this.price;
        set
        {
            this.price = value;
            Touch();
        }
    }

    internal int quantity;

    /// <summary>
    ///  The value for non-price statistics
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="QuantityNullValue"/>. Setting <see langword="null"/> writes <see cref="QuantityNullValue"/>, and a value equal to <see cref="QuantityNullValue"/> also reads back as <see langword="null"/>.
    /// </remarks>
    public int? Quantity
    {
        get => this.quantity == QuantityNullValue ? (int?)null : this.quantity;
        set
        {
            this.quantity = value ?? QuantityNullValue;
            Touch();
        }
    }

    internal uint sequence;

    /// <summary>
    ///  The message sequence number assigned at the venue
    /// </summary>
    public uint Sequence
    {
        get => this.sequence;
        set
        {
            this.sequence = value;
            Touch();
        }
    }

    internal int tsInDelta;

    /// <summary>
    ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
    /// </summary>
    public int TsInDelta
    {
        get => this.tsInDelta;
        set
        {
            this.tsInDelta = value;
            Touch();
        }
    }

    internal StatType statType;

    /// <summary>
    ///  The type of statistic value contained in the message
    /// </summary>
    public StatType StatType
    {
        get => this.statType;
        set
        {
            this.statType = value;
            Touch();
        }
    }

    internal ushort channel;

    /// <summary>
    ///  The channel ID assigned by Databento as an incrementing integer starting at zero
    /// </summary>
    public ushort Channel
    {
        get => this.channel;
        set
        {
            this.channel = value;
            Touch();
        }
    }

    internal UpdateAction updateAction;

    /// <summary>
    ///  Indicates if the statistic is newly added or deleted
    /// </summary>
    public UpdateAction UpdateAction
    {
        get => this.updateAction;
        set
        {
            this.updateAction = value;
            Touch();
        }
    }

    internal byte statFlags;

    /// <summary>
    ///  Additional flags associated with certain stat types
    /// </summary>
    public byte StatFlags
    {
        get => this.statFlags;
        set
        {
            this.statFlags = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue => RecordBodyCode.StatMsg;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.statType) && global::System.Enum.IsDefined(this.updateAction);

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

    IMessage IMessage.Copy() => new StatMsg(this);

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
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        TsRecv,
        /// <summary>
        ///  The reference timestamp of the statistic value expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        TsRef,
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        Price,
        /// <summary>
        ///  The value for non-price statistics
        /// </summary>
        Quantity,
        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        Sequence,
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        TsInDelta,
        /// <summary>
        ///  The type of statistic value contained in the message
        /// </summary>
        StatType,
        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        Channel,
        /// <summary>
        ///  Indicates if the statistic is newly added or deleted
        /// </summary>
        UpdateAction,
        /// <summary>
        ///  Additional flags associated with certain stat types
        /// </summary>
        StatFlags,
        /// <summary>
        ///  Reserved, 6 bytes
        /// </summary>
        Reserved6,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly StatMsg owner;

        internal FieldSet(StatMsg owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView TsRecv => this[FieldName.TsRecv];

        /// <summary>
        ///  The reference timestamp of the statistic value expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView TsRef => this[FieldName.TsRef];

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView Price => this[FieldName.Price];

        /// <summary>
        ///  The value for non-price statistics
        /// </summary>
        public FieldView Quantity => this[FieldName.Quantity];

        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public FieldView Sequence => this[FieldName.Sequence];

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public FieldView TsInDelta => this[FieldName.TsInDelta];

        /// <summary>
        ///  The type of statistic value contained in the message
        /// </summary>
        public FieldView StatType => this[FieldName.StatType];

        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public FieldView Channel => this[FieldName.Channel];

        /// <summary>
        ///  Indicates if the statistic is newly added or deleted
        /// </summary>
        public FieldView UpdateAction => this[FieldName.UpdateAction];

        /// <summary>
        ///  Additional flags associated with certain stat types
        /// </summary>
        public FieldView StatFlags => this[FieldName.StatFlags];

        /// <summary>
        ///  Reserved, 6 bytes
        /// </summary>
        public FieldView Reserved6 => this[FieldName.Reserved6];

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
        new(nameof(FieldName.DataValue), FieldKind.Enum, DataValueLength) { IsSigned = false },
        new(nameof(FieldName.TsRecv), FieldKind.DateTime, TsRecvLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = TsRecvNullValue },
        new(nameof(FieldName.TsRef), FieldKind.DateTime, TsRefLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = TsRefNullValue },
        new(nameof(FieldName.Price), FieldKind.Decimal, PriceLength) { Scale = PriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = PriceNullValue },
        new(nameof(FieldName.Quantity), FieldKind.SignedInteger, QuantityLength) { ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = QuantityNullValue },
        new(nameof(FieldName.Sequence), FieldKind.UnsignedInteger, SequenceLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.TsInDelta), FieldKind.SignedInteger, TsInDeltaLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.StatType), FieldKind.Enum, StatTypeLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Channel), FieldKind.UnsignedInteger, ChannelLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.UpdateAction), FieldKind.Enum, UpdateActionLength) { IsSigned = false },
        new(nameof(FieldName.StatFlags), FieldKind.UnsignedInteger, StatFlagsLength) { IsSigned = false },
        new(nameof(FieldName.Reserved6), FieldKind.Padding, Reserved6Length),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="StatMsg"/> class with default values.
    /// </summary>
    public StatMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="StatMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public StatMsg(StatMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(StatMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.tsRecv = other.tsRecv;
        this.tsRef = other.tsRef;
        this.price = other.price;
        this.quantity = other.quantity;
        this.sequence = other.sequence;
        this.tsInDelta = other.tsInDelta;
        this.statType = other.statType;
        this.channel = other.channel;
        this.updateAction = other.updateAction;
        this.statFlags = other.statFlags;
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
        this.tsRecv = default;
        this.tsRef = default;
        this.price = default;
        this.quantity = default;
        this.sequence = default;
        this.tsInDelta = default;
        this.statType = default;
        this.channel = default;
        this.updateAction = default;
        this.statFlags = default;
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
    public static StatMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new StatMsg();
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
    public static StatMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new StatMsg();
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
        if (Truncated(data, start, cursor, TsRecvLength, FieldName.TsRecv)) return;
        this.tsRecv = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, TsRecvLength));
        cursor += TsRecvLength;
        if (Truncated(data, start, cursor, TsRefLength, FieldName.TsRef)) return;
        this.tsRef = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, TsRefLength));
        cursor += TsRefLength;
        if (Truncated(data, start, cursor, PriceLength, FieldName.Price)) return;
        this.price = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, PriceLength));
        cursor += PriceLength;
        if (Truncated(data, start, cursor, QuantityLength, FieldName.Quantity)) return;
        this.quantity = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, QuantityLength));
        cursor += QuantityLength;
        if (Truncated(data, start, cursor, SequenceLength, FieldName.Sequence)) return;
        this.sequence = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, SequenceLength));
        cursor += SequenceLength;
        if (Truncated(data, start, cursor, TsInDeltaLength, FieldName.TsInDelta)) return;
        this.tsInDelta = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, TsInDeltaLength));
        cursor += TsInDeltaLength;
        if (Truncated(data, start, cursor, StatTypeLength, FieldName.StatType)) return;
        this.statType = (StatType)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, StatTypeLength));
        cursor += StatTypeLength;
        if (Truncated(data, start, cursor, ChannelLength, FieldName.Channel)) return;
        this.channel = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, ChannelLength));
        cursor += ChannelLength;
        if (Truncated(data, start, cursor, UpdateActionLength, FieldName.UpdateAction)) return;
        this.updateAction = (UpdateAction)data.Slice(cursor, UpdateActionLength)[0];
        cursor += UpdateActionLength;
        if (Truncated(data, start, cursor, StatFlagsLength, FieldName.StatFlags)) return;
        this.statFlags = data.Slice(cursor, StatFlagsLength)[0];
        cursor += StatFlagsLength;
        if (Truncated(data, start, cursor, Reserved6Length, FieldName.Reserved6)) return;
        cursor += Reserved6Length;
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
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    /// <remarks>
    ///  The bytes are those of the fields of the message, from its first field to its last.
    /// </remarks>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
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
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsRecvLength), this.tsRecv);
        cursor += TsRecvLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsRefLength), this.tsRef);
        cursor += TsRefLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, PriceLength), this.price);
        cursor += PriceLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, QuantityLength), this.quantity);
        cursor += QuantityLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, SequenceLength), this.sequence);
        cursor += SequenceLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, TsInDeltaLength), this.tsInDelta);
        cursor += TsInDeltaLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, StatTypeLength), unchecked((ushort)(this.statType)));
        cursor += StatTypeLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, ChannelLength), this.channel);
        cursor += ChannelLength;
        data.Slice(cursor, UpdateActionLength)[0] = unchecked((byte)(this.updateAction));
        cursor += UpdateActionLength;
        data.Slice(cursor, StatFlagsLength)[0] = unchecked((byte)(this.statFlags));
        cursor += StatFlagsLength;
        {
            if (this.raw.Length >= Reserved6Offset + Reserved6Length)
            {
                this.raw.AsSpan(Reserved6Offset, Reserved6Length).CopyTo(data.Slice(cursor, Reserved6Length));
            }
            else
            {
                data.Slice(cursor, Reserved6Length).Clear();
            }
            cursor += Reserved6Length;
        }
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
        if (!global::System.Enum.IsDefined(this.statType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StatType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.updateAction))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.UpdateAction), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(StatMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.tsRecv != other.tsRecv) return false;
        if (this.tsRef != other.tsRef) return false;
        if (this.price != other.price) return false;
        if (this.quantity != other.quantity) return false;
        if (this.sequence != other.sequence) return false;
        if (this.tsInDelta != other.tsInDelta) return false;
        if (this.statType != other.statType) return false;
        if (this.channel != other.channel) return false;
        if (this.updateAction != other.updateAction) return false;
        if (this.statFlags != other.statFlags) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is StatMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.tsRecv);
        hash.Add(this.tsRef);
        hash.Add(this.price);
        hash.Add(this.quantity);
        hash.Add(this.sequence);
        hash.Add(this.tsInDelta);
        hash.Add(this.statType);
        hash.Add(this.channel);
        hash.Add(this.updateAction);
        hash.Add(this.statFlags);
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
        private const int FieldCount = 12;

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
            if (Has(FieldName.TsRecv)) names.Add(nameof(FieldName.TsRecv));
            if (Has(FieldName.TsRef)) names.Add(nameof(FieldName.TsRef));
            if (Has(FieldName.Price)) names.Add(nameof(FieldName.Price));
            if (Has(FieldName.Quantity)) names.Add(nameof(FieldName.Quantity));
            if (Has(FieldName.Sequence)) names.Add(nameof(FieldName.Sequence));
            if (Has(FieldName.TsInDelta)) names.Add(nameof(FieldName.TsInDelta));
            if (Has(FieldName.StatType)) names.Add(nameof(FieldName.StatType));
            if (Has(FieldName.Channel)) names.Add(nameof(FieldName.Channel));
            if (Has(FieldName.UpdateAction)) names.Add(nameof(FieldName.UpdateAction));
            if (Has(FieldName.StatFlags)) names.Add(nameof(FieldName.StatFlags));
            if (Has(FieldName.Reserved6)) names.Add(nameof(FieldName.Reserved6));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(StatMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.tsRecv != other.tsRecv) diff = diff.With(FieldName.TsRecv);
        if (this.tsRef != other.tsRef) diff = diff.With(FieldName.TsRef);
        if (this.price != other.price) diff = diff.With(FieldName.Price);
        if (this.quantity != other.quantity) diff = diff.With(FieldName.Quantity);
        if (this.sequence != other.sequence) diff = diff.With(FieldName.Sequence);
        if (this.tsInDelta != other.tsInDelta) diff = diff.With(FieldName.TsInDelta);
        if (this.statType != other.statType) diff = diff.With(FieldName.StatType);
        if (this.channel != other.channel) diff = diff.With(FieldName.Channel);
        if (this.updateAction != other.updateAction) diff = diff.With(FieldName.UpdateAction);
        if (this.statFlags != other.statFlags) diff = diff.With(FieldName.StatFlags);
        return diff;
    }

    private void CopyField(FieldName field, StatMsg from)
    {
        switch (field)
        {
            case FieldName.TsRecv:
                this.tsRecv = from.tsRecv;
                break;
            case FieldName.TsRef:
                this.tsRef = from.tsRef;
                break;
            case FieldName.Price:
                this.price = from.price;
                break;
            case FieldName.Quantity:
                this.quantity = from.quantity;
                break;
            case FieldName.Sequence:
                this.sequence = from.sequence;
                break;
            case FieldName.TsInDelta:
                this.tsInDelta = from.tsInDelta;
                break;
            case FieldName.StatType:
                this.statType = from.statType;
                break;
            case FieldName.Channel:
                this.channel = from.channel;
                break;
            case FieldName.UpdateAction:
                this.updateAction = from.updateAction;
                break;
            case FieldName.StatFlags:
                this.statFlags = from.statFlags;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"StatMsg {{ TsRecv: {(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}, TsRef: {(Text.Instant(this.tsRef, TsRefNanosecondsPerUnit, TsRefNullValue))}, Price: {(Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)))}, Quantity: {(Text.Of(this.quantity == QuantityNullValue ? (int?)null : this.quantity))}, Sequence: {(Text.Of(this.sequence))}, TsInDelta: {(Text.Of(this.tsInDelta))}, StatType: {(Text.Enum(this.statType))}, Channel: {(Text.Of(this.channel))}, UpdateAction: {(Text.Enum(this.updateAction))}, StatFlags: {(Text.Of(this.statFlags))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}] [{(Text.Instant(this.tsRef, TsRefNanosecondsPerUnit, TsRefNullValue))}] [{(Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(RecordBodyCode.StatMsg),
        FieldName.TsRecv => Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue),
        FieldName.TsRef => Text.Instant(this.tsRef, TsRefNanosecondsPerUnit, TsRefNullValue),
        FieldName.Price => Text.Of(this.price == PriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.price, PriceScale)),
        FieldName.Quantity => Text.Of(this.quantity == QuantityNullValue ? (int?)null : this.quantity),
        FieldName.Sequence => Text.Of(this.sequence),
        FieldName.TsInDelta => Text.Of(this.tsInDelta),
        FieldName.StatType => Text.Enum(this.statType),
        FieldName.Channel => Text.Of(this.channel),
        FieldName.UpdateAction => Text.Enum(this.updateAction),
        FieldName.StatFlags => Text.Of(this.statFlags),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.StatType => global::System.Enum.IsDefined(this.statType),
        FieldName.UpdateAction => global::System.Enum.IsDefined(this.updateAction),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.TsRecv => this.tsRecv != TsRecvNullValue,
        FieldName.TsRef => this.tsRef != TsRefNullValue,
        FieldName.Price => this.price != PriceNullValue,
        FieldName.Quantity => this.quantity != QuantityNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.TsRecv => TsRecvOffset,
        FieldName.TsRef => TsRefOffset,
        FieldName.Price => PriceOffset,
        FieldName.Quantity => QuantityOffset,
        FieldName.Sequence => SequenceOffset,
        FieldName.TsInDelta => TsInDeltaOffset,
        FieldName.StatType => StatTypeOffset,
        FieldName.Channel => ChannelOffset,
        FieldName.UpdateAction => UpdateActionOffset,
        FieldName.StatFlags => StatFlagsOffset,
        FieldName.Reserved6 => Reserved6Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.TsRecv => TsRecvLength,
        FieldName.TsRef => TsRefLength,
        FieldName.Price => PriceLength,
        FieldName.Quantity => QuantityLength,
        FieldName.Sequence => SequenceLength,
        FieldName.TsInDelta => TsInDeltaLength,
        FieldName.StatType => StatTypeLength,
        FieldName.Channel => ChannelLength,
        FieldName.UpdateAction => UpdateActionLength,
        FieldName.StatFlags => StatFlagsLength,
        FieldName.Reserved6 => Reserved6Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is StatMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as StatMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(RecordBodyCode.StatMsg));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
