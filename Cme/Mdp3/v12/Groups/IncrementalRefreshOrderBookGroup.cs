using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Order Book Group: Number of entries in Market Data message
/// </summary>

public partial class IncrementalRefreshOrderBookGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderIdOptional OrderIdOptional;
        public MdOrderPriorityOptional MdOrderPriorityOptional;
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public MdDisplayQtyOptional MdDisplayQtyOptional;
        public SecurityId SecurityId;
        public MdUpdateAction MdUpdateAction;
        public MdEntryTypeBook MdEntryTypeBook;
        public Padding6 Padding6;
    };
};
