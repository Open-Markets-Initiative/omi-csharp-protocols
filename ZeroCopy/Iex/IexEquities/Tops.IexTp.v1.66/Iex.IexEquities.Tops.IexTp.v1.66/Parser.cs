using System;
using System.Buffers.Binary;
namespace Iex.IexEquities.Tops.IexTp.v1_66;
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
    internal const int SizeAdjustedPocPrice = 8;
    internal const int SizeAskPrice = 8;
    internal const int SizeAskSize = 4;
    internal const int SizeAuctionBookClearingPrice = 8;
    internal const int SizeAuctionInformationMessage = 79;
    internal const int SizeAuctionType = 1;
    internal const int SizeBidPrice = 8;
    internal const int SizeBidSize = 4;
    internal const int SizeChannelId = 4;
    internal const int SizeCollarReferencePrice = 8;
    internal const int SizeDetail = 1;
    internal const int SizeExtensionNumber = 1;
    internal const int SizeFirstMessageSequenceNumber = 8;
    internal const int SizeIextpHeader = 40;
    internal const int SizeImbalanceShares = 4;
    internal const int SizeImbalanceSide = 1;
    internal const int SizeIndicativeClearingPrice = 8;
    internal const int SizeLowerAuctionCollar = 8;
    internal const int SizeLuldTier = 1;
    internal const int SizeMessageCount = 2;
    internal const int SizeMessageHeader = 3;
    internal const int SizeMessageLength = 2;
    internal const int SizeMessageProtocolId = 2;
    internal const int SizeMessageType = 1;
    internal const int SizeOfficialPrice = 8;
    internal const int SizeOfficialPriceMessage = 25;
    internal const int SizeOperationalHaltStatus = 1;
    internal const int SizeOperationalHaltStatusMessage = 17;
    internal const int SizePairedShares = 4;
    internal const int SizePayloadLength = 2;
    internal const int SizePrice = 8;
    internal const int SizePriceType = 1;
    internal const int SizeQuoteUpdateMessage = 41;
    internal const int SizeReason = 4;
    internal const int SizeReferencePrice = 8;
    internal const int SizeReserved = 1;
    internal const int SizeRetailLiquidityIndicator = 1;
    internal const int SizeRetailLiquidityIndicatorMessage = 17;
    internal const int SizeRoundLotSize = 4;
    internal const int SizeScheduledAuctionTime = 4;
    internal const int SizeSecurityDirectoryMessage = 30;
    internal const int SizeSendTime = 8;
    internal const int SizeSessionId = 4;
    internal const int SizeShortSalePriceTestStatus = 1;
    internal const int SizeShortSalePriceTestStatusMessage = 18;
    internal const int SizeSize = 4;
    internal const int SizeStreamOffset = 8;
    internal const int SizeSymbol = 8;
    internal const int SizeSystemEvent = 1;
    internal const int SizeSystemEventMessage = 9;
    internal const int SizeTimestamp = 8;
    internal const int SizeTradeBreakMessage = 37;
    internal const int SizeTradeId = 8;
    internal const int SizeTradeReportMessage = 37;
    internal const int SizeTradingStatus = 1;
    internal const int SizeTradingStatusMessage = 21;
    internal const int SizeUpperAuctionCollar = 8;
    internal const int SizeVersion = 1;
    internal const int SizeSaleConditionFlags = 1;
    internal const int SizeQuoteUpdateFlags = 1;
    internal const int SizeSecurityDirectoryFlags = 1;
    internal static int MeasureMessageData(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool MessageDataBranchRule0Selected;
        bool MessageDataBranchRule1Selected;
        bool MessageDataBranchRule2Selected;
        bool MessageDataBranchRule3Selected;
        bool MessageDataBranchRule4Selected;
        bool MessageDataBranchRule5Selected;
        bool MessageDataBranchRule6Selected;
        bool MessageDataBranchRule7Selected;
        bool MessageDataBranchRule8Selected;
        bool MessageDataBranchRule9Selected;
        bool MessageDataBranchRule10Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule0Data2 = "S"u8;
            try
            {
                MessageDataBranchRule0Selected = dependency0.SequenceEqual(MessageDataBranchRule0Data2);
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
            if (MessageDataBranchRule0Selected)
            {
                
                {
                    const int systemEventMessageMeasuredBytes = SizeSystemEventMessage;
                    if (systemEventMessageMeasuredBytes < Position.Minimum || systemEventMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += systemEventMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule1Data2 = "D"u8;
            try
            {
                MessageDataBranchRule1Selected = dependency0.SequenceEqual(MessageDataBranchRule1Data2);
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
            if (MessageDataBranchRule1Selected)
            {
                
                {
                    const int securityDirectoryMessageMeasuredBytes = SizeSecurityDirectoryMessage;
                    if (securityDirectoryMessageMeasuredBytes < Position.Minimum || securityDirectoryMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += securityDirectoryMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule2Data2 = "H"u8;
            try
            {
                MessageDataBranchRule2Selected = dependency0.SequenceEqual(MessageDataBranchRule2Data2);
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
            if (MessageDataBranchRule2Selected)
            {
                
                {
                    const int tradingStatusMessageMeasuredBytes = SizeTradingStatusMessage;
                    if (tradingStatusMessageMeasuredBytes < Position.Minimum || tradingStatusMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += tradingStatusMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule3Data2 = "I"u8;
            try
            {
                MessageDataBranchRule3Selected = dependency0.SequenceEqual(MessageDataBranchRule3Data2);
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
            if (MessageDataBranchRule3Selected)
            {
                
                {
                    const int retailLiquidityIndicatorMessageMeasuredBytes = SizeRetailLiquidityIndicatorMessage;
                    if (retailLiquidityIndicatorMessageMeasuredBytes < Position.Minimum || retailLiquidityIndicatorMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += retailLiquidityIndicatorMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule4Data2 = "O"u8;
            try
            {
                MessageDataBranchRule4Selected = dependency0.SequenceEqual(MessageDataBranchRule4Data2);
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
            if (MessageDataBranchRule4Selected)
            {
                
                {
                    const int operationalHaltStatusMessageMeasuredBytes = SizeOperationalHaltStatusMessage;
                    if (operationalHaltStatusMessageMeasuredBytes < Position.Minimum || operationalHaltStatusMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += operationalHaltStatusMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule5Data2 = "P"u8;
            try
            {
                MessageDataBranchRule5Selected = dependency0.SequenceEqual(MessageDataBranchRule5Data2);
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
            if (MessageDataBranchRule5Selected)
            {
                
                {
                    const int shortSalePriceTestStatusMessageMeasuredBytes = SizeShortSalePriceTestStatusMessage;
                    if (shortSalePriceTestStatusMessageMeasuredBytes < Position.Minimum || shortSalePriceTestStatusMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += shortSalePriceTestStatusMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule6Data2 = "Q"u8;
            try
            {
                MessageDataBranchRule6Selected = dependency0.SequenceEqual(MessageDataBranchRule6Data2);
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
            if (MessageDataBranchRule6Selected)
            {
                
                {
                    const int quoteUpdateMessageMeasuredBytes = SizeQuoteUpdateMessage;
                    if (quoteUpdateMessageMeasuredBytes < Position.Minimum || quoteUpdateMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += quoteUpdateMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule7Data2 = "T"u8;
            try
            {
                MessageDataBranchRule7Selected = dependency0.SequenceEqual(MessageDataBranchRule7Data2);
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
            if (MessageDataBranchRule7Selected)
            {
                
                {
                    const int tradeReportMessageMeasuredBytes = SizeTradeReportMessage;
                    if (tradeReportMessageMeasuredBytes < Position.Minimum || tradeReportMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += tradeReportMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule8Data2 = "X"u8;
            try
            {
                MessageDataBranchRule8Selected = dependency0.SequenceEqual(MessageDataBranchRule8Data2);
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
            if (MessageDataBranchRule8Selected)
            {
                
                {
                    const int officialPriceMessageMeasuredBytes = SizeOfficialPriceMessage;
                    if (officialPriceMessageMeasuredBytes < Position.Minimum || officialPriceMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += officialPriceMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule9Data2 = "B"u8;
            try
            {
                MessageDataBranchRule9Selected = dependency0.SequenceEqual(MessageDataBranchRule9Data2);
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
            if (MessageDataBranchRule9Selected)
            {
                
                {
                    const int tradeBreakMessageMeasuredBytes = SizeTradeBreakMessage;
                    if (tradeBreakMessageMeasuredBytes < Position.Minimum || tradeBreakMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += tradeBreakMessageMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule10Data2 = "A"u8;
            try
            {
                MessageDataBranchRule10Selected = dependency0.SequenceEqual(MessageDataBranchRule10Data2);
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
            if (MessageDataBranchRule10Selected)
            {
                
                {
                    const int auctionInformationMessageMeasuredBytes = SizeAuctionInformationMessage;
                    if (auctionInformationMessageMeasuredBytes < Position.Minimum || auctionInformationMessageMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += auctionInformationMessageMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal const int SizeHeartbeat = 0;
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
            messageLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
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
    internal static int SelectMessageData(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool MessageDataBranchRule0Selected;
        bool MessageDataBranchRule1Selected;
        bool MessageDataBranchRule2Selected;
        bool MessageDataBranchRule3Selected;
        bool MessageDataBranchRule4Selected;
        bool MessageDataBranchRule5Selected;
        bool MessageDataBranchRule6Selected;
        bool MessageDataBranchRule7Selected;
        bool MessageDataBranchRule8Selected;
        bool MessageDataBranchRule9Selected;
        bool MessageDataBranchRule10Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule0Data2 = "S"u8;
            try
            {
                MessageDataBranchRule0Selected = dependency0.SequenceEqual(MessageDataBranchRule0Data2);
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
            if (MessageDataBranchRule0Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.SystemEventMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule1Data2 = "D"u8;
            try
            {
                MessageDataBranchRule1Selected = dependency0.SequenceEqual(MessageDataBranchRule1Data2);
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
            if (MessageDataBranchRule1Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.SecurityDirectoryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule2Data2 = "H"u8;
            try
            {
                MessageDataBranchRule2Selected = dependency0.SequenceEqual(MessageDataBranchRule2Data2);
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
            if (MessageDataBranchRule2Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradingStatusMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule3Data2 = "I"u8;
            try
            {
                MessageDataBranchRule3Selected = dependency0.SequenceEqual(MessageDataBranchRule3Data2);
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
            if (MessageDataBranchRule3Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.RetailLiquidityIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule4Data2 = "O"u8;
            try
            {
                MessageDataBranchRule4Selected = dependency0.SequenceEqual(MessageDataBranchRule4Data2);
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
            if (MessageDataBranchRule4Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.OperationalHaltStatusMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule5Data2 = "P"u8;
            try
            {
                MessageDataBranchRule5Selected = dependency0.SequenceEqual(MessageDataBranchRule5Data2);
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
            if (MessageDataBranchRule5Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.ShortSalePriceTestStatusMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule6Data2 = "Q"u8;
            try
            {
                MessageDataBranchRule6Selected = dependency0.SequenceEqual(MessageDataBranchRule6Data2);
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
            if (MessageDataBranchRule6Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.QuoteUpdateMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule7Data2 = "T"u8;
            try
            {
                MessageDataBranchRule7Selected = dependency0.SequenceEqual(MessageDataBranchRule7Data2);
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
            if (MessageDataBranchRule7Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradeReportMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule8Data2 = "X"u8;
            try
            {
                MessageDataBranchRule8Selected = dependency0.SequenceEqual(MessageDataBranchRule8Data2);
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
            if (MessageDataBranchRule8Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.OfficialPriceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule9Data2 = "B"u8;
            try
            {
                MessageDataBranchRule9Selected = dependency0.SequenceEqual(MessageDataBranchRule9Data2);
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
            if (MessageDataBranchRule9Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.TradeBreakMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule10Data2 = "A"u8;
            try
            {
                MessageDataBranchRule10Selected = dependency0.SequenceEqual(MessageDataBranchRule10Data2);
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
            if (MessageDataBranchRule10Selected)
            {
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.AuctionInformationMessage;
            }
        }
        return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Case.None;
    }
    internal static int MessageMessageDataStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ReadOnlySpan<byte> received0)
    {
        received0 = default;
        const int messageTypeDependencyReadDistanceFromMessageData = -1;
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
        messageTypeDependencyReadPosition = (long)index + messageTypeDependencyReadDistanceFromMessageData;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > limit || messageTypeDependencyReadWidth > limit - messageTypeDependencyReadPosition) return Position.Failure;
        messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
        received0 = messageTypeValue;
        return index;
    }
    internal static int SelectMessages(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        bool MessagesBranchRule1Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const ushort MessagesBranchRule1Data2 = 0;
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
                return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.Messages.Case.Heartbeat;
            }
        }
        
        {
            return (int)global::Iex.IexEquities.Tops.IexTp.v1_66.Messages.Case.Message;
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
    internal static int PacketMessagesStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ushort received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int messageCountDependencyReadDistanceFromPacket = 14;
        const int messageCountDependencyReadWidth = 2;
        long messageCountDependencyReadPosition;
        ushort messageCountValue;
        const int payloadLengthDependencyReadDistanceFromMessages = -28;
        const int payloadLengthDependencyReadWidth = 2;
        long payloadLengthDependencyReadPosition;
        ushort payloadLengthValue;
        const int messageCountDependencyReadDistanceFromMessages = -26;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        messageCountDependencyReadPosition = (long)start + messageCountDependencyReadDistanceFromPacket;
        if (start < Position.Minimum || start > limit || limit < Position.Minimum || limit > bytes.Length || messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > limit || messageCountDependencyReadWidth > limit - messageCountDependencyReadPosition) return Position.Failure;
        messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageCountDependencyReadPosition)..]);
        
        {
            const int iextpHeaderMeasuredBytes = SizeIextpHeader;
            if (iextpHeaderMeasuredBytes < Position.Minimum || iextpHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += iextpHeaderMeasuredBytes;
        }
        payloadLengthDependencyReadPosition = (long)index + payloadLengthDependencyReadDistanceFromMessages;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || payloadLengthDependencyReadPosition < Position.Minimum || payloadLengthDependencyReadPosition > limit || payloadLengthDependencyReadWidth > limit - payloadLengthDependencyReadPosition) return Position.Failure;
        payloadLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)payloadLengthDependencyReadPosition)..]);
        messageCountDependencyReadPosition = (long)index + messageCountDependencyReadDistanceFromMessages;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > limit || messageCountDependencyReadWidth > limit - messageCountDependencyReadPosition) return Position.Failure;
        messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageCountDependencyReadPosition)..]);
        received0 = messageCountValue;
        received1 = payloadLengthValue;
        return index;
    }
    internal static int PacketMessagesEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 MessagesSizeRule0Width;
        int MessagesSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                MessagesSizeRule0Width = checked((global::System.Int128)(dependency0));
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
            if (MessagesSizeRule0Width < Position.Minimum || MessagesSizeRule0Width > (long)limit - index) return Position.Failure;
            MessagesSizeRule0End = index + (int)MessagesSizeRule0Width;
            int messagesEnd = MessagesSizeRule0End;
            index = messagesEnd;
        }
        return index;
    }
}
