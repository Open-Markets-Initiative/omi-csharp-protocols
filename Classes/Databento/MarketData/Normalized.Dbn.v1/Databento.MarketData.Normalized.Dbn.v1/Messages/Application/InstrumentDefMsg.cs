using System.Buffers.Binary;
using System.Diagnostics;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  A definition of an instrument; the record of the definition schema
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class InstrumentDefMsg : IFileMessage, IGatewayMessage, IEquatable<InstrumentDefMsg>, IFieldOwner
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

    internal const int MinPriceIncrementOffset = TsRecvOffset + TsRecvLength;

    internal const int MinPriceIncrementLength = 8;

    /// <summary>
    ///  The number of decimal places in Min Price Increment.
    /// </summary>
    public const byte MinPriceIncrementScale = 9;

    /// <summary>
    ///  The factor by which <see cref="MinPriceIncrement"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal MinPriceIncrementFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Min Price Increment that means no value.
    /// </summary>
    public const long MinPriceIncrementNullValue = 9223372036854775807L;

    internal const int DisplayFactorOffset = MinPriceIncrementOffset + MinPriceIncrementLength;

    internal const int DisplayFactorLength = 8;

    /// <summary>
    ///  The number of decimal places in Display Factor.
    /// </summary>
    public const byte DisplayFactorScale = 9;

    /// <summary>
    ///  The factor by which <see cref="DisplayFactor"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal DisplayFactorFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Display Factor that means no value.
    /// </summary>
    public const long DisplayFactorNullValue = 9223372036854775807L;

    internal const int ExpirationOffset = DisplayFactorOffset + DisplayFactorLength;

    internal const int ExpirationLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Expiration"/>.
    /// </summary>
    public const long ExpirationNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Expiration that means no value.
    /// </summary>
    public const ulong ExpirationNullValue = 18446744073709551615UL;

    internal const int ActivationOffset = ExpirationOffset + ExpirationLength;

    internal const int ActivationLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Activation"/>.
    /// </summary>
    public const long ActivationNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of Activation that means no value.
    /// </summary>
    public const ulong ActivationNullValue = 18446744073709551615UL;

    internal const int HighLimitPriceOffset = ActivationOffset + ActivationLength;

    internal const int HighLimitPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in High Limit Price.
    /// </summary>
    public const byte HighLimitPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="HighLimitPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal HighLimitPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of High Limit Price that means no value.
    /// </summary>
    public const long HighLimitPriceNullValue = 9223372036854775807L;

    internal const int LowLimitPriceOffset = HighLimitPriceOffset + HighLimitPriceLength;

    internal const int LowLimitPriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Low Limit Price.
    /// </summary>
    public const byte LowLimitPriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="LowLimitPrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal LowLimitPriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Low Limit Price that means no value.
    /// </summary>
    public const long LowLimitPriceNullValue = 9223372036854775807L;

    internal const int MaxPriceVariationOffset = LowLimitPriceOffset + LowLimitPriceLength;

    internal const int MaxPriceVariationLength = 8;

    /// <summary>
    ///  The number of decimal places in Max Price Variation.
    /// </summary>
    public const byte MaxPriceVariationScale = 9;

    /// <summary>
    ///  The factor by which <see cref="MaxPriceVariation"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal MaxPriceVariationFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Max Price Variation that means no value.
    /// </summary>
    public const long MaxPriceVariationNullValue = 9223372036854775807L;

    internal const int TradingReferencePriceOffset = MaxPriceVariationOffset + MaxPriceVariationLength;

    internal const int TradingReferencePriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Trading Reference Price.
    /// </summary>
    public const byte TradingReferencePriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="TradingReferencePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal TradingReferencePriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Trading Reference Price that means no value.
    /// </summary>
    public const long TradingReferencePriceNullValue = 9223372036854775807L;

    internal const int UnitOfMeasureQtyOffset = TradingReferencePriceOffset + TradingReferencePriceLength;

    internal const int UnitOfMeasureQtyLength = 8;

    /// <summary>
    ///  The number of decimal places in Unit Of Measure Qty.
    /// </summary>
    public const byte UnitOfMeasureQtyScale = 9;

    /// <summary>
    ///  The factor by which <see cref="UnitOfMeasureQty"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal UnitOfMeasureQtyFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Unit Of Measure Qty that means no value.
    /// </summary>
    public const long UnitOfMeasureQtyNullValue = 9223372036854775807L;

    internal const int MinPriceIncrementAmountOffset = UnitOfMeasureQtyOffset + UnitOfMeasureQtyLength;

    internal const int MinPriceIncrementAmountLength = 8;

    /// <summary>
    ///  The number of decimal places in Min Price Increment Amount.
    /// </summary>
    public const byte MinPriceIncrementAmountScale = 9;

    /// <summary>
    ///  The factor by which <see cref="MinPriceIncrementAmount"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal MinPriceIncrementAmountFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Min Price Increment Amount that means no value.
    /// </summary>
    public const long MinPriceIncrementAmountNullValue = 9223372036854775807L;

    internal const int PriceRatioOffset = MinPriceIncrementAmountOffset + MinPriceIncrementAmountLength;

    internal const int PriceRatioLength = 8;

    /// <summary>
    ///  The number of decimal places in Price Ratio.
    /// </summary>
    public const byte PriceRatioScale = 9;

    /// <summary>
    ///  The factor by which <see cref="PriceRatio"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal PriceRatioFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Price Ratio that means no value.
    /// </summary>
    public const long PriceRatioNullValue = 9223372036854775807L;

    internal const int InstAttribValueOffset = PriceRatioOffset + PriceRatioLength;

    internal const int InstAttribValueLength = 4;

    internal const int UnderlyingIdOffset = InstAttribValueOffset + InstAttribValueLength;

    internal const int UnderlyingIdLength = 4;

    internal const int RawInstrumentIdOffset = UnderlyingIdOffset + UnderlyingIdLength;

    internal const int RawInstrumentIdLength = 4;

    internal const int MarketDepthImpliedOffset = RawInstrumentIdOffset + RawInstrumentIdLength;

    internal const int MarketDepthImpliedLength = 4;

    internal const int MarketDepthOffset = MarketDepthImpliedOffset + MarketDepthImpliedLength;

    internal const int MarketDepthLength = 4;

    internal const int MarketSegmentIdOffset = MarketDepthOffset + MarketDepthLength;

    internal const int MarketSegmentIdLength = 4;

    internal const int MaxTradeVolOffset = MarketSegmentIdOffset + MarketSegmentIdLength;

    internal const int MaxTradeVolLength = 4;

    internal const int MinLotSizeOffset = MaxTradeVolOffset + MaxTradeVolLength;

    internal const int MinLotSizeLength = 4;

    internal const int MinLotSizeBlockOffset = MinLotSizeOffset + MinLotSizeLength;

    internal const int MinLotSizeBlockLength = 4;

    internal const int MinLotSizeRoundLotOffset = MinLotSizeBlockOffset + MinLotSizeBlockLength;

    internal const int MinLotSizeRoundLotLength = 4;

    internal const int MinTradeVolOffset = MinLotSizeRoundLotOffset + MinLotSizeRoundLotLength;

    internal const int MinTradeVolLength = 4;

    internal const int Reserved4Offset = MinTradeVolOffset + MinTradeVolLength;

    internal const int Reserved4Length = 4;

    internal const int ContractMultiplierOffset = Reserved4Offset + Reserved4Length;

    internal const int ContractMultiplierLength = 4;

    internal const int DecayQuantityOffset = ContractMultiplierOffset + ContractMultiplierLength;

    internal const int DecayQuantityLength = 4;

    internal const int OriginalContractSizeOffset = DecayQuantityOffset + DecayQuantityLength;

    internal const int OriginalContractSizeLength = 4;

    internal const int SecondReserved4Offset = OriginalContractSizeOffset + OriginalContractSizeLength;

    internal const int SecondReserved4Length = 4;

    internal const int TradingReferenceDateOffset = SecondReserved4Offset + SecondReserved4Length;

    internal const int TradingReferenceDateLength = 2;

    internal const int ApplIdOffset = TradingReferenceDateOffset + TradingReferenceDateLength;

    internal const int ApplIdLength = 2;

    internal const int MaturityYearOffset = ApplIdOffset + ApplIdLength;

    internal const int MaturityYearLength = 2;

    internal const int DecayStartDateOffset = MaturityYearOffset + MaturityYearLength;

    internal const int DecayStartDateLength = 2;

    internal const int ChannelOffset = DecayStartDateOffset + DecayStartDateLength;

    internal const int ChannelLength = 2;

    internal const int CurrencyOffset = ChannelOffset + ChannelLength;

    internal const int CurrencyLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Currency.
    /// </summary>
    public const byte CurrencyFill = (byte)0;

    internal const int SettlCurrencyOffset = CurrencyOffset + CurrencyLength;

    internal const int SettlCurrencyLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Settl Currency.
    /// </summary>
    public const byte SettlCurrencyFill = (byte)0;

    internal const int SecsubtypeOffset = SettlCurrencyOffset + SettlCurrencyLength;

    internal const int SecsubtypeLength = 6;

    /// <summary>
    ///  The byte that fills the unused bytes of Secsubtype.
    /// </summary>
    public const byte SecsubtypeFill = (byte)0;

    internal const int RawSymbolOffset = SecsubtypeOffset + SecsubtypeLength;

    internal const int RawSymbolLength = 22;

    /// <summary>
    ///  The byte that fills the unused bytes of Raw Symbol.
    /// </summary>
    public const byte RawSymbolFill = (byte)0;

    internal const int GroupOffset = RawSymbolOffset + RawSymbolLength;

    internal const int GroupLength = 21;

    /// <summary>
    ///  The byte that fills the unused bytes of Group.
    /// </summary>
    public const byte GroupFill = (byte)0;

    internal const int ExchangeOffset = GroupOffset + GroupLength;

    internal const int ExchangeLength = 5;

    /// <summary>
    ///  The byte that fills the unused bytes of Exchange.
    /// </summary>
    public const byte ExchangeFill = (byte)0;

    internal const int AssetOffset = ExchangeOffset + ExchangeLength;

    internal const int AssetLength = 7;

    /// <summary>
    ///  The byte that fills the unused bytes of Asset.
    /// </summary>
    public const byte AssetFill = (byte)0;

    internal const int CfiOffset = AssetOffset + AssetLength;

    internal const int CfiLength = 7;

    /// <summary>
    ///  The byte that fills the unused bytes of Cfi.
    /// </summary>
    public const byte CfiFill = (byte)0;

    internal const int SecurityTypeOffset = CfiOffset + CfiLength;

    internal const int SecurityTypeLength = 7;

    /// <summary>
    ///  The byte that fills the unused bytes of Security Type.
    /// </summary>
    public const byte SecurityTypeFill = (byte)0;

    internal const int UnitOfMeasureOffset = SecurityTypeOffset + SecurityTypeLength;

    internal const int UnitOfMeasureLength = 31;

    /// <summary>
    ///  The byte that fills the unused bytes of Unit Of Measure.
    /// </summary>
    public const byte UnitOfMeasureFill = (byte)0;

    internal const int UnderlyingOffset = UnitOfMeasureOffset + UnitOfMeasureLength;

    internal const int UnderlyingLength = 21;

    /// <summary>
    ///  The byte that fills the unused bytes of Underlying.
    /// </summary>
    public const byte UnderlyingFill = (byte)0;

    internal const int StrikePriceCurrencyOffset = UnderlyingOffset + UnderlyingLength;

    internal const int StrikePriceCurrencyLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Strike Price Currency.
    /// </summary>
    public const byte StrikePriceCurrencyFill = (byte)0;

    internal const int InstrumentClassOffset = StrikePriceCurrencyOffset + StrikePriceCurrencyLength;

    internal const int InstrumentClassLength = 1;

    internal const int Reserved2Offset = InstrumentClassOffset + InstrumentClassLength;

    internal const int Reserved2Length = 2;

    internal const int StrikePriceOffset = Reserved2Offset + Reserved2Length;

    internal const int StrikePriceLength = 8;

    /// <summary>
    ///  The number of decimal places in Strike Price.
    /// </summary>
    public const byte StrikePriceScale = 9;

    /// <summary>
    ///  The factor by which <see cref="StrikePrice"/> is multiplied to give its wire value.
    /// </summary>
    public const decimal StrikePriceFactor = 1000000000m;

    /// <summary>
    ///  The encoded value of Strike Price that means no value.
    /// </summary>
    public const long StrikePriceNullValue = 9223372036854775807L;

    internal const int Reserved6Offset = StrikePriceOffset + StrikePriceLength;

    internal const int Reserved6Length = 6;

    internal const int MatchAlgorithmOffset = Reserved6Offset + Reserved6Length;

    internal const int MatchAlgorithmLength = 1;

    internal const int MdSecurityTradingStatusOffset = MatchAlgorithmOffset + MatchAlgorithmLength;

    internal const int MdSecurityTradingStatusLength = 1;

    internal const int MainFractionOffset = MdSecurityTradingStatusOffset + MdSecurityTradingStatusLength;

    internal const int MainFractionLength = 1;

    internal const int PriceDisplayFormatOffset = MainFractionOffset + MainFractionLength;

    internal const int PriceDisplayFormatLength = 1;

    internal const int SettlPriceTypeOffset = PriceDisplayFormatOffset + PriceDisplayFormatLength;

    internal const int SettlPriceTypeLength = 1;

    internal const int SubFractionOffset = SettlPriceTypeOffset + SettlPriceTypeLength;

    internal const int SubFractionLength = 1;

    internal const int UnderlyingProductOffset = SubFractionOffset + SubFractionLength;

    internal const int UnderlyingProductLength = 1;

    internal const int SecurityUpdateActionOffset = UnderlyingProductOffset + UnderlyingProductLength;

    internal const int SecurityUpdateActionLength = 1;

    internal const int MaturityMonthOffset = SecurityUpdateActionOffset + SecurityUpdateActionLength;

    internal const int MaturityMonthLength = 1;

    internal const int MaturityDayOffset = MaturityMonthOffset + MaturityMonthLength;

    internal const int MaturityDayLength = 1;

    internal const int MaturityWeekOffset = MaturityDayOffset + MaturityDayLength;

    internal const int MaturityWeekLength = 1;

    internal const int UserDefinedInstrumentOffset = MaturityWeekOffset + MaturityWeekLength;

    internal const int UserDefinedInstrumentLength = 1;

    internal const int ContractMultiplierUnitOffset = UserDefinedInstrumentOffset + UserDefinedInstrumentLength;

    internal const int ContractMultiplierUnitLength = 1;

    internal const int FlowScheduleTypeOffset = ContractMultiplierUnitOffset + ContractMultiplierUnitLength;

    internal const int FlowScheduleTypeLength = 1;

    internal const int TickRuleOffset = FlowScheduleTypeOffset + FlowScheduleTypeLength;

    internal const int TickRuleLength = 1;

    internal const int Dummy3Offset = TickRuleOffset + TickRuleLength;

    internal const int Dummy3Length = 3;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = TsRecvLength + MinPriceIncrementLength + DisplayFactorLength + ExpirationLength + ActivationLength + HighLimitPriceLength + LowLimitPriceLength + MaxPriceVariationLength + TradingReferencePriceLength + UnitOfMeasureQtyLength + MinPriceIncrementAmountLength + PriceRatioLength + InstAttribValueLength + UnderlyingIdLength + RawInstrumentIdLength + MarketDepthImpliedLength + MarketDepthLength + MarketSegmentIdLength + MaxTradeVolLength + MinLotSizeLength + MinLotSizeBlockLength + MinLotSizeRoundLotLength + MinTradeVolLength + Reserved4Length + ContractMultiplierLength + DecayQuantityLength + OriginalContractSizeLength + SecondReserved4Length + TradingReferenceDateLength + ApplIdLength + MaturityYearLength + DecayStartDateLength + ChannelLength + CurrencyLength + SettlCurrencyLength + SecsubtypeLength + RawSymbolLength + GroupLength + ExchangeLength + AssetLength + CfiLength + SecurityTypeLength + UnitOfMeasureLength + UnderlyingLength + StrikePriceCurrencyLength + InstrumentClassLength + Reserved2Length + StrikePriceLength + Reserved6Length + MatchAlgorithmLength + MdSecurityTradingStatusLength + MainFractionLength + PriceDisplayFormatLength + SettlPriceTypeLength + SubFractionLength + UnderlyingProductLength + SecurityUpdateActionLength + MaturityMonthLength + MaturityDayLength + MaturityWeekLength + UserDefinedInstrumentLength + ContractMultiplierUnitLength + FlowScheduleTypeLength + TickRuleLength + Dummy3Length;

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

    internal long minPriceIncrement;

    /// <summary>
    ///  The min price increment, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="MinPriceIncrementNullValue"/>. Setting <see langword="null"/> writes <see cref="MinPriceIncrementNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="MinPriceIncrementNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? MinPriceIncrement
    {
        get => this.minPriceIncrement == MinPriceIncrementNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrement, MinPriceIncrementScale);
        set
        {
            this.minPriceIncrement = value is { } present ? FixedPoint.ToRaw<long>(present, MinPriceIncrementFactor, long.MinValue, long.MaxValue, MinPriceIncrementNullValue) : MinPriceIncrementNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="MinPriceIncrement"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long MinPriceIncrementRaw
    {
        get => this.minPriceIncrement;
        set
        {
            this.minPriceIncrement = value;
            Touch();
        }
    }

    internal long displayFactor;

    /// <summary>
    ///  The display factor, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="DisplayFactorNullValue"/>. Setting <see langword="null"/> writes <see cref="DisplayFactorNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="DisplayFactorNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? DisplayFactor
    {
        get => this.displayFactor == DisplayFactorNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.displayFactor, DisplayFactorScale);
        set
        {
            this.displayFactor = value is { } present ? FixedPoint.ToRaw<long>(present, DisplayFactorFactor, long.MinValue, long.MaxValue, DisplayFactorNullValue) : DisplayFactorNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="DisplayFactor"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long DisplayFactorRaw
    {
        get => this.displayFactor;
        set
        {
            this.displayFactor = value;
            Touch();
        }
    }

    internal ulong expiration;

    /// <summary>
    ///  The last eligible trade time expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="ExpirationNullValue"/>. Setting <see langword="null"/> writes <see cref="ExpirationNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="ExpirationRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="ExpirationNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? Expiration
    {
        get => this.expiration == ExpirationNullValue ? (DateTime?)null : Instant.ToDateTime(this.expiration, ExpirationNanosecondsPerUnit);
        set
        {
            this.expiration = value is { } present ? Instant.ToRaw<ulong>(present, ExpirationNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, ExpirationNullValue) : ExpirationNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Expiration"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong ExpirationRaw
    {
        get => this.expiration;
        set
        {
            this.expiration = value;
            Touch();
        }
    }

    internal ulong activation;

    /// <summary>
    ///  The time of instrument activation expressed as the number of nanoseconds since the UNIX epoch
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="ActivationNullValue"/>. Setting <see langword="null"/> writes <see cref="ActivationNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="ActivationRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="ActivationNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? Activation
    {
        get => this.activation == ActivationNullValue ? (DateTime?)null : Instant.ToDateTime(this.activation, ActivationNanosecondsPerUnit);
        set
        {
            this.activation = value is { } present ? Instant.ToRaw<ulong>(present, ActivationNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, ActivationNullValue) : ActivationNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Activation"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong ActivationRaw
    {
        get => this.activation;
        set
        {
            this.activation = value;
            Touch();
        }
    }

    internal long highLimitPrice;

    /// <summary>
    ///  The high limit price, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="HighLimitPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="HighLimitPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="HighLimitPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? HighLimitPrice
    {
        get => this.highLimitPrice == HighLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.highLimitPrice, HighLimitPriceScale);
        set
        {
            this.highLimitPrice = value is { } present ? FixedPoint.ToRaw<long>(present, HighLimitPriceFactor, long.MinValue, long.MaxValue, HighLimitPriceNullValue) : HighLimitPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="HighLimitPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long HighLimitPriceRaw
    {
        get => this.highLimitPrice;
        set
        {
            this.highLimitPrice = value;
            Touch();
        }
    }

    internal long lowLimitPrice;

    /// <summary>
    ///  The low limit price, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="LowLimitPriceNullValue"/>. Setting <see langword="null"/> writes <see cref="LowLimitPriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="LowLimitPriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? LowLimitPrice
    {
        get => this.lowLimitPrice == LowLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowLimitPrice, LowLimitPriceScale);
        set
        {
            this.lowLimitPrice = value is { } present ? FixedPoint.ToRaw<long>(present, LowLimitPriceFactor, long.MinValue, long.MaxValue, LowLimitPriceNullValue) : LowLimitPriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="LowLimitPrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long LowLimitPriceRaw
    {
        get => this.lowLimitPrice;
        set
        {
            this.lowLimitPrice = value;
            Touch();
        }
    }

    internal long maxPriceVariation;

    /// <summary>
    ///  The max price variation, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="MaxPriceVariationNullValue"/>. Setting <see langword="null"/> writes <see cref="MaxPriceVariationNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="MaxPriceVariationNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? MaxPriceVariation
    {
        get => this.maxPriceVariation == MaxPriceVariationNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.maxPriceVariation, MaxPriceVariationScale);
        set
        {
            this.maxPriceVariation = value is { } present ? FixedPoint.ToRaw<long>(present, MaxPriceVariationFactor, long.MinValue, long.MaxValue, MaxPriceVariationNullValue) : MaxPriceVariationNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="MaxPriceVariation"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long MaxPriceVariationRaw
    {
        get => this.maxPriceVariation;
        set
        {
            this.maxPriceVariation = value;
            Touch();
        }
    }

    internal long tradingReferencePrice;

    /// <summary>
    ///  The trading session settlement price on trading_reference_date, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="TradingReferencePriceNullValue"/>. Setting <see langword="null"/> writes <see cref="TradingReferencePriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="TradingReferencePriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? TradingReferencePrice
    {
        get => this.tradingReferencePrice == TradingReferencePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.tradingReferencePrice, TradingReferencePriceScale);
        set
        {
            this.tradingReferencePrice = value is { } present ? FixedPoint.ToRaw<long>(present, TradingReferencePriceFactor, long.MinValue, long.MaxValue, TradingReferencePriceNullValue) : TradingReferencePriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="TradingReferencePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long TradingReferencePriceRaw
    {
        get => this.tradingReferencePrice;
        set
        {
            this.tradingReferencePrice = value;
            Touch();
        }
    }

    internal long unitOfMeasureQty;

    /// <summary>
    ///  The unit of measure qty, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="UnitOfMeasureQtyNullValue"/>. Setting <see langword="null"/> writes <see cref="UnitOfMeasureQtyNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="UnitOfMeasureQtyNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? UnitOfMeasureQty
    {
        get => this.unitOfMeasureQty == UnitOfMeasureQtyNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.unitOfMeasureQty, UnitOfMeasureQtyScale);
        set
        {
            this.unitOfMeasureQty = value is { } present ? FixedPoint.ToRaw<long>(present, UnitOfMeasureQtyFactor, long.MinValue, long.MaxValue, UnitOfMeasureQtyNullValue) : UnitOfMeasureQtyNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="UnitOfMeasureQty"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long UnitOfMeasureQtyRaw
    {
        get => this.unitOfMeasureQty;
        set
        {
            this.unitOfMeasureQty = value;
            Touch();
        }
    }

    internal long minPriceIncrementAmount;

    /// <summary>
    ///  The min price increment amount, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="MinPriceIncrementAmountNullValue"/>. Setting <see langword="null"/> writes <see cref="MinPriceIncrementAmountNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="MinPriceIncrementAmountNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? MinPriceIncrementAmount
    {
        get => this.minPriceIncrementAmount == MinPriceIncrementAmountNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrementAmount, MinPriceIncrementAmountScale);
        set
        {
            this.minPriceIncrementAmount = value is { } present ? FixedPoint.ToRaw<long>(present, MinPriceIncrementAmountFactor, long.MinValue, long.MaxValue, MinPriceIncrementAmountNullValue) : MinPriceIncrementAmountNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="MinPriceIncrementAmount"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long MinPriceIncrementAmountRaw
    {
        get => this.minPriceIncrementAmount;
        set
        {
            this.minPriceIncrementAmount = value;
            Touch();
        }
    }

    internal long priceRatio;

    /// <summary>
    ///  The price ratio, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="PriceRatioNullValue"/>. Setting <see langword="null"/> writes <see cref="PriceRatioNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="PriceRatioNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? PriceRatio
    {
        get => this.priceRatio == PriceRatioNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.priceRatio, PriceRatioScale);
        set
        {
            this.priceRatio = value is { } present ? FixedPoint.ToRaw<long>(present, PriceRatioFactor, long.MinValue, long.MaxValue, PriceRatioNullValue) : PriceRatioNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="PriceRatio"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long PriceRatioRaw
    {
        get => this.priceRatio;
        set
        {
            this.priceRatio = value;
            Touch();
        }
    }

    internal int instAttribValue;

    /// <summary>
    ///  A bitmap of instrument eligibility attributes; venue-specific
    /// </summary>
    public int InstAttribValue
    {
        get => this.instAttribValue;
        set
        {
            this.instAttribValue = value;
            Touch();
        }
    }

    internal uint underlyingId;

    /// <summary>
    ///  The underlying id
    /// </summary>
    public uint UnderlyingId
    {
        get => this.underlyingId;
        set
        {
            this.underlyingId = value;
            Touch();
        }
    }

    internal uint rawInstrumentId;

    /// <summary>
    ///  The instrument ID assigned by the publisher; may be the same as instrument_id
    /// </summary>
    public uint RawInstrumentId
    {
        get => this.rawInstrumentId;
        set
        {
            this.rawInstrumentId = value;
            Touch();
        }
    }

    internal int marketDepthImplied;

    /// <summary>
    ///  The market depth implied
    /// </summary>
    public int MarketDepthImplied
    {
        get => this.marketDepthImplied;
        set
        {
            this.marketDepthImplied = value;
            Touch();
        }
    }

    internal int marketDepth;

    /// <summary>
    ///  The market depth
    /// </summary>
    public int MarketDepth
    {
        get => this.marketDepth;
        set
        {
            this.marketDepth = value;
            Touch();
        }
    }

    internal uint marketSegmentId;

    /// <summary>
    ///  The market segment id
    /// </summary>
    public uint MarketSegmentId
    {
        get => this.marketSegmentId;
        set
        {
            this.marketSegmentId = value;
            Touch();
        }
    }

    internal uint maxTradeVol;

    /// <summary>
    ///  The max trade vol
    /// </summary>
    public uint MaxTradeVol
    {
        get => this.maxTradeVol;
        set
        {
            this.maxTradeVol = value;
            Touch();
        }
    }

    internal int minLotSize;

    /// <summary>
    ///  The min lot size
    /// </summary>
    public int MinLotSize
    {
        get => this.minLotSize;
        set
        {
            this.minLotSize = value;
            Touch();
        }
    }

    internal int minLotSizeBlock;

    /// <summary>
    ///  The min lot size block
    /// </summary>
    public int MinLotSizeBlock
    {
        get => this.minLotSizeBlock;
        set
        {
            this.minLotSizeBlock = value;
            Touch();
        }
    }

    internal int minLotSizeRoundLot;

    /// <summary>
    ///  The min lot size round lot
    /// </summary>
    public int MinLotSizeRoundLot
    {
        get => this.minLotSizeRoundLot;
        set
        {
            this.minLotSizeRoundLot = value;
            Touch();
        }
    }

    internal uint minTradeVol;

    /// <summary>
    ///  The min trade vol
    /// </summary>
    public uint MinTradeVol
    {
        get => this.minTradeVol;
        set
        {
            this.minTradeVol = value;
            Touch();
        }
    }

    internal int contractMultiplier;

    /// <summary>
    ///  The contract multiplier
    /// </summary>
    public int ContractMultiplier
    {
        get => this.contractMultiplier;
        set
        {
            this.contractMultiplier = value;
            Touch();
        }
    }

    internal int decayQuantity;

    /// <summary>
    ///  The decay quantity
    /// </summary>
    public int DecayQuantity
    {
        get => this.decayQuantity;
        set
        {
            this.decayQuantity = value;
            Touch();
        }
    }

    internal int originalContractSize;

    /// <summary>
    ///  The original contract size
    /// </summary>
    public int OriginalContractSize
    {
        get => this.originalContractSize;
        set
        {
            this.originalContractSize = value;
            Touch();
        }
    }

    internal ushort tradingReferenceDate;

    /// <summary>
    ///  The trading session date corresponding to the settlement price in trading_reference_price, in number of days since the UNIX epoch
    /// </summary>
    public ushort TradingReferenceDate
    {
        get => this.tradingReferenceDate;
        set
        {
            this.tradingReferenceDate = value;
            Touch();
        }
    }

    internal short applId;

    /// <summary>
    ///  The appl id
    /// </summary>
    public short ApplId
    {
        get => this.applId;
        set
        {
            this.applId = value;
            Touch();
        }
    }

    internal ushort maturityYear;

    /// <summary>
    ///  The maturity year
    /// </summary>
    public ushort MaturityYear
    {
        get => this.maturityYear;
        set
        {
            this.maturityYear = value;
            Touch();
        }
    }

    internal ushort decayStartDate;

    /// <summary>
    ///  The decay start date
    /// </summary>
    public ushort DecayStartDate
    {
        get => this.decayStartDate;
        set
        {
            this.decayStartDate = value;
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

    internal string currency = string.Empty;

    /// <summary>
    ///  The currency
    /// </summary>
    /// <remarks>
    ///  The field is 4 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Currency
    {
        get => this.currency;
        set
        {
            this.currency = value ?? string.Empty;
            Touch();
        }
    }

    internal string settlCurrency = string.Empty;

    /// <summary>
    ///  The settl currency
    /// </summary>
    /// <remarks>
    ///  The field is 4 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string SettlCurrency
    {
        get => this.settlCurrency;
        set
        {
            this.settlCurrency = value ?? string.Empty;
            Touch();
        }
    }

    internal string secsubtype = string.Empty;

    /// <summary>
    ///  The secsubtype
    /// </summary>
    /// <remarks>
    ///  The field is 6 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Secsubtype
    {
        get => this.secsubtype;
        set
        {
            this.secsubtype = value ?? string.Empty;
            Touch();
        }
    }

    internal string rawSymbol = string.Empty;

    /// <summary>
    ///  The raw symbol
    /// </summary>
    /// <remarks>
    ///  The field is 22 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string RawSymbol
    {
        get => this.rawSymbol;
        set
        {
            this.rawSymbol = value ?? string.Empty;
            Touch();
        }
    }

    internal string group = string.Empty;

    /// <summary>
    ///  The group
    /// </summary>
    /// <remarks>
    ///  The field is 21 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Group
    {
        get => this.group;
        set
        {
            this.group = value ?? string.Empty;
            Touch();
        }
    }

    internal string exchange = string.Empty;

    /// <summary>
    ///  The exchange
    /// </summary>
    /// <remarks>
    ///  The field is 5 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Exchange
    {
        get => this.exchange;
        set
        {
            this.exchange = value ?? string.Empty;
            Touch();
        }
    }

    internal string asset = string.Empty;

    /// <summary>
    ///  The asset
    /// </summary>
    /// <remarks>
    ///  The field is 7 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Asset
    {
        get => this.asset;
        set
        {
            this.asset = value ?? string.Empty;
            Touch();
        }
    }

    internal string cfi = string.Empty;

    /// <summary>
    ///  The cfi
    /// </summary>
    /// <remarks>
    ///  The field is 7 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Cfi
    {
        get => this.cfi;
        set
        {
            this.cfi = value ?? string.Empty;
            Touch();
        }
    }

    internal string securityType = string.Empty;

    /// <summary>
    ///  The security type
    /// </summary>
    /// <remarks>
    ///  The field is 7 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string SecurityType
    {
        get => this.securityType;
        set
        {
            this.securityType = value ?? string.Empty;
            Touch();
        }
    }

    internal string unitOfMeasure = string.Empty;

    /// <summary>
    ///  The unit of measure
    /// </summary>
    /// <remarks>
    ///  The field is 31 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string UnitOfMeasure
    {
        get => this.unitOfMeasure;
        set
        {
            this.unitOfMeasure = value ?? string.Empty;
            Touch();
        }
    }

    internal string underlying = string.Empty;

    /// <summary>
    ///  The underlying
    /// </summary>
    /// <remarks>
    ///  The field is 21 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Underlying
    {
        get => this.underlying;
        set
        {
            this.underlying = value ?? string.Empty;
            Touch();
        }
    }

    internal string strikePriceCurrency = string.Empty;

    /// <summary>
    ///  The strike price currency
    /// </summary>
    /// <remarks>
    ///  The field is 4 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string StrikePriceCurrency
    {
        get => this.strikePriceCurrency;
        set
        {
            this.strikePriceCurrency = value ?? string.Empty;
            Touch();
        }
    }

    internal InstrumentClass instrumentClass;

    /// <summary>
    ///  The classification of the instrument
    /// </summary>
    public InstrumentClass InstrumentClass
    {
        get => this.instrumentClass;
        set
        {
            this.instrumentClass = value;
            Touch();
        }
    }

    internal long strikePrice;

    /// <summary>
    ///  The strike price, where every 1 unit corresponds to 1e-9
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="StrikePriceNullValue"/>. Setting <see langword="null"/> writes <see cref="StrikePriceNullValue"/>. A value is rounded to the decimal places of the field, with a midpoint away from zero. A value outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="StrikePriceNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public decimal? StrikePrice
    {
        get => this.strikePrice == StrikePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.strikePrice, StrikePriceScale);
        set
        {
            this.strikePrice = value is { } present ? FixedPoint.ToRaw<long>(present, StrikePriceFactor, long.MinValue, long.MaxValue, StrikePriceNullValue) : StrikePriceNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="StrikePrice"/>, as the unscaled integer that the field holds.
    /// </summary>
    public long StrikePriceRaw
    {
        get => this.strikePrice;
        set
        {
            this.strikePrice = value;
            Touch();
        }
    }

    internal MatchAlgorithm matchAlgorithm;

    /// <summary>
    ///  The matching algorithm used for the instrument, typically FIFO
    /// </summary>
    public MatchAlgorithm MatchAlgorithm
    {
        get => this.matchAlgorithm;
        set
        {
            this.matchAlgorithm = value;
            Touch();
        }
    }

    internal byte mdSecurityTradingStatus;

    /// <summary>
    ///  The current trading state of the instrument
    /// </summary>
    public byte MdSecurityTradingStatus
    {
        get => this.mdSecurityTradingStatus;
        set
        {
            this.mdSecurityTradingStatus = value;
            Touch();
        }
    }

    internal byte mainFraction;

    /// <summary>
    ///  The main fraction
    /// </summary>
    public byte MainFraction
    {
        get => this.mainFraction;
        set
        {
            this.mainFraction = value;
            Touch();
        }
    }

    internal byte priceDisplayFormat;

    /// <summary>
    ///  The price display format
    /// </summary>
    public byte PriceDisplayFormat
    {
        get => this.priceDisplayFormat;
        set
        {
            this.priceDisplayFormat = value;
            Touch();
        }
    }

    internal byte settlPriceType;

    /// <summary>
    ///  The type indicators for the settlement price, as a bitmap
    /// </summary>
    public byte SettlPriceType
    {
        get => this.settlPriceType;
        set
        {
            this.settlPriceType = value;
            Touch();
        }
    }

    internal byte subFraction;

    /// <summary>
    ///  The sub fraction
    /// </summary>
    public byte SubFraction
    {
        get => this.subFraction;
        set
        {
            this.subFraction = value;
            Touch();
        }
    }

    internal byte underlyingProduct;

    /// <summary>
    ///  The underlying product
    /// </summary>
    public byte UnderlyingProduct
    {
        get => this.underlyingProduct;
        set
        {
            this.underlyingProduct = value;
            Touch();
        }
    }

    internal SecurityUpdateAction securityUpdateAction;

    /// <summary>
    ///  Indicates if the instrument definition has been added, modified, or deleted
    /// </summary>
    public SecurityUpdateAction SecurityUpdateAction
    {
        get => this.securityUpdateAction;
        set
        {
            this.securityUpdateAction = value;
            Touch();
        }
    }

    internal byte maturityMonth;

    /// <summary>
    ///  The maturity month
    /// </summary>
    public byte MaturityMonth
    {
        get => this.maturityMonth;
        set
        {
            this.maturityMonth = value;
            Touch();
        }
    }

    internal byte maturityDay;

    /// <summary>
    ///  The maturity day
    /// </summary>
    public byte MaturityDay
    {
        get => this.maturityDay;
        set
        {
            this.maturityDay = value;
            Touch();
        }
    }

    internal byte maturityWeek;

    /// <summary>
    ///  The maturity week
    /// </summary>
    public byte MaturityWeek
    {
        get => this.maturityWeek;
        set
        {
            this.maturityWeek = value;
            Touch();
        }
    }

    internal UserDefinedInstrument userDefinedInstrument;

    /// <summary>
    ///  Indicates if the instrument is user defined
    /// </summary>
    public UserDefinedInstrument UserDefinedInstrument
    {
        get => this.userDefinedInstrument;
        set
        {
            this.userDefinedInstrument = value;
            Touch();
        }
    }

    internal sbyte contractMultiplierUnit;

    /// <summary>
    ///  The contract multiplier unit
    /// </summary>
    public sbyte ContractMultiplierUnit
    {
        get => this.contractMultiplierUnit;
        set
        {
            this.contractMultiplierUnit = value;
            Touch();
        }
    }

    internal sbyte flowScheduleType;

    /// <summary>
    ///  The flow schedule type
    /// </summary>
    public sbyte FlowScheduleType
    {
        get => this.flowScheduleType;
        set
        {
            this.flowScheduleType = value;
            Touch();
        }
    }

    internal byte tickRule;

    /// <summary>
    ///  The tick rule
    /// </summary>
    public byte TickRule
    {
        get => this.tickRule;
        set
        {
            this.tickRule = value;
            Touch();
        }
    }

    /// <summary>
    ///  Gets the message type.
    /// </summary>
    public RecordBodyCode DataValue => RecordBodyCode.InstrumentDefMsg;

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
    public bool IsRecognized => global::System.Enum.IsDefined(this.instrumentClass) && global::System.Enum.IsDefined(this.matchAlgorithm) && global::System.Enum.IsDefined(this.securityUpdateAction) && global::System.Enum.IsDefined(this.userDefinedInstrument);

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

    IMessage IMessage.Copy() => new InstrumentDefMsg(this);

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
        ///  The min price increment, where every 1 unit corresponds to 1e-9
        /// </summary>
        MinPriceIncrement,
        /// <summary>
        ///  The display factor, where every 1 unit corresponds to 1e-9
        /// </summary>
        DisplayFactor,
        /// <summary>
        ///  The last eligible trade time expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        Expiration,
        /// <summary>
        ///  The time of instrument activation expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        Activation,
        /// <summary>
        ///  The high limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        HighLimitPrice,
        /// <summary>
        ///  The low limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        LowLimitPrice,
        /// <summary>
        ///  The max price variation, where every 1 unit corresponds to 1e-9
        /// </summary>
        MaxPriceVariation,
        /// <summary>
        ///  The trading session settlement price on trading_reference_date, where every 1 unit corresponds to 1e-9
        /// </summary>
        TradingReferencePrice,
        /// <summary>
        ///  The unit of measure qty, where every 1 unit corresponds to 1e-9
        /// </summary>
        UnitOfMeasureQty,
        /// <summary>
        ///  The min price increment amount, where every 1 unit corresponds to 1e-9
        /// </summary>
        MinPriceIncrementAmount,
        /// <summary>
        ///  The price ratio, where every 1 unit corresponds to 1e-9
        /// </summary>
        PriceRatio,
        /// <summary>
        ///  A bitmap of instrument eligibility attributes; venue-specific
        /// </summary>
        InstAttribValue,
        /// <summary>
        ///  The underlying id
        /// </summary>
        UnderlyingId,
        /// <summary>
        ///  The instrument ID assigned by the publisher; may be the same as instrument_id
        /// </summary>
        RawInstrumentId,
        /// <summary>
        ///  The market depth implied
        /// </summary>
        MarketDepthImplied,
        /// <summary>
        ///  The market depth
        /// </summary>
        MarketDepth,
        /// <summary>
        ///  The market segment id
        /// </summary>
        MarketSegmentId,
        /// <summary>
        ///  The max trade vol
        /// </summary>
        MaxTradeVol,
        /// <summary>
        ///  The min lot size
        /// </summary>
        MinLotSize,
        /// <summary>
        ///  The min lot size block
        /// </summary>
        MinLotSizeBlock,
        /// <summary>
        ///  The min lot size round lot
        /// </summary>
        MinLotSizeRoundLot,
        /// <summary>
        ///  The min trade vol
        /// </summary>
        MinTradeVol,
        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        Reserved4,
        /// <summary>
        ///  The contract multiplier
        /// </summary>
        ContractMultiplier,
        /// <summary>
        ///  The decay quantity
        /// </summary>
        DecayQuantity,
        /// <summary>
        ///  The original contract size
        /// </summary>
        OriginalContractSize,
        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        SecondReserved4,
        /// <summary>
        ///  The trading session date corresponding to the settlement price in trading_reference_price, in number of days since the UNIX epoch
        /// </summary>
        TradingReferenceDate,
        /// <summary>
        ///  The appl id
        /// </summary>
        ApplId,
        /// <summary>
        ///  The maturity year
        /// </summary>
        MaturityYear,
        /// <summary>
        ///  The decay start date
        /// </summary>
        DecayStartDate,
        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        Channel,
        /// <summary>
        ///  The currency
        /// </summary>
        Currency,
        /// <summary>
        ///  The settl currency
        /// </summary>
        SettlCurrency,
        /// <summary>
        ///  The secsubtype
        /// </summary>
        Secsubtype,
        /// <summary>
        ///  The raw symbol
        /// </summary>
        RawSymbol,
        /// <summary>
        ///  The group
        /// </summary>
        Group,
        /// <summary>
        ///  The exchange
        /// </summary>
        Exchange,
        /// <summary>
        ///  The asset
        /// </summary>
        Asset,
        /// <summary>
        ///  The cfi
        /// </summary>
        Cfi,
        /// <summary>
        ///  The security type
        /// </summary>
        SecurityType,
        /// <summary>
        ///  The unit of measure
        /// </summary>
        UnitOfMeasure,
        /// <summary>
        ///  The underlying
        /// </summary>
        Underlying,
        /// <summary>
        ///  The strike price currency
        /// </summary>
        StrikePriceCurrency,
        /// <summary>
        ///  The classification of the instrument
        /// </summary>
        InstrumentClass,
        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        Reserved2,
        /// <summary>
        ///  The strike price, where every 1 unit corresponds to 1e-9
        /// </summary>
        StrikePrice,
        /// <summary>
        ///  Reserved, 6 bytes
        /// </summary>
        Reserved6,
        /// <summary>
        ///  The matching algorithm used for the instrument, typically FIFO
        /// </summary>
        MatchAlgorithm,
        /// <summary>
        ///  The current trading state of the instrument
        /// </summary>
        MdSecurityTradingStatus,
        /// <summary>
        ///  The main fraction
        /// </summary>
        MainFraction,
        /// <summary>
        ///  The price display format
        /// </summary>
        PriceDisplayFormat,
        /// <summary>
        ///  The type indicators for the settlement price, as a bitmap
        /// </summary>
        SettlPriceType,
        /// <summary>
        ///  The sub fraction
        /// </summary>
        SubFraction,
        /// <summary>
        ///  The underlying product
        /// </summary>
        UnderlyingProduct,
        /// <summary>
        ///  Indicates if the instrument definition has been added, modified, or deleted
        /// </summary>
        SecurityUpdateAction,
        /// <summary>
        ///  The maturity month
        /// </summary>
        MaturityMonth,
        /// <summary>
        ///  The maturity day
        /// </summary>
        MaturityDay,
        /// <summary>
        ///  The maturity week
        /// </summary>
        MaturityWeek,
        /// <summary>
        ///  Indicates if the instrument is user defined
        /// </summary>
        UserDefinedInstrument,
        /// <summary>
        ///  The contract multiplier unit
        /// </summary>
        ContractMultiplierUnit,
        /// <summary>
        ///  The flow schedule type
        /// </summary>
        FlowScheduleType,
        /// <summary>
        ///  The tick rule
        /// </summary>
        TickRule,
        /// <summary>
        ///  Reserved, 3 bytes
        /// </summary>
        Dummy3,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly InstrumentDefMsg owner;

        internal FieldSet(InstrumentDefMsg owner)
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
        ///  The min price increment, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView MinPriceIncrement => this[FieldName.MinPriceIncrement];

        /// <summary>
        ///  The display factor, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView DisplayFactor => this[FieldName.DisplayFactor];

        /// <summary>
        ///  The last eligible trade time expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView Expiration => this[FieldName.Expiration];

        /// <summary>
        ///  The time of instrument activation expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public FieldView Activation => this[FieldName.Activation];

        /// <summary>
        ///  The high limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView HighLimitPrice => this[FieldName.HighLimitPrice];

        /// <summary>
        ///  The low limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView LowLimitPrice => this[FieldName.LowLimitPrice];

        /// <summary>
        ///  The max price variation, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView MaxPriceVariation => this[FieldName.MaxPriceVariation];

        /// <summary>
        ///  The trading session settlement price on trading_reference_date, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView TradingReferencePrice => this[FieldName.TradingReferencePrice];

        /// <summary>
        ///  The unit of measure qty, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView UnitOfMeasureQty => this[FieldName.UnitOfMeasureQty];

        /// <summary>
        ///  The min price increment amount, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView MinPriceIncrementAmount => this[FieldName.MinPriceIncrementAmount];

        /// <summary>
        ///  The price ratio, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView PriceRatio => this[FieldName.PriceRatio];

        /// <summary>
        ///  A bitmap of instrument eligibility attributes; venue-specific
        /// </summary>
        public FieldView InstAttribValue => this[FieldName.InstAttribValue];

        /// <summary>
        ///  The underlying id
        /// </summary>
        public FieldView UnderlyingId => this[FieldName.UnderlyingId];

        /// <summary>
        ///  The instrument ID assigned by the publisher; may be the same as instrument_id
        /// </summary>
        public FieldView RawInstrumentId => this[FieldName.RawInstrumentId];

        /// <summary>
        ///  The market depth implied
        /// </summary>
        public FieldView MarketDepthImplied => this[FieldName.MarketDepthImplied];

        /// <summary>
        ///  The market depth
        /// </summary>
        public FieldView MarketDepth => this[FieldName.MarketDepth];

        /// <summary>
        ///  The market segment id
        /// </summary>
        public FieldView MarketSegmentId => this[FieldName.MarketSegmentId];

        /// <summary>
        ///  The max trade vol
        /// </summary>
        public FieldView MaxTradeVol => this[FieldName.MaxTradeVol];

        /// <summary>
        ///  The min lot size
        /// </summary>
        public FieldView MinLotSize => this[FieldName.MinLotSize];

        /// <summary>
        ///  The min lot size block
        /// </summary>
        public FieldView MinLotSizeBlock => this[FieldName.MinLotSizeBlock];

        /// <summary>
        ///  The min lot size round lot
        /// </summary>
        public FieldView MinLotSizeRoundLot => this[FieldName.MinLotSizeRoundLot];

        /// <summary>
        ///  The min trade vol
        /// </summary>
        public FieldView MinTradeVol => this[FieldName.MinTradeVol];

        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        public FieldView Reserved4 => this[FieldName.Reserved4];

        /// <summary>
        ///  The contract multiplier
        /// </summary>
        public FieldView ContractMultiplier => this[FieldName.ContractMultiplier];

        /// <summary>
        ///  The decay quantity
        /// </summary>
        public FieldView DecayQuantity => this[FieldName.DecayQuantity];

        /// <summary>
        ///  The original contract size
        /// </summary>
        public FieldView OriginalContractSize => this[FieldName.OriginalContractSize];

        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        public FieldView SecondReserved4 => this[FieldName.SecondReserved4];

        /// <summary>
        ///  The trading session date corresponding to the settlement price in trading_reference_price, in number of days since the UNIX epoch
        /// </summary>
        public FieldView TradingReferenceDate => this[FieldName.TradingReferenceDate];

        /// <summary>
        ///  The appl id
        /// </summary>
        public FieldView ApplId => this[FieldName.ApplId];

        /// <summary>
        ///  The maturity year
        /// </summary>
        public FieldView MaturityYear => this[FieldName.MaturityYear];

        /// <summary>
        ///  The decay start date
        /// </summary>
        public FieldView DecayStartDate => this[FieldName.DecayStartDate];

        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public FieldView Channel => this[FieldName.Channel];

        /// <summary>
        ///  The currency
        /// </summary>
        public FieldView Currency => this[FieldName.Currency];

        /// <summary>
        ///  The settl currency
        /// </summary>
        public FieldView SettlCurrency => this[FieldName.SettlCurrency];

        /// <summary>
        ///  The secsubtype
        /// </summary>
        public FieldView Secsubtype => this[FieldName.Secsubtype];

        /// <summary>
        ///  The raw symbol
        /// </summary>
        public FieldView RawSymbol => this[FieldName.RawSymbol];

        /// <summary>
        ///  The group
        /// </summary>
        public FieldView Group => this[FieldName.Group];

        /// <summary>
        ///  The exchange
        /// </summary>
        public FieldView Exchange => this[FieldName.Exchange];

        /// <summary>
        ///  The asset
        /// </summary>
        public FieldView Asset => this[FieldName.Asset];

        /// <summary>
        ///  The cfi
        /// </summary>
        public FieldView Cfi => this[FieldName.Cfi];

        /// <summary>
        ///  The security type
        /// </summary>
        public FieldView SecurityType => this[FieldName.SecurityType];

        /// <summary>
        ///  The unit of measure
        /// </summary>
        public FieldView UnitOfMeasure => this[FieldName.UnitOfMeasure];

        /// <summary>
        ///  The underlying
        /// </summary>
        public FieldView Underlying => this[FieldName.Underlying];

        /// <summary>
        ///  The strike price currency
        /// </summary>
        public FieldView StrikePriceCurrency => this[FieldName.StrikePriceCurrency];

        /// <summary>
        ///  The classification of the instrument
        /// </summary>
        public FieldView InstrumentClass => this[FieldName.InstrumentClass];

        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        public FieldView Reserved2 => this[FieldName.Reserved2];

        /// <summary>
        ///  The strike price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public FieldView StrikePrice => this[FieldName.StrikePrice];

        /// <summary>
        ///  Reserved, 6 bytes
        /// </summary>
        public FieldView Reserved6 => this[FieldName.Reserved6];

        /// <summary>
        ///  The matching algorithm used for the instrument, typically FIFO
        /// </summary>
        public FieldView MatchAlgorithm => this[FieldName.MatchAlgorithm];

        /// <summary>
        ///  The current trading state of the instrument
        /// </summary>
        public FieldView MdSecurityTradingStatus => this[FieldName.MdSecurityTradingStatus];

        /// <summary>
        ///  The main fraction
        /// </summary>
        public FieldView MainFraction => this[FieldName.MainFraction];

        /// <summary>
        ///  The price display format
        /// </summary>
        public FieldView PriceDisplayFormat => this[FieldName.PriceDisplayFormat];

        /// <summary>
        ///  The type indicators for the settlement price, as a bitmap
        /// </summary>
        public FieldView SettlPriceType => this[FieldName.SettlPriceType];

        /// <summary>
        ///  The sub fraction
        /// </summary>
        public FieldView SubFraction => this[FieldName.SubFraction];

        /// <summary>
        ///  The underlying product
        /// </summary>
        public FieldView UnderlyingProduct => this[FieldName.UnderlyingProduct];

        /// <summary>
        ///  Indicates if the instrument definition has been added, modified, or deleted
        /// </summary>
        public FieldView SecurityUpdateAction => this[FieldName.SecurityUpdateAction];

        /// <summary>
        ///  The maturity month
        /// </summary>
        public FieldView MaturityMonth => this[FieldName.MaturityMonth];

        /// <summary>
        ///  The maturity day
        /// </summary>
        public FieldView MaturityDay => this[FieldName.MaturityDay];

        /// <summary>
        ///  The maturity week
        /// </summary>
        public FieldView MaturityWeek => this[FieldName.MaturityWeek];

        /// <summary>
        ///  Indicates if the instrument is user defined
        /// </summary>
        public FieldView UserDefinedInstrument => this[FieldName.UserDefinedInstrument];

        /// <summary>
        ///  The contract multiplier unit
        /// </summary>
        public FieldView ContractMultiplierUnit => this[FieldName.ContractMultiplierUnit];

        /// <summary>
        ///  The flow schedule type
        /// </summary>
        public FieldView FlowScheduleType => this[FieldName.FlowScheduleType];

        /// <summary>
        ///  The tick rule
        /// </summary>
        public FieldView TickRule => this[FieldName.TickRule];

        /// <summary>
        ///  Reserved, 3 bytes
        /// </summary>
        public FieldView Dummy3 => this[FieldName.Dummy3];

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
        new(nameof(FieldName.MinPriceIncrement), FieldKind.Decimal, MinPriceIncrementLength) { Scale = MinPriceIncrementScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = MinPriceIncrementNullValue },
        new(nameof(FieldName.DisplayFactor), FieldKind.Decimal, DisplayFactorLength) { Scale = DisplayFactorScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = DisplayFactorNullValue },
        new(nameof(FieldName.Expiration), FieldKind.DateTime, ExpirationLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = ExpirationNullValue },
        new(nameof(FieldName.Activation), FieldKind.DateTime, ActivationLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = ActivationNullValue },
        new(nameof(FieldName.HighLimitPrice), FieldKind.Decimal, HighLimitPriceLength) { Scale = HighLimitPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = HighLimitPriceNullValue },
        new(nameof(FieldName.LowLimitPrice), FieldKind.Decimal, LowLimitPriceLength) { Scale = LowLimitPriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = LowLimitPriceNullValue },
        new(nameof(FieldName.MaxPriceVariation), FieldKind.Decimal, MaxPriceVariationLength) { Scale = MaxPriceVariationScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = MaxPriceVariationNullValue },
        new(nameof(FieldName.TradingReferencePrice), FieldKind.Decimal, TradingReferencePriceLength) { Scale = TradingReferencePriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = TradingReferencePriceNullValue },
        new(nameof(FieldName.UnitOfMeasureQty), FieldKind.Decimal, UnitOfMeasureQtyLength) { Scale = UnitOfMeasureQtyScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = UnitOfMeasureQtyNullValue },
        new(nameof(FieldName.MinPriceIncrementAmount), FieldKind.Decimal, MinPriceIncrementAmountLength) { Scale = MinPriceIncrementAmountScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = MinPriceIncrementAmountNullValue },
        new(nameof(FieldName.PriceRatio), FieldKind.Decimal, PriceRatioLength) { Scale = PriceRatioScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = PriceRatioNullValue },
        new(nameof(FieldName.InstAttribValue), FieldKind.SignedInteger, InstAttribValueLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.UnderlyingId), FieldKind.UnsignedInteger, UnderlyingIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.RawInstrumentId), FieldKind.UnsignedInteger, RawInstrumentIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.MarketDepthImplied), FieldKind.SignedInteger, MarketDepthImpliedLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MarketDepth), FieldKind.SignedInteger, MarketDepthLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MarketSegmentId), FieldKind.UnsignedInteger, MarketSegmentIdLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.MaxTradeVol), FieldKind.UnsignedInteger, MaxTradeVolLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.MinLotSize), FieldKind.SignedInteger, MinLotSizeLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MinLotSizeBlock), FieldKind.SignedInteger, MinLotSizeBlockLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MinLotSizeRoundLot), FieldKind.SignedInteger, MinLotSizeRoundLotLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MinTradeVol), FieldKind.UnsignedInteger, MinTradeVolLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Reserved4), FieldKind.Padding, Reserved4Length),
        new(nameof(FieldName.ContractMultiplier), FieldKind.SignedInteger, ContractMultiplierLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.DecayQuantity), FieldKind.SignedInteger, DecayQuantityLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.OriginalContractSize), FieldKind.SignedInteger, OriginalContractSizeLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.SecondReserved4), FieldKind.Padding, SecondReserved4Length),
        new(nameof(FieldName.TradingReferenceDate), FieldKind.UnsignedInteger, TradingReferenceDateLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.ApplId), FieldKind.SignedInteger, ApplIdLength) { ByteOrder = "Little", IsSigned = true },
        new(nameof(FieldName.MaturityYear), FieldKind.UnsignedInteger, MaturityYearLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.DecayStartDate), FieldKind.UnsignedInteger, DecayStartDateLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Channel), FieldKind.UnsignedInteger, ChannelLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Currency), FieldKind.String, CurrencyLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.SettlCurrency), FieldKind.String, SettlCurrencyLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Secsubtype), FieldKind.String, SecsubtypeLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.RawSymbol), FieldKind.String, RawSymbolLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Group), FieldKind.String, GroupLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Exchange), FieldKind.String, ExchangeLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Asset), FieldKind.String, AssetLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Cfi), FieldKind.String, CfiLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.SecurityType), FieldKind.String, SecurityTypeLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.UnitOfMeasure), FieldKind.String, UnitOfMeasureLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Underlying), FieldKind.String, UnderlyingLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.StrikePriceCurrency), FieldKind.String, StrikePriceCurrencyLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.InstrumentClass), FieldKind.Enum, InstrumentClassLength),
        new(nameof(FieldName.Reserved2), FieldKind.Padding, Reserved2Length),
        new(nameof(FieldName.StrikePrice), FieldKind.Decimal, StrikePriceLength) { Scale = StrikePriceScale, ByteOrder = "Little", IsSigned = true, Nullable = "Value", NullValue = StrikePriceNullValue },
        new(nameof(FieldName.Reserved6), FieldKind.Padding, Reserved6Length),
        new(nameof(FieldName.MatchAlgorithm), FieldKind.Enum, MatchAlgorithmLength),
        new(nameof(FieldName.MdSecurityTradingStatus), FieldKind.UnsignedInteger, MdSecurityTradingStatusLength) { IsSigned = false },
        new(nameof(FieldName.MainFraction), FieldKind.UnsignedInteger, MainFractionLength) { IsSigned = false },
        new(nameof(FieldName.PriceDisplayFormat), FieldKind.UnsignedInteger, PriceDisplayFormatLength) { IsSigned = false },
        new(nameof(FieldName.SettlPriceType), FieldKind.UnsignedInteger, SettlPriceTypeLength) { IsSigned = false },
        new(nameof(FieldName.SubFraction), FieldKind.UnsignedInteger, SubFractionLength) { IsSigned = false },
        new(nameof(FieldName.UnderlyingProduct), FieldKind.UnsignedInteger, UnderlyingProductLength) { IsSigned = false },
        new(nameof(FieldName.SecurityUpdateAction), FieldKind.Enum, SecurityUpdateActionLength),
        new(nameof(FieldName.MaturityMonth), FieldKind.UnsignedInteger, MaturityMonthLength) { IsSigned = false },
        new(nameof(FieldName.MaturityDay), FieldKind.UnsignedInteger, MaturityDayLength) { IsSigned = false },
        new(nameof(FieldName.MaturityWeek), FieldKind.UnsignedInteger, MaturityWeekLength) { IsSigned = false },
        new(nameof(FieldName.UserDefinedInstrument), FieldKind.Enum, UserDefinedInstrumentLength),
        new(nameof(FieldName.ContractMultiplierUnit), FieldKind.SignedInteger, ContractMultiplierUnitLength) { IsSigned = true },
        new(nameof(FieldName.FlowScheduleType), FieldKind.SignedInteger, FlowScheduleTypeLength) { IsSigned = true },
        new(nameof(FieldName.TickRule), FieldKind.UnsignedInteger, TickRuleLength) { IsSigned = false },
        new(nameof(FieldName.Dummy3), FieldKind.Padding, Dummy3Length),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IMessage.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="InstrumentDefMsg"/> class with default values.
    /// </summary>
    public InstrumentDefMsg()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="InstrumentDefMsg"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public InstrumentDefMsg(InstrumentDefMsg other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(InstrumentDefMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.tsRecv = other.tsRecv;
        this.minPriceIncrement = other.minPriceIncrement;
        this.displayFactor = other.displayFactor;
        this.expiration = other.expiration;
        this.activation = other.activation;
        this.highLimitPrice = other.highLimitPrice;
        this.lowLimitPrice = other.lowLimitPrice;
        this.maxPriceVariation = other.maxPriceVariation;
        this.tradingReferencePrice = other.tradingReferencePrice;
        this.unitOfMeasureQty = other.unitOfMeasureQty;
        this.minPriceIncrementAmount = other.minPriceIncrementAmount;
        this.priceRatio = other.priceRatio;
        this.instAttribValue = other.instAttribValue;
        this.underlyingId = other.underlyingId;
        this.rawInstrumentId = other.rawInstrumentId;
        this.marketDepthImplied = other.marketDepthImplied;
        this.marketDepth = other.marketDepth;
        this.marketSegmentId = other.marketSegmentId;
        this.maxTradeVol = other.maxTradeVol;
        this.minLotSize = other.minLotSize;
        this.minLotSizeBlock = other.minLotSizeBlock;
        this.minLotSizeRoundLot = other.minLotSizeRoundLot;
        this.minTradeVol = other.minTradeVol;
        this.contractMultiplier = other.contractMultiplier;
        this.decayQuantity = other.decayQuantity;
        this.originalContractSize = other.originalContractSize;
        this.tradingReferenceDate = other.tradingReferenceDate;
        this.applId = other.applId;
        this.maturityYear = other.maturityYear;
        this.decayStartDate = other.decayStartDate;
        this.channel = other.channel;
        this.currency = other.currency;
        this.settlCurrency = other.settlCurrency;
        this.secsubtype = other.secsubtype;
        this.rawSymbol = other.rawSymbol;
        this.group = other.group;
        this.exchange = other.exchange;
        this.asset = other.asset;
        this.cfi = other.cfi;
        this.securityType = other.securityType;
        this.unitOfMeasure = other.unitOfMeasure;
        this.underlying = other.underlying;
        this.strikePriceCurrency = other.strikePriceCurrency;
        this.instrumentClass = other.instrumentClass;
        this.strikePrice = other.strikePrice;
        this.matchAlgorithm = other.matchAlgorithm;
        this.mdSecurityTradingStatus = other.mdSecurityTradingStatus;
        this.mainFraction = other.mainFraction;
        this.priceDisplayFormat = other.priceDisplayFormat;
        this.settlPriceType = other.settlPriceType;
        this.subFraction = other.subFraction;
        this.underlyingProduct = other.underlyingProduct;
        this.securityUpdateAction = other.securityUpdateAction;
        this.maturityMonth = other.maturityMonth;
        this.maturityDay = other.maturityDay;
        this.maturityWeek = other.maturityWeek;
        this.userDefinedInstrument = other.userDefinedInstrument;
        this.contractMultiplierUnit = other.contractMultiplierUnit;
        this.flowScheduleType = other.flowScheduleType;
        this.tickRule = other.tickRule;
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
        this.minPriceIncrement = default;
        this.displayFactor = default;
        this.expiration = default;
        this.activation = default;
        this.highLimitPrice = default;
        this.lowLimitPrice = default;
        this.maxPriceVariation = default;
        this.tradingReferencePrice = default;
        this.unitOfMeasureQty = default;
        this.minPriceIncrementAmount = default;
        this.priceRatio = default;
        this.instAttribValue = default;
        this.underlyingId = default;
        this.rawInstrumentId = default;
        this.marketDepthImplied = default;
        this.marketDepth = default;
        this.marketSegmentId = default;
        this.maxTradeVol = default;
        this.minLotSize = default;
        this.minLotSizeBlock = default;
        this.minLotSizeRoundLot = default;
        this.minTradeVol = default;
        this.contractMultiplier = default;
        this.decayQuantity = default;
        this.originalContractSize = default;
        this.tradingReferenceDate = default;
        this.applId = default;
        this.maturityYear = default;
        this.decayStartDate = default;
        this.channel = default;
        this.currency = string.Empty;
        this.settlCurrency = string.Empty;
        this.secsubtype = string.Empty;
        this.rawSymbol = string.Empty;
        this.group = string.Empty;
        this.exchange = string.Empty;
        this.asset = string.Empty;
        this.cfi = string.Empty;
        this.securityType = string.Empty;
        this.unitOfMeasure = string.Empty;
        this.underlying = string.Empty;
        this.strikePriceCurrency = string.Empty;
        this.instrumentClass = default;
        this.strikePrice = default;
        this.matchAlgorithm = default;
        this.mdSecurityTradingStatus = default;
        this.mainFraction = default;
        this.priceDisplayFormat = default;
        this.settlPriceType = default;
        this.subFraction = default;
        this.underlyingProduct = default;
        this.securityUpdateAction = default;
        this.maturityMonth = default;
        this.maturityDay = default;
        this.maturityWeek = default;
        this.userDefinedInstrument = default;
        this.contractMultiplierUnit = default;
        this.flowScheduleType = default;
        this.tickRule = default;
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
    public static InstrumentDefMsg Parse(ReadOnlySpan<byte> data)
    {
        var instance = new InstrumentDefMsg();
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
    public static InstrumentDefMsg Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new InstrumentDefMsg();
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
        if (Truncated(data, start, cursor, MinPriceIncrementLength, FieldName.MinPriceIncrement)) return;
        this.minPriceIncrement = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, MinPriceIncrementLength));
        cursor += MinPriceIncrementLength;
        if (Truncated(data, start, cursor, DisplayFactorLength, FieldName.DisplayFactor)) return;
        this.displayFactor = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, DisplayFactorLength));
        cursor += DisplayFactorLength;
        if (Truncated(data, start, cursor, ExpirationLength, FieldName.Expiration)) return;
        this.expiration = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, ExpirationLength));
        cursor += ExpirationLength;
        if (Truncated(data, start, cursor, ActivationLength, FieldName.Activation)) return;
        this.activation = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, ActivationLength));
        cursor += ActivationLength;
        if (Truncated(data, start, cursor, HighLimitPriceLength, FieldName.HighLimitPrice)) return;
        this.highLimitPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, HighLimitPriceLength));
        cursor += HighLimitPriceLength;
        if (Truncated(data, start, cursor, LowLimitPriceLength, FieldName.LowLimitPrice)) return;
        this.lowLimitPrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, LowLimitPriceLength));
        cursor += LowLimitPriceLength;
        if (Truncated(data, start, cursor, MaxPriceVariationLength, FieldName.MaxPriceVariation)) return;
        this.maxPriceVariation = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, MaxPriceVariationLength));
        cursor += MaxPriceVariationLength;
        if (Truncated(data, start, cursor, TradingReferencePriceLength, FieldName.TradingReferencePrice)) return;
        this.tradingReferencePrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, TradingReferencePriceLength));
        cursor += TradingReferencePriceLength;
        if (Truncated(data, start, cursor, UnitOfMeasureQtyLength, FieldName.UnitOfMeasureQty)) return;
        this.unitOfMeasureQty = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, UnitOfMeasureQtyLength));
        cursor += UnitOfMeasureQtyLength;
        if (Truncated(data, start, cursor, MinPriceIncrementAmountLength, FieldName.MinPriceIncrementAmount)) return;
        this.minPriceIncrementAmount = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, MinPriceIncrementAmountLength));
        cursor += MinPriceIncrementAmountLength;
        if (Truncated(data, start, cursor, PriceRatioLength, FieldName.PriceRatio)) return;
        this.priceRatio = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, PriceRatioLength));
        cursor += PriceRatioLength;
        if (Truncated(data, start, cursor, InstAttribValueLength, FieldName.InstAttribValue)) return;
        this.instAttribValue = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, InstAttribValueLength));
        cursor += InstAttribValueLength;
        if (Truncated(data, start, cursor, UnderlyingIdLength, FieldName.UnderlyingId)) return;
        this.underlyingId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, UnderlyingIdLength));
        cursor += UnderlyingIdLength;
        if (Truncated(data, start, cursor, RawInstrumentIdLength, FieldName.RawInstrumentId)) return;
        this.rawInstrumentId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, RawInstrumentIdLength));
        cursor += RawInstrumentIdLength;
        if (Truncated(data, start, cursor, MarketDepthImpliedLength, FieldName.MarketDepthImplied)) return;
        this.marketDepthImplied = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, MarketDepthImpliedLength));
        cursor += MarketDepthImpliedLength;
        if (Truncated(data, start, cursor, MarketDepthLength, FieldName.MarketDepth)) return;
        this.marketDepth = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, MarketDepthLength));
        cursor += MarketDepthLength;
        if (Truncated(data, start, cursor, MarketSegmentIdLength, FieldName.MarketSegmentId)) return;
        this.marketSegmentId = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MarketSegmentIdLength));
        cursor += MarketSegmentIdLength;
        if (Truncated(data, start, cursor, MaxTradeVolLength, FieldName.MaxTradeVol)) return;
        this.maxTradeVol = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MaxTradeVolLength));
        cursor += MaxTradeVolLength;
        if (Truncated(data, start, cursor, MinLotSizeLength, FieldName.MinLotSize)) return;
        this.minLotSize = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, MinLotSizeLength));
        cursor += MinLotSizeLength;
        if (Truncated(data, start, cursor, MinLotSizeBlockLength, FieldName.MinLotSizeBlock)) return;
        this.minLotSizeBlock = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, MinLotSizeBlockLength));
        cursor += MinLotSizeBlockLength;
        if (Truncated(data, start, cursor, MinLotSizeRoundLotLength, FieldName.MinLotSizeRoundLot)) return;
        this.minLotSizeRoundLot = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, MinLotSizeRoundLotLength));
        cursor += MinLotSizeRoundLotLength;
        if (Truncated(data, start, cursor, MinTradeVolLength, FieldName.MinTradeVol)) return;
        this.minTradeVol = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MinTradeVolLength));
        cursor += MinTradeVolLength;
        if (Truncated(data, start, cursor, Reserved4Length, FieldName.Reserved4)) return;
        cursor += Reserved4Length;
        if (Truncated(data, start, cursor, ContractMultiplierLength, FieldName.ContractMultiplier)) return;
        this.contractMultiplier = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, ContractMultiplierLength));
        cursor += ContractMultiplierLength;
        if (Truncated(data, start, cursor, DecayQuantityLength, FieldName.DecayQuantity)) return;
        this.decayQuantity = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, DecayQuantityLength));
        cursor += DecayQuantityLength;
        if (Truncated(data, start, cursor, OriginalContractSizeLength, FieldName.OriginalContractSize)) return;
        this.originalContractSize = BinaryPrimitives.ReadInt32LittleEndian(data.Slice(cursor, OriginalContractSizeLength));
        cursor += OriginalContractSizeLength;
        if (Truncated(data, start, cursor, SecondReserved4Length, FieldName.SecondReserved4)) return;
        cursor += SecondReserved4Length;
        if (Truncated(data, start, cursor, TradingReferenceDateLength, FieldName.TradingReferenceDate)) return;
        this.tradingReferenceDate = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, TradingReferenceDateLength));
        cursor += TradingReferenceDateLength;
        if (Truncated(data, start, cursor, ApplIdLength, FieldName.ApplId)) return;
        this.applId = BinaryPrimitives.ReadInt16LittleEndian(data.Slice(cursor, ApplIdLength));
        cursor += ApplIdLength;
        if (Truncated(data, start, cursor, MaturityYearLength, FieldName.MaturityYear)) return;
        this.maturityYear = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, MaturityYearLength));
        cursor += MaturityYearLength;
        if (Truncated(data, start, cursor, DecayStartDateLength, FieldName.DecayStartDate)) return;
        this.decayStartDate = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, DecayStartDateLength));
        cursor += DecayStartDateLength;
        if (Truncated(data, start, cursor, ChannelLength, FieldName.Channel)) return;
        this.channel = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, ChannelLength));
        cursor += ChannelLength;
        if (Truncated(data, start, cursor, CurrencyLength, FieldName.Currency)) return;
        this.currency = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, CurrencyLength), CurrencyFill);
        cursor += CurrencyLength;
        if (Truncated(data, start, cursor, SettlCurrencyLength, FieldName.SettlCurrency)) return;
        this.settlCurrency = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SettlCurrencyLength), SettlCurrencyFill);
        cursor += SettlCurrencyLength;
        if (Truncated(data, start, cursor, SecsubtypeLength, FieldName.Secsubtype)) return;
        this.secsubtype = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SecsubtypeLength), SecsubtypeFill);
        cursor += SecsubtypeLength;
        if (Truncated(data, start, cursor, RawSymbolLength, FieldName.RawSymbol)) return;
        this.rawSymbol = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, RawSymbolLength), RawSymbolFill);
        cursor += RawSymbolLength;
        if (Truncated(data, start, cursor, GroupLength, FieldName.Group)) return;
        this.group = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, GroupLength), GroupFill);
        cursor += GroupLength;
        if (Truncated(data, start, cursor, ExchangeLength, FieldName.Exchange)) return;
        this.exchange = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, ExchangeLength), ExchangeFill);
        cursor += ExchangeLength;
        if (Truncated(data, start, cursor, AssetLength, FieldName.Asset)) return;
        this.asset = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, AssetLength), AssetFill);
        cursor += AssetLength;
        if (Truncated(data, start, cursor, CfiLength, FieldName.Cfi)) return;
        this.cfi = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, CfiLength), CfiFill);
        cursor += CfiLength;
        if (Truncated(data, start, cursor, SecurityTypeLength, FieldName.SecurityType)) return;
        this.securityType = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SecurityTypeLength), SecurityTypeFill);
        cursor += SecurityTypeLength;
        if (Truncated(data, start, cursor, UnitOfMeasureLength, FieldName.UnitOfMeasure)) return;
        this.unitOfMeasure = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, UnitOfMeasureLength), UnitOfMeasureFill);
        cursor += UnitOfMeasureLength;
        if (Truncated(data, start, cursor, UnderlyingLength, FieldName.Underlying)) return;
        this.underlying = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, UnderlyingLength), UnderlyingFill);
        cursor += UnderlyingLength;
        if (Truncated(data, start, cursor, StrikePriceCurrencyLength, FieldName.StrikePriceCurrency)) return;
        this.strikePriceCurrency = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, StrikePriceCurrencyLength), StrikePriceCurrencyFill);
        cursor += StrikePriceCurrencyLength;
        if (Truncated(data, start, cursor, InstrumentClassLength, FieldName.InstrumentClass)) return;
        this.instrumentClass = (InstrumentClass)data.Slice(cursor, InstrumentClassLength)[0];
        cursor += InstrumentClassLength;
        if (Truncated(data, start, cursor, Reserved2Length, FieldName.Reserved2)) return;
        cursor += Reserved2Length;
        if (Truncated(data, start, cursor, StrikePriceLength, FieldName.StrikePrice)) return;
        this.strikePrice = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(cursor, StrikePriceLength));
        cursor += StrikePriceLength;
        if (Truncated(data, start, cursor, Reserved6Length, FieldName.Reserved6)) return;
        cursor += Reserved6Length;
        if (Truncated(data, start, cursor, MatchAlgorithmLength, FieldName.MatchAlgorithm)) return;
        this.matchAlgorithm = (MatchAlgorithm)data.Slice(cursor, MatchAlgorithmLength)[0];
        cursor += MatchAlgorithmLength;
        if (Truncated(data, start, cursor, MdSecurityTradingStatusLength, FieldName.MdSecurityTradingStatus)) return;
        this.mdSecurityTradingStatus = data.Slice(cursor, MdSecurityTradingStatusLength)[0];
        cursor += MdSecurityTradingStatusLength;
        if (Truncated(data, start, cursor, MainFractionLength, FieldName.MainFraction)) return;
        this.mainFraction = data.Slice(cursor, MainFractionLength)[0];
        cursor += MainFractionLength;
        if (Truncated(data, start, cursor, PriceDisplayFormatLength, FieldName.PriceDisplayFormat)) return;
        this.priceDisplayFormat = data.Slice(cursor, PriceDisplayFormatLength)[0];
        cursor += PriceDisplayFormatLength;
        if (Truncated(data, start, cursor, SettlPriceTypeLength, FieldName.SettlPriceType)) return;
        this.settlPriceType = data.Slice(cursor, SettlPriceTypeLength)[0];
        cursor += SettlPriceTypeLength;
        if (Truncated(data, start, cursor, SubFractionLength, FieldName.SubFraction)) return;
        this.subFraction = data.Slice(cursor, SubFractionLength)[0];
        cursor += SubFractionLength;
        if (Truncated(data, start, cursor, UnderlyingProductLength, FieldName.UnderlyingProduct)) return;
        this.underlyingProduct = data.Slice(cursor, UnderlyingProductLength)[0];
        cursor += UnderlyingProductLength;
        if (Truncated(data, start, cursor, SecurityUpdateActionLength, FieldName.SecurityUpdateAction)) return;
        this.securityUpdateAction = (SecurityUpdateAction)data.Slice(cursor, SecurityUpdateActionLength)[0];
        cursor += SecurityUpdateActionLength;
        if (Truncated(data, start, cursor, MaturityMonthLength, FieldName.MaturityMonth)) return;
        this.maturityMonth = data.Slice(cursor, MaturityMonthLength)[0];
        cursor += MaturityMonthLength;
        if (Truncated(data, start, cursor, MaturityDayLength, FieldName.MaturityDay)) return;
        this.maturityDay = data.Slice(cursor, MaturityDayLength)[0];
        cursor += MaturityDayLength;
        if (Truncated(data, start, cursor, MaturityWeekLength, FieldName.MaturityWeek)) return;
        this.maturityWeek = data.Slice(cursor, MaturityWeekLength)[0];
        cursor += MaturityWeekLength;
        if (Truncated(data, start, cursor, UserDefinedInstrumentLength, FieldName.UserDefinedInstrument)) return;
        this.userDefinedInstrument = (UserDefinedInstrument)data.Slice(cursor, UserDefinedInstrumentLength)[0];
        cursor += UserDefinedInstrumentLength;
        if (Truncated(data, start, cursor, ContractMultiplierUnitLength, FieldName.ContractMultiplierUnit)) return;
        this.contractMultiplierUnit = (sbyte)data.Slice(cursor, ContractMultiplierUnitLength)[0];
        cursor += ContractMultiplierUnitLength;
        if (Truncated(data, start, cursor, FlowScheduleTypeLength, FieldName.FlowScheduleType)) return;
        this.flowScheduleType = (sbyte)data.Slice(cursor, FlowScheduleTypeLength)[0];
        cursor += FlowScheduleTypeLength;
        if (Truncated(data, start, cursor, TickRuleLength, FieldName.TickRule)) return;
        this.tickRule = data.Slice(cursor, TickRuleLength)[0];
        cursor += TickRuleLength;
        if (Truncated(data, start, cursor, Dummy3Length, FieldName.Dummy3)) return;
        cursor += Dummy3Length;
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
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, MinPriceIncrementLength), this.minPriceIncrement);
        cursor += MinPriceIncrementLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, DisplayFactorLength), this.displayFactor);
        cursor += DisplayFactorLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, ExpirationLength), this.expiration);
        cursor += ExpirationLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, ActivationLength), this.activation);
        cursor += ActivationLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, HighLimitPriceLength), this.highLimitPrice);
        cursor += HighLimitPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, LowLimitPriceLength), this.lowLimitPrice);
        cursor += LowLimitPriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, MaxPriceVariationLength), this.maxPriceVariation);
        cursor += MaxPriceVariationLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, TradingReferencePriceLength), this.tradingReferencePrice);
        cursor += TradingReferencePriceLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, UnitOfMeasureQtyLength), this.unitOfMeasureQty);
        cursor += UnitOfMeasureQtyLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, MinPriceIncrementAmountLength), this.minPriceIncrementAmount);
        cursor += MinPriceIncrementAmountLength;
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, PriceRatioLength), this.priceRatio);
        cursor += PriceRatioLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, InstAttribValueLength), this.instAttribValue);
        cursor += InstAttribValueLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, UnderlyingIdLength), this.underlyingId);
        cursor += UnderlyingIdLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, RawInstrumentIdLength), this.rawInstrumentId);
        cursor += RawInstrumentIdLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, MarketDepthImpliedLength), this.marketDepthImplied);
        cursor += MarketDepthImpliedLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, MarketDepthLength), this.marketDepth);
        cursor += MarketDepthLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MarketSegmentIdLength), this.marketSegmentId);
        cursor += MarketSegmentIdLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MaxTradeVolLength), this.maxTradeVol);
        cursor += MaxTradeVolLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, MinLotSizeLength), this.minLotSize);
        cursor += MinLotSizeLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, MinLotSizeBlockLength), this.minLotSizeBlock);
        cursor += MinLotSizeBlockLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, MinLotSizeRoundLotLength), this.minLotSizeRoundLot);
        cursor += MinLotSizeRoundLotLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MinTradeVolLength), this.minTradeVol);
        cursor += MinTradeVolLength;
        {
            if (this.raw.Length >= Reserved4Offset + Reserved4Length)
            {
                this.raw.AsSpan(Reserved4Offset, Reserved4Length).CopyTo(data.Slice(cursor, Reserved4Length));
            }
            else
            {
                data.Slice(cursor, Reserved4Length).Clear();
            }
            cursor += Reserved4Length;
        }
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, ContractMultiplierLength), this.contractMultiplier);
        cursor += ContractMultiplierLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, DecayQuantityLength), this.decayQuantity);
        cursor += DecayQuantityLength;
        BinaryPrimitives.WriteInt32LittleEndian(data.Slice(cursor, OriginalContractSizeLength), this.originalContractSize);
        cursor += OriginalContractSizeLength;
        {
            if (this.raw.Length >= SecondReserved4Offset + SecondReserved4Length)
            {
                this.raw.AsSpan(SecondReserved4Offset, SecondReserved4Length).CopyTo(data.Slice(cursor, SecondReserved4Length));
            }
            else
            {
                data.Slice(cursor, SecondReserved4Length).Clear();
            }
            cursor += SecondReserved4Length;
        }
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, TradingReferenceDateLength), this.tradingReferenceDate);
        cursor += TradingReferenceDateLength;
        BinaryPrimitives.WriteInt16LittleEndian(data.Slice(cursor, ApplIdLength), this.applId);
        cursor += ApplIdLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, MaturityYearLength), this.maturityYear);
        cursor += MaturityYearLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, DecayStartDateLength), this.decayStartDate);
        cursor += DecayStartDateLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, ChannelLength), this.channel);
        cursor += ChannelLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, CurrencyLength), TextEncoding.Ascii.Fit(this.currency, CurrencyLength), CurrencyFill);
        cursor += CurrencyLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SettlCurrencyLength), TextEncoding.Ascii.Fit(this.settlCurrency, SettlCurrencyLength), SettlCurrencyFill);
        cursor += SettlCurrencyLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SecsubtypeLength), TextEncoding.Ascii.Fit(this.secsubtype, SecsubtypeLength), SecsubtypeFill);
        cursor += SecsubtypeLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, RawSymbolLength), TextEncoding.Ascii.Fit(this.rawSymbol, RawSymbolLength), RawSymbolFill);
        cursor += RawSymbolLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, GroupLength), TextEncoding.Ascii.Fit(this.group, GroupLength), GroupFill);
        cursor += GroupLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, ExchangeLength), TextEncoding.Ascii.Fit(this.exchange, ExchangeLength), ExchangeFill);
        cursor += ExchangeLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, AssetLength), TextEncoding.Ascii.Fit(this.asset, AssetLength), AssetFill);
        cursor += AssetLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, CfiLength), TextEncoding.Ascii.Fit(this.cfi, CfiLength), CfiFill);
        cursor += CfiLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SecurityTypeLength), TextEncoding.Ascii.Fit(this.securityType, SecurityTypeLength), SecurityTypeFill);
        cursor += SecurityTypeLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, UnitOfMeasureLength), TextEncoding.Ascii.Fit(this.unitOfMeasure, UnitOfMeasureLength), UnitOfMeasureFill);
        cursor += UnitOfMeasureLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, UnderlyingLength), TextEncoding.Ascii.Fit(this.underlying, UnderlyingLength), UnderlyingFill);
        cursor += UnderlyingLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, StrikePriceCurrencyLength), TextEncoding.Ascii.Fit(this.strikePriceCurrency, StrikePriceCurrencyLength), StrikePriceCurrencyFill);
        cursor += StrikePriceCurrencyLength;
        data.Slice(cursor, InstrumentClassLength)[0] = unchecked((byte)(this.instrumentClass));
        cursor += InstrumentClassLength;
        {
            if (this.raw.Length >= Reserved2Offset + Reserved2Length)
            {
                this.raw.AsSpan(Reserved2Offset, Reserved2Length).CopyTo(data.Slice(cursor, Reserved2Length));
            }
            else
            {
                data.Slice(cursor, Reserved2Length).Clear();
            }
            cursor += Reserved2Length;
        }
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(cursor, StrikePriceLength), this.strikePrice);
        cursor += StrikePriceLength;
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
        data.Slice(cursor, MatchAlgorithmLength)[0] = unchecked((byte)(this.matchAlgorithm));
        cursor += MatchAlgorithmLength;
        data.Slice(cursor, MdSecurityTradingStatusLength)[0] = unchecked((byte)(this.mdSecurityTradingStatus));
        cursor += MdSecurityTradingStatusLength;
        data.Slice(cursor, MainFractionLength)[0] = unchecked((byte)(this.mainFraction));
        cursor += MainFractionLength;
        data.Slice(cursor, PriceDisplayFormatLength)[0] = unchecked((byte)(this.priceDisplayFormat));
        cursor += PriceDisplayFormatLength;
        data.Slice(cursor, SettlPriceTypeLength)[0] = unchecked((byte)(this.settlPriceType));
        cursor += SettlPriceTypeLength;
        data.Slice(cursor, SubFractionLength)[0] = unchecked((byte)(this.subFraction));
        cursor += SubFractionLength;
        data.Slice(cursor, UnderlyingProductLength)[0] = unchecked((byte)(this.underlyingProduct));
        cursor += UnderlyingProductLength;
        data.Slice(cursor, SecurityUpdateActionLength)[0] = unchecked((byte)(this.securityUpdateAction));
        cursor += SecurityUpdateActionLength;
        data.Slice(cursor, MaturityMonthLength)[0] = unchecked((byte)(this.maturityMonth));
        cursor += MaturityMonthLength;
        data.Slice(cursor, MaturityDayLength)[0] = unchecked((byte)(this.maturityDay));
        cursor += MaturityDayLength;
        data.Slice(cursor, MaturityWeekLength)[0] = unchecked((byte)(this.maturityWeek));
        cursor += MaturityWeekLength;
        data.Slice(cursor, UserDefinedInstrumentLength)[0] = unchecked((byte)(this.userDefinedInstrument));
        cursor += UserDefinedInstrumentLength;
        data.Slice(cursor, ContractMultiplierUnitLength)[0] = unchecked((byte)(this.contractMultiplierUnit));
        cursor += ContractMultiplierUnitLength;
        data.Slice(cursor, FlowScheduleTypeLength)[0] = unchecked((byte)(this.flowScheduleType));
        cursor += FlowScheduleTypeLength;
        data.Slice(cursor, TickRuleLength)[0] = unchecked((byte)(this.tickRule));
        cursor += TickRuleLength;
        {
            if (this.raw.Length >= Dummy3Offset + Dummy3Length)
            {
                this.raw.AsSpan(Dummy3Offset, Dummy3Length).CopyTo(data.Slice(cursor, Dummy3Length));
            }
            else
            {
                data.Slice(cursor, Dummy3Length).Clear();
            }
            cursor += Dummy3Length;
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
        if (!global::System.Text.Ascii.IsValid(this.currency))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Currency), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.settlCurrency))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SettlCurrency), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.secsubtype))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Secsubtype), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.rawSymbol))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.RawSymbol), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.group))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Group), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.exchange))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Exchange), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.asset))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Asset), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.cfi))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Cfi), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.securityType))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SecurityType), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.unitOfMeasure))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.UnitOfMeasure), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.underlying))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Underlying), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.strikePriceCurrency))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StrikePriceCurrency), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Enum.IsDefined(this.instrumentClass))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.InstrumentClass), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.matchAlgorithm))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.MatchAlgorithm), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.securityUpdateAction))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SecurityUpdateAction), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.userDefinedInstrument))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.UserDefinedInstrument), ValidationStatus.UndeclaredValue));
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(InstrumentDefMsg? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.tsRecv != other.tsRecv) return false;
        if (this.minPriceIncrement != other.minPriceIncrement) return false;
        if (this.displayFactor != other.displayFactor) return false;
        if (this.expiration != other.expiration) return false;
        if (this.activation != other.activation) return false;
        if (this.highLimitPrice != other.highLimitPrice) return false;
        if (this.lowLimitPrice != other.lowLimitPrice) return false;
        if (this.maxPriceVariation != other.maxPriceVariation) return false;
        if (this.tradingReferencePrice != other.tradingReferencePrice) return false;
        if (this.unitOfMeasureQty != other.unitOfMeasureQty) return false;
        if (this.minPriceIncrementAmount != other.minPriceIncrementAmount) return false;
        if (this.priceRatio != other.priceRatio) return false;
        if (this.instAttribValue != other.instAttribValue) return false;
        if (this.underlyingId != other.underlyingId) return false;
        if (this.rawInstrumentId != other.rawInstrumentId) return false;
        if (this.marketDepthImplied != other.marketDepthImplied) return false;
        if (this.marketDepth != other.marketDepth) return false;
        if (this.marketSegmentId != other.marketSegmentId) return false;
        if (this.maxTradeVol != other.maxTradeVol) return false;
        if (this.minLotSize != other.minLotSize) return false;
        if (this.minLotSizeBlock != other.minLotSizeBlock) return false;
        if (this.minLotSizeRoundLot != other.minLotSizeRoundLot) return false;
        if (this.minTradeVol != other.minTradeVol) return false;
        if (this.contractMultiplier != other.contractMultiplier) return false;
        if (this.decayQuantity != other.decayQuantity) return false;
        if (this.originalContractSize != other.originalContractSize) return false;
        if (this.tradingReferenceDate != other.tradingReferenceDate) return false;
        if (this.applId != other.applId) return false;
        if (this.maturityYear != other.maturityYear) return false;
        if (this.decayStartDate != other.decayStartDate) return false;
        if (this.channel != other.channel) return false;
        if (this.currency != other.currency) return false;
        if (this.settlCurrency != other.settlCurrency) return false;
        if (this.secsubtype != other.secsubtype) return false;
        if (this.rawSymbol != other.rawSymbol) return false;
        if (this.group != other.group) return false;
        if (this.exchange != other.exchange) return false;
        if (this.asset != other.asset) return false;
        if (this.cfi != other.cfi) return false;
        if (this.securityType != other.securityType) return false;
        if (this.unitOfMeasure != other.unitOfMeasure) return false;
        if (this.underlying != other.underlying) return false;
        if (this.strikePriceCurrency != other.strikePriceCurrency) return false;
        if (this.instrumentClass != other.instrumentClass) return false;
        if (this.strikePrice != other.strikePrice) return false;
        if (this.matchAlgorithm != other.matchAlgorithm) return false;
        if (this.mdSecurityTradingStatus != other.mdSecurityTradingStatus) return false;
        if (this.mainFraction != other.mainFraction) return false;
        if (this.priceDisplayFormat != other.priceDisplayFormat) return false;
        if (this.settlPriceType != other.settlPriceType) return false;
        if (this.subFraction != other.subFraction) return false;
        if (this.underlyingProduct != other.underlyingProduct) return false;
        if (this.securityUpdateAction != other.securityUpdateAction) return false;
        if (this.maturityMonth != other.maturityMonth) return false;
        if (this.maturityDay != other.maturityDay) return false;
        if (this.maturityWeek != other.maturityWeek) return false;
        if (this.userDefinedInstrument != other.userDefinedInstrument) return false;
        if (this.contractMultiplierUnit != other.contractMultiplierUnit) return false;
        if (this.flowScheduleType != other.flowScheduleType) return false;
        if (this.tickRule != other.tickRule) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is InstrumentDefMsg other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.tsRecv);
        hash.Add(this.minPriceIncrement);
        hash.Add(this.displayFactor);
        hash.Add(this.expiration);
        hash.Add(this.activation);
        hash.Add(this.highLimitPrice);
        hash.Add(this.lowLimitPrice);
        hash.Add(this.maxPriceVariation);
        hash.Add(this.tradingReferencePrice);
        hash.Add(this.unitOfMeasureQty);
        hash.Add(this.minPriceIncrementAmount);
        hash.Add(this.priceRatio);
        hash.Add(this.instAttribValue);
        hash.Add(this.underlyingId);
        hash.Add(this.rawInstrumentId);
        hash.Add(this.marketDepthImplied);
        hash.Add(this.marketDepth);
        hash.Add(this.marketSegmentId);
        hash.Add(this.maxTradeVol);
        hash.Add(this.minLotSize);
        hash.Add(this.minLotSizeBlock);
        hash.Add(this.minLotSizeRoundLot);
        hash.Add(this.minTradeVol);
        hash.Add(this.contractMultiplier);
        hash.Add(this.decayQuantity);
        hash.Add(this.originalContractSize);
        hash.Add(this.tradingReferenceDate);
        hash.Add(this.applId);
        hash.Add(this.maturityYear);
        hash.Add(this.decayStartDate);
        hash.Add(this.channel);
        hash.Add(this.currency);
        hash.Add(this.settlCurrency);
        hash.Add(this.secsubtype);
        hash.Add(this.rawSymbol);
        hash.Add(this.group);
        hash.Add(this.exchange);
        hash.Add(this.asset);
        hash.Add(this.cfi);
        hash.Add(this.securityType);
        hash.Add(this.unitOfMeasure);
        hash.Add(this.underlying);
        hash.Add(this.strikePriceCurrency);
        hash.Add(this.instrumentClass);
        hash.Add(this.strikePrice);
        hash.Add(this.matchAlgorithm);
        hash.Add(this.mdSecurityTradingStatus);
        hash.Add(this.mainFraction);
        hash.Add(this.priceDisplayFormat);
        hash.Add(this.settlPriceType);
        hash.Add(this.subFraction);
        hash.Add(this.underlyingProduct);
        hash.Add(this.securityUpdateAction);
        hash.Add(this.maturityMonth);
        hash.Add(this.maturityDay);
        hash.Add(this.maturityWeek);
        hash.Add(this.userDefinedInstrument);
        hash.Add(this.contractMultiplierUnit);
        hash.Add(this.flowScheduleType);
        hash.Add(this.tickRule);
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
        private const int WordCount = 2;
        private const int FieldCount = 66;

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
            if (Has(FieldName.MinPriceIncrement)) names.Add(nameof(FieldName.MinPriceIncrement));
            if (Has(FieldName.DisplayFactor)) names.Add(nameof(FieldName.DisplayFactor));
            if (Has(FieldName.Expiration)) names.Add(nameof(FieldName.Expiration));
            if (Has(FieldName.Activation)) names.Add(nameof(FieldName.Activation));
            if (Has(FieldName.HighLimitPrice)) names.Add(nameof(FieldName.HighLimitPrice));
            if (Has(FieldName.LowLimitPrice)) names.Add(nameof(FieldName.LowLimitPrice));
            if (Has(FieldName.MaxPriceVariation)) names.Add(nameof(FieldName.MaxPriceVariation));
            if (Has(FieldName.TradingReferencePrice)) names.Add(nameof(FieldName.TradingReferencePrice));
            if (Has(FieldName.UnitOfMeasureQty)) names.Add(nameof(FieldName.UnitOfMeasureQty));
            if (Has(FieldName.MinPriceIncrementAmount)) names.Add(nameof(FieldName.MinPriceIncrementAmount));
            if (Has(FieldName.PriceRatio)) names.Add(nameof(FieldName.PriceRatio));
            if (Has(FieldName.InstAttribValue)) names.Add(nameof(FieldName.InstAttribValue));
            if (Has(FieldName.UnderlyingId)) names.Add(nameof(FieldName.UnderlyingId));
            if (Has(FieldName.RawInstrumentId)) names.Add(nameof(FieldName.RawInstrumentId));
            if (Has(FieldName.MarketDepthImplied)) names.Add(nameof(FieldName.MarketDepthImplied));
            if (Has(FieldName.MarketDepth)) names.Add(nameof(FieldName.MarketDepth));
            if (Has(FieldName.MarketSegmentId)) names.Add(nameof(FieldName.MarketSegmentId));
            if (Has(FieldName.MaxTradeVol)) names.Add(nameof(FieldName.MaxTradeVol));
            if (Has(FieldName.MinLotSize)) names.Add(nameof(FieldName.MinLotSize));
            if (Has(FieldName.MinLotSizeBlock)) names.Add(nameof(FieldName.MinLotSizeBlock));
            if (Has(FieldName.MinLotSizeRoundLot)) names.Add(nameof(FieldName.MinLotSizeRoundLot));
            if (Has(FieldName.MinTradeVol)) names.Add(nameof(FieldName.MinTradeVol));
            if (Has(FieldName.Reserved4)) names.Add(nameof(FieldName.Reserved4));
            if (Has(FieldName.ContractMultiplier)) names.Add(nameof(FieldName.ContractMultiplier));
            if (Has(FieldName.DecayQuantity)) names.Add(nameof(FieldName.DecayQuantity));
            if (Has(FieldName.OriginalContractSize)) names.Add(nameof(FieldName.OriginalContractSize));
            if (Has(FieldName.SecondReserved4)) names.Add(nameof(FieldName.SecondReserved4));
            if (Has(FieldName.TradingReferenceDate)) names.Add(nameof(FieldName.TradingReferenceDate));
            if (Has(FieldName.ApplId)) names.Add(nameof(FieldName.ApplId));
            if (Has(FieldName.MaturityYear)) names.Add(nameof(FieldName.MaturityYear));
            if (Has(FieldName.DecayStartDate)) names.Add(nameof(FieldName.DecayStartDate));
            if (Has(FieldName.Channel)) names.Add(nameof(FieldName.Channel));
            if (Has(FieldName.Currency)) names.Add(nameof(FieldName.Currency));
            if (Has(FieldName.SettlCurrency)) names.Add(nameof(FieldName.SettlCurrency));
            if (Has(FieldName.Secsubtype)) names.Add(nameof(FieldName.Secsubtype));
            if (Has(FieldName.RawSymbol)) names.Add(nameof(FieldName.RawSymbol));
            if (Has(FieldName.Group)) names.Add(nameof(FieldName.Group));
            if (Has(FieldName.Exchange)) names.Add(nameof(FieldName.Exchange));
            if (Has(FieldName.Asset)) names.Add(nameof(FieldName.Asset));
            if (Has(FieldName.Cfi)) names.Add(nameof(FieldName.Cfi));
            if (Has(FieldName.SecurityType)) names.Add(nameof(FieldName.SecurityType));
            if (Has(FieldName.UnitOfMeasure)) names.Add(nameof(FieldName.UnitOfMeasure));
            if (Has(FieldName.Underlying)) names.Add(nameof(FieldName.Underlying));
            if (Has(FieldName.StrikePriceCurrency)) names.Add(nameof(FieldName.StrikePriceCurrency));
            if (Has(FieldName.InstrumentClass)) names.Add(nameof(FieldName.InstrumentClass));
            if (Has(FieldName.Reserved2)) names.Add(nameof(FieldName.Reserved2));
            if (Has(FieldName.StrikePrice)) names.Add(nameof(FieldName.StrikePrice));
            if (Has(FieldName.Reserved6)) names.Add(nameof(FieldName.Reserved6));
            if (Has(FieldName.MatchAlgorithm)) names.Add(nameof(FieldName.MatchAlgorithm));
            if (Has(FieldName.MdSecurityTradingStatus)) names.Add(nameof(FieldName.MdSecurityTradingStatus));
            if (Has(FieldName.MainFraction)) names.Add(nameof(FieldName.MainFraction));
            if (Has(FieldName.PriceDisplayFormat)) names.Add(nameof(FieldName.PriceDisplayFormat));
            if (Has(FieldName.SettlPriceType)) names.Add(nameof(FieldName.SettlPriceType));
            if (Has(FieldName.SubFraction)) names.Add(nameof(FieldName.SubFraction));
            if (Has(FieldName.UnderlyingProduct)) names.Add(nameof(FieldName.UnderlyingProduct));
            if (Has(FieldName.SecurityUpdateAction)) names.Add(nameof(FieldName.SecurityUpdateAction));
            if (Has(FieldName.MaturityMonth)) names.Add(nameof(FieldName.MaturityMonth));
            if (Has(FieldName.MaturityDay)) names.Add(nameof(FieldName.MaturityDay));
            if (Has(FieldName.MaturityWeek)) names.Add(nameof(FieldName.MaturityWeek));
            if (Has(FieldName.UserDefinedInstrument)) names.Add(nameof(FieldName.UserDefinedInstrument));
            if (Has(FieldName.ContractMultiplierUnit)) names.Add(nameof(FieldName.ContractMultiplierUnit));
            if (Has(FieldName.FlowScheduleType)) names.Add(nameof(FieldName.FlowScheduleType));
            if (Has(FieldName.TickRule)) names.Add(nameof(FieldName.TickRule));
            if (Has(FieldName.Dummy3)) names.Add(nameof(FieldName.Dummy3));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(InstrumentDefMsg other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.tsRecv != other.tsRecv) diff = diff.With(FieldName.TsRecv);
        if (this.minPriceIncrement != other.minPriceIncrement) diff = diff.With(FieldName.MinPriceIncrement);
        if (this.displayFactor != other.displayFactor) diff = diff.With(FieldName.DisplayFactor);
        if (this.expiration != other.expiration) diff = diff.With(FieldName.Expiration);
        if (this.activation != other.activation) diff = diff.With(FieldName.Activation);
        if (this.highLimitPrice != other.highLimitPrice) diff = diff.With(FieldName.HighLimitPrice);
        if (this.lowLimitPrice != other.lowLimitPrice) diff = diff.With(FieldName.LowLimitPrice);
        if (this.maxPriceVariation != other.maxPriceVariation) diff = diff.With(FieldName.MaxPriceVariation);
        if (this.tradingReferencePrice != other.tradingReferencePrice) diff = diff.With(FieldName.TradingReferencePrice);
        if (this.unitOfMeasureQty != other.unitOfMeasureQty) diff = diff.With(FieldName.UnitOfMeasureQty);
        if (this.minPriceIncrementAmount != other.minPriceIncrementAmount) diff = diff.With(FieldName.MinPriceIncrementAmount);
        if (this.priceRatio != other.priceRatio) diff = diff.With(FieldName.PriceRatio);
        if (this.instAttribValue != other.instAttribValue) diff = diff.With(FieldName.InstAttribValue);
        if (this.underlyingId != other.underlyingId) diff = diff.With(FieldName.UnderlyingId);
        if (this.rawInstrumentId != other.rawInstrumentId) diff = diff.With(FieldName.RawInstrumentId);
        if (this.marketDepthImplied != other.marketDepthImplied) diff = diff.With(FieldName.MarketDepthImplied);
        if (this.marketDepth != other.marketDepth) diff = diff.With(FieldName.MarketDepth);
        if (this.marketSegmentId != other.marketSegmentId) diff = diff.With(FieldName.MarketSegmentId);
        if (this.maxTradeVol != other.maxTradeVol) diff = diff.With(FieldName.MaxTradeVol);
        if (this.minLotSize != other.minLotSize) diff = diff.With(FieldName.MinLotSize);
        if (this.minLotSizeBlock != other.minLotSizeBlock) diff = diff.With(FieldName.MinLotSizeBlock);
        if (this.minLotSizeRoundLot != other.minLotSizeRoundLot) diff = diff.With(FieldName.MinLotSizeRoundLot);
        if (this.minTradeVol != other.minTradeVol) diff = diff.With(FieldName.MinTradeVol);
        if (this.contractMultiplier != other.contractMultiplier) diff = diff.With(FieldName.ContractMultiplier);
        if (this.decayQuantity != other.decayQuantity) diff = diff.With(FieldName.DecayQuantity);
        if (this.originalContractSize != other.originalContractSize) diff = diff.With(FieldName.OriginalContractSize);
        if (this.tradingReferenceDate != other.tradingReferenceDate) diff = diff.With(FieldName.TradingReferenceDate);
        if (this.applId != other.applId) diff = diff.With(FieldName.ApplId);
        if (this.maturityYear != other.maturityYear) diff = diff.With(FieldName.MaturityYear);
        if (this.decayStartDate != other.decayStartDate) diff = diff.With(FieldName.DecayStartDate);
        if (this.channel != other.channel) diff = diff.With(FieldName.Channel);
        if (this.currency != other.currency) diff = diff.With(FieldName.Currency);
        if (this.settlCurrency != other.settlCurrency) diff = diff.With(FieldName.SettlCurrency);
        if (this.secsubtype != other.secsubtype) diff = diff.With(FieldName.Secsubtype);
        if (this.rawSymbol != other.rawSymbol) diff = diff.With(FieldName.RawSymbol);
        if (this.group != other.group) diff = diff.With(FieldName.Group);
        if (this.exchange != other.exchange) diff = diff.With(FieldName.Exchange);
        if (this.asset != other.asset) diff = diff.With(FieldName.Asset);
        if (this.cfi != other.cfi) diff = diff.With(FieldName.Cfi);
        if (this.securityType != other.securityType) diff = diff.With(FieldName.SecurityType);
        if (this.unitOfMeasure != other.unitOfMeasure) diff = diff.With(FieldName.UnitOfMeasure);
        if (this.underlying != other.underlying) diff = diff.With(FieldName.Underlying);
        if (this.strikePriceCurrency != other.strikePriceCurrency) diff = diff.With(FieldName.StrikePriceCurrency);
        if (this.instrumentClass != other.instrumentClass) diff = diff.With(FieldName.InstrumentClass);
        if (this.strikePrice != other.strikePrice) diff = diff.With(FieldName.StrikePrice);
        if (this.matchAlgorithm != other.matchAlgorithm) diff = diff.With(FieldName.MatchAlgorithm);
        if (this.mdSecurityTradingStatus != other.mdSecurityTradingStatus) diff = diff.With(FieldName.MdSecurityTradingStatus);
        if (this.mainFraction != other.mainFraction) diff = diff.With(FieldName.MainFraction);
        if (this.priceDisplayFormat != other.priceDisplayFormat) diff = diff.With(FieldName.PriceDisplayFormat);
        if (this.settlPriceType != other.settlPriceType) diff = diff.With(FieldName.SettlPriceType);
        if (this.subFraction != other.subFraction) diff = diff.With(FieldName.SubFraction);
        if (this.underlyingProduct != other.underlyingProduct) diff = diff.With(FieldName.UnderlyingProduct);
        if (this.securityUpdateAction != other.securityUpdateAction) diff = diff.With(FieldName.SecurityUpdateAction);
        if (this.maturityMonth != other.maturityMonth) diff = diff.With(FieldName.MaturityMonth);
        if (this.maturityDay != other.maturityDay) diff = diff.With(FieldName.MaturityDay);
        if (this.maturityWeek != other.maturityWeek) diff = diff.With(FieldName.MaturityWeek);
        if (this.userDefinedInstrument != other.userDefinedInstrument) diff = diff.With(FieldName.UserDefinedInstrument);
        if (this.contractMultiplierUnit != other.contractMultiplierUnit) diff = diff.With(FieldName.ContractMultiplierUnit);
        if (this.flowScheduleType != other.flowScheduleType) diff = diff.With(FieldName.FlowScheduleType);
        if (this.tickRule != other.tickRule) diff = diff.With(FieldName.TickRule);
        return diff;
    }

    private void CopyField(FieldName field, InstrumentDefMsg from)
    {
        switch (field)
        {
            case FieldName.TsRecv:
                this.tsRecv = from.tsRecv;
                break;
            case FieldName.MinPriceIncrement:
                this.minPriceIncrement = from.minPriceIncrement;
                break;
            case FieldName.DisplayFactor:
                this.displayFactor = from.displayFactor;
                break;
            case FieldName.Expiration:
                this.expiration = from.expiration;
                break;
            case FieldName.Activation:
                this.activation = from.activation;
                break;
            case FieldName.HighLimitPrice:
                this.highLimitPrice = from.highLimitPrice;
                break;
            case FieldName.LowLimitPrice:
                this.lowLimitPrice = from.lowLimitPrice;
                break;
            case FieldName.MaxPriceVariation:
                this.maxPriceVariation = from.maxPriceVariation;
                break;
            case FieldName.TradingReferencePrice:
                this.tradingReferencePrice = from.tradingReferencePrice;
                break;
            case FieldName.UnitOfMeasureQty:
                this.unitOfMeasureQty = from.unitOfMeasureQty;
                break;
            case FieldName.MinPriceIncrementAmount:
                this.minPriceIncrementAmount = from.minPriceIncrementAmount;
                break;
            case FieldName.PriceRatio:
                this.priceRatio = from.priceRatio;
                break;
            case FieldName.InstAttribValue:
                this.instAttribValue = from.instAttribValue;
                break;
            case FieldName.UnderlyingId:
                this.underlyingId = from.underlyingId;
                break;
            case FieldName.RawInstrumentId:
                this.rawInstrumentId = from.rawInstrumentId;
                break;
            case FieldName.MarketDepthImplied:
                this.marketDepthImplied = from.marketDepthImplied;
                break;
            case FieldName.MarketDepth:
                this.marketDepth = from.marketDepth;
                break;
            case FieldName.MarketSegmentId:
                this.marketSegmentId = from.marketSegmentId;
                break;
            case FieldName.MaxTradeVol:
                this.maxTradeVol = from.maxTradeVol;
                break;
            case FieldName.MinLotSize:
                this.minLotSize = from.minLotSize;
                break;
            case FieldName.MinLotSizeBlock:
                this.minLotSizeBlock = from.minLotSizeBlock;
                break;
            case FieldName.MinLotSizeRoundLot:
                this.minLotSizeRoundLot = from.minLotSizeRoundLot;
                break;
            case FieldName.MinTradeVol:
                this.minTradeVol = from.minTradeVol;
                break;
            case FieldName.ContractMultiplier:
                this.contractMultiplier = from.contractMultiplier;
                break;
            case FieldName.DecayQuantity:
                this.decayQuantity = from.decayQuantity;
                break;
            case FieldName.OriginalContractSize:
                this.originalContractSize = from.originalContractSize;
                break;
            case FieldName.TradingReferenceDate:
                this.tradingReferenceDate = from.tradingReferenceDate;
                break;
            case FieldName.ApplId:
                this.applId = from.applId;
                break;
            case FieldName.MaturityYear:
                this.maturityYear = from.maturityYear;
                break;
            case FieldName.DecayStartDate:
                this.decayStartDate = from.decayStartDate;
                break;
            case FieldName.Channel:
                this.channel = from.channel;
                break;
            case FieldName.Currency:
                this.currency = from.currency;
                break;
            case FieldName.SettlCurrency:
                this.settlCurrency = from.settlCurrency;
                break;
            case FieldName.Secsubtype:
                this.secsubtype = from.secsubtype;
                break;
            case FieldName.RawSymbol:
                this.rawSymbol = from.rawSymbol;
                break;
            case FieldName.Group:
                this.group = from.group;
                break;
            case FieldName.Exchange:
                this.exchange = from.exchange;
                break;
            case FieldName.Asset:
                this.asset = from.asset;
                break;
            case FieldName.Cfi:
                this.cfi = from.cfi;
                break;
            case FieldName.SecurityType:
                this.securityType = from.securityType;
                break;
            case FieldName.UnitOfMeasure:
                this.unitOfMeasure = from.unitOfMeasure;
                break;
            case FieldName.Underlying:
                this.underlying = from.underlying;
                break;
            case FieldName.StrikePriceCurrency:
                this.strikePriceCurrency = from.strikePriceCurrency;
                break;
            case FieldName.InstrumentClass:
                this.instrumentClass = from.instrumentClass;
                break;
            case FieldName.StrikePrice:
                this.strikePrice = from.strikePrice;
                break;
            case FieldName.MatchAlgorithm:
                this.matchAlgorithm = from.matchAlgorithm;
                break;
            case FieldName.MdSecurityTradingStatus:
                this.mdSecurityTradingStatus = from.mdSecurityTradingStatus;
                break;
            case FieldName.MainFraction:
                this.mainFraction = from.mainFraction;
                break;
            case FieldName.PriceDisplayFormat:
                this.priceDisplayFormat = from.priceDisplayFormat;
                break;
            case FieldName.SettlPriceType:
                this.settlPriceType = from.settlPriceType;
                break;
            case FieldName.SubFraction:
                this.subFraction = from.subFraction;
                break;
            case FieldName.UnderlyingProduct:
                this.underlyingProduct = from.underlyingProduct;
                break;
            case FieldName.SecurityUpdateAction:
                this.securityUpdateAction = from.securityUpdateAction;
                break;
            case FieldName.MaturityMonth:
                this.maturityMonth = from.maturityMonth;
                break;
            case FieldName.MaturityDay:
                this.maturityDay = from.maturityDay;
                break;
            case FieldName.MaturityWeek:
                this.maturityWeek = from.maturityWeek;
                break;
            case FieldName.UserDefinedInstrument:
                this.userDefinedInstrument = from.userDefinedInstrument;
                break;
            case FieldName.ContractMultiplierUnit:
                this.contractMultiplierUnit = from.contractMultiplierUnit;
                break;
            case FieldName.FlowScheduleType:
                this.flowScheduleType = from.flowScheduleType;
                break;
            case FieldName.TickRule:
                this.tickRule = from.tickRule;
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"InstrumentDefMsg {{ TsRecv: {(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}, MinPriceIncrement: {(Text.Of(this.minPriceIncrement == MinPriceIncrementNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrement, MinPriceIncrementScale)))}, DisplayFactor: {(Text.Of(this.displayFactor == DisplayFactorNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.displayFactor, DisplayFactorScale)))}, Expiration: {(Text.Instant(this.expiration, ExpirationNanosecondsPerUnit, ExpirationNullValue))}, Activation: {(Text.Instant(this.activation, ActivationNanosecondsPerUnit, ActivationNullValue))}, HighLimitPrice: {(Text.Of(this.highLimitPrice == HighLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.highLimitPrice, HighLimitPriceScale)))}, LowLimitPrice: {(Text.Of(this.lowLimitPrice == LowLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowLimitPrice, LowLimitPriceScale)))}, MaxPriceVariation: {(Text.Of(this.maxPriceVariation == MaxPriceVariationNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.maxPriceVariation, MaxPriceVariationScale)))}, TradingReferencePrice: {(Text.Of(this.tradingReferencePrice == TradingReferencePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.tradingReferencePrice, TradingReferencePriceScale)))}, UnitOfMeasureQty: {(Text.Of(this.unitOfMeasureQty == UnitOfMeasureQtyNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.unitOfMeasureQty, UnitOfMeasureQtyScale)))}, MinPriceIncrementAmount: {(Text.Of(this.minPriceIncrementAmount == MinPriceIncrementAmountNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrementAmount, MinPriceIncrementAmountScale)))}, PriceRatio: {(Text.Of(this.priceRatio == PriceRatioNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.priceRatio, PriceRatioScale)))}, InstAttribValue: {(Text.Of(this.instAttribValue))}, UnderlyingId: {(Text.Of(this.underlyingId))}, RawInstrumentId: {(Text.Of(this.rawInstrumentId))}, MarketDepthImplied: {(Text.Of(this.marketDepthImplied))}, MarketDepth: {(Text.Of(this.marketDepth))}, MarketSegmentId: {(Text.Of(this.marketSegmentId))}, MaxTradeVol: {(Text.Of(this.maxTradeVol))}, MinLotSize: {(Text.Of(this.minLotSize))}, MinLotSizeBlock: {(Text.Of(this.minLotSizeBlock))}, MinLotSizeRoundLot: {(Text.Of(this.minLotSizeRoundLot))}, MinTradeVol: {(Text.Of(this.minTradeVol))}, ContractMultiplier: {(Text.Of(this.contractMultiplier))}, DecayQuantity: {(Text.Of(this.decayQuantity))}, OriginalContractSize: {(Text.Of(this.originalContractSize))}, TradingReferenceDate: {(Text.Of(this.tradingReferenceDate))}, ApplId: {(Text.Of(this.applId))}, MaturityYear: {(Text.Of(this.maturityYear))}, DecayStartDate: {(Text.Of(this.decayStartDate))}, Channel: {(Text.Of(this.channel))}, Currency: {(Text.Of(this.currency))}, SettlCurrency: {(Text.Of(this.settlCurrency))}, Secsubtype: {(Text.Of(this.secsubtype))}, RawSymbol: {(Text.Of(this.rawSymbol))}, Group: {(Text.Of(this.group))}, Exchange: {(Text.Of(this.exchange))}, Asset: {(Text.Of(this.asset))}, Cfi: {(Text.Of(this.cfi))}, SecurityType: {(Text.Of(this.securityType))}, UnitOfMeasure: {(Text.Of(this.unitOfMeasure))}, Underlying: {(Text.Of(this.underlying))}, StrikePriceCurrency: {(Text.Of(this.strikePriceCurrency))}, InstrumentClass: {(Text.Enum(this.instrumentClass))}, StrikePrice: {(Text.Of(this.strikePrice == StrikePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.strikePrice, StrikePriceScale)))}, MatchAlgorithm: {(Text.Enum(this.matchAlgorithm))}, MdSecurityTradingStatus: {(Text.Of(this.mdSecurityTradingStatus))}, MainFraction: {(Text.Of(this.mainFraction))}, PriceDisplayFormat: {(Text.Of(this.priceDisplayFormat))}, SettlPriceType: {(Text.Of(this.settlPriceType))}, SubFraction: {(Text.Of(this.subFraction))}, UnderlyingProduct: {(Text.Of(this.underlyingProduct))}, SecurityUpdateAction: {(Text.Enum(this.securityUpdateAction))}, MaturityMonth: {(Text.Of(this.maturityMonth))}, MaturityDay: {(Text.Of(this.maturityDay))}, MaturityWeek: {(Text.Of(this.maturityWeek))}, UserDefinedInstrument: {(Text.Enum(this.userDefinedInstrument))}, ContractMultiplierUnit: {(Text.Of(this.contractMultiplierUnit))}, FlowScheduleType: {(Text.Of(this.flowScheduleType))}, TickRule: {(Text.Of(this.tickRule))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{DataValue}] [{(Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue))}] [{(Text.Of(this.minPriceIncrement == MinPriceIncrementNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrement, MinPriceIncrementScale)))}] [{(Text.Of(this.displayFactor == DisplayFactorNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.displayFactor, DisplayFactorScale)))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => Text.Enum(RecordBodyCode.InstrumentDefMsg),
        FieldName.TsRecv => Text.Instant(this.tsRecv, TsRecvNanosecondsPerUnit, TsRecvNullValue),
        FieldName.MinPriceIncrement => Text.Of(this.minPriceIncrement == MinPriceIncrementNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrement, MinPriceIncrementScale)),
        FieldName.DisplayFactor => Text.Of(this.displayFactor == DisplayFactorNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.displayFactor, DisplayFactorScale)),
        FieldName.Expiration => Text.Instant(this.expiration, ExpirationNanosecondsPerUnit, ExpirationNullValue),
        FieldName.Activation => Text.Instant(this.activation, ActivationNanosecondsPerUnit, ActivationNullValue),
        FieldName.HighLimitPrice => Text.Of(this.highLimitPrice == HighLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.highLimitPrice, HighLimitPriceScale)),
        FieldName.LowLimitPrice => Text.Of(this.lowLimitPrice == LowLimitPriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.lowLimitPrice, LowLimitPriceScale)),
        FieldName.MaxPriceVariation => Text.Of(this.maxPriceVariation == MaxPriceVariationNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.maxPriceVariation, MaxPriceVariationScale)),
        FieldName.TradingReferencePrice => Text.Of(this.tradingReferencePrice == TradingReferencePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.tradingReferencePrice, TradingReferencePriceScale)),
        FieldName.UnitOfMeasureQty => Text.Of(this.unitOfMeasureQty == UnitOfMeasureQtyNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.unitOfMeasureQty, UnitOfMeasureQtyScale)),
        FieldName.MinPriceIncrementAmount => Text.Of(this.minPriceIncrementAmount == MinPriceIncrementAmountNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.minPriceIncrementAmount, MinPriceIncrementAmountScale)),
        FieldName.PriceRatio => Text.Of(this.priceRatio == PriceRatioNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.priceRatio, PriceRatioScale)),
        FieldName.InstAttribValue => Text.Of(this.instAttribValue),
        FieldName.UnderlyingId => Text.Of(this.underlyingId),
        FieldName.RawInstrumentId => Text.Of(this.rawInstrumentId),
        FieldName.MarketDepthImplied => Text.Of(this.marketDepthImplied),
        FieldName.MarketDepth => Text.Of(this.marketDepth),
        FieldName.MarketSegmentId => Text.Of(this.marketSegmentId),
        FieldName.MaxTradeVol => Text.Of(this.maxTradeVol),
        FieldName.MinLotSize => Text.Of(this.minLotSize),
        FieldName.MinLotSizeBlock => Text.Of(this.minLotSizeBlock),
        FieldName.MinLotSizeRoundLot => Text.Of(this.minLotSizeRoundLot),
        FieldName.MinTradeVol => Text.Of(this.minTradeVol),
        FieldName.ContractMultiplier => Text.Of(this.contractMultiplier),
        FieldName.DecayQuantity => Text.Of(this.decayQuantity),
        FieldName.OriginalContractSize => Text.Of(this.originalContractSize),
        FieldName.TradingReferenceDate => Text.Of(this.tradingReferenceDate),
        FieldName.ApplId => Text.Of(this.applId),
        FieldName.MaturityYear => Text.Of(this.maturityYear),
        FieldName.DecayStartDate => Text.Of(this.decayStartDate),
        FieldName.Channel => Text.Of(this.channel),
        FieldName.Currency => Text.Of(this.currency),
        FieldName.SettlCurrency => Text.Of(this.settlCurrency),
        FieldName.Secsubtype => Text.Of(this.secsubtype),
        FieldName.RawSymbol => Text.Of(this.rawSymbol),
        FieldName.Group => Text.Of(this.group),
        FieldName.Exchange => Text.Of(this.exchange),
        FieldName.Asset => Text.Of(this.asset),
        FieldName.Cfi => Text.Of(this.cfi),
        FieldName.SecurityType => Text.Of(this.securityType),
        FieldName.UnitOfMeasure => Text.Of(this.unitOfMeasure),
        FieldName.Underlying => Text.Of(this.underlying),
        FieldName.StrikePriceCurrency => Text.Of(this.strikePriceCurrency),
        FieldName.InstrumentClass => Text.Enum(this.instrumentClass),
        FieldName.StrikePrice => Text.Of(this.strikePrice == StrikePriceNullValue ? (decimal?)null : FixedPoint.ToDecimal(this.strikePrice, StrikePriceScale)),
        FieldName.MatchAlgorithm => Text.Enum(this.matchAlgorithm),
        FieldName.MdSecurityTradingStatus => Text.Of(this.mdSecurityTradingStatus),
        FieldName.MainFraction => Text.Of(this.mainFraction),
        FieldName.PriceDisplayFormat => Text.Of(this.priceDisplayFormat),
        FieldName.SettlPriceType => Text.Of(this.settlPriceType),
        FieldName.SubFraction => Text.Of(this.subFraction),
        FieldName.UnderlyingProduct => Text.Of(this.underlyingProduct),
        FieldName.SecurityUpdateAction => Text.Enum(this.securityUpdateAction),
        FieldName.MaturityMonth => Text.Of(this.maturityMonth),
        FieldName.MaturityDay => Text.Of(this.maturityDay),
        FieldName.MaturityWeek => Text.Of(this.maturityWeek),
        FieldName.UserDefinedInstrument => Text.Enum(this.userDefinedInstrument),
        FieldName.ContractMultiplierUnit => Text.Of(this.contractMultiplierUnit),
        FieldName.FlowScheduleType => Text.Of(this.flowScheduleType),
        FieldName.TickRule => Text.Of(this.tickRule),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.InstrumentClass => global::System.Enum.IsDefined(this.instrumentClass),
        FieldName.MatchAlgorithm => global::System.Enum.IsDefined(this.matchAlgorithm),
        FieldName.SecurityUpdateAction => global::System.Enum.IsDefined(this.securityUpdateAction),
        FieldName.UserDefinedInstrument => global::System.Enum.IsDefined(this.userDefinedInstrument),
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.TsRecv => this.tsRecv != TsRecvNullValue,
        FieldName.MinPriceIncrement => this.minPriceIncrement != MinPriceIncrementNullValue,
        FieldName.DisplayFactor => this.displayFactor != DisplayFactorNullValue,
        FieldName.Expiration => this.expiration != ExpirationNullValue,
        FieldName.Activation => this.activation != ActivationNullValue,
        FieldName.HighLimitPrice => this.highLimitPrice != HighLimitPriceNullValue,
        FieldName.LowLimitPrice => this.lowLimitPrice != LowLimitPriceNullValue,
        FieldName.MaxPriceVariation => this.maxPriceVariation != MaxPriceVariationNullValue,
        FieldName.TradingReferencePrice => this.tradingReferencePrice != TradingReferencePriceNullValue,
        FieldName.UnitOfMeasureQty => this.unitOfMeasureQty != UnitOfMeasureQtyNullValue,
        FieldName.MinPriceIncrementAmount => this.minPriceIncrementAmount != MinPriceIncrementAmountNullValue,
        FieldName.PriceRatio => this.priceRatio != PriceRatioNullValue,
        FieldName.StrikePrice => this.strikePrice != StrikePriceNullValue,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => false;

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => FieldView.NotInMessage,
        FieldName.TsRecv => TsRecvOffset,
        FieldName.MinPriceIncrement => MinPriceIncrementOffset,
        FieldName.DisplayFactor => DisplayFactorOffset,
        FieldName.Expiration => ExpirationOffset,
        FieldName.Activation => ActivationOffset,
        FieldName.HighLimitPrice => HighLimitPriceOffset,
        FieldName.LowLimitPrice => LowLimitPriceOffset,
        FieldName.MaxPriceVariation => MaxPriceVariationOffset,
        FieldName.TradingReferencePrice => TradingReferencePriceOffset,
        FieldName.UnitOfMeasureQty => UnitOfMeasureQtyOffset,
        FieldName.MinPriceIncrementAmount => MinPriceIncrementAmountOffset,
        FieldName.PriceRatio => PriceRatioOffset,
        FieldName.InstAttribValue => InstAttribValueOffset,
        FieldName.UnderlyingId => UnderlyingIdOffset,
        FieldName.RawInstrumentId => RawInstrumentIdOffset,
        FieldName.MarketDepthImplied => MarketDepthImpliedOffset,
        FieldName.MarketDepth => MarketDepthOffset,
        FieldName.MarketSegmentId => MarketSegmentIdOffset,
        FieldName.MaxTradeVol => MaxTradeVolOffset,
        FieldName.MinLotSize => MinLotSizeOffset,
        FieldName.MinLotSizeBlock => MinLotSizeBlockOffset,
        FieldName.MinLotSizeRoundLot => MinLotSizeRoundLotOffset,
        FieldName.MinTradeVol => MinTradeVolOffset,
        FieldName.Reserved4 => Reserved4Offset,
        FieldName.ContractMultiplier => ContractMultiplierOffset,
        FieldName.DecayQuantity => DecayQuantityOffset,
        FieldName.OriginalContractSize => OriginalContractSizeOffset,
        FieldName.SecondReserved4 => SecondReserved4Offset,
        FieldName.TradingReferenceDate => TradingReferenceDateOffset,
        FieldName.ApplId => ApplIdOffset,
        FieldName.MaturityYear => MaturityYearOffset,
        FieldName.DecayStartDate => DecayStartDateOffset,
        FieldName.Channel => ChannelOffset,
        FieldName.Currency => CurrencyOffset,
        FieldName.SettlCurrency => SettlCurrencyOffset,
        FieldName.Secsubtype => SecsubtypeOffset,
        FieldName.RawSymbol => RawSymbolOffset,
        FieldName.Group => GroupOffset,
        FieldName.Exchange => ExchangeOffset,
        FieldName.Asset => AssetOffset,
        FieldName.Cfi => CfiOffset,
        FieldName.SecurityType => SecurityTypeOffset,
        FieldName.UnitOfMeasure => UnitOfMeasureOffset,
        FieldName.Underlying => UnderlyingOffset,
        FieldName.StrikePriceCurrency => StrikePriceCurrencyOffset,
        FieldName.InstrumentClass => InstrumentClassOffset,
        FieldName.Reserved2 => Reserved2Offset,
        FieldName.StrikePrice => StrikePriceOffset,
        FieldName.Reserved6 => Reserved6Offset,
        FieldName.MatchAlgorithm => MatchAlgorithmOffset,
        FieldName.MdSecurityTradingStatus => MdSecurityTradingStatusOffset,
        FieldName.MainFraction => MainFractionOffset,
        FieldName.PriceDisplayFormat => PriceDisplayFormatOffset,
        FieldName.SettlPriceType => SettlPriceTypeOffset,
        FieldName.SubFraction => SubFractionOffset,
        FieldName.UnderlyingProduct => UnderlyingProductOffset,
        FieldName.SecurityUpdateAction => SecurityUpdateActionOffset,
        FieldName.MaturityMonth => MaturityMonthOffset,
        FieldName.MaturityDay => MaturityDayOffset,
        FieldName.MaturityWeek => MaturityWeekOffset,
        FieldName.UserDefinedInstrument => UserDefinedInstrumentOffset,
        FieldName.ContractMultiplierUnit => ContractMultiplierUnitOffset,
        FieldName.FlowScheduleType => FlowScheduleTypeOffset,
        FieldName.TickRule => TickRuleOffset,
        FieldName.Dummy3 => Dummy3Offset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.DataValue => DataValueLength,
        FieldName.TsRecv => TsRecvLength,
        FieldName.MinPriceIncrement => MinPriceIncrementLength,
        FieldName.DisplayFactor => DisplayFactorLength,
        FieldName.Expiration => ExpirationLength,
        FieldName.Activation => ActivationLength,
        FieldName.HighLimitPrice => HighLimitPriceLength,
        FieldName.LowLimitPrice => LowLimitPriceLength,
        FieldName.MaxPriceVariation => MaxPriceVariationLength,
        FieldName.TradingReferencePrice => TradingReferencePriceLength,
        FieldName.UnitOfMeasureQty => UnitOfMeasureQtyLength,
        FieldName.MinPriceIncrementAmount => MinPriceIncrementAmountLength,
        FieldName.PriceRatio => PriceRatioLength,
        FieldName.InstAttribValue => InstAttribValueLength,
        FieldName.UnderlyingId => UnderlyingIdLength,
        FieldName.RawInstrumentId => RawInstrumentIdLength,
        FieldName.MarketDepthImplied => MarketDepthImpliedLength,
        FieldName.MarketDepth => MarketDepthLength,
        FieldName.MarketSegmentId => MarketSegmentIdLength,
        FieldName.MaxTradeVol => MaxTradeVolLength,
        FieldName.MinLotSize => MinLotSizeLength,
        FieldName.MinLotSizeBlock => MinLotSizeBlockLength,
        FieldName.MinLotSizeRoundLot => MinLotSizeRoundLotLength,
        FieldName.MinTradeVol => MinTradeVolLength,
        FieldName.Reserved4 => Reserved4Length,
        FieldName.ContractMultiplier => ContractMultiplierLength,
        FieldName.DecayQuantity => DecayQuantityLength,
        FieldName.OriginalContractSize => OriginalContractSizeLength,
        FieldName.SecondReserved4 => SecondReserved4Length,
        FieldName.TradingReferenceDate => TradingReferenceDateLength,
        FieldName.ApplId => ApplIdLength,
        FieldName.MaturityYear => MaturityYearLength,
        FieldName.DecayStartDate => DecayStartDateLength,
        FieldName.Channel => ChannelLength,
        FieldName.Currency => CurrencyLength,
        FieldName.SettlCurrency => SettlCurrencyLength,
        FieldName.Secsubtype => SecsubtypeLength,
        FieldName.RawSymbol => RawSymbolLength,
        FieldName.Group => GroupLength,
        FieldName.Exchange => ExchangeLength,
        FieldName.Asset => AssetLength,
        FieldName.Cfi => CfiLength,
        FieldName.SecurityType => SecurityTypeLength,
        FieldName.UnitOfMeasure => UnitOfMeasureLength,
        FieldName.Underlying => UnderlyingLength,
        FieldName.StrikePriceCurrency => StrikePriceCurrencyLength,
        FieldName.InstrumentClass => InstrumentClassLength,
        FieldName.Reserved2 => Reserved2Length,
        FieldName.StrikePrice => StrikePriceLength,
        FieldName.Reserved6 => Reserved6Length,
        FieldName.MatchAlgorithm => MatchAlgorithmLength,
        FieldName.MdSecurityTradingStatus => MdSecurityTradingStatusLength,
        FieldName.MainFraction => MainFractionLength,
        FieldName.PriceDisplayFormat => PriceDisplayFormatLength,
        FieldName.SettlPriceType => SettlPriceTypeLength,
        FieldName.SubFraction => SubFractionLength,
        FieldName.UnderlyingProduct => UnderlyingProductLength,
        FieldName.SecurityUpdateAction => SecurityUpdateActionLength,
        FieldName.MaturityMonth => MaturityMonthLength,
        FieldName.MaturityDay => MaturityDayLength,
        FieldName.MaturityWeek => MaturityWeekLength,
        FieldName.UserDefinedInstrument => UserDefinedInstrumentLength,
        FieldName.ContractMultiplierUnit => ContractMultiplierUnitLength,
        FieldName.FlowScheduleType => FlowScheduleTypeLength,
        FieldName.TickRule => TickRuleLength,
        FieldName.Dummy3 => Dummy3Length,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => null;

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is InstrumentDefMsg typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as InstrumentDefMsg ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));

    ReadOnlyMemory<byte> IFieldOwner.CodeBytes(int field)
    {
        if (field == (int)FieldName.DataValue)
        {
            var bytes = new byte[DataValueLength];
            bytes[0] = unchecked((byte)(RecordBodyCode.InstrumentDefMsg));
            return bytes;
        }
        return ReadOnlyMemory<byte>.Empty;
    }
}
