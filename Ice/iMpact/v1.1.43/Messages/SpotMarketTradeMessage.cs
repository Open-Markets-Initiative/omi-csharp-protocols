using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Spot Market Trade Message: This message will be sent only upon trade in spot markets and it will be rendered on spot market channels.
/// </summary>

public partial class SpotMarketTradeMessage
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
    ///  Deal date time
    /// </summary>
    public DateTime TransactDateTime => Fields.TransactDateTime.Value;

    /// <summary>
    ///  For Future use
    /// </summary>
    public sbyte ExtraFlags => Fields.ExtraFlags.Value;

    /// <summary>
    ///  Delivery begin date time
    /// </summary>
    public DateTime DeliveryBeginDateTime => Fields.DeliveryBeginDateTime.Value;

    /// <summary>
    ///  Delivery end date time
    /// </summary>
    public DateTime DeliveryEndDateTime => Fields.DeliveryEndDateTime.Value;

    /// <summary>
    ///  Indicate if it is a system priced leg, ‘Y’ or ‘N’
    /// </summary>
    public IsSystemPricedLeg IsSystemPricedLeg => Fields.IsSystemPricedLeg.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public TradeId TradeId;
        public Price Price;
        public Quantity Quantity;
        public TransactDateTime TransactDateTime;
        public ExtraFlags ExtraFlags;
        public DeliveryBeginDateTime DeliveryBeginDateTime;
        public DeliveryEndDateTime DeliveryEndDateTime;
        public IsSystemPricedLeg IsSystemPricedLeg;
    };

    protected Layout Fields;
};
