using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the OrderDeleteMessage message from the DeepPlus protocol.
/// </summary>

public partial class OrderDeleteMessage
{
    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public char Reserved1 => Fields.Reserved1.Value;

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

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Reserved1 Reserved1;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public OrderIdReference OrderIdReference;
    };

    protected Layout Fields;
};
