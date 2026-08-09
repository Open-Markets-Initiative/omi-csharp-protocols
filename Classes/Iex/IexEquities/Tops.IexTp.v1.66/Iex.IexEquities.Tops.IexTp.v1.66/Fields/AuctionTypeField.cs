using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Auction type identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class AuctionTypeField : Field, IEquatable<AuctionTypeField>, IComparable<AuctionTypeField>
{
    public const int ByteLength = 1;

    public override string Name => "AuctionType";

    private AuctionType value;
    public AuctionType Value
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

    public void CopyFrom(AuctionTypeField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (AuctionType)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static AuctionTypeField Sample(int seed = 0) => new() { Value = AuctionType.OpeningAuction };

    public bool Equals(AuctionTypeField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is AuctionTypeField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(AuctionTypeField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(AuctionTypeField? a, AuctionTypeField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(AuctionTypeField? a, AuctionTypeField? b) => !(a == b);

    public static implicit operator AuctionType(AuctionTypeField field) => field.Value;
    public static implicit operator AuctionTypeField(AuctionType value) => new() { Value = value };

    public static bool operator ==(AuctionTypeField? a, AuctionType b) => a is not null && a.Value == b;
    public static bool operator ==(AuctionType a, AuctionTypeField? b) => b is not null && b.Value == a;
    public static bool operator !=(AuctionTypeField? a, AuctionType b) => !(a == b);
    public static bool operator !=(AuctionType a, AuctionTypeField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        AuctionType.OpeningAuction => "OpeningAuction",
        AuctionType.ClosingAuction => "ClosingAuction",
        AuctionType.IpoAuction => "IpoAuction",
        AuctionType.HaltAuction => "HaltAuction",
        AuctionType.VolatilityAuction => "VolatilityAuction",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
