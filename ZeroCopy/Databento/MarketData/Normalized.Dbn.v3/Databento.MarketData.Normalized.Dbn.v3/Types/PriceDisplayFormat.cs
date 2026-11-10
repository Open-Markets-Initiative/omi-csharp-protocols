using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The price display format
/// </summary>
public struct PriceDisplayFormat
{
    /// <summary>
    ///  The size of the Price Display Format field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The price display format
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Price Display Format field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Price Display Format field.
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
