using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Rfq Message: Request for Quote Message.
/// </summary>

public partial class RfqMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Date time of the RFQ
    /// </summary>
    public DateTime MessageTimestamp => Fields.MessageTimestamp.Value;

    /// <summary>
    ///  System ID of the RFQ
    /// </summary>
    public long RfqSystemId => Fields.RfqSystemId.Value;

    /// <summary>
    ///  Number of cycles for a contract.
    /// </summary>
    public MarketTypeId MarketTypeId => Fields.MarketTypeId.Value;

    /// <summary>
    ///  Unique identifier of the underlying market
    /// </summary>
    public int UnderlyingMarketId => Fields.UnderlyingMarketId.Value;

    /// <summary>
    ///  Quantity
    /// </summary>
    public int Quantity => Fields.Quantity.Value;

    /// <summary>
    ///  1 = Bid, 2 = Offer
    /// </summary>
    public Side Side => Fields.Side.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public MessageTimestamp MessageTimestamp;
        public RfqSystemId RfqSystemId;
        public MarketTypeId MarketTypeId;
        public UnderlyingMarketId UnderlyingMarketId;
        public Quantity Quantity;
        public Side Side;
    };

    protected Layout Fields;
};
