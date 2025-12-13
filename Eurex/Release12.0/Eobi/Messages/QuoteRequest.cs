using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Quote Request
/// </summary>

public partial class QuoteRequest
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Side
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  Pad 7
    /// </summary>
    public string Pad7 => Fields.Pad7.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public LastQty LastQty;
        public Side Side;
        public Pad7 Pad7;
        public TransactTime TransactTime;
    };

    protected Layout Fields;
};
