using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Order Modify
/// </summary>

public partial class OrderModify
{
    /// <summary>
    ///  Request Time
    /// </summary>
    public DateTime RequestTime => Fields.RequestTime.Value;

    /// <summary>
    ///  Trd Reg Ts Prev Time Priority
    /// </summary>
    public DateTime TrdRegTsPrevTimePriority => Fields.TrdRegTsPrevTimePriority.Value;

    /// <summary>
    ///  Prev Price
    /// </summary>
    public ulong PrevPrice => Fields.PrevPrice.Value;

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

    /// <summary>
    ///  Prev Price Hhi Indicator
    /// </summary>
    public PrevPriceHhiIndicator PrevPriceHhiIndicator => Fields.PrevPriceHhiIndicator.Value;

    /// <summary>
    ///  Pad 7
    /// </summary>
    public string Pad7 => Fields.Pad7.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public RequestTime RequestTime;
        public TrdRegTsPrevTimePriority TrdRegTsPrevTimePriority;
        public PrevPrice PrevPrice;
        public PrevDisplayQty PrevDisplayQty;
        public SecurityId SecurityId;
        public OrderDetailsComp OrderDetailsComp;
        public PrevPriceHhiIndicator PrevPriceHhiIndicator;
        public Pad7 Pad7;
    };

    protected Layout Fields;
};
