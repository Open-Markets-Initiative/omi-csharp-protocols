using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Side : byte
{
    /// <summary>
    ///  Bid
    /// </summary>
    Bid = (byte)'B',

    /// <summary>
    ///  Offer (Ask)
    /// </summary>
    OfferAsk = (byte)'A',

    /// <summary>
    ///  Hidden
    /// </summary>
    Hidden = (byte)' ',

}

/// <summary>
///  Reads and writes the Side field in place.
/// </summary>
public struct SideField
{
    /// <summary>
    ///  The size of the Side field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
    /// </summary>
    public Side Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Side Decode()
        => (Side)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Side value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
