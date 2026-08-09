namespace Iex.IexEquities.DeepPlus.IexTp;

using System.Runtime.CompilerServices;

/// <summary>
///  A framed message view over pinned packet bytes.
/// </summary>
public readonly unsafe struct FramedMessage
{
    internal FramedMessage(MessageCode type, byte* payload, int payloadLength)
    {
        Type = type;
        Payload = payload;
        PayloadLength = payloadLength;
    }

    public MessageCode Type { get; }
    public byte* Payload { get; }
    public int PayloadLength { get; }
}

/// <summary>
///  Forward cursor over a packet's framed messages.
/// </summary>
public unsafe struct MessageWalker
{
    private readonly byte* packet;
    private readonly int length;
    private readonly ulong messageCount;
    private int offset;
    private ulong messageIndex;
    private FramedMessage current;
    private const int EmptyMessageLength = 0;
    private const int MessageFrameSizeAddend = 2;

    public MessageWalker(byte* packet, int length)
    {
        this.packet = packet;
        this.length = length;
        offset = Unsafe.SizeOf<IextpHeader.Layout>();
        messageIndex = 0;
        current = default;
        if (length < Unsafe.SizeOf<IextpHeader.Layout>())
        {
            this.messageCount = 0;
            return;
        }

        ref readonly var header = ref *(IextpHeader.Layout*)packet;
        var messageCount = (ulong)header.MessageCount.Value;
        this.messageCount = messageCount;
    }

    public readonly FramedMessage Current => current;

    public MessageWalker GetEnumerator() => this;

    public bool MoveNext()
    {
        while (messageIndex < messageCount)
        {
            if (offset + Unsafe.SizeOf<MessageHeader.Layout>() > length)
                return false;

            ref readonly var msgHeader = ref *(MessageHeader.Layout*)(packet + offset);
            var storedLength = (int)msgHeader.MessageLength.Value;
            if (storedLength == EmptyMessageLength)
            {
                offset += MessageFrameSizeAddend;
                messageIndex++;
                continue;
            }

            var frameSize = storedLength + MessageFrameSizeAddend;
            if (frameSize < Unsafe.SizeOf<MessageHeader.Layout>() || (long)offset + frameSize > length)
                return false;

            var messageType = (MessageCode)msgHeader.MessageType.Value;
            var payloadStart = offset + Unsafe.SizeOf<MessageHeader.Layout>();
            var payloadLength = frameSize - Unsafe.SizeOf<MessageHeader.Layout>();

            current = new FramedMessage(messageType, packet + payloadStart, payloadLength);
            offset += frameSize;
            messageIndex++;
            return true;
        }

        return false;
    }
}
