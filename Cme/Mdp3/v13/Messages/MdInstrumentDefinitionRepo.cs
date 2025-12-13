using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Instrument Definition Repo: MDInstrumentDefinitionRepo
/// </summary>

public partial class MdInstrumentDefinitionRepo
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
    ///  Indicates session date corresponding to the settlement price in tag 1150-TradingReferencePrice
    /// </summary>
    public ushort TradingReferenceDate => Fields.TradingReferenceDate.Value;

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
    ///  Long name of the instrument
    /// </summary>
    public string FinancialInstrumentFullName => Fields.FinancialInstrumentFullName.Value;

    /// <summary>
    ///  Clearing organization
    /// </summary>
    public string PartyRoleClearingOrg => Fields.PartyRoleClearingOrg.Value;

    /// <summary>
    ///  Start date of a financing deal, i.e. the date the buyer pays the seller cash and takes control of the collateral
    /// </summary>
    public ushort StartDate => Fields.StartDate.Value;

    /// <summary>
    ///  End date of a financing deal, i.e. the date the seller reimburses the buyer and takes back control of the collateral
    /// </summary>
    public ushort EndDate => Fields.EndDate.Value;

    /// <summary>
    ///  For Repos the timing or method for terminating the agreement. Term code
    /// </summary>
    public string TerminationType => Fields.TerminationType.Value;

    /// <summary>
    ///  Repo Sub Security Type
    /// </summary>
    public RepoSubType RepoSubType => Fields.RepoSubType.Value;

    /// <summary>
    ///  Money or Par indicates if the GC is filled by par amount or by money amount
    /// </summary>
    public MoneyOrPar MoneyOrPar => Fields.MoneyOrPar.Value;

    /// <summary>
    ///  Max number of substitutions allowed. The value of 0 indicates that substitutions are not allowed
    /// </summary>
    public byte MaxNoOfSubstitutions => Fields.MaxNoOfSubstitutions.Value;

    /// <summary>
    ///  Price quotation method
    /// </summary>
    public string PriceQuoteMethod => Fields.PriceQuoteMethod.Value;

    /// <summary>
    ///  User-defined instruments flag
    /// </summary>
    public char UserDefinedInstrument => Fields.UserDefinedInstrument.Value;

    /// <summary>
    ///  Risk Set identifies the list of instruments sharing credit limits set up
    /// </summary>
    public string RiskSet => Fields.RiskSet.Value;

    /// <summary>
    ///  Market Set defines the bilateral relationship and Self Match Prevention configuration for eligible markets
    /// </summary>
    public string MarketSet => Fields.MarketSet.Value;

    /// <summary>
    ///  External unique instrument ID
    /// </summary>
    public ulong InstrumentGuid => Fields.InstrumentGuid.Value;

    /// <summary>
    ///  Full Repo Term Code
    /// </summary>
    public string TermCode => Fields.TermCode.Value;

    /// <summary>
    ///  Optionally used in tailor made repo contracts and defines the type of broken dates as requested by trader. Null = Not applicable, 0 = Custom, 1 = Quarterly,  2 = Monthly
    /// </summary>
    public byte BrokenDateTermType => Fields.BrokenDateTermType.Value;

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
        public MatchAlgorithm MatchAlgorithm;
        public MinTradeVol MinTradeVol;
        public MaxTradeVol MaxTradeVol;
        public MinPriceIncrement MinPriceIncrement;
        public DisplayFactor DisplayFactor;
        public UnitOfMeasure UnitOfMeasure;
        public UnitOfMeasureQty UnitOfMeasureQty;
        public TradingReferencePrice TradingReferencePrice;
        public TradingReferenceDate TradingReferenceDate;
        public HighLimitPrice HighLimitPrice;
        public LowLimitPrice LowLimitPrice;
        public MaxPriceVariation MaxPriceVariation;
        public FinancialInstrumentFullName FinancialInstrumentFullName;
        public PartyRoleClearingOrg PartyRoleClearingOrg;
        public StartDate StartDate;
        public EndDate EndDate;
        public TerminationType TerminationType;
        public RepoSubType RepoSubType;
        public MoneyOrPar MoneyOrPar;
        public MaxNoOfSubstitutions MaxNoOfSubstitutions;
        public PriceQuoteMethod PriceQuoteMethod;
        public UserDefinedInstrument UserDefinedInstrument;
        public RiskSet RiskSet;
        public MarketSet MarketSet;
        public InstrumentGuid InstrumentGuid;
        public TermCode TermCode;
        public BrokenDateTermType BrokenDateTermType;
    };

    protected Layout Fields;
};
