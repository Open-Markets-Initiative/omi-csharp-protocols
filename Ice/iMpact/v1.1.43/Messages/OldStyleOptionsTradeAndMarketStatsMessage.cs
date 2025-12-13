using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Old Style Options Trade And Market Stats Message: Option settlement prices could be official or unofficial. For a given market, the exchange usually sends out unofficial price before the official one.
/// </summary>

public partial class OldStyleOptionsTradeAndMarketStatsMessage
{
    /// <summary>
    ///  Unique identifier of the underlying market
    /// </summary>
    public int UnderlyingMarketId => Fields.UnderlyingMarketId.Value;

    /// <summary>
    ///  Unique identifier of the trade message, unique per market.
    /// </summary>
    public long TradeId => Fields.TradeId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Price => Fields.Price.Value;

    /// <summary>
    ///  Quantity
    /// </summary>
    public int Quantity => Fields.Quantity.Value;

    /// <summary>
    ///  Legacy field that supports all single character trade types on ICE. The new 3-character “OffMarketTradeType” field replaces this field. In the future (no earlier than 2015), ICE anticipates the introduction of 3 character trade types that will only be available in the new field. Trade types that are longer than a single character will be represented with “#” in this field.
    /// </summary>
    public char OldOffMarketTradeType => Fields.OldOffMarketTradeType.Value;

    /// <summary>
    ///  Deal date time
    /// </summary>
    public DateTime TransactDateTime => Fields.TransactDateTime.Value;

    /// <summary>
    ///  “C” – Call, “P” – Put
    /// </summary>
    public OptionType OptionType => Fields.OptionType.Value;

    /// <summary>
    ///  Strike Price of the option. Used in conjunction with the NumDecimalsStrikePrice. This is often different from the premium price decimals.
    /// </summary>
    public long StrikePrice => Fields.StrikePrice.Value;

    /// <summary>
    ///  Event code: ‘0’ – Normal trade, ‘1’ – Cancelled trade, ‘2’ – Adjusted trade
    /// </summary>
    public EventCode EventCode => Fields.EventCode.Value;

    /// <summary>
    ///  N/A if set to -1
    /// </summary>
    public int TotalVolume => Fields.TotalVolume.Value;

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
    public long High => Fields.High.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Low => Fields.Low.Value;

    /// <summary>
    ///  Weighted Average Price. DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Vwap => Fields.Vwap.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public UnderlyingMarketId UnderlyingMarketId;
        public TradeId TradeId;
        public Price Price;
        public Quantity Quantity;
        public OldOffMarketTradeType OldOffMarketTradeType;
        public TransactDateTime TransactDateTime;
        public OptionType OptionType;
        public StrikePrice StrikePrice;
        public EventCode EventCode;
        public TotalVolume TotalVolume;
        public BlockVolume BlockVolume;
        public EfsVolume EfsVolume;
        public EfpVolume EfpVolume;
        public High High;
        public Low Low;
        public Vwap Vwap;
    };

    protected Layout Fields;
};
