using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Auction type identifier
/// </summary>

public struct AuctionType
{
    /// <summary>
    ///  Opening Auction
    /// </summary>
    public const char OpeningAuction = 'O';

    /// <summary>
    ///  Closing Auction
    /// </summary>
    public const char ClosingAuction = 'C';

    /// <summary>
    ///  Ipo Auction
    /// </summary>
    public const char IpoAuction = 'I';

    /// <summary>
    ///  Halt Auction
    /// </summary>
    public const char HaltAuction = 'H';

    /// <summary>
    ///  Volatility Auction
    /// </summary>
    public const char VolatilityAuction = 'V';

    /// <summary>
    ///  Size of AuctionType in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Auction Type value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Auction Type value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Auction Type bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Auction Type value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
