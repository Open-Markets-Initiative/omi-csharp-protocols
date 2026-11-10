using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Retail Liquidity Indicator identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RetailLiquidityIndicator : byte
{
    /// <summary>
    ///  Not Applicable
    /// </summary>
    NotApplicable = (byte)' ',

    /// <summary>
    ///  Buy Interest
    /// </summary>
    BuyInterest = (byte)'A',

    /// <summary>
    ///  Sell Interest
    /// </summary>
    SellInterest = (byte)'B',

    /// <summary>
    ///  Buy And Sell Interest
    /// </summary>
    BuyAndSellInterest = (byte)'C',

}

/// <summary>
///  Reads and writes the Retail Liquidity Indicator field in place.
/// </summary>
public struct RetailLiquidityIndicatorField
{
    /// <summary>
    ///  The size of the Retail Liquidity Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Retail Liquidity Indicator identifier
    /// </summary>
    public RetailLiquidityIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Retail Liquidity Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly RetailLiquidityIndicator Decode()
        => (RetailLiquidityIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Retail Liquidity Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(RetailLiquidityIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
