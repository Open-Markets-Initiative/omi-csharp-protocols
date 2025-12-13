using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Instrument Summary
/// </summary>

public partial class InstrumentSummary
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Last Update Time
    /// </summary>
    public DateTime LastUpdateTime => Fields.LastUpdateTime.Value;

    /// <summary>
    ///  Trd Reg Ts Execution Time
    /// </summary>
    public DateTime TrdRegTsExecutionTime => Fields.TrdRegTsExecutionTime.Value;

    /// <summary>
    ///  Tot No Orders
    /// </summary>
    public ushort TotNoOrders => Fields.TotNoOrders.Value;

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
    ///  High Px
    /// </summary>
    public ulong HighPx => Fields.HighPx.Value;

    /// <summary>
    ///  Low Px
    /// </summary>
    public ulong LowPx => Fields.LowPx.Value;

    /// <summary>
    ///  Product Complex
    /// </summary>
    public ProductComplex ProductComplex => Fields.ProductComplex.Value;

    /// <summary>
    ///  No Md Entries
    /// </summary>
    public byte NoMdEntries => Fields.NoMdEntries.Value;

    /// <summary>
    ///  Tes Security Status
    /// </summary>
    public TesSecurityStatus TesSecurityStatus => Fields.TesSecurityStatus.Value;

    /// <summary>
    ///  Pad 5
    /// </summary>
    public string Pad5 => Fields.Pad5.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public LastUpdateTime LastUpdateTime;
        public TrdRegTsExecutionTime TrdRegTsExecutionTime;
        public TotNoOrders TotNoOrders;
        public SecurityStatus SecurityStatus;
        public SecurityTradingStatus SecurityTradingStatus;
        public MarketCondition MarketCondition;
        public FastMarketIndicator FastMarketIndicator;
        public SecurityTradingEvent SecurityTradingEvent;
        public SoldOutIndicator SoldOutIndicator;
        public HighPx HighPx;
        public LowPx LowPx;
        public ProductComplex ProductComplex;
        public NoMdEntries NoMdEntries;
        public TesSecurityStatus TesSecurityStatus;
        public Pad5 Pad5;
    };

    protected Layout Fields;
};
