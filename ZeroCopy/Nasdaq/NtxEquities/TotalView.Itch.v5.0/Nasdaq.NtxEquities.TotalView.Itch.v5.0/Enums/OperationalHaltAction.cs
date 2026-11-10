using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes the operational halt action
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OperationalHaltAction : byte
{
    /// <summary>
    ///  Operationally Halted on the identified Market
    /// </summary>
    Halted = (byte)'H',

    /// <summary>
    ///  Operational Halt has been lifted and Trading resumed
    /// </summary>
    Resumed = (byte)'T',

}

/// <summary>
///  Reads and writes the Operational Halt Action field in place.
/// </summary>
public struct OperationalHaltActionField
{
    /// <summary>
    ///  The size of the Operational Halt Action field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Denotes the operational halt action
    /// </summary>
    public OperationalHaltAction Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Operational Halt Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OperationalHaltAction Decode()
        => (OperationalHaltAction)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Operational Halt Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OperationalHaltAction value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
