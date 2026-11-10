using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum LuldTier : byte
{
    /// <summary>
    ///  Not Applicable
    /// </summary>
    NotApplicable = 0,

    /// <summary>
    ///  Tier 1 Nms Stock
    /// </summary>
    Tier1NmsStock = 1,

    /// <summary>
    ///  Tier 2 Nms Stock
    /// </summary>
    Tier2NmsStock = 2,

}

/// <summary>
///  Reads and writes the Luld Tier field in place.
/// </summary>
public struct LuldTierField
{
    /// <summary>
    ///  The size of the Luld Tier field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
    /// </summary>
    public LuldTier Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Luld Tier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly LuldTier Decode()
        => (LuldTier)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Luld Tier field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(LuldTier value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
