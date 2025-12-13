using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Trade Summary Long Order Id Group: Number of OrderID and LastQty entries in Trade Summary message
/// </summary>

public partial class IncrementalRefreshTradeSummaryLongOrderIdGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderId OrderId;
        public LastQty LastQty;
        public Padding4 Padding4;
    };
};
