using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Siac.Recipient;

/// <summary>
///  Offer Price Short: Represents The Price At Which A Seller Is Offering To Sell An Option
/// </summary>

public struct OfferPriceShort
{
    /// <summary>
    ///  Decimal place factor for Offer Price Short
    /// </summary>
    public const short Factor = 100;

    /// <summary>
    ///  Size of Offer Price Short in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Offer Price Short value
    /// </summary>
    public readonly short Value
        => Decode();

    /// <summary>
    ///  Read Offer Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly short Decode()
        => BinaryPrimitives.ReverseEndianness(Underlying) / Factor;

    /// <summary>
    ///  Write Offer Price Short
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(short value)
        => Underlying = BinaryPrimitives.ReverseEndianness(value) * Factor;

    /// <summary>
    ///  Offer Price Short as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal short Underlying;
}
