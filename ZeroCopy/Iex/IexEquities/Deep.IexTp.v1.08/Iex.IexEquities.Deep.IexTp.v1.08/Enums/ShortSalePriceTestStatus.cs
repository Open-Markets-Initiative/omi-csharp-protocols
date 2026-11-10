using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Reg. SHO short sale price test restriction status
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ShortSalePriceTestStatus : byte
{
    /// <summary>
    ///  Not In Effect
    /// </summary>
    NotInEffect = 0,

    /// <summary>
    ///  In Effect
    /// </summary>
    InEffect = 1,

}

/// <summary>
///  Reads and writes the Short Sale Price Test Status field in place.
/// </summary>
public struct ShortSalePriceTestStatusField
{
    /// <summary>
    ///  The size of the Short Sale Price Test Status field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reg. SHO short sale price test restriction status
    /// </summary>
    public ShortSalePriceTestStatus Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Short Sale Price Test Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ShortSalePriceTestStatus Decode()
        => (ShortSalePriceTestStatus)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Short Sale Price Test Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ShortSalePriceTestStatus value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
