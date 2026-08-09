using System.Buffers.Binary;
using System.Diagnostics;
using System.Text;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Identifies the session
/// </summary>
[DebuggerDisplay("{Value}")]
public sealed class SessionIdField : Field, IEquatable<SessionIdField>, IComparable<SessionIdField>
{
    public const int ByteLength = 4;
    public const bool IsLittleEndian = true;

    public override string Name => "SessionId";

    private uint value;
    public uint Value
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

    public void CopyFrom(SessionIdField other)
    {
        ArgumentNullException.ThrowIfNull(other);
        value = other.value;
        Raw = other.Raw is { } raw ? raw.ToArray() : null;
        IsDecoded = other.IsDecoded;
    }

    public override bool Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        if (offset + ByteLength > data.Length) return false;
        value = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(offset, ByteLength));
        IsDecoded = true;
        offset += ByteLength;
        return true;
    }

    public override int Encode(Span<byte> data, int offset)
    {
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(offset, ByteLength), Value);
        return offset + ByteLength;
    }

    public static SessionIdField Sample(int seed = 0) => new() { Value = (uint)(seed % 2147483646 + 1) };

    public bool Equals(SessionIdField? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is SessionIdField other && Equals(other);
    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(SessionIdField? other) => other is null ? 1 : Value.CompareTo(other.Value);

    public static bool operator ==(SessionIdField? a, SessionIdField? b) => ReferenceEquals(a, b) || (a is not null && a.Equals(b));
    public static bool operator !=(SessionIdField? a, SessionIdField? b) => !(a == b);

    public static implicit operator uint(SessionIdField field) => field.Value;
    public static implicit operator SessionIdField(uint value) => new() { Value = value };
    public override string ToString() => Value.ToString();

    public static bool operator ==(SessionIdField? a, uint b) => a is not null && a.Value == b;
    public static bool operator ==(uint a, SessionIdField? b) => b is not null && b.Value == a;
    public static bool operator !=(SessionIdField? a, uint b) => !(a == b);
    public static bool operator !=(uint a, SessionIdField? b) => !(a == b);

    public static bool operator <(SessionIdField? a, uint b) => a is not null && a.Value < b;
    public static bool operator <(uint a, SessionIdField? b) => b is not null && a < b.Value;
    public static bool operator >(SessionIdField? a, uint b) => a is not null && a.Value > b;
    public static bool operator >(uint a, SessionIdField? b) => b is not null && a > b.Value;
    public static bool operator <=(SessionIdField? a, uint b) => a is not null && a.Value <= b;
    public static bool operator <=(uint a, SessionIdField? b) => b is not null && a <= b.Value;
    public static bool operator >=(SessionIdField? a, uint b) => a is not null && a.Value >= b;
    public static bool operator >=(uint a, SessionIdField? b) => b is not null && a >= b.Value;

    public string ToHex() => Value.ToString("X8");

    public override void ToFormattedString(StringBuilder builder, PrintOptions options = default) => builder.Append(Value.ToString());
    public override string ToFormattedString(PrintOptions options = default) => Value.ToString();
}
