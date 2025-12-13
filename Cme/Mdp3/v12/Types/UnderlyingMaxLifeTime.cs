using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Max Life Time: Max life time of the underlying instruments qualifying for the GC basket in number of year. Will contain null value for Repo specials
/// </summary>

public struct UnderlyingMaxLifeTime
{
    /// <summary>
    ///  Fix Tag for Underlying Max Life Time
    /// </summary>
    public const ushort FixTag = 37717;

    /// <summary>
    ///  Sentinel null value for Underlying Max Life Time
    /// </summary>
    public const byte NoValue = 255;

    /// <summary>
    ///  Length of Underlying Max Life Time in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Read Underlying Max Life Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte Decode()
        => Byte;

    /// <summary>
    ///  Write Underlying Max Life Time
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Underlying Max Life Time as string
    /// </summary>
    public readonly override string ToString()
        => $"{Decode()}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
