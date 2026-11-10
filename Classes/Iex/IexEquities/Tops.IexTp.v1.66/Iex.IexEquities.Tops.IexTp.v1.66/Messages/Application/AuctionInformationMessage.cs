using System.Buffers.Binary;
using System.Diagnostics;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class AuctionInformationMessage : IPacketMessage, IEquatable<AuctionInformationMessage>, IFieldOwner
{
    internal const int AuctionTypeOffset = 0;

    internal const int AuctionTypeLength = 1;

    internal const int TimestampOffset = AuctionTypeOffset + AuctionTypeLength;

    internal const int TimestampLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Timestamp"/>.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    internal const int SymbolOffset = TimestampOffset + TimestampLength;

    internal const int SymbolLength = 8;

    /// <summary>
    ///  The byte that fills the unused bytes of Symbol.
    /// </summary>
    public const byte SymbolFill = (byte)' ';

    internal const int PairedSharesOffset = SymbolOffset + SymbolLength;

    internal const int PairedSharesLength = 4;

    internal const int ReferencePriceOffset = PairedSharesOffset + PairedSharesLength;

    internal const int ReferencePriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Reference Price.
    /// </summary>
    public const byte ReferencePriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="ReferencePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal ReferencePriceFactor = 10000m;

    internal const int IndicativeClearingPriceOffset = ReferencePriceOffset + ReferencePriceLength;

    internal const int IndicativeClearingPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Indicative Clearing Price.
    /// </summary>
    public const byte IndicativeClearingPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="IndicativeClearingPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal IndicativeClearingPriceFactor = 10000m;

    internal const int ImbalanceSharesOffset = IndicativeClearingPriceOffset + IndicativeClearingPriceLength;

    internal const int ImbalanceSharesLength = 4;

    internal const int ImbalanceSideOffset = ImbalanceSharesOffset + ImbalanceSharesLength;

    internal const int ImbalanceSideLength = 1;

    internal const int ExtensionNumberOffset = ImbalanceSideOffset + ImbalanceSideLength;

    internal const int ExtensionNumberLength = 1;

    internal const int ScheduledAuctionTimeOffset = ExtensionNumberOffset + ExtensionNumberLength;

    internal const int ScheduledAuctionTimeLength = 4;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="ScheduledAuctionTime"/>.
    /// </summary>
    public const long ScheduledAuctionTimeNanosecondsPerUnit = TimeSpan.TicksPerSecond * TimeSpan.NanosecondsPerTick;

    internal const int AuctionBookClearingPriceOffset = ScheduledAuctionTimeOffset + ScheduledAuctionTimeLength;

    internal const int AuctionBookClearingPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Auction Book Clearing Price.
    /// </summary>
    public const byte AuctionBookClearingPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="AuctionBookClearingPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AuctionBookClearingPriceFactor = 10000m;

    internal const int CollarReferencePriceOffset = AuctionBookClearingPriceOffset + AuctionBookClearingPriceLength;

    internal const int CollarReferencePriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Collar Reference Price.
    /// </summary>
    public const byte CollarReferencePriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="CollarReferencePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal CollarReferencePriceFactor = 10000m;

    internal const int LowerAuctionCollarOffset = CollarReferencePriceOffset + CollarReferencePriceLength;

    internal const int LowerAuctionCollarLength = 8;

    /// <summary>
    ///  The number of decimal places in Lower Auction Collar.
    /// </summary>
    public const byte LowerAuctionCollarScale = 4;

    /// <summary>
    ///  The factor by which <see cref="LowerAuctionCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LowerAuctionCollarFactor = 10000m;

    internal const int UpperAuctionCollarOffset = LowerAuctionCollarOffset + LowerAuctionCollarLength;

    internal const int UpperAuctionCollarLength = 8;

    /// <summary>
    ///  The number of decimal places in Upper Auction Collar.
    /// </summary>
    public const byte UpperAuctionCollarScale = 4;

    /// <summary>
    ///  The factor by which <see cref="UpperAuctionCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal UpperAuctionCollarFactor = 10000m;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = AuctionTypeLength + TimestampLength + SymbolLength + PairedSharesLength + ReferencePriceLength + IndicativeClearingPriceLength + ImbalanceSharesLength + ImbalanceSideLength + ExtensionNumberLength + ScheduledAuctionTimeLength + AuctionBookClearingPriceLength + CollarReferencePriceLength + LowerAuctionCollarLength + UpperAuctionCollarLength;

    internal AuctionType auctionType;

    /// <summary>
    ///  Auction type identifier
    /// </summary>
    public AuctionType AuctionType
    {
        get => this.auctionType;
        set
        {
            this.auctionType = value;
            Touch();
        }
    }

    internal long timestamp;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    /// <remarks>
    ///  This property has a precision of 100 nanoseconds, and <see cref="TimestampRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public DateTime Timestamp
    {
        get => Instant.ToDateTime(this.timestamp, TimestampNanosecondsPerUnit);
        set
        {
            this.timestamp = Instant.ToRaw<long>(value, TimestampNanosecondsPerUnit, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Timestamp"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long TimestampRaw
    {
        get => this.timestamp;
        set
        {
            this.timestamp = value;
            Touch();
        }
    }

    internal string symbol = string.Empty;

    /// <summary>
    ///  Security identifier
    /// </summary>
    /// <remarks>
    ///  The field is 8 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with spaces at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Symbol
    {
        get => this.symbol;
        set
        {
            this.symbol = value ?? string.Empty;
            Touch();
        }
    }

    internal uint pairedShares;

    /// <summary>
    ///  Number of shares paired at the Reference Price using orders on the Auction Book
    /// </summary>
    public uint PairedShares
    {
        get => this.pairedShares;
        set
        {
            this.pairedShares = value;
            Touch();
        }
    }

    internal long referencePrice;

    /// <summary>
    ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal ReferencePrice
    {
        get => FixedPoint.ToDecimal(this.referencePrice, ReferencePriceScale);
        set
        {
            this.referencePrice = FixedPoint.ToRaw<long>(value, ReferencePriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="ReferencePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long ReferencePriceRaw
    {
        get => this.referencePrice;
        set
        {
            this.referencePrice = value;
            Touch();
        }
    }

    internal long indicativeClearingPrice;

    /// <summary>
    ///  Clearing price using Eligible Auction Orders
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal IndicativeClearingPrice
    {
        get => FixedPoint.ToDecimal(this.indicativeClearingPrice, IndicativeClearingPriceScale);
        set
        {
            this.indicativeClearingPrice = FixedPoint.ToRaw<long>(value, IndicativeClearingPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="IndicativeClearingPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long IndicativeClearingPriceRaw
    {
        get => this.indicativeClearingPrice;
        set
        {
            this.indicativeClearingPrice = value;
            Touch();
        }
    }

    internal uint imbalanceShares;

    /// <summary>
    ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public uint ImbalanceShares
    {
        get => this.imbalanceShares;
        set
        {
            this.imbalanceShares = value;
            Touch();
        }
    }

    internal ImbalanceSide imbalanceSide;

    /// <summary>
    ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public ImbalanceSide ImbalanceSide
    {
        get => this.imbalanceSide;
        set
        {
            this.imbalanceSide = value;
            Touch();
        }
    }

    internal byte extensionNumber;

    /// <summary>
    ///  Number of extensions an auction received
    /// </summary>
    public byte ExtensionNumber
    {
        get => this.extensionNumber;
        set
        {
            this.extensionNumber = value;
            Touch();
        }
    }

    internal uint scheduledAuctionTime;

    /// <summary>
    ///  Projected time of the auction match
    /// </summary>
    /// <remarks>
    ///  Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time is rounded to the wire unit of the field, with a midpoint away from zero. A time outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public DateTime ScheduledAuctionTime
    {
        get => Instant.ToDateTime(this.scheduledAuctionTime, ScheduledAuctionTimeNanosecondsPerUnit);
        set
        {
            this.scheduledAuctionTime = Instant.ToRaw<uint>(value, ScheduledAuctionTimeNanosecondsPerUnit, uint.MinValue, uint.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="ScheduledAuctionTime"/>, as the unscaled integer that the field holds.
    /// </summary>
    public uint ScheduledAuctionTimeRaw
    {
        get => this.scheduledAuctionTime;
        set
        {
            this.scheduledAuctionTime = value;
            Touch();
        }
    }

    internal long auctionBookClearingPrice;

    /// <summary>
    ///  Clearing price using orders on the Auction Book
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal AuctionBookClearingPrice
    {
        get => FixedPoint.ToDecimal(this.auctionBookClearingPrice, AuctionBookClearingPriceScale);
        set
        {
            this.auctionBookClearingPrice = FixedPoint.ToRaw<long>(value, AuctionBookClearingPriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AuctionBookClearingPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long AuctionBookClearingPriceRaw
    {
        get => this.auctionBookClearingPrice;
        set
        {
            this.auctionBookClearingPrice = value;
            Touch();
        }
    }

    internal long collarReferencePrice;

    /// <summary>
    ///  Reference priced used for the auction collar, if any
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal CollarReferencePrice
    {
        get => FixedPoint.ToDecimal(this.collarReferencePrice, CollarReferencePriceScale);
        set
        {
            this.collarReferencePrice = FixedPoint.ToRaw<long>(value, CollarReferencePriceFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="CollarReferencePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long CollarReferencePriceRaw
    {
        get => this.collarReferencePrice;
        set
        {
            this.collarReferencePrice = value;
            Touch();
        }
    }

    internal long lowerAuctionCollar;

    /// <summary>
    ///  Lower threshold price of the auction collar, if any
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal LowerAuctionCollar
    {
        get => FixedPoint.ToDecimal(this.lowerAuctionCollar, LowerAuctionCollarScale);
        set
        {
            this.lowerAuctionCollar = FixedPoint.ToRaw<long>(value, LowerAuctionCollarFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="LowerAuctionCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long LowerAuctionCollarRaw
    {
        get => this.lowerAuctionCollar;
        set
        {
            this.lowerAuctionCollar = value;
            Touch();
        }
    }

    internal long upperAuctionCollar;

    /// <summary>
    ///  Upper threshold price of the auction collar, if any
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal UpperAuctionCollar
    {
        get => FixedPoint.ToDecimal(this.upperAuctionCollar, UpperAuctionCollarScale);
        set
        {
            this.upperAuctionCollar = FixedPoint.ToRaw<long>(value, UpperAuctionCollarFactor, long.MinValue, long.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="UpperAuctionCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long UpperAuctionCollarRaw
    {
        get => this.upperAuctionCollar;
        set
        {
            this.upperAuctionCollar = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.AuctionInformationMessage;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.auctionType) && global::System.Enum.IsDefined(this.imbalanceSide);

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

    IMessage IMessage.Copy() => new AuctionInformationMessage(this);

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
        ///  Auction type identifier
        /// </summary>
        AuctionType,
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        Timestamp,
        /// <summary>
        ///  Security identifier
        /// </summary>
        Symbol,
        /// <summary>
        ///  Number of shares paired at the Reference Price using orders on the Auction Book
        /// </summary>
        PairedShares,
        /// <summary>
        ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
        /// </summary>
        ReferencePrice,
        /// <summary>
        ///  Clearing price using Eligible Auction Orders
        /// </summary>
        IndicativeClearingPrice,
        /// <summary>
        ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        ImbalanceShares,
        /// <summary>
        ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        ImbalanceSide,
        /// <summary>
        ///  Number of extensions an auction received
        /// </summary>
        ExtensionNumber,
        /// <summary>
        ///  Projected time of the auction match
        /// </summary>
        ScheduledAuctionTime,
        /// <summary>
        ///  Clearing price using orders on the Auction Book
        /// </summary>
        AuctionBookClearingPrice,
        /// <summary>
        ///  Reference priced used for the auction collar, if any
        /// </summary>
        CollarReferencePrice,
        /// <summary>
        ///  Lower threshold price of the auction collar, if any
        /// </summary>
        LowerAuctionCollar,
        /// <summary>
        ///  Upper threshold price of the auction collar, if any
        /// </summary>
        UpperAuctionCollar,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly AuctionInformationMessage owner;

        internal FieldSet(AuctionInformationMessage owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The message type.
        /// </summary>
        public FieldView DataValue => this[FieldName.DataValue];

        /// <summary>
        ///  Auction type identifier
        /// </summary>
        public FieldView AuctionType => this[FieldName.AuctionType];

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public FieldView Timestamp => this[FieldName.Timestamp];

        /// <summary>
        ///  Security identifier
        /// </summary>
        public FieldView Symbol => this[FieldName.Symbol];

        /// <summary>
        ///  Number of shares paired at the Reference Price using orders on the Auction Book
        /// </summary>
        public FieldView PairedShares => this[FieldName.PairedShares];

        /// <summary>
        ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
        /// </summary>
        public FieldView ReferencePrice => this[FieldName.ReferencePrice];

        /// <summary>
        ///  Clearing price using Eligible Auction Orders
        /// </summary>
        public FieldView IndicativeClearingPrice => this[FieldName.IndicativeClearingPrice];

        /// <summary>
        ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public FieldView ImbalanceShares => this[FieldName.ImbalanceShares];

        /// <summary>
        ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public FieldView ImbalanceSide => this[FieldName.ImbalanceSide];

        /// <summary>
        ///  Number of extensions an auction received
        /// </summary>
        public FieldView ExtensionNumber => this[FieldName.ExtensionNumber];

        /// <summary>
        ///  Projected time of the auction match
        /// </summary>
        public FieldView ScheduledAuctionTime => this[FieldName.ScheduledAuctionTime];

        /// <summary>
        ///  Clearing price using orders on the Auction Book
        /// </summary>
        public FieldView AuctionBookClearingPrice => this[FieldName.AuctionBookClearingPrice];

        /// <summary>
        ///  Reference priced used for the auction collar, if any
        /// </summary>
        public FieldView CollarReferencePrice => this[FieldName.CollarReferencePrice];

        /// <summary>
        ///  Lower threshold price of the auction collar, if any
        /// </summary>
        public FieldView LowerAuctionCollar => this[FieldName.LowerAuctionCollar];

        /// <summary>
        ///  Upper threshold price of the auction collar, if any
        /// </summary>
        public FieldView UpperAuctionCollar => this[FieldName.UpperAuctionCollar];

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
        new(nameof(FieldName.AuctionType), FieldKind.Enum, AuctionTypeLength),
        new(nameof(FieldName.Timestamp), FieldKind.DateTime, TimestampLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.Symbol), FieldKind.String, SymbolLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.PairedShares), FieldKind.UnsignedInteger, PairedSharesLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.ReferencePrice), FieldKind.Decimal, ReferencePriceLength) { Scale = ReferencePriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.IndicativeClearingPrice), FieldKind.Decimal, IndicativeClearingPriceLength) { Scale = IndicativeClearingPriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.ImbalanceShares), FieldKind.UnsignedInteger, ImbalanceSharesLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.ImbalanceSide), FieldKind.Enum, ImbalanceSideLength),
        new(nameof(FieldName.ExtensionNumber), FieldKind.UnsignedInteger, ExtensionNumberLength) { IsSigned = false },
        new(nameof(FieldName.ScheduledAuctionTime), FieldKind.DateTime, ScheduledAuctionTimeLength) { Unit = "Seconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.AuctionBookClearingPrice), FieldKind.Decimal, AuctionBookClearingPriceLength) { Scale = AuctionBookClearingPriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.CollarReferencePrice), FieldKind.Decimal, CollarReferencePriceLength) { Scale = CollarReferencePriceScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.LowerAuctionCollar), FieldKind.Decimal, LowerAuctionCollarLength) { Scale = LowerAuctionCollarScale, ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.UpperAuctionCollar), FieldKind.Decimal, UpperAuctionCollarLength) { Scale = UpperAuctionCollarScale, ByteOrder = "Little", IsSigned = true },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="AuctionInformationMessage"/> class with default values.
    /// </summary>
    public AuctionInformationMessage()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="AuctionInformationMessage"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public AuctionInformationMessage(AuctionInformationMessage other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(AuctionInformationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.auctionType = other.auctionType;
        this.timestamp = other.timestamp;
        this.symbol = other.symbol;
        this.pairedShares = other.pairedShares;
        this.referencePrice = other.referencePrice;
        this.indicativeClearingPrice = other.indicativeClearingPrice;
        this.imbalanceShares = other.imbalanceShares;
        this.imbalanceSide = other.imbalanceSide;
        this.extensionNumber = other.extensionNumber;
        this.scheduledAuctionTime = other.scheduledAuctionTime;
        this.auctionBookClearingPrice = other.auctionBookClearingPrice;
        this.collarReferencePrice = other.collarReferencePrice;
        this.lowerAuctionCollar = other.lowerAuctionCollar;
        this.upperAuctionCollar = other.upperAuctionCollar;
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
        this.auctionType = default;
        this.timestamp = default;
        this.symbol = string.Empty;
        this.pairedShares = default;
        this.referencePrice = default;
        this.indicativeClearingPrice = default;
        this.imbalanceShares = default;
        this.imbalanceSide = default;
        this.extensionNumber = default;
        this.scheduledAuctionTime = default;
        this.auctionBookClearingPrice = default;
        this.collarReferencePrice = default;
        this.lowerAuctionCollar = default;
        this.upperAuctionCollar = default;
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
    public static AuctionInformationMessage Parse(ReadOnlySpan<byte> data)
    {
        var instance = new AuctionInformationMessage();
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
    public static AuctionInformationMessage Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new AuctionInformationMessage();
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
        if (Truncated(data, start, cursor, AuctionTypeLength, FieldName.AuctionType)) return;
        this.auctionType = (AuctionType)data.Slice(cursor, AuctionTypeLength)[0];
        cursor += AuctionTypeLength;
        if (Truncated(data, start, cursor, TimestampLength, FieldName.Timestamp)) return;
        this.timestamp = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, TimestampLength));
        cursor += TimestampLength;
        if (Truncated(data, start, cursor, SymbolLength, FieldName.Symbol)) return;
        this.symbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        if (Truncated(data, start, cursor, PairedSharesLength, FieldName.PairedShares)) return;
        this.pairedShares = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, PairedSharesLength));
        cursor += PairedSharesLength;
        if (Truncated(data, start, cursor, ReferencePriceLength, FieldName.ReferencePrice)) return;
        this.referencePrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, ReferencePriceLength));
        cursor += ReferencePriceLength;
        if (Truncated(data, start, cursor, IndicativeClearingPriceLength, FieldName.IndicativeClearingPrice)) return;
        this.indicativeClearingPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, IndicativeClearingPriceLength));
        cursor += IndicativeClearingPriceLength;
        if (Truncated(data, start, cursor, ImbalanceSharesLength, FieldName.ImbalanceShares)) return;
        this.imbalanceShares = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, ImbalanceSharesLength));
        cursor += ImbalanceSharesLength;
        if (Truncated(data, start, cursor, ImbalanceSideLength, FieldName.ImbalanceSide)) return;
        this.imbalanceSide = (ImbalanceSide)data.Slice(cursor, ImbalanceSideLength)[0];
        cursor += ImbalanceSideLength;
        if (Truncated(data, start, cursor, ExtensionNumberLength, FieldName.ExtensionNumber)) return;
        this.extensionNumber = data.Slice(cursor, ExtensionNumberLength)[0];
        cursor += ExtensionNumberLength;
        if (Truncated(data, start, cursor, ScheduledAuctionTimeLength, FieldName.ScheduledAuctionTime)) return;
        this.scheduledAuctionTime = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, ScheduledAuctionTimeLength));
        cursor += ScheduledAuctionTimeLength;
        if (Truncated(data, start, cursor, AuctionBookClearingPriceLength, FieldName.AuctionBookClearingPrice)) return;
        this.auctionBookClearingPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, AuctionBookClearingPriceLength));
        cursor += AuctionBookClearingPriceLength;
        if (Truncated(data, start, cursor, CollarReferencePriceLength, FieldName.CollarReferencePrice)) return;
        this.collarReferencePrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, CollarReferencePriceLength));
        cursor += CollarReferencePriceLength;
        if (Truncated(data, start, cursor, LowerAuctionCollarLength, FieldName.LowerAuctionCollar)) return;
        this.lowerAuctionCollar = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, LowerAuctionCollarLength));
        cursor += LowerAuctionCollarLength;
        if (Truncated(data, start, cursor, UpperAuctionCollarLength, FieldName.UpperAuctionCollar)) return;
        this.upperAuctionCollar = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, UpperAuctionCollarLength));
        cursor += UpperAuctionCollarLength;
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
        data.Slice(cursor, AuctionTypeLength)[0] = unchecked((byte)(this.auctionType));
        cursor += AuctionTypeLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, TimestampLength), this.timestamp);
        cursor += TimestampLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SymbolLength), TextEncoding.Ascii.Fit(this.symbol, SymbolLength), SymbolFill);
        cursor += SymbolLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, PairedSharesLength), this.pairedShares);
        cursor += PairedSharesLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, ReferencePriceLength), this.referencePrice);
        cursor += ReferencePriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, IndicativeClearingPriceLength), this.indicativeClearingPrice);
        cursor += IndicativeClearingPriceLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, ImbalanceSharesLength), this.imbalanceShares);
        cursor += ImbalanceSharesLength;
        data.Slice(cursor, ImbalanceSideLength)[0] = unchecked((byte)(this.imbalanceSide));
        cursor += ImbalanceSideLength;
        data.Slice(cursor, ExtensionNumberLength)[0] = unchecked((byte)(this.extensionNumber));
        cursor += ExtensionNumberLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, ScheduledAuctionTimeLength), this.scheduledAuctionTime);
        cursor += ScheduledAuctionTimeLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, AuctionBookClearingPriceLength), this.auctionBookClearingPrice);
        cursor += AuctionBookClearingPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, CollarReferencePriceLength), this.collarReferencePrice);
        cursor += CollarReferencePriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, LowerAuctionCollarLength), this.lowerAuctionCollar);
        cursor += LowerAuctionCollarLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, UpperAuctionCollarLength), this.upperAuctionCollar);
        cursor += UpperAuctionCollarLength;
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
        if (!global::System.Enum.IsDefined(this.auctionType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AuctionType), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.symbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Symbol), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.imbalanceSide))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.ImbalanceSide), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(AuctionInformationMessage? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.auctionType != other.auctionType) return false;
        if (this.timestamp != other.timestamp) return false;
        if (this.symbol != other.symbol) return false;
        if (this.pairedShares != other.pairedShares) return false;
        if (this.referencePrice != other.referencePrice) return false;
        if (this.indicativeClearingPrice != other.indicativeClearingPrice) return false;
        if (this.imbalanceShares != other.imbalanceShares) return false;
        if (this.imbalanceSide != other.imbalanceSide) return false;
        if (this.extensionNumber != other.extensionNumber) return false;
        if (this.scheduledAuctionTime != other.scheduledAuctionTime) return false;
        if (this.auctionBookClearingPrice != other.auctionBookClearingPrice) return false;
        if (this.collarReferencePrice != other.collarReferencePrice) return false;
        if (this.lowerAuctionCollar != other.lowerAuctionCollar) return false;
        if (this.upperAuctionCollar != other.upperAuctionCollar) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is AuctionInformationMessage other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.auctionType);
        hash.Add(this.timestamp);
        hash.Add(this.symbol);
        hash.Add(this.pairedShares);
        hash.Add(this.referencePrice);
        hash.Add(this.indicativeClearingPrice);
        hash.Add(this.imbalanceShares);
        hash.Add(this.imbalanceSide);
        hash.Add(this.extensionNumber);
        hash.Add(this.scheduledAuctionTime);
        hash.Add(this.auctionBookClearingPrice);
        hash.Add(this.collarReferencePrice);
        hash.Add(this.lowerAuctionCollar);
        hash.Add(this.upperAuctionCollar);
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
        private const int FieldCount = 15;

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
            if (Has(FieldName.AuctionType)) names.Add(nameof(FieldName.AuctionType));
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.Symbol)) names.Add(nameof(FieldName.Symbol));
            if (Has(FieldName.PairedShares)) names.Add(nameof(FieldName.PairedShares));
            if (Has(FieldName.ReferencePrice)) names.Add(nameof(FieldName.ReferencePrice));
            if (Has(FieldName.IndicativeClearingPrice)) names.Add(nameof(FieldName.IndicativeClearingPrice));
            if (Has(FieldName.ImbalanceShares)) names.Add(nameof(FieldName.ImbalanceShares));
            if (Has(FieldName.ImbalanceSide)) names.Add(nameof(FieldName.ImbalanceSide));
            if (Has(FieldName.ExtensionNumber)) names.Add(nameof(FieldName.ExtensionNumber));
            if (Has(FieldName.ScheduledAuctionTime)) names.Add(nameof(FieldName.ScheduledAuctionTime));
            if (Has(FieldName.AuctionBookClearingPrice)) names.Add(nameof(FieldName.AuctionBookClearingPrice));
            if (Has(FieldName.CollarReferencePrice)) names.Add(nameof(FieldName.CollarReferencePrice));
            if (Has(FieldName.LowerAuctionCollar)) names.Add(nameof(FieldName.LowerAuctionCollar));
            if (Has(FieldName.UpperAuctionCollar)) names.Add(nameof(FieldName.UpperAuctionCollar));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(AuctionInformationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.auctionType != other.auctionType) diff = diff.With(FieldName.AuctionType);
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.symbol != other.symbol) diff = diff.With(FieldName.Symbol);
        if (this.pairedShares != other.pairedShares) diff = diff.With(FieldName.PairedShares);
        if (this.referencePrice != other.referencePrice) diff = diff.With(FieldName.ReferencePrice);
        if (this.indicativeClearingPrice != other.indicativeClearingPrice) diff = diff.With(FieldName.IndicativeClearingPrice);
        if (this.imbalanceShares != other.imbalanceShares) diff = diff.With(FieldName.ImbalanceShares);
        if (this.imbalanceSide != other.imbalanceSide) diff = diff.With(FieldName.ImbalanceSide);
        if (this.extensionNumber != other.extensionNumber) diff = diff.With(FieldName.ExtensionNumber);
        if (this.scheduledAuctionTime != other.scheduledAuctionTime) diff = diff.With(FieldName.ScheduledAuctionTime);
        if (this.auctionBookClearingPrice != other.auctionBookClearingPrice) diff = diff.With(FieldName.AuctionBookClearingPrice);
        if (this.collarReferencePrice != other.collarReferencePrice) diff = diff.With(FieldName.CollarReferencePrice);
        if (this.lowerAuctionCollar != other.lowerAuctionCollar) diff = diff.With(FieldName.LowerAuctionCollar);
        if (this.upperAuctionCollar != other.upperAuctionCollar) diff = diff.With(FieldName.UpperAuctionCollar);
        return diff;
    }

    private void CopyField(FieldName field, AuctionInformationMessage from)
    {
        switch (field)
        {
            case FieldName.AuctionType:
                this.auctionType = from.auctionType;
                break;
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.Symbol:
                this.symbol = from.symbol;
                break;
            case FieldName.PairedShares:
                this.pairedShares = from.pairedShares;
                break;
            case FieldName.ReferencePrice:
                this.referencePrice = from.referencePrice;
                break;
            case FieldName.IndicativeClearingPrice:
                this.indicativeClearingPrice = from.indicativeClearingPrice;
                break;
            case FieldName.ImbalanceShares:
                this.imbalanceShares = from.imbalanceShares;
                break;
            case FieldName.ImbalanceSide:
                this.imbalanceSide = from.imbalanceSide;
                break;
            case FieldName.ExtensionNumber:
                this.extensionNumber = from.extensionNumber;
                break;
            case FieldName.ScheduledAuctionTime:
                this.scheduledAuctionTime = from.scheduledAuctionTime;
                break;
            case FieldName.AuctionBookClearingPrice:
                this.auctionBookClearingPrice = from.auctionBookClearingPrice;
                break;
            case FieldName.CollarReferencePrice:
                this.collarReferencePrice = from.collarReferencePrice;
                break;
            case FieldName.LowerAuctionCollar:
                this.lowerAuctionCollar = from.lowerAuctionCollar;
                break;
            case FieldName.UpperAuctionCollar:
                this.upperAuctionCollar = from.upperAuctionCollar;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"AuctionInformationMessage {{ AuctionType: {(Text.Enum(this.auctionType))}, Timestamp: {(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}, Symbol: {(Text.Of(this.symbol))}, PairedShares: {(Text.Of(this.pairedShares))}, ReferencePrice: {(Text.Of(FixedPoint.ToDecimal(this.referencePrice, ReferencePriceScale)))}, IndicativeClearingPrice: {(Text.Of(FixedPoint.ToDecimal(this.indicativeClearingPrice, IndicativeClearingPriceScale)))}, ImbalanceShares: {(Text.Of(this.imbalanceShares))}, ImbalanceSide: {(Text.Enum(this.imbalanceSide))}, ExtensionNumber: {(Text.Of(this.extensionNumber))}, ScheduledAuctionTime: {(Text.Instant(this.scheduledAuctionTime, ScheduledAuctionTimeNanosecondsPerUnit))}, AuctionBookClearingPrice: {(Text.Of(FixedPoint.ToDecimal(this.auctionBookClearingPrice, AuctionBookClearingPriceScale)))}, CollarReferencePrice: {(Text.Of(FixedPoint.ToDecimal(this.collarReferencePrice, CollarReferencePriceScale)))}, LowerAuctionCollar: {(Text.Of(FixedPoint.ToDecimal(this.lowerAuctionCollar, LowerAuctionCollarScale)))}, UpperAuctionCollar: {(Text.Of(FixedPoint.ToDecimal(this.upperAuctionCollar, UpperAuctionCollarScale)))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Enum(this.auctionType))}] [{(Text.Instant(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.symbol))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.AuctionInformationMessage),
        FieldName.AuctionType => Text.Enum(this.auctionType),
        FieldName.Timestamp => Text.Instant(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.Symbol => Text.Of(this.symbol),
        FieldName.PairedShares => Text.Of(this.pairedShares),
        FieldName.ReferencePrice => Text.Of(FixedPoint.ToDecimal(this.referencePrice, ReferencePriceScale)),
        FieldName.IndicativeClearingPrice => Text.Of(FixedPoint.ToDecimal(this.indicativeClearingPrice, IndicativeClearingPriceScale)),
        FieldName.ImbalanceShares => Text.Of(this.imbalanceShares),
        FieldName.ImbalanceSide => Text.Enum(this.imbalanceSide),
        FieldName.ExtensionNumber => Text.Of(this.extensionNumber),
        FieldName.ScheduledAuctionTime => Text.Instant(this.scheduledAuctionTime, ScheduledAuctionTimeNanosecondsPerUnit),
        FieldName.AuctionBookClearingPrice => Text.Of(FixedPoint.ToDecimal(this.auctionBookClearingPrice, AuctionBookClearingPriceScale)),
        FieldName.CollarReferencePrice => Text.Of(FixedPoint.ToDecimal(this.collarReferencePrice, CollarReferencePriceScale)),
        FieldName.LowerAuctionCollar => Text.Of(FixedPoint.ToDecimal(this.lowerAuctionCollar, LowerAuctionCollarScale)),
        FieldName.UpperAuctionCollar => Text.Of(FixedPoint.ToDecimal(this.upperAuctionCollar, UpperAuctionCollarScale)),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.AuctionType => global::System.Enum.IsDefined(this.auctionType),
        FieldName.ImbalanceSide => global::System.Enum.IsDefined(this.imbalanceSide),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => true;

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.AuctionType => AuctionTypeOffset,
        FieldName.Timestamp => TimestampOffset,
        FieldName.Symbol => SymbolOffset,
        FieldName.PairedShares => PairedSharesOffset,
        FieldName.ReferencePrice => ReferencePriceOffset,
        FieldName.IndicativeClearingPrice => IndicativeClearingPriceOffset,
        FieldName.ImbalanceShares => ImbalanceSharesOffset,
        FieldName.ImbalanceSide => ImbalanceSideOffset,
        FieldName.ExtensionNumber => ExtensionNumberOffset,
        FieldName.ScheduledAuctionTime => ScheduledAuctionTimeOffset,
        FieldName.AuctionBookClearingPrice => AuctionBookClearingPriceOffset,
        FieldName.CollarReferencePrice => CollarReferencePriceOffset,
        FieldName.LowerAuctionCollar => LowerAuctionCollarOffset,
        FieldName.UpperAuctionCollar => UpperAuctionCollarOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.AuctionType => AuctionTypeLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.Symbol => SymbolLength,
        FieldName.PairedShares => PairedSharesLength,
        FieldName.ReferencePrice => ReferencePriceLength,
        FieldName.IndicativeClearingPrice => IndicativeClearingPriceLength,
        FieldName.ImbalanceShares => ImbalanceSharesLength,
        FieldName.ImbalanceSide => ImbalanceSideLength,
        FieldName.ExtensionNumber => ExtensionNumberLength,
        FieldName.ScheduledAuctionTime => ScheduledAuctionTimeLength,
        FieldName.AuctionBookClearingPrice => AuctionBookClearingPriceLength,
        FieldName.CollarReferencePrice => CollarReferencePriceLength,
        FieldName.LowerAuctionCollar => LowerAuctionCollarLength,
        FieldName.UpperAuctionCollar => UpperAuctionCollarLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is AuctionInformationMessage typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as AuctionInformationMessage ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.AuctionInformationMessage));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
