using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  User Reference Index – identifies the channel within the given port
/// </summary>
public struct UserRefIdx
{
    /// <summary>
    ///  The size of the User Ref Idx field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  User Reference Index – identifies the channel within the given port
    /// </summary>
    public byte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the User Ref Idx field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly byte Decode()
        => Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the User Ref Idx field.
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
