using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Min Trade Vol: The minimum trading volume for a security
/// </summary>

public struct MinTradeVol
{
    /// <summary>
    ///  Fix Tag for Min Trade Vol
    /// </summary>
    public const ushort FixTag = 562;

    /// <summary>
    ///  Size of Min Trade Vol in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Min Trade Vol value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Min Trade Vol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Min Trade Vol
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Min Trade Vol as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
