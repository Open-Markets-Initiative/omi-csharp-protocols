using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Packet: Cme Mdp Packet
/// </summary>

public partial class Packet
{
    /// <summary>
    ///  Cme Mdp Packet Header
    /// </summary>
    public string BinaryPacketHeader => Fields.BinaryPacketHeader.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public BinaryPacketHeader BinaryPacketHeader;
    };

    protected Layout Fields;
};
