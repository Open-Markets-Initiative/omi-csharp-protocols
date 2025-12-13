using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Contract Multiplier: Number of deliverable units per instrument, e.g., peak days in maturity month or number of calendar days in maturity month
/// </summary>

public struct ContractMultiplier
{
    /// <summary>
    ///  Fix Tag for Contract Multiplier
    /// </summary>
    public const ushort FixTag = 231;

    /// <summary>
    ///  Sentinel null value for Contract Multiplier
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Contract Multiplier in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Contract Multiplier value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Contract Multiplier field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Contract Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Contract Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Contract Multiplier
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Contract Multiplier to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Contract Multiplier as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
