using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Quote Update Flags
/// </summary>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class QuoteUpdateFlagsField : Field, IEquatable<QuoteUpdateFlagsField>
{
    public const int ByteLength = 1;

    public override string Name => "QuoteUpdateFlags";

    private byte value;
    public byte Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte MarketSessionMask = unchecked((byte)(1UL << 6));

    /// <summary>
    ///  Market Session Flag
    /// </summary>
    public bool MarketSession => (Value & MarketSessionMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte SymbolAvailabilityMask = unchecked((byte)(1UL << 7));

    /// <summary>
    ///  Symbol is halted, paused, or otherwise not available for trading on IEX
    /// </summary>
    public bool SymbolAvailability => (Value & SymbolAvailabilityMask) != 0;

    public override FieldKind Kind => FieldKind.Bitfield;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(QuoteUpdateFlagsField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = Value;
        return offset + ByteLength;
    }

    public static QuoteUpdateFlagsField Sample(int seed = 0) => new() { Value = (byte)0x55 };

    public bool Equals(QuoteUpdateFlagsField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is QuoteUpdateFlagsField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(QuoteUpdateFlagsField? a, QuoteUpdateFlagsField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(QuoteUpdateFlagsField? a, QuoteUpdateFlagsField? b) => !(a == b);

    public static implicit operator byte(QuoteUpdateFlagsField field) => field.Value;
    public static implicit operator QuoteUpdateFlagsField(byte value) => new() { Value = value };

    public static bool operator ==(QuoteUpdateFlagsField? a, byte b) => a is not null && a.Value == b;
    public static bool operator ==(byte a, QuoteUpdateFlagsField? b) => b is not null && b.Value == a;
    public static bool operator !=(QuoteUpdateFlagsField? a, byte b) => !(a == b);
    public static bool operator !=(byte a, QuoteUpdateFlagsField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        builder.Append("0x").Append(Value.ToString("X2"));
        bool first = true;
        if (MarketSession) { builder.Append(first ? " (" : ", "); builder.Append("MarketSession"); first = false; }
        if (SymbolAvailability) { builder.Append(first ? " (" : ", "); builder.Append("SymbolAvailability"); first = false; }
        if (!first) builder.Append(')');
    }

    public override string ToFormattedString(PrintOptions options = default)
    {
        var builder = new StringBuilder();
        ToFormattedString(builder, options);
        return builder.ToString();
    }

    public override string ToString() => ToFormattedString();
}
