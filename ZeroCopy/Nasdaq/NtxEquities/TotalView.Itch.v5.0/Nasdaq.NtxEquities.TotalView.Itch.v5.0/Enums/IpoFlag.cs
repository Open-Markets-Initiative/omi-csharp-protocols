using System.Runtime.CompilerServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the Nasdaq security is set up for IPO release.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoFlag : byte
{
    /// <summary>
    ///  Nasdaq system only accepts round lots orders for this security.
    /// </summary>
    IpoSecurity = (byte)'Y',

    /// <summary>
    ///  Nasdaq system does not have any order size restrictions for this security. Odd and mixed lot orders are allowed.
    /// </summary>
    NotIpoSecurity = (byte)'N',

    /// <summary>
    ///  Not available
    /// </summary>
    NotAvailable = (byte)' ',

}

/// <summary>
///  Reads and writes the Ipo Flag field in place.
/// </summary>
public struct IpoFlagField
{
    /// <summary>
    ///  The size of the Ipo Flag field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the Nasdaq security is set up for IPO release.
    /// </summary>
    public IpoFlag Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Ipo Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly IpoFlag Decode()
        => (IpoFlag)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Ipo Flag field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(IpoFlag value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
