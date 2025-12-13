using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Open Interest Qty: The total open interest for the market at the close of the prior trading session.
/// </summary>

public struct OpenInterestQty
{
    /// <summary>
    ///  Fix Tag for Open Interest Qty
    /// </summary>
    public const ushort FixTag = 5792;

    /// <summary>
    ///  Sentinel null value for Open Interest Qty
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Open Interest Qty in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Open Interest Qty value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Open Interest Qty field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Open Interest Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Open Interest Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Open Interest Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Open Interest Qty to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Open Interest Qty as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
