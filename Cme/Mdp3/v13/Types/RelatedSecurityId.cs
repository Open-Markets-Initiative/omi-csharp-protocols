using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Related Security Id: Related Security ID
/// </summary>

public struct RelatedSecurityId
{
    /// <summary>
    ///  Fix Tag for Related Security Id
    /// </summary>
    public const ushort FixTag = 1650;

    /// <summary>
    ///  Size of Related Security Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Related Security Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Related Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Related Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Related Security Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
