using System;
using System.Buffers.Binary;
namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;
internal static partial class Parser
{
    internal const string WriterNoRoom = "The buffer has no room for the bytes.";
    internal const string ReadTooShort = "The bytes are shorter than the fixed fields.";
    internal const string WriterRuleDisagrees = "A length, count or case value in the bytes does not agree with the bytes written.";
    internal const string WriterNoCase = "The value has no case.";
    internal const string WriterTwoCases = "The value already has a case.";
    internal const string FieldTextTooLong = "The encoded text is longer than the field.";
    internal const string FieldTextNotExact = "The encoded text does not fill the field exactly.";
    internal const string FieldNumberTextTooLong = "The text of the number is longer than the field.";
    internal const string FieldBytesLength = "The value is not as long as the field.";
    internal const string FieldRemainderOutOfRange = "The remainder is negative or not below one tick.";
    internal const string FieldTimeNotWholeUnit = "The time is not a whole number of the field unit.";
    internal const string FieldTimeIsSentinel = "The time encodes as the NoValue sentinel.";
    internal const string WriterValueDoesNotFit = "The value does not fit its field.";
    internal const string WriterDestination = "destination";
    internal const string WriterValue = "value";

    private static global::System.Int128 DivideRuleIntegerExact(global::System.Int128 numerator, global::System.Int128 divisor)
    {
        if (divisor == global::System.Int128.Zero || numerator % divisor != global::System.Int128.Zero) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        return numerator / divisor;
    }

    internal struct WriterCursor
    {
        internal int Index;
        internal bool PacketHeader_MessageCount_Used;
        internal bool MessageHeader_MessageLength_Used;
        internal bool MessageHeader_MessageType_Used;
    }

