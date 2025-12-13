using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Option Settlement Price Message: Option settlement prices could be official or unofficial. For a given market, the exchange usually sends out unofficial price before the official one.
/// </summary>

public partial class OptionSettlementPriceMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get this price.
    /// </summary>
    public long SettlementPriceWithDealPricePrecision => Fields.SettlementPriceWithDealPricePrecision.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  Flag to indicate this is official settlement price or not
    /// </summary>
    public IsOfficial IsOfficial => Fields.IsOfficial.Value;

    /// <summary>
    ///  Date time the settlement price is for
    /// </summary>
    public DateTime ValuationDateTime => Fields.ValuationDateTime.Value;

    /// <summary>
    ///  Apply 2 as the denominator to get the real value. For example, volatility of 3.00 will be sent as 300.
    /// </summary>
    public long Volatility => Fields.Volatility.Value;

    /// <summary>
    ///  SettlePriceDenominator for the market should be applied to get the real settlement price.
    /// </summary>
    public long SettlementPrice => Fields.SettlementPrice.Value;

    /// <summary>
    ///  Apply 2 as the denominator to get the real value. For example, delta of 3.00 will be sent as 300.
    /// </summary>
    public long Delta => Fields.Delta.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public SettlementPriceWithDealPricePrecision SettlementPriceWithDealPricePrecision;
        public MessageDateTime MessageDateTime;
        public IsOfficial IsOfficial;
        public ValuationDateTime ValuationDateTime;
        public Volatility Volatility;
        public SettlementPrice SettlementPrice;
        public Delta Delta;
    };

    protected Layout Fields;
};
