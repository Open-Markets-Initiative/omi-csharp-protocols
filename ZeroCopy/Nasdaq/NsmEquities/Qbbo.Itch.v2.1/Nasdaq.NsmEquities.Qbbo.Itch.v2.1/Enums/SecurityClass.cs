using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates the primary listing market for the issue.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityClass : byte
{
    /// <summary>
    ///  Nasdaq-Listed Issue
    /// </summary>
    NasdaqListedIssue = (byte)'Q',

    /// <summary>
    ///  NYSE
    /// </summary>
    Nyse = (byte)'N',

    /// <summary>
    ///  NYSE American
    /// </summary>
    NyseAmerican = (byte)'A',

    /// <summary>
    ///  NYSE Arca
    /// </summary>
    NyseArca = (byte)'P',

    /// <summary>
    ///  NYSE Texas
    /// </summary>
    NyseTexas = (byte)'M',

    /// <summary>
    ///  BATS
    /// </summary>
    Bats = (byte)'Z',

    /// <summary>
    ///  IEXG
    /// </summary>
    Iexg = (byte)'V',

}

/// <summary>
///  Reads and writes the Security Class field in place.
/// </summary>
public struct SecurityClassField
{
    /// <summary>
    ///  The size of the Security Class field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates the primary listing market for the issue.
    /// </summary>
    public SecurityClass Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Security Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SecurityClass Decode()
        => (SecurityClass)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Security Class field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SecurityClass value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
