using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the AuctionInformationMessage message from the Tops protocol.
/// </summary>

public partial class AuctionInformationMessage
{
    /// <summary>
    ///  Auction type identifier
    /// </summary>
    public char AuctionType => Fields.AuctionType.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Number of shares paired at the Reference Price using orders on the Auction Book
    /// </summary>
    public uint PairedShares => Fields.PairedShares.Value;

    /// <summary>
    ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
    /// </summary>
    public decimal ReferencePrice => Fields.ReferencePrice.Value;

    /// <summary>
    ///  Clearing price using Eligible Auction Orders
    /// </summary>
    public decimal IndicativeClearingPrice => Fields.IndicativeClearingPrice.Value;

    /// <summary>
    ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public uint ImbalanceShares => Fields.ImbalanceShares.Value;

    /// <summary>
    ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public char ImbalanceSide => Fields.ImbalanceSide.Value;

    /// <summary>
    ///  Number of extensions an auction received
    /// </summary>
    public char ExtensionNumber => Fields.ExtensionNumber.Value;

    /// <summary>
    ///  Projected time of the auction match
    /// </summary>
    public DateTime ScheduledAuctionTime => Fields.ScheduledAuctionTime.Value;

    /// <summary>
    ///  Clearing price using orders on the Auction Book
    /// </summary>
    public decimal AuctionBookClearingPrice => Fields.AuctionBookClearingPrice.Value;

    /// <summary>
    ///  Reference priced used for the auction collar, if any
    /// </summary>
    public decimal CollarReferencePrice => Fields.CollarReferencePrice.Value;

    /// <summary>
    ///  Lower threshold price of the auction collar, if any
    /// </summary>
    public decimal LowerAuctionCollar => Fields.LowerAuctionCollar.Value;

    /// <summary>
    ///  Upper threshold price of the auction collar, if any
    /// </summary>
    public decimal UpperAuctionCollar => Fields.UpperAuctionCollar.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public AuctionType AuctionType;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public PairedShares PairedShares;
        public ReferencePrice ReferencePrice;
        public IndicativeClearingPrice IndicativeClearingPrice;
        public ImbalanceShares ImbalanceShares;
        public ImbalanceSide ImbalanceSide;
        public ExtensionNumber ExtensionNumber;
        public ScheduledAuctionTime ScheduledAuctionTime;
        public AuctionBookClearingPrice AuctionBookClearingPrice;
        public CollarReferencePrice CollarReferencePrice;
        public LowerAuctionCollar LowerAuctionCollar;
        public UpperAuctionCollar UpperAuctionCollar;
    };

    protected Layout Fields;
};
