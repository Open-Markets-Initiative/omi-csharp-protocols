using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ComplexStrategyOrderOnBookMessage : IPacketMessage, IEquatable<ComplexStrategyOrderOnBookMessage>, IFieldOwner
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

    internal const int OrderTypeOffset = StrategyIdOffset + StrategyIdLength;

    internal const int OrderTypeLength = 1;

    internal const int SideOffset = OrderTypeOffset + OrderTypeLength;

    internal const int SideLength = 1;

    internal const int PriceOffset = SideOffset + SideLength;

    internal const int PriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Price.
    /// </summary>
    public const byte PriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="Price"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PriceFactor = 10000m;

    internal const int SizeOffset = PriceOffset + PriceLength;

    internal const int SizeLength = 4;

    internal const int ExecFlagOffset = SizeOffset + SizeLength;

    internal const int ExecFlagLength = 1;

    internal const int OrderCapacityOffset = ExecFlagOffset + ExecFlagLength;

    internal const int OrderCapacityLength = 1;

    internal const int ScopeOffset = OrderCapacityOffset + OrderCapacityLength;

    internal const int ScopeLength = 1;

    internal const int OwnerIdOffset = ScopeOffset + ScopeLength;

    internal const int OwnerIdLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Owner Id.
    /// </summary>
    public const byte OwnerIdFill = (byte)' ';

    internal const int GiveupOffset = OwnerIdOffset + OwnerIdLength;

    internal const int GiveupLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Giveup.
    /// </summary>
    public const byte GiveupFill = (byte)' ';

    internal const int CmtaOffset = GiveupOffset + GiveupLength;

    internal const int CmtaLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Cmta.
    /// </summary>
    public const byte CmtaFill = (byte)' ';

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + StrategyIdLength + OrderTypeLength + SideLength + PriceLength + SizeLength + ExecFlagLength + OrderCapacityLength + ScopeLength + OwnerIdLength + GiveupLength + CmtaLength;

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

    internal OrderType orderType;

    /// <summary>
    ///  Order Type
    /// </summary>
    public OrderType OrderType
    {
        get => this.orderType;
        set
        {
            this.orderType = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
    /// </summary>
    public Side Side
    {
        get => this.side;
        set
        {
            this.side = value;
            Touch();
        }
    }

    internal int price;

    /// <summary>
    ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal Price
    {
        get => FixedPoint.ToDecimal(this.price, PriceScale);
        set
        {
            this.price = FixedPoint.ToRaw<int>(value, PriceFactor, int.MinValue, int.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Price"/>, as the unscaled integer that the field holds.
    /// </summary>
    public int PriceRaw
    {
        get => this.price;
        set
        {
            this.price = value;
            Touch();
        }
    }

    internal uint size;

    /// <summary>
    ///  Size of the order
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

    internal ExecFlag execFlag;

    /// <summary>
    ///  ‘N’ = None ‘A’ = AON
    /// </summary>
    public ExecFlag ExecFlag
    {
        get => this.execFlag;
        set
        {
            this.execFlag = value;
            Touch();
        }
    }

    internal OrderCapacity orderCapacity;

    /// <summary>
    ///  Order Capacity value
    /// </summary>
    public OrderCapacity OrderCapacity
    {
        get => this.orderCapacity;
        set
        {
            this.orderCapacity = value;
            Touch();
        }
    }

    internal Scope scope;

    /// <summary>
    ///  Scope value
    /// </summary>
    public Scope Scope
    {
        get => this.scope;
        set
        {
            this.scope = value;
            Touch();
        }
    }

    internal string ownerId = string.Empty;

    /// <summary>
    ///  Spaces when not set
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string OwnerId
    {
        get => this.ownerId;
        set
        {
            this.ownerId = value ?? string.Empty;
            Touch();
        }
    }

    internal string giveup = string.Empty;

    /// <summary>
    ///  Spaces when not set
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Giveup
    {
        get => this.giveup;
        set
        {
            this.giveup = value ?? string.Empty;
            Touch();
        }
    }

    internal string cmta = string.Empty;

    /// <summary>
    ///  Spaces when not set
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Cmta
    {
        get => this.cmta;
        set
        {
            this.cmta = value ?? string.Empty;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.ComplexStrategyOrderOnBookMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.orderType) && global::System.Enum.IsDefined(this.side) && global::System.Enum.IsDefined(this.execFlag) && global::System.Enum.IsDefined(this.orderCapacity) && global::System.Enum.IsDefined(this.scope);

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

    IMessage IMessage.Copy() => new ComplexStrategyOrderOnBookMessage(this);

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
        ///  Order Type
        /// </summary>
        OrderType,
        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        Side,
        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        Price,
        /// <summary>
        ///  Size of the order
        /// </summary>
        Size,
        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        ExecFlag,
        /// <summary>
        ///  Order Capacity value
        /// </summary>
        OrderCapacity,
        /// <summary>
        ///  Scope value
        /// </summary>
        Scope,
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        OwnerId,
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        Giveup,
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        Cmta,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ComplexStrategyOrderOnBookMessage owner;

        internal FieldSet(ComplexStrategyOrderOnBookMessage owner)
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
        ///  Order Type
        /// </summary>
        public FieldView OrderType => this[FieldName.OrderType];

        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public FieldView Price => this[FieldName.Price];

        /// <summary>
        ///  Size of the order
        /// </summary>
        public FieldView Size => this[FieldName.Size];

        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public FieldView ExecFlag => this[FieldName.ExecFlag];

        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public FieldView OrderCapacity => this[FieldName.OrderCapacity];

        /// <summary>
        ///  Scope value
        /// </summary>
        public FieldView Scope => this[FieldName.Scope];

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public FieldView OwnerId => this[FieldName.OwnerId];

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public FieldView Giveup => this[FieldName.Giveup];

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public FieldView Cmta => this[FieldName.Cmta];

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
        new(nameof(FieldName.OrderType), FieldKind.Enum, OrderTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Price), FieldKind.Decimal, PriceLength) { Scale = PriceScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.Size), FieldKind.UnsignedInteger, SizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExecFlag), FieldKind.Enum, ExecFlagLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.OrderCapacity), FieldKind.Enum, OrderCapacityLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Scope), FieldKind.Enum, ScopeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.OwnerId), FieldKind.String, OwnerIdLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Giveup), FieldKind.String, GiveupLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Cmta), FieldKind.String, CmtaLength) { Fill = "Spaces", Justification = "Left" },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyOrderOnBookMessage"/> class with default values.
    /// </summary>
    public ComplexStrategyOrderOnBookMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ComplexStrategyOrderOnBookMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ComplexStrategyOrderOnBookMessage(ComplexStrategyOrderOnBookMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ComplexStrategyOrderOnBookMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.strategyId = other.strategyId;
        this.orderType = other.orderType;
        this.side = other.side;
        this.price = other.price;
        this.size = other.size;
        this.execFlag = other.execFlag;
        this.orderCapacity = other.orderCapacity;
        this.scope = other.scope;
        this.ownerId = other.ownerId;
        this.giveup = other.giveup;
        this.cmta = other.cmta;
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
        this.orderType = default;
        this.side = default;
        this.price = default;
        this.size = default;
        this.execFlag = default;
        this.orderCapacity = default;
        this.scope = default;
        this.ownerId = string.Empty;
        this.giveup = string.Empty;
        this.cmta = string.Empty;
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
    public static ComplexStrategyOrderOnBookMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ComplexStrategyOrderOnBookMessage();
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
    public static ComplexStrategyOrderOnBookMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ComplexStrategyOrderOnBookMessage();
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
        if (Truncated(data, start, cursor, OrderTypeLength, FieldName.OrderType)) return;
        this.orderType = (OrderType)data.Slice(cursor, OrderTypeLength)[0];
        cursor += OrderTypeLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, PriceLength, FieldName.Price)) return;
        this.price = BinaryPrimitives.ReadInt32BigEndian(data.Slice(cursor, PriceLength));
        cursor += PriceLength;
        if (Truncated(data, start, cursor, SizeLength, FieldName.Size)) return;
        this.size = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, SizeLength));
        cursor += SizeLength;
        if (Truncated(data, start, cursor, ExecFlagLength, FieldName.ExecFlag)) return;
        this.execFlag = (ExecFlag)data.Slice(cursor, ExecFlagLength)[0];
        cursor += ExecFlagLength;
        if (Truncated(data, start, cursor, OrderCapacityLength, FieldName.OrderCapacity)) return;
        this.orderCapacity = (OrderCapacity)data.Slice(cursor, OrderCapacityLength)[0];
        cursor += OrderCapacityLength;
        if (Truncated(data, start, cursor, ScopeLength, FieldName.Scope)) return;
        this.scope = (Scope)data.Slice(cursor, ScopeLength)[0];
        cursor += ScopeLength;
        if (Truncated(data, start, cursor, OwnerIdLength, FieldName.OwnerId)) return;
        this.ownerId = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, OwnerIdLength), OwnerIdFill);
        cursor += OwnerIdLength;
        if (Truncated(data, start, cursor, GiveupLength, FieldName.Giveup)) return;
        this.giveup = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, GiveupLength), GiveupFill);
        cursor += GiveupLength;
        if (Truncated(data, start, cursor, CmtaLength, FieldName.Cmta)) return;
        this.cmta = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, CmtaLength), CmtaFill);
        cursor += CmtaLength;
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
        data.Slice(cursor, OrderTypeLength)[0] = unchecked((byte)(this.orderType));
        cursor += OrderTypeLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        BinaryPrimitives.WriteInt32BigEndian(data.Slice(cursor, PriceLength), this.price);
        cursor += PriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, SizeLength), this.size);
        cursor += SizeLength;
        data.Slice(cursor, ExecFlagLength)[0] = unchecked((byte)(this.execFlag));
        cursor += ExecFlagLength;
        data.Slice(cursor, OrderCapacityLength)[0] = unchecked((byte)(this.orderCapacity));
        cursor += OrderCapacityLength;
        data.Slice(cursor, ScopeLength)[0] = unchecked((byte)(this.scope));
        cursor += ScopeLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, OwnerIdLength), TextEncoding.Ascii.Fit(this.ownerId, OwnerIdLength), OwnerIdFill);
        cursor += OwnerIdLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, GiveupLength), TextEncoding.Ascii.Fit(this.giveup, GiveupLength), GiveupFill);
        cursor += GiveupLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, CmtaLength), TextEncoding.Ascii.Fit(this.cmta, CmtaLength), CmtaFill);
        cursor += CmtaLength;
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
        if (!global::System.Enum.IsDefined(this.orderType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OrderType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.side))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Side), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.execFlag))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ExecFlag), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.orderCapacity))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OrderCapacity), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.scope))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Scope), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.ownerId))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OwnerId), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.giveup))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Giveup), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.cmta))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Cmta), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ComplexStrategyOrderOnBookMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.strategyId != other.strategyId) return false;
        if (this.orderType != other.orderType) return false;
        if (this.side != other.side) return false;
        if (this.price != other.price) return false;
        if (this.size != other.size) return false;
        if (this.execFlag != other.execFlag) return false;
        if (this.orderCapacity != other.orderCapacity) return false;
        if (this.scope != other.scope) return false;
        if (this.ownerId != other.ownerId) return false;
        if (this.giveup != other.giveup) return false;
        if (this.cmta != other.cmta) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ComplexStrategyOrderOnBookMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.strategyId);
        hash.Add(this.orderType);
        hash.Add(this.side);
        hash.Add(this.price);
        hash.Add(this.size);
        hash.Add(this.execFlag);
        hash.Add(this.orderCapacity);
        hash.Add(this.scope);
        hash.Add(this.ownerId);
        hash.Add(this.giveup);
        hash.Add(this.cmta);
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
        private const int FieldCount = 13;

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
            if (Has(FieldName.OrderType)) names.Add(nameof(FieldName.OrderType));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.Price)) names.Add(nameof(FieldName.Price));
            if (Has(FieldName.Size)) names.Add(nameof(FieldName.Size));
            if (Has(FieldName.ExecFlag)) names.Add(nameof(FieldName.ExecFlag));
            if (Has(FieldName.OrderCapacity)) names.Add(nameof(FieldName.OrderCapacity));
            if (Has(FieldName.Scope)) names.Add(nameof(FieldName.Scope));
            if (Has(FieldName.OwnerId)) names.Add(nameof(FieldName.OwnerId));
            if (Has(FieldName.Giveup)) names.Add(nameof(FieldName.Giveup));
            if (Has(FieldName.Cmta)) names.Add(nameof(FieldName.Cmta));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ComplexStrategyOrderOnBookMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.strategyId != other.strategyId) diff = diff.With(FieldName.StrategyId);
        if (this.orderType != other.orderType) diff = diff.With(FieldName.OrderType);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.price != other.price) diff = diff.With(FieldName.Price);
        if (this.size != other.size) diff = diff.With(FieldName.Size);
        if (this.execFlag != other.execFlag) diff = diff.With(FieldName.ExecFlag);
        if (this.orderCapacity != other.orderCapacity) diff = diff.With(FieldName.OrderCapacity);
        if (this.scope != other.scope) diff = diff.With(FieldName.Scope);
        if (this.ownerId != other.ownerId) diff = diff.With(FieldName.OwnerId);
        if (this.giveup != other.giveup) diff = diff.With(FieldName.Giveup);
        if (this.cmta != other.cmta) diff = diff.With(FieldName.Cmta);
        return diff;
    }

    private void CopyField(FieldName field, ComplexStrategyOrderOnBookMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.StrategyId:
                this.strategyId = from.strategyId;
                break;
            case FieldName.OrderType:
                this.orderType = from.orderType;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.Price:
                this.price = from.price;
                break;
            case FieldName.Size:
                this.size = from.size;
                break;
            case FieldName.ExecFlag:
                this.execFlag = from.execFlag;
                break;
            case FieldName.OrderCapacity:
                this.orderCapacity = from.orderCapacity;
                break;
            case FieldName.Scope:
                this.scope = from.scope;
                break;
            case FieldName.OwnerId:
                this.ownerId = from.ownerId;
                break;
            case FieldName.Giveup:
                this.giveup = from.giveup;
                break;
            case FieldName.Cmta:
                this.cmta = from.cmta;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ComplexStrategyOrderOnBookMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, StrategyId: {(Text.Of(this.strategyId))}, OrderType: {(Text.Enum(this.orderType))}, Side: {(Text.Enum(this.side))}, Price: {(Text.Of(FixedPoint.ToDecimal(this.price, PriceScale)))}, Size: {(Text.Of(this.size))}, ExecFlag: {(Text.Enum(this.execFlag))}, OrderCapacity: {(Text.Enum(this.orderCapacity))}, Scope: {(Text.Enum(this.scope))}, OwnerId: {(Text.Of(this.ownerId))}, Giveup: {(Text.Of(this.giveup))}, Cmta: {(Text.Of(this.cmta))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.strategyId))}] [{(Text.Enum(this.orderType))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.ComplexStrategyOrderOnBookMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.StrategyId => Text.Of(this.strategyId),
        FieldName.OrderType => Text.Enum(this.orderType),
        FieldName.Side => Text.Enum(this.side),
        FieldName.Price => Text.Of(FixedPoint.ToDecimal(this.price, PriceScale)),
        FieldName.Size => Text.Of(this.size),
        FieldName.ExecFlag => Text.Enum(this.execFlag),
        FieldName.OrderCapacity => Text.Enum(this.orderCapacity),
        FieldName.Scope => Text.Enum(this.scope),
        FieldName.OwnerId => Text.Of(this.ownerId),
        FieldName.Giveup => Text.Of(this.giveup),
        FieldName.Cmta => Text.Of(this.cmta),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.OrderType => global::System.Enum.IsDefined(this.orderType),
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        FieldName.ExecFlag => global::System.Enum.IsDefined(this.execFlag),
        FieldName.OrderCapacity => global::System.Enum.IsDefined(this.orderCapacity),
        FieldName.Scope => global::System.Enum.IsDefined(this.scope),
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
        FieldName.StrategyId => StrategyIdOffset,
        FieldName.OrderType => OrderTypeOffset,
        FieldName.Side => SideOffset,
        FieldName.Price => PriceOffset,
        FieldName.Size => SizeOffset,
        FieldName.ExecFlag => ExecFlagOffset,
        FieldName.OrderCapacity => OrderCapacityOffset,
        FieldName.Scope => ScopeOffset,
        FieldName.OwnerId => OwnerIdOffset,
        FieldName.Giveup => GiveupOffset,
        FieldName.Cmta => CmtaOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.StrategyId => StrategyIdLength,
        FieldName.OrderType => OrderTypeLength,
        FieldName.Side => SideLength,
        FieldName.Price => PriceLength,
        FieldName.Size => SizeLength,
        FieldName.ExecFlag => ExecFlagLength,
        FieldName.OrderCapacity => OrderCapacityLength,
        FieldName.Scope => ScopeLength,
        FieldName.OwnerId => OwnerIdLength,
        FieldName.Giveup => GiveupLength,
        FieldName.Cmta => CmtaLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ComplexStrategyOrderOnBookMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ComplexStrategyOrderOnBookMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.ComplexStrategyOrderOnBookMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
