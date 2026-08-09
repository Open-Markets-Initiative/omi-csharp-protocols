using System.Text;
using System.Buffers.Binary;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  A single parsed Iex.IexEquities.Tops.IexTp packet: transport header plus all decoded messages from one framing unit.
/// </summary>
public sealed class Packet
{
    /// <summary>
    ///  The IextpHeader transport header parsed from the beginning of the UDP payload.
    /// </summary>
    public IextpHeader Header { get; private init; }

    /// <summary>
    ///  The decoded messages contained in this packet, in wire order.
    /// </summary>
    public MessageList Messages { get; private init; } = null!;

    /// <summary>
    ///  Whether this packet parsed successfully — false if any message had an unknown type or failed to parse.
    /// </summary>
    public bool IsValid { get; private init; } = true;

    /// <summary>
    ///  The number of messages in this packet, normalized from the model's Count dependency.
    /// </summary>
    public int MessageCount { get; private init; }

    /// <summary>
    ///  Parses one transport packet from a UDP payload span.
    /// </summary>
    /// <param name="data">
    ///  The raw UDP payload bytes (starting immediately after the UDP header).
    /// </param>
    /// <returns>
    ///  A parsed Packet with the decoded header and all messages. IsValid is false if the span is too short.
    /// </returns>
    public static Packet Parse(ReadOnlySpan<byte> data)
    {
        var header = IextpHeader.Parse(data);
        if (!header.IsDecoded) { var empty = 0; return new Packet { Header = header, MessageCount = 0, Messages = new MessageList(data, ref empty, 0), IsValid = false }; }

        var messageCountValue = header.MessageCount.Value;
        var offset = header.ByteLength;
        var messageCount = (int)messageCountValue;

        var messages = new MessageList(data, ref offset, messageCount);
        var packet = new Packet { Header = header, MessageCount = messageCount, Messages = messages, IsValid = messages.IsValid };
        foreach (var message in messages) message.SetPacket(packet);
        return packet;
    }

    /// <summary>
    ///  Encodes this packet — header plus all messages — into the supplied span.
    /// </summary>
    /// <param name="data">
    ///  The destination span. Must be large enough to hold the header and all messages.
    /// </param>
    /// <returns>
    ///  The offset after the last written byte.
    /// </returns>
    public int Encode(Span<byte> data)
    {
        // TODO(model): computed header fields (sequence numbers, payload length, send time, checksums)
        // require upstream characteristics to be stamped here automatically. Until those exist, the
        // consumer must populate any such fields on Header (e.g. Header.FirstMessageSequenceNumber,
        // Header.PayloadLength) before calling Encode. The message-count field is the one exception:
        // it is model-discoverable via the Count dependency and is updated below.
        Header.MessageCount = (ushort)Messages.Count;
        var offset = Header.Encode(data);
        const int MessageHeaderSize = 3;
        const int LengthFieldOffset = 0;
        const int LengthFieldBytes = 2;
        const int TypeFieldOffset = 2;
        const int SizeAddend = 2;

        foreach (var message in Messages)
        {
            var lengthValue = message.ByteLength + MessageHeaderSize - SizeAddend;
            var totalSize = message.ByteLength + MessageHeaderSize;

            BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(offset + LengthFieldOffset, LengthFieldBytes), (ushort)lengthValue);
            data[offset + TypeFieldOffset] = (byte)message.Type;
            message.Encode(data, offset + MessageHeaderSize);
            offset += totalSize;
        }
        return offset;
    }

    /// <summary>
    ///  Returns a human-readable multi-line representation of this packet: header fields then each message.
    /// </summary>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet [IextpHeader]");
        Header.ToFormattedString(builder);
        builder.Append("  Messages: ").Append(Messages.Count).AppendLine();
        var messageOptions = new PrintOptions { IndentDepth = 1 };
        foreach (var message in Messages)
        {
            message.ToFormattedString(builder, messageOptions);
        }
        return builder.ToString();
    }
}
