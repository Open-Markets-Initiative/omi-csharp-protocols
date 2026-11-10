using System.Globalization;
using System.Numerics;

namespace Databento.MarketData.Normalized.Dbn.v2;

internal static class Text
{
    private const string Absent = "null";
    private const string Separator = ", ";
    private const string Negative = "-";
    private const char Zero = '0';
    private const int DigitBase = 10;
    private const long NanosecondsPerSecond = TimeSpan.TicksPerSecond * TimeSpan.NanosecondsPerTick;
    private const long NanosecondsPerMinute = TimeSpan.TicksPerMinute * TimeSpan.NanosecondsPerTick;
    private const long NanosecondsPerHour = TimeSpan.TicksPerHour * TimeSpan.NanosecondsPerTick;
    private const long NanosecondsPerDay = TimeSpan.TicksPerDay * TimeSpan.NanosecondsPerTick;

    internal static string Of<T>(T value) where T : IFormattable => value.ToString(null, CultureInfo.InvariantCulture);

    internal static string Of<T>(T? value) where T : struct, IFormattable => value is { } present ? Of(present) : Absent;

    internal static string Of(string? value) => value ?? Absent;

    internal static string Of(char value) => value.ToString();

    internal static string Of(char? value) => value is { } present ? Of(present) : Absent;

    internal static string Enum<T>(T value) where T : struct, System.Enum => System.Enum.IsDefined(value) ? value.ToString() : $"Unknown(0x{value:X})";

    internal static string Enum<T>(T? value) where T : struct, System.Enum => value is { } present ? Enum(present) : Absent;

    internal static string Value<T>(T value) where T : struct => value.ToString() ?? string.Empty;

    internal static string Value<T>(T? value) where T : struct => value is { } present ? Value(present) : Absent;

    internal static string Instant<T>(T raw, long nanosecondsPerUnit) where T : IBinaryInteger<T>
    {
        var (days, ofDay) = Int128.DivRem(Int128.CreateTruncating(raw) * nanosecondsPerUnit, NanosecondsPerDay);
        if (Int128.IsNegative(ofDay))
        {
            days--;
            ofDay += NanosecondsPerDay;
        }
        var ticks = long.CreateSaturating(days * TimeSpan.TicksPerDay);
        var date = ticks > DateTime.MaxValue.Ticks - DateTime.UnixEpoch.Ticks ? DateTime.MaxValue : ticks < DateTime.MinValue.Ticks - DateTime.UnixEpoch.Ticks ? DateTime.MinValue : DateTime.UnixEpoch.AddTicks(ticks);
        return string.Create(CultureInfo.InvariantCulture, $"{date:yyyy-MM-dd}T{Clock(ofDay, nanosecondsPerUnit)}Z");
    }

    internal static string Instant<T>(T raw, long nanosecondsPerUnit, T nullValue) where T : IBinaryInteger<T> => raw == nullValue ? Absent : Instant(raw, nanosecondsPerUnit);

    internal static string Instant<T>(T? raw, long nanosecondsPerUnit) where T : struct, IBinaryInteger<T> => raw is { } present ? Instant(present, nanosecondsPerUnit) : Absent;

    internal static string Instant<T>(T? raw, long nanosecondsPerUnit, T nullValue) where T : struct, IBinaryInteger<T> => raw is { } present ? Instant(present, nanosecondsPerUnit, nullValue) : Absent;

    internal static string Midnight<T>(T raw, long nanosecondsPerUnit) where T : IBinaryInteger<T> => Clock(Int128.CreateTruncating(raw) * nanosecondsPerUnit, nanosecondsPerUnit);

    internal static string Midnight<T>(T raw, long nanosecondsPerUnit, T nullValue) where T : IBinaryInteger<T> => raw == nullValue ? Absent : Midnight(raw, nanosecondsPerUnit);

    internal static string Midnight<T>(T? raw, long nanosecondsPerUnit) where T : struct, IBinaryInteger<T> => raw is { } present ? Midnight(present, nanosecondsPerUnit) : Absent;

    internal static string Midnight<T>(T? raw, long nanosecondsPerUnit, T nullValue) where T : struct, IBinaryInteger<T> => raw is { } present ? Midnight(present, nanosecondsPerUnit, nullValue) : Absent;

    private static string Clock(Int128 nanoseconds, long nanosecondsPerUnit)
    {
        var sign = Int128.IsNegative(nanoseconds) ? Negative : string.Empty;
        var magnitude = Int128.Abs(nanoseconds);
        var clock = string.Create(CultureInfo.InvariantCulture, $"{sign}{magnitude / NanosecondsPerHour:D2}:{magnitude % NanosecondsPerHour / NanosecondsPerMinute:D2}:{magnitude % NanosecondsPerMinute / NanosecondsPerSecond:D2}");
        var digits = 0;
        for (var unit = nanosecondsPerUnit; unit < NanosecondsPerSecond; unit *= DigitBase)
        {
            digits++;
        }
        var fraction = magnitude % NanosecondsPerSecond / nanosecondsPerUnit;
        return digits == 0 ? clock : $"{clock}.{fraction.ToString(CultureInfo.InvariantCulture).PadLeft(digits, Zero)}";
    }

    internal static string Bytes(ReadOnlyMemory<byte> value) => Convert.ToHexString(value.Span);

    internal static string Bytes(ReadOnlyMemory<byte>? value) => value is { } present ? Bytes(present) : Absent;

    internal static string Entries<T>(IEnumerable<T> entries) => $"[{string.Join(Separator, entries)}]";
}
