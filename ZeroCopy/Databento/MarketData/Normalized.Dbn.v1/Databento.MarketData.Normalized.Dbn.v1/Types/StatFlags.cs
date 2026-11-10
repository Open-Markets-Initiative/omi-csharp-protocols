using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Additional flags associated with certain stat types
/// </summary>
public struct StatFlags
{
    /// <summary>
    ///  The size of the Stat Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Additional flags associated with certain stat types
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Stat Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Stat Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Byte = value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
