using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionEvent : byte
{
    /// <summary>
    ///  Start
    /// </summary>
    Start = (byte)'S',

    /// <summary>
    ///  Auction Update
    /// </summary>
    AuctionUpdate = (byte)'U',

    /// <summary>
    ///  End of Auction
    /// </summary>
    EndOfAuction = (byte)'E',

}

/// <summary>
///  Reads and writes the Auction Event field in place.
/// </summary>
public struct AuctionEventField
{
    /// <summary>
    ///  The size of the Auction Event field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
    /// </summary>
    public AuctionEvent Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Auction Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly AuctionEvent Decode()
        => (AuctionEvent)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Auction Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(AuctionEvent value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
