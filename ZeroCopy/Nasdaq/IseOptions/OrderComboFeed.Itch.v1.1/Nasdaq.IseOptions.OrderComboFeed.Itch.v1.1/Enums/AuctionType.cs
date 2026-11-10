using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AuctionType : byte
{
    /// <summary>
    ///  Exposure
    /// </summary>
    Exposure = (byte)'E',

    /// <summary>
    ///  Facilitation
    /// </summary>
    Facilitation = (byte)'C',

    /// <summary>
    ///  Solicitation
    /// </summary>
    Solicitation = (byte)'S',

    /// <summary>
    ///  PIM
    /// </summary>
    Pim = (byte)'P',

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
    ///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
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
