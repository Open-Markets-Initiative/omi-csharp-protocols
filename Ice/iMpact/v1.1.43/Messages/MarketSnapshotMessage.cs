using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Market Snapshot Message: The market snapshot message is the same for full order depth and price level snapshot channel.
/// </summary>

public partial class MarketSnapshotMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Market Type
    /// </summary>
    public short MarketType => Fields.MarketType.Value;

    /// <summary>
    ///  See Appendix A on the trading status codes
    /// </summary>
    public TradingStatus TradingStatus => Fields.TradingStatus.Value;

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
    ///  Open Interest
    /// </summary>
    public int OpenInterest => Fields.OpenInterest.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long OpeningPrice => Fields.OpeningPrice.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get this price.
    /// </summary>
    public long SettlementPriceWithDealPricePrecision => Fields.SettlementPriceWithDealPricePrecision.Value;

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
    ///  Number of book entries in the market. It is the number of order messages followed for full order depth snapshot channel. In case of price level snapshot, it is the number of price level messages that followed for the market.
    /// </summary>
    public int NumOfBookEntries => Fields.NumOfBookEntries.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long LastTradePrice => Fields.LastTradePrice.Value;

    /// <summary>
    ///  Last Trade Quantity
    /// </summary>
    public int LastTradeQuantity => Fields.LastTradeQuantity.Value;

    /// <summary>
    ///  time of last trade
    /// </summary>
    public DateTime LastTradeDateTime => Fields.LastTradeDateTime.Value;

    /// <summary>
    ///  Settlement price time
    /// </summary>
    public DateTime SettlePriceDateTime => Fields.SettlePriceDateTime.Value;

    /// <summary>
    ///  This should be used for synchronization with live update messages. Please see the main tech spec for details on how it can be done.
    /// </summary>
    public int LastMessageSequenceId => Fields.LastMessageSequenceId.Value;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public string Reserved2 => Fields.Reserved2.Value;

    /// <summary>
    ///  The date Open Interest is effective for
    /// </summary>
    public string OpenInterestDate => Fields.OpenInterestDate.Value;

    /// <summary>
    ///  Indicate if the SettlementPrice is official, ‘Y’ or ‘N’.
    /// </summary>
    public IsSettlePriceOfficial IsSettlePriceOfficial => Fields.IsSettlePriceOfficial.Value;

    /// <summary>
    ///  SettlePriceDenominator for the market should be applied to get the real settlement price.
    /// </summary>
    public long SettlementPrice => Fields.SettlementPrice.Value;

    /// <summary>
    ///  Indicate if the PreviousSettlementDayPrice is populated, ‘Y’ or ‘N’.
    /// </summary>
    public HasPreviousDaySettlementPrice HasPreviousDaySettlementPrice => Fields.HasPreviousDaySettlementPrice.Value;

    /// <summary>
    ///  SettlePriceDenominator for the market should be applied to get the real previous day settlement price.
    /// </summary>
    public long PreviousDaySettlementPrice => Fields.PreviousDaySettlementPrice.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public MarketType MarketType;
        public TradingStatus TradingStatus;
        public Volume Volume;
        public BlockVolume BlockVolume;
        public EfsVolume EfsVolume;
        public EfpVolume EfpVolume;
        public OpenInterest OpenInterest;
        public OpeningPrice OpeningPrice;
        public SettlementPriceWithDealPricePrecision SettlementPriceWithDealPricePrecision;
        public High High;
        public Low Low;
        public Vwap Vwap;
        public NumOfBookEntries NumOfBookEntries;
        public LastTradePrice LastTradePrice;
        public LastTradeQuantity LastTradeQuantity;
        public LastTradeDateTime LastTradeDateTime;
        public SettlePriceDateTime SettlePriceDateTime;
        public LastMessageSequenceId LastMessageSequenceId;
        public Reserved2 Reserved2;
        public OpenInterestDate OpenInterestDate;
        public IsSettlePriceOfficial IsSettlePriceOfficial;
        public SettlementPrice SettlementPrice;
        public HasPreviousDaySettlementPrice HasPreviousDaySettlementPrice;
        public PreviousDaySettlementPrice PreviousDaySettlementPrice;
    };

    protected Layout Fields;
};
