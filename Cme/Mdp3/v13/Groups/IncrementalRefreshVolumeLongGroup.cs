using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Volume Long Group: Number of entries in Market Data message
/// </summary>

public partial class IncrementalRefreshVolumeLongGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntrySize MdEntrySize;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public MdUpdateAction MdUpdateAction;
        public Padding7 Padding7;
    };
};
