using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the AuctionInformationMessage from the Tops protocol.
/// </summary>
/// <remarks>
///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
/// </remarks>
[DebuggerDisplay("{ToFormattedString(PrintOptions.Identifier),nq}")]
public sealed class AuctionInformationMessage : IMessage, IEquatable<AuctionInformationMessage>
{
    /// <summary>
    ///  Message type identifier.
    /// </summary>
    public const char MessageType = 'A';

    /// <summary>
    ///  Total fixed byte length of this message.
    /// </summary>
    private const int MessageByteLength = 79;

    /// <summary>
    ///  Auction type identifier
    /// </summary>
    public AuctionTypeField AuctionType { get; set; } = new();

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public TimestampField Timestamp { get; set; } = new();

    /// <summary>
    ///  Security identifier
    /// </summary>
    public SymbolField Symbol { get; set; } = new();

    /// <summary>
    ///  Number of shares paired at the Reference Price using orders on the Auction Book
    /// </summary>
    public PairedSharesField PairedShares { get; set; } = new();

    /// <summary>
    ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
    /// </summary>
    public ReferencePriceField ReferencePrice { get; set; } = new();

    /// <summary>
    ///  Clearing price using Eligible Auction Orders
    /// </summary>
    public IndicativeClearingPriceField IndicativeClearingPrice { get; set; } = new();

    /// <summary>
    ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public ImbalanceSharesField ImbalanceShares { get; set; } = new();

    /// <summary>
    ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public ImbalanceSideField ImbalanceSide { get; set; } = new();

    /// <summary>
    ///  Number of extensions an auction received
    /// </summary>
    public ExtensionNumberField ExtensionNumber { get; set; } = new();

    /// <summary>
    ///  Projected time of the auction match
    /// </summary>
    public ScheduledAuctionTimeField ScheduledAuctionTime { get; set; } = new();

    /// <summary>
    ///  Clearing price using orders on the Auction Book
    /// </summary>
    public AuctionBookClearingPriceField AuctionBookClearingPrice { get; set; } = new();

    /// <summary>
    ///  Reference priced used for the auction collar, if any
    /// </summary>
    public CollarReferencePriceField CollarReferencePrice { get; set; } = new();

    /// <summary>
    ///  Lower threshold price of the auction collar, if any
    /// </summary>
    public LowerAuctionCollarField LowerAuctionCollar { get; set; } = new();

    /// <summary>
    ///  Upper threshold price of the auction collar, if any
    /// </summary>
    public UpperAuctionCollarField UpperAuctionCollar { get; set; } = new();

    /// <summary>
    ///  The message type character constant, or '\0' if no constant is defined.
    /// </summary>
    public char Type => MessageType;

