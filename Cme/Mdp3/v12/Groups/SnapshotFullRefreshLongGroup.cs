using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Long Group: Number of entries in Market Data message
/// </summary>

public partial class SnapshotFullRefreshLongGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public MdEntrySizeOptional MdEntrySizeOptional;
        public NumberOfOrdersOptional NumberOfOrdersOptional;
        public MdPriceLevelUnsignedOptional MdPriceLevelUnsignedOptional;
        public OpenCloseSettlFlag OpenCloseSettlFlag;
        public MdEntryType MdEntryType;
    };
};
