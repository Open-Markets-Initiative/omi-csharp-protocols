using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Unique identifier of the higher layer protocol
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class MessageProtocolIdField : Field, IEquatable<MessageProtocolIdField>, IComparable<MessageProtocolIdField>
{
    public const int ByteLength = 2;
    public const bool IsLittleEndian = true;

    public override string Name => "MessageProtocolId";

    private ushort value;
    public ushort Value
    {
        get => value;
        set
        {
            this.value = value;
            Raw = null;
            IsDecoded = false;
        }
    }

    public override FieldKind Kind => FieldKind.UnsignedInteger;

    public override void Reset()
    {
        value = default;
        Raw = null;
        IsDecoded = false;
    }

    public void CopyFrom(MessageProtocolIdField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(offset, ByteLength));
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(offset, ByteLength), Value);
        return offset + ByteLength;
    }

    public static MessageProtocolIdField Sample(int seed = 0) => new() { Value = (ushort)(seed % 65534 + 1) };

    public bool Equals(MessageProtocolIdField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is MessageProtocolIdField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(MessageProtocolIdField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(MessageProtocolIdField? a, MessageProtocolIdField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(MessageProtocolIdField? a, MessageProtocolIdField? b) => !(a == b);

    public static implicit operator ushort(MessageProtocolIdField field) => field.Value;
    public static implicit operator MessageProtocolIdField(ushort value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(MessageProtocolIdField? a, ushort b) => a is not null && a.Value == b;
    public static bool operator ==(ushort a, MessageProtocolIdField? b) => b is not null && b.Value == a;
    public static bool operator !=(MessageProtocolIdField? a, ushort b) => !(a == b);
    public static bool operator !=(ushort a, MessageProtocolIdField? b) => !(a == b);

    public static bool operator <(MessageProtocolIdField? a, ushort b) => a is not null && a.Value < b;
    public static bool operator <(ushort a, MessageProtocolIdField? b) => b is not null && a < b.Value;
    public static bool operator >(MessageProtocolIdField? a, ushort b) => a is not null && a.Value > b;
    public static bool operator >(ushort a, MessageProtocolIdField? b) => b is not null && a > b.Value;
    public static bool operator <=(MessageProtocolIdField? a, ushort b) => a is not null && a.Value <= b;
    public static bool operator <=(ushort a, MessageProtocolIdField? b) => b is not null && a <= b.Value;
    public static bool operator >=(MessageProtocolIdField? a, ushort b) => a is not null && a.Value >= b;
    public static bool operator >=(ushort a, MessageProtocolIdField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X4");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
