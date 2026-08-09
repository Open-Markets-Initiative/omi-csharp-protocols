using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Number of extensions an auction received
/// </summary>
/// <remarks>
///  Single ASCII character stored as one byte.
/// </remarks>
[DebuggerDisplay("{Value}")]
public sealed class ExtensionNumberField : Field, IEquatable<ExtensionNumberField>, IComparable<ExtensionNumberField>
{
    public const int ByteLength = 1;

    public override string Name => "ExtensionNumber";

    private char value;
    public char Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    public override FieldKind Kind => FieldKind.Char;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(ExtensionNumberField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (char)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static ExtensionNumberField Sample(int seed = 0) => new() { Value = (char)('A' + seed % 26) };

    public bool Equals(ExtensionNumberField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ExtensionNumberField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(ExtensionNumberField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(ExtensionNumberField? a, ExtensionNumberField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(ExtensionNumberField? a, ExtensionNumberField? b) => !(a == b);

    public static implicit operator char(ExtensionNumberField field) => field.Value;
    public static implicit operator ExtensionNumberField(char value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(ExtensionNumberField? a, char b) => a is not null && a.Value == b;
    public static bool operator ==(char a, ExtensionNumberField? b) => b is not null && b.Value == a;
    public static bool operator !=(ExtensionNumberField? a, char b) => !(a == b);
    public static bool operator !=(char a, ExtensionNumberField? b) => !(a == b);

    public static bool operator <(ExtensionNumberField? a, char b) => a is not null && a.Value < b;
    public static bool operator <(char a, ExtensionNumberField? b) => b is not null && a < b.Value;
    public static bool operator >(ExtensionNumberField? a, char b) => a is not null && a.Value > b;
    public static bool operator >(char a, ExtensionNumberField? b) => b is not null && a > b.Value;
    public static bool operator <=(ExtensionNumberField? a, char b) => a is not null && a.Value <= b;
    public static bool operator <=(char a, ExtensionNumberField? b) => b is not null && a <= b.Value;
    public static bool operator >=(ExtensionNumberField? a, char b) => a is not null && a.Value >= b;
    public static bool operator >=(char a, ExtensionNumberField? b) => b is not null && a >= b.Value;

    public string ToHex() => ((byte)Value).ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value);
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
