using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the Packet struct from the Deep protocol.
/// </summary>

public partial class Packet
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public IextpHeader.Layout IextpHeader;
    };

    protected Layout Fields;
};
