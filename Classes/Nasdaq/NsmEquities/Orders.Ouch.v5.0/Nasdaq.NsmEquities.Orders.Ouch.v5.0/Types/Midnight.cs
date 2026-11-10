using System.Numerics;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

internal static class Midnight
{
    internal static TimeOnly? ToTimeOnly<T>(T raw, long nanosecondsPerUnit, long unitsPerDay) where T : IBinaryInteger<T>
    {
        if (T.IsNegative(raw) || long.CreateSaturating(raw) >= unitsPerDay)
        {
            return null;
        }
        return new TimeOnly(long.CreateSaturating(raw) * nanosecondsPerUnit / TimeSpan.NanosecondsPerTick);
    }

    internal static T ToRaw<T>(TimeOnly value, long nanosecondsPerUnit, T wireMaximum) where T : IBinaryInteger<T>
    {
        var nanoseconds = value.Ticks * TimeSpan.NanosecondsPerTick;
        var units = Math.DivRem(nanoseconds, nanosecondsPerUnit, out var remainder);
        if (remainder >= nanosecondsPerUnit - remainder)
        {
            units++;
        }
        return T.Min(T.CreateSaturating(units), wireMaximum);
    }

    internal static T ToRaw<T>(TimeOnly value, long nanosecondsPerUnit, T wireMaximum, T nullValue) where T : IBinaryInteger<T>
    {
        var raw = ToRaw(value, nanosecondsPerUnit, wireMaximum);
        if (raw != nullValue)
        {
            return raw;
        }
        var side = ((Int128)value.Ticks * TimeSpan.NanosecondsPerTick).CompareTo(Int128.CreateSaturating(raw) * nanosecondsPerUnit);
        var up = side >= 0 ? raw != wireMaximum : raw == T.Zero;
        return up ? raw + T.One : raw - T.One;
    }
}
