using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Expressed as nanoseconds since midnight
/// </summary>
public struct Timestamp
{
    /// <summary>
    ///  The size of the Timestamp field in bytes.
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Expressed as nanoseconds since midnight
    /// </summary>
    public ulong Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Timestamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => global::System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.ReadOnlySpan<ulong>(in Underlying)));

    /// <summary>
    ///  Writes an unscaled wire value into the Timestamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => global::System.Buffers.Binary.BinaryPrimitives.WriteUInt64BigEndian(global::System.Runtime.InteropServices.MemoryMarshal.AsBytes(new global::System.Span<ulong>(ref Underlying)), value);

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of Timestamp.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of Timestamp for a time of one full day.
    /// </summary>
    public const long TimestampUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / TimestampNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that the Timestamp field can hold.
    /// </summary>
    public const ulong TimestampWireMaximum = ulong.MaxValue;

    private const int TimestampNoRemainder = 0;

    /// <summary>
    ///  Tries to get the Timestamp field as a time of day.
    /// </summary>
    /// <param name="time">
    ///  The time of day, to the tick, when the method returns <see langword="true"/>.
    /// </param>
    /// <param name="nanosecondRemainder">
    ///  The nanoseconds of the time below one tick, when the method returns <see langword="true"/>.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the field holds a time of day; otherwise, <see langword="false"/>.
    /// </returns>
    public readonly bool TryDecodeTimeOfDay(out TimeOnly time, out int nanosecondRemainder)
    {
        time = default;
        nanosecondRemainder = TimestampNoRemainder;
        var raw = Decode();
        if (raw >= (ulong)TimestampUnitsPerDay)
            return false;
        var nanoseconds = (long)raw * TimestampNanosecondsPerUnit;
        time = new TimeOnly(nanoseconds / TimeSpan.NanosecondsPerTick);
        nanosecondRemainder = (int)(nanoseconds % TimeSpan.NanosecondsPerTick);
        return true;
    }

    /// <summary>
    ///  Writes a time of day into the Timestamp field.
    /// </summary>
    /// <param name="time">
    ///  The time of day to write.
    /// </param>
    /// <param name="nanosecondRemainder">
    ///  The nanoseconds of the time below one tick.
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The remainder is negative or not below one tick.
    ///  -or-
    ///  The time is not a whole number of the field unit.
    ///  -or-
    ///  The value does not fit its field.
    /// </exception>
    public void EncodeTimeOfDay(TimeOnly time, int nanosecondRemainder = TimestampNoRemainder)
    {
        if (nanosecondRemainder < TimestampNoRemainder || nanosecondRemainder >= TimeSpan.NanosecondsPerTick)
            throw new ArgumentOutOfRangeException(nameof(nanosecondRemainder), nanosecondRemainder, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.FieldRemainderOutOfRange);
        var nanoseconds = time.Ticks * TimeSpan.NanosecondsPerTick + nanosecondRemainder;
        if (nanoseconds % TimestampNanosecondsPerUnit != TimestampNoRemainder)
            throw new ArgumentOutOfRangeException(nameof(time), time, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.FieldTimeNotWholeUnit);
        var units = nanoseconds / TimestampNanosecondsPerUnit;
        if ((ulong)units > TimestampWireMaximum)
            throw new ArgumentOutOfRangeException(nameof(time), time, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValueDoesNotFit);
        var raw = (ulong)units;
        Encode(raw);
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal ulong Underlying;
}
