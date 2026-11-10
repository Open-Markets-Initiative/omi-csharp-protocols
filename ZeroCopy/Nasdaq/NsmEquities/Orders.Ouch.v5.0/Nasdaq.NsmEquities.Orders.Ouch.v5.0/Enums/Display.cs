using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Display
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Display : byte
{
    /// <summary>
    ///  Attributable
    /// </summary>
    Attributable = (byte)'A',

    /// <summary>
    ///  Visible
    /// </summary>
    Visible = (byte)'Y',

    /// <summary>
    ///  Hidden
    /// </summary>
    Hidden = (byte)'N',

    /// <summary>
    ///  conformant
    /// </summary>
    Conformant = (byte)'Z',

}

/// <summary>
///  Reads and writes the Display field in place.
/// </summary>
public struct DisplayField
{
    /// <summary>
    ///  The size of the Display field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Display
    /// </summary>
    public Display Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Display field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Display Decode()
        => (Display)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Display field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Display value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
