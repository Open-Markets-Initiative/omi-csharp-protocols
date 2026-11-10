using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;
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
    private const int RuleTextUtf16UnitBytes = 2;
    private const int TraversalNumberStackBytes = 64;
    private static bool ReadTraversalNumber<T>(ReadOnlySpan<byte> text, int encoding, global::System.Globalization.NumberStyles styles, out T value, out bool present) where T : struct, global::System.Numerics.INumberBase<T>
    {
        scoped ReadOnlySpan<byte> digits = text;
        if (encoding == RuleTextUtf16Little || encoding == RuleTextUtf16Big)
        {
            int units = text.Length / RuleTextUtf16UnitBytes;
            Span<byte> narrowed = units <= TraversalNumberStackBytes ? stackalloc byte[TraversalNumberStackBytes] : new byte[units];
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
    internal const int SizeAcceptedSequenceNumber = 20;
    internal const int SizeAcceptedSession = 10;
    internal const int SizeAddOrderNoMpidAttributionMessage = 35;
    internal const int SizeAddOrderWithMpidAttributionMessage = 39;
    internal const int SizeAttribution = 4;
    internal const int SizeAuctionCollarExtension = 4;
    internal const int SizeAuctionCollarReferencePrice = 4;
    internal const int SizeAuthenticity = 1;
    internal const int SizeBreachedLevel = 1;
    internal const int SizeBrokenTradeMessage = 18;
    internal const int SizeBuySellIndicator = 1;
    internal const int SizeCanceledShares = 4;
    internal const int SizeClientPacketHeader = 3;
    internal const int SizeClientPacketType = 1;
    internal const int SizeCrossPrice = 4;
    internal const int SizeCrossShares = 8;
    internal const int SizeCrossTradeMessage = 39;
    internal const int SizeCrossType = 1;
    internal const int SizeCurrentReferencePrice = 4;
    internal const int SizeDirectListingWithCapitalRaisePriceDiscoveryMessage = 47;
    internal const int SizeEtpFlag = 1;
    internal const int SizeEtpLeverageFactor = 4;
    internal const int SizeEventCode = 1;
    internal const int SizeExecutedShares = 4;
    internal const int SizeExecutionPrice = 4;
    internal const int SizeFarPrice = 4;
    internal const int SizeFinancialStatusIndicator = 1;
    internal const int SizeImbalanceDirection = 1;
    internal const int SizeImbalanceShares = 8;
    internal const int SizeInterestFlag = 1;
    internal const int SizeInverseIndicator = 1;
    internal const int SizeIpoFlag = 1;
    internal const int SizeIpoPrice = 4;
    internal const int SizeIpoQuotationReleaseQualifier = 1;
    internal const int SizeIpoQuotationReleaseTime = 4;
    internal const int SizeIpoQuotingPeriodUpdate = 27;
    internal const int SizeIssueClassification = 1;
    internal const int SizeIssueSubType = 2;
    internal const int SizeLevel1 = 8;
    internal const int SizeLevel2 = 8;
    internal const int SizeLevel3 = 8;
    internal const int SizeLocateCode = 2;
    internal const int SizeLoginAcceptedPacket = 30;
    internal const int SizeLoginRejectedPacket = 1;
    internal const int SizeLoginRequestPacket = 46;
    internal const int SizeLowerAuctionCollarPrice = 4;
    internal const int SizeLowerPriceRangeCollar = 4;
    internal const int SizeLuldAuctionCollarMessage = 34;
    internal const int SizeLuldReferencePriceTier = 1;
    internal const int SizeMarketCategory = 1;
    internal const int SizeMarketCode = 1;
    internal const int SizeMarketMakerMode = 1;
    internal const int SizeMarketParticipantPositionMessage = 25;
    internal const int SizeMarketParticipantState = 1;
    internal const int SizeMatchNumber = 8;
    internal const int SizeMaximumAllowablePrice = 4;
    internal const int SizeMessageCount = 2;
    internal const int SizeMessageHeader = 3;
    internal const int SizeMessageLength = 2;
    internal const int SizeMessageType = 1;
    internal const int SizeMinimumAllowablePrice = 4;
    internal const int SizeMpid = 4;
    internal const int SizeMwcbDeclineLevelMessage = 34;
    internal const int SizeMwcbStatusLevelMessage = 11;
    internal const int SizeNearExecutionPrice = 4;
    internal const int SizeNearExecutionTime = 8;
    internal const int SizeNearPrice = 4;
    internal const int SizeNetOrderImbalanceIndicatorMessage = 49;
    internal const int SizeNewOrderReferenceNumber = 8;
    internal const int SizeNonCrossTradeMessage = 43;
    internal const int SizeOpenEligibilityStatus = 1;
    internal const int SizeOperationalHaltAction = 1;
    internal const int SizeOperationalHaltMessage = 20;
    internal const int SizeOrderCancelMessage = 22;
    internal const int SizeOrderDeleteMessage = 18;
    internal const int SizeOrderExecutedMessage = 30;
    internal const int SizeOrderExecutedWithPriceMessage = 35;
    internal const int SizeOrderReferenceNumber = 8;
    internal const int SizeOrderReplaceMessage = 34;
    internal const int SizeOriginalOrderReferenceNumber = 8;
    internal const int SizePacketHeader = 20;
    internal const int SizePacketLength = 2;
    internal const int SizePairedShares = 8;
    internal const int SizePassword = 10;
    internal const int SizePrice = 4;
    internal const int SizePriceVariationIndicator = 1;
    internal const int SizePrimaryMarketMaker = 1;
    internal const int SizePrintable = 1;
    internal const int SizeReasonCode = 4;
    internal const int SizeRegShoAction = 1;
    internal const int SizeRegShoShortSalePriceTestRestrictedIndicatorMessage = 19;
    internal const int SizeRejectReasonCode = 1;
    internal const int SizeRequestedSequenceNumber = 20;
    internal const int SizeRequestedSession = 10;
    internal const int SizeReserved = 1;
    internal const int SizeRetailPriceImprovementIndicatorMessage = 19;
    internal const int SizeRoundLotSize = 4;
    internal const int SizeRoundLotsOnly = 1;
    internal const int SizeSequenceNumber = 8;
    internal const int SizeSequencedMessageType = 1;
    internal const int SizeServerPacketHeader = 3;
    internal const int SizeServerPacketType = 1;
    internal const int SizeSession = 10;
    internal const int SizeShares = 4;
    internal const int SizeShortSaleThresholdIndicator = 1;
    internal const int SizeStock = 8;
    internal const int SizeStockDirectoryMessage = 38;
    internal const int SizeStockLocate = 2;
    internal const int SizeStockTradingActionMessage = 24;
    internal const int SizeSystemEventMessage = 11;
    internal const int SizeTimestamp = 6;
    internal const int SizeTrackingNumber = 2;
    internal const int SizeTradingState = 1;
    internal const int SizeUnsequencedMessageType = 1;
    internal const int SizeUpperAuctionCollarPrice = 4;
    internal const int SizeUpperPriceRangeCollar = 4;
    internal const int SizeUsername = 6;
    private const long FileMessageReassembleRule1Data2 = 2;
    private static ushort FileMessageReassembleRule1Dependency0Read(global::System.ReadOnlySpan<byte> bytes, int limit, long position)
    {
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        const int messageLengthDependencyReadWidth = 2;
        messageLengthDependencyReadPosition = position;
        if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || limit > bytes.Length || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) throw new RuleValueException();
        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
        return messageLengthValue;
    }
    internal static int FileMessageReassembleRule1BytesRemaining(ReadOnlySpan<byte> bytes, int start, int limit, out int current)
    {
        global::System.Int128 expected;
        current = Position.Failure;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int remaining = limit - start;
        const int messageLengthDependencyReassembleDistance = 0;
        long messageLengthDependencyReassemblePosition = (long)start + messageLengthDependencyReassembleDistance;
        int messageLengthDependencyReassembleRequired = messageLengthDependencyReassembleDistance + SizeMessageLength;
        if (remaining < messageLengthDependencyReassembleRequired)
        {
            current = Position.Minimum;
            return Position.Failure;
        }
        try
        {
            expected = ((FileMessageReassembleRule1Dependency0Read(bytes, limit, messageLengthDependencyReassemblePosition)) switch { var FileMessageReassembleRule1ArithmeticLeft0 => ((FileMessageReassembleRule1Data2) switch { var FileMessageReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticRight1)))) }) });
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
    internal static int StepFileFileMessage(ReadOnlySpan<byte> bytes, int start, int limit, int FileMessagePayloadRule0End)
    {
        const int messageLengthDependencyReadDistanceFromFileMessage = 0;
        const int messageLengthDependencyReadWidth = 2;
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        int FileMessageReassembleRule1Available;
        int FileMessageReassembleRule1Width;
        int FileMessageReassembleRule1End;
        global::System.Int128 FileMessageSizeRule2Width;
        int FileMessageSizeRule2End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < FileMessagePayloadRule0End)) return Position.Ended;
        int fileMessageEntryStart = index;
        
        {
            messageLengthDependencyReadPosition = (long)index + messageLengthDependencyReadDistanceFromFileMessage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) return Position.Failure;
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            FileMessageReassembleRule1Available = FileMessageReassembleRule1BytesRemaining(bytes, index, limit, out FileMessageReassembleRule1Width);
            if (FileMessageReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (FileMessageReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            FileMessageReassembleRule1End = index + FileMessageReassembleRule1Width;
            const long FileMessageSizeRule2Data2 = 2;
            try
            {
                FileMessageSizeRule2Width = ((messageLengthValue) switch { var FileMessageSizeRule2ArithmeticLeft0 => ((FileMessageSizeRule2Data2) switch { var FileMessageSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(FileMessageSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(FileMessageSizeRule2ArithmeticRight1)))) }) });
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
            if (FileMessageSizeRule2Width < Position.Minimum || FileMessageSizeRule2Width > (long)limit - index) return Position.Failure;
            FileMessageSizeRule2End = index + (int)FileMessageSizeRule2Width;
            int fileMessageEnd = FileMessageReassembleRule1End;
            if (FileMessageSizeRule2End != fileMessageEnd) return Position.Failure;
            index = fileMessageEnd;
        }
        if (index <= fileMessageEntryStart || index > FileMessagePayloadRule0End) return Position.Failure;
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
    internal const int SizeServerHeartbeat = 0;
    internal static int MeasureDebugPacket(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 DebugTextSizeRule0Width;
        int DebugTextSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long DebugTextSizeRule0Data2 = 1;
            try
            {
                DebugTextSizeRule0Width = ((dependency0) switch { var DebugTextSizeRule0ArithmeticLeft0 => ((DebugTextSizeRule0Data2) switch { var DebugTextSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DebugTextSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(DebugTextSizeRule0ArithmeticRight1)))) }) });
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
            if (DebugTextSizeRule0Width < Position.Minimum || DebugTextSizeRule0Width > (long)limit - index) return Position.Failure;
            DebugTextSizeRule0End = index + (int)DebugTextSizeRule0Width;
            int debugTextEnd = DebugTextSizeRule0End;
            index = debugTextEnd;
        }
        return index;
    }
    internal static int MeasureServerPayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1)
    {
        bool ServerPayloadBranchRule0Selected;
        bool ServerPayloadBranchRule1Selected;
        bool ServerPayloadBranchRule2Selected;
        bool ServerPayloadBranchRule3Selected;
        global::System.Int128 SequencedDataPacketSizeRule0Width;
        int SequencedDataPacketSizeRule0End;
        bool ServerPayloadBranchRule4Selected;
        bool ServerPayloadBranchRule5Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = "\u002B"u8;
            try
            {
                ServerPayloadBranchRule0Selected = dependency0.SequenceEqual(ServerPayloadBranchRule0Data2);
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
            if (ServerPayloadBranchRule0Selected)
            {
                
                {
                    int debugPacketMeasureStart = index;
                    index = MeasureDebugPacket(bytes, index, limit, start, dependency1);
                    if (index < Position.Minimum) return Position.Failure;
                    if (index < debugPacketMeasureStart || index > limit) return Position.Failure;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = "A"u8;
            try
            {
                ServerPayloadBranchRule1Selected = dependency0.SequenceEqual(ServerPayloadBranchRule1Data2);
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
            if (ServerPayloadBranchRule1Selected)
            {
                
                {
                    const int loginAcceptedPacketMeasuredBytes = SizeLoginAcceptedPacket;
                    if (loginAcceptedPacketMeasuredBytes < Position.Minimum || loginAcceptedPacketMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += loginAcceptedPacketMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = "J"u8;
            try
            {
                ServerPayloadBranchRule2Selected = dependency0.SequenceEqual(ServerPayloadBranchRule2Data2);
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
            if (ServerPayloadBranchRule2Selected)
            {
                
                {
                    const int loginRejectedPacketMeasuredBytes = SizeLoginRejectedPacket;
                    if (loginRejectedPacketMeasuredBytes < Position.Minimum || loginRejectedPacketMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += loginRejectedPacketMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = "S"u8;
            try
            {
                ServerPayloadBranchRule3Selected = dependency0.SequenceEqual(ServerPayloadBranchRule3Data2);
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
            if (ServerPayloadBranchRule3Selected)
            {
                
                {
                    const long SequencedDataPacketSizeRule0Data2 = 1;
                    try
                    {
                        SequencedDataPacketSizeRule0Width = ((dependency1) switch { var SequencedDataPacketSizeRule0ArithmeticLeft0 => ((SequencedDataPacketSizeRule0Data2) switch { var SequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticRight1)))) }) });
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
                    if (SequencedDataPacketSizeRule0Width < Position.Minimum || SequencedDataPacketSizeRule0Width > (long)limit - index) return Position.Failure;
                    SequencedDataPacketSizeRule0End = index + (int)SequencedDataPacketSizeRule0Width;
                    int sequencedDataPacketEnd = SequencedDataPacketSizeRule0End;
                    index = sequencedDataPacketEnd;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = "H"u8;
            try
            {
                ServerPayloadBranchRule4Selected = dependency0.SequenceEqual(ServerPayloadBranchRule4Data2);
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
            if (ServerPayloadBranchRule4Selected)
            {
                
                {
                    const int serverHeartbeatMeasuredBytes = SizeServerHeartbeat;
                    if (serverHeartbeatMeasuredBytes < Position.Minimum || serverHeartbeatMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += serverHeartbeatMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = "Z"u8;
            try
            {
                ServerPayloadBranchRule5Selected = dependency0.SequenceEqual(ServerPayloadBranchRule5Data2);
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
            if (ServerPayloadBranchRule5Selected)
            {
                
                {
                    const int endOfSessionMeasuredBytes = SizeEndOfSession;
                    if (endOfSessionMeasuredBytes < Position.Minimum || endOfSessionMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += endOfSessionMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    private const long ServerSoupBinTcpPacketReassembleRule1Data2 = 2;
    private static ushort ServerSoupBinTcpPacketReassembleRule1Dependency0Read(global::System.ReadOnlySpan<byte> bytes, int limit, long position)
    {
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        const int packetLengthDependencyReadWidth = 2;
        packetLengthDependencyReadPosition = position;
        if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || limit > bytes.Length || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) throw new RuleValueException();
        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
        return packetLengthValue;
    }
    internal static int ServerSoupBinTcpPacketReassembleRule1BytesRemaining(ReadOnlySpan<byte> bytes, int start, int limit, out int current)
    {
        global::System.Int128 expected;
        current = Position.Failure;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int remaining = limit - start;
        const int packetLengthDependencyReassembleDistance = 0;
        long packetLengthDependencyReassemblePosition = (long)start + packetLengthDependencyReassembleDistance;
        int packetLengthDependencyReassembleRequired = packetLengthDependencyReassembleDistance + SizePacketLength;
        if (remaining < packetLengthDependencyReassembleRequired)
        {
            current = Position.Minimum;
            return Position.Failure;
        }
        try
        {
            expected = ((ServerSoupBinTcpPacketReassembleRule1Dependency0Read(bytes, limit, packetLengthDependencyReassemblePosition)) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft0 => ((ServerSoupBinTcpPacketReassembleRule1Data2) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticRight1)))) }) });
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
    internal static int StepServerPacketServerSoupBinTcpPacket(ReadOnlySpan<byte> bytes, int start, int limit, int ServerSoupBinTcpPacketPayloadRule0End)
    {
        const int packetLengthDependencyReadDistanceFromServerSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        int ServerSoupBinTcpPacketReassembleRule1Available;
        int ServerSoupBinTcpPacketReassembleRule1Width;
        int ServerSoupBinTcpPacketReassembleRule1End;
        global::System.Int128 ServerSoupBinTcpPacketSizeRule2Width;
        int ServerSoupBinTcpPacketSizeRule2End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ServerSoupBinTcpPacketPayloadRule0End)) return Position.Ended;
        int serverSoupBinTcpPacketEntryStart = index;
        
        {
            packetLengthDependencyReadPosition = (long)index + packetLengthDependencyReadDistanceFromServerSoupBinTcpPacket;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) return Position.Failure;
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            ServerSoupBinTcpPacketReassembleRule1Available = ServerSoupBinTcpPacketReassembleRule1BytesRemaining(bytes, index, limit, out ServerSoupBinTcpPacketReassembleRule1Width);
            if (ServerSoupBinTcpPacketReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (ServerSoupBinTcpPacketReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            ServerSoupBinTcpPacketReassembleRule1End = index + ServerSoupBinTcpPacketReassembleRule1Width;
            const long ServerSoupBinTcpPacketSizeRule2Data2 = 2;
            try
            {
                ServerSoupBinTcpPacketSizeRule2Width = ((packetLengthValue) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticLeft0 => ((ServerSoupBinTcpPacketSizeRule2Data2) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticRight1)))) }) });
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
            if (ServerSoupBinTcpPacketSizeRule2Width < Position.Minimum || ServerSoupBinTcpPacketSizeRule2Width > (long)limit - index) return Position.Failure;
            ServerSoupBinTcpPacketSizeRule2End = index + (int)ServerSoupBinTcpPacketSizeRule2Width;
            int serverSoupBinTcpPacketEnd = ServerSoupBinTcpPacketReassembleRule1End;
            if (ServerSoupBinTcpPacketSizeRule2End != serverSoupBinTcpPacketEnd) return Position.Failure;
            index = serverSoupBinTcpPacketEnd;
        }
        if (index <= serverSoupBinTcpPacketEntryStart || index > ServerSoupBinTcpPacketPayloadRule0End) return Position.Failure;
        return index;
    }
    internal const int SizeLogoutRequest = 0;
    internal const int SizeClientHeartbeat = 0;
    internal static int MeasureClientPayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1)
    {
        bool ClientPayloadBranchRule0Selected;
        bool ClientPayloadBranchRule1Selected;
        bool ClientPayloadBranchRule2Selected;
        global::System.Int128 UnsequencedDataPacketSizeRule0Width;
        int UnsequencedDataPacketSizeRule0End;
        bool ClientPayloadBranchRule3Selected;
        bool ClientPayloadBranchRule4Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = "\u002B"u8;
            try
            {
                ClientPayloadBranchRule0Selected = dependency0.SequenceEqual(ClientPayloadBranchRule0Data2);
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
            if (ClientPayloadBranchRule0Selected)
            {
                
                {
                    int debugPacketMeasureStart = index;
                    index = MeasureDebugPacket(bytes, index, limit, start, dependency1);
                    if (index < Position.Minimum) return Position.Failure;
                    if (index < debugPacketMeasureStart || index > limit) return Position.Failure;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = "L"u8;
            try
            {
                ClientPayloadBranchRule1Selected = dependency0.SequenceEqual(ClientPayloadBranchRule1Data2);
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
            if (ClientPayloadBranchRule1Selected)
            {
                
                {
                    const int loginRequestPacketMeasuredBytes = SizeLoginRequestPacket;
                    if (loginRequestPacketMeasuredBytes < Position.Minimum || loginRequestPacketMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += loginRequestPacketMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = "U"u8;
            try
            {
                ClientPayloadBranchRule2Selected = dependency0.SequenceEqual(ClientPayloadBranchRule2Data2);
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
            if (ClientPayloadBranchRule2Selected)
            {
                
                {
                    const long UnsequencedDataPacketSizeRule0Data2 = 1;
                    try
                    {
                        UnsequencedDataPacketSizeRule0Width = ((dependency1) switch { var UnsequencedDataPacketSizeRule0ArithmeticLeft0 => ((UnsequencedDataPacketSizeRule0Data2) switch { var UnsequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticRight1)))) }) });
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
                    if (UnsequencedDataPacketSizeRule0Width < Position.Minimum || UnsequencedDataPacketSizeRule0Width > (long)limit - index) return Position.Failure;
                    UnsequencedDataPacketSizeRule0End = index + (int)UnsequencedDataPacketSizeRule0Width;
                    int unsequencedDataPacketEnd = UnsequencedDataPacketSizeRule0End;
                    index = unsequencedDataPacketEnd;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = "R"u8;
            try
            {
                ClientPayloadBranchRule3Selected = dependency0.SequenceEqual(ClientPayloadBranchRule3Data2);
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
            if (ClientPayloadBranchRule3Selected)
            {
                
                {
                    const int clientHeartbeatMeasuredBytes = SizeClientHeartbeat;
                    if (clientHeartbeatMeasuredBytes < Position.Minimum || clientHeartbeatMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += clientHeartbeatMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = "O"u8;
            try
            {
                ClientPayloadBranchRule4Selected = dependency0.SequenceEqual(ClientPayloadBranchRule4Data2);
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
            if (ClientPayloadBranchRule4Selected)
            {
                
                {
                    const int logoutRequestMeasuredBytes = SizeLogoutRequest;
                    if (logoutRequestMeasuredBytes < Position.Minimum || logoutRequestMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += logoutRequestMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    private const long ClientSoupBinTcpPacketReassembleRule1Data2 = 2;
    private static ushort ClientSoupBinTcpPacketReassembleRule1Dependency0Read(global::System.ReadOnlySpan<byte> bytes, int limit, long position)
    {
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        const int packetLengthDependencyReadWidth = 2;
        packetLengthDependencyReadPosition = position;
        if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || limit > bytes.Length || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) throw new RuleValueException();
        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
        return packetLengthValue;
    }
    internal static int ClientSoupBinTcpPacketReassembleRule1BytesRemaining(ReadOnlySpan<byte> bytes, int start, int limit, out int current)
    {
        global::System.Int128 expected;
        current = Position.Failure;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int remaining = limit - start;
        const int packetLengthDependencyReassembleDistance = 0;
        long packetLengthDependencyReassemblePosition = (long)start + packetLengthDependencyReassembleDistance;
        int packetLengthDependencyReassembleRequired = packetLengthDependencyReassembleDistance + SizePacketLength;
        if (remaining < packetLengthDependencyReassembleRequired)
        {
            current = Position.Minimum;
            return Position.Failure;
        }
        try
        {
            expected = ((ClientSoupBinTcpPacketReassembleRule1Dependency0Read(bytes, limit, packetLengthDependencyReassemblePosition)) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft0 => ((ClientSoupBinTcpPacketReassembleRule1Data2) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticRight1)))) }) });
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
    internal static int StepClientPacketClientSoupBinTcpPacket(ReadOnlySpan<byte> bytes, int start, int limit, int ClientSoupBinTcpPacketPayloadRule0End)
    {
        const int packetLengthDependencyReadDistanceFromClientSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        int ClientSoupBinTcpPacketReassembleRule1Available;
        int ClientSoupBinTcpPacketReassembleRule1Width;
        int ClientSoupBinTcpPacketReassembleRule1End;
        global::System.Int128 ClientSoupBinTcpPacketSizeRule2Width;
        int ClientSoupBinTcpPacketSizeRule2End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ClientSoupBinTcpPacketPayloadRule0End)) return Position.Ended;
        int clientSoupBinTcpPacketEntryStart = index;
        
        {
            packetLengthDependencyReadPosition = (long)index + packetLengthDependencyReadDistanceFromClientSoupBinTcpPacket;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) return Position.Failure;
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            ClientSoupBinTcpPacketReassembleRule1Available = ClientSoupBinTcpPacketReassembleRule1BytesRemaining(bytes, index, limit, out ClientSoupBinTcpPacketReassembleRule1Width);
            if (ClientSoupBinTcpPacketReassembleRule1Width < Position.Minimum) return Position.Failure;
            if (ClientSoupBinTcpPacketReassembleRule1Available < Position.Minimum)
            {
                return Position.Failure;
            }
            ClientSoupBinTcpPacketReassembleRule1End = index + ClientSoupBinTcpPacketReassembleRule1Width;
            const long ClientSoupBinTcpPacketSizeRule2Data2 = 2;
            try
            {
                ClientSoupBinTcpPacketSizeRule2Width = ((packetLengthValue) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticLeft0 => ((ClientSoupBinTcpPacketSizeRule2Data2) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticRight1)))) }) });
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
            if (ClientSoupBinTcpPacketSizeRule2Width < Position.Minimum || ClientSoupBinTcpPacketSizeRule2Width > (long)limit - index) return Position.Failure;
            ClientSoupBinTcpPacketSizeRule2End = index + (int)ClientSoupBinTcpPacketSizeRule2Width;
            int clientSoupBinTcpPacketEnd = ClientSoupBinTcpPacketReassembleRule1End;
            if (ClientSoupBinTcpPacketSizeRule2End != clientSoupBinTcpPacketEnd) return Position.Failure;
            index = clientSoupBinTcpPacketEnd;
        }
        if (index <= clientSoupBinTcpPacketEntryStart || index > ClientSoupBinTcpPacketPayloadRule0End) return Position.Failure;
        return index;
    }
    internal static int SelectFilePayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool FilePayloadBranchRule1Selected;
        bool FilePayloadBranchRule2Selected;
        bool FilePayloadBranchRule3Selected;
        bool FilePayloadBranchRule4Selected;
        bool FilePayloadBranchRule5Selected;
        bool FilePayloadBranchRule6Selected;
        bool FilePayloadBranchRule7Selected;
        bool FilePayloadBranchRule8Selected;
        bool FilePayloadBranchRule9Selected;
        bool FilePayloadBranchRule10Selected;
        bool FilePayloadBranchRule11Selected;
        bool FilePayloadBranchRule12Selected;
        bool FilePayloadBranchRule13Selected;
        bool FilePayloadBranchRule14Selected;
        bool FilePayloadBranchRule15Selected;
        bool FilePayloadBranchRule16Selected;
        bool FilePayloadBranchRule17Selected;
        bool FilePayloadBranchRule18Selected;
        bool FilePayloadBranchRule19Selected;
        bool FilePayloadBranchRule20Selected;
        bool FilePayloadBranchRule21Selected;
        bool FilePayloadBranchRule22Selected;
        bool FilePayloadBranchRule23Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule1Data2 = "S"u8;
            try
            {
                FilePayloadBranchRule1Selected = dependency0.SequenceEqual(FilePayloadBranchRule1Data2);
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
            if (FilePayloadBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.SystemEventMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule2Data2 = "R"u8;
            try
            {
                FilePayloadBranchRule2Selected = dependency0.SequenceEqual(FilePayloadBranchRule2Data2);
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
            if (FilePayloadBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockDirectoryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule3Data2 = "H"u8;
            try
            {
                FilePayloadBranchRule3Selected = dependency0.SequenceEqual(FilePayloadBranchRule3Data2);
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
            if (FilePayloadBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockTradingActionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule4Data2 = "Y"u8;
            try
            {
                FilePayloadBranchRule4Selected = dependency0.SequenceEqual(FilePayloadBranchRule4Data2);
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
            if (FilePayloadBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule5Data2 = "L"u8;
            try
            {
                FilePayloadBranchRule5Selected = dependency0.SequenceEqual(FilePayloadBranchRule5Data2);
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
            if (FilePayloadBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MarketParticipantPositionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule6Data2 = "V"u8;
            try
            {
                FilePayloadBranchRule6Selected = dependency0.SequenceEqual(FilePayloadBranchRule6Data2);
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
            if (FilePayloadBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbDeclineLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule7Data2 = "W"u8;
            try
            {
                FilePayloadBranchRule7Selected = dependency0.SequenceEqual(FilePayloadBranchRule7Data2);
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
            if (FilePayloadBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbStatusLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule8Data2 = "K"u8;
            try
            {
                FilePayloadBranchRule8Selected = dependency0.SequenceEqual(FilePayloadBranchRule8Data2);
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
            if (FilePayloadBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.IpoQuotingPeriodUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule9Data2 = "J"u8;
            try
            {
                FilePayloadBranchRule9Selected = dependency0.SequenceEqual(FilePayloadBranchRule9Data2);
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
            if (FilePayloadBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.LuldAuctionCollarMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule10Data2 = "h"u8;
            try
            {
                FilePayloadBranchRule10Selected = dependency0.SequenceEqual(FilePayloadBranchRule10Data2);
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
            if (FilePayloadBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OperationalHaltMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule11Data2 = "A"u8;
            try
            {
                FilePayloadBranchRule11Selected = dependency0.SequenceEqual(FilePayloadBranchRule11Data2);
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
            if (FilePayloadBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderNoMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule12Data2 = "F"u8;
            try
            {
                FilePayloadBranchRule12Selected = dependency0.SequenceEqual(FilePayloadBranchRule12Data2);
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
            if (FilePayloadBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderWithMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule13Data2 = "E"u8;
            try
            {
                FilePayloadBranchRule13Selected = dependency0.SequenceEqual(FilePayloadBranchRule13Data2);
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
            if (FilePayloadBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule14Data2 = "C"u8;
            try
            {
                FilePayloadBranchRule14Selected = dependency0.SequenceEqual(FilePayloadBranchRule14Data2);
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
            if (FilePayloadBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedWithPriceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule15Data2 = "X"u8;
            try
            {
                FilePayloadBranchRule15Selected = dependency0.SequenceEqual(FilePayloadBranchRule15Data2);
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
            if (FilePayloadBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderCancelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule16Data2 = "D"u8;
            try
            {
                FilePayloadBranchRule16Selected = dependency0.SequenceEqual(FilePayloadBranchRule16Data2);
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
            if (FilePayloadBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderDeleteMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule17Data2 = "U"u8;
            try
            {
                FilePayloadBranchRule17Selected = dependency0.SequenceEqual(FilePayloadBranchRule17Data2);
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
            if (FilePayloadBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderReplaceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule18Data2 = "P"u8;
            try
            {
                FilePayloadBranchRule18Selected = dependency0.SequenceEqual(FilePayloadBranchRule18Data2);
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
            if (FilePayloadBranchRule18Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NonCrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule19Data2 = "Q"u8;
            try
            {
                FilePayloadBranchRule19Selected = dependency0.SequenceEqual(FilePayloadBranchRule19Data2);
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
            if (FilePayloadBranchRule19Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.CrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule20Data2 = "B"u8;
            try
            {
                FilePayloadBranchRule20Selected = dependency0.SequenceEqual(FilePayloadBranchRule20Data2);
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
            if (FilePayloadBranchRule20Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.BrokenTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule21Data2 = "I"u8;
            try
            {
                FilePayloadBranchRule21Selected = dependency0.SequenceEqual(FilePayloadBranchRule21Data2);
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
            if (FilePayloadBranchRule21Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NetOrderImbalanceIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule22Data2 = "N"u8;
            try
            {
                FilePayloadBranchRule22Selected = dependency0.SequenceEqual(FilePayloadBranchRule22Data2);
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
            if (FilePayloadBranchRule22Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RetailPriceImprovementIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule23Data2 = "O"u8;
            try
            {
                FilePayloadBranchRule23Selected = dependency0.SequenceEqual(FilePayloadBranchRule23Data2);
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
            if (FilePayloadBranchRule23Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage;
            }
        }
        return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.None;
    }
    internal static int FileMessageFilePayloadStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int messageLengthDependencyReadDistanceFromFileMessageFilePayload = -3;
        const int messageLengthDependencyReadWidth = 2;
        long messageLengthDependencyReadPosition;
        ushort messageLengthValue;
        const int messageTypeDependencyReadDistanceFromFileMessageFilePayload = -1;
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
        messageLengthDependencyReadPosition = (long)index + messageLengthDependencyReadDistanceFromFileMessageFilePayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > limit || messageLengthDependencyReadWidth > limit - messageLengthDependencyReadPosition) return Position.Failure;
        messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
        messageTypeDependencyReadPosition = (long)index + messageTypeDependencyReadDistanceFromFileMessageFilePayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > limit || messageTypeDependencyReadWidth > limit - messageTypeDependencyReadPosition) return Position.Failure;
        messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
        received0 = messageTypeValue;
        received1 = messageLengthValue;
        return index;
    }
    internal static int FileMessageFilePayloadEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 FilePayloadSizeRule0Width;
        int FilePayloadSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long FilePayloadSizeRule0Data2 = 1;
            try
            {
                FilePayloadSizeRule0Width = ((dependency0) switch { var FilePayloadSizeRule0ArithmeticLeft0 => ((FilePayloadSizeRule0Data2) switch { var FilePayloadSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticRight1)))) }) });
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
            if (FilePayloadSizeRule0Width < Position.Minimum || FilePayloadSizeRule0Width > (long)limit - index) return Position.Failure;
            FilePayloadSizeRule0End = index + (int)FilePayloadSizeRule0Width;
            int fileMessageFilePayloadEnd = FilePayloadSizeRule0End;
            index = fileMessageFilePayloadEnd;
        }
        return index;
    }
    internal static int FileFileMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        global::System.Int128 FileMessagePayloadRule0RawEnd;
        int FileMessagePayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        try
        {
            FileMessagePayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (FileMessagePayloadRule0RawEnd < index || FileMessagePayloadRule0RawEnd > limit) return Position.Failure;
        FileMessagePayloadRule0End = (int)FileMessagePayloadRule0RawEnd;
        received0 = limit;
        received1 = FileMessagePayloadRule0End;
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
        bool PayloadBranchRule9Selected;
        bool PayloadBranchRule10Selected;
        bool PayloadBranchRule11Selected;
        bool PayloadBranchRule12Selected;
        bool PayloadBranchRule13Selected;
        bool PayloadBranchRule14Selected;
        bool PayloadBranchRule15Selected;
        bool PayloadBranchRule16Selected;
        bool PayloadBranchRule17Selected;
        bool PayloadBranchRule18Selected;
        bool PayloadBranchRule19Selected;
        bool PayloadBranchRule20Selected;
        bool PayloadBranchRule21Selected;
        bool PayloadBranchRule22Selected;
        bool PayloadBranchRule23Selected;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.SystemEventMessage;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockDirectoryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = "H"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockTradingActionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = "Y"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = "L"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MarketParticipantPositionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = "V"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbDeclineLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = "W"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbStatusLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = "K"u8;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.IpoQuotingPeriodUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = "J"u8;
            try
            {
                PayloadBranchRule9Selected = dependency0.SequenceEqual(PayloadBranchRule9Data2);
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
            if (PayloadBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.LuldAuctionCollarMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = "h"u8;
            try
            {
                PayloadBranchRule10Selected = dependency0.SequenceEqual(PayloadBranchRule10Data2);
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
            if (PayloadBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OperationalHaltMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = "A"u8;
            try
            {
                PayloadBranchRule11Selected = dependency0.SequenceEqual(PayloadBranchRule11Data2);
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
            if (PayloadBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderNoMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = "F"u8;
            try
            {
                PayloadBranchRule12Selected = dependency0.SequenceEqual(PayloadBranchRule12Data2);
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
            if (PayloadBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderWithMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = "E"u8;
            try
            {
                PayloadBranchRule13Selected = dependency0.SequenceEqual(PayloadBranchRule13Data2);
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
            if (PayloadBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = "C"u8;
            try
            {
                PayloadBranchRule14Selected = dependency0.SequenceEqual(PayloadBranchRule14Data2);
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
            if (PayloadBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedWithPriceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = "X"u8;
            try
            {
                PayloadBranchRule15Selected = dependency0.SequenceEqual(PayloadBranchRule15Data2);
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
            if (PayloadBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderCancelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = "D"u8;
            try
            {
                PayloadBranchRule16Selected = dependency0.SequenceEqual(PayloadBranchRule16Data2);
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
            if (PayloadBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderDeleteMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = "U"u8;
            try
            {
                PayloadBranchRule17Selected = dependency0.SequenceEqual(PayloadBranchRule17Data2);
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
            if (PayloadBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderReplaceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = "P"u8;
            try
            {
                PayloadBranchRule18Selected = dependency0.SequenceEqual(PayloadBranchRule18Data2);
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
            if (PayloadBranchRule18Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NonCrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = "Q"u8;
            try
            {
                PayloadBranchRule19Selected = dependency0.SequenceEqual(PayloadBranchRule19Data2);
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
            if (PayloadBranchRule19Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.CrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = "B"u8;
            try
            {
                PayloadBranchRule20Selected = dependency0.SequenceEqual(PayloadBranchRule20Data2);
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
            if (PayloadBranchRule20Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.BrokenTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule21Data2 = "I"u8;
            try
            {
                PayloadBranchRule21Selected = dependency0.SequenceEqual(PayloadBranchRule21Data2);
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
            if (PayloadBranchRule21Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NetOrderImbalanceIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule22Data2 = "N"u8;
            try
            {
                PayloadBranchRule22Selected = dependency0.SequenceEqual(PayloadBranchRule22Data2);
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
            if (PayloadBranchRule22Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RetailPriceImprovementIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule23Data2 = "O"u8;
            try
            {
                PayloadBranchRule23Selected = dependency0.SequenceEqual(PayloadBranchRule23Data2);
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
            if (PayloadBranchRule23Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage;
            }
        }
        return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.None;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Heartbeat;
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
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.EndOfSession;
            }
        }
        
        {
            return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Message;
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
    internal static int SelectSequencedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool SequencedMessageBranchRule1Selected;
        bool SequencedMessageBranchRule2Selected;
        bool SequencedMessageBranchRule3Selected;
        bool SequencedMessageBranchRule4Selected;
        bool SequencedMessageBranchRule5Selected;
        bool SequencedMessageBranchRule6Selected;
        bool SequencedMessageBranchRule7Selected;
        bool SequencedMessageBranchRule8Selected;
        bool SequencedMessageBranchRule9Selected;
        bool SequencedMessageBranchRule10Selected;
        bool SequencedMessageBranchRule11Selected;
        bool SequencedMessageBranchRule12Selected;
        bool SequencedMessageBranchRule13Selected;
        bool SequencedMessageBranchRule14Selected;
        bool SequencedMessageBranchRule15Selected;
        bool SequencedMessageBranchRule16Selected;
        bool SequencedMessageBranchRule17Selected;
        bool SequencedMessageBranchRule18Selected;
        bool SequencedMessageBranchRule19Selected;
        bool SequencedMessageBranchRule20Selected;
        bool SequencedMessageBranchRule21Selected;
        bool SequencedMessageBranchRule22Selected;
        bool SequencedMessageBranchRule23Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule1Data2 = "S"u8;
            try
            {
                SequencedMessageBranchRule1Selected = dependency0.SequenceEqual(SequencedMessageBranchRule1Data2);
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
            if (SequencedMessageBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = "R"u8;
            try
            {
                SequencedMessageBranchRule2Selected = dependency0.SequenceEqual(SequencedMessageBranchRule2Data2);
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
            if (SequencedMessageBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = "H"u8;
            try
            {
                SequencedMessageBranchRule3Selected = dependency0.SequenceEqual(SequencedMessageBranchRule3Data2);
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
            if (SequencedMessageBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = "Y"u8;
            try
            {
                SequencedMessageBranchRule4Selected = dependency0.SequenceEqual(SequencedMessageBranchRule4Data2);
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
            if (SequencedMessageBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = "L"u8;
            try
            {
                SequencedMessageBranchRule5Selected = dependency0.SequenceEqual(SequencedMessageBranchRule5Data2);
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
            if (SequencedMessageBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = "V"u8;
            try
            {
                SequencedMessageBranchRule6Selected = dependency0.SequenceEqual(SequencedMessageBranchRule6Data2);
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
            if (SequencedMessageBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = "W"u8;
            try
            {
                SequencedMessageBranchRule7Selected = dependency0.SequenceEqual(SequencedMessageBranchRule7Data2);
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
            if (SequencedMessageBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = "K"u8;
            try
            {
                SequencedMessageBranchRule8Selected = dependency0.SequenceEqual(SequencedMessageBranchRule8Data2);
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
            if (SequencedMessageBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = "J"u8;
            try
            {
                SequencedMessageBranchRule9Selected = dependency0.SequenceEqual(SequencedMessageBranchRule9Data2);
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
            if (SequencedMessageBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = "h"u8;
            try
            {
                SequencedMessageBranchRule10Selected = dependency0.SequenceEqual(SequencedMessageBranchRule10Data2);
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
            if (SequencedMessageBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = "A"u8;
            try
            {
                SequencedMessageBranchRule11Selected = dependency0.SequenceEqual(SequencedMessageBranchRule11Data2);
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
            if (SequencedMessageBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = "F"u8;
            try
            {
                SequencedMessageBranchRule12Selected = dependency0.SequenceEqual(SequencedMessageBranchRule12Data2);
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
            if (SequencedMessageBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = "E"u8;
            try
            {
                SequencedMessageBranchRule13Selected = dependency0.SequenceEqual(SequencedMessageBranchRule13Data2);
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
            if (SequencedMessageBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = "C"u8;
            try
            {
                SequencedMessageBranchRule14Selected = dependency0.SequenceEqual(SequencedMessageBranchRule14Data2);
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
            if (SequencedMessageBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule15Data2 = "X"u8;
            try
            {
                SequencedMessageBranchRule15Selected = dependency0.SequenceEqual(SequencedMessageBranchRule15Data2);
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
            if (SequencedMessageBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = "D"u8;
            try
            {
                SequencedMessageBranchRule16Selected = dependency0.SequenceEqual(SequencedMessageBranchRule16Data2);
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
            if (SequencedMessageBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = "U"u8;
            try
            {
                SequencedMessageBranchRule17Selected = dependency0.SequenceEqual(SequencedMessageBranchRule17Data2);
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
            if (SequencedMessageBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule18Data2 = "P"u8;
            try
            {
                SequencedMessageBranchRule18Selected = dependency0.SequenceEqual(SequencedMessageBranchRule18Data2);
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
            if (SequencedMessageBranchRule18Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule19Data2 = "Q"u8;
            try
            {
                SequencedMessageBranchRule19Selected = dependency0.SequenceEqual(SequencedMessageBranchRule19Data2);
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
            if (SequencedMessageBranchRule19Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule20Data2 = "B"u8;
            try
            {
                SequencedMessageBranchRule20Selected = dependency0.SequenceEqual(SequencedMessageBranchRule20Data2);
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
            if (SequencedMessageBranchRule20Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule21Data2 = "I"u8;
            try
            {
                SequencedMessageBranchRule21Selected = dependency0.SequenceEqual(SequencedMessageBranchRule21Data2);
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
            if (SequencedMessageBranchRule21Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule22Data2 = "N"u8;
            try
            {
                SequencedMessageBranchRule22Selected = dependency0.SequenceEqual(SequencedMessageBranchRule22Data2);
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
            if (SequencedMessageBranchRule22Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule23Data2 = "O"u8;
            try
            {
                SequencedMessageBranchRule23Selected = dependency0.SequenceEqual(SequencedMessageBranchRule23Data2);
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
            if (SequencedMessageBranchRule23Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage;
            }
        }
        return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.None;
    }
    internal static int SequencedDataPacketSequencedMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int sequencedMessageTypeDependencyReadDistanceFromSequencedMessage = -1;
        const int sequencedMessageTypeDependencyReadWidth = 1;
        long sequencedMessageTypeDependencyReadPosition;
        ReadOnlySpan<byte> sequencedMessageTypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int sequencedMessageTypeMeasuredBytes = SizeSequencedMessageType;
            if (sequencedMessageTypeMeasuredBytes < Position.Minimum || sequencedMessageTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += sequencedMessageTypeMeasuredBytes;
        }
        sequencedMessageTypeDependencyReadPosition = (long)index + sequencedMessageTypeDependencyReadDistanceFromSequencedMessage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > limit || sequencedMessageTypeDependencyReadWidth > limit - sequencedMessageTypeDependencyReadPosition) return Position.Failure;
        sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
        received0 = sequencedMessageTypeValue;
        received1 = dependency0;
        return index;
    }
    internal static int SequencedDataPacketSequencedMessageEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 SequencedMessageSizeRule0Width;
        int SequencedMessageSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long SequencedMessageSizeRule0Data2 = 2;
            try
            {
                SequencedMessageSizeRule0Width = ((dependency0) switch { var SequencedMessageSizeRule0ArithmeticLeft0 => ((SequencedMessageSizeRule0Data2) switch { var SequencedMessageSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticRight1)))) }) });
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
            if (SequencedMessageSizeRule0Width < Position.Minimum || SequencedMessageSizeRule0Width > (long)limit - index) return Position.Failure;
            SequencedMessageSizeRule0End = index + (int)SequencedMessageSizeRule0Width;
            int sequencedMessageEnd = SequencedMessageSizeRule0End;
            index = sequencedMessageEnd;
        }
        return index;
    }
    internal static int DebugPacketDebugTextStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        received0 = dependency0;
        return index;
    }
    internal static int DebugPacketDebugTextEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 DebugTextSizeRule0Width;
        int DebugTextSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long DebugTextSizeRule0Data2 = 1;
            try
            {
                DebugTextSizeRule0Width = ((dependency0) switch { var DebugTextSizeRule0ArithmeticLeft0 => ((DebugTextSizeRule0Data2) switch { var DebugTextSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DebugTextSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(DebugTextSizeRule0ArithmeticRight1)))) }) });
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
            if (DebugTextSizeRule0Width < Position.Minimum || DebugTextSizeRule0Width > (long)limit - index) return Position.Failure;
            DebugTextSizeRule0End = index + (int)DebugTextSizeRule0Width;
            int debugTextEnd = DebugTextSizeRule0End;
            index = debugTextEnd;
        }
        return index;
    }
    internal static int SelectServerPayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1)
    {
        bool ServerPayloadBranchRule0Selected;
        bool ServerPayloadBranchRule1Selected;
        bool ServerPayloadBranchRule2Selected;
        bool ServerPayloadBranchRule3Selected;
        bool ServerPayloadBranchRule4Selected;
        bool ServerPayloadBranchRule5Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = "\u002B"u8;
            try
            {
                ServerPayloadBranchRule0Selected = dependency0.SequenceEqual(ServerPayloadBranchRule0Data2);
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
            if (ServerPayloadBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = "A"u8;
            try
            {
                ServerPayloadBranchRule1Selected = dependency0.SequenceEqual(ServerPayloadBranchRule1Data2);
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
            if (ServerPayloadBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = "J"u8;
            try
            {
                ServerPayloadBranchRule2Selected = dependency0.SequenceEqual(ServerPayloadBranchRule2Data2);
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
            if (ServerPayloadBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = "S"u8;
            try
            {
                ServerPayloadBranchRule3Selected = dependency0.SequenceEqual(ServerPayloadBranchRule3Data2);
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
            if (ServerPayloadBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = "H"u8;
            try
            {
                ServerPayloadBranchRule4Selected = dependency0.SequenceEqual(ServerPayloadBranchRule4Data2);
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
            if (ServerPayloadBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = "Z"u8;
            try
            {
                ServerPayloadBranchRule5Selected = dependency0.SequenceEqual(ServerPayloadBranchRule5Data2);
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
            if (ServerPayloadBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession;
            }
        }
        return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.None;
    }
    internal static int ServerPayloadDebugPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        received0 = dependency1;
        return index;
    }
    internal static int ServerPayloadSequencedDataPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        received0 = dependency1;
        return index;
    }
    internal static int ServerPayloadSequencedDataPacketEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 SequencedDataPacketSizeRule0Width;
        int SequencedDataPacketSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long SequencedDataPacketSizeRule0Data2 = 1;
            try
            {
                SequencedDataPacketSizeRule0Width = ((dependency0) switch { var SequencedDataPacketSizeRule0ArithmeticLeft0 => ((SequencedDataPacketSizeRule0Data2) switch { var SequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticRight1)))) }) });
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
            if (SequencedDataPacketSizeRule0Width < Position.Minimum || SequencedDataPacketSizeRule0Width > (long)limit - index) return Position.Failure;
            SequencedDataPacketSizeRule0End = index + (int)SequencedDataPacketSizeRule0Width;
            int sequencedDataPacketEnd = SequencedDataPacketSizeRule0End;
            index = sequencedDataPacketEnd;
        }
        return index;
    }
    internal static int ServerSoupBinTcpPacketServerPayloadStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int packetLengthDependencyReadDistanceFromServerSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        const int serverPacketTypeDependencyReadDistanceFromServerPayload = -1;
        const int serverPacketTypeDependencyReadWidth = 1;
        long serverPacketTypeDependencyReadPosition;
        ReadOnlySpan<byte> serverPacketTypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        packetLengthDependencyReadPosition = (long)start + packetLengthDependencyReadDistanceFromServerSoupBinTcpPacket;
        if (start < Position.Minimum || start > limit || limit < Position.Minimum || limit > bytes.Length || packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) return Position.Failure;
        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
        
        {
            const int serverPacketHeaderMeasuredBytes = SizeServerPacketHeader;
            if (serverPacketHeaderMeasuredBytes < Position.Minimum || serverPacketHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += serverPacketHeaderMeasuredBytes;
        }
        serverPacketTypeDependencyReadPosition = (long)index + serverPacketTypeDependencyReadDistanceFromServerPayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > limit || serverPacketTypeDependencyReadWidth > limit - serverPacketTypeDependencyReadPosition) return Position.Failure;
        serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
        received0 = serverPacketTypeValue;
        received1 = packetLengthValue;
        return index;
    }
    internal static int ServerPacketServerSoupBinTcpPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        global::System.Int128 ServerSoupBinTcpPacketPayloadRule0RawEnd;
        int ServerSoupBinTcpPacketPayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        try
        {
            ServerSoupBinTcpPacketPayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (ServerSoupBinTcpPacketPayloadRule0RawEnd < index || ServerSoupBinTcpPacketPayloadRule0RawEnd > limit) return Position.Failure;
        ServerSoupBinTcpPacketPayloadRule0End = (int)ServerSoupBinTcpPacketPayloadRule0RawEnd;
        received0 = limit;
        received1 = ServerSoupBinTcpPacketPayloadRule0End;
        return index;
    }
    internal static int UnsequencedDataPacketUnsequencedMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int unsequencedMessageTypeMeasuredBytes = SizeUnsequencedMessageType;
            if (unsequencedMessageTypeMeasuredBytes < Position.Minimum || unsequencedMessageTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += unsequencedMessageTypeMeasuredBytes;
        }
        received0 = dependency0;
        return index;
    }
    internal static int UnsequencedDataPacketUnsequencedMessageEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 UnsequencedMessageSizeRule0Width;
        int UnsequencedMessageSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long UnsequencedMessageSizeRule0Data2 = 2;
            try
            {
                UnsequencedMessageSizeRule0Width = ((dependency0) switch { var UnsequencedMessageSizeRule0ArithmeticLeft0 => ((UnsequencedMessageSizeRule0Data2) switch { var UnsequencedMessageSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticRight1)))) }) });
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
            if (UnsequencedMessageSizeRule0Width < Position.Minimum || UnsequencedMessageSizeRule0Width > (long)limit - index) return Position.Failure;
            UnsequencedMessageSizeRule0End = index + (int)UnsequencedMessageSizeRule0Width;
            int unsequencedMessageEnd = UnsequencedMessageSizeRule0End;
            index = unsequencedMessageEnd;
        }
        return index;
    }
    internal static int SelectClientPayload(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1)
    {
        bool ClientPayloadBranchRule0Selected;
        bool ClientPayloadBranchRule1Selected;
        bool ClientPayloadBranchRule2Selected;
        bool ClientPayloadBranchRule3Selected;
        bool ClientPayloadBranchRule4Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = "\u002B"u8;
            try
            {
                ClientPayloadBranchRule0Selected = dependency0.SequenceEqual(ClientPayloadBranchRule0Data2);
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
            if (ClientPayloadBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = "L"u8;
            try
            {
                ClientPayloadBranchRule1Selected = dependency0.SequenceEqual(ClientPayloadBranchRule1Data2);
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
            if (ClientPayloadBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = "U"u8;
            try
            {
                ClientPayloadBranchRule2Selected = dependency0.SequenceEqual(ClientPayloadBranchRule2Data2);
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
            if (ClientPayloadBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = "R"u8;
            try
            {
                ClientPayloadBranchRule3Selected = dependency0.SequenceEqual(ClientPayloadBranchRule3Data2);
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
            if (ClientPayloadBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = "O"u8;
            try
            {
                ClientPayloadBranchRule4Selected = dependency0.SequenceEqual(ClientPayloadBranchRule4Data2);
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
            if (ClientPayloadBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest;
            }
        }
        return (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.None;
    }
    internal static int ClientPayloadDebugPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        received0 = dependency1;
        return index;
    }
    internal static int ClientPayloadUnsequencedDataPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0, ushort dependency1, out ushort received0)
    {
        received0 = default;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        received0 = dependency1;
        return index;
    }
    internal static int ClientPayloadUnsequencedDataPacketEnd(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0)
    {
        global::System.Int128 UnsequencedDataPacketSizeRule0Width;
        int UnsequencedDataPacketSizeRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const long UnsequencedDataPacketSizeRule0Data2 = 1;
            try
            {
                UnsequencedDataPacketSizeRule0Width = ((dependency0) switch { var UnsequencedDataPacketSizeRule0ArithmeticLeft0 => ((UnsequencedDataPacketSizeRule0Data2) switch { var UnsequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticRight1)))) }) });
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
            if (UnsequencedDataPacketSizeRule0Width < Position.Minimum || UnsequencedDataPacketSizeRule0Width > (long)limit - index) return Position.Failure;
            UnsequencedDataPacketSizeRule0End = index + (int)UnsequencedDataPacketSizeRule0Width;
            int unsequencedDataPacketEnd = UnsequencedDataPacketSizeRule0End;
            index = unsequencedDataPacketEnd;
        }
        return index;
    }
    internal static int ClientSoupBinTcpPacketClientPayloadStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int packetLengthDependencyReadDistanceFromClientSoupBinTcpPacket = 0;
        const int packetLengthDependencyReadWidth = 2;
        long packetLengthDependencyReadPosition;
        ushort packetLengthValue;
        const int clientPacketTypeDependencyReadDistanceFromClientPayload = -1;
        const int clientPacketTypeDependencyReadWidth = 1;
        long clientPacketTypeDependencyReadPosition;
        ReadOnlySpan<byte> clientPacketTypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        packetLengthDependencyReadPosition = (long)start + packetLengthDependencyReadDistanceFromClientSoupBinTcpPacket;
        if (start < Position.Minimum || start > limit || limit < Position.Minimum || limit > bytes.Length || packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > limit || packetLengthDependencyReadWidth > limit - packetLengthDependencyReadPosition) return Position.Failure;
        packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
        
        {
            const int clientPacketHeaderMeasuredBytes = SizeClientPacketHeader;
            if (clientPacketHeaderMeasuredBytes < Position.Minimum || clientPacketHeaderMeasuredBytes > (long)limit - index) return Position.Failure;
            index += clientPacketHeaderMeasuredBytes;
        }
        clientPacketTypeDependencyReadPosition = (long)index + clientPacketTypeDependencyReadDistanceFromClientPayload;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > limit || clientPacketTypeDependencyReadWidth > limit - clientPacketTypeDependencyReadPosition) return Position.Failure;
        clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
        received0 = clientPacketTypeValue;
        received1 = packetLengthValue;
        return index;
    }
    internal static int ClientPacketClientSoupBinTcpPacketStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        global::System.Int128 ClientSoupBinTcpPacketPayloadRule0RawEnd;
        int ClientSoupBinTcpPacketPayloadRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        try
        {
            ClientSoupBinTcpPacketPayloadRule0RawEnd = checked((global::System.Int128)(limit));
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
        if (ClientSoupBinTcpPacketPayloadRule0RawEnd < index || ClientSoupBinTcpPacketPayloadRule0RawEnd > limit) return Position.Failure;
        ClientSoupBinTcpPacketPayloadRule0End = (int)ClientSoupBinTcpPacketPayloadRule0RawEnd;
        received0 = limit;
        received1 = ClientSoupBinTcpPacketPayloadRule0End;
        return index;
    }
}
