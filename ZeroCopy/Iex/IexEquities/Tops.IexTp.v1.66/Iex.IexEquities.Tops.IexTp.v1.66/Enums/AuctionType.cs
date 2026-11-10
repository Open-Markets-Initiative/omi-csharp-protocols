using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Auction type identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionType : byte
{
    /// <summary>
    ///  Opening Auction
    /// </summary>
    OpeningAuction = (byte)'O',

    /// <summary>
    ///  Closing Auction
    /// </summary>
    ClosingAuction = (byte)'C',

    /// <summary>
    ///  Ipo Auction
    /// </summary>
    IpoAuction = (byte)'I',

    /// <summary>
    ///  Halt Auction
    /// </summary>
    HaltAuction = (byte)'H',

    /// <summary>
    ///  Volatility Auction
    /// </summary>
    VolatilityAuction = (byte)'V',

}

/// <summary>
///  Reads and writes the Auction Type field in place.
/// </summary>
public struct AuctionTypeField
{
    /// <summary>
    ///  The size of the Auction Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Auction type identifier
    /// </summary>
    public AuctionType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Auction Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly AuctionType Decode()
        => (AuctionType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Auction Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(AuctionType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
