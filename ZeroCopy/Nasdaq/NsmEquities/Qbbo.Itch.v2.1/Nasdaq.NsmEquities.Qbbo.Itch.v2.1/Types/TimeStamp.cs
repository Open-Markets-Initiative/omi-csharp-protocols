using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Timestamp
/// </summary>
public unsafe struct TimeStamp
{
    /// <summary>
    ///  The size of the Time Stamp field in bytes.
    /// </summary>
    public const int Size = 6;

    /// <summary>
    ///  Timestamp
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    public ulong Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    private const int WidthShift = 16;
    private const int Byte0Offset = 0;
    private const int Byte1Offset = 1;
    private const int Byte2Offset = 2;
    private const int Byte3Offset = 3;
    private const int Byte4Offset = 4;
    private const int Byte5Offset = 5;
    private const int Byte0Shift = 40;
    private const int Byte1Shift = 32;
    private const int Byte2Shift = 24;
    private const int Byte3Shift = 16;
    private const int Byte4Shift = 8;
    /// <summary>
    ///  Returns the wire value of the Time Stamp field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
    {
        return ((ulong)Bytes[Byte0Offset] << Byte0Shift | (ulong)Bytes[Byte1Offset] << Byte1Shift | (ulong)Bytes[Byte2Offset] << Byte2Shift | (ulong)Bytes[Byte3Offset] << Byte3Shift | (ulong)Bytes[Byte4Offset] << Byte4Shift | (ulong)Bytes[Byte5Offset]);
    }

    /// <summary>
    ///  Writes an unscaled wire value into the Time Stamp field.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException">
    ///  The value does not fit its field.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
    {
        if (value << WidthShift >> WidthShift != value)
        {
            throw new ArgumentOutOfRangeException(nameof(value), global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValueDoesNotFit);
        }
        Bytes[Byte0Offset] = unchecked((byte)(value >> Byte0Shift));
        Bytes[Byte1Offset] = unchecked((byte)(value >> Byte1Shift));
        Bytes[Byte2Offset] = unchecked((byte)(value >> Byte2Shift));
        Bytes[Byte3Offset] = unchecked((byte)(value >> Byte3Shift));
        Bytes[Byte4Offset] = unchecked((byte)(value >> Byte4Shift));
        Bytes[Byte5Offset] = unchecked((byte)value);
    }

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of Time Stamp.
    /// </summary>
    public const long TimestampNanosecondsPerUnit = 1;

    /// <summary>
    ///  The wire value of Time Stamp for a time of one full day.
    /// </summary>
    public const long TimestampUnitsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick / TimestampNanosecondsPerUnit;

    /// <summary>
    ///  The largest wire value that the Time Stamp field can hold.
    /// </summary>
    public const ulong TimestampWireMaximum = (1UL << 48) - 1;

    private const int TimestampNoRemainder = 0;

    /// <summary>
    ///  Tries to get the Time Stamp field as a time of day.
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
    ///  Writes a time of day into the Time Stamp field.
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
            throw new ArgumentOutOfRangeException(nameof(nanosecondRemainder), nanosecondRemainder, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.FieldRemainderOutOfRange);
        var nanoseconds = time.Ticks * TimeSpan.NanosecondsPerTick + nanosecondRemainder;
        if (nanoseconds % TimestampNanosecondsPerUnit != TimestampNoRemainder)
            throw new ArgumentOutOfRangeException(nameof(time), time, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.FieldTimeNotWholeUnit);
        var units = nanoseconds / TimestampNanosecondsPerUnit;
        if ((ulong)units > TimestampWireMaximum)
            throw new ArgumentOutOfRangeException(nameof(time), time, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValueDoesNotFit);
        var raw = (ulong)units;
        Encode(raw);
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal fixed byte Bytes[Size];
}
