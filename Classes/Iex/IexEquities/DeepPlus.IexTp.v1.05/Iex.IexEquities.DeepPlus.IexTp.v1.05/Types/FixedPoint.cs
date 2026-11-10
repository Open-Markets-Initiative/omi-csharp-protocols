using System.Numerics;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

internal static class FixedPoint
{
    private const int MiddleShift = 32;
    private const int HighShift = 64;
    private const int DecimalBits = 96;

    internal static decimal ToDecimal<T>(T raw, byte scale) where T : IBinaryInteger<T>
    {
        var negative = T.IsNegative(raw);
        var magnitude = negative ? UInt128.CreateTruncating(-Int128.CreateTruncating(raw)) : UInt128.CreateTruncating(raw);
        if (magnitude >> DecimalBits != UInt128.Zero)
        {
            throw new OverflowException("The wire value does not fit a decimal.");
        }
        return new decimal((int)(uint)magnitude, (int)(uint)(magnitude >> MiddleShift), (int)(uint)(magnitude >> HighShift), negative, scale);
    }

    internal static T ToRaw<T>(decimal value, decimal factor, T minimum, T maximum) where T : IBinaryInteger<T>
        => Fit(Multiply(value, factor), minimum, maximum).Raw;

    internal static T ToRaw<T>(decimal value, decimal factor, T minimum, T maximum, T nullValue) where T : IBinaryInteger<T>
        => OffNull(Fit(Multiply(value, factor), minimum, maximum), minimum, maximum, nullValue);

    private static (bool Negative, UInt128 Magnitude, int Side) Multiply(decimal value, decimal factor)
    {
        var whole = decimal.Truncate(value);
        var part = (value - whole) * factor;
        var rounded = decimal.Round(part, MidpointRounding.AwayFromZero);
        var units = (UInt128)decimal.Abs(whole);
        var added = (UInt128)decimal.Abs(rounded);
        var scale = (UInt128)factor;
        var magnitude = units > (UInt128.MaxValue - added) / scale ? UInt128.MaxValue : units * scale + added;
        return (value < decimal.Zero, magnitude, part.CompareTo(rounded));
    }

    private static (T Raw, int Side) Fit<T>((bool Negative, UInt128 Magnitude, int Side) exact, T minimum, T maximum) where T : IBinaryInteger<T>
    {
        var raw = !exact.Negative ? T.CreateSaturating(exact.Magnitude)
            : T.CreateSaturating(exact.Magnitude > (UInt128)Int128.MaxValue ? Int128.MinValue : -(Int128)exact.Magnitude);
        return raw < minimum ? (minimum, -1) : raw > maximum ? (maximum, 1) : (raw, exact.Side);
    }

    private static T OffNull<T>((T Raw, int Side) fit, T minimum, T maximum, T nullValue) where T : IBinaryInteger<T>
    {
        if (fit.Raw != nullValue)
        {
            return fit.Raw;
        }
        return (fit.Side >= 0 ? fit.Raw != maximum : fit.Raw == minimum) ? fit.Raw + T.One : fit.Raw - T.One;
    }
}
