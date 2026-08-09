using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the TradeBreakMessage message from the Deep protocol.
/// </summary>

public partial class TradeBreakMessage
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
    ///  Aggregate quoted size
    /// </summary>
    public uint Size => Fields.Size.Value;

    /// <summary>
    ///  Price level to add/update in the IEX Order Book
    /// </summary>
    public decimal Price => Fields.Price.Value;

    /// <summary>
    ///  IEX Generated Identifier. Trade ID is also
    /// </summary>
    public ulong TradeId => Fields.TradeId.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SaleConditionFlags SaleConditionFlags;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public Size Size;
        public Price Price;
        public TradeId TradeId;
    };

    protected Layout Fields;
};
