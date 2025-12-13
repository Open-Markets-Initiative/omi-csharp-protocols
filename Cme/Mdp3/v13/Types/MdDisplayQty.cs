using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Display Qty: Visible order qty
/// </summary>

public struct MdDisplayQty
{
    /// <summary>
    ///  Fix Tag for Md Display Qty
    /// </summary>
    public const ushort FixTag = 37706;

    /// <summary>
    ///  Size of Md Display Qty in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Md Display Qty value
    /// </summary>
    public readonly int Value
        => Decode();

    /// <summary>
    ///  Read Md Display Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly int Decode()
        => Underlying;

    /// <summary>
    ///  Write Md Display Qty
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(int value)
        => Underlying = value;

    /// <summary>
    ///  Md Display Qty as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal int Underlying;
}
