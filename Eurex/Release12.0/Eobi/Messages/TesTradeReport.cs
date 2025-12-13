using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Tes Trade Report
/// </summary>

public partial class TesTradeReport
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
    ///  Trd Type
    /// </summary>
    public TrdType TrdType => Fields.TrdType.Value;

    /// <summary>
    ///  Trade Condition
    /// </summary>
    public TradeCondition TradeCondition => Fields.TradeCondition.Value;

    /// <summary>
    ///  Multi Leg Reporting Type
    /// </summary>
    public MultiLegReportingType MultiLegReportingType => Fields.MultiLegReportingType.Value;

    /// <summary>
    ///  Multi Leg Price Model
    /// </summary>
    public MultiLegPriceModel MultiLegPriceModel => Fields.MultiLegPriceModel.Value;

    /// <summary>
    ///  Pad 6
    /// </summary>
    public string Pad6 => Fields.Pad6.Value;

    /// <summary>
    ///  Non Disclosed Trade Volume
    /// </summary>
    public ulong NonDisclosedTradeVolume => Fields.NonDisclosedTradeVolume.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public TransactTime TransactTime;
        public LastQty LastQty;
        public LastPx LastPx;
        public TrdMatchId TrdMatchId;
        public TrdType TrdType;
        public TradeCondition TradeCondition;
        public MultiLegReportingType MultiLegReportingType;
        public MultiLegPriceModel MultiLegPriceModel;
        public Pad6 Pad6;
        public NonDisclosedTradeVolume NonDisclosedTradeVolume;
    };

    protected Layout Fields;
};
