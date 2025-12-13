using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Order Priority: Order priority for execution on the order book
/// </summary>

public struct MdOrderPriority
{
    /// <summary>
    ///  Fix Tag for Md Order Priority
    /// </summary>
    public const ushort FixTag = 37707;

    /// <summary>
    ///  Size of Md Order Priority in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Md Order Priority value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Md Order Priority
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Md Order Priority
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Md Order Priority as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
