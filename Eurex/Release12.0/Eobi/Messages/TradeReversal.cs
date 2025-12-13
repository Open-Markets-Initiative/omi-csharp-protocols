using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Trade Reversal
/// </summary>

public partial class TradeReversal
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
    ///  Trd Reg Ts Execution Time
    /// </summary>
    public DateTime TrdRegTsExecutionTime => Fields.TrdRegTsExecutionTime.Value;

    /// <summary>
    ///  Trd Match Id
    /// </summary>
    public uint TrdMatchId => Fields.TrdMatchId.Value;

    /// <summary>
    ///  Trade Condition
    /// </summary>
    public TradeCondition TradeCondition => Fields.TradeCondition.Value;

    /// <summary>
    ///  Md Origin Type
    /// </summary>
    public MdOriginType MdOriginType => Fields.MdOriginType.Value;

    /// <summary>
    ///  No Md Entries
    /// </summary>
    public byte NoMdEntries => Fields.NoMdEntries.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public LastQty LastQty;
        public LastPx LastPx;
        public TrdRegTsExecutionTime TrdRegTsExecutionTime;
        public TrdMatchId TrdMatchId;
        public TradeCondition TradeCondition;
        public MdOriginType MdOriginType;
        public NoMdEntries NoMdEntries;
    };

    protected Layout Fields;
};
