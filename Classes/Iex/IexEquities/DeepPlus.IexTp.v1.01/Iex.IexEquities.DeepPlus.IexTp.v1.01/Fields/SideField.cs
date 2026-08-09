using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Side of order
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class SideField : Field, IEquatable<SideField>, IComparable<SideField>
{
    public const int ByteLength = 1;

    public override string Name => "Side";

    private Side value;
    public Side Value
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

    public void CopyFrom(SideField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (Side)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static SideField Sample(int seed = 0) => new() { Value = Side.Buy };

    public bool Equals(SideField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SideField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(SideField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(SideField? a, SideField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SideField? a, SideField? b) => !(a == b);

    public static implicit operator Side(SideField field) => field.Value;
    public static implicit operator SideField(Side value) => new() { Value = value };

    public static bool operator ==(SideField? a, Side b) => a is not null && a.Value == b;
    public static bool operator ==(Side a, SideField? b) => b is not null && b.Value == a;
    public static bool operator !=(SideField? a, Side b) => !(a == b);
    public static bool operator !=(Side a, SideField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        Side.Buy => "Buy",
        Side.Sell => "Sell",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
