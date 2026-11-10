using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;
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
    internal const int SizeAccountQueryOptionalField = 1;
    internal const int SizeAccountQueryResponseOptionalField = 1;
    internal const int SizeAiqCanceledOptionalField = 1;
    internal const int SizeAiqGroupId = 2;
    internal const int SizeAiqStrategy = 1;
    internal const int SizeAppendageLength = 2;
    internal const int SizeBboWeightIndicator = 1;
    internal const int SizeBrokenTradeOptionalField = 1;
    internal const int SizeBrokenTradeReason = 1;
    internal const int SizeCancelOrderOptionalField = 1;
    internal const int SizeCancelOrderReason = 1;
    internal const int SizeCancelPendingOptionalField = 1;
    internal const int SizeCancelRejectOptionalField = 1;
    internal const int SizeCanceledMessageOptionalField = 1;
    internal const int SizeCapacity = 1;
    internal const int SizeClientPacketHeader = 3;
    internal const int SizeClientPacketType = 1;
    internal const int SizeClordid = 14;
    internal const int SizeCrossType = 1;
    internal const int SizeCustomerType = 1;
    internal const int SizeDecrementShares = 4;
    internal const int SizeDisableOrderEntryRequestOptionalField = 1;
    internal const int SizeDisableOrderEntryResponseOptionalField = 1;
    internal const int SizeDiscretionPegOffset = 4;
    internal const int SizeDiscretionPrice = 8;
    internal const int SizeDiscretionPriceType = 1;
    internal const int SizeDisplay = 1;
    internal const int SizeDisplayPrice = 8;
    internal const int SizeDisplayQuantity = 4;
    internal const int SizeEnableOrderEntryRequestOptionalField = 1;
    internal const int SizeEnableOrderEntryResponseOptionalField = 1;
    internal const int SizeEnterOrderOptionalField = 1;
    internal const int SizeEventCode = 1;
    internal const int SizeExecutionPrice = 8;
    internal const int SizeExpireTime = 4;
    internal const int SizeFirm = 4;
    internal const int SizeGroupId = 2;
    internal const int SizeHandleInst = 1;
    internal const int SizeInterMarketSweepEligibility = 1;
    internal const int SizeLiquidityFlag = 1;
    internal const int SizeLocateBroker = 4;
    internal const int SizeLoginAcceptedPacket = 30;
    internal const int SizeLoginRejectedPacket = 1;
    internal const int SizeLoginRequestPacket = 46;
    internal const int SizeMassCancelRequestOptionalField = 1;
    internal const int SizeMassCancelResponseOptionalField = 1;
    internal const int SizeMatchNumber = 8;
    internal const int SizeMaxFloor = 4;
    internal const int SizeMinQty = 4;
    internal const int SizeModifyOrderOptionalField = 1;
    internal const int SizeNextUserRefNum = 4;
    internal const int SizeOptionalFieldLength = 1;
    internal const int SizeOrderAcceptedOptionalField = 1;
    internal const int SizeOrderCancelReason = 1;
    internal const int SizeOrderExecutedOptionalField = 1;
    internal const int SizeOrderModifiedOptionalField = 1;
    internal const int SizeOrderPriorityUpdateOptionalField = 1;
    internal const int SizeOrderReferenceNumber = 8;
    internal const int SizeOrderRestatedOptionalField = 1;
    internal const int SizeOrderRestatedReason = 1;
    internal const int SizeOrderState = 1;
    internal const int SizeOrigUserRefNum = 4;
    internal const int SizePacketLength = 2;
    internal const int SizePassword = 10;
    internal const int SizePegOffset = 4;
    internal const int SizePostOnly = 1;
    internal const int SizePrice = 8;
    internal const int SizePriceType = 1;
    internal const int SizeQuantity = 4;
    internal const int SizeQuantityPreventedFromTrading = 4;
    internal const int SizeRandomReserves = 4;
    internal const int SizeRejectReasonCode = 1;
    internal const int SizeRejectedOrderOptionalField = 1;
    internal const int SizeRejectedOrderReason = 2;
    internal const int SizeReplaceOrderOptionalField = 1;
    internal const int SizeReplacedMessageOptionalField = 1;
    internal const int SizeRequestedSequenceNumber = 20;
    internal const int SizeRequestedSession = 10;
    internal const int SizeRoute = 4;
    internal const int SizeSecondaryOrdRefNum = 8;
    internal const int SizeSequencedMessageType = 1;
    internal const int SizeServerPacketHeader = 3;
    internal const int SizeServerPacketType = 1;
    internal const int SizeSharesLocated = 1;
    internal const int SizeSide = 1;
    internal const int SizeSymbol = 8;
    internal const int SizeSystemEventMessage = 9;
    internal const int SizeTimeInForce = 1;
    internal const int SizeTimestamp = 8;
    internal const int SizeTradeNow = 1;
    internal const int SizeUnsequencedMessageType = 1;
    internal const int SizeUserRefIdx = 1;
    internal const int SizeUserRefNum = 4;
    internal const int SizeUsername = 6;
    internal const int SizeEndOfSession = 0;
    internal const int SizeServerHeartbeat = 0;
    internal static int MeasureEnableOrderEntryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnableOrderEntryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnableOrderEntryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                EnableOrderEntryResponseOptionalValueBranchRule0Selected = (dependency0 == EnableOrderEntryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnableOrderEntryResponseOptionalValueBranchRule0Selected)
            {
                
                {
                    const int enableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (enableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || enableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureEnableOrderEntryResponseMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int enableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage = -2;
        const int enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
        ushort enableOrderEntryResponseMessageAppendageLengthValue;
        global::System.Int128 EnableOrderEntryResponseAppendageCountRule0RawEnd;
        int EnableOrderEntryResponseAppendageCountRule0End;
        const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage = 0;
        const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte enableOrderEntryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 EnableOrderEntryResponseAppendageSizeRule1Width;
        int EnableOrderEntryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (enableOrderEntryResponseMessageTimestampMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enableOrderEntryResponseMessageUserRefNumMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageFirmMeasuredBytes = SizeFirm;
            if (enableOrderEntryResponseMessageFirmMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enableOrderEntryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageAppendageLengthMeasuredBytes;
        }
        
        {
            enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = (long)index + enableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > limit || enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > limit - enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            enableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                EnableOrderEntryResponseAppendageCountRule0RawEnd = ((enableOrderEntryResponseMessageAppendageLengthValue) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnableOrderEntryResponseAppendageCountRule0RawEnd < index || EnableOrderEntryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
            EnableOrderEntryResponseAppendageCountRule0End = (int)EnableOrderEntryResponseAppendageCountRule0RawEnd;
            while (index < EnableOrderEntryResponseAppendageCountRule0End)
            {
                int enableOrderEntryResponseAppendageEntryStart = index;
                
                {
                    enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    enableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
                    const long EnableOrderEntryResponseAppendageSizeRule1Data2 = 1;
                    try
                    {
                        EnableOrderEntryResponseAppendageSizeRule1Width = ((enableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryResponseAppendageSizeRule1Data2) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (EnableOrderEntryResponseAppendageSizeRule1Width < Position.Minimum || EnableOrderEntryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    EnableOrderEntryResponseAppendageSizeRule1End = index + (int)EnableOrderEntryResponseAppendageSizeRule1Width;
                    int enableOrderEntryResponseAppendageEnd = EnableOrderEntryResponseAppendageSizeRule1End;
                    index = enableOrderEntryResponseAppendageEnd;
                }
                if (index <= enableOrderEntryResponseAppendageEntryStart || index > EnableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
            }
            if (index != EnableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepEnableOrderEntryResponseMessageEnableOrderEntryResponseAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int EnableOrderEntryResponseAppendageCountRule0End)
    {
        const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage = 0;
        const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte enableOrderEntryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 EnableOrderEntryResponseAppendageSizeRule1Width;
        int EnableOrderEntryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < EnableOrderEntryResponseAppendageCountRule0End)) return Position.Ended;
        int enableOrderEntryResponseAppendageEntryStart = index;
        
        {
            enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            enableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            const long EnableOrderEntryResponseAppendageSizeRule1Data2 = 1;
            try
            {
                EnableOrderEntryResponseAppendageSizeRule1Width = ((enableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryResponseAppendageSizeRule1Data2) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnableOrderEntryResponseAppendageSizeRule1Width < Position.Minimum || EnableOrderEntryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            EnableOrderEntryResponseAppendageSizeRule1End = index + (int)EnableOrderEntryResponseAppendageSizeRule1Width;
            int enableOrderEntryResponseAppendageEnd = EnableOrderEntryResponseAppendageSizeRule1End;
            index = enableOrderEntryResponseAppendageEnd;
        }
        if (index <= enableOrderEntryResponseAppendageEntryStart || index > EnableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureDisableOrderEntryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool DisableOrderEntryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte DisableOrderEntryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                DisableOrderEntryResponseOptionalValueBranchRule0Selected = (dependency0 == DisableOrderEntryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (DisableOrderEntryResponseOptionalValueBranchRule0Selected)
            {
                
                {
                    const int disableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (disableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || disableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += disableOrderEntryResponseOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureDisableOrderEntryResponseMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int disableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage = -2;
        const int disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
        ushort disableOrderEntryResponseMessageAppendageLengthValue;
        global::System.Int128 DisableOrderEntryResponseAppendageCountRule0RawEnd;
        int DisableOrderEntryResponseAppendageCountRule0End;
        const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage = 0;
        const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte disableOrderEntryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 DisableOrderEntryResponseAppendageSizeRule1Width;
        int DisableOrderEntryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (disableOrderEntryResponseMessageTimestampMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (disableOrderEntryResponseMessageUserRefNumMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageFirmMeasuredBytes = SizeFirm;
            if (disableOrderEntryResponseMessageFirmMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (disableOrderEntryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageAppendageLengthMeasuredBytes;
        }
        
        {
            disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = (long)index + disableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > limit || disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > limit - disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            disableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                DisableOrderEntryResponseAppendageCountRule0RawEnd = ((disableOrderEntryResponseMessageAppendageLengthValue) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (DisableOrderEntryResponseAppendageCountRule0RawEnd < index || DisableOrderEntryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
            DisableOrderEntryResponseAppendageCountRule0End = (int)DisableOrderEntryResponseAppendageCountRule0RawEnd;
            while (index < DisableOrderEntryResponseAppendageCountRule0End)
            {
                int disableOrderEntryResponseAppendageEntryStart = index;
                
                {
                    disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    disableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
                    const long DisableOrderEntryResponseAppendageSizeRule1Data2 = 1;
                    try
                    {
                        DisableOrderEntryResponseAppendageSizeRule1Width = ((disableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryResponseAppendageSizeRule1Data2) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (DisableOrderEntryResponseAppendageSizeRule1Width < Position.Minimum || DisableOrderEntryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    DisableOrderEntryResponseAppendageSizeRule1End = index + (int)DisableOrderEntryResponseAppendageSizeRule1Width;
                    int disableOrderEntryResponseAppendageEnd = DisableOrderEntryResponseAppendageSizeRule1End;
                    index = disableOrderEntryResponseAppendageEnd;
                }
                if (index <= disableOrderEntryResponseAppendageEntryStart || index > DisableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
            }
            if (index != DisableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepDisableOrderEntryResponseMessageDisableOrderEntryResponseAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int DisableOrderEntryResponseAppendageCountRule0End)
    {
        const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage = 0;
        const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte disableOrderEntryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 DisableOrderEntryResponseAppendageSizeRule1Width;
        int DisableOrderEntryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < DisableOrderEntryResponseAppendageCountRule0End)) return Position.Ended;
        int disableOrderEntryResponseAppendageEntryStart = index;
        
        {
            disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            disableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            const long DisableOrderEntryResponseAppendageSizeRule1Data2 = 1;
            try
            {
                DisableOrderEntryResponseAppendageSizeRule1Width = ((disableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryResponseAppendageSizeRule1Data2) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (DisableOrderEntryResponseAppendageSizeRule1Width < Position.Minimum || DisableOrderEntryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            DisableOrderEntryResponseAppendageSizeRule1End = index + (int)DisableOrderEntryResponseAppendageSizeRule1Width;
            int disableOrderEntryResponseAppendageEnd = DisableOrderEntryResponseAppendageSizeRule1End;
            index = disableOrderEntryResponseAppendageEnd;
        }
        if (index <= disableOrderEntryResponseAppendageEntryStart || index > DisableOrderEntryResponseAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureMassCancelResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool MassCancelResponseOptionalValueBranchRule0Selected;
        bool MassCancelResponseOptionalValueBranchRule1Selected;
        bool MassCancelResponseOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte MassCancelResponseOptionalValueBranchRule0Data2 = 27;
            try
            {
                MassCancelResponseOptionalValueBranchRule0Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule0Selected)
            {
                
                {
                    const int massCancelResponseOptionalValueSideMeasuredBytes = SizeSide;
                    if (massCancelResponseOptionalValueSideMeasuredBytes < Position.Minimum || massCancelResponseOptionalValueSideMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelResponseOptionalValueSideMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte MassCancelResponseOptionalValueBranchRule1Data2 = 24;
            try
            {
                MassCancelResponseOptionalValueBranchRule1Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule1Selected)
            {
                
                {
                    const int massCancelResponseOptionalValueGroupIdMeasuredBytes = SizeGroupId;
                    if (massCancelResponseOptionalValueGroupIdMeasuredBytes < Position.Minimum || massCancelResponseOptionalValueGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelResponseOptionalValueGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte MassCancelResponseOptionalValueBranchRule2Data2 = 28;
            try
            {
                MassCancelResponseOptionalValueBranchRule2Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule2Selected)
            {
                
                {
                    const int massCancelResponseOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (massCancelResponseOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || massCancelResponseOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelResponseOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureMassCancelResponseMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int massCancelResponseMessageAppendageLengthDependencyReadDistanceFromMassCancelResponseAppendage = -2;
        const int massCancelResponseMessageAppendageLengthDependencyReadWidth = 2;
        long massCancelResponseMessageAppendageLengthDependencyReadPosition;
        ushort massCancelResponseMessageAppendageLengthValue;
        global::System.Int128 MassCancelResponseAppendageCountRule0RawEnd;
        int MassCancelResponseAppendageCountRule0End;
        const int massCancelResponseAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelResponseAppendage = 0;
        const int massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte massCancelResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 MassCancelResponseAppendageSizeRule1Width;
        int MassCancelResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (massCancelResponseMessageTimestampMeasuredBytes < Position.Minimum || massCancelResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (massCancelResponseMessageUserRefNumMeasuredBytes < Position.Minimum || massCancelResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageFirmMeasuredBytes = SizeFirm;
            if (massCancelResponseMessageFirmMeasuredBytes < Position.Minimum || massCancelResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageSymbolMeasuredBytes = SizeSymbol;
            if (massCancelResponseMessageSymbolMeasuredBytes < Position.Minimum || massCancelResponseMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageSymbolMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (massCancelResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || massCancelResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageAppendageLengthMeasuredBytes;
        }
        
        {
            massCancelResponseMessageAppendageLengthDependencyReadPosition = (long)index + massCancelResponseMessageAppendageLengthDependencyReadDistanceFromMassCancelResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelResponseMessageAppendageLengthDependencyReadPosition > limit || massCancelResponseMessageAppendageLengthDependencyReadWidth > limit - massCancelResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            massCancelResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelResponseMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                MassCancelResponseAppendageCountRule0RawEnd = ((massCancelResponseMessageAppendageLengthValue) switch { var MassCancelResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (MassCancelResponseAppendageCountRule0RawEnd < index || MassCancelResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
            MassCancelResponseAppendageCountRule0End = (int)MassCancelResponseAppendageCountRule0RawEnd;
            while (index < MassCancelResponseAppendageCountRule0End)
            {
                int massCancelResponseAppendageEntryStart = index;
                
                {
                    massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + massCancelResponseAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelResponseAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    massCancelResponseAppendageOptionalFieldLengthValue = bytes[(int)massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition];
                    const long MassCancelResponseAppendageSizeRule1Data2 = 1;
                    try
                    {
                        MassCancelResponseAppendageSizeRule1Width = ((massCancelResponseAppendageOptionalFieldLengthValue) switch { var MassCancelResponseAppendageSizeRule1ArithmeticLeft0 => ((MassCancelResponseAppendageSizeRule1Data2) switch { var MassCancelResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (MassCancelResponseAppendageSizeRule1Width < Position.Minimum || MassCancelResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    MassCancelResponseAppendageSizeRule1End = index + (int)MassCancelResponseAppendageSizeRule1Width;
                    int massCancelResponseAppendageEnd = MassCancelResponseAppendageSizeRule1End;
                    index = massCancelResponseAppendageEnd;
                }
                if (index <= massCancelResponseAppendageEntryStart || index > MassCancelResponseAppendageCountRule0End) return Position.Failure;
            }
            if (index != MassCancelResponseAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepMassCancelResponseMessageMassCancelResponseAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int MassCancelResponseAppendageCountRule0End)
    {
        const int massCancelResponseAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelResponseAppendage = 0;
        const int massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte massCancelResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 MassCancelResponseAppendageSizeRule1Width;
        int MassCancelResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < MassCancelResponseAppendageCountRule0End)) return Position.Ended;
        int massCancelResponseAppendageEntryStart = index;
        
        {
            massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + massCancelResponseAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            massCancelResponseAppendageOptionalFieldLengthValue = bytes[(int)massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition];
            const long MassCancelResponseAppendageSizeRule1Data2 = 1;
            try
            {
                MassCancelResponseAppendageSizeRule1Width = ((massCancelResponseAppendageOptionalFieldLengthValue) switch { var MassCancelResponseAppendageSizeRule1ArithmeticLeft0 => ((MassCancelResponseAppendageSizeRule1Data2) switch { var MassCancelResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (MassCancelResponseAppendageSizeRule1Width < Position.Minimum || MassCancelResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            MassCancelResponseAppendageSizeRule1End = index + (int)MassCancelResponseAppendageSizeRule1Width;
            int massCancelResponseAppendageEnd = MassCancelResponseAppendageSizeRule1End;
            index = massCancelResponseAppendageEnd;
        }
        if (index <= massCancelResponseAppendageEntryStart || index > MassCancelResponseAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureAccountQueryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AccountQueryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AccountQueryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                AccountQueryResponseOptionalValueBranchRule0Selected = (dependency0 == AccountQueryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AccountQueryResponseOptionalValueBranchRule0Selected)
            {
                
                {
                    const int accountQueryResponseOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (accountQueryResponseOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || accountQueryResponseOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryResponseOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureAccountQueryResponseMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int accountQueryResponseMessageAppendageLengthDependencyReadDistanceFromAccountQueryResponseMessageAppendageLength = 0;
        const int accountQueryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long accountQueryResponseMessageAppendageLengthDependencyReadPosition = default;
        ushort accountQueryResponseMessageAppendageLengthValue = default;
        global::System.Int128 AccountQueryResponseAppendageCountRule0RawEnd;
        int AccountQueryResponseAppendageCountRule0End;
        const int accountQueryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryResponseAppendage = 0;
        const int accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte accountQueryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 AccountQueryResponseAppendageSizeRule1Width;
        int AccountQueryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int accountQueryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (accountQueryResponseMessageTimestampMeasuredBytes < Position.Minimum || accountQueryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int nextUserRefNumMeasuredBytes = SizeNextUserRefNum;
            if (nextUserRefNumMeasuredBytes < Position.Minimum || nextUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += nextUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                accountQueryResponseMessageAppendageLengthDependencyReadPosition = (long)index + accountQueryResponseMessageAppendageLengthDependencyReadDistanceFromAccountQueryResponseMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryResponseMessageAppendageLengthDependencyReadPosition > limit || accountQueryResponseMessageAppendageLengthDependencyReadWidth > limit - accountQueryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                accountQueryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryResponseMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int accountQueryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (accountQueryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || accountQueryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryResponseMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                AccountQueryResponseAppendageCountRule0RawEnd = ((accountQueryResponseMessageAppendageLengthValue) switch { var AccountQueryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AccountQueryResponseAppendageCountRule0RawEnd < index || AccountQueryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
            AccountQueryResponseAppendageCountRule0End = (int)AccountQueryResponseAppendageCountRule0RawEnd;
            while (index < AccountQueryResponseAppendageCountRule0End)
            {
                int accountQueryResponseAppendageEntryStart = index;
                
                {
                    accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + accountQueryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryResponseAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    accountQueryResponseAppendageOptionalFieldLengthValue = bytes[(int)accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition];
                    const long AccountQueryResponseAppendageSizeRule1Data2 = 1;
                    try
                    {
                        AccountQueryResponseAppendageSizeRule1Width = ((accountQueryResponseAppendageOptionalFieldLengthValue) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryResponseAppendageSizeRule1Data2) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (AccountQueryResponseAppendageSizeRule1Width < Position.Minimum || AccountQueryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    AccountQueryResponseAppendageSizeRule1End = index + (int)AccountQueryResponseAppendageSizeRule1Width;
                    int accountQueryResponseAppendageEnd = AccountQueryResponseAppendageSizeRule1End;
                    index = accountQueryResponseAppendageEnd;
                }
                if (index <= accountQueryResponseAppendageEntryStart || index > AccountQueryResponseAppendageCountRule0End) return Position.Failure;
            }
            if (index != AccountQueryResponseAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepAccountQueryResponseMessageAccountQueryResponseAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int AccountQueryResponseAppendageCountRule0End)
    {
        const int accountQueryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryResponseAppendage = 0;
        const int accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition;
        byte accountQueryResponseAppendageOptionalFieldLengthValue;
        global::System.Int128 AccountQueryResponseAppendageSizeRule1Width;
        int AccountQueryResponseAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < AccountQueryResponseAppendageCountRule0End)) return Position.Ended;
        int accountQueryResponseAppendageEntryStart = index;
        
        {
            accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition = (long)index + accountQueryResponseAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryResponseAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition > limit || accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth > limit - accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            accountQueryResponseAppendageOptionalFieldLengthValue = bytes[(int)accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            const long AccountQueryResponseAppendageSizeRule1Data2 = 1;
            try
            {
                AccountQueryResponseAppendageSizeRule1Width = ((accountQueryResponseAppendageOptionalFieldLengthValue) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryResponseAppendageSizeRule1Data2) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AccountQueryResponseAppendageSizeRule1Width < Position.Minimum || AccountQueryResponseAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            AccountQueryResponseAppendageSizeRule1End = index + (int)AccountQueryResponseAppendageSizeRule1Width;
            int accountQueryResponseAppendageEnd = AccountQueryResponseAppendageSizeRule1End;
            index = accountQueryResponseAppendageEnd;
        }
        if (index <= accountQueryResponseAppendageEntryStart || index > AccountQueryResponseAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureOrderRestatedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderRestatedOptionalValueBranchRule0Selected;
        bool OrderRestatedOptionalValueBranchRule1Selected;
        bool OrderRestatedOptionalValueBranchRule2Selected;
        bool OrderRestatedOptionalValueBranchRule3Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderRestatedOptionalValueBranchRule0Data2 = 22;
            try
            {
                OrderRestatedOptionalValueBranchRule0Selected = (dependency0 == OrderRestatedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule0Selected)
            {
                
                {
                    const int displayQuantityMeasuredBytes = SizeDisplayQuantity;
                    if (displayQuantityMeasuredBytes < Position.Minimum || displayQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += displayQuantityMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule1Data2 = 23;
            try
            {
                OrderRestatedOptionalValueBranchRule1Selected = (dependency0 == OrderRestatedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule1Selected)
            {
                
                {
                    const int displayPriceMeasuredBytes = SizeDisplayPrice;
                    if (displayPriceMeasuredBytes < Position.Minimum || displayPriceMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += displayPriceMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule2Data2 = 1;
            try
            {
                OrderRestatedOptionalValueBranchRule2Selected = (dependency0 == OrderRestatedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule2Selected)
            {
                
                {
                    const int secondaryOrdRefNumMeasuredBytes = SizeSecondaryOrdRefNum;
                    if (secondaryOrdRefNumMeasuredBytes < Position.Minimum || secondaryOrdRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += secondaryOrdRefNumMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule3Data2 = 28;
            try
            {
                OrderRestatedOptionalValueBranchRule3Selected = (dependency0 == OrderRestatedOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule3Selected)
            {
                
                {
                    const int orderRestatedOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (orderRestatedOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || orderRestatedOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderRestatedOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureOrderRestatedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int orderRestatedMessageAppendageLengthDependencyReadDistanceFromOrderRestatedAppendage = -2;
        const int orderRestatedMessageAppendageLengthDependencyReadWidth = 2;
        long orderRestatedMessageAppendageLengthDependencyReadPosition;
        ushort orderRestatedMessageAppendageLengthValue;
        global::System.Int128 OrderRestatedAppendageCountRule0RawEnd;
        int OrderRestatedAppendageCountRule0End;
        const int orderRestatedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderRestatedAppendage = 0;
        const int orderRestatedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderRestatedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderRestatedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderRestatedAppendageSizeRule1Width;
        int OrderRestatedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderRestatedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderRestatedMessageTimestampMeasuredBytes < Position.Minimum || orderRestatedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderRestatedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderRestatedMessageUserRefNumMeasuredBytes < Position.Minimum || orderRestatedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderRestatedReasonMeasuredBytes = SizeOrderRestatedReason;
            if (orderRestatedReasonMeasuredBytes < Position.Minimum || orderRestatedReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedReasonMeasuredBytes;
        }
        
        {
            const int orderRestatedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderRestatedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderRestatedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageAppendageLengthMeasuredBytes;
        }
        
        {
            orderRestatedMessageAppendageLengthDependencyReadPosition = (long)index + orderRestatedMessageAppendageLengthDependencyReadDistanceFromOrderRestatedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderRestatedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderRestatedMessageAppendageLengthDependencyReadPosition > limit || orderRestatedMessageAppendageLengthDependencyReadWidth > limit - orderRestatedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            orderRestatedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderRestatedMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                OrderRestatedAppendageCountRule0RawEnd = ((orderRestatedMessageAppendageLengthValue) switch { var OrderRestatedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderRestatedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderRestatedAppendageCountRule0RawEnd < index || OrderRestatedAppendageCountRule0RawEnd > limit) return Position.Failure;
            OrderRestatedAppendageCountRule0End = (int)OrderRestatedAppendageCountRule0RawEnd;
            while (index < OrderRestatedAppendageCountRule0End)
            {
                int orderRestatedAppendageEntryStart = index;
                
                {
                    orderRestatedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderRestatedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderRestatedAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderRestatedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderRestatedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderRestatedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderRestatedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    orderRestatedAppendageOptionalFieldLengthValue = bytes[(int)orderRestatedAppendageOptionalFieldLengthDependencyReadPosition];
                    const long OrderRestatedAppendageSizeRule1Data2 = 1;
                    try
                    {
                        OrderRestatedAppendageSizeRule1Width = ((orderRestatedAppendageOptionalFieldLengthValue) switch { var OrderRestatedAppendageSizeRule1ArithmeticLeft0 => ((OrderRestatedAppendageSizeRule1Data2) switch { var OrderRestatedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (OrderRestatedAppendageSizeRule1Width < Position.Minimum || OrderRestatedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    OrderRestatedAppendageSizeRule1End = index + (int)OrderRestatedAppendageSizeRule1Width;
                    int orderRestatedAppendageEnd = OrderRestatedAppendageSizeRule1End;
                    index = orderRestatedAppendageEnd;
                }
                if (index <= orderRestatedAppendageEntryStart || index > OrderRestatedAppendageCountRule0End) return Position.Failure;
            }
            if (index != OrderRestatedAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepOrderRestatedMessageOrderRestatedAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int OrderRestatedAppendageCountRule0End)
    {
        const int orderRestatedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderRestatedAppendage = 0;
        const int orderRestatedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderRestatedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderRestatedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderRestatedAppendageSizeRule1Width;
        int OrderRestatedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < OrderRestatedAppendageCountRule0End)) return Position.Ended;
        int orderRestatedAppendageEntryStart = index;
        
        {
            orderRestatedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderRestatedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderRestatedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderRestatedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderRestatedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderRestatedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderRestatedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            orderRestatedAppendageOptionalFieldLengthValue = bytes[(int)orderRestatedAppendageOptionalFieldLengthDependencyReadPosition];
            const long OrderRestatedAppendageSizeRule1Data2 = 1;
            try
            {
                OrderRestatedAppendageSizeRule1Width = ((orderRestatedAppendageOptionalFieldLengthValue) switch { var OrderRestatedAppendageSizeRule1ArithmeticLeft0 => ((OrderRestatedAppendageSizeRule1Data2) switch { var OrderRestatedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderRestatedAppendageSizeRule1Width < Position.Minimum || OrderRestatedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            OrderRestatedAppendageSizeRule1End = index + (int)OrderRestatedAppendageSizeRule1Width;
            int orderRestatedAppendageEnd = OrderRestatedAppendageSizeRule1End;
            index = orderRestatedAppendageEnd;
        }
        if (index <= orderRestatedAppendageEntryStart || index > OrderRestatedAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureOrderModifiedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderModifiedOptionalValueBranchRule0Selected;
        bool OrderModifiedOptionalValueBranchRule1Selected;
        bool OrderModifiedOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderModifiedOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderModifiedOptionalValueBranchRule0Selected = (dependency0 == OrderModifiedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule0Selected)
            {
                
                {
                    const int orderModifiedOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (orderModifiedOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || orderModifiedOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderModifiedOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderModifiedOptionalValueBranchRule1Data2 = 25;
            try
            {
                OrderModifiedOptionalValueBranchRule1Selected = (dependency0 == OrderModifiedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule1Selected)
            {
                
                {
                    const int orderModifiedOptionalValueSharesLocatedMeasuredBytes = SizeSharesLocated;
                    if (orderModifiedOptionalValueSharesLocatedMeasuredBytes < Position.Minimum || orderModifiedOptionalValueSharesLocatedMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderModifiedOptionalValueSharesLocatedMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderModifiedOptionalValueBranchRule2Data2 = 26;
            try
            {
                OrderModifiedOptionalValueBranchRule2Selected = (dependency0 == OrderModifiedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule2Selected)
            {
                
                {
                    const int locateBrokerMeasuredBytes = SizeLocateBroker;
                    if (locateBrokerMeasuredBytes < Position.Minimum || locateBrokerMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += locateBrokerMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureOrderModifiedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int orderModifiedMessageAppendageLengthDependencyReadDistanceFromOrderModifiedMessageAppendageLength = 0;
        const int orderModifiedMessageAppendageLengthDependencyReadWidth = 2;
        long orderModifiedMessageAppendageLengthDependencyReadPosition = default;
        ushort orderModifiedMessageAppendageLengthValue = default;
        global::System.Int128 OrderModifiedAppendageCountRule0RawEnd;
        int OrderModifiedAppendageCountRule0End;
        const int orderModifiedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderModifiedAppendage = 0;
        const int orderModifiedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderModifiedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderModifiedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderModifiedAppendageSizeRule1Width;
        int OrderModifiedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderModifiedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderModifiedMessageTimestampMeasuredBytes < Position.Minimum || orderModifiedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderModifiedMessageUserRefNumMeasuredBytes < Position.Minimum || orderModifiedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageSideMeasuredBytes = SizeSide;
            if (orderModifiedMessageSideMeasuredBytes < Position.Minimum || orderModifiedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageSideMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderModifiedMessageQuantityMeasuredBytes < Position.Minimum || orderModifiedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                orderModifiedMessageAppendageLengthDependencyReadPosition = (long)index + orderModifiedMessageAppendageLengthDependencyReadDistanceFromOrderModifiedMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderModifiedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderModifiedMessageAppendageLengthDependencyReadPosition > limit || orderModifiedMessageAppendageLengthDependencyReadWidth > limit - orderModifiedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                orderModifiedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderModifiedMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int orderModifiedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (orderModifiedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderModifiedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderModifiedMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                OrderModifiedAppendageCountRule0RawEnd = ((orderModifiedMessageAppendageLengthValue) switch { var OrderModifiedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderModifiedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderModifiedAppendageCountRule0RawEnd < index || OrderModifiedAppendageCountRule0RawEnd > limit) return Position.Failure;
            OrderModifiedAppendageCountRule0End = (int)OrderModifiedAppendageCountRule0RawEnd;
            while (index < OrderModifiedAppendageCountRule0End)
            {
                int orderModifiedAppendageEntryStart = index;
                
                {
                    orderModifiedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderModifiedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderModifiedAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderModifiedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderModifiedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderModifiedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderModifiedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    orderModifiedAppendageOptionalFieldLengthValue = bytes[(int)orderModifiedAppendageOptionalFieldLengthDependencyReadPosition];
                    const long OrderModifiedAppendageSizeRule1Data2 = 1;
                    try
                    {
                        OrderModifiedAppendageSizeRule1Width = ((orderModifiedAppendageOptionalFieldLengthValue) switch { var OrderModifiedAppendageSizeRule1ArithmeticLeft0 => ((OrderModifiedAppendageSizeRule1Data2) switch { var OrderModifiedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (OrderModifiedAppendageSizeRule1Width < Position.Minimum || OrderModifiedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    OrderModifiedAppendageSizeRule1End = index + (int)OrderModifiedAppendageSizeRule1Width;
                    int orderModifiedAppendageEnd = OrderModifiedAppendageSizeRule1End;
                    index = orderModifiedAppendageEnd;
                }
                if (index <= orderModifiedAppendageEntryStart || index > OrderModifiedAppendageCountRule0End) return Position.Failure;
            }
            if (index != OrderModifiedAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepOrderModifiedMessageOrderModifiedAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int OrderModifiedAppendageCountRule0End)
    {
        const int orderModifiedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderModifiedAppendage = 0;
        const int orderModifiedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderModifiedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderModifiedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderModifiedAppendageSizeRule1Width;
        int OrderModifiedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < OrderModifiedAppendageCountRule0End)) return Position.Ended;
        int orderModifiedAppendageEntryStart = index;
        
        {
            orderModifiedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderModifiedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderModifiedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderModifiedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderModifiedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderModifiedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderModifiedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            orderModifiedAppendageOptionalFieldLengthValue = bytes[(int)orderModifiedAppendageOptionalFieldLengthDependencyReadPosition];
            const long OrderModifiedAppendageSizeRule1Data2 = 1;
            try
            {
                OrderModifiedAppendageSizeRule1Width = ((orderModifiedAppendageOptionalFieldLengthValue) switch { var OrderModifiedAppendageSizeRule1ArithmeticLeft0 => ((OrderModifiedAppendageSizeRule1Data2) switch { var OrderModifiedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderModifiedAppendageSizeRule1Width < Position.Minimum || OrderModifiedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            OrderModifiedAppendageSizeRule1End = index + (int)OrderModifiedAppendageSizeRule1Width;
            int orderModifiedAppendageEnd = OrderModifiedAppendageSizeRule1End;
            index = orderModifiedAppendageEnd;
        }
        if (index <= orderModifiedAppendageEntryStart || index > OrderModifiedAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureOrderPriorityUpdateOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderPriorityUpdateOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderPriorityUpdateOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderPriorityUpdateOptionalValueBranchRule0Selected = (dependency0 == OrderPriorityUpdateOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderPriorityUpdateOptionalValueBranchRule0Selected)
            {
                
                {
                    const int orderPriorityUpdateOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (orderPriorityUpdateOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || orderPriorityUpdateOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderPriorityUpdateOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureOrderPriorityUpdateMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int orderPriorityUpdateMessageAppendageLengthDependencyReadDistanceFromOrderPriorityUpdateMessageAppendageLength = 0;
        const int orderPriorityUpdateMessageAppendageLengthDependencyReadWidth = 2;
        long orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = default;
        ushort orderPriorityUpdateMessageAppendageLengthValue = default;
        global::System.Int128 OrderPriorityUpdateAppendageCountRule0RawEnd;
        int OrderPriorityUpdateAppendageCountRule0End;
        const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadDistanceFromOrderPriorityUpdateAppendage = 0;
        const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderPriorityUpdateAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderPriorityUpdateAppendageSizeRule1Width;
        int OrderPriorityUpdateAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderPriorityUpdateMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderPriorityUpdateMessageTimestampMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderPriorityUpdateMessageUserRefNumMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessagePriceMeasuredBytes = SizePrice;
            if (orderPriorityUpdateMessagePriceMeasuredBytes < Position.Minimum || orderPriorityUpdateMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessagePriceMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageDisplayMeasuredBytes = SizeDisplay;
            if (orderPriorityUpdateMessageDisplayMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageDisplayMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = (long)index + orderPriorityUpdateMessageAppendageLengthDependencyReadDistanceFromOrderPriorityUpdateMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderPriorityUpdateMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateMessageAppendageLengthDependencyReadPosition > limit || orderPriorityUpdateMessageAppendageLengthDependencyReadWidth > limit - orderPriorityUpdateMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                orderPriorityUpdateMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderPriorityUpdateMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int orderPriorityUpdateMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (orderPriorityUpdateMessageAppendageLengthMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderPriorityUpdateMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                OrderPriorityUpdateAppendageCountRule0RawEnd = ((orderPriorityUpdateMessageAppendageLengthValue) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderPriorityUpdateAppendageCountRule0RawEnd < index || OrderPriorityUpdateAppendageCountRule0RawEnd > limit) return Position.Failure;
            OrderPriorityUpdateAppendageCountRule0End = (int)OrderPriorityUpdateAppendageCountRule0RawEnd;
            while (index < OrderPriorityUpdateAppendageCountRule0End)
            {
                int orderPriorityUpdateAppendageEntryStart = index;
                
                {
                    orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadDistanceFromOrderPriorityUpdateAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition > limit || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth > limit - orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    orderPriorityUpdateAppendageOptionalFieldLengthValue = bytes[(int)orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition];
                    const long OrderPriorityUpdateAppendageSizeRule1Data2 = 1;
                    try
                    {
                        OrderPriorityUpdateAppendageSizeRule1Width = ((orderPriorityUpdateAppendageOptionalFieldLengthValue) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0 => ((OrderPriorityUpdateAppendageSizeRule1Data2) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (OrderPriorityUpdateAppendageSizeRule1Width < Position.Minimum || OrderPriorityUpdateAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    OrderPriorityUpdateAppendageSizeRule1End = index + (int)OrderPriorityUpdateAppendageSizeRule1Width;
                    int orderPriorityUpdateAppendageEnd = OrderPriorityUpdateAppendageSizeRule1End;
                    index = orderPriorityUpdateAppendageEnd;
                }
                if (index <= orderPriorityUpdateAppendageEntryStart || index > OrderPriorityUpdateAppendageCountRule0End) return Position.Failure;
            }
            if (index != OrderPriorityUpdateAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepOrderPriorityUpdateMessageOrderPriorityUpdateAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int OrderPriorityUpdateAppendageCountRule0End)
    {
        const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadDistanceFromOrderPriorityUpdateAppendage = 0;
        const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderPriorityUpdateAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderPriorityUpdateAppendageSizeRule1Width;
        int OrderPriorityUpdateAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < OrderPriorityUpdateAppendageCountRule0End)) return Position.Ended;
        int orderPriorityUpdateAppendageEntryStart = index;
        
        {
            orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadDistanceFromOrderPriorityUpdateAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition > limit || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth > limit - orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            orderPriorityUpdateAppendageOptionalFieldLengthValue = bytes[(int)orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition];
            const long OrderPriorityUpdateAppendageSizeRule1Data2 = 1;
            try
            {
                OrderPriorityUpdateAppendageSizeRule1Width = ((orderPriorityUpdateAppendageOptionalFieldLengthValue) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0 => ((OrderPriorityUpdateAppendageSizeRule1Data2) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderPriorityUpdateAppendageSizeRule1Width < Position.Minimum || OrderPriorityUpdateAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            OrderPriorityUpdateAppendageSizeRule1End = index + (int)OrderPriorityUpdateAppendageSizeRule1Width;
            int orderPriorityUpdateAppendageEnd = OrderPriorityUpdateAppendageSizeRule1End;
            index = orderPriorityUpdateAppendageEnd;
        }
        if (index <= orderPriorityUpdateAppendageEntryStart || index > OrderPriorityUpdateAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureCancelRejectOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelRejectOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelRejectOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelRejectOptionalValueBranchRule0Selected = (dependency0 == CancelRejectOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelRejectOptionalValueBranchRule0Selected)
            {
                
                {
                    const int cancelRejectOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (cancelRejectOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || cancelRejectOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelRejectOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureCancelRejectMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int cancelRejectMessageAppendageLengthDependencyReadDistanceFromCancelRejectMessageAppendageLength = 0;
        const int cancelRejectMessageAppendageLengthDependencyReadWidth = 2;
        long cancelRejectMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelRejectMessageAppendageLengthValue = default;
        global::System.Int128 CancelRejectAppendageCountRule0RawEnd;
        int CancelRejectAppendageCountRule0End;
        const int cancelRejectAppendageOptionalFieldLengthDependencyReadDistanceFromCancelRejectAppendage = 0;
        const int cancelRejectAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelRejectAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelRejectAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelRejectAppendageSizeRule1Width;
        int CancelRejectAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelRejectMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelRejectMessageTimestampMeasuredBytes < Position.Minimum || cancelRejectMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelRejectMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelRejectMessageUserRefNumMeasuredBytes < Position.Minimum || cancelRejectMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelRejectMessageAppendageLengthDependencyReadPosition = (long)index + cancelRejectMessageAppendageLengthDependencyReadDistanceFromCancelRejectMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelRejectMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelRejectMessageAppendageLengthDependencyReadPosition > limit || cancelRejectMessageAppendageLengthDependencyReadWidth > limit - cancelRejectMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelRejectMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelRejectMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelRejectMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelRejectMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelRejectMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelRejectMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                CancelRejectAppendageCountRule0RawEnd = ((cancelRejectMessageAppendageLengthValue) switch { var CancelRejectAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelRejectAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelRejectAppendageCountRule0RawEnd < index || CancelRejectAppendageCountRule0RawEnd > limit) return Position.Failure;
            CancelRejectAppendageCountRule0End = (int)CancelRejectAppendageCountRule0RawEnd;
            while (index < CancelRejectAppendageCountRule0End)
            {
                int cancelRejectAppendageEntryStart = index;
                
                {
                    cancelRejectAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelRejectAppendageOptionalFieldLengthDependencyReadDistanceFromCancelRejectAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelRejectAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelRejectAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelRejectAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelRejectAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    cancelRejectAppendageOptionalFieldLengthValue = bytes[(int)cancelRejectAppendageOptionalFieldLengthDependencyReadPosition];
                    const long CancelRejectAppendageSizeRule1Data2 = 1;
                    try
                    {
                        CancelRejectAppendageSizeRule1Width = ((cancelRejectAppendageOptionalFieldLengthValue) switch { var CancelRejectAppendageSizeRule1ArithmeticLeft0 => ((CancelRejectAppendageSizeRule1Data2) switch { var CancelRejectAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (CancelRejectAppendageSizeRule1Width < Position.Minimum || CancelRejectAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    CancelRejectAppendageSizeRule1End = index + (int)CancelRejectAppendageSizeRule1Width;
                    int cancelRejectAppendageEnd = CancelRejectAppendageSizeRule1End;
                    index = cancelRejectAppendageEnd;
                }
                if (index <= cancelRejectAppendageEntryStart || index > CancelRejectAppendageCountRule0End) return Position.Failure;
            }
            if (index != CancelRejectAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepCancelRejectMessageCancelRejectAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int CancelRejectAppendageCountRule0End)
    {
        const int cancelRejectAppendageOptionalFieldLengthDependencyReadDistanceFromCancelRejectAppendage = 0;
        const int cancelRejectAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelRejectAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelRejectAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelRejectAppendageSizeRule1Width;
        int CancelRejectAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < CancelRejectAppendageCountRule0End)) return Position.Ended;
        int cancelRejectAppendageEntryStart = index;
        
        {
            cancelRejectAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelRejectAppendageOptionalFieldLengthDependencyReadDistanceFromCancelRejectAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelRejectAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelRejectAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelRejectAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelRejectAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            cancelRejectAppendageOptionalFieldLengthValue = bytes[(int)cancelRejectAppendageOptionalFieldLengthDependencyReadPosition];
            const long CancelRejectAppendageSizeRule1Data2 = 1;
            try
            {
                CancelRejectAppendageSizeRule1Width = ((cancelRejectAppendageOptionalFieldLengthValue) switch { var CancelRejectAppendageSizeRule1ArithmeticLeft0 => ((CancelRejectAppendageSizeRule1Data2) switch { var CancelRejectAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelRejectAppendageSizeRule1Width < Position.Minimum || CancelRejectAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            CancelRejectAppendageSizeRule1End = index + (int)CancelRejectAppendageSizeRule1Width;
            int cancelRejectAppendageEnd = CancelRejectAppendageSizeRule1End;
            index = cancelRejectAppendageEnd;
        }
        if (index <= cancelRejectAppendageEntryStart || index > CancelRejectAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureCancelPendingOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelPendingOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelPendingOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelPendingOptionalValueBranchRule0Selected = (dependency0 == CancelPendingOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelPendingOptionalValueBranchRule0Selected)
            {
                
                {
                    const int cancelPendingOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (cancelPendingOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || cancelPendingOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelPendingOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureCancelPendingMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int cancelPendingMessageAppendageLengthDependencyReadDistanceFromCancelPendingMessageAppendageLength = 0;
        const int cancelPendingMessageAppendageLengthDependencyReadWidth = 2;
        long cancelPendingMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelPendingMessageAppendageLengthValue = default;
        global::System.Int128 CancelPendingAppendageCountRule0RawEnd;
        int CancelPendingAppendageCountRule0End;
        const int cancelPendingAppendageOptionalFieldLengthDependencyReadDistanceFromCancelPendingAppendage = 0;
        const int cancelPendingAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelPendingAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelPendingAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelPendingAppendageSizeRule1Width;
        int CancelPendingAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelPendingMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelPendingMessageTimestampMeasuredBytes < Position.Minimum || cancelPendingMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelPendingMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelPendingMessageUserRefNumMeasuredBytes < Position.Minimum || cancelPendingMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelPendingMessageAppendageLengthDependencyReadPosition = (long)index + cancelPendingMessageAppendageLengthDependencyReadDistanceFromCancelPendingMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelPendingMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelPendingMessageAppendageLengthDependencyReadPosition > limit || cancelPendingMessageAppendageLengthDependencyReadWidth > limit - cancelPendingMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelPendingMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelPendingMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelPendingMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelPendingMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelPendingMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelPendingMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                CancelPendingAppendageCountRule0RawEnd = ((cancelPendingMessageAppendageLengthValue) switch { var CancelPendingAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelPendingAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelPendingAppendageCountRule0RawEnd < index || CancelPendingAppendageCountRule0RawEnd > limit) return Position.Failure;
            CancelPendingAppendageCountRule0End = (int)CancelPendingAppendageCountRule0RawEnd;
            while (index < CancelPendingAppendageCountRule0End)
            {
                int cancelPendingAppendageEntryStart = index;
                
                {
                    cancelPendingAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelPendingAppendageOptionalFieldLengthDependencyReadDistanceFromCancelPendingAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelPendingAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelPendingAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelPendingAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelPendingAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    cancelPendingAppendageOptionalFieldLengthValue = bytes[(int)cancelPendingAppendageOptionalFieldLengthDependencyReadPosition];
                    const long CancelPendingAppendageSizeRule1Data2 = 1;
                    try
                    {
                        CancelPendingAppendageSizeRule1Width = ((cancelPendingAppendageOptionalFieldLengthValue) switch { var CancelPendingAppendageSizeRule1ArithmeticLeft0 => ((CancelPendingAppendageSizeRule1Data2) switch { var CancelPendingAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (CancelPendingAppendageSizeRule1Width < Position.Minimum || CancelPendingAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    CancelPendingAppendageSizeRule1End = index + (int)CancelPendingAppendageSizeRule1Width;
                    int cancelPendingAppendageEnd = CancelPendingAppendageSizeRule1End;
                    index = cancelPendingAppendageEnd;
                }
                if (index <= cancelPendingAppendageEntryStart || index > CancelPendingAppendageCountRule0End) return Position.Failure;
            }
            if (index != CancelPendingAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepCancelPendingMessageCancelPendingAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int CancelPendingAppendageCountRule0End)
    {
        const int cancelPendingAppendageOptionalFieldLengthDependencyReadDistanceFromCancelPendingAppendage = 0;
        const int cancelPendingAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelPendingAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelPendingAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelPendingAppendageSizeRule1Width;
        int CancelPendingAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < CancelPendingAppendageCountRule0End)) return Position.Ended;
        int cancelPendingAppendageEntryStart = index;
        
        {
            cancelPendingAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelPendingAppendageOptionalFieldLengthDependencyReadDistanceFromCancelPendingAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelPendingAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelPendingAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelPendingAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelPendingAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            cancelPendingAppendageOptionalFieldLengthValue = bytes[(int)cancelPendingAppendageOptionalFieldLengthDependencyReadPosition];
            const long CancelPendingAppendageSizeRule1Data2 = 1;
            try
            {
                CancelPendingAppendageSizeRule1Width = ((cancelPendingAppendageOptionalFieldLengthValue) switch { var CancelPendingAppendageSizeRule1ArithmeticLeft0 => ((CancelPendingAppendageSizeRule1Data2) switch { var CancelPendingAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelPendingAppendageSizeRule1Width < Position.Minimum || CancelPendingAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            CancelPendingAppendageSizeRule1End = index + (int)CancelPendingAppendageSizeRule1Width;
            int cancelPendingAppendageEnd = CancelPendingAppendageSizeRule1End;
            index = cancelPendingAppendageEnd;
        }
        if (index <= cancelPendingAppendageEntryStart || index > CancelPendingAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureRejectedOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool RejectedOrderOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte RejectedOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                RejectedOrderOptionalValueBranchRule0Selected = (dependency0 == RejectedOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (RejectedOrderOptionalValueBranchRule0Selected)
            {
                
                {
                    const int rejectedOrderOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (rejectedOrderOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || rejectedOrderOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += rejectedOrderOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureRejectedOrderMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int rejectedOrderMessageAppendageLengthDependencyReadDistanceFromRejectedOrderMessageAppendageLength = 0;
        const int rejectedOrderMessageAppendageLengthDependencyReadWidth = 2;
        long rejectedOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort rejectedOrderMessageAppendageLengthValue = default;
        global::System.Int128 RejectedOrderAppendageCountRule0RawEnd;
        int RejectedOrderAppendageCountRule0End;
        const int rejectedOrderAppendageOptionalFieldLengthDependencyReadDistanceFromRejectedOrderAppendage = 0;
        const int rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte rejectedOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 RejectedOrderAppendageSizeRule1Width;
        int RejectedOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rejectedOrderMessageTimestampMeasuredBytes = SizeTimestamp;
            if (rejectedOrderMessageTimestampMeasuredBytes < Position.Minimum || rejectedOrderMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageTimestampMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (rejectedOrderMessageUserRefNumMeasuredBytes < Position.Minimum || rejectedOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int rejectedOrderReasonMeasuredBytes = SizeRejectedOrderReason;
            if (rejectedOrderReasonMeasuredBytes < Position.Minimum || rejectedOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderReasonMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageClordidMeasuredBytes = SizeClordid;
            if (rejectedOrderMessageClordidMeasuredBytes < Position.Minimum || rejectedOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                rejectedOrderMessageAppendageLengthDependencyReadPosition = (long)index + rejectedOrderMessageAppendageLengthDependencyReadDistanceFromRejectedOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rejectedOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || rejectedOrderMessageAppendageLengthDependencyReadPosition > limit || rejectedOrderMessageAppendageLengthDependencyReadWidth > limit - rejectedOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                rejectedOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)rejectedOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int rejectedOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (rejectedOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || rejectedOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += rejectedOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                RejectedOrderAppendageCountRule0RawEnd = ((rejectedOrderMessageAppendageLengthValue) switch { var RejectedOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var RejectedOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (RejectedOrderAppendageCountRule0RawEnd < index || RejectedOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
            RejectedOrderAppendageCountRule0End = (int)RejectedOrderAppendageCountRule0RawEnd;
            while (index < RejectedOrderAppendageCountRule0End)
            {
                int rejectedOrderAppendageEntryStart = index;
                
                {
                    rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + rejectedOrderAppendageOptionalFieldLengthDependencyReadDistanceFromRejectedOrderAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    rejectedOrderAppendageOptionalFieldLengthValue = bytes[(int)rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition];
                    const long RejectedOrderAppendageSizeRule1Data2 = 1;
                    try
                    {
                        RejectedOrderAppendageSizeRule1Width = ((rejectedOrderAppendageOptionalFieldLengthValue) switch { var RejectedOrderAppendageSizeRule1ArithmeticLeft0 => ((RejectedOrderAppendageSizeRule1Data2) switch { var RejectedOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (RejectedOrderAppendageSizeRule1Width < Position.Minimum || RejectedOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    RejectedOrderAppendageSizeRule1End = index + (int)RejectedOrderAppendageSizeRule1Width;
                    int rejectedOrderAppendageEnd = RejectedOrderAppendageSizeRule1End;
                    index = rejectedOrderAppendageEnd;
                }
                if (index <= rejectedOrderAppendageEntryStart || index > RejectedOrderAppendageCountRule0End) return Position.Failure;
            }
            if (index != RejectedOrderAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepRejectedOrderMessageRejectedOrderAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int RejectedOrderAppendageCountRule0End)
    {
        const int rejectedOrderAppendageOptionalFieldLengthDependencyReadDistanceFromRejectedOrderAppendage = 0;
        const int rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte rejectedOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 RejectedOrderAppendageSizeRule1Width;
        int RejectedOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < RejectedOrderAppendageCountRule0End)) return Position.Ended;
        int rejectedOrderAppendageEntryStart = index;
        
        {
            rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + rejectedOrderAppendageOptionalFieldLengthDependencyReadDistanceFromRejectedOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            rejectedOrderAppendageOptionalFieldLengthValue = bytes[(int)rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition];
            const long RejectedOrderAppendageSizeRule1Data2 = 1;
            try
            {
                RejectedOrderAppendageSizeRule1Width = ((rejectedOrderAppendageOptionalFieldLengthValue) switch { var RejectedOrderAppendageSizeRule1ArithmeticLeft0 => ((RejectedOrderAppendageSizeRule1Data2) switch { var RejectedOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (RejectedOrderAppendageSizeRule1Width < Position.Minimum || RejectedOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            RejectedOrderAppendageSizeRule1End = index + (int)RejectedOrderAppendageSizeRule1Width;
            int rejectedOrderAppendageEnd = RejectedOrderAppendageSizeRule1End;
            index = rejectedOrderAppendageEnd;
        }
        if (index <= rejectedOrderAppendageEntryStart || index > RejectedOrderAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureBrokenTradeOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool BrokenTradeOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte BrokenTradeOptionalValueBranchRule0Data2 = 28;
            try
            {
                BrokenTradeOptionalValueBranchRule0Selected = (dependency0 == BrokenTradeOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (BrokenTradeOptionalValueBranchRule0Selected)
            {
                
                {
                    const int brokenTradeOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (brokenTradeOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || brokenTradeOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += brokenTradeOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureBrokenTradeMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int brokenTradeMessageAppendageLengthDependencyReadDistanceFromBrokenTradeMessageAppendageLength = 0;
        const int brokenTradeMessageAppendageLengthDependencyReadWidth = 2;
        long brokenTradeMessageAppendageLengthDependencyReadPosition = default;
        ushort brokenTradeMessageAppendageLengthValue = default;
        global::System.Int128 BrokenTradeAppendageCountRule0RawEnd;
        int BrokenTradeAppendageCountRule0End;
        const int brokenTradeAppendageOptionalFieldLengthDependencyReadDistanceFromBrokenTradeAppendage = 0;
        const int brokenTradeAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long brokenTradeAppendageOptionalFieldLengthDependencyReadPosition;
        byte brokenTradeAppendageOptionalFieldLengthValue;
        global::System.Int128 BrokenTradeAppendageSizeRule1Width;
        int BrokenTradeAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int brokenTradeMessageTimestampMeasuredBytes = SizeTimestamp;
            if (brokenTradeMessageTimestampMeasuredBytes < Position.Minimum || brokenTradeMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageTimestampMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (brokenTradeMessageUserRefNumMeasuredBytes < Position.Minimum || brokenTradeMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageMatchNumberMeasuredBytes = SizeMatchNumber;
            if (brokenTradeMessageMatchNumberMeasuredBytes < Position.Minimum || brokenTradeMessageMatchNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageMatchNumberMeasuredBytes;
        }
        
        {
            const int brokenTradeReasonMeasuredBytes = SizeBrokenTradeReason;
            if (brokenTradeReasonMeasuredBytes < Position.Minimum || brokenTradeReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeReasonMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageClordidMeasuredBytes = SizeClordid;
            if (brokenTradeMessageClordidMeasuredBytes < Position.Minimum || brokenTradeMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                brokenTradeMessageAppendageLengthDependencyReadPosition = (long)index + brokenTradeMessageAppendageLengthDependencyReadDistanceFromBrokenTradeMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || brokenTradeMessageAppendageLengthDependencyReadPosition < Position.Minimum || brokenTradeMessageAppendageLengthDependencyReadPosition > limit || brokenTradeMessageAppendageLengthDependencyReadWidth > limit - brokenTradeMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                brokenTradeMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)brokenTradeMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int brokenTradeMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (brokenTradeMessageAppendageLengthMeasuredBytes < Position.Minimum || brokenTradeMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += brokenTradeMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                BrokenTradeAppendageCountRule0RawEnd = ((brokenTradeMessageAppendageLengthValue) switch { var BrokenTradeAppendageCountRule0ArithmeticLeft0 => ((index) switch { var BrokenTradeAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (BrokenTradeAppendageCountRule0RawEnd < index || BrokenTradeAppendageCountRule0RawEnd > limit) return Position.Failure;
            BrokenTradeAppendageCountRule0End = (int)BrokenTradeAppendageCountRule0RawEnd;
            while (index < BrokenTradeAppendageCountRule0End)
            {
                int brokenTradeAppendageEntryStart = index;
                
                {
                    brokenTradeAppendageOptionalFieldLengthDependencyReadPosition = (long)index + brokenTradeAppendageOptionalFieldLengthDependencyReadDistanceFromBrokenTradeAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || brokenTradeAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || brokenTradeAppendageOptionalFieldLengthDependencyReadPosition > limit || brokenTradeAppendageOptionalFieldLengthDependencyReadWidth > limit - brokenTradeAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    brokenTradeAppendageOptionalFieldLengthValue = bytes[(int)brokenTradeAppendageOptionalFieldLengthDependencyReadPosition];
                    const long BrokenTradeAppendageSizeRule1Data2 = 1;
                    try
                    {
                        BrokenTradeAppendageSizeRule1Width = ((brokenTradeAppendageOptionalFieldLengthValue) switch { var BrokenTradeAppendageSizeRule1ArithmeticLeft0 => ((BrokenTradeAppendageSizeRule1Data2) switch { var BrokenTradeAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (BrokenTradeAppendageSizeRule1Width < Position.Minimum || BrokenTradeAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    BrokenTradeAppendageSizeRule1End = index + (int)BrokenTradeAppendageSizeRule1Width;
                    int brokenTradeAppendageEnd = BrokenTradeAppendageSizeRule1End;
                    index = brokenTradeAppendageEnd;
                }
                if (index <= brokenTradeAppendageEntryStart || index > BrokenTradeAppendageCountRule0End) return Position.Failure;
            }
            if (index != BrokenTradeAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepBrokenTradeMessageBrokenTradeAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int BrokenTradeAppendageCountRule0End)
    {
        const int brokenTradeAppendageOptionalFieldLengthDependencyReadDistanceFromBrokenTradeAppendage = 0;
        const int brokenTradeAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long brokenTradeAppendageOptionalFieldLengthDependencyReadPosition;
        byte brokenTradeAppendageOptionalFieldLengthValue;
        global::System.Int128 BrokenTradeAppendageSizeRule1Width;
        int BrokenTradeAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < BrokenTradeAppendageCountRule0End)) return Position.Ended;
        int brokenTradeAppendageEntryStart = index;
        
        {
            brokenTradeAppendageOptionalFieldLengthDependencyReadPosition = (long)index + brokenTradeAppendageOptionalFieldLengthDependencyReadDistanceFromBrokenTradeAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || brokenTradeAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || brokenTradeAppendageOptionalFieldLengthDependencyReadPosition > limit || brokenTradeAppendageOptionalFieldLengthDependencyReadWidth > limit - brokenTradeAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            brokenTradeAppendageOptionalFieldLengthValue = bytes[(int)brokenTradeAppendageOptionalFieldLengthDependencyReadPosition];
            const long BrokenTradeAppendageSizeRule1Data2 = 1;
            try
            {
                BrokenTradeAppendageSizeRule1Width = ((brokenTradeAppendageOptionalFieldLengthValue) switch { var BrokenTradeAppendageSizeRule1ArithmeticLeft0 => ((BrokenTradeAppendageSizeRule1Data2) switch { var BrokenTradeAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (BrokenTradeAppendageSizeRule1Width < Position.Minimum || BrokenTradeAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            BrokenTradeAppendageSizeRule1End = index + (int)BrokenTradeAppendageSizeRule1Width;
            int brokenTradeAppendageEnd = BrokenTradeAppendageSizeRule1End;
            index = brokenTradeAppendageEnd;
        }
        if (index <= brokenTradeAppendageEntryStart || index > BrokenTradeAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureOrderExecutedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderExecutedOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderExecutedOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderExecutedOptionalValueBranchRule0Selected = (dependency0 == OrderExecutedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderExecutedOptionalValueBranchRule0Selected)
            {
                
                {
                    const int orderExecutedOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (orderExecutedOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || orderExecutedOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderExecutedOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureOrderExecutedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int orderExecutedMessageAppendageLengthDependencyReadDistanceFromOrderExecutedAppendage = -2;
        const int orderExecutedMessageAppendageLengthDependencyReadWidth = 2;
        long orderExecutedMessageAppendageLengthDependencyReadPosition;
        ushort orderExecutedMessageAppendageLengthValue;
        global::System.Int128 OrderExecutedAppendageCountRule0RawEnd;
        int OrderExecutedAppendageCountRule0End;
        const int orderExecutedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderExecutedAppendage = 0;
        const int orderExecutedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderExecutedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderExecutedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderExecutedAppendageSizeRule1Width;
        int OrderExecutedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderExecutedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderExecutedMessageTimestampMeasuredBytes < Position.Minimum || orderExecutedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderExecutedMessageUserRefNumMeasuredBytes < Position.Minimum || orderExecutedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderExecutedMessageQuantityMeasuredBytes < Position.Minimum || orderExecutedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageQuantityMeasuredBytes;
        }
        
        {
            const int orderExecutedMessagePriceMeasuredBytes = SizePrice;
            if (orderExecutedMessagePriceMeasuredBytes < Position.Minimum || orderExecutedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessagePriceMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageLiquidityFlagMeasuredBytes = SizeLiquidityFlag;
            if (orderExecutedMessageLiquidityFlagMeasuredBytes < Position.Minimum || orderExecutedMessageLiquidityFlagMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageLiquidityFlagMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageMatchNumberMeasuredBytes = SizeMatchNumber;
            if (orderExecutedMessageMatchNumberMeasuredBytes < Position.Minimum || orderExecutedMessageMatchNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageMatchNumberMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderExecutedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderExecutedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageAppendageLengthMeasuredBytes;
        }
        
        {
            orderExecutedMessageAppendageLengthDependencyReadPosition = (long)index + orderExecutedMessageAppendageLengthDependencyReadDistanceFromOrderExecutedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderExecutedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderExecutedMessageAppendageLengthDependencyReadPosition > limit || orderExecutedMessageAppendageLengthDependencyReadWidth > limit - orderExecutedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            orderExecutedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderExecutedMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                OrderExecutedAppendageCountRule0RawEnd = ((orderExecutedMessageAppendageLengthValue) switch { var OrderExecutedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderExecutedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderExecutedAppendageCountRule0RawEnd < index || OrderExecutedAppendageCountRule0RawEnd > limit) return Position.Failure;
            OrderExecutedAppendageCountRule0End = (int)OrderExecutedAppendageCountRule0RawEnd;
            while (index < OrderExecutedAppendageCountRule0End)
            {
                int orderExecutedAppendageEntryStart = index;
                
                {
                    orderExecutedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderExecutedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderExecutedAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderExecutedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderExecutedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderExecutedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderExecutedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    orderExecutedAppendageOptionalFieldLengthValue = bytes[(int)orderExecutedAppendageOptionalFieldLengthDependencyReadPosition];
                    const long OrderExecutedAppendageSizeRule1Data2 = 1;
                    try
                    {
                        OrderExecutedAppendageSizeRule1Width = ((orderExecutedAppendageOptionalFieldLengthValue) switch { var OrderExecutedAppendageSizeRule1ArithmeticLeft0 => ((OrderExecutedAppendageSizeRule1Data2) switch { var OrderExecutedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (OrderExecutedAppendageSizeRule1Width < Position.Minimum || OrderExecutedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    OrderExecutedAppendageSizeRule1End = index + (int)OrderExecutedAppendageSizeRule1Width;
                    int orderExecutedAppendageEnd = OrderExecutedAppendageSizeRule1End;
                    index = orderExecutedAppendageEnd;
                }
                if (index <= orderExecutedAppendageEntryStart || index > OrderExecutedAppendageCountRule0End) return Position.Failure;
            }
            if (index != OrderExecutedAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepOrderExecutedMessageOrderExecutedAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int OrderExecutedAppendageCountRule0End)
    {
        const int orderExecutedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderExecutedAppendage = 0;
        const int orderExecutedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderExecutedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderExecutedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderExecutedAppendageSizeRule1Width;
        int OrderExecutedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < OrderExecutedAppendageCountRule0End)) return Position.Ended;
        int orderExecutedAppendageEntryStart = index;
        
        {
            orderExecutedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderExecutedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderExecutedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderExecutedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderExecutedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderExecutedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderExecutedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            orderExecutedAppendageOptionalFieldLengthValue = bytes[(int)orderExecutedAppendageOptionalFieldLengthDependencyReadPosition];
            const long OrderExecutedAppendageSizeRule1Data2 = 1;
            try
            {
                OrderExecutedAppendageSizeRule1Width = ((orderExecutedAppendageOptionalFieldLengthValue) switch { var OrderExecutedAppendageSizeRule1ArithmeticLeft0 => ((OrderExecutedAppendageSizeRule1Data2) switch { var OrderExecutedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderExecutedAppendageSizeRule1Width < Position.Minimum || OrderExecutedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            OrderExecutedAppendageSizeRule1End = index + (int)OrderExecutedAppendageSizeRule1Width;
            int orderExecutedAppendageEnd = OrderExecutedAppendageSizeRule1End;
            index = orderExecutedAppendageEnd;
        }
        if (index <= orderExecutedAppendageEntryStart || index > OrderExecutedAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureAiqCanceledOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AiqCanceledOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AiqCanceledOptionalValueBranchRule0Data2 = 28;
            try
            {
                AiqCanceledOptionalValueBranchRule0Selected = (dependency0 == AiqCanceledOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AiqCanceledOptionalValueBranchRule0Selected)
            {
                
                {
                    const int aiqCanceledOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (aiqCanceledOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || aiqCanceledOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += aiqCanceledOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureAiqCanceledMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int aiqCanceledMessageAppendageLengthDependencyReadDistanceFromAiqCanceledMessageAppendageLength = 0;
        const int aiqCanceledMessageAppendageLengthDependencyReadWidth = 2;
        long aiqCanceledMessageAppendageLengthDependencyReadPosition = default;
        ushort aiqCanceledMessageAppendageLengthValue = default;
        global::System.Int128 AiqCanceledAppendageCountRule0RawEnd;
        int AiqCanceledAppendageCountRule0End;
        const int aiqCanceledAppendageOptionalFieldLengthDependencyReadDistanceFromAiqCanceledAppendage = 0;
        const int aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition;
        byte aiqCanceledAppendageOptionalFieldLengthValue;
        global::System.Int128 AiqCanceledAppendageSizeRule1Width;
        int AiqCanceledAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int aiqCanceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (aiqCanceledMessageTimestampMeasuredBytes < Position.Minimum || aiqCanceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (aiqCanceledMessageUserRefNumMeasuredBytes < Position.Minimum || aiqCanceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int decrementSharesMeasuredBytes = SizeDecrementShares;
            if (decrementSharesMeasuredBytes < Position.Minimum || decrementSharesMeasuredBytes > (long)limit - index) return Position.Failure;
            index += decrementSharesMeasuredBytes;
        }
        
        {
            const int orderCancelReasonMeasuredBytes = SizeOrderCancelReason;
            if (orderCancelReasonMeasuredBytes < Position.Minimum || orderCancelReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderCancelReasonMeasuredBytes;
        }
        
        {
            const int quantityPreventedFromTradingMeasuredBytes = SizeQuantityPreventedFromTrading;
            if (quantityPreventedFromTradingMeasuredBytes < Position.Minimum || quantityPreventedFromTradingMeasuredBytes > (long)limit - index) return Position.Failure;
            index += quantityPreventedFromTradingMeasuredBytes;
        }
        
        {
            const int executionPriceMeasuredBytes = SizeExecutionPrice;
            if (executionPriceMeasuredBytes < Position.Minimum || executionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += executionPriceMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageLiquidityFlagMeasuredBytes = SizeLiquidityFlag;
            if (aiqCanceledMessageLiquidityFlagMeasuredBytes < Position.Minimum || aiqCanceledMessageLiquidityFlagMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageLiquidityFlagMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageAiqStrategyMeasuredBytes = SizeAiqStrategy;
            if (aiqCanceledMessageAiqStrategyMeasuredBytes < Position.Minimum || aiqCanceledMessageAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageAiqStrategyMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                aiqCanceledMessageAppendageLengthDependencyReadPosition = (long)index + aiqCanceledMessageAppendageLengthDependencyReadDistanceFromAiqCanceledMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || aiqCanceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || aiqCanceledMessageAppendageLengthDependencyReadPosition > limit || aiqCanceledMessageAppendageLengthDependencyReadWidth > limit - aiqCanceledMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                aiqCanceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)aiqCanceledMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int aiqCanceledMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (aiqCanceledMessageAppendageLengthMeasuredBytes < Position.Minimum || aiqCanceledMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += aiqCanceledMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                AiqCanceledAppendageCountRule0RawEnd = ((aiqCanceledMessageAppendageLengthValue) switch { var AiqCanceledAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AiqCanceledAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AiqCanceledAppendageCountRule0RawEnd < index || AiqCanceledAppendageCountRule0RawEnd > limit) return Position.Failure;
            AiqCanceledAppendageCountRule0End = (int)AiqCanceledAppendageCountRule0RawEnd;
            while (index < AiqCanceledAppendageCountRule0End)
            {
                int aiqCanceledAppendageEntryStart = index;
                
                {
                    aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition = (long)index + aiqCanceledAppendageOptionalFieldLengthDependencyReadDistanceFromAiqCanceledAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition > limit || aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth > limit - aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    aiqCanceledAppendageOptionalFieldLengthValue = bytes[(int)aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition];
                    const long AiqCanceledAppendageSizeRule1Data2 = 1;
                    try
                    {
                        AiqCanceledAppendageSizeRule1Width = ((aiqCanceledAppendageOptionalFieldLengthValue) switch { var AiqCanceledAppendageSizeRule1ArithmeticLeft0 => ((AiqCanceledAppendageSizeRule1Data2) switch { var AiqCanceledAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (AiqCanceledAppendageSizeRule1Width < Position.Minimum || AiqCanceledAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    AiqCanceledAppendageSizeRule1End = index + (int)AiqCanceledAppendageSizeRule1Width;
                    int aiqCanceledAppendageEnd = AiqCanceledAppendageSizeRule1End;
                    index = aiqCanceledAppendageEnd;
                }
                if (index <= aiqCanceledAppendageEntryStart || index > AiqCanceledAppendageCountRule0End) return Position.Failure;
            }
            if (index != AiqCanceledAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepAiqCanceledMessageAiqCanceledAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int AiqCanceledAppendageCountRule0End)
    {
        const int aiqCanceledAppendageOptionalFieldLengthDependencyReadDistanceFromAiqCanceledAppendage = 0;
        const int aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition;
        byte aiqCanceledAppendageOptionalFieldLengthValue;
        global::System.Int128 AiqCanceledAppendageSizeRule1Width;
        int AiqCanceledAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < AiqCanceledAppendageCountRule0End)) return Position.Ended;
        int aiqCanceledAppendageEntryStart = index;
        
        {
            aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition = (long)index + aiqCanceledAppendageOptionalFieldLengthDependencyReadDistanceFromAiqCanceledAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition > limit || aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth > limit - aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            aiqCanceledAppendageOptionalFieldLengthValue = bytes[(int)aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition];
            const long AiqCanceledAppendageSizeRule1Data2 = 1;
            try
            {
                AiqCanceledAppendageSizeRule1Width = ((aiqCanceledAppendageOptionalFieldLengthValue) switch { var AiqCanceledAppendageSizeRule1ArithmeticLeft0 => ((AiqCanceledAppendageSizeRule1Data2) switch { var AiqCanceledAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AiqCanceledAppendageSizeRule1Width < Position.Minimum || AiqCanceledAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            AiqCanceledAppendageSizeRule1End = index + (int)AiqCanceledAppendageSizeRule1Width;
            int aiqCanceledAppendageEnd = AiqCanceledAppendageSizeRule1End;
            index = aiqCanceledAppendageEnd;
        }
        if (index <= aiqCanceledAppendageEntryStart || index > AiqCanceledAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureCanceledMessageOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CanceledMessageOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CanceledMessageOptionalValueBranchRule0Data2 = 28;
            try
            {
                CanceledMessageOptionalValueBranchRule0Selected = (dependency0 == CanceledMessageOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CanceledMessageOptionalValueBranchRule0Selected)
            {
                
                {
                    const int canceledMessageOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (canceledMessageOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || canceledMessageOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += canceledMessageOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureCanceledMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int canceledMessageAppendageLengthDependencyReadDistanceFromCanceledMessageAppendageLength = 0;
        const int canceledMessageAppendageLengthDependencyReadWidth = 2;
        long canceledMessageAppendageLengthDependencyReadPosition = default;
        ushort canceledMessageAppendageLengthValue = default;
        global::System.Int128 CanceledMessageAppendageCountRule0RawEnd;
        int CanceledMessageAppendageCountRule0End;
        const int canceledMessageAppendageOptionalFieldLengthDependencyReadDistanceFromCanceledMessageAppendage = 0;
        const int canceledMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long canceledMessageAppendageOptionalFieldLengthDependencyReadPosition;
        byte canceledMessageAppendageOptionalFieldLengthValue;
        global::System.Int128 CanceledMessageAppendageSizeRule1Width;
        int CanceledMessageAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int canceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (canceledMessageTimestampMeasuredBytes < Position.Minimum || canceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int canceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (canceledMessageUserRefNumMeasuredBytes < Position.Minimum || canceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int canceledMessageQuantityMeasuredBytes = SizeQuantity;
            if (canceledMessageQuantityMeasuredBytes < Position.Minimum || canceledMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageQuantityMeasuredBytes;
        }
        
        {
            const int cancelOrderReasonMeasuredBytes = SizeCancelOrderReason;
            if (cancelOrderReasonMeasuredBytes < Position.Minimum || cancelOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderReasonMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                canceledMessageAppendageLengthDependencyReadPosition = (long)index + canceledMessageAppendageLengthDependencyReadDistanceFromCanceledMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || canceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageLengthDependencyReadPosition > limit || canceledMessageAppendageLengthDependencyReadWidth > limit - canceledMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                canceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)canceledMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int canceledMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (canceledMessageAppendageLengthMeasuredBytes < Position.Minimum || canceledMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += canceledMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                CanceledMessageAppendageCountRule0RawEnd = ((canceledMessageAppendageLengthValue) switch { var CanceledMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CanceledMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CanceledMessageAppendageCountRule0RawEnd < index || CanceledMessageAppendageCountRule0RawEnd > limit) return Position.Failure;
            CanceledMessageAppendageCountRule0End = (int)CanceledMessageAppendageCountRule0RawEnd;
            while (index < CanceledMessageAppendageCountRule0End)
            {
                int canceledMessageAppendageEntryStart = index;
                
                {
                    canceledMessageAppendageOptionalFieldLengthDependencyReadPosition = (long)index + canceledMessageAppendageOptionalFieldLengthDependencyReadDistanceFromCanceledMessageAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || canceledMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageOptionalFieldLengthDependencyReadPosition > limit || canceledMessageAppendageOptionalFieldLengthDependencyReadWidth > limit - canceledMessageAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    canceledMessageAppendageOptionalFieldLengthValue = bytes[(int)canceledMessageAppendageOptionalFieldLengthDependencyReadPosition];
                    const long CanceledMessageAppendageSizeRule1Data2 = 1;
                    try
                    {
                        CanceledMessageAppendageSizeRule1Width = ((canceledMessageAppendageOptionalFieldLengthValue) switch { var CanceledMessageAppendageSizeRule1ArithmeticLeft0 => ((CanceledMessageAppendageSizeRule1Data2) switch { var CanceledMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (CanceledMessageAppendageSizeRule1Width < Position.Minimum || CanceledMessageAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    CanceledMessageAppendageSizeRule1End = index + (int)CanceledMessageAppendageSizeRule1Width;
                    int canceledMessageAppendageEnd = CanceledMessageAppendageSizeRule1End;
                    index = canceledMessageAppendageEnd;
                }
                if (index <= canceledMessageAppendageEntryStart || index > CanceledMessageAppendageCountRule0End) return Position.Failure;
            }
            if (index != CanceledMessageAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepCanceledMessageCanceledMessageAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int CanceledMessageAppendageCountRule0End)
    {
        const int canceledMessageAppendageOptionalFieldLengthDependencyReadDistanceFromCanceledMessageAppendage = 0;
        const int canceledMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long canceledMessageAppendageOptionalFieldLengthDependencyReadPosition;
        byte canceledMessageAppendageOptionalFieldLengthValue;
        global::System.Int128 CanceledMessageAppendageSizeRule1Width;
        int CanceledMessageAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < CanceledMessageAppendageCountRule0End)) return Position.Ended;
        int canceledMessageAppendageEntryStart = index;
        
        {
            canceledMessageAppendageOptionalFieldLengthDependencyReadPosition = (long)index + canceledMessageAppendageOptionalFieldLengthDependencyReadDistanceFromCanceledMessageAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || canceledMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageOptionalFieldLengthDependencyReadPosition > limit || canceledMessageAppendageOptionalFieldLengthDependencyReadWidth > limit - canceledMessageAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            canceledMessageAppendageOptionalFieldLengthValue = bytes[(int)canceledMessageAppendageOptionalFieldLengthDependencyReadPosition];
            const long CanceledMessageAppendageSizeRule1Data2 = 1;
            try
            {
                CanceledMessageAppendageSizeRule1Width = ((canceledMessageAppendageOptionalFieldLengthValue) switch { var CanceledMessageAppendageSizeRule1ArithmeticLeft0 => ((CanceledMessageAppendageSizeRule1Data2) switch { var CanceledMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CanceledMessageAppendageSizeRule1Width < Position.Minimum || CanceledMessageAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            CanceledMessageAppendageSizeRule1End = index + (int)CanceledMessageAppendageSizeRule1Width;
            int canceledMessageAppendageEnd = CanceledMessageAppendageSizeRule1End;
            index = canceledMessageAppendageEnd;
        }
        if (index <= canceledMessageAppendageEntryStart || index > CanceledMessageAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureReplacedMessageOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ReplacedMessageOptionalValueBranchRule0Selected;
        bool ReplacedMessageOptionalValueBranchRule1Selected;
        bool ReplacedMessageOptionalValueBranchRule2Selected;
        bool ReplacedMessageOptionalValueBranchRule3Selected;
        bool ReplacedMessageOptionalValueBranchRule4Selected;
        bool ReplacedMessageOptionalValueBranchRule5Selected;
        bool ReplacedMessageOptionalValueBranchRule6Selected;
        bool ReplacedMessageOptionalValueBranchRule7Selected;
        bool ReplacedMessageOptionalValueBranchRule8Selected;
        bool ReplacedMessageOptionalValueBranchRule9Selected;
        bool ReplacedMessageOptionalValueBranchRule10Selected;
        bool ReplacedMessageOptionalValueBranchRule11Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ReplacedMessageOptionalValueBranchRule0Data2 = 2;
            try
            {
                ReplacedMessageOptionalValueBranchRule0Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule0Selected)
            {
                
                {
                    const int replacedMessageOptionalValueFirmMeasuredBytes = SizeFirm;
                    if (replacedMessageOptionalValueFirmMeasuredBytes < Position.Minimum || replacedMessageOptionalValueFirmMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueFirmMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule1Data2 = 3;
            try
            {
                ReplacedMessageOptionalValueBranchRule1Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule1Selected)
            {
                
                {
                    const int replacedMessageOptionalValueMinQtyMeasuredBytes = SizeMinQty;
                    if (replacedMessageOptionalValueMinQtyMeasuredBytes < Position.Minimum || replacedMessageOptionalValueMinQtyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueMinQtyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule2Data2 = 5;
            try
            {
                ReplacedMessageOptionalValueBranchRule2Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule2Selected)
            {
                
                {
                    const int replacedMessageOptionalValueMaxFloorMeasuredBytes = SizeMaxFloor;
                    if (replacedMessageOptionalValueMaxFloorMeasuredBytes < Position.Minimum || replacedMessageOptionalValueMaxFloorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueMaxFloorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule3Data2 = 6;
            try
            {
                ReplacedMessageOptionalValueBranchRule3Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule3Selected)
            {
                
                {
                    const int replacedMessageOptionalValuePriceTypeMeasuredBytes = SizePriceType;
                    if (replacedMessageOptionalValuePriceTypeMeasuredBytes < Position.Minimum || replacedMessageOptionalValuePriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValuePriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule4Data2 = 12;
            try
            {
                ReplacedMessageOptionalValueBranchRule4Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule4Selected)
            {
                
                {
                    const int replacedMessageOptionalValuePostOnlyMeasuredBytes = SizePostOnly;
                    if (replacedMessageOptionalValuePostOnlyMeasuredBytes < Position.Minimum || replacedMessageOptionalValuePostOnlyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValuePostOnlyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule5Data2 = 15;
            try
            {
                ReplacedMessageOptionalValueBranchRule5Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule5Selected)
            {
                
                {
                    const int replacedMessageOptionalValueExpireTimeMeasuredBytes = SizeExpireTime;
                    if (replacedMessageOptionalValueExpireTimeMeasuredBytes < Position.Minimum || replacedMessageOptionalValueExpireTimeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueExpireTimeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule6Data2 = 16;
            try
            {
                ReplacedMessageOptionalValueBranchRule6Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule6Selected)
            {
                
                {
                    const int replacedMessageOptionalValueTradeNowMeasuredBytes = SizeTradeNow;
                    if (replacedMessageOptionalValueTradeNowMeasuredBytes < Position.Minimum || replacedMessageOptionalValueTradeNowMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueTradeNowMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule7Data2 = 17;
            try
            {
                ReplacedMessageOptionalValueBranchRule7Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule7Selected)
            {
                
                {
                    const int replacedMessageOptionalValueHandleInstMeasuredBytes = SizeHandleInst;
                    if (replacedMessageOptionalValueHandleInstMeasuredBytes < Position.Minimum || replacedMessageOptionalValueHandleInstMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueHandleInstMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule8Data2 = 18;
            try
            {
                ReplacedMessageOptionalValueBranchRule8Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule8Selected)
            {
                
                {
                    const int replacedMessageOptionalValueBboWeightIndicatorMeasuredBytes = SizeBboWeightIndicator;
                    if (replacedMessageOptionalValueBboWeightIndicatorMeasuredBytes < Position.Minimum || replacedMessageOptionalValueBboWeightIndicatorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueBboWeightIndicatorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule9Data2 = 28;
            try
            {
                ReplacedMessageOptionalValueBranchRule9Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule9Selected)
            {
                
                {
                    const int replacedMessageOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (replacedMessageOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || replacedMessageOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule10Data2 = 29;
            try
            {
                ReplacedMessageOptionalValueBranchRule10Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule10Selected)
            {
                
                {
                    const int replacedMessageOptionalValueAiqStrategyMeasuredBytes = SizeAiqStrategy;
                    if (replacedMessageOptionalValueAiqStrategyMeasuredBytes < Position.Minimum || replacedMessageOptionalValueAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueAiqStrategyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule11Data2 = 30;
            try
            {
                ReplacedMessageOptionalValueBranchRule11Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule11Selected)
            {
                
                {
                    const int replacedMessageOptionalValueAiqGroupIdMeasuredBytes = SizeAiqGroupId;
                    if (replacedMessageOptionalValueAiqGroupIdMeasuredBytes < Position.Minimum || replacedMessageOptionalValueAiqGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replacedMessageOptionalValueAiqGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureReplacedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int replacedMessageAppendageLengthDependencyReadDistanceFromReplacedMessageAppendage = -2;
        const int replacedMessageAppendageLengthDependencyReadWidth = 2;
        long replacedMessageAppendageLengthDependencyReadPosition;
        ushort replacedMessageAppendageLengthValue;
        global::System.Int128 ReplacedMessageAppendageCountRule0RawEnd;
        int ReplacedMessageAppendageCountRule0End;
        const int replacedMessageAppendageOptionalFieldLengthDependencyReadDistanceFromReplacedMessageAppendage = 0;
        const int replacedMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long replacedMessageAppendageOptionalFieldLengthDependencyReadPosition;
        byte replacedMessageAppendageOptionalFieldLengthValue;
        global::System.Int128 ReplacedMessageAppendageSizeRule1Width;
        int ReplacedMessageAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int replacedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (replacedMessageTimestampMeasuredBytes < Position.Minimum || replacedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageTimestampMeasuredBytes;
        }
        
        {
            const int origUserRefNumMeasuredBytes = SizeOrigUserRefNum;
            if (origUserRefNumMeasuredBytes < Position.Minimum || origUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += origUserRefNumMeasuredBytes;
        }
        
        {
            const int replacedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (replacedMessageUserRefNumMeasuredBytes < Position.Minimum || replacedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int replacedMessageSideMeasuredBytes = SizeSide;
            if (replacedMessageSideMeasuredBytes < Position.Minimum || replacedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageSideMeasuredBytes;
        }
        
        {
            const int replacedMessageQuantityMeasuredBytes = SizeQuantity;
            if (replacedMessageQuantityMeasuredBytes < Position.Minimum || replacedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageQuantityMeasuredBytes;
        }
        
        {
            const int replacedMessageSymbolMeasuredBytes = SizeSymbol;
            if (replacedMessageSymbolMeasuredBytes < Position.Minimum || replacedMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageSymbolMeasuredBytes;
        }
        
        {
            const int replacedMessagePriceMeasuredBytes = SizePrice;
            if (replacedMessagePriceMeasuredBytes < Position.Minimum || replacedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessagePriceMeasuredBytes;
        }
        
        {
            const int replacedMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (replacedMessageTimeInForceMeasuredBytes < Position.Minimum || replacedMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int replacedMessageDisplayMeasuredBytes = SizeDisplay;
            if (replacedMessageDisplayMeasuredBytes < Position.Minimum || replacedMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageDisplayMeasuredBytes;
        }
        
        {
            const int replacedMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (replacedMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || replacedMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            const int replacedMessageCapacityMeasuredBytes = SizeCapacity;
            if (replacedMessageCapacityMeasuredBytes < Position.Minimum || replacedMessageCapacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageCapacityMeasuredBytes;
        }
        
        {
            const int replacedMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (replacedMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || replacedMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int replacedMessageCrossTypeMeasuredBytes = SizeCrossType;
            if (replacedMessageCrossTypeMeasuredBytes < Position.Minimum || replacedMessageCrossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageCrossTypeMeasuredBytes;
        }
        
        {
            const int replacedMessageOrderStateMeasuredBytes = SizeOrderState;
            if (replacedMessageOrderStateMeasuredBytes < Position.Minimum || replacedMessageOrderStateMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageOrderStateMeasuredBytes;
        }
        
        {
            const int replacedMessageClordidMeasuredBytes = SizeClordid;
            if (replacedMessageClordidMeasuredBytes < Position.Minimum || replacedMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageClordidMeasuredBytes;
        }
        
        {
            const int replacedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (replacedMessageAppendageLengthMeasuredBytes < Position.Minimum || replacedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageAppendageLengthMeasuredBytes;
        }
        
        {
            replacedMessageAppendageLengthDependencyReadPosition = (long)index + replacedMessageAppendageLengthDependencyReadDistanceFromReplacedMessageAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replacedMessageAppendageLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageLengthDependencyReadPosition > limit || replacedMessageAppendageLengthDependencyReadWidth > limit - replacedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            replacedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replacedMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                ReplacedMessageAppendageCountRule0RawEnd = ((replacedMessageAppendageLengthValue) switch { var ReplacedMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplacedMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ReplacedMessageAppendageCountRule0RawEnd < index || ReplacedMessageAppendageCountRule0RawEnd > limit) return Position.Failure;
            ReplacedMessageAppendageCountRule0End = (int)ReplacedMessageAppendageCountRule0RawEnd;
            while (index < ReplacedMessageAppendageCountRule0End)
            {
                int replacedMessageAppendageEntryStart = index;
                
                {
                    replacedMessageAppendageOptionalFieldLengthDependencyReadPosition = (long)index + replacedMessageAppendageOptionalFieldLengthDependencyReadDistanceFromReplacedMessageAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replacedMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageOptionalFieldLengthDependencyReadPosition > limit || replacedMessageAppendageOptionalFieldLengthDependencyReadWidth > limit - replacedMessageAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    replacedMessageAppendageOptionalFieldLengthValue = bytes[(int)replacedMessageAppendageOptionalFieldLengthDependencyReadPosition];
                    const long ReplacedMessageAppendageSizeRule1Data2 = 1;
                    try
                    {
                        ReplacedMessageAppendageSizeRule1Width = ((replacedMessageAppendageOptionalFieldLengthValue) switch { var ReplacedMessageAppendageSizeRule1ArithmeticLeft0 => ((ReplacedMessageAppendageSizeRule1Data2) switch { var ReplacedMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (ReplacedMessageAppendageSizeRule1Width < Position.Minimum || ReplacedMessageAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    ReplacedMessageAppendageSizeRule1End = index + (int)ReplacedMessageAppendageSizeRule1Width;
                    int replacedMessageAppendageEnd = ReplacedMessageAppendageSizeRule1End;
                    index = replacedMessageAppendageEnd;
                }
                if (index <= replacedMessageAppendageEntryStart || index > ReplacedMessageAppendageCountRule0End) return Position.Failure;
            }
            if (index != ReplacedMessageAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepReplacedMessageReplacedMessageAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int ReplacedMessageAppendageCountRule0End)
    {
        const int replacedMessageAppendageOptionalFieldLengthDependencyReadDistanceFromReplacedMessageAppendage = 0;
        const int replacedMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long replacedMessageAppendageOptionalFieldLengthDependencyReadPosition;
        byte replacedMessageAppendageOptionalFieldLengthValue;
        global::System.Int128 ReplacedMessageAppendageSizeRule1Width;
        int ReplacedMessageAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ReplacedMessageAppendageCountRule0End)) return Position.Ended;
        int replacedMessageAppendageEntryStart = index;
        
        {
            replacedMessageAppendageOptionalFieldLengthDependencyReadPosition = (long)index + replacedMessageAppendageOptionalFieldLengthDependencyReadDistanceFromReplacedMessageAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replacedMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageOptionalFieldLengthDependencyReadPosition > limit || replacedMessageAppendageOptionalFieldLengthDependencyReadWidth > limit - replacedMessageAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            replacedMessageAppendageOptionalFieldLengthValue = bytes[(int)replacedMessageAppendageOptionalFieldLengthDependencyReadPosition];
            const long ReplacedMessageAppendageSizeRule1Data2 = 1;
            try
            {
                ReplacedMessageAppendageSizeRule1Width = ((replacedMessageAppendageOptionalFieldLengthValue) switch { var ReplacedMessageAppendageSizeRule1ArithmeticLeft0 => ((ReplacedMessageAppendageSizeRule1Data2) switch { var ReplacedMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ReplacedMessageAppendageSizeRule1Width < Position.Minimum || ReplacedMessageAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            ReplacedMessageAppendageSizeRule1End = index + (int)ReplacedMessageAppendageSizeRule1Width;
            int replacedMessageAppendageEnd = ReplacedMessageAppendageSizeRule1End;
            index = replacedMessageAppendageEnd;
        }
        if (index <= replacedMessageAppendageEntryStart || index > ReplacedMessageAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureOrderAcceptedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderAcceptedOptionalValueBranchRule0Selected;
        bool OrderAcceptedOptionalValueBranchRule1Selected;
        bool OrderAcceptedOptionalValueBranchRule2Selected;
        bool OrderAcceptedOptionalValueBranchRule3Selected;
        bool OrderAcceptedOptionalValueBranchRule4Selected;
        bool OrderAcceptedOptionalValueBranchRule5Selected;
        bool OrderAcceptedOptionalValueBranchRule6Selected;
        bool OrderAcceptedOptionalValueBranchRule7Selected;
        bool OrderAcceptedOptionalValueBranchRule8Selected;
        bool OrderAcceptedOptionalValueBranchRule9Selected;
        bool OrderAcceptedOptionalValueBranchRule10Selected;
        bool OrderAcceptedOptionalValueBranchRule11Selected;
        bool OrderAcceptedOptionalValueBranchRule12Selected;
        bool OrderAcceptedOptionalValueBranchRule13Selected;
        bool OrderAcceptedOptionalValueBranchRule14Selected;
        bool OrderAcceptedOptionalValueBranchRule15Selected;
        bool OrderAcceptedOptionalValueBranchRule16Selected;
        bool OrderAcceptedOptionalValueBranchRule17Selected;
        bool OrderAcceptedOptionalValueBranchRule18Selected;
        bool OrderAcceptedOptionalValueBranchRule19Selected;
        bool OrderAcceptedOptionalValueBranchRule20Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderAcceptedOptionalValueBranchRule0Data2 = 2;
            try
            {
                OrderAcceptedOptionalValueBranchRule0Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule0Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueFirmMeasuredBytes = SizeFirm;
                    if (orderAcceptedOptionalValueFirmMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueFirmMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueFirmMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule1Data2 = 3;
            try
            {
                OrderAcceptedOptionalValueBranchRule1Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule1Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueMinQtyMeasuredBytes = SizeMinQty;
                    if (orderAcceptedOptionalValueMinQtyMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueMinQtyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueMinQtyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule2Data2 = 4;
            try
            {
                OrderAcceptedOptionalValueBranchRule2Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule2Selected)
            {
                
                {
                    const int customerTypeMeasuredBytes = SizeCustomerType;
                    if (customerTypeMeasuredBytes < Position.Minimum || customerTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += customerTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule3Data2 = 5;
            try
            {
                OrderAcceptedOptionalValueBranchRule3Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule3Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueMaxFloorMeasuredBytes = SizeMaxFloor;
                    if (orderAcceptedOptionalValueMaxFloorMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueMaxFloorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueMaxFloorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule4Data2 = 6;
            try
            {
                OrderAcceptedOptionalValueBranchRule4Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule4Selected)
            {
                
                {
                    const int orderAcceptedOptionalValuePriceTypeMeasuredBytes = SizePriceType;
                    if (orderAcceptedOptionalValuePriceTypeMeasuredBytes < Position.Minimum || orderAcceptedOptionalValuePriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValuePriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule5Data2 = 7;
            try
            {
                OrderAcceptedOptionalValueBranchRule5Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule5Selected)
            {
                
                {
                    const int pegOffsetMeasuredBytes = SizePegOffset;
                    if (pegOffsetMeasuredBytes < Position.Minimum || pegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += pegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule6Data2 = 9;
            try
            {
                OrderAcceptedOptionalValueBranchRule6Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule6Selected)
            {
                
                {
                    const int discretionPriceMeasuredBytes = SizeDiscretionPrice;
                    if (discretionPriceMeasuredBytes < Position.Minimum || discretionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += discretionPriceMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule7Data2 = 10;
            try
            {
                OrderAcceptedOptionalValueBranchRule7Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule7Selected)
            {
                
                {
                    const int discretionPriceTypeMeasuredBytes = SizeDiscretionPriceType;
                    if (discretionPriceTypeMeasuredBytes < Position.Minimum || discretionPriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += discretionPriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule8Data2 = 11;
            try
            {
                OrderAcceptedOptionalValueBranchRule8Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule8Selected)
            {
                
                {
                    const int discretionPegOffsetMeasuredBytes = SizeDiscretionPegOffset;
                    if (discretionPegOffsetMeasuredBytes < Position.Minimum || discretionPegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += discretionPegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule9Data2 = 12;
            try
            {
                OrderAcceptedOptionalValueBranchRule9Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule9Selected)
            {
                
                {
                    const int orderAcceptedOptionalValuePostOnlyMeasuredBytes = SizePostOnly;
                    if (orderAcceptedOptionalValuePostOnlyMeasuredBytes < Position.Minimum || orderAcceptedOptionalValuePostOnlyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValuePostOnlyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule10Data2 = 13;
            try
            {
                OrderAcceptedOptionalValueBranchRule10Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule10Selected)
            {
                
                {
                    const int randomReservesMeasuredBytes = SizeRandomReserves;
                    if (randomReservesMeasuredBytes < Position.Minimum || randomReservesMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += randomReservesMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule11Data2 = 14;
            try
            {
                OrderAcceptedOptionalValueBranchRule11Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule11Selected)
            {
                
                {
                    const int routeMeasuredBytes = SizeRoute;
                    if (routeMeasuredBytes < Position.Minimum || routeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += routeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule12Data2 = 15;
            try
            {
                OrderAcceptedOptionalValueBranchRule12Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule12Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueExpireTimeMeasuredBytes = SizeExpireTime;
                    if (orderAcceptedOptionalValueExpireTimeMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueExpireTimeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueExpireTimeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule13Data2 = 16;
            try
            {
                OrderAcceptedOptionalValueBranchRule13Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule13Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueTradeNowMeasuredBytes = SizeTradeNow;
                    if (orderAcceptedOptionalValueTradeNowMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueTradeNowMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueTradeNowMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule14Data2 = 17;
            try
            {
                OrderAcceptedOptionalValueBranchRule14Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule14Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueHandleInstMeasuredBytes = SizeHandleInst;
                    if (orderAcceptedOptionalValueHandleInstMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueHandleInstMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueHandleInstMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule15Data2 = 18;
            try
            {
                OrderAcceptedOptionalValueBranchRule15Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule15Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueBboWeightIndicatorMeasuredBytes = SizeBboWeightIndicator;
                    if (orderAcceptedOptionalValueBboWeightIndicatorMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueBboWeightIndicatorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueBboWeightIndicatorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule16Data2 = 24;
            try
            {
                OrderAcceptedOptionalValueBranchRule16Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule16Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueGroupIdMeasuredBytes = SizeGroupId;
                    if (orderAcceptedOptionalValueGroupIdMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule17Data2 = 25;
            try
            {
                OrderAcceptedOptionalValueBranchRule17Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule17Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueSharesLocatedMeasuredBytes = SizeSharesLocated;
                    if (orderAcceptedOptionalValueSharesLocatedMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueSharesLocatedMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueSharesLocatedMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule18Data2 = 28;
            try
            {
                OrderAcceptedOptionalValueBranchRule18Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule18Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule18Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (orderAcceptedOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule19Data2 = 29;
            try
            {
                OrderAcceptedOptionalValueBranchRule19Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule19Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule19Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueAiqStrategyMeasuredBytes = SizeAiqStrategy;
                    if (orderAcceptedOptionalValueAiqStrategyMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueAiqStrategyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule20Data2 = 30;
            try
            {
                OrderAcceptedOptionalValueBranchRule20Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule20Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule20Selected)
            {
                
                {
                    const int orderAcceptedOptionalValueAiqGroupIdMeasuredBytes = SizeAiqGroupId;
                    if (orderAcceptedOptionalValueAiqGroupIdMeasuredBytes < Position.Minimum || orderAcceptedOptionalValueAiqGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderAcceptedOptionalValueAiqGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureOrderAcceptedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int orderAcceptedMessageAppendageLengthDependencyReadDistanceFromOrderAcceptedAppendage = -2;
        const int orderAcceptedMessageAppendageLengthDependencyReadWidth = 2;
        long orderAcceptedMessageAppendageLengthDependencyReadPosition;
        ushort orderAcceptedMessageAppendageLengthValue;
        global::System.Int128 OrderAcceptedAppendageCountRule0RawEnd;
        int OrderAcceptedAppendageCountRule0End;
        const int orderAcceptedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderAcceptedAppendage = 0;
        const int orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderAcceptedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderAcceptedAppendageSizeRule1Width;
        int OrderAcceptedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderAcceptedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderAcceptedMessageTimestampMeasuredBytes < Position.Minimum || orderAcceptedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderAcceptedMessageUserRefNumMeasuredBytes < Position.Minimum || orderAcceptedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageSideMeasuredBytes = SizeSide;
            if (orderAcceptedMessageSideMeasuredBytes < Position.Minimum || orderAcceptedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageSideMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderAcceptedMessageQuantityMeasuredBytes < Position.Minimum || orderAcceptedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageQuantityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageSymbolMeasuredBytes = SizeSymbol;
            if (orderAcceptedMessageSymbolMeasuredBytes < Position.Minimum || orderAcceptedMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageSymbolMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessagePriceMeasuredBytes = SizePrice;
            if (orderAcceptedMessagePriceMeasuredBytes < Position.Minimum || orderAcceptedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessagePriceMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (orderAcceptedMessageTimeInForceMeasuredBytes < Position.Minimum || orderAcceptedMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageDisplayMeasuredBytes = SizeDisplay;
            if (orderAcceptedMessageDisplayMeasuredBytes < Position.Minimum || orderAcceptedMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageDisplayMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (orderAcceptedMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || orderAcceptedMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageCapacityMeasuredBytes = SizeCapacity;
            if (orderAcceptedMessageCapacityMeasuredBytes < Position.Minimum || orderAcceptedMessageCapacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageCapacityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageCrossTypeMeasuredBytes = SizeCrossType;
            if (orderAcceptedMessageCrossTypeMeasuredBytes < Position.Minimum || orderAcceptedMessageCrossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageCrossTypeMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageOrderStateMeasuredBytes = SizeOrderState;
            if (orderAcceptedMessageOrderStateMeasuredBytes < Position.Minimum || orderAcceptedMessageOrderStateMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageOrderStateMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageClordidMeasuredBytes = SizeClordid;
            if (orderAcceptedMessageClordidMeasuredBytes < Position.Minimum || orderAcceptedMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageClordidMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderAcceptedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderAcceptedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageAppendageLengthMeasuredBytes;
        }
        
        {
            orderAcceptedMessageAppendageLengthDependencyReadPosition = (long)index + orderAcceptedMessageAppendageLengthDependencyReadDistanceFromOrderAcceptedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderAcceptedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderAcceptedMessageAppendageLengthDependencyReadPosition > limit || orderAcceptedMessageAppendageLengthDependencyReadWidth > limit - orderAcceptedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            orderAcceptedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderAcceptedMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                OrderAcceptedAppendageCountRule0RawEnd = ((orderAcceptedMessageAppendageLengthValue) switch { var OrderAcceptedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderAcceptedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderAcceptedAppendageCountRule0RawEnd < index || OrderAcceptedAppendageCountRule0RawEnd > limit) return Position.Failure;
            OrderAcceptedAppendageCountRule0End = (int)OrderAcceptedAppendageCountRule0RawEnd;
            while (index < OrderAcceptedAppendageCountRule0End)
            {
                int orderAcceptedAppendageEntryStart = index;
                
                {
                    orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderAcceptedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderAcceptedAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    orderAcceptedAppendageOptionalFieldLengthValue = bytes[(int)orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition];
                    const long OrderAcceptedAppendageSizeRule1Data2 = 1;
                    try
                    {
                        OrderAcceptedAppendageSizeRule1Width = ((orderAcceptedAppendageOptionalFieldLengthValue) switch { var OrderAcceptedAppendageSizeRule1ArithmeticLeft0 => ((OrderAcceptedAppendageSizeRule1Data2) switch { var OrderAcceptedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (OrderAcceptedAppendageSizeRule1Width < Position.Minimum || OrderAcceptedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    OrderAcceptedAppendageSizeRule1End = index + (int)OrderAcceptedAppendageSizeRule1Width;
                    int orderAcceptedAppendageEnd = OrderAcceptedAppendageSizeRule1End;
                    index = orderAcceptedAppendageEnd;
                }
                if (index <= orderAcceptedAppendageEntryStart || index > OrderAcceptedAppendageCountRule0End) return Position.Failure;
            }
            if (index != OrderAcceptedAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepOrderAcceptedMessageOrderAcceptedAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int OrderAcceptedAppendageCountRule0End)
    {
        const int orderAcceptedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderAcceptedAppendage = 0;
        const int orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition;
        byte orderAcceptedAppendageOptionalFieldLengthValue;
        global::System.Int128 OrderAcceptedAppendageSizeRule1Width;
        int OrderAcceptedAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < OrderAcceptedAppendageCountRule0End)) return Position.Ended;
        int orderAcceptedAppendageEntryStart = index;
        
        {
            orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition = (long)index + orderAcceptedAppendageOptionalFieldLengthDependencyReadDistanceFromOrderAcceptedAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition > limit || orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth > limit - orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            orderAcceptedAppendageOptionalFieldLengthValue = bytes[(int)orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition];
            const long OrderAcceptedAppendageSizeRule1Data2 = 1;
            try
            {
                OrderAcceptedAppendageSizeRule1Width = ((orderAcceptedAppendageOptionalFieldLengthValue) switch { var OrderAcceptedAppendageSizeRule1ArithmeticLeft0 => ((OrderAcceptedAppendageSizeRule1Data2) switch { var OrderAcceptedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (OrderAcceptedAppendageSizeRule1Width < Position.Minimum || OrderAcceptedAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            OrderAcceptedAppendageSizeRule1End = index + (int)OrderAcceptedAppendageSizeRule1Width;
            int orderAcceptedAppendageEnd = OrderAcceptedAppendageSizeRule1End;
            index = orderAcceptedAppendageEnd;
        }
        if (index <= orderAcceptedAppendageEntryStart || index > OrderAcceptedAppendageCountRule0End) return Position.Failure;
        return index;
    }
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
    internal static int MeasureEnableOrderEntryRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnableOrderEntryRequestOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnableOrderEntryRequestOptionalValueBranchRule0Data2 = 28;
            try
            {
                EnableOrderEntryRequestOptionalValueBranchRule0Selected = (dependency0 == EnableOrderEntryRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnableOrderEntryRequestOptionalValueBranchRule0Selected)
            {
                
                {
                    const int enableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (enableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || enableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureEnableOrderEntryRequestMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int enableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage = -2;
        const int enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
        long enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
        ushort enableOrderEntryRequestMessageAppendageLengthValue;
        global::System.Int128 EnableOrderEntryRequestAppendageCountRule0RawEnd;
        int EnableOrderEntryRequestAppendageCountRule0End;
        const int enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage = 0;
        const int enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte enableOrderEntryRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 EnableOrderEntryRequestAppendageSizeRule1Width;
        int EnableOrderEntryRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enableOrderEntryRequestMessageUserRefNumMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enableOrderEntryRequestMessageFirmMeasuredBytes = SizeFirm;
            if (enableOrderEntryRequestMessageFirmMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int enableOrderEntryRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enableOrderEntryRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageAppendageLengthMeasuredBytes;
        }
        
        {
            enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = (long)index + enableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > limit || enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > limit - enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            enableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                EnableOrderEntryRequestAppendageCountRule0RawEnd = ((enableOrderEntryRequestMessageAppendageLengthValue) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnableOrderEntryRequestAppendageCountRule0RawEnd < index || EnableOrderEntryRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
            EnableOrderEntryRequestAppendageCountRule0End = (int)EnableOrderEntryRequestAppendageCountRule0RawEnd;
            while (index < EnableOrderEntryRequestAppendageCountRule0End)
            {
                int enableOrderEntryRequestAppendageEntryStart = index;
                
                {
                    enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    enableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
                    const long EnableOrderEntryRequestAppendageSizeRule1Data2 = 1;
                    try
                    {
                        EnableOrderEntryRequestAppendageSizeRule1Width = ((enableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryRequestAppendageSizeRule1Data2) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (EnableOrderEntryRequestAppendageSizeRule1Width < Position.Minimum || EnableOrderEntryRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    EnableOrderEntryRequestAppendageSizeRule1End = index + (int)EnableOrderEntryRequestAppendageSizeRule1Width;
                    int enableOrderEntryRequestAppendageEnd = EnableOrderEntryRequestAppendageSizeRule1End;
                    index = enableOrderEntryRequestAppendageEnd;
                }
                if (index <= enableOrderEntryRequestAppendageEntryStart || index > EnableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
            }
            if (index != EnableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepEnableOrderEntryRequestMessageEnableOrderEntryRequestAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int EnableOrderEntryRequestAppendageCountRule0End)
    {
        const int enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage = 0;
        const int enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte enableOrderEntryRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 EnableOrderEntryRequestAppendageSizeRule1Width;
        int EnableOrderEntryRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < EnableOrderEntryRequestAppendageCountRule0End)) return Position.Ended;
        int enableOrderEntryRequestAppendageEntryStart = index;
        
        {
            enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            enableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
            const long EnableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            try
            {
                EnableOrderEntryRequestAppendageSizeRule1Width = ((enableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryRequestAppendageSizeRule1Data2) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnableOrderEntryRequestAppendageSizeRule1Width < Position.Minimum || EnableOrderEntryRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            EnableOrderEntryRequestAppendageSizeRule1End = index + (int)EnableOrderEntryRequestAppendageSizeRule1Width;
            int enableOrderEntryRequestAppendageEnd = EnableOrderEntryRequestAppendageSizeRule1End;
            index = enableOrderEntryRequestAppendageEnd;
        }
        if (index <= enableOrderEntryRequestAppendageEntryStart || index > EnableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureDisableOrderEntryRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool DisableOrderEntryRequestOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte DisableOrderEntryRequestOptionalValueBranchRule0Data2 = 28;
            try
            {
                DisableOrderEntryRequestOptionalValueBranchRule0Selected = (dependency0 == DisableOrderEntryRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (DisableOrderEntryRequestOptionalValueBranchRule0Selected)
            {
                
                {
                    const int disableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (disableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || disableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += disableOrderEntryRequestOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureDisableOrderEntryRequestMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int disableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage = -2;
        const int disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
        long disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
        ushort disableOrderEntryRequestMessageAppendageLengthValue;
        global::System.Int128 DisableOrderEntryRequestAppendageCountRule0RawEnd;
        int DisableOrderEntryRequestAppendageCountRule0End;
        const int disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage = 0;
        const int disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte disableOrderEntryRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 DisableOrderEntryRequestAppendageSizeRule1Width;
        int DisableOrderEntryRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (disableOrderEntryRequestMessageUserRefNumMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int disableOrderEntryRequestMessageFirmMeasuredBytes = SizeFirm;
            if (disableOrderEntryRequestMessageFirmMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int disableOrderEntryRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (disableOrderEntryRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageAppendageLengthMeasuredBytes;
        }
        
        {
            disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = (long)index + disableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > limit || disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > limit - disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            disableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                DisableOrderEntryRequestAppendageCountRule0RawEnd = ((disableOrderEntryRequestMessageAppendageLengthValue) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (DisableOrderEntryRequestAppendageCountRule0RawEnd < index || DisableOrderEntryRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
            DisableOrderEntryRequestAppendageCountRule0End = (int)DisableOrderEntryRequestAppendageCountRule0RawEnd;
            while (index < DisableOrderEntryRequestAppendageCountRule0End)
            {
                int disableOrderEntryRequestAppendageEntryStart = index;
                
                {
                    disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    disableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
                    const long DisableOrderEntryRequestAppendageSizeRule1Data2 = 1;
                    try
                    {
                        DisableOrderEntryRequestAppendageSizeRule1Width = ((disableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryRequestAppendageSizeRule1Data2) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (DisableOrderEntryRequestAppendageSizeRule1Width < Position.Minimum || DisableOrderEntryRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    DisableOrderEntryRequestAppendageSizeRule1End = index + (int)DisableOrderEntryRequestAppendageSizeRule1Width;
                    int disableOrderEntryRequestAppendageEnd = DisableOrderEntryRequestAppendageSizeRule1End;
                    index = disableOrderEntryRequestAppendageEnd;
                }
                if (index <= disableOrderEntryRequestAppendageEntryStart || index > DisableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
            }
            if (index != DisableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepDisableOrderEntryRequestMessageDisableOrderEntryRequestAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int DisableOrderEntryRequestAppendageCountRule0End)
    {
        const int disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage = 0;
        const int disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte disableOrderEntryRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 DisableOrderEntryRequestAppendageSizeRule1Width;
        int DisableOrderEntryRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < DisableOrderEntryRequestAppendageCountRule0End)) return Position.Ended;
        int disableOrderEntryRequestAppendageEntryStart = index;
        
        {
            disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            disableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
            const long DisableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            try
            {
                DisableOrderEntryRequestAppendageSizeRule1Width = ((disableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryRequestAppendageSizeRule1Data2) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (DisableOrderEntryRequestAppendageSizeRule1Width < Position.Minimum || DisableOrderEntryRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            DisableOrderEntryRequestAppendageSizeRule1End = index + (int)DisableOrderEntryRequestAppendageSizeRule1Width;
            int disableOrderEntryRequestAppendageEnd = DisableOrderEntryRequestAppendageSizeRule1End;
            index = disableOrderEntryRequestAppendageEnd;
        }
        if (index <= disableOrderEntryRequestAppendageEntryStart || index > DisableOrderEntryRequestAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureMassCancelRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool MassCancelRequestOptionalValueBranchRule0Selected;
        bool MassCancelRequestOptionalValueBranchRule1Selected;
        bool MassCancelRequestOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte MassCancelRequestOptionalValueBranchRule0Data2 = 27;
            try
            {
                MassCancelRequestOptionalValueBranchRule0Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule0Selected)
            {
                
                {
                    const int massCancelRequestOptionalValueSideMeasuredBytes = SizeSide;
                    if (massCancelRequestOptionalValueSideMeasuredBytes < Position.Minimum || massCancelRequestOptionalValueSideMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelRequestOptionalValueSideMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte MassCancelRequestOptionalValueBranchRule1Data2 = 24;
            try
            {
                MassCancelRequestOptionalValueBranchRule1Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule1Selected)
            {
                
                {
                    const int massCancelRequestOptionalValueGroupIdMeasuredBytes = SizeGroupId;
                    if (massCancelRequestOptionalValueGroupIdMeasuredBytes < Position.Minimum || massCancelRequestOptionalValueGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelRequestOptionalValueGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte MassCancelRequestOptionalValueBranchRule2Data2 = 28;
            try
            {
                MassCancelRequestOptionalValueBranchRule2Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule2Selected)
            {
                
                {
                    const int massCancelRequestOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (massCancelRequestOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || massCancelRequestOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += massCancelRequestOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureMassCancelRequestMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int massCancelRequestMessageAppendageLengthDependencyReadDistanceFromMassCancelRequestAppendage = -2;
        const int massCancelRequestMessageAppendageLengthDependencyReadWidth = 2;
        long massCancelRequestMessageAppendageLengthDependencyReadPosition;
        ushort massCancelRequestMessageAppendageLengthValue;
        global::System.Int128 MassCancelRequestAppendageCountRule0RawEnd;
        int MassCancelRequestAppendageCountRule0End;
        const int massCancelRequestAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelRequestAppendage = 0;
        const int massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte massCancelRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 MassCancelRequestAppendageSizeRule1Width;
        int MassCancelRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (massCancelRequestMessageUserRefNumMeasuredBytes < Position.Minimum || massCancelRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageFirmMeasuredBytes = SizeFirm;
            if (massCancelRequestMessageFirmMeasuredBytes < Position.Minimum || massCancelRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageSymbolMeasuredBytes = SizeSymbol;
            if (massCancelRequestMessageSymbolMeasuredBytes < Position.Minimum || massCancelRequestMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageSymbolMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (massCancelRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || massCancelRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageAppendageLengthMeasuredBytes;
        }
        
        {
            massCancelRequestMessageAppendageLengthDependencyReadPosition = (long)index + massCancelRequestMessageAppendageLengthDependencyReadDistanceFromMassCancelRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelRequestMessageAppendageLengthDependencyReadPosition > limit || massCancelRequestMessageAppendageLengthDependencyReadWidth > limit - massCancelRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            massCancelRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelRequestMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                MassCancelRequestAppendageCountRule0RawEnd = ((massCancelRequestMessageAppendageLengthValue) switch { var MassCancelRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (MassCancelRequestAppendageCountRule0RawEnd < index || MassCancelRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
            MassCancelRequestAppendageCountRule0End = (int)MassCancelRequestAppendageCountRule0RawEnd;
            while (index < MassCancelRequestAppendageCountRule0End)
            {
                int massCancelRequestAppendageEntryStart = index;
                
                {
                    massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + massCancelRequestAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelRequestAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    massCancelRequestAppendageOptionalFieldLengthValue = bytes[(int)massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition];
                    const long MassCancelRequestAppendageSizeRule1Data2 = 1;
                    try
                    {
                        MassCancelRequestAppendageSizeRule1Width = ((massCancelRequestAppendageOptionalFieldLengthValue) switch { var MassCancelRequestAppendageSizeRule1ArithmeticLeft0 => ((MassCancelRequestAppendageSizeRule1Data2) switch { var MassCancelRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (MassCancelRequestAppendageSizeRule1Width < Position.Minimum || MassCancelRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    MassCancelRequestAppendageSizeRule1End = index + (int)MassCancelRequestAppendageSizeRule1Width;
                    int massCancelRequestAppendageEnd = MassCancelRequestAppendageSizeRule1End;
                    index = massCancelRequestAppendageEnd;
                }
                if (index <= massCancelRequestAppendageEntryStart || index > MassCancelRequestAppendageCountRule0End) return Position.Failure;
            }
            if (index != MassCancelRequestAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepMassCancelRequestMessageMassCancelRequestAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int MassCancelRequestAppendageCountRule0End)
    {
        const int massCancelRequestAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelRequestAppendage = 0;
        const int massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition;
        byte massCancelRequestAppendageOptionalFieldLengthValue;
        global::System.Int128 MassCancelRequestAppendageSizeRule1Width;
        int MassCancelRequestAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < MassCancelRequestAppendageCountRule0End)) return Position.Ended;
        int massCancelRequestAppendageEntryStart = index;
        
        {
            massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition = (long)index + massCancelRequestAppendageOptionalFieldLengthDependencyReadDistanceFromMassCancelRequestAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition > limit || massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth > limit - massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            massCancelRequestAppendageOptionalFieldLengthValue = bytes[(int)massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition];
            const long MassCancelRequestAppendageSizeRule1Data2 = 1;
            try
            {
                MassCancelRequestAppendageSizeRule1Width = ((massCancelRequestAppendageOptionalFieldLengthValue) switch { var MassCancelRequestAppendageSizeRule1ArithmeticLeft0 => ((MassCancelRequestAppendageSizeRule1Data2) switch { var MassCancelRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (MassCancelRequestAppendageSizeRule1Width < Position.Minimum || MassCancelRequestAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            MassCancelRequestAppendageSizeRule1End = index + (int)MassCancelRequestAppendageSizeRule1Width;
            int massCancelRequestAppendageEnd = MassCancelRequestAppendageSizeRule1End;
            index = massCancelRequestAppendageEnd;
        }
        if (index <= massCancelRequestAppendageEntryStart || index > MassCancelRequestAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureAccountQueryOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AccountQueryOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AccountQueryOptionalValueBranchRule0Data2 = 28;
            try
            {
                AccountQueryOptionalValueBranchRule0Selected = (dependency0 == AccountQueryOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AccountQueryOptionalValueBranchRule0Selected)
            {
                
                {
                    const int accountQueryOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (accountQueryOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || accountQueryOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureAccountQueryMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int accountQueryMessageAppendageLengthDependencyReadDistanceFromAccountQueryMessageAppendageLength = 0;
        const int accountQueryMessageAppendageLengthDependencyReadWidth = 2;
        long accountQueryMessageAppendageLengthDependencyReadPosition = default;
        ushort accountQueryMessageAppendageLengthValue = default;
        global::System.Int128 AccountQueryAppendageCountRule0RawEnd;
        int AccountQueryAppendageCountRule0End;
        const int accountQueryAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryAppendage = 0;
        const int accountQueryAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long accountQueryAppendageOptionalFieldLengthDependencyReadPosition;
        byte accountQueryAppendageOptionalFieldLengthValue;
        global::System.Int128 AccountQueryAppendageSizeRule1Width;
        int AccountQueryAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                accountQueryMessageAppendageLengthDependencyReadPosition = (long)index + accountQueryMessageAppendageLengthDependencyReadDistanceFromAccountQueryMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryMessageAppendageLengthDependencyReadPosition > limit || accountQueryMessageAppendageLengthDependencyReadWidth > limit - accountQueryMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                accountQueryMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int accountQueryMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (accountQueryMessageAppendageLengthMeasuredBytes < Position.Minimum || accountQueryMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                AccountQueryAppendageCountRule0RawEnd = ((accountQueryMessageAppendageLengthValue) switch { var AccountQueryAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AccountQueryAppendageCountRule0RawEnd < index || AccountQueryAppendageCountRule0RawEnd > limit) return Position.Failure;
            AccountQueryAppendageCountRule0End = (int)AccountQueryAppendageCountRule0RawEnd;
            while (index < AccountQueryAppendageCountRule0End)
            {
                int accountQueryAppendageEntryStart = index;
                
                {
                    accountQueryAppendageOptionalFieldLengthDependencyReadPosition = (long)index + accountQueryAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryAppendageOptionalFieldLengthDependencyReadPosition > limit || accountQueryAppendageOptionalFieldLengthDependencyReadWidth > limit - accountQueryAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    accountQueryAppendageOptionalFieldLengthValue = bytes[(int)accountQueryAppendageOptionalFieldLengthDependencyReadPosition];
                    const long AccountQueryAppendageSizeRule1Data2 = 1;
                    try
                    {
                        AccountQueryAppendageSizeRule1Width = ((accountQueryAppendageOptionalFieldLengthValue) switch { var AccountQueryAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryAppendageSizeRule1Data2) switch { var AccountQueryAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (AccountQueryAppendageSizeRule1Width < Position.Minimum || AccountQueryAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    AccountQueryAppendageSizeRule1End = index + (int)AccountQueryAppendageSizeRule1Width;
                    int accountQueryAppendageEnd = AccountQueryAppendageSizeRule1End;
                    index = accountQueryAppendageEnd;
                }
                if (index <= accountQueryAppendageEntryStart || index > AccountQueryAppendageCountRule0End) return Position.Failure;
            }
            if (index != AccountQueryAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepAccountQueryMessageAccountQueryAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int AccountQueryAppendageCountRule0End)
    {
        const int accountQueryAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryAppendage = 0;
        const int accountQueryAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long accountQueryAppendageOptionalFieldLengthDependencyReadPosition;
        byte accountQueryAppendageOptionalFieldLengthValue;
        global::System.Int128 AccountQueryAppendageSizeRule1Width;
        int AccountQueryAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < AccountQueryAppendageCountRule0End)) return Position.Ended;
        int accountQueryAppendageEntryStart = index;
        
        {
            accountQueryAppendageOptionalFieldLengthDependencyReadPosition = (long)index + accountQueryAppendageOptionalFieldLengthDependencyReadDistanceFromAccountQueryAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryAppendageOptionalFieldLengthDependencyReadPosition > limit || accountQueryAppendageOptionalFieldLengthDependencyReadWidth > limit - accountQueryAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            accountQueryAppendageOptionalFieldLengthValue = bytes[(int)accountQueryAppendageOptionalFieldLengthDependencyReadPosition];
            const long AccountQueryAppendageSizeRule1Data2 = 1;
            try
            {
                AccountQueryAppendageSizeRule1Width = ((accountQueryAppendageOptionalFieldLengthValue) switch { var AccountQueryAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryAppendageSizeRule1Data2) switch { var AccountQueryAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (AccountQueryAppendageSizeRule1Width < Position.Minimum || AccountQueryAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            AccountQueryAppendageSizeRule1End = index + (int)AccountQueryAppendageSizeRule1Width;
            int accountQueryAppendageEnd = AccountQueryAppendageSizeRule1End;
            index = accountQueryAppendageEnd;
        }
        if (index <= accountQueryAppendageEntryStart || index > AccountQueryAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureModifyOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ModifyOrderOptionalValueBranchRule0Selected;
        bool ModifyOrderOptionalValueBranchRule1Selected;
        bool ModifyOrderOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ModifyOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                ModifyOrderOptionalValueBranchRule0Selected = (dependency0 == ModifyOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule0Selected)
            {
                
                {
                    const int modifyOrderOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (modifyOrderOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || modifyOrderOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += modifyOrderOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ModifyOrderOptionalValueBranchRule1Data2 = 25;
            try
            {
                ModifyOrderOptionalValueBranchRule1Selected = (dependency0 == ModifyOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule1Selected)
            {
                
                {
                    const int modifyOrderOptionalValueSharesLocatedMeasuredBytes = SizeSharesLocated;
                    if (modifyOrderOptionalValueSharesLocatedMeasuredBytes < Position.Minimum || modifyOrderOptionalValueSharesLocatedMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += modifyOrderOptionalValueSharesLocatedMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ModifyOrderOptionalValueBranchRule2Data2 = 26;
            try
            {
                ModifyOrderOptionalValueBranchRule2Selected = (dependency0 == ModifyOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule2Selected)
            {
                
                {
                    const int modifyOrderOptionalValueLocateBrokerMeasuredBytes = SizeLocateBroker;
                    if (modifyOrderOptionalValueLocateBrokerMeasuredBytes < Position.Minimum || modifyOrderOptionalValueLocateBrokerMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += modifyOrderOptionalValueLocateBrokerMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureModifyOrderMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int modifyOrderMessageAppendageLengthDependencyReadDistanceFromModifyOrderMessageAppendageLength = 0;
        const int modifyOrderMessageAppendageLengthDependencyReadWidth = 2;
        long modifyOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort modifyOrderMessageAppendageLengthValue = default;
        global::System.Int128 ModifyOrderAppendageCountRule0RawEnd;
        int ModifyOrderAppendageCountRule0End;
        const int modifyOrderAppendageOptionalFieldLengthDependencyReadDistanceFromModifyOrderAppendage = 0;
        const int modifyOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long modifyOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte modifyOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 ModifyOrderAppendageSizeRule1Width;
        int ModifyOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int modifyOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (modifyOrderMessageUserRefNumMeasuredBytes < Position.Minimum || modifyOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageSideMeasuredBytes = SizeSide;
            if (modifyOrderMessageSideMeasuredBytes < Position.Minimum || modifyOrderMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageSideMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (modifyOrderMessageQuantityMeasuredBytes < Position.Minimum || modifyOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                modifyOrderMessageAppendageLengthDependencyReadPosition = (long)index + modifyOrderMessageAppendageLengthDependencyReadDistanceFromModifyOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || modifyOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || modifyOrderMessageAppendageLengthDependencyReadPosition > limit || modifyOrderMessageAppendageLengthDependencyReadWidth > limit - modifyOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                modifyOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)modifyOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int modifyOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (modifyOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || modifyOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += modifyOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                ModifyOrderAppendageCountRule0RawEnd = ((modifyOrderMessageAppendageLengthValue) switch { var ModifyOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ModifyOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ModifyOrderAppendageCountRule0RawEnd < index || ModifyOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
            ModifyOrderAppendageCountRule0End = (int)ModifyOrderAppendageCountRule0RawEnd;
            while (index < ModifyOrderAppendageCountRule0End)
            {
                int modifyOrderAppendageEntryStart = index;
                
                {
                    modifyOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + modifyOrderAppendageOptionalFieldLengthDependencyReadDistanceFromModifyOrderAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || modifyOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || modifyOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || modifyOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - modifyOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    modifyOrderAppendageOptionalFieldLengthValue = bytes[(int)modifyOrderAppendageOptionalFieldLengthDependencyReadPosition];
                    const long ModifyOrderAppendageSizeRule1Data2 = 1;
                    try
                    {
                        ModifyOrderAppendageSizeRule1Width = ((modifyOrderAppendageOptionalFieldLengthValue) switch { var ModifyOrderAppendageSizeRule1ArithmeticLeft0 => ((ModifyOrderAppendageSizeRule1Data2) switch { var ModifyOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (ModifyOrderAppendageSizeRule1Width < Position.Minimum || ModifyOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    ModifyOrderAppendageSizeRule1End = index + (int)ModifyOrderAppendageSizeRule1Width;
                    int modifyOrderAppendageEnd = ModifyOrderAppendageSizeRule1End;
                    index = modifyOrderAppendageEnd;
                }
                if (index <= modifyOrderAppendageEntryStart || index > ModifyOrderAppendageCountRule0End) return Position.Failure;
            }
            if (index != ModifyOrderAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepModifyOrderMessageModifyOrderAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int ModifyOrderAppendageCountRule0End)
    {
        const int modifyOrderAppendageOptionalFieldLengthDependencyReadDistanceFromModifyOrderAppendage = 0;
        const int modifyOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long modifyOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte modifyOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 ModifyOrderAppendageSizeRule1Width;
        int ModifyOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ModifyOrderAppendageCountRule0End)) return Position.Ended;
        int modifyOrderAppendageEntryStart = index;
        
        {
            modifyOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + modifyOrderAppendageOptionalFieldLengthDependencyReadDistanceFromModifyOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || modifyOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || modifyOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || modifyOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - modifyOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            modifyOrderAppendageOptionalFieldLengthValue = bytes[(int)modifyOrderAppendageOptionalFieldLengthDependencyReadPosition];
            const long ModifyOrderAppendageSizeRule1Data2 = 1;
            try
            {
                ModifyOrderAppendageSizeRule1Width = ((modifyOrderAppendageOptionalFieldLengthValue) switch { var ModifyOrderAppendageSizeRule1ArithmeticLeft0 => ((ModifyOrderAppendageSizeRule1Data2) switch { var ModifyOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ModifyOrderAppendageSizeRule1Width < Position.Minimum || ModifyOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            ModifyOrderAppendageSizeRule1End = index + (int)ModifyOrderAppendageSizeRule1Width;
            int modifyOrderAppendageEnd = ModifyOrderAppendageSizeRule1End;
            index = modifyOrderAppendageEnd;
        }
        if (index <= modifyOrderAppendageEntryStart || index > ModifyOrderAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureCancelOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelOrderOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelOrderOptionalValueBranchRule0Selected = (dependency0 == CancelOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelOrderOptionalValueBranchRule0Selected)
            {
                
                {
                    const int cancelOrderOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (cancelOrderOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || cancelOrderOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelOrderOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureCancelOrderMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        const int cancelOrderMessageAppendageLengthDependencyReadDistanceFromCancelOrderMessageAppendageLength = 0;
        const int cancelOrderMessageAppendageLengthDependencyReadWidth = 2;
        long cancelOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelOrderMessageAppendageLengthValue = default;
        global::System.Int128 CancelOrderAppendageCountRule0RawEnd;
        int CancelOrderAppendageCountRule0End;
        const int cancelOrderAppendageOptionalFieldLengthDependencyReadDistanceFromCancelOrderAppendage = 0;
        const int cancelOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelOrderAppendageSizeRule1Width;
        int CancelOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelOrderMessageUserRefNumMeasuredBytes < Position.Minimum || cancelOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int cancelOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (cancelOrderMessageQuantityMeasuredBytes < Position.Minimum || cancelOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelOrderMessageAppendageLengthDependencyReadPosition = (long)index + cancelOrderMessageAppendageLengthDependencyReadDistanceFromCancelOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelOrderMessageAppendageLengthDependencyReadPosition > limit || cancelOrderMessageAppendageLengthDependencyReadWidth > limit - cancelOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        
        {
            try
            {
                CancelOrderAppendageCountRule0RawEnd = ((cancelOrderMessageAppendageLengthValue) switch { var CancelOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelOrderAppendageCountRule0RawEnd < index || CancelOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
            CancelOrderAppendageCountRule0End = (int)CancelOrderAppendageCountRule0RawEnd;
            while (index < CancelOrderAppendageCountRule0End)
            {
                int cancelOrderAppendageEntryStart = index;
                
                {
                    cancelOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelOrderAppendageOptionalFieldLengthDependencyReadDistanceFromCancelOrderAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    cancelOrderAppendageOptionalFieldLengthValue = bytes[(int)cancelOrderAppendageOptionalFieldLengthDependencyReadPosition];
                    const long CancelOrderAppendageSizeRule1Data2 = 1;
                    try
                    {
                        CancelOrderAppendageSizeRule1Width = ((cancelOrderAppendageOptionalFieldLengthValue) switch { var CancelOrderAppendageSizeRule1ArithmeticLeft0 => ((CancelOrderAppendageSizeRule1Data2) switch { var CancelOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (CancelOrderAppendageSizeRule1Width < Position.Minimum || CancelOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    CancelOrderAppendageSizeRule1End = index + (int)CancelOrderAppendageSizeRule1Width;
                    int cancelOrderAppendageEnd = CancelOrderAppendageSizeRule1End;
                    index = cancelOrderAppendageEnd;
                }
                if (index <= cancelOrderAppendageEntryStart || index > CancelOrderAppendageCountRule0End) return Position.Failure;
            }
            if (index != CancelOrderAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepCancelOrderMessageCancelOrderAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int CancelOrderAppendageCountRule0End)
    {
        const int cancelOrderAppendageOptionalFieldLengthDependencyReadDistanceFromCancelOrderAppendage = 0;
        const int cancelOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long cancelOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte cancelOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 CancelOrderAppendageSizeRule1Width;
        int CancelOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < CancelOrderAppendageCountRule0End)) return Position.Ended;
        int cancelOrderAppendageEntryStart = index;
        
        {
            cancelOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + cancelOrderAppendageOptionalFieldLengthDependencyReadDistanceFromCancelOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || cancelOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - cancelOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            cancelOrderAppendageOptionalFieldLengthValue = bytes[(int)cancelOrderAppendageOptionalFieldLengthDependencyReadPosition];
            const long CancelOrderAppendageSizeRule1Data2 = 1;
            try
            {
                CancelOrderAppendageSizeRule1Width = ((cancelOrderAppendageOptionalFieldLengthValue) switch { var CancelOrderAppendageSizeRule1ArithmeticLeft0 => ((CancelOrderAppendageSizeRule1Data2) switch { var CancelOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (CancelOrderAppendageSizeRule1Width < Position.Minimum || CancelOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            CancelOrderAppendageSizeRule1End = index + (int)CancelOrderAppendageSizeRule1Width;
            int cancelOrderAppendageEnd = CancelOrderAppendageSizeRule1End;
            index = cancelOrderAppendageEnd;
        }
        if (index <= cancelOrderAppendageEntryStart || index > CancelOrderAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureReplaceOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ReplaceOrderOptionalValueBranchRule0Selected;
        bool ReplaceOrderOptionalValueBranchRule1Selected;
        bool ReplaceOrderOptionalValueBranchRule2Selected;
        bool ReplaceOrderOptionalValueBranchRule3Selected;
        bool ReplaceOrderOptionalValueBranchRule4Selected;
        bool ReplaceOrderOptionalValueBranchRule5Selected;
        bool ReplaceOrderOptionalValueBranchRule6Selected;
        bool ReplaceOrderOptionalValueBranchRule7Selected;
        bool ReplaceOrderOptionalValueBranchRule8Selected;
        bool ReplaceOrderOptionalValueBranchRule9Selected;
        bool ReplaceOrderOptionalValueBranchRule10Selected;
        bool ReplaceOrderOptionalValueBranchRule11Selected;
        bool ReplaceOrderOptionalValueBranchRule12Selected;
        bool ReplaceOrderOptionalValueBranchRule13Selected;
        bool ReplaceOrderOptionalValueBranchRule14Selected;
        bool ReplaceOrderOptionalValueBranchRule15Selected;
        bool ReplaceOrderOptionalValueBranchRule16Selected;
        bool ReplaceOrderOptionalValueBranchRule17Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ReplaceOrderOptionalValueBranchRule0Data2 = 3;
            try
            {
                ReplaceOrderOptionalValueBranchRule0Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule0Selected)
            {
                
                {
                    const int replaceOrderOptionalValueMinQtyMeasuredBytes = SizeMinQty;
                    if (replaceOrderOptionalValueMinQtyMeasuredBytes < Position.Minimum || replaceOrderOptionalValueMinQtyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueMinQtyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule1Data2 = 5;
            try
            {
                ReplaceOrderOptionalValueBranchRule1Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule1Selected)
            {
                
                {
                    const int replaceOrderOptionalValueMaxFloorMeasuredBytes = SizeMaxFloor;
                    if (replaceOrderOptionalValueMaxFloorMeasuredBytes < Position.Minimum || replaceOrderOptionalValueMaxFloorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueMaxFloorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule2Data2 = 6;
            try
            {
                ReplaceOrderOptionalValueBranchRule2Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule2Selected)
            {
                
                {
                    const int replaceOrderOptionalValuePriceTypeMeasuredBytes = SizePriceType;
                    if (replaceOrderOptionalValuePriceTypeMeasuredBytes < Position.Minimum || replaceOrderOptionalValuePriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValuePriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule3Data2 = 12;
            try
            {
                ReplaceOrderOptionalValueBranchRule3Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule3Selected)
            {
                
                {
                    const int replaceOrderOptionalValuePostOnlyMeasuredBytes = SizePostOnly;
                    if (replaceOrderOptionalValuePostOnlyMeasuredBytes < Position.Minimum || replaceOrderOptionalValuePostOnlyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValuePostOnlyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule4Data2 = 15;
            try
            {
                ReplaceOrderOptionalValueBranchRule4Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule4Selected)
            {
                
                {
                    const int replaceOrderOptionalValueExpireTimeMeasuredBytes = SizeExpireTime;
                    if (replaceOrderOptionalValueExpireTimeMeasuredBytes < Position.Minimum || replaceOrderOptionalValueExpireTimeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueExpireTimeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule5Data2 = 16;
            try
            {
                ReplaceOrderOptionalValueBranchRule5Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule5Selected)
            {
                
                {
                    const int replaceOrderOptionalValueTradeNowMeasuredBytes = SizeTradeNow;
                    if (replaceOrderOptionalValueTradeNowMeasuredBytes < Position.Minimum || replaceOrderOptionalValueTradeNowMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueTradeNowMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule6Data2 = 17;
            try
            {
                ReplaceOrderOptionalValueBranchRule6Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule6Selected)
            {
                
                {
                    const int replaceOrderOptionalValueHandleInstMeasuredBytes = SizeHandleInst;
                    if (replaceOrderOptionalValueHandleInstMeasuredBytes < Position.Minimum || replaceOrderOptionalValueHandleInstMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueHandleInstMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule7Data2 = 13;
            try
            {
                ReplaceOrderOptionalValueBranchRule7Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule7Selected)
            {
                
                {
                    const int replaceOrderOptionalValueRandomReservesMeasuredBytes = SizeRandomReserves;
                    if (replaceOrderOptionalValueRandomReservesMeasuredBytes < Position.Minimum || replaceOrderOptionalValueRandomReservesMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueRandomReservesMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule8Data2 = 7;
            try
            {
                ReplaceOrderOptionalValueBranchRule8Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule8Selected)
            {
                
                {
                    const int replaceOrderOptionalValuePegOffsetMeasuredBytes = SizePegOffset;
                    if (replaceOrderOptionalValuePegOffsetMeasuredBytes < Position.Minimum || replaceOrderOptionalValuePegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValuePegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule9Data2 = 9;
            try
            {
                ReplaceOrderOptionalValueBranchRule9Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule9Selected)
            {
                
                {
                    const int replaceOrderOptionalValueDiscretionPriceMeasuredBytes = SizeDiscretionPrice;
                    if (replaceOrderOptionalValueDiscretionPriceMeasuredBytes < Position.Minimum || replaceOrderOptionalValueDiscretionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueDiscretionPriceMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule10Data2 = 10;
            try
            {
                ReplaceOrderOptionalValueBranchRule10Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule10Selected)
            {
                
                {
                    const int replaceOrderOptionalValueDiscretionPriceTypeMeasuredBytes = SizeDiscretionPriceType;
                    if (replaceOrderOptionalValueDiscretionPriceTypeMeasuredBytes < Position.Minimum || replaceOrderOptionalValueDiscretionPriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueDiscretionPriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule11Data2 = 11;
            try
            {
                ReplaceOrderOptionalValueBranchRule11Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule11Selected)
            {
                
                {
                    const int replaceOrderOptionalValueDiscretionPegOffsetMeasuredBytes = SizeDiscretionPegOffset;
                    if (replaceOrderOptionalValueDiscretionPegOffsetMeasuredBytes < Position.Minimum || replaceOrderOptionalValueDiscretionPegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueDiscretionPegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule12Data2 = 25;
            try
            {
                ReplaceOrderOptionalValueBranchRule12Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule12Selected)
            {
                
                {
                    const int replaceOrderOptionalValueSharesLocatedMeasuredBytes = SizeSharesLocated;
                    if (replaceOrderOptionalValueSharesLocatedMeasuredBytes < Position.Minimum || replaceOrderOptionalValueSharesLocatedMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueSharesLocatedMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule13Data2 = 26;
            try
            {
                ReplaceOrderOptionalValueBranchRule13Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule13Selected)
            {
                
                {
                    const int replaceOrderOptionalValueLocateBrokerMeasuredBytes = SizeLocateBroker;
                    if (replaceOrderOptionalValueLocateBrokerMeasuredBytes < Position.Minimum || replaceOrderOptionalValueLocateBrokerMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueLocateBrokerMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule14Data2 = 28;
            try
            {
                ReplaceOrderOptionalValueBranchRule14Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule14Selected)
            {
                
                {
                    const int replaceOrderOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (replaceOrderOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || replaceOrderOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule15Data2 = 27;
            try
            {
                ReplaceOrderOptionalValueBranchRule15Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule15Selected)
            {
                
                {
                    const int replaceOrderOptionalValueSideMeasuredBytes = SizeSide;
                    if (replaceOrderOptionalValueSideMeasuredBytes < Position.Minimum || replaceOrderOptionalValueSideMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueSideMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule16Data2 = 29;
            try
            {
                ReplaceOrderOptionalValueBranchRule16Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule16Selected)
            {
                
                {
                    const int replaceOrderOptionalValueAiqStrategyMeasuredBytes = SizeAiqStrategy;
                    if (replaceOrderOptionalValueAiqStrategyMeasuredBytes < Position.Minimum || replaceOrderOptionalValueAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueAiqStrategyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule17Data2 = 30;
            try
            {
                ReplaceOrderOptionalValueBranchRule17Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule17Selected)
            {
                
                {
                    const int replaceOrderOptionalValueAiqGroupIdMeasuredBytes = SizeAiqGroupId;
                    if (replaceOrderOptionalValueAiqGroupIdMeasuredBytes < Position.Minimum || replaceOrderOptionalValueAiqGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += replaceOrderOptionalValueAiqGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureReplaceOrderMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int replaceOrderMessageAppendageLengthDependencyReadDistanceFromReplaceOrderAppendage = -2;
        const int replaceOrderMessageAppendageLengthDependencyReadWidth = 2;
        long replaceOrderMessageAppendageLengthDependencyReadPosition;
        ushort replaceOrderMessageAppendageLengthValue;
        global::System.Int128 ReplaceOrderAppendageCountRule0RawEnd;
        int ReplaceOrderAppendageCountRule0End;
        const int replaceOrderAppendageOptionalFieldLengthDependencyReadDistanceFromReplaceOrderAppendage = 0;
        const int replaceOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long replaceOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte replaceOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 ReplaceOrderAppendageSizeRule1Width;
        int ReplaceOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int origUserRefNumMeasuredBytes = SizeOrigUserRefNum;
            if (origUserRefNumMeasuredBytes < Position.Minimum || origUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += origUserRefNumMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (replaceOrderMessageUserRefNumMeasuredBytes < Position.Minimum || replaceOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (replaceOrderMessageQuantityMeasuredBytes < Position.Minimum || replaceOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageQuantityMeasuredBytes;
        }
        
        {
            const int replaceOrderMessagePriceMeasuredBytes = SizePrice;
            if (replaceOrderMessagePriceMeasuredBytes < Position.Minimum || replaceOrderMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessagePriceMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (replaceOrderMessageTimeInForceMeasuredBytes < Position.Minimum || replaceOrderMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageDisplayMeasuredBytes = SizeDisplay;
            if (replaceOrderMessageDisplayMeasuredBytes < Position.Minimum || replaceOrderMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageDisplayMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageClordidMeasuredBytes = SizeClordid;
            if (replaceOrderMessageClordidMeasuredBytes < Position.Minimum || replaceOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageClordidMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (replaceOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || replaceOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageAppendageLengthMeasuredBytes;
        }
        
        {
            replaceOrderMessageAppendageLengthDependencyReadPosition = (long)index + replaceOrderMessageAppendageLengthDependencyReadDistanceFromReplaceOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replaceOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || replaceOrderMessageAppendageLengthDependencyReadPosition > limit || replaceOrderMessageAppendageLengthDependencyReadWidth > limit - replaceOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            replaceOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replaceOrderMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                ReplaceOrderAppendageCountRule0RawEnd = ((replaceOrderMessageAppendageLengthValue) switch { var ReplaceOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplaceOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ReplaceOrderAppendageCountRule0RawEnd < index || ReplaceOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
            ReplaceOrderAppendageCountRule0End = (int)ReplaceOrderAppendageCountRule0RawEnd;
            while (index < ReplaceOrderAppendageCountRule0End)
            {
                int replaceOrderAppendageEntryStart = index;
                
                {
                    replaceOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + replaceOrderAppendageOptionalFieldLengthDependencyReadDistanceFromReplaceOrderAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replaceOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replaceOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || replaceOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - replaceOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    replaceOrderAppendageOptionalFieldLengthValue = bytes[(int)replaceOrderAppendageOptionalFieldLengthDependencyReadPosition];
                    const long ReplaceOrderAppendageSizeRule1Data2 = 1;
                    try
                    {
                        ReplaceOrderAppendageSizeRule1Width = ((replaceOrderAppendageOptionalFieldLengthValue) switch { var ReplaceOrderAppendageSizeRule1ArithmeticLeft0 => ((ReplaceOrderAppendageSizeRule1Data2) switch { var ReplaceOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (ReplaceOrderAppendageSizeRule1Width < Position.Minimum || ReplaceOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    ReplaceOrderAppendageSizeRule1End = index + (int)ReplaceOrderAppendageSizeRule1Width;
                    int replaceOrderAppendageEnd = ReplaceOrderAppendageSizeRule1End;
                    index = replaceOrderAppendageEnd;
                }
                if (index <= replaceOrderAppendageEntryStart || index > ReplaceOrderAppendageCountRule0End) return Position.Failure;
            }
            if (index != ReplaceOrderAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepReplaceOrderMessageReplaceOrderAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int ReplaceOrderAppendageCountRule0End)
    {
        const int replaceOrderAppendageOptionalFieldLengthDependencyReadDistanceFromReplaceOrderAppendage = 0;
        const int replaceOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long replaceOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte replaceOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 ReplaceOrderAppendageSizeRule1Width;
        int ReplaceOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < ReplaceOrderAppendageCountRule0End)) return Position.Ended;
        int replaceOrderAppendageEntryStart = index;
        
        {
            replaceOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + replaceOrderAppendageOptionalFieldLengthDependencyReadDistanceFromReplaceOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replaceOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replaceOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || replaceOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - replaceOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            replaceOrderAppendageOptionalFieldLengthValue = bytes[(int)replaceOrderAppendageOptionalFieldLengthDependencyReadPosition];
            const long ReplaceOrderAppendageSizeRule1Data2 = 1;
            try
            {
                ReplaceOrderAppendageSizeRule1Width = ((replaceOrderAppendageOptionalFieldLengthValue) switch { var ReplaceOrderAppendageSizeRule1ArithmeticLeft0 => ((ReplaceOrderAppendageSizeRule1Data2) switch { var ReplaceOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (ReplaceOrderAppendageSizeRule1Width < Position.Minimum || ReplaceOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            ReplaceOrderAppendageSizeRule1End = index + (int)ReplaceOrderAppendageSizeRule1Width;
            int replaceOrderAppendageEnd = ReplaceOrderAppendageSizeRule1End;
            index = replaceOrderAppendageEnd;
        }
        if (index <= replaceOrderAppendageEntryStart || index > ReplaceOrderAppendageCountRule0End) return Position.Failure;
        return index;
    }
    internal static int MeasureEnterOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnterOrderOptionalValueBranchRule0Selected;
        bool EnterOrderOptionalValueBranchRule1Selected;
        bool EnterOrderOptionalValueBranchRule2Selected;
        bool EnterOrderOptionalValueBranchRule3Selected;
        bool EnterOrderOptionalValueBranchRule4Selected;
        bool EnterOrderOptionalValueBranchRule5Selected;
        bool EnterOrderOptionalValueBranchRule6Selected;
        bool EnterOrderOptionalValueBranchRule7Selected;
        bool EnterOrderOptionalValueBranchRule8Selected;
        bool EnterOrderOptionalValueBranchRule9Selected;
        bool EnterOrderOptionalValueBranchRule10Selected;
        bool EnterOrderOptionalValueBranchRule11Selected;
        bool EnterOrderOptionalValueBranchRule12Selected;
        bool EnterOrderOptionalValueBranchRule13Selected;
        bool EnterOrderOptionalValueBranchRule14Selected;
        bool EnterOrderOptionalValueBranchRule15Selected;
        bool EnterOrderOptionalValueBranchRule16Selected;
        bool EnterOrderOptionalValueBranchRule17Selected;
        bool EnterOrderOptionalValueBranchRule18Selected;
        bool EnterOrderOptionalValueBranchRule19Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnterOrderOptionalValueBranchRule0Data2 = 2;
            try
            {
                EnterOrderOptionalValueBranchRule0Selected = (dependency0 == EnterOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule0Selected)
            {
                
                {
                    const int enterOrderOptionalValueFirmMeasuredBytes = SizeFirm;
                    if (enterOrderOptionalValueFirmMeasuredBytes < Position.Minimum || enterOrderOptionalValueFirmMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueFirmMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule1Data2 = 3;
            try
            {
                EnterOrderOptionalValueBranchRule1Selected = (dependency0 == EnterOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule1Selected)
            {
                
                {
                    const int enterOrderOptionalValueMinQtyMeasuredBytes = SizeMinQty;
                    if (enterOrderOptionalValueMinQtyMeasuredBytes < Position.Minimum || enterOrderOptionalValueMinQtyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueMinQtyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule2Data2 = 4;
            try
            {
                EnterOrderOptionalValueBranchRule2Selected = (dependency0 == EnterOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule2Selected)
            {
                
                {
                    const int customerTypeMeasuredBytes = SizeCustomerType;
                    if (customerTypeMeasuredBytes < Position.Minimum || customerTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += customerTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule3Data2 = 5;
            try
            {
                EnterOrderOptionalValueBranchRule3Selected = (dependency0 == EnterOrderOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule3Selected)
            {
                
                {
                    const int enterOrderOptionalValueMaxFloorMeasuredBytes = SizeMaxFloor;
                    if (enterOrderOptionalValueMaxFloorMeasuredBytes < Position.Minimum || enterOrderOptionalValueMaxFloorMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueMaxFloorMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule4Data2 = 6;
            try
            {
                EnterOrderOptionalValueBranchRule4Selected = (dependency0 == EnterOrderOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule4Selected)
            {
                
                {
                    const int enterOrderOptionalValuePriceTypeMeasuredBytes = SizePriceType;
                    if (enterOrderOptionalValuePriceTypeMeasuredBytes < Position.Minimum || enterOrderOptionalValuePriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValuePriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule5Data2 = 7;
            try
            {
                EnterOrderOptionalValueBranchRule5Selected = (dependency0 == EnterOrderOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule5Selected)
            {
                
                {
                    const int enterOrderOptionalValuePegOffsetMeasuredBytes = SizePegOffset;
                    if (enterOrderOptionalValuePegOffsetMeasuredBytes < Position.Minimum || enterOrderOptionalValuePegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValuePegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule6Data2 = 9;
            try
            {
                EnterOrderOptionalValueBranchRule6Selected = (dependency0 == EnterOrderOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule6Selected)
            {
                
                {
                    const int enterOrderOptionalValueDiscretionPriceMeasuredBytes = SizeDiscretionPrice;
                    if (enterOrderOptionalValueDiscretionPriceMeasuredBytes < Position.Minimum || enterOrderOptionalValueDiscretionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueDiscretionPriceMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule7Data2 = 10;
            try
            {
                EnterOrderOptionalValueBranchRule7Selected = (dependency0 == EnterOrderOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule7Selected)
            {
                
                {
                    const int enterOrderOptionalValueDiscretionPriceTypeMeasuredBytes = SizeDiscretionPriceType;
                    if (enterOrderOptionalValueDiscretionPriceTypeMeasuredBytes < Position.Minimum || enterOrderOptionalValueDiscretionPriceTypeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueDiscretionPriceTypeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule8Data2 = 11;
            try
            {
                EnterOrderOptionalValueBranchRule8Selected = (dependency0 == EnterOrderOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule8Selected)
            {
                
                {
                    const int enterOrderOptionalValueDiscretionPegOffsetMeasuredBytes = SizeDiscretionPegOffset;
                    if (enterOrderOptionalValueDiscretionPegOffsetMeasuredBytes < Position.Minimum || enterOrderOptionalValueDiscretionPegOffsetMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueDiscretionPegOffsetMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule9Data2 = 12;
            try
            {
                EnterOrderOptionalValueBranchRule9Selected = (dependency0 == EnterOrderOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule9Selected)
            {
                
                {
                    const int enterOrderOptionalValuePostOnlyMeasuredBytes = SizePostOnly;
                    if (enterOrderOptionalValuePostOnlyMeasuredBytes < Position.Minimum || enterOrderOptionalValuePostOnlyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValuePostOnlyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule10Data2 = 13;
            try
            {
                EnterOrderOptionalValueBranchRule10Selected = (dependency0 == EnterOrderOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule10Selected)
            {
                
                {
                    const int enterOrderOptionalValueRandomReservesMeasuredBytes = SizeRandomReserves;
                    if (enterOrderOptionalValueRandomReservesMeasuredBytes < Position.Minimum || enterOrderOptionalValueRandomReservesMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueRandomReservesMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule11Data2 = 15;
            try
            {
                EnterOrderOptionalValueBranchRule11Selected = (dependency0 == EnterOrderOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule11Selected)
            {
                
                {
                    const int enterOrderOptionalValueExpireTimeMeasuredBytes = SizeExpireTime;
                    if (enterOrderOptionalValueExpireTimeMeasuredBytes < Position.Minimum || enterOrderOptionalValueExpireTimeMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueExpireTimeMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule12Data2 = 16;
            try
            {
                EnterOrderOptionalValueBranchRule12Selected = (dependency0 == EnterOrderOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule12Selected)
            {
                
                {
                    const int enterOrderOptionalValueTradeNowMeasuredBytes = SizeTradeNow;
                    if (enterOrderOptionalValueTradeNowMeasuredBytes < Position.Minimum || enterOrderOptionalValueTradeNowMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueTradeNowMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule13Data2 = 17;
            try
            {
                EnterOrderOptionalValueBranchRule13Selected = (dependency0 == EnterOrderOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule13Selected)
            {
                
                {
                    const int enterOrderOptionalValueHandleInstMeasuredBytes = SizeHandleInst;
                    if (enterOrderOptionalValueHandleInstMeasuredBytes < Position.Minimum || enterOrderOptionalValueHandleInstMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueHandleInstMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule14Data2 = 24;
            try
            {
                EnterOrderOptionalValueBranchRule14Selected = (dependency0 == EnterOrderOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule14Selected)
            {
                
                {
                    const int enterOrderOptionalValueGroupIdMeasuredBytes = SizeGroupId;
                    if (enterOrderOptionalValueGroupIdMeasuredBytes < Position.Minimum || enterOrderOptionalValueGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule15Data2 = 25;
            try
            {
                EnterOrderOptionalValueBranchRule15Selected = (dependency0 == EnterOrderOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule15Selected)
            {
                
                {
                    const int enterOrderOptionalValueSharesLocatedMeasuredBytes = SizeSharesLocated;
                    if (enterOrderOptionalValueSharesLocatedMeasuredBytes < Position.Minimum || enterOrderOptionalValueSharesLocatedMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueSharesLocatedMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule16Data2 = 26;
            try
            {
                EnterOrderOptionalValueBranchRule16Selected = (dependency0 == EnterOrderOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule16Selected)
            {
                
                {
                    const int enterOrderOptionalValueLocateBrokerMeasuredBytes = SizeLocateBroker;
                    if (enterOrderOptionalValueLocateBrokerMeasuredBytes < Position.Minimum || enterOrderOptionalValueLocateBrokerMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueLocateBrokerMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule17Data2 = 28;
            try
            {
                EnterOrderOptionalValueBranchRule17Selected = (dependency0 == EnterOrderOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule17Selected)
            {
                
                {
                    const int enterOrderOptionalValueUserRefIdxMeasuredBytes = SizeUserRefIdx;
                    if (enterOrderOptionalValueUserRefIdxMeasuredBytes < Position.Minimum || enterOrderOptionalValueUserRefIdxMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueUserRefIdxMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule18Data2 = 29;
            try
            {
                EnterOrderOptionalValueBranchRule18Selected = (dependency0 == EnterOrderOptionalValueBranchRule18Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule18Selected)
            {
                
                {
                    const int enterOrderOptionalValueAiqStrategyMeasuredBytes = SizeAiqStrategy;
                    if (enterOrderOptionalValueAiqStrategyMeasuredBytes < Position.Minimum || enterOrderOptionalValueAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueAiqStrategyMeasuredBytes;
                }
                return index;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule19Data2 = 30;
            try
            {
                EnterOrderOptionalValueBranchRule19Selected = (dependency0 == EnterOrderOptionalValueBranchRule19Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule19Selected)
            {
                
                {
                    const int enterOrderOptionalValueAiqGroupIdMeasuredBytes = SizeAiqGroupId;
                    if (enterOrderOptionalValueAiqGroupIdMeasuredBytes < Position.Minimum || enterOrderOptionalValueAiqGroupIdMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += enterOrderOptionalValueAiqGroupIdMeasuredBytes;
                }
                return index;
            }
        }
        return index;
    }
    internal static int MeasureEnterOrderMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        const int enterOrderMessageAppendageLengthDependencyReadDistanceFromEnterOrderAppendage = -2;
        const int enterOrderMessageAppendageLengthDependencyReadWidth = 2;
        long enterOrderMessageAppendageLengthDependencyReadPosition;
        ushort enterOrderMessageAppendageLengthValue;
        global::System.Int128 EnterOrderAppendageCountRule0RawEnd;
        int EnterOrderAppendageCountRule0End;
        const int enterOrderAppendageOptionalFieldLengthDependencyReadDistanceFromEnterOrderAppendage = 0;
        const int enterOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enterOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte enterOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 EnterOrderAppendageSizeRule1Width;
        int EnterOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enterOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enterOrderMessageUserRefNumMeasuredBytes < Position.Minimum || enterOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enterOrderMessageSideMeasuredBytes = SizeSide;
            if (enterOrderMessageSideMeasuredBytes < Position.Minimum || enterOrderMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageSideMeasuredBytes;
        }
        
        {
            const int enterOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (enterOrderMessageQuantityMeasuredBytes < Position.Minimum || enterOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageQuantityMeasuredBytes;
        }
        
        {
            const int enterOrderMessageSymbolMeasuredBytes = SizeSymbol;
            if (enterOrderMessageSymbolMeasuredBytes < Position.Minimum || enterOrderMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageSymbolMeasuredBytes;
        }
        
        {
            const int enterOrderMessagePriceMeasuredBytes = SizePrice;
            if (enterOrderMessagePriceMeasuredBytes < Position.Minimum || enterOrderMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessagePriceMeasuredBytes;
        }
        
        {
            const int enterOrderMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (enterOrderMessageTimeInForceMeasuredBytes < Position.Minimum || enterOrderMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int enterOrderMessageDisplayMeasuredBytes = SizeDisplay;
            if (enterOrderMessageDisplayMeasuredBytes < Position.Minimum || enterOrderMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageDisplayMeasuredBytes;
        }
        
        {
            const int capacityMeasuredBytes = SizeCapacity;
            if (capacityMeasuredBytes < Position.Minimum || capacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += capacityMeasuredBytes;
        }
        
        {
            const int enterOrderMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (enterOrderMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || enterOrderMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int crossTypeMeasuredBytes = SizeCrossType;
            if (crossTypeMeasuredBytes < Position.Minimum || crossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += crossTypeMeasuredBytes;
        }
        
        {
            const int enterOrderMessageClordidMeasuredBytes = SizeClordid;
            if (enterOrderMessageClordidMeasuredBytes < Position.Minimum || enterOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageClordidMeasuredBytes;
        }
        
        {
            const int enterOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enterOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || enterOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageAppendageLengthMeasuredBytes;
        }
        
        {
            enterOrderMessageAppendageLengthDependencyReadPosition = (long)index + enterOrderMessageAppendageLengthDependencyReadDistanceFromEnterOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enterOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || enterOrderMessageAppendageLengthDependencyReadPosition > limit || enterOrderMessageAppendageLengthDependencyReadWidth > limit - enterOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
            enterOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enterOrderMessageAppendageLengthDependencyReadPosition)..]);
            try
            {
                EnterOrderAppendageCountRule0RawEnd = ((enterOrderMessageAppendageLengthValue) switch { var EnterOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnterOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnterOrderAppendageCountRule0RawEnd < index || EnterOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
            EnterOrderAppendageCountRule0End = (int)EnterOrderAppendageCountRule0RawEnd;
            while (index < EnterOrderAppendageCountRule0End)
            {
                int enterOrderAppendageEntryStart = index;
                
                {
                    enterOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enterOrderAppendageOptionalFieldLengthDependencyReadDistanceFromEnterOrderAppendage;
                    if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enterOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enterOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || enterOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - enterOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
                    enterOrderAppendageOptionalFieldLengthValue = bytes[(int)enterOrderAppendageOptionalFieldLengthDependencyReadPosition];
                    const long EnterOrderAppendageSizeRule1Data2 = 1;
                    try
                    {
                        EnterOrderAppendageSizeRule1Width = ((enterOrderAppendageOptionalFieldLengthValue) switch { var EnterOrderAppendageSizeRule1ArithmeticLeft0 => ((EnterOrderAppendageSizeRule1Data2) switch { var EnterOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticRight1)))) }) });
                    }
                    catch (global::System.OverflowException)
                    {
                        return Position.Failure;
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
                    if (EnterOrderAppendageSizeRule1Width < Position.Minimum || EnterOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
                    EnterOrderAppendageSizeRule1End = index + (int)EnterOrderAppendageSizeRule1Width;
                    int enterOrderAppendageEnd = EnterOrderAppendageSizeRule1End;
                    index = enterOrderAppendageEnd;
                }
                if (index <= enterOrderAppendageEntryStart || index > EnterOrderAppendageCountRule0End) return Position.Failure;
            }
            if (index != EnterOrderAppendageCountRule0End) return Position.Failure;
        }
        return index;
    }
    internal static int StepEnterOrderMessageEnterOrderAppendage(ReadOnlySpan<byte> bytes, int start, int limit, int EnterOrderAppendageCountRule0End)
    {
        const int enterOrderAppendageOptionalFieldLengthDependencyReadDistanceFromEnterOrderAppendage = 0;
        const int enterOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
        long enterOrderAppendageOptionalFieldLengthDependencyReadPosition;
        byte enterOrderAppendageOptionalFieldLengthValue;
        global::System.Int128 EnterOrderAppendageSizeRule1Width;
        int EnterOrderAppendageSizeRule1End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        if (!(index < EnterOrderAppendageCountRule0End)) return Position.Ended;
        int enterOrderAppendageEntryStart = index;
        
        {
            enterOrderAppendageOptionalFieldLengthDependencyReadPosition = (long)index + enterOrderAppendageOptionalFieldLengthDependencyReadDistanceFromEnterOrderAppendage;
            if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enterOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enterOrderAppendageOptionalFieldLengthDependencyReadPosition > limit || enterOrderAppendageOptionalFieldLengthDependencyReadWidth > limit - enterOrderAppendageOptionalFieldLengthDependencyReadPosition) return Position.Failure;
            enterOrderAppendageOptionalFieldLengthValue = bytes[(int)enterOrderAppendageOptionalFieldLengthDependencyReadPosition];
            const long EnterOrderAppendageSizeRule1Data2 = 1;
            try
            {
                EnterOrderAppendageSizeRule1Width = ((enterOrderAppendageOptionalFieldLengthValue) switch { var EnterOrderAppendageSizeRule1ArithmeticLeft0 => ((EnterOrderAppendageSizeRule1Data2) switch { var EnterOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticRight1)))) }) });
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
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
            if (EnterOrderAppendageSizeRule1Width < Position.Minimum || EnterOrderAppendageSizeRule1Width > (long)limit - index) return Position.Failure;
            EnterOrderAppendageSizeRule1End = index + (int)EnterOrderAppendageSizeRule1Width;
            int enterOrderAppendageEnd = EnterOrderAppendageSizeRule1End;
            index = enterOrderAppendageEnd;
        }
        if (index <= enterOrderAppendageEntryStart || index > EnterOrderAppendageCountRule0End) return Position.Failure;
        return index;
    }
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
    internal static int SelectEnableOrderEntryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnableOrderEntryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnableOrderEntryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                EnableOrderEntryResponseOptionalValueBranchRule0Selected = (dependency0 == EnableOrderEntryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnableOrderEntryResponseOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.None;
    }
    internal static int EnableOrderEntryResponseAppendageEnableOrderEntryResponseOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int enableOrderEntryResponseOptionalFieldDependencyReadDistanceFromEnableOrderEntryResponseOptionalValue = -1;
        const int enableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
        long enableOrderEntryResponseOptionalFieldDependencyReadPosition;
        byte enableOrderEntryResponseOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (enableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || enableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseOptionalFieldMeasuredBytes = SizeEnableOrderEntryResponseOptionalField;
            if (enableOrderEntryResponseOptionalFieldMeasuredBytes < Position.Minimum || enableOrderEntryResponseOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseOptionalFieldMeasuredBytes;
        }
        enableOrderEntryResponseOptionalFieldDependencyReadPosition = (long)index + enableOrderEntryResponseOptionalFieldDependencyReadDistanceFromEnableOrderEntryResponseOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryResponseOptionalFieldDependencyReadPosition > limit || enableOrderEntryResponseOptionalFieldDependencyReadWidth > limit - enableOrderEntryResponseOptionalFieldDependencyReadPosition) return Position.Failure;
        enableOrderEntryResponseOptionalFieldValue = bytes[(int)enableOrderEntryResponseOptionalFieldDependencyReadPosition];
        received0 = enableOrderEntryResponseOptionalFieldValue;
        return index;
    }
    internal static int EnableOrderEntryResponseMessageEnableOrderEntryResponseAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int enableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage = -2;
        const int enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
        ushort enableOrderEntryResponseMessageAppendageLengthValue;
        global::System.Int128 EnableOrderEntryResponseAppendageCountRule0RawEnd;
        int EnableOrderEntryResponseAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (enableOrderEntryResponseMessageTimestampMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enableOrderEntryResponseMessageUserRefNumMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageFirmMeasuredBytes = SizeFirm;
            if (enableOrderEntryResponseMessageFirmMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int enableOrderEntryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enableOrderEntryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || enableOrderEntryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryResponseMessageAppendageLengthMeasuredBytes;
        }
        enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = (long)index + enableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryResponseAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > limit || enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > limit - enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        enableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            EnableOrderEntryResponseAppendageCountRule0RawEnd = ((enableOrderEntryResponseMessageAppendageLengthValue) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (EnableOrderEntryResponseAppendageCountRule0RawEnd < index || EnableOrderEntryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
        EnableOrderEntryResponseAppendageCountRule0End = (int)EnableOrderEntryResponseAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = EnableOrderEntryResponseAppendageCountRule0End;
        return index;
    }
    internal static int SelectDisableOrderEntryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool DisableOrderEntryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte DisableOrderEntryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                DisableOrderEntryResponseOptionalValueBranchRule0Selected = (dependency0 == DisableOrderEntryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (DisableOrderEntryResponseOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.None;
    }
    internal static int DisableOrderEntryResponseAppendageDisableOrderEntryResponseOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int disableOrderEntryResponseOptionalFieldDependencyReadDistanceFromDisableOrderEntryResponseOptionalValue = -1;
        const int disableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
        long disableOrderEntryResponseOptionalFieldDependencyReadPosition;
        byte disableOrderEntryResponseOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (disableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || disableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseOptionalFieldMeasuredBytes = SizeDisableOrderEntryResponseOptionalField;
            if (disableOrderEntryResponseOptionalFieldMeasuredBytes < Position.Minimum || disableOrderEntryResponseOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseOptionalFieldMeasuredBytes;
        }
        disableOrderEntryResponseOptionalFieldDependencyReadPosition = (long)index + disableOrderEntryResponseOptionalFieldDependencyReadDistanceFromDisableOrderEntryResponseOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryResponseOptionalFieldDependencyReadPosition > limit || disableOrderEntryResponseOptionalFieldDependencyReadWidth > limit - disableOrderEntryResponseOptionalFieldDependencyReadPosition) return Position.Failure;
        disableOrderEntryResponseOptionalFieldValue = bytes[(int)disableOrderEntryResponseOptionalFieldDependencyReadPosition];
        received0 = disableOrderEntryResponseOptionalFieldValue;
        return index;
    }
    internal static int DisableOrderEntryResponseMessageDisableOrderEntryResponseAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int disableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage = -2;
        const int disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
        ushort disableOrderEntryResponseMessageAppendageLengthValue;
        global::System.Int128 DisableOrderEntryResponseAppendageCountRule0RawEnd;
        int DisableOrderEntryResponseAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (disableOrderEntryResponseMessageTimestampMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (disableOrderEntryResponseMessageUserRefNumMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageFirmMeasuredBytes = SizeFirm;
            if (disableOrderEntryResponseMessageFirmMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int disableOrderEntryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (disableOrderEntryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || disableOrderEntryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryResponseMessageAppendageLengthMeasuredBytes;
        }
        disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = (long)index + disableOrderEntryResponseMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryResponseAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > limit || disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > limit - disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        disableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            DisableOrderEntryResponseAppendageCountRule0RawEnd = ((disableOrderEntryResponseMessageAppendageLengthValue) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (DisableOrderEntryResponseAppendageCountRule0RawEnd < index || DisableOrderEntryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
        DisableOrderEntryResponseAppendageCountRule0End = (int)DisableOrderEntryResponseAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = DisableOrderEntryResponseAppendageCountRule0End;
        return index;
    }
    internal static int SelectMassCancelResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool MassCancelResponseOptionalValueBranchRule0Selected;
        bool MassCancelResponseOptionalValueBranchRule1Selected;
        bool MassCancelResponseOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte MassCancelResponseOptionalValueBranchRule0Data2 = 27;
            try
            {
                MassCancelResponseOptionalValueBranchRule0Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.Side;
            }
        }
        
        {
            const byte MassCancelResponseOptionalValueBranchRule1Data2 = 24;
            try
            {
                MassCancelResponseOptionalValueBranchRule1Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.GroupId;
            }
        }
        
        {
            const byte MassCancelResponseOptionalValueBranchRule2Data2 = 28;
            try
            {
                MassCancelResponseOptionalValueBranchRule2Selected = (dependency0 == MassCancelResponseOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelResponseOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.None;
    }
    internal static int MassCancelResponseAppendageMassCancelResponseOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int massCancelResponseOptionalFieldDependencyReadDistanceFromMassCancelResponseOptionalValue = -1;
        const int massCancelResponseOptionalFieldDependencyReadWidth = 1;
        long massCancelResponseOptionalFieldDependencyReadPosition;
        byte massCancelResponseOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelResponseAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (massCancelResponseAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || massCancelResponseAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int massCancelResponseOptionalFieldMeasuredBytes = SizeMassCancelResponseOptionalField;
            if (massCancelResponseOptionalFieldMeasuredBytes < Position.Minimum || massCancelResponseOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseOptionalFieldMeasuredBytes;
        }
        massCancelResponseOptionalFieldDependencyReadPosition = (long)index + massCancelResponseOptionalFieldDependencyReadDistanceFromMassCancelResponseOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelResponseOptionalFieldDependencyReadPosition < Position.Minimum || massCancelResponseOptionalFieldDependencyReadPosition > limit || massCancelResponseOptionalFieldDependencyReadWidth > limit - massCancelResponseOptionalFieldDependencyReadPosition) return Position.Failure;
        massCancelResponseOptionalFieldValue = bytes[(int)massCancelResponseOptionalFieldDependencyReadPosition];
        received0 = massCancelResponseOptionalFieldValue;
        return index;
    }
    internal static int MassCancelResponseMessageMassCancelResponseAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int massCancelResponseMessageAppendageLengthDependencyReadDistanceFromMassCancelResponseAppendage = -2;
        const int massCancelResponseMessageAppendageLengthDependencyReadWidth = 2;
        long massCancelResponseMessageAppendageLengthDependencyReadPosition;
        ushort massCancelResponseMessageAppendageLengthValue;
        global::System.Int128 MassCancelResponseAppendageCountRule0RawEnd;
        int MassCancelResponseAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (massCancelResponseMessageTimestampMeasuredBytes < Position.Minimum || massCancelResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (massCancelResponseMessageUserRefNumMeasuredBytes < Position.Minimum || massCancelResponseMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageFirmMeasuredBytes = SizeFirm;
            if (massCancelResponseMessageFirmMeasuredBytes < Position.Minimum || massCancelResponseMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageFirmMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageSymbolMeasuredBytes = SizeSymbol;
            if (massCancelResponseMessageSymbolMeasuredBytes < Position.Minimum || massCancelResponseMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageSymbolMeasuredBytes;
        }
        
        {
            const int massCancelResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (massCancelResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || massCancelResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelResponseMessageAppendageLengthMeasuredBytes;
        }
        massCancelResponseMessageAppendageLengthDependencyReadPosition = (long)index + massCancelResponseMessageAppendageLengthDependencyReadDistanceFromMassCancelResponseAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelResponseMessageAppendageLengthDependencyReadPosition > limit || massCancelResponseMessageAppendageLengthDependencyReadWidth > limit - massCancelResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        massCancelResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelResponseMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            MassCancelResponseAppendageCountRule0RawEnd = ((massCancelResponseMessageAppendageLengthValue) switch { var MassCancelResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (MassCancelResponseAppendageCountRule0RawEnd < index || MassCancelResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
        MassCancelResponseAppendageCountRule0End = (int)MassCancelResponseAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = MassCancelResponseAppendageCountRule0End;
        return index;
    }
    internal static int SelectAccountQueryResponseOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AccountQueryResponseOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AccountQueryResponseOptionalValueBranchRule0Data2 = 28;
            try
            {
                AccountQueryResponseOptionalValueBranchRule0Selected = (dependency0 == AccountQueryResponseOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AccountQueryResponseOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.None;
    }
    internal static int AccountQueryResponseAppendageAccountQueryResponseOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int accountQueryResponseOptionalFieldDependencyReadDistanceFromAccountQueryResponseOptionalValue = -1;
        const int accountQueryResponseOptionalFieldDependencyReadWidth = 1;
        long accountQueryResponseOptionalFieldDependencyReadPosition;
        byte accountQueryResponseOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int accountQueryResponseAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (accountQueryResponseAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || accountQueryResponseAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryResponseAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int accountQueryResponseOptionalFieldMeasuredBytes = SizeAccountQueryResponseOptionalField;
            if (accountQueryResponseOptionalFieldMeasuredBytes < Position.Minimum || accountQueryResponseOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryResponseOptionalFieldMeasuredBytes;
        }
        accountQueryResponseOptionalFieldDependencyReadPosition = (long)index + accountQueryResponseOptionalFieldDependencyReadDistanceFromAccountQueryResponseOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryResponseOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryResponseOptionalFieldDependencyReadPosition > limit || accountQueryResponseOptionalFieldDependencyReadWidth > limit - accountQueryResponseOptionalFieldDependencyReadPosition) return Position.Failure;
        accountQueryResponseOptionalFieldValue = bytes[(int)accountQueryResponseOptionalFieldDependencyReadPosition];
        received0 = accountQueryResponseOptionalFieldValue;
        return index;
    }
    internal static int AccountQueryResponseMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int accountQueryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (accountQueryResponseMessageTimestampMeasuredBytes < Position.Minimum || accountQueryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int nextUserRefNumMeasuredBytes = SizeNextUserRefNum;
            if (nextUserRefNumMeasuredBytes < Position.Minimum || nextUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += nextUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int AccountQueryResponseMessageAccountQueryResponseAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int accountQueryResponseMessageAppendageLengthDependencyReadDistanceFromAccountQueryResponseMessageAppendageLength = 0;
        const int accountQueryResponseMessageAppendageLengthDependencyReadWidth = 2;
        long accountQueryResponseMessageAppendageLengthDependencyReadPosition = default;
        ushort accountQueryResponseMessageAppendageLengthValue = default;
        global::System.Int128 AccountQueryResponseAppendageCountRule0RawEnd;
        int AccountQueryResponseAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int accountQueryResponseMessageTimestampMeasuredBytes = SizeTimestamp;
            if (accountQueryResponseMessageTimestampMeasuredBytes < Position.Minimum || accountQueryResponseMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryResponseMessageTimestampMeasuredBytes;
        }
        
        {
            const int nextUserRefNumMeasuredBytes = SizeNextUserRefNum;
            if (nextUserRefNumMeasuredBytes < Position.Minimum || nextUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += nextUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                accountQueryResponseMessageAppendageLengthDependencyReadPosition = (long)index + accountQueryResponseMessageAppendageLengthDependencyReadDistanceFromAccountQueryResponseMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryResponseMessageAppendageLengthDependencyReadPosition > limit || accountQueryResponseMessageAppendageLengthDependencyReadWidth > limit - accountQueryResponseMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                accountQueryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryResponseMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int accountQueryResponseMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (accountQueryResponseMessageAppendageLengthMeasuredBytes < Position.Minimum || accountQueryResponseMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryResponseMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            AccountQueryResponseAppendageCountRule0RawEnd = ((accountQueryResponseMessageAppendageLengthValue) switch { var AccountQueryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (AccountQueryResponseAppendageCountRule0RawEnd < index || AccountQueryResponseAppendageCountRule0RawEnd > limit) return Position.Failure;
        AccountQueryResponseAppendageCountRule0End = (int)AccountQueryResponseAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = AccountQueryResponseAppendageCountRule0End;
        return index;
    }
    internal static int SelectOrderRestatedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderRestatedOptionalValueBranchRule0Selected;
        bool OrderRestatedOptionalValueBranchRule1Selected;
        bool OrderRestatedOptionalValueBranchRule2Selected;
        bool OrderRestatedOptionalValueBranchRule3Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderRestatedOptionalValueBranchRule0Data2 = 22;
            try
            {
                OrderRestatedOptionalValueBranchRule0Selected = (dependency0 == OrderRestatedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule1Data2 = 23;
            try
            {
                OrderRestatedOptionalValueBranchRule1Selected = (dependency0 == OrderRestatedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule2Data2 = 1;
            try
            {
                OrderRestatedOptionalValueBranchRule2Selected = (dependency0 == OrderRestatedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum;
            }
        }
        
        {
            const byte OrderRestatedOptionalValueBranchRule3Data2 = 28;
            try
            {
                OrderRestatedOptionalValueBranchRule3Selected = (dependency0 == OrderRestatedOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderRestatedOptionalValueBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.None;
    }
    internal static int OrderRestatedAppendageOrderRestatedOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int orderRestatedOptionalFieldDependencyReadDistanceFromOrderRestatedOptionalValue = -1;
        const int orderRestatedOptionalFieldDependencyReadWidth = 1;
        long orderRestatedOptionalFieldDependencyReadPosition;
        byte orderRestatedOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderRestatedAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (orderRestatedAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || orderRestatedAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int orderRestatedOptionalFieldMeasuredBytes = SizeOrderRestatedOptionalField;
            if (orderRestatedOptionalFieldMeasuredBytes < Position.Minimum || orderRestatedOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedOptionalFieldMeasuredBytes;
        }
        orderRestatedOptionalFieldDependencyReadPosition = (long)index + orderRestatedOptionalFieldDependencyReadDistanceFromOrderRestatedOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > limit || orderRestatedOptionalFieldDependencyReadWidth > limit - orderRestatedOptionalFieldDependencyReadPosition) return Position.Failure;
        orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
        received0 = orderRestatedOptionalFieldValue;
        return index;
    }
    internal static int OrderRestatedMessageOrderRestatedAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int orderRestatedMessageAppendageLengthDependencyReadDistanceFromOrderRestatedAppendage = -2;
        const int orderRestatedMessageAppendageLengthDependencyReadWidth = 2;
        long orderRestatedMessageAppendageLengthDependencyReadPosition;
        ushort orderRestatedMessageAppendageLengthValue;
        global::System.Int128 OrderRestatedAppendageCountRule0RawEnd;
        int OrderRestatedAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderRestatedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderRestatedMessageTimestampMeasuredBytes < Position.Minimum || orderRestatedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderRestatedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderRestatedMessageUserRefNumMeasuredBytes < Position.Minimum || orderRestatedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderRestatedReasonMeasuredBytes = SizeOrderRestatedReason;
            if (orderRestatedReasonMeasuredBytes < Position.Minimum || orderRestatedReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedReasonMeasuredBytes;
        }
        
        {
            const int orderRestatedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderRestatedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderRestatedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderRestatedMessageAppendageLengthMeasuredBytes;
        }
        orderRestatedMessageAppendageLengthDependencyReadPosition = (long)index + orderRestatedMessageAppendageLengthDependencyReadDistanceFromOrderRestatedAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderRestatedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderRestatedMessageAppendageLengthDependencyReadPosition > limit || orderRestatedMessageAppendageLengthDependencyReadWidth > limit - orderRestatedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        orderRestatedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderRestatedMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            OrderRestatedAppendageCountRule0RawEnd = ((orderRestatedMessageAppendageLengthValue) switch { var OrderRestatedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderRestatedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (OrderRestatedAppendageCountRule0RawEnd < index || OrderRestatedAppendageCountRule0RawEnd > limit) return Position.Failure;
        OrderRestatedAppendageCountRule0End = (int)OrderRestatedAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = OrderRestatedAppendageCountRule0End;
        return index;
    }
    internal static int SelectOrderModifiedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderModifiedOptionalValueBranchRule0Selected;
        bool OrderModifiedOptionalValueBranchRule1Selected;
        bool OrderModifiedOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderModifiedOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderModifiedOptionalValueBranchRule0Selected = (dependency0 == OrderModifiedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte OrderModifiedOptionalValueBranchRule1Data2 = 25;
            try
            {
                OrderModifiedOptionalValueBranchRule1Selected = (dependency0 == OrderModifiedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated;
            }
        }
        
        {
            const byte OrderModifiedOptionalValueBranchRule2Data2 = 26;
            try
            {
                OrderModifiedOptionalValueBranchRule2Selected = (dependency0 == OrderModifiedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderModifiedOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.None;
    }
    internal static int OrderModifiedAppendageOrderModifiedOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int orderModifiedOptionalFieldDependencyReadDistanceFromOrderModifiedOptionalValue = -1;
        const int orderModifiedOptionalFieldDependencyReadWidth = 1;
        long orderModifiedOptionalFieldDependencyReadPosition;
        byte orderModifiedOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderModifiedAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (orderModifiedAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || orderModifiedAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int orderModifiedOptionalFieldMeasuredBytes = SizeOrderModifiedOptionalField;
            if (orderModifiedOptionalFieldMeasuredBytes < Position.Minimum || orderModifiedOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedOptionalFieldMeasuredBytes;
        }
        orderModifiedOptionalFieldDependencyReadPosition = (long)index + orderModifiedOptionalFieldDependencyReadDistanceFromOrderModifiedOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderModifiedOptionalFieldDependencyReadPosition < Position.Minimum || orderModifiedOptionalFieldDependencyReadPosition > limit || orderModifiedOptionalFieldDependencyReadWidth > limit - orderModifiedOptionalFieldDependencyReadPosition) return Position.Failure;
        orderModifiedOptionalFieldValue = bytes[(int)orderModifiedOptionalFieldDependencyReadPosition];
        received0 = orderModifiedOptionalFieldValue;
        return index;
    }
    internal static int OrderModifiedMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderModifiedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderModifiedMessageTimestampMeasuredBytes < Position.Minimum || orderModifiedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderModifiedMessageUserRefNumMeasuredBytes < Position.Minimum || orderModifiedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageSideMeasuredBytes = SizeSide;
            if (orderModifiedMessageSideMeasuredBytes < Position.Minimum || orderModifiedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageSideMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderModifiedMessageQuantityMeasuredBytes < Position.Minimum || orderModifiedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int OrderModifiedMessageOrderModifiedAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int orderModifiedMessageAppendageLengthDependencyReadDistanceFromOrderModifiedMessageAppendageLength = 0;
        const int orderModifiedMessageAppendageLengthDependencyReadWidth = 2;
        long orderModifiedMessageAppendageLengthDependencyReadPosition = default;
        ushort orderModifiedMessageAppendageLengthValue = default;
        global::System.Int128 OrderModifiedAppendageCountRule0RawEnd;
        int OrderModifiedAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderModifiedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderModifiedMessageTimestampMeasuredBytes < Position.Minimum || orderModifiedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderModifiedMessageUserRefNumMeasuredBytes < Position.Minimum || orderModifiedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageSideMeasuredBytes = SizeSide;
            if (orderModifiedMessageSideMeasuredBytes < Position.Minimum || orderModifiedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageSideMeasuredBytes;
        }
        
        {
            const int orderModifiedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderModifiedMessageQuantityMeasuredBytes < Position.Minimum || orderModifiedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderModifiedMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                orderModifiedMessageAppendageLengthDependencyReadPosition = (long)index + orderModifiedMessageAppendageLengthDependencyReadDistanceFromOrderModifiedMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderModifiedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderModifiedMessageAppendageLengthDependencyReadPosition > limit || orderModifiedMessageAppendageLengthDependencyReadWidth > limit - orderModifiedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                orderModifiedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderModifiedMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int orderModifiedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (orderModifiedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderModifiedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderModifiedMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            OrderModifiedAppendageCountRule0RawEnd = ((orderModifiedMessageAppendageLengthValue) switch { var OrderModifiedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderModifiedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (OrderModifiedAppendageCountRule0RawEnd < index || OrderModifiedAppendageCountRule0RawEnd > limit) return Position.Failure;
        OrderModifiedAppendageCountRule0End = (int)OrderModifiedAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = OrderModifiedAppendageCountRule0End;
        return index;
    }
    internal static int SelectOrderPriorityUpdateOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderPriorityUpdateOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderPriorityUpdateOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderPriorityUpdateOptionalValueBranchRule0Selected = (dependency0 == OrderPriorityUpdateOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderPriorityUpdateOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.None;
    }
    internal static int OrderPriorityUpdateAppendageOrderPriorityUpdateOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int orderPriorityUpdateOptionalFieldDependencyReadDistanceFromOrderPriorityUpdateOptionalValue = -1;
        const int orderPriorityUpdateOptionalFieldDependencyReadWidth = 1;
        long orderPriorityUpdateOptionalFieldDependencyReadPosition;
        byte orderPriorityUpdateOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderPriorityUpdateAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (orderPriorityUpdateAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || orderPriorityUpdateAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateOptionalFieldMeasuredBytes = SizeOrderPriorityUpdateOptionalField;
            if (orderPriorityUpdateOptionalFieldMeasuredBytes < Position.Minimum || orderPriorityUpdateOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateOptionalFieldMeasuredBytes;
        }
        orderPriorityUpdateOptionalFieldDependencyReadPosition = (long)index + orderPriorityUpdateOptionalFieldDependencyReadDistanceFromOrderPriorityUpdateOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderPriorityUpdateOptionalFieldDependencyReadPosition < Position.Minimum || orderPriorityUpdateOptionalFieldDependencyReadPosition > limit || orderPriorityUpdateOptionalFieldDependencyReadWidth > limit - orderPriorityUpdateOptionalFieldDependencyReadPosition) return Position.Failure;
        orderPriorityUpdateOptionalFieldValue = bytes[(int)orderPriorityUpdateOptionalFieldDependencyReadPosition];
        received0 = orderPriorityUpdateOptionalFieldValue;
        return index;
    }
    internal static int OrderPriorityUpdateMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderPriorityUpdateMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderPriorityUpdateMessageTimestampMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderPriorityUpdateMessageUserRefNumMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessagePriceMeasuredBytes = SizePrice;
            if (orderPriorityUpdateMessagePriceMeasuredBytes < Position.Minimum || orderPriorityUpdateMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessagePriceMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageDisplayMeasuredBytes = SizeDisplay;
            if (orderPriorityUpdateMessageDisplayMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageDisplayMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int OrderPriorityUpdateMessageOrderPriorityUpdateAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int orderPriorityUpdateMessageAppendageLengthDependencyReadDistanceFromOrderPriorityUpdateMessageAppendageLength = 0;
        const int orderPriorityUpdateMessageAppendageLengthDependencyReadWidth = 2;
        long orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = default;
        ushort orderPriorityUpdateMessageAppendageLengthValue = default;
        global::System.Int128 OrderPriorityUpdateAppendageCountRule0RawEnd;
        int OrderPriorityUpdateAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderPriorityUpdateMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderPriorityUpdateMessageTimestampMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderPriorityUpdateMessageUserRefNumMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessagePriceMeasuredBytes = SizePrice;
            if (orderPriorityUpdateMessagePriceMeasuredBytes < Position.Minimum || orderPriorityUpdateMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessagePriceMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageDisplayMeasuredBytes = SizeDisplay;
            if (orderPriorityUpdateMessageDisplayMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageDisplayMeasuredBytes;
        }
        
        {
            const int orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderPriorityUpdateMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = (long)index + orderPriorityUpdateMessageAppendageLengthDependencyReadDistanceFromOrderPriorityUpdateMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderPriorityUpdateMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateMessageAppendageLengthDependencyReadPosition > limit || orderPriorityUpdateMessageAppendageLengthDependencyReadWidth > limit - orderPriorityUpdateMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                orderPriorityUpdateMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderPriorityUpdateMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int orderPriorityUpdateMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (orderPriorityUpdateMessageAppendageLengthMeasuredBytes < Position.Minimum || orderPriorityUpdateMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += orderPriorityUpdateMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            OrderPriorityUpdateAppendageCountRule0RawEnd = ((orderPriorityUpdateMessageAppendageLengthValue) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (OrderPriorityUpdateAppendageCountRule0RawEnd < index || OrderPriorityUpdateAppendageCountRule0RawEnd > limit) return Position.Failure;
        OrderPriorityUpdateAppendageCountRule0End = (int)OrderPriorityUpdateAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = OrderPriorityUpdateAppendageCountRule0End;
        return index;
    }
    internal static int SelectCancelRejectOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelRejectOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelRejectOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelRejectOptionalValueBranchRule0Selected = (dependency0 == CancelRejectOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelRejectOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.None;
    }
    internal static int CancelRejectAppendageCancelRejectOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int cancelRejectOptionalFieldDependencyReadDistanceFromCancelRejectOptionalValue = -1;
        const int cancelRejectOptionalFieldDependencyReadWidth = 1;
        long cancelRejectOptionalFieldDependencyReadPosition;
        byte cancelRejectOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelRejectAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (cancelRejectAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || cancelRejectAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int cancelRejectOptionalFieldMeasuredBytes = SizeCancelRejectOptionalField;
            if (cancelRejectOptionalFieldMeasuredBytes < Position.Minimum || cancelRejectOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectOptionalFieldMeasuredBytes;
        }
        cancelRejectOptionalFieldDependencyReadPosition = (long)index + cancelRejectOptionalFieldDependencyReadDistanceFromCancelRejectOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelRejectOptionalFieldDependencyReadPosition < Position.Minimum || cancelRejectOptionalFieldDependencyReadPosition > limit || cancelRejectOptionalFieldDependencyReadWidth > limit - cancelRejectOptionalFieldDependencyReadPosition) return Position.Failure;
        cancelRejectOptionalFieldValue = bytes[(int)cancelRejectOptionalFieldDependencyReadPosition];
        received0 = cancelRejectOptionalFieldValue;
        return index;
    }
    internal static int CancelRejectMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelRejectMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelRejectMessageTimestampMeasuredBytes < Position.Minimum || cancelRejectMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelRejectMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelRejectMessageUserRefNumMeasuredBytes < Position.Minimum || cancelRejectMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int CancelRejectMessageCancelRejectAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int cancelRejectMessageAppendageLengthDependencyReadDistanceFromCancelRejectMessageAppendageLength = 0;
        const int cancelRejectMessageAppendageLengthDependencyReadWidth = 2;
        long cancelRejectMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelRejectMessageAppendageLengthValue = default;
        global::System.Int128 CancelRejectAppendageCountRule0RawEnd;
        int CancelRejectAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelRejectMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelRejectMessageTimestampMeasuredBytes < Position.Minimum || cancelRejectMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelRejectMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelRejectMessageUserRefNumMeasuredBytes < Position.Minimum || cancelRejectMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelRejectMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelRejectMessageAppendageLengthDependencyReadPosition = (long)index + cancelRejectMessageAppendageLengthDependencyReadDistanceFromCancelRejectMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelRejectMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelRejectMessageAppendageLengthDependencyReadPosition > limit || cancelRejectMessageAppendageLengthDependencyReadWidth > limit - cancelRejectMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelRejectMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelRejectMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelRejectMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelRejectMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelRejectMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelRejectMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            CancelRejectAppendageCountRule0RawEnd = ((cancelRejectMessageAppendageLengthValue) switch { var CancelRejectAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelRejectAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (CancelRejectAppendageCountRule0RawEnd < index || CancelRejectAppendageCountRule0RawEnd > limit) return Position.Failure;
        CancelRejectAppendageCountRule0End = (int)CancelRejectAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = CancelRejectAppendageCountRule0End;
        return index;
    }
    internal static int SelectCancelPendingOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelPendingOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelPendingOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelPendingOptionalValueBranchRule0Selected = (dependency0 == CancelPendingOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelPendingOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.None;
    }
    internal static int CancelPendingAppendageCancelPendingOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int cancelPendingOptionalFieldDependencyReadDistanceFromCancelPendingOptionalValue = -1;
        const int cancelPendingOptionalFieldDependencyReadWidth = 1;
        long cancelPendingOptionalFieldDependencyReadPosition;
        byte cancelPendingOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelPendingAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (cancelPendingAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || cancelPendingAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int cancelPendingOptionalFieldMeasuredBytes = SizeCancelPendingOptionalField;
            if (cancelPendingOptionalFieldMeasuredBytes < Position.Minimum || cancelPendingOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingOptionalFieldMeasuredBytes;
        }
        cancelPendingOptionalFieldDependencyReadPosition = (long)index + cancelPendingOptionalFieldDependencyReadDistanceFromCancelPendingOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelPendingOptionalFieldDependencyReadPosition < Position.Minimum || cancelPendingOptionalFieldDependencyReadPosition > limit || cancelPendingOptionalFieldDependencyReadWidth > limit - cancelPendingOptionalFieldDependencyReadPosition) return Position.Failure;
        cancelPendingOptionalFieldValue = bytes[(int)cancelPendingOptionalFieldDependencyReadPosition];
        received0 = cancelPendingOptionalFieldValue;
        return index;
    }
    internal static int CancelPendingMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelPendingMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelPendingMessageTimestampMeasuredBytes < Position.Minimum || cancelPendingMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelPendingMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelPendingMessageUserRefNumMeasuredBytes < Position.Minimum || cancelPendingMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int CancelPendingMessageCancelPendingAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int cancelPendingMessageAppendageLengthDependencyReadDistanceFromCancelPendingMessageAppendageLength = 0;
        const int cancelPendingMessageAppendageLengthDependencyReadWidth = 2;
        long cancelPendingMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelPendingMessageAppendageLengthValue = default;
        global::System.Int128 CancelPendingAppendageCountRule0RawEnd;
        int CancelPendingAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelPendingMessageTimestampMeasuredBytes = SizeTimestamp;
            if (cancelPendingMessageTimestampMeasuredBytes < Position.Minimum || cancelPendingMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageTimestampMeasuredBytes;
        }
        
        {
            const int cancelPendingMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelPendingMessageUserRefNumMeasuredBytes < Position.Minimum || cancelPendingMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelPendingMessageUserRefNumMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelPendingMessageAppendageLengthDependencyReadPosition = (long)index + cancelPendingMessageAppendageLengthDependencyReadDistanceFromCancelPendingMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelPendingMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelPendingMessageAppendageLengthDependencyReadPosition > limit || cancelPendingMessageAppendageLengthDependencyReadWidth > limit - cancelPendingMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelPendingMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelPendingMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelPendingMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelPendingMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelPendingMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelPendingMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            CancelPendingAppendageCountRule0RawEnd = ((cancelPendingMessageAppendageLengthValue) switch { var CancelPendingAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelPendingAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (CancelPendingAppendageCountRule0RawEnd < index || CancelPendingAppendageCountRule0RawEnd > limit) return Position.Failure;
        CancelPendingAppendageCountRule0End = (int)CancelPendingAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = CancelPendingAppendageCountRule0End;
        return index;
    }
    internal static int SelectRejectedOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool RejectedOrderOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte RejectedOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                RejectedOrderOptionalValueBranchRule0Selected = (dependency0 == RejectedOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (RejectedOrderOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.None;
    }
    internal static int RejectedOrderAppendageRejectedOrderOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int rejectedOrderOptionalFieldDependencyReadDistanceFromRejectedOrderOptionalValue = -1;
        const int rejectedOrderOptionalFieldDependencyReadWidth = 1;
        long rejectedOrderOptionalFieldDependencyReadPosition;
        byte rejectedOrderOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rejectedOrderAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (rejectedOrderAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || rejectedOrderAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int rejectedOrderOptionalFieldMeasuredBytes = SizeRejectedOrderOptionalField;
            if (rejectedOrderOptionalFieldMeasuredBytes < Position.Minimum || rejectedOrderOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderOptionalFieldMeasuredBytes;
        }
        rejectedOrderOptionalFieldDependencyReadPosition = (long)index + rejectedOrderOptionalFieldDependencyReadDistanceFromRejectedOrderOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rejectedOrderOptionalFieldDependencyReadPosition < Position.Minimum || rejectedOrderOptionalFieldDependencyReadPosition > limit || rejectedOrderOptionalFieldDependencyReadWidth > limit - rejectedOrderOptionalFieldDependencyReadPosition) return Position.Failure;
        rejectedOrderOptionalFieldValue = bytes[(int)rejectedOrderOptionalFieldDependencyReadPosition];
        received0 = rejectedOrderOptionalFieldValue;
        return index;
    }
    internal static int RejectedOrderMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rejectedOrderMessageTimestampMeasuredBytes = SizeTimestamp;
            if (rejectedOrderMessageTimestampMeasuredBytes < Position.Minimum || rejectedOrderMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageTimestampMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (rejectedOrderMessageUserRefNumMeasuredBytes < Position.Minimum || rejectedOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int rejectedOrderReasonMeasuredBytes = SizeRejectedOrderReason;
            if (rejectedOrderReasonMeasuredBytes < Position.Minimum || rejectedOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderReasonMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageClordidMeasuredBytes = SizeClordid;
            if (rejectedOrderMessageClordidMeasuredBytes < Position.Minimum || rejectedOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int RejectedOrderMessageRejectedOrderAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int rejectedOrderMessageAppendageLengthDependencyReadDistanceFromRejectedOrderMessageAppendageLength = 0;
        const int rejectedOrderMessageAppendageLengthDependencyReadWidth = 2;
        long rejectedOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort rejectedOrderMessageAppendageLengthValue = default;
        global::System.Int128 RejectedOrderAppendageCountRule0RawEnd;
        int RejectedOrderAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int rejectedOrderMessageTimestampMeasuredBytes = SizeTimestamp;
            if (rejectedOrderMessageTimestampMeasuredBytes < Position.Minimum || rejectedOrderMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageTimestampMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (rejectedOrderMessageUserRefNumMeasuredBytes < Position.Minimum || rejectedOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int rejectedOrderReasonMeasuredBytes = SizeRejectedOrderReason;
            if (rejectedOrderReasonMeasuredBytes < Position.Minimum || rejectedOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderReasonMeasuredBytes;
        }
        
        {
            const int rejectedOrderMessageClordidMeasuredBytes = SizeClordid;
            if (rejectedOrderMessageClordidMeasuredBytes < Position.Minimum || rejectedOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += rejectedOrderMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                rejectedOrderMessageAppendageLengthDependencyReadPosition = (long)index + rejectedOrderMessageAppendageLengthDependencyReadDistanceFromRejectedOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || rejectedOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || rejectedOrderMessageAppendageLengthDependencyReadPosition > limit || rejectedOrderMessageAppendageLengthDependencyReadWidth > limit - rejectedOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                rejectedOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)rejectedOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int rejectedOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (rejectedOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || rejectedOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += rejectedOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            RejectedOrderAppendageCountRule0RawEnd = ((rejectedOrderMessageAppendageLengthValue) switch { var RejectedOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var RejectedOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (RejectedOrderAppendageCountRule0RawEnd < index || RejectedOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
        RejectedOrderAppendageCountRule0End = (int)RejectedOrderAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = RejectedOrderAppendageCountRule0End;
        return index;
    }
    internal static int SelectBrokenTradeOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool BrokenTradeOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte BrokenTradeOptionalValueBranchRule0Data2 = 28;
            try
            {
                BrokenTradeOptionalValueBranchRule0Selected = (dependency0 == BrokenTradeOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (BrokenTradeOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.None;
    }
    internal static int BrokenTradeAppendageBrokenTradeOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int brokenTradeOptionalFieldDependencyReadDistanceFromBrokenTradeOptionalValue = -1;
        const int brokenTradeOptionalFieldDependencyReadWidth = 1;
        long brokenTradeOptionalFieldDependencyReadPosition;
        byte brokenTradeOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int brokenTradeAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (brokenTradeAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || brokenTradeAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int brokenTradeOptionalFieldMeasuredBytes = SizeBrokenTradeOptionalField;
            if (brokenTradeOptionalFieldMeasuredBytes < Position.Minimum || brokenTradeOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeOptionalFieldMeasuredBytes;
        }
        brokenTradeOptionalFieldDependencyReadPosition = (long)index + brokenTradeOptionalFieldDependencyReadDistanceFromBrokenTradeOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || brokenTradeOptionalFieldDependencyReadPosition < Position.Minimum || brokenTradeOptionalFieldDependencyReadPosition > limit || brokenTradeOptionalFieldDependencyReadWidth > limit - brokenTradeOptionalFieldDependencyReadPosition) return Position.Failure;
        brokenTradeOptionalFieldValue = bytes[(int)brokenTradeOptionalFieldDependencyReadPosition];
        received0 = brokenTradeOptionalFieldValue;
        return index;
    }
    internal static int BrokenTradeMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int brokenTradeMessageTimestampMeasuredBytes = SizeTimestamp;
            if (brokenTradeMessageTimestampMeasuredBytes < Position.Minimum || brokenTradeMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageTimestampMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (brokenTradeMessageUserRefNumMeasuredBytes < Position.Minimum || brokenTradeMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageMatchNumberMeasuredBytes = SizeMatchNumber;
            if (brokenTradeMessageMatchNumberMeasuredBytes < Position.Minimum || brokenTradeMessageMatchNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageMatchNumberMeasuredBytes;
        }
        
        {
            const int brokenTradeReasonMeasuredBytes = SizeBrokenTradeReason;
            if (brokenTradeReasonMeasuredBytes < Position.Minimum || brokenTradeReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeReasonMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageClordidMeasuredBytes = SizeClordid;
            if (brokenTradeMessageClordidMeasuredBytes < Position.Minimum || brokenTradeMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int BrokenTradeMessageBrokenTradeAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int brokenTradeMessageAppendageLengthDependencyReadDistanceFromBrokenTradeMessageAppendageLength = 0;
        const int brokenTradeMessageAppendageLengthDependencyReadWidth = 2;
        long brokenTradeMessageAppendageLengthDependencyReadPosition = default;
        ushort brokenTradeMessageAppendageLengthValue = default;
        global::System.Int128 BrokenTradeAppendageCountRule0RawEnd;
        int BrokenTradeAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int brokenTradeMessageTimestampMeasuredBytes = SizeTimestamp;
            if (brokenTradeMessageTimestampMeasuredBytes < Position.Minimum || brokenTradeMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageTimestampMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (brokenTradeMessageUserRefNumMeasuredBytes < Position.Minimum || brokenTradeMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageMatchNumberMeasuredBytes = SizeMatchNumber;
            if (brokenTradeMessageMatchNumberMeasuredBytes < Position.Minimum || brokenTradeMessageMatchNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageMatchNumberMeasuredBytes;
        }
        
        {
            const int brokenTradeReasonMeasuredBytes = SizeBrokenTradeReason;
            if (brokenTradeReasonMeasuredBytes < Position.Minimum || brokenTradeReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeReasonMeasuredBytes;
        }
        
        {
            const int brokenTradeMessageClordidMeasuredBytes = SizeClordid;
            if (brokenTradeMessageClordidMeasuredBytes < Position.Minimum || brokenTradeMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += brokenTradeMessageClordidMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                brokenTradeMessageAppendageLengthDependencyReadPosition = (long)index + brokenTradeMessageAppendageLengthDependencyReadDistanceFromBrokenTradeMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || brokenTradeMessageAppendageLengthDependencyReadPosition < Position.Minimum || brokenTradeMessageAppendageLengthDependencyReadPosition > limit || brokenTradeMessageAppendageLengthDependencyReadWidth > limit - brokenTradeMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                brokenTradeMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)brokenTradeMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int brokenTradeMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (brokenTradeMessageAppendageLengthMeasuredBytes < Position.Minimum || brokenTradeMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += brokenTradeMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            BrokenTradeAppendageCountRule0RawEnd = ((brokenTradeMessageAppendageLengthValue) switch { var BrokenTradeAppendageCountRule0ArithmeticLeft0 => ((index) switch { var BrokenTradeAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (BrokenTradeAppendageCountRule0RawEnd < index || BrokenTradeAppendageCountRule0RawEnd > limit) return Position.Failure;
        BrokenTradeAppendageCountRule0End = (int)BrokenTradeAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = BrokenTradeAppendageCountRule0End;
        return index;
    }
    internal static int SelectOrderExecutedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderExecutedOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderExecutedOptionalValueBranchRule0Data2 = 28;
            try
            {
                OrderExecutedOptionalValueBranchRule0Selected = (dependency0 == OrderExecutedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderExecutedOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.None;
    }
    internal static int OrderExecutedAppendageOrderExecutedOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int orderExecutedOptionalFieldDependencyReadDistanceFromOrderExecutedOptionalValue = -1;
        const int orderExecutedOptionalFieldDependencyReadWidth = 1;
        long orderExecutedOptionalFieldDependencyReadPosition;
        byte orderExecutedOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderExecutedAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (orderExecutedAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || orderExecutedAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int orderExecutedOptionalFieldMeasuredBytes = SizeOrderExecutedOptionalField;
            if (orderExecutedOptionalFieldMeasuredBytes < Position.Minimum || orderExecutedOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedOptionalFieldMeasuredBytes;
        }
        orderExecutedOptionalFieldDependencyReadPosition = (long)index + orderExecutedOptionalFieldDependencyReadDistanceFromOrderExecutedOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderExecutedOptionalFieldDependencyReadPosition < Position.Minimum || orderExecutedOptionalFieldDependencyReadPosition > limit || orderExecutedOptionalFieldDependencyReadWidth > limit - orderExecutedOptionalFieldDependencyReadPosition) return Position.Failure;
        orderExecutedOptionalFieldValue = bytes[(int)orderExecutedOptionalFieldDependencyReadPosition];
        received0 = orderExecutedOptionalFieldValue;
        return index;
    }
    internal static int OrderExecutedMessageOrderExecutedAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int orderExecutedMessageAppendageLengthDependencyReadDistanceFromOrderExecutedAppendage = -2;
        const int orderExecutedMessageAppendageLengthDependencyReadWidth = 2;
        long orderExecutedMessageAppendageLengthDependencyReadPosition;
        ushort orderExecutedMessageAppendageLengthValue;
        global::System.Int128 OrderExecutedAppendageCountRule0RawEnd;
        int OrderExecutedAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderExecutedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderExecutedMessageTimestampMeasuredBytes < Position.Minimum || orderExecutedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderExecutedMessageUserRefNumMeasuredBytes < Position.Minimum || orderExecutedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderExecutedMessageQuantityMeasuredBytes < Position.Minimum || orderExecutedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageQuantityMeasuredBytes;
        }
        
        {
            const int orderExecutedMessagePriceMeasuredBytes = SizePrice;
            if (orderExecutedMessagePriceMeasuredBytes < Position.Minimum || orderExecutedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessagePriceMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageLiquidityFlagMeasuredBytes = SizeLiquidityFlag;
            if (orderExecutedMessageLiquidityFlagMeasuredBytes < Position.Minimum || orderExecutedMessageLiquidityFlagMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageLiquidityFlagMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageMatchNumberMeasuredBytes = SizeMatchNumber;
            if (orderExecutedMessageMatchNumberMeasuredBytes < Position.Minimum || orderExecutedMessageMatchNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageMatchNumberMeasuredBytes;
        }
        
        {
            const int orderExecutedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderExecutedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderExecutedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderExecutedMessageAppendageLengthMeasuredBytes;
        }
        orderExecutedMessageAppendageLengthDependencyReadPosition = (long)index + orderExecutedMessageAppendageLengthDependencyReadDistanceFromOrderExecutedAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderExecutedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderExecutedMessageAppendageLengthDependencyReadPosition > limit || orderExecutedMessageAppendageLengthDependencyReadWidth > limit - orderExecutedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        orderExecutedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderExecutedMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            OrderExecutedAppendageCountRule0RawEnd = ((orderExecutedMessageAppendageLengthValue) switch { var OrderExecutedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderExecutedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (OrderExecutedAppendageCountRule0RawEnd < index || OrderExecutedAppendageCountRule0RawEnd > limit) return Position.Failure;
        OrderExecutedAppendageCountRule0End = (int)OrderExecutedAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = OrderExecutedAppendageCountRule0End;
        return index;
    }
    internal static int SelectAiqCanceledOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AiqCanceledOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AiqCanceledOptionalValueBranchRule0Data2 = 28;
            try
            {
                AiqCanceledOptionalValueBranchRule0Selected = (dependency0 == AiqCanceledOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AiqCanceledOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.None;
    }
    internal static int AiqCanceledAppendageAiqCanceledOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int aiqCanceledOptionalFieldDependencyReadDistanceFromAiqCanceledOptionalValue = -1;
        const int aiqCanceledOptionalFieldDependencyReadWidth = 1;
        long aiqCanceledOptionalFieldDependencyReadPosition;
        byte aiqCanceledOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int aiqCanceledAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (aiqCanceledAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || aiqCanceledAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int aiqCanceledOptionalFieldMeasuredBytes = SizeAiqCanceledOptionalField;
            if (aiqCanceledOptionalFieldMeasuredBytes < Position.Minimum || aiqCanceledOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledOptionalFieldMeasuredBytes;
        }
        aiqCanceledOptionalFieldDependencyReadPosition = (long)index + aiqCanceledOptionalFieldDependencyReadDistanceFromAiqCanceledOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || aiqCanceledOptionalFieldDependencyReadPosition < Position.Minimum || aiqCanceledOptionalFieldDependencyReadPosition > limit || aiqCanceledOptionalFieldDependencyReadWidth > limit - aiqCanceledOptionalFieldDependencyReadPosition) return Position.Failure;
        aiqCanceledOptionalFieldValue = bytes[(int)aiqCanceledOptionalFieldDependencyReadPosition];
        received0 = aiqCanceledOptionalFieldValue;
        return index;
    }
    internal static int AiqCanceledMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int aiqCanceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (aiqCanceledMessageTimestampMeasuredBytes < Position.Minimum || aiqCanceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (aiqCanceledMessageUserRefNumMeasuredBytes < Position.Minimum || aiqCanceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int decrementSharesMeasuredBytes = SizeDecrementShares;
            if (decrementSharesMeasuredBytes < Position.Minimum || decrementSharesMeasuredBytes > (long)limit - index) return Position.Failure;
            index += decrementSharesMeasuredBytes;
        }
        
        {
            const int orderCancelReasonMeasuredBytes = SizeOrderCancelReason;
            if (orderCancelReasonMeasuredBytes < Position.Minimum || orderCancelReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderCancelReasonMeasuredBytes;
        }
        
        {
            const int quantityPreventedFromTradingMeasuredBytes = SizeQuantityPreventedFromTrading;
            if (quantityPreventedFromTradingMeasuredBytes < Position.Minimum || quantityPreventedFromTradingMeasuredBytes > (long)limit - index) return Position.Failure;
            index += quantityPreventedFromTradingMeasuredBytes;
        }
        
        {
            const int executionPriceMeasuredBytes = SizeExecutionPrice;
            if (executionPriceMeasuredBytes < Position.Minimum || executionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += executionPriceMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageLiquidityFlagMeasuredBytes = SizeLiquidityFlag;
            if (aiqCanceledMessageLiquidityFlagMeasuredBytes < Position.Minimum || aiqCanceledMessageLiquidityFlagMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageLiquidityFlagMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageAiqStrategyMeasuredBytes = SizeAiqStrategy;
            if (aiqCanceledMessageAiqStrategyMeasuredBytes < Position.Minimum || aiqCanceledMessageAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageAiqStrategyMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int AiqCanceledMessageAiqCanceledAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int aiqCanceledMessageAppendageLengthDependencyReadDistanceFromAiqCanceledMessageAppendageLength = 0;
        const int aiqCanceledMessageAppendageLengthDependencyReadWidth = 2;
        long aiqCanceledMessageAppendageLengthDependencyReadPosition = default;
        ushort aiqCanceledMessageAppendageLengthValue = default;
        global::System.Int128 AiqCanceledAppendageCountRule0RawEnd;
        int AiqCanceledAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int aiqCanceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (aiqCanceledMessageTimestampMeasuredBytes < Position.Minimum || aiqCanceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (aiqCanceledMessageUserRefNumMeasuredBytes < Position.Minimum || aiqCanceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int decrementSharesMeasuredBytes = SizeDecrementShares;
            if (decrementSharesMeasuredBytes < Position.Minimum || decrementSharesMeasuredBytes > (long)limit - index) return Position.Failure;
            index += decrementSharesMeasuredBytes;
        }
        
        {
            const int orderCancelReasonMeasuredBytes = SizeOrderCancelReason;
            if (orderCancelReasonMeasuredBytes < Position.Minimum || orderCancelReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderCancelReasonMeasuredBytes;
        }
        
        {
            const int quantityPreventedFromTradingMeasuredBytes = SizeQuantityPreventedFromTrading;
            if (quantityPreventedFromTradingMeasuredBytes < Position.Minimum || quantityPreventedFromTradingMeasuredBytes > (long)limit - index) return Position.Failure;
            index += quantityPreventedFromTradingMeasuredBytes;
        }
        
        {
            const int executionPriceMeasuredBytes = SizeExecutionPrice;
            if (executionPriceMeasuredBytes < Position.Minimum || executionPriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += executionPriceMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageLiquidityFlagMeasuredBytes = SizeLiquidityFlag;
            if (aiqCanceledMessageLiquidityFlagMeasuredBytes < Position.Minimum || aiqCanceledMessageLiquidityFlagMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageLiquidityFlagMeasuredBytes;
        }
        
        {
            const int aiqCanceledMessageAiqStrategyMeasuredBytes = SizeAiqStrategy;
            if (aiqCanceledMessageAiqStrategyMeasuredBytes < Position.Minimum || aiqCanceledMessageAiqStrategyMeasuredBytes > (long)limit - index) return Position.Failure;
            index += aiqCanceledMessageAiqStrategyMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                aiqCanceledMessageAppendageLengthDependencyReadPosition = (long)index + aiqCanceledMessageAppendageLengthDependencyReadDistanceFromAiqCanceledMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || aiqCanceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || aiqCanceledMessageAppendageLengthDependencyReadPosition > limit || aiqCanceledMessageAppendageLengthDependencyReadWidth > limit - aiqCanceledMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                aiqCanceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)aiqCanceledMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int aiqCanceledMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (aiqCanceledMessageAppendageLengthMeasuredBytes < Position.Minimum || aiqCanceledMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += aiqCanceledMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            AiqCanceledAppendageCountRule0RawEnd = ((aiqCanceledMessageAppendageLengthValue) switch { var AiqCanceledAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AiqCanceledAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (AiqCanceledAppendageCountRule0RawEnd < index || AiqCanceledAppendageCountRule0RawEnd > limit) return Position.Failure;
        AiqCanceledAppendageCountRule0End = (int)AiqCanceledAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = AiqCanceledAppendageCountRule0End;
        return index;
    }
    internal static int SelectCanceledMessageOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CanceledMessageOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CanceledMessageOptionalValueBranchRule0Data2 = 28;
            try
            {
                CanceledMessageOptionalValueBranchRule0Selected = (dependency0 == CanceledMessageOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CanceledMessageOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.None;
    }
    internal static int CanceledMessageAppendageCanceledMessageOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int canceledMessageOptionalFieldDependencyReadDistanceFromCanceledMessageOptionalValue = -1;
        const int canceledMessageOptionalFieldDependencyReadWidth = 1;
        long canceledMessageOptionalFieldDependencyReadPosition;
        byte canceledMessageOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int canceledMessageAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (canceledMessageAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || canceledMessageAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int canceledMessageOptionalFieldMeasuredBytes = SizeCanceledMessageOptionalField;
            if (canceledMessageOptionalFieldMeasuredBytes < Position.Minimum || canceledMessageOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageOptionalFieldMeasuredBytes;
        }
        canceledMessageOptionalFieldDependencyReadPosition = (long)index + canceledMessageOptionalFieldDependencyReadDistanceFromCanceledMessageOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || canceledMessageOptionalFieldDependencyReadPosition < Position.Minimum || canceledMessageOptionalFieldDependencyReadPosition > limit || canceledMessageOptionalFieldDependencyReadWidth > limit - canceledMessageOptionalFieldDependencyReadPosition) return Position.Failure;
        canceledMessageOptionalFieldValue = bytes[(int)canceledMessageOptionalFieldDependencyReadPosition];
        received0 = canceledMessageOptionalFieldValue;
        return index;
    }
    internal static int CanceledMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int canceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (canceledMessageTimestampMeasuredBytes < Position.Minimum || canceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int canceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (canceledMessageUserRefNumMeasuredBytes < Position.Minimum || canceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int canceledMessageQuantityMeasuredBytes = SizeQuantity;
            if (canceledMessageQuantityMeasuredBytes < Position.Minimum || canceledMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageQuantityMeasuredBytes;
        }
        
        {
            const int cancelOrderReasonMeasuredBytes = SizeCancelOrderReason;
            if (cancelOrderReasonMeasuredBytes < Position.Minimum || cancelOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderReasonMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int CanceledMessageCanceledMessageAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int canceledMessageAppendageLengthDependencyReadDistanceFromCanceledMessageAppendageLength = 0;
        const int canceledMessageAppendageLengthDependencyReadWidth = 2;
        long canceledMessageAppendageLengthDependencyReadPosition = default;
        ushort canceledMessageAppendageLengthValue = default;
        global::System.Int128 CanceledMessageAppendageCountRule0RawEnd;
        int CanceledMessageAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int canceledMessageTimestampMeasuredBytes = SizeTimestamp;
            if (canceledMessageTimestampMeasuredBytes < Position.Minimum || canceledMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageTimestampMeasuredBytes;
        }
        
        {
            const int canceledMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (canceledMessageUserRefNumMeasuredBytes < Position.Minimum || canceledMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int canceledMessageQuantityMeasuredBytes = SizeQuantity;
            if (canceledMessageQuantityMeasuredBytes < Position.Minimum || canceledMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += canceledMessageQuantityMeasuredBytes;
        }
        
        {
            const int cancelOrderReasonMeasuredBytes = SizeCancelOrderReason;
            if (cancelOrderReasonMeasuredBytes < Position.Minimum || cancelOrderReasonMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderReasonMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                canceledMessageAppendageLengthDependencyReadPosition = (long)index + canceledMessageAppendageLengthDependencyReadDistanceFromCanceledMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || canceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageLengthDependencyReadPosition > limit || canceledMessageAppendageLengthDependencyReadWidth > limit - canceledMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                canceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)canceledMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int canceledMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (canceledMessageAppendageLengthMeasuredBytes < Position.Minimum || canceledMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += canceledMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            CanceledMessageAppendageCountRule0RawEnd = ((canceledMessageAppendageLengthValue) switch { var CanceledMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CanceledMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (CanceledMessageAppendageCountRule0RawEnd < index || CanceledMessageAppendageCountRule0RawEnd > limit) return Position.Failure;
        CanceledMessageAppendageCountRule0End = (int)CanceledMessageAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = CanceledMessageAppendageCountRule0End;
        return index;
    }
    internal static int SelectReplacedMessageOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ReplacedMessageOptionalValueBranchRule0Selected;
        bool ReplacedMessageOptionalValueBranchRule1Selected;
        bool ReplacedMessageOptionalValueBranchRule2Selected;
        bool ReplacedMessageOptionalValueBranchRule3Selected;
        bool ReplacedMessageOptionalValueBranchRule4Selected;
        bool ReplacedMessageOptionalValueBranchRule5Selected;
        bool ReplacedMessageOptionalValueBranchRule6Selected;
        bool ReplacedMessageOptionalValueBranchRule7Selected;
        bool ReplacedMessageOptionalValueBranchRule8Selected;
        bool ReplacedMessageOptionalValueBranchRule9Selected;
        bool ReplacedMessageOptionalValueBranchRule10Selected;
        bool ReplacedMessageOptionalValueBranchRule11Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ReplacedMessageOptionalValueBranchRule0Data2 = 2;
            try
            {
                ReplacedMessageOptionalValueBranchRule0Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule1Data2 = 3;
            try
            {
                ReplacedMessageOptionalValueBranchRule1Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule2Data2 = 5;
            try
            {
                ReplacedMessageOptionalValueBranchRule2Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule3Data2 = 6;
            try
            {
                ReplacedMessageOptionalValueBranchRule3Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule4Data2 = 12;
            try
            {
                ReplacedMessageOptionalValueBranchRule4Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule5Data2 = 15;
            try
            {
                ReplacedMessageOptionalValueBranchRule5Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule6Data2 = 16;
            try
            {
                ReplacedMessageOptionalValueBranchRule6Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule7Data2 = 17;
            try
            {
                ReplacedMessageOptionalValueBranchRule7Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule8Data2 = 18;
            try
            {
                ReplacedMessageOptionalValueBranchRule8Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule9Data2 = 28;
            try
            {
                ReplacedMessageOptionalValueBranchRule9Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule10Data2 = 29;
            try
            {
                ReplacedMessageOptionalValueBranchRule10Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy;
            }
        }
        
        {
            const byte ReplacedMessageOptionalValueBranchRule11Data2 = 30;
            try
            {
                ReplacedMessageOptionalValueBranchRule11Selected = (dependency0 == ReplacedMessageOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplacedMessageOptionalValueBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.None;
    }
    internal static int ReplacedMessageAppendageReplacedMessageOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int replacedMessageOptionalFieldDependencyReadDistanceFromReplacedMessageOptionalValue = -1;
        const int replacedMessageOptionalFieldDependencyReadWidth = 1;
        long replacedMessageOptionalFieldDependencyReadPosition;
        byte replacedMessageOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int replacedMessageAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (replacedMessageAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || replacedMessageAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int replacedMessageOptionalFieldMeasuredBytes = SizeReplacedMessageOptionalField;
            if (replacedMessageOptionalFieldMeasuredBytes < Position.Minimum || replacedMessageOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageOptionalFieldMeasuredBytes;
        }
        replacedMessageOptionalFieldDependencyReadPosition = (long)index + replacedMessageOptionalFieldDependencyReadDistanceFromReplacedMessageOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > limit || replacedMessageOptionalFieldDependencyReadWidth > limit - replacedMessageOptionalFieldDependencyReadPosition) return Position.Failure;
        replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
        received0 = replacedMessageOptionalFieldValue;
        return index;
    }
    internal static int ReplacedMessageReplacedMessageAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int replacedMessageAppendageLengthDependencyReadDistanceFromReplacedMessageAppendage = -2;
        const int replacedMessageAppendageLengthDependencyReadWidth = 2;
        long replacedMessageAppendageLengthDependencyReadPosition;
        ushort replacedMessageAppendageLengthValue;
        global::System.Int128 ReplacedMessageAppendageCountRule0RawEnd;
        int ReplacedMessageAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int replacedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (replacedMessageTimestampMeasuredBytes < Position.Minimum || replacedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageTimestampMeasuredBytes;
        }
        
        {
            const int origUserRefNumMeasuredBytes = SizeOrigUserRefNum;
            if (origUserRefNumMeasuredBytes < Position.Minimum || origUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += origUserRefNumMeasuredBytes;
        }
        
        {
            const int replacedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (replacedMessageUserRefNumMeasuredBytes < Position.Minimum || replacedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int replacedMessageSideMeasuredBytes = SizeSide;
            if (replacedMessageSideMeasuredBytes < Position.Minimum || replacedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageSideMeasuredBytes;
        }
        
        {
            const int replacedMessageQuantityMeasuredBytes = SizeQuantity;
            if (replacedMessageQuantityMeasuredBytes < Position.Minimum || replacedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageQuantityMeasuredBytes;
        }
        
        {
            const int replacedMessageSymbolMeasuredBytes = SizeSymbol;
            if (replacedMessageSymbolMeasuredBytes < Position.Minimum || replacedMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageSymbolMeasuredBytes;
        }
        
        {
            const int replacedMessagePriceMeasuredBytes = SizePrice;
            if (replacedMessagePriceMeasuredBytes < Position.Minimum || replacedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessagePriceMeasuredBytes;
        }
        
        {
            const int replacedMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (replacedMessageTimeInForceMeasuredBytes < Position.Minimum || replacedMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int replacedMessageDisplayMeasuredBytes = SizeDisplay;
            if (replacedMessageDisplayMeasuredBytes < Position.Minimum || replacedMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageDisplayMeasuredBytes;
        }
        
        {
            const int replacedMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (replacedMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || replacedMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            const int replacedMessageCapacityMeasuredBytes = SizeCapacity;
            if (replacedMessageCapacityMeasuredBytes < Position.Minimum || replacedMessageCapacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageCapacityMeasuredBytes;
        }
        
        {
            const int replacedMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (replacedMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || replacedMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int replacedMessageCrossTypeMeasuredBytes = SizeCrossType;
            if (replacedMessageCrossTypeMeasuredBytes < Position.Minimum || replacedMessageCrossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageCrossTypeMeasuredBytes;
        }
        
        {
            const int replacedMessageOrderStateMeasuredBytes = SizeOrderState;
            if (replacedMessageOrderStateMeasuredBytes < Position.Minimum || replacedMessageOrderStateMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageOrderStateMeasuredBytes;
        }
        
        {
            const int replacedMessageClordidMeasuredBytes = SizeClordid;
            if (replacedMessageClordidMeasuredBytes < Position.Minimum || replacedMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageClordidMeasuredBytes;
        }
        
        {
            const int replacedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (replacedMessageAppendageLengthMeasuredBytes < Position.Minimum || replacedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replacedMessageAppendageLengthMeasuredBytes;
        }
        replacedMessageAppendageLengthDependencyReadPosition = (long)index + replacedMessageAppendageLengthDependencyReadDistanceFromReplacedMessageAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replacedMessageAppendageLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageLengthDependencyReadPosition > limit || replacedMessageAppendageLengthDependencyReadWidth > limit - replacedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        replacedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replacedMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            ReplacedMessageAppendageCountRule0RawEnd = ((replacedMessageAppendageLengthValue) switch { var ReplacedMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplacedMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (ReplacedMessageAppendageCountRule0RawEnd < index || ReplacedMessageAppendageCountRule0RawEnd > limit) return Position.Failure;
        ReplacedMessageAppendageCountRule0End = (int)ReplacedMessageAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = ReplacedMessageAppendageCountRule0End;
        return index;
    }
    internal static int SelectOrderAcceptedOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool OrderAcceptedOptionalValueBranchRule0Selected;
        bool OrderAcceptedOptionalValueBranchRule1Selected;
        bool OrderAcceptedOptionalValueBranchRule2Selected;
        bool OrderAcceptedOptionalValueBranchRule3Selected;
        bool OrderAcceptedOptionalValueBranchRule4Selected;
        bool OrderAcceptedOptionalValueBranchRule5Selected;
        bool OrderAcceptedOptionalValueBranchRule6Selected;
        bool OrderAcceptedOptionalValueBranchRule7Selected;
        bool OrderAcceptedOptionalValueBranchRule8Selected;
        bool OrderAcceptedOptionalValueBranchRule9Selected;
        bool OrderAcceptedOptionalValueBranchRule10Selected;
        bool OrderAcceptedOptionalValueBranchRule11Selected;
        bool OrderAcceptedOptionalValueBranchRule12Selected;
        bool OrderAcceptedOptionalValueBranchRule13Selected;
        bool OrderAcceptedOptionalValueBranchRule14Selected;
        bool OrderAcceptedOptionalValueBranchRule15Selected;
        bool OrderAcceptedOptionalValueBranchRule16Selected;
        bool OrderAcceptedOptionalValueBranchRule17Selected;
        bool OrderAcceptedOptionalValueBranchRule18Selected;
        bool OrderAcceptedOptionalValueBranchRule19Selected;
        bool OrderAcceptedOptionalValueBranchRule20Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte OrderAcceptedOptionalValueBranchRule0Data2 = 2;
            try
            {
                OrderAcceptedOptionalValueBranchRule0Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule1Data2 = 3;
            try
            {
                OrderAcceptedOptionalValueBranchRule1Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule2Data2 = 4;
            try
            {
                OrderAcceptedOptionalValueBranchRule2Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule3Data2 = 5;
            try
            {
                OrderAcceptedOptionalValueBranchRule3Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule4Data2 = 6;
            try
            {
                OrderAcceptedOptionalValueBranchRule4Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule5Data2 = 7;
            try
            {
                OrderAcceptedOptionalValueBranchRule5Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule6Data2 = 9;
            try
            {
                OrderAcceptedOptionalValueBranchRule6Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule7Data2 = 10;
            try
            {
                OrderAcceptedOptionalValueBranchRule7Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule8Data2 = 11;
            try
            {
                OrderAcceptedOptionalValueBranchRule8Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule9Data2 = 12;
            try
            {
                OrderAcceptedOptionalValueBranchRule9Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule10Data2 = 13;
            try
            {
                OrderAcceptedOptionalValueBranchRule10Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule11Data2 = 14;
            try
            {
                OrderAcceptedOptionalValueBranchRule11Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule12Data2 = 15;
            try
            {
                OrderAcceptedOptionalValueBranchRule12Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule13Data2 = 16;
            try
            {
                OrderAcceptedOptionalValueBranchRule13Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule14Data2 = 17;
            try
            {
                OrderAcceptedOptionalValueBranchRule14Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule15Data2 = 18;
            try
            {
                OrderAcceptedOptionalValueBranchRule15Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule16Data2 = 24;
            try
            {
                OrderAcceptedOptionalValueBranchRule16Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule17Data2 = 25;
            try
            {
                OrderAcceptedOptionalValueBranchRule17Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule18Data2 = 28;
            try
            {
                OrderAcceptedOptionalValueBranchRule18Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule18Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule18Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule19Data2 = 29;
            try
            {
                OrderAcceptedOptionalValueBranchRule19Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule19Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule19Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy;
            }
        }
        
        {
            const byte OrderAcceptedOptionalValueBranchRule20Data2 = 30;
            try
            {
                OrderAcceptedOptionalValueBranchRule20Selected = (dependency0 == OrderAcceptedOptionalValueBranchRule20Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (OrderAcceptedOptionalValueBranchRule20Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.None;
    }
    internal static int OrderAcceptedAppendageOrderAcceptedOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int orderAcceptedOptionalFieldDependencyReadDistanceFromOrderAcceptedOptionalValue = -1;
        const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
        long orderAcceptedOptionalFieldDependencyReadPosition;
        byte orderAcceptedOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderAcceptedAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (orderAcceptedAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || orderAcceptedAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int orderAcceptedOptionalFieldMeasuredBytes = SizeOrderAcceptedOptionalField;
            if (orderAcceptedOptionalFieldMeasuredBytes < Position.Minimum || orderAcceptedOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedOptionalFieldMeasuredBytes;
        }
        orderAcceptedOptionalFieldDependencyReadPosition = (long)index + orderAcceptedOptionalFieldDependencyReadDistanceFromOrderAcceptedOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > limit || orderAcceptedOptionalFieldDependencyReadWidth > limit - orderAcceptedOptionalFieldDependencyReadPosition) return Position.Failure;
        orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
        received0 = orderAcceptedOptionalFieldValue;
        return index;
    }
    internal static int OrderAcceptedMessageOrderAcceptedAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int orderAcceptedMessageAppendageLengthDependencyReadDistanceFromOrderAcceptedAppendage = -2;
        const int orderAcceptedMessageAppendageLengthDependencyReadWidth = 2;
        long orderAcceptedMessageAppendageLengthDependencyReadPosition;
        ushort orderAcceptedMessageAppendageLengthValue;
        global::System.Int128 OrderAcceptedAppendageCountRule0RawEnd;
        int OrderAcceptedAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int orderAcceptedMessageTimestampMeasuredBytes = SizeTimestamp;
            if (orderAcceptedMessageTimestampMeasuredBytes < Position.Minimum || orderAcceptedMessageTimestampMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageTimestampMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (orderAcceptedMessageUserRefNumMeasuredBytes < Position.Minimum || orderAcceptedMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageSideMeasuredBytes = SizeSide;
            if (orderAcceptedMessageSideMeasuredBytes < Position.Minimum || orderAcceptedMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageSideMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageQuantityMeasuredBytes = SizeQuantity;
            if (orderAcceptedMessageQuantityMeasuredBytes < Position.Minimum || orderAcceptedMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageQuantityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageSymbolMeasuredBytes = SizeSymbol;
            if (orderAcceptedMessageSymbolMeasuredBytes < Position.Minimum || orderAcceptedMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageSymbolMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessagePriceMeasuredBytes = SizePrice;
            if (orderAcceptedMessagePriceMeasuredBytes < Position.Minimum || orderAcceptedMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessagePriceMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (orderAcceptedMessageTimeInForceMeasuredBytes < Position.Minimum || orderAcceptedMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageDisplayMeasuredBytes = SizeDisplay;
            if (orderAcceptedMessageDisplayMeasuredBytes < Position.Minimum || orderAcceptedMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageDisplayMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageOrderReferenceNumberMeasuredBytes = SizeOrderReferenceNumber;
            if (orderAcceptedMessageOrderReferenceNumberMeasuredBytes < Position.Minimum || orderAcceptedMessageOrderReferenceNumberMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageOrderReferenceNumberMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageCapacityMeasuredBytes = SizeCapacity;
            if (orderAcceptedMessageCapacityMeasuredBytes < Position.Minimum || orderAcceptedMessageCapacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageCapacityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageCrossTypeMeasuredBytes = SizeCrossType;
            if (orderAcceptedMessageCrossTypeMeasuredBytes < Position.Minimum || orderAcceptedMessageCrossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageCrossTypeMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageOrderStateMeasuredBytes = SizeOrderState;
            if (orderAcceptedMessageOrderStateMeasuredBytes < Position.Minimum || orderAcceptedMessageOrderStateMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageOrderStateMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageClordidMeasuredBytes = SizeClordid;
            if (orderAcceptedMessageClordidMeasuredBytes < Position.Minimum || orderAcceptedMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageClordidMeasuredBytes;
        }
        
        {
            const int orderAcceptedMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (orderAcceptedMessageAppendageLengthMeasuredBytes < Position.Minimum || orderAcceptedMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += orderAcceptedMessageAppendageLengthMeasuredBytes;
        }
        orderAcceptedMessageAppendageLengthDependencyReadPosition = (long)index + orderAcceptedMessageAppendageLengthDependencyReadDistanceFromOrderAcceptedAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || orderAcceptedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderAcceptedMessageAppendageLengthDependencyReadPosition > limit || orderAcceptedMessageAppendageLengthDependencyReadWidth > limit - orderAcceptedMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        orderAcceptedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderAcceptedMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            OrderAcceptedAppendageCountRule0RawEnd = ((orderAcceptedMessageAppendageLengthValue) switch { var OrderAcceptedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderAcceptedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (OrderAcceptedAppendageCountRule0RawEnd < index || OrderAcceptedAppendageCountRule0RawEnd > limit) return Position.Failure;
        OrderAcceptedAppendageCountRule0End = (int)OrderAcceptedAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = OrderAcceptedAppendageCountRule0End;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = "A"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = "U"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = "C"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = "D"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = "E"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = "B"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = "J"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = "P"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = "I"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = "T"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = "M"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = "R"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = "Q"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = "G"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = "K"u8;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.None;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.DebugPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginAcceptedPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginRejectedPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.SequencedDataPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.ServerHeartbeat;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.EndOfSession;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.None;
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
    internal static int SelectEnableOrderEntryRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnableOrderEntryRequestOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnableOrderEntryRequestOptionalValueBranchRule0Data2 = 28;
            try
            {
                EnableOrderEntryRequestOptionalValueBranchRule0Selected = (dependency0 == EnableOrderEntryRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnableOrderEntryRequestOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Case.None;
    }
    internal static int EnableOrderEntryRequestAppendageEnableOrderEntryRequestOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int enableOrderEntryRequestOptionalFieldDependencyReadDistanceFromEnableOrderEntryRequestOptionalValue = -1;
        const int enableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
        long enableOrderEntryRequestOptionalFieldDependencyReadPosition;
        byte enableOrderEntryRequestOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (enableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || enableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int enableOrderEntryRequestOptionalFieldMeasuredBytes = SizeEnableOrderEntryRequestOptionalField;
            if (enableOrderEntryRequestOptionalFieldMeasuredBytes < Position.Minimum || enableOrderEntryRequestOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestOptionalFieldMeasuredBytes;
        }
        enableOrderEntryRequestOptionalFieldDependencyReadPosition = (long)index + enableOrderEntryRequestOptionalFieldDependencyReadDistanceFromEnableOrderEntryRequestOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryRequestOptionalFieldDependencyReadPosition > limit || enableOrderEntryRequestOptionalFieldDependencyReadWidth > limit - enableOrderEntryRequestOptionalFieldDependencyReadPosition) return Position.Failure;
        enableOrderEntryRequestOptionalFieldValue = bytes[(int)enableOrderEntryRequestOptionalFieldDependencyReadPosition];
        received0 = enableOrderEntryRequestOptionalFieldValue;
        return index;
    }
    internal static int EnableOrderEntryRequestMessageEnableOrderEntryRequestAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int enableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage = -2;
        const int enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
        long enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
        ushort enableOrderEntryRequestMessageAppendageLengthValue;
        global::System.Int128 EnableOrderEntryRequestAppendageCountRule0RawEnd;
        int EnableOrderEntryRequestAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enableOrderEntryRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enableOrderEntryRequestMessageUserRefNumMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enableOrderEntryRequestMessageFirmMeasuredBytes = SizeFirm;
            if (enableOrderEntryRequestMessageFirmMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int enableOrderEntryRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enableOrderEntryRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || enableOrderEntryRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enableOrderEntryRequestMessageAppendageLengthMeasuredBytes;
        }
        enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = (long)index + enableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromEnableOrderEntryRequestAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > limit || enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > limit - enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        enableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            EnableOrderEntryRequestAppendageCountRule0RawEnd = ((enableOrderEntryRequestMessageAppendageLengthValue) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (EnableOrderEntryRequestAppendageCountRule0RawEnd < index || EnableOrderEntryRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
        EnableOrderEntryRequestAppendageCountRule0End = (int)EnableOrderEntryRequestAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = EnableOrderEntryRequestAppendageCountRule0End;
        return index;
    }
    internal static int SelectDisableOrderEntryRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool DisableOrderEntryRequestOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte DisableOrderEntryRequestOptionalValueBranchRule0Data2 = 28;
            try
            {
                DisableOrderEntryRequestOptionalValueBranchRule0Selected = (dependency0 == DisableOrderEntryRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (DisableOrderEntryRequestOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Case.None;
    }
    internal static int DisableOrderEntryRequestAppendageDisableOrderEntryRequestOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int disableOrderEntryRequestOptionalFieldDependencyReadDistanceFromDisableOrderEntryRequestOptionalValue = -1;
        const int disableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
        long disableOrderEntryRequestOptionalFieldDependencyReadPosition;
        byte disableOrderEntryRequestOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (disableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || disableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int disableOrderEntryRequestOptionalFieldMeasuredBytes = SizeDisableOrderEntryRequestOptionalField;
            if (disableOrderEntryRequestOptionalFieldMeasuredBytes < Position.Minimum || disableOrderEntryRequestOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestOptionalFieldMeasuredBytes;
        }
        disableOrderEntryRequestOptionalFieldDependencyReadPosition = (long)index + disableOrderEntryRequestOptionalFieldDependencyReadDistanceFromDisableOrderEntryRequestOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryRequestOptionalFieldDependencyReadPosition > limit || disableOrderEntryRequestOptionalFieldDependencyReadWidth > limit - disableOrderEntryRequestOptionalFieldDependencyReadPosition) return Position.Failure;
        disableOrderEntryRequestOptionalFieldValue = bytes[(int)disableOrderEntryRequestOptionalFieldDependencyReadPosition];
        received0 = disableOrderEntryRequestOptionalFieldValue;
        return index;
    }
    internal static int DisableOrderEntryRequestMessageDisableOrderEntryRequestAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int disableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage = -2;
        const int disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
        long disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
        ushort disableOrderEntryRequestMessageAppendageLengthValue;
        global::System.Int128 DisableOrderEntryRequestAppendageCountRule0RawEnd;
        int DisableOrderEntryRequestAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int disableOrderEntryRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (disableOrderEntryRequestMessageUserRefNumMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int disableOrderEntryRequestMessageFirmMeasuredBytes = SizeFirm;
            if (disableOrderEntryRequestMessageFirmMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int disableOrderEntryRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (disableOrderEntryRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || disableOrderEntryRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += disableOrderEntryRequestMessageAppendageLengthMeasuredBytes;
        }
        disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = (long)index + disableOrderEntryRequestMessageAppendageLengthDependencyReadDistanceFromDisableOrderEntryRequestAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > limit || disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > limit - disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        disableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            DisableOrderEntryRequestAppendageCountRule0RawEnd = ((disableOrderEntryRequestMessageAppendageLengthValue) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (DisableOrderEntryRequestAppendageCountRule0RawEnd < index || DisableOrderEntryRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
        DisableOrderEntryRequestAppendageCountRule0End = (int)DisableOrderEntryRequestAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = DisableOrderEntryRequestAppendageCountRule0End;
        return index;
    }
    internal static int SelectMassCancelRequestOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool MassCancelRequestOptionalValueBranchRule0Selected;
        bool MassCancelRequestOptionalValueBranchRule1Selected;
        bool MassCancelRequestOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte MassCancelRequestOptionalValueBranchRule0Data2 = 27;
            try
            {
                MassCancelRequestOptionalValueBranchRule0Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side;
            }
        }
        
        {
            const byte MassCancelRequestOptionalValueBranchRule1Data2 = 24;
            try
            {
                MassCancelRequestOptionalValueBranchRule1Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId;
            }
        }
        
        {
            const byte MassCancelRequestOptionalValueBranchRule2Data2 = 28;
            try
            {
                MassCancelRequestOptionalValueBranchRule2Selected = (dependency0 == MassCancelRequestOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (MassCancelRequestOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.None;
    }
    internal static int MassCancelRequestAppendageMassCancelRequestOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int massCancelRequestOptionalFieldDependencyReadDistanceFromMassCancelRequestOptionalValue = -1;
        const int massCancelRequestOptionalFieldDependencyReadWidth = 1;
        long massCancelRequestOptionalFieldDependencyReadPosition;
        byte massCancelRequestOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelRequestAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (massCancelRequestAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || massCancelRequestAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int massCancelRequestOptionalFieldMeasuredBytes = SizeMassCancelRequestOptionalField;
            if (massCancelRequestOptionalFieldMeasuredBytes < Position.Minimum || massCancelRequestOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestOptionalFieldMeasuredBytes;
        }
        massCancelRequestOptionalFieldDependencyReadPosition = (long)index + massCancelRequestOptionalFieldDependencyReadDistanceFromMassCancelRequestOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelRequestOptionalFieldDependencyReadPosition < Position.Minimum || massCancelRequestOptionalFieldDependencyReadPosition > limit || massCancelRequestOptionalFieldDependencyReadWidth > limit - massCancelRequestOptionalFieldDependencyReadPosition) return Position.Failure;
        massCancelRequestOptionalFieldValue = bytes[(int)massCancelRequestOptionalFieldDependencyReadPosition];
        received0 = massCancelRequestOptionalFieldValue;
        return index;
    }
    internal static int MassCancelRequestMessageMassCancelRequestAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int massCancelRequestMessageAppendageLengthDependencyReadDistanceFromMassCancelRequestAppendage = -2;
        const int massCancelRequestMessageAppendageLengthDependencyReadWidth = 2;
        long massCancelRequestMessageAppendageLengthDependencyReadPosition;
        ushort massCancelRequestMessageAppendageLengthValue;
        global::System.Int128 MassCancelRequestAppendageCountRule0RawEnd;
        int MassCancelRequestAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int massCancelRequestMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (massCancelRequestMessageUserRefNumMeasuredBytes < Position.Minimum || massCancelRequestMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageFirmMeasuredBytes = SizeFirm;
            if (massCancelRequestMessageFirmMeasuredBytes < Position.Minimum || massCancelRequestMessageFirmMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageFirmMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageSymbolMeasuredBytes = SizeSymbol;
            if (massCancelRequestMessageSymbolMeasuredBytes < Position.Minimum || massCancelRequestMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageSymbolMeasuredBytes;
        }
        
        {
            const int massCancelRequestMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (massCancelRequestMessageAppendageLengthMeasuredBytes < Position.Minimum || massCancelRequestMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += massCancelRequestMessageAppendageLengthMeasuredBytes;
        }
        massCancelRequestMessageAppendageLengthDependencyReadPosition = (long)index + massCancelRequestMessageAppendageLengthDependencyReadDistanceFromMassCancelRequestAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || massCancelRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelRequestMessageAppendageLengthDependencyReadPosition > limit || massCancelRequestMessageAppendageLengthDependencyReadWidth > limit - massCancelRequestMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        massCancelRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelRequestMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            MassCancelRequestAppendageCountRule0RawEnd = ((massCancelRequestMessageAppendageLengthValue) switch { var MassCancelRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (MassCancelRequestAppendageCountRule0RawEnd < index || MassCancelRequestAppendageCountRule0RawEnd > limit) return Position.Failure;
        MassCancelRequestAppendageCountRule0End = (int)MassCancelRequestAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = MassCancelRequestAppendageCountRule0End;
        return index;
    }
    internal static int SelectAccountQueryOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool AccountQueryOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte AccountQueryOptionalValueBranchRule0Data2 = 28;
            try
            {
                AccountQueryOptionalValueBranchRule0Selected = (dependency0 == AccountQueryOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AccountQueryOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Case.None;
    }
    internal static int AccountQueryAppendageAccountQueryOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int accountQueryOptionalFieldDependencyReadDistanceFromAccountQueryOptionalValue = -1;
        const int accountQueryOptionalFieldDependencyReadWidth = 1;
        long accountQueryOptionalFieldDependencyReadPosition;
        byte accountQueryOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int accountQueryAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (accountQueryAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || accountQueryAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int accountQueryOptionalFieldMeasuredBytes = SizeAccountQueryOptionalField;
            if (accountQueryOptionalFieldMeasuredBytes < Position.Minimum || accountQueryOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += accountQueryOptionalFieldMeasuredBytes;
        }
        accountQueryOptionalFieldDependencyReadPosition = (long)index + accountQueryOptionalFieldDependencyReadDistanceFromAccountQueryOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryOptionalFieldDependencyReadPosition > limit || accountQueryOptionalFieldDependencyReadWidth > limit - accountQueryOptionalFieldDependencyReadPosition) return Position.Failure;
        accountQueryOptionalFieldValue = bytes[(int)accountQueryOptionalFieldDependencyReadPosition];
        received0 = accountQueryOptionalFieldValue;
        return index;
    }
    internal static int AccountQueryMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int AccountQueryMessageAccountQueryAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int accountQueryMessageAppendageLengthDependencyReadDistanceFromAccountQueryMessageAppendageLength = 0;
        const int accountQueryMessageAppendageLengthDependencyReadWidth = 2;
        long accountQueryMessageAppendageLengthDependencyReadPosition = default;
        ushort accountQueryMessageAppendageLengthValue = default;
        global::System.Int128 AccountQueryAppendageCountRule0RawEnd;
        int AccountQueryAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                accountQueryMessageAppendageLengthDependencyReadPosition = (long)index + accountQueryMessageAppendageLengthDependencyReadDistanceFromAccountQueryMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || accountQueryMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryMessageAppendageLengthDependencyReadPosition > limit || accountQueryMessageAppendageLengthDependencyReadWidth > limit - accountQueryMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                accountQueryMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int accountQueryMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (accountQueryMessageAppendageLengthMeasuredBytes < Position.Minimum || accountQueryMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += accountQueryMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            AccountQueryAppendageCountRule0RawEnd = ((accountQueryMessageAppendageLengthValue) switch { var AccountQueryAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (AccountQueryAppendageCountRule0RawEnd < index || AccountQueryAppendageCountRule0RawEnd > limit) return Position.Failure;
        AccountQueryAppendageCountRule0End = (int)AccountQueryAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = AccountQueryAppendageCountRule0End;
        return index;
    }
    internal static int SelectModifyOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ModifyOrderOptionalValueBranchRule0Selected;
        bool ModifyOrderOptionalValueBranchRule1Selected;
        bool ModifyOrderOptionalValueBranchRule2Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ModifyOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                ModifyOrderOptionalValueBranchRule0Selected = (dependency0 == ModifyOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte ModifyOrderOptionalValueBranchRule1Data2 = 25;
            try
            {
                ModifyOrderOptionalValueBranchRule1Selected = (dependency0 == ModifyOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated;
            }
        }
        
        {
            const byte ModifyOrderOptionalValueBranchRule2Data2 = 26;
            try
            {
                ModifyOrderOptionalValueBranchRule2Selected = (dependency0 == ModifyOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ModifyOrderOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.None;
    }
    internal static int ModifyOrderAppendageModifyOrderOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int modifyOrderOptionalFieldDependencyReadDistanceFromModifyOrderOptionalValue = -1;
        const int modifyOrderOptionalFieldDependencyReadWidth = 1;
        long modifyOrderOptionalFieldDependencyReadPosition;
        byte modifyOrderOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int modifyOrderAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (modifyOrderAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || modifyOrderAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int modifyOrderOptionalFieldMeasuredBytes = SizeModifyOrderOptionalField;
            if (modifyOrderOptionalFieldMeasuredBytes < Position.Minimum || modifyOrderOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderOptionalFieldMeasuredBytes;
        }
        modifyOrderOptionalFieldDependencyReadPosition = (long)index + modifyOrderOptionalFieldDependencyReadDistanceFromModifyOrderOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || modifyOrderOptionalFieldDependencyReadPosition < Position.Minimum || modifyOrderOptionalFieldDependencyReadPosition > limit || modifyOrderOptionalFieldDependencyReadWidth > limit - modifyOrderOptionalFieldDependencyReadPosition) return Position.Failure;
        modifyOrderOptionalFieldValue = bytes[(int)modifyOrderOptionalFieldDependencyReadPosition];
        received0 = modifyOrderOptionalFieldValue;
        return index;
    }
    internal static int ModifyOrderMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int modifyOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (modifyOrderMessageUserRefNumMeasuredBytes < Position.Minimum || modifyOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageSideMeasuredBytes = SizeSide;
            if (modifyOrderMessageSideMeasuredBytes < Position.Minimum || modifyOrderMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageSideMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (modifyOrderMessageQuantityMeasuredBytes < Position.Minimum || modifyOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int ModifyOrderMessageModifyOrderAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int modifyOrderMessageAppendageLengthDependencyReadDistanceFromModifyOrderMessageAppendageLength = 0;
        const int modifyOrderMessageAppendageLengthDependencyReadWidth = 2;
        long modifyOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort modifyOrderMessageAppendageLengthValue = default;
        global::System.Int128 ModifyOrderAppendageCountRule0RawEnd;
        int ModifyOrderAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int modifyOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (modifyOrderMessageUserRefNumMeasuredBytes < Position.Minimum || modifyOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageSideMeasuredBytes = SizeSide;
            if (modifyOrderMessageSideMeasuredBytes < Position.Minimum || modifyOrderMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageSideMeasuredBytes;
        }
        
        {
            const int modifyOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (modifyOrderMessageQuantityMeasuredBytes < Position.Minimum || modifyOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += modifyOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                modifyOrderMessageAppendageLengthDependencyReadPosition = (long)index + modifyOrderMessageAppendageLengthDependencyReadDistanceFromModifyOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || modifyOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || modifyOrderMessageAppendageLengthDependencyReadPosition > limit || modifyOrderMessageAppendageLengthDependencyReadWidth > limit - modifyOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                modifyOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)modifyOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int modifyOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (modifyOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || modifyOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += modifyOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            ModifyOrderAppendageCountRule0RawEnd = ((modifyOrderMessageAppendageLengthValue) switch { var ModifyOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ModifyOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (ModifyOrderAppendageCountRule0RawEnd < index || ModifyOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
        ModifyOrderAppendageCountRule0End = (int)ModifyOrderAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = ModifyOrderAppendageCountRule0End;
        return index;
    }
    internal static int SelectCancelOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool CancelOrderOptionalValueBranchRule0Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte CancelOrderOptionalValueBranchRule0Data2 = 28;
            try
            {
                CancelOrderOptionalValueBranchRule0Selected = (dependency0 == CancelOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (CancelOrderOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Case.UserRefIdx;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Case.None;
    }
    internal static int CancelOrderAppendageCancelOrderOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int cancelOrderOptionalFieldDependencyReadDistanceFromCancelOrderOptionalValue = -1;
        const int cancelOrderOptionalFieldDependencyReadWidth = 1;
        long cancelOrderOptionalFieldDependencyReadPosition;
        byte cancelOrderOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelOrderAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (cancelOrderAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || cancelOrderAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int cancelOrderOptionalFieldMeasuredBytes = SizeCancelOrderOptionalField;
            if (cancelOrderOptionalFieldMeasuredBytes < Position.Minimum || cancelOrderOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderOptionalFieldMeasuredBytes;
        }
        cancelOrderOptionalFieldDependencyReadPosition = (long)index + cancelOrderOptionalFieldDependencyReadDistanceFromCancelOrderOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelOrderOptionalFieldDependencyReadPosition < Position.Minimum || cancelOrderOptionalFieldDependencyReadPosition > limit || cancelOrderOptionalFieldDependencyReadWidth > limit - cancelOrderOptionalFieldDependencyReadPosition) return Position.Failure;
        cancelOrderOptionalFieldValue = bytes[(int)cancelOrderOptionalFieldDependencyReadPosition];
        received0 = cancelOrderOptionalFieldValue;
        return index;
    }
    internal static int CancelOrderMessageAppendageLengthStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart)
    {
        bool AppendageLengthOptionalRule0Present;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelOrderMessageUserRefNumMeasuredBytes < Position.Minimum || cancelOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int cancelOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (cancelOrderMessageQuantityMeasuredBytes < Position.Minimum || cancelOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                return index;
            }
        }
        return Position.Absent;
    }
    internal static int CancelOrderMessageCancelOrderAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        bool AppendageLengthOptionalRule0Present;
        const int cancelOrderMessageAppendageLengthDependencyReadDistanceFromCancelOrderMessageAppendageLength = 0;
        const int cancelOrderMessageAppendageLengthDependencyReadWidth = 2;
        long cancelOrderMessageAppendageLengthDependencyReadPosition = default;
        ushort cancelOrderMessageAppendageLengthValue = default;
        global::System.Int128 CancelOrderAppendageCountRule0RawEnd;
        int CancelOrderAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int cancelOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (cancelOrderMessageUserRefNumMeasuredBytes < Position.Minimum || cancelOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int cancelOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (cancelOrderMessageQuantityMeasuredBytes < Position.Minimum || cancelOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += cancelOrderMessageQuantityMeasuredBytes;
        }
        
        {
            try
            {
                AppendageLengthOptionalRule0Present = index < limit;
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (AppendageLengthOptionalRule0Present)
            {
                cancelOrderMessageAppendageLengthDependencyReadPosition = (long)index + cancelOrderMessageAppendageLengthDependencyReadDistanceFromCancelOrderMessageAppendageLength;
                if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || cancelOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelOrderMessageAppendageLengthDependencyReadPosition > limit || cancelOrderMessageAppendageLengthDependencyReadWidth > limit - cancelOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
                cancelOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelOrderMessageAppendageLengthDependencyReadPosition)..]);
                
                {
                    const int cancelOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
                    if (cancelOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || cancelOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
                    index += cancelOrderMessageAppendageLengthMeasuredBytes;
                }
            }
        }
        try
        {
            CancelOrderAppendageCountRule0RawEnd = ((cancelOrderMessageAppendageLengthValue) switch { var CancelOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (CancelOrderAppendageCountRule0RawEnd < index || CancelOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
        CancelOrderAppendageCountRule0End = (int)CancelOrderAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = CancelOrderAppendageCountRule0End;
        return index;
    }
    internal static int SelectReplaceOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool ReplaceOrderOptionalValueBranchRule0Selected;
        bool ReplaceOrderOptionalValueBranchRule1Selected;
        bool ReplaceOrderOptionalValueBranchRule2Selected;
        bool ReplaceOrderOptionalValueBranchRule3Selected;
        bool ReplaceOrderOptionalValueBranchRule4Selected;
        bool ReplaceOrderOptionalValueBranchRule5Selected;
        bool ReplaceOrderOptionalValueBranchRule6Selected;
        bool ReplaceOrderOptionalValueBranchRule7Selected;
        bool ReplaceOrderOptionalValueBranchRule8Selected;
        bool ReplaceOrderOptionalValueBranchRule9Selected;
        bool ReplaceOrderOptionalValueBranchRule10Selected;
        bool ReplaceOrderOptionalValueBranchRule11Selected;
        bool ReplaceOrderOptionalValueBranchRule12Selected;
        bool ReplaceOrderOptionalValueBranchRule13Selected;
        bool ReplaceOrderOptionalValueBranchRule14Selected;
        bool ReplaceOrderOptionalValueBranchRule15Selected;
        bool ReplaceOrderOptionalValueBranchRule16Selected;
        bool ReplaceOrderOptionalValueBranchRule17Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte ReplaceOrderOptionalValueBranchRule0Data2 = 3;
            try
            {
                ReplaceOrderOptionalValueBranchRule0Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule1Data2 = 5;
            try
            {
                ReplaceOrderOptionalValueBranchRule1Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule2Data2 = 6;
            try
            {
                ReplaceOrderOptionalValueBranchRule2Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule3Data2 = 12;
            try
            {
                ReplaceOrderOptionalValueBranchRule3Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule4Data2 = 15;
            try
            {
                ReplaceOrderOptionalValueBranchRule4Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule5Data2 = 16;
            try
            {
                ReplaceOrderOptionalValueBranchRule5Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule6Data2 = 17;
            try
            {
                ReplaceOrderOptionalValueBranchRule6Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule7Data2 = 13;
            try
            {
                ReplaceOrderOptionalValueBranchRule7Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule8Data2 = 7;
            try
            {
                ReplaceOrderOptionalValueBranchRule8Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule9Data2 = 9;
            try
            {
                ReplaceOrderOptionalValueBranchRule9Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule10Data2 = 10;
            try
            {
                ReplaceOrderOptionalValueBranchRule10Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule11Data2 = 11;
            try
            {
                ReplaceOrderOptionalValueBranchRule11Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule12Data2 = 25;
            try
            {
                ReplaceOrderOptionalValueBranchRule12Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule13Data2 = 26;
            try
            {
                ReplaceOrderOptionalValueBranchRule13Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule14Data2 = 28;
            try
            {
                ReplaceOrderOptionalValueBranchRule14Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule15Data2 = 27;
            try
            {
                ReplaceOrderOptionalValueBranchRule15Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule16Data2 = 29;
            try
            {
                ReplaceOrderOptionalValueBranchRule16Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy;
            }
        }
        
        {
            const byte ReplaceOrderOptionalValueBranchRule17Data2 = 30;
            try
            {
                ReplaceOrderOptionalValueBranchRule17Selected = (dependency0 == ReplaceOrderOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (ReplaceOrderOptionalValueBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.None;
    }
    internal static int ReplaceOrderAppendageReplaceOrderOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int replaceOrderOptionalFieldDependencyReadDistanceFromReplaceOrderOptionalValue = -1;
        const int replaceOrderOptionalFieldDependencyReadWidth = 1;
        long replaceOrderOptionalFieldDependencyReadPosition;
        byte replaceOrderOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int replaceOrderAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (replaceOrderAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || replaceOrderAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int replaceOrderOptionalFieldMeasuredBytes = SizeReplaceOrderOptionalField;
            if (replaceOrderOptionalFieldMeasuredBytes < Position.Minimum || replaceOrderOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderOptionalFieldMeasuredBytes;
        }
        replaceOrderOptionalFieldDependencyReadPosition = (long)index + replaceOrderOptionalFieldDependencyReadDistanceFromReplaceOrderOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > limit || replaceOrderOptionalFieldDependencyReadWidth > limit - replaceOrderOptionalFieldDependencyReadPosition) return Position.Failure;
        replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
        received0 = replaceOrderOptionalFieldValue;
        return index;
    }
    internal static int ReplaceOrderMessageReplaceOrderAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int replaceOrderMessageAppendageLengthDependencyReadDistanceFromReplaceOrderAppendage = -2;
        const int replaceOrderMessageAppendageLengthDependencyReadWidth = 2;
        long replaceOrderMessageAppendageLengthDependencyReadPosition;
        ushort replaceOrderMessageAppendageLengthValue;
        global::System.Int128 ReplaceOrderAppendageCountRule0RawEnd;
        int ReplaceOrderAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int origUserRefNumMeasuredBytes = SizeOrigUserRefNum;
            if (origUserRefNumMeasuredBytes < Position.Minimum || origUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += origUserRefNumMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (replaceOrderMessageUserRefNumMeasuredBytes < Position.Minimum || replaceOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (replaceOrderMessageQuantityMeasuredBytes < Position.Minimum || replaceOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageQuantityMeasuredBytes;
        }
        
        {
            const int replaceOrderMessagePriceMeasuredBytes = SizePrice;
            if (replaceOrderMessagePriceMeasuredBytes < Position.Minimum || replaceOrderMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessagePriceMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (replaceOrderMessageTimeInForceMeasuredBytes < Position.Minimum || replaceOrderMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageDisplayMeasuredBytes = SizeDisplay;
            if (replaceOrderMessageDisplayMeasuredBytes < Position.Minimum || replaceOrderMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageDisplayMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageClordidMeasuredBytes = SizeClordid;
            if (replaceOrderMessageClordidMeasuredBytes < Position.Minimum || replaceOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageClordidMeasuredBytes;
        }
        
        {
            const int replaceOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (replaceOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || replaceOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += replaceOrderMessageAppendageLengthMeasuredBytes;
        }
        replaceOrderMessageAppendageLengthDependencyReadPosition = (long)index + replaceOrderMessageAppendageLengthDependencyReadDistanceFromReplaceOrderAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || replaceOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || replaceOrderMessageAppendageLengthDependencyReadPosition > limit || replaceOrderMessageAppendageLengthDependencyReadWidth > limit - replaceOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        replaceOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replaceOrderMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            ReplaceOrderAppendageCountRule0RawEnd = ((replaceOrderMessageAppendageLengthValue) switch { var ReplaceOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplaceOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (ReplaceOrderAppendageCountRule0RawEnd < index || ReplaceOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
        ReplaceOrderAppendageCountRule0End = (int)ReplaceOrderAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = ReplaceOrderAppendageCountRule0End;
        return index;
    }
    internal static int SelectEnterOrderOptionalValue(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, byte dependency0)
    {
        bool EnterOrderOptionalValueBranchRule0Selected;
        bool EnterOrderOptionalValueBranchRule1Selected;
        bool EnterOrderOptionalValueBranchRule2Selected;
        bool EnterOrderOptionalValueBranchRule3Selected;
        bool EnterOrderOptionalValueBranchRule4Selected;
        bool EnterOrderOptionalValueBranchRule5Selected;
        bool EnterOrderOptionalValueBranchRule6Selected;
        bool EnterOrderOptionalValueBranchRule7Selected;
        bool EnterOrderOptionalValueBranchRule8Selected;
        bool EnterOrderOptionalValueBranchRule9Selected;
        bool EnterOrderOptionalValueBranchRule10Selected;
        bool EnterOrderOptionalValueBranchRule11Selected;
        bool EnterOrderOptionalValueBranchRule12Selected;
        bool EnterOrderOptionalValueBranchRule13Selected;
        bool EnterOrderOptionalValueBranchRule14Selected;
        bool EnterOrderOptionalValueBranchRule15Selected;
        bool EnterOrderOptionalValueBranchRule16Selected;
        bool EnterOrderOptionalValueBranchRule17Selected;
        bool EnterOrderOptionalValueBranchRule18Selected;
        bool EnterOrderOptionalValueBranchRule19Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const byte EnterOrderOptionalValueBranchRule0Data2 = 2;
            try
            {
                EnterOrderOptionalValueBranchRule0Selected = (dependency0 == EnterOrderOptionalValueBranchRule0Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule0Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule1Data2 = 3;
            try
            {
                EnterOrderOptionalValueBranchRule1Selected = (dependency0 == EnterOrderOptionalValueBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule2Data2 = 4;
            try
            {
                EnterOrderOptionalValueBranchRule2Selected = (dependency0 == EnterOrderOptionalValueBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule3Data2 = 5;
            try
            {
                EnterOrderOptionalValueBranchRule3Selected = (dependency0 == EnterOrderOptionalValueBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule4Data2 = 6;
            try
            {
                EnterOrderOptionalValueBranchRule4Selected = (dependency0 == EnterOrderOptionalValueBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule5Data2 = 7;
            try
            {
                EnterOrderOptionalValueBranchRule5Selected = (dependency0 == EnterOrderOptionalValueBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule6Data2 = 9;
            try
            {
                EnterOrderOptionalValueBranchRule6Selected = (dependency0 == EnterOrderOptionalValueBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule7Data2 = 10;
            try
            {
                EnterOrderOptionalValueBranchRule7Selected = (dependency0 == EnterOrderOptionalValueBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule8Data2 = 11;
            try
            {
                EnterOrderOptionalValueBranchRule8Selected = (dependency0 == EnterOrderOptionalValueBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule9Data2 = 12;
            try
            {
                EnterOrderOptionalValueBranchRule9Selected = (dependency0 == EnterOrderOptionalValueBranchRule9Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule9Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule10Data2 = 13;
            try
            {
                EnterOrderOptionalValueBranchRule10Selected = (dependency0 == EnterOrderOptionalValueBranchRule10Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule10Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule11Data2 = 15;
            try
            {
                EnterOrderOptionalValueBranchRule11Selected = (dependency0 == EnterOrderOptionalValueBranchRule11Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule11Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule12Data2 = 16;
            try
            {
                EnterOrderOptionalValueBranchRule12Selected = (dependency0 == EnterOrderOptionalValueBranchRule12Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule12Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule13Data2 = 17;
            try
            {
                EnterOrderOptionalValueBranchRule13Selected = (dependency0 == EnterOrderOptionalValueBranchRule13Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule13Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule14Data2 = 24;
            try
            {
                EnterOrderOptionalValueBranchRule14Selected = (dependency0 == EnterOrderOptionalValueBranchRule14Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule14Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule15Data2 = 25;
            try
            {
                EnterOrderOptionalValueBranchRule15Selected = (dependency0 == EnterOrderOptionalValueBranchRule15Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule15Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule16Data2 = 26;
            try
            {
                EnterOrderOptionalValueBranchRule16Selected = (dependency0 == EnterOrderOptionalValueBranchRule16Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule16Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule17Data2 = 28;
            try
            {
                EnterOrderOptionalValueBranchRule17Selected = (dependency0 == EnterOrderOptionalValueBranchRule17Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule17Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule18Data2 = 29;
            try
            {
                EnterOrderOptionalValueBranchRule18Selected = (dependency0 == EnterOrderOptionalValueBranchRule18Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule18Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy;
            }
        }
        
        {
            const byte EnterOrderOptionalValueBranchRule19Data2 = 30;
            try
            {
                EnterOrderOptionalValueBranchRule19Selected = (dependency0 == EnterOrderOptionalValueBranchRule19Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (EnterOrderOptionalValueBranchRule19Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.None;
    }
    internal static int EnterOrderAppendageEnterOrderOptionalValueStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out byte received0)
    {
        received0 = default;
        const int enterOrderOptionalFieldDependencyReadDistanceFromEnterOrderOptionalValue = -1;
        const int enterOrderOptionalFieldDependencyReadWidth = 1;
        long enterOrderOptionalFieldDependencyReadPosition;
        byte enterOrderOptionalFieldValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enterOrderAppendageOptionalFieldLengthMeasuredBytes = SizeOptionalFieldLength;
            if (enterOrderAppendageOptionalFieldLengthMeasuredBytes < Position.Minimum || enterOrderAppendageOptionalFieldLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderAppendageOptionalFieldLengthMeasuredBytes;
        }
        
        {
            const int enterOrderOptionalFieldMeasuredBytes = SizeEnterOrderOptionalField;
            if (enterOrderOptionalFieldMeasuredBytes < Position.Minimum || enterOrderOptionalFieldMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderOptionalFieldMeasuredBytes;
        }
        enterOrderOptionalFieldDependencyReadPosition = (long)index + enterOrderOptionalFieldDependencyReadDistanceFromEnterOrderOptionalValue;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > limit || enterOrderOptionalFieldDependencyReadWidth > limit - enterOrderOptionalFieldDependencyReadPosition) return Position.Failure;
        enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
        received0 = enterOrderOptionalFieldValue;
        return index;
    }
    internal static int EnterOrderMessageEnterOrderAppendageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, out int received0, out int received1)
    {
        received0 = default;
        received1 = default;
        const int enterOrderMessageAppendageLengthDependencyReadDistanceFromEnterOrderAppendage = -2;
        const int enterOrderMessageAppendageLengthDependencyReadWidth = 2;
        long enterOrderMessageAppendageLengthDependencyReadPosition;
        ushort enterOrderMessageAppendageLengthValue;
        global::System.Int128 EnterOrderAppendageCountRule0RawEnd;
        int EnterOrderAppendageCountRule0End;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int enterOrderMessageUserRefNumMeasuredBytes = SizeUserRefNum;
            if (enterOrderMessageUserRefNumMeasuredBytes < Position.Minimum || enterOrderMessageUserRefNumMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageUserRefNumMeasuredBytes;
        }
        
        {
            const int enterOrderMessageSideMeasuredBytes = SizeSide;
            if (enterOrderMessageSideMeasuredBytes < Position.Minimum || enterOrderMessageSideMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageSideMeasuredBytes;
        }
        
        {
            const int enterOrderMessageQuantityMeasuredBytes = SizeQuantity;
            if (enterOrderMessageQuantityMeasuredBytes < Position.Minimum || enterOrderMessageQuantityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageQuantityMeasuredBytes;
        }
        
        {
            const int enterOrderMessageSymbolMeasuredBytes = SizeSymbol;
            if (enterOrderMessageSymbolMeasuredBytes < Position.Minimum || enterOrderMessageSymbolMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageSymbolMeasuredBytes;
        }
        
        {
            const int enterOrderMessagePriceMeasuredBytes = SizePrice;
            if (enterOrderMessagePriceMeasuredBytes < Position.Minimum || enterOrderMessagePriceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessagePriceMeasuredBytes;
        }
        
        {
            const int enterOrderMessageTimeInForceMeasuredBytes = SizeTimeInForce;
            if (enterOrderMessageTimeInForceMeasuredBytes < Position.Minimum || enterOrderMessageTimeInForceMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageTimeInForceMeasuredBytes;
        }
        
        {
            const int enterOrderMessageDisplayMeasuredBytes = SizeDisplay;
            if (enterOrderMessageDisplayMeasuredBytes < Position.Minimum || enterOrderMessageDisplayMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageDisplayMeasuredBytes;
        }
        
        {
            const int capacityMeasuredBytes = SizeCapacity;
            if (capacityMeasuredBytes < Position.Minimum || capacityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += capacityMeasuredBytes;
        }
        
        {
            const int enterOrderMessageInterMarketSweepEligibilityMeasuredBytes = SizeInterMarketSweepEligibility;
            if (enterOrderMessageInterMarketSweepEligibilityMeasuredBytes < Position.Minimum || enterOrderMessageInterMarketSweepEligibilityMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageInterMarketSweepEligibilityMeasuredBytes;
        }
        
        {
            const int crossTypeMeasuredBytes = SizeCrossType;
            if (crossTypeMeasuredBytes < Position.Minimum || crossTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += crossTypeMeasuredBytes;
        }
        
        {
            const int enterOrderMessageClordidMeasuredBytes = SizeClordid;
            if (enterOrderMessageClordidMeasuredBytes < Position.Minimum || enterOrderMessageClordidMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageClordidMeasuredBytes;
        }
        
        {
            const int enterOrderMessageAppendageLengthMeasuredBytes = SizeAppendageLength;
            if (enterOrderMessageAppendageLengthMeasuredBytes < Position.Minimum || enterOrderMessageAppendageLengthMeasuredBytes > (long)limit - index) return Position.Failure;
            index += enterOrderMessageAppendageLengthMeasuredBytes;
        }
        enterOrderMessageAppendageLengthDependencyReadPosition = (long)index + enterOrderMessageAppendageLengthDependencyReadDistanceFromEnterOrderAppendage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || enterOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || enterOrderMessageAppendageLengthDependencyReadPosition > limit || enterOrderMessageAppendageLengthDependencyReadWidth > limit - enterOrderMessageAppendageLengthDependencyReadPosition) return Position.Failure;
        enterOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enterOrderMessageAppendageLengthDependencyReadPosition)..]);
        try
        {
            EnterOrderAppendageCountRule0RawEnd = ((enterOrderMessageAppendageLengthValue) switch { var EnterOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnterOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticRight1)))) }) });
        }
        catch (global::System.OverflowException)
        {
            return Position.Failure;
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
        if (EnterOrderAppendageCountRule0RawEnd < index || EnterOrderAppendageCountRule0RawEnd > limit) return Position.Failure;
        EnterOrderAppendageCountRule0End = (int)EnterOrderAppendageCountRule0RawEnd;
        received0 = limit;
        received1 = EnterOrderAppendageCountRule0End;
        return index;
    }
    internal static int SelectUnsequencedMessage(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ReadOnlySpan<byte> dependency0)
    {
        bool UnsequencedMessageBranchRule1Selected;
        bool UnsequencedMessageBranchRule2Selected;
        bool UnsequencedMessageBranchRule3Selected;
        bool UnsequencedMessageBranchRule4Selected;
        bool UnsequencedMessageBranchRule5Selected;
        bool UnsequencedMessageBranchRule6Selected;
        bool UnsequencedMessageBranchRule7Selected;
        bool UnsequencedMessageBranchRule8Selected;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule1Data2 = "O"u8;
            try
            {
                UnsequencedMessageBranchRule1Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule1Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule1Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule2Data2 = "U"u8;
            try
            {
                UnsequencedMessageBranchRule2Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule2Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule2Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule3Data2 = "X"u8;
            try
            {
                UnsequencedMessageBranchRule3Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule3Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule3Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule4Data2 = "M"u8;
            try
            {
                UnsequencedMessageBranchRule4Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule4Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule4Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule5Data2 = "Q"u8;
            try
            {
                UnsequencedMessageBranchRule5Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule5Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule5Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule6Data2 = "C"u8;
            try
            {
                UnsequencedMessageBranchRule6Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule6Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule6Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule7Data2 = "D"u8;
            try
            {
                UnsequencedMessageBranchRule7Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule7Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule7Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage;
            }
        }
        
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule8Data2 = "E"u8;
            try
            {
                UnsequencedMessageBranchRule8Selected = dependency0.SequenceEqual(UnsequencedMessageBranchRule8Data2);
            }
            catch (global::System.OverflowException)
            {
                return Position.Failure;
            }
            catch (global::System.DivideByZeroException)
            {
                return Position.Failure;
            }
            catch (RuleValueException)
            {
                return Position.Failure;
            }
            if (UnsequencedMessageBranchRule8Selected)
            {
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.None;
    }
    internal static int UnsequencedDataPacketUnsequencedMessageStart(ReadOnlySpan<byte> bytes, int start, int limit, int parentStart, ushort dependency0, out ReadOnlySpan<byte> received0, out ushort received1)
    {
        received0 = default;
        received1 = default;
        const int unsequencedMessageTypeDependencyReadDistanceFromUnsequencedMessage = -1;
        const int unsequencedMessageTypeDependencyReadWidth = 1;
        long unsequencedMessageTypeDependencyReadPosition;
        ReadOnlySpan<byte> unsequencedMessageTypeValue;
        if (start < Position.Minimum || limit < start || limit > bytes.Length) return Position.Failure;
        int index = start;
        
        {
            const int unsequencedMessageTypeMeasuredBytes = SizeUnsequencedMessageType;
            if (unsequencedMessageTypeMeasuredBytes < Position.Minimum || unsequencedMessageTypeMeasuredBytes > (long)limit - index) return Position.Failure;
            index += unsequencedMessageTypeMeasuredBytes;
        }
        unsequencedMessageTypeDependencyReadPosition = (long)index + unsequencedMessageTypeDependencyReadDistanceFromUnsequencedMessage;
        if (index < Position.Minimum || index > limit || limit < Position.Minimum || limit > bytes.Length || unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > limit || unsequencedMessageTypeDependencyReadWidth > limit - unsequencedMessageTypeDependencyReadPosition) return Position.Failure;
        unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
        received0 = unsequencedMessageTypeValue;
        received1 = dependency0;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.DebugPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LoginRequestPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.UnsequencedDataPacket;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.ClientHeartbeat;
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
                return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LogoutRequest;
            }
        }
        return (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.None;
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
