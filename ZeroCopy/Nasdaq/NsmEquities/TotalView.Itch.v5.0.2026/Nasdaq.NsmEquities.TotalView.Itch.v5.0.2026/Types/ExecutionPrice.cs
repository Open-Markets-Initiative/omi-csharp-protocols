using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The price at which the order execution occurred
/// </summary>
public struct ExecutionPrice
{
    /// <summary>
    ///  The number by which the wire value of Execution Price is divided to give its value.
    /// </summary>
    public const decimal Factor = 10000m;

    private const string NotMultiple = "The value is not a multiple of 0.0001.";

    /// <summary>
    ///  The size of the Execution Price field in bytes.
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  The price at which the order execution occurred
    /// </summary>
    /// <remarks>
    ///  The value is <see cref="Decode"/> divided by <see cref="Factor"/>.
    /// </remarks>
    /// <exception cref="ArgumentException">
    ///  The value is not a multiple of 0.0001.
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
            Encode(checked((uint)units));
        }
    }

    /// <summary>
    ///  Returns the wire value of the Execution Price field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<uint>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Execution Price field.
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
