using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ImbalanceSide : byte
{
    /// <summary>
    ///  Buy
    /// </summary>
    Buy = (byte)'B',

    /// <summary>
    ///  Sell
    /// </summary>
    Sell = (byte)'S',

    /// <summary>
    ///  None
    /// </summary>
    None = (byte)'N',

}

/// <summary>
///  Reads and writes the Imbalance Side field in place.
/// </summary>
public struct ImbalanceSideField
{
    /// <summary>
    ///  The size of the Imbalance Side field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
    /// </summary>
    public ImbalanceSide Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Imbalance Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ImbalanceSide Decode()
        => (ImbalanceSide)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Imbalance Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ImbalanceSide value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
