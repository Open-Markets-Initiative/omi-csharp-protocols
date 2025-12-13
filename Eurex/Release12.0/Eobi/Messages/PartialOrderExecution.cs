using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Partial Order Execution
/// </summary>

public partial class PartialOrderExecution
{
    /// <summary>
    ///  Side
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  Ord Type
    /// </summary>
    public OrdType OrdType => Fields.OrdType.Value;

    /// <summary>
    ///  Algorithmic Trade Indicator
    /// </summary>
    public AlgorithmicTradeIndicator AlgorithmicTradeIndicator => Fields.AlgorithmicTradeIndicator.Value;

    /// <summary>
    ///  Hhi Indicator
    /// </summary>
    public HhiIndicator HhiIndicator => Fields.HhiIndicator.Value;

    /// <summary>
    ///  Trd Match Id
    /// </summary>
    public uint TrdMatchId => Fields.TrdMatchId.Value;

    /// <summary>
    ///  Price
    /// </summary>
    public ulong Price => Fields.Price.Value;

    /// <summary>
    ///  Trd Reg Ts Time Priority
    /// </summary>
    public DateTime TrdRegTsTimePriority => Fields.TrdRegTsTimePriority.Value;

    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Last Px
    /// </summary>
    public ulong LastPx => Fields.LastPx.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Side Side;
        public OrdType OrdType;
        public AlgorithmicTradeIndicator AlgorithmicTradeIndicator;
        public HhiIndicator HhiIndicator;
        public TrdMatchId TrdMatchId;
        public Price Price;
        public TrdRegTsTimePriority TrdRegTsTimePriority;
        public SecurityId SecurityId;
        public LastQty LastQty;
        public LastPx LastPx;
    };

    protected Layout Fields;
};
