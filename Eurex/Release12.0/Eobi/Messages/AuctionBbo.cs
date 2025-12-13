using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Auction Bbo
/// </summary>

public partial class AuctionBbo
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
    ///  Bid Px
    /// </summary>
    public ulong BidPx => Fields.BidPx.Value;

    /// <summary>
    ///  Offer Px
    /// </summary>
    public ulong OfferPx => Fields.OfferPx.Value;

    /// <summary>
    ///  Bid Size
    /// </summary>
    public ulong BidSize => Fields.BidSize.Value;

    /// <summary>
    ///  Offer Size
    /// </summary>
    public ulong OfferSize => Fields.OfferSize.Value;

    /// <summary>
    ///  Potential Security Trading Event
    /// </summary>
    public PotentialSecurityTradingEvent PotentialSecurityTradingEvent => Fields.PotentialSecurityTradingEvent.Value;

    /// <summary>
    ///  Bid Ord Type
    /// </summary>
    public BidOrdType BidOrdType => Fields.BidOrdType.Value;

    /// <summary>
    ///  Offer Ord Type
    /// </summary>
    public OfferOrdType OfferOrdType => Fields.OfferOrdType.Value;

    /// <summary>
    ///  Pad 5
    /// </summary>
    public string Pad5 => Fields.Pad5.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public SecurityId SecurityId;
        public BidPx BidPx;
        public OfferPx OfferPx;
        public BidSize BidSize;
        public OfferSize OfferSize;
        public PotentialSecurityTradingEvent PotentialSecurityTradingEvent;
        public BidOrdType BidOrdType;
        public OfferOrdType OfferOrdType;
        public Pad5 Pad5;
    };

    protected Layout Fields;
};
