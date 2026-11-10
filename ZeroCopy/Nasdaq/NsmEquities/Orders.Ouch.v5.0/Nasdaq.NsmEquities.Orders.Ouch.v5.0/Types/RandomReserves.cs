using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Shares to do random reserve with.
/// </summary>
public struct RandomReserves
{
    /// <summary>
    ///  The size of the Random Reserves field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Shares to do random reserve with.
    /// </summary>
    public uint Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Random Reserves field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Random Reserves field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<uint>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal uint Underlying;
}
