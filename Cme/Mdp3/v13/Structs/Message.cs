using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Message: Cme Market Data Message
/// </summary>

public partial class Message
{
    /// <summary>
    ///  Message Size
    /// </summary>
    public ushort MessageSize => Fields.MessageSize.Value;

    /// <summary>
    ///  Template ID and length of message root
    /// </summary>
    public string MessageHeader => Fields.MessageHeader.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageSize MessageSize;
        public MessageHeader MessageHeader;
    };

    protected Layout Fields;
};
