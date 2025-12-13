using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Heartbeat
/// </summary>

public partial class Heartbeat
{
    /// <summary>
    ///  Last Msg Seq Num Processed
    /// </summary>
    public uint LastMsgSeqNumProcessed => Fields.LastMsgSeqNumProcessed.Value;

    /// <summary>
    ///  Pad 4
    /// </summary>
    public string Pad4 => Fields.Pad4.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LastMsgSeqNumProcessed LastMsgSeqNumProcessed;
        public Pad4 Pad4;
    };

    protected Layout Fields;
};
