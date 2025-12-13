using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Fixing Lockdown Message: This message supports ICE Benchmark Administration’s (IBA) electronic Gold Auction and it is disseminated when there is a transition of the fixing market.
/// </summary>

public partial class FixingLockdownMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Date format
    /// </summary>
    public string AuctionDate => Fields.AuctionDate.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  The auction runs twice daily at 10:30am and 3:00pm London time.  The values disseminated via this field are: ‘GOLD_1030’, ‘GOLD_1500’
    /// </summary>
    public string Description => Fields.Description.Value;

    /// <summary>
    ///  Round
    /// </summary>
    public short Round => Fields.Round.Value;

    /// <summary>
    ///  Aggregate bid quantity
    /// </summary>
    public int AggBidQty => Fields.AggBidQty.Value;

    /// <summary>
    ///  Aggregate offer quantity
    /// </summary>
    public int AggOfferQty => Fields.AggOfferQty.Value;

    /// <summary>
    ///  Auctioneers price for the round in USD. Use 2 decimal places for USD Price.
    /// </summary>
    public long UsdPrice => Fields.UsdPrice.Value;

    /// <summary>
    ///  ‘Y’/’N’
    /// </summary>
    public IsBalanced IsBalanced => Fields.IsBalanced.Value;

    /// <summary>
    ///  ‘Y’/’N’
    /// </summary>
    public IsFinal IsFinal => Fields.IsFinal.Value;

    /// <summary>
    ///  Obsolete. The price will be published via the Fixing Indicative Price Message.
    /// </summary>
    public long GbpPrice => Fields.GbpPrice.Value;

    /// <summary>
    ///  Obsolete. The price will be published via the Fixing Indicative Price Message.
    /// </summary>
    public long EurPrice => Fields.EurPrice.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public AuctionDate AuctionDate;
        public MessageDateTime MessageDateTime;
        public Description Description;
        public Round Round;
        public AggBidQty AggBidQty;
        public AggOfferQty AggOfferQty;
        public UsdPrice UsdPrice;
        public IsBalanced IsBalanced;
        public IsFinal IsFinal;
        public GbpPrice GbpPrice;
        public EurPrice EurPrice;
    };

    protected Layout Fields;
};
