using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Packet: Eurex Eobi Packet
/// </summary>

public partial class Packet
{
    /// <summary>
    ///  Eurex Eobi Packet Header
    /// </summary>
    public string PacketHeader => Fields.PacketHeader.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public PacketHeader PacketHeader;
    };

    protected Layout Fields;
};
