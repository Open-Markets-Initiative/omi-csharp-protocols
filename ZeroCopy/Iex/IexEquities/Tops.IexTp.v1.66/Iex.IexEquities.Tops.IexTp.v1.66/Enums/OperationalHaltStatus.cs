using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Operational halt status identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OperationalHaltStatus : byte
{
    /// <summary>
    ///  Iex Specific Operational Trading Halt
    /// </summary>
    IexSpecificOperationalTradingHalt = (byte)'O',

    /// <summary>
    ///  Not Operationally Halted On Iex
    /// </summary>
    NotOperationallyHaltedOnIex = (byte)'N',

}

/// <summary>
///  Reads and writes the Operational Halt Status field in place.
/// </summary>
public struct OperationalHaltStatusField
{
    /// <summary>
    ///  The size of the Operational Halt Status field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Operational halt status identifier
    /// </summary>
    public OperationalHaltStatus Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Operational Halt Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OperationalHaltStatus Decode()
        => (OperationalHaltStatus)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Operational Halt Status field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OperationalHaltStatus value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
