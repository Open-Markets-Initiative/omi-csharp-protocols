using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the QuoteUpdateMessage message from the Tops protocol.
/// </summary>

public partial class QuoteUpdateMessage
{
    /// <summary>
    ///  Quote Update Flags
    /// </summary>
    public QuoteUpdateFlags QuoteUpdateFlags => Fields.QuoteUpdateFlags;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Aggregate quoted best bid size
    /// </summary>
    public uint BidSize => Fields.BidSize.Value;

    /// <summary>
    ///  Best quoted bid price
    /// </summary>
    public decimal BidPrice => Fields.BidPrice.Value;

    /// <summary>
    ///  Best quoted ask price
    /// </summary>
    public decimal AskPrice => Fields.AskPrice.Value;

    /// <summary>
    ///  Aggregate quoted best ask size
    /// </summary>
    public uint AskSize => Fields.AskSize.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public QuoteUpdateFlags QuoteUpdateFlags;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public BidSize BidSize;
        public BidPrice BidPrice;
        public AskPrice AskPrice;
        public AskSize AskSize;
    };

    protected Layout Fields;
};
