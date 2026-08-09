using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Detail of the Reg. SHO short sale price test restriction status
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class DetailField : Field, IEquatable<DetailField>, IComparable<DetailField>
{
    public const int ByteLength = 1;

    public override string Name => "Detail";

    private Detail value;
    public Detail Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    public override bool IsRecognized => Enum.IsDefined(Value);

    public override FieldKind Kind => FieldKind.Enum;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(DetailField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (Detail)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static DetailField Sample(int seed = 0) => new() { Value = Detail.NoPriceTestInPlace };

    public bool Equals(DetailField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is DetailField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(DetailField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(DetailField? a, DetailField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(DetailField? a, DetailField? b) => !(a == b);

    public static implicit operator Detail(DetailField field) => field.Value;
    public static implicit operator DetailField(Detail value) => new() { Value = value };

    public static bool operator ==(DetailField? a, Detail b) => a is not null && a.Value == b;
    public static bool operator ==(Detail a, DetailField? b) => b is not null && b.Value == a;
    public static bool operator !=(DetailField? a, Detail b) => !(a == b);
    public static bool operator !=(Detail a, DetailField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        Detail.NoPriceTestInPlace => "NoPriceTestInPlace",
        Detail.ShortSalePriceTestRestrictionInEffectDueToAnIntradayPriceDropInTheSecurity => "ShortSalePriceTestRestrictionInEffectDueToAnIntradayPriceDropInTheSecurity",
        Detail.ShortSalePriceTestRestrictionRemainsInEffectFromPriorDay => "ShortSalePriceTestRestrictionRemainsInEffectFromPriorDay",
        Detail.ShortSalePriceTestRestrictionDeactivated => "ShortSalePriceTestRestrictionDeactivated",
        Detail.DetailNotAvailable => "DetailNotAvailable",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
