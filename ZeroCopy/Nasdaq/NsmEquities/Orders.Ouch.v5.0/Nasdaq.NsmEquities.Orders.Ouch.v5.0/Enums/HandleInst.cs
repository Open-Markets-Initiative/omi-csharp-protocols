using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  HandleInst
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum HandleInst : byte
{
    /// <summary>
    ///  imbalance only (CrossType O and C)
    /// </summary>
    ImbalanceOnly = (byte)'I',

    /// <summary>
    ///  retail order type 1 (CrossType R)
    /// </summary>
    RetailOrderType1 = (byte)'O',

    /// <summary>
    ///  retail order type 2 (CrossType R)
    /// </summary>
    RetailOrderType2 = (byte)'T',

    /// <summary>
    ///  retail price improvement (CrossType R)
    /// </summary>
    RetailPriceImprovement = (byte)'Q',

    /// <summary>
    ///  Extended Life + Continuous (CrossType E)
    /// </summary>
    ExtendedLifeContinuous = (byte)'B',

    /// <summary>
    ///  Direct Listing Capital Raise (CrossType H)
    /// </summary>
    DirectListingCapitalRaise = (byte)'D',

}

/// <summary>
///  Reads and writes the Handle Inst field in place.
/// </summary>
public struct HandleInstField
{
    /// <summary>
    ///  The size of the Handle Inst field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  HandleInst
    /// </summary>
    public HandleInst Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Handle Inst field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly HandleInst Decode()
        => (HandleInst)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Handle Inst field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(HandleInst value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
