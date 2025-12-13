using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Order Id Optional: Order ID
/// </summary>

public struct OrderIdOptional
{
    /// <summary>
    ///  Fix Tag for Order Id Optional
    /// </summary>
    public const ushort FixTag = 37;

    /// <summary>
    ///  Sentinel null value for Order Id Optional
    /// </summary>
    public const ulong NoValue = 18446744073709551615;

    /// <summary>
    ///  Size of Order Id Optional in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Order Id Optional value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Does Order Id Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Order Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Order Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Order Id Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Set Order Id Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Order Id Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
