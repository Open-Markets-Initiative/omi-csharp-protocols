using System;
using System.Buffers.Binary;
namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;
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
        internal bool ComplexStrategyDirectoryMessage_NumberOfLegs_Used;
        internal bool ComplexStrategyAuctionMessage_NumberOfResponses_Used;
    }

    internal static class ComplexStrategyAuctionMessageWriter
    {
        internal static void AuctionResponseCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long numberOfResponsesDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long numberOfResponsesDependencyReadPosition;
            byte numberOfResponsesValue;
            if (!cursor.ComplexStrategyAuctionMessage_NumberOfResponses_Used)
            {
                bytes[(int)numberOfResponsesDependencyWritePosition] = unchecked((byte)checked((byte)(measure)));
                cursor.ComplexStrategyAuctionMessage_NumberOfResponses_Used = true;
            }
            const int numberOfResponsesDependencyReadWidth = 1;
            numberOfResponsesDependencyReadPosition = numberOfResponsesDependencyWritePosition;
            if (numberOfResponsesDependencyReadPosition < Position.Minimum || numberOfResponsesDependencyReadPosition > bytes.Length || numberOfResponsesDependencyReadWidth > bytes.Length - numberOfResponsesDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            numberOfResponsesValue = bytes[(int)numberOfResponsesDependencyReadPosition];
            if (checked((global::System.Int128)(numberOfResponsesValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ComplexStrategyAuctionMessage_NumberOfResponses_Used = true;
        }
    }

    internal static class ComplexStrategyDirectoryMessageWriter
    {
        internal static void LegInformationCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long numberOfLegsDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long numberOfLegsDependencyReadPosition;
            byte numberOfLegsValue;
            if (!cursor.ComplexStrategyDirectoryMessage_NumberOfLegs_Used)
            {
                bytes[(int)numberOfLegsDependencyWritePosition] = unchecked((byte)checked((byte)(measure)));
                cursor.ComplexStrategyDirectoryMessage_NumberOfLegs_Used = true;
            }
            const int numberOfLegsDependencyReadWidth = 1;
            numberOfLegsDependencyReadPosition = numberOfLegsDependencyWritePosition;
            if (numberOfLegsDependencyReadPosition < Position.Minimum || numberOfLegsDependencyReadPosition > bytes.Length || numberOfLegsDependencyReadWidth > bytes.Length - numberOfLegsDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            numberOfLegsValue = bytes[(int)numberOfLegsDependencyReadPosition];
            if (checked((global::System.Int128)(numberOfLegsValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ComplexStrategyDirectoryMessage_NumberOfLegs_Used = true;
        }
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.SystemEventMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyDirectoryMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.StrategyTradingActionMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.StrategyOpenClosedMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyOrderOnBookMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyAuctionMessageMessageType;
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

        internal static void PayloadCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index, int written)
        {
            const int SystemEventMessageCase = 1;
            const int ComplexStrategyDirectoryMessageCase = 2;
            const int StrategyTradingActionMessageCase = 3;
            const int StrategyOpenClosedMessageCase = 4;
            const int ComplexStrategyOrderOnBookMessageCase = 5;
            const int ComplexStrategyAuctionMessageCase = 6;
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.SystemEventMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyDirectoryMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.StrategyTradingActionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.StrategyOpenClosedMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyOrderOnBookMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Value.ComplexStrategyAuctionMessageMessageType;
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
                if (messageTypeValue.SequenceEqual(PayloadBranchRule2Data2)) selected = ComplexStrategyDirectoryMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule3Data2)) selected = StrategyTradingActionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule4Data2)) selected = StrategyOpenClosedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule5Data2)) selected = ComplexStrategyOrderOnBookMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule6Data2)) selected = ComplexStrategyAuctionMessageCase;
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
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Value.HeartbeatMessageCount;
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
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Value.EndOfSessionMessageCount;
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
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Value.HeartbeatMessageCount;
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Value.EndOfSessionMessageCount;
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
/// <summary>
///  Gives the entries of a repeated part whose values hold spans, one list for each key.
/// </summary>
public interface IEntries<TValue> where TValue : allows ref struct
{
    /// <summary>
    ///  Gets the number of entries in the list with the specified key.
    /// </summary>
    int Count(int key);

    /// <summary>
    ///  Gets the entry at the zero-based index in the list with the specified key.
    /// </summary>
    TValue Entry(int key, int index);
}
/// <summary>
///  The entries of a repeated part whose values hold spans, which cannot outlive the provider they refer to.
/// </summary>
public readonly unsafe ref struct Entries<TValue> where TValue : allows ref struct
{
    private const int FirstKey = 0;

    private readonly ref byte provider;
    private readonly delegate*<ref byte, int, int> count;
    private readonly delegate*<ref byte, int, int, TValue> entry;
    private readonly int key;

    private Entries(ref byte provider, delegate*<ref byte, int, int> count, delegate*<ref byte, int, int, TValue> entry, int key)
    {
        this.provider = ref provider;
        this.count = count;
        this.entry = entry;
        this.key = key;
    }

    /// <summary>
    ///  Gets the entries of the list with key 0 of a provider.
    /// </summary>
    public static Entries<TValue> Of<TProvider>(ref TProvider provider) where TProvider : IEntries<TValue>, allows ref struct => new(ref global::System.Runtime.CompilerServices.Unsafe.As<TProvider, byte>(ref provider), &CountOf<TProvider>, &EntryOf<TProvider>, FirstKey);

    /// <summary>
    ///  Gets the entries of the list with the specified key of the same provider.
    /// </summary>
    public Entries<TValue> For(int key) => new(ref this.provider, this.count, this.entry, key);

    internal int Count => this.count is null ? 0 : this.count(ref this.provider, this.key);

    internal TValue this[int index] => this.entry(ref this.provider, this.key, index);

    private static int CountOf<TProvider>(ref byte provider, int key) where TProvider : IEntries<TValue>, allows ref struct => global::System.Runtime.CompilerServices.Unsafe.As<byte, TProvider>(ref provider).Count(key);

    private static TValue EntryOf<TProvider>(ref byte provider, int key, int index) where TProvider : IEntries<TValue>, allows ref struct => global::System.Runtime.CompilerServices.Unsafe.As<byte, TProvider>(ref provider).Entry(key, index);
}
