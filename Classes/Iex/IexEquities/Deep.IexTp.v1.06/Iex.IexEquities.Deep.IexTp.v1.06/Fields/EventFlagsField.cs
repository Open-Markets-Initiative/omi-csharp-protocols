using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Identifies event processing by the System
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class EventFlagsField : Field, IEquatable<EventFlagsField>, IComparable<EventFlagsField>
{
    public const int ByteLength = 1;

    public override string Name => "EventFlags";

    private EventFlags value;
    public EventFlags Value
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

    public void CopyFrom(EventFlagsField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (EventFlags)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static EventFlagsField Sample(int seed = 0) => new() { Value = EventFlags.OrderBookIsProcessingAnEvent };

    public bool Equals(EventFlagsField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is EventFlagsField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(EventFlagsField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(EventFlagsField? a, EventFlagsField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(EventFlagsField? a, EventFlagsField? b) => !(a == b);

    public static implicit operator EventFlags(EventFlagsField field) => field.Value;
    public static implicit operator EventFlagsField(EventFlags value) => new() { Value = value };

    public static bool operator ==(EventFlagsField? a, EventFlags b) => a is not null && a.Value == b;
    public static bool operator ==(EventFlags a, EventFlagsField? b) => b is not null && b.Value == a;
    public static bool operator !=(EventFlagsField? a, EventFlags b) => !(a == b);
    public static bool operator !=(EventFlags a, EventFlagsField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        EventFlags.OrderBookIsProcessingAnEvent => "OrderBookIsProcessingAnEvent",
        EventFlags.EventProcessingComplete => "EventProcessingComplete",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
