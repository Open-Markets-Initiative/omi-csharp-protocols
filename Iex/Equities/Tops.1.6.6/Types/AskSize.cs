using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Ask Size: Aggregate quoted best ask size
/// </summary>

public struct AskSize
{
    /// <summary>
    ///  Size of Ask Size in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Ask Size value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Ask Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Ask Size
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Ask Size as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
