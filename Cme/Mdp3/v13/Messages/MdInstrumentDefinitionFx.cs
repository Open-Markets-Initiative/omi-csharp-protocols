using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Instrument Definition Fx: MDInstrumentDefinitionFX
/// </summary>

public partial class MdInstrumentDefinitionFx
{
    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  Total number of instruments in the Replay loop. Used on Replay Feed only
    /// </summary>
    public uint TotNumReportsOptional => Fields.TotNumReportsOptional.Value;

    /// <summary>
    ///  Last Security update action on Incremental feed, 'D' or 'M' is used when a mid-week deletion or modification (i.e. extension) occurs
    /// </summary>
    public SecurityUpdateAction SecurityUpdateAction => Fields.SecurityUpdateAction.Value;

    /// <summary>
    ///  UTC Date and time of last Security Definition add, update or delete on a given Market Data channel
    /// </summary>
    public DateTime LastUpdateTime => Fields.LastUpdateTime.Value;

    /// <summary>
    ///  Identifies the current trading state of the instrument
    /// </summary>
    public MdSecurityTradingStatus MdSecurityTradingStatus => Fields.MdSecurityTradingStatus.Value;

    /// <summary>
    ///  The channel ID as defined in the XML Configuration file
    /// </summary>
    public short ApplId => Fields.ApplId.Value;

    /// <summary>
    ///  Identifies the market segment, populated for all CME Globex instruments
    /// </summary>
    public byte MarketSegmentId => Fields.MarketSegmentId.Value;

    /// <summary>
    ///  Product complex
    /// </summary>
    public byte UnderlyingProduct => Fields.UnderlyingProduct.Value;

    /// <summary>
    ///  Exchange used to identify a security
    /// </summary>
    public string SecurityExchange => Fields.SecurityExchange.Value;

    /// <summary>
    ///  Security Group
    /// </summary>
    public string SecurityGroup => Fields.SecurityGroup.Value;

    /// <summary>
    ///  Product Code within Security Group specified
    /// </summary>
    public string Asset => Fields.Asset.Value;

    /// <summary>
    ///  Instrument Name or Symbol
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Security ID
    /// </summary>
    public int SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Security Type
    /// </summary>
    public string SecurityType => Fields.SecurityType.Value;

    /// <summary>
    ///  ISO standard instrument categorization code
    /// </summary>
    public string CfiCode => Fields.CfiCode.Value;

    /// <summary>
    ///  Identifies currency used for price
    /// </summary>
    public string Currency => Fields.Currency.Value;

    /// <summary>
    ///  Identifies currency used for settlement, if different from trading currency
    /// </summary>
    public string SettlCurrency => Fields.SettlCurrency.Value;

    /// <summary>
    ///  Local (counter) currency
    /// </summary>
    public string PriceQuoteCurrency => Fields.PriceQuoteCurrency.Value;

    /// <summary>
    ///  Matching algorithm
    /// </summary>
    public char MatchAlgorithm => Fields.MatchAlgorithm.Value;

    /// <summary>
    ///  The minimum trading volume for a security
    /// </summary>
    public uint MinTradeVol => Fields.MinTradeVol.Value;

    /// <summary>
    ///  The maximum trading volume for a security
    /// </summary>
    public uint MaxTradeVol => Fields.MaxTradeVol.Value;

    /// <summary>
    ///  Minimum constant tick for the instrument, sent only if instrument is non-VTT (Variable Tick table) eligible
    /// </summary>
    public long MinPriceIncrement => Fields.MinPriceIncrement.Value;

    /// <summary>
    ///  Contains the multiplier to convert the CME Globex display price to the conventional price
    /// </summary>
    public long DisplayFactor => Fields.DisplayFactor.Value;

    /// <summary>
    ///  Specifies price decimal precision for EBS instrument
    /// </summary>
    public byte PricePrecision => Fields.PricePrecision.Value;

    /// <summary>
    ///  Unit of measure for the products' original contract size. This will be populated for all products listed on CME Globex
    /// </summary>
    public string UnitOfMeasure => Fields.UnitOfMeasure.Value;

    /// <summary>
    ///  This field contains the contract size for each instrument. Used in combination with tag 996-UnitofMeasure
    /// </summary>
    public long UnitOfMeasureQty => Fields.UnitOfMeasureQty.Value;

    /// <summary>
    ///  Upper price threshold for the instrument
    /// </summary>
    public long HighLimitPrice => Fields.HighLimitPrice.Value;

    /// <summary>
    ///  Lower price threshold for the instrument
    /// </summary>
    public long LowLimitPrice => Fields.LowLimitPrice.Value;

    /// <summary>
    ///  Differential value for price banding
    /// </summary>
    public long MaxPriceVariation => Fields.MaxPriceVariation.Value;

    /// <summary>
    ///  User-defined instruments flag
    /// </summary>
    public char UserDefinedInstrument => Fields.UserDefinedInstrument.Value;

    /// <summary>
    ///  Long name of the instrument
    /// </summary>
    public string FinancialInstrumentFullName => Fields.FinancialInstrumentFullName.Value;

    /// <summary>
    ///  Base/Local. Denotes the currency pair in CCY1/CCY2 convention
    /// </summary>
    public string FxCurrencySymbol => Fields.FxCurrencySymbol.Value;

