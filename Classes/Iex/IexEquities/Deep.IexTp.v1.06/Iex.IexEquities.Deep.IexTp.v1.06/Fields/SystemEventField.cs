using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  System event identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class SystemEventField : Field, IEquatable<SystemEventField>, IComparable<SystemEventField>
{
    public const int ByteLength = 1;

    public override string Name => "SystemEvent";

    private SystemEvent value;
    public SystemEvent Value
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

    public void CopyFrom(SystemEventField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (SystemEvent)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static SystemEventField Sample(int seed = 0) => new() { Value = SystemEvent.StartOfSystemHours };

    public bool Equals(SystemEventField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SystemEventField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(SystemEventField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(SystemEventField? a, SystemEventField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SystemEventField? a, SystemEventField? b) => !(a == b);

    public static implicit operator SystemEvent(SystemEventField field) => field.Value;
    public static implicit operator SystemEventField(SystemEvent value) => new() { Value = value };

    public static bool operator ==(SystemEventField? a, SystemEvent b) => a is not null && a.Value == b;
    public static bool operator ==(SystemEvent a, SystemEventField? b) => b is not null && b.Value == a;
    public static bool operator !=(SystemEventField? a, SystemEvent b) => !(a == b);
    public static bool operator !=(SystemEvent a, SystemEventField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        SystemEvent.StartOfSystemHours => "StartOfSystemHours",
        SystemEvent.StartOfRegularMarketHours => "StartOfRegularMarketHours",
        SystemEvent.EndOfRegularMarketHours => "EndOfRegularMarketHours",
        SystemEvent.EndOfSystemHours => "EndOfSystemHours",
        SystemEvent.EndOfMessages => "EndOfMessages",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
