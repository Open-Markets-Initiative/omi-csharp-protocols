using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Group: Number of entries in Market Data message
/// </summary>

public partial class SnapshotFullRefreshGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public MdEntrySizeShortOptional MdEntrySizeShortOptional;
        public NumberOfOrdersOptional NumberOfOrdersOptional;
        public MdPriceLevelOptional MdPriceLevelOptional;
        public TradingReferenceDate TradingReferenceDate;
        public OpenCloseSettlFlag OpenCloseSettlFlag;
        public SettlPriceType SettlPriceType;
        public MdEntryType MdEntryType;
    };
};
