using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum BrokenTradeOptionalField : byte
{
    /// <summary>
    ///  Broken Trade Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

}

/// <summary>
///  Reads and writes the Broken Trade Optional Field field in place.
/// </summary>
public struct BrokenTradeOptionalFieldField
{
    /// <summary>
    ///  The size of the Broken Trade Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public BrokenTradeOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Broken Trade Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BrokenTradeOptionalField Decode()
        => (BrokenTradeOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Broken Trade Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(BrokenTradeOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
