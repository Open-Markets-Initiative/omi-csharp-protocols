using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Security event identifier
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class SecurityEventField : Field, IEquatable<SecurityEventField>, IComparable<SecurityEventField>
{
    public const int ByteLength = 1;

    public override string Name => "SecurityEvent";

    private SecurityEvent value;
    public SecurityEvent Value
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

    public void CopyFrom(SecurityEventField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = (SecurityEvent)data[offset];
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        data[offset] = (byte)Value;
        return offset + ByteLength;
    }

    public static SecurityEventField Sample(int seed = 0) => new() { Value = SecurityEvent.OpeningProcessComplete };

    public bool Equals(SecurityEventField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SecurityEventField other && Equals(other);
    public override int GetHashCode() => ((byte)Value).GetHashCode();

    public int CompareTo(SecurityEventField? other) => other is null ? 1 : ((byte)Value).CompareTo((byte)other.Value);

    public static bool operator ==(SecurityEventField? a, SecurityEventField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SecurityEventField? a, SecurityEventField? b) => !(a == b);

    public static implicit operator SecurityEvent(SecurityEventField field) => field.Value;
    public static implicit operator SecurityEventField(SecurityEvent value) => new() { Value = value };

    public static bool operator ==(SecurityEventField? a, SecurityEvent b) => a is not null && a.Value == b;
    public static bool operator ==(SecurityEvent a, SecurityEventField? b) => b is not null && b.Value == a;
    public static bool operator !=(SecurityEventField? a, SecurityEvent b) => !(a == b);
    public static bool operator !=(SecurityEvent a, SecurityEventField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");


    private string FormatName() => Value switch
    {
        SecurityEvent.OpeningProcessComplete => "OpeningProcessComplete",
        SecurityEvent.ClosingProcessComplete => "ClosingProcessComplete",
        _ => "unknown",
    };
    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(FormatName());
    public override string ToFormattedString(PrintOptions options = default) => FormatName();
    public override string ToString() => FormatName();
}
