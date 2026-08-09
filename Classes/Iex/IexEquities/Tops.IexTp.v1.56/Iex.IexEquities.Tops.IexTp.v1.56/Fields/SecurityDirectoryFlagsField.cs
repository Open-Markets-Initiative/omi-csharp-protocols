using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Security Directory Flags
/// </summary>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class SecurityDirectoryFlagsField : Field, IEquatable<SecurityDirectoryFlagsField>
{
    public const int ByteLength = 1;

    public override string Name => "SecurityDirectoryFlags";

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
    public const byte EtpMask = unchecked((byte)(1UL << 5));

    /// <summary>
    ///  Symbol is an ETP
    /// </summary>
    public bool Etp => (Value & EtpMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte WhenIssuedMask = unchecked((byte)(1UL << 6));

    /// <summary>
    ///  Symbol is a when issued security
    /// </summary>
    public bool WhenIssued => (Value & WhenIssuedMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte TestSecurityMask = unchecked((byte)(1UL << 7));

    /// <summary>
    ///  Symbol is a test security
    /// </summary>
    public bool TestSecurity => (Value & TestSecurityMask) != 0;

    public override FieldKind Kind => FieldKind.Bitfield;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(SecurityDirectoryFlagsField other)
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

    public static SecurityDirectoryFlagsField Sample(int seed = 0) => new() { Value = (byte)0x55 };

    public bool Equals(SecurityDirectoryFlagsField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SecurityDirectoryFlagsField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(SecurityDirectoryFlagsField? a, SecurityDirectoryFlagsField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SecurityDirectoryFlagsField? a, SecurityDirectoryFlagsField? b) => !(a == b);

    public static implicit operator byte(SecurityDirectoryFlagsField field) => field.Value;
    public static implicit operator SecurityDirectoryFlagsField(byte value) => new() { Value = value };

    public static bool operator ==(SecurityDirectoryFlagsField? a, byte b) => a is not null && a.Value == b;
    public static bool operator ==(byte a, SecurityDirectoryFlagsField? b) => b is not null && b.Value == a;
    public static bool operator !=(SecurityDirectoryFlagsField? a, byte b) => !(a == b);
    public static bool operator !=(byte a, SecurityDirectoryFlagsField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        builder.Append("0x").Append(Value.ToString("X2"));
        bool first = true;
        if (Etp) { builder.Append(first ? " (" : ", "); builder.Append("Etp"); first = false; }
        if (WhenIssued) { builder.Append(first ? " (" : ", "); builder.Append("WhenIssued"); first = false; }
        if (TestSecurity) { builder.Append(first ? " (" : ", "); builder.Append("TestSecurity"); first = false; }
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
