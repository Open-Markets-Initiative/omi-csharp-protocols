using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the SecurityEventMessage from the Tops protocol.
/// </summary>
/// <remarks>
///  The Security Event Message is used to indicate events that apply to a security
/// </remarks>
[DebuggerDisplay("{ToFormattedString(PrintOptions.Identifier),nq}")]
public sealed class SecurityEventMessage : IMessage, IEquatable<SecurityEventMessage>
{
    /// <summary>
    ///  Message type identifier.
    /// </summary>
    public const char MessageType = 'E';

    /// <summary>
    ///  Total fixed byte length of this message.
    /// </summary>
    private const int MessageByteLength = 17;

    /// <summary>
    ///  Security event identifier
    /// </summary>
    public SecurityEventField SecurityEvent { get; set; } = new();

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public TimestampField Timestamp { get; set; } = new();

    /// <summary>
    ///  Security identifier
    /// </summary>
    public SymbolField Symbol { get; set; } = new();

    /// <summary>
    ///  The message type character constant, or '\0' if no constant is defined.
    /// </summary>
    public char Type => MessageType;

    /// <summary>
    ///  The total fixed byte length of this message (17 bytes).
    /// </summary>
    public int ByteLength => MessageByteLength;

    public ReadOnlyMemory<byte>? Raw { get; private set; }
    public bool IsDecoded { get; private set; }

    /// <summary>The packet this message was parsed from, or null if not parsed via Packet.Parse.</summary>
    public Packet? Packet { get; private set; }

    /// <summary>Sets the back-pointer to the containing packet. Called by Packet.Parse only.</summary>
    public void SetPacket(Packet packet) => Packet = packet;

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

    /// <summary>
    ///  The timestamp field value parsed from this message.
    /// </summary>
    DateTime? IMessage.Timestamp => Timestamp.Value;

    /// <summary>Identifies each field in this message for compile-time-safe lookup via GetField.</summary>
    public enum FieldName
    {
        SecurityEvent,
        Timestamp,
        Symbol,
    }

    /// <summary>
    ///  Initializes a new <see cref="SecurityEventMessage"/> with all fields at their default values.
    /// </summary>
    public SecurityEventMessage() { }

