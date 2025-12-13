using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Interval Price Limit Notification Message: Interval Price Limit (IPL) might be enabled for certain markets. IPL check is to prevent sudden movements (in both directions) in the market during a short period of time.
/// </summary>

public partial class IntervalPriceLimitNotificationMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  IPL Hold Type: ‘S’ – IPL Hold Start, ‘E’ – IPL Hold End
    /// </summary>
    public IplHoldType IplHoldType => Fields.IplHoldType.Value;

    /// <summary>
    ///  Date time of the IPL Hold notification
    /// </summary>
    public DateTime NotificationDateTime => Fields.NotificationDateTime.Value;

    /// <summary>
    ///  ‘Y’ – IPL Upper bound violation  (Bidding too high), ‘N’ – IPL Lower bound violation (Asking too low), N/A when IPLHoldType = ‘E’
    /// </summary>
    public IsUp IsUp => Fields.IsUp.Value;

    /// <summary>
    ///  Hold duration, in milliseconds. N/A when IPLHoldNotifyType = ‘E’
    /// </summary>
    public int IplHoldDuration => Fields.IplHoldDuration.Value;

    /// <summary>
    ///  IPL upper bound. OrderPriceDenominator for the market should be applied to get the real price limit. N/A when IPLHoldNotifyType = ‘E’
    /// </summary>
    public long IplUp => Fields.IplUp.Value;

    /// <summary>
    ///  IPL lower bound. OrderPriceDenominator for the market should be applied to get the real price limit. >N/A when IPLHoldNotifyType = ‘E’
    /// </summary>
    public long IplDown => Fields.IplDown.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public IplHoldType IplHoldType;
        public NotificationDateTime NotificationDateTime;
        public IsUp IsUp;
        public IplHoldDuration IplHoldDuration;
        public IplUp IplUp;
        public IplDown IplDown;
    };

    protected Layout Fields;
};
