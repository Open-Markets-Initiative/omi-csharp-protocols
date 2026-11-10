using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  An auction imbalance message; the record of the imbalance schema
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class ImbalanceMsg : IFileMessage, IGatewayMessage, IEquatable<ImbalanceMsg>, IFieldOwner
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

    internal const int RefPriceOffset = TsRecvOffset + TsRecvLength;

    internal const int RefPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Ref Price.
    /// </summary>
    public const byte RefPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="RefPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal RefPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Ref Price that means no value.
    /// </summary>
    public const long RefPriceNullValue = 9223372036854775807L;

    internal const int AuctionTimeOffset = RefPriceOffset + RefPriceLength;

    internal const int AuctionTimeLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="AuctionTime"/>.
    /// </summary>
    public const long AuctionTimeNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Auction Time that means no value.
    /// </summary>
    public const ulong AuctionTimeNullValue = 18446744073709551615UL;

    internal const int ContBookClrPriceOffset = AuctionTimeOffset + AuctionTimeLength;

    internal const int ContBookClrPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Cont Book Clr Price.
    /// </summary>
    public const byte ContBookClrPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="ContBookClrPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal ContBookClrPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Cont Book Clr Price that means no value.
    /// </summary>
    public const long ContBookClrPriceNullValue = 9223372036854775807L;

    internal const int AuctInterestClrPriceOffset = ContBookClrPriceOffset + ContBookClrPriceLength;

    internal const int AuctInterestClrPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Auct Interest Clr Price.
    /// </summary>
    public const byte AuctInterestClrPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="AuctInterestClrPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal AuctInterestClrPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Auct Interest Clr Price that means no value.
    /// </summary>
    public const long AuctInterestClrPriceNullValue = 9223372036854775807L;

    internal const int SsrFillingPriceOffset = AuctInterestClrPriceOffset + AuctInterestClrPriceLength;

    internal const int SsrFillingPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Ssr Filling Price.
    /// </summary>
    public const byte SsrFillingPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="SsrFillingPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal SsrFillingPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Ssr Filling Price that means no value.
    /// </summary>
    public const long SsrFillingPriceNullValue = 9223372036854775807L;

    internal const int IndMatchPriceOffset = SsrFillingPriceOffset + SsrFillingPriceLength;

    internal const int IndMatchPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Ind Match Price.
    /// </summary>
    public const byte IndMatchPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="IndMatchPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal IndMatchPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Ind Match Price that means no value.
    /// </summary>
    public const long IndMatchPriceNullValue = 9223372036854775807L;

    internal const int UpperCollarOffset = IndMatchPriceOffset + IndMatchPriceLength;

    internal const int UpperCollarLength = 8;

    /// <summary>
    ///  The number of decimal places in Upper Collar.
    /// </summary>
    public const byte UpperCollarScale = 9;

    /// <summary>
    ///  The factor by which <see cref="UpperCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal UpperCollarFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Upper Collar that means no value.
    /// </summary>
    public const long UpperCollarNullValue = 9223372036854775807L;

    internal const int LowerCollarOffset = UpperCollarOffset + UpperCollarLength;

    internal const int LowerCollarLength = 8;

    /// <summary>
    ///  The number of decimal places in Lower Collar.
    /// </summary>
    public const byte LowerCollarScale = 9;

    /// <summary>
    ///  The factor by which <see cref="LowerCollar"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LowerCollarFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Lower Collar that means no value.
    /// </summary>
    public const long LowerCollarNullValue = 9223372036854775807L;

    internal const int PairedQtyOffset = LowerCollarOffset + LowerCollarLength;

    internal const int PairedQtyLength = 4;

    internal const int TotalImbalanceQtyOffset = PairedQtyOffset + PairedQtyLength;

    internal const int TotalImbalanceQtyLength = 4;

    internal const int MarketImbalanceQtyOffset = TotalImbalanceQtyOffset + TotalImbalanceQtyLength;

    internal const int MarketImbalanceQtyLength = 4;

    internal const int UnpairedQtyOffset = MarketImbalanceQtyOffset + MarketImbalanceQtyLength;

    internal const int UnpairedQtyLength = 4;

    internal const int AuctionTypeOffset = UnpairedQtyOffset + UnpairedQtyLength;

    internal const int AuctionTypeLength = 1;

    internal const int SideOffset = AuctionTypeOffset + AuctionTypeLength;

    internal const int SideLength = 1;

    internal const int AuctionStatusOffset = SideOffset + SideLength;

    internal const int AuctionStatusLength = 1;

    internal const int FreezeStatusOffset = AuctionStatusOffset + AuctionStatusLength;

    internal const int FreezeStatusLength = 1;

    internal const int NumExtensionsOffset = FreezeStatusOffset + FreezeStatusLength;

    internal const int NumExtensionsLength = 1;

    internal const int UnpairedSideOffset = NumExtensionsOffset + NumExtensionsLength;

    internal const int UnpairedSideLength = 1;

    internal const int SignificantImbalanceOffset = UnpairedSideOffset + UnpairedSideLength;

    internal const int SignificantImbalanceLength = 1;

    internal const int Reserved1Offset = SignificantImbalanceOffset + SignificantImbalanceLength;

    internal const int Reserved1Length = 1;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TsRecvLength + RefPriceLength + AuctionTimeLength + ContBookClrPriceLength + AuctInterestClrPriceLength + SsrFillingPriceLength + IndMatchPriceLength + UpperCollarLength + LowerCollarLength + PairedQtyLength + TotalImbalanceQtyLength + MarketImbalanceQtyLength + UnpairedQtyLength + AuctionTypeLength + SideLength + AuctionStatusLength + FreezeStatusLength + NumExtensionsLength + UnpairedSideLength + SignificantImbalanceLength + Reserved1Length;

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

    internal long refPrice;

    /// <summary>
    ///  The price at which the imbalance shares are calculated
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="RefPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="RefPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="RefPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? RefPrice
    {
        get => this.refPrice == RefPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.refPrice, RefPriceScale);
        set
        {
            this.refPrice = value is { } present ? FixedPoint.ToRaw<long>(present, RefPriceFactor, long.MinValue, long.MaxValue, RefPriceNullValue) : RefPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="RefPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long RefPriceRaw
    {
        get => this.refPrice;
        set
        {
            this.refPrice = value;
            Touch();
        }
    }

    internal ulong auctionTime;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="AuctionTimeNullValue"/>. Setting <see langword="null"/> writes <see cref="AuctionTimeNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="AuctionTimeRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="AuctionTimeNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? AuctionTime
    {
        get => this.auctionTime == AuctionTimeNullValue ? (DateTime?)null : Instant.ToDateTime(this.auctionTime, AuctionTimeNanosecondsPerUnit);
        set
        {
            this.auctionTime = value is { } present ? Instant.ToRaw<ulong>(present, AuctionTimeNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, AuctionTimeNullValue) : AuctionTimeNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AuctionTime"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong AuctionTimeRaw
    {
        get => this.auctionTime;
        set
        {
            this.auctionTime = value;
            Touch();
        }
    }

    internal long contBookClrPrice;

    /// <summary>
    ///  The hypothetical auction-clearing price for both cross and continuous orders
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="ContBookClrPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="ContBookClrPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="ContBookClrPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? ContBookClrPrice
    {
        get => this.contBookClrPrice == ContBookClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.contBookClrPrice, ContBookClrPriceScale);
        set
        {
            this.contBookClrPrice = value is { } present ? FixedPoint.ToRaw<long>(present, ContBookClrPriceFactor, long.MinValue, long.MaxValue, ContBookClrPriceNullValue) : ContBookClrPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="ContBookClrPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long ContBookClrPriceRaw
    {
        get => this.contBookClrPrice;
        set
        {
            this.contBookClrPrice = value;
            Touch();
        }
    }

    internal long auctInterestClrPrice;

    /// <summary>
    ///  The hypothetical auction-clearing price for cross orders only
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="AuctInterestClrPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="AuctInterestClrPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="AuctInterestClrPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? AuctInterestClrPrice
    {
        get => this.auctInterestClrPrice == AuctInterestClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.auctInterestClrPrice, AuctInterestClrPriceScale);
        set
        {
            this.auctInterestClrPrice = value is { } present ? FixedPoint.ToRaw<long>(present, AuctInterestClrPriceFactor, long.MinValue, long.MaxValue, AuctInterestClrPriceNullValue) : AuctInterestClrPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="AuctInterestClrPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long AuctInterestClrPriceRaw
    {
        get => this.auctInterestClrPrice;
        set
        {
            this.auctInterestClrPrice = value;
            Touch();
        }
    }

    internal long ssrFillingPrice;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="SsrFillingPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="SsrFillingPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="SsrFillingPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? SsrFillingPrice
    {
        get => this.ssrFillingPrice == SsrFillingPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.ssrFillingPrice, SsrFillingPriceScale);
        set
        {
            this.ssrFillingPrice = value is { } present ? FixedPoint.ToRaw<long>(present, SsrFillingPriceFactor, long.MinValue, long.MaxValue, SsrFillingPriceNullValue) : SsrFillingPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="SsrFillingPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long SsrFillingPriceRaw
    {
        get => this.ssrFillingPrice;
        set
        {
            this.ssrFillingPrice = value;
            Touch();
        }
    }

    internal long indMatchPrice;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="IndMatchPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="IndMatchPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="IndMatchPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? IndMatchPrice
    {
        get => this.indMatchPrice == IndMatchPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.indMatchPrice, IndMatchPriceScale);
        set
        {
            this.indMatchPrice = value is { } present ? FixedPoint.ToRaw<long>(present, IndMatchPriceFactor, long.MinValue, long.MaxValue, IndMatchPriceNullValue) : IndMatchPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="IndMatchPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long IndMatchPriceRaw
    {
        get => this.indMatchPrice;
        set
        {
            this.indMatchPrice = value;
            Touch();
        }
    }

    internal long upperCollar;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="UpperCollarNullValue"/>. Setting <see langword="null"/> writes <see cref="UpperCollarNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="UpperCollarNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? UpperCollar
    {
        get => this.upperCollar == UpperCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.upperCollar, UpperCollarScale);
        set
        {
            this.upperCollar = value is { } present ? FixedPoint.ToRaw<long>(present, UpperCollarFactor, long.MinValue, long.MaxValue, UpperCollarNullValue) : UpperCollarNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="UpperCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long UpperCollarRaw
    {
        get => this.upperCollar;
        set
        {
            this.upperCollar = value;
            Touch();
        }
    }

    internal long lowerCollar;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="LowerCollarNullValue"/>. Setting <see langword="null"/> writes <see cref="LowerCollarNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="LowerCollarNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? LowerCollar
    {
        get => this.lowerCollar == LowerCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowerCollar, LowerCollarScale);
        set
        {
            this.lowerCollar = value is { } present ? FixedPoint.ToRaw<long>(present, LowerCollarFactor, long.MinValue, long.MaxValue, LowerCollarNullValue) : LowerCollarNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="LowerCollar"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long LowerCollarRaw
    {
        get => this.lowerCollar;
        set
        {
            this.lowerCollar = value;
            Touch();
        }
    }

    internal uint pairedQty;

    /// <summary>
    ///  The quantity of shares that are eligible to be matched at ref_price
    /// </summary>
    public uint PairedQty
    {
        get => this.pairedQty;
        set
        {
            this.pairedQty = value;
            Touch();
        }
    }

    internal uint totalImbalanceQty;

    /// <summary>
    ///  The quantity of shares that are not paired at ref_price
    /// </summary>
    public uint TotalImbalanceQty
    {
        get => this.totalImbalanceQty;
        set
        {
            this.totalImbalanceQty = value;
            Touch();
        }
    }

    internal uint marketImbalanceQty;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public uint MarketImbalanceQty
    {
        get => this.marketImbalanceQty;
        set
        {
            this.marketImbalanceQty = value;
            Touch();
        }
    }

    internal uint unpairedQty;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public uint UnpairedQty
    {
        get => this.unpairedQty;
        set
        {
            this.unpairedQty = value;
            Touch();
        }
    }

    internal char auctionType;

    /// <summary>
    ///  Venue-specific character code indicating the auction type; ~ when unused
    /// </summary>
    public char AuctionType
    {
        get => this.auctionType;
        set
        {
            this.auctionType = value;
            Touch();
        }
    }

    internal Side side;

    /// <summary>
    ///  The side that initiates the event
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

    internal byte auctionStatus;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public byte AuctionStatus
    {
        get => this.auctionStatus;
        set
        {
            this.auctionStatus = value;
            Touch();
        }
    }

    internal byte freezeStatus;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public byte FreezeStatus
    {
        get => this.freezeStatus;
        set
        {
            this.freezeStatus = value;
            Touch();
        }
    }

    internal byte numExtensions;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public byte NumExtensions
    {
        get => this.numExtensions;
        set
        {
            this.numExtensions = value;
            Touch();
        }
    }

    internal UnpairedSide unpairedSide;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public UnpairedSide UnpairedSide
    {
        get => this.unpairedSide;
        set
        {
            this.unpairedSide = value;
            Touch();
        }
    }

    internal char significantImbalance;

    /// <summary>
    ///  Venue-specific character code; for Nasdaq, contains the raw Price Variation Indicator
    /// </summary>
    public char SignificantImbalance
    {
        get => this.significantImbalance;
        set
        {
            this.significantImbalance = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue => RecordBodyCode.ImbalanceMsg;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.side) && global::System.Enum.IsDefined(this.unpairedSide);

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

    IMessage IMessage.Copy() => new ImbalanceMsg(this);

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
        ///  The price at which the imbalance shares are calculated
        /// </summary>
        RefPrice,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        AuctionTime,
        /// <summary>
        ///  The hypothetical auction-clearing price for both cross and continuous orders
        /// </summary>
        ContBookClrPrice,
        /// <summary>
        ///  The hypothetical auction-clearing price for cross orders only
        /// </summary>
        AuctInterestClrPrice,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        SsrFillingPrice,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        IndMatchPrice,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        UpperCollar,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        LowerCollar,
        /// <summary>
        ///  The quantity of shares that are eligible to be matched at ref_price
        /// </summary>
        PairedQty,
        /// <summary>
        ///  The quantity of shares that are not paired at ref_price
        /// </summary>
        TotalImbalanceQty,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        MarketImbalanceQty,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        UnpairedQty,
        /// <summary>
        ///  Venue-specific character code indicating the auction type; ~ when unused
        /// </summary>
        AuctionType,
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        Side,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        AuctionStatus,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        FreezeStatus,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        NumExtensions,
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        UnpairedSide,
        /// <summary>
        ///  Venue-specific character code; for Nasdaq, contains the raw Price Variation Indicator
        /// </summary>
        SignificantImbalance,
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        Reserved1,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly ImbalanceMsg owner;

        internal FieldSet(ImbalanceMsg owner)
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
        ///  The price at which the imbalance shares are calculated
        /// </summary>
        public FieldView RefPrice => this[FieldName.RefPrice];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView AuctionTime => this[FieldName.AuctionTime];

        /// <summary>
        ///  The hypothetical auction-clearing price for both cross and continuous orders
        /// </summary>
        public FieldView ContBookClrPrice => this[FieldName.ContBookClrPrice];

        /// <summary>
        ///  The hypothetical auction-clearing price for cross orders only
        /// </summary>
        public FieldView AuctInterestClrPrice => this[FieldName.AuctInterestClrPrice];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView SsrFillingPrice => this[FieldName.SsrFillingPrice];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView IndMatchPrice => this[FieldName.IndMatchPrice];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView UpperCollar => this[FieldName.UpperCollar];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView LowerCollar => this[FieldName.LowerCollar];

        /// <summary>
        ///  The quantity of shares that are eligible to be matched at ref_price
        /// </summary>
        public FieldView PairedQty => this[FieldName.PairedQty];

        /// <summary>
        ///  The quantity of shares that are not paired at ref_price
        /// </summary>
        public FieldView TotalImbalanceQty => this[FieldName.TotalImbalanceQty];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView MarketImbalanceQty => this[FieldName.MarketImbalanceQty];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView UnpairedQty => this[FieldName.UnpairedQty];

        /// <summary>
        ///  Venue-specific character code indicating the auction type; ~ when unused
        /// </summary>
        public FieldView AuctionType => this[FieldName.AuctionType];

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public FieldView Side => this[FieldName.Side];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView AuctionStatus => this[FieldName.AuctionStatus];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView FreezeStatus => this[FieldName.FreezeStatus];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView NumExtensions => this[FieldName.NumExtensions];

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public FieldView UnpairedSide => this[FieldName.UnpairedSide];

        /// <summary>
        ///  Venue-specific character code; for Nasdaq, contains the raw Price Variation Indicator
        /// </summary>
        public FieldView SignificantImbalance => this[FieldName.SignificantImbalance];

        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public FieldView Reserved1 => this[FieldName.Reserved1];

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
        new(nameof(FieldName.RefPrice), FieldKind.Decimal, RefPriceLength) { Scale = RefPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = RefPriceNullValue },
        new(nameof(FieldName.AuctionTime), FieldKind.DateTime, AuctionTimeLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = AuctionTimeNullValue },
        new(nameof(FieldName.ContBookClrPrice), FieldKind.Decimal, ContBookClrPriceLength) { Scale = ContBookClrPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = ContBookClrPriceNullValue },
        new(nameof(FieldName.AuctInterestClrPrice), FieldKind.Decimal, AuctInterestClrPriceLength) { Scale = AuctInterestClrPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = AuctInterestClrPriceNullValue },
        new(nameof(FieldName.SsrFillingPrice), FieldKind.Decimal, SsrFillingPriceLength) { Scale = SsrFillingPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = SsrFillingPriceNullValue },
        new(nameof(FieldName.IndMatchPrice), FieldKind.Decimal, IndMatchPriceLength) { Scale = IndMatchPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = IndMatchPriceNullValue },
        new(nameof(FieldName.UpperCollar), FieldKind.Decimal, UpperCollarLength) { Scale = UpperCollarScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = UpperCollarNullValue },
        new(nameof(FieldName.LowerCollar), FieldKind.Decimal, LowerCollarLength) { Scale = LowerCollarScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = LowerCollarNullValue },
        new(nameof(FieldName.PairedQty), FieldKind.UnsignedInteger, PairedQtyLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.TotalImbalanceQty), FieldKind.UnsignedInteger, TotalImbalanceQtyLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.MarketImbalanceQty), FieldKind.UnsignedInteger, MarketImbalanceQtyLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.UnpairedQty), FieldKind.UnsignedInteger, UnpairedQtyLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.AuctionType), FieldKind.Char, AuctionTypeLength),
        new(nameof(FieldName.Side), FieldKind.Enum, SideLength),
        new(nameof(FieldName.AuctionStatus), FieldKind.UnsignedInteger, AuctionStatusLength) { IsSigned = false },
        new(nameof(FieldName.FreezeStatus), FieldKind.UnsignedInteger, FreezeStatusLength) { IsSigned = false },
        new(nameof(FieldName.NumExtensions), FieldKind.UnsignedInteger, NumExtensionsLength) { IsSigned = false },
        new(nameof(FieldName.UnpairedSide), FieldKind.Enum, UnpairedSideLength),
        new(nameof(FieldName.SignificantImbalance), FieldKind.Char, SignificantImbalanceLength),
        new(nameof(FieldName.Reserved1), FieldKind.Padding, Reserved1Length),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="ImbalanceMsg"/> class with default values.
    /// </summary>
    public ImbalanceMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ImbalanceMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public ImbalanceMsg(ImbalanceMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ImbalanceMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.tsRecv = other.tsRecv;
        this.refPrice = other.refPrice;
        this.auctionTime = other.auctionTime;
        this.contBookClrPrice = other.contBookClrPrice;
        this.auctInterestClrPrice = other.auctInterestClrPrice;
        this.ssrFillingPrice = other.ssrFillingPrice;
        this.indMatchPrice = other.indMatchPrice;
        this.upperCollar = other.upperCollar;
        this.lowerCollar = other.lowerCollar;
        this.pairedQty = other.pairedQty;
        this.totalImbalanceQty = other.totalImbalanceQty;
        this.marketImbalanceQty = other.marketImbalanceQty;
        this.unpairedQty = other.unpairedQty;
        this.auctionType = other.auctionType;
        this.side = other.side;
        this.auctionStatus = other.auctionStatus;
        this.freezeStatus = other.freezeStatus;
        this.numExtensions = other.numExtensions;
        this.unpairedSide = other.unpairedSide;
        this.significantImbalance = other.significantImbalance;
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
        this.refPrice = default;
        this.auctionTime = default;
        this.contBookClrPrice = default;
        this.auctInterestClrPrice = default;
        this.ssrFillingPrice = default;
        this.indMatchPrice = default;
        this.upperCollar = default;
        this.lowerCollar = default;
        this.pairedQty = default;
        this.totalImbalanceQty = default;
        this.marketImbalanceQty = default;
        this.unpairedQty = default;
        this.auctionType = default;
        this.side = default;
        this.auctionStatus = default;
        this.freezeStatus = default;
        this.numExtensions = default;
        this.unpairedSide = default;
        this.significantImbalance = default;
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
    public static ImbalanceMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new ImbalanceMsg();
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
    public static ImbalanceMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new ImbalanceMsg();
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
        if (Truncated(data, start, cursor, RefPriceLength, FieldName.RefPrice)) return;
        this.refPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, RefPriceLength));
        cursor += RefPriceLength;
        if (Truncated(data, start, cursor, AuctionTimeLength, FieldName.AuctionTime)) return;
        this.auctionTime = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, AuctionTimeLength));
        cursor += AuctionTimeLength;
        if (Truncated(data, start, cursor, ContBookClrPriceLength, FieldName.ContBookClrPrice)) return;
        this.contBookClrPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, ContBookClrPriceLength));
        cursor += ContBookClrPriceLength;
        if (Truncated(data, start, cursor, AuctInterestClrPriceLength, FieldName.AuctInterestClrPrice)) return;
        this.auctInterestClrPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, AuctInterestClrPriceLength));
        cursor += AuctInterestClrPriceLength;
        if (Truncated(data, start, cursor, SsrFillingPriceLength, FieldName.SsrFillingPrice)) return;
        this.ssrFillingPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, SsrFillingPriceLength));
        cursor += SsrFillingPriceLength;
        if (Truncated(data, start, cursor, IndMatchPriceLength, FieldName.IndMatchPrice)) return;
        this.indMatchPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, IndMatchPriceLength));
        cursor += IndMatchPriceLength;
        if (Truncated(data, start, cursor, UpperCollarLength, FieldName.UpperCollar)) return;
        this.upperCollar = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, UpperCollarLength));
        cursor += UpperCollarLength;
        if (Truncated(data, start, cursor, LowerCollarLength, FieldName.LowerCollar)) return;
        this.lowerCollar = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, LowerCollarLength));
        cursor += LowerCollarLength;
        if (Truncated(data, start, cursor, PairedQtyLength, FieldName.PairedQty)) return;
        this.pairedQty = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, PairedQtyLength));
        cursor += PairedQtyLength;
        if (Truncated(data, start, cursor, TotalImbalanceQtyLength, FieldName.TotalImbalanceQty)) return;
        this.totalImbalanceQty = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, TotalImbalanceQtyLength));
        cursor += TotalImbalanceQtyLength;
        if (Truncated(data, start, cursor, MarketImbalanceQtyLength, FieldName.MarketImbalanceQty)) return;
        this.marketImbalanceQty = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MarketImbalanceQtyLength));
        cursor += MarketImbalanceQtyLength;
        if (Truncated(data, start, cursor, UnpairedQtyLength, FieldName.UnpairedQty)) return;
        this.unpairedQty = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, UnpairedQtyLength));
        cursor += UnpairedQtyLength;
        if (Truncated(data, start, cursor, AuctionTypeLength, FieldName.AuctionType)) return;
        this.auctionType = TextEncoding.Ascii.ReadChar(data.Slice(cursor, AuctionTypeLength)[0]);
        cursor += AuctionTypeLength;
        if (Truncated(data, start, cursor, SideLength, FieldName.Side)) return;
        this.side = (Side)data.Slice(cursor, SideLength)[0];
        cursor += SideLength;
        if (Truncated(data, start, cursor, AuctionStatusLength, FieldName.AuctionStatus)) return;
        this.auctionStatus = data.Slice(cursor, AuctionStatusLength)[0];
        cursor += AuctionStatusLength;
        if (Truncated(data, start, cursor, FreezeStatusLength, FieldName.FreezeStatus)) return;
        this.freezeStatus = data.Slice(cursor, FreezeStatusLength)[0];
        cursor += FreezeStatusLength;
        if (Truncated(data, start, cursor, NumExtensionsLength, FieldName.NumExtensions)) return;
        this.numExtensions = data.Slice(cursor, NumExtensionsLength)[0];
        cursor += NumExtensionsLength;
        if (Truncated(data, start, cursor, UnpairedSideLength, FieldName.UnpairedSide)) return;
        this.unpairedSide = (UnpairedSide)data.Slice(cursor, UnpairedSideLength)[0];
        cursor += UnpairedSideLength;
        if (Truncated(data, start, cursor, SignificantImbalanceLength, FieldName.SignificantImbalance)) return;
        this.significantImbalance = TextEncoding.Ascii.ReadChar(data.Slice(cursor, SignificantImbalanceLength)[0]);
        cursor += SignificantImbalanceLength;
        if (Truncated(data, start, cursor, Reserved1Length, FieldName.Reserved1)) return;
        cursor += Reserved1Length;
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
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, TsRecvLength), this.tsRecv);
        cursor += TsRecvLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, RefPriceLength), this.refPrice);
        cursor += RefPriceLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, AuctionTimeLength), this.auctionTime);
        cursor += AuctionTimeLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, ContBookClrPriceLength), this.contBookClrPrice);
        cursor += ContBookClrPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, AuctInterestClrPriceLength), this.auctInterestClrPrice);
        cursor += AuctInterestClrPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, SsrFillingPriceLength), this.ssrFillingPrice);
        cursor += SsrFillingPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, IndMatchPriceLength), this.indMatchPrice);
        cursor += IndMatchPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, UpperCollarLength), this.upperCollar);
        cursor += UpperCollarLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, LowerCollarLength), this.lowerCollar);
        cursor += LowerCollarLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, PairedQtyLength), this.pairedQty);
        cursor += PairedQtyLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, TotalImbalanceQtyLength), this.totalImbalanceQty);
        cursor += TotalImbalanceQtyLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MarketImbalanceQtyLength), this.marketImbalanceQty);
        cursor += MarketImbalanceQtyLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, UnpairedQtyLength), this.unpairedQty);
        cursor += UnpairedQtyLength;
        if (!TextEncoding.Ascii.CanWrite(this.auctionType))
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.AuctionType), EncodeStatus.DoesNotFit);
            return false;
        }
        data.Slice(cursor, AuctionTypeLength)[0] = TextEncoding.Ascii.WriteChar(this.auctionType);
        cursor += AuctionTypeLength;
        data.Slice(cursor, SideLength)[0] = unchecked((byte)(this.side));
        cursor += SideLength;
        data.Slice(cursor, AuctionStatusLength)[0] = unchecked((byte)(this.auctionStatus));
        cursor += AuctionStatusLength;
        data.Slice(cursor, FreezeStatusLength)[0] = unchecked((byte)(this.freezeStatus));
        cursor += FreezeStatusLength;
        data.Slice(cursor, NumExtensionsLength)[0] = unchecked((byte)(this.numExtensions));
        cursor += NumExtensionsLength;
        data.Slice(cursor, UnpairedSideLength)[0] = unchecked((byte)(this.unpairedSide));
        cursor += UnpairedSideLength;
        if (!TextEncoding.Ascii.CanWrite(this.significantImbalance))
        {
            failure = new EncodeFailure(new FieldView(this, (int)FieldName.SignificantImbalance), EncodeStatus.DoesNotFit);
            return false;
        }
        data.Slice(cursor, SignificantImbalanceLength)[0] = TextEncoding.Ascii.WriteChar(this.significantImbalance);
        cursor += SignificantImbalanceLength;
        {
            if (this.raw.Length >= Reserved1Offset + Reserved1Length)
            {
                this.raw.AsSpan(Reserved1Offset, Reserved1Length).CopyTo(data.Slice(cursor, Reserved1Length));
            }
            else
            {
                data.Slice(cursor, Reserved1Length).Clear();
            }
            cursor += Reserved1Length;
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
        if (!global::System.Text.Ascii.IsValid(this.auctionType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.AuctionType), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.side))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Side), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.unpairedSide))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.UnpairedSide), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Text.Ascii.IsValid(this.significantImbalance))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SignificantImbalance), ValidationStatus.NonAsciiText));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(ImbalanceMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.tsRecv != other.tsRecv) return false;
        if (this.refPrice != other.refPrice) return false;
        if (this.auctionTime != other.auctionTime) return false;
        if (this.contBookClrPrice != other.contBookClrPrice) return false;
        if (this.auctInterestClrPrice != other.auctInterestClrPrice) return false;
        if (this.ssrFillingPrice != other.ssrFillingPrice) return false;
        if (this.indMatchPrice != other.indMatchPrice) return false;
        if (this.upperCollar != other.upperCollar) return false;
        if (this.lowerCollar != other.lowerCollar) return false;
        if (this.pairedQty != other.pairedQty) return false;
        if (this.totalImbalanceQty != other.totalImbalanceQty) return false;
        if (this.marketImbalanceQty != other.marketImbalanceQty) return false;
        if (this.unpairedQty != other.unpairedQty) return false;
        if (this.auctionType != other.auctionType) return false;
        if (this.side != other.side) return false;
        if (this.auctionStatus != other.auctionStatus) return false;
        if (this.freezeStatus != other.freezeStatus) return false;
        if (this.numExtensions != other.numExtensions) return false;
        if (this.unpairedSide != other.unpairedSide) return false;
        if (this.significantImbalance != other.significantImbalance) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is ImbalanceMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.tsRecv);
        hash.Add(this.refPrice);
        hash.Add(this.auctionTime);
        hash.Add(this.contBookClrPrice);
        hash.Add(this.auctInterestClrPrice);
        hash.Add(this.ssrFillingPrice);
        hash.Add(this.indMatchPrice);
        hash.Add(this.upperCollar);
        hash.Add(this.lowerCollar);
        hash.Add(this.pairedQty);
        hash.Add(this.totalImbalanceQty);
        hash.Add(this.marketImbalanceQty);
        hash.Add(this.unpairedQty);
        hash.Add(this.auctionType);
        hash.Add(this.side);
        hash.Add(this.auctionStatus);
        hash.Add(this.freezeStatus);
        hash.Add(this.numExtensions);
        hash.Add(this.unpairedSide);
        hash.Add(this.significantImbalance);
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
        private const int FieldCount = 22;

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
            if (Has(FieldName.RefPrice)) names.Add(nameof(FieldName.RefPrice));
            if (Has(FieldName.AuctionTime)) names.Add(nameof(FieldName.AuctionTime));
            if (Has(FieldName.ContBookClrPrice)) names.Add(nameof(FieldName.ContBookClrPrice));
            if (Has(FieldName.AuctInterestClrPrice)) names.Add(nameof(FieldName.AuctInterestClrPrice));
            if (Has(FieldName.SsrFillingPrice)) names.Add(nameof(FieldName.SsrFillingPrice));
            if (Has(FieldName.IndMatchPrice)) names.Add(nameof(FieldName.IndMatchPrice));
            if (Has(FieldName.UpperCollar)) names.Add(nameof(FieldName.UpperCollar));
            if (Has(FieldName.LowerCollar)) names.Add(nameof(FieldName.LowerCollar));
            if (Has(FieldName.PairedQty)) names.Add(nameof(FieldName.PairedQty));
            if (Has(FieldName.TotalImbalanceQty)) names.Add(nameof(FieldName.TotalImbalanceQty));
            if (Has(FieldName.MarketImbalanceQty)) names.Add(nameof(FieldName.MarketImbalanceQty));
            if (Has(FieldName.UnpairedQty)) names.Add(nameof(FieldName.UnpairedQty));
            if (Has(FieldName.AuctionType)) names.Add(nameof(FieldName.AuctionType));
            if (Has(FieldName.Side)) names.Add(nameof(FieldName.Side));
            if (Has(FieldName.AuctionStatus)) names.Add(nameof(FieldName.AuctionStatus));
            if (Has(FieldName.FreezeStatus)) names.Add(nameof(FieldName.FreezeStatus));
            if (Has(FieldName.NumExtensions)) names.Add(nameof(FieldName.NumExtensions));
            if (Has(FieldName.UnpairedSide)) names.Add(nameof(FieldName.UnpairedSide));
            if (Has(FieldName.SignificantImbalance)) names.Add(nameof(FieldName.SignificantImbalance));
            if (Has(FieldName.Reserved1)) names.Add(nameof(FieldName.Reserved1));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(ImbalanceMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.tsRecv != other.tsRecv) diff = diff.With(FieldName.TsRecv);
        if (this.refPrice != other.refPrice) diff = diff.With(FieldName.RefPrice);
        if (this.auctionTime != other.auctionTime) diff = diff.With(FieldName.AuctionTime);
        if (this.contBookClrPrice != other.contBookClrPrice) diff = diff.With(FieldName.ContBookClrPrice);
        if (this.auctInterestClrPrice != other.auctInterestClrPrice) diff = diff.With(FieldName.AuctInterestClrPrice);
        if (this.ssrFillingPrice != other.ssrFillingPrice) diff = diff.With(FieldName.SsrFillingPrice);
        if (this.indMatchPrice != other.indMatchPrice) diff = diff.With(FieldName.IndMatchPrice);
        if (this.upperCollar != other.upperCollar) diff = diff.With(FieldName.UpperCollar);
        if (this.lowerCollar != other.lowerCollar) diff = diff.With(FieldName.LowerCollar);
        if (this.pairedQty != other.pairedQty) diff = diff.With(FieldName.PairedQty);
        if (this.totalImbalanceQty != other.totalImbalanceQty) diff = diff.With(FieldName.TotalImbalanceQty);
        if (this.marketImbalanceQty != other.marketImbalanceQty) diff = diff.With(FieldName.MarketImbalanceQty);
        if (this.unpairedQty != other.unpairedQty) diff = diff.With(FieldName.UnpairedQty);
        if (this.auctionType != other.auctionType) diff = diff.With(FieldName.AuctionType);
        if (this.side != other.side) diff = diff.With(FieldName.Side);
        if (this.auctionStatus != other.auctionStatus) diff = diff.With(FieldName.AuctionStatus);
        if (this.freezeStatus != other.freezeStatus) diff = diff.With(FieldName.FreezeStatus);
        if (this.numExtensions != other.numExtensions) diff = diff.With(FieldName.NumExtensions);
        if (this.unpairedSide != other.unpairedSide) diff = diff.With(FieldName.UnpairedSide);
        if (this.significantImbalance != other.significantImbalance) diff = diff.With(FieldName.SignificantImbalance);
        return diff;
    }

    private void CopyField(FieldName field, ImbalanceMsg from)
    {
        switch (field)
        {
            case FieldName.TsRecv:
                this.tsRecv = from.tsRecv;
                break;
            case FieldName.RefPrice:
                this.refPrice = from.refPrice;
                break;
            case FieldName.AuctionTime:
                this.auctionTime = from.auctionTime;
                break;
            case FieldName.ContBookClrPrice:
                this.contBookClrPrice = from.contBookClrPrice;
                break;
            case FieldName.AuctInterestClrPrice:
                this.auctInterestClrPrice = from.auctInterestClrPrice;
                break;
            case FieldName.SsrFillingPrice:
                this.ssrFillingPrice = from.ssrFillingPrice;
                break;
            case FieldName.IndMatchPrice:
                this.indMatchPrice = from.indMatchPrice;
                break;
            case FieldName.UpperCollar:
                this.upperCollar = from.upperCollar;
                break;
            case FieldName.LowerCollar:
                this.lowerCollar = from.lowerCollar;
                break;
            case FieldName.PairedQty:
                this.pairedQty = from.pairedQty;
                break;
            case FieldName.TotalImbalanceQty:
                this.totalImbalanceQty = from.totalImbalanceQty;
                break;
            case FieldName.MarketImbalanceQty:
                this.marketImbalanceQty = from.marketImbalanceQty;
                break;
            case FieldName.UnpairedQty:
                this.unpairedQty = from.unpairedQty;
                break;
            case FieldName.AuctionType:
                this.auctionType = from.auctionType;
                break;
            case FieldName.Side:
                this.side = from.side;
                break;
            case FieldName.AuctionStatus:
                this.auctionStatus = from.auctionStatus;
                break;
            case FieldName.FreezeStatus:
                this.freezeStatus = from.freezeStatus;
                break;
            case FieldName.NumExtensions:
                this.numExtensions = from.numExtensions;
                break;
            case FieldName.UnpairedSide:
                this.unpairedSide = from.unpairedSide;
                break;
            case FieldName.SignificantImbalance:
                this.significantImbalance = from.significantImbalance;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"ImbalanceMsg {{ TsRecv: {(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}, RefPrice: {(Text.Of(this.refPrice == RefPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.refPrice, RefPriceScale)))}, AuctionTime: {(Text.Instant(this.auctionTime, AuctionTimeNanosecondsPerUnit, AuctionTimeNullValue))}, ContBookClrPrice: {(Text.Of(this.contBookClrPrice == ContBookClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.contBookClrPrice, ContBookClrPriceScale)))}, AuctInterestClrPrice: {(Text.Of(this.auctInterestClrPrice == AuctInterestClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.auctInterestClrPrice, AuctInterestClrPriceScale)))}, SsrFillingPrice: {(Text.Of(this.ssrFillingPrice == SsrFillingPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.ssrFillingPrice, SsrFillingPriceScale)))}, IndMatchPrice: {(Text.Of(this.indMatchPrice == IndMatchPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.indMatchPrice, IndMatchPriceScale)))}, UpperCollar: {(Text.Of(this.upperCollar == UpperCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.upperCollar, UpperCollarScale)))}, LowerCollar: {(Text.Of(this.lowerCollar == LowerCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowerCollar, LowerCollarScale)))}, PairedQty: {(Text.Of(this.pairedQty))}, TotalImbalanceQty: {(Text.Of(this.totalImbalanceQty))}, MarketImbalanceQty: {(Text.Of(this.marketImbalanceQty))}, UnpairedQty: {(Text.Of(this.unpairedQty))}, AuctionType: {(Text.Of(this.auctionType))}, Side: {(Text.Enum(this.side))}, AuctionStatus: {(Text.Of(this.auctionStatus))}, FreezeStatus: {(Text.Of(this.freezeStatus))}, NumExtensions: {(Text.Of(this.numExtensions))}, UnpairedSide: {(Text.Enum(this.unpairedSide))}, SignificantImbalance: {(Text.Of(this.significantImbalance))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}] [{(Text.Of(this.refPrice == RefPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.refPrice, RefPriceScale)))}] [{(Text.Instant(this.auctionTime, AuctionTimeNanosecondsPerUnit, AuctionTimeNullValue))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(RecordBodyCode.ImbalanceMsg),
        FieldName.TsRecv => Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue),
        FieldName.RefPrice => Text.Of(this.refPrice == RefPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.refPrice, RefPriceScale)),
        FieldName.AuctionTime => Text.Instant(this.auctionTime, AuctionTimeNanosecondsPerUnit, AuctionTimeNullValue),
        FieldName.ContBookClrPrice => Text.Of(this.contBookClrPrice == ContBookClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.contBookClrPrice, ContBookClrPriceScale)),
        FieldName.AuctInterestClrPrice => Text.Of(this.auctInterestClrPrice == AuctInterestClrPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.auctInterestClrPrice, AuctInterestClrPriceScale)),
        FieldName.SsrFillingPrice => Text.Of(this.ssrFillingPrice == SsrFillingPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.ssrFillingPrice, SsrFillingPriceScale)),
        FieldName.IndMatchPrice => Text.Of(this.indMatchPrice == IndMatchPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.indMatchPrice, IndMatchPriceScale)),
        FieldName.UpperCollar => Text.Of(this.upperCollar == UpperCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.upperCollar, UpperCollarScale)),
        FieldName.LowerCollar => Text.Of(this.lowerCollar == LowerCollarNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowerCollar, LowerCollarScale)),
        FieldName.PairedQty => Text.Of(this.pairedQty),
        FieldName.TotalImbalanceQty => Text.Of(this.totalImbalanceQty),
        FieldName.MarketImbalanceQty => Text.Of(this.marketImbalanceQty),
        FieldName.UnpairedQty => Text.Of(this.unpairedQty),
        FieldName.AuctionType => Text.Of(this.auctionType),
        FieldName.Side => Text.Enum(this.side),
        FieldName.AuctionStatus => Text.Of(this.auctionStatus),
        FieldName.FreezeStatus => Text.Of(this.freezeStatus),
        FieldName.NumExtensions => Text.Of(this.numExtensions),
        FieldName.UnpairedSide => Text.Enum(this.unpairedSide),
        FieldName.SignificantImbalance => Text.Of(this.significantImbalance),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Side => global::System.Enum.IsDefined(this.side),
        FieldName.UnpairedSide => global::System.Enum.IsDefined(this.unpairedSide),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.TsRecv => this.tsRecv != TsRecvNullValue,
        FieldName.RefPrice => this.refPrice != RefPriceNullValue,
        FieldName.AuctionTime => this.auctionTime != AuctionTimeNullValue,
        FieldName.ContBookClrPrice => this.contBookClrPrice != ContBookClrPriceNullValue,
        FieldName.AuctInterestClrPrice => this.auctInterestClrPrice != AuctInterestClrPriceNullValue,
        FieldName.SsrFillingPrice => this.ssrFillingPrice != SsrFillingPriceNullValue,
        FieldName.IndMatchPrice => this.indMatchPrice != IndMatchPriceNullValue,
        FieldName.UpperCollar => this.upperCollar != UpperCollarNullValue,
        FieldName.LowerCollar => this.lowerCollar != LowerCollarNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.TsRecv => TsRecvOffset,
        FieldName.RefPrice => RefPriceOffset,
        FieldName.AuctionTime => AuctionTimeOffset,
        FieldName.ContBookClrPrice => ContBookClrPriceOffset,
        FieldName.AuctInterestClrPrice => AuctInterestClrPriceOffset,
        FieldName.SsrFillingPrice => SsrFillingPriceOffset,
        FieldName.IndMatchPrice => IndMatchPriceOffset,
        FieldName.UpperCollar => UpperCollarOffset,
        FieldName.LowerCollar => LowerCollarOffset,
        FieldName.PairedQty => PairedQtyOffset,
        FieldName.TotalImbalanceQty => TotalImbalanceQtyOffset,
        FieldName.MarketImbalanceQty => MarketImbalanceQtyOffset,
        FieldName.UnpairedQty => UnpairedQtyOffset,
        FieldName.AuctionType => AuctionTypeOffset,
        FieldName.Side => SideOffset,
        FieldName.AuctionStatus => AuctionStatusOffset,
        FieldName.FreezeStatus => FreezeStatusOffset,
        FieldName.NumExtensions => NumExtensionsOffset,
        FieldName.UnpairedSide => UnpairedSideOffset,
        FieldName.SignificantImbalance => SignificantImbalanceOffset,
        FieldName.Reserved1 => Reserved1Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.TsRecv => TsRecvLength,
        FieldName.RefPrice => RefPriceLength,
        FieldName.AuctionTime => AuctionTimeLength,
        FieldName.ContBookClrPrice => ContBookClrPriceLength,
        FieldName.AuctInterestClrPrice => AuctInterestClrPriceLength,
        FieldName.SsrFillingPrice => SsrFillingPriceLength,
        FieldName.IndMatchPrice => IndMatchPriceLength,
        FieldName.UpperCollar => UpperCollarLength,
        FieldName.LowerCollar => LowerCollarLength,
        FieldName.PairedQty => PairedQtyLength,
        FieldName.TotalImbalanceQty => TotalImbalanceQtyLength,
        FieldName.MarketImbalanceQty => MarketImbalanceQtyLength,
        FieldName.UnpairedQty => UnpairedQtyLength,
        FieldName.AuctionType => AuctionTypeLength,
        FieldName.Side => SideLength,
        FieldName.AuctionStatus => AuctionStatusLength,
        FieldName.FreezeStatus => FreezeStatusLength,
        FieldName.NumExtensions => NumExtensionsLength,
        FieldName.UnpairedSide => UnpairedSideLength,
        FieldName.SignificantImbalance => SignificantImbalanceLength,
        FieldName.Reserved1 => Reserved1Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is ImbalanceMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as ImbalanceMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(RecordBodyCode.ImbalanceMsg));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
