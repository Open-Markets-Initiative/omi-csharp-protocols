using System.Buffers.Binary;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Decoded IMessage entries parsed from a binary span.
/// </summary>
public sealed class MessageList : IReadOnlyList<IMessage>
{
    private readonly IMessage[] items;

    /// <summary>
    ///  Number of IMessage entries that were successfully parsed.
    /// </summary>
    public int Count => items.Length;

    /// <summary>
    ///  True if the requested count was fully consumed without structural failure.
    /// </summary>
    public bool IsDecoded { get; }

    /// <summary>
    ///  True if IsDecoded is true and every entry's IsValid is true.
    /// </summary>
    public bool IsValid { get; }

    /// <summary>
    ///  The first field that failed structural parsing inside this counted container, or null if parsing completed without structural failure.
    /// </summary>
    public Field? FailedAt { get; }

    /// <summary>
    ///  Number of messages dropped because their type code matched no modelled message and was not a known empty template. Zero in a fully-modelled stream; non-zero flags an unknown wire code encountered during the parse walk.
    /// </summary>
    public int UnknownMessageCount { get; }

    /// <summary>
    ///  Returns the IMessage at the given index.
    /// </summary>
    public IMessage this[int index] => items[index];

    /// <summary>
    ///  Enumerates all decoded IMessage entries.
    /// </summary>
    public IEnumerator<IMessage> GetEnumerator()
    {
        foreach (var item in items) yield return item;
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        => GetEnumerator();

    /// <summary>
    ///  Parses <paramref name="count"/> IMessage entries from <paramref name="data"/> starting at <paramref name="offset"/>.
    ///  On return, offset has been advanced past all consumed bytes.
    ///  If parsing fails structurally before <paramref name="count"/> items are consumed, IsDecoded is false.
    /// </summary>
    /// <param name="data">The raw binary span.</param>
    /// <param name="offset">The starting offset into data; advanced on return.</param>
    /// <param name="count">The expected number of entries to parse.</param>
    public MessageList(ReadOnlySpan<byte> data, ref int offset, int count)
    {
        const int MessageHeaderSize = 3;
        const int LengthFieldOffset = 0;
        const int LengthFieldBytes = 2;
        const int LengthFieldExtent = LengthFieldOffset + LengthFieldBytes;
        const int TypeFieldOffset = 2;
        const int SizeAddend = 2;

        var capacity = Math.Min(count, (data.Length - offset) / MessageHeaderSize);
        var parsed = new IMessage[capacity];
        var index = 0;
        var ok = true;
        var completed = true;
        var unknownDropped = 0;

        for (var i = 0; i < count; i++)
        {
            if (offset > data.Length - LengthFieldExtent) { ok = false; completed = false; break; }

            var lengthValue = (int)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset + LengthFieldOffset, LengthFieldBytes));
            if (lengthValue == 0) { offset += SizeAddend; continue; }

            if (offset > data.Length - MessageHeaderSize) { ok = false; completed = false; break; }

            var totalSize = (long)lengthValue + SizeAddend;
            var msgType = (char)data[offset + TypeFieldOffset];
            var payloadStart = offset + MessageHeaderSize;
            var payloadLen = totalSize - MessageHeaderSize;

            if (payloadLen < 0 || payloadStart > data.Length || payloadLen > data.Length - payloadStart)
            {
                ok = false;
                completed = false;
                break;
            }

            var payloadLength = (int)payloadLen;
            var msg = Dispatch.Parse(msgType, data.Slice(payloadStart, payloadLength));
            if (msg is null)
            {
                if (!Dispatch.IsKnownEmptyType(msgType)) { unknownDropped++; ok = false; }
                offset += (int)totalSize;
                continue;
            }
            if (!msg.IsDecoded)
            {
                ok = false;
                completed = false;
                FailedAt = msg.FailedAt;
                break;
            }
            parsed[index++] = msg;
            offset += (int)totalSize;
            if (!msg.IsValid)
            {
                ok = false;
            }
        }

        items = parsed[..index];
        IsDecoded = completed;
        IsValid = ok && IsDecoded;
        UnknownMessageCount = unknownDropped;
    }
}
