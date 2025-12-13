using System.Runtime.InteropServices;

namespace Iex.Tops;

/// <summary>
///  Message
/// </summary>

public partial class Message
{
    /// <summary>
    ///  IexTp message header
    /// </summary>
    public string MessageHeader => Fields.MessageHeader.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageHeader MessageHeader;
    };

    protected Layout Fields;
};
