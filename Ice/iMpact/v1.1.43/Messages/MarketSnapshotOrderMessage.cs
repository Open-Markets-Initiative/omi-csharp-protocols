using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Market Snapshot Order Message: This message is for orders in snapshot only. It is different from the order message for incremental updates.
/// </summary>

public partial class MarketSnapshotOrderMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Unique per market
    /// </summary>
    public long OrderId => Fields.OrderId.Value;

    /// <summary>
    ///  Sequence ID of the order. When an order is modified, this will be incremented while OrderID remains the same. It is for legacy reason and can be ignored.
    /// </summary>
    public short OrderSequenceId => Fields.OrderSequenceId.Value;

    /// <summary>
    ///  1 = Bid, 2 = Offer
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Price => Fields.Price.Value;

    /// <summary>
    ///  Quantity
    /// </summary>
    public int Quantity => Fields.Quantity.Value;

    /// <summary>
    ///  Indicate if this is an implied order or not
    /// </summary>
    public IsImplied IsImplied => Fields.IsImplied.Value;

    /// <summary>
    ///  Indicate whether it is just an RFQ or not. Client should filter the order if it doesn’t care about RFQ.
    /// </summary>
    public IsRfq IsRfq => Fields.IsRfq.Value;

    /// <summary>
    ///  Order entry date time
    /// </summary>
    public DateTime OrderEntryDateTime => Fields.OrderEntryDateTime.Value;

    /// <summary>
    ///  Can be used in conjunction with TransactDateTime field for sequence of deals within same milliseconds time.
    /// </summary>
    public int SequenceWithinMillis => Fields.SequenceWithinMillis.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public OrderId OrderId;
        public OrderSequenceId OrderSequenceId;
        public Side Side;
        public Price Price;
        public Quantity Quantity;
        public IsImplied IsImplied;
        public IsRfq IsRfq;
        public OrderEntryDateTime OrderEntryDateTime;
        public SequenceWithinMillis SequenceWithinMillis;
    };

    protected Layout Fields;
};
