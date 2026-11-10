using System.Runtime.CompilerServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Denotes the MWCB Level that was breached.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BreachedLevel : byte
{
    /// <summary>
    ///  Level 1
    /// </summary>
    Level1 = (byte)'1',

    /// <summary>
    ///  Level 2
    /// </summary>
    Level2 = (byte)'2',

    /// <summary>
    ///  Level 3
    /// </summary>
    Level3 = (byte)'3',

}

/// <summary>
///  Reads and writes the Breached Level field in place.
/// </summary>
public struct BreachedLevelField
{
    /// <summary>
    ///  The size of the Breached Level field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Denotes the MWCB Level that was breached.
    /// </summary>
    public BreachedLevel Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Breached Level field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly BreachedLevel Decode()
        => (BreachedLevel)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Breached Level field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(BreachedLevel value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
