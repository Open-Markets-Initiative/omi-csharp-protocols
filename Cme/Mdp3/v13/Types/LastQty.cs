using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Last Qty: Quantity bought or sold on this last fill
/// </summary>

public struct LastQty
{
    /// <summary>
    ///  Fix Tag for Last Qty
    /// </summary>
    public const ushort FixTag = 32;

    /// <summary>
    ///  Size of Last Qty in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Last Qty value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Last Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Last Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Last Qty as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
