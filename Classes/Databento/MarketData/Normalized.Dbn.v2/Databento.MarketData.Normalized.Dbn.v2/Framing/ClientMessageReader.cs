using System.Buffers.Binary;
using static Databento.MarketData.Normalized.Dbn.v2.Rules;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Represents one message of a packet and the bytes that it occupies.
/// </summary>
public readonly ref struct ClientFramedMessage
{
    internal ClientFramedMessage(ReadOnlySpan<byte> header, ReadOnlySpan<byte> payload, int consumed, IMessage? message)
    {
        Header = header;
        Payload = payload;
        Consumed = consumed;
        Message = message;
    }

    /// <summary>
    ///  Gets the bytes that precede the payload of the message.
    /// </summary>
    public ReadOnlySpan<byte> Header { get; }
    /// <summary>
    ///  Gets the payload bytes of the message.
    /// </summary>
    public ReadOnlySpan<byte> Payload { get; }
    /// <summary>
    ///  Gets the number of packet bytes that the message occupies.
    /// </summary>
    public int Consumed { get; }
    /// <summary>
    ///  Gets the parsed message, or <see langword="null"/> if the packet or frame carries no message.
    /// </summary>
    public IMessage? Message { get; }
}

/// <summary>
///  Reads the messages of a packet one at a time.
/// </summary>
public ref struct ClientMessageReader
{
    private const int Start = 0;
    private const int ControlMessageOccurrence = 1;
    private const int Ended = 2;
    private readonly ReadOnlySpan<byte> buffer;
    private int state;
    private int position;
    private readonly ClientStream target;
    List<ControlMessage> controlMessageEntries = [];
    int ControlMessagePayloadRule0End;
    global::System.Int128 ControlMessagePayloadRule0RawEnd;
    int controlMessageEntryStart;
    int controlMessageEnd;
    int ControlMessageReassembleRule1End;
    global::System.Int128 ControlMessageReassembleRule1Width;
    int ControlMessageReassembleRule1Terminator0Bytes;
    int ControlMessageReassembleRule1Terminator0Offset;
    int controlFieldsEntryStart;
    int controlFieldsEnd;
    int ControlFieldsSizeRule0End;
    global::System.Int128 ControlFieldsSizeRule0Width;
    int ControlFieldsSizeRule0Terminator0Bytes;
    int ControlFieldsSizeRule0Terminator0Offset;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientMessageReader"/> structure that reads the messages from the bytes of a packet.
    /// </summary>
    public ClientMessageReader(ReadOnlySpan<byte> buffer) : this(buffer, new ClientStream())
    {
    }

    internal ClientMessageReader(ReadOnlySpan<byte> buffer, ClientStream target)
    {
        this.buffer = buffer;
        Status = ReadStatus.Complete;
        this.target = target;
    }

    /// <summary>
    ///  Gets the message at the current position.
    /// </summary>
    public ClientFramedMessage Current { get; private set; }
    /// <summary>
    ///  Gets the reason that reading stopped, or <see cref="ReadStatus.Complete"/> if no error stopped it.
    /// </summary>
    public ReadStatus Status { get; private set; }
    /// <summary>
    ///  Gets a value indicating whether no error has stopped reading.
    /// </summary>
    public bool IsComplete => Status == ReadStatus.Complete;
    /// <summary>
    ///  Gets the offset of the next unread byte.
    /// </summary>
    public int Consumed => position;

    /// <summary>
    ///  Advances to the next message.
    /// </summary>
    /// <returns><see langword="true"/> if there is another message, or <see langword="false"/> if the end was reached or an error stopped reading.</returns>
    public bool MoveNext()
    {
        switch (state)
        {
            case Start:
            {
                controlMessageEntries.Clear();
                {
                    ControlMessagePayloadRule0RawEnd = checked((global::System.Int128)(buffer.Length));
                    if (ControlMessagePayloadRule0RawEnd < position || ControlMessagePayloadRule0RawEnd > buffer.Length) return Stop(ReadStatus.Truncated);
                    ControlMessagePayloadRule0End = (int)ControlMessagePayloadRule0RawEnd;
                }
                goto case ControlMessageOccurrence;
            }
            case ControlMessageOccurrence:
            {
                if (!(position < ControlMessagePayloadRule0End))
                {
                    {
                        if (position != ControlMessagePayloadRule0End) return Stop(ReadStatus.InvalidLength);
                        position = ControlMessagePayloadRule0End;
                        target.SetControlMessageList(new ControlMessageList(controlMessageEntries));
                        position = buffer.Length;
                        if (state == Start)
                        {
                            Current = new ClientFramedMessage(buffer[0..position], buffer[position..position], position - 0, null);
                            state = Ended;
                            return true;
                        }
                        state = Ended;
                        return false;
                    }
                }
                controlMessageEntryStart = position;
                {
                    controlMessageEntryStart = position;
                    const byte ControlMessageReassembleRule1Terminator0 = 10;
                    {
                        ControlMessageReassembleRule1Terminator0Offset = buffer.Slice(position, ControlMessagePayloadRule0End - position).IndexOf(ControlMessageReassembleRule1Terminator0);
                        if (ControlMessageReassembleRule1Terminator0Offset < 0) return Stop(ReadStatus.Truncated);
                        ControlMessageReassembleRule1Terminator0Bytes = ControlMessageReassembleRule1Terminator0Offset + sizeof(byte);
                    }
                    ControlMessageReassembleRule1Width = checked((global::System.Int128)(ControlMessageReassembleRule1Terminator0Bytes));
                    if (ControlMessageReassembleRule1Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ControlMessageReassembleRule1Width > (long)ControlMessagePayloadRule0End - position) return Stop(ReadStatus.Truncated);
                    ControlMessageReassembleRule1End = position + (int)ControlMessageReassembleRule1Width;
                    controlMessageEnd = ControlMessageReassembleRule1End;
                    controlFieldsEntryStart = position;
                    const byte ControlFieldsSizeRule0Terminator0 = 10;
                    {
                        ControlFieldsSizeRule0Terminator0Offset = buffer.Slice(position, controlMessageEnd - position).IndexOf(ControlFieldsSizeRule0Terminator0);
                        if (ControlFieldsSizeRule0Terminator0Offset < 0) return Stop(ReadStatus.Truncated);
                        ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
                    }
                    ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
                    if (ControlFieldsSizeRule0Width < RuleEmptyWidth) return Stop(ReadStatus.InvalidLength);
                    if (ControlFieldsSizeRule0Width > (long)controlMessageEnd - position) return Stop(ReadStatus.Truncated);
                    ControlFieldsSizeRule0End = position + (int)ControlFieldsSizeRule0Width;
                    controlFieldsEnd = ControlFieldsSizeRule0End;
                    position = controlFieldsEnd;
                    position = controlFieldsEnd;
                    position = controlMessageEnd;
                    {
                        var controlMessageParsed = new ControlMessage();
                        var controlMessageOffset = controlMessageEntryStart;
                        controlMessageParsed.ParseFrom(buffer[..ControlMessagePayloadRule0End], ref controlMessageOffset);
                        controlMessageEntries.Add(controlMessageParsed);
                    }
                    if (position <= controlMessageEntryStart || position > ControlMessagePayloadRule0End) return Stop(ReadStatus.InvalidLength);
                    goto case ControlMessageOccurrence;
                }
            }
            default:
            {
                return false;
            }
        }
    }

    private bool Stop(ReadStatus status) { Status = status; state = Ended; return false; }

    private bool Take(int width, int end)
    {
        if (position + width > end) return Stop(ReadStatus.Truncated);
        position += width;
        return true;
    }
}
