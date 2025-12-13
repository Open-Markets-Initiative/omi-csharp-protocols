using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Security Type: Underlying Security Type
/// </summary>

public unsafe struct UnderlyingSecurityType
{
    /// <summary>
    ///  Fix Tag for Underlying Security Type
    /// </summary>
    public const ushort FixTag = 310;

    /// <summary>
    ///  Size of Underlying Security Type underlying field in bytes
    /// </summary>
    public const int Size = 6;

    /// <summary>
    ///  Length of Underlying Security Type field
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

            return 6;
        }
    }

    /// <summary>
    ///  Underlying Security Type value
    /// </summary>
    public readonly string Value
        => Decode(this);

    /// <summary>
    ///  Does Underlying Security Type field contain a value?
    /// </summary>
    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return Bytes[0] != 0; }
    }

    /// <summary>
    ///  Read Underlying Security Type
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Decode(UnderlyingSecurityType value)
        => new string((sbyte*)value.Bytes, 0, value.Length);

    /// <summary>
    ///  Try Read Underlying Security Type
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
    ///  Encode Underlying Security Type
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
    }

    /// <summary>
    ///  Underlying Security Type as string
    /// </summary>
    public readonly override string ToString()
        => Value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal fixed byte Bytes[Size];
}
