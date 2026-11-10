using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Expiration Month of the option (1-12)
/// </summary>
public struct ExpirationMonth
{
    /// <summary>
    ///  The size of the Expiration Month field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Expiration Month of the option (1-12)
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Expiration Month field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Expiration Month field.
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
