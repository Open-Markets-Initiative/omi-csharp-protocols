using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Trade Summary Group: Number of Trade Summary entries
/// </summary>

public partial class IncrementalRefreshTradeSummaryGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxEx MdEntryPxEx;
        public MdEntrySizeShort MdEntrySizeShort;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public NumberOfOrders NumberOfOrders;
        public AggressorSide AggressorSide;
        public MdUpdateAction MdUpdateAction;
        public MdTradeEntryOptional MdTradeEntryOptional;
        public Padding2 Padding2;
    };
};
