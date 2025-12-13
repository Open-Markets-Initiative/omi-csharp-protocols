using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Volume Group: Number of entries in Market Data message
/// </summary>

public partial class IncrementalRefreshVolumeGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntrySizeShort MdEntrySizeShort;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public MdUpdateAction MdUpdateAction;
        public Padding3 Padding3;
    };
};
