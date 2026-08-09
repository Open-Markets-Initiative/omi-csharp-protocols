using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Price type identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class PriceTypeField : Field, IEquatable<PriceTypeField>, IComparable<PriceTypeField>
{
    public const int ByteLength = 1;

    public override string Name => "PriceType";

    private PriceType value;
    public PriceType Value
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

    public void CopyFrom(PriceTypeField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (PriceType)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static PriceTypeField Sample(int seed = 0) => new() { Value = PriceType.IexOfficialOpeningPrice };

    public bool Equals(PriceTypeField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is PriceTypeField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(PriceTypeField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(PriceTypeField? a, PriceTypeField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(PriceTypeField? a, PriceTypeField? b) => !(a == b);

    public static implicit operator PriceType(PriceTypeField field) => field.Value;
    public static implicit operator PriceTypeField(PriceType value) => new() { Value = value };

    public static bool operator ==(PriceTypeField? a, PriceType b) => a is not null && a.Value == b;
    public static bool operator ==(PriceType a, PriceTypeField? b) => b is not null && b.Value == a;
    public static bool operator !=(PriceTypeField? a, PriceType b) => !(a == b);
    public static bool operator !=(PriceType a, PriceTypeField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        PriceType.IexOfficialOpeningPrice => "IexOfficialOpeningPrice",
        PriceType.IexOfficialClosingPrice => "IexOfficialClosingPrice",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
