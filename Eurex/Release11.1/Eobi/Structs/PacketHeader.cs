using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Packet Header: Eurex Eobi Packet Header
/// </summary>

public partial class PacketHeader
{
    /// <summary>
    ///  Packet Info
    /// </summary>
    public string PacketInfo => Fields.PacketInfo.Value;

    /// <summary>
    ///  Appl Seq Num
    /// </summary>
    public uint ApplSeqNum => Fields.ApplSeqNum.Value;

    /// <summary>
    ///  Market Segment Id
    /// </summary>
    public int MarketSegmentId => Fields.MarketSegmentId.Value;

    /// <summary>
    ///  Partition Id
    /// </summary>
    public byte PartitionId => Fields.PartitionId.Value;

    /// <summary>
    ///  Completion Indicator
    /// </summary>
    public CompletionIndicator CompletionIndicator => Fields.CompletionIndicator.Value;

    /// <summary>
    ///  Appl Seq Reset Indicator
    /// </summary>
    public ApplSeqResetIndicator ApplSeqResetIndicator => Fields.ApplSeqResetIndicator.Value;

    /// <summary>
    ///  Pad 5
    /// </summary>
    public string Pad5 => Fields.Pad5.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public PacketInfo PacketInfo;
        public ApplSeqNum ApplSeqNum;
        public MarketSegmentId MarketSegmentId;
        public PartitionId PartitionId;
        public CompletionIndicator CompletionIndicator;
        public ApplSeqResetIndicator ApplSeqResetIndicator;
        public Pad5 Pad5;
        public TransactTime TransactTime;
    };

    protected Layout Fields;
};
