using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Upper threshold price of the auction collar, if any
/// </summary>
/// <remarks>
///  Fixed-point decimal: raw 8-byte little-endian signed integer divided by 10000.
/// </remarks>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class UpperAuctionCollarField : Field, IEquatable<UpperAuctionCollarField>, IComparable<UpperAuctionCollarField>
{
    public const int ByteLength = 8;
    public const bool IsLittleEndian = true;
    public const long Factor = 10000;

    public override string Name => "UpperAuctionCollar";

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

    public void CopyFrom(UpperAuctionCollarField other)
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

    public static UpperAuctionCollarField Sample(int seed = 0) => new() { Value = 1m };

    public bool Equals(UpperAuctionCollarField? other) => other is not null && RawValue == other.RawValue;
    public override bool Equals(object? obj) => obj is UpperAuctionCollarField other && Equals(other);
    public override int GetHashCode() => RawValue.GetHashCode();

    public int CompareTo(UpperAuctionCollarField? other) => other is null ? 1 : RawValue.CompareTo(other.RawValue);

    public static bool operator ==(UpperAuctionCollarField? a, UpperAuctionCollarField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(UpperAuctionCollarField? a, UpperAuctionCollarField? b) => !(a == b);

    public static implicit operator decimal(UpperAuctionCollarField field) => field.Value;
    public static implicit operator UpperAuctionCollarField(decimal value) => new() { Value = value };

    public static bool operator ==(UpperAuctionCollarField? a, decimal b) => a is not null && a.Value == b;
    public static bool operator ==(decimal a, UpperAuctionCollarField? b) => b is not null && b.Value == a;
    public static bool operator !=(UpperAuctionCollarField? a, decimal b) => !(a == b);
    public static bool operator !=(decimal a, UpperAuctionCollarField? b) => !(a == b);

    public static bool operator <(UpperAuctionCollarField? a, decimal b) => a is not null && a.Value < b;
    public static bool operator <(decimal a, UpperAuctionCollarField? b) => b is not null && a < b.Value;
    public static bool operator >(UpperAuctionCollarField? a, decimal b) => a is not null && a.Value > b;
    public static bool operator >(decimal a, UpperAuctionCollarField? b) => b is not null && a > b.Value;
    public static bool operator <=(UpperAuctionCollarField? a, decimal b) => a is not null && a.Value <= b;
    public static bool operator <=(decimal a, UpperAuctionCollarField? b) => b is not null && a <= b.Value;
    public static bool operator >=(UpperAuctionCollarField? a, decimal b) => a is not null && a.Value >= b;
    public static bool operator >=(decimal a, UpperAuctionCollarField? b) => b is not null && a >= b.Value;

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
