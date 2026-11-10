namespace Iex.IexEquities.Tops.IexTp.v1_66;

internal static class Rules
{
    internal const int RuleTextAscii = 0;
    internal const int RuleTextUtf8 = 1;
    internal const int RuleTextUtf16Little = 2;
    internal const int RuleTextUtf16Big = 3;
    internal const int RuleTextLatin1 = 4;
    internal const int RuleTextWindows1252 = 5;
    internal const int RuleTextLatin5 = 6;
    internal const int RuleTextEucKr = 7;
    internal const int RuleTextHex = 8;
    internal const int RuleTextUtf8Maximum = 4;
    internal const uint RuleTextByteMask = 255U;
    internal const int RuleTextZero = 0;
    internal const int RuleEmptyWidth = 0;
    internal const int RuleNumberZero = 0;
    internal const int RuleNumberOne = 1;
    internal const double RuleScaleBase = 10D;
    internal const ulong RuleSequenceStep = 1UL;
    internal const bool RuleValuePresent = true;
    internal const int RuleTextOne = 1;
    internal const int RuleTextTwo = 2;
    internal const int RuleTextThree = 3;
    internal const int RuleTextNibbleBits = 4;
    internal const int RuleTextNibbleMask = 15;
    internal const int RuleTextAsciiLimit = 128;
    internal const int RuleTextC1Limit = 160;
    internal const int RuleTextReplacement = 0xFFFD;
    internal const int RuleTextByteBits = 8;
    internal const int RuleEucMinimum = 0xA1;
    internal const int RuleEucMaximum = 0xFE;
    internal const int RuleEucPlane = 94;
    internal const int RuleSurrogateStart = 0xD800;
    internal const int RuleSurrogateEnd = 0xDFFF;
    internal const int RuleLeadSurrogateEnd = 0xDBFF;
    internal const int RuleTrailSurrogateStart = 0xDC00;
    internal const int RuleSupplementaryStart = 0x10000;
    internal const int RuleSurrogateBits = 10;
    internal const string RuleTextHexDigits = "0123456789ABCDEF";
    internal const string RuleTextWindows1252Map = "\u20AC\uFFFD\u201A\u0192\u201E\u2026\u2020\u2021\u02C6\u2030\u0160\u2039\u0152\uFFFD\u017D\uFFFD\uFFFD\u2018\u2019\u201C\u201D\u2022\u2013\u2014\u02DC\u2122\u0161\u203A\u0153\uFFFD\u017E\u0178";
    internal const int RuleLatin5GUpperByte = 0xD0;
    internal const int RuleLatin5IUpperByte = 0xDD;
    internal const int RuleLatin5SUpperByte = 0xDE;
    internal const int RuleLatin5GLowerByte = 0xF0;
    internal const int RuleLatin5ILowerByte = 0xFD;
    internal const int RuleLatin5SLowerByte = 0xFE;
    internal const int RuleLatin5GUpper = 0x011E;
    internal const int RuleLatin5IUpper = 0x0130;
    internal const int RuleLatin5SUpper = 0x015E;
    internal const int RuleLatin5GLower = 0x011F;
    internal const int RuleLatin5ILower = 0x0131;
    internal const int RuleLatin5SLower = 0x015F;
    internal static int CompareRuleText(global::System.ReadOnlySpan<byte> left, int leftEncoding, global::System.ReadOnlySpan<byte> right, int rightEncoding)
    {
        long leftIndex = RuleTextZero;
        long rightIndex = RuleTextZero;
        uint leftPending = RuleTextZero;
        uint rightPending = RuleTextZero;
        int leftRemaining = RuleTextZero;
        int rightRemaining = RuleTextZero;
        while (true)
        {
            bool hasLeft = ReadRuleTextByte(left, leftEncoding, ref leftIndex, ref leftPending, ref leftRemaining, out int leftRune);
            bool hasRight = ReadRuleTextByte(right, rightEncoding, ref rightIndex, ref rightPending, ref rightRemaining, out int rightRune);
            if (!hasLeft || !hasRight) return hasLeft.CompareTo(hasRight);
            if (leftRune != rightRune) return leftRune.CompareTo(rightRune);
        }
    }
    internal static bool ReadRuleTextByte(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index, ref uint pending, ref int remaining, out int value)
    {
        if (remaining == RuleTextZero)
        {
            if (!ReadRuleTextRune(bytes, encoding, ref index, out int rune)) { value = RuleTextZero; return false; }
            global::System.Span<byte> utf8 = stackalloc byte[RuleTextUtf8Maximum];
            remaining = new global::System.Text.Rune(rune).EncodeToUtf8(utf8);
            pending = RuleTextZero;
            for (int part = RuleTextZero; part < remaining; part++)
            {
                pending |= (uint)utf8[part] << (part * RuleTextByteBits);
            }
        }
        value = (int)(pending & RuleTextByteMask);
        pending >>= RuleTextByteBits;
        remaining--;
        return true;
    }
    internal const double RuleIntegerRealUpper = 170141183460469231731687303715884105728D;
    internal const double RuleUnsignedIntegerRealUpper = 340282366920938463463374607431768211456D;
    internal const double RuleIntegerRealLower = -170141183460469231731687303715884105728D;
    internal const int RuleComparisonLess = -1;
    internal const int RuleComparisonEqual = 0;
    internal const int RuleComparisonGreater = 1;
    internal const int RuleRealMantissaBits = 52;
    internal const int RuleUnsignedIntegerBits = 128;
    internal const decimal RuleDecimalRadix = 10M;
    internal const uint RuleDecimalRadixOddFactor = 5U;
    internal static int CompareRuleIntegerUnsigned(global::System.Int128 integer, global::System.UInt128 unsigned)
    {
        return integer < RuleNumberZero ? RuleComparisonLess : ((global::System.UInt128)integer).CompareTo(unsigned);
    }
    internal static int? CompareRuleIntegerReal(global::System.UInt128 integer, double real)
    {
        if (double.IsNaN(real)) return null;
        if (real >= RuleUnsignedIntegerRealUpper) return RuleComparisonLess;
        if (real < RuleNumberZero) return RuleComparisonGreater;
        global::System.UInt128 truncated = checked((global::System.UInt128)real);
        int comparison = integer.CompareTo(truncated);
        if (comparison != RuleComparisonEqual || real == global::System.Math.Truncate(real)) return comparison;
        return RuleComparisonLess;
    }
    internal static int? CompareRuleIntegerReal(global::System.Int128 integer, double real)
    {
        if (double.IsNaN(real)) return null;
        if (real >= RuleIntegerRealUpper) return RuleComparisonLess;
        if (real < RuleIntegerRealLower) return RuleComparisonGreater;
        global::System.Int128 truncated = checked((global::System.Int128)real);
        int comparison = integer.CompareTo(truncated);
        if (comparison != RuleComparisonEqual || real == global::System.Math.Truncate(real)) return comparison;
        return real > RuleNumberZero ? RuleComparisonLess : RuleComparisonGreater;
    }
    internal static int CompareRuleIntegerDecimal(global::System.UInt128 integer, decimal value)
    {
        if (value < RuleNumberZero) return RuleComparisonGreater;
        decimal integral = decimal.Truncate(value);
        int comparison = integer.CompareTo((global::System.UInt128)integral);
        return comparison != RuleComparisonEqual ? comparison : -decimal.Sign(value - integral);
    }
    internal static int CompareRuleIntegerDecimal(global::System.Int128 integer, decimal value)
    {
        decimal integral = decimal.Truncate(value);
        int comparison = integer.CompareTo((global::System.Int128)integral);
        return comparison != RuleComparisonEqual ? comparison : -decimal.Sign(value - integral);
    }
    internal static int? CompareRuleDecimalReal(decimal value, double real)
    {
        decimal integral = decimal.Truncate(value);
        double realIntegral = global::System.Math.Truncate(real);
        int? comparison = CompareRuleIntegerReal((global::System.Int128)integral, realIntegral);
        if (comparison != RuleComparisonEqual) return comparison;
        decimal fraction = value - integral;
        double realFraction = real - realIntegral;
        int sign = decimal.Sign(fraction);
        int realSign = global::System.Math.Sign(realFraction);
        if (sign != realSign || sign == RuleNumberZero) return sign.CompareTo(realSign);
        fraction = decimal.Abs(fraction);
        realFraction = global::System.Math.Abs(realFraction);
        int shift = RuleRealMantissaBits - global::System.Math.ILogB(realFraction);
        global::System.UInt128 realNumerator = (ulong)global::System.Math.ScaleB(realFraction, shift);
        for (; fraction != decimal.Truncate(fraction); shift--)
        {
            fraction *= RuleDecimalRadix;
            realNumerator *= RuleDecimalRadixOddFactor;
        }
        global::System.UInt128 realQuotient = shift < RuleUnsignedIntegerBits ? realNumerator >> shift : global::System.UInt128.Zero;
        int magnitude = ((global::System.UInt128)fraction).CompareTo(realQuotient);
        if (magnitude == RuleComparisonEqual && realQuotient << shift != realNumerator) magnitude = RuleComparisonLess;
        return sign * magnitude;
    }
    internal static bool ReadRuleTextRune(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index, out int rune)
    {
        rune = RuleTextZero;
        if (encoding == RuleTextHex)
        {
            int byteIndex = (int)(index / RuleTextThree);
            int part = (int)(index % RuleTextThree);
            if (byteIndex >= bytes.Length || (part == RuleTextTwo && byteIndex == bytes.Length - RuleTextOne)) return false;
            rune = part == RuleTextTwo ? ' ' : RuleTextHexDigits[(bytes[byteIndex] >> (part == RuleTextZero ? RuleTextNibbleBits : RuleTextZero)) & RuleTextNibbleMask];
            index++;
            return true;
        }
        if (index >= bytes.Length) return false;
        if (encoding == RuleTextUtf8)
        {
            var status = global::System.Text.Rune.DecodeFromUtf8(bytes[(int)index..], out var scalar, out int consumed);
            if (status == global::System.Buffers.OperationStatus.NeedMoreData) { index = bytes.Length; rune = RuleTextReplacement; return true; }
            index += global::System.Math.Max(consumed, RuleTextOne);
            rune = status == global::System.Buffers.OperationStatus.Done ? scalar.Value : RuleTextReplacement;
            return true;
        }
        if (encoding == RuleTextUtf16Little || encoding == RuleTextUtf16Big) { rune = ReadRuleUtf16Rune(bytes, encoding, ref index); return true; }
        int value = bytes[(int)index++];
        if (encoding == RuleTextAscii) { rune = value < RuleTextAsciiLimit ? value : RuleTextReplacement; return true; }
        if (encoding == RuleTextWindows1252 && value >= RuleTextAsciiLimit && value < RuleTextC1Limit) { rune = RuleTextWindows1252Map[value - RuleTextAsciiLimit]; return true; }
        if (encoding == RuleTextLatin5)
        {
            rune = value switch { RuleLatin5GUpperByte => RuleLatin5GUpper, RuleLatin5IUpperByte => RuleLatin5IUpper, RuleLatin5SUpperByte => RuleLatin5SUpper, RuleLatin5GLowerByte => RuleLatin5GLower, RuleLatin5ILowerByte => RuleLatin5ILower, RuleLatin5SLowerByte => RuleLatin5SLower, _ => value };
            return true;
        }
        rune = value;
        return true;
    }
    internal static int ReadRuleUtf16Rune(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index)
    {
        if (bytes.Length - index < RuleTextTwo) { index = bytes.Length; return RuleTextReplacement; }
        int first = encoding == RuleTextUtf16Little ? bytes[(int)index] | (bytes[(int)index + RuleTextOne] << RuleTextByteBits) : (bytes[(int)index] << RuleTextByteBits) | bytes[(int)index + RuleTextOne];
        index += RuleTextTwo;
        if (first < RuleSurrogateStart || first > RuleSurrogateEnd) return first;
        if (first > RuleLeadSurrogateEnd || bytes.Length - index < RuleTextTwo) return RuleTextReplacement;
        int second = encoding == RuleTextUtf16Little ? bytes[(int)index] | (bytes[(int)index + RuleTextOne] << RuleTextByteBits) : (bytes[(int)index] << RuleTextByteBits) | bytes[(int)index + RuleTextOne];
        if (second < RuleTrailSurrogateStart || second > RuleSurrogateEnd) return RuleTextReplacement;
        index += RuleTextTwo;
        return RuleSupplementaryStart + ((first - RuleSurrogateStart) << RuleSurrogateBits) + second - RuleTrailSurrogateStart;
    }
    internal const int RuleTextUtf16UnitBytes = 2;
    internal const int TraversalNumberStackBytes = 64;
    internal static bool ReadTraversalNumber<T>(global::System.ReadOnlySpan<byte> text, int encoding, global::System.Globalization.NumberStyles styles, out T value, out bool present) where T : struct, global::System.Numerics.INumberBase<T>
    {
        scoped global::System.ReadOnlySpan<byte> digits = text;
        if (encoding == RuleTextUtf16Little || encoding == RuleTextUtf16Big)
        {
            int units = text.Length / RuleTextUtf16UnitBytes;
            global::System.Span<byte> narrowed = units <= TraversalNumberStackBytes ? stackalloc byte[TraversalNumberStackBytes] : new byte[units];
            int length = RuleTextZero;
            long index = RuleTextZero;
            while (index < text.Length)
            {
                if (!ReadRuleTextRune(text, encoding, ref index, out int rune) || rune >= RuleTextAsciiLimit)
                {
                    value = T.Zero;
                    present = false;
                    return false;
                }
                narrowed[length++] = (byte)rune;
            }
            digits = narrowed[..length];
        }
        present = T.TryParse(digits, styles, global::System.Globalization.CultureInfo.InvariantCulture, out value);
        return present || digits[global::System.Text.Ascii.Trim(digits)].IsEmpty;
    }
    internal static T ReadRuleNumber<T>(global::System.ReadOnlySpan<byte> text, int encoding, global::System.Globalization.NumberStyles styles) where T : struct, global::System.Numerics.INumberBase<T> => ReadTraversalNumber(text, encoding, styles, out T value, out _) ? value : InvalidRuleValue<T>();
    internal static C ReadTraversalInteger<C>(global::System.ReadOnlySpan<byte> range, bool littleEndian) where C : global::System.Numerics.IBinaryInteger<C>
    {
        C value = C.Zero;
        for (int index = 0; index < range.Length; index++)
        {
            int position = littleEndian ? range.Length - index - 1 : index;
            value = (value << (sizeof(byte) * 8)) | C.CreateTruncating(range[position]);
        }
        return value;
    }
    internal sealed class RuleValueException : global::System.Exception
    {
    }
    internal static T InvalidRuleValue<T>() where T : allows ref struct
    {
        throw new RuleValueException();
    }
    internal const ulong RuleCountNone = 0;
    private const ulong RuleCountOne = 1;
    internal static global::System.UInt128 CreateRuleCount(global::System.Int128 value) => value < RuleCountOne ? RuleCountNone : (global::System.UInt128)value;
    internal static global::System.UInt128 CreateRuleCount(double value)
    {
        if (!(value >= RuleCountOne)) return RuleCountNone;
        if (global::System.Double.IsPositiveInfinity(value)) throw new RuleValueException();
        return value >= RuleUnsignedIntegerRealUpper ? global::System.UInt128.MaxValue : (global::System.UInt128)value;
    }
}
