using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Order Add
/// </summary>

public partial class OrderAdd
{
    /// <summary>
    ///  Request Time
    /// </summary>
    public DateTime RequestTime => Fields.RequestTime.Value;

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
        public SecurityId SecurityId;
        public OrderDetailsComp OrderDetailsComp;
    };

    protected Layout Fields;
};
