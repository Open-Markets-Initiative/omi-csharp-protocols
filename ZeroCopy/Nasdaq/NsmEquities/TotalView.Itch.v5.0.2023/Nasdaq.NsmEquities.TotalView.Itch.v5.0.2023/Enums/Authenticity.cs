using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Authenticity : byte
{
    /// <summary>
    ///  Live Production
    /// </summary>
    LiveProduction = (byte)'P',

    /// <summary>
    ///  Test
    /// </summary>
    Test = (byte)'T',

}

/// <summary>
///  Reads and writes the Authenticity field in place.
/// </summary>
public struct AuthenticityField
{
    /// <summary>
    ///  The size of the Authenticity field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
    /// </summary>
    public Authenticity Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Authenticity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly Authenticity Decode()
        => (Authenticity)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Authenticity field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(Authenticity value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
