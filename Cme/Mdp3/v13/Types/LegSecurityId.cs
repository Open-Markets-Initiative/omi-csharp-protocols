using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Leg Security Id: Leg Security ID
/// </summary>

public struct LegSecurityId
{
    /// <summary>
    ///  Fix Tag for Leg Security Id
    /// </summary>
    public const ushort FixTag = 602;

    /// <summary>
    ///  Size of Leg Security Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Leg Security Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Leg Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Leg Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Leg Security Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
