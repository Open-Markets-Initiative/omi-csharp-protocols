using System.Runtime.InteropServices;

namespace Siac.Recipient;

/// <summary>
///  Block Header
/// </summary>

public partial class BlockHeader
{
    /// <summary>
    ///  Designates The Opra Binary Version
    /// </summary>
    public byte Version => Fields.Version.Value;

    /// <summary>
    ///  Size In Bytes Of Entire Transmission Block
    /// </summary>
    public ushort BlockSize => Fields.BlockSize.Value;

    /// <summary>
    ///  Value Is‘ O For Opra
    /// </summary>
    public char DataFeedIndicator => Fields.DataFeedIndicator.Value;

    /// <summary>
    ///  Contains Either A Space Not A Retransmitted Message Or V Indicating A Retransmission Of The Block
    /// </summary>
    public char RetransmissionIndicator => Fields.RetransmissionIndicator.Value;

    /// <summary>
    ///  0 X 00 For Regular Trading Session
    /// </summary>
    public SessionIndicator SessionIndicator => Fields.SessionIndicator.Value;

    /// <summary>
    ///  Packet Sequence Number
    /// </summary>
    public uint BlockSequenceNumber => Fields.BlockSequenceNumber.Value;

    /// <summary>
    ///  The Number Of Messages Contained In The Block Data Of The Transmission Block
    /// </summary>
    public byte MessagesInBlock => Fields.MessagesInBlock.Value;

    /// <summary>
    ///  Block Timestamp
    /// </summary>
    public DateTime BlockTimestamp => Fields.BlockTimestamp.Value;

    /// <summary>
    ///  Lower 16 Bits Of The 32 Bit Sum Of All Bytes In The Block Excluding The Block Checksum Field
    /// </summary>
    public ushort BlockChecksum => Fields.BlockChecksum.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public Version Version;
        public BlockSize BlockSize;
        public DataFeedIndicator DataFeedIndicator;
        public RetransmissionIndicator RetransmissionIndicator;
        public SessionIndicator SessionIndicator;
        public BlockSequenceNumber BlockSequenceNumber;
        public MessagesInBlock MessagesInBlock;
        public BlockTimestamp BlockTimestamp;
        public BlockChecksum BlockChecksum;
    };

    protected Layout Fields;
};
