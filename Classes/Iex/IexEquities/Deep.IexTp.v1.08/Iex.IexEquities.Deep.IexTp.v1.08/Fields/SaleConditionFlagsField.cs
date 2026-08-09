using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Sale Condition Flags
/// </summary>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class SaleConditionFlagsField : Field, IEquatable<SaleConditionFlagsField>
{
    public const int ByteLength = 1;

    public override string Name => "SaleConditionFlags";

    private byte value;
    public byte Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte SinglepriceCrossTradeMask = unchecked((byte)(1UL << 3));

    /// <summary>
    ///  Trade resulting from a single-price cross
    /// </summary>
    public bool SinglepriceCrossTrade => (Value & SinglepriceCrossTradeMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte TradeThroughExemptMask = unchecked((byte)(1UL << 4));

    /// <summary>
    ///  Trade is not subject to Rule 611
    /// </summary>
    public bool TradeThroughExempt => (Value & TradeThroughExemptMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte OddLotMask = unchecked((byte)(1UL << 5));

    /// <summary>
    ///  Odd Lot
    /// </summary>
    public bool OddLot => (Value & OddLotMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte ExtendedHoursMask = unchecked((byte)(1UL << 6));

    /// <summary>
    ///  Extended Hours Trade
    /// </summary>
    public bool ExtendedHours => (Value & ExtendedHoursMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte IntermarketSweepMask = unchecked((byte)(1UL << 7));

    /// <summary>
    ///  Intermarket Sweep Order
    /// </summary>
    public bool IntermarketSweep => (Value & IntermarketSweepMask) != 0;

    public override FieldKind Kind => FieldKind.Bitfield;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(SaleConditionFlagsField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = Value;
        return offset + ByteLength;
    }

    public static SaleConditionFlagsField Sample(int seed = 0) => new() { Value = (byte)0x55 };

    public bool Equals(SaleConditionFlagsField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SaleConditionFlagsField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(SaleConditionFlagsField? a, SaleConditionFlagsField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SaleConditionFlagsField? a, SaleConditionFlagsField? b) => !(a == b);

    public static implicit operator byte(SaleConditionFlagsField field) => field.Value;
    public static implicit operator SaleConditionFlagsField(byte value) => new() { Value = value };

    public static bool operator ==(SaleConditionFlagsField? a, byte b) => a is not null && a.Value == b;
    public static bool operator ==(byte a, SaleConditionFlagsField? b) => b is not null && b.Value == a;
    public static bool operator !=(SaleConditionFlagsField? a, byte b) => !(a == b);
    public static bool operator !=(byte a, SaleConditionFlagsField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        builder.Append("0x").Append(Value.ToString("X2"));
        bool first = true;
        if (SinglepriceCrossTrade) { builder.Append(first ? " (" : ", "); builder.Append("SinglepriceCrossTrade"); first = false; }
        if (TradeThroughExempt) { builder.Append(first ? " (" : ", "); builder.Append("TradeThroughExempt"); first = false; }
        if (OddLot) { builder.Append(first ? " (" : ", "); builder.Append("OddLot"); first = false; }
        if (ExtendedHours) { builder.Append(first ? " (" : ", "); builder.Append("ExtendedHours"); first = false; }
        if (IntermarketSweep) { builder.Append(first ? " (" : ", "); builder.Append("IntermarketSweep"); first = false; }
        if (!first) builder.Append(')');
    }

    public override string ToFormattedString(PrintOptions options = default)
    {
        var builder = new StringBuilder();
        ToFormattedString(builder, options);
        return builder.ToString();
    }

    public override string ToString() => ToFormattedString();
}
