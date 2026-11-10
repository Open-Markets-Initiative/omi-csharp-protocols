using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Security event identifier
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityEvent : byte
{
    /// <summary>
    ///  Opening Process Complete
    /// </summary>
    OpeningProcessComplete = (byte)'O',

    /// <summary>
    ///  Closing Process Complete
    /// </summary>
    ClosingProcessComplete = (byte)'C',

}

/// <summary>
///  Reads and writes the Security Event field in place.
/// </summary>
public struct SecurityEventField
{
    /// <summary>
    ///  The size of the Security Event field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Security event identifier
    /// </summary>
    public SecurityEvent Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Security Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly SecurityEvent Decode()
        => (SecurityEvent)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Security Event field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(SecurityEvent value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
