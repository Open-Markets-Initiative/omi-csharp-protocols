using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Session Statistics Long Group: Number of entries in Market Data message
/// </summary>

public partial class IncrementalRefreshSessionStatisticsLongGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxEx MdEntryPxEx;
        public MdEntrySizeOptional MdEntrySizeOptional;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public OpenCloseSettlFlag OpenCloseSettlFlag;
        public MdUpdateAction MdUpdateAction;
        public MdEntryTypeStatistics MdEntryTypeStatistics;
        public Padding5 Padding5;
    };
};
