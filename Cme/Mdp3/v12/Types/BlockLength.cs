using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Block Length
/// </summary>

public struct BlockLength
{
    /// <summary>
    ///  Size of Block Length in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Block Length value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Block Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Block Length
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Block Length as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
