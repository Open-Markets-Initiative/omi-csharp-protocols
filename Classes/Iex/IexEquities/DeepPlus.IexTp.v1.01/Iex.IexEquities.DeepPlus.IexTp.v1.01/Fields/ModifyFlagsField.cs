using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Modify Flags
/// </summary>
[DebuggerDisplay("{ToFormattedString(),nq}")]
public sealed class ModifyFlagsField : Field, IEquatable<ModifyFlagsField>
{
    public const int ByteLength = 1;

    public override string Name => "ModifyFlags";

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
    public const byte PriorityMask = unchecked((byte)(1UL << 7));

    /// <summary>
    ///  Order Priority
    /// </summary>
    public bool Priority => (Value & PriorityMask) != 0;

    public override FieldKind Kind => FieldKind.Bitfield;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(ModifyFlagsField other)
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

    public static ModifyFlagsField Sample(int seed = 0) => new() { Value = (byte)0x55 };

    public bool Equals(ModifyFlagsField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is ModifyFlagsField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(ModifyFlagsField? a, ModifyFlagsField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(ModifyFlagsField? a, ModifyFlagsField? b) => !(a == b);

    public static implicit operator byte(ModifyFlagsField field) => field.Value;
    public static implicit operator ModifyFlagsField(byte value) => new() { Value = value };

    public static bool operator ==(ModifyFlagsField? a, byte b) => a is not null && a.Value == b;
    public static bool operator ==(byte a, ModifyFlagsField? b) => b is not null && b.Value == a;
    public static bool operator !=(ModifyFlagsField? a, byte b) => !(a == b);
    public static bool operator !=(byte a, ModifyFlagsField? b) => !(a == b);

    public string ToHex() => Value.ToString("X2");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default)
    {
        builder.Append("0x").Append(Value.ToString("X2"));
        bool first = true;
        if (Priority) { builder.Append(first ? " (" : ", "); builder.Append("Priority"); first = false; }
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
