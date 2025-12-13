using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Book Order Id Group: Number of OrderID entries
/// </summary>

public partial class IncrementalRefreshBookOrderIdGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderId OrderId;
        public MdOrderPriorityOptional MdOrderPriorityOptional;
        public MdDisplayQtyOptional MdDisplayQtyOptional;
        public ReferenceId ReferenceId;
        public OrderUpdateAction OrderUpdateAction;
        public Padding2 Padding2;
    };
};
