using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Trade Summary Order Id Group: Number of OrderID entries
/// </summary>

public partial class IncrementalRefreshTradeSummaryOrderIdGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderId OrderId;
        public LastQty LastQty;
        public Padding4 Padding4;
    };
};
