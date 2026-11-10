using System.Numerics;

namespace Iex.IexEquities.Tops.IexTp.v1_56;

internal static class Instant
{
    internal static DateTime ToDateTime<T>(T raw, long nanosecondsPerUnit) where T : IBinaryInteger<T>
    {
        var ticks = long.CreateSaturating(Int128.CreateTruncating(raw) * nanosecondsPerUnit / TimeSpan.NanosecondsPerTick);
        return ticks > DateTime.MaxValue.Ticks - DateTime.UnixEpoch.Ticks ? DateTime.MaxValue : ticks < DateTime.MinValue.Ticks - DateTime.UnixEpoch.Ticks ? DateTime.MinValue : DateTime.UnixEpoch.AddTicks(ticks);
    }

    private static Int128 Nanoseconds(DateTime value)
    {
        var utc = value.Kind == DateTimeKind.Local ? value.ToUniversalTime() : value;
        return (Int128)(utc - DateTime.UnixEpoch).Ticks * TimeSpan.NanosecondsPerTick;
    }

    private static Int128 Units(Int128 nanoseconds, long nanosecondsPerUnit)
    {
        var (units, remainder) = Int128.DivRem(nanoseconds, nanosecondsPerUnit);
        var magnitude = Int128.Abs(remainder);
        return magnitude >= nanosecondsPerUnit - magnitude ? units + Int128.Sign(remainder) : units;
    }

    internal static T ToRaw<T>(DateTime value, long nanosecondsPerUnit, T minimum, T maximum) where T : IBinaryInteger<T>
    {
        var units = Units(Nanoseconds(value), nanosecondsPerUnit);
        if (units > Int128.CreateSaturating(maximum))
            return maximum;
        if (units < Int128.CreateSaturating(minimum))
            return minimum;
        return T.CreateTruncating(units);
    }

    internal static T ToRaw<T>(DateTime value, long nanosecondsPerUnit, T minimum, T maximum, T nullValue) where T : IBinaryInteger<T>
    {
        var raw = ToRaw(value, nanosecondsPerUnit, minimum, maximum);
        return raw == nullValue ? OffReserved(raw, Nanoseconds(value).CompareTo(Int128.CreateTruncating(raw) * nanosecondsPerUnit), minimum, maximum) : raw;
    }

    private static T OffReserved<T>(T raw, int side, T minimum, T maximum) where T : IBinaryInteger<T>
    {
        var up = side >= 0 ? raw != maximum : raw == minimum;
        return up ? raw + T.One : raw - T.One;
    }
}
