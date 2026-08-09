using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Reg. SHO short sale price test restriction status
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class ShortSalePriceTestStatusField : Field, IEquatable<ShortSalePriceTestStatusField>, IComparable<ShortSalePriceTestStatusField>
{
    public const int ByteLength = 1;

    public override string Name => "ShortSalePriceTestStatus";

    private ShortSalePriceTestStatus value;
    public ShortSalePriceTestStatus Value
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

    public void CopyFrom(ShortSalePriceTestStatusField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (ShortSalePriceTestStatus)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static ShortSalePriceTestStatusField Sample(int seed = 0) => new() { Value = ShortSalePriceTestStatus.NotInEffect };

    public bool Equals(ShortSalePriceTestStatusField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ShortSalePriceTestStatusField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(ShortSalePriceTestStatusField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(ShortSalePriceTestStatusField? a, ShortSalePriceTestStatusField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(ShortSalePriceTestStatusField? a, ShortSalePriceTestStatusField? b) => !(a == b);

    public static implicit operator ShortSalePriceTestStatus(ShortSalePriceTestStatusField field) => field.Value;
    public static implicit operator ShortSalePriceTestStatusField(ShortSalePriceTestStatus value) => new() { Value = value };

    public static bool operator ==(ShortSalePriceTestStatusField? a, ShortSalePriceTestStatus b) => a is not null && a.Value == b;
    public static bool operator ==(ShortSalePriceTestStatus a, ShortSalePriceTestStatusField? b) => b is not null && b.Value == a;
    public static bool operator !=(ShortSalePriceTestStatusField? a, ShortSalePriceTestStatus b) => !(a == b);
    public static bool operator !=(ShortSalePriceTestStatus a, ShortSalePriceTestStatusField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        ShortSalePriceTestStatus.NotInEffect => "NotInEffect",
        ShortSalePriceTestStatus.InEffect => "InEffect",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
