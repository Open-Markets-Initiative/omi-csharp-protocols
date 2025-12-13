using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Display Qty Optional: Visible qty of order
/// </summary>

public struct MdDisplayQtyOptional
{
    /// <summary>
    ///  Fix Tag for Md Display Qty Optional
    /// </summary>
    public const ushort FixTag = 37706;

    /// <summary>
    ///  Sentinel null value for Md Display Qty Optional
    /// </summary>
    public const int NoValue = 2147483647;

    /// <summary>
    ///  Size of Md Display Qty Optional in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Md Display Qty Optional value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Does Md Display Qty Optional field contain a value?
    /// </summary>
    public readonly bool HasValue
        => Underlying != NoValue;

    /// <summary>
    ///  Read Md Display Qty Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Try Read Md Display Qty Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly bool TryRead(out int value)
    {
        value = Decode();
        return HasValue;
    }

    /// <summary>
    ///  Write Md Display Qty Optional
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Set Md Display Qty Optional to unused
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset()
        => Encode(NoValue);

    /// <summary>
    ///  Md Display Qty Optional as string
    /// </summary>
    public readonly override string ToString()
        => TryRead(out var value) ? $"{value}" : "Not Applicable";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
