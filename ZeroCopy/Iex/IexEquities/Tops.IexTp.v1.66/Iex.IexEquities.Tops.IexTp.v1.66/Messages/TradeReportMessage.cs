using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the TradeReportMessage message from the Tops protocol.
/// </summary>

public partial class TradeReportMessage
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
    ///  Trade volume
    /// </summary>
    public uint Size => Fields.Size.Value;

    /// <summary>
    ///  Trade price
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
        public Size Size;
        public Price Price;
        public TradeId TradeId;
    };

    protected Layout Fields;
};