    internal static class PayloadWriter
    {
        internal static void PayloadSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageLengthDependencyWritePosition, ref bool messageLengthDependencyWriteUsed, int index, int end)
        {
            if (messageLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageLengthDependencyWriteUsed)) return;
            const long PayloadSizeRule0Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long messageLengthDependencyReadPosition;
            ushort messageLengthValue;
            if (!messageLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var PayloadSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var PayloadSizeRule0ArithmeticLeft2 => ((PayloadSizeRule0Data2) switch { var PayloadSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(PayloadSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(PayloadSizeRule0ArithmeticRight3)))) }) })) switch { var PayloadSizeRule0ArithmeticRight5 => checked((ushort)(checked((PayloadSizeRule0ArithmeticLeft4 - PayloadSizeRule0ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var PayloadSizeRule0ArithmeticLeft0 => ((PayloadSizeRule0Data2) switch { var PayloadSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(PayloadSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(PayloadSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }
    }

    internal static class MessageWriter
    {
        internal static void MessageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageLengthDependencyWritePosition, ref bool messageLengthDependencyWriteUsed, int index, int end)
        {
            const long MessageSizeRule1Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long messageLengthDependencyReadPosition;
            ushort messageLengthValue;
            if (!messageLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var MessageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var MessageSizeRule1ArithmeticLeft2 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight3)))) }) })) switch { var MessageSizeRule1ArithmeticRight5 => checked((ushort)(checked((MessageSizeRule1ArithmeticLeft4 - MessageSizeRule1ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var MessageSizeRule1ArithmeticLeft0 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.SystemEventMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.StockDirectoryMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.StockTradingActionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MarketParticipantPositionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule5Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MwcbDeclineLevelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule6Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MwcbStatusLevelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule7Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.LuldAuctionCollarMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule8Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OperationalHaltMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule9Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.AddOrderNoMpidAttributionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule10Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.AddOrderWithMpidAttributionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule11Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderExecutedMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule12Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderExecutedWithPriceMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule13Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderCancelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule14Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderDeleteMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule15Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderReplaceMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule16Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.TradeMessageNonCrossMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule17Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule18(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.CrossTradeMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule18Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule18Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule19(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.BrokenTradeMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule19Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule19Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule20(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule20Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule20Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index, int written)
        {
            const int SystemEventMessageCase = 1;
            const int StockDirectoryMessageCase = 2;
            const int StockTradingActionMessageCase = 3;
            const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
            const int MarketParticipantPositionMessageCase = 5;
            const int MwcbDeclineLevelMessageCase = 6;
            const int MwcbStatusLevelMessageCase = 7;
            const int LuldAuctionCollarMessageCase = 8;
            const int OperationalHaltMessageCase = 9;
            const int AddOrderNoMpidAttributionMessageCase = 10;
            const int AddOrderWithMpidAttributionMessageCase = 11;
            const int OrderExecutedMessageCase = 12;
            const int OrderExecutedWithPriceMessageCase = 13;
            const int OrderCancelMessageCase = 14;
            const int OrderDeleteMessageCase = 15;
            const int OrderReplaceMessageCase = 16;
            const int TradeMessageNonCrossCase = 17;
            const int CrossTradeMessageCase = 18;
            const int BrokenTradeMessageCase = 19;
            const int NetOrderImbalanceIndicatorMessageCase = 20;
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.SystemEventMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.StockDirectoryMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.StockTradingActionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MarketParticipantPositionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MwcbDeclineLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.MwcbStatusLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.LuldAuctionCollarMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OperationalHaltMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.AddOrderNoMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.AddOrderWithMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderExecutedMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderExecutedWithPriceMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderCancelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderDeleteMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.OrderReplaceMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.TradeMessageNonCrossMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.CrossTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.BrokenTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            int selected = Position.Absent;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule1Data2)) selected = SystemEventMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule2Data2)) selected = StockDirectoryMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule3Data2)) selected = StockTradingActionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule4Data2)) selected = RegShoShortSalePriceTestRestrictedIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule5Data2)) selected = MarketParticipantPositionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule6Data2)) selected = MwcbDeclineLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule7Data2)) selected = MwcbStatusLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule8Data2)) selected = LuldAuctionCollarMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule9Data2)) selected = OperationalHaltMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule10Data2)) selected = AddOrderNoMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule11Data2)) selected = AddOrderWithMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule12Data2)) selected = OrderExecutedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule13Data2)) selected = OrderExecutedWithPriceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule14Data2)) selected = OrderCancelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule15Data2)) selected = OrderDeleteMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule16Data2)) selected = OrderReplaceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule17Data2)) selected = TradeMessageNonCrossCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule18Data2)) selected = CrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule19Data2)) selected = BrokenTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule20Data2)) selected = NetOrderImbalanceIndicatorMessageCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }
    }

    internal static class MessagesWriter
    {
        internal static void MessageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index, int count)
        {
            if (messageCountDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageCountDependencyWriteUsed)) return;
            global::System.Int128 measure = count;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            if (!messageCountDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageCountDependencyWritePosition..], unchecked((ushort)checked((ushort)(measure))));
                messageCountDependencyWriteUsed = true;
            }
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(messageCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }
    }

    internal static class PacketWriter
    {
        internal static void MessagesBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index)
        {
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Value.HeartbeatMessageCount;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            if (!messageCountDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageCountDependencyWritePosition..], unchecked((ushort)MessagesBranchRule0Data2));
                messageCountDependencyWriteUsed = true;
            }
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (!(messageCountValue == MessagesBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }

        internal static void MessagesBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index)
        {
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Value.EndOfSessionMessageCount;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            if (!messageCountDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageCountDependencyWritePosition..], unchecked((ushort)MessagesBranchRule1Data2));
                messageCountDependencyWriteUsed = true;
            }
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (!(messageCountValue == MessagesBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }

        internal static void MessagesCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index, int written)
        {
            const int HeartbeatCase = 1;
            const int EndOfSessionCase = 2;
            const int MessageCase = 3;
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Value.HeartbeatMessageCount;
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Value.EndOfSessionMessageCount;
            int selected = Position.Absent;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (selected == Position.Absent)
            {
                if ((messageCountValue == MessagesBranchRule0Data2)) selected = HeartbeatCase;
            }
            if (selected == Position.Absent)
            {
                if ((messageCountValue == MessagesBranchRule1Data2)) selected = EndOfSessionCase;
            }
            if (selected == Position.Absent) selected = MessageCase;
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }
    }
}
