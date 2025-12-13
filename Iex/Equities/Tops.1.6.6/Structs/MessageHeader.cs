using System.Runtime.InteropServices;

namespace Iex.Tops;

/// <summary>
///  Message Header: IexTp message header
/// </summary>

public partial class MessageHeader
{
    /// <summary>
    ///  Length of the message
    /// </summary>
    public ushort MessageLength => Fields.MessageLength.Value;

    /// <summary>
    ///  Code identifying this message type
    /// </summary>
    public MessageType MessageType => Fields.MessageType.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageLength MessageLength;
        public MessageType MessageType;
    };

    protected Layout Fields;
};
