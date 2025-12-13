using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Security Status Workup Groups: Number of OrderID entries Block
/// </summary>

public partial class SecurityStatusWorkupGroups
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
