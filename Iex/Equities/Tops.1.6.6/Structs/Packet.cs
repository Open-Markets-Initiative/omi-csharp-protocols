using System.Runtime.InteropServices;

namespace Iex.Tops;

/// <summary>
///  Packet: IexTp Udp Packet
/// </summary>

public partial class Packet
{
    /// <summary>
    ///  IexTp packet header
    /// </summary>
    public string IextpHeader => Fields.IextpHeader.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public IextpHeader IextpHeader;
    };

    protected Layout Fields;
};
