using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Projected time of the auction match
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class ScheduledAuctionTimeField : Field, IEquatable<ScheduledAuctionTimeField>, IComparable<ScheduledAuctionTimeField>
{
    public const int ByteLength = 4;
    public const bool IsLittleEndian = true;

    public override string Name => "ScheduledAuctionTime";

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

    public void CopyFrom(ScheduledAuctionTimeField other)
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

    public static ScheduledAuctionTimeField Sample(int seed = 0) => new() { Value = (uint)(seed % 2147483646 + 1) };

    public bool Equals(ScheduledAuctionTimeField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ScheduledAuctionTimeField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(ScheduledAuctionTimeField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(ScheduledAuctionTimeField? a, ScheduledAuctionTimeField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(ScheduledAuctionTimeField? a, ScheduledAuctionTimeField? b) => !(a == b);

    public static implicit operator uint(ScheduledAuctionTimeField field) => field.Value;
    public static implicit operator ScheduledAuctionTimeField(uint value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(ScheduledAuctionTimeField? a, uint b) => a is not null && a.Value == b;
    public static bool operator ==(uint a, ScheduledAuctionTimeField? b) => b is not null && b.Value == a;
    public static bool operator !=(ScheduledAuctionTimeField? a, uint b) => !(a == b);
    public static bool operator !=(uint a, ScheduledAuctionTimeField? b) => !(a == b);

    public static bool operator <(ScheduledAuctionTimeField? a, uint b) => a is not null && a.Value < b;
    public static bool operator <(uint a, ScheduledAuctionTimeField? b) => b is not null && a < b.Value;
    public static bool operator >(ScheduledAuctionTimeField? a, uint b) => a is not null && a.Value > b;
    public static bool operator >(uint a, ScheduledAuctionTimeField? b) => b is not null && a > b.Value;
    public static bool operator <=(ScheduledAuctionTimeField? a, uint b) => a is not null && a.Value <= b;
    public static bool operator <=(uint a, ScheduledAuctionTimeField? b) => b is not null && a <= b.Value;
    public static bool operator >=(ScheduledAuctionTimeField? a, uint b) => a is not null && a.Value >= b;
    public static bool operator >=(uint a, ScheduledAuctionTimeField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X8");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
