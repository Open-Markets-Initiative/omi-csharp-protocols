using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Repo Related Instruments Groups: Number of related instruments group Block
/// </summary>

public partial class RepoRelatedInstrumentsGroups
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
