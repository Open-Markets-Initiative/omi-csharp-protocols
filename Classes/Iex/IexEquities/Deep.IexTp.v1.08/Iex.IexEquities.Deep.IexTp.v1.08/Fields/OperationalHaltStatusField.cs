using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Operational halt status identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class OperationalHaltStatusField : Field, IEquatable<OperationalHaltStatusField>, IComparable<OperationalHaltStatusField>
{
    public const int ByteLength = 1;

    public override string Name => "OperationalHaltStatus";

    private OperationalHaltStatus value;
    public OperationalHaltStatus Value
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

    public void CopyFrom(OperationalHaltStatusField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (OperationalHaltStatus)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static OperationalHaltStatusField Sample(int seed = 0) => new() { Value = OperationalHaltStatus.IexSpecificOperationalTradingHalt };

    public bool Equals(OperationalHaltStatusField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is OperationalHaltStatusField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(OperationalHaltStatusField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(OperationalHaltStatusField? a, OperationalHaltStatusField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(OperationalHaltStatusField? a, OperationalHaltStatusField? b) => !(a == b);

    public static implicit operator OperationalHaltStatus(OperationalHaltStatusField field) => field.Value;
    public static implicit operator OperationalHaltStatusField(OperationalHaltStatus value) => new() { Value = value };

    public static bool operator ==(OperationalHaltStatusField? a, OperationalHaltStatus b) => a is not null && a.Value == b;
    public static bool operator ==(OperationalHaltStatus a, OperationalHaltStatusField? b) => b is not null && b.Value == a;
    public static bool operator !=(OperationalHaltStatusField? a, OperationalHaltStatus b) => !(a == b);
    public static bool operator !=(OperationalHaltStatus a, OperationalHaltStatusField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        OperationalHaltStatus.IexSpecificOperationalTradingHalt => "IexSpecificOperationalTradingHalt",
        OperationalHaltStatus.NotOperationallyHaltedOnIex => "NotOperationallyHaltedOnIex",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
