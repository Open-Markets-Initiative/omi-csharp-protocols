using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Price Level Unsigned Optional: Aggregate book level
/// </summary>

public struct MdPriceLevelUnsignedOptional
{
    /// <summary>
    ///  Fix Tag for Md Price Level Unsigned Optional
    /// </summary>
    public const ushort FixTag = 1023;

    /// <summary>
    ///  Sentinel null value for Md Price Level Unsigned Optional
    /// </summary>
    public const byte NoValue = 255;

    /// <summary>
    ///  Length of Md Price Level Unsigned Optional in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Read Md Price Level Unsigned Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte Decode()
        => Byte;

    /// <summary>
    ///  Write Md Price Level Unsigned Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Md Price Level Unsigned Optional as string
    /// </summary>
    public readonly override string ToString()
        => $"{Decode()}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
