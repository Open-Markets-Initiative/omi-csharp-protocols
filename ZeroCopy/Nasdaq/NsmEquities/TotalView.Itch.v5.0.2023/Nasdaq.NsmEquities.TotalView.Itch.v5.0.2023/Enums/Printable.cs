using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates if the execution should be reflected on time and sale displays and volume calculations
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Printable : byte
{
    /// <summary>
    ///  Non-printable
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Printable
    /// </summary>
    Yes = (byte)'Y',

}

/// <summary>
///  Reads and writes the Printable field in place.
/// </summary>
public struct PrintableField
{
    /// <summary>
    ///  The size of the Printable field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the execution should be reflected on time and sale displays and volume calculations
    /// </summary>
    public Printable Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Printable field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Printable Decode()
        => (Printable)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Printable field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Printable value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
