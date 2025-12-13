using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Close Price Message: Close Price Message
/// </summary>

public partial class ClosePriceMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long ClosePrice => Fields.ClosePrice.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public ClosePrice ClosePrice;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
