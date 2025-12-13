using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Order Book Group: Number of entries in Market Data message
/// </summary>

public partial class SnapshotFullRefreshOrderBookGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderId OrderId;
        public MdOrderPriorityOptional MdOrderPriorityOptional;
        public MdEntryPxEx MdEntryPxEx;
        public MdDisplayQty MdDisplayQty;
        public MdEntryTypeBook MdEntryTypeBook;
    };
};
