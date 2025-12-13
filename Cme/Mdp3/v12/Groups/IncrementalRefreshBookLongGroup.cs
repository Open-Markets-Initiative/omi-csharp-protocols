using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Book Long Group: Number of entries in Market Data message.
/// </summary>

public partial class IncrementalRefreshBookLongGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public MdEntrySizeOptional MdEntrySizeOptional;
        public SecurityId SecurityId;
        public RptSeq RptSeq;
        public NumberOfOrdersOptional NumberOfOrdersOptional;
        public MdPriceLevel MdPriceLevel;
        public MdUpdateAction MdUpdateAction;
        public MdEntryTypeBook MdEntryTypeBook;
        public Padding1 Padding1;
    };
};
