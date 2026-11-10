using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum FinancialStatusIndicator : byte
{
    /// <summary>
    ///  Deficient
    /// </summary>
    Deficient = (byte)'D',

    /// <summary>
    ///  Delinquent
    /// </summary>
    Delinquent = (byte)'E',

    /// <summary>
    ///  Bankrupt
    /// </summary>
    Bankrupt = (byte)'Q',

    /// <summary>
    ///  Suspended
    /// </summary>
    Suspended = (byte)'S',

    /// <summary>
    ///  Deficient and Bankrupt
    /// </summary>
    DeficientAndBankrupt = (byte)'G',

    /// <summary>
    ///  Deficient and Delinquent
    /// </summary>
    DeficientAndDelinquent = (byte)'H',

    /// <summary>
    ///  Delinquent and Bankrupt
    /// </summary>
    DelinquentAndBankrupt = (byte)'J',

    /// <summary>
    ///  Deficient Delinquent and Bankrupt
    /// </summary>
    DeficientDelinquentAndBankrupt = (byte)'K',

    /// <summary>
    ///  Creations and/or Redemptions Suspended for Exchange Traded Product
    /// </summary>
    CreationsAndRedemptionsSuspended = (byte)'C',

    /// <summary>
    ///  Issuer is NOT Deficient, Delinquent, or Bankrupt
    /// </summary>
    Normal = (byte)'N',

    /// <summary>
    ///  Firms should refer to SIAC feeds for code if needed
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Financial Status Indicator field in place.
/// </summary>
public struct FinancialStatusIndicatorField
{
    /// <summary>
    ///  The size of the Financial Status Indicator field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
    /// </summary>
    public FinancialStatusIndicator Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Financial Status Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly FinancialStatusIndicator Decode()
        => (FinancialStatusIndicator)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Financial Status Indicator field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(FinancialStatusIndicator value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
