using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the OrderExecutedMessage from the DeepPlus protocol.
/// </summary>
/// <remarks>
///  A displayed order that was executed against
/// </remarks>
[DebuggerDisplay("{ToFormattedString(PrintOptions.Identifier),nq}")]
public sealed class OrderExecutedMessage : IMessage, IEquatable<OrderExecutedMessage>
{
    /// <summary>
    ///  Message type identifier.
    /// </summary>
    public const char MessageType = 'L';

    /// <summary>
    ///  Total fixed byte length of this message.
    /// </summary>
    private const int MessageByteLength = 45;

    /// <summary>
    ///  Sale Condition Flags
    /// </summary>
    public SaleConditionFlagsField SaleConditionFlags { get; set; } = new();

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public TimestampField Timestamp { get; set; } = new();

    /// <summary>
    ///  Security identifier
    /// </summary>
    public SymbolField Symbol { get; set; } = new();

    /// <summary>
    ///  Order ID of the referenced order
    /// </summary>
    public OrderIdReferenceField OrderIdReference { get; set; } = new();

    /// <summary>
    ///  Quoted size
    /// </summary>
    public SizeField Size { get; set; } = new();

    /// <summary>
    ///  Booking price on the IEX Order Book
    /// </summary>
    public PriceField Price { get; set; } = new();

    /// <summary>
    ///  IEX Generated Identifier
    /// </summary>
    public TradeIdField TradeId { get; set; } = new();

    /// <summary>
    ///  The message type character constant, or '\0' if no constant is defined.
    /// </summary>
    public char Type => MessageType;

