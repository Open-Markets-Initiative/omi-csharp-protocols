using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Siac.Recipient;

/// <summary>
///  Bid Price Short: Represents The Price At Which A Buyer Is Willing To Buy An Option
/// </summary>

public struct BidPriceShort
{
    /// <summary>
    ///  Decimal place factor for Bid Price Short
    /// </summary>
    public const short Factor = 100;

    /// <summary>
    ///  Size of Bid Price Short in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Bid Price Short value
    /// </summary>
    public readonly short Value
        => Decode();

    /// <summary>
    ///  Read Bid Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly short Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying) / Factor;

    /// <summary>
    ///  Write Bid Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(short value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value) * Factor;

    /// <summary>
    ///  Bid Price Short as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal short Underlying;
}
