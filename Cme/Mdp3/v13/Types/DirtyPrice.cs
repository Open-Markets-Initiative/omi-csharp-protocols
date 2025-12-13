using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Dirty Price: Dirty Price
/// </summary>

public struct DirtyPrice
{
    /// <summary>
    ///  Fix Tag for Dirty Price
    /// </summary>
    public const ushort FixTag = 37001;

    /// <summary>
    ///  Decimal place factor for Dirty Price
    /// </summary>
    public const long Factor = 1000000000;

    /// <summary>
    ///  Size of Dirty Price in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Dirty Price value
    /// </summary>
    public readonly long Value
        => Decode();

    /// <summary>
    ///  Read Dirty Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly long Decode()
        => Underlying / Factor;

    /// <summary>
    ///  Write Dirty Price
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(long value)
        => Underlying = value * Factor;

    /// <summary>
    ///  Dirty Price as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal long Underlying;
}
