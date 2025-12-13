using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Session Statistics Group: Number of entries in Market Data message
/// </summary>

public partial class IncrementalRefreshSessionStatisticsGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxEx MdEntryPxEx;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public OpenCloseSettlFlag OpenCloseSettlFlag;
        public MdUpdateAction MdUpdateAction;
        public MdEntryTypeStatistics MdEntryTypeStatistics;
        public MdEntrySizeShortOptional MdEntrySizeShortOptional;
        public Padding1 Padding1;
    };
};