    /// <summary>
    ///  The total fixed byte length of this message (79 bytes).
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
        AuctionType,
        Timestamp,
        Symbol,
        PairedShares,
        ReferencePrice,
        IndicativeClearingPrice,
        ImbalanceShares,
        ImbalanceSide,
        ExtensionNumber,
        ScheduledAuctionTime,
        AuctionBookClearingPrice,
        CollarReferencePrice,
        LowerAuctionCollar,
        UpperAuctionCollar,
    }

    /// <summary>
    ///  Initializes a new <see cref="AuctionInformationMessage"/> with all fields at their default values.
    /// </summary>
    public AuctionInformationMessage() { }

    /// <summary>
    ///  Initializes a new <see cref="AuctionInformationMessage"/> from an existing instance, copying all field values.
    /// </summary>
    /// <param name="other">The source instance to copy from.</param>
    public AuctionInformationMessage(AuctionInformationMessage other) : this()
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other.Raw is { } raw) Raw = raw.ToArray();
        IsDecoded = other.IsDecoded;
        AuctionType.CopyFrom(other.AuctionType);
        Timestamp.CopyFrom(other.Timestamp);
        Symbol.CopyFrom(other.Symbol);
        PairedShares.CopyFrom(other.PairedShares);
        ReferencePrice.CopyFrom(other.ReferencePrice);
        IndicativeClearingPrice.CopyFrom(other.IndicativeClearingPrice);
        ImbalanceShares.CopyFrom(other.ImbalanceShares);
        ImbalanceSide.CopyFrom(other.ImbalanceSide);
        ExtensionNumber.CopyFrom(other.ExtensionNumber);
        ScheduledAuctionTime.CopyFrom(other.ScheduledAuctionTime);
        AuctionBookClearingPrice.CopyFrom(other.AuctionBookClearingPrice);
        CollarReferencePrice.CopyFrom(other.CollarReferencePrice);
        LowerAuctionCollar.CopyFrom(other.LowerAuctionCollar);
        UpperAuctionCollar.CopyFrom(other.UpperAuctionCollar);
    }

    /// <summary>
    ///  Parses a <see cref="AuctionInformationMessage"/> from a binary data span at the given offset.
    ///  On return, offset has been advanced past the bytes consumed by this message.
    ///  If any field fails to parse (insufficient bytes), parsing stops and IsDecoded remains false.
    /// </summary>
    /// <param name="data">The raw message bytes.</param>
    /// <param name="offset">The starting offset into data; advanced on return.</param>
    public AuctionInformationMessage(ReadOnlySpan<byte> data, ref int offset)
    {
        var start = offset;
        var decoded = true;
        foreach (var field in Fields)
            if (!field.Parse(data, ref offset)) { decoded = false; break; }
        Raw = data[start..offset].ToArray();
        IsDecoded = decoded;
    }

    /// <summary>
    ///  Parses a <see cref="AuctionInformationMessage"/> from a binary data span.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 79 bytes.</param>
    /// <returns>A new <see cref="AuctionInformationMessage"/> with all fields populated from the binary data.</returns>
    public static AuctionInformationMessage Parse(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        return new AuctionInformationMessage(data, ref offset);
    }

    /// <summary>
    ///  Parses binary data into this <see cref="AuctionInformationMessage"/> instance in place, reusing existing field objects.
    /// </summary>
    /// <param name="data">The raw message bytes. Must contain at least 79 bytes.</param>
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
    ///  Encodes this <see cref="AuctionInformationMessage"/> into a binary data span starting at the given offset.
    /// </summary>
    /// <param name="data">The destination span. Must be at least offset + 79 bytes.</param>
    /// <param name="offset">The byte offset in data at which to start writing.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data, int offset)
    {
        offset = AuctionType.Encode(data, offset);
        offset = Timestamp.Encode(data, offset);
        offset = Symbol.Encode(data, offset);
        offset = PairedShares.Encode(data, offset);
        offset = ReferencePrice.Encode(data, offset);
        offset = IndicativeClearingPrice.Encode(data, offset);
        offset = ImbalanceShares.Encode(data, offset);
        offset = ImbalanceSide.Encode(data, offset);
        offset = ExtensionNumber.Encode(data, offset);
        offset = ScheduledAuctionTime.Encode(data, offset);
        offset = AuctionBookClearingPrice.Encode(data, offset);
        offset = CollarReferencePrice.Encode(data, offset);
        offset = LowerAuctionCollar.Encode(data, offset);
        offset = UpperAuctionCollar.Encode(data, offset);
        return offset;
    }

    /// <summary>
    ///  Encodes this <see cref="AuctionInformationMessage"/> into a binary data span starting at offset 0.
    /// </summary>
    /// <param name="data">The destination span. Must be at least 79 bytes.</param>
    /// <returns>The offset after the last written byte.</returns>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Returns a <see cref="AuctionInformationMessage"/> with deterministic sample values in every field,
    ///  suitable for round-trip encoding tests.
    /// </summary>
    public static AuctionInformationMessage Sample()
    {
        var msg = new AuctionInformationMessage();
        msg.AuctionType = AuctionTypeField.Sample(seed: 0);
        msg.Timestamp = TimestampField.Sample(seed: 1);
        msg.Symbol = SymbolField.Sample(seed: 2);
        msg.PairedShares = PairedSharesField.Sample(seed: 3);
        msg.ReferencePrice = ReferencePriceField.Sample(seed: 4);
        msg.IndicativeClearingPrice = IndicativeClearingPriceField.Sample(seed: 5);
        msg.ImbalanceShares = ImbalanceSharesField.Sample(seed: 6);
        msg.ImbalanceSide = ImbalanceSideField.Sample(seed: 7);
        msg.ExtensionNumber = ExtensionNumberField.Sample(seed: 8);
        msg.ScheduledAuctionTime = ScheduledAuctionTimeField.Sample(seed: 9);
        msg.AuctionBookClearingPrice = AuctionBookClearingPriceField.Sample(seed: 10);
        msg.CollarReferencePrice = CollarReferencePriceField.Sample(seed: 11);
        msg.LowerAuctionCollar = LowerAuctionCollarField.Sample(seed: 12);
        msg.UpperAuctionCollar = UpperAuctionCollarField.Sample(seed: 13);
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
    ///  Resets all fields of this <see cref="AuctionInformationMessage"/> to their default values and clears the raw backing store.
    /// </summary>
    public void Clear()
    {
        Raw = null;
        IsDecoded = false;
        foreach (var field in Fields) field.Reset();
        Packet = null;
    }

    /// <summary>
    ///  Compares this <see cref="AuctionInformationMessage"/> to another using field-by-field comparison.
    /// </summary>
    public bool Equals(AuctionInformationMessage? other)
    {
        if (other is null) return false;
        if (!AuctionType.Equals(other.AuctionType)) return false;
        if (!Timestamp.Equals(other.Timestamp)) return false;
        if (!Symbol.Equals(other.Symbol)) return false;
        if (!PairedShares.Equals(other.PairedShares)) return false;
        if (!ReferencePrice.Equals(other.ReferencePrice)) return false;
        if (!IndicativeClearingPrice.Equals(other.IndicativeClearingPrice)) return false;
        if (!ImbalanceShares.Equals(other.ImbalanceShares)) return false;
        if (!ImbalanceSide.Equals(other.ImbalanceSide)) return false;
        if (!ExtensionNumber.Equals(other.ExtensionNumber)) return false;
        if (!ScheduledAuctionTime.Equals(other.ScheduledAuctionTime)) return false;
        if (!AuctionBookClearingPrice.Equals(other.AuctionBookClearingPrice)) return false;
        if (!CollarReferencePrice.Equals(other.CollarReferencePrice)) return false;
        if (!LowerAuctionCollar.Equals(other.LowerAuctionCollar)) return false;
        if (!UpperAuctionCollar.Equals(other.UpperAuctionCollar)) return false;
        return true;
    }

    /// <summary>
    ///  Compares two <see cref="AuctionInformationMessage"/> instances field by field.
    /// </summary>
    public override bool Equals(object? obj)
    {
        return obj is AuctionInformationMessage other && Equals(other);
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
    ///  Returns a hash code for this <see cref="AuctionInformationMessage"/> by combining the hash of each field and counted-group container.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(AuctionType);
        hash.Add(Timestamp);
        hash.Add(Symbol);
        hash.Add(PairedShares);
        hash.Add(ReferencePrice);
        hash.Add(IndicativeClearingPrice);
        hash.Add(ImbalanceShares);
        hash.Add(ImbalanceSide);
        hash.Add(ExtensionNumber);
        hash.Add(ScheduledAuctionTime);
        hash.Add(AuctionBookClearingPrice);
        hash.Add(CollarReferencePrice);
        hash.Add(LowerAuctionCollar);
        hash.Add(UpperAuctionCollar);
        return hash.ToHashCode();
    }

    public static bool operator ==(AuctionInformationMessage? a, AuctionInformationMessage? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(AuctionInformationMessage? a, AuctionInformationMessage? b) => !(a == b);

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
            if (Has(0)) { if (!first) sb.Append(", "); sb.Append("AuctionType"); first = false; }
            if (Has(1)) { if (!first) sb.Append(", "); sb.Append("Timestamp"); first = false; }
            if (Has(2)) { if (!first) sb.Append(", "); sb.Append("Symbol"); first = false; }
            if (Has(3)) { if (!first) sb.Append(", "); sb.Append("PairedShares"); first = false; }
            if (Has(4)) { if (!first) sb.Append(", "); sb.Append("ReferencePrice"); first = false; }
            if (Has(5)) { if (!first) sb.Append(", "); sb.Append("IndicativeClearingPrice"); first = false; }
            if (Has(6)) { if (!first) sb.Append(", "); sb.Append("ImbalanceShares"); first = false; }
            if (Has(7)) { if (!first) sb.Append(", "); sb.Append("ImbalanceSide"); first = false; }
            if (Has(8)) { if (!first) sb.Append(", "); sb.Append("ExtensionNumber"); first = false; }
            if (Has(9)) { if (!first) sb.Append(", "); sb.Append("ScheduledAuctionTime"); first = false; }
            if (Has(10)) { if (!first) sb.Append(", "); sb.Append("AuctionBookClearingPrice"); first = false; }
            if (Has(11)) { if (!first) sb.Append(", "); sb.Append("CollarReferencePrice"); first = false; }
            if (Has(12)) { if (!first) sb.Append(", "); sb.Append("LowerAuctionCollar"); first = false; }
            if (Has(13)) { if (!first) sb.Append(", "); sb.Append("UpperAuctionCollar"); first = false; }
            return sb.ToString();
        }
    }

    /// <summary>Compares two messages field by field and returns a <c>Changes</c> value identifying which fields and counted-group containers differ.</summary>
    public Changes Diff(AuctionInformationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (!AuctionType.Equals(other.AuctionType)) diff = diff.With(0);
        if (!Timestamp.Equals(other.Timestamp)) diff = diff.With(1);
        if (!Symbol.Equals(other.Symbol)) diff = diff.With(2);
        if (!PairedShares.Equals(other.PairedShares)) diff = diff.With(3);
        if (!ReferencePrice.Equals(other.ReferencePrice)) diff = diff.With(4);
        if (!IndicativeClearingPrice.Equals(other.IndicativeClearingPrice)) diff = diff.With(5);
        if (!ImbalanceShares.Equals(other.ImbalanceShares)) diff = diff.With(6);
        if (!ImbalanceSide.Equals(other.ImbalanceSide)) diff = diff.With(7);
        if (!ExtensionNumber.Equals(other.ExtensionNumber)) diff = diff.With(8);
        if (!ScheduledAuctionTime.Equals(other.ScheduledAuctionTime)) diff = diff.With(9);
        if (!AuctionBookClearingPrice.Equals(other.AuctionBookClearingPrice)) diff = diff.With(10);
        if (!CollarReferencePrice.Equals(other.CollarReferencePrice)) diff = diff.With(11);
        if (!LowerAuctionCollar.Equals(other.LowerAuctionCollar)) diff = diff.With(12);
        if (!UpperAuctionCollar.Equals(other.UpperAuctionCollar)) diff = diff.With(13);
        return diff;
    }

    /// <summary>Enumerates scalar fields that differ between this message and another. The other message must be the same type. Counted-group container changes are visible via Diff() and DiffReport() but not enumerated here.</summary>
    public IEnumerable<Field> DiffFields(IMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (other is not AuctionInformationMessage typed) throw new ArgumentException("DiffFields requires same message type", nameof(other));
        var changes = Diff(typed);
        const ulong FieldMaskW0 = 0x0000000000003FFFUL;
        var word0 = changes.Word(0) & FieldMaskW0;
        while (word0 != 0)
        {
            var bit = (0 * 64) + System.Numerics.BitOperations.TrailingZeroCount(word0);
            yield return GetField((FieldName)bit);
            word0 &= word0 - 1;
        }
    }

    /// <summary>Enumerates changed fields and counted-group containers as (Name, Before, After) tuples. The other message must be the same message type.</summary>
    public IEnumerable<(string Name, string Before, string After)> DiffReport(AuctionInformationMessage other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var changes = Diff(other);
        if (changes.IsNone) yield break;
        if (changes.Has(0)) yield return (AuctionType.Name, AuctionType.ToFormattedString(), other.AuctionType.ToFormattedString());
        if (changes.Has(1)) yield return (Timestamp.Name, Timestamp.ToFormattedString(), other.Timestamp.ToFormattedString());
        if (changes.Has(2)) yield return (Symbol.Name, Symbol.ToFormattedString(), other.Symbol.ToFormattedString());
        if (changes.Has(3)) yield return (PairedShares.Name, PairedShares.ToFormattedString(), other.PairedShares.ToFormattedString());
        if (changes.Has(4)) yield return (ReferencePrice.Name, ReferencePrice.ToFormattedString(), other.ReferencePrice.ToFormattedString());
        if (changes.Has(5)) yield return (IndicativeClearingPrice.Name, IndicativeClearingPrice.ToFormattedString(), other.IndicativeClearingPrice.ToFormattedString());
        if (changes.Has(6)) yield return (ImbalanceShares.Name, ImbalanceShares.ToFormattedString(), other.ImbalanceShares.ToFormattedString());
        if (changes.Has(7)) yield return (ImbalanceSide.Name, ImbalanceSide.ToFormattedString(), other.ImbalanceSide.ToFormattedString());
        if (changes.Has(8)) yield return (ExtensionNumber.Name, ExtensionNumber.ToFormattedString(), other.ExtensionNumber.ToFormattedString());
        if (changes.Has(9)) yield return (ScheduledAuctionTime.Name, ScheduledAuctionTime.ToFormattedString(), other.ScheduledAuctionTime.ToFormattedString());
        if (changes.Has(10)) yield return (AuctionBookClearingPrice.Name, AuctionBookClearingPrice.ToFormattedString(), other.AuctionBookClearingPrice.ToFormattedString());
        if (changes.Has(11)) yield return (CollarReferencePrice.Name, CollarReferencePrice.ToFormattedString(), other.CollarReferencePrice.ToFormattedString());
        if (changes.Has(12)) yield return (LowerAuctionCollar.Name, LowerAuctionCollar.ToFormattedString(), other.LowerAuctionCollar.ToFormattedString());
        if (changes.Has(13)) yield return (UpperAuctionCollar.Name, UpperAuctionCollar.ToFormattedString(), other.UpperAuctionCollar.ToFormattedString());
    }

    /// <summary>Enumerates all fields in declaration order.</summary>
    public IEnumerable<Field> Fields
    {
        get
        {
            yield return AuctionType;
            yield return Timestamp;
            yield return Symbol;
            yield return PairedShares;
            yield return ReferencePrice;
            yield return IndicativeClearingPrice;
            yield return ImbalanceShares;
            yield return ImbalanceSide;
            yield return ExtensionNumber;
            yield return ScheduledAuctionTime;
            yield return AuctionBookClearingPrice;
            yield return CollarReferencePrice;
            yield return LowerAuctionCollar;
            yield return UpperAuctionCollar;
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
        FieldName.AuctionType => AuctionType,
        FieldName.Timestamp => Timestamp,
        FieldName.Symbol => Symbol,
        FieldName.PairedShares => PairedShares,
        FieldName.ReferencePrice => ReferencePrice,
        FieldName.IndicativeClearingPrice => IndicativeClearingPrice,
        FieldName.ImbalanceShares => ImbalanceShares,
        FieldName.ImbalanceSide => ImbalanceSide,
        FieldName.ExtensionNumber => ExtensionNumber,
        FieldName.ScheduledAuctionTime => ScheduledAuctionTime,
        FieldName.AuctionBookClearingPrice => AuctionBookClearingPrice,
        FieldName.CollarReferencePrice => CollarReferencePrice,
        FieldName.LowerAuctionCollar => LowerAuctionCollar,
        FieldName.UpperAuctionCollar => UpperAuctionCollar,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    /// <summary>
    ///  Appends a human-readable representation of all fields in this <see cref="AuctionInformationMessage"/> to <paramref name="builder"/>,
    ///  indented according to <paramref name="options"/>.
    /// </summary>
    public void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        var indent = options.Prefix;
        builder.Append(indent).AppendLine("AuctionInformationMessage:");
        foreach (var f in Fields)
        {
            builder.Append(indent).Append("  ").Append(f.Name).Append(": ");
            f.ToFormattedString(builder, options);
            builder.AppendLine();
        }
    }

    /// <summary>
    ///  Returns a human-readable representation of all fields in this <see cref="AuctionInformationMessage"/>,
    ///  indented according to <paramref name="options"/>. When <paramref name="options"/> is
    ///  <see cref="PrintOptions.Identifier"/>, returns a compact one-line summary.
    ///  When <paramref name="options"/> is <see cref="PrintOptions.Compact"/>, returns a one-line
    ///  summary with all fields.
    /// </summary>
    public string ToFormattedString(PrintOptions options = default)
    {
        if (options.IsIdentifier)
            return $"[{MessageType}] [{AuctionType.Value}] [{Timestamp.Value}] [{Symbol.Value}]";
        if (options.IsCompact)
        {
            var sb = new StringBuilder();
            sb.Append("AuctionInformationMessage { ");
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
    ///  Returns a compact one-line representation of all fields in this <see cref="AuctionInformationMessage"/>.
    /// </summary>
    public string ToCompact() => ToFormattedString(PrintOptions.Compact);

    /// <summary>
    ///  Returns a human-readable representation of this <see cref="AuctionInformationMessage"/>.
    /// </summary>
    public override string ToString() => ToFormattedString();
}
