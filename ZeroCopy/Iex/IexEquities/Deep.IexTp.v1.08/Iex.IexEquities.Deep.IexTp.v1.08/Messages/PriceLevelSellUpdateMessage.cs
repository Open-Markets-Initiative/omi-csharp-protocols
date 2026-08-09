using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the PriceLevelSellUpdateMessage message from the Deep protocol.
/// </summary>

public partial class PriceLevelSellUpdateMessage
{
    /// <summary>
    ///  Identifies event processing by the System
    /// </summary>
    public EventFlags EventFlags => Fields.EventFlags;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Aggregate quoted size
    /// </summary>
    public uint Size => Fields.Size.Value;

    /// <summary>
    ///  Price level to add/update in the IEX Order Book
    /// </summary>
    public decimal Price => Fields.Price.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public EventFlags EventFlags;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public Size Size;
        public Price Price;
    };

    protected Layout Fields;
};
