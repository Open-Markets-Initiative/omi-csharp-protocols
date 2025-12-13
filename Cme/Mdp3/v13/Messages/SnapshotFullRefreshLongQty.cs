using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Long Qty: SnapshotFullRefreshLongQty
/// </summary>

public partial class SnapshotFullRefreshLongQty
{
    /// <summary>
    ///  Sequence number of the last Incremental feed packet processed. This value is used to synchronize the snapshot loop with the real-time feed
    /// </summary>
    public uint LastMsgSeqNumProcessed => Fields.LastMsgSeqNumProcessed.Value;

    /// <summary>
    ///  Total number of messages replayed in the loop
    /// </summary>
    public uint TotNumReports => Fields.TotNumReports.Value;

    /// <summary>
    ///  Security ID
    /// </summary>
    public int SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Sequence number of the last Market Data entry processed for the instrument
    /// </summary>
    public uint RptSeq => Fields.RptSeq.Value;

    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  UTC Date and time of last Security Definition add, update or delete on a given Market Data channel
    /// </summary>
    public DateTime LastUpdateTime => Fields.LastUpdateTime.Value;

    /// <summary>
    ///  Trade Session Date
    /// </summary>
    public ushort TradeDate => Fields.TradeDate.Value;

    /// <summary>
    ///  Identifies the current trading state of the instrument
    /// </summary>
    public MdSecurityTradingStatus MdSecurityTradingStatus => Fields.MdSecurityTradingStatus.Value;

    /// <summary>
    ///  Upper price threshold for the instrument
    /// </summary>
    public long HighLimitPrice => Fields.HighLimitPrice.Value;

    /// <summary>
    ///  Lower price threshold for the instrument
    /// </summary>
    public long LowLimitPrice => Fields.LowLimitPrice.Value;

    /// <summary>
    ///  Differential value for price banding
    /// </summary>
    public long MaxPriceVariation => Fields.MaxPriceVariation.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LastMsgSeqNumProcessed LastMsgSeqNumProcessed;
        public TotNumReports TotNumReports;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public TransactTime TransactTime;
        public LastUpdateTime LastUpdateTime;
        public TradeDate TradeDate;
        public MdSecurityTradingStatus MdSecurityTradingStatus;
        public HighLimitPrice HighLimitPrice;
        public LowLimitPrice LowLimitPrice;
        public MaxPriceVariation MaxPriceVariation;
    };

    protected Layout Fields;
};
