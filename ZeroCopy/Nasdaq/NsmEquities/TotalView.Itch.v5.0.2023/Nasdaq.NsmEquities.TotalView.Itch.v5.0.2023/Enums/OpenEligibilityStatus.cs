using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates if the security is eligible to be released for trading
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OpenEligibilityStatus : byte
{
    /// <summary>
    ///  Not Eligible
    /// </summary>
    NotEligible = (byte)'N',

    /// <summary>
    ///  Eligible
    /// </summary>
    Eligible = (byte)'Y',

}

/// <summary>
///  Reads and writes the Open Eligibility Status field in place.
/// </summary>
public struct OpenEligibilityStatusField
{
    /// <summary>
    ///  The size of the Open Eligibility Status field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the security is eligible to be released for trading
    /// </summary>
    public OpenEligibilityStatus Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Open Eligibility Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OpenEligibilityStatus Decode()
        => (OpenEligibilityStatus)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Open Eligibility Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OpenEligibilityStatus value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
