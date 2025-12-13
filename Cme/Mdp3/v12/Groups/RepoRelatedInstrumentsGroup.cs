using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Repo Related Instruments Group: Number of related instruments group
/// </summary>

public partial class RepoRelatedInstrumentsGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public RelatedSecurityId RelatedSecurityId;
        public RelatedSymbol RelatedSymbol;
        public RelatedInstrumentGuid RelatedInstrumentGuid;
    };
};
