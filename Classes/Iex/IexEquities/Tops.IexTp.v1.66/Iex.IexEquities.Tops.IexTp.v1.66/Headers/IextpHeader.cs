using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the IextpHeader transport header from the Tops protocol.
/// </summary>
/// <remarks>
///  IexTp packet header
/// </remarks>
[DebuggerDisplay("{ToFormattedString(PrintOptions.Identifier),nq}")]
public sealed class IextpHeader : IHeader, IEquatable<IextpHeader>
{
    /// <summary>
    ///  Total fixed byte length of this header.
    /// </summary>
    private const int HeaderByteLength = 40;

    /// <summary>
    ///  Byte length of the Reserved padding run.
    /// </summary>
    private const int ReservedBytes = 1;

    /// <summary>
    ///  Version of transport specification
    /// </summary>
    public VersionField Version { get; set; } = new();

    /// <summary>
    ///  Unique identifier of the higher layer protocol
    /// </summary>
    public MessageProtocolIdField MessageProtocolId { get; set; } = new();

    /// <summary>
    ///  Identifies the stream of bytes sequenced messages
    /// </summary>
    public ChannelIdField ChannelId { get; set; } = new();

    /// <summary>
    ///  Identifies the session
    /// </summary>
    public SessionIdField SessionId { get; set; } = new();

    /// <summary>
    ///  Byte length of the payload
    /// </summary>
    public PayloadLengthField PayloadLength { get; set; } = new();

    /// <summary>
    ///  Number of messages in the payload
    /// </summary>
    public MessageCountField MessageCount { get; set; } = new();

    /// <summary>
    ///  Byte offset of the data stream
    /// </summary>
    public StreamOffsetField StreamOffset { get; set; } = new();

    /// <summary>
    ///  Sequence of the first message in the segment
    /// </summary>
    public FirstMessageSequenceNumberField FirstMessageSequenceNumber { get; set; } = new();

    /// <summary>
    ///  Send time of segment
    /// </summary>
    public SendTimeField SendTime { get; set; } = new();

    /// <summary>
    ///  The total fixed byte length of this header (40 bytes).
    /// </summary>
    public int ByteLength => HeaderByteLength;

    public ReadOnlyMemory<byte>? Raw { get; private set; }
    public bool IsDecoded { get; private set; }

    /// <summary>
    ///  The first field that failed structural parsing (insufficient bytes), or null if the last parse succeeded or no parse has been attempted.
    /// </summary>
    public Field? FailedAt
    {
        get
        {
            if (Raw is null || IsDecoded) return null;
            foreach (var f in Fields)
                if (!f.IsDecoded) return f;
            return null;
        }
    }

    /// <summary>
    ///  True if every field's IsRecognized is true and every counted-group container is valid. Non-enum fields inherit IsRecognized =&gt; IsDecoded, so they are false until parsed. Enum fields additionally require the value to be a declared enum member.
    /// </summary>
    public bool IsRecognized
    {
        get
        {
            foreach (var f in Fields)
                if (!f.IsRecognized) return false;
            return true;
        }
    }

    /// <summary>
    ///  Convenience: true if IsDecoded and IsRecognized are both true.
    /// </summary>
    public bool IsValid => IsDecoded && IsRecognized;

    /// <summary>Identifies each field in this message for compile-time-safe lookup via GetField.</summary>
    public enum FieldName
    {
        Version,
        MessageProtocolId,
        ChannelId,
        SessionId,
        PayloadLength,
        MessageCount,
        StreamOffset,
        FirstMessageSequenceNumber,
        SendTime,
    }

    /// <summary>
    ///  Initializes a new <see cref="IextpHeader"/> with all fields at their default values.
    /// </summary>
    public IextpHeader() { }

