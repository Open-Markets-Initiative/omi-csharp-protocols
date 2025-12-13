using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Instrument Definition Option: MDInstrumentDefinitionOption
/// </summary>

public partial class MdInstrumentDefinitionOption
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
    ///  Indicates whether an option instrument is a put or call
    /// </summary>
    public PutOrCall PutOrCall => Fields.PutOrCall.Value;

    /// <summary>
    ///  MDInstrumentDefinitionFuture
    /// </summary>
    public string MaturityMonthYear => Fields.MaturityMonthYear.Value;

    /// <summary>
    ///  Identifies currency used for price
    /// </summary>
    public string Currency => Fields.Currency.Value;

    /// <summary>
    ///  Strike Price for an option instrument
    /// </summary>
    public long StrikePrice => Fields.StrikePrice.Value;

    /// <summary>
    ///  Currency in which the StrikePrice is denominated
    /// </summary>
    public string StrikeCurrency => Fields.StrikeCurrency.Value;

    /// <summary>
    ///  Identifies currency used for settlement, if different from trading currency
    /// </summary>
    public string SettlCurrency => Fields.SettlCurrency.Value;

    /// <summary>
    ///  Defines cabinet price for outright options products
    /// </summary>
    public long MinCabPrice => Fields.MinCabPrice.Value;

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
    ///  Minimum constant tick for the instrument
    /// </summary>
    public long MinPriceIncrementOptional => Fields.MinPriceIncrementOptional.Value;

    /// <summary>
    ///  Monetary value equivalent to the minimum price fluctuation
    /// </summary>
    public long MinPriceIncrementAmount => Fields.MinPriceIncrementAmount.Value;

    /// <summary>
    ///  Contains the multiplier to convert the CME Globex display price to the conventional price
    /// </summary>
    public long DisplayFactor => Fields.DisplayFactor.Value;

    /// <summary>
    ///  VTT code referencing variable tick table
    /// </summary>
    public sbyte TickRule => Fields.TickRule.Value;

    /// <summary>
    ///  Price Denominator of Main Fraction
    /// </summary>
    public byte MainFraction => Fields.MainFraction.Value;

    /// <summary>
    ///  Price Denominator of Sub Fraction
    /// </summary>
    public byte SubFraction => Fields.SubFraction.Value;

    /// <summary>
    ///  Number of decimals in fractional display price
    /// </summary>
    public byte PriceDisplayFormat => Fields.PriceDisplayFormat.Value;

    /// <summary>
    ///  Unit of measure for the products' original contract size. This will be populated for all products listed on CME Globex
    /// </summary>
    public string UnitOfMeasure => Fields.UnitOfMeasure.Value;

    /// <summary>
    ///  This field contains the contract size for each instrument. Used in combination with tag 996-UnitofMeasure
    /// </summary>
    public long UnitOfMeasureQty => Fields.UnitOfMeasureQty.Value;

    /// <summary>
    ///  Reference price for prelisted instruments or the last calculated Settlement whether it be Theoretical, Preliminary or a Final Settle of the session.
    /// </summary>
    public long TradingReferencePrice => Fields.TradingReferencePrice.Value;

    /// <summary>
    ///  SettlPriceType bit set
    /// </summary>
    public string SettlPriceType => Fields.SettlPriceType.Value;

    /// <summary>
    ///  The total cleared volume of instrument traded during the prior trading session.
    /// </summary>
    public int ClearedVolume => Fields.ClearedVolume.Value;

    /// <summary>
    ///  The total open interest for the market at the close of the prior trading session.
    /// </summary>
    public int OpenInterestQty => Fields.OpenInterestQty.Value;

    /// <summary>
    ///  Lower price threshold for the instrument
    /// </summary>
    public long LowLimitPrice => Fields.LowLimitPrice.Value;

    /// <summary>
    ///  Upper price threshold for the instrument
    /// </summary>
    public long HighLimitPrice => Fields.HighLimitPrice.Value;

    /// <summary>
    ///  User-defined instruments flag
    /// </summary>
    public char UserDefinedInstrument => Fields.UserDefinedInstrument.Value;

    /// <summary>
    ///  Indicates session date corresponding to the settlement price in tag 1150-TradingReferencePrice
    /// </summary>
    public ushort TradingReferenceDate => Fields.TradingReferenceDate.Value;

    /// <summary>
    ///  External unique instrument ID
    /// </summary>
    public ulong InstrumentGuid => Fields.InstrumentGuid.Value;

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
        public PutOrCall PutOrCall;
        public MaturityMonthYear MaturityMonthYear;
        public Currency Currency;
        public StrikePrice StrikePrice;
        public StrikeCurrency StrikeCurrency;
        public SettlCurrency SettlCurrency;
        public MinCabPrice MinCabPrice;
        public MatchAlgorithm MatchAlgorithm;
        public MinTradeVol MinTradeVol;
        public MaxTradeVol MaxTradeVol;
        public MinPriceIncrementOptional MinPriceIncrementOptional;
        public MinPriceIncrementAmount MinPriceIncrementAmount;
        public DisplayFactor DisplayFactor;
        public TickRule TickRule;
        public MainFraction MainFraction;
        public SubFraction SubFraction;
        public PriceDisplayFormat PriceDisplayFormat;
        public UnitOfMeasure UnitOfMeasure;
        public UnitOfMeasureQty UnitOfMeasureQty;
        public TradingReferencePrice TradingReferencePrice;
        public SettlPriceType SettlPriceType;
        public ClearedVolume ClearedVolume;
        public OpenInterestQty OpenInterestQty;
        public LowLimitPrice LowLimitPrice;
        public HighLimitPrice HighLimitPrice;
        public UserDefinedInstrument UserDefinedInstrument;
        public TradingReferenceDate TradingReferenceDate;
        public InstrumentGuid InstrumentGuid;
    };

    protected Layout Fields;
};
