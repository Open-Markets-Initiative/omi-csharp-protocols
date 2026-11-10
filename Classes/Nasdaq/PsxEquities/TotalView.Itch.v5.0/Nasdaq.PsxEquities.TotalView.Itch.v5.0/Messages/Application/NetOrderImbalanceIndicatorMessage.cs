using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  NASDAQ PSX disseminates NOII data at 5-second intervals in the minutes leading up to the NASDAQ Opening Cross and NASDAQ Closing Cross
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class NetOrderImbalanceIndicatorMessage : IPacketMessage, IEquatable<NetOrderImbalanceIndicatorMessage>, IFieldOwner
{
    internal const int StockLocateOffset = 0;

    internal const int StockLocateLength = 2;

    internal const int TrackingNumberOffset = StockLocateOffset + StockLocateLength;

    internal const int TrackingNumberLength = 2;

    internal const int TimestampOffset = TrackingNumberOffset + TrackingNumberLength;

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

    internal const int PairedSharesOffset = TimestampOffset + TimestampLength;

    internal const int PairedSharesLength = 8;

    internal const int ImbalanceSharesOffset = PairedSharesOffset + PairedSharesLength;

    internal const int ImbalanceSharesLength = 8;

    internal const int ImbalanceDirectionOffset = ImbalanceSharesOffset + ImbalanceSharesLength;

    internal const int ImbalanceDirectionLength = 1;

    internal const int StockOffset = ImbalanceDirectionOffset + ImbalanceDirectionLength;

    internal const int StockLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Stock.
    /// </summary>
    public const byte StockFill = (byte)' ';

    internal const int FarPriceOffset = StockOffset + StockLength;

    internal const int FarPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Far Price.
    /// </summary>
    public const byte FarPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="FarPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal FarPriceFactor = 10000m;

    internal const int NearPriceOffset = FarPriceOffset + FarPriceLength;

    internal const int NearPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Near Price.
    /// </summary>
    public const byte NearPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="NearPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal NearPriceFactor = 10000m;

    internal const int CurrentReferencePriceOffset = NearPriceOffset + NearPriceLength;

    internal const int CurrentReferencePriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Current Reference Price.
    /// </summary>
    public const byte CurrentReferencePriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="CurrentReferencePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal CurrentReferencePriceFactor = 10000m;

    internal const int CrossTypeOffset = CurrentReferencePriceOffset + CurrentReferencePriceLength;

    internal const int CrossTypeLength = 1;

    internal const int PriceVariationIndicatorOffset = CrossTypeOffset + CrossTypeLength;

    internal const int PriceVariationIndicatorLength = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = StockLocateLength + TrackingNumberLength + TimestampLength + PairedSharesLength + ImbalanceSharesLength + ImbalanceDirectionLength + StockLength + FarPriceLength + NearPriceLength + CurrentReferencePriceLength + CrossTypeLength + PriceVariationIndicatorLength;

    internal ushort stockLocate;

    /// <summary>
    ///  Always 0
    /// </summary>
    public ushort StockLocate
    {
        get => this.stockLocate;
        set
        {
            this.stockLocate = value;
            Touch();
        }
    }

    internal ushort trackingNumber;

    /// <summary>
    ///  NASDAQ PSX internal tracking number
    /// </summary>
    public ushort TrackingNumber
    {
        get => this.trackingNumber;
        set
        {
            this.trackingNumber = value;
            Touch();
        }
    }

    internal ulong timestamp;

    /// <summary>
    ///  Nanoseconds since midnight.
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

    internal ulong pairedShares;

    /// <summary>
    ///  The total number of shares that are eligible to be matched at the Current Reference Price.
    /// </summary>
    public ulong PairedShares
    {
        get => this.pairedShares;
        set
        {
            this.pairedShares = value;
            Touch();
        }
    }

    internal ulong imbalanceShares;

    /// <summary>
    ///  The number of shares not paired at the Current Reference Price.
    /// </summary>
    public ulong ImbalanceShares
    {
        get => this.imbalanceShares;
        set
        {
            this.imbalanceShares = value;
            Touch();
        }
    }

    internal ImbalanceDirection imbalanceDirection;

    /// <summary>
    ///  The market side of the order imbalance.
    /// </summary>
    public ImbalanceDirection ImbalanceDirection
    {
        get => this.imbalanceDirection;
        set
        {
            this.imbalanceDirection = value;
            Touch();
        }
    }

    internal string stock = string.Empty;

    /// <summary>
    ///  Denotes the security symbol for the issue in the NASDAQ execution system.
    /// </summary>
    /// <remarks>
    ///  The field is 8 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Stock
    {
        get => this.stock;
        set
        {
            this.stock = value ?? string.Empty;
            Touch();
        }
    }

    internal uint farPrice;

    /// <summary>
    ///  A hypothetical auction-clearing price for cross orders only. Refer to Data Types for field processing notes.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal FarPrice
    {
        get => FixedPoint.ToDecimal(this.farPrice, FarPriceScale);
        set
        {
            this.farPrice = FixedPoint.ToRaw<uint>(value, FarPriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="FarPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint FarPriceRaw
    {
        get => this.farPrice;
        set
        {
            this.farPrice = value;
            Touch();
        }
    }

    internal uint nearPrice;

    /// <summary>
    ///  A hypothetical auction-clearing price for cross orders as well as continuous orders. Refer to Data Types for field processing notes.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal NearPrice
    {
        get => FixedPoint.ToDecimal(this.nearPrice, NearPriceScale);
        set
        {
            this.nearPrice = FixedPoint.ToRaw<uint>(value, NearPriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="NearPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint NearPriceRaw
    {
        get => this.nearPrice;
        set
        {
            this.nearPrice = value;
            Touch();
        }
    }

    internal uint currentReferencePrice;

    /// <summary>
    ///  The price at which the NOII shares are being calculated. Refer to Data Types for field processing notes.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal CurrentReferencePrice
    {
        get => FixedPoint.ToDecimal(this.currentReferencePrice, CurrentReferencePriceScale);
        set
        {
            this.currentReferencePrice = FixedPoint.ToRaw<uint>(value, CurrentReferencePriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="CurrentReferencePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint CurrentReferencePriceRaw
    {
        get => this.currentReferencePrice;
        set
        {
            this.currentReferencePrice = value;
            Touch();
        }
    }

    internal CrossType crossType;

    /// <summary>
    ///  The Nasdaq PSX cross session for which the message is being generated.
    /// </summary>
    public CrossType CrossType
    {
        get => this.crossType;
        set
        {
            this.crossType = value;
            Touch();
        }
    }

    internal PriceVariationIndicator priceVariationIndicator;

    /// <summary>
    ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
    /// </summary>
    public PriceVariationIndicator PriceVariationIndicator
    {
        get => this.priceVariationIndicator;
        set
        {
            this.priceVariationIndicator = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.NetOrderImbalanceIndicatorMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.imbalanceDirection) && global::System.Enum.IsDefined(this.crossType) && global::System.Enum.IsDefined(this.priceVariationIndicator);

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

    IMessage IMessage.Copy() => new NetOrderImbalanceIndicatorMessage(this);

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
        ///  Always 0
        /// </summary>
        StockLocate,
        /// <summary>
        ///  NASDAQ PSX internal tracking number
        /// </summary>
        TrackingNumber,
        /// <summary>
        ///  Nanoseconds since midnight.
        /// </summary>
        Timestamp,
        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price.
        /// </summary>
        PairedShares,
        /// <summary>
        ///  The number of shares not paired at the Current Reference Price.
        /// </summary>
        ImbalanceShares,
        /// <summary>
        ///  The market side of the order imbalance.
        /// </summary>
        ImbalanceDirection,
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        Stock,
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only. Refer to Data Types for field processing notes.
        /// </summary>
        FarPrice,
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders. Refer to Data Types for field processing notes.
        /// </summary>
        NearPrice,
        /// <summary>
        ///  The price at which the NOII shares are being calculated. Refer to Data Types for field processing notes.
        /// </summary>
        CurrentReferencePrice,
        /// <summary>
        ///  The Nasdaq PSX cross session for which the message is being generated.
        /// </summary>
        CrossType,
        /// <summary>
        ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
        /// </summary>
        PriceVariationIndicator,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly NetOrderImbalanceIndicatorMessage owner;

        internal FieldSet(NetOrderImbalanceIndicatorMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Always 0
        /// </summary>
        public FieldView StockLocate => this[FieldName.StockLocate];

        /// <summary>
        ///  NASDAQ PSX internal tracking number
        /// </summary>
        public FieldView TrackingNumber => this[FieldName.TrackingNumber];

        /// <summary>
        ///  Nanoseconds since midnight.
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price.
        /// </summary>
        public FieldView PairedShares => this[FieldName.PairedShares];

        /// <summary>
        ///  The number of shares not paired at the Current Reference Price.
        /// </summary>
        public FieldView ImbalanceShares => this[FieldName.ImbalanceShares];

        /// <summary>
        ///  The market side of the order imbalance.
        /// </summary>
        public FieldView ImbalanceDirection => this[FieldName.ImbalanceDirection];

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public FieldView Stock => this[FieldName.Stock];

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only. Refer to Data Types for field processing notes.
        /// </summary>
        public FieldView FarPrice => this[FieldName.FarPrice];

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders. Refer to Data Types for field processing notes.
        /// </summary>
        public FieldView NearPrice => this[FieldName.NearPrice];

        /// <summary>
        ///  The price at which the NOII shares are being calculated. Refer to Data Types for field processing notes.
        /// </summary>
        public FieldView CurrentReferencePrice => this[FieldName.CurrentReferencePrice];

        /// <summary>
        ///  The Nasdaq PSX cross session for which the message is being generated.
        /// </summary>
        public FieldView CrossType => this[FieldName.CrossType];

        /// <summary>
        ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
        /// </summary>
        public FieldView PriceVariationIndicator => this[FieldName.PriceVariationIndicator];

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
        new(nameof(FieldName.StockLocate), FieldKind.UnsignedInteger, StockLocateLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TrackingNumber), FieldKind.UnsignedInteger, TrackingNumberLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.PairedShares), FieldKind.UnsignedInteger, PairedSharesLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ImbalanceShares), FieldKind.UnsignedInteger, ImbalanceSharesLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.ImbalanceDirection), FieldKind.Enum, ImbalanceDirectionLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Stock), FieldKind.String, StockLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.FarPrice), FieldKind.Decimal, FarPriceLength) { Scale = FarPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.NearPrice), FieldKind.Decimal, NearPriceLength) { Scale = NearPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.CurrentReferencePrice), FieldKind.Decimal, CurrentReferencePriceLength) { Scale = CurrentReferencePriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.CrossType), FieldKind.Enum, CrossTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.PriceVariationIndicator), FieldKind.Enum, PriceVariationIndicatorLength) { Fill = "Spaces", Justification = "Left" },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="NetOrderImbalanceIndicatorMessage"/> class with default values.
    /// </summary>
    public NetOrderImbalanceIndicatorMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="NetOrderImbalanceIndicatorMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public NetOrderImbalanceIndicatorMessage(NetOrderImbalanceIndicatorMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(NetOrderImbalanceIndicatorMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.stockLocate = other.stockLocate;
        this.trackingNumber = other.trackingNumber;
        this.timestamp = other.timestamp;
        this.pairedShares = other.pairedShares;
        this.imbalanceShares = other.imbalanceShares;
        this.imbalanceDirection = other.imbalanceDirection;
        this.stock = other.stock;
        this.farPrice = other.farPrice;
        this.nearPrice = other.nearPrice;
        this.currentReferencePrice = other.currentReferencePrice;
        this.crossType = other.crossType;
        this.priceVariationIndicator = other.priceVariationIndicator;
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
        this.stockLocate = default;
        this.trackingNumber = default;
        this.timestamp = default;
        this.pairedShares = default;
        this.imbalanceShares = default;
        this.imbalanceDirection = default;
        this.stock = string.Empty;
        this.farPrice = default;
        this.nearPrice = default;
        this.currentReferencePrice = default;
        this.crossType = default;
        this.priceVariationIndicator = default;
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
    public static NetOrderImbalanceIndicatorMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new NetOrderImbalanceIndicatorMessage();
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
    public static NetOrderImbalanceIndicatorMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new NetOrderImbalanceIndicatorMessage();
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
        if (Truncated(data, start, cursor, StockLocateLength, FieldName.StockLocate)) return;
        this.stockLocate = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, StockLocateLength));
        cursor += StockLocateLength;
        if (Truncated(data, start, cursor, TrackingNumberLength, FieldName.TrackingNumber)) return;
        this.trackingNumber = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, TrackingNumberLength));
        cursor += TrackingNumberLength;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = Padded.ReadBigEndian<ulong>(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, PairedSharesLength, FieldName.PairedShares)) return;
        this.pairedShares = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, PairedSharesLength));
        cursor += PairedSharesLength;
        if (Truncated(data, start, cursor, ImbalanceSharesLength, FieldName.ImbalanceShares)) return;
        this.imbalanceShares = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, ImbalanceSharesLength));
        cursor += ImbalanceSharesLength;
        if (Truncated(data, start, cursor, ImbalanceDirectionLength, FieldName.ImbalanceDirection)) return;
        this.imbalanceDirection = (ImbalanceDirection)data.Slice(cursor, ImbalanceDirectionLength)[0];
        cursor += ImbalanceDirectionLength;
        if (Truncated(data, start, cursor, StockLength, FieldName.Stock)) return;
        this.stock = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StockLength), StockFill);
        cursor += StockLength;
        if (Truncated(data, start, cursor, FarPriceLength, FieldName.FarPrice)) return;
        this.farPrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, FarPriceLength));
        cursor += FarPriceLength;
        if (Truncated(data, start, cursor, NearPriceLength, FieldName.NearPrice)) return;
        this.nearPrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, NearPriceLength));
        cursor += NearPriceLength;
        if (Truncated(data, start, cursor, CurrentReferencePriceLength, FieldName.CurrentReferencePrice)) return;
        this.currentReferencePrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, CurrentReferencePriceLength));
        cursor += CurrentReferencePriceLength;
        if (Truncated(data, start, cursor, CrossTypeLength, FieldName.CrossType)) return;
        this.crossType = (CrossType)data.Slice(cursor, CrossTypeLength)[0];
        cursor += CrossTypeLength;
        if (Truncated(data, start, cursor, PriceVariationIndicatorLength, FieldName.PriceVariationIndicator)) return;
        this.priceVariationIndicator = (PriceVariationIndicator)data.Slice(cursor, PriceVariationIndicatorLength)[0];
        cursor += PriceVariationIndicatorLength;
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
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, StockLocateLength), this.stockLocate);
        cursor += StockLocateLength;
        BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, TrackingNumberLength), this.trackingNumber);
        cursor += TrackingNumberLength;
        if (this.timestamp > TimestampWireMaximum)
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.Timestamp), EncodeStatus.DoesNotFit);
            return false;
        }
        Padded.WriteBigEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, PairedSharesLength), this.pairedShares);
        cursor += PairedSharesLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, ImbalanceSharesLength), this.imbalanceShares);
        cursor += ImbalanceSharesLength;
        data.Slice(cursor, ImbalanceDirectionLength)[0] = unchecked((byte)(this.imbalanceDirection));
        cursor += ImbalanceDirectionLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StockLength), TextEncoding.Ascii.Fit(this.stock, StockLength), StockFill);
        cursor += StockLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, FarPriceLength), this.farPrice);
        cursor += FarPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, NearPriceLength), this.nearPrice);
        cursor += NearPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, CurrentReferencePriceLength), this.currentReferencePrice);
        cursor += CurrentReferencePriceLength;
        data.Slice(cursor, CrossTypeLength)[0] = unchecked((byte)(this.crossType));
        cursor += CrossTypeLength;
        data.Slice(cursor, PriceVariationIndicatorLength)[0] = unchecked((byte)(this.priceVariationIndicator));
        cursor += PriceVariationIndicatorLength;
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
        if (!global::System.Enum.IsDefined(this.imbalanceDirection))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ImbalanceDirection), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.stock))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Stock), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.crossType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.CrossType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.priceVariationIndicator))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.PriceVariationIndicator), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(NetOrderImbalanceIndicatorMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.stockLocate != other.stockLocate) return false;
        if (this.trackingNumber != other.trackingNumber) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.pairedShares != other.pairedShares) return false;
        if (this.imbalanceShares != other.imbalanceShares) return false;
        if (this.imbalanceDirection != other.imbalanceDirection) return false;
        if (this.stock != other.stock) return false;
        if (this.farPrice != other.farPrice) return false;
        if (this.nearPrice != other.nearPrice) return false;
        if (this.currentReferencePrice != other.currentReferencePrice) return false;
        if (this.crossType != other.crossType) return false;
        if (this.priceVariationIndicator != other.priceVariationIndicator) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is NetOrderImbalanceIndicatorMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.stockLocate);
        hash.Add(this.trackingNumber);
        hash.Add(this.timestamp);
        hash.Add(this.pairedShares);
        hash.Add(this.imbalanceShares);
        hash.Add(this.imbalanceDirection);
        hash.Add(this.stock);
        hash.Add(this.farPrice);
        hash.Add(this.nearPrice);
        hash.Add(this.currentReferencePrice);
        hash.Add(this.crossType);
        hash.Add(this.priceVariationIndicator);
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
            if (Has(FieldName.StockLocate)) names.Add(nameof(FieldName.StockLocate));
            if (Has(FieldName.TrackingNumber)) names.Add(nameof(FieldName.TrackingNumber));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.PairedShares)) names.Add(nameof(FieldName.PairedShares));
            if (Has(FieldName.ImbalanceShares)) names.Add(nameof(FieldName.ImbalanceShares));
            if (Has(FieldName.ImbalanceDirection)) names.Add(nameof(FieldName.ImbalanceDirection));
            if (Has(FieldName.Stock)) names.Add(nameof(FieldName.Stock));
            if (Has(FieldName.FarPrice)) names.Add(nameof(FieldName.FarPrice));
            if (Has(FieldName.NearPrice)) names.Add(nameof(FieldName.NearPrice));
            if (Has(FieldName.CurrentReferencePrice)) names.Add(nameof(FieldName.CurrentReferencePrice));
            if (Has(FieldName.CrossType)) names.Add(nameof(FieldName.CrossType));
            if (Has(FieldName.PriceVariationIndicator)) names.Add(nameof(FieldName.PriceVariationIndicator));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(NetOrderImbalanceIndicatorMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.stockLocate != other.stockLocate) diff = diff.With(FieldName.StockLocate);
        if (this.trackingNumber != other.trackingNumber) diff = diff.With(FieldName.TrackingNumber);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.pairedShares != other.pairedShares) diff = diff.With(FieldName.PairedShares);
        if (this.imbalanceShares != other.imbalanceShares) diff = diff.With(FieldName.ImbalanceShares);
        if (this.imbalanceDirection != other.imbalanceDirection) diff = diff.With(FieldName.ImbalanceDirection);
        if (this.stock != other.stock) diff = diff.With(FieldName.Stock);
        if (this.farPrice != other.farPrice) diff = diff.With(FieldName.FarPrice);
        if (this.nearPrice != other.nearPrice) diff = diff.With(FieldName.NearPrice);
        if (this.currentReferencePrice != other.currentReferencePrice) diff = diff.With(FieldName.CurrentReferencePrice);
        if (this.crossType != other.crossType) diff = diff.With(FieldName.CrossType);
        if (this.priceVariationIndicator != other.priceVariationIndicator) diff = diff.With(FieldName.PriceVariationIndicator);
        return diff;
    }

    private void CopyField(FieldName field, NetOrderImbalanceIndicatorMessage from)
    {
        switch (field)
        {
            case FieldName.StockLocate:
                this.stockLocate = from.stockLocate;
                break;
            case FieldName.TrackingNumber:
                this.trackingNumber = from.trackingNumber;
                break;
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.PairedShares:
                this.pairedShares = from.pairedShares;
                break;
            case FieldName.ImbalanceShares:
                this.imbalanceShares = from.imbalanceShares;
                break;
            case FieldName.ImbalanceDirection:
                this.imbalanceDirection = from.imbalanceDirection;
                break;
            case FieldName.Stock:
                this.stock = from.stock;
                break;
            case FieldName.FarPrice:
                this.farPrice = from.farPrice;
                break;
            case FieldName.NearPrice:
                this.nearPrice = from.nearPrice;
                break;
            case FieldName.CurrentReferencePrice:
                this.currentReferencePrice = from.currentReferencePrice;
                break;
            case FieldName.CrossType:
                this.crossType = from.crossType;
                break;
            case FieldName.PriceVariationIndicator:
                this.priceVariationIndicator = from.priceVariationIndicator;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"NetOrderImbalanceIndicatorMessage {{ StockLocate: {(Text.Of(this.stockLocate))}, TrackingNumber: {(Text.Of(this.trackingNumber))}, Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, PairedShares: {(Text.Of(this.pairedShares))}, ImbalanceShares: {(Text.Of(this.imbalanceShares))}, ImbalanceDirection: {(Text.Enum(this.imbalanceDirection))}, Stock: {(Text.Of(this.stock))}, FarPrice: {(Text.Of(FixedPoint.ToDecimal(this.farPrice, FarPriceScale)))}, NearPrice: {(Text.Of(FixedPoint.ToDecimal(this.nearPrice, NearPriceScale)))}, CurrentReferencePrice: {(Text.Of(FixedPoint.ToDecimal(this.currentReferencePrice, CurrentReferencePriceScale)))}, CrossType: {(Text.Enum(this.crossType))}, PriceVariationIndicator: {(Text.Enum(this.priceVariationIndicator))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Of(this.stockLocate))}] [{(Text.Of(this.trackingNumber))}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.NetOrderImbalanceIndicatorMessage),
        FieldName.StockLocate => Text.Of(this.stockLocate),
        FieldName.TrackingNumber => Text.Of(this.trackingNumber),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.PairedShares => Text.Of(this.pairedShares),
        FieldName.ImbalanceShares => Text.Of(this.imbalanceShares),
        FieldName.ImbalanceDirection => Text.Enum(this.imbalanceDirection),
        FieldName.Stock => Text.Of(this.stock),
        FieldName.FarPrice => Text.Of(FixedPoint.ToDecimal(this.farPrice, FarPriceScale)),
        FieldName.NearPrice => Text.Of(FixedPoint.ToDecimal(this.nearPrice, NearPriceScale)),
        FieldName.CurrentReferencePrice => Text.Of(FixedPoint.ToDecimal(this.currentReferencePrice, CurrentReferencePriceScale)),
        FieldName.CrossType => Text.Enum(this.crossType),
        FieldName.PriceVariationIndicator => Text.Enum(this.priceVariationIndicator),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.ImbalanceDirection => global::System.Enum.IsDefined(this.imbalanceDirection),
        FieldName.CrossType => global::System.Enum.IsDefined(this.crossType),
        FieldName.PriceVariationIndicator => global::System.Enum.IsDefined(this.priceVariationIndicator),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.StockLocate => StockLocateOffset,
        FieldName.TrackingNumber => TrackingNumberOffset,
        FieldName.Timestamp => TimestampOffset,
        FieldName.PairedShares => PairedSharesOffset,
        FieldName.ImbalanceShares => ImbalanceSharesOffset,
        FieldName.ImbalanceDirection => ImbalanceDirectionOffset,
        FieldName.Stock => StockOffset,
        FieldName.FarPrice => FarPriceOffset,
        FieldName.NearPrice => NearPriceOffset,
        FieldName.CurrentReferencePrice => CurrentReferencePriceOffset,
        FieldName.CrossType => CrossTypeOffset,
        FieldName.PriceVariationIndicator => PriceVariationIndicatorOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.StockLocate => StockLocateLength,
        FieldName.TrackingNumber => TrackingNumberLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.PairedShares => PairedSharesLength,
        FieldName.ImbalanceShares => ImbalanceSharesLength,
        FieldName.ImbalanceDirection => ImbalanceDirectionLength,
        FieldName.Stock => StockLength,
        FieldName.FarPrice => FarPriceLength,
        FieldName.NearPrice => NearPriceLength,
        FieldName.CurrentReferencePrice => CurrentReferencePriceLength,
        FieldName.CrossType => CrossTypeLength,
        FieldName.PriceVariationIndicator => PriceVariationIndicatorLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is NetOrderImbalanceIndicatorMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as NetOrderImbalanceIndicatorMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.NetOrderImbalanceIndicatorMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
