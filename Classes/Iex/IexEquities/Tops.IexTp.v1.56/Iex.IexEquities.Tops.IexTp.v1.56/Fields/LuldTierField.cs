using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class LuldTierField : Field, IEquatable<LuldTierField>, IComparable<LuldTierField>
{
    public const int ByteLength = 1;

    public override string Name => "LuldTier";

    private LuldTier value;
    public LuldTier Value
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

    public void CopyFrom(LuldTierField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (LuldTier)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static LuldTierField Sample(int seed = 0) => new() { Value = LuldTier.NotApplicable };

    public bool Equals(LuldTierField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is LuldTierField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(LuldTierField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(LuldTierField? a, LuldTierField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(LuldTierField? a, LuldTierField? b) => !(a == b);

    public static implicit operator LuldTier(LuldTierField field) => field.Value;
    public static implicit operator LuldTierField(LuldTier value) => new() { Value = value };

    public static bool operator ==(LuldTierField? a, LuldTier b) => a is not null && a.Value == b;
    public static bool operator ==(LuldTier a, LuldTierField? b) => b is not null && b.Value == a;
    public static bool operator !=(LuldTierField? a, LuldTier b) => !(a == b);
    public static bool operator !=(LuldTier a, LuldTierField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        LuldTier.NotApplicable => "NotApplicable",
        LuldTier.Tier1NmsStock => "Tier1NmsStock",
        LuldTier.Tier2NmsStock => "Tier2NmsStock",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
