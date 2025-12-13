using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Packet Header
/// </summary>

public partial class PacketHeader
{
    /// <summary>
    ///  TODO
    /// </summary>
    public ushort Session => Fields.Session.Value;

    /// <summary>
    ///  TODO
    /// </summary>
    public uint Sequence => Fields.Sequence.Value;

    /// <summary>
    ///  Field indicates the number of messages contained in the block.
    /// </summary>
    public ushort NumberOfMsgs => Fields.NumberOfMsgs.Value;

    /// <summary>
    ///  Field is the timestamp of when the message block is sent, the number of milliseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>
    public DateTime SendDatetime => Fields.SendDatetime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Session Session;
        public Sequence Sequence;
        public NumberOfMsgs NumberOfMsgs;
        public SendDatetime SendDatetime;
    };

    protected Layout Fields;
};
