using System.Buffers.Binary;
using System.Diagnostics;
using static Nasdaq.NsmEquities.Orders.Ouch.v5_0.Rules;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  AIQ Canceled Message
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class AiqCanceledMessage : ISequencedMessage, IEquatable<AiqCanceledMessage>, IFieldOwner
{
    internal const int TimestampOffset = 0;

    internal const int TimestampLength = 8;

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
    public const ulong TimestampWireMaximum = ulong.MaxValue;

    internal const int UserRefNumOffset = TimestampOffset + TimestampLength;

    internal const int UserRefNumLength = 4;

    internal const int DecrementSharesOffset = UserRefNumOffset + UserRefNumLength;

    internal const int DecrementSharesLength = 4;

    internal const int OrderCancelReasonOffset = DecrementSharesOffset + DecrementSharesLength;

    internal const int OrderCancelReasonLength = 1;

    internal const int QuantityPreventedFromTradingOffset = OrderCancelReasonOffset + OrderCancelReasonLength;

    internal const int QuantityPreventedFromTradingLength = 4;

    internal const int ExecutionPriceOffset = QuantityPreventedFromTradingOffset + QuantityPreventedFromTradingLength;

    internal const int ExecutionPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Execution Price.
    /// </summary>
    public const byte ExecutionPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="ExecutionPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal ExecutionPriceFactor = 10000m;

    internal const int LiquidityFlagOffset = ExecutionPriceOffset + ExecutionPriceLength;

    internal const int LiquidityFlagLength = 1;

    internal const int AiqStrategyOffset = LiquidityFlagOffset + LiquidityFlagLength;

    internal const int AiqStrategyLength = 1;

    internal const int AppendageLengthOffset = AiqStrategyOffset + AiqStrategyLength;

    internal const int AppendageLengthLength = 2;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + UserRefNumLength + DecrementSharesLength + OrderCancelReasonLength + QuantityPreventedFromTradingLength + ExecutionPriceLength + LiquidityFlagLength + AiqStrategyLength;

    private const int AiqCanceledAppendageListEntryByteLength = AiqCanceledAppendage.MessageByteLength;

    internal ulong timestamp;

    /// <summary>
    ///  Expressed as nanoseconds since midnight
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

    internal uint userRefNum;

    /// <summary>
    ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
    /// </summary>
    public uint UserRefNum
    {
        get => this.userRefNum;
        set
        {
            this.userRefNum = value;
            Touch();
        }
    }

    internal uint decrementShares;

    /// <summary>
    ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
    /// </summary>
    public uint DecrementShares
    {
        get => this.decrementShares;
        set
        {
            this.decrementShares = value;
            Touch();
        }
    }

    internal char orderCancelReason;

    /// <summary>
    ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
    /// </summary>
    public char OrderCancelReason
    {
        get => this.orderCancelReason;
        set
        {
            this.orderCancelReason = value;
            Touch();
        }
    }

    internal uint quantityPreventedFromTrading;

    /// <summary>
    ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
    /// </summary>
    public uint QuantityPreventedFromTrading
    {
        get => this.quantityPreventedFromTrading;
        set
        {
            this.quantityPreventedFromTrading = value;
            Touch();
        }
    }

    internal ulong executionPrice;

    /// <summary>
    ///  Price at which the trade would have occurred
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal ExecutionPrice
    {
        get => FixedPoint.ToDecimal(this.executionPrice, ExecutionPriceScale);
        set
        {
            this.executionPrice = FixedPoint.ToRaw<ulong>(value, ExecutionPriceFactor, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="ExecutionPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong ExecutionPriceRaw
    {
        get => this.executionPrice;
        set
        {
            this.executionPrice = value;
            Touch();
        }
    }

    internal LiquidityFlag liquidityFlag;

    /// <summary>
    ///  Liquidity flag the order would have received.
    /// </summary>
    public LiquidityFlag LiquidityFlag
    {
        get => this.liquidityFlag;
        set
        {
            this.liquidityFlag = value;
            Touch();
        }
    }

    internal AiqStrategy aiqStrategy;

    /// <summary>
    ///  AIQ strategy used on the cancelled order.
    /// </summary>
    public AiqStrategy AiqStrategy
    {
        get => this.aiqStrategy;
        set
        {
            this.aiqStrategy = value;
            Touch();
        }
    }

    internal ushort? appendageLength;

    /// <summary>
    ///  The length of the remaining Optional Appendage field.
    /// </summary>
    public ushort? AppendageLength
    {
        get => this.appendageLength;
        set
        {
            this.appendageLength = value;
            Touch();
        }
    }

    internal AiqCanceledAppendageList appendage = new([]);

    /// <summary>
    ///  AIQ Canceled Appendage
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public AiqCanceledAppendageList Appendage
    {
        get => this.appendage;
        set
        {
            value ??= new([]);
            this.appendage.SetOwner(null);
            this.appendage = new AiqCanceledAppendageList(value);
            this.appendage.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public SequencedMessageCode DataValue => SequencedMessageCode.AiqCanceledMessage;

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => MessageByteLength;

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var position = 0;
            position += TimestampLength;
            position += UserRefNumLength;
            position += DecrementSharesLength;
            position += OrderCancelReasonLength;
            position += QuantityPreventedFromTradingLength;
            position += ExecutionPriceLength;
            position += LiquidityFlagLength;
            position += AiqStrategyLength;
            if (this.appendageLength is not null || this.appendage.Count != 0)
            {
                position += AppendageLengthLength;
            }
            foreach (var entry in this.appendage)
            {
                position += entry.EncodedByteLength;
            }
            return position;
        }
    }

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.liquidityFlag) && global::System.Enum.IsDefined(this.aiqStrategy) && this.appendage.IsRecognized;

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
    private int appendageOffset;
    private int appendageByteLength;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    IMessage IMessage.Copy() => new AiqCanceledMessage(this);

    void IMessage.SetOwner(IFieldOwner? owner) => this.owner = owner;

    int IMessage.EncodedByteLength => EncodedByteLength;

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
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        Timestamp,
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        UserRefNum,
        /// <summary>
        ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
        /// </summary>
        DecrementShares,
        /// <summary>
        ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
        /// </summary>
        OrderCancelReason,
        /// <summary>
        ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
        /// </summary>
        QuantityPreventedFromTrading,
        /// <summary>
        ///  Price at which the trade would have occurred
        /// </summary>
        ExecutionPrice,
        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        LiquidityFlag,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy,
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        AppendageLength,
        /// <summary>
        ///  AIQ Canceled Appendage
        /// </summary>
        Appendage,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly AiqCanceledMessage owner;

        internal FieldSet(AiqCanceledMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public FieldView UserRefNum => this[FieldName.UserRefNum];

        /// <summary>
        ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
        /// </summary>
        public FieldView DecrementShares => this[FieldName.DecrementShares];

        /// <summary>
        ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
        /// </summary>
        public FieldView OrderCancelReason => this[FieldName.OrderCancelReason];

        /// <summary>
        ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
        /// </summary>
        public FieldView QuantityPreventedFromTrading => this[FieldName.QuantityPreventedFromTrading];

        /// <summary>
        ///  Price at which the trade would have occurred
        /// </summary>
        public FieldView ExecutionPrice => this[FieldName.ExecutionPrice];

        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public FieldView LiquidityFlag => this[FieldName.LiquidityFlag];

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        public FieldView AiqStrategy => this[FieldName.AiqStrategy];

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public FieldView AppendageLength => this[FieldName.AppendageLength];

        /// <summary>
        ///  AIQ Canceled Appendage
        /// </summary>
        public FieldView Appendage => this[FieldName.Appendage];

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
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "UTC", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.UserRefNum), FieldKind.UnsignedInteger, UserRefNumLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.DecrementShares), FieldKind.UnsignedInteger, DecrementSharesLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.OrderCancelReason), FieldKind.Char, OrderCancelReasonLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.QuantityPreventedFromTrading), FieldKind.UnsignedInteger, QuantityPreventedFromTradingLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ExecutionPrice), FieldKind.Decimal, ExecutionPriceLength) { Scale = ExecutionPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.LiquidityFlag), FieldKind.Enum, LiquidityFlagLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.AiqStrategy), FieldKind.Enum, AiqStrategyLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.AppendageLength), FieldKind.UnsignedInteger, AppendageLengthLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Appendage), FieldKind.Group, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="AiqCanceledMessage"/> class with default values.
    /// </summary>
    public AiqCanceledMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="AiqCanceledMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public AiqCanceledMessage(AiqCanceledMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(AiqCanceledMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.userRefNum = other.userRefNum;
        this.decrementShares = other.decrementShares;
        this.orderCancelReason = other.orderCancelReason;
        this.quantityPreventedFromTrading = other.quantityPreventedFromTrading;
        this.executionPrice = other.executionPrice;
        this.liquidityFlag = other.liquidityFlag;
        this.aiqStrategy = other.aiqStrategy;
        this.appendageLength = other.appendageLength;
        this.appendage.SetOwner(null);
        this.appendage = new AiqCanceledAppendageList(other.appendage);
        this.appendage.SetOwner(this);
        this.appendageOffset = other.appendageOffset;
        this.appendageByteLength = other.appendageByteLength;
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
        this.userRefNum = default;
        this.decrementShares = default;
        this.orderCancelReason = default;
        this.quantityPreventedFromTrading = default;
        this.executionPrice = default;
        this.liquidityFlag = default;
        this.aiqStrategy = default;
        this.appendageLength = null;
        this.appendage.SetOwner(null);
        this.appendage = new([]);
        this.appendage.SetOwner(this);
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
    public static AiqCanceledMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new AiqCanceledMessage();
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
    public static AiqCanceledMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new AiqCanceledMessage();
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
        this.timestamp = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, UserRefNumLength, FieldName.UserRefNum)) return;
        this.userRefNum = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, UserRefNumLength));
        cursor += UserRefNumLength;
        if (Truncated(data, start, cursor, DecrementSharesLength, FieldName.DecrementShares)) return;
        this.decrementShares = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, DecrementSharesLength));
        cursor += DecrementSharesLength;
        if (Truncated(data, start, cursor, OrderCancelReasonLength, FieldName.OrderCancelReason)) return;
        this.orderCancelReason = TextEncoding.Ascii.ReadChar(data.Slice(cursor, OrderCancelReasonLength)[0]);
        cursor += OrderCancelReasonLength;
        if (Truncated(data, start, cursor, QuantityPreventedFromTradingLength, FieldName.QuantityPreventedFromTrading)) return;
        this.quantityPreventedFromTrading = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, QuantityPreventedFromTradingLength));
        cursor += QuantityPreventedFromTradingLength;
        if (Truncated(data, start, cursor, ExecutionPriceLength, FieldName.ExecutionPrice)) return;
        this.executionPrice = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, ExecutionPriceLength));
        cursor += ExecutionPriceLength;
        if (Truncated(data, start, cursor, LiquidityFlagLength, FieldName.LiquidityFlag)) return;
        this.liquidityFlag = (LiquidityFlag)data.Slice(cursor, LiquidityFlagLength)[0];
        cursor += LiquidityFlagLength;
        if (Truncated(data, start, cursor, AiqStrategyLength, FieldName.AiqStrategy)) return;
        this.aiqStrategy = (AiqStrategy)data.Slice(cursor, AiqStrategyLength)[0];
        cursor += AiqStrategyLength;
        {
            bool AiqCanceledMessageAppendageLengthOptionalRule0Present;
            AiqCanceledMessageAppendageLengthOptionalRule0Present = cursor < data.Length;
            if (AiqCanceledMessageAppendageLengthOptionalRule0Present)
            {
                if (Truncated(data, start, cursor, AppendageLengthLength, FieldName.AppendageLength)) return;
                this.appendageLength = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, AppendageLengthLength));
                cursor += AppendageLengthLength;
            }
        }
        {
            this.appendageOffset = cursor - start;
            var groupStart = cursor;
            this.appendageByteLength = data.Length - groupStart;
            int AiqCanceledAppendageCountRule0End;
            global::System.Int128 AiqCanceledAppendageCountRule0RawEnd;
            ushort aiqCanceledMessageAppendageLengthValue = this.appendageLength.GetValueOrDefault();
            var entries = new List<AiqCanceledAppendage>();
            AiqCanceledAppendageCountRule0RawEnd = checked((checked((global::System.Int128)(aiqCanceledMessageAppendageLengthValue)) + checked((global::System.Int128)(cursor))));
            if (AiqCanceledAppendageCountRule0RawEnd < cursor || AiqCanceledAppendageCountRule0RawEnd > data.Length) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            AiqCanceledAppendageCountRule0End = (int)AiqCanceledAppendageCountRule0RawEnd;
            while (cursor < AiqCanceledAppendageCountRule0End)
            {
                int aiqCanceledAppendageEntryStart = cursor;
                var entry = new AiqCanceledAppendage();
                var contract = (IGroupEntry)entry;
                contract.ParseFrom(data[..AiqCanceledAppendageCountRule0End], ref cursor);
                if (contract.Status != ParseStatus.Ok)
                {
                    this.appendage = new AiqCanceledAppendageList(entries, contract.Status, entry);
                    this.appendage.SetOwner(this);
                    Fail(contract.Status, FieldName.Appendage, groupStart - start, data[start..]);
                    return;
                }
                entries.Add(entry);
                if (cursor <= aiqCanceledAppendageEntryStart || cursor > AiqCanceledAppendageCountRule0End) { Fail(ParseStatus.Truncated, FieldName.Appendage, groupStart - start, data[start..]); return; }
            }
            if (cursor != AiqCanceledAppendageCountRule0End) { Fail(ParseStatus.BadCount, FieldName.Appendage, groupStart - start, data[start..]); return; }
            this.appendage = new AiqCanceledAppendageList(entries, ParseStatus.Ok, null);
            this.appendage.SetOwner(this);
            this.appendageByteLength = cursor - groupStart;
        }
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
        if (data.Length - offset < EncodedByteLength)
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
        var buffer = new byte[EncodedByteLength];
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
        if (data.Length < EncodedByteLength)
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
        var position = offset;
        position += TimestampLength;
        position += UserRefNumLength;
        position += DecrementSharesLength;
        position += OrderCancelReasonLength;
        position += QuantityPreventedFromTradingLength;
        position += ExecutionPriceLength;
        position += LiquidityFlagLength;
        position += AiqStrategyLength;
        var aiqCanceledMessageAppendageLengthMeasureStart = position;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            position += AppendageLengthLength;
        }
        var aiqCanceledMessageAppendageLengthMeasureEnd = position;
        var aiqCanceledAppendageMeasureStart = position;
        foreach (var entry in this.appendage)
        {
            position += entry.EncodedByteLength;
        }
        var aiqCanceledAppendageMeasureEnd = position;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            global::System.Int128 measure = aiqCanceledAppendageMeasureEnd;
            global::System.Int128 aiqCanceledMessageAppendageLengthStampedValue;
            try
            {
                aiqCanceledMessageAppendageLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + checked((global::System.Int128)(aiqCanceledAppendageMeasureStart))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (aiqCanceledMessageAppendageLengthStampedValue > ushort.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.TooLong);
                return false;
            }
            if (aiqCanceledMessageAppendageLengthStampedValue < ushort.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.AppendageLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.appendageLength = checked((ushort)(aiqCanceledMessageAppendageLengthStampedValue));
        }
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, UserRefNumLength), this.userRefNum);
        cursor += UserRefNumLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, DecrementSharesLength), this.decrementShares);
        cursor += DecrementSharesLength;
        if (!TextEncoding.Ascii.CanWrite(this.orderCancelReason))
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.OrderCancelReason), EncodeStatus.DoesNotFit);
            return false;
        }
        data.Slice(cursor, OrderCancelReasonLength)[0] = TextEncoding.Ascii.WriteChar(this.orderCancelReason);
        cursor += OrderCancelReasonLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, QuantityPreventedFromTradingLength), this.quantityPreventedFromTrading);
        cursor += QuantityPreventedFromTradingLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, ExecutionPriceLength), this.executionPrice);
        cursor += ExecutionPriceLength;
        data.Slice(cursor, LiquidityFlagLength)[0] = unchecked((byte)(this.liquidityFlag));
        cursor += LiquidityFlagLength;
        data.Slice(cursor, AiqStrategyLength)[0] = unchecked((byte)(this.aiqStrategy));
        cursor += AiqStrategyLength;
        if (this.appendageLength is not null || this.appendage.Count != 0)
        {
            BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, AppendageLengthLength), this.appendageLength.GetValueOrDefault());
            cursor += AppendageLengthLength;
        }
        {
            this.appendageOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.appendage.Count; index++)
            {
                if (!((IGroupEntry)this.appendage[index]).Write(data, cursor, out cursor, out var memberFailure))
                {
                    failure = memberFailure.Within(new FieldView(this, (int)FieldName.Appendage), index);
                    return false;
                }
            }
            this.appendageByteLength = cursor - groupStart;
        }
        {
            bool agrees;
            agrees = (aiqCanceledMessageAppendageLengthMeasureStart < offset + EncodedByteLength) == (this.appendageLength is not null || this.appendage.Count != 0);
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
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
                var buffer = new byte[EncodedByteLength];
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
        if (!global::System.Text.Ascii.IsValid(this.orderCancelReason))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OrderCancelReason), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.liquidityFlag))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.LiquidityFlag), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.aiqStrategy))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AiqStrategy), ValidationStatus.UndeclaredValue));
        }
        for (var index = 0; index < this.appendage.Count; index++)
        {
            foreach (var failure in ((IGroupEntry)this.appendage[index]).Validate())
            {
                failures.Add(failure.Within(new FieldView(this, (int)FieldName.Appendage), index));
            }
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(AiqCanceledMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.userRefNum != other.userRefNum) return false;
        if (this.decrementShares != other.decrementShares) return false;
        if (this.orderCancelReason != other.orderCancelReason) return false;
        if (this.quantityPreventedFromTrading != other.quantityPreventedFromTrading) return false;
        if (this.executionPrice != other.executionPrice) return false;
        if (this.liquidityFlag != other.liquidityFlag) return false;
        if (this.aiqStrategy != other.aiqStrategy) return false;
        if (!this.appendage.Equals(other.appendage)) return false;
        if ((this.appendageLength is not null || this.appendage.Count != 0) != (other.appendageLength is not null || other.appendage.Count != 0)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is AiqCanceledMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.userRefNum);
        hash.Add(this.decrementShares);
        hash.Add(this.orderCancelReason);
        hash.Add(this.quantityPreventedFromTrading);
        hash.Add(this.executionPrice);
        hash.Add(this.liquidityFlag);
        hash.Add(this.aiqStrategy);
        hash.Add(this.appendage);
        hash.Add(this.appendageLength is not null || this.appendage.Count != 0);
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
        private const int FieldCount = 11;

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
            if (Has(FieldName.UserRefNum)) names.Add(nameof(FieldName.UserRefNum));
            if (Has(FieldName.DecrementShares)) names.Add(nameof(FieldName.DecrementShares));
            if (Has(FieldName.OrderCancelReason)) names.Add(nameof(FieldName.OrderCancelReason));
            if (Has(FieldName.QuantityPreventedFromTrading)) names.Add(nameof(FieldName.QuantityPreventedFromTrading));
            if (Has(FieldName.ExecutionPrice)) names.Add(nameof(FieldName.ExecutionPrice));
            if (Has(FieldName.LiquidityFlag)) names.Add(nameof(FieldName.LiquidityFlag));
            if (Has(FieldName.AiqStrategy)) names.Add(nameof(FieldName.AiqStrategy));
            if (Has(FieldName.AppendageLength)) names.Add(nameof(FieldName.AppendageLength));
            if (Has(FieldName.Appendage)) names.Add(nameof(FieldName.Appendage));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(AiqCanceledMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.userRefNum != other.userRefNum) diff = diff.With(FieldName.UserRefNum);
        if (this.decrementShares != other.decrementShares) diff = diff.With(FieldName.DecrementShares);
        if (this.orderCancelReason != other.orderCancelReason) diff = diff.With(FieldName.OrderCancelReason);
        if (this.quantityPreventedFromTrading != other.quantityPreventedFromTrading) diff = diff.With(FieldName.QuantityPreventedFromTrading);
        if (this.executionPrice != other.executionPrice) diff = diff.With(FieldName.ExecutionPrice);
        if (this.liquidityFlag != other.liquidityFlag) diff = diff.With(FieldName.LiquidityFlag);
        if (this.aiqStrategy != other.aiqStrategy) diff = diff.With(FieldName.AiqStrategy);
        if (!this.appendage.Equals(other.appendage)) diff = diff.With(FieldName.Appendage);
        if ((this.appendageLength is not null || this.appendage.Count != 0) != (other.appendageLength is not null || other.appendage.Count != 0)) diff = diff.With(FieldName.AppendageLength);
        return diff;
    }

    private void CopyField(FieldName field, AiqCanceledMessage from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.UserRefNum:
                this.userRefNum = from.userRefNum;
                break;
            case FieldName.DecrementShares:
                this.decrementShares = from.decrementShares;
                break;
            case FieldName.OrderCancelReason:
                this.orderCancelReason = from.orderCancelReason;
                break;
            case FieldName.QuantityPreventedFromTrading:
                this.quantityPreventedFromTrading = from.quantityPreventedFromTrading;
                break;
            case FieldName.ExecutionPrice:
                this.executionPrice = from.executionPrice;
                break;
            case FieldName.LiquidityFlag:
                this.liquidityFlag = from.liquidityFlag;
                break;
            case FieldName.AiqStrategy:
                this.aiqStrategy = from.aiqStrategy;
                break;
            case FieldName.AppendageLength:
                this.appendageLength = from.appendageLength;
                break;
            case FieldName.Appendage:
                this.appendage.SetOwner(null);
                this.appendage = new AiqCanceledAppendageList(from.appendage);
                this.appendage.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"AiqCanceledMessage {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, UserRefNum: {(Text.Of(this.userRefNum))}, DecrementShares: {(Text.Of(this.decrementShares))}, OrderCancelReason: {(Text.Of(this.orderCancelReason))}, QuantityPreventedFromTrading: {(Text.Of(this.quantityPreventedFromTrading))}, ExecutionPrice: {(Text.Of(FixedPoint.ToDecimal(this.executionPrice, ExecutionPriceScale)))}, LiquidityFlag: {(Text.Enum(this.liquidityFlag))}, AiqStrategy: {(Text.Enum(this.aiqStrategy))}, AppendageLength: {(Text.Of(this.appendageLength))}, Appendage: {(Text.Entries(this.appendage))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.userRefNum))}] [{(Text.Of(this.decrementShares))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(SequencedMessageCode.AiqCanceledMessage),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.UserRefNum => Text.Of(this.userRefNum),
        FieldName.DecrementShares => Text.Of(this.decrementShares),
        FieldName.OrderCancelReason => Text.Of(this.orderCancelReason),
        FieldName.QuantityPreventedFromTrading => Text.Of(this.quantityPreventedFromTrading),
        FieldName.ExecutionPrice => Text.Of(FixedPoint.ToDecimal(this.executionPrice, ExecutionPriceScale)),
        FieldName.LiquidityFlag => Text.Enum(this.liquidityFlag),
        FieldName.AiqStrategy => Text.Enum(this.aiqStrategy),
        FieldName.AppendageLength => Text.Of(this.appendageLength),
        FieldName.Appendage => Text.Entries(this.appendage),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.LiquidityFlag => global::System.Enum.IsDefined(this.liquidityFlag),
        FieldName.AiqStrategy => global::System.Enum.IsDefined(this.aiqStrategy),
        FieldName.Appendage => this.appendage.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.AppendageLength => this.appendageLength is not null || this.appendage.Count != 0,
        FieldName.Appendage => this.appendage.Count != 0,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => (FieldName)field switch
    {
        FieldName.Appendage => true,
        _ => false,
    };

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.Timestamp => TimestampOffset,
        FieldName.UserRefNum => UserRefNumOffset,
        FieldName.DecrementShares => DecrementSharesOffset,
        FieldName.OrderCancelReason => OrderCancelReasonOffset,
        FieldName.QuantityPreventedFromTrading => QuantityPreventedFromTradingOffset,
        FieldName.ExecutionPrice => ExecutionPriceOffset,
        FieldName.LiquidityFlag => LiquidityFlagOffset,
        FieldName.AiqStrategy => AiqStrategyOffset,
        FieldName.AppendageLength => AppendageLengthOffset,
        FieldName.Appendage => this.appendageOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.UserRefNum => UserRefNumLength,
        FieldName.DecrementShares => DecrementSharesLength,
        FieldName.OrderCancelReason => OrderCancelReasonLength,
        FieldName.QuantityPreventedFromTrading => QuantityPreventedFromTradingLength,
        FieldName.ExecutionPrice => ExecutionPriceLength,
        FieldName.LiquidityFlag => LiquidityFlagLength,
        FieldName.AiqStrategy => AiqStrategyLength,
        FieldName.AppendageLength => this.appendageLength is not null || this.appendage.Count != 0 ? AppendageLengthLength : 0,
        FieldName.Appendage => this.appendageByteLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => (FieldName)field switch
    {
        FieldName.Appendage => this.appendage,
        _ => null,
    };

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is AiqCanceledMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as AiqCanceledMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(SequencedMessageCode.AiqCanceledMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
