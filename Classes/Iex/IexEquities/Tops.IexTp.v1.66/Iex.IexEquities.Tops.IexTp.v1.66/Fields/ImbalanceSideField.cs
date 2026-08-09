using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class ImbalanceSideField : Field, IEquatable<ImbalanceSideField>, IComparable<ImbalanceSideField>
{
    public const int ByteLength = 1;

    public override string Name => "ImbalanceSide";

    private ImbalanceSide value;
    public ImbalanceSide Value
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

    public void CopyFrom(ImbalanceSideField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (ImbalanceSide)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static ImbalanceSideField Sample(int seed = 0) => new() { Value = ImbalanceSide.Buy };

    public bool Equals(ImbalanceSideField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ImbalanceSideField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(ImbalanceSideField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(ImbalanceSideField? a, ImbalanceSideField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(ImbalanceSideField? a, ImbalanceSideField? b) => !(a == b);

    public static implicit operator ImbalanceSide(ImbalanceSideField field) => field.Value;
    public static implicit operator ImbalanceSideField(ImbalanceSide value) => new() { Value = value };

    public static bool operator ==(ImbalanceSideField? a, ImbalanceSide b) => a is not null && a.Value == b;
    public static bool operator ==(ImbalanceSide a, ImbalanceSideField? b) => b is not null && b.Value == a;
    public static bool operator !=(ImbalanceSideField? a, ImbalanceSide b) => !(a == b);
    public static bool operator !=(ImbalanceSide a, ImbalanceSideField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        ImbalanceSide.Buy => "Buy",
        ImbalanceSide.Sell => "Sell",
        ImbalanceSide.None => "None",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
