using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the Reason field as 4-byte ASCII string.
/// </summary>

public unsafe struct Reason
{
    /// <summary>
    ///  Size of Reason in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Length of Reason field
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (Bytes[0] == (byte)' ') { return 0; }
            if (Bytes[1] == (byte)' ') { return 1; }
            if (Bytes[2] == (byte)' ') { return 2; }
            if (Bytes[3] == (byte)' ') { return 3; }

            return 4;
        }
    }

    /// <summary>
    ///  Gets the decoded Reason value.
    /// </summary>
    public readonly string Value
        => Decode(this);

    /// <summary>
    ///  Does Reason field contain a value?
    /// </summary>
    public readonly bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return Bytes[0] != (byte)' '; }
    }

    /// <summary>
    ///  Decodes the raw bytes into the Reason value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Decode(Reason value)
        => new string((sbyte*)value.Bytes, 0, value.Length);

    /// <summary>
    ///  Attempts to read Reason, returning false if the field is not present.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out string value)
    {
        if (HasValue)
        {
            value = Decode(this);
            return true;
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    ///  Encodes a value into the underlying Reason bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(string value)
    {
        var length = value.Length;
        Bytes[0] = length > 0 ? (byte)value[0] : (byte)' ';
        Bytes[1] = length > 1 ? (byte)value[1] : (byte)' ';
        Bytes[2] = length > 2 ? (byte)value[2] : (byte)' ';
        Bytes[3] = length > 3 ? (byte)value[3] : (byte)' ';
    }

    /// <summary>
    ///  Returns the string representation of the Reason value.
    /// </summary>
    public readonly override string ToString()
        => Value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal fixed byte Bytes[Size];
}
