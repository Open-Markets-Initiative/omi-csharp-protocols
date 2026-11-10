using System.Runtime.CompilerServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  Reflects the current eligibility for auto execution of the options security in the options market.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OpenState : byte
{
    /// <summary>
    ///  Open for auto execution
    /// </summary>
    Open = (byte)'Y',

    /// <summary>
    ///  Closed for auto execution
    /// </summary>
    Closed = (byte)'N',

}

/// <summary>
///  Reads and writes the Open State field in place.
/// </summary>
public struct OpenStateField
{
    /// <summary>
    ///  The size of the Open State field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Reflects the current eligibility for auto execution of the options security in the options market.
    /// </summary>
    public OpenState Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Open State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly OpenState Decode()
        => (OpenState)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Open State field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(OpenState value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
