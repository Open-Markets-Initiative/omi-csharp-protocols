using System;
using System.Buffers.Binary;
namespace Databento.MarketData.Normalized.Dbn.v2;
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
    internal const int SizeAction = 1;
    internal const int SizeActivation = 8;
    internal const int SizeApplId = 2;
    internal const int SizeAskCt = 4;
    internal const int SizeAskPb = 2;
    internal const int SizeAskPx = 8;
    internal const int SizeAskSz = 4;
    internal const int SizeAsset = 7;
    internal const int SizeAuctInterestClrPrice = 8;
    internal const int SizeAuctionStatus = 1;
    internal const int SizeAuctionTime = 8;
    internal const int SizeAuctionType = 1;
    internal const int SizeBboMsg = 64;
    internal const int SizeBidAskPair = 32;
    internal const int SizeBidCt = 4;
    internal const int SizeBidPb = 2;
    internal const int SizeBidPx = 8;
    internal const int SizeBidSz = 4;
    internal const int SizeCbboMsg = 64;
    internal const int SizeCfi = 7;
    internal const int SizeChannel = 2;
    internal const int SizeChannelId = 1;
    internal const int SizeClose = 8;
    internal const int SizeCmbp1Msg = 64;
    internal const int SizeConsolidatedBidAskPair = 32;
    internal const int SizeContBookClrPrice = 8;
    internal const int SizeContractMultiplier = 4;
    internal const int SizeContractMultiplierUnit = 1;
    internal const int SizeCurrency = 4;
    internal const int SizeDataset = 16;
    internal const int SizeDecayQuantity = 4;
    internal const int SizeDecayStartDate = 2;
    internal const int SizeDepth = 1;
    internal const int SizeDisplayFactor = 8;
    internal const int SizeEnd = 8;
    internal const int SizeEndDate = 4;
    internal const int SizeEndTs = 8;
    internal const int SizeErr = 302;
    internal const int SizeErrorCode = 1;
    internal const int SizeErrorMsg = 304;
    internal const int SizeExchange = 5;
    internal const int SizeExpiration = 8;
    internal const int SizeFlowScheduleType = 1;
    internal const int SizeFreezeStatus = 1;
    internal const int SizeGroup = 21;
    internal const int SizeHigh = 8;
    internal const int SizeHighLimitPrice = 8;
    internal const int SizeImbalanceMsg = 96;
    internal const int SizeIndMatchPrice = 8;
    internal const int SizeInstAttribValue = 4;
    internal const int SizeInstrumentClass = 1;
    internal const int SizeInstrumentDefMsg = 384;
    internal const int SizeInstrumentId = 4;
    internal const int SizeIntervalCount = 4;
    internal const int SizeIsLast = 1;
    internal const int SizeIsQuoting = 1;
    internal const int SizeIsShortSellRestricted = 1;
    internal const int SizeIsTrading = 1;
    internal const int SizeLimit = 8;
    internal const int SizeLow = 8;
    internal const int SizeLowLimitPrice = 8;
    internal const int SizeLowerCollar = 8;
    internal const int SizeMagic = 3;
    internal const int SizeMainFraction = 1;
    internal const int SizeMappedSymbol = 71;
    internal const int SizeMappingInterval = 79;
    internal const int SizeMappingsCount = 4;
    internal const int SizeMarketDepth = 4;
    internal const int SizeMarketDepthImplied = 4;
    internal const int SizeMarketImbalanceQty = 4;
    internal const int SizeMarketSegmentId = 4;
    internal const int SizeMatchAlgorithm = 1;
    internal const int SizeMaturityDay = 1;
    internal const int SizeMaturityMonth = 1;
    internal const int SizeMaturityWeek = 1;
    internal const int SizeMaturityYear = 2;
    internal const int SizeMaxPriceVariation = 8;
    internal const int SizeMaxTradeVol = 4;
    internal const int SizeMboMsg = 40;
    internal const int SizeMbp1Msg = 64;
    internal const int SizeMdSecurityTradingStatus = 1;
    internal const int SizeMetadataLength = 4;
    internal const int SizeMetadataReserved = 53;
    internal const int SizeMinLotSize = 4;
    internal const int SizeMinLotSizeBlock = 4;
    internal const int SizeMinLotSizeRoundLot = 4;
    internal const int SizeMinPriceIncrement = 8;
    internal const int SizeMinPriceIncrementAmount = 8;
    internal const int SizeMinTradeVol = 4;
    internal const int SizeMsg = 303;
    internal const int SizeNotFoundCount = 4;
    internal const int SizeNotFoundSymbol = 71;
    internal const int SizeNumExtensions = 1;
    internal const int SizeOhlcvMsg = 40;
    internal const int SizeOpen = 8;
    internal const int SizeOrderId = 8;
    internal const int SizeOriginalContractSize = 4;
    internal const int SizePairedQty = 4;
    internal const int SizePartialCount = 4;
    internal const int SizePartialSymbol = 71;
    internal const int SizePrice = 8;
    internal const int SizePriceDisplayFormat = 1;
    internal const int SizePriceRatio = 8;
    internal const int SizePublisherId = 2;
    internal const int SizeQuantity = 4;
    internal const int SizeRawInstrumentId = 4;
    internal const int SizeRawSymbol = 71;
    internal const int SizeRawSymbolIn = 71;
    internal const int SizeRecordHeader = 16;
    internal const int SizeRecordLength = 1;
    internal const int SizeRefPrice = 8;
    internal const int SizeReserved1 = 1;
    internal const int SizeReserved10 = 10;
    internal const int SizeReserved2 = 2;
    internal const int SizeReserved4 = 4;
    internal const int SizeReserved6 = 6;
    internal const int SizeReserved7 = 7;
    internal const int SizeReserved8 = 8;
    internal const int SizeRtype = 1;
    internal const int SizeSchema = 2;
    internal const int SizeSchemaDefinitionLength = 4;
    internal const int SizeSecondReserved1 = 1;
    internal const int SizeSecondReserved2 = 2;
    internal const int SizeSecsubtype = 6;
    internal const int SizeSecurityType = 7;
    internal const int SizeSecurityUpdateAction = 1;
    internal const int SizeSequence = 4;
    internal const int SizeSettlCurrency = 4;
    internal const int SizeSettlPriceType = 1;
    internal const int SizeSide = 1;
    internal const int SizeSignificantImbalance = 1;
    internal const int SizeSize = 4;
    internal const int SizeSsrFillingPrice = 8;
    internal const int SizeStart = 8;
    internal const int SizeStartDate = 4;
    internal const int SizeStartTs = 8;
    internal const int SizeStatFlags = 1;
    internal const int SizeStatMsg = 48;
    internal const int SizeStatType = 2;
    internal const int SizeStatusAction = 2;
    internal const int SizeStatusMsg = 24;
    internal const int SizeStatusReason = 2;
    internal const int SizeStrikePrice = 8;
    internal const int SizeStrikePriceCurrency = 4;
    internal const int SizeStypeIn = 1;
    internal const int SizeStypeInSymbol = 71;
    internal const int SizeStypeOut = 1;
    internal const int SizeStypeOutSymbol = 71;
    internal const int SizeSubFraction = 1;
    internal const int SizeSymbol = 71;
    internal const int SizeSymbolCstrLen = 2;
    internal const int SizeSymbolMappingMsg = 160;
    internal const int SizeSymbolsCount = 4;
    internal const int SizeSystemCode = 1;
    internal const int SizeSystemMsg = 304;
    internal const int SizeTickRule = 1;
    internal const int SizeTotalImbalanceQty = 4;
    internal const int SizeTradeMsg = 32;
    internal const int SizeTradingEvent = 2;
    internal const int SizeTradingReferenceDate = 2;
    internal const int SizeTradingReferencePrice = 8;
    internal const int SizeTsEvent = 8;
    internal const int SizeTsInDelta = 4;
    internal const int SizeTsOut = 1;
    internal const int SizeTsOutTime = 8;
    internal const int SizeTsRecv = 8;
    internal const int SizeTsRef = 8;
    internal const int SizeUnderlying = 21;
    internal const int SizeUnderlyingId = 4;
    internal const int SizeUnderlyingProduct = 1;
    internal const int SizeUnitOfMeasure = 31;
    internal const int SizeUnitOfMeasureQty = 8;
    internal const int SizeUnpairedQty = 4;
    internal const int SizeUnpairedSide = 1;
    internal const int SizeUpdateAction = 1;
    internal const int SizeUpperCollar = 8;
    internal const int SizeUserDefinedInstrument = 1;
    internal const int SizeVersion = 1;
    internal const int SizeVolume = 8;
    private const byte ControlMessageReassembleRule1Terminator0 = 10;
    internal static int ControlMessageReassembleRule1BytesRemaining(ReadOnlySpan<byte> bytes, int start, int limit, out int current)
    {
        int ControlMessageReassembleRule1Terminator0Bytes;
        int ControlMessageReassembleRule1Terminator0Offset;
        global::System.Int128 expected;
        current = Position.Failure;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int remaining = limit - start;
        
        {
            ControlMessageReassembleRule1Terminator0Offset = bytes.Slice(start, limit - start).IndexOf(ControlMessageReassembleRule1Terminator0);
            if (ControlMessageReassembleRule1Terminator0Offset < 0)
            {
                current = Position.Minimum;
                return Position.Failure;
            }
            ControlMessageReassembleRule1Terminator0Bytes = ControlMessageReassembleRule1Terminator0Offset + sizeof(byte);
        }
        try
        {
            expected = checked((global::System.Int128)(ControlMessageReassembleRule1Terminator0Bytes));
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
        if (expected < Position.Minimum || expected > int.MaxValue) return Position.Failure;
        current = (int)expected;
        if (remaining < current) return -(current - remaining);
        return remaining;
    }
    internal static int StepClientStreamControlMessage(ReadOnlySpan<byte> bytes, int start, int limit, int ControlMessagePayloadRule0End)
    {
        int ControlMessageReassembleRule1Available;
        int ControlMessageReassembleRule1Width;
        int ControlMessageReassembleRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ControlMessagePayloadRule0End)) return Position.Ended;
        int controlMessageEntryStart = index;
        
        {
            ControlMessageReassembleRule1Available = ControlMessageReassembleRule1BytesRemaining(bytes, index, limit, out ControlMessageReassembleRule1Width);
            if (ControlMessageReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (ControlMessageReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            ControlMessageReassembleRule1End = index + ControlMessageReassembleRule1Width;
            int controlMessageEnd = ControlMessageReassembleRule1End;
            index = controlMessageEnd;
        }
        if (index <= controlMessageEntryStart || index > ControlMessagePayloadRule0End) return Position.Failure;
        return index;
    }
    internal const int SizeFlags = 1;
    internal static int MeasureMbp10Msg(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        RuleCount BidAskPairCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int mbp10MsgPriceMeasuredBytes = SizePrice;
            if (mbp10MsgPriceMeasuredBytes < Position.Minimum || mbp10MsgPriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgPriceMeasuredBytes;
        }
        
        {
            const int recordBodyMbp10MsgSizeMeasuredBytes = SizeSize;
            if (recordBodyMbp10MsgSizeMeasuredBytes < Position.Minimum || recordBodyMbp10MsgSizeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += recordBodyMbp10MsgSizeMeasuredBytes;
        }
        
        {
            const int mbp10MsgActionMeasuredBytes = SizeAction;
            if (mbp10MsgActionMeasuredBytes < Position.Minimum || mbp10MsgActionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgActionMeasuredBytes;
        }
        
        {
            const int mbp10MsgSideMeasuredBytes = SizeSide;
            if (mbp10MsgSideMeasuredBytes < Position.Minimum || mbp10MsgSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgSideMeasuredBytes;
        }
        
        {
            const int mbp10MsgFlagsMeasuredBytes = SizeFlags;
            if (mbp10MsgFlagsMeasuredBytes < Position.Minimum || mbp10MsgFlagsMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgFlagsMeasuredBytes;
        }
        
        {
            const int mbp10MsgDepthMeasuredBytes = SizeDepth;
            if (mbp10MsgDepthMeasuredBytes < Position.Minimum || mbp10MsgDepthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgDepthMeasuredBytes;
        }
        
        {
            const int mbp10MsgTsRecvMeasuredBytes = SizeTsRecv;
            if (mbp10MsgTsRecvMeasuredBytes < Position.Minimum || mbp10MsgTsRecvMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgTsRecvMeasuredBytes;
        }
        
        {
            const int mbp10MsgTsInDeltaMeasuredBytes = SizeTsInDelta;
            if (mbp10MsgTsInDeltaMeasuredBytes < Position.Minimum || mbp10MsgTsInDeltaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgTsInDeltaMeasuredBytes;
        }
        
        {
            const int mbp10MsgSequenceMeasuredBytes = SizeSequence;
            if (mbp10MsgSequenceMeasuredBytes < Position.Minimum || mbp10MsgSequenceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgSequenceMeasuredBytes;
        }
        
        {
            const long BidAskPairCountRule0Data0 = 10;
            try
            {
                BidAskPairCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(BidAskPairCountRule0Data0)));
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
            while (HasRuleCount(in BidAskPairCountRule0Entries))
            {
                int mbp10MsgBidAskPairEntryStart = index;
                
                {
                    const int mbp10MsgBidAskPairMeasuredBytes = SizeBidAskPair;
                    if (mbp10MsgBidAskPairMeasuredBytes < Position.Minimum || mbp10MsgBidAskPairMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += mbp10MsgBidAskPairMeasuredBytes;
                }
                TakeRuleCount(ref BidAskPairCountRule0Entries);
            }
            if (HasRuleCount(in BidAskPairCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int StepMbp10MsgBidAskPair(ReadOnlySpan<byte> bytes, int start, int limit, scoped ref Parser.RuleCount BidAskPairCountRule0Entries)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(HasRuleCount(in BidAskPairCountRule0Entries))) return Position.Ended;
        int mbp10MsgBidAskPairEntryStart = index;
        
        {
            const int mbp10MsgBidAskPairMeasuredBytes = SizeBidAskPair;
            if (mbp10MsgBidAskPairMeasuredBytes < Position.Minimum || mbp10MsgBidAskPairMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgBidAskPairMeasuredBytes;
        }
        TakeRuleCount(ref BidAskPairCountRule0Entries);
        return index;
    }
    internal static int MeasureRecordBody(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool RecordBodyBranchRule0Selected;
        bool RecordBodyBranchRule1Selected;
        bool RecordBodyBranchRule2Selected;
        bool RecordBodyBranchRule3Selected;
        bool RecordBodyBranchRule4Selected;
        bool RecordBodyBranchRule5Selected;
        bool RecordBodyBranchRule6Selected;
        bool RecordBodyBranchRule7Selected;
        bool RecordBodyBranchRule8Selected;
        bool RecordBodyBranchRule9Selected;
        bool RecordBodyBranchRule10Selected;
        bool RecordBodyBranchRule11Selected;
        bool RecordBodyBranchRule12Selected;
        bool RecordBodyBranchRule13Selected;
        bool RecordBodyBranchRule14Selected;
        bool RecordBodyBranchRule15Selected;
        bool RecordBodyBranchRule16Selected;
        bool RecordBodyBranchRule17Selected;
        bool RecordBodyBranchRule18Selected;
        bool RecordBodyBranchRule19Selected;
        bool RecordBodyBranchRule20Selected;
        bool RecordBodyBranchRule21Selected;
        bool RecordBodyBranchRule22Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte RecordBodyBranchRule0Data2 = 0;
            try
            {
                RecordBodyBranchRule0Selected = (dependency0 == RecordBodyBranchRule0Data2);
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
            if (RecordBodyBranchRule0Selected)
            {
                
                {
                    const int tradeMsgMeasuredBytes = SizeTradeMsg;
                    if (tradeMsgMeasuredBytes < Position.Minimum || tradeMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += tradeMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule1Data2 = 1;
            try
            {
                RecordBodyBranchRule1Selected = (dependency0 == RecordBodyBranchRule1Data2);
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
            if (RecordBodyBranchRule1Selected)
            {
                
                {
                    const int mbp1MsgMeasuredBytes = SizeMbp1Msg;
                    if (mbp1MsgMeasuredBytes < Position.Minimum || mbp1MsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += mbp1MsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule2Data2 = 10;
            try
            {
                RecordBodyBranchRule2Selected = (dependency0 == RecordBodyBranchRule2Data2);
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
            if (RecordBodyBranchRule2Selected)
            {
                
                {
                    int mbp10MsgMeasureStart = index;
                    index = MeasureMbp10Msg(bytes, index, limit, start);
                    if (index < Position.Minimum) return Position.Failure;
                    if (index < mbp10MsgMeasureStart || index > limit) return Position.Failure;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule3Data2 = 17;
            try
            {
                RecordBodyBranchRule3Selected = (dependency0 == RecordBodyBranchRule3Data2);
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
            if (RecordBodyBranchRule3Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule4Data2 = 18;
            try
            {
                RecordBodyBranchRule4Selected = (dependency0 == RecordBodyBranchRule4Data2);
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
            if (RecordBodyBranchRule4Selected)
            {
                
                {
                    const int statusMsgMeasuredBytes = SizeStatusMsg;
                    if (statusMsgMeasuredBytes < Position.Minimum || statusMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += statusMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule5Data2 = 19;
            try
            {
                RecordBodyBranchRule5Selected = (dependency0 == RecordBodyBranchRule5Data2);
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
            if (RecordBodyBranchRule5Selected)
            {
                
                {
                    const int instrumentDefMsgMeasuredBytes = SizeInstrumentDefMsg;
                    if (instrumentDefMsgMeasuredBytes < Position.Minimum || instrumentDefMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += instrumentDefMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule6Data2 = 20;
            try
            {
                RecordBodyBranchRule6Selected = (dependency0 == RecordBodyBranchRule6Data2);
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
            if (RecordBodyBranchRule6Selected)
            {
                
                {
                    const int imbalanceMsgMeasuredBytes = SizeImbalanceMsg;
                    if (imbalanceMsgMeasuredBytes < Position.Minimum || imbalanceMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += imbalanceMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule7Data2 = 21;
            try
            {
                RecordBodyBranchRule7Selected = (dependency0 == RecordBodyBranchRule7Data2);
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
            if (RecordBodyBranchRule7Selected)
            {
                
                {
                    const int errorMsgMeasuredBytes = SizeErrorMsg;
                    if (errorMsgMeasuredBytes < Position.Minimum || errorMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += errorMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule8Data2 = 22;
            try
            {
                RecordBodyBranchRule8Selected = (dependency0 == RecordBodyBranchRule8Data2);
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
            if (RecordBodyBranchRule8Selected)
            {
                
                {
                    const int symbolMappingMsgMeasuredBytes = SizeSymbolMappingMsg;
                    if (symbolMappingMsgMeasuredBytes < Position.Minimum || symbolMappingMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMappingMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule9Data2 = 23;
            try
            {
                RecordBodyBranchRule9Selected = (dependency0 == RecordBodyBranchRule9Data2);
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
            if (RecordBodyBranchRule9Selected)
            {
                
                {
                    const int systemMsgMeasuredBytes = SizeSystemMsg;
                    if (systemMsgMeasuredBytes < Position.Minimum || systemMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += systemMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule10Data2 = 24;
            try
            {
                RecordBodyBranchRule10Selected = (dependency0 == RecordBodyBranchRule10Data2);
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
            if (RecordBodyBranchRule10Selected)
            {
                
                {
                    const int statMsgMeasuredBytes = SizeStatMsg;
                    if (statMsgMeasuredBytes < Position.Minimum || statMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += statMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule11Data2 = 32;
            try
            {
                RecordBodyBranchRule11Selected = (dependency0 == RecordBodyBranchRule11Data2);
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
            if (RecordBodyBranchRule11Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule12Data2 = 33;
            try
            {
                RecordBodyBranchRule12Selected = (dependency0 == RecordBodyBranchRule12Data2);
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
            if (RecordBodyBranchRule12Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule13Data2 = 34;
            try
            {
                RecordBodyBranchRule13Selected = (dependency0 == RecordBodyBranchRule13Data2);
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
            if (RecordBodyBranchRule13Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule14Data2 = 35;
            try
            {
                RecordBodyBranchRule14Selected = (dependency0 == RecordBodyBranchRule14Data2);
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
            if (RecordBodyBranchRule14Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule15Data2 = 36;
            try
            {
                RecordBodyBranchRule15Selected = (dependency0 == RecordBodyBranchRule15Data2);
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
            if (RecordBodyBranchRule15Selected)
            {
                
                {
                    const int ohlcvMsgMeasuredBytes = SizeOhlcvMsg;
                    if (ohlcvMsgMeasuredBytes < Position.Minimum || ohlcvMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += ohlcvMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule16Data2 = 160;
            try
            {
                RecordBodyBranchRule16Selected = (dependency0 == RecordBodyBranchRule16Data2);
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
            if (RecordBodyBranchRule16Selected)
            {
                
                {
                    const int mboMsgMeasuredBytes = SizeMboMsg;
                    if (mboMsgMeasuredBytes < Position.Minimum || mboMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += mboMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule17Data2 = 177;
            try
            {
                RecordBodyBranchRule17Selected = (dependency0 == RecordBodyBranchRule17Data2);
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
            if (RecordBodyBranchRule17Selected)
            {
                
                {
                    const int cmbp1MsgMeasuredBytes = SizeCmbp1Msg;
                    if (cmbp1MsgMeasuredBytes < Position.Minimum || cmbp1MsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cmbp1MsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule18Data2 = 192;
            try
            {
                RecordBodyBranchRule18Selected = (dependency0 == RecordBodyBranchRule18Data2);
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
            if (RecordBodyBranchRule18Selected)
            {
                
                {
                    const int cbboMsgMeasuredBytes = SizeCbboMsg;
                    if (cbboMsgMeasuredBytes < Position.Minimum || cbboMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cbboMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule19Data2 = 193;
            try
            {
                RecordBodyBranchRule19Selected = (dependency0 == RecordBodyBranchRule19Data2);
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
            if (RecordBodyBranchRule19Selected)
            {
                
                {
                    const int cbboMsgMeasuredBytes = SizeCbboMsg;
                    if (cbboMsgMeasuredBytes < Position.Minimum || cbboMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cbboMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule20Data2 = 194;
            try
            {
                RecordBodyBranchRule20Selected = (dependency0 == RecordBodyBranchRule20Data2);
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
            if (RecordBodyBranchRule20Selected)
            {
                
                {
                    const int cmbp1MsgMeasuredBytes = SizeCmbp1Msg;
                    if (cmbp1MsgMeasuredBytes < Position.Minimum || cmbp1MsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cmbp1MsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule21Data2 = 195;
            try
            {
                RecordBodyBranchRule21Selected = (dependency0 == RecordBodyBranchRule21Data2);
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
            if (RecordBodyBranchRule21Selected)
            {
                
                {
                    const int bboMsgMeasuredBytes = SizeBboMsg;
                    if (bboMsgMeasuredBytes < Position.Minimum || bboMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += bboMsgMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte RecordBodyBranchRule22Data2 = 196;
            try
            {
                RecordBodyBranchRule22Selected = (dependency0 == RecordBodyBranchRule22Data2);
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
            if (RecordBodyBranchRule22Selected)
            {
                
                {
                    const int bboMsgMeasuredBytes = SizeBboMsg;
                    if (bboMsgMeasuredBytes < Position.Minimum || bboMsgMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += bboMsgMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    private const long RecordReassembleRule1Data2 = 4;
    private static byte RecordReassembleRule1Dependency0Read(global::System.ReadOnlySpan<byte> bytes, int limit, long position)
    {
        long recordLengthDependencyReadPosition;
        byte recordLengthValue;
        const int recordLengthDependencyReadWidth = 1;
        recordLengthDependencyReadPosition = position;
        if (recordLengthDependencyReadPosition < Position.Minimum || recordLengthDependencyReadPosition > limit || limit > bytes.Length || recordLengthDependencyReadWidth > limit - recordLengthDependencyReadPosition) throw new RuleValueException();
        recordLengthValue = bytes[(int)recordLengthDependencyReadPosition];
        return recordLengthValue;
    }
    internal static int RecordReassembleRule1BytesRemaining(ReadOnlySpan<byte> bytes, int start, int limit, out int current, byte? dependency0)
    {
        global::System.Int128 expected;
        current = Position.Failure;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int remaining = limit - start;
        const int recordLengthDependencyReassembleDistance = 0;
        long recordLengthDependencyReassemblePosition = (long)start + recordLengthDependencyReassembleDistance;
        int recordLengthDependencyReassembleRequired = recordLengthDependencyReassembleDistance + SizeRecordLength;
        if (remaining < recordLengthDependencyReassembleRequired)
        {
            current = Position.Minimum;
            return Position.Failure;
        }
        try
        {
            expected = ((RecordReassembleRule1Dependency0Read(bytes, limit, recordLengthDependencyReassemblePosition)) switch { var RecordReassembleRule1ArithmeticLeft0 => ((RecordReassembleRule1Data2) switch { var RecordReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(RecordReassembleRule1ArithmeticLeft0)) * checked((global::System.Int128)(RecordReassembleRule1ArithmeticRight1)))) }) });
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
        if (expected < Position.Minimum || expected > int.MaxValue) return Position.Failure;
        current = (int)expected;
        if (remaining < current) return -(current - remaining);
        return remaining;
    }
    internal static int MeasureSymbolMapping(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int intervalCountDependencyReadDistanceFromMappingInterval = -4;
        const int intervalCountDependencyReadWidth = 4;
        long intervalCountDependencyReadPosition;
        uint intervalCountValue;
        RuleCount MappingIntervalCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rawSymbolInMeasuredBytes = SizeRawSymbolIn;
            if (rawSymbolInMeasuredBytes < Position.Minimum || rawSymbolInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rawSymbolInMeasuredBytes;
        }
        
        {
            const int intervalCountMeasuredBytes = SizeIntervalCount;
            if (intervalCountMeasuredBytes < Position.Minimum || intervalCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += intervalCountMeasuredBytes;
        }
        
        {
            intervalCountDependencyReadPosition = (long)index + intervalCountDependencyReadDistanceFromMappingInterval;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || intervalCountDependencyReadPosition < Position.Minimum || intervalCountDependencyReadPosition > limit || intervalCountDependencyReadWidth > limit - intervalCountDependencyReadPosition) return Position.Failure;
            intervalCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)intervalCountDependencyReadPosition)..]);
            try
            {
                MappingIntervalCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(intervalCountValue)));
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
            while (HasRuleCount(in MappingIntervalCountRule0Entries))
            {
                int mappingIntervalEntryStart = index;
                
                {
                    const int mappingIntervalMeasuredBytes = SizeMappingInterval;
                    if (mappingIntervalMeasuredBytes < Position.Minimum || mappingIntervalMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += mappingIntervalMeasuredBytes;
                }
                TakeRuleCount(ref MappingIntervalCountRule0Entries);
            }
            if (HasRuleCount(in MappingIntervalCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int StepSymbolMappingMappingInterval(ReadOnlySpan<byte> bytes, int start, int limit, scoped ref Parser.RuleCount MappingIntervalCountRule0Entries)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(HasRuleCount(in MappingIntervalCountRule0Entries))) return Position.Ended;
        int mappingIntervalEntryStart = index;
        
        {
            const int mappingIntervalMeasuredBytes = SizeMappingInterval;
            if (mappingIntervalMeasuredBytes < Position.Minimum || mappingIntervalMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mappingIntervalMeasuredBytes;
        }
        TakeRuleCount(ref MappingIntervalCountRule0Entries);
        return index;
    }
    internal static int StepMetadataSymbolMapping(ReadOnlySpan<byte> bytes, int start, int limit, scoped ref Parser.RuleCount SymbolMappingCountRule0Entries)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(HasRuleCount(in SymbolMappingCountRule0Entries))) return Position.Ended;
        int symbolMappingEntryStart = index;
        
        {
            int symbolMappingMeasureStart = index;
            index = MeasureSymbolMapping(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < symbolMappingMeasureStart || index > limit) return Position.Failure;
        }
        TakeRuleCount(ref SymbolMappingCountRule0Entries);
        return index;
    }
    internal static int MeasureAuthenticationResponse(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int authenticationResponseControlFieldsEnd = ControlFieldsSizeRule0End;
            index = authenticationResponseControlFieldsEnd;
        }
        return index;
    }
    internal static int MeasureChallengeRequest(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int challengeRequestControlFieldsEnd = ControlFieldsSizeRule0End;
            index = challengeRequestControlFieldsEnd;
        }
        return index;
    }
    internal static int MeasureGreeting(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int greetingControlFieldsEnd = ControlFieldsSizeRule0End;
            index = greetingControlFieldsEnd;
        }
        return index;
    }
    internal static int StepGatewayStreamRecord(ReadOnlySpan<byte> bytes, int start, int limit, int RecordPayloadRule0End, byte? dependency0)
    {
        const int recordLengthDependencyReadDistanceFromRecord = 0;
        const int recordLengthDependencyReadWidth = 1;
        long recordLengthDependencyReadPosition;
        byte recordLengthValue;
        int RecordReassembleRule1Available;
        int RecordReassembleRule1Width;
        int RecordReassembleRule1End;
        global::System.Int128 RecordSizeRule2Width;
        int RecordSizeRule2End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < RecordPayloadRule0End)) return Position.Ended;
        int recordEntryStart = index;
        
        {
            recordLengthDependencyReadPosition = (long)index + recordLengthDependencyReadDistanceFromRecord;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || recordLengthDependencyReadPosition < Position.Minimum || recordLengthDependencyReadPosition > limit || recordLengthDependencyReadWidth > limit - recordLengthDependencyReadPosition) return Position.Failure;
            recordLengthValue = bytes[(int)recordLengthDependencyReadPosition];
            RecordReassembleRule1Available = RecordReassembleRule1BytesRemaining(bytes, index, limit, out RecordReassembleRule1Width, dependency0);
            if (RecordReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (RecordReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            RecordReassembleRule1End = index + RecordReassembleRule1Width;
            const long RecordSizeRule2Data2 = 4;
            try
            {
                RecordSizeRule2Width = ((recordLengthValue) switch { var RecordSizeRule2ArithmeticLeft0 => ((RecordSizeRule2Data2) switch { var RecordSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(RecordSizeRule2ArithmeticLeft0)) * checked((global::System.Int128)(RecordSizeRule2ArithmeticRight1)))) }) });
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
            if (RecordSizeRule2Width < Position.Minimum || RecordSizeRule2Width > (long)limit - index) return Position.Failure;
            RecordSizeRule2End = index + (int)RecordSizeRule2Width;
            int recordEnd = RecordReassembleRule1End;
            if (RecordSizeRule2End != recordEnd) return Position.Failure;
            index = recordEnd;
        }
        if (index <= recordEntryStart || index > RecordPayloadRule0End) return Position.Failure;
        return index;
    }
    internal static int StepFileRecord(ReadOnlySpan<byte> bytes, int start, int limit, int RecordPayloadRule0End, byte? dependency0)
    {
        const int recordLengthDependencyReadDistanceFromRecord = 0;
        const int recordLengthDependencyReadWidth = 1;
        long recordLengthDependencyReadPosition;
        byte recordLengthValue;
        int RecordReassembleRule1Available;
        int RecordReassembleRule1Width;
        int RecordReassembleRule1End;
        global::System.Int128 RecordSizeRule2Width;
        int RecordSizeRule2End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < RecordPayloadRule0End)) return Position.Ended;
        int recordEntryStart = index;
        
        {
            recordLengthDependencyReadPosition = (long)index + recordLengthDependencyReadDistanceFromRecord;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || recordLengthDependencyReadPosition < Position.Minimum || recordLengthDependencyReadPosition > limit || recordLengthDependencyReadWidth > limit - recordLengthDependencyReadPosition) return Position.Failure;
            recordLengthValue = bytes[(int)recordLengthDependencyReadPosition];
            RecordReassembleRule1Available = RecordReassembleRule1BytesRemaining(bytes, index, limit, out RecordReassembleRule1Width, dependency0);
            if (RecordReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (RecordReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            RecordReassembleRule1End = index + RecordReassembleRule1Width;
            const long RecordSizeRule2Data2 = 4;
            try
            {
                RecordSizeRule2Width = ((recordLengthValue) switch { var RecordSizeRule2ArithmeticLeft0 => ((RecordSizeRule2Data2) switch { var RecordSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(RecordSizeRule2ArithmeticLeft0)) * checked((global::System.Int128)(RecordSizeRule2ArithmeticRight1)))) }) });
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
            if (RecordSizeRule2Width < Position.Minimum || RecordSizeRule2Width > (long)limit - index) return Position.Failure;
            RecordSizeRule2End = index + (int)RecordSizeRule2Width;
            int recordEnd = RecordReassembleRule1End;
            if (RecordSizeRule2End != recordEnd) return Position.Failure;
            index = recordEnd;
        }
        if (index <= recordEntryStart || index > RecordPayloadRule0End) return Position.Failure;
        return index;
    }
    internal static int ControlMessageControlFieldsStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        return index;
    }
    internal static int ControlMessageControlFieldsEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int controlFieldsEnd = ControlFieldsSizeRule0End;
            index = controlFieldsEnd;
        }
        return index;
    }
    internal static int ClientStreamControlMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        global::System.Int128 ControlMessagePayloadRule0RawEnd;
        int ControlMessagePayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        try
        {
            ControlMessagePayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (ControlMessagePayloadRule0RawEnd < index || ControlMessagePayloadRule0RawEnd > limit) return Position.Failure;
        ControlMessagePayloadRule0End = (int)ControlMessagePayloadRule0RawEnd;
        received0 = limit;
        received1 = ControlMessagePayloadRule0End;
        return index;
    }
    internal static int Mbp10MsgBidAskPairStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out Parser.RuleCount received1)
    {
        received0 = default;
        received1 = default;
        RuleCount BidAskPairCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int mbp10MsgPriceMeasuredBytes = SizePrice;
            if (mbp10MsgPriceMeasuredBytes < Position.Minimum || mbp10MsgPriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgPriceMeasuredBytes;
        }
        
        {
            const int recordBodyMbp10MsgSizeMeasuredBytes = SizeSize;
            if (recordBodyMbp10MsgSizeMeasuredBytes < Position.Minimum || recordBodyMbp10MsgSizeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += recordBodyMbp10MsgSizeMeasuredBytes;
        }
        
        {
            const int mbp10MsgActionMeasuredBytes = SizeAction;
            if (mbp10MsgActionMeasuredBytes < Position.Minimum || mbp10MsgActionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgActionMeasuredBytes;
        }
        
        {
            const int mbp10MsgSideMeasuredBytes = SizeSide;
            if (mbp10MsgSideMeasuredBytes < Position.Minimum || mbp10MsgSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgSideMeasuredBytes;
        }
        
        {
            const int mbp10MsgFlagsMeasuredBytes = SizeFlags;
            if (mbp10MsgFlagsMeasuredBytes < Position.Minimum || mbp10MsgFlagsMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgFlagsMeasuredBytes;
        }
        
        {
            const int mbp10MsgDepthMeasuredBytes = SizeDepth;
            if (mbp10MsgDepthMeasuredBytes < Position.Minimum || mbp10MsgDepthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgDepthMeasuredBytes;
        }
        
        {
            const int mbp10MsgTsRecvMeasuredBytes = SizeTsRecv;
            if (mbp10MsgTsRecvMeasuredBytes < Position.Minimum || mbp10MsgTsRecvMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgTsRecvMeasuredBytes;
        }
        
        {
            const int mbp10MsgTsInDeltaMeasuredBytes = SizeTsInDelta;
            if (mbp10MsgTsInDeltaMeasuredBytes < Position.Minimum || mbp10MsgTsInDeltaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgTsInDeltaMeasuredBytes;
        }
        
        {
            const int mbp10MsgSequenceMeasuredBytes = SizeSequence;
            if (mbp10MsgSequenceMeasuredBytes < Position.Minimum || mbp10MsgSequenceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mbp10MsgSequenceMeasuredBytes;
        }
        const long BidAskPairCountRule0Data0 = 10;
        try
        {
            BidAskPairCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(BidAskPairCountRule0Data0)));
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
        received1 = BidAskPairCountRule0Entries;
        return index;
    }
    internal static int SelectRecordBody(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool RecordBodyBranchRule0Selected;
        bool RecordBodyBranchRule1Selected;
        bool RecordBodyBranchRule2Selected;
        bool RecordBodyBranchRule3Selected;
        bool RecordBodyBranchRule4Selected;
        bool RecordBodyBranchRule5Selected;
        bool RecordBodyBranchRule6Selected;
        bool RecordBodyBranchRule7Selected;
        bool RecordBodyBranchRule8Selected;
        bool RecordBodyBranchRule9Selected;
        bool RecordBodyBranchRule10Selected;
        bool RecordBodyBranchRule11Selected;
        bool RecordBodyBranchRule12Selected;
        bool RecordBodyBranchRule13Selected;
        bool RecordBodyBranchRule14Selected;
        bool RecordBodyBranchRule15Selected;
        bool RecordBodyBranchRule16Selected;
        bool RecordBodyBranchRule17Selected;
        bool RecordBodyBranchRule18Selected;
        bool RecordBodyBranchRule19Selected;
        bool RecordBodyBranchRule20Selected;
        bool RecordBodyBranchRule21Selected;
        bool RecordBodyBranchRule22Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte RecordBodyBranchRule0Data2 = 0;
            try
            {
                RecordBodyBranchRule0Selected = (dependency0 == RecordBodyBranchRule0Data2);
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
            if (RecordBodyBranchRule0Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.TradeMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule1Data2 = 1;
            try
            {
                RecordBodyBranchRule1Selected = (dependency0 == RecordBodyBranchRule1Data2);
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
            if (RecordBodyBranchRule1Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Mbp1Msg;
            }
        }
        
        {
            const byte RecordBodyBranchRule2Data2 = 10;
            try
            {
                RecordBodyBranchRule2Selected = (dependency0 == RecordBodyBranchRule2Data2);
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
            if (RecordBodyBranchRule2Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Mbp10Msg;
            }
        }
        
        {
            const byte RecordBodyBranchRule3Data2 = 17;
            try
            {
                RecordBodyBranchRule3Selected = (dependency0 == RecordBodyBranchRule3Data2);
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
            if (RecordBodyBranchRule3Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule4Data2 = 18;
            try
            {
                RecordBodyBranchRule4Selected = (dependency0 == RecordBodyBranchRule4Data2);
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
            if (RecordBodyBranchRule4Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.StatusMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule5Data2 = 19;
            try
            {
                RecordBodyBranchRule5Selected = (dependency0 == RecordBodyBranchRule5Data2);
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
            if (RecordBodyBranchRule5Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.InstrumentDefMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule6Data2 = 20;
            try
            {
                RecordBodyBranchRule6Selected = (dependency0 == RecordBodyBranchRule6Data2);
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
            if (RecordBodyBranchRule6Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.ImbalanceMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule7Data2 = 21;
            try
            {
                RecordBodyBranchRule7Selected = (dependency0 == RecordBodyBranchRule7Data2);
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
            if (RecordBodyBranchRule7Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.ErrorMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule8Data2 = 22;
            try
            {
                RecordBodyBranchRule8Selected = (dependency0 == RecordBodyBranchRule8Data2);
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
            if (RecordBodyBranchRule8Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.SymbolMappingMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule9Data2 = 23;
            try
            {
                RecordBodyBranchRule9Selected = (dependency0 == RecordBodyBranchRule9Data2);
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
            if (RecordBodyBranchRule9Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.SystemMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule10Data2 = 24;
            try
            {
                RecordBodyBranchRule10Selected = (dependency0 == RecordBodyBranchRule10Data2);
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
            if (RecordBodyBranchRule10Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.StatMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule11Data2 = 32;
            try
            {
                RecordBodyBranchRule11Selected = (dependency0 == RecordBodyBranchRule11Data2);
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
            if (RecordBodyBranchRule11Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule12Data2 = 33;
            try
            {
                RecordBodyBranchRule12Selected = (dependency0 == RecordBodyBranchRule12Data2);
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
            if (RecordBodyBranchRule12Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule13Data2 = 34;
            try
            {
                RecordBodyBranchRule13Selected = (dependency0 == RecordBodyBranchRule13Data2);
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
            if (RecordBodyBranchRule13Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule14Data2 = 35;
            try
            {
                RecordBodyBranchRule14Selected = (dependency0 == RecordBodyBranchRule14Data2);
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
            if (RecordBodyBranchRule14Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule15Data2 = 36;
            try
            {
                RecordBodyBranchRule15Selected = (dependency0 == RecordBodyBranchRule15Data2);
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
            if (RecordBodyBranchRule15Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.OhlcvMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule16Data2 = 160;
            try
            {
                RecordBodyBranchRule16Selected = (dependency0 == RecordBodyBranchRule16Data2);
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
            if (RecordBodyBranchRule16Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.MboMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule17Data2 = 177;
            try
            {
                RecordBodyBranchRule17Selected = (dependency0 == RecordBodyBranchRule17Data2);
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
            if (RecordBodyBranchRule17Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Cmbp1Msg;
            }
        }
        
        {
            const byte RecordBodyBranchRule18Data2 = 192;
            try
            {
                RecordBodyBranchRule18Selected = (dependency0 == RecordBodyBranchRule18Data2);
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
            if (RecordBodyBranchRule18Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.CbboMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule19Data2 = 193;
            try
            {
                RecordBodyBranchRule19Selected = (dependency0 == RecordBodyBranchRule19Data2);
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
            if (RecordBodyBranchRule19Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.CbboMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule20Data2 = 194;
            try
            {
                RecordBodyBranchRule20Selected = (dependency0 == RecordBodyBranchRule20Data2);
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
            if (RecordBodyBranchRule20Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.Cmbp1Msg;
            }
        }
        
        {
            const byte RecordBodyBranchRule21Data2 = 195;
            try
            {
                RecordBodyBranchRule21Selected = (dependency0 == RecordBodyBranchRule21Data2);
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
            if (RecordBodyBranchRule21Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.BboMsg;
            }
        }
        
        {
            const byte RecordBodyBranchRule22Data2 = 196;
            try
            {
                RecordBodyBranchRule22Selected = (dependency0 == RecordBodyBranchRule22Data2);
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
            if (RecordBodyBranchRule22Selected)
            {
                return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.BboMsg;
            }
        }
        return (int)global::Databento.MarketData.Normalized.Dbn.v2.RecordBody.Case.None;
    }
    internal static int RecordRecordBodyStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0, out byte received0)
    {
        received0 = default;
        const int rtypeDependencyReadDistanceFromRecordBody = -15;
        const int rtypeDependencyReadWidth = 1;
        long rtypeDependencyReadPosition;
        byte rtypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int recordHeaderMeasuredBytes = SizeRecordHeader;
            if (recordHeaderMeasuredBytes < Position.Minimum || recordHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += recordHeaderMeasuredBytes;
        }
        rtypeDependencyReadPosition = (long)index + rtypeDependencyReadDistanceFromRecordBody;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > limit || rtypeDependencyReadWidth > limit - rtypeDependencyReadPosition) return Position.Failure;
        rtypeValue = bytes[(int)rtypeDependencyReadPosition];
        received0 = rtypeValue;
        return index;
    }
    internal static int RecordTsOutTimeStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        const int rtypeDependencyReadDistanceFromRecordBody = -15;
        const int rtypeDependencyReadWidth = 1;
        long rtypeDependencyReadPosition;
        byte rtypeValue;
        bool TsOutTimeOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int recordHeaderMeasuredBytes = SizeRecordHeader;
            if (recordHeaderMeasuredBytes < Position.Minimum || recordHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += recordHeaderMeasuredBytes;
        }
        
        {
            int recordBodyMeasureStart = index;
            rtypeDependencyReadPosition = (long)index + rtypeDependencyReadDistanceFromRecordBody;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > limit || rtypeDependencyReadWidth > limit - rtypeDependencyReadPosition) return Position.Failure;
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            index = MeasureRecordBody(bytes, index, limit, start, rtypeValue);
            if (index < Position.Minimum) return Position.Failure;
            if (index < recordBodyMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            const byte TsOutTimeOptionalRule0Stored0Default = 0;
            const byte TsOutTimeOptionalRule0Data2 = 1;
            try
            {
                TsOutTimeOptionalRule0Present = ((dependency0 ?? TsOutTimeOptionalRule0Stored0Default) == TsOutTimeOptionalRule0Data2);
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
            if (TsOutTimeOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int SymbolMappingMappingIntervalStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out Parser.RuleCount received1)
    {
        received0 = default;
        received1 = default;
        const int intervalCountDependencyReadDistanceFromMappingInterval = -4;
        const int intervalCountDependencyReadWidth = 4;
        long intervalCountDependencyReadPosition;
        uint intervalCountValue;
        RuleCount MappingIntervalCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rawSymbolInMeasuredBytes = SizeRawSymbolIn;
            if (rawSymbolInMeasuredBytes < Position.Minimum || rawSymbolInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rawSymbolInMeasuredBytes;
        }
        
        {
            const int intervalCountMeasuredBytes = SizeIntervalCount;
            if (intervalCountMeasuredBytes < Position.Minimum || intervalCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += intervalCountMeasuredBytes;
        }
        intervalCountDependencyReadPosition = (long)index + intervalCountDependencyReadDistanceFromMappingInterval;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || intervalCountDependencyReadPosition < Position.Minimum || intervalCountDependencyReadPosition > limit || intervalCountDependencyReadWidth > limit - intervalCountDependencyReadPosition) return Position.Failure;
        intervalCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)intervalCountDependencyReadPosition)..]);
        try
        {
            MappingIntervalCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(intervalCountValue)));
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
        received1 = MappingIntervalCountRule0Entries;
        return index;
    }
    internal static int MetadataSymbolsCountStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        return index;
    }
    internal static int MetadataPartialCountStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int MetadataNotFoundCountStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int MetadataMappingsCountStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        const int notFoundCountDependencyReadDistanceFromNotFoundSymbol = -4;
        const int notFoundCountDependencyReadWidth = 4;
        long notFoundCountDependencyReadPosition;
        uint notFoundCountValue;
        RuleCount NotFoundSymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int notFoundCountMeasuredBytes = SizeNotFoundCount;
            if (notFoundCountMeasuredBytes < Position.Minimum || notFoundCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += notFoundCountMeasuredBytes;
        }
        
        {
            notFoundCountDependencyReadPosition = (long)index + notFoundCountDependencyReadDistanceFromNotFoundSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || notFoundCountDependencyReadPosition < Position.Minimum || notFoundCountDependencyReadPosition > limit || notFoundCountDependencyReadWidth > limit - notFoundCountDependencyReadPosition) return Position.Failure;
            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)notFoundCountDependencyReadPosition)..]);
            try
            {
                NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
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
            while (HasRuleCount(in NotFoundSymbolCountRule0Entries))
            {
                int notFoundSymbolEntryStart = index;
                
                {
                    const int notFoundSymbolMeasuredBytes = SizeNotFoundSymbol;
                    if (notFoundSymbolMeasuredBytes < Position.Minimum || notFoundSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += notFoundSymbolMeasuredBytes;
                }
                TakeRuleCount(ref NotFoundSymbolCountRule0Entries);
            }
            if (HasRuleCount(in NotFoundSymbolCountRule0Entries)) return Position.Failure;
        }
        return index;
    }
    internal static int MetadataSchemaDefinitionStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out uint received0)
    {
        received0 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
        schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
        received0 = schemaDefinitionLengthValue;
        return index;
    }
    internal static int MetadataSymbolStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0)
    {
        received0 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        int symbolReaderStart = index;
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        received0 = (index - symbolReaderStart) / SizeSymbol;
        return symbolReaderStart;
    }
    internal static int MetadataPartialSymbolStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0)
    {
        received0 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        int partialSymbolReaderStart = index;
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        received0 = (index - partialSymbolReaderStart) / SizePartialSymbol;
        return partialSymbolReaderStart;
    }
    internal static int MetadataNotFoundSymbolStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0)
    {
        received0 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        const int notFoundCountDependencyReadDistanceFromNotFoundSymbol = -4;
        const int notFoundCountDependencyReadWidth = 4;
        long notFoundCountDependencyReadPosition;
        uint notFoundCountValue;
        RuleCount NotFoundSymbolCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int notFoundCountMeasuredBytes = SizeNotFoundCount;
            if (notFoundCountMeasuredBytes < Position.Minimum || notFoundCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += notFoundCountMeasuredBytes;
        }
        int notFoundSymbolReaderStart = index;
        
        {
            notFoundCountDependencyReadPosition = (long)index + notFoundCountDependencyReadDistanceFromNotFoundSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || notFoundCountDependencyReadPosition < Position.Minimum || notFoundCountDependencyReadPosition > limit || notFoundCountDependencyReadWidth > limit - notFoundCountDependencyReadPosition) return Position.Failure;
            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)notFoundCountDependencyReadPosition)..]);
            try
            {
                NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
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
            while (HasRuleCount(in NotFoundSymbolCountRule0Entries))
            {
                int notFoundSymbolEntryStart = index;
                
                {
                    const int notFoundSymbolMeasuredBytes = SizeNotFoundSymbol;
                    if (notFoundSymbolMeasuredBytes < Position.Minimum || notFoundSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += notFoundSymbolMeasuredBytes;
                }
                TakeRuleCount(ref NotFoundSymbolCountRule0Entries);
            }
            if (HasRuleCount(in NotFoundSymbolCountRule0Entries)) return Position.Failure;
        }
        received0 = (index - notFoundSymbolReaderStart) / SizeNotFoundSymbol;
        return notFoundSymbolReaderStart;
    }
    internal static int MetadataSymbolMappingStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out Parser.RuleCount received1)
    {
        received0 = default;
        received1 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        const int notFoundCountDependencyReadDistanceFromNotFoundSymbol = -4;
        const int notFoundCountDependencyReadWidth = 4;
        long notFoundCountDependencyReadPosition;
        uint notFoundCountValue;
        RuleCount NotFoundSymbolCountRule0Entries;
        const int mappingsCountDependencyReadDistanceFromSymbolMapping = -4;
        const int mappingsCountDependencyReadWidth = 4;
        long mappingsCountDependencyReadPosition;
        uint mappingsCountValue;
        RuleCount SymbolMappingCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int notFoundCountMeasuredBytes = SizeNotFoundCount;
            if (notFoundCountMeasuredBytes < Position.Minimum || notFoundCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += notFoundCountMeasuredBytes;
        }
        
        {
            notFoundCountDependencyReadPosition = (long)index + notFoundCountDependencyReadDistanceFromNotFoundSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || notFoundCountDependencyReadPosition < Position.Minimum || notFoundCountDependencyReadPosition > limit || notFoundCountDependencyReadWidth > limit - notFoundCountDependencyReadPosition) return Position.Failure;
            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)notFoundCountDependencyReadPosition)..]);
            try
            {
                NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
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
            while (HasRuleCount(in NotFoundSymbolCountRule0Entries))
            {
                int notFoundSymbolEntryStart = index;
                
                {
                    const int notFoundSymbolMeasuredBytes = SizeNotFoundSymbol;
                    if (notFoundSymbolMeasuredBytes < Position.Minimum || notFoundSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += notFoundSymbolMeasuredBytes;
                }
                TakeRuleCount(ref NotFoundSymbolCountRule0Entries);
            }
            if (HasRuleCount(in NotFoundSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int mappingsCountMeasuredBytes = SizeMappingsCount;
            if (mappingsCountMeasuredBytes < Position.Minimum || mappingsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mappingsCountMeasuredBytes;
        }
        mappingsCountDependencyReadPosition = (long)index + mappingsCountDependencyReadDistanceFromSymbolMapping;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || mappingsCountDependencyReadPosition < Position.Minimum || mappingsCountDependencyReadPosition > limit || mappingsCountDependencyReadWidth > limit - mappingsCountDependencyReadPosition) return Position.Failure;
        mappingsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)mappingsCountDependencyReadPosition)..]);
        try
        {
            SymbolMappingCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(mappingsCountValue)));
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
        received1 = SymbolMappingCountRule0Entries;
        return index;
    }
    internal static int MetadataMetadataPaddingStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0)
    {
        received0 = default;
        const int schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition = -4;
        const int schemaDefinitionLengthDependencyReadWidth = 4;
        long schemaDefinitionLengthDependencyReadPosition;
        uint schemaDefinitionLengthValue;
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        const int symbolsCountDependencyReadDistanceFromSymbol = -4;
        const int symbolsCountDependencyReadWidth = 4;
        long symbolsCountDependencyReadPosition;
        uint symbolsCountValue;
        RuleCount SymbolCountRule0Entries;
        const int partialCountDependencyReadDistanceFromPartialSymbol = -4;
        const int partialCountDependencyReadWidth = 4;
        long partialCountDependencyReadPosition;
        uint partialCountValue;
        RuleCount PartialSymbolCountRule0Entries;
        const int notFoundCountDependencyReadDistanceFromNotFoundSymbol = -4;
        const int notFoundCountDependencyReadWidth = 4;
        long notFoundCountDependencyReadPosition;
        uint notFoundCountValue;
        RuleCount NotFoundSymbolCountRule0Entries;
        const int mappingsCountDependencyReadDistanceFromSymbolMapping = -4;
        const int mappingsCountDependencyReadWidth = 4;
        long mappingsCountDependencyReadPosition;
        uint mappingsCountValue;
        RuleCount SymbolMappingCountRule0Entries;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int magicMeasuredBytes = SizeMagic;
            if (magicMeasuredBytes < Position.Minimum || magicMeasuredBytes > (long)limit - index) return Position.Failure;
            index += magicMeasuredBytes;
        }
        
        {
            const int versionMeasuredBytes = SizeVersion;
            if (versionMeasuredBytes < Position.Minimum || versionMeasuredBytes > (long)limit - index) return Position.Failure;
            index += versionMeasuredBytes;
        }
        
        {
            const int metadataLengthMeasuredBytes = SizeMetadataLength;
            if (metadataLengthMeasuredBytes < Position.Minimum || metadataLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataLengthMeasuredBytes;
        }
        
        {
            const int datasetMeasuredBytes = SizeDataset;
            if (datasetMeasuredBytes < Position.Minimum || datasetMeasuredBytes > (long)limit - index) return Position.Failure;
            index += datasetMeasuredBytes;
        }
        
        {
            const int schemaMeasuredBytes = SizeSchema;
            if (schemaMeasuredBytes < Position.Minimum || schemaMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaMeasuredBytes;
        }
        
        {
            const int startMeasuredBytes = SizeStart;
            if (startMeasuredBytes < Position.Minimum || startMeasuredBytes > (long)limit - index) return Position.Failure;
            index += startMeasuredBytes;
        }
        
        {
            const int endMeasuredBytes = SizeEnd;
            if (endMeasuredBytes < Position.Minimum || endMeasuredBytes > (long)limit - index) return Position.Failure;
            index += endMeasuredBytes;
        }
        
        {
            const int limitMeasuredBytes = SizeLimit;
            if (limitMeasuredBytes < Position.Minimum || limitMeasuredBytes > (long)limit - index) return Position.Failure;
            index += limitMeasuredBytes;
        }
        
        {
            const int metadataStypeInMeasuredBytes = SizeStypeIn;
            if (metadataStypeInMeasuredBytes < Position.Minimum || metadataStypeInMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeInMeasuredBytes;
        }
        
        {
            const int metadataStypeOutMeasuredBytes = SizeStypeOut;
            if (metadataStypeOutMeasuredBytes < Position.Minimum || metadataStypeOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataStypeOutMeasuredBytes;
        }
        
        {
            const int tsOutMeasuredBytes = SizeTsOut;
            if (tsOutMeasuredBytes < Position.Minimum || tsOutMeasuredBytes > (long)limit - index) return Position.Failure;
            index += tsOutMeasuredBytes;
        }
        
        {
            const int symbolCstrLenMeasuredBytes = SizeSymbolCstrLen;
            if (symbolCstrLenMeasuredBytes < Position.Minimum || symbolCstrLenMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolCstrLenMeasuredBytes;
        }
        
        {
            const int metadataReservedMeasuredBytes = SizeMetadataReserved;
            if (metadataReservedMeasuredBytes < Position.Minimum || metadataReservedMeasuredBytes > (long)limit - index) return Position.Failure;
            index += metadataReservedMeasuredBytes;
        }
        
        {
            const int schemaDefinitionLengthMeasuredBytes = SizeSchemaDefinitionLength;
            if (schemaDefinitionLengthMeasuredBytes < Position.Minimum || schemaDefinitionLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += schemaDefinitionLengthMeasuredBytes;
        }
        
        {
            schemaDefinitionLengthDependencyReadPosition = (long)index + schemaDefinitionLengthDependencyReadDistanceFromSchemaDefinition;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > limit || schemaDefinitionLengthDependencyReadWidth > limit - schemaDefinitionLengthDependencyReadPosition) return Position.Failure;
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        
        {
            const int symbolsCountMeasuredBytes = SizeSymbolsCount;
            if (symbolsCountMeasuredBytes < Position.Minimum || symbolsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += symbolsCountMeasuredBytes;
        }
        
        {
            symbolsCountDependencyReadPosition = (long)index + symbolsCountDependencyReadDistanceFromSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > limit || symbolsCountDependencyReadWidth > limit - symbolsCountDependencyReadPosition) return Position.Failure;
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            try
            {
                SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
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
            while (HasRuleCount(in SymbolCountRule0Entries))
            {
                int symbolEntryStart = index;
                
                {
                    const int symbolMeasuredBytes = SizeSymbol;
                    if (symbolMeasuredBytes < Position.Minimum || symbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += symbolMeasuredBytes;
                }
                TakeRuleCount(ref SymbolCountRule0Entries);
            }
            if (HasRuleCount(in SymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int partialCountMeasuredBytes = SizePartialCount;
            if (partialCountMeasuredBytes < Position.Minimum || partialCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += partialCountMeasuredBytes;
        }
        
        {
            partialCountDependencyReadPosition = (long)index + partialCountDependencyReadDistanceFromPartialSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > limit || partialCountDependencyReadWidth > limit - partialCountDependencyReadPosition) return Position.Failure;
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            try
            {
                PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
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
            while (HasRuleCount(in PartialSymbolCountRule0Entries))
            {
                int partialSymbolEntryStart = index;
                
                {
                    const int partialSymbolMeasuredBytes = SizePartialSymbol;
                    if (partialSymbolMeasuredBytes < Position.Minimum || partialSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += partialSymbolMeasuredBytes;
                }
                TakeRuleCount(ref PartialSymbolCountRule0Entries);
            }
            if (HasRuleCount(in PartialSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int notFoundCountMeasuredBytes = SizeNotFoundCount;
            if (notFoundCountMeasuredBytes < Position.Minimum || notFoundCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += notFoundCountMeasuredBytes;
        }
        
        {
            notFoundCountDependencyReadPosition = (long)index + notFoundCountDependencyReadDistanceFromNotFoundSymbol;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || notFoundCountDependencyReadPosition < Position.Minimum || notFoundCountDependencyReadPosition > limit || notFoundCountDependencyReadWidth > limit - notFoundCountDependencyReadPosition) return Position.Failure;
            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)notFoundCountDependencyReadPosition)..]);
            try
            {
                NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
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
            while (HasRuleCount(in NotFoundSymbolCountRule0Entries))
            {
                int notFoundSymbolEntryStart = index;
                
                {
                    const int notFoundSymbolMeasuredBytes = SizeNotFoundSymbol;
                    if (notFoundSymbolMeasuredBytes < Position.Minimum || notFoundSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += notFoundSymbolMeasuredBytes;
                }
                TakeRuleCount(ref NotFoundSymbolCountRule0Entries);
            }
            if (HasRuleCount(in NotFoundSymbolCountRule0Entries)) return Position.Failure;
        }
        
        {
            const int mappingsCountMeasuredBytes = SizeMappingsCount;
            if (mappingsCountMeasuredBytes < Position.Minimum || mappingsCountMeasuredBytes > (long)limit - index) return Position.Failure;
            index += mappingsCountMeasuredBytes;
        }
        
        {
            mappingsCountDependencyReadPosition = (long)index + mappingsCountDependencyReadDistanceFromSymbolMapping;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || mappingsCountDependencyReadPosition < Position.Minimum || mappingsCountDependencyReadPosition > limit || mappingsCountDependencyReadWidth > limit - mappingsCountDependencyReadPosition) return Position.Failure;
            mappingsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)mappingsCountDependencyReadPosition)..]);
            try
            {
                SymbolMappingCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(mappingsCountValue)));
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
            while (HasRuleCount(in SymbolMappingCountRule0Entries))
            {
                int symbolMappingEntryStart = index;
                
                {
                    int symbolMappingMeasureStart = index;
                    index = MeasureSymbolMapping(bytes, index, limit, start);
                    if (index < Position.Minimum) return Position.Failure;
                    if (index < symbolMappingMeasureStart || index > limit) return Position.Failure;
                }
                TakeRuleCount(ref SymbolMappingCountRule0Entries);
            }
            if (HasRuleCount(in SymbolMappingCountRule0Entries)) return Position.Failure;
        }
        received0 = limit;
        return index;
    }
    internal static int MetadataSchemaDefinitionEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, uint dependency0)
    {
        global::System.Int128 SchemaDefinitionSizeRule0Width;
        int SchemaDefinitionSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(dependency0));
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
            if (SchemaDefinitionSizeRule0Width < Position.Minimum || SchemaDefinitionSizeRule0Width > (long)limit - index) return Position.Failure;
            SchemaDefinitionSizeRule0End = index + (int)SchemaDefinitionSizeRule0Width;
            int schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
            index = schemaDefinitionEnd;
        }
        return index;
    }
    internal static int MetadataMetadataPaddingEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, int limit0)
    {
        global::System.Int128 MetadataPaddingSizeRule0Width;
        int MetadataPaddingSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                MetadataPaddingSizeRule0Width = checked((global::System.Int128)(((long)limit0 - index)));
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
            if (MetadataPaddingSizeRule0Width < Position.Minimum || MetadataPaddingSizeRule0Width > (long)limit - index) return Position.Failure;
            MetadataPaddingSizeRule0End = index + (int)MetadataPaddingSizeRule0Width;
            int metadataPaddingEnd = MetadataPaddingSizeRule0End;
            index = metadataPaddingEnd;
        }
        return index;
    }
    internal static int AuthenticationResponseControlFieldsStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        return index;
    }
    internal static int AuthenticationResponseControlFieldsEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int authenticationResponseControlFieldsEnd = ControlFieldsSizeRule0End;
            index = authenticationResponseControlFieldsEnd;
        }
        return index;
    }
    internal static int ChallengeRequestControlFieldsStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        return index;
    }
    internal static int ChallengeRequestControlFieldsEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int challengeRequestControlFieldsEnd = ControlFieldsSizeRule0End;
            index = challengeRequestControlFieldsEnd;
        }
        return index;
    }
    internal static int GreetingControlFieldsStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        return index;
    }
    internal static int GreetingControlFieldsEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        int ControlFieldsSizeRule0Terminator0Bytes;
        int ControlFieldsSizeRule0Terminator0Offset;
        global::System.Int128 ControlFieldsSizeRule0Width;
        int ControlFieldsSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, limit - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    return Position.Failure;
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            try
            {
                ControlFieldsSizeRule0Width = checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes));
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
            if (ControlFieldsSizeRule0Width < Position.Minimum || ControlFieldsSizeRule0Width > (long)limit - index) return Position.Failure;
            ControlFieldsSizeRule0End = index + (int)ControlFieldsSizeRule0Width;
            int greetingControlFieldsEnd = ControlFieldsSizeRule0End;
            index = greetingControlFieldsEnd;
        }
        return index;
    }
    internal static int GatewayStreamGreetingStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        return index;
    }
    internal static int GatewayStreamChallengeRequestStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            int greetingMeasureStart = index;
            index = MeasureGreeting(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < greetingMeasureStart || index > limit) return Position.Failure;
        }
        return index;
    }
    internal static int GatewayStreamAuthenticationResponseStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            int greetingMeasureStart = index;
            index = MeasureGreeting(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < greetingMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            int challengeRequestMeasureStart = index;
            index = MeasureChallengeRequest(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < challengeRequestMeasureStart || index > limit) return Position.Failure;
        }
        return index;
    }
    internal static int GatewayStreamMetadataStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            int greetingMeasureStart = index;
            index = MeasureGreeting(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < greetingMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            int challengeRequestMeasureStart = index;
            index = MeasureChallengeRequest(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < challengeRequestMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            int authenticationResponseMeasureStart = index;
            index = MeasureAuthenticationResponse(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < authenticationResponseMeasureStart || index > limit) return Position.Failure;
        }
        metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
        metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
        return index;
    }
    internal static int GatewayStreamRecordStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0, out int received0, out int received1, out byte? received2)
    {
        received0 = default;
        received1 = default;
        received2 = default;
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        global::System.Int128 MetadataSizeRule0Width;
        int MetadataSizeRule0End;
        global::System.Int128 RecordPayloadRule0RawEnd;
        int RecordPayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            int greetingMeasureStart = index;
            index = MeasureGreeting(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < greetingMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            int challengeRequestMeasureStart = index;
            index = MeasureChallengeRequest(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < challengeRequestMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            int authenticationResponseMeasureStart = index;
            index = MeasureAuthenticationResponse(bytes, index, limit, start);
            if (index < Position.Minimum) return Position.Failure;
            if (index < authenticationResponseMeasureStart || index > limit) return Position.Failure;
        }
        
        {
            metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
            metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
            const long MetadataSizeRule0Data2 = 8;
            try
            {
                MetadataSizeRule0Width = ((metadataLengthValue) switch { var MetadataSizeRule0ArithmeticLeft0 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft0)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight1)))) }) });
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
            if (MetadataSizeRule0Width < Position.Minimum || MetadataSizeRule0Width > (long)limit - index) return Position.Failure;
            MetadataSizeRule0End = index + (int)MetadataSizeRule0Width;
            int metadataEnd = MetadataSizeRule0End;
            index = metadataEnd;
        }
        try
        {
            RecordPayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (RecordPayloadRule0RawEnd < index || RecordPayloadRule0RawEnd > limit) return Position.Failure;
        RecordPayloadRule0End = (int)RecordPayloadRule0RawEnd;
        received0 = limit;
        received1 = RecordPayloadRule0End;
        received2 = dependency0;
        return index;
    }
    internal static int GatewayStreamMetadataEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        global::System.Int128 MetadataSizeRule0Width;
        int MetadataSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
            metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
            const long MetadataSizeRule0Data2 = 8;
            try
            {
                MetadataSizeRule0Width = ((metadataLengthValue) switch { var MetadataSizeRule0ArithmeticLeft0 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft0)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight1)))) }) });
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
            if (MetadataSizeRule0Width < Position.Minimum || MetadataSizeRule0Width > (long)limit - index) return Position.Failure;
            MetadataSizeRule0End = index + (int)MetadataSizeRule0Width;
            int metadataEnd = MetadataSizeRule0End;
            index = metadataEnd;
        }
        return index;
    }
    internal static int FileMetadataStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0)
    {
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
        metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
        return index;
    }
    internal static int FileRecordStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte? dependency0, out int received0, out int received1, out byte? received2)
    {
        received0 = default;
        received1 = default;
        received2 = default;
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        global::System.Int128 MetadataSizeRule0Width;
        int MetadataSizeRule0End;
        global::System.Int128 RecordPayloadRule0RawEnd;
        int RecordPayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
            metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
            const long MetadataSizeRule0Data2 = 8;
            try
            {
                MetadataSizeRule0Width = ((metadataLengthValue) switch { var MetadataSizeRule0ArithmeticLeft0 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft0)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight1)))) }) });
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
            if (MetadataSizeRule0Width < Position.Minimum || MetadataSizeRule0Width > (long)limit - index) return Position.Failure;
            MetadataSizeRule0End = index + (int)MetadataSizeRule0Width;
            int metadataEnd = MetadataSizeRule0End;
            index = metadataEnd;
        }
        try
        {
            RecordPayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (RecordPayloadRule0RawEnd < index || RecordPayloadRule0RawEnd > limit) return Position.Failure;
        RecordPayloadRule0End = (int)RecordPayloadRule0RawEnd;
        received0 = limit;
        received1 = RecordPayloadRule0End;
        received2 = dependency0;
        return index;
    }
    internal static int FileMetadataEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int metadataLengthDependencyReadDistanceFromMetadata = 4;
        const int metadataLengthDependencyReadWidth = 4;
        long metadataLengthDependencyReadPosition;
        uint metadataLengthValue;
        global::System.Int128 MetadataSizeRule0Width;
        int MetadataSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            metadataLengthDependencyReadPosition = (long)index + metadataLengthDependencyReadDistanceFromMetadata;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > limit || metadataLengthDependencyReadWidth > limit - metadataLengthDependencyReadPosition) return Position.Failure;
            metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
            const long MetadataSizeRule0Data2 = 8;
            try
            {
                MetadataSizeRule0Width = ((metadataLengthValue) switch { var MetadataSizeRule0ArithmeticLeft0 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft0)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight1)))) }) });
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
            if (MetadataSizeRule0Width < Position.Minimum || MetadataSizeRule0Width > (long)limit - index) return Position.Failure;
            MetadataSizeRule0End = index + (int)MetadataSizeRule0Width;
            int metadataEnd = MetadataSizeRule0End;
            index = metadataEnd;
        }
        return index;
    }
}
