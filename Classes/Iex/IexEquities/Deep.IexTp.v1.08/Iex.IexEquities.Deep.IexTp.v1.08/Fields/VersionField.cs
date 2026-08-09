using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Version of transport specification
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class VersionField : Field, IEquatable<VersionField>, IComparable<VersionField>
{
    public const int ByteLength = 1;

    public override string Name => "Version";

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

    public override FieldKind Kind => FieldKind.UnsignedInteger;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(VersionField other)
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

    public static VersionField Sample(int seed = 0) => new() { Value = (byte)(seed % 255 + 1) };

    public bool Equals(VersionField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is VersionField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(VersionField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(VersionField? a, VersionField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(VersionField? a, VersionField? b) => !(a == b);

    public static implicit operator byte(VersionField field) => field.Value;
    public static implicit operator VersionField(byte value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(VersionField? a, byte b) => a is not null && a.Value == b;
    public static bool operator ==(byte a, VersionField? b) => b is not null && b.Value == a;
    public static bool operator !=(VersionField? a, byte b) => !(a == b);
    public static bool operator !=(byte a, VersionField? b) => !(a == b);

    public static bool operator <(VersionField? a, byte b) => a is not null && a.Value < b;
    public static bool operator <(byte a, VersionField? b) => b is not null && a < b.Value;
    public static bool operator >(VersionField? a, byte b) => a is not null && a.Value > b;
    public static bool operator >(byte a, VersionField? b) => b is not null && a > b.Value;
    public static bool operator <=(VersionField? a, byte b) => a is not null && a.Value <= b;
    public static bool operator <=(byte a, VersionField? b) => b is not null && a <= b.Value;
    public static bool operator >=(VersionField? a, byte b) => a is not null && a.Value >= b;
    public static bool operator >=(byte a, VersionField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
