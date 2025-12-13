using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Fixing Transition Message: This message supports ICE Benchmark Administration’s (IBA) electronic Gold Auction and it is disseminated when there is a transition of the fixing market.
/// </summary>

public partial class FixingTransitionMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  C - Closed, P - Preopen, L - Lockdown
    /// </summary>
    public FixingStatus FixingStatus => Fields.FixingStatus.Value;

    /// <summary>
    ///  Date time the Auction will end
    /// </summary>
    public DateTime AuctionEndTime => Fields.AuctionEndTime.Value;

    /// <summary>
    ///  Threshold Imbalance Qty
    /// </summary>
    public int ThresholdImbalanceQty => Fields.ThresholdImbalanceQty.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public FixingStatus FixingStatus;
        public AuctionEndTime AuctionEndTime;
        public ThresholdImbalanceQty ThresholdImbalanceQty;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
