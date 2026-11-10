using System.Runtime.CompilerServices;

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

/// <summary>
///  Reads and writes the Issue Classification field in place.
/// </summary>
public struct IssueClassificationField
{
    /// <summary>
    ///  The size of the Issue Classification field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Identifies the security class for the issue as assigned by NASDAQ
    /// </summary>
    public IssueClassification Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Issue Classification field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly IssueClassification Decode()
        => (IssueClassification)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Issue Classification field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(IssueClassification value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
