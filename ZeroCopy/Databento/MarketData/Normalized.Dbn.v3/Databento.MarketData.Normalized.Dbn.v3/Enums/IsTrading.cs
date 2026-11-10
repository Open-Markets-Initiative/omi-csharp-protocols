using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The state of trading in the instrument
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IsTrading : byte
{
    /// <summary>
    ///  Not Available
    /// </summary>
    NotAvailable = (byte)'~',

    /// <summary>
    ///  No
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = (byte)'Y',

}

/// <summary>
///  Reads and writes the Is Trading field in place.
/// </summary>
public struct IsTradingField
{
    /// <summary>
    ///  The size of the Is Trading field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The state of trading in the instrument
    /// </summary>
    public IsTrading Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Is Trading field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly IsTrading Decode()
        => (IsTrading)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Is Trading field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(IsTrading value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
