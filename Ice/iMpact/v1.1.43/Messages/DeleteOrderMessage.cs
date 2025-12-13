using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Delete Order Message: Upon receipt of this message, client should remove the order from its local book.
/// </summary>

public partial class DeleteOrderMessage
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
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

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
        public OrderId OrderId;
        public MessageDateTime MessageDateTime;
        public SequenceWithinMillis SequenceWithinMillis;
        public RequestTradingEngineReceivedTimestamp RequestTradingEngineReceivedTimestamp;
    };

    protected Layout Fields;
};
