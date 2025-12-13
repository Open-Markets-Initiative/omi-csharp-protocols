using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Settlement Price Message: Settlement prices could be official or unofficial. For a given market, the exchange usually sends out unofficial price before the official one.
/// </summary>

public partial class SettlementPriceMessage
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
    ///  SettlePriceDenominator for the market should be applied to get the real settlement price.
    /// </summary>
    public long SettlementPrice => Fields.SettlementPrice.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public SettlementPriceWithDealPricePrecision SettlementPriceWithDealPricePrecision;
        public MessageDateTime MessageDateTime;
        public IsOfficial IsOfficial;
        public ValuationDateTime ValuationDateTime;
        public SettlementPrice SettlementPrice;
    };

    protected Layout Fields;
};
