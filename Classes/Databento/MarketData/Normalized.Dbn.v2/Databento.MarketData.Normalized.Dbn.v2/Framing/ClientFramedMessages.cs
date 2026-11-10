namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Represents the messages of one Client Stream, in wire order.
/// </summary>
public sealed class ClientFramedMessages : IReadOnlyList<IMessage>
{
    private readonly IMessage[] items = [];
    private readonly ClientStream? packet;

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
    public IMessage? FailedMessage { get; private set; }

    /// <summary>
    ///  Gets a value indicating whether the list contains every message of the packet.
    /// </summary>
    /// <remarks>
    ///  An incomplete final message makes the list incomplete. Bytes after the last complete message are not the trailer of the packet.
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

    /// <summary>
    ///  Gets a value indicating whether every message has a type and values that the protocol defines.
    /// </summary>
    public bool IsRecognized => this.All(static message => message.IsRecognized);

    /// <summary>
    ///  Gets the number of messages whose type the protocol does not define.
    /// </summary>
    public int UnknownMessageCount => 0;

    /// <summary>
    ///  Gets the message at the specified index.
    /// </summary>
    public IMessage this[int index] => this.packet is null ? this.items[index] : this.packet.Collect().ElementAt(index);

    /// <summary>
    ///  Returns an enumerator that iterates through the messages in wire order.
    /// </summary>
    public IEnumerator<IMessage> GetEnumerator() => (this.packet?.Collect() ?? this.items).GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientFramedMessages"/> class with the messages parsed from the bytes of a packet.
    /// </summary>
    public ClientFramedMessages(ReadOnlySpan<byte> data) : this(data, new ClientStream())
    {
    }

    internal ClientFramedMessages(ReadOnlySpan<byte> data, ClientStream target)
    {
        var parsed = new List<IMessage>();
        var complete = true;
        Status = ParseStatus.Ok;
        var reader = new ClientMessageReader(data, target);
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
        if (complete)
        {
            StopReason = reader.Status;
            if (!reader.IsComplete)
            {
                FailedOffset = reader.Consumed;
            }
        }
        IsComplete = complete && reader.IsComplete;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientFramedMessages"/> class that contains the specified messages, in order.
    /// </summary>
    /// <exception cref="ArgumentNullException">The messages, or one of the messages, is <see langword="null"/>.</exception>
    public ClientFramedMessages(IEnumerable<IMessage> messages)
    {
        ArgumentNullException.ThrowIfNull(messages);
        this.items = [.. messages];
        foreach (var message in this.items)
        {
            ArgumentNullException.ThrowIfNull(message, nameof(messages));
        }
    }

    internal ClientFramedMessages(ClientStream packet)
    {
        this.packet = packet;
    }

    internal void Keep(ClientFramedMessages? parsed)
    {
        Status = parsed?.Status ?? default;
        FailedMessage = parsed?.FailedMessage;
        IsComplete = parsed?.IsComplete ?? default;
        StopReason = parsed?.StopReason;
        FailedOffset = parsed?.FailedOffset ?? default;
    }

    internal void KeepCopy(ClientFramedMessages other)
    {
        Keep(other);
        FailedMessage = (IMessage?)((IMessage?)other.FailedMessage)?.Copy();
    }
}
