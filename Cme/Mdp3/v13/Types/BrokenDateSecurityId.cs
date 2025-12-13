using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Broken Date Security Id: Broken date Instrument Globex Security ID
/// </summary>

public struct BrokenDateSecurityId
{
    /// <summary>
    ///  Fix Tag for Broken Date Security Id
    /// </summary>
    public const ushort FixTag = 39027;

    /// <summary>
    ///  Size of Broken Date Security Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Broken Date Security Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Broken Date Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Broken Date Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Broken Date Security Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
