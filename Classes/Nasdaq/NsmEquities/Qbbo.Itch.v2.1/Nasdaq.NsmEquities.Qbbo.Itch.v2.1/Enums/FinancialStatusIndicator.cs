namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  For Nasdaq-listed issues, this field indicates when a firm is not in compliance with Nasdaq continued listing requirements.
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
    ///  Deficient, Delinquent and Bankrupt
    /// </summary>
    DeficientDelinquentAndBankrupt = (byte)'K',

    /// <summary>
    ///  Creations and/or Redemptions Suspended for Exchange Traded Product
    /// </summary>
    CreationsAndorRedemptionsSuspended = (byte)'C',

    /// <summary>
    ///  Normal (Default): Issuer Is NOT Deficient, Delinquent, or Bankrupt
    /// </summary>
    Normal = (byte)'N',

    /// <summary>
    ///  Company is in compliance, if Nasdaq-listed issue
    /// </summary>
    NotAvailable = (byte)' ',
}
