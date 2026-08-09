using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the Packet struct from the DeepPlus protocol.
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
