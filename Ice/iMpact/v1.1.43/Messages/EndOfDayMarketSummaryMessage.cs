using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  End Of Day Market Summary Message: The message is streamed to client when market is closed and settlement price is available for the current trading day.
/// </summary>

public partial class EndOfDayMarketSummaryMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Electronic trade volume only, excluding block and other volumes.
    /// </summary>
    public int Volume => Fields.Volume.Value;

    /// <summary>
    ///  Block Volume
    /// </summary>
    public int BlockVolume => Fields.BlockVolume.Value;

    /// <summary>
    ///  Efs Volume
    /// </summary>
    public int EfsVolume => Fields.EfsVolume.Value;

    /// <summary>
    ///  Efp Volume
    /// </summary>
    public int EfpVolume => Fields.EfpVolume.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long OpeningPrice => Fields.OpeningPrice.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long High => Fields.High.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Low => Fields.Low.Value;

    /// <summary>
    ///  Weighted Average Price. DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Vwap => Fields.Vwap.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get this price.
    /// </summary>
    public long SettlementPriceWithDealPricePrecision => Fields.SettlementPriceWithDealPricePrecision.Value;

    /// <summary>
    ///  Open Interest
    /// </summary>
    public int OpenInterest => Fields.OpenInterest.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  SettlePriceDenominator for the market should be applied to get the real settlement price.
    /// </summary>
    public long SettlementPrice => Fields.SettlementPrice.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public Volume Volume;
        public BlockVolume BlockVolume;
        public EfsVolume EfsVolume;
        public EfpVolume EfpVolume;
        public OpeningPrice OpeningPrice;
        public High High;
        public Low Low;
        public Vwap Vwap;
        public SettlementPriceWithDealPricePrecision SettlementPriceWithDealPricePrecision;
        public OpenInterest OpenInterest;
        public MessageDateTime MessageDateTime;
        public SettlementPrice SettlementPrice;
    };

    protected Layout Fields;
};
