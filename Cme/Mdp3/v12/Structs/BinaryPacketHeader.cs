using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Binary Packet Header: Cme Mdp Packet Header
/// </summary>

public partial class BinaryPacketHeader
{
    /// <summary>
    ///  Packet Sequence Number
    /// </summary>
    public uint PacketSequenceNumber => Fields.PacketSequenceNumber.Value;

    /// <summary>
    ///  Packet Sending Time
    /// </summary>
    public DateTime SendingTime => Fields.SendingTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public PacketSequenceNumber PacketSequenceNumber;
        public SendingTime SendingTime;
    };

    protected Layout Fields;
};
