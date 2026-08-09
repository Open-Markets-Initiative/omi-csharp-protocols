using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the OrderExecutedMessage message from the DeepPlus protocol.
/// </summary>

public partial class OrderExecutedMessage
{
    /// <summary>
    ///  Sale Condition Flags
    /// </summary>
    public SaleConditionFlags SaleConditionFlags => Fields.SaleConditionFlags;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Order ID of the referenced order
    /// </summary>
    public ulong OrderIdReference => Fields.OrderIdReference.Value;

    /// <summary>
    ///  Quoted size
    /// </summary>
    public uint Size => Fields.Size.Value;

    /// <summary>
    ///  Booking price on the IEX Order Book
    /// </summary>
    public decimal Price => Fields.Price.Value;

    /// <summary>
    ///  IEX Generated Identifier
    /// </summary>
    public ulong TradeId => Fields.TradeId.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SaleConditionFlags SaleConditionFlags;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public OrderIdReference OrderIdReference;
        public Size Size;
        public Price Price;
        public TradeId TradeId;
    };

    protected Layout Fields;
};