    /// <summary>
    ///  For SPOTs will contain 0. For Fixed date NDFs will contain the value 'B'. For the standard NDFs tenors expressed using Dx, Mx, Wx, and Yx values, where 'x' does not denote business days, but calendar days
    /// </summary>
    public string SettlType => Fields.SettlType.Value;

    /// <summary>
    ///  For SPOT, number of business days between trade date and value (settlement) date. For NDF, number of business days between NDF valuation (fixing) and settlement
    /// </summary>
    public ushort InterveningDays => Fields.InterveningDays.Value;

    /// <summary>
    ///  Fixing Rate Description
    /// </summary>
    public string FxBenchmarkRateFix => Fields.FxBenchmarkRateFix.Value;

    /// <summary>
    ///  Fixing Rate Source
    /// </summary>
    public string RateSource => Fields.RateSource.Value;

    /// <summary>
    ///  Fixing Rate Local Time, denoted in HH:MM:SS format
    /// </summary>
    public string FixRateLocalTime => Fields.FixRateLocalTime.Value;

    /// <summary>
    ///  Fixing Rate Local Time Zone corresponding to Fixing Local Time.
    /// </summary>
    public string FixRateLocalTimeZone => Fields.FixRateLocalTimeZone.Value;

    /// <summary>
    ///  Minimum Quote Life in number of microseconds
    /// </summary>
    public uint MinQuoteLife => Fields.MinQuoteLife.Value;

    /// <summary>
    ///  Max allowed discretionary offset from Limit order price. When the value in this field = 0, discretionary price cannot be submitted for the instrument
    /// </summary>
    public long MaxPriceDiscretionOffset => Fields.MaxPriceDiscretionOffset.Value;

    /// <summary>
    ///  External unique instrument ID
    /// </summary>
    public ulong InstrumentGuid => Fields.InstrumentGuid.Value;

    /// <summary>
    ///  MDInstrumentDefinitionFuture
    /// </summary>
    public string MaturityMonthYear => Fields.MaturityMonthYear.Value;

    /// <summary>
    ///  Settlement Locale. Optionally used to differentiate settlement location
    /// </summary>
    public string SettlementLocale => Fields.SettlementLocale.Value;

    /// <summary>
    ///  New sub-tick which is only available for order entry when certain conditions are met, tick value which corresponds to the Alt Min Quote Life
    /// </summary>
    public long AltMinPriceIncrement => Fields.AltMinPriceIncrement.Value;

    /// <summary>
    ///  MQL duration in number of microseconds applied to orders at AltMinPriceIncrement
    /// </summary>
    public uint AltMinQuoteLife => Fields.AltMinQuoteLife.Value;

    /// <summary>
    ///  Minimum price offset better than the best Standard Tick order for an order to be allowed into the market
    /// </summary>
    public long AltPriceIncrementConstraint => Fields.AltPriceIncrementConstraint.Value;

    /// <summary>
    ///  Maximum bid/ask spread for which sub-tick orders will be accepted (Sub tick orders will be rejected if bid/ask spread is greater than this value)
    /// </summary>
    public long MaxBidAskConstraint => Fields.MaxBidAskConstraint.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MatchEventIndicator MatchEventIndicator;
        public TotNumReportsOptional TotNumReportsOptional;
        public SecurityUpdateAction SecurityUpdateAction;
        public LastUpdateTime LastUpdateTime;
        public MdSecurityTradingStatus MdSecurityTradingStatus;
        public ApplId ApplId;
        public MarketSegmentId MarketSegmentId;
        public UnderlyingProduct UnderlyingProduct;
        public SecurityExchange SecurityExchange;
        public SecurityGroup SecurityGroup;
        public Asset Asset;
        public Symbol Symbol;
        public SecurityId SecurityId;
        public SecurityType SecurityType;
        public CfiCode CfiCode;
        public Currency Currency;
        public SettlCurrency SettlCurrency;
        public PriceQuoteCurrency PriceQuoteCurrency;
        public MatchAlgorithm MatchAlgorithm;
        public MinTradeVol MinTradeVol;
        public MaxTradeVol MaxTradeVol;
        public MinPriceIncrement MinPriceIncrement;
        public DisplayFactor DisplayFactor;
        public PricePrecision PricePrecision;
        public UnitOfMeasure UnitOfMeasure;
        public UnitOfMeasureQty UnitOfMeasureQty;
        public HighLimitPrice HighLimitPrice;
        public LowLimitPrice LowLimitPrice;
        public MaxPriceVariation MaxPriceVariation;
        public UserDefinedInstrument UserDefinedInstrument;
        public FinancialInstrumentFullName FinancialInstrumentFullName;
        public FxCurrencySymbol FxCurrencySymbol;
        public SettlType SettlType;
        public InterveningDays InterveningDays;
        public FxBenchmarkRateFix FxBenchmarkRateFix;
        public RateSource RateSource;
        public FixRateLocalTime FixRateLocalTime;
        public FixRateLocalTimeZone FixRateLocalTimeZone;
        public MinQuoteLife MinQuoteLife;
        public MaxPriceDiscretionOffset MaxPriceDiscretionOffset;
        public InstrumentGuid InstrumentGuid;
        public MaturityMonthYear MaturityMonthYear;
        public SettlementLocale SettlementLocale;
        public AltMinPriceIncrement AltMinPriceIncrement;
        public AltMinQuoteLife AltMinQuoteLife;
        public AltPriceIncrementConstraint AltPriceIncrementConstraint;
        public MaxBidAskConstraint MaxBidAskConstraint;
    };

    protected Layout Fields;
};
