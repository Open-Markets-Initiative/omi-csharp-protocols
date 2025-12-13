using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Special Field: Special Field
/// </summary>

public partial class SpecialField
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SpecialFieldId SpecialFieldId;
        public SpecialFieldLength SpecialFieldLength;
    };
};
