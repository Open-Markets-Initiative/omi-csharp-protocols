using System.Text;
using System.Buffers.Binary;
using static Databento.MarketData.Normalized.Dbn.v2.Rules;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The client's control messages: authentication, subscriptions and the session start
/// </summary>
public sealed class ClientStream : IPacket
{
    IHeader? IPacket.Header => null;

    private ControlMessageList controlMessageList = new([]);

    /// <summary>
    ///  A control message: key=value fields separated by |, ending on a newline
    /// </summary>
    public ControlMessageList ControlMessageList => this.controlMessageList;

    /// <summary>
    ///  Gets the messages of the packet, in wire order.
    /// </summary>
    /// <remarks>
    ///  The list reads the packet entries each time that it is used, so it shows each change to them. Add or remove messages through the entries.
    /// </remarks>
    public ClientFramedMessages Messages { get; }

    IReadOnlyList<IMessage> IPacket.Messages => Messages;

    /// <summary>
    ///  Gets a value indicating whether the header and every message of the packet parsed, with no message missing.
    /// </summary>
    /// <remarks>
    ///  Unlike <see cref="IsValid"/>, this property keeps the result of the parse when a value changes. It does not test whether the protocol defines each value. A packet with an unknown message type or an undefined value can be parsed. The value is <see langword="true"/> for a packet that is created in code rather than parsed.
    /// </remarks>
    public bool IsParsed { get; private set; } = true;

    /// <summary>
    ///  Gets a value indicating whether the protocol specification allows every value of the header and of each message.
    /// </summary>
    /// <remarks>
    ///  The value is computed from the current values each time that it is read. It is <see langword="true"/> when <see cref="Validate"/> reports no failures. It is <see langword="false"/> for a message of a type that the protocol does not define. It does not show whether the parse succeeded. A field that the parse did not reach holds its cleared value, so check <see cref="IsParsed"/> first.
    /// </remarks>
    public bool IsValid => Validate().Count == 0;

    ReadOnlyMemory<byte> IPacket.Trailer => ReadOnlyMemory<byte>.Empty;

    /// <summary>
    ///  Initializes a new instance of the <see cref="ClientStream"/> class with default values and no messages.
    /// </summary>
    public ClientStream()
    {
        Messages = new ClientFramedMessages(this);
        Clear();
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(ClientStream other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (ReferenceEquals(this, other))
            return;
        this.controlMessageList = new ControlMessageList(other.controlMessageList);
        Messages.KeepCopy(other.Messages);
        this.IsParsed = other.IsParsed;
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    public void Clear()
    {
        this.controlMessageList = new([]);
        Messages.Keep(null);
        this.IsParsed = true;
    }

    /// <summary>
    ///  Parses a packet from the bytes.
    /// </summary>
    /// <returns>The packet. Check <see cref="IsParsed"/> for the result of the parse, and <see cref="IsValid"/> for whether the protocol allows every value.</returns>
    public static ClientStream Parse(ReadOnlySpan<byte> data)
    {
        var packet = new ClientStream();
        packet.ParseFrom(data);
        return packet;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var messages = new ClientFramedMessages(data, this);
        this.IsParsed = messages.Status == ParseStatus.Ok && messages.IsComplete;
        Messages.Keep(messages);
    }

    /// <summary>
    ///  Gets the length of the Control Message frame at the start of the bytes.
    /// </summary>
    /// <param name="source">The bytes, from the start of the frame.</param>
    /// <param name="length">The number of bytes of the frame, which can be more than the bytes hold.</param>
    /// <returns><see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">The bytes state a frame length that is not valid.</exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, out int length)
    {
        const string FrameLengthInvalid = "The bytes state a frame length that is not valid.";
        global::System.Int128 ControlMessageReassembleRule1Width;
        int ControlMessageReassembleRule1Terminator0Bytes;
        int ControlMessageReassembleRule1Terminator0Offset;
        length = 0;
        const byte ControlMessageReassembleRule1Terminator0 = 10;
        {
            ControlMessageReassembleRule1Terminator0Offset = source[..source.Length].IndexOf(ControlMessageReassembleRule1Terminator0);
            if (ControlMessageReassembleRule1Terminator0Offset < 0) return false;
            ControlMessageReassembleRule1Terminator0Bytes = ControlMessageReassembleRule1Terminator0Offset + sizeof(byte);
        }
        ControlMessageReassembleRule1Width = checked((global::System.Int128)(ControlMessageReassembleRule1Terminator0Bytes));
        if (ControlMessageReassembleRule1Width <= RuleEmptyWidth || ControlMessageReassembleRule1Width > int.MaxValue) throw new global::System.ArgumentOutOfRangeException(nameof(source), FrameLengthInvalid);
        length = (int)ControlMessageReassembleRule1Width;
        return true;
    }

    internal IEnumerable<IMessage> Collect()
    {
        yield break;
    }

    internal void SetControlMessageList(ControlMessageList value)
    {
        this.controlMessageList = value;
    }

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var length = 0;
            var controlMessageMeasuredEntries = this.ControlMessageList;
            var controlMessageMeasuredFirst = 0;
            while (controlMessageMeasuredFirst < controlMessageMeasuredEntries.Count)
            {
                length += (controlMessageMeasuredEntries[controlMessageMeasuredFirst]).EncodedByteLength;
                controlMessageMeasuredFirst++;
            }
            return length;
        }
    }

