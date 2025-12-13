using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Trade Summary Long Group: Number of Trade Summary entries
/// </summary>

public partial class IncrementalRefreshTradeSummaryLongGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxEx MdEntryPxEx;
        public MdEntrySize MdEntrySize;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public NumberOfOrders NumberOfOrders;
        public MdTradeEntry MdTradeEntry;
        public AggressorSide AggressorSide;
        public MdUpdateAction MdUpdateAction;
        public Padding6 Padding6;
    };
};
