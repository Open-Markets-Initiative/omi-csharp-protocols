using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Instrument State Change
/// </summary>

public partial class InstrumentStateChange
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Security Status
    /// </summary>
    public SecurityStatus SecurityStatus => Fields.SecurityStatus.Value;

    /// <summary>
    ///  Security Trading Status
    /// </summary>
    public SecurityTradingStatus SecurityTradingStatus => Fields.SecurityTradingStatus.Value;

    /// <summary>
    ///  Market Condition
    /// </summary>
    public MarketCondition MarketCondition => Fields.MarketCondition.Value;

    /// <summary>
    ///  Fast Market Indicator
    /// </summary>
    public FastMarketIndicator FastMarketIndicator => Fields.FastMarketIndicator.Value;

    /// <summary>
    ///  Security Trading Event
    /// </summary>
    public SecurityTradingEvent SecurityTradingEvent => Fields.SecurityTradingEvent.Value;

    /// <summary>
    ///  Sold Out Indicator
    /// </summary>
    public SoldOutIndicator SoldOutIndicator => Fields.SoldOutIndicator.Value;

    /// <summary>
    ///  Pad 2
    /// </summary>
    public string Pad2 => Fields.Pad2.Value;

    /// <summary>
    ///  High Px
    /// </summary>
    public ulong HighPx => Fields.HighPx.Value;

    /// <summary>
    ///  Low Px
    /// </summary>
    public ulong LowPx => Fields.LowPx.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Tes Security Status
    /// </summary>
    public TesSecurityStatus TesSecurityStatus => Fields.TesSecurityStatus.Value;

    /// <summary>
    ///  Pad 7
    /// </summary>
    public string Pad7 => Fields.Pad7.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public SecurityStatus SecurityStatus;
        public SecurityTradingStatus SecurityTradingStatus;
        public MarketCondition MarketCondition;
        public FastMarketIndicator FastMarketIndicator;
        public SecurityTradingEvent SecurityTradingEvent;
        public SoldOutIndicator SoldOutIndicator;
        public Pad2 Pad2;
        public HighPx HighPx;
        public LowPx LowPx;
        public TransactTime TransactTime;
        public TesSecurityStatus TesSecurityStatus;
        public Pad7 Pad7;
    };

    protected Layout Fields;
};
