using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Appl Id: The channel ID as defined in the XML Configuration file
/// </summary>

public struct ApplId
{
    /// <summary>
    ///  Fix Tag for Appl Id
    /// </summary>
    public const ushort FixTag = 1180;

    /// <summary>
    ///  Size of Appl Id in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Appl Id value
    /// </summary>
    public readonly short Value
        => Decode();

    /// <summary>
    ///  Read Appl Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly short Decode()
        => Underlying;

    /// <summary>
    ///  Write Appl Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(short value)
        => Underlying = value;

    /// <summary>
    ///  Appl Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal short Underlying;
}
