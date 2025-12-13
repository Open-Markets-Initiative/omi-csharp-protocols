using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Order Modify Same Prio
/// </summary>

public partial class OrderModifySamePrio
{
    /// <summary>
    ///  Request Time
    /// </summary>
    public DateTime RequestTime => Fields.RequestTime.Value;

    /// <summary>
    ///  Transact Time
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Prev Display Qty
    /// </summary>
    public ulong PrevDisplayQty => Fields.PrevDisplayQty.Value;

    /// <summary>
    ///  Security Id
    /// </summary>
    public long SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  Order Details Comp
    /// </summary>
    public string OrderDetailsComp => Fields.OrderDetailsComp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public RequestTime RequestTime;
        public TransactTime TransactTime;
        public PrevDisplayQty PrevDisplayQty;
        public SecurityId SecurityId;
        public OrderDetailsComp OrderDetailsComp;
    };

    protected Layout Fields;
};
