using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Delete Price Level Message: Upon receipt of this message, client should remove the price level at the specified position in its book for the given market.
/// </summary>

public partial class DeletePriceLevelMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  1 = Bid, 2 = Offer
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  Position of the price level
    /// </summary>
    public sbyte PriceLevelPosition => Fields.PriceLevelPosition.Value;

    /// <summary>
    ///  Timestamp of last update used to derive the price level message
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public Side Side;
        public PriceLevelPosition PriceLevelPosition;
        public Timestamp Timestamp;
    };

    protected Layout Fields;
};
