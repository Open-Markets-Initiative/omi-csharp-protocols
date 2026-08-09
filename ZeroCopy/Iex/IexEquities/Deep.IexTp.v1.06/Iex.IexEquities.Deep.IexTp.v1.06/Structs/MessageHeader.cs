using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the MessageHeader struct from the Deep protocol.
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
    public char MessageType => Fields.MessageType.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageLength MessageLength;
        public MessageType MessageType;
    };

    protected Layout Fields;
};
