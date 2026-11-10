using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class DirectListingWithCapitalRaisePriceDiscoveryMessage : ISequencedMessage, IPacketMessage, IFileMessage, IEquatable<DirectListingWithCapitalRaisePriceDiscoveryMessage>, IFieldOwner
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

    internal const int StockOffset = TimestampOffset + TimestampLength;

    internal const int StockLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Stock.
    /// </summary>
    public const byte StockFill = (byte)' ';

    internal const int OpenEligibilityStatusOffset = StockOffset + StockLength;

    internal const int OpenEligibilityStatusLength = 1;

    internal const int MinimumAllowablePriceOffset = OpenEligibilityStatusOffset + OpenEligibilityStatusLength;

    internal const int MinimumAllowablePriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Minimum Allowable Price.
    /// </summary>
    public const byte MinimumAllowablePriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="MinimumAllowablePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal MinimumAllowablePriceFactor = 10000m;

    internal const int MaximumAllowablePriceOffset = MinimumAllowablePriceOffset + MinimumAllowablePriceLength;

    internal const int MaximumAllowablePriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Maximum Allowable Price.
    /// </summary>
    public const byte MaximumAllowablePriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="MaximumAllowablePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal MaximumAllowablePriceFactor = 10000m;

    internal const int NearExecutionPriceOffset = MaximumAllowablePriceOffset + MaximumAllowablePriceLength;

    internal const int NearExecutionPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Near Execution Price.
    /// </summary>
    public const byte NearExecutionPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="NearExecutionPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal NearExecutionPriceFactor = 10000m;

    internal const int NearExecutionTimeOffset = NearExecutionPriceOffset + NearExecutionPriceLength;

    internal const int NearExecutionTimeLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="NearExecutionTime"/>.
    /// </summary>
    public const long NearExecutionTimeNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of <see cref="NearExecutionTime"/> for a time of one full day.
    /// </summary>
    public const long NearExecutionTimeUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / NearExecutionTimeNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that <see cref="NearExecutionTime"/> can hold.
    /// </summary>
    public const ulong NearExecutionTimeWireMaximum = ulong.MaxValue;

    internal const int LowerPriceRangeCollarOffset = NearExecutionTimeOffset + NearExecutionTimeLength;

    internal const int LowerPriceRangeCollarLength = 4;

    /// <summary>
    ///  The number of decimal places in Lower Price Range Collar.
    /// </summary>
    public const byte LowerPriceRangeCollarScale = 4;

    /// <summary>
    ///  The factor by which <see cref="LowerPriceRangeCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LowerPriceRangeCollarFactor = 10000m;

    internal const int UpperPriceRangeCollarOffset = LowerPriceRangeCollarOffset + LowerPriceRangeCollarLength;

    internal const int UpperPriceRangeCollarLength = 4;

    /// <summary>
    ///  The number of decimal places in Upper Price Range Collar.
    /// </summary>
    public const byte UpperPriceRangeCollarScale = 4;

    /// <summary>
    ///  The factor by which <see cref="UpperPriceRangeCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal UpperPriceRangeCollarFactor = 10000m;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = StockLocateLength + TrackingNumberLength + TimestampLength + StockLength + OpenEligibilityStatusLength + MinimumAllowablePriceLength + MaximumAllowablePriceLength + NearExecutionPriceLength + NearExecutionTimeLength + LowerPriceRangeCollarLength + UpperPriceRangeCollarLength;

    internal ushort stockLocate;

    /// <summary>
    ///  Locate Code uniquely assigned to the security symbol for the day
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
    ///  Nasdaq internal tracking number
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
    ///  Nanoseconds since midnight
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

    internal OpenEligibilityStatus openEligibilityStatus;

    /// <summary>
    ///  Indicates if the security is eligible to be released for trading
    /// </summary>
    public OpenEligibilityStatus OpenEligibilityStatus
    {
        get => this.openEligibilityStatus;
        set
        {
            this.openEligibilityStatus = value;
            Touch();
        }
    }

    internal uint minimumAllowablePrice;

    /// <summary>
    ///  20% below Registration Statement Lower Price
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal MinimumAllowablePrice
    {
        get => FixedPoint.ToDecimal(this.minimumAllowablePrice, MinimumAllowablePriceScale);
        set
        {
            this.minimumAllowablePrice = FixedPoint.ToRaw<uint>(value, MinimumAllowablePriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="MinimumAllowablePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint MinimumAllowablePriceRaw
    {
        get => this.minimumAllowablePrice;
        set
        {
            this.minimumAllowablePrice = value;
            Touch();
        }
    }

    internal uint maximumAllowablePrice;

    /// <summary>
    ///  80% above Registration Statement Highest Price
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal MaximumAllowablePrice
    {
        get => FixedPoint.ToDecimal(this.maximumAllowablePrice, MaximumAllowablePriceScale);
        set
        {
            this.maximumAllowablePrice = FixedPoint.ToRaw<uint>(value, MaximumAllowablePriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="MaximumAllowablePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint MaximumAllowablePriceRaw
    {
        get => this.maximumAllowablePrice;
        set
        {
            this.maximumAllowablePrice = value;
            Touch();
        }
    }

    internal uint nearExecutionPrice;

    /// <summary>
    ///  The current reference price when the DLCR volatility test has successfully passed
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal NearExecutionPrice
    {
        get => FixedPoint.ToDecimal(this.nearExecutionPrice, NearExecutionPriceScale);
        set
        {
            this.nearExecutionPrice = FixedPoint.ToRaw<uint>(value, NearExecutionPriceFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="NearExecutionPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint NearExecutionPriceRaw
    {
        get => this.nearExecutionPrice;
        set
        {
            this.nearExecutionPrice = value;
            Touch();
        }
    }

    internal ulong nearExecutionTime;

    /// <summary>
    ///  The time at which the Near Execution Price was determined
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds a time outside one day. This property has a precision of 100 nanoseconds, and <see cref="NearExecutionTimeRaw"/> holds the exact value. A time is rounded to the wire unit of the field, with a midpoint away from zero. A time past the largest wire value is set to that value.
    /// </remarks>
    public TimeOnly? NearExecutionTime
    {
        get => Midnight.ToTimeOnly(this.nearExecutionTime, NearExecutionTimeNanosecondsPerUnit, NearExecutionTimeUnitsPerDay);
        set
        {
            this.nearExecutionTime = value is { } present ? Midnight.ToRaw<ulong>(present, NearExecutionTimeNanosecondsPerUnit, NearExecutionTimeWireMaximum) : default;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="NearExecutionTime"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong NearExecutionTimeRaw
    {
        get => this.nearExecutionTime;
        set
        {
            this.nearExecutionTime = value;
            Touch();
        }
    }

    internal uint lowerPriceRangeCollar;

    /// <summary>
    ///  Indicates the price of the Lower Auction Collar Threshold
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal LowerPriceRangeCollar
    {
        get => FixedPoint.ToDecimal(this.lowerPriceRangeCollar, LowerPriceRangeCollarScale);
        set
        {
            this.lowerPriceRangeCollar = FixedPoint.ToRaw<uint>(value, LowerPriceRangeCollarFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="LowerPriceRangeCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint LowerPriceRangeCollarRaw
    {
        get => this.lowerPriceRangeCollar;
        set
        {
            this.lowerPriceRangeCollar = value;
            Touch();
        }
    }

    internal uint upperPriceRangeCollar;

    /// <summary>
    ///  Indicates the price of the Upper Auction Collar Threshold
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal UpperPriceRangeCollar
    {
        get => FixedPoint.ToDecimal(this.upperPriceRangeCollar, UpperPriceRangeCollarScale);
        set
        {
            this.upperPriceRangeCollar = FixedPoint.ToRaw<uint>(value, UpperPriceRangeCollarFactor, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="UpperPriceRangeCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint UpperPriceRangeCollarRaw
    {
        get => this.upperPriceRangeCollar;
        set
        {
            this.upperPriceRangeCollar = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public MessageCode DataValue => MessageCode.DirectListingWithCapitalRaisePriceDiscoveryMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.openEligibilityStatus);

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

    IMessage IMessage.Copy() => new DirectListingWithCapitalRaisePriceDiscoveryMessage(this);

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
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        StockLocate,
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        TrackingNumber,
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        Stock,
        /// <summary>
        ///  Indicates if the security is eligible to be released for trading
        /// </summary>
        OpenEligibilityStatus,
        /// <summary>
        ///  20% below Registration Statement Lower Price
        /// </summary>
        MinimumAllowablePrice,
        /// <summary>
        ///  80% above Registration Statement Highest Price
        /// </summary>
        MaximumAllowablePrice,
        /// <summary>
        ///  The current reference price when the DLCR volatility test has successfully passed
        /// </summary>
        NearExecutionPrice,
        /// <summary>
        ///  The time at which the Near Execution Price was determined
        /// </summary>
        NearExecutionTime,
        /// <summary>
        ///  Indicates the price of the Lower Auction Collar Threshold
        /// </summary>
        LowerPriceRangeCollar,
        /// <summary>
        ///  Indicates the price of the Upper Auction Collar Threshold
        /// </summary>
        UpperPriceRangeCollar,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly DirectListingWithCapitalRaisePriceDiscoveryMessage owner;

        internal FieldSet(DirectListingWithCapitalRaisePriceDiscoveryMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public FieldView StockLocate => this[FieldName.StockLocate];

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public FieldView TrackingNumber => this[FieldName.TrackingNumber];

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public FieldView Stock => this[FieldName.Stock];

        /// <summary>
        ///  Indicates if the security is eligible to be released for trading
        /// </summary>
        public FieldView OpenEligibilityStatus => this[FieldName.OpenEligibilityStatus];

        /// <summary>
        ///  20% below Registration Statement Lower Price
        /// </summary>
        public FieldView MinimumAllowablePrice => this[FieldName.MinimumAllowablePrice];

        /// <summary>
        ///  80% above Registration Statement Highest Price
        /// </summary>
        public FieldView MaximumAllowablePrice => this[FieldName.MaximumAllowablePrice];

        /// <summary>
        ///  The current reference price when the DLCR volatility test has successfully passed
        /// </summary>
        public FieldView NearExecutionPrice => this[FieldName.NearExecutionPrice];

        /// <summary>
        ///  The time at which the Near Execution Price was determined
        /// </summary>
        public FieldView NearExecutionTime => this[FieldName.NearExecutionTime];

        /// <summary>
        ///  Indicates the price of the Lower Auction Collar Threshold
        /// </summary>
        public FieldView LowerPriceRangeCollar => this[FieldName.LowerPriceRangeCollar];

        /// <summary>
        ///  Indicates the price of the Upper Auction Collar Threshold
        /// </summary>
        public FieldView UpperPriceRangeCollar => this[FieldName.UpperPriceRangeCollar];

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
        new(nameof(FieldName.DataValue), FieldKind.Char, DataValueLength),
        new(nameof(FieldName.StockLocate), FieldKind.UnsignedInteger, StockLocateLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TrackingNumber), FieldKind.UnsignedInteger, TrackingNumberLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Timestamp), FieldKind.UnsignedInteger, TimestampLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Stock), FieldKind.String, StockLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.OpenEligibilityStatus), FieldKind.Enum, OpenEligibilityStatusLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.MinimumAllowablePrice), FieldKind.Decimal, MinimumAllowablePriceLength) { Scale = MinimumAllowablePriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.MaximumAllowablePrice), FieldKind.Decimal, MaximumAllowablePriceLength) { Scale = MaximumAllowablePriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.NearExecutionPrice), FieldKind.Decimal, NearExecutionPriceLength) { Scale = NearExecutionPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.NearExecutionTime), FieldKind.UnsignedInteger, NearExecutionTimeLength) { Unit = "Nanoseconds", Epoch = "Midnight", Timezone = "America/New_York", ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.LowerPriceRangeCollar), FieldKind.Decimal, LowerPriceRangeCollarLength) { Scale = LowerPriceRangeCollarScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.UpperPriceRangeCollar), FieldKind.Decimal, UpperPriceRangeCollarLength) { Scale = UpperPriceRangeCollarScale, ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="DirectListingWithCapitalRaisePriceDiscoveryMessage"/> class with default values.
    /// </summary>
    public DirectListingWithCapitalRaisePriceDiscoveryMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="DirectListingWithCapitalRaisePriceDiscoveryMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public DirectListingWithCapitalRaisePriceDiscoveryMessage(DirectListingWithCapitalRaisePriceDiscoveryMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(DirectListingWithCapitalRaisePriceDiscoveryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.stockLocate = other.stockLocate;
        this.trackingNumber = other.trackingNumber;
        this.timestamp = other.timestamp;
        this.stock = other.stock;
        this.openEligibilityStatus = other.openEligibilityStatus;
        this.minimumAllowablePrice = other.minimumAllowablePrice;
        this.maximumAllowablePrice = other.maximumAllowablePrice;
        this.nearExecutionPrice = other.nearExecutionPrice;
        this.nearExecutionTime = other.nearExecutionTime;
        this.lowerPriceRangeCollar = other.lowerPriceRangeCollar;
        this.upperPriceRangeCollar = other.upperPriceRangeCollar;
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
        this.stock = string.Empty;
        this.openEligibilityStatus = default;
        this.minimumAllowablePrice = default;
        this.maximumAllowablePrice = default;
        this.nearExecutionPrice = default;
        this.nearExecutionTime = default;
        this.lowerPriceRangeCollar = default;
        this.upperPriceRangeCollar = default;
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
    public static DirectListingWithCapitalRaisePriceDiscoveryMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new DirectListingWithCapitalRaisePriceDiscoveryMessage();
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
    public static DirectListingWithCapitalRaisePriceDiscoveryMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new DirectListingWithCapitalRaisePriceDiscoveryMessage();
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
        if (Truncated(data, start, cursor, StockLength, FieldName.Stock)) return;
        this.stock = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StockLength), StockFill);
        cursor += StockLength;
        if (Truncated(data, start, cursor, OpenEligibilityStatusLength, FieldName.OpenEligibilityStatus)) return;
        this.openEligibilityStatus = (OpenEligibilityStatus)data.Slice(cursor, OpenEligibilityStatusLength)[0];
        cursor += OpenEligibilityStatusLength;
        if (Truncated(data, start, cursor, MinimumAllowablePriceLength, FieldName.MinimumAllowablePrice)) return;
        this.minimumAllowablePrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, MinimumAllowablePriceLength));
        cursor += MinimumAllowablePriceLength;
        if (Truncated(data, start, cursor, MaximumAllowablePriceLength, FieldName.MaximumAllowablePrice)) return;
        this.maximumAllowablePrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, MaximumAllowablePriceLength));
        cursor += MaximumAllowablePriceLength;
        if (Truncated(data, start, cursor, NearExecutionPriceLength, FieldName.NearExecutionPrice)) return;
        this.nearExecutionPrice = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, NearExecutionPriceLength));
        cursor += NearExecutionPriceLength;
        if (Truncated(data, start, cursor, NearExecutionTimeLength, FieldName.NearExecutionTime)) return;
        this.nearExecutionTime = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, NearExecutionTimeLength));
        cursor += NearExecutionTimeLength;
        if (Truncated(data, start, cursor, LowerPriceRangeCollarLength, FieldName.LowerPriceRangeCollar)) return;
        this.lowerPriceRangeCollar = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, LowerPriceRangeCollarLength));
        cursor += LowerPriceRangeCollarLength;
        if (Truncated(data, start, cursor, UpperPriceRangeCollarLength, FieldName.UpperPriceRangeCollar)) return;
        this.upperPriceRangeCollar = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, UpperPriceRangeCollarLength));
        cursor += UpperPriceRangeCollarLength;
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
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StockLength), TextEncoding.Ascii.Fit(this.stock, StockLength), StockFill);
        cursor += StockLength;
        data.Slice(cursor, OpenEligibilityStatusLength)[0] = unchecked((byte)(this.openEligibilityStatus));
        cursor += OpenEligibilityStatusLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, MinimumAllowablePriceLength), this.minimumAllowablePrice);
        cursor += MinimumAllowablePriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, MaximumAllowablePriceLength), this.maximumAllowablePrice);
        cursor += MaximumAllowablePriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, NearExecutionPriceLength), this.nearExecutionPrice);
        cursor += NearExecutionPriceLength;
        BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, NearExecutionTimeLength), this.nearExecutionTime);
        cursor += NearExecutionTimeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, LowerPriceRangeCollarLength), this.lowerPriceRangeCollar);
        cursor += LowerPriceRangeCollarLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, UpperPriceRangeCollarLength), this.upperPriceRangeCollar);
        cursor += UpperPriceRangeCollarLength;
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
        if (!global::System.Text.Ascii.IsValid(this.stock))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Stock), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.openEligibilityStatus))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.OpenEligibilityStatus), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(DirectListingWithCapitalRaisePriceDiscoveryMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.stockLocate != other.stockLocate) return false;
        if (this.trackingNumber != other.trackingNumber) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.stock != other.stock) return false;
        if (this.openEligibilityStatus != other.openEligibilityStatus) return false;
        if (this.minimumAllowablePrice != other.minimumAllowablePrice) return false;
        if (this.maximumAllowablePrice != other.maximumAllowablePrice) return false;
        if (this.nearExecutionPrice != other.nearExecutionPrice) return false;
        if (this.nearExecutionTime != other.nearExecutionTime) return false;
        if (this.lowerPriceRangeCollar != other.lowerPriceRangeCollar) return false;
        if (this.upperPriceRangeCollar != other.upperPriceRangeCollar) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is DirectListingWithCapitalRaisePriceDiscoveryMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.stockLocate);
        hash.Add(this.trackingNumber);
        hash.Add(this.timestamp);
        hash.Add(this.stock);
        hash.Add(this.openEligibilityStatus);
        hash.Add(this.minimumAllowablePrice);
        hash.Add(this.maximumAllowablePrice);
        hash.Add(this.nearExecutionPrice);
        hash.Add(this.nearExecutionTime);
        hash.Add(this.lowerPriceRangeCollar);
        hash.Add(this.upperPriceRangeCollar);
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
            if (Has(FieldName.StockLocate)) names.Add(nameof(FieldName.StockLocate));
            if (Has(FieldName.TrackingNumber)) names.Add(nameof(FieldName.TrackingNumber));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.Stock)) names.Add(nameof(FieldName.Stock));
            if (Has(FieldName.OpenEligibilityStatus)) names.Add(nameof(FieldName.OpenEligibilityStatus));
            if (Has(FieldName.MinimumAllowablePrice)) names.Add(nameof(FieldName.MinimumAllowablePrice));
            if (Has(FieldName.MaximumAllowablePrice)) names.Add(nameof(FieldName.MaximumAllowablePrice));
            if (Has(FieldName.NearExecutionPrice)) names.Add(nameof(FieldName.NearExecutionPrice));
            if (Has(FieldName.NearExecutionTime)) names.Add(nameof(FieldName.NearExecutionTime));
            if (Has(FieldName.LowerPriceRangeCollar)) names.Add(nameof(FieldName.LowerPriceRangeCollar));
            if (Has(FieldName.UpperPriceRangeCollar)) names.Add(nameof(FieldName.UpperPriceRangeCollar));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(DirectListingWithCapitalRaisePriceDiscoveryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.stockLocate != other.stockLocate) diff = diff.With(FieldName.StockLocate);
        if (this.trackingNumber != other.trackingNumber) diff = diff.With(FieldName.TrackingNumber);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.stock != other.stock) diff = diff.With(FieldName.Stock);
        if (this.openEligibilityStatus != other.openEligibilityStatus) diff = diff.With(FieldName.OpenEligibilityStatus);
        if (this.minimumAllowablePrice != other.minimumAllowablePrice) diff = diff.With(FieldName.MinimumAllowablePrice);
        if (this.maximumAllowablePrice != other.maximumAllowablePrice) diff = diff.With(FieldName.MaximumAllowablePrice);
        if (this.nearExecutionPrice != other.nearExecutionPrice) diff = diff.With(FieldName.NearExecutionPrice);
        if (this.nearExecutionTime != other.nearExecutionTime) diff = diff.With(FieldName.NearExecutionTime);
        if (this.lowerPriceRangeCollar != other.lowerPriceRangeCollar) diff = diff.With(FieldName.LowerPriceRangeCollar);
        if (this.upperPriceRangeCollar != other.upperPriceRangeCollar) diff = diff.With(FieldName.UpperPriceRangeCollar);
        return diff;
    }

    private void CopyField(FieldName field, DirectListingWithCapitalRaisePriceDiscoveryMessage from)
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
            case FieldName.Stock:
                this.stock = from.stock;
                break;
            case FieldName.OpenEligibilityStatus:
                this.openEligibilityStatus = from.openEligibilityStatus;
                break;
            case FieldName.MinimumAllowablePrice:
                this.minimumAllowablePrice = from.minimumAllowablePrice;
                break;
            case FieldName.MaximumAllowablePrice:
                this.maximumAllowablePrice = from.maximumAllowablePrice;
                break;
            case FieldName.NearExecutionPrice:
                this.nearExecutionPrice = from.nearExecutionPrice;
                break;
            case FieldName.NearExecutionTime:
                this.nearExecutionTime = from.nearExecutionTime;
                break;
            case FieldName.LowerPriceRangeCollar:
                this.lowerPriceRangeCollar = from.lowerPriceRangeCollar;
                break;
            case FieldName.UpperPriceRangeCollar:
                this.upperPriceRangeCollar = from.upperPriceRangeCollar;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"DirectListingWithCapitalRaisePriceDiscoveryMessage {{ StockLocate: {(Text.Of(this.stockLocate))}, TrackingNumber: {(Text.Of(this.trackingNumber))}, Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, Stock: {(Text.Of(this.stock))}, OpenEligibilityStatus: {(Text.Enum(this.openEligibilityStatus))}, MinimumAllowablePrice: {(Text.Of(FixedPoint.ToDecimal(this.minimumAllowablePrice, MinimumAllowablePriceScale)))}, MaximumAllowablePrice: {(Text.Of(FixedPoint.ToDecimal(this.maximumAllowablePrice, MaximumAllowablePriceScale)))}, NearExecutionPrice: {(Text.Of(FixedPoint.ToDecimal(this.nearExecutionPrice, NearExecutionPriceScale)))}, NearExecutionTime: {(Text.Midnight(this.nearExecutionTime, NearExecutionTimeNanosecondsPerUnit))}, LowerPriceRangeCollar: {(Text.Of(FixedPoint.ToDecimal(this.lowerPriceRangeCollar, LowerPriceRangeCollarScale)))}, UpperPriceRangeCollar: {(Text.Of(FixedPoint.ToDecimal(this.upperPriceRangeCollar, UpperPriceRangeCollarScale)))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

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
        FieldName.DataValue => Text.Enum(MessageCode.DirectListingWithCapitalRaisePriceDiscoveryMessage),
        FieldName.StockLocate => Text.Of(this.stockLocate),
        FieldName.TrackingNumber => Text.Of(this.trackingNumber),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Stock => Text.Of(this.stock),
        FieldName.OpenEligibilityStatus => Text.Enum(this.openEligibilityStatus),
        FieldName.MinimumAllowablePrice => Text.Of(FixedPoint.ToDecimal(this.minimumAllowablePrice, MinimumAllowablePriceScale)),
        FieldName.MaximumAllowablePrice => Text.Of(FixedPoint.ToDecimal(this.maximumAllowablePrice, MaximumAllowablePriceScale)),
        FieldName.NearExecutionPrice => Text.Of(FixedPoint.ToDecimal(this.nearExecutionPrice, NearExecutionPriceScale)),
        FieldName.NearExecutionTime => Text.Midnight(this.nearExecutionTime, NearExecutionTimeNanosecondsPerUnit),
        FieldName.LowerPriceRangeCollar => Text.Of(FixedPoint.ToDecimal(this.lowerPriceRangeCollar, LowerPriceRangeCollarScale)),
        FieldName.UpperPriceRangeCollar => Text.Of(FixedPoint.ToDecimal(this.upperPriceRangeCollar, UpperPriceRangeCollarScale)),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.OpenEligibilityStatus => global::System.Enum.IsDefined(this.openEligibilityStatus),
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
        FieldName.Stock => StockOffset,
        FieldName.OpenEligibilityStatus => OpenEligibilityStatusOffset,
        FieldName.MinimumAllowablePrice => MinimumAllowablePriceOffset,
        FieldName.MaximumAllowablePrice => MaximumAllowablePriceOffset,
        FieldName.NearExecutionPrice => NearExecutionPriceOffset,
        FieldName.NearExecutionTime => NearExecutionTimeOffset,
        FieldName.LowerPriceRangeCollar => LowerPriceRangeCollarOffset,
        FieldName.UpperPriceRangeCollar => UpperPriceRangeCollarOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.StockLocate => StockLocateLength,
        FieldName.TrackingNumber => TrackingNumberLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Stock => StockLength,
        FieldName.OpenEligibilityStatus => OpenEligibilityStatusLength,
        FieldName.MinimumAllowablePrice => MinimumAllowablePriceLength,
        FieldName.MaximumAllowablePrice => MaximumAllowablePriceLength,
        FieldName.NearExecutionPrice => NearExecutionPriceLength,
        FieldName.NearExecutionTime => NearExecutionTimeLength,
        FieldName.LowerPriceRangeCollar => LowerPriceRangeCollarLength,
        FieldName.UpperPriceRangeCollar => UpperPriceRangeCollarLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is DirectListingWithCapitalRaisePriceDiscoveryMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as DirectListingWithCapitalRaisePriceDiscoveryMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(MessageCode.DirectListingWithCapitalRaisePriceDiscoveryMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
