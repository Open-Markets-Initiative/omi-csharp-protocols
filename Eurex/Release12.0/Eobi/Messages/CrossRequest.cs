using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Cross Request
/// </summary>

public partial class CrossRequest
{
    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Last Px
    /// </summary>
    public ulong LastPx => Fields.LastPx.Value;

    /// <summary>
    ///  Last Qty
    /// </summary>
    public ulong LastQty => Fields.LastQty.Value;

    /// <summary>
    ///  Side
    /// </summary>
    public Side Side => Fields.Side.Value;

    /// <summary>
    ///  Cross Request Type
    /// </summary>
    public CrossRequestType CrossRequestType => Fields.CrossRequestType.Value;

    /// <summary>
    ///  Input Source
    /// </summary>
    public InputSource InputSource => Fields.InputSource.Value;

    /// <summary>
    ///  Pad 5
    /// </summary>
    public string Pad5 => Fields.Pad5.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SecurityId SecurityId;
        public LastPx LastPx;
        public LastQty LastQty;
        public Side Side;
        public CrossRequestType CrossRequestType;
        public InputSource InputSource;
        public Pad5 Pad5;
        public TransactTime TransactTime;
    };

    protected Layout Fields;
};
