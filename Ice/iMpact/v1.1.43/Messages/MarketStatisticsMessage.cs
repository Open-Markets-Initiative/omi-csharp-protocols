using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Market Statistics Message: This is usually sent after a trade or cancelled trade message.
/// </summary>

public partial class MarketStatisticsMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  Electronic trade volume only, excluding block and other volumes.
    /// </summary>
    public int Volume => Fields.Volume.Value;

    /// <summary>
    ///  Block Volume
    /// </summary>
    public int BlockVolume => Fields.BlockVolume.Value;

    /// <summary>
    ///  Efs Volume
    /// </summary>
    public int EfsVolume => Fields.EfsVolume.Value;

    /// <summary>
    ///  Efp Volume
    /// </summary>
    public int EfpVolume => Fields.EfpVolume.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long High => Fields.High.Value;

    /// <summary>
    ///  DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Low => Fields.Low.Value;

    /// <summary>
    ///  Weighted Average Price. DealPriceDenominator for the market should be applied to get the real price.
    /// </summary>
    public long Vwap => Fields.Vwap.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public Volume Volume;
        public BlockVolume BlockVolume;
        public EfsVolume EfsVolume;
        public EfpVolume EfpVolume;
        public High High;
        public Low Low;
        public Vwap Vwap;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
