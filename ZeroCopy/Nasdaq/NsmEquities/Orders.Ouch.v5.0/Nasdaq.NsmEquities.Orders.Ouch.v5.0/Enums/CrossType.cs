using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  CrossType
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  continuous market
    /// </summary>
    ContinuousMarket = (byte)'N',

    /// <summary>
    ///  opening cross (Nasdaq only)
    /// </summary>
    OpeningCross = (byte)'O',

    /// <summary>
    ///  closing cross (Nasdaq only)
    /// </summary>
    ClosingCross = (byte)'C',

    /// <summary>
    ///  halt/IPO (Nasdaq only)
    /// </summary>
    HaltIpo = (byte)'H',

    /// <summary>
    ///  supplemental (Nasdaq only)
    /// </summary>
    Supplemental = (byte)'S',

    /// <summary>
    ///  retail (BX only)
    /// </summary>
    Retail = (byte)'R',

    /// <summary>
    ///  extended life (Nasdaq only)
    /// </summary>
    ExtendedLife = (byte)'E',

    /// <summary>
    ///  after hours close (Nasdaq only)
    /// </summary>
    AfterHoursClose = (byte)'A',

}

/// <summary>
///  Reads and writes the Cross Type field in place.
/// </summary>
public struct CrossTypeField
{
    /// <summary>
    ///  The size of the Cross Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  CrossType
    /// </summary>
    public CrossType Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Cross Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly CrossType Decode()
        => (CrossType)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Cross Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(CrossType value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
