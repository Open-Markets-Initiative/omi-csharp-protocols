using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Pre Open Price Indicator Message: This message contains the estimate of what the opening price could be, based on the orders in the market or previous settlement price.
/// </summary>

public partial class PreOpenPriceIndicatorMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long PreOpenPrice => Fields.PreOpenPrice.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  Indicates message contains PreOpenVolume. ‘Y’ or ‘N’
    /// </summary>
    public char HasPreOpenVolume => Fields.HasPreOpenVolume.Value;

    /// <summary>
    ///  Indicative volume that would trade at the preOpenPrice
    /// </summary>
    public int PreOpenVolume => Fields.PreOpenVolume.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public PreOpenPrice PreOpenPrice;
        public MessageDateTime MessageDateTime;
        public HasPreOpenVolume HasPreOpenVolume;
        public PreOpenVolume PreOpenVolume;
    };

    protected Layout Fields;
};
