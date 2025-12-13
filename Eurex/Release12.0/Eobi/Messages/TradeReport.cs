using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Trade Report
/// </summary>

public partial class TradeReport
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Last Px
    /// </summary>
    public ulong LastPx => Fields.LastPx.Value;

    /// <summary>
    ///  Trd Match Id
    /// </summary>
    public uint TrdMatchId => Fields.TrdMatchId.Value;

    /// <summary>
    ///  Match Type
    /// </summary>
    public MatchType MatchType => Fields.MatchType.Value;

    /// <summary>
    ///  Match Sub Type
    /// </summary>
    public MatchSubType MatchSubType => Fields.MatchSubType.Value;

    /// <summary>
    ///  Algorithmic Trade Indicator
    /// </summary>
    public AlgorithmicTradeIndicator AlgorithmicTradeIndicator => Fields.AlgorithmicTradeIndicator.Value;

    /// <summary>
    ///  Pad 1
    /// </summary>
    public string Pad1 => Fields.Pad1.Value;

    /// <summary>
    ///  Trade Condition
    /// </summary>
    public TradeCondition TradeCondition => Fields.TradeCondition.Value;

    /// <summary>
    ///  Pad 6
    /// </summary>
    public string Pad6 => Fields.Pad6.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public LastQty LastQty;
        public LastPx LastPx;
        public TrdMatchId TrdMatchId;
        public MatchType MatchType;
        public MatchSubType MatchSubType;
        public AlgorithmicTradeIndicator AlgorithmicTradeIndicator;
        public Pad1 Pad1;
        public TradeCondition TradeCondition;
        public Pad6 Pad6;
    };

    protected Layout Fields;
};
