using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Legs Group: Number of Leg entries
/// </summary>

public partial class LegsGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public LegSecurityId LegSecurityId;
        public LegSide LegSide;
        public LegRatioQty LegRatioQty;
        public LegPrice LegPrice;
        public LegOptionDelta LegOptionDelta;
    };
};
