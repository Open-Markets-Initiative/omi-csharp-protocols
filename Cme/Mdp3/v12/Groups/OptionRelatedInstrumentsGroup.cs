using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Option Related Instruments Group: Number of related instruments group
/// </summary>

public partial class OptionRelatedInstrumentsGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public RelatedSecurityId RelatedSecurityId;
        public RelatedSymbol RelatedSymbol;
    };
};
