using System.Runtime.CompilerServices;

namespace Iex.Deep;

/// <summary>
///  Size: Aggregate quoted size
/// </summary>

public struct Size
{
    /// <summary>
    ///  Size of Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Size value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
