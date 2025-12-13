using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Index Prices Message: The exchange sends out Marker/Index Prices when there is an update.
/// </summary>

public partial class IndexPricesMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Price => Fields.Price.Value;

    /// <summary>
    ///  The short name date of the Marker/Index.  For example “Morn5Min”
    /// </summary>
    public string ShortName => Fields.ShortName.Value;

    /// <summary>
    ///  The date and time the marker was put into
    /// </summary>
    public DateTime PublishedDateTime => Fields.PublishedDateTime.Value;

    /// <summary>
    ///  The date this price is effective for
    /// </summary>
    public string ValuationDateApplyingDate => Fields.ValuationDateApplyingDate.Value;

    /// <summary>
    ///  For Endex Spot markets
    /// </summary>
    public char Status => Fields.Status.Value;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public string Reserved2 => Fields.Reserved2.Value;

    /// <summary>
    ///  This field should be applied to the Price field to get the number of decimals for the Marker/Index.
    /// </summary>
    public char IndexPriceDenominator => Fields.IndexPriceDenominator.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public Price Price;
        public ShortName ShortName;
        public PublishedDateTime PublishedDateTime;
        public ValuationDateApplyingDate ValuationDateApplyingDate;
        public Status Status;
        public Reserved2 Reserved2;
        public IndexPriceDenominator IndexPriceDenominator;
    };

    protected Layout Fields;
};
