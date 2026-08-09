using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Order ID of the referenced order
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class OrderIdReferenceField : Field, IEquatable<OrderIdReferenceField>, IComparable<OrderIdReferenceField>
{
    public const int ByteLength = 8;
    public const bool IsLittleEndian = true;

    public override string Name => "OrderIdReference";

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

    public void CopyFrom(OrderIdReferenceField other)
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

    public static OrderIdReferenceField Sample(int seed = 0) => new() { Value = (ulong)(seed % 1000000 + 1) };

    public bool Equals(OrderIdReferenceField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is OrderIdReferenceField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(OrderIdReferenceField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(OrderIdReferenceField? a, OrderIdReferenceField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(OrderIdReferenceField? a, OrderIdReferenceField? b) => !(a == b);

    public static implicit operator ulong(OrderIdReferenceField field) => field.Value;
    public static implicit operator OrderIdReferenceField(ulong value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(OrderIdReferenceField? a, ulong b) => a is not null && a.Value == b;
    public static bool operator ==(ulong a, OrderIdReferenceField? b) => b is not null && b.Value == a;
    public static bool operator !=(OrderIdReferenceField? a, ulong b) => !(a == b);
    public static bool operator !=(ulong a, OrderIdReferenceField? b) => !(a == b);

    public static bool operator <(OrderIdReferenceField? a, ulong b) => a is not null && a.Value < b;
    public static bool operator <(ulong a, OrderIdReferenceField? b) => b is not null && a < b.Value;
    public static bool operator >(OrderIdReferenceField? a, ulong b) => a is not null && a.Value > b;
    public static bool operator >(ulong a, OrderIdReferenceField? b) => b is not null && a > b.Value;
    public static bool operator <=(OrderIdReferenceField? a, ulong b) => a is not null && a.Value <= b;
    public static bool operator <=(ulong a, OrderIdReferenceField? b) => b is not null && a <= b.Value;
    public static bool operator >=(OrderIdReferenceField? a, ulong b) => a is not null && a.Value >= b;
    public static bool operator >=(ulong a, OrderIdReferenceField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X16");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
