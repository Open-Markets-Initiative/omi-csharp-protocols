using System;
using System.Buffers.Binary;
namespace Iex.IexEquities.Tops.IexTp.v1_56;
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
        internal bool IextpHeader_PayloadLength_Used;
        internal bool IextpHeader_MessageCount_Used;
        internal bool MessageHeader_MessageLength_Used;
        internal bool MessageHeader_MessageType_Used;
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
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var MessageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var MessageSizeRule1ArithmeticLeft2 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight3)))) }) })) switch { var MessageSizeRule1ArithmeticRight5 => checked((ushort)(checked((MessageSizeRule1ArithmeticLeft4 - MessageSizeRule1ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var MessageSizeRule1ArithmeticLeft0 => ((MessageSizeRule1Data2) switch { var MessageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MessageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MessageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }

        internal static void MessageDataBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule0Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.QuoteUpdateMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = MessageDataBranchRule0Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(MessageDataBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void MessageDataBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule1Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.TradeReportMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = MessageDataBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(MessageDataBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void MessageDataBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> MessageDataBranchRule2Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.TradeBreakMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = MessageDataBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(MessageDataBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void MessageDataCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index, int written)
        {
            const int QuoteUpdateMessageCase = 1;
            const int TradeReportMessageCase = 2;
            const int TradeBreakMessageCase = 3;
            global::System.ReadOnlySpan<byte> MessageDataBranchRule0Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.QuoteUpdateMessageMessageType;
            global::System.ReadOnlySpan<byte> MessageDataBranchRule1Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.TradeReportMessageMessageType;
            global::System.ReadOnlySpan<byte> MessageDataBranchRule2Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Value.TradeBreakMessageMessageType;
            int selected = Position.Absent;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(MessageDataBranchRule0Data2)) selected = QuoteUpdateMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(MessageDataBranchRule1Data2)) selected = TradeReportMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(MessageDataBranchRule2Data2)) selected = TradeBreakMessageCase;
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
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(bytes[(int)messageCountDependencyWritePosition..], unchecked((ushort)checked((ushort)(measure))));
                messageCountDependencyWriteUsed = true;
            }
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(messageCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }

        internal static void MessagesSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long payloadLengthDependencyWritePosition, ref bool payloadLengthDependencyWriteUsed, int index, int end)
        {
            if (payloadLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadLengthDependencyWriteUsed)) return;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long payloadLengthDependencyReadPosition;
            ushort payloadLengthValue;
            if (!payloadLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(bytes[(int)payloadLengthDependencyWritePosition..], unchecked((ushort)checked((ushort)(measure))));
                payloadLengthDependencyWriteUsed = true;
            }
            const int payloadLengthDependencyReadWidth = 2;
            payloadLengthDependencyReadPosition = payloadLengthDependencyWritePosition;
            if (payloadLengthDependencyReadPosition < Position.Minimum || payloadLengthDependencyReadPosition > bytes.Length || payloadLengthDependencyReadWidth > bytes.Length - payloadLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            payloadLengthValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)payloadLengthDependencyReadPosition)..]);
            if (checked((global::System.Int128)(payloadLengthValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            payloadLengthDependencyWriteUsed = true;
        }
    }

    internal static class PacketWriter
    {
        internal static void MessagesBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index)
        {
            const ushort MessagesBranchRule1Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Value.HeartbeatMessageCount;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            if (!messageCountDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16LittleEndian(bytes[(int)messageCountDependencyWritePosition..], unchecked((ushort)MessagesBranchRule1Data2));
                messageCountDependencyWriteUsed = true;
            }
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (!(messageCountValue == MessagesBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }

        internal static void MessagesCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageCountDependencyWritePosition, ref bool messageCountDependencyWriteUsed, int index, int written)
        {
            const int HeartbeatCase = 1;
            const int MessageCase = 2;
            const ushort MessagesBranchRule1Data2 = global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Value.HeartbeatMessageCount;
            int selected = Position.Absent;
            long messageCountDependencyReadPosition;
            ushort messageCountValue;
            const int messageCountDependencyReadWidth = 2;
            messageCountDependencyReadPosition = messageCountDependencyWritePosition;
            if (messageCountDependencyReadPosition < Position.Minimum || messageCountDependencyReadPosition > bytes.Length || messageCountDependencyReadWidth > bytes.Length - messageCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountValue = BinaryPrimitives.ReadUInt16LittleEndian(bytes[((int)messageCountDependencyReadPosition)..]);
            if (selected == Position.Absent)
            {
                if ((messageCountValue == MessagesBranchRule1Data2)) selected = HeartbeatCase;
            }
            if (selected == Position.Absent) selected = MessageCase;
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageCountDependencyWriteUsed = true;
        }
    }
}
