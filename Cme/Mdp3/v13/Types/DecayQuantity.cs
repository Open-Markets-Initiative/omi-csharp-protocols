using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Decay Quantity: Indicates the quantity that a contract will decay daily by once the decay start date is reached
/// </summary>

public struct DecayQuantity
{
    /// <summary>
    ///  Fix Tag for Decay Quantity
    /// </summary>
    public const ushort FixTag = 5818;

    /// <summary>
    ///  Sentinel null value for Decay Quantity
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Decay Quantity in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Decay Quantity value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Decay Quantity field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Decay Quantity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Decay Quantity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Decay Quantity
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Decay Quantity to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Decay Quantity as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
