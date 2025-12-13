using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Security Id: Unique Instrument ID as qualified by the exchange per tag 305-UnderlyingSecurityIDSource
/// </summary>

public struct UnderlyingSecurityId
{
    /// <summary>
    ///  Fix Tag for Underlying Security Id
    /// </summary>
    public const ushort FixTag = 309;

    /// <summary>
    ///  Size of Underlying Security Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Underlying Security Id value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Underlying Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Underlying Security Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Underlying Security Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
