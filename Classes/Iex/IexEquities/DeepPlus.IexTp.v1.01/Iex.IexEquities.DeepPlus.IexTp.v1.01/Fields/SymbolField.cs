using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Security identifier
/// </summary>
/// <remarks>
///  8-byte ASCII field, right-padded with spaces. Trailing padding is stripped.
/// </remarks>
[DebuggerDisplay("{Value,nq}")]
public sealed class SymbolField : Field, IEquatable<SymbolField>, IComparable<SymbolField>
{
    public const int ByteLength = 8;
    public const byte StringPaddingByte = (byte)' ';

    public override string Name => "Symbol";

    private string value = string.Empty;
    public string Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    public override FieldKind Kind => FieldKind.String;

    public override void Reset()
    {
        value = string.Empty;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(SymbolField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = Encoding.ASCII.GetString(data.Slice(offset, ByteLength).TrimEnd(StringPaddingByte));
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        var slice = data.Slice(offset, ByteLength);
        slice.Fill(StringPaddingByte);
        Encoding.ASCII.GetBytes(Value, slice);
        return offset + ByteLength;
    }

    public static SymbolField Sample(int seed = 0) => new() { Value = new string((char)('A' + seed % 26), ByteLength) };

    public bool Equals(SymbolField? other) => other is not null && string.Equals(Value, other.Value, StringComparison.Ordinal);
    public override bool Equals(object? obj) => obj is SymbolField other && Equals(other);
    public override int GetHashCode() => StringComparer.Ordinal.GetHashCode(Value);

    public int CompareTo(SymbolField? other) => other is null ? 1 : string.Compare(Value, other.Value, StringComparison.Ordinal);

    public static bool operator ==(SymbolField? a, SymbolField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SymbolField? a, SymbolField? b) => !(a == b);

    public static implicit operator string(SymbolField field) => field.Value;
    public static implicit operator SymbolField(string value) => new() { Value = value };
    public override string ToString() => Value;

    public static bool operator ==(SymbolField? a, string b) => a is not null && string.Equals(a.Value, b, StringComparison.Ordinal);
    public static bool operator ==(string a, SymbolField? b) => b is not null && string.Equals(a, b.Value, StringComparison.Ordinal);
    public static bool operator !=(SymbolField? a, string b) => !(a == b);
    public static bool operator !=(string a, SymbolField? b) => !(a == b);

    public string ToHex()
    {
        var buf = new byte[ByteLength];
        buf.AsSpan().Fill(StringPaddingByte);
        Encoding.ASCII.GetBytes(Value.AsSpan(0, Math.Min(Value.Length, ByteLength)), buf);
        return Convert.ToHexString(buf);
    }

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value);
    public override string ToFormattedString(PrintOptions options = default) => Value;
}
