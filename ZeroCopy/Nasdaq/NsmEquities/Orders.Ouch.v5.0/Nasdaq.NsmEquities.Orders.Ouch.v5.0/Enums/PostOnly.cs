using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  PostOnly
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PostOnly : byte
{
    /// <summary>
    ///  Post Only (Mid-point Post Only not available on BX)
    /// </summary>
    PostOnlyValue = (byte)'P',

    /// <summary>
    ///  No (default)
    /// </summary>
    No = (byte)'N',

}

/// <summary>
///  Reads and writes the Post Only field in place.
/// </summary>
public struct PostOnlyField
{
    /// <summary>
    ///  The size of the Post Only field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  PostOnly
    /// </summary>
    public PostOnly Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Post Only field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly PostOnly Decode()
        => (PostOnly)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Post Only field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(PostOnly value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
