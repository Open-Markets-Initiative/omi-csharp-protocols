using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  New Futures Strategy Definition Message: New Futures Strategy Definition messages will be sent out when new UDS markets for futures are created.
/// </summary>

public partial class NewFuturesStrategyDefinitionMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Futures Contract Symbol
    /// </summary>
    public string FuturesContractSymbol => Fields.FuturesContractSymbol.Value;

    /// <summary>
    ///  See Appendix A on the trading status codes
    /// </summary>
    public TradingStatus TradingStatus => Fields.TradingStatus.Value;

    /// <summary>
    ///  Denominator for the order price fields in this market.
    /// </summary>
    public char OrderPriceDenominator => Fields.OrderPriceDenominator.Value;

    /// <summary>
    ///  Minimum increment premium price for this market. OrderPriceDenominator should be applied to this field.
    /// </summary>
    public int IncrementPrice => Fields.IncrementPrice.Value;

    /// <summary>
    ///  Minimum increment quantity for this market
    /// </summary>
    public int IncrementQty => Fields.IncrementQty.Value;

    /// <summary>
    ///  Minimum quantity for this market
    /// </summary>
    public int MinQty => Fields.MinQty.Value;

    /// <summary>
    ///  Number of strategy leg definitions. The leg info
    /// </summary>
    public sbyte NumberOfLegDefinitions => Fields.NumberOfLegDefinitions.Value;

    /// <summary>
    ///  Contains the Strategy Code for defined market where applicable. See Appendix E for list of codes.
    /// </summary>
    public SecuritySubType SecuritySubType => Fields.SecuritySubType.Value;

    /// <summary>
    ///  Indicates if Market is only tradable via ICE Block Trade. This also means the screen trading is not allowed for the market. ‘Y’ or ‘N’
    /// </summary>
    public IsBlockOnly IsBlockOnly => Fields.IsBlockOnly.Value;

    /// <summary>
    ///  Strategy Symbol
    /// </summary>
    public string StrategySymbol => Fields.StrategySymbol.Value;

    /// <summary>
    ///  Indicates if GTC is allowed in the market. ‘Y’ or ‘N’
    /// </summary>
    public GtAllowed GtAllowed => Fields.GtAllowed.Value;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public string Reserved4 => Fields.Reserved4.Value;

    /// <summary>
    ///  Indicates MIFID-II market. ‘Y’ or ‘N’
    /// </summary>
    public MifidRegulatedMarket MifidRegulatedMarket => Fields.MifidRegulatedMarket.Value;

    /// <summary>
    ///  Description of the market
    /// </summary>
    public string MarketDesc => Fields.MarketDesc.Value;

    /// <summary>
    ///  4 digit year
    /// </summary>
    public short MaturityYear => Fields.MaturityYear.Value;

    /// <summary>
    ///  Month range 1-12
    /// </summary>
    public short MaturityMonth => Fields.MaturityMonth.Value;

    /// <summary>
    ///  Maturity Day
    /// </summary>
    public short MaturityDay => Fields.MaturityDay.Value;

    /// <summary>
    ///  Denominator for the deal price fields in the market. For most markets, this is the same as OrderPriceDenominator. However, it could be different for some crack or spread markets.
    /// </summary>
    public char DealPriceDenominator => Fields.DealPriceDenominator.Value;

    /// <summary>
    ///  The quantity in unit of measurement per lot. For example, it is 1000 barrels per lot for Brent.
    /// </summary>
    public int UnitQuantity => Fields.UnitQuantity.Value;

    /// <summary>
    ///  Only used for OffExchangeIncrementOptionPrice.
    /// </summary>
    public char NumDecimalsOptionsPrice => Fields.NumDecimalsOptionsPrice.Value;

    /// <summary>
    ///  Indicate if the market supports option markets, ‘Y’ or ‘N’
    /// </summary>
    public AllowOptions AllowOptions => Fields.AllowOptions.Value;

    /// <summary>
    ///  Clearing limit admin related
    /// </summary>
    public string ClearedAlias => Fields.ClearedAlias.Value;

    /// <summary>
    ///  ‘Y’ indicates this is a spread market, and, implied is allowed in this market
    /// </summary>
    public AllowsImplied AllowsImplied => Fields.AllowsImplied.Value;

    /// <summary>
    ///  Minimum Price, OrderPriceDenominator should be applied to this field
    /// </summary>
    public long MinPrice => Fields.MinPrice.Value;

    /// <summary>
    ///  Maximum Price, OrderPriceDenominator should be applied to this field
    /// </summary>
    public long MaxPrice => Fields.MaxPrice.Value;

    /// <summary>
    ///  Name of the product that the contract/market is under
    /// </summary>
    public string ProductName => Fields.ProductName.Value;

    /// <summary>
    ///  Alias of the hub for the contract/market
    /// </summary>
    public string HubAlias => Fields.HubAlias.Value;

    /// <summary>
    ///  Name of the strip for the contract/market
    /// </summary>
    public string StripName => Fields.StripName.Value;

    /// <summary>
    ///  Indicate if the contract is tradable. ‘Y’ or ‘N’.
    /// </summary>
    public IsTradable IsTradable => Fields.IsTradable.Value;

    /// <summary>
    ///  Denominator for the settlement price fields in the market. For most markets, this is the same as DealPriceDenominator.
    /// </summary>
    public char SettlePriceDenominator => Fields.SettlePriceDenominator.Value;

    /// <summary>
    ///  Market Identifier Code for the market.
    /// </summary>
    public string MicCode => Fields.MicCode.Value;

    /// <summary>
    ///  Denominator for UnitQuantity. This field will be ‘0’ for most of the markets.
    /// </summary>
    public char UnitQtyDenominator => Fields.UnitQtyDenominator.Value;

    /// <summary>
    ///  Indicate if the contract is for hedge only. ‘Y’ or ‘N’.
    /// </summary>
    public HedgeOnly HedgeOnly => Fields.HedgeOnly.Value;

    /// <summary>
    ///  Exchange silo code for the market.
    /// </summary>
    public ExchangeSilo ExchangeSilo => Fields.ExchangeSilo.Value;

    /// <summary>
    ///  Denominator for OffExchangeIncrementQty.
    /// </summary>
    public char OffExchangeIncrementQtyDenominator => Fields.OffExchangeIncrementQtyDenominator.Value;

    /// <summary>
    ///  Off exchange increment qty. OffExchangeIncrementQtyDenominator should be applied to this field.
    /// </summary>
    public int OffExchangeIncrementQty => Fields.OffExchangeIncrementQty.Value;

    /// <summary>
    ///  Off exchange increment price.
    /// </summary>
    public int OffExchangeIncrementPrice => Fields.OffExchangeIncrementPrice.Value;

    /// <summary>
    ///  Off exchange options increment price. NumDecimalsOptionsPrice should be applied to this field
    /// </summary>
    public int OffExchangeIncrementOptionPrice => Fields.OffExchangeIncrementOptionPrice.Value;

    /// <summary>
    ///  ID of the product that the contract/market is under.
    /// </summary>
    public int ProductId => Fields.ProductId.Value;

    /// <summary>
    ///  ID of the hub for the contract/market
    /// </summary>
    public int HubId => Fields.HubId.Value;

    /// <summary>
    ///  StripID
    /// </summary>
    public int StripId => Fields.StripId.Value;

    /// <summary>
    ///  The ISIN of the security this market is associated with. This is currently only populated for Liffe Equity markets.
    /// </summary>
    public string UnderlyingIsin => Fields.UnderlyingIsin.Value;

    /// <summary>
    ///  Indicates Test Market. ‘Y’ or ‘N’
    /// </summary>
    public TestMarketIndicator TestMarketIndicator => Fields.TestMarketIndicator.Value;

    /// <summary>
    ///  Indicates whether leg deals are suppressed. ‘Y’ or ‘N’
    /// </summary>
    public LegDealSuppressed LegDealSuppressed => Fields.LegDealSuppressed.Value;

    /// <summary>
    ///  Number of cycles for a contract.
    /// </summary>
    public short OldNumOfCycles => Fields.OldNumOfCycles.Value;

    /// <summary>
    ///  Number of cycles for a contract.
    /// </summary>
    public MarketTypeId MarketTypeId => Fields.MarketTypeId.Value;

    /// <summary>
    ///  Override Block Minimum
    /// </summary>
    public char OverrideBlockMin => Fields.OverrideBlockMin.Value;

    /// <summary>
    ///  Number of strategy leg definitions. The leg info
    /// </summary>
    public sbyte NumberOfExtraLegDefinitions => Fields.NumberOfExtraLegDefinitions.Value;

    /// <summary>
    ///  Numeric Number of cycles for a contract
    /// </summary>
    public int NumOfCycles => Fields.NumOfCycles.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public FuturesContractSymbol FuturesContractSymbol;
        public TradingStatus TradingStatus;
        public OrderPriceDenominator OrderPriceDenominator;
        public IncrementPrice IncrementPrice;
        public IncrementQty IncrementQty;
        public MinQty MinQty;
        public NumberOfLegDefinitions NumberOfLegDefinitions;
        public SecuritySubType SecuritySubType;
        public IsBlockOnly IsBlockOnly;
        public StrategySymbol StrategySymbol;
        public GtAllowed GtAllowed;
        public Reserved4 Reserved4;
        public MifidRegulatedMarket MifidRegulatedMarket;
        public MarketDesc MarketDesc;
        public MaturityYear MaturityYear;
        public MaturityMonth MaturityMonth;
        public MaturityDay MaturityDay;
        public DealPriceDenominator DealPriceDenominator;
        public UnitQuantity UnitQuantity;
        public NumDecimalsOptionsPrice NumDecimalsOptionsPrice;
        public AllowOptions AllowOptions;
        public ClearedAlias ClearedAlias;
        public AllowsImplied AllowsImplied;
        public MinPrice MinPrice;
        public MaxPrice MaxPrice;
        public ProductName ProductName;
        public HubAlias HubAlias;
        public StripName StripName;
        public IsTradable IsTradable;
        public SettlePriceDenominator SettlePriceDenominator;
        public MicCode MicCode;
        public UnitQtyDenominator UnitQtyDenominator;
        public HedgeOnly HedgeOnly;
        public ExchangeSilo ExchangeSilo;
        public OffExchangeIncrementQtyDenominator OffExchangeIncrementQtyDenominator;
        public OffExchangeIncrementQty OffExchangeIncrementQty;
        public OffExchangeIncrementPrice OffExchangeIncrementPrice;
        public OffExchangeIncrementOptionPrice OffExchangeIncrementOptionPrice;
        public ProductId ProductId;
        public HubId HubId;
        public StripId StripId;
        public UnderlyingIsin UnderlyingIsin;
        public TestMarketIndicator TestMarketIndicator;
        public LegDealSuppressed LegDealSuppressed;
        public OldNumOfCycles OldNumOfCycles;
        public MarketTypeId MarketTypeId;
        public OverrideBlockMin OverrideBlockMin;
        public NumberOfExtraLegDefinitions NumberOfExtraLegDefinitions;
        public NumOfCycles NumOfCycles;
    };

    protected Layout Fields;
};
