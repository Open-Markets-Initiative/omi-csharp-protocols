using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Open Price Message: Open Price Message
/// </summary>

public partial class OpenPriceMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long OpenPrice => Fields.OpenPrice.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public OpenPrice OpenPrice;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
