using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Option Underlyings Groups: Number of underlying instruments Block
/// </summary>

public partial class OptionUnderlyingsGroups
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
