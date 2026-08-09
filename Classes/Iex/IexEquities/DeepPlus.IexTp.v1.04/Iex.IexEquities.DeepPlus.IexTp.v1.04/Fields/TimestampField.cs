using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Time stamp of the system event
/// </summary>
/// <remarks>
///  Nanoseconds since Unix epoch, converted to DateTime with tick precision.
/// </remarks>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class TimestampField : Field, IEquatable<TimestampField>, IComparable<TimestampField>
{
    public const int ByteLength = 8;
    public const bool IsLittleEndian = true;

    public override string Name => "Timestamp";

    /// <summary>
    ///  The raw nanosecond value as read from the wire, before DateTime conversion.
    /// </summary>
    public long RawValue { get; private set; }

    private DateTime value;
    public DateTime Value
    {
        get => value;
        set
        {
            this.value = value;
            RawValue = (value - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick;
            Raw = null;
            IsDecoded = false;
        }
    }

    public override FieldKind Kind => FieldKind.DateTime;

    public override void Reset()
    {
        value = default;
        RawValue = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(TimestampField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        RawValue = other.RawValue;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        RawValue = BinaryPrimitives.ReadInt64LittleEndian(data.Slice(offset, ByteLength));
        value = DateTime.UnixEpoch.AddTicks(RawValue / TimeSpan.NanosecondsPerTick);
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(offset, ByteLength), RawValue);
        return offset + ByteLength;
    }

    public static TimestampField Sample(int seed = 0) => new() { Value = DateTime.UnixEpoch.AddTicks(123_456_789L + seed * 1_000_000L) };

    public bool Equals(TimestampField? other) => other is not null && RawValue == other.RawValue;
    public override bool Equals(object? obj) => obj is TimestampField other && Equals(other);
    public override int GetHashCode() => RawValue.GetHashCode();

    public int CompareTo(TimestampField? other) => other is null ? 1 : RawValue.CompareTo(other.RawValue);

    public static bool operator ==(TimestampField? a, TimestampField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(TimestampField? a, TimestampField? b) => !(a == b);

    public static implicit operator DateTime(TimestampField field) => field.Value;
    public static implicit operator TimestampField(DateTime value) => new() { Value = value };

    public static bool operator ==(TimestampField? a, DateTime b) => a is not null && a.Value == b;
    public static bool operator ==(DateTime a, TimestampField? b) => b is not null && b.Value == a;
    public static bool operator !=(TimestampField? a, DateTime b) => !(a == b);
    public static bool operator !=(DateTime a, TimestampField? b) => !(a == b);

    public static bool operator <(TimestampField? a, DateTime b) => a is not null && a.Value < b;
    public static bool operator <(DateTime a, TimestampField? b) => b is not null && a < b.Value;
    public static bool operator >(TimestampField? a, DateTime b) => a is not null && a.Value > b;
    public static bool operator >(DateTime a, TimestampField? b) => b is not null && a > b.Value;
    public static bool operator <=(TimestampField? a, DateTime b) => a is not null && a.Value <= b;
    public static bool operator <=(DateTime a, TimestampField? b) => b is not null && a <= b.Value;
    public static bool operator >=(TimestampField? a, DateTime b) => a is not null && a.Value >= b;
    public static bool operator >=(DateTime a, TimestampField? b) => b is not null && a >= b.Value;

    public string ToHex() => RawValue.ToString("X16");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString("O"));
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString("O");
    public override string ToString() => ToFormattedString();
}
