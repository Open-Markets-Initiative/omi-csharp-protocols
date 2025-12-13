using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Trade Entry: Market Data Trade Entry ID
/// </summary>

public struct MdTradeEntry
{
    /// <summary>
    ///  Fix Tag for Md Trade Entry
    /// </summary>
    public const ushort FixTag = 37711;

    /// <summary>
    ///  Size of Md Trade Entry in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Md Trade Entry value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Md Trade Entry
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Md Trade Entry
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Md Trade Entry as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
