using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the Message struct from the Deep protocol.
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
