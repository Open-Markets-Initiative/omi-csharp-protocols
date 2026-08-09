using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Retail Liquidity Indicator identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class RetailLiquidityIndicatorField : Field, IEquatable<RetailLiquidityIndicatorField>, IComparable<RetailLiquidityIndicatorField>
{
    public const int ByteLength = 1;

    public override string Name => "RetailLiquidityIndicator";

    private RetailLiquidityIndicator value;
    public RetailLiquidityIndicator Value
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

    public void CopyFrom(RetailLiquidityIndicatorField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (RetailLiquidityIndicator)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static RetailLiquidityIndicatorField Sample(int seed = 0) => new() { Value = RetailLiquidityIndicator.NotApplicable };

    public bool Equals(RetailLiquidityIndicatorField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is RetailLiquidityIndicatorField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(RetailLiquidityIndicatorField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(RetailLiquidityIndicatorField? a, RetailLiquidityIndicatorField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(RetailLiquidityIndicatorField? a, RetailLiquidityIndicatorField? b) => !(a == b);

    public static implicit operator RetailLiquidityIndicator(RetailLiquidityIndicatorField field) => field.Value;
    public static implicit operator RetailLiquidityIndicatorField(RetailLiquidityIndicator value) => new() { Value = value };

    public static bool operator ==(RetailLiquidityIndicatorField? a, RetailLiquidityIndicator b) => a is not null && a.Value == b;
    public static bool operator ==(RetailLiquidityIndicator a, RetailLiquidityIndicatorField? b) => b is not null && b.Value == a;
    public static bool operator !=(RetailLiquidityIndicatorField? a, RetailLiquidityIndicator b) => !(a == b);
    public static bool operator !=(RetailLiquidityIndicator a, RetailLiquidityIndicatorField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        RetailLiquidityIndicator.NotApplicable => "NotApplicable",
        RetailLiquidityIndicator.BuyInterest => "BuyInterest",
        RetailLiquidityIndicator.SellInterest => "SellInterest",
        RetailLiquidityIndicator.BuyAndSellInterest => "BuyAndSellInterest",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
