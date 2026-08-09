using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Retail Liquidity Indicator identifier
/// </summary>

public struct RetailLiquidityIndicator
{
    /// <summary>
    ///  Not Applicable
    /// </summary>
    public const char NotApplicable = ' ';

    /// <summary>
    ///  Buy Interest
    /// </summary>
    public const char BuyInterest = 'A';

    /// <summary>
    ///  Sell Interest
    /// </summary>
    public const char SellInterest = 'B';

    /// <summary>
    ///  Buy And Sell Interest
    /// </summary>
    public const char BuyAndSellInterest = 'C';

    /// <summary>
    ///  Size of RetailLiquidityIndicator in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Retail Liquidity Indicator value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Retail Liquidity Indicator value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Retail Liquidity Indicator bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Retail Liquidity Indicator value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
