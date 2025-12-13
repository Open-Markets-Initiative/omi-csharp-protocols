using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Market State Change Message: Market State Change Message
/// </summary>

public partial class MarketStateChangeMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  See Appendix A on the trading status codes
    /// </summary>
    public TradingStatus TradingStatus => Fields.TradingStatus.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public TradingStatus TradingStatus;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
