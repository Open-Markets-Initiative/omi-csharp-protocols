using System.Runtime.CompilerServices;

namespace Ice.iMpact;

/// <summary>
///  Open Interest Date: The date Open Interest is effective for
/// </summary>

public unsafe struct OpenInterestDate
{
    /// <summary>
    ///  Size of Open Interest Date underlying field in bytes
    /// </summary>
    public const int Size = 10;

    /// <summary>
    ///  Length of Open Interest Date field
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (Bytes[0] == 0) { return 0; }
            if (Bytes[1] == 0) { return 1; }
            if (Bytes[2] == 0) { return 2; }
            if (Bytes[3] == 0) { return 3; }
            if (Bytes[4] == 0) { return 4; }
            if (Bytes[5] == 0) { return 5; }
            if (Bytes[6] == 0) { return 6; }
            if (Bytes[7] == 0) { return 7; }
            if (Bytes[8] == 0) { return 8; }
            if (Bytes[9] == 0) { return 9; }

            return 10;
        }
    }

    /// <summary>
    ///  Open Interest Date value
    /// </summary>
    public readonly string Value
        => Decode(this);

    /// <summary>
    ///  Does Open Interest Date field contain a value?
    /// </summary>
    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return Bytes[0] != 0; }
    }

    /// <summary>
    ///  Read Open Interest Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Decode(OpenInterestDate value)
        => new string((sbyte*)value.Bytes, 0, value.Length);

    /// <summary>
    ///  Try Read Open Interest Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryRead(out string value)
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
    ///  Encode Open Interest Date
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(string value)
    {
        var length = value.Length;
        Bytes[0] = length > 0 ? (byte)value[0] : (byte)0;
        Bytes[1] = length > 1 ? (byte)value[1] : (byte)0;
        Bytes[2] = length > 2 ? (byte)value[2] : (byte)0;
        Bytes[3] = length > 3 ? (byte)value[3] : (byte)0;
        Bytes[4] = length > 4 ? (byte)value[4] : (byte)0;
        Bytes[5] = length > 5 ? (byte)value[5] : (byte)0;
        Bytes[6] = length > 6 ? (byte)value[6] : (byte)0;
        Bytes[7] = length > 7 ? (byte)value[7] : (byte)0;
        Bytes[8] = length > 8 ? (byte)value[8] : (byte)0;
        Bytes[9] = length > 9 ? (byte)value[9] : (byte)0;
    }

    /// <summary>
    ///  Open Interest Date as string
    /// </summary>
    public readonly override string ToString()
        => Value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal fixed byte Bytes[Size];
}
