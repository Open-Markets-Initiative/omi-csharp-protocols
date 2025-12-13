using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Intervening Days: For SPOT, number of business days between trade date and value (settlement) date. For NDF, number of business days between NDF valuation (fixing) and settlement
/// </summary>

public struct InterveningDays
{
    /// <summary>
    ///  Fix Tag for Intervening Days
    /// </summary>
    public const ushort FixTag = 37730;

    /// <summary>
    ///  Size of Intervening Days in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Intervening Days value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Intervening Days
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => Underlying;

    /// <summary>
    ///  Write Intervening Days
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = value;

    /// <summary>
    ///  Intervening Days as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
