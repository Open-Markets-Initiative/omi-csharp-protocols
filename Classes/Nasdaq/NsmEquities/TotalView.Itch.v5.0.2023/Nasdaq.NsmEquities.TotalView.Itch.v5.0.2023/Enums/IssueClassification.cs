namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Identifies the security class for the issue as assigned by NASDAQ
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IssueClassification : byte
{
    /// <summary>
    ///  American Depositary Share
    /// </summary>
    AmericanDepositaryShare = (byte)'A',

    /// <summary>
    ///  Bond
    /// </summary>
    Bond = (byte)'B',

    /// <summary>
    ///  Common Stock
    /// </summary>
    CommonStock = (byte)'C',

    /// <summary>
    ///  Depository Receipt
    /// </summary>
    DepositoryReceipt = (byte)'F',

    /// <summary>
    ///  Sec 144A
    /// </summary>
    Sec144A = (byte)'I',

    /// <summary>
    ///  Limited Partnership
    /// </summary>
    LimitedPartnership = (byte)'L',

    /// <summary>
    ///  Notes
    /// </summary>
    Notes = (byte)'N',

    /// <summary>
    ///  Ordinary Share
    /// </summary>
    OrdinaryShare = (byte)'O',

    /// <summary>
    ///  Preferred Stock
    /// </summary>
    PreferredStock = (byte)'P',

    /// <summary>
    ///  Other Securities
    /// </summary>
    OtherSecurities = (byte)'Q',

    /// <summary>
    ///  Right
    /// </summary>
    Right = (byte)'R',

    /// <summary>
    ///  Shares of Beneficial Interest
    /// </summary>
    SharesOfBeneficialInterest = (byte)'S',

    /// <summary>
    ///  Convertible Debenture
    /// </summary>
    ConvertibleDebenture = (byte)'T',

    /// <summary>
    ///  Unit
    /// </summary>
    Unit = (byte)'U',

    /// <summary>
    ///  Units of Beneficial Interest
    /// </summary>
    UnitsOfBeneficialInterest = (byte)'V',

    /// <summary>
    ///  Warrant
    /// </summary>
    Warrant = (byte)'W',
}
