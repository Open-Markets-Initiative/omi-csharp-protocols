using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Order Book: SnapshotFullRefreshOrderBook
/// </summary>

public partial class SnapshotFullRefreshOrderBook
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
    ///  Total number of packets that constitutes a single instrument order book
    /// </summary>
    public uint NoChunks => Fields.NoChunks.Value;

    /// <summary>
    ///  Chunk sequence
    /// </summary>
    public uint CurrentChunk => Fields.CurrentChunk.Value;

    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LastMsgSeqNumProcessed LastMsgSeqNumProcessed;
        public TotNumReports TotNumReports;
        public SecurityId SecurityId;
        public NoChunks NoChunks;
        public CurrentChunk CurrentChunk;
        public TransactTime TransactTime;
    };

    protected Layout Fields;
};
