using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the Message struct from the DeepPlus protocol.
/// </summary>

public partial class Message
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageHeader.Layout MessageHeader;
    };

    protected Layout Fields;
};
