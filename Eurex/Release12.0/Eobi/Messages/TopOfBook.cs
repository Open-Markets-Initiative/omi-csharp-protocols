using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Top Of Book
/// </summary>

public partial class TopOfBook
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
    ///  Number Of Buy Orders
    /// </summary>
    public ushort NumberOfBuyOrders => Fields.NumberOfBuyOrders.Value;

    /// <summary>
    ///  Number Of Sell Orders
    /// </summary>
    public ushort NumberOfSellOrders => Fields.NumberOfSellOrders.Value;

    /// <summary>
    ///  Pad 4
    /// </summary>
    public string Pad4 => Fields.Pad4.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public SecurityId SecurityId;
        public BidPx BidPx;
        public OfferPx OfferPx;
        public BidSize BidSize;
        public OfferSize OfferSize;
        public NumberOfBuyOrders NumberOfBuyOrders;
        public NumberOfSellOrders NumberOfSellOrders;
        public Pad4 Pad4;
    };

    protected Layout Fields;
};