    /// <summary>
    ///  The total fixed byte length of this message (45 bytes).
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
        SaleConditionFlags,
        Timestamp,
        Symbol,
        OrderIdReference,
        Size,
        Price,
        TradeId,
    }

    /// <summary>
    ///  Initializes a new <see cref="OrderExecutedMessage"/> with all fields at their default values.
    /// </summary>
    public OrderExecutedMessage() { }

    /// <summary>
    ///  Initializes a new <see cref="OrderExecutedMessage"/> from an existing instance, copying all field values.
    /// </summary>
    /// <param name="other">The source instance to copy from.</param>
    public OrderExecutedMessage(OrderExecutedMessage other) : this()
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other.Raw is { } raw) Raw = raw.ToArray();
        IsDecoded = other.IsDecoded;
        SaleConditionFlags.CopyFrom(other.SaleConditionFlags);
        Timestamp.CopyFrom(other.Timestamp);
        Symbol.CopyFrom(other.Symbol);
        OrderIdReference.CopyFrom(other.OrderIdReference);
        Size.CopyFrom(other.Size);
        Price.CopyFrom(other.Price);
        TradeId.CopyFrom(other.TradeId);
    }

    /// <summary>
    ///  Parses a <see cref="OrderExecutedMessage"/> from a binary data span at the given offset.
    ///  On return, offset has been advanced past the bytes consumed by this message.
    ///  If any field fails to parse (insufficient bytes), parsing stops and IsDecoded remains false.
    /// </summary>
    /// <param name="data">The raw message bytes.</param>
    /// <param name="offset">The starting offset into data; advanced on return.</param>
    public OrderExecutedMessage(ReadOnlySpan<byte> data, ref int offset)
    {
        var start = offset;
        var decoded = true;
        foreach (var field in Fields)
            if (!field.Parse(data, ref offset)) { decoded = false; break; }
        Raw = data[start..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Parses a <see cref="OrderExecutedMessage"/> from a binary data span.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 45 bytes.</param>
    /// <returns>A new <see cref="OrderExecutedMessage"/> with all fields populated from the binary data.</returns>
    public static OrderExecutedMessage Parse(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        return new OrderExecutedMessage(data, ref offset);
    }

    /// <summary>
    ///  Parses binary data into this <see cref="OrderExecutedMessage"/> instance in place, reusing existing field objects.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 45 bytes.</param>
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
    ///  Encodes this <see cref="OrderExecutedMessage"/> into a binary data span starting at the given offset.
    /// </summary>
    /// <param name="data">The destination span. Must be at least offset + 45 bytes.</param>
    /// <param name="offset">The byte offset in data at which to start writing.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data, int offset)
    {
        offset = SaleConditionFlags.Encode(data, offset);
        offset = Timestamp.Encode(data, offset);
        offset = Symbol.Encode(data, offset);
        offset = OrderIdReference.Encode(data, offset);
        offset = Size.Encode(data, offset);
        offset = Price.Encode(data, offset);
        offset = TradeId.Encode(data, offset);
        return offset;
    }

    /// <summary>
    ///  Encodes this <see cref="OrderExecutedMessage"/> into a binary data span starting at offset 0.
    /// </summary>
    /// <param name="data">The destination span. Must be at least 45 bytes.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Returns a <see cref="OrderExecutedMessage"/> with deterministic sample values in every field,
    ///  suitable for round-trip encoding tests.
    /// </summary>
    public static OrderExecutedMessage Sample()
    {
        var msg = new OrderExecutedMessage();
        msg.SaleConditionFlags = SaleConditionFlagsField.Sample(seed: 0);
        msg.Timestamp = TimestampField.Sample(seed: 1);
        msg.Symbol = SymbolField.Sample(seed: 2);
        msg.OrderIdReference = OrderIdReferenceField.Sample(seed: 3);
        msg.Size = SizeField.Sample(seed: 4);
        msg.Price = PriceField.Sample(seed: 5);
        msg.TradeId = TradeIdField.Sample(seed: 6);
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
    ///  Resets all fields of this <see cref="OrderExecutedMessage"/> to their default values and clears the raw backing store.
    /// </summary>
    public void Clear()
    {
        Raw = null;
        IsDecoded = false;
        foreach (var field in Fields) field.Reset();
        Packet = null;
    }

    /// <summary>
    ///  Compares this <see cref="OrderExecutedMessage"/> to another using field-by-field comparison.
    /// </summary>
    public bool Equals(OrderExecutedMessage? other)
    {
        if (other is null) return false;
        if (!SaleConditionFlags.Equals(other.SaleConditionFlags)) return false;
        if (!Timestamp.Equals(other.Timestamp)) return false;
        if (!Symbol.Equals(other.Symbol)) return false;
        if (!OrderIdReference.Equals(other.OrderIdReference)) return false;
        if (!Size.Equals(other.Size)) return false;
        if (!Price.Equals(other.Price)) return false;
        if (!TradeId.Equals(other.TradeId)) return false;
        return true;
    }

    /// <summary>
    ///  Compares two <see cref="OrderExecutedMessage"/> instances field by field.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is OrderExecutedMessage other && Equals(other);
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
    ///  Returns a hash code for this <see cref="OrderExecutedMessage"/> by combining the hash of each field and counted-group container.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(SaleConditionFlags);
        hash.Add(Timestamp);
        hash.Add(Symbol);
        hash.Add(OrderIdReference);
        hash.Add(Size);
        hash.Add(Price);
        hash.Add(TradeId);
        return hash.ToHashCode();
    }

    public static bool operator ==(OrderExecutedMessage? a, OrderExecutedMessage? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(OrderExecutedMessage? a, OrderExecutedMessage? b) => !(a == b);

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
            if (Has(0)) { if (!first) sb.Append(", "); sb.Append("SaleConditionFlags"); first = false; }
            if (Has(1)) { if (!first) sb.Append(", "); sb.Append("Timestamp"); first = false; }
            if (Has(2)) { if (!first) sb.Append(", "); sb.Append("Symbol"); first = false; }
            if (Has(3)) { if (!first) sb.Append(", "); sb.Append("OrderIdReference"); first = false; }
            if (Has(4)) { if (!first) sb.Append(", "); sb.Append("Size"); first = false; }
            if (Has(5)) { if (!first) sb.Append(", "); sb.Append("Price"); first = false; }
            if (Has(6)) { if (!first) sb.Append(", "); sb.Append("TradeId"); first = false; }
            return sb.ToString();
        }
    }

    /// <summary>Compares two messages field by field and returns a <c>Changes</c> value identifying which fields and counted-group containers differ.</summary>
    public Changes Diff(OrderExecutedMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (!SaleConditionFlags.Equals(other.SaleConditionFlags)) diff = diff.With(0);
        if (!Timestamp.Equals(other.Timestamp)) diff = diff.With(1);
        if (!Symbol.Equals(other.Symbol)) diff = diff.With(2);
        if (!OrderIdReference.Equals(other.OrderIdReference)) diff = diff.With(3);
        if (!Size.Equals(other.Size)) diff = diff.With(4);
        if (!Price.Equals(other.Price)) diff = diff.With(5);
        if (!TradeId.Equals(other.TradeId)) diff = diff.With(6);
        return diff;
    }

    /// <summary>Enumerates scalar fields that differ between this message and another. The other message must be the same type. Counted-group container changes are visible via Diff() and DiffReport() but not enumerated here.</summary>
    public IEnumerable<Field> DiffFields(IMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other is not OrderExecutedMessage typed) throw new ArgumentException("DiffFields requires same message type", nameof(other));
        var changes = Diff(typed);
        const ulong FieldMaskW0 = 0x000000000000007FUL;
        var word0 = changes.Word(0) & FieldMaskW0;
        while (word0 != 0)
        {
            var bit = (0 * 64) + System.Numerics.BitOperations.TrailingZeroCount(word0);
            yield return GetField((FieldName)bit);
            word0 &= word0 - 1;
        }
    }

    /// <summary>Enumerates changed fields and counted-group containers as (Name, Before, After) tuples. The other message must be the same message type.</summary>
    public IEnumerable<(string Name, string Before, string After)> DiffReport(OrderExecutedMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var changes = Diff(other);
        if (changes.IsNone) yield break;
        if (changes.Has(0)) yield return (SaleConditionFlags.Name, SaleConditionFlags.ToFormattedString(), other.SaleConditionFlags.ToFormattedString());
        if (changes.Has(1)) yield return (Timestamp.Name, Timestamp.ToFormattedString(), other.Timestamp.ToFormattedString());
        if (changes.Has(2)) yield return (Symbol.Name, Symbol.ToFormattedString(), other.Symbol.ToFormattedString());
        if (changes.Has(3)) yield return (OrderIdReference.Name, OrderIdReference.ToFormattedString(), other.OrderIdReference.ToFormattedString());
        if (changes.Has(4)) yield return (Size.Name, Size.ToFormattedString(), other.Size.ToFormattedString());
        if (changes.Has(5)) yield return (Price.Name, Price.ToFormattedString(), other.Price.ToFormattedString());
        if (changes.Has(6)) yield return (TradeId.Name, TradeId.ToFormattedString(), other.TradeId.ToFormattedString());
    }

    /// <summary>Enumerates all fields in declaration order.</summary>
    public IEnumerable<Field> Fields
    {
        get
        {
            yield return SaleConditionFlags;
            yield return Timestamp;
            yield return Symbol;
            yield return OrderIdReference;
            yield return Size;
            yield return Price;
            yield return TradeId;
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
        FieldName.SaleConditionFlags => SaleConditionFlags,
        FieldName.Timestamp => Timestamp,
        FieldName.Symbol => Symbol,
        FieldName.OrderIdReference => OrderIdReference,
        FieldName.Size => Size,
        FieldName.Price => Price,
        FieldName.TradeId => TradeId,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    /// <summary>
    ///  Appends a human-readable representation of all fields in this <see cref="OrderExecutedMessage"/> to <paramref name="builder"/>,
    ///  indented according to <paramref name="options"/>.
    /// </summary>
    public void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        var indent = options.Prefix;
        builder.Append(indent).AppendLine("OrderExecutedMessage:");
        foreach (var f in Fields)
        {
            builder.Append(indent).Append("  ").Append(f.Name).Append(": ");
            f.ToFormattedString(builder, options);
            builder.AppendLine();
        }
    }

    /// <summary>
    ///  Returns a human-readable representation of all fields in this <see cref="OrderExecutedMessage"/>,
    ///  indented according to <paramref name="options"/>. When <paramref name="options"/> is
    ///  <see cref="PrintOptions.Identifier"/>, returns a compact one-line summary.
    ///  When <paramref name="options"/> is <see cref="PrintOptions.Compact"/>, returns a one-line
    ///  summary with all fields.
    /// </summary>
    public string ToFormattedString(PrintOptions options = default)
    {
        if (options.IsIdentifier)
            return $"[{MessageType}] [{SaleConditionFlags.Value}] [{Timestamp.Value}] [{Symbol.Value}]";
        if (options.IsCompact)
        {
            var sb = new StringBuilder();
            sb.Append("OrderExecutedMessage { ");
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
    ///  Returns a compact one-line representation of all fields in this <see cref="OrderExecutedMessage"/>.
    /// </summary>
    public string ToCompact() => ToFormattedString(PrintOptions.Compact);

    /// <summary>
    ///  Returns a human-readable representation of this <see cref="OrderExecutedMessage"/>.
    /// </summary>
    public override string ToString() => ToFormattedString();
}
