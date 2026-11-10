using System;
using System.Buffers.Binary;
namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;
internal static partial class Parser
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
    private const int RuleTextUtf8Maximum = 4;
    private const uint RuleTextByteMask = 255U;
    private const int RuleTextZero = 0;
    private const double RuleScaleBase = 10D;
    internal const ulong RuleSequenceStep = 1UL;
    private const bool RuleValuePresent = true;
    private const int RuleTextOne = 1;
    private const int RuleTextTwo = 2;
    private const int RuleTextThree = 3;
    private const int RuleTextNibbleBits = 4;
    private const int RuleTextNibbleMask = 15;
    private const int RuleTextAsciiLimit = 128;
    private const int RuleTextC1Limit = 160;
    private const int RuleTextReplacement = 0xFFFD;
    private const int RuleTextByteBits = 8;
    private const int RuleEucMinimum = 0xA1;
    private const int RuleEucMaximum = 0xFE;
    private const int RuleEucPlane = 94;
    private const int RuleSurrogateStart = 0xD800;
    private const int RuleSurrogateEnd = 0xDFFF;
    private const int RuleLeadSurrogateEnd = 0xDBFF;
    private const int RuleTrailSurrogateStart = 0xDC00;
    private const int RuleSupplementaryStart = 0x10000;
    private const int RuleSurrogateBits = 10;
    private const string RuleTextHexDigits = "0123456789ABCDEF";
    private const string RuleTextWindows1252Map = "\u20AC\uFFFD\u201A\u0192\u201E\u2026\u2020\u2021\u02C6\u2030\u0160\u2039\u0152\uFFFD\u017D\uFFFD\uFFFD\u2018\u2019\u201C\u201D\u2022\u2013\u2014\u02DC\u2122\u0161\u203A\u0153\uFFFD\u017E\u0178";
    private const int RuleLatin5GUpperByte = 0xD0;
    private const int RuleLatin5IUpperByte = 0xDD;
    private const int RuleLatin5SUpperByte = 0xDE;
    private const int RuleLatin5GLowerByte = 0xF0;
    private const int RuleLatin5ILowerByte = 0xFD;
    private const int RuleLatin5SLowerByte = 0xFE;
    private const int RuleLatin5GUpper = 0x011E;
    private const int RuleLatin5IUpper = 0x0130;
    private const int RuleLatin5SUpper = 0x015E;
    private const int RuleLatin5GLower = 0x011F;
    private const int RuleLatin5ILower = 0x0131;
    private const int RuleLatin5SLower = 0x015F;
    private static int CompareRuleText(global::System.ReadOnlySpan<byte> left, int leftEncoding, global::System.ReadOnlySpan<byte> right, int rightEncoding)
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
    private static bool ReadRuleTextByte(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index, ref uint pending, ref int remaining, out int value)
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
    private const double RuleIntegerRealUpper = 170141183460469231731687303715884105728D;
    private const double RuleUnsignedIntegerRealUpper = 340282366920938463463374607431768211456D;
    private const double RuleIntegerRealLower = -170141183460469231731687303715884105728D;
    private const int RuleComparisonLess = -1;
    private const int RuleComparisonEqual = 0;
    private const int RuleComparisonGreater = 1;
    private const int RuleRealMantissaBits = 52;
    private const int RuleUnsignedIntegerBits = 128;
    private const decimal RuleDecimalRadix = 10M;
    private const uint RuleDecimalRadixOddFactor = 5U;
    private static int CompareRuleIntegerUnsigned(global::System.Int128 integer, global::System.UInt128 unsigned)
    {
        return integer < RuleComparisonEqual ? RuleComparisonLess : ((global::System.UInt128)integer).CompareTo(unsigned);
    }
    private static int? CompareRuleIntegerReal(global::System.UInt128 integer, double real)
    {
        if (double.IsNaN(real)) return null;
        if (real >= RuleUnsignedIntegerRealUpper) return RuleComparisonLess;
        if (real < RuleComparisonEqual) return RuleComparisonGreater;
        global::System.UInt128 truncated = checked((global::System.UInt128)real);
        int comparison = integer.CompareTo(truncated);
        if (comparison != RuleComparisonEqual || real == global::System.Math.Truncate(real)) return comparison;
        return RuleComparisonLess;
    }
    private static int? CompareRuleIntegerReal(global::System.Int128 integer, double real)
    {
        if (double.IsNaN(real)) return null;
        if (real >= RuleIntegerRealUpper) return RuleComparisonLess;
        if (real < RuleIntegerRealLower) return RuleComparisonGreater;
        global::System.Int128 truncated = checked((global::System.Int128)real);
        int comparison = integer.CompareTo(truncated);
        if (comparison != RuleComparisonEqual || real == global::System.Math.Truncate(real)) return comparison;
        return real > RuleComparisonEqual ? RuleComparisonLess : RuleComparisonGreater;
    }
    private static int CompareRuleIntegerDecimal(global::System.UInt128 integer, decimal value)
    {
        if (value < RuleComparisonEqual) return RuleComparisonGreater;
        decimal integral = decimal.Truncate(value);
        int comparison = integer.CompareTo((global::System.UInt128)integral);
        return comparison != RuleComparisonEqual ? comparison : -decimal.Sign(value - integral);
    }
    private static int CompareRuleIntegerDecimal(global::System.Int128 integer, decimal value)
    {
        decimal integral = decimal.Truncate(value);
        int comparison = integer.CompareTo((global::System.Int128)integral);
        return comparison != RuleComparisonEqual ? comparison : -decimal.Sign(value - integral);
    }
    private static int? CompareRuleDecimalReal(decimal value, double real)
    {
        decimal integral = decimal.Truncate(value);
        double realIntegral = global::System.Math.Truncate(real);
        int? comparison = CompareRuleIntegerReal((global::System.Int128)integral, realIntegral);
        if (comparison != RuleComparisonEqual) return comparison;
        decimal fraction = value - integral;
        double realFraction = real - realIntegral;
        int sign = decimal.Sign(fraction);
        int realSign = global::System.Math.Sign(realFraction);
        if (sign != realSign || sign == RuleComparisonEqual) return sign.CompareTo(realSign);
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
    private static bool ReadRuleTextRune(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index, out int rune)
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
    private static int ReadRuleUtf16Rune(global::System.ReadOnlySpan<byte> bytes, int encoding, ref long index)
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
    private sealed class RuleValueException : global::System.Exception
    {
    }
    private static T InvalidRuleValue<T>() where T : allows ref struct
    {
        throw new RuleValueException();
    }
    private static global::System.ReadOnlySpan<byte> TrimDependencyLeading(global::System.ReadOnlySpan<byte> bytes, int encoding, int fill)
    {
        long index = RuleTextZero;
        long start = RuleTextZero;
        while (ReadRuleTextRune(bytes, encoding, ref index, out int rune) && rune == fill) start = index;
        return bytes[(int)start..];
    }
    private static global::System.ReadOnlySpan<byte> TrimDependencyTrailing(global::System.ReadOnlySpan<byte> bytes, int encoding, int fill)
    {
        long index = RuleTextZero;
        long retained = RuleTextZero;
        while (ReadRuleTextRune(bytes, encoding, ref index, out int rune))
        {
            if (rune != fill) retained = index;
        }
        return bytes[..(int)retained];
    }
    private static global::System.ReadOnlySpan<byte> DependencyBeforeTerminator(global::System.ReadOnlySpan<byte> bytes, byte terminator)
    {
        int offset = bytes.IndexOf(terminator);
        return offset < 0 ? bytes : bytes[..offset];
    }
    private static C ReadTraversalInteger<C>(ReadOnlySpan<byte> range, bool littleEndian) where C : global::System.Numerics.IBinaryInteger<C>
    {
        C value = C.Zero;
        for (int index = 0; index < range.Length; index++)
        {
            int position = littleEndian ? range.Length - index - 1 : index;
            value = (value << (sizeof(byte) * 8)) | C.CreateTruncating(range[position]);
        }
        return value;
    }
    private static long ReadTraversalSignedInteger(ulong value, int length)
    {
        int shift = (sizeof(ulong) - length) * (sizeof(byte) * 8);
        return unchecked((long)(value << shift)) >> shift;
    }
    private static double ReadTraversalFloatingPoint(ReadOnlySpan<byte> range, bool littleEndian)
    {
        if (range.Length == sizeof(float))
        {
            return littleEndian ? global::System.Buffers.Binary.BinaryPrimitives.ReadSingleLittleEndian(range) : global::System.Buffers.Binary.BinaryPrimitives.ReadSingleBigEndian(range);
        }
        return littleEndian ? global::System.Buffers.Binary.BinaryPrimitives.ReadDoubleLittleEndian(range) : global::System.Buffers.Binary.BinaryPrimitives.ReadDoubleBigEndian(range);
    }
    private const int RuleCountWordBits = 64;
    private const int RuleCountBinary64Bits = 1024;
    private const int RuleCountWords = RuleCountBinary64Bits / RuleCountWordBits;
    private const int RuleCountFractionBits = 52;
    private const int RuleCountExponentBias = 1023;
    private const ulong RuleCountFractionMask = (1UL << RuleCountFractionBits) - 1;
    private const ulong RuleCountExponentMask = 0x7FF;
    private const int RuleCountZero = 0;
    private const ulong RuleCountOne = 1;
    [global::System.Runtime.CompilerServices.InlineArray(RuleCountWords)]
    internal struct RuleCountStorage
    {
        private ulong word;
    }
    internal struct RuleCount
    {
        internal RuleCountStorage Words;
        internal int Highest;
    }
    private static RuleCount CreateRuleCount(global::System.Int128 value)
    {
        RuleCount result = default;
        if (value <= RuleCountZero) return result;
        global::System.UInt128 unsigned = (global::System.UInt128)value;
        result.Words[RuleCountZero] = (ulong)unsigned;
        result.Words[(int)RuleCountOne] = (ulong)(unsigned >> RuleCountWordBits);
        result.Highest = result.Words[(int)RuleCountOne] == RuleCountZero ? RuleCountZero : (int)RuleCountOne;
        return result;
    }
    private static RuleCount CreateRuleCount(double value)
    {
        RuleCount result = default;
        if (!(value >= RuleCountOne)) return result;
        if (global::System.Double.IsPositiveInfinity(value)) throw new RuleValueException();
        ulong bits = (ulong)global::System.BitConverter.DoubleToInt64Bits(value);
        int exponent = (int)((bits >> RuleCountFractionBits) & RuleCountExponentMask) - RuleCountExponentBias;
        ulong significand = (bits & RuleCountFractionMask) | (RuleCountOne << RuleCountFractionBits);
        int shift = exponent - RuleCountFractionBits;
        if (shift < RuleCountZero)
        {
            result.Words[RuleCountZero] = significand >> -shift;
            return result;
        }
        int word = shift / RuleCountWordBits;
        int remainder = shift % RuleCountWordBits;
        result.Words[word] = significand << remainder;
        result.Highest = word;
        if (remainder != RuleCountZero && significand >> (RuleCountWordBits - remainder) is var high && high != RuleCountZero)
        {
            result.Words[word + (int)RuleCountOne] = high;
            result.Highest = word + (int)RuleCountOne;
        }
        return result;
    }
    private static bool HasRuleCount(in RuleCount count) => count.Highest != RuleCountZero || count.Words[RuleCountZero] != RuleCountZero;
    private static bool TakeRuleCount(ref RuleCount count)
    {
        if (count.Highest == RuleCountZero && count.Words[RuleCountZero] == RuleCountZero) return false;
        int word = RuleCountZero;
        while (count.Words[word] == RuleCountZero)
        {
            count.Words[word] = global::System.UInt64.MaxValue;
            word++;
        }
        count.Words[word]--;
        while (count.Highest > RuleCountZero && count.Words[count.Highest] == RuleCountZero) count.Highest--;
        return true;
    }
    internal static bool TryRuleCountValue(in RuleCount count, out int value)
    {
        value = RuleCountZero;
        if (count.Highest != RuleCountZero || count.Words[RuleCountZero] > int.MaxValue) return false;
        value = (int)count.Words[RuleCountZero];
        return true;
    }
    internal static class Position
    {
        internal const int Minimum = 0;
        internal const int Failure = -1;
        internal const int Absent = -4;
        internal const int NoEntries = 0;
        internal const int NoCase = -2;
        internal const int Ended = -3;
    }
    internal static int Valid(int position) => position == Position.Failure ? throw new global::System.ArgumentOutOfRangeException(nameof(position), PastEnd) : position;
    internal const string NoEntry = "The entries have no current entry.";
    internal const string CountTooLarge = "The number of entries is larger than an int can hold.";
    internal const string OtherCase = "The bytes select another case.";
    internal const string Absent = "The bytes leave the member out.";
    internal const string FrameLength = "The bytes state a frame length that is not valid.";
    internal const string PastEnd = "The bytes end before the length that they state.";
    internal const string Unconverted = "The bytes cannot be converted, or they convert to more bytes than the maximum.";
    internal const int SizeAskCustSize = 4;
    internal const int SizeAskMarketSize = 4;
    internal const int SizeAskNttMarketSize = 4;
    internal const int SizeAskNttSize = 4;
    internal const int SizeAskPrice = 4;
    internal const int SizeAskProCustSize = 4;
    internal const int SizeAskSize = 4;
    internal const int SizeBidCustSize = 4;
    internal const int SizeBidMarketSize = 4;
    internal const int SizeBidNttMarketSize = 4;
    internal const int SizeBidNttSize = 4;
    internal const int SizeBidPrice = 4;
    internal const int SizeBidProCustSize = 4;
    internal const int SizeBidSize = 4;
    internal const int SizeComplexStrategyTickerMessage = 51;
    internal const int SizeCurrentDay = 1;
    internal const int SizeCurrentMonth = 1;
    internal const int SizeCurrentTradingState = 1;
    internal const int SizeCurrentYear = 2;
    internal const int SizeCustSize = 4;
    internal const int SizeEventCode = 1;
    internal const int SizeExpirationDay = 1;
    internal const int SizeExpirationMonth = 1;
    internal const int SizeExpirationYear = 1;
    internal const int SizeExplicitStrikePrice = 8;
    internal const int SizeFirst = 8;
    internal const int SizeHigh = 8;
    internal const int SizeLastPrice = 8;
    internal const int SizeLegId = 1;
    internal const int SizeLegInformation = 28;
    internal const int SizeLegRatio = 4;
    internal const int SizeLow = 8;
    internal const int SizeMarketSize = 4;
    internal const int SizeMessageCount = 2;
    internal const int SizeMessageHeader = 3;
    internal const int SizeMessageLength = 2;
    internal const int SizeMessageType = 1;
    internal const int SizeNttMarketSize = 4;
    internal const int SizeNttSize = 4;
    internal const int SizeNumberOfLegs = 1;
    internal const int SizeOpenState = 1;
    internal const int SizeOptionId = 4;
    internal const int SizeOptionType = 1;
    internal const int SizePacketHeader = 20;
    internal const int SizePrice = 4;
    internal const int SizeProCustSize = 4;
    internal const int SizeQuoteCondition = 1;
    internal const int SizeSecuritySymbol = 6;
    internal const int SizeSequenceNumber = 8;
    internal const int SizeSession = 10;
    internal const int SizeSide = 1;
    internal const int SizeSize = 4;
    internal const int SizeSource = 1;
    internal const int SizeStrategyBestAskUpdate = 39;
    internal const int SizeStrategyBestBidAndAskUpdate = 67;
    internal const int SizeStrategyBestBidUpdate = 39;
    internal const int SizeStrategyId = 4;
    internal const int SizeStrategyOpenClosedMessage = 11;
    internal const int SizeStrategyTradingActionMessage = 11;
    internal const int SizeStrategyType = 1;
    internal const int SizeSubVersion = 1;
    internal const int SizeSystemEventMessage = 13;
    internal const int SizeTimestamp = 6;
    internal const int SizeTradeCondition = 1;
    internal const int SizeUnderlyingSymbol = 13;
    internal const int SizeVersion = 1;
    internal const int SizeVolume = 4;
    internal static int MeasureComplexStrategyDirectoryMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int numberOfLegsDependencyReadDistanceFromLegInformation = -1;
        const int numberOfLegsDependencyReadWidth = 1;
        long numberOfLegsDependencyReadPosition;
        byte numberOfLegsValue;
        RuleCount LegInformationCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int complexStrategyDirectoryMessageTimestampMeasuredBytes = SizeTimestamp;
            if (complexStrategyDirectoryMessageTimestampMeasuredBytes < Position.Minimum || complexStrategyDirectoryMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += complexStrategyDirectoryMessageTimestampMeasuredBytes;
        }
        
        {
            const int complexStrategyDirectoryMessageStrategyIdMeasuredBytes = SizeStrategyId;
            if (complexStrategyDirectoryMessageStrategyIdMeasuredBytes < Position.Minimum || complexStrategyDirectoryMessageStrategyIdMeasuredBytes > (long)limit - index) return Position.Failure;
            index += complexStrategyDirectoryMessageStrategyIdMeasuredBytes;
        }
        
        {
            const int strategyTypeMeasuredBytes = SizeStrategyType;
            if (strategyTypeMeasuredBytes < Position.Minimum || strategyTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += strategyTypeMeasuredBytes;
        }
        
        {
            const int sourceMeasuredBytes = SizeSource;
            if (sourceMeasuredBytes < Position.Minimum || sourceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += sourceMeasuredBytes;
        }
        
        {
            const int underlyingSymbolMeasuredBytes = SizeUnderlyingSymbol;
            if (underlyingSymbolMeasuredBytes < Position.Minimum || underlyingSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += underlyingSymbolMeasuredBytes;
        }
        
        {
            const int numberOfLegsMeasuredBytes = SizeNumberOfLegs;
            if (numberOfLegsMeasuredBytes < Position.Minimum || numberOfLegsMeasuredBytes > (long)limit - index) return Position.Failure;
            index += numberOfLegsMeasuredBytes;
        }
        
        {
            numberOfLegsDependencyReadPosition = (long)index + numberOfLegsDependencyReadDistanceFromLegInformation;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || numberOfLegsDependencyReadPosition < Position.Minimum || numberOfLegsDependencyReadPosition > limit || numberOfLegsDependencyReadWidth > limit - numberOfLegsDependencyReadPosition) return Position.Failure;
            numberOfLegsValue = bytes[(int)numberOfLegsDependencyReadPosition];
            try
            {
                LegInformationCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(numberOfLegsValue)));
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            while (HasRuleCount(in LegInformationCountRule0Entries))
            {
                int legInformationEntryStart = index;
                
                {
                    const int legInformationMeasuredBytes = SizeLegInformation;
                    if (legInformationMeasuredBytes < Position.Minimum || legInformationMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += legInformationMeasuredBytes;
                }
                TakeRuleCount(ref LegInformationCountRule0Entries);
            }
            if (HasRuleCount(in LegInformationCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int StepComplexStrategyDirectoryMessageLegInformation(ReadOnlySpan<byte> bytes, int start, int limit, scoped ref Parser.RuleCount LegInformationCountRule0Entries)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(HasRuleCount(in LegInformationCountRule0Entries))) return Position.Ended;
        int legInformationEntryStart = index;
        
        {
            const int legInformationMeasuredBytes = SizeLegInformation;
            if (legInformationMeasuredBytes < Position.Minimum || legInformationMeasuredBytes > (long)limit - index) return Position.Failure;
            index += legInformationMeasuredBytes;
        }
        TakeRuleCount(ref LegInformationCountRule0Entries);
        return index;
    }
    internal const int SizeEndOfSession = 0;
    internal const int SizeHeartbeat = 0;
    internal static int MeasureMessages(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        bool MessagesBranchRule0Selected;
        bool MessagesBranchRule1Selected;
        RuleCount MessageCountRule0Entries;
        const int messageLengthDependencyReadDistanceFromMessage = 0;
        const int messageLengthDependencyReadWidth = 2;
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        global::System.Int128 MessageSizeRule1Width;
        int MessageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const ushort MessagesBranchRule0Data2 = 0;
            try
            {
                MessagesBranchRule0Selected = (dependency0 == MessagesBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MessagesBranchRule0Selected)
            {
                
                {
                    const int heartbeatMeasuredBytes = SizeHeartbeat;
                    if (heartbeatMeasuredBytes < Position.Minimum || heartbeatMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += heartbeatMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const ushort MessagesBranchRule1Data2 = 65535;
            try
            {
                MessagesBranchRule1Selected = (dependency0 == MessagesBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MessagesBranchRule1Selected)
            {
                
                {
                    const int endOfSessionMeasuredBytes = SizeEndOfSession;
                    if (endOfSessionMeasuredBytes < Position.Minimum || endOfSessionMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += endOfSessionMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            
            {
                try
                {
                    MessageCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(dependency0)));
                }
                catch (global::System.OverflowException)
                {
                    return Position.Failure;
                }
                catch (global::System.DivideByZeroException)
                {
                    return Position.Failure;
                }
                catch (RuleValueException)
                {
                    return Position.Failure;
                }
                while (HasRuleCount(in MessageCountRule0Entries))
                {
                    int messageEntryStart = index;
                    
                    {
                        messageLengthDependencyReadPosition = (long)index + messageLengthDependencyReadDistanceFromMessage;
                        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) return Position.Failure;
                        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
                        const long MessageSizeRule1Data2 = 2;
                        try
                        {
                            MessageSizeRule1Width = ((messageLengthValue) switch { var MessageSizeRule1ArithmeticLeft0 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight1)))) }) });
                        }
                        catch (global::System.OverflowException)
                        {
                            return Position.Failure;
                        }
                        catch (global::System.DivideByZeroException)
                        {
                            return Position.Failure;
                        }
                        catch (RuleValueException)
                        {
                            return Position.Failure;
                        }
                        if (index < Position.Minimum || limit < index || limit > bytes.Length) return Position.Failure;
                        if (MessageSizeRule1Width < Position.Minimum || MessageSizeRule1Width > (long)limit - index) return Position.Failure;
                        MessageSizeRule1End = index + (int)MessageSizeRule1Width;
                        int messageEnd = MessageSizeRule1End;
                        index = messageEnd;
                    }
                    TakeRuleCount(ref MessageCountRule0Entries);
                }
                if (HasRuleCount(in MessageCountRule0Entries)) return Position.Failure;
            }
            return index;
        }
    }
    internal static int StepMessagesMessage(ReadOnlySpan<byte> bytes, int start, int limit, scoped ref Parser.RuleCount MessageCountRule0Entries)
    {
        const int messageLengthDependencyReadDistanceFromMessage = 0;
        const int messageLengthDependencyReadWidth = 2;
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        global::System.Int128 MessageSizeRule1Width;
        int MessageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(HasRuleCount(in MessageCountRule0Entries))) return Position.Ended;
        int messageEntryStart = index;
        
        {
            messageLengthDependencyReadPosition = (long)index + messageLengthDependencyReadDistanceFromMessage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) return Position.Failure;
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            const long MessageSizeRule1Data2 = 2;
            try
            {
                MessageSizeRule1Width = ((messageLengthValue) switch { var MessageSizeRule1ArithmeticLeft0 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (index < Position.Minimum || limit < index || limit > bytes.Length) return Position.Failure;
            if (MessageSizeRule1Width < Position.Minimum || MessageSizeRule1Width > (long)limit - index) return Position.Failure;
            MessageSizeRule1End = index + (int)MessageSizeRule1Width;
            int messageEnd = MessageSizeRule1End;
            index = messageEnd;
        }
        TakeRuleCount(ref MessageCountRule0Entries);
        return index;
    }
    internal static int ComplexStrategyDirectoryMessageLegInformationStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out Parser.RuleCount received1)
    {
        received0 = default;
        received1 = default;
        const int numberOfLegsDependencyReadDistanceFromLegInformation = -1;
        const int numberOfLegsDependencyReadWidth = 1;
        long numberOfLegsDependencyReadPosition;
        byte numberOfLegsValue;
        RuleCount LegInformationCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int complexStrategyDirectoryMessageTimestampMeasuredBytes = SizeTimestamp;
            if (complexStrategyDirectoryMessageTimestampMeasuredBytes < Position.Minimum || complexStrategyDirectoryMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += complexStrategyDirectoryMessageTimestampMeasuredBytes;
        }
        
        {
            const int complexStrategyDirectoryMessageStrategyIdMeasuredBytes = SizeStrategyId;
            if (complexStrategyDirectoryMessageStrategyIdMeasuredBytes < Position.Minimum || complexStrategyDirectoryMessageStrategyIdMeasuredBytes > (long)limit - index) return Position.Failure;
            index += complexStrategyDirectoryMessageStrategyIdMeasuredBytes;
        }
        
        {
            const int strategyTypeMeasuredBytes = SizeStrategyType;
            if (strategyTypeMeasuredBytes < Position.Minimum || strategyTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += strategyTypeMeasuredBytes;
        }
        
        {
            const int sourceMeasuredBytes = SizeSource;
            if (sourceMeasuredBytes < Position.Minimum || sourceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += sourceMeasuredBytes;
        }
        
        {
            const int underlyingSymbolMeasuredBytes = SizeUnderlyingSymbol;
            if (underlyingSymbolMeasuredBytes < Position.Minimum || underlyingSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += underlyingSymbolMeasuredBytes;
        }
        
        {
            const int numberOfLegsMeasuredBytes = SizeNumberOfLegs;
            if (numberOfLegsMeasuredBytes < Position.Minimum || numberOfLegsMeasuredBytes > (long)limit - index) return Position.Failure;
            index += numberOfLegsMeasuredBytes;
        }
        numberOfLegsDependencyReadPosition = (long)index + numberOfLegsDependencyReadDistanceFromLegInformation;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || numberOfLegsDependencyReadPosition < Position.Minimum || numberOfLegsDependencyReadPosition > limit || numberOfLegsDependencyReadWidth > limit - numberOfLegsDependencyReadPosition) return Position.Failure;
        numberOfLegsValue = bytes[(int)numberOfLegsDependencyReadPosition];
        try
        {
            LegInformationCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(numberOfLegsValue)));
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
        }
        catch (global::System.DivideByZeroException)
        {
            return Position.Failure;
        }
        catch (RuleValueException)
        {
            return Position.Failure;
        }
        received0 = limit;
        received1 = LegInformationCountRule0Entries;
        return index;
    }
    internal static int SelectPayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool PayloadBranchRule1Selected;
        bool PayloadBranchRule2Selected;
        bool PayloadBranchRule3Selected;
        bool PayloadBranchRule4Selected;
        bool PayloadBranchRule5Selected;
        bool PayloadBranchRule6Selected;
        bool PayloadBranchRule7Selected;
        bool PayloadBranchRule8Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = "S"u8;
            try
            {
                PayloadBranchRule1Selected = dependency0.SequenceEqual(PayloadBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule1Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = "R"u8;
            try
            {
                PayloadBranchRule2Selected = dependency0.SequenceEqual(PayloadBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule2Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = "O"u8;
            try
            {
                PayloadBranchRule3Selected = dependency0.SequenceEqual(PayloadBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule3Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = "H"u8;
            try
            {
                PayloadBranchRule4Selected = dependency0.SequenceEqual(PayloadBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule4Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = "C"u8;
            try
            {
                PayloadBranchRule5Selected = dependency0.SequenceEqual(PayloadBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule5Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = "D"u8;
            try
            {
                PayloadBranchRule6Selected = dependency0.SequenceEqual(PayloadBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule6Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = "E"u8;
            try
            {
                PayloadBranchRule7Selected = dependency0.SequenceEqual(PayloadBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule7Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = "t"u8;
            try
            {
                PayloadBranchRule8Selected = dependency0.SequenceEqual(PayloadBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (PayloadBranchRule8Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage;
            }
        }
        return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.None;
    }
    internal static int MessagePayloadStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int messageLengthDependencyReadDistanceFromPayload = -3;
        const int messageLengthDependencyReadWidth = 2;
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        const int messageTypeDependencyReadDistanceFromPayload = -1;
        const int messageTypeDependencyReadWidth = 1;
        long messageTypeDependencyReadPosition;
        ReadOnlySpan<byte> messageTypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int messageHeaderMeasuredBytes = SizeMessageHeader;
            if (messageHeaderMeasuredBytes < Position.Minimum || messageHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += messageHeaderMeasuredBytes;
        }
        messageLengthDependencyReadPosition = (long)index + messageLengthDependencyReadDistanceFromPayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) return Position.Failure;
        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
        messageTypeDependencyReadPosition = (long)index + messageTypeDependencyReadDistanceFromPayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > limit || messageTypeDependencyReadWidth > limit - messageTypeDependencyReadPosition) return Position.Failure;
        messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
        received0 = messageTypeValue;
        received1 = messageLengthValue;
        return index;
    }
    internal static int MessagePayloadEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 PayloadSizeRule0Width;
        int PayloadSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long PayloadSizeRule0Data2 = 1;
            try
            {
                PayloadSizeRule0Width = ((dependency0) switch { var PayloadSizeRule0ArithmeticLeft0 => ((PayloadSizeRule0Data2) switch { var PayloadSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(PayloadSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(PayloadSizeRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (index < Position.Minimum || limit < index || limit > bytes.Length) return Position.Failure;
            if (PayloadSizeRule0Width < Position.Minimum || PayloadSizeRule0Width > (long)limit - index) return Position.Failure;
            PayloadSizeRule0End = index + (int)PayloadSizeRule0Width;
            int payloadEnd = PayloadSizeRule0End;
            index = payloadEnd;
        }
        return index;
    }
    internal static int SelectMessages(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        bool MessagesBranchRule0Selected;
        bool MessagesBranchRule1Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const ushort MessagesBranchRule0Data2 = 0;
            try
            {
                MessagesBranchRule0Selected = (dependency0 == MessagesBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MessagesBranchRule0Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat;
            }
        }
        
        {
            const ushort MessagesBranchRule1Data2 = 65535;
            try
            {
                MessagesBranchRule1Selected = (dependency0 == MessagesBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MessagesBranchRule1Selected)
            {
                return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession;
            }
        }
        
        {
            return (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message;
        }
    }
    internal static int MessagesMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0, out int received0, out Parser.RuleCount received1)
    {
        received0 = default;
        received1 = default;
        RuleCount MessageCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        try
        {
            MessageCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(dependency0)));
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
        }
        catch (global::System.DivideByZeroException)
        {
            return Position.Failure;
        }
        catch (RuleValueException)
        {
            return Position.Failure;
        }
        received0 = limit;
        received1 = MessageCountRule0Entries;
        return index;
    }
    internal static int PacketMessagesStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ushort received0)
    {
        received0 = default;
        const int messageCountDependencyReadDistanceFromPacket = 18;
        const int messageCountDependencyReadWidth = 2;
        long messageCountDependencyReadPosition;
        ushort messageCountValue;
        const int messageCountDependencyReadDistanceFromMessages = -2;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        messageCountDependencyReadPosition = (long)start + messageCountDependencyReadDistanceFromPacket;
        if (start < Position.Minimum || start > limit || limit < Position.Minimum || limit > bytes.Length || messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > limit || messageCountDependencyReadWidth > limit - messageCountDependencyReadPosition) return Position.Failure;
        messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
        
        {
            const int packetHeaderMeasuredBytes = SizePacketHeader;
            if (packetHeaderMeasuredBytes < Position.Minimum || packetHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += packetHeaderMeasuredBytes;
        }
        messageCountDependencyReadPosition = (long)index + messageCountDependencyReadDistanceFromMessages;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > limit || messageCountDependencyReadWidth > limit - messageCountDependencyReadPosition) return Position.Failure;
        messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
        received0 = messageCountValue;
        return index;
    }
}
