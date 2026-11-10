using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Reserved for future use
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum UnpairedSide : byte
{
    /// <summary>
    ///  A sell order or sell aggressor in a trade
    /// </summary>
    Ask = (byte)'A',

    /// <summary>
    ///  A buy order or a buy aggressor in a trade
    /// </summary>
    Bid = (byte)'B',

    /// <summary>
    ///  No side specified by the original source
    /// </summary>
    None = (byte)'N',

}

/// <summary>
///  Reads and writes the Unpaired Side field in place.
/// </summary>
public struct UnpairedSideField
{
    /// <summary>
    ///  The size of the Unpaired Side field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public UnpairedSide Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Unpaired Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly UnpairedSide Decode()
        => (UnpairedSide)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Unpaired Side field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(UnpairedSide value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
