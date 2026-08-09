using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Trading status identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class TradingStatusField : Field, IEquatable<TradingStatusField>, IComparable<TradingStatusField>
{
    public const int ByteLength = 1;

    public override string Name => "TradingStatus";

    private TradingStatus value;
    public TradingStatus Value
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

    public void CopyFrom(TradingStatusField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (TradingStatus)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static TradingStatusField Sample(int seed = 0) => new() { Value = TradingStatus.TradingHaltedAcrossAllUsEquityMarkets };

    public bool Equals(TradingStatusField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is TradingStatusField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(TradingStatusField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(TradingStatusField? a, TradingStatusField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(TradingStatusField? a, TradingStatusField? b) => !(a == b);

    public static implicit operator TradingStatus(TradingStatusField field) => field.Value;
    public static implicit operator TradingStatusField(TradingStatus value) => new() { Value = value };

    public static bool operator ==(TradingStatusField? a, TradingStatus b) => a is not null && a.Value == b;
    public static bool operator ==(TradingStatus a, TradingStatusField? b) => b is not null && b.Value == a;
    public static bool operator !=(TradingStatusField? a, TradingStatus b) => !(a == b);
    public static bool operator !=(TradingStatus a, TradingStatusField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        TradingStatus.TradingHaltedAcrossAllUsEquityMarkets => "TradingHaltedAcrossAllUsEquityMarkets",
        TradingStatus.TradingPausedAndOrderAcceptancePeriodOnIex => "TradingPausedAndOrderAcceptancePeriodOnIex",
        TradingStatus.TradingOnIex => "TradingOnIex",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
