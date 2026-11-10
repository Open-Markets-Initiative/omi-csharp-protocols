using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Whether every record carries a ts_out trailer, the live gateway send timestamp
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum TsOut : byte
{
    /// <summary>
    ///  No
    /// </summary>
    No = 0,

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = 1,

}

/// <summary>
///  Reads and writes the Ts Out field in place.
/// </summary>
public struct TsOutField
{
    /// <summary>
    ///  The size of the Ts Out field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
    /// </summary>
    public TsOut Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Ts Out field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly TsOut Decode()
        => (TsOut)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Ts Out field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(TsOut value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
