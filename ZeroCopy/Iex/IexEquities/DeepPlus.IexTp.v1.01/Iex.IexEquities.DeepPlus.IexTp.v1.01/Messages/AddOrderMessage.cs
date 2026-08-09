using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the AddOrderMessage message from the DeepPlus protocol.
/// </summary>

public partial class AddOrderMessage
{
    /// <summary>
    ///  Side of order
    /// </summary>
    public char Side => Fields.Side.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Order ID of new order
    /// </summary>
    public ulong OrderId => Fields.OrderId.Value;

    /// <summary>
    ///  Quoted size
    /// </summary>
    public uint Size => Fields.Size.Value;

    /// <summary>
    ///  Booking price on the IEX Order Book
    /// </summary>
    public decimal Price => Fields.Price.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Side Side;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public OrderId OrderId;
        public Size Size;
        public Price Price;
    };

    protected Layout Fields;
};
