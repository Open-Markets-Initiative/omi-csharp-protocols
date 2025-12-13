using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Product Summary
/// </summary>

public partial class ProductSummary
{
    /// <summary>
    ///  Last Msg Seq Num Processed
    /// </summary>
    public uint LastMsgSeqNumProcessed => Fields.LastMsgSeqNumProcessed.Value;

    /// <summary>
    ///  Trading Session Id
    /// </summary>
    public TradingSessionId TradingSessionId => Fields.TradingSessionId.Value;

    /// <summary>
    ///  Trading Session Sub Id
    /// </summary>
    public TradingSessionSubId TradingSessionSubId => Fields.TradingSessionSubId.Value;

    /// <summary>
    ///  Trad Ses Status
    /// </summary>
    public TradSesStatus TradSesStatus => Fields.TradSesStatus.Value;

    /// <summary>
    ///  Market Condition
    /// </summary>
    public MarketCondition MarketCondition => Fields.MarketCondition.Value;

    /// <summary>
    ///  Fast Market Indicator
    /// </summary>
    public FastMarketIndicator FastMarketIndicator => Fields.FastMarketIndicator.Value;

    /// <summary>
    ///  Tes Trad Ses Status
    /// </summary>
    public TesTradSesStatus TesTradSesStatus => Fields.TesTradSesStatus.Value;

    /// <summary>
    ///  Pad 6
    /// </summary>
    public string Pad6 => Fields.Pad6.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LastMsgSeqNumProcessed LastMsgSeqNumProcessed;
        public TradingSessionId TradingSessionId;
        public TradingSessionSubId TradingSessionSubId;
        public TradSesStatus TradSesStatus;
        public MarketCondition MarketCondition;
        public FastMarketIndicator FastMarketIndicator;
        public TesTradSesStatus TesTradSesStatus;
        public Pad6 Pad6;
    };

    protected Layout Fields;
};
