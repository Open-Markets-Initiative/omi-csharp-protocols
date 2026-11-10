using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class StrategyBestBidAndAskUpdate : IPacketMessage, IEquatable<StrategyBestBidAndAskUpdate>, IFieldOwner
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

    internal const int QuoteConditionOffset = StrategyIdOffset + StrategyIdLength;

    internal const int QuoteConditionLength = 1;

    internal const int BidPriceOffset = QuoteConditionOffset + QuoteConditionLength;

    internal const int BidPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Bid Price.
    /// </summary>
    public const byte BidPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="BidPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal BidPriceFactor = 10000m;

    internal const int BidSizeOffset = BidPriceOffset + BidPriceLength;

    internal const int BidSizeLength = 4;

    internal const int BidCustSizeOffset = BidSizeOffset + BidSizeLength;

    internal const int BidCustSizeLength = 4;

    internal const int BidProCustSizeOffset = BidCustSizeOffset + BidCustSizeLength;

    internal const int BidProCustSizeLength = 4;

    internal const int BidNttSizeOffset = BidProCustSizeOffset + BidProCustSizeLength;

    internal const int BidNttSizeLength = 4;

    internal const int BidMarketSizeOffset = BidNttSizeOffset + BidNttSizeLength;

    internal const int BidMarketSizeLength = 4;

    internal const int BidNttMarketSizeOffset = BidMarketSizeOffset + BidMarketSizeLength;

    internal const int BidNttMarketSizeLength = 4;

    internal const int AskPriceOffset = BidNttMarketSizeOffset + BidNttMarketSizeLength;

    internal const int AskPriceLength = 4;

    /// <summary>
    ///  The number of decimal places in Ask Price.
    /// </summary>
    public const byte AskPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="AskPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AskPriceFactor = 10000m;

    internal const int AskSizeOffset = AskPriceOffset + AskPriceLength;

    internal const int AskSizeLength = 4;

    internal const int AskCustSizeOffset = AskSizeOffset + AskSizeLength;

    internal const int AskCustSizeLength = 4;

    internal const int AskProCustSizeOffset = AskCustSizeOffset + AskCustSizeLength;

    internal const int AskProCustSizeLength = 4;

    internal const int AskNttSizeOffset = AskProCustSizeOffset + AskProCustSizeLength;

    internal const int AskNttSizeLength = 4;

    internal const int AskMarketSizeOffset = AskNttSizeOffset + AskNttSizeLength;

    internal const int AskMarketSizeLength = 4;

    internal const int AskNttMarketSizeOffset = AskMarketSizeOffset + AskMarketSizeLength;

    internal const int AskNttMarketSizeLength = 4;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TimestampLength + StrategyIdLength + QuoteConditionLength + BidPriceLength + BidSizeLength + BidCustSizeLength + BidProCustSizeLength + BidNttSizeLength + BidMarketSizeLength + BidNttMarketSizeLength + AskPriceLength + AskSizeLength + AskCustSizeLength + AskProCustSizeLength + AskNttSizeLength + AskMarketSizeLength + AskNttMarketSizeLength;

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

    internal QuoteCondition quoteCondition;

    /// <summary>
    ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
    /// </summary>
    public QuoteCondition QuoteCondition
    {
        get => this.quoteCondition;
        set
        {
            this.quoteCondition = value;
            Touch();
        }
    }

    internal int bidPrice;

    /// <summary>
    ///  Best bid price in fixed point format with 6 whole number places followed by 4 decimal digits.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal BidPrice
    {
        get => FixedPoint.ToDecimal(this.bidPrice, BidPriceScale);
        set
        {
            this.bidPrice = FixedPoint.ToRaw<int>(value, BidPriceFactor, int.MinValue, int.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="BidPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public int BidPriceRaw
    {
        get => this.bidPrice;
        set
        {
            this.bidPrice = value;
            Touch();
        }
    }

    internal uint bidSize;

    /// <summary>
    ///  Aggregated number of contracts on the bid side being displayed in the options market at the current time.
    /// </summary>
    public uint BidSize
    {
        get => this.bidSize;
        set
        {
            this.bidSize = value;
            Touch();
        }
    }

    internal uint bidCustSize;

    /// <summary>
    ///  Customer quantity on the bid side.
    /// </summary>
    public uint BidCustSize
    {
        get => this.bidCustSize;
        set
        {
            this.bidCustSize = value;
            Touch();
        }
    }

    internal uint bidProCustSize;

    /// <summary>
    ///  Customer professional quantity on the bid side.
    /// </summary>
    public uint BidProCustSize
    {
        get => this.bidProCustSize;
        set
        {
            this.bidProCustSize = value;
            Touch();
        }
    }

    internal uint bidNttSize;

    /// <summary>
    ///  Limit Bid size that can’t be traded through.
    /// </summary>
    public uint BidNttSize
    {
        get => this.bidNttSize;
        set
        {
            this.bidNttSize = value;
            Touch();
        }
    }

    internal uint bidMarketSize;

    /// <summary>
    ///  The aggregate market size on Bid side.
    /// </summary>
    public uint BidMarketSize
    {
        get => this.bidMarketSize;
        set
        {
            this.bidMarketSize = value;
            Touch();
        }
    }

    internal uint bidNttMarketSize;

    /// <summary>
    ///  The aggregated NTT Market Size on Bid Size.
    /// </summary>
    public uint BidNttMarketSize
    {
        get => this.bidNttMarketSize;
        set
        {
            this.bidNttMarketSize = value;
            Touch();
        }
    }

    internal int askPrice;

    /// <summary>
    ///  Best ask price in fixed point format with 6 whole number places followed by 4 decimal digits.
    /// </summary>
    /// <remarks>
    ///  A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public decimal AskPrice
    {
        get => FixedPoint.ToDecimal(this.askPrice, AskPriceScale);
        set
        {
            this.askPrice = FixedPoint.ToRaw<int>(value, AskPriceFactor, int.MinValue, int.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AskPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public int AskPriceRaw
    {
        get => this.askPrice;
        set
        {
            this.askPrice = value;
            Touch();
        }
    }

    internal uint askSize;

    /// <summary>
    ///  Aggregated number of contracts on the ask side being displayed in the options market at the current time.
    /// </summary>
    public uint AskSize
    {
        get => this.askSize;
        set
        {
            this.askSize = value;
            Touch();
        }
    }

    internal uint askCustSize;

    /// <summary>
    ///  Customer quantity on the ask side.
    /// </summary>
    public uint AskCustSize
    {
        get => this.askCustSize;
        set
        {
            this.askCustSize = value;
            Touch();
        }
    }

    internal uint askProCustSize;

    /// <summary>
    ///  Customer professional quantity on the ask side.
    /// </summary>
    public uint AskProCustSize
    {
        get => this.askProCustSize;
        set
        {
            this.askProCustSize = value;
            Touch();
        }
    }

    internal uint askNttSize;

    /// <summary>
    ///  Limit Ask size that can’t be traded through.
    /// </summary>
    public uint AskNttSize
    {
        get => this.askNttSize;
        set
        {
            this.askNttSize = value;
            Touch();
        }
    }

    internal uint askMarketSize;

    /// <summary>
    ///  Aggregate market quantity on Ask side.
    /// </summary>
    public uint AskMarketSize
    {
        get => this.askMarketSize;
        set
        {
            this.askMarketSize = value;
            Touch();
        }
    }

    internal uint askNttMarketSize;

    /// <summary>
    ///  The aggregated NTT Market Size on Ask Size.
    /// </summary>
    public uint AskNttMarketSize
    {
        get => this.askNttMarketSize;
        set
        {
            this.askNttMarketSize = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public PacketMessageCode DataValue => PacketMessageCode.StrategyBestBidAndAskUpdate;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.quoteCondition);

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

    IMessage IMessage.Copy() => new StrategyBestBidAndAskUpdate(this);

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
        ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
        /// </summary>
        QuoteCondition,
        /// <summary>
        ///  Best bid price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        BidPrice,
        /// <summary>
        ///  Aggregated number of contracts on the bid side being displayed in the options market at the current time.
        /// </summary>
        BidSize,
        /// <summary>
        ///  Customer quantity on the bid side.
        /// </summary>
        BidCustSize,
        /// <summary>
        ///  Customer professional quantity on the bid side.
        /// </summary>
        BidProCustSize,
        /// <summary>
        ///  Limit Bid size that can’t be traded through.
        /// </summary>
        BidNttSize,
        /// <summary>
        ///  The aggregate market size on Bid side.
        /// </summary>
        BidMarketSize,
        /// <summary>
        ///  The aggregated NTT Market Size on Bid Size.
        /// </summary>
        BidNttMarketSize,
        /// <summary>
        ///  Best ask price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        AskPrice,
        /// <summary>
        ///  Aggregated number of contracts on the ask side being displayed in the options market at the current time.
        /// </summary>
        AskSize,
        /// <summary>
        ///  Customer quantity on the ask side.
        /// </summary>
        AskCustSize,
        /// <summary>
        ///  Customer professional quantity on the ask side.
        /// </summary>
        AskProCustSize,
        /// <summary>
        ///  Limit Ask size that can’t be traded through.
        /// </summary>
        AskNttSize,
        /// <summary>
        ///  Aggregate market quantity on Ask side.
        /// </summary>
        AskMarketSize,
        /// <summary>
        ///  The aggregated NTT Market Size on Ask Size.
        /// </summary>
        AskNttMarketSize,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly StrategyBestBidAndAskUpdate owner;

        internal FieldSet(StrategyBestBidAndAskUpdate owner)
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
        ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
        /// </summary>
        public FieldView QuoteCondition => this[FieldName.QuoteCondition];

        /// <summary>
        ///  Best bid price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public FieldView BidPrice => this[FieldName.BidPrice];

        /// <summary>
        ///  Aggregated number of contracts on the bid side being displayed in the options market at the current time.
        /// </summary>
        public FieldView BidSize => this[FieldName.BidSize];

        /// <summary>
        ///  Customer quantity on the bid side.
        /// </summary>
        public FieldView BidCustSize => this[FieldName.BidCustSize];

        /// <summary>
        ///  Customer professional quantity on the bid side.
        /// </summary>
        public FieldView BidProCustSize => this[FieldName.BidProCustSize];

        /// <summary>
        ///  Limit Bid size that can’t be traded through.
        /// </summary>
        public FieldView BidNttSize => this[FieldName.BidNttSize];

        /// <summary>
        ///  The aggregate market size on Bid side.
        /// </summary>
        public FieldView BidMarketSize => this[FieldName.BidMarketSize];

        /// <summary>
        ///  The aggregated NTT Market Size on Bid Size.
        /// </summary>
        public FieldView BidNttMarketSize => this[FieldName.BidNttMarketSize];

        /// <summary>
        ///  Best ask price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public FieldView AskPrice => this[FieldName.AskPrice];

        /// <summary>
        ///  Aggregated number of contracts on the ask side being displayed in the options market at the current time.
        /// </summary>
        public FieldView AskSize => this[FieldName.AskSize];

        /// <summary>
        ///  Customer quantity on the ask side.
        /// </summary>
        public FieldView AskCustSize => this[FieldName.AskCustSize];

        /// <summary>
        ///  Customer professional quantity on the ask side.
        /// </summary>
        public FieldView AskProCustSize => this[FieldName.AskProCustSize];

        /// <summary>
        ///  Limit Ask size that can’t be traded through.
        /// </summary>
        public FieldView AskNttSize => this[FieldName.AskNttSize];

        /// <summary>
        ///  Aggregate market quantity on Ask side.
        /// </summary>
        public FieldView AskMarketSize => this[FieldName.AskMarketSize];

        /// <summary>
        ///  The aggregated NTT Market Size on Ask Size.
        /// </summary>
        public FieldView AskNttMarketSize => this[FieldName.AskNttMarketSize];

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
        new(nameof(FieldName.QuoteCondition), FieldKind.Enum, QuoteConditionLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.BidPrice), FieldKind.Decimal, BidPriceLength) { Scale = BidPriceScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.BidSize), FieldKind.UnsignedInteger, BidSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BidCustSize), FieldKind.UnsignedInteger, BidCustSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BidProCustSize), FieldKind.UnsignedInteger, BidProCustSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BidNttSize), FieldKind.UnsignedInteger, BidNttSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BidMarketSize), FieldKind.UnsignedInteger, BidMarketSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.BidNttMarketSize), FieldKind.UnsignedInteger, BidNttMarketSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskPrice), FieldKind.Decimal, AskPriceLength) { Scale = AskPriceScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.AskSize), FieldKind.UnsignedInteger, AskSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskCustSize), FieldKind.UnsignedInteger, AskCustSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskProCustSize), FieldKind.UnsignedInteger, AskProCustSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskNttSize), FieldKind.UnsignedInteger, AskNttSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskMarketSize), FieldKind.UnsignedInteger, AskMarketSizeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AskNttMarketSize), FieldKind.UnsignedInteger, AskNttMarketSizeLength) { ByteOrder = "Big", IsSigned = false },
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="StrategyBestBidAndAskUpdate"/> class with default values.
    /// </summary>
    public StrategyBestBidAndAskUpdate()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="StrategyBestBidAndAskUpdate"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public StrategyBestBidAndAskUpdate(StrategyBestBidAndAskUpdate other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(StrategyBestBidAndAskUpdate other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.timestamp = other.timestamp;
        this.strategyId = other.strategyId;
        this.quoteCondition = other.quoteCondition;
        this.bidPrice = other.bidPrice;
        this.bidSize = other.bidSize;
        this.bidCustSize = other.bidCustSize;
        this.bidProCustSize = other.bidProCustSize;
        this.bidNttSize = other.bidNttSize;
        this.bidMarketSize = other.bidMarketSize;
        this.bidNttMarketSize = other.bidNttMarketSize;
        this.askPrice = other.askPrice;
        this.askSize = other.askSize;
        this.askCustSize = other.askCustSize;
        this.askProCustSize = other.askProCustSize;
        this.askNttSize = other.askNttSize;
        this.askMarketSize = other.askMarketSize;
        this.askNttMarketSize = other.askNttMarketSize;
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
        this.quoteCondition = default;
        this.bidPrice = default;
        this.bidSize = default;
        this.bidCustSize = default;
        this.bidProCustSize = default;
        this.bidNttSize = default;
        this.bidMarketSize = default;
        this.bidNttMarketSize = default;
        this.askPrice = default;
        this.askSize = default;
        this.askCustSize = default;
        this.askProCustSize = default;
        this.askNttSize = default;
        this.askMarketSize = default;
        this.askNttMarketSize = default;
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
    public static StrategyBestBidAndAskUpdate Parse(ReadOnlySpan<byte> data)
    {
        var instance = new StrategyBestBidAndAskUpdate();
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
    public static StrategyBestBidAndAskUpdate Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new StrategyBestBidAndAskUpdate();
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
        if (Truncated(data, start, cursor, QuoteConditionLength, FieldName.QuoteCondition)) return;
        this.quoteCondition = (QuoteCondition)data.Slice(cursor, QuoteConditionLength)[0];
        cursor += QuoteConditionLength;
        if (Truncated(data, start, cursor, BidPriceLength, FieldName.BidPrice)) return;
        this.bidPrice = BinaryPrimitives.ReadInt32BigEndian(data.Slice(cursor, BidPriceLength));
        cursor += BidPriceLength;
        if (Truncated(data, start, cursor, BidSizeLength, FieldName.BidSize)) return;
        this.bidSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidSizeLength));
        cursor += BidSizeLength;
        if (Truncated(data, start, cursor, BidCustSizeLength, FieldName.BidCustSize)) return;
        this.bidCustSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidCustSizeLength));
        cursor += BidCustSizeLength;
        if (Truncated(data, start, cursor, BidProCustSizeLength, FieldName.BidProCustSize)) return;
        this.bidProCustSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidProCustSizeLength));
        cursor += BidProCustSizeLength;
        if (Truncated(data, start, cursor, BidNttSizeLength, FieldName.BidNttSize)) return;
        this.bidNttSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidNttSizeLength));
        cursor += BidNttSizeLength;
        if (Truncated(data, start, cursor, BidMarketSizeLength, FieldName.BidMarketSize)) return;
        this.bidMarketSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidMarketSizeLength));
        cursor += BidMarketSizeLength;
        if (Truncated(data, start, cursor, BidNttMarketSizeLength, FieldName.BidNttMarketSize)) return;
        this.bidNttMarketSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, BidNttMarketSizeLength));
        cursor += BidNttMarketSizeLength;
        if (Truncated(data, start, cursor, AskPriceLength, FieldName.AskPrice)) return;
        this.askPrice = BinaryPrimitives.ReadInt32BigEndian(data.Slice(cursor, AskPriceLength));
        cursor += AskPriceLength;
        if (Truncated(data, start, cursor, AskSizeLength, FieldName.AskSize)) return;
        this.askSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskSizeLength));
        cursor += AskSizeLength;
        if (Truncated(data, start, cursor, AskCustSizeLength, FieldName.AskCustSize)) return;
        this.askCustSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskCustSizeLength));
        cursor += AskCustSizeLength;
        if (Truncated(data, start, cursor, AskProCustSizeLength, FieldName.AskProCustSize)) return;
        this.askProCustSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskProCustSizeLength));
        cursor += AskProCustSizeLength;
        if (Truncated(data, start, cursor, AskNttSizeLength, FieldName.AskNttSize)) return;
        this.askNttSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskNttSizeLength));
        cursor += AskNttSizeLength;
        if (Truncated(data, start, cursor, AskMarketSizeLength, FieldName.AskMarketSize)) return;
        this.askMarketSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskMarketSizeLength));
        cursor += AskMarketSizeLength;
        if (Truncated(data, start, cursor, AskNttMarketSizeLength, FieldName.AskNttMarketSize)) return;
        this.askNttMarketSize = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, AskNttMarketSizeLength));
        cursor += AskNttMarketSizeLength;
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
        data.Slice(cursor, QuoteConditionLength)[0] = unchecked((byte)(this.quoteCondition));
        cursor += QuoteConditionLength;
        BinaryPrimitives.WriteInt32BigEndian(data.Slice(cursor, BidPriceLength), this.bidPrice);
        cursor += BidPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidSizeLength), this.bidSize);
        cursor += BidSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidCustSizeLength), this.bidCustSize);
        cursor += BidCustSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidProCustSizeLength), this.bidProCustSize);
        cursor += BidProCustSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidNttSizeLength), this.bidNttSize);
        cursor += BidNttSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidMarketSizeLength), this.bidMarketSize);
        cursor += BidMarketSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, BidNttMarketSizeLength), this.bidNttMarketSize);
        cursor += BidNttMarketSizeLength;
        BinaryPrimitives.WriteInt32BigEndian(data.Slice(cursor, AskPriceLength), this.askPrice);
        cursor += AskPriceLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskSizeLength), this.askSize);
        cursor += AskSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskCustSizeLength), this.askCustSize);
        cursor += AskCustSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskProCustSizeLength), this.askProCustSize);
        cursor += AskProCustSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskNttSizeLength), this.askNttSize);
        cursor += AskNttSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskMarketSizeLength), this.askMarketSize);
        cursor += AskMarketSizeLength;
        BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, AskNttMarketSizeLength), this.askNttMarketSize);
        cursor += AskNttMarketSizeLength;
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
        if (!global::System.Enum.IsDefined(this.quoteCondition))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.QuoteCondition), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(StrategyBestBidAndAskUpdate? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.timestamp != other.timestamp) return false;
        if (this.strategyId != other.strategyId) return false;
        if (this.quoteCondition != other.quoteCondition) return false;
        if (this.bidPrice != other.bidPrice) return false;
        if (this.bidSize != other.bidSize) return false;
        if (this.bidCustSize != other.bidCustSize) return false;
        if (this.bidProCustSize != other.bidProCustSize) return false;
        if (this.bidNttSize != other.bidNttSize) return false;
        if (this.bidMarketSize != other.bidMarketSize) return false;
        if (this.bidNttMarketSize != other.bidNttMarketSize) return false;
        if (this.askPrice != other.askPrice) return false;
        if (this.askSize != other.askSize) return false;
        if (this.askCustSize != other.askCustSize) return false;
        if (this.askProCustSize != other.askProCustSize) return false;
        if (this.askNttSize != other.askNttSize) return false;
        if (this.askMarketSize != other.askMarketSize) return false;
        if (this.askNttMarketSize != other.askNttMarketSize) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is StrategyBestBidAndAskUpdate other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.timestamp);
        hash.Add(this.strategyId);
        hash.Add(this.quoteCondition);
        hash.Add(this.bidPrice);
        hash.Add(this.bidSize);
        hash.Add(this.bidCustSize);
        hash.Add(this.bidProCustSize);
        hash.Add(this.bidNttSize);
        hash.Add(this.bidMarketSize);
        hash.Add(this.bidNttMarketSize);
        hash.Add(this.askPrice);
        hash.Add(this.askSize);
        hash.Add(this.askCustSize);
        hash.Add(this.askProCustSize);
        hash.Add(this.askNttSize);
        hash.Add(this.askMarketSize);
        hash.Add(this.askNttMarketSize);
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
            if (Has(FieldName.Timestamp)) names.Add(nameof(FieldName.Timestamp));
            if (Has(FieldName.StrategyId)) names.Add(nameof(FieldName.StrategyId));
            if (Has(FieldName.QuoteCondition)) names.Add(nameof(FieldName.QuoteCondition));
            if (Has(FieldName.BidPrice)) names.Add(nameof(FieldName.BidPrice));
            if (Has(FieldName.BidSize)) names.Add(nameof(FieldName.BidSize));
            if (Has(FieldName.BidCustSize)) names.Add(nameof(FieldName.BidCustSize));
            if (Has(FieldName.BidProCustSize)) names.Add(nameof(FieldName.BidProCustSize));
            if (Has(FieldName.BidNttSize)) names.Add(nameof(FieldName.BidNttSize));
            if (Has(FieldName.BidMarketSize)) names.Add(nameof(FieldName.BidMarketSize));
            if (Has(FieldName.BidNttMarketSize)) names.Add(nameof(FieldName.BidNttMarketSize));
            if (Has(FieldName.AskPrice)) names.Add(nameof(FieldName.AskPrice));
            if (Has(FieldName.AskSize)) names.Add(nameof(FieldName.AskSize));
            if (Has(FieldName.AskCustSize)) names.Add(nameof(FieldName.AskCustSize));
            if (Has(FieldName.AskProCustSize)) names.Add(nameof(FieldName.AskProCustSize));
            if (Has(FieldName.AskNttSize)) names.Add(nameof(FieldName.AskNttSize));
            if (Has(FieldName.AskMarketSize)) names.Add(nameof(FieldName.AskMarketSize));
            if (Has(FieldName.AskNttMarketSize)) names.Add(nameof(FieldName.AskNttMarketSize));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(StrategyBestBidAndAskUpdate other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.timestamp != other.timestamp) diff = diff.With(FieldName.Timestamp);
        if (this.strategyId != other.strategyId) diff = diff.With(FieldName.StrategyId);
        if (this.quoteCondition != other.quoteCondition) diff = diff.With(FieldName.QuoteCondition);
        if (this.bidPrice != other.bidPrice) diff = diff.With(FieldName.BidPrice);
        if (this.bidSize != other.bidSize) diff = diff.With(FieldName.BidSize);
        if (this.bidCustSize != other.bidCustSize) diff = diff.With(FieldName.BidCustSize);
        if (this.bidProCustSize != other.bidProCustSize) diff = diff.With(FieldName.BidProCustSize);
        if (this.bidNttSize != other.bidNttSize) diff = diff.With(FieldName.BidNttSize);
        if (this.bidMarketSize != other.bidMarketSize) diff = diff.With(FieldName.BidMarketSize);
        if (this.bidNttMarketSize != other.bidNttMarketSize) diff = diff.With(FieldName.BidNttMarketSize);
        if (this.askPrice != other.askPrice) diff = diff.With(FieldName.AskPrice);
        if (this.askSize != other.askSize) diff = diff.With(FieldName.AskSize);
        if (this.askCustSize != other.askCustSize) diff = diff.With(FieldName.AskCustSize);
        if (this.askProCustSize != other.askProCustSize) diff = diff.With(FieldName.AskProCustSize);
        if (this.askNttSize != other.askNttSize) diff = diff.With(FieldName.AskNttSize);
        if (this.askMarketSize != other.askMarketSize) diff = diff.With(FieldName.AskMarketSize);
        if (this.askNttMarketSize != other.askNttMarketSize) diff = diff.With(FieldName.AskNttMarketSize);
        return diff;
    }

    private void CopyField(FieldName field, StrategyBestBidAndAskUpdate from)
    {
        switch (field)
        {
            case FieldName.Timestamp:
                this.timestamp = from.timestamp;
                break;
            case FieldName.StrategyId:
                this.strategyId = from.strategyId;
                break;
            case FieldName.QuoteCondition:
                this.quoteCondition = from.quoteCondition;
                break;
            case FieldName.BidPrice:
                this.bidPrice = from.bidPrice;
                break;
            case FieldName.BidSize:
                this.bidSize = from.bidSize;
                break;
            case FieldName.BidCustSize:
                this.bidCustSize = from.bidCustSize;
                break;
            case FieldName.BidProCustSize:
                this.bidProCustSize = from.bidProCustSize;
                break;
            case FieldName.BidNttSize:
                this.bidNttSize = from.bidNttSize;
                break;
            case FieldName.BidMarketSize:
                this.bidMarketSize = from.bidMarketSize;
                break;
            case FieldName.BidNttMarketSize:
                this.bidNttMarketSize = from.bidNttMarketSize;
                break;
            case FieldName.AskPrice:
                this.askPrice = from.askPrice;
                break;
            case FieldName.AskSize:
                this.askSize = from.askSize;
                break;
            case FieldName.AskCustSize:
                this.askCustSize = from.askCustSize;
                break;
            case FieldName.AskProCustSize:
                this.askProCustSize = from.askProCustSize;
                break;
            case FieldName.AskNttSize:
                this.askNttSize = from.askNttSize;
                break;
            case FieldName.AskMarketSize:
                this.askMarketSize = from.askMarketSize;
                break;
            case FieldName.AskNttMarketSize:
                this.askNttMarketSize = from.askNttMarketSize;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"StrategyBestBidAndAskUpdate {{ Timestamp: {(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}, StrategyId: {(Text.Of(this.strategyId))}, QuoteCondition: {(Text.Enum(this.quoteCondition))}, BidPrice: {(Text.Of(FixedPoint.ToDecimal(this.bidPrice, BidPriceScale)))}, BidSize: {(Text.Of(this.bidSize))}, BidCustSize: {(Text.Of(this.bidCustSize))}, BidProCustSize: {(Text.Of(this.bidProCustSize))}, BidNttSize: {(Text.Of(this.bidNttSize))}, BidMarketSize: {(Text.Of(this.bidMarketSize))}, BidNttMarketSize: {(Text.Of(this.bidNttMarketSize))}, AskPrice: {(Text.Of(FixedPoint.ToDecimal(this.askPrice, AskPriceScale)))}, AskSize: {(Text.Of(this.askSize))}, AskCustSize: {(Text.Of(this.askCustSize))}, AskProCustSize: {(Text.Of(this.askProCustSize))}, AskNttSize: {(Text.Of(this.askNttSize))}, AskMarketSize: {(Text.Of(this.askMarketSize))}, AskNttMarketSize: {(Text.Of(this.askNttMarketSize))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit))}] [{(Text.Of(this.strategyId))}] [{(Text.Enum(this.quoteCondition))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(PacketMessageCode.StrategyBestBidAndAskUpdate),
        FieldName.Timestamp => Text.Midnight(this.timestamp, TimestampNanosecondsPerUnit),
        FieldName.StrategyId => Text.Of(this.strategyId),
        FieldName.QuoteCondition => Text.Enum(this.quoteCondition),
        FieldName.BidPrice => Text.Of(FixedPoint.ToDecimal(this.bidPrice, BidPriceScale)),
        FieldName.BidSize => Text.Of(this.bidSize),
        FieldName.BidCustSize => Text.Of(this.bidCustSize),
        FieldName.BidProCustSize => Text.Of(this.bidProCustSize),
        FieldName.BidNttSize => Text.Of(this.bidNttSize),
        FieldName.BidMarketSize => Text.Of(this.bidMarketSize),
        FieldName.BidNttMarketSize => Text.Of(this.bidNttMarketSize),
        FieldName.AskPrice => Text.Of(FixedPoint.ToDecimal(this.askPrice, AskPriceScale)),
        FieldName.AskSize => Text.Of(this.askSize),
        FieldName.AskCustSize => Text.Of(this.askCustSize),
        FieldName.AskProCustSize => Text.Of(this.askProCustSize),
        FieldName.AskNttSize => Text.Of(this.askNttSize),
        FieldName.AskMarketSize => Text.Of(this.askMarketSize),
        FieldName.AskNttMarketSize => Text.Of(this.askNttMarketSize),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.QuoteCondition => global::System.Enum.IsDefined(this.quoteCondition),
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
        FieldName.QuoteCondition => QuoteConditionOffset,
        FieldName.BidPrice => BidPriceOffset,
        FieldName.BidSize => BidSizeOffset,
        FieldName.BidCustSize => BidCustSizeOffset,
        FieldName.BidProCustSize => BidProCustSizeOffset,
        FieldName.BidNttSize => BidNttSizeOffset,
        FieldName.BidMarketSize => BidMarketSizeOffset,
        FieldName.BidNttMarketSize => BidNttMarketSizeOffset,
        FieldName.AskPrice => AskPriceOffset,
        FieldName.AskSize => AskSizeOffset,
        FieldName.AskCustSize => AskCustSizeOffset,
        FieldName.AskProCustSize => AskProCustSizeOffset,
        FieldName.AskNttSize => AskNttSizeOffset,
        FieldName.AskMarketSize => AskMarketSizeOffset,
        FieldName.AskNttMarketSize => AskNttMarketSizeOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.Timestamp => TimestampLength,
        FieldName.StrategyId => StrategyIdLength,
        FieldName.QuoteCondition => QuoteConditionLength,
        FieldName.BidPrice => BidPriceLength,
        FieldName.BidSize => BidSizeLength,
        FieldName.BidCustSize => BidCustSizeLength,
        FieldName.BidProCustSize => BidProCustSizeLength,
        FieldName.BidNttSize => BidNttSizeLength,
        FieldName.BidMarketSize => BidMarketSizeLength,
        FieldName.BidNttMarketSize => BidNttMarketSizeLength,
        FieldName.AskPrice => AskPriceLength,
        FieldName.AskSize => AskSizeLength,
        FieldName.AskCustSize => AskCustSizeLength,
        FieldName.AskProCustSize => AskProCustSizeLength,
        FieldName.AskNttSize => AskNttSizeLength,
        FieldName.AskMarketSize => AskMarketSizeLength,
        FieldName.AskNttMarketSize => AskNttMarketSizeLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is StrategyBestBidAndAskUpdate typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as StrategyBestBidAndAskUpdate ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(PacketMessageCode.StrategyBestBidAndAskUpdate));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
