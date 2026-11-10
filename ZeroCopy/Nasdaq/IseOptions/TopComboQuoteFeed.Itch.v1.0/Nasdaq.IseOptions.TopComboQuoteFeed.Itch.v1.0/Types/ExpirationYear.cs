using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Last two digits of the year of the option expiration
/// </summary>
public struct ExpirationYear
{
    /// <summary>
    ///  The size of the Expiration Year field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Last two digits of the year of the option expiration
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Expiration Year field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Expiration Year field.
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
