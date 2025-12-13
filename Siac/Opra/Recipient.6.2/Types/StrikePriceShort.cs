using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Siac.Recipient;

/// <summary>
///  Strike Price Short: Represents The Stated Price Per Share For Which The Underlying Security May Be Purchased In The Case Of A Call Or Sold In The Case Of A Put By The Option Holder Upon Exercise Of The Option Contract
/// </summary>

public struct StrikePriceShort
{
    /// <summary>
    ///  Decimal place factor for Strike Price Short
    /// </summary>
    public const ushort Factor = 10;

    /// <summary>
    ///  Size of Strike Price Short in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Strike Price Short value
    /// </summary>
    public readonly ushort Value
        => Decode();

    /// <summary>
    ///  Read Strike Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ushort Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying) / Factor;

    /// <summary>
    ///  Write Strike Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ushort value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value) * Factor;

    /// <summary>
    ///  Strike Price Short as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ushort Underlying;
}
