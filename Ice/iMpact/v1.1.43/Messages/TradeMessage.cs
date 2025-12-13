using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Trade Message: A trade with IsSystemPricedLeg equal to ‘Y’ should not be used for the last price, High, Low and Open. Also please refer to Appendix B on how to handle market stats for given OffTradeMarketType. This message will not be sent for trades in Endex Spot markets.
/// </summary>

public partial class TradeMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Unique identifier of the trade message, unique per market.
    /// </summary>
    public long TradeId => Fields.TradeId.Value;

    /// <summary>
    ///  Indicate if it is a system priced leg, ‘Y’ or ‘N’
    /// </summary>
    public IsSystemPricedLeg IsSystemPricedLeg => Fields.IsSystemPricedLeg.Value;

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
    ///  System Priced Leg Type
    /// </summary>
    public SystemPricedLegType SystemPricedLegType => Fields.SystemPricedLegType.Value;

    /// <summary>
    ///  Indicate if the trade happens at market open due to spread implied. When set to ‘Y’, such deal should not be included in market stats.
    /// </summary>
    public char IsImpliedSpreadAtMarketOpen => Fields.IsImpliedSpreadAtMarketOpen.Value;

    /// <summary>
    ///  Indicate if the trade is an adjusted trade, ‘Y’ or ‘N’
    /// </summary>
    public char IsAdjustedTrade => Fields.IsAdjustedTrade.Value;

    /// <summary>
    ///  Aggressor Side
    /// </summary>
    public AggressorSide AggressorSide => Fields.AggressorSide.Value;

    /// <summary>
    ///  Trade Message Flags
    /// </summary>
    public string TradeFlags => Fields.TradeFlags.Value;

    /// <summary>
    ///  Only for off market trade. The first character is ‘ ‘ when it is a regular trade. One or two null characters (‘\0’) will be appended to the end of this field when applicable.
    /// </summary>
    public string OffMarketTradeType => Fields.OffMarketTradeType.Value;

    /// <summary>
    ///  Can be used in conjunction with TransactDateTime field for sequence of deals within same milliseconds time.
    /// </summary>
    public int SequenceWithinMillis => Fields.SequenceWithinMillis.Value;

    /// <summary>
    ///  time the trading engine received the request that triggers this message
    /// </summary>
    public DateTime RequestTradingEngineReceivedTimestamp => Fields.RequestTradingEngineReceivedTimestamp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public TradeId TradeId;
        public IsSystemPricedLeg IsSystemPricedLeg;
        public Price Price;
        public Quantity Quantity;
        public OldOffMarketTradeType OldOffMarketTradeType;
        public TransactDateTime TransactDateTime;
        public SystemPricedLegType SystemPricedLegType;
        public IsImpliedSpreadAtMarketOpen IsImpliedSpreadAtMarketOpen;
        public IsAdjustedTrade IsAdjustedTrade;
        public AggressorSide AggressorSide;
        public TradeFlags TradeFlags;
        public OffMarketTradeType OffMarketTradeType;
        public SequenceWithinMillis SequenceWithinMillis;
        public RequestTradingEngineReceivedTimestamp RequestTradingEngineReceivedTimestamp;
    };

    protected Layout Fields;
};
