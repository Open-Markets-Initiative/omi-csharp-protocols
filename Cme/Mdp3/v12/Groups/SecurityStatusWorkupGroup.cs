using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Security Status Workup Group: Number of OrderID entries
/// </summary>

public partial class SecurityStatusWorkupGroup
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OrderId OrderId;
        public Side Side;
        public AggressorIndicator AggressorIndicator;
    };
};
