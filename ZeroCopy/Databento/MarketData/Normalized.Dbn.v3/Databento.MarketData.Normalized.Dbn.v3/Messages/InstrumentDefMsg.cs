using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A definition of an instrument; the record of the definition schema
/// </summary>
public static class InstrumentDefMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Instrument Def Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinPriceIncrement + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDisplayFactor + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeExpiration + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeActivation + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeHighLimitPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLowLimitPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaxPriceVariation + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnitOfMeasureQty + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinPriceIncrementAmount + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePriceRatio + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStrikePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRawInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstAttribValue + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlyingId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketDepthImplied + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketDepth + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketSegmentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaxTradeVol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSizeBlock + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSizeRoundLot + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinTradeVol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeContractMultiplier + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDecayQuantity + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeOriginalContractSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioPriceNumerator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioPriceDenominator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioQtyNumerator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioQtyDenominator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegUnderlyingId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeApplId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityYear + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDecayStartDate + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeChannel + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegCount + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegIndex + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSettlCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecsubtype + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRawSymbol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeGroup + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeExchange + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeAsset + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeCfi + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecurityType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnitOfMeasure + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlying + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStrikePriceCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRawSymbol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstrumentClass + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMatchAlgorithm + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMainFraction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePriceDisplayFormat + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSubFraction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlyingProduct + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecurityUpdateAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityMonth + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityDay + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityWeek + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUserDefinedInstrument + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeContractMultiplierUnit + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeFlowScheduleType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTickRule + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegInstrumentClass + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegSide + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved17;

        /// <summary>
        ///  Gets the Instrument Def Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Instrument Def Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Instrument Def Msg at the start of the buffer, writes its fixed values, and gets the Instrument Def Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Instrument Def Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination);
        }

        /// <summary>
        ///  Gets the fixed fields over the start of the bytes without clearing or writing them, for changes in place.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the fixed fields.
        /// </exception>
        public static ref Layout Over(global::System.Span<byte> bytes)
        {
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv;
        /// <summary>
        ///  The min price increment, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrement MinPriceIncrement;
        /// <summary>
        ///  The display factor, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.DisplayFactor DisplayFactor;
        /// <summary>
        ///  The last eligible trade time expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Expiration Expiration;
        /// <summary>
        ///  The time of instrument activation expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Activation Activation;
        /// <summary>
        ///  The high limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.HighLimitPrice HighLimitPrice;
        /// <summary>
        ///  The low limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LowLimitPrice LowLimitPrice;
        /// <summary>
        ///  The max price variation, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaxPriceVariation MaxPriceVariation;
        /// <summary>
        ///  The unit of measure qty, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UnitOfMeasureQty UnitOfMeasureQty;
        /// <summary>
        ///  The min price increment amount, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrementAmount MinPriceIncrementAmount;
        /// <summary>
        ///  The price ratio, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.PriceRatio PriceRatio;
        /// <summary>
        ///  The strike price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StrikePrice StrikePrice;
        /// <summary>
        ///  The instrument ID assigned by the publisher; may be the same as instrument_id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RawInstrumentId RawInstrumentId;
        /// <summary>
        ///  The leg price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegPrice LegPrice;
        /// <summary>
        ///  The leg delta, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegDelta LegDelta;
        /// <summary>
        ///  A bitmap of instrument eligibility attributes; venue-specific
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.InstAttribValue InstAttribValue;
        /// <summary>
        ///  The underlying id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UnderlyingId UnderlyingId;
        /// <summary>
        ///  The market depth implied
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MarketDepthImplied MarketDepthImplied;
        /// <summary>
        ///  The market depth
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MarketDepth MarketDepth;
        /// <summary>
        ///  The market segment id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MarketSegmentId MarketSegmentId;
        /// <summary>
        ///  The max trade vol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaxTradeVol MaxTradeVol;
        /// <summary>
        ///  The min lot size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinLotSize MinLotSize;
        /// <summary>
        ///  The min lot size block
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinLotSizeBlock MinLotSizeBlock;
        /// <summary>
        ///  The min lot size round lot
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinLotSizeRoundLot MinLotSizeRoundLot;
        /// <summary>
        ///  The min trade vol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinTradeVol MinTradeVol;
        /// <summary>
        ///  The contract multiplier
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.ContractMultiplier ContractMultiplier;
        /// <summary>
        ///  The decay quantity
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.DecayQuantity DecayQuantity;
        /// <summary>
        ///  The original contract size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.OriginalContractSize OriginalContractSize;
        /// <summary>
        ///  The leg instrument id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegInstrumentId LegInstrumentId;
        /// <summary>
        ///  The leg ratio price numerator
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegRatioPriceNumerator LegRatioPriceNumerator;
        /// <summary>
        ///  The leg ratio price denominator
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegRatioPriceDenominator LegRatioPriceDenominator;
        /// <summary>
        ///  The leg ratio qty numerator
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegRatioQtyNumerator LegRatioQtyNumerator;
        /// <summary>
        ///  The leg ratio qty denominator
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegRatioQtyDenominator LegRatioQtyDenominator;
        /// <summary>
        ///  The leg underlying id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegUnderlyingId LegUnderlyingId;
        /// <summary>
        ///  The appl id
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.ApplId ApplId;
        /// <summary>
        ///  The maturity year
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaturityYear MaturityYear;
        /// <summary>
        ///  The decay start date
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.DecayStartDate DecayStartDate;
        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Channel Channel;
        /// <summary>
        ///  The leg count
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegCount LegCount;
        /// <summary>
        ///  The leg index
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegIndex LegIndex;
        /// <summary>
        ///  The currency
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Currency Currency;
        /// <summary>
        ///  The settl currency
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SettlCurrency SettlCurrency;
        /// <summary>
        ///  The secsubtype
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Secsubtype Secsubtype;
        /// <summary>
        ///  The raw symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RawSymbol RawSymbol;
        /// <summary>
        ///  The group
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Group Group;
        /// <summary>
        ///  The exchange
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Exchange Exchange;
        /// <summary>
        ///  The asset
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Asset Asset;
        /// <summary>
        ///  The cfi
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Cfi Cfi;
        /// <summary>
        ///  The security type
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SecurityType SecurityType;
        /// <summary>
        ///  The unit of measure
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UnitOfMeasure UnitOfMeasure;
        /// <summary>
        ///  The underlying
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Underlying Underlying;
        /// <summary>
        ///  The strike price currency
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StrikePriceCurrency StrikePriceCurrency;
        /// <summary>
        ///  The leg raw symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegRawSymbol LegRawSymbol;
        /// <summary>
        ///  The classification of the instrument
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.InstrumentClassField InstrumentClass;
        /// <summary>
        ///  The matching algorithm used for the instrument, typically FIFO
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MatchAlgorithmField MatchAlgorithm;
        /// <summary>
        ///  The main fraction
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MainFraction MainFraction;
        /// <summary>
        ///  The price display format
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.PriceDisplayFormat PriceDisplayFormat;
        /// <summary>
        ///  The sub fraction
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SubFraction SubFraction;
        /// <summary>
        ///  The underlying product
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UnderlyingProduct UnderlyingProduct;
        /// <summary>
        ///  Indicates if the instrument definition has been added, modified, or deleted
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SecurityUpdateActionField SecurityUpdateAction;
        /// <summary>
        ///  The maturity month
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaturityMonth MaturityMonth;
        /// <summary>
        ///  The maturity day
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaturityDay MaturityDay;
        /// <summary>
        ///  The maturity week
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaturityWeek MaturityWeek;
        /// <summary>
        ///  Indicates if the instrument is user defined
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UserDefinedInstrumentField UserDefinedInstrument;
        /// <summary>
        ///  The contract multiplier unit
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.ContractMultiplierUnit ContractMultiplierUnit;
        /// <summary>
        ///  The flow schedule type
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.FlowScheduleType FlowScheduleType;
        /// <summary>
        ///  The tick rule
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TickRule TickRule;
        /// <summary>
        ///  The classification of the leg instrument
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegInstrumentClassField LegInstrumentClass;
        /// <summary>
        ///  The side taken for the leg when purchasing the spread
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegSideField LegSide;
        /// <summary>
        ///  Reserved, 17 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Reserved17 Reserved17;
    }

    /// <summary>
    ///  The Instrument Def Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.TsRecv.Absent;

        /// <summary>
        ///  The min price increment, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrement MinPriceIncrement { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrement.Absent;

        /// <summary>
        ///  The display factor, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.DisplayFactor DisplayFactor { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.DisplayFactor.Absent;

        /// <summary>
        ///  The last eligible trade time expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Expiration Expiration { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Expiration.Absent;

        /// <summary>
        ///  The time of instrument activation expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Activation Activation { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Activation.Absent;

        /// <summary>
        ///  The high limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.HighLimitPrice HighLimitPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.HighLimitPrice.Absent;

        /// <summary>
        ///  The low limit price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LowLimitPrice LowLimitPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.LowLimitPrice.Absent;

        /// <summary>
        ///  The max price variation, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MaxPriceVariation MaxPriceVariation { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.MaxPriceVariation.Absent;

        /// <summary>
        ///  The unit of measure qty, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UnitOfMeasureQty UnitOfMeasureQty { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.UnitOfMeasureQty.Absent;

        /// <summary>
        ///  The min price increment amount, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrementAmount MinPriceIncrementAmount { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.MinPriceIncrementAmount.Absent;

        /// <summary>
        ///  The price ratio, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.PriceRatio PriceRatio { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.PriceRatio.Absent;

        /// <summary>
        ///  The strike price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StrikePrice StrikePrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.StrikePrice.Absent;

        /// <summary>
        ///  The instrument ID assigned by the publisher; may be the same as instrument_id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.RawInstrumentId RawInstrumentId { get; init; }

        /// <summary>
        ///  The leg price, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegPrice LegPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.LegPrice.Absent;

        /// <summary>
        ///  The leg delta, where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.LegDelta LegDelta { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.LegDelta.Absent;

        /// <summary>
        ///  A bitmap of instrument eligibility attributes; venue-specific
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.InstAttribValue InstAttribValue { get; init; }

        /// <summary>
        ///  The underlying id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.UnderlyingId UnderlyingId { get; init; }

        /// <summary>
        ///  The market depth implied
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MarketDepthImplied MarketDepthImplied { get; init; }

        /// <summary>
        ///  The market depth
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MarketDepth MarketDepth { get; init; }

        /// <summary>
        ///  The market segment id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MarketSegmentId MarketSegmentId { get; init; }

        /// <summary>
        ///  The max trade vol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MaxTradeVol MaxTradeVol { get; init; }

        /// <summary>
        ///  The min lot size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MinLotSize MinLotSize { get; init; }

        /// <summary>
        ///  The min lot size block
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MinLotSizeBlock MinLotSizeBlock { get; init; }

        /// <summary>
        ///  The min lot size round lot
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MinLotSizeRoundLot MinLotSizeRoundLot { get; init; }

        /// <summary>
        ///  The min trade vol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MinTradeVol MinTradeVol { get; init; }

        /// <summary>
        ///  The contract multiplier
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.ContractMultiplier ContractMultiplier { get; init; }

        /// <summary>
        ///  The decay quantity
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.DecayQuantity DecayQuantity { get; init; }

        /// <summary>
        ///  The original contract size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.OriginalContractSize OriginalContractSize { get; init; }

        /// <summary>
        ///  The leg instrument id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegInstrumentId LegInstrumentId { get; init; }

        /// <summary>
        ///  The leg ratio price numerator
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegRatioPriceNumerator LegRatioPriceNumerator { get; init; }

        /// <summary>
        ///  The leg ratio price denominator
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegRatioPriceDenominator LegRatioPriceDenominator { get; init; }

        /// <summary>
        ///  The leg ratio qty numerator
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegRatioQtyNumerator LegRatioQtyNumerator { get; init; }

        /// <summary>
        ///  The leg ratio qty denominator
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegRatioQtyDenominator LegRatioQtyDenominator { get; init; }

        /// <summary>
        ///  The leg underlying id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegUnderlyingId LegUnderlyingId { get; init; }

        /// <summary>
        ///  The appl id
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.ApplId ApplId { get; init; }

        /// <summary>
        ///  The maturity year
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MaturityYear MaturityYear { get; init; }

        /// <summary>
        ///  The decay start date
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.DecayStartDate DecayStartDate { get; init; }

        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Channel Channel { get; init; }

        /// <summary>
        ///  The leg count
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegCount LegCount { get; init; }

        /// <summary>
        ///  The leg index
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegIndex LegIndex { get; init; }

        /// <summary>
        ///  The currency
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Currency Currency { get; init; }

        /// <summary>
        ///  The settl currency
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SettlCurrency SettlCurrency { get; init; }

        /// <summary>
        ///  The secsubtype
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Secsubtype Secsubtype { get; init; }

        /// <summary>
        ///  The raw symbol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.RawSymbol RawSymbol { get; init; }

        /// <summary>
        ///  The group
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Group Group { get; init; }

        /// <summary>
        ///  The exchange
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Exchange Exchange { get; init; }

        /// <summary>
        ///  The asset
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Asset Asset { get; init; }

        /// <summary>
        ///  The cfi
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Cfi Cfi { get; init; }

        /// <summary>
        ///  The security type
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SecurityType SecurityType { get; init; }

        /// <summary>
        ///  The unit of measure
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.UnitOfMeasure UnitOfMeasure { get; init; }

        /// <summary>
        ///  The underlying
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Underlying Underlying { get; init; }

        /// <summary>
        ///  The strike price currency
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.StrikePriceCurrency StrikePriceCurrency { get; init; }

        /// <summary>
        ///  The leg raw symbol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegRawSymbol LegRawSymbol { get; init; }

        /// <summary>
        ///  The classification of the instrument
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.InstrumentClassField InstrumentClass { get; init; }

        /// <summary>
        ///  The matching algorithm used for the instrument, typically FIFO
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MatchAlgorithmField MatchAlgorithm { get; init; }

        /// <summary>
        ///  The main fraction
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MainFraction MainFraction { get; init; }

        /// <summary>
        ///  The price display format
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.PriceDisplayFormat PriceDisplayFormat { get; init; }

        /// <summary>
        ///  The sub fraction
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SubFraction SubFraction { get; init; }

        /// <summary>
        ///  The underlying product
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.UnderlyingProduct UnderlyingProduct { get; init; }

        /// <summary>
        ///  Indicates if the instrument definition has been added, modified, or deleted
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SecurityUpdateActionField SecurityUpdateAction { get; init; }

        /// <summary>
        ///  The maturity month
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MaturityMonth MaturityMonth { get; init; }

        /// <summary>
        ///  The maturity day
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MaturityDay MaturityDay { get; init; }

        /// <summary>
        ///  The maturity week
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.MaturityWeek MaturityWeek { get; init; }

        /// <summary>
        ///  Indicates if the instrument is user defined
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.UserDefinedInstrumentField UserDefinedInstrument { get; init; }

        /// <summary>
        ///  The contract multiplier unit
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.ContractMultiplierUnit ContractMultiplierUnit { get; init; }

        /// <summary>
        ///  The flow schedule type
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.FlowScheduleType FlowScheduleType { get; init; }

        /// <summary>
        ///  The tick rule
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.TickRule TickRule { get; init; }

        /// <summary>
        ///  The classification of the leg instrument
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegInstrumentClassField LegInstrumentClass { get; init; }

        /// <summary>
        ///  The side taken for the leg when purchasing the spread
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.LegSideField LegSide { get; init; }

        /// <summary>
        ///  Creates the value of the Instrument Def Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinPriceIncrement + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDisplayFactor + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeExpiration + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeActivation + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeHighLimitPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLowLimitPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaxPriceVariation + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnitOfMeasureQty + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinPriceIncrementAmount + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePriceRatio + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStrikePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRawInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegPrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstAttribValue + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlyingId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketDepthImplied + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketDepth + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMarketSegmentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaxTradeVol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSizeBlock + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinLotSizeRoundLot + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMinTradeVol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeContractMultiplier + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDecayQuantity + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeOriginalContractSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioPriceNumerator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioPriceDenominator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioQtyNumerator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRatioQtyDenominator + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegUnderlyingId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeApplId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityYear + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDecayStartDate + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeChannel + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegCount + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegIndex + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSettlCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecsubtype + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRawSymbol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeGroup + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeExchange + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeAsset + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeCfi + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecurityType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnitOfMeasure + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlying + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStrikePriceCurrency + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegRawSymbol + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstrumentClass + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMatchAlgorithm + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMainFraction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePriceDisplayFormat + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSubFraction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUnderlyingProduct + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSecurityUpdateAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityMonth + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityDay + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMaturityWeek + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUserDefinedInstrument + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeContractMultiplierUnit + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeFlowScheduleType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTickRule + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegInstrumentClass + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLegSide + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved17;

    /// <summary>
    ///  Writes the Instrument Def Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Instrument Def Msg.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        ref Layout instrumentDefMsgTsRecvWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        instrumentDefMsgTsRecvWriterLayout.TsRecv = value.TsRecv;
        instrumentDefMsgTsRecvWriterLayout.MinPriceIncrement = value.MinPriceIncrement;
        instrumentDefMsgTsRecvWriterLayout.DisplayFactor = value.DisplayFactor;
        instrumentDefMsgTsRecvWriterLayout.Expiration = value.Expiration;
        instrumentDefMsgTsRecvWriterLayout.Activation = value.Activation;
        instrumentDefMsgTsRecvWriterLayout.HighLimitPrice = value.HighLimitPrice;
        instrumentDefMsgTsRecvWriterLayout.LowLimitPrice = value.LowLimitPrice;
        instrumentDefMsgTsRecvWriterLayout.MaxPriceVariation = value.MaxPriceVariation;
        instrumentDefMsgTsRecvWriterLayout.UnitOfMeasureQty = value.UnitOfMeasureQty;
        instrumentDefMsgTsRecvWriterLayout.MinPriceIncrementAmount = value.MinPriceIncrementAmount;
        instrumentDefMsgTsRecvWriterLayout.PriceRatio = value.PriceRatio;
        instrumentDefMsgTsRecvWriterLayout.StrikePrice = value.StrikePrice;
        instrumentDefMsgTsRecvWriterLayout.RawInstrumentId = value.RawInstrumentId;
        instrumentDefMsgTsRecvWriterLayout.LegPrice = value.LegPrice;
        instrumentDefMsgTsRecvWriterLayout.LegDelta = value.LegDelta;
        instrumentDefMsgTsRecvWriterLayout.InstAttribValue = value.InstAttribValue;
        instrumentDefMsgTsRecvWriterLayout.UnderlyingId = value.UnderlyingId;
        instrumentDefMsgTsRecvWriterLayout.MarketDepthImplied = value.MarketDepthImplied;
        instrumentDefMsgTsRecvWriterLayout.MarketDepth = value.MarketDepth;
        instrumentDefMsgTsRecvWriterLayout.MarketSegmentId = value.MarketSegmentId;
        instrumentDefMsgTsRecvWriterLayout.MaxTradeVol = value.MaxTradeVol;
        instrumentDefMsgTsRecvWriterLayout.MinLotSize = value.MinLotSize;
        instrumentDefMsgTsRecvWriterLayout.MinLotSizeBlock = value.MinLotSizeBlock;
        instrumentDefMsgTsRecvWriterLayout.MinLotSizeRoundLot = value.MinLotSizeRoundLot;
        instrumentDefMsgTsRecvWriterLayout.MinTradeVol = value.MinTradeVol;
        instrumentDefMsgTsRecvWriterLayout.ContractMultiplier = value.ContractMultiplier;
        instrumentDefMsgTsRecvWriterLayout.DecayQuantity = value.DecayQuantity;
        instrumentDefMsgTsRecvWriterLayout.OriginalContractSize = value.OriginalContractSize;
        instrumentDefMsgTsRecvWriterLayout.LegInstrumentId = value.LegInstrumentId;
        instrumentDefMsgTsRecvWriterLayout.LegRatioPriceNumerator = value.LegRatioPriceNumerator;
        instrumentDefMsgTsRecvWriterLayout.LegRatioPriceDenominator = value.LegRatioPriceDenominator;
        instrumentDefMsgTsRecvWriterLayout.LegRatioQtyNumerator = value.LegRatioQtyNumerator;
        instrumentDefMsgTsRecvWriterLayout.LegRatioQtyDenominator = value.LegRatioQtyDenominator;
        instrumentDefMsgTsRecvWriterLayout.LegUnderlyingId = value.LegUnderlyingId;
        instrumentDefMsgTsRecvWriterLayout.ApplId = value.ApplId;
        instrumentDefMsgTsRecvWriterLayout.MaturityYear = value.MaturityYear;
        instrumentDefMsgTsRecvWriterLayout.DecayStartDate = value.DecayStartDate;
        instrumentDefMsgTsRecvWriterLayout.Channel = value.Channel;
        instrumentDefMsgTsRecvWriterLayout.LegCount = value.LegCount;
        instrumentDefMsgTsRecvWriterLayout.LegIndex = value.LegIndex;
        instrumentDefMsgTsRecvWriterLayout.Currency = value.Currency;
        instrumentDefMsgTsRecvWriterLayout.SettlCurrency = value.SettlCurrency;
        instrumentDefMsgTsRecvWriterLayout.Secsubtype = value.Secsubtype;
        instrumentDefMsgTsRecvWriterLayout.RawSymbol = value.RawSymbol;
        instrumentDefMsgTsRecvWriterLayout.Group = value.Group;
        instrumentDefMsgTsRecvWriterLayout.Exchange = value.Exchange;
        instrumentDefMsgTsRecvWriterLayout.Asset = value.Asset;
        instrumentDefMsgTsRecvWriterLayout.Cfi = value.Cfi;
        instrumentDefMsgTsRecvWriterLayout.SecurityType = value.SecurityType;
        instrumentDefMsgTsRecvWriterLayout.UnitOfMeasure = value.UnitOfMeasure;
        instrumentDefMsgTsRecvWriterLayout.Underlying = value.Underlying;
        instrumentDefMsgTsRecvWriterLayout.StrikePriceCurrency = value.StrikePriceCurrency;
        instrumentDefMsgTsRecvWriterLayout.LegRawSymbol = value.LegRawSymbol;
        instrumentDefMsgTsRecvWriterLayout.InstrumentClass = value.InstrumentClass;
        instrumentDefMsgTsRecvWriterLayout.MatchAlgorithm = value.MatchAlgorithm;
        instrumentDefMsgTsRecvWriterLayout.MainFraction = value.MainFraction;
        instrumentDefMsgTsRecvWriterLayout.PriceDisplayFormat = value.PriceDisplayFormat;
        instrumentDefMsgTsRecvWriterLayout.SubFraction = value.SubFraction;
        instrumentDefMsgTsRecvWriterLayout.UnderlyingProduct = value.UnderlyingProduct;
        instrumentDefMsgTsRecvWriterLayout.SecurityUpdateAction = value.SecurityUpdateAction;
        instrumentDefMsgTsRecvWriterLayout.MaturityMonth = value.MaturityMonth;
        instrumentDefMsgTsRecvWriterLayout.MaturityDay = value.MaturityDay;
        instrumentDefMsgTsRecvWriterLayout.MaturityWeek = value.MaturityWeek;
        instrumentDefMsgTsRecvWriterLayout.UserDefinedInstrument = value.UserDefinedInstrument;
        instrumentDefMsgTsRecvWriterLayout.ContractMultiplierUnit = value.ContractMultiplierUnit;
        instrumentDefMsgTsRecvWriterLayout.FlowScheduleType = value.FlowScheduleType;
        instrumentDefMsgTsRecvWriterLayout.TickRule = value.TickRule;
        instrumentDefMsgTsRecvWriterLayout.LegInstrumentClass = value.LegInstrumentClass;
        instrumentDefMsgTsRecvWriterLayout.LegSide = value.LegSide;
        instrumentDefMsgTsRecvWriterLayout.Reserved17 = default;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
