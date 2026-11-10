using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  ‘N’ = None ‘A’ = AON
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ExecFlag : byte
{
    /// <summary>
    ///  None
    /// </summary>
    None = (byte)'N',

    /// <summary>
    ///  AON
    /// </summary>
    Aon = (byte)'A',

}

/// <summary>
///  Reads and writes the Exec Flag field in place.
/// </summary>
public struct ExecFlagField
{
    /// <summary>
    ///  The size of the Exec Flag field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  ‘N’ = None ‘A’ = AON
    /// </summary>
    public ExecFlag Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Exec Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ExecFlag Decode()
        => (ExecFlag)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Exec Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ExecFlag value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
