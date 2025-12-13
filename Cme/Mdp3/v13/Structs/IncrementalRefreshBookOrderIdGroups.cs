using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Book Order Id Groups: Number of OrderID entries Block
/// </summary>

public partial class IncrementalRefreshBookOrderIdGroups
{
    /// <summary>
    ///  8 Byte aligned repeating group dimensions
    /// </summary>
    public string GroupSize8Byte => Fields.GroupSize8Byte.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public GroupSize8Byte GroupSize8Byte;
    };

    protected Layout Fields;
};
