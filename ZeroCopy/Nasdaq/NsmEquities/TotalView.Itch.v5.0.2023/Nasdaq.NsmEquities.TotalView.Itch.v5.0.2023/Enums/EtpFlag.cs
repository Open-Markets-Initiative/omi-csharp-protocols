using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Indicates whether the security is an exchange traded product
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum EtpFlag : byte
{
    /// <summary>
    ///  Instrument is an ETP
    /// </summary>
    Etp = (byte)'Y',

    /// <summary>
    ///  Instrument is not an ETP
    /// </summary>
    NotEtp = (byte)'N',

    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Etp Flag field in place.
/// </summary>
public struct EtpFlagField
{
    /// <summary>
    ///  The size of the Etp Flag field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates whether the security is an exchange traded product
    /// </summary>
    public EtpFlag Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Etp Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly EtpFlag Decode()
        => (EtpFlag)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Etp Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(EtpFlag value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
