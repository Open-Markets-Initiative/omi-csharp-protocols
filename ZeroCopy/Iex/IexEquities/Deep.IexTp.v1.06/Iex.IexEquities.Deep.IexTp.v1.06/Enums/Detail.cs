using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_06;

/// <summary>
///  Detail of the Reg. SHO short sale price test restriction status
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Detail : byte
{
    /// <summary>
    ///  No Price Test In Place
    /// </summary>
    NoPriceTestInPlace = (byte)' ',

    /// <summary>
    ///  Short Sale Price Test Restriction In Effect Due To An Intraday Price Drop In The Security
    /// </summary>
    ShortSalePriceTestRestrictionInEffectDueToAnIntradayPriceDropInTheSecurity = (byte)'A',

    /// <summary>
    ///  Short Sale Price Test Restriction Remains In Effect From Prior Day
    /// </summary>
    ShortSalePriceTestRestrictionRemainsInEffectFromPriorDay = (byte)'C',

    /// <summary>
    ///  Short Sale Price Test Restriction Deactivated
    /// </summary>
    ShortSalePriceTestRestrictionDeactivated = (byte)'D',

    /// <summary>
    ///  Detail Not Available
    /// </summary>
    DetailNotAvailable = (byte)'N',

}

/// <summary>
///  Reads and writes the Detail field in place.
/// </summary>
public struct DetailField
{
    /// <summary>
    ///  The size of the Detail field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Detail of the Reg. SHO short sale price test restriction status
    /// </summary>
    public Detail Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Detail field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Detail Decode()
        => (Detail)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Detail field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Detail value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
