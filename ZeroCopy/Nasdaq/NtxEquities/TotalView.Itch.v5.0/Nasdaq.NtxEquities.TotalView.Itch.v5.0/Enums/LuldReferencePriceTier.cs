using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum LuldReferencePriceTier : byte
{
    /// <summary>
    ///  Tier 1 NMS Stocks and select ETPs
    /// </summary>
    Tier1NmsStocksAndSelectEtPs = (byte)'1',

    /// <summary>
    ///  Tier 2 NMS Stocks
    /// </summary>
    Tier2NmsStocks = (byte)'2',

    /// <summary>
    ///  Not available
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Luld Reference Price Tier field in place.
/// </summary>
public struct LuldReferencePriceTierField
{
    /// <summary>
    ///  The size of the Luld Reference Price Tier field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument.
    /// </summary>
    public LuldReferencePriceTier Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Luld Reference Price Tier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly LuldReferencePriceTier Decode()
        => (LuldReferencePriceTier)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Luld Reference Price Tier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(LuldReferencePriceTier value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
