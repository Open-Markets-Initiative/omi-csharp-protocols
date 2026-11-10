using System.Buffers.Binary;
using System.Diagnostics;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class OrderAcceptedOptionalValue : IGroupEntry, IEquatable<OrderAcceptedOptionalValue>, IFieldOwner
{
    internal const int FirmOffset = 0;

    internal const int FirmLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Firm.
    /// </summary>
    public const byte FirmFill = (byte)' ';

    internal const int MinQtyOffset = 0;

    internal const int MinQtyLength = 4;

    internal const int CustomerTypeOffset = 0;

    internal const int CustomerTypeLength = 1;

    internal const int MaxFloorOffset = 0;

    internal const int MaxFloorLength = 4;

    internal const int PriceTypeOffset = 0;

    internal const int PriceTypeLength = 1;

    internal const int PegOffsetOffset = 0;

    internal const int PegOffsetLength = 4;

    /// <summary>
    ///  The number of decimal places in Peg Offset.
    /// </summary>
    public const byte PegOffsetScale = 4;

    /// <summary>
    ///  The factor by which <see cref="PegOffset"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PegOffsetFactor = 10000m;

    internal const int DiscretionPriceOffset = 0;

    internal const int DiscretionPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Discretion Price.
    /// </summary>
    public const byte DiscretionPriceScale = 4;

    /// <summary>
    ///  The factor by which <see cref="DiscretionPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal DiscretionPriceFactor = 10000m;

    internal const int DiscretionPriceTypeOffset = 0;

    internal const int DiscretionPriceTypeLength = 1;

    internal const int DiscretionPegOffsetOffset = 0;

    internal const int DiscretionPegOffsetLength = 4;

    /// <summary>
    ///  The number of decimal places in Discretion Peg Offset.
    /// </summary>
    public const byte DiscretionPegOffsetScale = 4;

    /// <summary>
    ///  The factor by which <see cref="DiscretionPegOffset"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal DiscretionPegOffsetFactor = 10000m;

    internal const int PostOnlyOffset = 0;

    internal const int PostOnlyLength = 1;

    internal const int RandomReservesOffset = 0;

    internal const int RandomReservesLength = 4;

    internal const int RouteOffset = 0;

    internal const int RouteLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Route.
    /// </summary>
    public const byte RouteFill = (byte)' ';

    internal const int ExpireTimeOffset = 0;

    internal const int ExpireTimeLength = 4;

    internal const int TradeNowOffset = 0;

    internal const int TradeNowLength = 1;

    internal const int HandleInstOffset = 0;

    internal const int HandleInstLength = 1;

    internal const int BboWeightIndicatorOffset = 0;

    internal const int BboWeightIndicatorLength = 1;

    internal const int GroupIdOffset = 0;

    internal const int GroupIdLength = 2;

    internal const int SharesLocatedOffset = 0;

    internal const int SharesLocatedLength = 1;

    internal const int UserRefIdxOffset = 0;

    internal const int UserRefIdxLength = 1;

    internal const int AiqStrategyOffset = 0;

    internal const int AiqStrategyLength = 1;

    internal const int AiqGroupIdOffset = 0;

    internal const int AiqGroupIdLength = 2;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = 0;

    internal string firm = string.Empty;

    /// <summary>
    ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
    /// </summary>
    public string? Firm
    {
        get => this.selectedCase == FieldName.Firm ? this.firm : null;
        set
        {
            this.firm = value is { } present ? present : string.Empty;
            Touch();
            this.selectedCase = FieldName.Firm;
        }
    }

    internal uint minQty;

    /// <summary>
    ///  MinQty must be a round lot.
    /// </summary>
    public uint? MinQty
    {
        get => this.selectedCase == FieldName.MinQty ? this.minQty : null;
        set
        {
            this.minQty = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.MinQty;
        }
    }

    internal CustomerType customerType;

    /// <summary>
    ///  CustomerType
    /// </summary>
    public CustomerType? CustomerType
    {
        get => this.selectedCase == FieldName.CustomerType ? this.customerType : null;
        set
        {
            this.customerType = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.CustomerType;
        }
    }

    internal uint maxFloor;

    /// <summary>
    ///  Represents the portion of your order that you wish to have displayed.
    /// </summary>
    public uint? MaxFloor
    {
        get => this.selectedCase == FieldName.MaxFloor ? this.maxFloor : null;
        set
        {
            this.maxFloor = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.MaxFloor;
        }
    }

    internal PriceType priceType;

    /// <summary>
    ///  PriceType
    /// </summary>
    public PriceType? PriceType
    {
        get => this.selectedCase == FieldName.PriceType ? this.priceType : null;
        set
        {
            this.priceType = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.PriceType;
        }
    }

    internal int pegOffset;

    /// <summary>
    ///  Offset amount for the pegged value.
    /// </summary>
    public decimal? PegOffset
    {
        get => this.selectedCase == FieldName.PegOffset ? FixedPoint.ToDecimal(this.pegOffset, PegOffsetScale) : null;
        set
        {
            this.pegOffset = value is { } present ? FixedPoint.ToRaw<int>(present, PegOffsetFactor, int.MinValue, int.MaxValue) : default;
            Touch();
            this.selectedCase = FieldName.PegOffset;
        }
    }

    internal ulong discretionPrice;

    /// <summary>
    ///  Discretion price for Discretionary Order.
    /// </summary>
    public decimal? DiscretionPrice
    {
        get => this.selectedCase == FieldName.DiscretionPrice ? FixedPoint.ToDecimal(this.discretionPrice, DiscretionPriceScale) : null;
        set
        {
            this.discretionPrice = value is { } present ? FixedPoint.ToRaw<ulong>(present, DiscretionPriceFactor, ulong.MinValue, ulong.MaxValue) : default;
            Touch();
            this.selectedCase = FieldName.DiscretionPrice;
        }
    }

    internal DiscretionPriceType discretionPriceType;

    /// <summary>
    ///  DiscretionPriceType
    /// </summary>
    public DiscretionPriceType? DiscretionPriceType
    {
        get => this.selectedCase == FieldName.DiscretionPriceType ? this.discretionPriceType : null;
        set
        {
            this.discretionPriceType = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.DiscretionPriceType;
        }
    }

    internal int discretionPegOffset;

    /// <summary>
    ///  Offset amount for the pegged value of the Discretionary Price.
    /// </summary>
    public decimal? DiscretionPegOffset
    {
        get => this.selectedCase == FieldName.DiscretionPegOffset ? FixedPoint.ToDecimal(this.discretionPegOffset, DiscretionPegOffsetScale) : null;
        set
        {
            this.discretionPegOffset = value is { } present ? FixedPoint.ToRaw<int>(present, DiscretionPegOffsetFactor, int.MinValue, int.MaxValue) : default;
            Touch();
            this.selectedCase = FieldName.DiscretionPegOffset;
        }
    }

    internal PostOnly postOnly;

    /// <summary>
    ///  PostOnly
    /// </summary>
    public PostOnly? PostOnly
    {
        get => this.selectedCase == FieldName.PostOnly ? this.postOnly : null;
        set
        {
            this.postOnly = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.PostOnly;
        }
    }

    internal uint randomReserves;

    /// <summary>
    ///  Shares to do random reserve with.
    /// </summary>
    public uint? RandomReserves
    {
        get => this.selectedCase == FieldName.RandomReserves ? this.randomReserves : null;
        set
        {
            this.randomReserves = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.RandomReserves;
        }
    }

    internal string route = string.Empty;

    /// <summary>
    ///  Route
    /// </summary>
    public string? Route
    {
        get => this.selectedCase == FieldName.Route ? this.route : null;
        set
        {
            this.route = value is { } present ? present : string.Empty;
            Touch();
            this.selectedCase = FieldName.Route;
        }
    }

    internal uint expireTime;

    /// <summary>
    ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
    /// </summary>
    public uint? ExpireTime
    {
        get => this.selectedCase == FieldName.ExpireTime ? this.expireTime : null;
        set
        {
            this.expireTime = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.ExpireTime;
        }
    }

    internal TradeNow tradeNow;

    /// <summary>
    ///  TradeNow
    /// </summary>
    public TradeNow? TradeNow
    {
        get => this.selectedCase == FieldName.TradeNow ? this.tradeNow : null;
        set
        {
            this.tradeNow = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.TradeNow;
        }
    }

    internal HandleInst handleInst;

    /// <summary>
    ///  HandleInst
    /// </summary>
    public HandleInst? HandleInst
    {
        get => this.selectedCase == FieldName.HandleInst ? this.handleInst : null;
        set
        {
            this.handleInst = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.HandleInst;
        }
    }

    internal BboWeightIndicator bboWeightIndicator;

    /// <summary>
    ///  BBO Weight Indicator
    /// </summary>
    public BboWeightIndicator? BboWeightIndicator
    {
        get => this.selectedCase == FieldName.BboWeightIndicator ? this.bboWeightIndicator : null;
        set
        {
            this.bboWeightIndicator = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.BboWeightIndicator;
        }
    }

    internal ushort groupId;

    /// <summary>
    ///  Customer Group ID – identifies specific entity
    /// </summary>
    public ushort? GroupId
    {
        get => this.selectedCase == FieldName.GroupId ? this.groupId : null;
        set
        {
            this.groupId = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.GroupId;
        }
    }

    internal SharesLocated sharesLocated;

    /// <summary>
    ///  Shares located for short sale order
    /// </summary>
    public SharesLocated? SharesLocated
    {
        get => this.selectedCase == FieldName.SharesLocated ? this.sharesLocated : null;
        set
        {
            this.sharesLocated = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.SharesLocated;
        }
    }

    internal byte userRefIdx;

    /// <summary>
    ///  User Reference Index – identifies the channel within the given port
    /// </summary>
    public byte? UserRefIdx
    {
        get => this.selectedCase == FieldName.UserRefIdx ? this.userRefIdx : null;
        set
        {
            this.userRefIdx = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.UserRefIdx;
        }
    }

    internal AiqStrategy aiqStrategy;

    /// <summary>
    ///  AIQ strategy used on the cancelled order.
    /// </summary>
    public AiqStrategy? AiqStrategy
    {
        get => this.selectedCase == FieldName.AiqStrategy ? this.aiqStrategy : null;
        set
        {
            this.aiqStrategy = value is { } present ? present : default;
            Touch();
            this.selectedCase = FieldName.AiqStrategy;
        }
    }

    internal string aiqGroupId = new string(default, AiqGroupIdLength);

    /// <summary>
    ///  Specified to enable self match prevention at a more granular level within a given matching level
    /// </summary>
    public string? AiqGroupId
    {
        get => this.selectedCase == FieldName.AiqGroupId ? this.aiqGroupId : null;
        set
        {
            this.aiqGroupId = value is { } present ? present : new string(default, AiqGroupIdLength);
            Touch();
            this.selectedCase = FieldName.AiqGroupId;
        }
    }

    internal FieldName? selectedCase;

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
            if (this.selectedCase == FieldName.Firm)
            {
                position += FirmLength;
            }
            if (this.selectedCase == FieldName.MinQty)
            {
                position += MinQtyLength;
            }
            if (this.selectedCase == FieldName.CustomerType)
            {
                position += CustomerTypeLength;
            }
            if (this.selectedCase == FieldName.MaxFloor)
            {
                position += MaxFloorLength;
            }
            if (this.selectedCase == FieldName.PriceType)
            {
                position += PriceTypeLength;
            }
            if (this.selectedCase == FieldName.PegOffset)
            {
                position += PegOffsetLength;
            }
            if (this.selectedCase == FieldName.DiscretionPrice)
            {
                position += DiscretionPriceLength;
            }
            if (this.selectedCase == FieldName.DiscretionPriceType)
            {
                position += DiscretionPriceTypeLength;
            }
            if (this.selectedCase == FieldName.DiscretionPegOffset)
            {
                position += DiscretionPegOffsetLength;
            }
            if (this.selectedCase == FieldName.PostOnly)
            {
                position += PostOnlyLength;
            }
            if (this.selectedCase == FieldName.RandomReserves)
            {
                position += RandomReservesLength;
            }
            if (this.selectedCase == FieldName.Route)
            {
                position += RouteLength;
            }
            if (this.selectedCase == FieldName.ExpireTime)
            {
                position += ExpireTimeLength;
            }
            if (this.selectedCase == FieldName.TradeNow)
            {
                position += TradeNowLength;
            }
            if (this.selectedCase == FieldName.HandleInst)
            {
                position += HandleInstLength;
            }
            if (this.selectedCase == FieldName.BboWeightIndicator)
            {
                position += BboWeightIndicatorLength;
            }
            if (this.selectedCase == FieldName.GroupId)
            {
                position += GroupIdLength;
            }
            if (this.selectedCase == FieldName.SharesLocated)
            {
                position += SharesLocatedLength;
            }
            if (this.selectedCase == FieldName.UserRefIdx)
            {
                position += UserRefIdxLength;
            }
            if (this.selectedCase == FieldName.AiqStrategy)
            {
                position += AiqStrategyLength;
            }
            if (this.selectedCase == FieldName.AiqGroupId)
            {
                position += AiqGroupIdLength;
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
    public bool IsRecognized => (!(this.selectedCase == FieldName.CustomerType) || global::System.Enum.IsDefined(this.customerType)) && (!(this.selectedCase == FieldName.PriceType) || global::System.Enum.IsDefined(this.priceType)) && (!(this.selectedCase == FieldName.DiscretionPriceType) || global::System.Enum.IsDefined(this.discretionPriceType)) && (!(this.selectedCase == FieldName.PostOnly) || global::System.Enum.IsDefined(this.postOnly)) && (!(this.selectedCase == FieldName.TradeNow) || global::System.Enum.IsDefined(this.tradeNow)) && (!(this.selectedCase == FieldName.HandleInst) || global::System.Enum.IsDefined(this.handleInst)) && (!(this.selectedCase == FieldName.BboWeightIndicator) || global::System.Enum.IsDefined(this.bboWeightIndicator)) && (!(this.selectedCase == FieldName.SharesLocated) || global::System.Enum.IsDefined(this.sharesLocated)) && (!(this.selectedCase == FieldName.AiqStrategy) || global::System.Enum.IsDefined(this.aiqStrategy));

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

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        Firm,
        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        MinQty,
        /// <summary>
        ///  CustomerType
        /// </summary>
        CustomerType,
        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        MaxFloor,
        /// <summary>
        ///  PriceType
        /// </summary>
        PriceType,
        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        PegOffset,
        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        DiscretionPrice,
        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        DiscretionPriceType,
        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        DiscretionPegOffset,
        /// <summary>
        ///  PostOnly
        /// </summary>
        PostOnly,
        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        RandomReserves,
        /// <summary>
        ///  Route
        /// </summary>
        Route,
        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        ExpireTime,
        /// <summary>
        ///  TradeNow
        /// </summary>
        TradeNow,
        /// <summary>
        ///  HandleInst
        /// </summary>
        HandleInst,
        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        BboWeightIndicator,
        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        GroupId,
        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        SharesLocated,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy,
        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        AiqGroupId,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly OrderAcceptedOptionalValue owner;

        internal FieldSet(OrderAcceptedOptionalValue owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        public FieldView Firm => this[FieldName.Firm];

        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        public FieldView MinQty => this[FieldName.MinQty];

        /// <summary>
        ///  CustomerType
        /// </summary>
        public FieldView CustomerType => this[FieldName.CustomerType];

        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        public FieldView MaxFloor => this[FieldName.MaxFloor];

        /// <summary>
        ///  PriceType
        /// </summary>
        public FieldView PriceType => this[FieldName.PriceType];

        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        public FieldView PegOffset => this[FieldName.PegOffset];

        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        public FieldView DiscretionPrice => this[FieldName.DiscretionPrice];

        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        public FieldView DiscretionPriceType => this[FieldName.DiscretionPriceType];

        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        public FieldView DiscretionPegOffset => this[FieldName.DiscretionPegOffset];

        /// <summary>
        ///  PostOnly
        /// </summary>
        public FieldView PostOnly => this[FieldName.PostOnly];

        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        public FieldView RandomReserves => this[FieldName.RandomReserves];

        /// <summary>
        ///  Route
        /// </summary>
        public FieldView Route => this[FieldName.Route];

        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        public FieldView ExpireTime => this[FieldName.ExpireTime];

        /// <summary>
        ///  TradeNow
        /// </summary>
        public FieldView TradeNow => this[FieldName.TradeNow];

        /// <summary>
        ///  HandleInst
        /// </summary>
        public FieldView HandleInst => this[FieldName.HandleInst];

        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        public FieldView BboWeightIndicator => this[FieldName.BboWeightIndicator];

        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        public FieldView GroupId => this[FieldName.GroupId];

        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        public FieldView SharesLocated => this[FieldName.SharesLocated];

        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        public FieldView UserRefIdx => this[FieldName.UserRefIdx];

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        public FieldView AiqStrategy => this[FieldName.AiqStrategy];

        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        public FieldView AiqGroupId => this[FieldName.AiqGroupId];

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
        new(nameof(FieldName.Firm), FieldKind.String, FirmLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.MinQty), FieldKind.UnsignedInteger, MinQtyLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.CustomerType), FieldKind.Enum, CustomerTypeLength),
        new(nameof(FieldName.MaxFloor), FieldKind.UnsignedInteger, MaxFloorLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.PriceType), FieldKind.Enum, PriceTypeLength),
        new(nameof(FieldName.PegOffset), FieldKind.Decimal, PegOffsetLength) { Scale = PegOffsetScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.DiscretionPrice), FieldKind.Decimal, DiscretionPriceLength) { Scale = DiscretionPriceScale, ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.DiscretionPriceType), FieldKind.Enum, DiscretionPriceTypeLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.DiscretionPegOffset), FieldKind.Decimal, DiscretionPegOffsetLength) { Scale = DiscretionPegOffsetScale, ByteOrder = "Big", IsSigned = true },
        new(nameof(FieldName.PostOnly), FieldKind.Enum, PostOnlyLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.RandomReserves), FieldKind.UnsignedInteger, RandomReservesLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.Route), FieldKind.String, RouteLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.ExpireTime), FieldKind.UnsignedInteger, ExpireTimeLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.TradeNow), FieldKind.Enum, TradeNowLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.HandleInst), FieldKind.Enum, HandleInstLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.BboWeightIndicator), FieldKind.Enum, BboWeightIndicatorLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.GroupId), FieldKind.UnsignedInteger, GroupIdLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.SharesLocated), FieldKind.Enum, SharesLocatedLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.UserRefIdx), FieldKind.UnsignedInteger, UserRefIdxLength) { ByteOrder = "Big", IsSigned = false },
        new(nameof(FieldName.AiqStrategy), FieldKind.Enum, AiqStrategyLength) { Fill = "Spaces", Justification = "Left" },
        new(nameof(FieldName.AiqGroupId), FieldKind.String, AiqGroupIdLength),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderAcceptedOptionalValue"/> class with default values.
    /// </summary>
    public OrderAcceptedOptionalValue()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="OrderAcceptedOptionalValue"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public OrderAcceptedOptionalValue(OrderAcceptedOptionalValue other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(OrderAcceptedOptionalValue other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.firm = other.firm;
        this.minQty = other.minQty;
        this.customerType = other.customerType;
        this.maxFloor = other.maxFloor;
        this.priceType = other.priceType;
        this.pegOffset = other.pegOffset;
        this.discretionPrice = other.discretionPrice;
        this.discretionPriceType = other.discretionPriceType;
        this.discretionPegOffset = other.discretionPegOffset;
        this.postOnly = other.postOnly;
        this.randomReserves = other.randomReserves;
        this.route = other.route;
        this.expireTime = other.expireTime;
        this.tradeNow = other.tradeNow;
        this.handleInst = other.handleInst;
        this.bboWeightIndicator = other.bboWeightIndicator;
        this.groupId = other.groupId;
        this.sharesLocated = other.sharesLocated;
        this.userRefIdx = other.userRefIdx;
        this.aiqStrategy = other.aiqStrategy;
        this.aiqGroupId = other.aiqGroupId;
        this.selectedCase = other.selectedCase;
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
        this.firm = string.Empty;
        this.minQty = default;
        this.customerType = default;
        this.maxFloor = default;
        this.priceType = default;
        this.pegOffset = default;
        this.discretionPrice = default;
        this.discretionPriceType = default;
        this.discretionPegOffset = default;
        this.postOnly = default;
        this.randomReserves = default;
        this.route = string.Empty;
        this.expireTime = default;
        this.tradeNow = default;
        this.handleInst = default;
        this.bboWeightIndicator = default;
        this.groupId = default;
        this.sharesLocated = default;
        this.userRefIdx = default;
        this.aiqStrategy = default;
        this.aiqGroupId = new string(default, AiqGroupIdLength);
        this.selectedCase = default;
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
    public static OrderAcceptedOptionalValue Parse(ReadOnlySpan<byte> data)
    {
        var instance = new OrderAcceptedOptionalValue();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static OrderAcceptedOptionalValue Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new OrderAcceptedOptionalValue();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
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
    public void ParseFrom(ReadOnlySpan<byte> data, ref int offset) => ParseFrom(data, ref offset, null);

    internal void ParseFrom(ReadOnlySpan<byte> data, ref int offset, FieldName? selectedCase)
    {
        Clear();
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        this.selectedCase = selectedCase;
        switch (this.selectedCase)
        {
            case FieldName.Firm:
                if (Truncated(data, start, cursor, FirmLength, FieldName.Firm)) return;
                this.firm = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, FirmLength), FirmFill);
                cursor += FirmLength;
                break;
            case FieldName.MinQty:
                if (Truncated(data, start, cursor, MinQtyLength, FieldName.MinQty)) return;
                this.minQty = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, MinQtyLength));
                cursor += MinQtyLength;
                break;
            case FieldName.CustomerType:
                if (Truncated(data, start, cursor, CustomerTypeLength, FieldName.CustomerType)) return;
                this.customerType = (CustomerType)data.Slice(cursor, CustomerTypeLength)[0];
                cursor += CustomerTypeLength;
                break;
            case FieldName.MaxFloor:
                if (Truncated(data, start, cursor, MaxFloorLength, FieldName.MaxFloor)) return;
                this.maxFloor = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, MaxFloorLength));
                cursor += MaxFloorLength;
                break;
            case FieldName.PriceType:
                if (Truncated(data, start, cursor, PriceTypeLength, FieldName.PriceType)) return;
                this.priceType = (PriceType)data.Slice(cursor, PriceTypeLength)[0];
                cursor += PriceTypeLength;
                break;
            case FieldName.PegOffset:
                if (Truncated(data, start, cursor, PegOffsetLength, FieldName.PegOffset)) return;
                this.pegOffset = BinaryPrimitives.ReadInt32BigEndian(data.Slice(cursor, PegOffsetLength));
                cursor += PegOffsetLength;
                break;
            case FieldName.DiscretionPrice:
                if (Truncated(data, start, cursor, DiscretionPriceLength, FieldName.DiscretionPrice)) return;
                this.discretionPrice = BinaryPrimitives.ReadUInt64BigEndian(data.Slice(cursor, DiscretionPriceLength));
                cursor += DiscretionPriceLength;
                break;
            case FieldName.DiscretionPriceType:
                if (Truncated(data, start, cursor, DiscretionPriceTypeLength, FieldName.DiscretionPriceType)) return;
                this.discretionPriceType = (DiscretionPriceType)data.Slice(cursor, DiscretionPriceTypeLength)[0];
                cursor += DiscretionPriceTypeLength;
                break;
            case FieldName.DiscretionPegOffset:
                if (Truncated(data, start, cursor, DiscretionPegOffsetLength, FieldName.DiscretionPegOffset)) return;
                this.discretionPegOffset = BinaryPrimitives.ReadInt32BigEndian(data.Slice(cursor, DiscretionPegOffsetLength));
                cursor += DiscretionPegOffsetLength;
                break;
            case FieldName.PostOnly:
                if (Truncated(data, start, cursor, PostOnlyLength, FieldName.PostOnly)) return;
                this.postOnly = (PostOnly)data.Slice(cursor, PostOnlyLength)[0];
                cursor += PostOnlyLength;
                break;
            case FieldName.RandomReserves:
                if (Truncated(data, start, cursor, RandomReservesLength, FieldName.RandomReserves)) return;
                this.randomReserves = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, RandomReservesLength));
                cursor += RandomReservesLength;
                break;
            case FieldName.Route:
                if (Truncated(data, start, cursor, RouteLength, FieldName.Route)) return;
                this.route = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, RouteLength), RouteFill);
                cursor += RouteLength;
                break;
            case FieldName.ExpireTime:
                if (Truncated(data, start, cursor, ExpireTimeLength, FieldName.ExpireTime)) return;
                this.expireTime = BinaryPrimitives.ReadUInt32BigEndian(data.Slice(cursor, ExpireTimeLength));
                cursor += ExpireTimeLength;
                break;
            case FieldName.TradeNow:
                if (Truncated(data, start, cursor, TradeNowLength, FieldName.TradeNow)) return;
                this.tradeNow = (TradeNow)data.Slice(cursor, TradeNowLength)[0];
                cursor += TradeNowLength;
                break;
            case FieldName.HandleInst:
                if (Truncated(data, start, cursor, HandleInstLength, FieldName.HandleInst)) return;
                this.handleInst = (HandleInst)data.Slice(cursor, HandleInstLength)[0];
                cursor += HandleInstLength;
                break;
            case FieldName.BboWeightIndicator:
                if (Truncated(data, start, cursor, BboWeightIndicatorLength, FieldName.BboWeightIndicator)) return;
                this.bboWeightIndicator = (BboWeightIndicator)data.Slice(cursor, BboWeightIndicatorLength)[0];
                cursor += BboWeightIndicatorLength;
                break;
            case FieldName.GroupId:
                if (Truncated(data, start, cursor, GroupIdLength, FieldName.GroupId)) return;
                this.groupId = BinaryPrimitives.ReadUInt16BigEndian(data.Slice(cursor, GroupIdLength));
                cursor += GroupIdLength;
                break;
            case FieldName.SharesLocated:
                if (Truncated(data, start, cursor, SharesLocatedLength, FieldName.SharesLocated)) return;
                this.sharesLocated = (SharesLocated)data.Slice(cursor, SharesLocatedLength)[0];
                cursor += SharesLocatedLength;
                break;
            case FieldName.UserRefIdx:
                if (Truncated(data, start, cursor, UserRefIdxLength, FieldName.UserRefIdx)) return;
                this.userRefIdx = data.Slice(cursor, UserRefIdxLength)[0];
                cursor += UserRefIdxLength;
                break;
            case FieldName.AiqStrategy:
                if (Truncated(data, start, cursor, AiqStrategyLength, FieldName.AiqStrategy)) return;
                this.aiqStrategy = (AiqStrategy)data.Slice(cursor, AiqStrategyLength)[0];
                cursor += AiqStrategyLength;
                break;
            case FieldName.AiqGroupId:
                if (Truncated(data, start, cursor, AiqGroupIdLength, FieldName.AiqGroupId)) return;
                this.aiqGroupId = TextEncoding.Ascii.Read(data.Slice(cursor, AiqGroupIdLength));
                cursor += AiqGroupIdLength;
                break;
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
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < EncodedByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IGroupEntry)this).Write(data, offset, out var cursor, out var failure))
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
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the entry into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the entry was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < EncodedByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IGroupEntry)this).Write(data, 0, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IGroupEntry.Write(Span<byte> data, int offset, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        switch (this.selectedCase)
        {
            case FieldName.Firm:
                TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, FirmLength), TextEncoding.Ascii.Fit(this.firm, FirmLength), FirmFill);
                cursor += FirmLength;
                break;
            case FieldName.MinQty:
                BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, MinQtyLength), this.minQty);
                cursor += MinQtyLength;
                break;
            case FieldName.CustomerType:
                data.Slice(cursor, CustomerTypeLength)[0] = unchecked((byte)(this.customerType));
                cursor += CustomerTypeLength;
                break;
            case FieldName.MaxFloor:
                BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, MaxFloorLength), this.maxFloor);
                cursor += MaxFloorLength;
                break;
            case FieldName.PriceType:
                data.Slice(cursor, PriceTypeLength)[0] = unchecked((byte)(this.priceType));
                cursor += PriceTypeLength;
                break;
            case FieldName.PegOffset:
                BinaryPrimitives.WriteInt32BigEndian(data.Slice(cursor, PegOffsetLength), this.pegOffset);
                cursor += PegOffsetLength;
                break;
            case FieldName.DiscretionPrice:
                BinaryPrimitives.WriteUInt64BigEndian(data.Slice(cursor, DiscretionPriceLength), this.discretionPrice);
                cursor += DiscretionPriceLength;
                break;
            case FieldName.DiscretionPriceType:
                data.Slice(cursor, DiscretionPriceTypeLength)[0] = unchecked((byte)(this.discretionPriceType));
                cursor += DiscretionPriceTypeLength;
                break;
            case FieldName.DiscretionPegOffset:
                BinaryPrimitives.WriteInt32BigEndian(data.Slice(cursor, DiscretionPegOffsetLength), this.discretionPegOffset);
                cursor += DiscretionPegOffsetLength;
                break;
            case FieldName.PostOnly:
                data.Slice(cursor, PostOnlyLength)[0] = unchecked((byte)(this.postOnly));
                cursor += PostOnlyLength;
                break;
            case FieldName.RandomReserves:
                BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, RandomReservesLength), this.randomReserves);
                cursor += RandomReservesLength;
                break;
            case FieldName.Route:
                TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, RouteLength), TextEncoding.Ascii.Fit(this.route, RouteLength), RouteFill);
                cursor += RouteLength;
                break;
            case FieldName.ExpireTime:
                BinaryPrimitives.WriteUInt32BigEndian(data.Slice(cursor, ExpireTimeLength), this.expireTime);
                cursor += ExpireTimeLength;
                break;
            case FieldName.TradeNow:
                data.Slice(cursor, TradeNowLength)[0] = unchecked((byte)(this.tradeNow));
                cursor += TradeNowLength;
                break;
            case FieldName.HandleInst:
                data.Slice(cursor, HandleInstLength)[0] = unchecked((byte)(this.handleInst));
                cursor += HandleInstLength;
                break;
            case FieldName.BboWeightIndicator:
                data.Slice(cursor, BboWeightIndicatorLength)[0] = unchecked((byte)(this.bboWeightIndicator));
                cursor += BboWeightIndicatorLength;
                break;
            case FieldName.GroupId:
                BinaryPrimitives.WriteUInt16BigEndian(data.Slice(cursor, GroupIdLength), this.groupId);
                cursor += GroupIdLength;
                break;
            case FieldName.SharesLocated:
                data.Slice(cursor, SharesLocatedLength)[0] = unchecked((byte)(this.sharesLocated));
                cursor += SharesLocatedLength;
                break;
            case FieldName.UserRefIdx:
                data.Slice(cursor, UserRefIdxLength)[0] = unchecked((byte)(this.userRefIdx));
                cursor += UserRefIdxLength;
                break;
            case FieldName.AiqStrategy:
                data.Slice(cursor, AiqStrategyLength)[0] = unchecked((byte)(this.aiqStrategy));
                cursor += AiqStrategyLength;
                break;
            case FieldName.AiqGroupId:
                if (TextEncoding.Ascii.ByteCount(TextEncoding.Ascii.Fit(this.aiqGroupId, AiqGroupIdLength)) < AiqGroupIdLength)
                {
                    failure = new EncodeFailure(new FieldView(this, (int)FieldName.AiqGroupId), EncodeStatus.DoesNotFit);
                    return false;
                }
                TextEncoding.Ascii.Write(data.Slice(cursor, AiqGroupIdLength), TextEncoding.Ascii.Fit(this.aiqGroupId, AiqGroupIdLength));
                cursor += AiqGroupIdLength;
                break;
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
                if (!((IGroupEntry)this).Write(buffer, 0, out _, out _))
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
        if (this.selectedCase == FieldName.Firm && !global::System.Text.Ascii.IsValid(this.firm))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Firm), ValidationStatus.NonAsciiText));
        }
        if (!(!(this.selectedCase == FieldName.CustomerType) || global::System.Enum.IsDefined(this.customerType)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.CustomerType), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.PriceType) || global::System.Enum.IsDefined(this.priceType)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.PriceType), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.DiscretionPriceType) || global::System.Enum.IsDefined(this.discretionPriceType)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.DiscretionPriceType), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.PostOnly) || global::System.Enum.IsDefined(this.postOnly)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.PostOnly), ValidationStatus.UndeclaredValue));
        }
        if (this.selectedCase == FieldName.Route && !global::System.Text.Ascii.IsValid(this.route))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Route), ValidationStatus.NonAsciiText));
        }
        if (!(!(this.selectedCase == FieldName.TradeNow) || global::System.Enum.IsDefined(this.tradeNow)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.TradeNow), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.HandleInst) || global::System.Enum.IsDefined(this.handleInst)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.HandleInst), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.BboWeightIndicator) || global::System.Enum.IsDefined(this.bboWeightIndicator)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.BboWeightIndicator), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.SharesLocated) || global::System.Enum.IsDefined(this.sharesLocated)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SharesLocated), ValidationStatus.UndeclaredValue));
        }
        if (!(!(this.selectedCase == FieldName.AiqStrategy) || global::System.Enum.IsDefined(this.aiqStrategy)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AiqStrategy), ValidationStatus.UndeclaredValue));
        }
        if (this.selectedCase == FieldName.AiqGroupId && !global::System.Text.Ascii.IsValid(this.aiqGroupId))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AiqGroupId), ValidationStatus.NonAsciiText));
        }
        if (this.selectedCase == FieldName.AiqGroupId && TextEncoding.Ascii.ByteCount(TextEncoding.Ascii.Fit(this.aiqGroupId, AiqGroupIdLength)) < AiqGroupIdLength)
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AiqGroupId), ValidationStatus.TextTooShort));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(OrderAcceptedOptionalValue? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.Firm != other.Firm) return false;
        if (this.MinQty != other.MinQty) return false;
        if (this.CustomerType != other.CustomerType) return false;
        if (this.MaxFloor != other.MaxFloor) return false;
        if (this.PriceType != other.PriceType) return false;
        if (this.PegOffset != other.PegOffset) return false;
        if (this.DiscretionPrice != other.DiscretionPrice) return false;
        if (this.DiscretionPriceType != other.DiscretionPriceType) return false;
        if (this.DiscretionPegOffset != other.DiscretionPegOffset) return false;
        if (this.PostOnly != other.PostOnly) return false;
        if (this.RandomReserves != other.RandomReserves) return false;
        if (this.Route != other.Route) return false;
        if (this.ExpireTime != other.ExpireTime) return false;
        if (this.TradeNow != other.TradeNow) return false;
        if (this.HandleInst != other.HandleInst) return false;
        if (this.BboWeightIndicator != other.BboWeightIndicator) return false;
        if (this.GroupId != other.GroupId) return false;
        if (this.SharesLocated != other.SharesLocated) return false;
        if (this.UserRefIdx != other.UserRefIdx) return false;
        if (this.AiqStrategy != other.AiqStrategy) return false;
        if (this.AiqGroupId != other.AiqGroupId) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is OrderAcceptedOptionalValue other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.Firm);
        hash.Add(this.MinQty);
        hash.Add(this.CustomerType);
        hash.Add(this.MaxFloor);
        hash.Add(this.PriceType);
        hash.Add(this.PegOffset);
        hash.Add(this.DiscretionPrice);
        hash.Add(this.DiscretionPriceType);
        hash.Add(this.DiscretionPegOffset);
        hash.Add(this.PostOnly);
        hash.Add(this.RandomReserves);
        hash.Add(this.Route);
        hash.Add(this.ExpireTime);
        hash.Add(this.TradeNow);
        hash.Add(this.HandleInst);
        hash.Add(this.BboWeightIndicator);
        hash.Add(this.GroupId);
        hash.Add(this.SharesLocated);
        hash.Add(this.UserRefIdx);
        hash.Add(this.AiqStrategy);
        hash.Add(this.AiqGroupId);
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
        private const int FieldCount = 21;

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
            if (Has(FieldName.Firm)) names.Add(nameof(FieldName.Firm));
            if (Has(FieldName.MinQty)) names.Add(nameof(FieldName.MinQty));
            if (Has(FieldName.CustomerType)) names.Add(nameof(FieldName.CustomerType));
            if (Has(FieldName.MaxFloor)) names.Add(nameof(FieldName.MaxFloor));
            if (Has(FieldName.PriceType)) names.Add(nameof(FieldName.PriceType));
            if (Has(FieldName.PegOffset)) names.Add(nameof(FieldName.PegOffset));
            if (Has(FieldName.DiscretionPrice)) names.Add(nameof(FieldName.DiscretionPrice));
            if (Has(FieldName.DiscretionPriceType)) names.Add(nameof(FieldName.DiscretionPriceType));
            if (Has(FieldName.DiscretionPegOffset)) names.Add(nameof(FieldName.DiscretionPegOffset));
            if (Has(FieldName.PostOnly)) names.Add(nameof(FieldName.PostOnly));
            if (Has(FieldName.RandomReserves)) names.Add(nameof(FieldName.RandomReserves));
            if (Has(FieldName.Route)) names.Add(nameof(FieldName.Route));
            if (Has(FieldName.ExpireTime)) names.Add(nameof(FieldName.ExpireTime));
            if (Has(FieldName.TradeNow)) names.Add(nameof(FieldName.TradeNow));
            if (Has(FieldName.HandleInst)) names.Add(nameof(FieldName.HandleInst));
            if (Has(FieldName.BboWeightIndicator)) names.Add(nameof(FieldName.BboWeightIndicator));
            if (Has(FieldName.GroupId)) names.Add(nameof(FieldName.GroupId));
            if (Has(FieldName.SharesLocated)) names.Add(nameof(FieldName.SharesLocated));
            if (Has(FieldName.UserRefIdx)) names.Add(nameof(FieldName.UserRefIdx));
            if (Has(FieldName.AiqStrategy)) names.Add(nameof(FieldName.AiqStrategy));
            if (Has(FieldName.AiqGroupId)) names.Add(nameof(FieldName.AiqGroupId));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(OrderAcceptedOptionalValue other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.Firm != other.Firm) diff = diff.With(FieldName.Firm);
        if (this.MinQty != other.MinQty) diff = diff.With(FieldName.MinQty);
        if (this.CustomerType != other.CustomerType) diff = diff.With(FieldName.CustomerType);
        if (this.MaxFloor != other.MaxFloor) diff = diff.With(FieldName.MaxFloor);
        if (this.PriceType != other.PriceType) diff = diff.With(FieldName.PriceType);
        if (this.PegOffset != other.PegOffset) diff = diff.With(FieldName.PegOffset);
        if (this.DiscretionPrice != other.DiscretionPrice) diff = diff.With(FieldName.DiscretionPrice);
        if (this.DiscretionPriceType != other.DiscretionPriceType) diff = diff.With(FieldName.DiscretionPriceType);
        if (this.DiscretionPegOffset != other.DiscretionPegOffset) diff = diff.With(FieldName.DiscretionPegOffset);
        if (this.PostOnly != other.PostOnly) diff = diff.With(FieldName.PostOnly);
        if (this.RandomReserves != other.RandomReserves) diff = diff.With(FieldName.RandomReserves);
        if (this.Route != other.Route) diff = diff.With(FieldName.Route);
        if (this.ExpireTime != other.ExpireTime) diff = diff.With(FieldName.ExpireTime);
        if (this.TradeNow != other.TradeNow) diff = diff.With(FieldName.TradeNow);
        if (this.HandleInst != other.HandleInst) diff = diff.With(FieldName.HandleInst);
        if (this.BboWeightIndicator != other.BboWeightIndicator) diff = diff.With(FieldName.BboWeightIndicator);
        if (this.GroupId != other.GroupId) diff = diff.With(FieldName.GroupId);
        if (this.SharesLocated != other.SharesLocated) diff = diff.With(FieldName.SharesLocated);
        if (this.UserRefIdx != other.UserRefIdx) diff = diff.With(FieldName.UserRefIdx);
        if (this.AiqStrategy != other.AiqStrategy) diff = diff.With(FieldName.AiqStrategy);
        if (this.AiqGroupId != other.AiqGroupId) diff = diff.With(FieldName.AiqGroupId);
        return diff;
    }

    private void CopyField(FieldName field, OrderAcceptedOptionalValue from)
    {
        switch (field)
        {
            case FieldName.Firm:
                this.firm = from.firm;
                if (from.selectedCase == FieldName.Firm)
                {
                    this.selectedCase = FieldName.Firm;
                }
                else if (this.selectedCase == FieldName.Firm)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.MinQty:
                this.minQty = from.minQty;
                if (from.selectedCase == FieldName.MinQty)
                {
                    this.selectedCase = FieldName.MinQty;
                }
                else if (this.selectedCase == FieldName.MinQty)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.CustomerType:
                this.customerType = from.customerType;
                if (from.selectedCase == FieldName.CustomerType)
                {
                    this.selectedCase = FieldName.CustomerType;
                }
                else if (this.selectedCase == FieldName.CustomerType)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.MaxFloor:
                this.maxFloor = from.maxFloor;
                if (from.selectedCase == FieldName.MaxFloor)
                {
                    this.selectedCase = FieldName.MaxFloor;
                }
                else if (this.selectedCase == FieldName.MaxFloor)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.PriceType:
                this.priceType = from.priceType;
                if (from.selectedCase == FieldName.PriceType)
                {
                    this.selectedCase = FieldName.PriceType;
                }
                else if (this.selectedCase == FieldName.PriceType)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.PegOffset:
                this.pegOffset = from.pegOffset;
                if (from.selectedCase == FieldName.PegOffset)
                {
                    this.selectedCase = FieldName.PegOffset;
                }
                else if (this.selectedCase == FieldName.PegOffset)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.DiscretionPrice:
                this.discretionPrice = from.discretionPrice;
                if (from.selectedCase == FieldName.DiscretionPrice)
                {
                    this.selectedCase = FieldName.DiscretionPrice;
                }
                else if (this.selectedCase == FieldName.DiscretionPrice)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.DiscretionPriceType:
                this.discretionPriceType = from.discretionPriceType;
                if (from.selectedCase == FieldName.DiscretionPriceType)
                {
                    this.selectedCase = FieldName.DiscretionPriceType;
                }
                else if (this.selectedCase == FieldName.DiscretionPriceType)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.DiscretionPegOffset:
                this.discretionPegOffset = from.discretionPegOffset;
                if (from.selectedCase == FieldName.DiscretionPegOffset)
                {
                    this.selectedCase = FieldName.DiscretionPegOffset;
                }
                else if (this.selectedCase == FieldName.DiscretionPegOffset)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.PostOnly:
                this.postOnly = from.postOnly;
                if (from.selectedCase == FieldName.PostOnly)
                {
                    this.selectedCase = FieldName.PostOnly;
                }
                else if (this.selectedCase == FieldName.PostOnly)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.RandomReserves:
                this.randomReserves = from.randomReserves;
                if (from.selectedCase == FieldName.RandomReserves)
                {
                    this.selectedCase = FieldName.RandomReserves;
                }
                else if (this.selectedCase == FieldName.RandomReserves)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.Route:
                this.route = from.route;
                if (from.selectedCase == FieldName.Route)
                {
                    this.selectedCase = FieldName.Route;
                }
                else if (this.selectedCase == FieldName.Route)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.ExpireTime:
                this.expireTime = from.expireTime;
                if (from.selectedCase == FieldName.ExpireTime)
                {
                    this.selectedCase = FieldName.ExpireTime;
                }
                else if (this.selectedCase == FieldName.ExpireTime)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.TradeNow:
                this.tradeNow = from.tradeNow;
                if (from.selectedCase == FieldName.TradeNow)
                {
                    this.selectedCase = FieldName.TradeNow;
                }
                else if (this.selectedCase == FieldName.TradeNow)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.HandleInst:
                this.handleInst = from.handleInst;
                if (from.selectedCase == FieldName.HandleInst)
                {
                    this.selectedCase = FieldName.HandleInst;
                }
                else if (this.selectedCase == FieldName.HandleInst)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.BboWeightIndicator:
                this.bboWeightIndicator = from.bboWeightIndicator;
                if (from.selectedCase == FieldName.BboWeightIndicator)
                {
                    this.selectedCase = FieldName.BboWeightIndicator;
                }
                else if (this.selectedCase == FieldName.BboWeightIndicator)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.GroupId:
                this.groupId = from.groupId;
                if (from.selectedCase == FieldName.GroupId)
                {
                    this.selectedCase = FieldName.GroupId;
                }
                else if (this.selectedCase == FieldName.GroupId)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.SharesLocated:
                this.sharesLocated = from.sharesLocated;
                if (from.selectedCase == FieldName.SharesLocated)
                {
                    this.selectedCase = FieldName.SharesLocated;
                }
                else if (this.selectedCase == FieldName.SharesLocated)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.UserRefIdx:
                this.userRefIdx = from.userRefIdx;
                if (from.selectedCase == FieldName.UserRefIdx)
                {
                    this.selectedCase = FieldName.UserRefIdx;
                }
                else if (this.selectedCase == FieldName.UserRefIdx)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.AiqStrategy:
                this.aiqStrategy = from.aiqStrategy;
                if (from.selectedCase == FieldName.AiqStrategy)
                {
                    this.selectedCase = FieldName.AiqStrategy;
                }
                else if (this.selectedCase == FieldName.AiqStrategy)
                {
                    this.selectedCase = null;
                }
                break;
            case FieldName.AiqGroupId:
                this.aiqGroupId = from.aiqGroupId;
                if (from.selectedCase == FieldName.AiqGroupId)
                {
                    this.selectedCase = FieldName.AiqGroupId;
                }
                else if (this.selectedCase == FieldName.AiqGroupId)
                {
                    this.selectedCase = null;
                }
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"OrderAcceptedOptionalValue {{ Firm: {(Text.Of(this.Firm))}, MinQty: {(Text.Of(this.MinQty))}, CustomerType: {(Text.Enum(this.CustomerType))}, MaxFloor: {(Text.Of(this.MaxFloor))}, PriceType: {(Text.Enum(this.PriceType))}, PegOffset: {(Text.Of(this.PegOffset))}, DiscretionPrice: {(Text.Of(this.DiscretionPrice))}, DiscretionPriceType: {(Text.Enum(this.DiscretionPriceType))}, DiscretionPegOffset: {(Text.Of(this.DiscretionPegOffset))}, PostOnly: {(Text.Enum(this.PostOnly))}, RandomReserves: {(Text.Of(this.RandomReserves))}, Route: {(Text.Of(this.Route))}, ExpireTime: {(Text.Of(this.ExpireTime))}, TradeNow: {(Text.Enum(this.TradeNow))}, HandleInst: {(Text.Enum(this.HandleInst))}, BboWeightIndicator: {(Text.Enum(this.BboWeightIndicator))}, GroupId: {(Text.Of(this.GroupId))}, SharesLocated: {(Text.Enum(this.SharesLocated))}, UserRefIdx: {(Text.Of(this.UserRefIdx))}, AiqStrategy: {(Text.Enum(this.AiqStrategy))}, AiqGroupId: {(Text.Of(this.AiqGroupId))} }}";

    private string DebuggerDisplay => $"OrderAcceptedOptionalValue";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.Firm => Text.Of(this.Firm),
        FieldName.MinQty => Text.Of(this.MinQty),
        FieldName.CustomerType => Text.Enum(this.CustomerType),
        FieldName.MaxFloor => Text.Of(this.MaxFloor),
        FieldName.PriceType => Text.Enum(this.PriceType),
        FieldName.PegOffset => Text.Of(this.PegOffset),
        FieldName.DiscretionPrice => Text.Of(this.DiscretionPrice),
        FieldName.DiscretionPriceType => Text.Enum(this.DiscretionPriceType),
        FieldName.DiscretionPegOffset => Text.Of(this.DiscretionPegOffset),
        FieldName.PostOnly => Text.Enum(this.PostOnly),
        FieldName.RandomReserves => Text.Of(this.RandomReserves),
        FieldName.Route => Text.Of(this.Route),
        FieldName.ExpireTime => Text.Of(this.ExpireTime),
        FieldName.TradeNow => Text.Enum(this.TradeNow),
        FieldName.HandleInst => Text.Enum(this.HandleInst),
        FieldName.BboWeightIndicator => Text.Enum(this.BboWeightIndicator),
        FieldName.GroupId => Text.Of(this.GroupId),
        FieldName.SharesLocated => Text.Enum(this.SharesLocated),
        FieldName.UserRefIdx => Text.Of(this.UserRefIdx),
        FieldName.AiqStrategy => Text.Enum(this.AiqStrategy),
        FieldName.AiqGroupId => Text.Of(this.AiqGroupId),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.CustomerType => (!(this.selectedCase == FieldName.CustomerType) || global::System.Enum.IsDefined(this.customerType)),
        FieldName.PriceType => (!(this.selectedCase == FieldName.PriceType) || global::System.Enum.IsDefined(this.priceType)),
        FieldName.DiscretionPriceType => (!(this.selectedCase == FieldName.DiscretionPriceType) || global::System.Enum.IsDefined(this.discretionPriceType)),
        FieldName.PostOnly => (!(this.selectedCase == FieldName.PostOnly) || global::System.Enum.IsDefined(this.postOnly)),
        FieldName.TradeNow => (!(this.selectedCase == FieldName.TradeNow) || global::System.Enum.IsDefined(this.tradeNow)),
        FieldName.HandleInst => (!(this.selectedCase == FieldName.HandleInst) || global::System.Enum.IsDefined(this.handleInst)),
        FieldName.BboWeightIndicator => (!(this.selectedCase == FieldName.BboWeightIndicator) || global::System.Enum.IsDefined(this.bboWeightIndicator)),
        FieldName.SharesLocated => (!(this.selectedCase == FieldName.SharesLocated) || global::System.Enum.IsDefined(this.sharesLocated)),
        FieldName.AiqStrategy => (!(this.selectedCase == FieldName.AiqStrategy) || global::System.Enum.IsDefined(this.aiqStrategy)),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.Firm => this.selectedCase == FieldName.Firm,
        FieldName.MinQty => this.selectedCase == FieldName.MinQty,
        FieldName.CustomerType => this.selectedCase == FieldName.CustomerType,
        FieldName.MaxFloor => this.selectedCase == FieldName.MaxFloor,
        FieldName.PriceType => this.selectedCase == FieldName.PriceType,
        FieldName.PegOffset => this.selectedCase == FieldName.PegOffset,
        FieldName.DiscretionPrice => this.selectedCase == FieldName.DiscretionPrice,
        FieldName.DiscretionPriceType => this.selectedCase == FieldName.DiscretionPriceType,
        FieldName.DiscretionPegOffset => this.selectedCase == FieldName.DiscretionPegOffset,
        FieldName.PostOnly => this.selectedCase == FieldName.PostOnly,
        FieldName.RandomReserves => this.selectedCase == FieldName.RandomReserves,
        FieldName.Route => this.selectedCase == FieldName.Route,
        FieldName.ExpireTime => this.selectedCase == FieldName.ExpireTime,
        FieldName.TradeNow => this.selectedCase == FieldName.TradeNow,
        FieldName.HandleInst => this.selectedCase == FieldName.HandleInst,
        FieldName.BboWeightIndicator => this.selectedCase == FieldName.BboWeightIndicator,
        FieldName.GroupId => this.selectedCase == FieldName.GroupId,
        FieldName.SharesLocated => this.selectedCase == FieldName.SharesLocated,
        FieldName.UserRefIdx => this.selectedCase == FieldName.UserRefIdx,
        FieldName.AiqStrategy => this.selectedCase == FieldName.AiqStrategy,
        FieldName.AiqGroupId => this.selectedCase == FieldName.AiqGroupId,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.Firm => FirmOffset,
        FieldName.MinQty => MinQtyOffset,
        FieldName.CustomerType => CustomerTypeOffset,
        FieldName.MaxFloor => MaxFloorOffset,
        FieldName.PriceType => PriceTypeOffset,
        FieldName.PegOffset => PegOffsetOffset,
        FieldName.DiscretionPrice => DiscretionPriceOffset,
        FieldName.DiscretionPriceType => DiscretionPriceTypeOffset,
        FieldName.DiscretionPegOffset => DiscretionPegOffsetOffset,
        FieldName.PostOnly => PostOnlyOffset,
        FieldName.RandomReserves => RandomReservesOffset,
        FieldName.Route => RouteOffset,
        FieldName.ExpireTime => ExpireTimeOffset,
        FieldName.TradeNow => TradeNowOffset,
        FieldName.HandleInst => HandleInstOffset,
        FieldName.BboWeightIndicator => BboWeightIndicatorOffset,
        FieldName.GroupId => GroupIdOffset,
        FieldName.SharesLocated => SharesLocatedOffset,
        FieldName.UserRefIdx => UserRefIdxOffset,
        FieldName.AiqStrategy => AiqStrategyOffset,
        FieldName.AiqGroupId => AiqGroupIdOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.Firm => this.selectedCase == FieldName.Firm ? FirmLength : 0,
        FieldName.MinQty => this.selectedCase == FieldName.MinQty ? MinQtyLength : 0,
        FieldName.CustomerType => this.selectedCase == FieldName.CustomerType ? CustomerTypeLength : 0,
        FieldName.MaxFloor => this.selectedCase == FieldName.MaxFloor ? MaxFloorLength : 0,
        FieldName.PriceType => this.selectedCase == FieldName.PriceType ? PriceTypeLength : 0,
        FieldName.PegOffset => this.selectedCase == FieldName.PegOffset ? PegOffsetLength : 0,
        FieldName.DiscretionPrice => this.selectedCase == FieldName.DiscretionPrice ? DiscretionPriceLength : 0,
        FieldName.DiscretionPriceType => this.selectedCase == FieldName.DiscretionPriceType ? DiscretionPriceTypeLength : 0,
        FieldName.DiscretionPegOffset => this.selectedCase == FieldName.DiscretionPegOffset ? DiscretionPegOffsetLength : 0,
        FieldName.PostOnly => this.selectedCase == FieldName.PostOnly ? PostOnlyLength : 0,
        FieldName.RandomReserves => this.selectedCase == FieldName.RandomReserves ? RandomReservesLength : 0,
        FieldName.Route => this.selectedCase == FieldName.Route ? RouteLength : 0,
        FieldName.ExpireTime => this.selectedCase == FieldName.ExpireTime ? ExpireTimeLength : 0,
        FieldName.TradeNow => this.selectedCase == FieldName.TradeNow ? TradeNowLength : 0,
        FieldName.HandleInst => this.selectedCase == FieldName.HandleInst ? HandleInstLength : 0,
        FieldName.BboWeightIndicator => this.selectedCase == FieldName.BboWeightIndicator ? BboWeightIndicatorLength : 0,
        FieldName.GroupId => this.selectedCase == FieldName.GroupId ? GroupIdLength : 0,
        FieldName.SharesLocated => this.selectedCase == FieldName.SharesLocated ? SharesLocatedLength : 0,
        FieldName.UserRefIdx => this.selectedCase == FieldName.UserRefIdx ? UserRefIdxLength : 0,
        FieldName.AiqStrategy => this.selectedCase == FieldName.AiqStrategy ? AiqStrategyLength : 0,
        FieldName.AiqGroupId => this.selectedCase == FieldName.AiqGroupId ? AiqGroupIdLength : 0,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is OrderAcceptedOptionalValue typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as OrderAcceptedOptionalValue ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