    /// <summary>
    ///  Initializes a new <see cref="IextpHeader"/> from an existing instance, copying all field values.
    /// </summary>
    /// <param name="other">The source instance to copy from.</param>
    public IextpHeader(IextpHeader other) : this()
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other.Raw is { } raw) Raw = raw.ToArray();
        IsDecoded = other.IsDecoded;
        Version.CopyFrom(other.Version);
        MessageProtocolId.CopyFrom(other.MessageProtocolId);
        ChannelId.CopyFrom(other.ChannelId);
        SessionId.CopyFrom(other.SessionId);
        PayloadLength.CopyFrom(other.PayloadLength);
        MessageCount.CopyFrom(other.MessageCount);
        StreamOffset.CopyFrom(other.StreamOffset);
        FirstMessageSequenceNumber.CopyFrom(other.FirstMessageSequenceNumber);
        SendTime.CopyFrom(other.SendTime);
    }

    /// <summary>
    ///  Parses a <see cref="IextpHeader"/> from a binary data span at the given offset.
    ///  On return, offset has been advanced past the bytes consumed by this message.
    ///  If any field fails to parse (insufficient bytes), parsing stops and IsDecoded remains false.
    /// </summary>
    /// <param name="data">The raw message bytes.</param>
    /// <param name="offset">The starting offset into data; advanced on return.</param>
    public IextpHeader(ReadOnlySpan<byte> data, ref int offset)
    {
        var start = offset;
        var decoded = true;
        do
        {
            if (!Version.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (data.Length - offset < ReservedBytes)
            {
                decoded = false;
                break;
            }
            offset += ReservedBytes; // Reserved (padding)
            if (!MessageProtocolId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!ChannelId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!SessionId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!PayloadLength.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!MessageCount.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!StreamOffset.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!FirstMessageSequenceNumber.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!SendTime.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
        }
        while (false);
        Raw = data[start..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Parses a <see cref="IextpHeader"/> from a binary data span.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 40 bytes.</param>
    /// <returns>A new <see cref="IextpHeader"/> with all fields populated from the binary data.</returns>
    public static IextpHeader Parse(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        return new IextpHeader(data, ref offset);
    }

    /// <summary>
    ///  Parses binary data into this <see cref="IextpHeader"/> instance in place, reusing existing field objects.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 40 bytes.</param>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var offset = 0;
        var decoded = true;
        do
        {
            if (!Version.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (data.Length - offset < ReservedBytes)
            {
                decoded = false;
                break;
            }
            offset += ReservedBytes; // Reserved (padding)
            if (!MessageProtocolId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!ChannelId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!SessionId.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!PayloadLength.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!MessageCount.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!StreamOffset.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!FirstMessageSequenceNumber.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
            if (!SendTime.Parse(data, ref offset))
            {
                decoded = false;
                break;
            }
        }
        while (false);
        Raw = data[..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Encodes this <see cref="IextpHeader"/> into a binary data span starting at the given offset.
    /// </summary>
    /// <param name="data">The destination span. Must be at least offset + 40 bytes.</param>
    /// <param name="offset">The byte offset in data at which to start writing.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data, int offset)
    {
        offset = Version.Encode(data, offset);
        data.Slice(offset, ReservedBytes).Clear();
        offset += ReservedBytes;
        offset = MessageProtocolId.Encode(data, offset);
        offset = ChannelId.Encode(data, offset);
        offset = SessionId.Encode(data, offset);
        offset = PayloadLength.Encode(data, offset);
        offset = MessageCount.Encode(data, offset);
        offset = StreamOffset.Encode(data, offset);
        offset = FirstMessageSequenceNumber.Encode(data, offset);
        offset = SendTime.Encode(data, offset);
        return offset;
    }

    /// <summary>
    ///  Encodes this <see cref="IextpHeader"/> into a binary data span starting at offset 0.
    /// </summary>
    /// <param name="data">The destination span. Must be at least 40 bytes.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Returns a <see cref="IextpHeader"/> with deterministic sample values in every field,
    ///  suitable for round-trip encoding tests.
    /// </summary>
    public static IextpHeader Sample()
    {
        var msg = new IextpHeader();
        msg.Version = VersionField.Sample(seed: 0);
        msg.MessageProtocolId = MessageProtocolIdField.Sample(seed: 1);
        msg.ChannelId = ChannelIdField.Sample(seed: 2);
        msg.SessionId = SessionIdField.Sample(seed: 3);
        msg.PayloadLength = PayloadLengthField.Sample(seed: 4);
        msg.MessageCount = MessageCountField.Sample(seed: 5);
        msg.StreamOffset = StreamOffsetField.Sample(seed: 6);
        msg.FirstMessageSequenceNumber = FirstMessageSequenceNumberField.Sample(seed: 7);
        msg.SendTime = SendTimeField.Sample(seed: 8);
        return msg;
    }

    /// <summary>
    ///  Encodes this message to a new byte array.
    /// </summary>
    public byte[] ToBytes()
    {
        var buf = new byte[HeaderByteLength];
        Encode(buf);
        return buf;
    }

    /// <summary>
    ///  Resets all fields of this <see cref="IextpHeader"/> to their default values and clears the raw backing store.
    /// </summary>
    public void Clear()
    {
        Raw = null;
        IsDecoded = false;
        foreach (var field in Fields) field.Reset();
    }

    /// <summary>
    ///  Compares this <see cref="IextpHeader"/> to another using field-by-field comparison.
    /// </summary>
    public bool Equals(IextpHeader? other)
    {
        if (other is null) return false;
        if (!Version.Equals(other.Version)) return false;
        if (!MessageProtocolId.Equals(other.MessageProtocolId)) return false;
        if (!ChannelId.Equals(other.ChannelId)) return false;
        if (!SessionId.Equals(other.SessionId)) return false;
        if (!PayloadLength.Equals(other.PayloadLength)) return false;
        if (!MessageCount.Equals(other.MessageCount)) return false;
        if (!StreamOffset.Equals(other.StreamOffset)) return false;
        if (!FirstMessageSequenceNumber.Equals(other.FirstMessageSequenceNumber)) return false;
        if (!SendTime.Equals(other.SendTime)) return false;
        return true;
    }

    /// <summary>
    ///  Compares two <see cref="IextpHeader"/> instances field by field.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is IextpHeader other && Equals(other);
    }

    /// <summary>
    ///  Returns true if the message was fully decoded and its raw bytes are equal to data.
    /// </summary>
    public bool Equals(ReadOnlySpan<byte> data)
    {
        if (!IsDecoded || Raw is not { } raw) return false;
        return raw.Span.SequenceEqual(data);
    }

    /// <summary>
    ///  Returns a hash code for this <see cref="IextpHeader"/> by combining the hash of each field and counted-group container.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Version);
        hash.Add(MessageProtocolId);
        hash.Add(ChannelId);
        hash.Add(SessionId);
        hash.Add(PayloadLength);
        hash.Add(MessageCount);
        hash.Add(StreamOffset);
        hash.Add(FirstMessageSequenceNumber);
        hash.Add(SendTime);
        return hash.ToHashCode();
    }

    public static bool operator ==(IextpHeader? a, IextpHeader? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(IextpHeader? a, IextpHeader? b) => !(a == b);

    /// <summary>Identifies which fields differ between two messages of the same type. Scalar field bit positions match FieldName enum order; counted-group container bits follow after the last field bit.</summary>
    public readonly struct Changes : IEquatable<Changes>
    {
        private readonly ulong w0;

        private Changes(ulong w0)
        {
            this.w0 = w0;
        }

        public static Changes None => default;

        public bool IsNone => (w0) == 0;

        internal ulong Word(int i) => i switch
        {
            0 => w0,
            _ => 0UL,
        };

        internal Changes With(int bit)
        {
            var word = bit >> 6;
            var offset = bit & 63;
            var mask = 1UL << offset;
            return word switch
            {
                0 => new Changes(w0 | mask),
                _ => this,
            };
        }

        internal bool Has(int bit)
        {
            var word = bit >> 6;
            var offset = bit & 63;
            var mask = 1UL << offset;
            return (Word(word) & mask) != 0;
        }

        public bool Has(FieldName field) => Has((int)field);

        public bool Equals(Changes other) => w0 == other.w0;
        public override bool Equals(object? obj) => obj is Changes other && Equals(other);

        public override int GetHashCode()
        {
            var hc = new HashCode();
            hc.Add(w0);
            return hc.ToHashCode();
        }

        public static bool operator ==(Changes a, Changes b) => a.Equals(b);
        public static bool operator !=(Changes a, Changes b) => !a.Equals(b);

        public override string ToString()
        {
            if (IsNone) return "None";
            var sb = new System.Text.StringBuilder();
            var first = true;
            if (Has(0)) { if (!first) sb.Append(", "); sb.Append("Version"); first = false; }
            if (Has(1)) { if (!first) sb.Append(", "); sb.Append("MessageProtocolId"); first = false; }
            if (Has(2)) { if (!first) sb.Append(", "); sb.Append("ChannelId"); first = false; }
            if (Has(3)) { if (!first) sb.Append(", "); sb.Append("SessionId"); first = false; }
            if (Has(4)) { if (!first) sb.Append(", "); sb.Append("PayloadLength"); first = false; }
            if (Has(5)) { if (!first) sb.Append(", "); sb.Append("MessageCount"); first = false; }
            if (Has(6)) { if (!first) sb.Append(", "); sb.Append("StreamOffset"); first = false; }
            if (Has(7)) { if (!first) sb.Append(", "); sb.Append("FirstMessageSequenceNumber"); first = false; }
            if (Has(8)) { if (!first) sb.Append(", "); sb.Append("SendTime"); first = false; }
            return sb.ToString();
        }
    }

    /// <summary>Compares two messages field by field and returns a <c>Changes</c> value identifying which fields and counted-group containers differ.</summary>
    public Changes Diff(IextpHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (!Version.Equals(other.Version)) diff = diff.With(0);
        if (!MessageProtocolId.Equals(other.MessageProtocolId)) diff = diff.With(1);
        if (!ChannelId.Equals(other.ChannelId)) diff = diff.With(2);
        if (!SessionId.Equals(other.SessionId)) diff = diff.With(3);
        if (!PayloadLength.Equals(other.PayloadLength)) diff = diff.With(4);
        if (!MessageCount.Equals(other.MessageCount)) diff = diff.With(5);
        if (!StreamOffset.Equals(other.StreamOffset)) diff = diff.With(6);
        if (!FirstMessageSequenceNumber.Equals(other.FirstMessageSequenceNumber)) diff = diff.With(7);
        if (!SendTime.Equals(other.SendTime)) diff = diff.With(8);
        return diff;
    }

    /// <summary>Enumerates scalar fields that differ between this header and another. The other header must be the same type. Counted-group container changes are visible via Diff() and DiffReport() but not enumerated here.</summary>
    public IEnumerable<Field> DiffFields(IHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other is not IextpHeader typed) throw new ArgumentException("DiffFields requires same header type", nameof(other));
        var changes = Diff(typed);
        const ulong FieldMaskW0 = 0x00000000000001FFUL;
        var word0 = changes.Word(0) & FieldMaskW0;
        while (word0 != 0)
        {
            var bit = (0 * 64) + System.Numerics.BitOperations.TrailingZeroCount(word0);
            yield return GetField((FieldName)bit);
            word0 &= word0 - 1;
        }
    }

    /// <summary>Enumerates changed fields and counted-group containers as (Name, Before, After) tuples. The other message must be the same message type.</summary>
    public IEnumerable<(string Name, string Before, string After)> DiffReport(IextpHeader other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var changes = Diff(other);
        if (changes.IsNone) yield break;
        if (changes.Has(0)) yield return (Version.Name, Version.ToFormattedString(), other.Version.ToFormattedString());
        if (changes.Has(1)) yield return (MessageProtocolId.Name, MessageProtocolId.ToFormattedString(), other.MessageProtocolId.ToFormattedString());
        if (changes.Has(2)) yield return (ChannelId.Name, ChannelId.ToFormattedString(), other.ChannelId.ToFormattedString());
        if (changes.Has(3)) yield return (SessionId.Name, SessionId.ToFormattedString(), other.SessionId.ToFormattedString());
        if (changes.Has(4)) yield return (PayloadLength.Name, PayloadLength.ToFormattedString(), other.PayloadLength.ToFormattedString());
        if (changes.Has(5)) yield return (MessageCount.Name, MessageCount.ToFormattedString(), other.MessageCount.ToFormattedString());
        if (changes.Has(6)) yield return (StreamOffset.Name, StreamOffset.ToFormattedString(), other.StreamOffset.ToFormattedString());
        if (changes.Has(7)) yield return (FirstMessageSequenceNumber.Name, FirstMessageSequenceNumber.ToFormattedString(), other.FirstMessageSequenceNumber.ToFormattedString());
        if (changes.Has(8)) yield return (SendTime.Name, SendTime.ToFormattedString(), other.SendTime.ToFormattedString());
    }

    /// <summary>Enumerates all fields in declaration order.</summary>
    public IEnumerable<Field> Fields
    {
        get
        {
            yield return Version;
            yield return MessageProtocolId;
            yield return ChannelId;
            yield return SessionId;
            yield return PayloadLength;
            yield return MessageCount;
            yield return StreamOffset;
            yield return FirstMessageSequenceNumber;
            yield return SendTime;
        }
    }

    /// <summary>Looks up a field by its declared PascalCase name. Returns false if the name is not found.</summary>
    public bool TryGetField(string name, out Field field)
    {
        foreach (var f in Fields)
            if (f.Name == name) { field = f; return true; }
        field = null!;
        return false;
    }

    /// <summary>Returns the field identified by <paramref name="field"/>.</summary>
    public Field GetField(FieldName field) => field switch
    {
        FieldName.Version => Version,
        FieldName.MessageProtocolId => MessageProtocolId,
        FieldName.ChannelId => ChannelId,
        FieldName.SessionId => SessionId,
        FieldName.PayloadLength => PayloadLength,
        FieldName.MessageCount => MessageCount,
        FieldName.StreamOffset => StreamOffset,
        FieldName.FirstMessageSequenceNumber => FirstMessageSequenceNumber,
        FieldName.SendTime => SendTime,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    /// <summary>
    ///  Appends a human-readable representation of all fields in this <see cref="IextpHeader"/> to <paramref name="builder"/>,
    ///  indented according to <paramref name="options"/>.
    /// </summary>
    public void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        var indent = options.Prefix;
        builder.Append(indent).AppendLine("IextpHeader:");
        foreach (var f in Fields)
        {
            builder.Append(indent).Append("  ").Append(f.Name).Append(": ");
            f.ToFormattedString(builder, options);
            builder.AppendLine();
        }
    }

    /// <summary>
    ///  Returns a human-readable representation of all fields in this <see cref="IextpHeader"/>,
    ///  indented according to <paramref name="options"/>. When <paramref name="options"/> is
    ///  <see cref="PrintOptions.Identifier"/>, returns a compact one-line summary.
    ///  When <paramref name="options"/> is <see cref="PrintOptions.Compact"/>, returns a one-line
    ///  summary with all fields.
    /// </summary>
    public string ToFormattedString(PrintOptions options = default)
    {
        if (options.IsIdentifier)
            return $"[{Version.Value}] [{MessageProtocolId.Value}] [{ChannelId.Value}]";
        if (options.IsCompact)
        {
            var sb = new StringBuilder();
            sb.Append("IextpHeader { ");
            bool first = true;
            foreach (var f in Fields)
            {
                if (!first) sb.Append(", ");
                sb.Append(f.Name).Append(": ");
                f.ToFormattedString(sb, options);
                first = false;
            }
            sb.Append(" }");
            return sb.ToString();
        }
        var builder = new StringBuilder();
        ToFormattedString(builder, options);
        return builder.ToString();
    }

    /// <summary>
    ///  Returns a compact one-line representation of all fields in this <see cref="IextpHeader"/>.
    /// </summary>
    public string ToCompact() => ToFormattedString(PrintOptions.Compact);

    /// <summary>
    ///  Returns a human-readable representation of this <see cref="IextpHeader"/>.
    /// </summary>
    public override string ToString() => ToFormattedString();
}
