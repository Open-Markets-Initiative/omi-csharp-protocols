using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Change Price Level Message: Upon receipt of this message, client should update the price level at the specified position in its book for the given market.
/// </summary>

public partial class ChangePriceLevelMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  1 = Bid, 2 = Offer
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  Position of the price level
    /// </summary>
    public sbyte PriceLevelPosition => Fields.PriceLevelPosition.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Price => Fields.Price.Value;

    /// <summary>
    ///  Quantity
    /// </summary>
    public int Quantity => Fields.Quantity.Value;

    /// <summary>
    ///  Number of all orders at the price level
    /// </summary>
    public short OrderCount => Fields.OrderCount.Value;

    /// <summary>
    ///  Quantity that were implied
    /// </summary>
    public int ImpliedQuantity => Fields.ImpliedQuantity.Value;

    /// <summary>
    ///  Number of implied orders at the price level
    /// </summary>
    public short ImpliedOrderCount => Fields.ImpliedOrderCount.Value;

    /// <summary>
    ///  Timestamp of last update used to derive the price level message
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public Side Side;
        public PriceLevelPosition PriceLevelPosition;
        public Price Price;
        public Quantity Quantity;
        public OrderCount OrderCount;
        public ImpliedQuantity ImpliedQuantity;
        public ImpliedOrderCount ImpliedOrderCount;
        public Timestamp Timestamp;
    };

    protected Layout Fields;
};
