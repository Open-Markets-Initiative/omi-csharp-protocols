using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class StockDirectoryMessage : ISequencedMessage, IPacketMessage, IFileMessage, IEquatable<StockDirectoryMessage>, IFieldOwner
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

    internal const int MarketCategoryOffset = StockOffset + StockLength;

    internal const int MarketCategoryLength = 1;

    internal const int FinancialStatusIndicatorOffset = MarketCategoryOffset + MarketCategoryLength;

    internal const int FinancialStatusIndicatorLength = 1;

    internal const int RoundLotSizeOffset = FinancialStatusIndicatorOffset + FinancialStatusIndicatorLength;

    internal const int RoundLotSizeLength = 4;

    internal const int RoundLotsOnlyOffset = RoundLotSizeOffset + RoundLotSizeLength;

    internal const int RoundLotsOnlyLength = 1;

    internal const int IssueClassificationOffset = RoundLotsOnlyOffset + RoundLotsOnlyLength;

    internal const int IssueClassificationLength = 1;

    internal const int IssueSubTypeOffset = IssueClassificationOffset + IssueClassificationLength;

    internal const int IssueSubTypeLength = 2;

    /// <summary>
    ///  The byte that fills the unused bytes of Issue Sub Type.
    /// </summary>
    public const byte IssueSubTypeFill = (byte)' ';

    internal const int AuthenticityOffset = IssueSubTypeOffset + IssueSubTypeLength;

    internal const int AuthenticityLength = 1;

    internal const int ShortSaleThresholdIndicatorOffset = AuthenticityOffset + AuthenticityLength;

    internal const int ShortSaleThresholdIndicatorLength = 1;

    internal const int IpoFlagOffset = ShortSaleThresholdIndicatorOffset + ShortSaleThresholdIndicatorLength;

    internal const int IpoFlagLength = 1;

    internal const int LuldReferencePriceTierOffset = IpoFlagOffset + IpoFlagLength;

    internal const int LuldReferencePriceTierLength = 1;

    internal const int EtpFlagOffset = LuldReferencePriceTierOffset + LuldReferencePriceTierLength;

    internal const int EtpFlagLength = 1;

    internal const int EtpLeverageFactorOffset = EtpFlagOffset + EtpFlagLength;

    internal const int EtpLeverageFactorLength = 4;

    internal const int InverseIndicatorOffset = EtpLeverageFactorOffset + EtpLeverageFactorLength;

    internal const int InverseIndicatorLength = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = StockLocateLength + TrackingNumberLength + TimestampLength + StockLength + MarketCategoryLength + FinancialStatusIndicatorLength + RoundLotSizeLength + RoundLotsOnlyLength + IssueClassificationLength + IssueSubTypeLength + AuthenticityLength + ShortSaleThresholdIndicatorLength + IpoFlagLength + LuldReferencePriceTierLength + EtpFlagLength + EtpLeverageFactorLength + InverseIndicatorLength;

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

    internal MarketCategory marketCategory;

    /// <summary>
    ///  Indicates listing market or listing market tier for the issue
    /// </summary>
    public MarketCategory MarketCategory
    {
        get => this.marketCategory;
        set
        {
            this.marketCategory = value;
            Touch();
        }
    }

    internal FinancialStatusIndicator financialStatusIndicator;

    /// <summary>
    ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
    /// </summary>
    public FinancialStatusIndicator FinancialStatusIndicator
    {
        get => this.financialStatusIndicator;
        set
        {
            this.financialStatusIndicator = value;
            Touch();
        }
    }

    internal uint roundLotSize;

    /// <summary>
    ///  Denotes the number of shares that represent a round lot for the issue
    /// </summary>
    public uint RoundLotSize
    {
        get => this.roundLotSize;
        set
        {
            this.roundLotSize = value;
            Touch();
        }
    }

    internal RoundLotsOnly roundLotsOnly;

    /// <summary>
    ///  Indicates if Nasdaq system limits order entry for issue
    /// </summary>
    public RoundLotsOnly RoundLotsOnly
    {
        get => this.roundLotsOnly;
        set
        {
            this.roundLotsOnly = value;
            Touch();
        }
    }

    internal IssueClassification issueClassification;

    /// <summary>
    ///  Identifies the security class for the issue as assigned by NASDAQ
    /// </summary>
    public IssueClassification IssueClassification
    {
        get => this.issueClassification;
        set
        {
            this.issueClassification = value;
            Touch();
        }
    }

    internal string issueSubType = string.Empty;

    /// <summary>
    ///  Identifies the security sub-type for the issue as assigned by NASDAQ
    /// </summary>
    /// <remarks>
    ///  The field is 2 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string IssueSubType
    {
        get => this.issueSubType;
        set
        {
            this.issueSubType = value ?? string.Empty;
            Touch();
        }
    }

    internal Authenticity authenticity;

    /// <summary>
    ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
    /// </summary>
    public Authenticity Authenticity
    {
        get => this.authenticity;
        set
        {
            this.authenticity = value;
            Touch();
        }
    }

    internal ShortSaleThresholdIndicator shortSaleThresholdIndicator;

    /// <summary>
    ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
    /// </summary>
    public ShortSaleThresholdIndicator ShortSaleThresholdIndicator
    {
        get => this.shortSaleThresholdIndicator;
        set
        {
            this.shortSaleThresholdIndicator = value;
            Touch();
        }
    }

    internal IpoFlag ipoFlag;

    /// <summary>
    ///  Indicates if the NASDAQ security is set up for IPO release
    /// </summary>
    public IpoFlag IpoFlag
    {
        get => this.ipoFlag;
        set
        {
            this.ipoFlag = value;
            Touch();
        }
    }

    internal LuldReferencePriceTier luldReferencePriceTier;

    /// <summary>
    ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument
    /// </summary>
    public LuldReferencePriceTier LuldReferencePriceTier
    {
        get => this.luldReferencePriceTier;
        set
        {
            this.luldReferencePriceTier = value;
            Touch();
        }
    }

    internal EtpFlag etpFlag;

    /// <summary>
    ///  Indicates whether the security is an exchange traded product
    /// </summary>
    public EtpFlag EtpFlag
    {
        get => this.etpFlag;
        set
        {
            this.etpFlag = value;
            Touch();
        }
    }

    internal uint etpLeverageFactor;

    /// <summary>
    ///  Tracks the integral relationship of the ETP to the underlying index
    /// </summary>
    public uint EtpLeverageFactor
    {
        get => this.etpLeverageFactor;
        set
        {
            this.etpLeverageFactor = value;
            Touch();
        }
    }

    internal InverseIndicator inverseIndicator;

    /// <summary>
    ///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of 'Y' indicates the ETP will decrease by a value of 3.
    /// </summary>
    public InverseIndicator InverseIndicator
    {
        get => this.inverseIndicator;
        set
        {
            this.inverseIndicator = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public MessageCode DataValue => MessageCode.StockDirectoryMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.marketCategory) && global::System.Enum.IsDefined(this.financialStatusIndicator) && global::System.Enum.IsDefined(this.roundLotsOnly) && global::System.Enum.IsDefined(this.issueClassification) && global::System.Enum.IsDefined(this.authenticity) && global::System.Enum.IsDefined(this.shortSaleThresholdIndicator) && global::System.Enum.IsDefined(this.ipoFlag) && global::System.Enum.IsDefined(this.luldReferencePriceTier) && global::System.Enum.IsDefined(this.etpFlag) && global::System.Enum.IsDefined(this.inverseIndicator);

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

    IMessage IMessage.Copy() => new StockDirectoryMessage(this);

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
        ///  Indicates listing market or listing market tier for the issue
        /// </summary>
        MarketCategory,
        /// <summary>
        ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
        /// </summary>
        FinancialStatusIndicator,
        /// <summary>
        ///  Denotes the number of shares that represent a round lot for the issue
        /// </summary>
        RoundLotSize,
        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        RoundLotsOnly,
        /// <summary>
        ///  Identifies the security class for the issue as assigned by NASDAQ
        /// </summary>
        IssueClassification,
        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by NASDAQ
        /// </summary>
        IssueSubType,
        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
        /// </summary>
        Authenticity,
        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        ShortSaleThresholdIndicator,
        /// <summary>
        ///  Indicates if the NASDAQ security is set up for IPO release
        /// </summary>
        IpoFlag,
        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument
        /// </summary>
        LuldReferencePriceTier,
        /// <summary>
        ///  Indicates whether the security is an exchange traded product
        /// </summary>
        EtpFlag,
        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index
        /// </summary>
        EtpLeverageFactor,
        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of 'Y' indicates the ETP will decrease by a value of 3.
        /// </summary>
        InverseIndicator,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly StockDirectoryMessage owner;

        internal FieldSet(StockDirectoryMessage owner)
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
        ///  Indicates listing market or listing market tier for the issue
        /// </summary>
        public FieldView MarketCategory => this[FieldName.MarketCategory];

        /// <summary>
        ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
        /// </summary>
        public FieldView FinancialStatusIndicator => this[FieldName.FinancialStatusIndicator];

        /// <summary>
        ///  Denotes the number of shares that represent a round lot for the issue
        /// </summary>
        public FieldView RoundLotSize => this[FieldName.RoundLotSize];

        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        public FieldView RoundLotsOnly => this[FieldName.RoundLotsOnly];

        /// <summary>
        ///  Identifies the security class for the issue as assigned by NASDAQ
        /// </summary>
        public FieldView IssueClassification => this[FieldName.IssueClassification];

        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by NASDAQ
        /// </summary>
        public FieldView IssueSubType => this[FieldName.IssueSubType];

        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
        /// </summary>
        public FieldView Authenticity => this[FieldName.Authenticity];

        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        public FieldView ShortSaleThresholdIndicator => this[FieldName.ShortSaleThresholdIndicator];

        /// <summary>
        ///  Indicates if the NASDAQ security is set up for IPO release
        /// </summary>
        public FieldView IpoFlag => this[FieldName.IpoFlag];

        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument
        /// </summary>
        public FieldView LuldReferencePriceTier => this[FieldName.LuldReferencePriceTier];

        /// <summary>
        ///  Indicates whether the security is an exchange traded product
        /// </summary>
        public FieldView EtpFlag => this[FieldName.EtpFlag];

        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index
        /// </summary>
        public FieldView EtpLeverageFactor => this[FieldName.EtpLeverageFactor];

        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of 'Y' indicates the ETP will decrease by a value of 3.
        /// </summary>
        public FieldView InverseIndicator => this[FieldName.InverseIndicator];

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
        new(nameof(FieldName.Stock), FieldKind.String, StockLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.MarketCategory), FieldKind.Enum, MarketCategoryLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.FinancialStatusIndicator), FieldKind.Enum, FinancialStatusIndicatorLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.RoundLotSize), FieldKind.UnsignedInteger, RoundLotSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.RoundLotsOnly), FieldKind.Enum, RoundLotsOnlyLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.IssueClassification), FieldKind.Enum, IssueClassificationLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.IssueSubType), FieldKind.String, IssueSubTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.Authenticity), FieldKind.Enum, AuthenticityLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.ShortSaleThresholdIndicator), FieldKind.Enum, ShortSaleThresholdIndicatorLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.IpoFlag), FieldKind.Enum, IpoFlagLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.LuldReferencePriceTier), FieldKind.Enum, LuldReferencePriceTierLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.EtpFlag), FieldKind.Enum, EtpFlagLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.EtpLeverageFactor), FieldKind.UnsignedInteger, EtpLeverageFactorLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.InverseIndicator), FieldKind.Enum, InverseIndicatorLength) { Fill = "Spaces", Justification = "Left" },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="StockDirectoryMessage"/> class with default values.
    /// </summary>
    public StockDirectoryMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="StockDirectoryMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public StockDirectoryMessage(StockDirectoryMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(StockDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.stockLocate = other.stockLocate;
        this.trackingNumber = other.trackingNumber;
        this.timestamp = other.timestamp;
        this.stock = other.stock;
        this.marketCategory = other.marketCategory;
        this.financialStatusIndicator = other.financialStatusIndicator;
        this.roundLotSize = other.roundLotSize;
        this.roundLotsOnly = other.roundLotsOnly;
        this.issueClassification = other.issueClassification;
        this.issueSubType = other.issueSubType;
        this.authenticity = other.authenticity;
        this.shortSaleThresholdIndicator = other.shortSaleThresholdIndicator;
        this.ipoFlag = other.ipoFlag;
        this.luldReferencePriceTier = other.luldReferencePriceTier;
        this.etpFlag = other.etpFlag;
        this.etpLeverageFactor = other.etpLeverageFactor;
        this.inverseIndicator = other.inverseIndicator;
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
        this.marketCategory = default;
        this.financialStatusIndicator = default;
        this.roundLotSize = default;
        this.roundLotsOnly = default;
        this.issueClassification = default;
        this.issueSubType = string.Empty;
        this.authenticity = default;
        this.shortSaleThresholdIndicator = default;
        this.ipoFlag = default;
        this.luldReferencePriceTier = default;
        this.etpFlag = default;
        this.etpLeverageFactor = default;
        this.inverseIndicator = default;
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
    public static StockDirectoryMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new StockDirectoryMessage();
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
    public static StockDirectoryMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new StockDirectoryMessage();
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
        if (Truncated(data, start, cursor, MarketCategoryLength, FieldName.MarketCategory)) return;
        this.marketCategory = (MarketCategory)data.Slice(cursor, MarketCategoryLength)[0];
        cursor += MarketCategoryLength;
        if (Truncated(data, start, cursor, FinancialStatusIndicatorLength, FieldName.FinancialStatusIndicator)) return;
        this.financialStatusIndicator = (FinancialStatusIndicator)data.Slice(cursor, FinancialStatusIndicatorLength)[0];
        cursor += FinancialStatusIndicatorLength;
        if (Truncated(data, start, cursor, RoundLotSizeLength, FieldName.RoundLotSize)) return;
        this.roundLotSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, RoundLotSizeLength));
        cursor += RoundLotSizeLength;
        if (Truncated(data, start, cursor, RoundLotsOnlyLength, FieldName.RoundLotsOnly)) return;
        this.roundLotsOnly = (RoundLotsOnly)data.Slice(cursor, RoundLotsOnlyLength)[0];
        cursor += RoundLotsOnlyLength;
        if (Truncated(data, start, cursor, IssueClassificationLength, FieldName.IssueClassification)) return;
        this.issueClassification = (IssueClassification)data.Slice(cursor, IssueClassificationLength)[0];
        cursor += IssueClassificationLength;
        if (Truncated(data, start, cursor, IssueSubTypeLength, FieldName.IssueSubType)) return;
        this.issueSubType = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, IssueSubTypeLength), IssueSubTypeFill);
        cursor += IssueSubTypeLength;
        if (Truncated(data, start, cursor, AuthenticityLength, FieldName.Authenticity)) return;
        this.authenticity = (Authenticity)data.Slice(cursor, AuthenticityLength)[0];
        cursor += AuthenticityLength;
        if (Truncated(data, start, cursor, ShortSaleThresholdIndicatorLength, FieldName.ShortSaleThresholdIndicator)) return;
        this.shortSaleThresholdIndicator = (ShortSaleThresholdIndicator)data.Slice(cursor, ShortSaleThresholdIndicatorLength)[0];
        cursor += ShortSaleThresholdIndicatorLength;
        if (Truncated(data, start, cursor, IpoFlagLength, FieldName.IpoFlag)) return;
        this.ipoFlag = (IpoFlag)data.Slice(cursor, IpoFlagLength)[0];
        cursor += IpoFlagLength;
        if (Truncated(data, start, cursor, LuldReferencePriceTierLength, FieldName.LuldReferencePriceTier)) return;
        this.luldReferencePriceTier = (LuldReferencePriceTier)data.Slice(cursor, LuldReferencePriceTierLength)[0];
        cursor += LuldReferencePriceTierLength;
        if (Truncated(data, start, cursor, EtpFlagLength, FieldName.EtpFlag)) return;
        this.etpFlag = (EtpFlag)data.Slice(cursor, EtpFlagLength)[0];
        cursor += EtpFlagLength;
        if (Truncated(data, start, cursor, EtpLeverageFactorLength, FieldName.EtpLeverageFactor)) return;
        this.etpLeverageFactor = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, EtpLeverageFactorLength));
        cursor += EtpLeverageFactorLength;
        if (Truncated(data, start, cursor, InverseIndicatorLength, FieldName.InverseIndicator)) return;
        this.inverseIndicator = (InverseIndicator)data.Slice(cursor, InverseIndicatorLength)[0];
        cursor += InverseIndicatorLength;
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
        data.Slice(cursor, MarketCategoryLength)[0] = unchecked((byte)(this.marketCategory));
        cursor += MarketCategoryLength;
        data.Slice(cursor, FinancialStatusIndicatorLength)[0] = unchecked((byte)(this.financialStatusIndicator));
        cursor += FinancialStatusIndicatorLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, RoundLotSizeLength), this.roundLotSize);
        cursor += RoundLotSizeLength;
        data.Slice(cursor, RoundLotsOnlyLength)[0] = unchecked((byte)(this.roundLotsOnly));
        cursor += RoundLotsOnlyLength;
        data.Slice(cursor, IssueClassificationLength)[0] = unchecked((byte)(this.issueClassification));
        cursor += IssueClassificationLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, IssueSubTypeLength), TextEncoding.Ascii.Fit(this.issueSubType, IssueSubTypeLength), IssueSubTypeFill);
        cursor += IssueSubTypeLength;
        data.Slice(cursor, AuthenticityLength)[0] = unchecked((byte)(this.authenticity));
        cursor += AuthenticityLength;
        data.Slice(cursor, ShortSaleThresholdIndicatorLength)[0] = unchecked((byte)(this.shortSaleThresholdIndicator));
        cursor += ShortSaleThresholdIndicatorLength;
        data.Slice(cursor, IpoFlagLength)[0] = unchecked((byte)(this.ipoFlag));
        cursor += IpoFlagLength;
        data.Slice(cursor, LuldReferencePriceTierLength)[0] = unchecked((byte)(this.luldReferencePriceTier));
        cursor += LuldReferencePriceTierLength;
        data.Slice(cursor, EtpFlagLength)[0] = unchecked((byte)(this.etpFlag));
        cursor += EtpFlagLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, EtpLeverageFactorLength), this.etpLeverageFactor);
        cursor += EtpLeverageFactorLength;
        data.Slice(cursor, InverseIndicatorLength)[0] = unchecked((byte)(this.inverseIndicator));
        cursor += InverseIndicatorLength;
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
        if (!global::System.Enum.IsDefined(this.marketCategory))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.MarketCategory), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.financialStatusIndicator))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.FinancialStatusIndicator), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.roundLotsOnly))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.RoundLotsOnly), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.issueClassification))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IssueClassification), ValidationStatus.UndeclaredValue));
        }
        if (!global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IssueSubType.IsDefined(this.issueSubType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IssueSubType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.issueSubType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IssueSubType), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.authenticity))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Authenticity), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.shortSaleThresholdIndicator))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ShortSaleThresholdIndicator), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.ipoFlag))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.IpoFlag), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.luldReferencePriceTier))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.LuldReferencePriceTier), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.etpFlag))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.EtpFlag), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.inverseIndicator))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.InverseIndicator), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(StockDirectoryMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.stockLocate != other.stockLocate) return false;
        if (this.trackingNumber != other.trackingNumber) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.stock != other.stock) return false;
        if (this.marketCategory != other.marketCategory) return false;
        if (this.financialStatusIndicator != other.financialStatusIndicator) return false;
        if (this.roundLotSize != other.roundLotSize) return false;
        if (this.roundLotsOnly != other.roundLotsOnly) return false;
        if (this.issueClassification != other.issueClassification) return false;
        if (this.issueSubType != other.issueSubType) return false;
        if (this.authenticity != other.authenticity) return false;
        if (this.shortSaleThresholdIndicator != other.shortSaleThresholdIndicator) return false;
        if (this.ipoFlag != other.ipoFlag) return false;
        if (this.luldReferencePriceTier != other.luldReferencePriceTier) return false;
        if (this.etpFlag != other.etpFlag) return false;
        if (this.etpLeverageFactor != other.etpLeverageFactor) return false;
        if (this.inverseIndicator != other.inverseIndicator) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is StockDirectoryMessage other && Equals(other);

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
        hash.Add(this.marketCategory);
        hash.Add(this.financialStatusIndicator);
        hash.Add(this.roundLotSize);
        hash.Add(this.roundLotsOnly);
        hash.Add(this.issueClassification);
        hash.Add(this.issueSubType);
        hash.Add(this.authenticity);
        hash.Add(this.shortSaleThresholdIndicator);
        hash.Add(this.ipoFlag);
        hash.Add(this.luldReferencePriceTier);
        hash.Add(this.etpFlag);
        hash.Add(this.etpLeverageFactor);
        hash.Add(this.inverseIndicator);
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
        private const int FieldCount = 18;

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
            if (Has(FieldName.MarketCategory)) names.Add(nameof(FieldName.MarketCategory));
            if (Has(FieldName.FinancialStatusIndicator)) names.Add(nameof(FieldName.FinancialStatusIndicator));
            if (Has(FieldName.RoundLotSize)) names.Add(nameof(FieldName.RoundLotSize));
            if (Has(FieldName.RoundLotsOnly)) names.Add(nameof(FieldName.RoundLotsOnly));
            if (Has(FieldName.IssueClassification)) names.Add(nameof(FieldName.IssueClassification));
            if (Has(FieldName.IssueSubType)) names.Add(nameof(FieldName.IssueSubType));
            if (Has(FieldName.Authenticity)) names.Add(nameof(FieldName.Authenticity));
            if (Has(FieldName.ShortSaleThresholdIndicator)) names.Add(nameof(FieldName.ShortSaleThresholdIndicator));
            if (Has(FieldName.IpoFlag)) names.Add(nameof(FieldName.IpoFlag));
            if (Has(FieldName.LuldReferencePriceTier)) names.Add(nameof(FieldName.LuldReferencePriceTier));
            if (Has(FieldName.EtpFlag)) names.Add(nameof(FieldName.EtpFlag));
            if (Has(FieldName.EtpLeverageFactor)) names.Add(nameof(FieldName.EtpLeverageFactor));
            if (Has(FieldName.InverseIndicator)) names.Add(nameof(FieldName.InverseIndicator));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(StockDirectoryMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.stockLocate != other.stockLocate) diff = diff.With(FieldName.StockLocate);
        if (this.trackingNumber != other.trackingNumber) diff = diff.With(FieldName.TrackingNumber);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.stock != other.stock) diff = diff.With(FieldName.Stock);
        if (this.marketCategory != other.marketCategory) diff = diff.With(FieldName.MarketCategory);
        if (this.financialStatusIndicator != other.financialStatusIndicator) diff = diff.With(FieldName.FinancialStatusIndicator);
        if (this.roundLotSize != other.roundLotSize) diff = diff.With(FieldName.RoundLotSize);
        if (this.roundLotsOnly != other.roundLotsOnly) diff = diff.With(FieldName.RoundLotsOnly);
        if (this.issueClassification != other.issueClassification) diff = diff.With(FieldName.IssueClassification);
        if (this.issueSubType != other.issueSubType) diff = diff.With(FieldName.IssueSubType);
        if (this.authenticity != other.authenticity) diff = diff.With(FieldName.Authenticity);
        if (this.shortSaleThresholdIndicator != other.shortSaleThresholdIndicator) diff = diff.With(FieldName.ShortSaleThresholdIndicator);
        if (this.ipoFlag != other.ipoFlag) diff = diff.With(FieldName.IpoFlag);
        if (this.luldReferencePriceTier != other.luldReferencePriceTier) diff = diff.With(FieldName.LuldReferencePriceTier);
        if (this.etpFlag != other.etpFlag) diff = diff.With(FieldName.EtpFlag);
        if (this.etpLeverageFactor != other.etpLeverageFactor) diff = diff.With(FieldName.EtpLeverageFactor);
        if (this.inverseIndicator != other.inverseIndicator) diff = diff.With(FieldName.InverseIndicator);
        return diff;
    }

    private void CopyField(FieldName field, StockDirectoryMessage from)
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
            case FieldName.MarketCategory:
                this.marketCategory = from.marketCategory;
                break;
            case FieldName.FinancialStatusIndicator:
                this.financialStatusIndicator = from.financialStatusIndicator;
                break;
            case FieldName.RoundLotSize:
                this.roundLotSize = from.roundLotSize;
                break;
            case FieldName.RoundLotsOnly:
                this.roundLotsOnly = from.roundLotsOnly;
                break;
            case FieldName.IssueClassification:
                this.issueClassification = from.issueClassification;
                break;
            case FieldName.IssueSubType:
                this.issueSubType = from.issueSubType;
                break;
            case FieldName.Authenticity:
                this.authenticity = from.authenticity;
                break;
            case FieldName.ShortSaleThresholdIndicator:
                this.shortSaleThresholdIndicator = from.shortSaleThresholdIndicator;
                break;
            case FieldName.IpoFlag:
                this.ipoFlag = from.ipoFlag;
                break;
            case FieldName.LuldReferencePriceTier:
                this.luldReferencePriceTier = from.luldReferencePriceTier;
                break;
            case FieldName.EtpFlag:
                this.etpFlag = from.etpFlag;
                break;
            case FieldName.EtpLeverageFactor:
                this.etpLeverageFactor = from.etpLeverageFactor;
                break;
            case FieldName.InverseIndicator:
                this.inverseIndicator = from.inverseIndicator;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"StockDirectoryMessage {{ StockLocate: {(Text.Of(this.stockLocate))}, TrackingNumber: {(Text.Of(this.trackingNumber))}, Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, Stock: {(Text.Of(this.stock))}, MarketCategory: {(Text.Enum(this.marketCategory))}, FinancialStatusIndicator: {(Text.Enum(this.financialStatusIndicator))}, RoundLotSize: {(Text.Of(this.roundLotSize))}, RoundLotsOnly: {(Text.Enum(this.roundLotsOnly))}, IssueClassification: {(Text.Enum(this.issueClassification))}, IssueSubType: {(Text.Of(this.issueSubType))}, Authenticity: {(Text.Enum(this.authenticity))}, ShortSaleThresholdIndicator: {(Text.Enum(this.shortSaleThresholdIndicator))}, IpoFlag: {(Text.Enum(this.ipoFlag))}, LuldReferencePriceTier: {(Text.Enum(this.luldReferencePriceTier))}, EtpFlag: {(Text.Enum(this.etpFlag))}, EtpLeverageFactor: {(Text.Of(this.etpLeverageFactor))}, InverseIndicator: {(Text.Enum(this.inverseIndicator))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

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
        FieldName.DataValue => Text.Enum(MessageCode.StockDirectoryMessage),
        FieldName.StockLocate => Text.Of(this.stockLocate),
        FieldName.TrackingNumber => Text.Of(this.trackingNumber),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Stock => Text.Of(this.stock),
        FieldName.MarketCategory => Text.Enum(this.marketCategory),
        FieldName.FinancialStatusIndicator => Text.Enum(this.financialStatusIndicator),
        FieldName.RoundLotSize => Text.Of(this.roundLotSize),
        FieldName.RoundLotsOnly => Text.Enum(this.roundLotsOnly),
        FieldName.IssueClassification => Text.Enum(this.issueClassification),
        FieldName.IssueSubType => Text.Of(this.issueSubType),
        FieldName.Authenticity => Text.Enum(this.authenticity),
        FieldName.ShortSaleThresholdIndicator => Text.Enum(this.shortSaleThresholdIndicator),
        FieldName.IpoFlag => Text.Enum(this.ipoFlag),
        FieldName.LuldReferencePriceTier => Text.Enum(this.luldReferencePriceTier),
        FieldName.EtpFlag => Text.Enum(this.etpFlag),
        FieldName.EtpLeverageFactor => Text.Of(this.etpLeverageFactor),
        FieldName.InverseIndicator => Text.Enum(this.inverseIndicator),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.MarketCategory => global::System.Enum.IsDefined(this.marketCategory),
        FieldName.FinancialStatusIndicator => global::System.Enum.IsDefined(this.financialStatusIndicator),
        FieldName.RoundLotsOnly => global::System.Enum.IsDefined(this.roundLotsOnly),
        FieldName.IssueClassification => global::System.Enum.IsDefined(this.issueClassification),
        FieldName.Authenticity => global::System.Enum.IsDefined(this.authenticity),
        FieldName.ShortSaleThresholdIndicator => global::System.Enum.IsDefined(this.shortSaleThresholdIndicator),
        FieldName.IpoFlag => global::System.Enum.IsDefined(this.ipoFlag),
        FieldName.LuldReferencePriceTier => global::System.Enum.IsDefined(this.luldReferencePriceTier),
        FieldName.EtpFlag => global::System.Enum.IsDefined(this.etpFlag),
        FieldName.InverseIndicator => global::System.Enum.IsDefined(this.inverseIndicator),
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
        FieldName.MarketCategory => MarketCategoryOffset,
        FieldName.FinancialStatusIndicator => FinancialStatusIndicatorOffset,
        FieldName.RoundLotSize => RoundLotSizeOffset,
        FieldName.RoundLotsOnly => RoundLotsOnlyOffset,
        FieldName.IssueClassification => IssueClassificationOffset,
        FieldName.IssueSubType => IssueSubTypeOffset,
        FieldName.Authenticity => AuthenticityOffset,
        FieldName.ShortSaleThresholdIndicator => ShortSaleThresholdIndicatorOffset,
        FieldName.IpoFlag => IpoFlagOffset,
        FieldName.LuldReferencePriceTier => LuldReferencePriceTierOffset,
        FieldName.EtpFlag => EtpFlagOffset,
        FieldName.EtpLeverageFactor => EtpLeverageFactorOffset,
        FieldName.InverseIndicator => InverseIndicatorOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.StockLocate => StockLocateLength,
        FieldName.TrackingNumber => TrackingNumberLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Stock => StockLength,
        FieldName.MarketCategory => MarketCategoryLength,
        FieldName.FinancialStatusIndicator => FinancialStatusIndicatorLength,
        FieldName.RoundLotSize => RoundLotSizeLength,
        FieldName.RoundLotsOnly => RoundLotsOnlyLength,
        FieldName.IssueClassification => IssueClassificationLength,
        FieldName.IssueSubType => IssueSubTypeLength,
        FieldName.Authenticity => AuthenticityLength,
        FieldName.ShortSaleThresholdIndicator => ShortSaleThresholdIndicatorLength,
        FieldName.IpoFlag => IpoFlagLength,
        FieldName.LuldReferencePriceTier => LuldReferencePriceTierLength,
        FieldName.EtpFlag => EtpFlagLength,
        FieldName.EtpLeverageFactor => EtpLeverageFactorLength,
        FieldName.InverseIndicator => InverseIndicatorLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is StockDirectoryMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as StockDirectoryMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(MessageCode.StockDirectoryMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