    /// <summary>
    ///  Initializes a new <see cref="SecurityEventMessage"/> from an existing instance, copying all field values.
    /// </summary>
    /// <param name="other">The source instance to copy from.</param>
    public SecurityEventMessage(SecurityEventMessage other) : this()
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other.Raw is { } raw) Raw = raw.ToArray();
        IsDecoded = other.IsDecoded;
        SecurityEvent.CopyFrom(other.SecurityEvent);
        Timestamp.CopyFrom(other.Timestamp);
        Symbol.CopyFrom(other.Symbol);
    }

    /// <summary>
    ///  Parses a <see cref="SecurityEventMessage"/> from a binary data span at the given offset.
    ///  On return, offset has been advanced past the bytes consumed by this message.
    ///  If any field fails to parse (insufficient bytes), parsing stops and IsDecoded remains false.
    /// </summary>
    /// <param name="data">The raw message bytes.</param>
    /// <param name="offset">The starting offset into data; advanced on return.</param>
    public SecurityEventMessage(ReadOnlySpan<byte> data, ref int offset)
    {
        var start = offset;
        var decoded = true;
        foreach (var field in Fields)
            if (!field.Parse(data, ref offset)) { decoded = false; break; }
        Raw = data[start..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Parses a <see cref="SecurityEventMessage"/> from a binary data span.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 17 bytes.</param>
    /// <returns>A new <see cref="SecurityEventMessage"/> with all fields populated from the binary data.</returns>
    public static SecurityEventMessage Parse(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        return new SecurityEventMessage(data, ref offset);
    }

    /// <summary>
    ///  Parses binary data into this <see cref="SecurityEventMessage"/> instance in place, reusing existing field objects.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 17 bytes.</param>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        Clear();
        var offset = 0;
        var decoded = true;
        foreach (var field in Fields)
            if (!field.Parse(data, ref offset)) { decoded = false; break; }
        Raw = data[..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Encodes this <see cref="SecurityEventMessage"/> into a binary data span starting at the given offset.
    /// </summary>
    /// <param name="data">The destination span. Must be at least offset + 17 bytes.</param>
    /// <param name="offset">The byte offset in data at which to start writing.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data, int offset)
    {
        offset = SecurityEvent.Encode(data, offset);
        offset = Timestamp.Encode(data, offset);
        offset = Symbol.Encode(data, offset);
        return offset;
    }

    /// <summary>
    ///  Encodes this <see cref="SecurityEventMessage"/> into a binary data span starting at offset 0.
    /// </summary>
    /// <param name="data">The destination span. Must be at least 17 bytes.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Returns a <see cref="SecurityEventMessage"/> with deterministic sample values in every field,
    ///  suitable for round-trip encoding tests.
    /// </summary>
    public static SecurityEventMessage Sample()
    {
        var msg = new SecurityEventMessage();
        msg.SecurityEvent = SecurityEventField.Sample(seed: 0);
        msg.Timestamp = TimestampField.Sample(seed: 1);
        msg.Symbol = SymbolField.Sample(seed: 2);
        return msg;
    }

    /// <summary>
    ///  Encodes this message to a new byte array.
    /// </summary>
    public byte[] ToBytes()
    {
        var buf = new byte[MessageByteLength];
        Encode(buf);
        return buf;
    }

    /// <summary>
    ///  Resets all fields of this <see cref="SecurityEventMessage"/> to their default values and clears the raw backing store.
    /// </summary>
    public void Clear()
    {
        Raw = null;
        IsDecoded = false;
        foreach (var field in Fields) field.Reset();
        Packet = null;
    }

    /// <summary>
    ///  Compares this <see cref="SecurityEventMessage"/> to another using field-by-field comparison.
    /// </summary>
    public bool Equals(SecurityEventMessage? other)
    {
        if (other is null) return false;
        if (!SecurityEvent.Equals(other.SecurityEvent)) return false;
        if (!Timestamp.Equals(other.Timestamp)) return false;
        if (!Symbol.Equals(other.Symbol)) return false;
        return true;
    }

    /// <summary>
    ///  Compares two <see cref="SecurityEventMessage"/> instances field by field.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is SecurityEventMessage other && Equals(other);
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
    ///  Returns a hash code for this <see cref="SecurityEventMessage"/> by combining the hash of each field and counted-group container.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(SecurityEvent);
        hash.Add(Timestamp);
        hash.Add(Symbol);
        return hash.ToHashCode();
    }

    public static bool operator ==(SecurityEventMessage? a, SecurityEventMessage? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SecurityEventMessage? a, SecurityEventMessage? b) => !(a == b);

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
            if (Has(0)) { if (!first) sb.Append(", "); sb.Append("SecurityEvent"); first = false; }
            if (Has(1)) { if (!first) sb.Append(", "); sb.Append("Timestamp"); first = false; }
            if (Has(2)) { if (!first) sb.Append(", "); sb.Append("Symbol"); first = false; }
            return sb.ToString();
        }
    }

    /// <summary>Compares two messages field by field and returns a <c>Changes</c> value identifying which fields and counted-group containers differ.</summary>
    public Changes Diff(SecurityEventMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (!SecurityEvent.Equals(other.SecurityEvent)) diff = diff.With(0);
        if (!Timestamp.Equals(other.Timestamp)) diff = diff.With(1);
        if (!Symbol.Equals(other.Symbol)) diff = diff.With(2);
        return diff;
    }

    /// <summary>Enumerates scalar fields that differ between this message and another. The other message must be the same type. Counted-group container changes are visible via Diff() and DiffReport() but not enumerated here.</summary>
    public IEnumerable<Field> DiffFields(IMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other is not SecurityEventMessage typed) throw new ArgumentException("DiffFields requires same message type", nameof(other));
        var changes = Diff(typed);
        const ulong FieldMaskW0 = 0x0000000000000007UL;
        var word0 = changes.Word(0) & FieldMaskW0;
        while (word0 != 0)
        {
            var bit = (0 * 64) + System.Numerics.BitOperations.TrailingZeroCount(word0);
            yield return GetField((FieldName)bit);
            word0 &= word0 - 1;
        }
    }

    /// <summary>Enumerates changed fields and counted-group containers as (Name, Before, After) tuples. The other message must be the same message type.</summary>
    public IEnumerable<(string Name, string Before, string After)> DiffReport(SecurityEventMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var changes = Diff(other);
        if (changes.IsNone) yield break;
        if (changes.Has(0)) yield return (SecurityEvent.Name, SecurityEvent.ToFormattedString(), other.SecurityEvent.ToFormattedString());
        if (changes.Has(1)) yield return (Timestamp.Name, Timestamp.ToFormattedString(), other.Timestamp.ToFormattedString());
        if (changes.Has(2)) yield return (Symbol.Name, Symbol.ToFormattedString(), other.Symbol.ToFormattedString());
    }

    /// <summary>Enumerates all fields in declaration order.</summary>
    public IEnumerable<Field> Fields
    {
        get
        {
            yield return SecurityEvent;
            yield return Timestamp;
            yield return Symbol;
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
        FieldName.SecurityEvent => SecurityEvent,
        FieldName.Timestamp => Timestamp,
        FieldName.Symbol => Symbol,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    /// <summary>
    ///  Appends a human-readable representation of all fields in this <see cref="SecurityEventMessage"/> to <paramref name="builder"/>,
    ///  indented according to <paramref name="options"/>.
    /// </summary>
    public void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        var indent = options.Prefix;
        builder.Append(indent).AppendLine("SecurityEventMessage:");
        foreach (var f in Fields)
        {
            builder.Append(indent).Append("  ").Append(f.Name).Append(": ");
            f.ToFormattedString(builder, options);
            builder.AppendLine();
        }
    }

    /// <summary>
    ///  Returns a human-readable representation of all fields in this <see cref="SecurityEventMessage"/>,
    ///  indented according to <paramref name="options"/>. When <paramref name="options"/> is
    ///  <see cref="PrintOptions.Identifier"/>, returns a compact one-line summary.
    ///  When <paramref name="options"/> is <see cref="PrintOptions.Compact"/>, returns a one-line
    ///  summary with all fields.
    /// </summary>
    public string ToFormattedString(PrintOptions options = default)
    {
        if (options.IsIdentifier)
            return $"[{MessageType}] [{SecurityEvent.Value}] [{Timestamp.Value}] [{Symbol.Value}]";
        if (options.IsCompact)
        {
            var sb = new StringBuilder();
            sb.Append("SecurityEventMessage { ");
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
    ///  Returns a compact one-line representation of all fields in this <see cref="SecurityEventMessage"/>.
    /// </summary>
    public string ToCompact() => ToFormattedString(PrintOptions.Compact);

    /// <summary>
    ///  Returns a human-readable representation of this <see cref="SecurityEventMessage"/>.
    /// </summary>
    public override string ToString() => ToFormattedString();
}
