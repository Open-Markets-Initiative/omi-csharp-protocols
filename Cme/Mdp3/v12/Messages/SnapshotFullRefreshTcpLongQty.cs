using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Snapshot Full Refresh Tcp Long Qty: SnapshotFullRefreshTCPLongQty
/// </summary>

public partial class SnapshotFullRefreshTcpLongQty
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  Security ID
    /// </summary>
    public int SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Upper price threshold for the instrument
    /// </summary>
    public long HighLimitPrice => Fields.HighLimitPrice.Value;

    /// <summary>
    ///  Lower price threshold for the instrument
    /// </summary>
    public long LowLimitPrice => Fields.LowLimitPrice.Value;

    /// <summary>
    ///  Differential value for price banding
    /// </summary>
    public long MaxPriceVariation => Fields.MaxPriceVariation.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public MatchEventIndicator MatchEventIndicator;
        public SecurityId SecurityId;
        public HighLimitPrice HighLimitPrice;
        public LowLimitPrice LowLimitPrice;
        public MaxPriceVariation MaxPriceVariation;
    };

    protected Layout Fields;
};
