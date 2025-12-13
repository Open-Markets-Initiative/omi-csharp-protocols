using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Legs Groups: Number of Leg entries Block
/// </summary>

public partial class LegsGroups
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
