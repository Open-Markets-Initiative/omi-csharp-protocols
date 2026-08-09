using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Official opening or closing price, as specified
/// </summary>
/// <remarks>
///  Fixed-point decimal: raw 8-byte little-endian signed integer divided by 10000.
/// </remarks>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class OfficialPriceField : Field, IEquatable<OfficialPriceField>, IComparable<OfficialPriceField>
{
    public const int ByteLength = 8;
    public const bool IsLittleEndian = true;
    public const long Factor = 10000;

    public override string Name => "OfficialPrice";

    /// <summary>
    ///  The raw integer value as read from the wire, before fixed-point conversion.
    /// </summary>
    public long RawValue { get; private set; }

    private decimal value;
    public decimal Value
    {
        get => value;
        set
        {
            this.value = value;
            // Round half away from zero; checked so an out-of-range value faults instead of silently wrapping.
            RawValue = checked((long)Math.Round(value * Factor, MidpointRounding.AwayFromZero));
            Raw = null;
            IsDecoded = false;
        }
    }

    public override FieldKind Kind => FieldKind.Decimal;

    public override void Reset()
    {
        value = default;
        RawValue = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(OfficialPriceField other)
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
        value = (decimal)RawValue / Factor;
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteInt64LittleEndian(data.Slice(offset, ByteLength), RawValue);
        return offset + ByteLength;
    }

    public static OfficialPriceField Sample(int seed = 0) => new() { Value = 1m };

    public bool Equals(OfficialPriceField? other) => other is not null && RawValue == other.RawValue;
    public override bool Equals(object? obj) => obj is OfficialPriceField other && Equals(other);
    public override int GetHashCode() => RawValue.GetHashCode();

    public int CompareTo(OfficialPriceField? other) => other is null ? 1 : RawValue.CompareTo(other.RawValue);

    public static bool operator ==(OfficialPriceField? a, OfficialPriceField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(OfficialPriceField? a, OfficialPriceField? b) => !(a == b);

    public static implicit operator decimal(OfficialPriceField field) => field.Value;
    public static implicit operator OfficialPriceField(decimal value) => new() { Value = value };

    public static bool operator ==(OfficialPriceField? a, decimal b) => a is not null && a.Value == b;
    public static bool operator ==(decimal a, OfficialPriceField? b) => b is not null && b.Value == a;
    public static bool operator !=(OfficialPriceField? a, decimal b) => !(a == b);
    public static bool operator !=(decimal a, OfficialPriceField? b) => !(a == b);

    public static bool operator <(OfficialPriceField? a, decimal b) => a is not null && a.Value < b;
    public static bool operator <(decimal a, OfficialPriceField? b) => b is not null && a < b.Value;
    public static bool operator >(OfficialPriceField? a, decimal b) => a is not null && a.Value > b;
    public static bool operator >(decimal a, OfficialPriceField? b) => b is not null && a > b.Value;
    public static bool operator <=(OfficialPriceField? a, decimal b) => a is not null && a.Value <= b;
    public static bool operator <=(decimal a, OfficialPriceField? b) => b is not null && a <= b.Value;
    public static bool operator >=(OfficialPriceField? a, decimal b) => a is not null && a.Value >= b;
    public static bool operator >=(decimal a, OfficialPriceField? b) => b is not null && a >= b.Value;

    public string ToHex() => RawValue.ToString("X16");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString("F4"));
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString("F4");

    public string ToFormattedString(string? currency, int? decimalPlaces = null)
    {
        var places = decimalPlaces ?? 4;
        var formatted = Value.ToString($"F{places}");
        return currency is null ? formatted : $"{currency}{formatted}";
    }

    public override string ToString() => ToFormattedString();
}
