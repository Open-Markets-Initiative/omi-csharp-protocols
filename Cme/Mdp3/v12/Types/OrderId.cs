using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Order Id: Unique Order ID
/// </summary>

public struct OrderId
{
    /// <summary>
    ///  Fix Tag for Order Id
    /// </summary>
    public const ushort FixTag = 37;

    /// <summary>
    ///  Size of Order Id in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Order Id value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Order Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Order Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Order Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
