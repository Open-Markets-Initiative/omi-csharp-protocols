using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Session Statistics Long Groups: Number of entries in Market Data message Block
/// </summary>

public partial class IncrementalRefreshSessionStatisticsLongGroups
{
    /// <summary>
    ///  Repeating group dimensions
    /// </summary>
    public string GroupSize => Fields.GroupSize.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public GroupSize GroupSize;
    };

    protected Layout Fields;
};
