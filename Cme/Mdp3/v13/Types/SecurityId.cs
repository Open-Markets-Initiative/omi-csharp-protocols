using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Security Id: Security ID
/// </summary>

public struct SecurityId
{
    /// <summary>
    ///  Fix Tag for Security Id
    /// </summary>
    public const ushort FixTag = 48;

    /// <summary>
    ///  Size of Security Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Security Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Security Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
