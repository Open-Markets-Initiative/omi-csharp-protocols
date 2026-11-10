using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The reason the trade was broken.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BrokenTradeReason : byte
{
    /// <summary>
    ///  Erroneous (trade deemed clearly erroneous)
    /// </summary>
    Erroneous = (byte)'E',

    /// <summary>
    ///  Consent (both parties agreed to break the trade)
    /// </summary>
    Consent = (byte)'C',

    /// <summary>
    ///  Supervisory (manually broken by supervisory)
    /// </summary>
    Supervisory = (byte)'S',

    /// <summary>
    ///  External (trade broken by an external third party)
    /// </summary>
    External = (byte)'X',

}

/// <summary>
///  Reads and writes the Broken Trade Reason field in place.
/// </summary>
public struct BrokenTradeReasonField
{
    /// <summary>
    ///  The size of the Broken Trade Reason field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The reason the trade was broken.
    /// </summary>
    public BrokenTradeReason Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Broken Trade Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BrokenTradeReason Decode()
        => (BrokenTradeReason)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Broken Trade Reason field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(BrokenTradeReason value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