    /// <summary>
    ///  Encodes the packet into the start of the span.
    /// </summary>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data)
    {
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!Write(data, length, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the packet into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the reason and the first field whose value cannot be encoded, if any. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes.</param>
    /// <returns><see langword="true"/> if the packet was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  The field is in the header or in a message. For a field in a group entry or nested message, the failure also names the group or field holding it. If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        var length = EncodedByteLength;
        if (data.Length < length)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!Write(data, length, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    private bool Write(Span<byte> data, int length, out int cursor, out EncodeFailure failure)
    {
        cursor = 0;
        var clientStreamWriterStart = cursor;
        var controlMessageWriterPosition = cursor;
        var controlMessageMeasuredBytes = 0;
        {
            var controlMessageMeasuredEntries = this.ControlMessageList;
            var controlMessageMeasuredFirst = 0;
            while (controlMessageMeasuredFirst < controlMessageMeasuredEntries.Count)
            {
                controlMessageMeasuredBytes += (controlMessageMeasuredEntries[controlMessageMeasuredFirst]).EncodedByteLength;
                controlMessageMeasuredFirst++;
            }
        }
        var controlMessageWriterEntries = this.ControlMessageList;
        var controlMessageIndex = 0;
        while (controlMessageIndex < controlMessageWriterEntries.Count)
        {
            var controlMessageWriterStart = cursor;
            var controlMessageOccurrenceBytes = 0;
            {
                controlMessageOccurrenceBytes += (controlMessageWriterEntries[controlMessageIndex]).EncodedByteLength;
            }
            var controlMessageWriting = controlMessageWriterEntries[controlMessageIndex];
            if (!((IGroupEntry)controlMessageWriting).Write(data, cursor, out cursor, out failure)) return false;
            {
                int ControlMessageReassembleRule1Terminator0Bytes;
                int ControlMessageReassembleRule1Terminator0Offset;
                const byte ControlMessageReassembleRule1Terminator0 = 10;
                global::System.Int128 measure = controlMessageOccurrenceBytes;
                bool agrees;
                {
                    {
                        ControlMessageReassembleRule1Terminator0Offset = data.Slice(controlMessageWriterStart, length - controlMessageWriterStart).IndexOf(ControlMessageReassembleRule1Terminator0);
                        if (ControlMessageReassembleRule1Terminator0Offset < 0) { failure = new EncodeFailure(EncodeStatus.RuleMismatch); return false; }
                        ControlMessageReassembleRule1Terminator0Bytes = ControlMessageReassembleRule1Terminator0Offset + sizeof(byte);
                    }
                    agrees = checked((global::System.Int128)(ControlMessageReassembleRule1Terminator0Bytes)) == measure;
                }
                if (!agrees)
                {
                    failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                    return false;
                }
            }
            controlMessageIndex++;
        }
        {
            global::System.Int128 measure = controlMessageWriterPosition + controlMessageMeasuredBytes;
            bool agrees;
            agrees = checked((global::System.Int128)(length)) == measure;
            if (!agrees)
            {
                failure = new EncodeFailure(EncodeStatus.RuleMismatch);
                return false;
            }
        }
        failure = default;
        return true;
    }

    /// <summary>
    ///  Checks every field of the header and of each message against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        foreach (var (index, message) in Messages.Index())
        {
            foreach (var failure in message.Validate())
            {
                failures.Add(failure.InMessage(index));
            }
        }
        failures.AddRange(this.ControlMessageList.SelectMany(static entry => entry.Validate()));
        return failures;
    }

    /// <summary>
    ///  Returns the packet formatted on multiple lines.
    /// </summary>
    /// <remarks>
    ///  The text holds each kind and message of the packet, and a line for each frame entry.
    /// </remarks>
    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.AppendLine("Packet");
        return builder.ToString();
    }
}
