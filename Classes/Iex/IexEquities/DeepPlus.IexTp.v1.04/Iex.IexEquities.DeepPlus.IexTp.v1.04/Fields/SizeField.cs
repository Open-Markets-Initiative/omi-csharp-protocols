using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Quoted size
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class SizeField : Field, IEquatable<SizeField>, IComparable<SizeField>
{
    public const int ByteLength = 4;
    public const bool IsLittleEndian = true;

    public override string Name => "Size";

    private uint value;
    public uint Value
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

    public void CopyFrom(SizeField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, ByteLength));
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(offset, ByteLength), Value);
        return offset + ByteLength;
    }

    public static SizeField Sample(int seed = 0) => new() { Value = (uint)(seed % 2147483646 + 1) };

    public bool Equals(SizeField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SizeField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(SizeField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(SizeField? a, SizeField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SizeField? a, SizeField? b) => !(a == b);

    public static implicit operator uint(SizeField field) => field.Value;
    public static implicit operator SizeField(uint value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(SizeField? a, uint b) => a is not null && a.Value == b;
    public static bool operator ==(uint a, SizeField? b) => b is not null && b.Value == a;
    public static bool operator !=(SizeField? a, uint b) => !(a == b);
    public static bool operator !=(uint a, SizeField? b) => !(a == b);

    public static bool operator <(SizeField? a, uint b) => a is not null && a.Value < b;
    public static bool operator <(uint a, SizeField? b) => b is not null && a < b.Value;
    public static bool operator >(SizeField? a, uint b) => a is not null && a.Value > b;
    public static bool operator >(uint a, SizeField? b) => b is not null && a > b.Value;
    public static bool operator <=(SizeField? a, uint b) => a is not null && a.Value <= b;
    public static bool operator <=(uint a, SizeField? b) => b is not null && a <= b.Value;
    public static bool operator >=(SizeField? a, uint b) => a is not null && a.Value >= b;
    public static bool operator >=(uint a, SizeField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X8");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
