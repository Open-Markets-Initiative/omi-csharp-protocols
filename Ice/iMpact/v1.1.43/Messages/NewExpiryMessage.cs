using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  New Expiry Message: New Expiry message will be sent out when a new equity market is created during the day.
/// </summary>

public partial class NewExpiryMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Number of cycles for a contract.
    /// </summary>
    public MarketTypeId MarketTypeId => Fields.MarketTypeId.Value;

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
    ///  The lot size is minimum size of contracts in lots. It is multiplier to determine the total lots.
    /// </summary>
    public int LotSize => Fields.LotSize.Value;

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
    ///  Minimum quantity for this market
    /// </summary>
    public int MinQty => Fields.MinQty.Value;

    /// <summary>
    ///  The quantity in unit of measurement per lot. For example, it is 1000 barrels per lot for Brent.
    /// </summary>
    public int UnitQuantity => Fields.UnitQuantity.Value;

    /// <summary>
    ///  The currency that the market is traded on.
    /// </summary>
    public string Currency => Fields.Currency.Value;

    /// <summary>
    ///  Clearing limit admin related
    /// </summary>
    public string ClearedAlias => Fields.ClearedAlias.Value;

    /// <summary>
    ///  Minimum Price, OrderPriceDenominator should be applied to this field
    /// </summary>
    public long MinPrice => Fields.MinPrice.Value;

    /// <summary>
    ///  Maximum Price, OrderPriceDenominator should be applied to this field
    /// </summary>
    public long MaxPrice => Fields.MaxPrice.Value;

    /// <summary>
    ///  ID of the product that the contract/market is under.
    /// </summary>
    public int ProductId => Fields.ProductId.Value;

    /// <summary>
    ///  Name of the product that the contract/market is under
    /// </summary>
    public string ProductName => Fields.ProductName.Value;

    /// <summary>
    ///  ID of the hub for the contract/market
    /// </summary>
    public int HubId => Fields.HubId.Value;

    /// <summary>
    ///  Alias of the hub for the contract/market
    /// </summary>
    public string HubAlias => Fields.HubAlias.Value;

    /// <summary>
    ///  StripID
    /// </summary>
    public int StripId => Fields.StripId.Value;

    /// <summary>
    ///  Name of the strip for the contract/market
    /// </summary>
    public string StripName => Fields.StripName.Value;

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
    ///  Contract Symbol
    /// </summary>
    public string ContractSymbol => Fields.ContractSymbol.Value;

    /// <summary>
    ///  The ISIN of the security this market is associated with. This is currently only populated for Liffe Equity markets.
    /// </summary>
    public string Isin => Fields.Isin.Value;

    /// <summary>
    ///  Only used for OffExchangeIncrementOptionPrice.
    /// </summary>
    public char NumDecimalsOptionsPrice => Fields.NumDecimalsOptionsPrice.Value;

    /// <summary>
    ///  Market ID for the corresponding hedge market.
    /// </summary>
    public int HedgeMarketId => Fields.HedgeMarketId.Value;

    /// <summary>
    ///  Settlement Type: ‘0’ - financial, ‘1’ - physical
    /// </summary>
    public SettlementType SettlementType => Fields.SettlementType.Value;

    /// <summary>
    ///  Indicates if GTC is allowed in the market. ‘Y’ or ‘N’
    /// </summary>
    public GtAllowed GtAllowed => Fields.GtAllowed.Value;

    /// <summary>
    ///  Indicates if Cross Order is supported in the market. ‘Y’ or ‘N’
    /// </summary>
    public CrossOrderSupported CrossOrderSupported => Fields.CrossOrderSupported.Value;

    /// <summary>
    ///  Unit Of Measure
    /// </summary>
    public string UnitOfMeasure => Fields.UnitOfMeasure.Value;

    /// <summary>
    ///  Indicates MIFID-II market. ‘Y’ or ‘N’
    /// </summary>
    public MifidRegulatedMarket MifidRegulatedMarket => Fields.MifidRegulatedMarket.Value;

    /// <summary>
    ///  Screen last trade year, 4 digits
    /// </summary>
    public short ScreenLastTradeYear => Fields.ScreenLastTradeYear.Value;

    /// <summary>
    ///  Screen last trade month, range 1-12
    /// </summary>
    public short ScreenLastTradeMonth => Fields.ScreenLastTradeMonth.Value;

    /// <summary>
    ///  Screen last trade day of the month
    /// </summary>
    public short ScreenLastTradeDay => Fields.ScreenLastTradeDay.Value;

    /// <summary>
    ///  Number of cycles for a contract.
    /// </summary>
    public short OldNumOfCycles => Fields.OldNumOfCycles.Value;

    /// <summary>
    ///  Numeric Number of cycles for a contract
    /// </summary>
    public int NumOfCycles => Fields.NumOfCycles.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public MarketTypeId MarketTypeId;
        public OrderPriceDenominator OrderPriceDenominator;
        public IncrementPrice IncrementPrice;
        public IncrementQty IncrementQty;
        public LotSize LotSize;
        public MarketDesc MarketDesc;
        public MaturityYear MaturityYear;
        public MaturityMonth MaturityMonth;
        public MaturityDay MaturityDay;
        public DealPriceDenominator DealPriceDenominator;
        public MinQty MinQty;
        public UnitQuantity UnitQuantity;
        public Currency Currency;
        public ClearedAlias ClearedAlias;
        public MinPrice MinPrice;
        public MaxPrice MaxPrice;
        public ProductId ProductId;
        public ProductName ProductName;
        public HubId HubId;
        public HubAlias HubAlias;
        public StripId StripId;
        public StripName StripName;
        public SettlePriceDenominator SettlePriceDenominator;
        public MicCode MicCode;
        public UnitQtyDenominator UnitQtyDenominator;
        public OffExchangeIncrementQtyDenominator OffExchangeIncrementQtyDenominator;
        public OffExchangeIncrementQty OffExchangeIncrementQty;
        public OffExchangeIncrementPrice OffExchangeIncrementPrice;
        public OffExchangeIncrementOptionPrice OffExchangeIncrementOptionPrice;
        public ContractSymbol ContractSymbol;
        public Isin Isin;
        public NumDecimalsOptionsPrice NumDecimalsOptionsPrice;
        public HedgeMarketId HedgeMarketId;
        public SettlementType SettlementType;
        public GtAllowed GtAllowed;
        public CrossOrderSupported CrossOrderSupported;
        public UnitOfMeasure UnitOfMeasure;
        public MifidRegulatedMarket MifidRegulatedMarket;
        public ScreenLastTradeYear ScreenLastTradeYear;
        public ScreenLastTradeMonth ScreenLastTradeMonth;
        public ScreenLastTradeDay ScreenLastTradeDay;
        public OldNumOfCycles OldNumOfCycles;
        public NumOfCycles NumOfCycles;
    };

    protected Layout Fields;
};
