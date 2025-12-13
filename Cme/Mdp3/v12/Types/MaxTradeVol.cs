using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Max Trade Vol: The maximum trading volume for a security
/// </summary>

public struct MaxTradeVol
{
    /// <summary>
    ///  Fix Tag for Max Trade Vol
    /// </summary>
    public const ushort FixTag = 1140;

    /// <summary>
    ///  Size of Max Trade Vol in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Max Trade Vol value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Max Trade Vol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Max Trade Vol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Max Trade Vol as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
