using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  IEX Generated Identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class TradeIdField : Field, IEquatable<TradeIdField>, IComparable<TradeIdField>
{
    public const int ByteLength = 8;
    public const bool IsLittleEndian = true;

    public override string Name => "TradeId";

    private ulong value;
    public ulong Value
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

    public void CopyFrom(TradeIdField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(offset, ByteLength));
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(offset, ByteLength), Value);
        return offset + ByteLength;
    }

    public static TradeIdField Sample(int seed = 0) => new() { Value = (ulong)(seed % 1000000 + 1) };

    public bool Equals(TradeIdField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is TradeIdField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(TradeIdField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(TradeIdField? a, TradeIdField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(TradeIdField? a, TradeIdField? b) => !(a == b);

    public static implicit operator ulong(TradeIdField field) => field.Value;
    public static implicit operator TradeIdField(ulong value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(TradeIdField? a, ulong b) => a is not null && a.Value == b;
    public static bool operator ==(ulong a, TradeIdField? b) => b is not null && b.Value == a;
    public static bool operator !=(TradeIdField? a, ulong b) => !(a == b);
    public static bool operator !=(ulong a, TradeIdField? b) => !(a == b);

    public static bool operator <(TradeIdField? a, ulong b) => a is not null && a.Value < b;
    public static bool operator <(ulong a, TradeIdField? b) => b is not null && a < b.Value;
    public static bool operator >(TradeIdField? a, ulong b) => a is not null && a.Value > b;
    public static bool operator >(ulong a, TradeIdField? b) => b is not null && a > b.Value;
    public static bool operator <=(TradeIdField? a, ulong b) => a is not null && a.Value <= b;
    public static bool operator <=(ulong a, TradeIdField? b) => b is not null && a <= b.Value;
    public static bool operator >=(TradeIdField? a, ulong b) => a is not null && a.Value >= b;
    public static bool operator >=(ulong a, TradeIdField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X16");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
