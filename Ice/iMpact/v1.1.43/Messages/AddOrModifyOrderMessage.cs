using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Add Or Modify Order Message: Client should add the order to book if it is not there already. Otherwise, just overwrite the existing order.
/// </summary>

public partial class AddOrModifyOrderMessage
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
    ///  For Future use
    /// </summary>
    public sbyte ExtraFlags => Fields.ExtraFlags.Value;

    /// <summary>
    ///  Can be used in conjunction with TransactDateTime field for sequence of deals within same milliseconds time.
    /// </summary>
    public int SequenceWithinMillis => Fields.SequenceWithinMillis.Value;

    /// <summary>
    ///  This field can be used to get the order modification time
    /// </summary>
    public DateTime ModificationTimestamp => Fields.ModificationTimestamp.Value;

    /// <summary>
    ///  time the trading engine received the request that triggers this message
    /// </summary>
    public DateTime RequestTradingEngineReceivedTimestamp => Fields.RequestTradingEngineReceivedTimestamp.Value;

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
        public ExtraFlags ExtraFlags;
        public SequenceWithinMillis SequenceWithinMillis;
        public ModificationTimestamp ModificationTimestamp;
        public RequestTradingEngineReceivedTimestamp RequestTradingEngineReceivedTimestamp;
    };

    protected Layout Fields;
};
