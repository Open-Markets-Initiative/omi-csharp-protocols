namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Represents the messages of one Packet, in wire order.
/// </summary>
public sealed class PacketFramedMessages : IReadOnlyList<IPacketMessage>
{
    private readonly IPacketMessage[] items = [];
    private readonly Packet? packet;

    /// <summary>
    ///  Gets the number of messages.
    /// </summary>
    public int Count => this.packet is null ? this.items.Length : this.packet.Collect().Count();

    /// <summary>
    ///  Gets the parse result of the message that failed to parse, or <see cref="ParseStatus.Ok"/> if no message failed.
    /// </summary>
    /// <remarks>
    ///  The value stays <see cref="ParseStatus.Ok"/> when reading stops for another reason, such as a short packet or an invalid message length. <see cref="IsComplete"/> reports whether the list holds every message of the packet. The value is <see cref="ParseStatus.None"/> for a list that was not parsed.
    /// </remarks>
    public ParseStatus Status { get; private set; }

    /// <summary>
    ///  Gets the message that failed to parse, or <see langword="null"/> if no message failed.
    /// </summary>
    public IPacketMessage? FailedMessage { get; private set; }

    /// <summary>
    ///  Gets a value indicating whether the list contains every message of the packet.
    /// </summary>
    /// <remarks>
    ///  Bytes after the declared number of messages are the packet trailer and do not make the list incomplete.
    /// </remarks>
    public bool IsComplete { get; private set; }

    /// <summary>
    ///  Gets why reading the messages ended, or <see langword="null"/> if a message failed to parse or the list was not parsed.
    /// </summary>
    public ReadStatus? StopReason { get; private set; }

    /// <summary>
    ///  Gets the byte offset in the packet of the first message missing from the list.
    /// </summary>
    /// <remarks>
    ///  The value is 0 if the list is complete, was not parsed, or the packet ended before its first message.
    /// </remarks>
    public int FailedOffset { get; private set; }

    internal byte[] Trailer { get; } = [];

    /// <summary>
    ///  Gets a value indicating whether every message has a type and values that the protocol defines.
    /// </summary>
    public bool IsRecognized => this.All(static message => message.IsRecognized);

    /// <summary>
    ///  Gets the number of messages whose type the protocol does not define.
    /// </summary>
    public int UnknownMessageCount => this.Count(static message => message is UnknownPacketMessage);

    /// <summary>
    ///  Gets the number of messages that the packet declares.
    /// </summary>
    /// <remarks>
    ///  The value is 0 for a packet that was not parsed or that ended before its message count. It is also 0 when the count field holds a value that marks a packet with no messages.
    /// </remarks>
    public ulong MessageCount { get; private set; }

    internal int HeaderStart { get; }


    /// <summary>
    ///  Gets the message at the specified index.
    /// </summary>
    public IPacketMessage this[int index] => this.packet is null ? this.items[index] : this.packet.Collect().ElementAt(index);

    /// <summary>
    ///  Returns an enumerator that iterates through the messages in wire order.
    /// </summary>
    public IEnumerator<IPacketMessage> GetEnumerator() => (this.packet?.Collect() ?? this.items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///  Initializes a new instance of the <see cref="PacketFramedMessages"/> class with the messages parsed from the bytes of a packet.
    /// </summary>
    public PacketFramedMessages(ReadOnlySpan<byte> data) : this(data, new Packet())
    {
    }

    internal PacketFramedMessages(ReadOnlySpan<byte> data, Packet target)
    {
        var parsed = new List<IPacketMessage>();
        var complete = true;
        Status = ParseStatus.Ok;
        var reader = new PacketMessageReader(data, target);
        while (reader.MoveNext())
        {
            var framed = reader.Current;
            if (framed.Message is { } message)
            {
                if (message.Status != ParseStatus.Ok)
                {
                    Status = message.Status;
                    FailedMessage = message;
                    FailedOffset = reader.Consumed - framed.Consumed;
                    complete = false;
                    break;
                }
                parsed.Add(message);
            }
        }

        this.items = parsed.ToArray();
        var trailing = reader.Status == ReadStatus.TrailingBytes;
        if (complete)
        {
            StopReason = reader.Status;
            if (!reader.IsComplete && !trailing)
            {
                FailedOffset = reader.Consumed;
            }
        }
        IsComplete = complete && (reader.IsComplete || trailing);
        if (trailing)
        {
            Trailer = reader.Trailer.ToArray();
        }
        MessageCount = reader.MessageCount;
        HeaderStart = reader.HeaderStart;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="PacketFramedMessages"/> class that contains the specified messages, in order.
    /// </summary>
    /// <exception cref="ArgumentNullException">The messages, or one of the messages, is <see langword="null"/>.</exception>
    public PacketFramedMessages(IEnumerable<IPacketMessage> messages)
    {
        ArgumentNullException.ThrowIfNull(messages);
        this.items = [.. messages];
        foreach (var message in this.items)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(messages));
        }
    }

    internal PacketFramedMessages(Packet packet)
    {
        this.packet = packet;
    }

    internal void Keep(PacketFramedMessages? parsed)
    {
        Status = parsed?.Status ?? default;
        FailedMessage = parsed?.FailedMessage;
        IsComplete = parsed?.IsComplete ?? default;
        StopReason = parsed?.StopReason;
        FailedOffset = parsed?.FailedOffset ?? default;
        MessageCount = parsed?.MessageCount ?? default;
    }

    internal void KeepCopy(PacketFramedMessages other)
    {
        Keep(other);
        FailedMessage = (IPacketMessage?)((IMessage?)other.FailedMessage)?.Copy();
    }
}
