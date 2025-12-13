using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Incremental Refresh Trade Summary Groups: Number of Trade Summary entries Block
/// </summary>

public partial class IncrementalRefreshTradeSummaryGroups
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
