using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Execution Summary
/// </summary>

public partial class ExecutionSummary
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Request Time
    /// </summary>
    public DateTime RequestTime => Fields.RequestTime.Value;

    /// <summary>
    ///  Exec Id
    /// </summary>
    public DateTime ExecId => Fields.ExecId.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Aggressor Side
    /// </summary>
    public AggressorSide AggressorSide => Fields.AggressorSide.Value;

    /// <summary>
    ///  Pad 1
    /// </summary>
    public string Pad1 => Fields.Pad1.Value;

    /// <summary>
    ///  Trade Condition
    /// </summary>
    public TradeCondition TradeCondition => Fields.TradeCondition.Value;

    /// <summary>
    ///  Trading Hhi Indicator
    /// </summary>
    public byte TradingHhiIndicator => Fields.TradingHhiIndicator.Value;

    /// <summary>
    ///  Pad 3
    /// </summary>
    public string Pad3 => Fields.Pad3.Value;

    /// <summary>
    ///  Last Px
    /// </summary>
    public ulong LastPx => Fields.LastPx.Value;

    /// <summary>
    ///  Remaining Order Details Comp
    /// </summary>
    public string RemainingOrderDetailsComp => Fields.RemainingOrderDetailsComp.Value;

    /// <summary>
    ///  Resting Hidden Qty
    /// </summary>
    public ulong RestingHiddenQty => Fields.RestingHiddenQty.Value;

    /// <summary>
    ///  Resting Cxl Qty
    /// </summary>
    public ulong RestingCxlQty => Fields.RestingCxlQty.Value;

    /// <summary>
    ///  Aggressor Time
    /// </summary>
    public DateTime AggressorTime => Fields.AggressorTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public RequestTime RequestTime;
        public ExecId ExecId;
        public LastQty LastQty;
        public AggressorSide AggressorSide;
        public Pad1 Pad1;
        public TradeCondition TradeCondition;
        public TradingHhiIndicator TradingHhiIndicator;
        public Pad3 Pad3;
        public LastPx LastPx;
        public RemainingOrderDetailsComp RemainingOrderDetailsComp;
        public RestingHiddenQty RestingHiddenQty;
        public RestingCxlQty RestingCxlQty;
        public AggressorTime AggressorTime;
    };

    protected Layout Fields;
};
