using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Tcp Group: Number of data blocks listed in the Incremental message
/// </summary>

public partial class SnapshotFullRefreshTcpGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public MdEntrySizeShortOptional MdEntrySizeShortOptional;
        public TradeableSize TradeableSize;
        public NumberOfOrdersOptional NumberOfOrdersOptional;
        public MdPriceLevelOptional MdPriceLevelOptional;
        public OpenCloseSettlFlag OpenCloseSettlFlag;
        public MdEntryType MdEntryType;
        public TradingReferenceDate TradingReferenceDate;
        public SettlPriceType SettlPriceType;
    };
};
