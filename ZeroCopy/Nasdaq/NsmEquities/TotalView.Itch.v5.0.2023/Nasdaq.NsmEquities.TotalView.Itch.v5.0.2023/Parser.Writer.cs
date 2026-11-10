using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;
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
        internal bool ClientPacketHeader_PacketLength_Used;
        internal bool ClientPacketHeader_ClientPacketType_Used;
        internal bool ServerPacketHeader_PacketLength_Used;
        internal bool ServerPacketHeader_ServerPacketType_Used;
        internal bool SequencedDataPacket_SequencedMessageType_Used;
        internal bool PacketHeader_MessageCount_Used;
        internal bool MessageHeader_MessageLength_Used;
        internal bool MessageHeader_MessageType_Used;
    }

    internal static class FilePayloadWriter
    {
        internal static void FileMessageFilePayloadSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageLengthDependencyWritePosition, ref bool messageLengthDependencyWriteUsed, int index, int end)
        {
            if (messageLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageLengthDependencyWriteUsed)) return;
            const long FilePayloadSizeRule0Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long messageLengthDependencyReadPosition;
            ushort messageLengthValue;
            if (!messageLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var FilePayloadSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var FilePayloadSizeRule0ArithmeticLeft2 => ((FilePayloadSizeRule0Data2) switch { var FilePayloadSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticRight3)))) }) })) switch { var FilePayloadSizeRule0ArithmeticRight5 => checked((ushort)(checked((FilePayloadSizeRule0ArithmeticLeft4 - FilePayloadSizeRule0ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var FilePayloadSizeRule0ArithmeticLeft0 => ((FilePayloadSizeRule0Data2) switch { var FilePayloadSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(FilePayloadSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }
    }

    internal static class FileMessageWriter
    {
        internal static void FileMessageReassembleRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageLengthDependencyWritePosition, ref bool messageLengthDependencyWriteUsed, int index, int end)
        {
            const long FileMessageReassembleRule1Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long messageLengthDependencyReadPosition;
            ushort messageLengthValue;
            if (!messageLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var FileMessageReassembleRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var FileMessageReassembleRule1ArithmeticLeft2 => ((FileMessageReassembleRule1Data2) switch { var FileMessageReassembleRule1ArithmeticRight3 => checked((checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticLeft2)) + checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticRight3)))) }) })) switch { var FileMessageReassembleRule1ArithmeticRight5 => checked((ushort)(checked((FileMessageReassembleRule1ArithmeticLeft4 - FileMessageReassembleRule1ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var FileMessageReassembleRule1ArithmeticLeft0 => ((FileMessageReassembleRule1Data2) switch { var FileMessageReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(FileMessageReassembleRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }

        internal static void FileMessageSizeRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageLengthDependencyWritePosition, ref bool messageLengthDependencyWriteUsed, int index, int end)
        {
            const long FileMessageSizeRule2Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long messageLengthDependencyReadPosition;
            ushort messageLengthValue;
            if (!messageLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)messageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var FileMessageSizeRule2ArithmeticLeft4 => ((((RuleTextZero) switch { var FileMessageSizeRule2ArithmeticLeft2 => ((FileMessageSizeRule2Data2) switch { var FileMessageSizeRule2ArithmeticRight3 => checked((checked((global::System.Int128)(FileMessageSizeRule2ArithmeticLeft2)) + checked((global::System.Int128)(FileMessageSizeRule2ArithmeticRight3)))) }) })) switch { var FileMessageSizeRule2ArithmeticRight5 => checked((ushort)(checked((FileMessageSizeRule2ArithmeticLeft4 - FileMessageSizeRule2ArithmeticRight5)))) }) })));
                messageLengthDependencyWriteUsed = true;
            }
            const int messageLengthDependencyReadWidth = 2;
            messageLengthDependencyReadPosition = messageLengthDependencyWritePosition;
            if (messageLengthDependencyReadPosition < Position.Minimum || messageLengthDependencyReadPosition > bytes.Length || messageLengthDependencyReadWidth > bytes.Length - messageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)messageLengthDependencyReadPosition)..]);
            if (((messageLengthValue) switch { var FileMessageSizeRule2ArithmeticLeft0 => ((FileMessageSizeRule2Data2) switch { var FileMessageSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(FileMessageSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(FileMessageSizeRule2ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageLengthDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.SystemEventMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.StockDirectoryMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.StockTradingActionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MarketParticipantPositionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule5Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MwcbDeclineLevelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule6Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MwcbStatusLevelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule7Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.IpoQuotingPeriodUpdateMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule8Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.LuldAuctionCollarMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule9Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OperationalHaltMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule10Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.AddOrderNoMpidAttributionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule11Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.AddOrderWithMpidAttributionMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule12Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderExecutedMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule13Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderExecutedWithPriceMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule14Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderCancelMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule15Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderDeleteMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule16Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderReplaceMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule17Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule18(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.NonCrossTradeMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule18Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule18Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule19(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.CrossTradeMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule19Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule19Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule20(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.BrokenTradeMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule20Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule20Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule21(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule21Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule21Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule22(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.RetailPriceImprovementIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule22Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule22Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadBranchRule23(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = FilePayloadBranchRule23Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(FilePayloadBranchRule23Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void FileMessageFilePayloadCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index, int written)
        {
            const int SystemEventMessageCase = 1;
            const int StockDirectoryMessageCase = 2;
            const int StockTradingActionMessageCase = 3;
            const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
            const int MarketParticipantPositionMessageCase = 5;
            const int MwcbDeclineLevelMessageCase = 6;
            const int MwcbStatusLevelMessageCase = 7;
            const int IpoQuotingPeriodUpdateCase = 8;
            const int LuldAuctionCollarMessageCase = 9;
            const int OperationalHaltMessageCase = 10;
            const int AddOrderNoMpidAttributionMessageCase = 11;
            const int AddOrderWithMpidAttributionMessageCase = 12;
            const int OrderExecutedMessageCase = 13;
            const int OrderExecutedWithPriceMessageCase = 14;
            const int OrderCancelMessageCase = 15;
            const int OrderDeleteMessageCase = 16;
            const int OrderReplaceMessageCase = 17;
            const int NonCrossTradeMessageCase = 18;
            const int CrossTradeMessageCase = 19;
            const int BrokenTradeMessageCase = 20;
            const int NetOrderImbalanceIndicatorMessageCase = 21;
            const int RetailPriceImprovementIndicatorMessageCase = 22;
            const int DirectListingWithCapitalRaisePriceDiscoveryMessageCase = 23;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.SystemEventMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.StockDirectoryMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.StockTradingActionMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MarketParticipantPositionMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MwcbDeclineLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.MwcbStatusLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.IpoQuotingPeriodUpdateMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.LuldAuctionCollarMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OperationalHaltMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.AddOrderNoMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.AddOrderWithMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderExecutedMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderExecutedWithPriceMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderCancelMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderDeleteMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.OrderReplaceMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.NonCrossTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.CrossTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.BrokenTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.RetailPriceImprovementIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> FilePayloadBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageMessageType;
            int selected = Position.Absent;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule1Data2)) selected = SystemEventMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule2Data2)) selected = StockDirectoryMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule3Data2)) selected = StockTradingActionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule4Data2)) selected = RegShoShortSalePriceTestRestrictedIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule5Data2)) selected = MarketParticipantPositionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule6Data2)) selected = MwcbDeclineLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule7Data2)) selected = MwcbStatusLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule8Data2)) selected = IpoQuotingPeriodUpdateCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule9Data2)) selected = LuldAuctionCollarMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule10Data2)) selected = OperationalHaltMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule11Data2)) selected = AddOrderNoMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule12Data2)) selected = AddOrderWithMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule13Data2)) selected = OrderExecutedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule14Data2)) selected = OrderExecutedWithPriceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule15Data2)) selected = OrderCancelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule16Data2)) selected = OrderDeleteMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule17Data2)) selected = OrderReplaceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule18Data2)) selected = NonCrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule19Data2)) selected = CrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule20Data2)) selected = BrokenTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule21Data2)) selected = NetOrderImbalanceIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule22Data2)) selected = RetailPriceImprovementIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(FilePayloadBranchRule23Data2)) selected = DirectListingWithCapitalRaisePriceDiscoveryMessageCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }
    }

    internal static class FileWriter
    {
        internal static void FileMessagePayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = end;
            if (checked((global::System.Int128)(limit)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.SystemEventMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.StockDirectoryMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.StockTradingActionMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MarketParticipantPositionMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MwcbDeclineLevelMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MwcbStatusLevelMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.IpoQuotingPeriodUpdateMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.LuldAuctionCollarMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OperationalHaltMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.AddOrderNoMpidAttributionMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.AddOrderWithMpidAttributionMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderExecutedMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderExecutedWithPriceMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderCancelMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderDeleteMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderReplaceMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.NonCrossTradeMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.CrossTradeMessageMessageType;
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
            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.BrokenTradeMessageMessageType;
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

        internal static void PayloadBranchRule21(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule21Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule21Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule22(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.RetailPriceImprovementIndicatorMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule22Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule22Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeDependencyWriteUsed = true;
        }

        internal static void PayloadBranchRule23(global::System.Span<byte> bytes, ref WriterCursor cursor, long messageTypeDependencyWritePosition, ref bool messageTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> PayloadBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageMessageType;
            long messageTypeDependencyReadPosition;
            ReadOnlySpan<byte> messageTypeValue;
            if (!messageTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = PayloadBranchRule23Data2;
                global::System.Span<byte> target = bytes.Slice((int)messageTypeDependencyWritePosition, SizeMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                messageTypeDependencyWriteUsed = true;
            }
            const int messageTypeDependencyReadWidth = 1;
            messageTypeDependencyReadPosition = messageTypeDependencyWritePosition;
            if (messageTypeDependencyReadPosition < Position.Minimum || messageTypeDependencyReadPosition > bytes.Length || messageTypeDependencyReadWidth > bytes.Length - messageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            messageTypeValue = bytes.Slice((int)messageTypeDependencyReadPosition, messageTypeDependencyReadWidth);
            if (!messageTypeValue.SequenceEqual(PayloadBranchRule23Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
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
            const int IpoQuotingPeriodUpdateCase = 8;
            const int LuldAuctionCollarMessageCase = 9;
            const int OperationalHaltMessageCase = 10;
            const int AddOrderNoMpidAttributionMessageCase = 11;
            const int AddOrderWithMpidAttributionMessageCase = 12;
            const int OrderExecutedMessageCase = 13;
            const int OrderExecutedWithPriceMessageCase = 14;
            const int OrderCancelMessageCase = 15;
            const int OrderDeleteMessageCase = 16;
            const int OrderReplaceMessageCase = 17;
            const int NonCrossTradeMessageCase = 18;
            const int CrossTradeMessageCase = 19;
            const int BrokenTradeMessageCase = 20;
            const int NetOrderImbalanceIndicatorMessageCase = 21;
            const int RetailPriceImprovementIndicatorMessageCase = 22;
            const int DirectListingWithCapitalRaisePriceDiscoveryMessageCase = 23;
            global::System.ReadOnlySpan<byte> PayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.SystemEventMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.StockDirectoryMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.StockTradingActionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MarketParticipantPositionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MwcbDeclineLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.MwcbStatusLevelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.IpoQuotingPeriodUpdateMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.LuldAuctionCollarMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OperationalHaltMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.AddOrderNoMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.AddOrderWithMpidAttributionMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderExecutedMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderExecutedWithPriceMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderCancelMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderDeleteMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.OrderReplaceMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.NonCrossTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.CrossTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.BrokenTradeMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.NetOrderImbalanceIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.RetailPriceImprovementIndicatorMessageMessageType;
            global::System.ReadOnlySpan<byte> PayloadBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageMessageType;
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
                if (messageTypeValue.SequenceEqual(PayloadBranchRule8Data2)) selected = IpoQuotingPeriodUpdateCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule9Data2)) selected = LuldAuctionCollarMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule10Data2)) selected = OperationalHaltMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule11Data2)) selected = AddOrderNoMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule12Data2)) selected = AddOrderWithMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule13Data2)) selected = OrderExecutedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule14Data2)) selected = OrderExecutedWithPriceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule15Data2)) selected = OrderCancelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule16Data2)) selected = OrderDeleteMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule17Data2)) selected = OrderReplaceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule18Data2)) selected = NonCrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule19Data2)) selected = CrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule20Data2)) selected = BrokenTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule21Data2)) selected = NetOrderImbalanceIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule22Data2)) selected = RetailPriceImprovementIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (messageTypeValue.SequenceEqual(PayloadBranchRule23Data2)) selected = DirectListingWithCapitalRaisePriceDiscoveryMessageCase;
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
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Value.HeartbeatMessageCount;
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
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Value.EndOfSessionMessageCount;
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
            const ushort MessagesBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Value.HeartbeatMessageCount;
            const ushort MessagesBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Value.EndOfSessionMessageCount;
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

    internal static class SequencedMessageWriter
    {
        internal static void SequencedMessageSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            if (packetLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref packetLengthDependencyWriteUsed)) return;
            const long SequencedMessageSizeRule0Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var SequencedMessageSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var SequencedMessageSizeRule0ArithmeticLeft2 => ((SequencedMessageSizeRule0Data2) switch { var SequencedMessageSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticRight3)))) }) })) switch { var SequencedMessageSizeRule0ArithmeticRight5 => checked((ushort)(checked((SequencedMessageSizeRule0ArithmeticLeft4 - SequencedMessageSizeRule0ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var SequencedMessageSizeRule0ArithmeticLeft0 => ((SequencedMessageSizeRule0Data2) switch { var SequencedMessageSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(SequencedMessageSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }
    }

    internal static class SequencedDataPacketWriter
    {
        internal static void SequencedDataPacketSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            if (packetLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref packetLengthDependencyWriteUsed)) return;
            const long SequencedDataPacketSizeRule0Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var SequencedDataPacketSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var SequencedDataPacketSizeRule0ArithmeticLeft2 => ((SequencedDataPacketSizeRule0Data2) switch { var SequencedDataPacketSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticRight3)))) }) })) switch { var SequencedDataPacketSizeRule0ArithmeticRight5 => checked((ushort)(checked((SequencedDataPacketSizeRule0ArithmeticLeft4 - SequencedDataPacketSizeRule0ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var SequencedDataPacketSizeRule0ArithmeticLeft0 => ((SequencedDataPacketSizeRule0Data2) switch { var SequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(SequencedDataPacketSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void SequencedMessageBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.SystemEventMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.StockDirectoryMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.StockTradingActionMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MarketParticipantPositionMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule5Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MwcbDeclineLevelMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule6Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MwcbStatusLevelMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule7Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.IpoQuotingPeriodUpdateSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule8Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.LuldAuctionCollarMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule9Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OperationalHaltMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule10Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.AddOrderNoMpidAttributionMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule11Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.AddOrderWithMpidAttributionMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule12Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderExecutedMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule13Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderExecutedWithPriceMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule14Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderCancelMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule15Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderDeleteMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule16Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderReplaceMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule17Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule18(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.NonCrossTradeMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule18Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule18Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule19(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.CrossTradeMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule19Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule19Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule20(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.BrokenTradeMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule20Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule20Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule21(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.NetOrderImbalanceIndicatorMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule21Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule21Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule22(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.RetailPriceImprovementIndicatorMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule22Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule22Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageBranchRule23(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageSequencedMessageType;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            if (!cursor.SequencedDataPacket_SequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = SequencedMessageBranchRule23Data2;
                global::System.Span<byte> target = bytes.Slice((int)sequencedMessageTypeDependencyWritePosition, SizeSequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.SequencedDataPacket_SequencedMessageType_Used = true;
            }
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (!sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule23Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }

        internal static void SequencedMessageCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index, int written)
        {
            const int SystemEventMessageCase = 1;
            const int StockDirectoryMessageCase = 2;
            const int StockTradingActionMessageCase = 3;
            const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
            const int MarketParticipantPositionMessageCase = 5;
            const int MwcbDeclineLevelMessageCase = 6;
            const int MwcbStatusLevelMessageCase = 7;
            const int IpoQuotingPeriodUpdateCase = 8;
            const int LuldAuctionCollarMessageCase = 9;
            const int OperationalHaltMessageCase = 10;
            const int AddOrderNoMpidAttributionMessageCase = 11;
            const int AddOrderWithMpidAttributionMessageCase = 12;
            const int OrderExecutedMessageCase = 13;
            const int OrderExecutedWithPriceMessageCase = 14;
            const int OrderCancelMessageCase = 15;
            const int OrderDeleteMessageCase = 16;
            const int OrderReplaceMessageCase = 17;
            const int NonCrossTradeMessageCase = 18;
            const int CrossTradeMessageCase = 19;
            const int BrokenTradeMessageCase = 20;
            const int NetOrderImbalanceIndicatorMessageCase = 21;
            const int RetailPriceImprovementIndicatorMessageCase = 22;
            const int DirectListingWithCapitalRaisePriceDiscoveryMessageCase = 23;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.SystemEventMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.StockDirectoryMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.StockTradingActionMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.RegShoShortSalePriceTestRestrictedIndicatorMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MarketParticipantPositionMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MwcbDeclineLevelMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.MwcbStatusLevelMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.IpoQuotingPeriodUpdateSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.LuldAuctionCollarMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OperationalHaltMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.AddOrderNoMpidAttributionMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.AddOrderWithMpidAttributionMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderExecutedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderExecutedWithPriceMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule15Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderCancelMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderDeleteMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.OrderReplaceMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule18Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.NonCrossTradeMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule19Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.CrossTradeMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule20Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.BrokenTradeMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule21Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.NetOrderImbalanceIndicatorMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule22Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.RetailPriceImprovementIndicatorMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule23Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Value.DirectListingWithCapitalRaisePriceDiscoveryMessageSequencedMessageType;
            int selected = Position.Absent;
            long sequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> sequencedMessageTypeValue;
            const int sequencedMessageTypeDependencyReadWidth = 1;
            sequencedMessageTypeDependencyReadPosition = sequencedMessageTypeDependencyWritePosition;
            if (sequencedMessageTypeDependencyReadPosition < Position.Minimum || sequencedMessageTypeDependencyReadPosition > bytes.Length || sequencedMessageTypeDependencyReadWidth > bytes.Length - sequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            sequencedMessageTypeValue = bytes.Slice((int)sequencedMessageTypeDependencyReadPosition, sequencedMessageTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule1Data2)) selected = SystemEventMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule2Data2)) selected = StockDirectoryMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule3Data2)) selected = StockTradingActionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule4Data2)) selected = RegShoShortSalePriceTestRestrictedIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule5Data2)) selected = MarketParticipantPositionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule6Data2)) selected = MwcbDeclineLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule7Data2)) selected = MwcbStatusLevelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule8Data2)) selected = IpoQuotingPeriodUpdateCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule9Data2)) selected = LuldAuctionCollarMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule10Data2)) selected = OperationalHaltMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule11Data2)) selected = AddOrderNoMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule12Data2)) selected = AddOrderWithMpidAttributionMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule13Data2)) selected = OrderExecutedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule14Data2)) selected = OrderExecutedWithPriceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule15Data2)) selected = OrderCancelMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule16Data2)) selected = OrderDeleteMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule17Data2)) selected = OrderReplaceMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule18Data2)) selected = NonCrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule19Data2)) selected = CrossTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule20Data2)) selected = BrokenTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule21Data2)) selected = NetOrderImbalanceIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule22Data2)) selected = RetailPriceImprovementIndicatorMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule23Data2)) selected = DirectListingWithCapitalRaisePriceDiscoveryMessageCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SequencedDataPacket_SequencedMessageType_Used = true;
        }
    }

    internal static class DebugPacketWriter
    {
        internal static void DebugTextSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            if (packetLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref packetLengthDependencyWriteUsed)) return;
            const long DebugTextSizeRule0Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var DebugTextSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var DebugTextSizeRule0ArithmeticLeft2 => ((DebugTextSizeRule0Data2) switch { var DebugTextSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(DebugTextSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(DebugTextSizeRule0ArithmeticRight3)))) }) })) switch { var DebugTextSizeRule0ArithmeticRight5 => checked((ushort)(checked((DebugTextSizeRule0ArithmeticLeft4 - DebugTextSizeRule0ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var DebugTextSizeRule0ArithmeticLeft0 => ((DebugTextSizeRule0Data2) switch { var DebugTextSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DebugTextSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(DebugTextSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }
    }

    internal static class ServerSoupBinTcpPacketWriter
    {
        internal static void ServerSoupBinTcpPacketReassembleRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            const long ServerSoupBinTcpPacketReassembleRule1Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft2 => ((ServerSoupBinTcpPacketReassembleRule1Data2) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticRight3 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft2)) + checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticRight3)))) }) })) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticRight5 => checked((ushort)(checked((ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft4 - ServerSoupBinTcpPacketReassembleRule1ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft0 => ((ServerSoupBinTcpPacketReassembleRule1Data2) switch { var ServerSoupBinTcpPacketReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(ServerSoupBinTcpPacketReassembleRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void ServerSoupBinTcpPacketSizeRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            const long ServerSoupBinTcpPacketSizeRule2Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticLeft4 => ((((RuleTextZero) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticLeft2 => ((ServerSoupBinTcpPacketSizeRule2Data2) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticRight3 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticLeft2)) + checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticRight3)))) }) })) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticRight5 => checked((ushort)(checked((ServerSoupBinTcpPacketSizeRule2ArithmeticLeft4 - ServerSoupBinTcpPacketSizeRule2ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticLeft0 => ((ServerSoupBinTcpPacketSizeRule2Data2) switch { var ServerSoupBinTcpPacketSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(ServerSoupBinTcpPacketSizeRule2ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.DebugPacketServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule0Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.LoginAcceptedPacketServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.LoginRejectedPacketServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.SequencedDataPacketServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.ServerHeartbeatServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.EndOfSessionServerPacketType;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            if (!serverPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ServerPayloadBranchRule5Data2;
                global::System.Span<byte> target = bytes.Slice((int)serverPacketTypeDependencyWritePosition, SizeServerPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                serverPacketTypeDependencyWriteUsed = true;
            }
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (!serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }

        internal static void ServerPayloadCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long serverPacketTypeDependencyWritePosition, ref bool serverPacketTypeDependencyWriteUsed, int index, int written)
        {
            const int DebugPacketCase = 1;
            const int LoginAcceptedPacketCase = 2;
            const int LoginRejectedPacketCase = 3;
            const int SequencedDataPacketCase = 4;
            const int ServerHeartbeatCase = 5;
            const int EndOfSessionCase = 6;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.DebugPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.LoginAcceptedPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.LoginRejectedPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.SequencedDataPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.ServerHeartbeatServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Value.EndOfSessionServerPacketType;
            int selected = Position.Absent;
            long serverPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> serverPacketTypeValue;
            const int serverPacketTypeDependencyReadWidth = 1;
            serverPacketTypeDependencyReadPosition = serverPacketTypeDependencyWritePosition;
            if (serverPacketTypeDependencyReadPosition < Position.Minimum || serverPacketTypeDependencyReadPosition > bytes.Length || serverPacketTypeDependencyReadWidth > bytes.Length - serverPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeValue = bytes.Slice((int)serverPacketTypeDependencyReadPosition, serverPacketTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule0Data2)) selected = DebugPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule1Data2)) selected = LoginAcceptedPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule2Data2)) selected = LoginRejectedPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule3Data2)) selected = SequencedDataPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule4Data2)) selected = ServerHeartbeatCase;
            }
            if (selected == Position.Absent)
            {
                if (serverPacketTypeValue.SequenceEqual(ServerPayloadBranchRule5Data2)) selected = EndOfSessionCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            serverPacketTypeDependencyWriteUsed = true;
        }
    }

    internal static class ServerPacketWriter
    {
        internal static void ServerSoupBinTcpPacketPayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = end;
            if (checked((global::System.Int128)(limit)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class UnsequencedDataPacketWriter
    {
        internal static void UnsequencedDataPacketSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            if (packetLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref packetLengthDependencyWriteUsed)) return;
            const long UnsequencedDataPacketSizeRule0Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var UnsequencedDataPacketSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var UnsequencedDataPacketSizeRule0ArithmeticLeft2 => ((UnsequencedDataPacketSizeRule0Data2) switch { var UnsequencedDataPacketSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticRight3)))) }) })) switch { var UnsequencedDataPacketSizeRule0ArithmeticRight5 => checked((ushort)(checked((UnsequencedDataPacketSizeRule0ArithmeticLeft4 - UnsequencedDataPacketSizeRule0ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var UnsequencedDataPacketSizeRule0ArithmeticLeft0 => ((UnsequencedDataPacketSizeRule0Data2) switch { var UnsequencedDataPacketSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(UnsequencedDataPacketSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void UnsequencedMessageSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            if (packetLengthDependencyWritePosition < Position.Minimum || global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref packetLengthDependencyWriteUsed)) return;
            const long UnsequencedMessageSizeRule0Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var UnsequencedMessageSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var UnsequencedMessageSizeRule0ArithmeticLeft2 => ((UnsequencedMessageSizeRule0Data2) switch { var UnsequencedMessageSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticLeft2)) - checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticRight3)))) }) })) switch { var UnsequencedMessageSizeRule0ArithmeticRight5 => checked((ushort)(checked((UnsequencedMessageSizeRule0ArithmeticLeft4 - UnsequencedMessageSizeRule0ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var UnsequencedMessageSizeRule0ArithmeticLeft0 => ((UnsequencedMessageSizeRule0Data2) switch { var UnsequencedMessageSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticLeft0)) - checked((global::System.Int128)(UnsequencedMessageSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }
    }

    internal static class ClientSoupBinTcpPacketWriter
    {
        internal static void ClientSoupBinTcpPacketReassembleRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            const long ClientSoupBinTcpPacketReassembleRule1Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft2 => ((ClientSoupBinTcpPacketReassembleRule1Data2) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticRight3 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft2)) + checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticRight3)))) }) })) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticRight5 => checked((ushort)(checked((ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft4 - ClientSoupBinTcpPacketReassembleRule1ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft0 => ((ClientSoupBinTcpPacketReassembleRule1Data2) switch { var ClientSoupBinTcpPacketReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticLeft0)) + checked((global::System.Int128)(ClientSoupBinTcpPacketReassembleRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void ClientSoupBinTcpPacketSizeRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long packetLengthDependencyWritePosition, ref bool packetLengthDependencyWriteUsed, int index, int end)
        {
            const long ClientSoupBinTcpPacketSizeRule2Data2 = 2;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long packetLengthDependencyReadPosition;
            ushort packetLengthValue;
            if (!packetLengthDependencyWriteUsed)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)packetLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticLeft4 => ((((RuleTextZero) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticLeft2 => ((ClientSoupBinTcpPacketSizeRule2Data2) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticRight3 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticLeft2)) + checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticRight3)))) }) })) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticRight5 => checked((ushort)(checked((ClientSoupBinTcpPacketSizeRule2ArithmeticLeft4 - ClientSoupBinTcpPacketSizeRule2ArithmeticRight5)))) }) })));
                packetLengthDependencyWriteUsed = true;
            }
            const int packetLengthDependencyReadWidth = 2;
            packetLengthDependencyReadPosition = packetLengthDependencyWritePosition;
            if (packetLengthDependencyReadPosition < Position.Minimum || packetLengthDependencyReadPosition > bytes.Length || packetLengthDependencyReadWidth > bytes.Length - packetLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)packetLengthDependencyReadPosition)..]);
            if (((packetLengthValue) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticLeft0 => ((ClientSoupBinTcpPacketSizeRule2Data2) switch { var ClientSoupBinTcpPacketSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticLeft0)) + checked((global::System.Int128)(ClientSoupBinTcpPacketSizeRule2ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            packetLengthDependencyWriteUsed = true;
        }

        internal static void ClientPayloadBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.DebugPacketClientPacketType;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            if (!clientPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ClientPayloadBranchRule0Data2;
                global::System.Span<byte> target = bytes.Slice((int)clientPacketTypeDependencyWritePosition, SizeClientPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                clientPacketTypeDependencyWriteUsed = true;
            }
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (!clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }

        internal static void ClientPayloadBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.LoginRequestPacketClientPacketType;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            if (!clientPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ClientPayloadBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)clientPacketTypeDependencyWritePosition, SizeClientPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                clientPacketTypeDependencyWriteUsed = true;
            }
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (!clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }

        internal static void ClientPayloadBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.UnsequencedDataPacketClientPacketType;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            if (!clientPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ClientPayloadBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)clientPacketTypeDependencyWritePosition, SizeClientPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                clientPacketTypeDependencyWriteUsed = true;
            }
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (!clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }

        internal static void ClientPayloadBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.ClientHeartbeatClientPacketType;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            if (!clientPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ClientPayloadBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)clientPacketTypeDependencyWritePosition, SizeClientPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                clientPacketTypeDependencyWriteUsed = true;
            }
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (!clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }

        internal static void ClientPayloadBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index)
        {
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.LogoutRequestClientPacketType;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            if (!clientPacketTypeDependencyWriteUsed)
            {
                global::System.ReadOnlySpan<byte> stored = ClientPayloadBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)clientPacketTypeDependencyWritePosition, SizeClientPacketType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                clientPacketTypeDependencyWriteUsed = true;
            }
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (!clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }

        internal static void ClientPayloadCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long clientPacketTypeDependencyWritePosition, ref bool clientPacketTypeDependencyWriteUsed, int index, int written)
        {
            const int DebugPacketCase = 1;
            const int LoginRequestPacketCase = 2;
            const int UnsequencedDataPacketCase = 3;
            const int ClientHeartbeatCase = 4;
            const int LogoutRequestCase = 5;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.DebugPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.LoginRequestPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.UnsequencedDataPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.ClientHeartbeatClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Value.LogoutRequestClientPacketType;
            int selected = Position.Absent;
            long clientPacketTypeDependencyReadPosition;
            ReadOnlySpan<byte> clientPacketTypeValue;
            const int clientPacketTypeDependencyReadWidth = 1;
            clientPacketTypeDependencyReadPosition = clientPacketTypeDependencyWritePosition;
            if (clientPacketTypeDependencyReadPosition < Position.Minimum || clientPacketTypeDependencyReadPosition > bytes.Length || clientPacketTypeDependencyReadWidth > bytes.Length - clientPacketTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeValue = bytes.Slice((int)clientPacketTypeDependencyReadPosition, clientPacketTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule0Data2)) selected = DebugPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule1Data2)) selected = LoginRequestPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule2Data2)) selected = UnsequencedDataPacketCase;
            }
            if (selected == Position.Absent)
            {
                if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule3Data2)) selected = ClientHeartbeatCase;
            }
            if (selected == Position.Absent)
            {
                if (clientPacketTypeValue.SequenceEqual(ClientPayloadBranchRule4Data2)) selected = LogoutRequestCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            clientPacketTypeDependencyWriteUsed = true;
        }
    }

    internal static class ClientPacketWriter
    {
        internal static void ClientSoupBinTcpPacketPayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = end;
            if (checked((global::System.Int128)(limit)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
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
