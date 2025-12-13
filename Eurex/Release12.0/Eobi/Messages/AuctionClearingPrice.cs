using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Auction Clearing Price
/// </summary>

public partial class AuctionClearingPrice
{
    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Last Px
    /// </summary>
    public ulong LastPx => Fields.LastPx.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Imbalance Qty
    /// </summary>
    public ulong ImbalanceQty => Fields.ImbalanceQty.Value;

    /// <summary>
    ///  Security Trading Status
    /// </summary>
    public SecurityTradingStatus SecurityTradingStatus => Fields.SecurityTradingStatus.Value;

    /// <summary>
    ///  Potential Security Trading Event
    /// </summary>
    public PotentialSecurityTradingEvent PotentialSecurityTradingEvent => Fields.PotentialSecurityTradingEvent.Value;

    /// <summary>
    ///  Pad 6
    /// </summary>
    public string Pad6 => Fields.Pad6.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public SecurityId SecurityId;
        public LastPx LastPx;
        public LastQty LastQty;
        public ImbalanceQty ImbalanceQty;
        public SecurityTradingStatus SecurityTradingStatus;
        public PotentialSecurityTradingEvent PotentialSecurityTradingEvent;
        public Pad6 Pad6;
    };

    protected Layout Fields;
};
