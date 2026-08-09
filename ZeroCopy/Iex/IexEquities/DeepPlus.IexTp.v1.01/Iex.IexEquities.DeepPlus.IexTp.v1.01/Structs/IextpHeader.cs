using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the IextpHeader struct from the DeepPlus protocol.
/// </summary>

public partial class IextpHeader
{
    /// <summary>
    ///  Version of transport specification
    /// </summary>
    public byte Version => Fields.Version.Value;

    /// <summary>
    ///  Unique identifier of the higher layer protocol
    /// </summary>
    public ushort MessageProtocolId => Fields.MessageProtocolId.Value;

    /// <summary>
    ///  Identifies the stream of bytes sequenced messages
    /// </summary>
    public uint ChannelId => Fields.ChannelId.Value;

    /// <summary>
    ///  Identifies the session
    /// </summary>
    public uint SessionId => Fields.SessionId.Value;

    /// <summary>
    ///  Byte length of the payload
    /// </summary>
    public ushort PayloadLength => Fields.PayloadLength.Value;

    /// <summary>
    ///  Number of messages in the payload
    /// </summary>
    public ushort MessageCount => Fields.MessageCount.Value;

    /// <summary>
    ///  Byte offset of the data stream
    /// </summary>
    public ulong StreamOffset => Fields.StreamOffset.Value;

    /// <summary>
    ///  Sequence of the first message in the segment
    /// </summary>
    public ulong FirstMessageSequenceNumber => Fields.FirstMessageSequenceNumber.Value;

    /// <summary>
    ///  Send time of segment
    /// </summary>
    public DateTime SendTime => Fields.SendTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Version Version;
        public Reserved Reserved;
        public MessageProtocolId MessageProtocolId;
        public ChannelId ChannelId;
        public SessionId SessionId;
        public PayloadLength PayloadLength;
        public MessageCount MessageCount;
        public StreamOffset StreamOffset;
        public FirstMessageSequenceNumber FirstMessageSequenceNumber;
        public SendTime SendTime;
    };

    protected Layout Fields;
};
