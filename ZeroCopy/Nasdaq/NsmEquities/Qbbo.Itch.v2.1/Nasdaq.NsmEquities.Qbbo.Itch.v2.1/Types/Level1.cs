using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Denotes the MWCB Level 1 Value.
/// </summary>
public struct Level1
{
    /// <summary>
    ///  The number by which the wire value of Level 1 is divided to give its value.
    /// </summary>
    public const decimal Factor = 100000000m;

    private const string NotMultiple = "The value is not a multiple of 0.00000001.";

    /// <summary>
    ///  The size of the Level 1 field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Denotes the MWCB Level 1 Value.
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="Decode"/> divided by <see cref="Factor"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///  The value is not a multiple of 0.00000001.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  The value does not fit its field.
    /// </exception>
    public decimal Value
    {
        readonly get => (decimal)Decode() / Factor;
        set
        {
            var units = value * Factor;
            if (units != decimal.Truncate(units) || units / Factor != value)
            {
                throw new ArgumentException(NotMultiple, nameof(value));
            }
            Encode(checked((ulong)units));
        }
    }

    /// <summary>
    ///  Returns the wire value of the Level 1 field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Level 1 field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ulong Underlying;
}
