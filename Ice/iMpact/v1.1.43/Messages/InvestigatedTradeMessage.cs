using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Investigated Trade Message: This message is sent when a trade is put under investigation or the investigation is completed. Client can use the market ID and order ID to find and flag the original trade if needed.
/// </summary>

public partial class InvestigatedTradeMessage
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
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Price => Fields.Price.Value;

    /// <summary>
    ///  Quantity
    /// </summary>
    public int Quantity => Fields.Quantity.Value;

    /// <summary>
    ///  Legacy field that supports all single character trade types on ICE. The new 3-character “OffMarketTradeType” field replaces this field. In the future (no earlier than 2015), ICE anticipates the introduction of 3 character trade types that will only be available in the new field. Trade types that are longer than a single character will be represented with “#” in this field.  Only for off market trade. The first character is ‘ ‘ when it is a regular trade.
    /// </summary>
    public char OffMarketTradeIndicator => Fields.OffMarketTradeIndicator.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  ‘1’ – Under Investigation, ‘2’ – Investigation Completed
    /// </summary>
    public InvestigationStatus InvestigationStatus => Fields.InvestigationStatus.Value;

    /// <summary>
    ///  Only for off market trade. The first character is ‘ ‘ when it is a regular trade. One or two null characters (‘\0’) will be appended to the end of this field when applicable.
    /// </summary>
    public string OffMarketTradeType => Fields.OffMarketTradeType.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public TradeId TradeId;
        public Price Price;
        public Quantity Quantity;
        public OffMarketTradeIndicator OffMarketTradeIndicator;
        public MessageDateTime MessageDateTime;
        public InvestigationStatus InvestigationStatus;
        public OffMarketTradeType OffMarketTradeType;
    };

    protected Layout Fields;
};
