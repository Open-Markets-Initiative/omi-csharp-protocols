using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Order Priority Optional: Order priority for execution on the order book
/// </summary>

public struct MdOrderPriorityOptional
{
    /// <summary>
    ///  Fix Tag for Md Order Priority Optional
    /// </summary>
    public const ushort FixTag = 37707;

    /// <summary>
    ///  Sentinel null value for Md Order Priority Optional
    /// </summary>
    public const ulong NoValue = 18446744073709551615;

    /// <summary>
    ///  Size of Md Order Priority Optional in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Md Order Priority Optional value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Does Md Order Priority Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Md Order Priority Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Md Order Priority Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out ulong value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Md Order Priority Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Set Md Order Priority Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Md Order Priority Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
