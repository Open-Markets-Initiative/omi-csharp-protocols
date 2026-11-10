using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;
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
        internal bool UnsequencedDataPacket_UnsequencedMessageType_Used;
        internal bool EnterOrderMessage_AppendageLength_Used;
        internal bool EnterOrderAppendage_OptionalFieldLength_Used;
        internal bool EnterOrderAppendage_EnterOrderOptionalField_Used;
        internal bool ReplaceOrderMessage_AppendageLength_Used;
        internal bool ReplaceOrderAppendage_OptionalFieldLength_Used;
        internal bool ReplaceOrderAppendage_ReplaceOrderOptionalField_Used;
        internal bool CancelOrderMessage_AppendageLength_Used;
        internal bool CancelOrderAppendage_OptionalFieldLength_Used;
        internal bool CancelOrderAppendage_CancelOrderOptionalField_Used;
        internal bool ModifyOrderMessage_AppendageLength_Used;
        internal bool ModifyOrderAppendage_OptionalFieldLength_Used;
        internal bool ModifyOrderAppendage_ModifyOrderOptionalField_Used;
        internal bool AccountQueryMessage_AppendageLength_Used;
        internal bool AccountQueryAppendage_OptionalFieldLength_Used;
        internal bool AccountQueryAppendage_AccountQueryOptionalField_Used;
        internal bool MassCancelRequestMessage_AppendageLength_Used;
        internal bool MassCancelRequestAppendage_OptionalFieldLength_Used;
        internal bool MassCancelRequestAppendage_MassCancelRequestOptionalField_Used;
        internal bool DisableOrderEntryRequestMessage_AppendageLength_Used;
        internal bool DisableOrderEntryRequestAppendage_OptionalFieldLength_Used;
        internal bool DisableOrderEntryRequestAppendage_DisableOrderEntryRequestOptionalField_Used;
        internal bool EnableOrderEntryRequestMessage_AppendageLength_Used;
        internal bool EnableOrderEntryRequestAppendage_OptionalFieldLength_Used;
        internal bool EnableOrderEntryRequestAppendage_EnableOrderEntryRequestOptionalField_Used;
        internal bool ServerPacketHeader_PacketLength_Used;
        internal bool ServerPacketHeader_ServerPacketType_Used;
        internal bool SequencedDataPacket_SequencedMessageType_Used;
        internal bool OrderAcceptedMessage_AppendageLength_Used;
        internal bool OrderAcceptedAppendage_OptionalFieldLength_Used;
        internal bool OrderAcceptedAppendage_OrderAcceptedOptionalField_Used;
        internal bool ReplacedMessage_AppendageLength_Used;
        internal bool ReplacedMessageAppendage_OptionalFieldLength_Used;
        internal bool ReplacedMessageAppendage_ReplacedMessageOptionalField_Used;
        internal bool CanceledMessage_AppendageLength_Used;
        internal bool CanceledMessageAppendage_OptionalFieldLength_Used;
        internal bool CanceledMessageAppendage_CanceledMessageOptionalField_Used;
        internal bool AiqCanceledMessage_AppendageLength_Used;
        internal bool AiqCanceledAppendage_OptionalFieldLength_Used;
        internal bool AiqCanceledAppendage_AiqCanceledOptionalField_Used;
        internal bool OrderExecutedMessage_AppendageLength_Used;
        internal bool OrderExecutedAppendage_OptionalFieldLength_Used;
        internal bool OrderExecutedAppendage_OrderExecutedOptionalField_Used;
        internal bool BrokenTradeMessage_AppendageLength_Used;
        internal bool BrokenTradeAppendage_OptionalFieldLength_Used;
        internal bool BrokenTradeAppendage_BrokenTradeOptionalField_Used;
        internal bool RejectedOrderMessage_AppendageLength_Used;
        internal bool RejectedOrderAppendage_OptionalFieldLength_Used;
        internal bool RejectedOrderAppendage_RejectedOrderOptionalField_Used;
        internal bool CancelPendingMessage_AppendageLength_Used;
        internal bool CancelPendingAppendage_OptionalFieldLength_Used;
        internal bool CancelPendingAppendage_CancelPendingOptionalField_Used;
        internal bool CancelRejectMessage_AppendageLength_Used;
        internal bool CancelRejectAppendage_OptionalFieldLength_Used;
        internal bool CancelRejectAppendage_CancelRejectOptionalField_Used;
        internal bool OrderPriorityUpdateMessage_AppendageLength_Used;
        internal bool OrderPriorityUpdateAppendage_OptionalFieldLength_Used;
        internal bool OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used;
        internal bool OrderModifiedMessage_AppendageLength_Used;
        internal bool OrderModifiedAppendage_OptionalFieldLength_Used;
        internal bool OrderModifiedAppendage_OrderModifiedOptionalField_Used;
        internal bool OrderRestatedMessage_AppendageLength_Used;
        internal bool OrderRestatedAppendage_OptionalFieldLength_Used;
        internal bool OrderRestatedAppendage_OrderRestatedOptionalField_Used;
        internal bool AccountQueryResponseMessage_AppendageLength_Used;
        internal bool AccountQueryResponseAppendage_OptionalFieldLength_Used;
        internal bool AccountQueryResponseAppendage_AccountQueryResponseOptionalField_Used;
        internal bool MassCancelResponseMessage_AppendageLength_Used;
        internal bool MassCancelResponseAppendage_OptionalFieldLength_Used;
        internal bool MassCancelResponseAppendage_MassCancelResponseOptionalField_Used;
        internal bool DisableOrderEntryResponseMessage_AppendageLength_Used;
        internal bool DisableOrderEntryResponseAppendage_OptionalFieldLength_Used;
        internal bool DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used;
        internal bool EnableOrderEntryResponseMessage_AppendageLength_Used;
        internal bool EnableOrderEntryResponseAppendage_OptionalFieldLength_Used;
        internal bool EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used;
        internal int SequencedDataPacket_SequencedMessage_End;
        internal bool SequencedDataPacket_SequencedMessage_EndFrozen;
        internal int UnsequencedDataPacket_UnsequencedMessage_End;
        internal bool UnsequencedDataPacket_UnsequencedMessage_EndFrozen;
    }

    internal static class EnableOrderEntryResponseAppendageWriter
    {
        internal static void EnableOrderEntryResponseAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long EnableOrderEntryResponseAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
            byte enableOrderEntryResponseAppendageOptionalFieldLengthValue;
            if (!cursor.EnableOrderEntryResponseAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)enableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft2 => ((EnableOrderEntryResponseAppendageSizeRule1Data2) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft4 - EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.EnableOrderEntryResponseAppendage_OptionalFieldLength_Used = true;
            }
            const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
            enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = enableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition;
            if (enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            if (((enableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryResponseAppendageSizeRule1Data2) switch { var EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryResponseAppendage_OptionalFieldLength_Used = true;
        }

        internal static void EnableOrderEntryResponseOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnableOrderEntryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Value.UserRefIdxEnableOrderEntryResponseOptionalField;
            long enableOrderEntryResponseOptionalFieldDependencyReadPosition;
            byte enableOrderEntryResponseOptionalFieldValue;
            if (!cursor.EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used)
            {
                bytes[(int)enableOrderEntryResponseOptionalFieldDependencyWritePosition] = unchecked((byte)EnableOrderEntryResponseOptionalValueBranchRule0Data2);
                cursor.EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used = true;
            }
            const int enableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
            enableOrderEntryResponseOptionalFieldDependencyReadPosition = enableOrderEntryResponseOptionalFieldDependencyWritePosition;
            if (enableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryResponseOptionalFieldDependencyReadPosition > bytes.Length || enableOrderEntryResponseOptionalFieldDependencyReadWidth > bytes.Length - enableOrderEntryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryResponseOptionalFieldValue = bytes[(int)enableOrderEntryResponseOptionalFieldDependencyReadPosition];
            if (!(enableOrderEntryResponseOptionalFieldValue == EnableOrderEntryResponseOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used = true;
        }

        internal static void EnableOrderEntryResponseOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryResponseOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int EnableOrderEntryResponseOptionalValueUserRefIdxCase = 1;
            const byte EnableOrderEntryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Value.UserRefIdxEnableOrderEntryResponseOptionalField;
            int selected = Position.Absent;
            long enableOrderEntryResponseOptionalFieldDependencyReadPosition;
            byte enableOrderEntryResponseOptionalFieldValue;
            const int enableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
            enableOrderEntryResponseOptionalFieldDependencyReadPosition = enableOrderEntryResponseOptionalFieldDependencyWritePosition;
            if (enableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryResponseOptionalFieldDependencyReadPosition > bytes.Length || enableOrderEntryResponseOptionalFieldDependencyReadWidth > bytes.Length - enableOrderEntryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryResponseOptionalFieldValue = bytes[(int)enableOrderEntryResponseOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((enableOrderEntryResponseOptionalFieldValue == EnableOrderEntryResponseOptionalValueBranchRule0Data2)) selected = EnableOrderEntryResponseOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used = true;
        }
    }

    internal static class EnableOrderEntryResponseMessageWriter
    {
        internal static void EnableOrderEntryResponseAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryResponseMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
            ushort enableOrderEntryResponseMessageAppendageLengthValue;
            if (!cursor.EnableOrderEntryResponseMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)enableOrderEntryResponseMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft2 => ((index) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticRight3)))) }) })) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft4 - EnableOrderEntryResponseAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.EnableOrderEntryResponseMessage_AppendageLength_Used = true;
            }
            const int enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
            enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = enableOrderEntryResponseMessageAppendageLengthDependencyWritePosition;
            if (enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > bytes.Length || enableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > bytes.Length - enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
            if (((enableOrderEntryResponseMessageAppendageLengthValue) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryResponseMessage_AppendageLength_Used = true;
        }
    }

    internal static class DisableOrderEntryResponseAppendageWriter
    {
        internal static void DisableOrderEntryResponseAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long DisableOrderEntryResponseAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition;
            byte disableOrderEntryResponseAppendageOptionalFieldLengthValue;
            if (!cursor.DisableOrderEntryResponseAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)disableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft2 => ((DisableOrderEntryResponseAppendageSizeRule1Data2) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft4 - DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.DisableOrderEntryResponseAppendage_OptionalFieldLength_Used = true;
            }
            const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
            disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition = disableOrderEntryResponseAppendageOptionalFieldLengthDependencyWritePosition;
            if (disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryResponseAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            if (((disableOrderEntryResponseAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryResponseAppendageSizeRule1Data2) switch { var DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryResponseAppendage_OptionalFieldLength_Used = true;
        }

        internal static void DisableOrderEntryResponseOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte DisableOrderEntryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Value.UserRefIdxDisableOrderEntryResponseOptionalField;
            long disableOrderEntryResponseOptionalFieldDependencyReadPosition;
            byte disableOrderEntryResponseOptionalFieldValue;
            if (!cursor.DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used)
            {
                bytes[(int)disableOrderEntryResponseOptionalFieldDependencyWritePosition] = unchecked((byte)DisableOrderEntryResponseOptionalValueBranchRule0Data2);
                cursor.DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used = true;
            }
            const int disableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
            disableOrderEntryResponseOptionalFieldDependencyReadPosition = disableOrderEntryResponseOptionalFieldDependencyWritePosition;
            if (disableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryResponseOptionalFieldDependencyReadPosition > bytes.Length || disableOrderEntryResponseOptionalFieldDependencyReadWidth > bytes.Length - disableOrderEntryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryResponseOptionalFieldValue = bytes[(int)disableOrderEntryResponseOptionalFieldDependencyReadPosition];
            if (!(disableOrderEntryResponseOptionalFieldValue == DisableOrderEntryResponseOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used = true;
        }

        internal static void DisableOrderEntryResponseOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryResponseOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int DisableOrderEntryResponseOptionalValueUserRefIdxCase = 1;
            const byte DisableOrderEntryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Value.UserRefIdxDisableOrderEntryResponseOptionalField;
            int selected = Position.Absent;
            long disableOrderEntryResponseOptionalFieldDependencyReadPosition;
            byte disableOrderEntryResponseOptionalFieldValue;
            const int disableOrderEntryResponseOptionalFieldDependencyReadWidth = 1;
            disableOrderEntryResponseOptionalFieldDependencyReadPosition = disableOrderEntryResponseOptionalFieldDependencyWritePosition;
            if (disableOrderEntryResponseOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryResponseOptionalFieldDependencyReadPosition > bytes.Length || disableOrderEntryResponseOptionalFieldDependencyReadWidth > bytes.Length - disableOrderEntryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryResponseOptionalFieldValue = bytes[(int)disableOrderEntryResponseOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((disableOrderEntryResponseOptionalFieldValue == DisableOrderEntryResponseOptionalValueBranchRule0Data2)) selected = DisableOrderEntryResponseOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used = true;
        }
    }

    internal static class DisableOrderEntryResponseMessageWriter
    {
        internal static void DisableOrderEntryResponseAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryResponseMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition;
            ushort disableOrderEntryResponseMessageAppendageLengthValue;
            if (!cursor.DisableOrderEntryResponseMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)disableOrderEntryResponseMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft2 => ((index) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticRight3)))) }) })) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft4 - DisableOrderEntryResponseAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.DisableOrderEntryResponseMessage_AppendageLength_Used = true;
            }
            const int disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth = 2;
            disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition = disableOrderEntryResponseMessageAppendageLengthDependencyWritePosition;
            if (disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition > bytes.Length || disableOrderEntryResponseMessageAppendageLengthDependencyReadWidth > bytes.Length - disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryResponseMessageAppendageLengthDependencyReadPosition)..]);
            if (((disableOrderEntryResponseMessageAppendageLengthValue) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryResponseAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryResponseMessage_AppendageLength_Used = true;
        }
    }

    internal static class MassCancelResponseAppendageWriter
    {
        internal static void MassCancelResponseAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long MassCancelResponseAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition;
            byte massCancelResponseAppendageOptionalFieldLengthValue;
            if (!cursor.MassCancelResponseAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)massCancelResponseAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var MassCancelResponseAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var MassCancelResponseAppendageSizeRule1ArithmeticLeft2 => ((MassCancelResponseAppendageSizeRule1Data2) switch { var MassCancelResponseAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var MassCancelResponseAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((MassCancelResponseAppendageSizeRule1ArithmeticLeft4 - MassCancelResponseAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.MassCancelResponseAppendage_OptionalFieldLength_Used = true;
            }
            const int massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
            massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition = massCancelResponseAppendageOptionalFieldLengthDependencyWritePosition;
            if (massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || massCancelResponseAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseAppendageOptionalFieldLengthValue = bytes[(int)massCancelResponseAppendageOptionalFieldLengthDependencyReadPosition];
            if (((massCancelResponseAppendageOptionalFieldLengthValue) switch { var MassCancelResponseAppendageSizeRule1ArithmeticLeft0 => ((MassCancelResponseAppendageSizeRule1Data2) switch { var MassCancelResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseAppendage_OptionalFieldLength_Used = true;
        }

        internal static void MassCancelResponseOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.SideMassCancelResponseOptionalField;
            long massCancelResponseOptionalFieldDependencyReadPosition;
            byte massCancelResponseOptionalFieldValue;
            if (!cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used)
            {
                bytes[(int)massCancelResponseOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelResponseOptionalValueBranchRule0Data2);
                cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
            }
            const int massCancelResponseOptionalFieldDependencyReadWidth = 1;
            massCancelResponseOptionalFieldDependencyReadPosition = massCancelResponseOptionalFieldDependencyWritePosition;
            if (massCancelResponseOptionalFieldDependencyReadPosition < Position.Minimum || massCancelResponseOptionalFieldDependencyReadPosition > bytes.Length || massCancelResponseOptionalFieldDependencyReadWidth > bytes.Length - massCancelResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseOptionalFieldValue = bytes[(int)massCancelResponseOptionalFieldDependencyReadPosition];
            if (!(massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
        }

        internal static void MassCancelResponseOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelResponseOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.GroupIdMassCancelResponseOptionalField;
            long massCancelResponseOptionalFieldDependencyReadPosition;
            byte massCancelResponseOptionalFieldValue;
            if (!cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used)
            {
                bytes[(int)massCancelResponseOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelResponseOptionalValueBranchRule1Data2);
                cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
            }
            const int massCancelResponseOptionalFieldDependencyReadWidth = 1;
            massCancelResponseOptionalFieldDependencyReadPosition = massCancelResponseOptionalFieldDependencyWritePosition;
            if (massCancelResponseOptionalFieldDependencyReadPosition < Position.Minimum || massCancelResponseOptionalFieldDependencyReadPosition > bytes.Length || massCancelResponseOptionalFieldDependencyReadWidth > bytes.Length - massCancelResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseOptionalFieldValue = bytes[(int)massCancelResponseOptionalFieldDependencyReadPosition];
            if (!(massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
        }

        internal static void MassCancelResponseOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelResponseOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.UserRefIdxMassCancelResponseOptionalField;
            long massCancelResponseOptionalFieldDependencyReadPosition;
            byte massCancelResponseOptionalFieldValue;
            if (!cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used)
            {
                bytes[(int)massCancelResponseOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelResponseOptionalValueBranchRule2Data2);
                cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
            }
            const int massCancelResponseOptionalFieldDependencyReadWidth = 1;
            massCancelResponseOptionalFieldDependencyReadPosition = massCancelResponseOptionalFieldDependencyWritePosition;
            if (massCancelResponseOptionalFieldDependencyReadPosition < Position.Minimum || massCancelResponseOptionalFieldDependencyReadPosition > bytes.Length || massCancelResponseOptionalFieldDependencyReadWidth > bytes.Length - massCancelResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseOptionalFieldValue = bytes[(int)massCancelResponseOptionalFieldDependencyReadPosition];
            if (!(massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
        }

        internal static void MassCancelResponseOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int MassCancelResponseOptionalValueSideCase = 1;
            const int MassCancelResponseOptionalValueGroupIdCase = 2;
            const int MassCancelResponseOptionalValueUserRefIdxCase = 3;
            const byte MassCancelResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.SideMassCancelResponseOptionalField;
            const byte MassCancelResponseOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.GroupIdMassCancelResponseOptionalField;
            const byte MassCancelResponseOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Value.UserRefIdxMassCancelResponseOptionalField;
            int selected = Position.Absent;
            long massCancelResponseOptionalFieldDependencyReadPosition;
            byte massCancelResponseOptionalFieldValue;
            const int massCancelResponseOptionalFieldDependencyReadWidth = 1;
            massCancelResponseOptionalFieldDependencyReadPosition = massCancelResponseOptionalFieldDependencyWritePosition;
            if (massCancelResponseOptionalFieldDependencyReadPosition < Position.Minimum || massCancelResponseOptionalFieldDependencyReadPosition > bytes.Length || massCancelResponseOptionalFieldDependencyReadWidth > bytes.Length - massCancelResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseOptionalFieldValue = bytes[(int)massCancelResponseOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule0Data2)) selected = MassCancelResponseOptionalValueSideCase;
            }
            if (selected == Position.Absent)
            {
                if ((massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule1Data2)) selected = MassCancelResponseOptionalValueGroupIdCase;
            }
            if (selected == Position.Absent)
            {
                if ((massCancelResponseOptionalFieldValue == MassCancelResponseOptionalValueBranchRule2Data2)) selected = MassCancelResponseOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseAppendage_MassCancelResponseOptionalField_Used = true;
        }
    }

    internal static class MassCancelResponseMessageWriter
    {
        internal static void MassCancelResponseAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelResponseMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long massCancelResponseMessageAppendageLengthDependencyReadPosition;
            ushort massCancelResponseMessageAppendageLengthValue;
            if (!cursor.MassCancelResponseMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)massCancelResponseMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var MassCancelResponseAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var MassCancelResponseAppendageCountRule0ArithmeticLeft2 => ((index) switch { var MassCancelResponseAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticRight3)))) }) })) switch { var MassCancelResponseAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((MassCancelResponseAppendageCountRule0ArithmeticLeft4 - MassCancelResponseAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.MassCancelResponseMessage_AppendageLength_Used = true;
            }
            const int massCancelResponseMessageAppendageLengthDependencyReadWidth = 2;
            massCancelResponseMessageAppendageLengthDependencyReadPosition = massCancelResponseMessageAppendageLengthDependencyWritePosition;
            if (massCancelResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelResponseMessageAppendageLengthDependencyReadPosition > bytes.Length || massCancelResponseMessageAppendageLengthDependencyReadWidth > bytes.Length - massCancelResponseMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelResponseMessageAppendageLengthDependencyReadPosition)..]);
            if (((massCancelResponseMessageAppendageLengthValue) switch { var MassCancelResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelResponseAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelResponseMessage_AppendageLength_Used = true;
        }
    }

    internal static class AccountQueryResponseAppendageWriter
    {
        internal static void AccountQueryResponseAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryResponseAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long AccountQueryResponseAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition;
            byte accountQueryResponseAppendageOptionalFieldLengthValue;
            if (!cursor.AccountQueryResponseAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)accountQueryResponseAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticLeft2 => ((AccountQueryResponseAppendageSizeRule1Data2) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((AccountQueryResponseAppendageSizeRule1ArithmeticLeft4 - AccountQueryResponseAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.AccountQueryResponseAppendage_OptionalFieldLength_Used = true;
            }
            const int accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth = 1;
            accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition = accountQueryResponseAppendageOptionalFieldLengthDependencyWritePosition;
            if (accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || accountQueryResponseAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryResponseAppendageOptionalFieldLengthValue = bytes[(int)accountQueryResponseAppendageOptionalFieldLengthDependencyReadPosition];
            if (((accountQueryResponseAppendageOptionalFieldLengthValue) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryResponseAppendageSizeRule1Data2) switch { var AccountQueryResponseAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryResponseAppendage_OptionalFieldLength_Used = true;
        }

        internal static void AccountQueryResponseOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryResponseOptionalFieldDependencyWritePosition, int index)
        {
            const byte AccountQueryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Value.UserRefIdxAccountQueryResponseOptionalField;
            long accountQueryResponseOptionalFieldDependencyReadPosition;
            byte accountQueryResponseOptionalFieldValue;
            if (!cursor.AccountQueryResponseAppendage_AccountQueryResponseOptionalField_Used)
            {
                bytes[(int)accountQueryResponseOptionalFieldDependencyWritePosition] = unchecked((byte)AccountQueryResponseOptionalValueBranchRule0Data2);
                cursor.AccountQueryResponseAppendage_AccountQueryResponseOptionalField_Used = true;
            }
            const int accountQueryResponseOptionalFieldDependencyReadWidth = 1;
            accountQueryResponseOptionalFieldDependencyReadPosition = accountQueryResponseOptionalFieldDependencyWritePosition;
            if (accountQueryResponseOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryResponseOptionalFieldDependencyReadPosition > bytes.Length || accountQueryResponseOptionalFieldDependencyReadWidth > bytes.Length - accountQueryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryResponseOptionalFieldValue = bytes[(int)accountQueryResponseOptionalFieldDependencyReadPosition];
            if (!(accountQueryResponseOptionalFieldValue == AccountQueryResponseOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryResponseAppendage_AccountQueryResponseOptionalField_Used = true;
        }

        internal static void AccountQueryResponseOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryResponseOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int AccountQueryResponseOptionalValueUserRefIdxCase = 1;
            const byte AccountQueryResponseOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Value.UserRefIdxAccountQueryResponseOptionalField;
            int selected = Position.Absent;
            long accountQueryResponseOptionalFieldDependencyReadPosition;
            byte accountQueryResponseOptionalFieldValue;
            const int accountQueryResponseOptionalFieldDependencyReadWidth = 1;
            accountQueryResponseOptionalFieldDependencyReadPosition = accountQueryResponseOptionalFieldDependencyWritePosition;
            if (accountQueryResponseOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryResponseOptionalFieldDependencyReadPosition > bytes.Length || accountQueryResponseOptionalFieldDependencyReadWidth > bytes.Length - accountQueryResponseOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryResponseOptionalFieldValue = bytes[(int)accountQueryResponseOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((accountQueryResponseOptionalFieldValue == AccountQueryResponseOptionalValueBranchRule0Data2)) selected = AccountQueryResponseOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryResponseAppendage_AccountQueryResponseOptionalField_Used = true;
        }
    }

    internal static class AccountQueryResponseMessageWriter
    {
        internal static void AccountQueryResponseMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = AccountQueryResponseMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void AccountQueryResponseAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryResponseMessageAppendageLengthDependencyWritePosition, bool accountQueryResponseMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long accountQueryResponseMessageAppendageLengthDependencyReadPosition = default;
            ushort accountQueryResponseMessageAppendageLengthValue = default;
            if (accountQueryResponseMessageAppendageLengthExists && !cursor.AccountQueryResponseMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)accountQueryResponseMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var AccountQueryResponseAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var AccountQueryResponseAppendageCountRule0ArithmeticLeft2 => ((index) switch { var AccountQueryResponseAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticRight3)))) }) })) switch { var AccountQueryResponseAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((AccountQueryResponseAppendageCountRule0ArithmeticLeft4 - AccountQueryResponseAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.AccountQueryResponseMessage_AppendageLength_Used = true;
            }
            if (accountQueryResponseMessageAppendageLengthExists)
            {
                const int accountQueryResponseMessageAppendageLengthDependencyReadWidth = 2;
                accountQueryResponseMessageAppendageLengthDependencyReadPosition = accountQueryResponseMessageAppendageLengthDependencyWritePosition;
                if (accountQueryResponseMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryResponseMessageAppendageLengthDependencyReadPosition > bytes.Length || accountQueryResponseMessageAppendageLengthDependencyReadWidth > bytes.Length - accountQueryResponseMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                accountQueryResponseMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryResponseMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((accountQueryResponseMessageAppendageLengthValue) switch { var AccountQueryResponseAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryResponseAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryResponseAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryResponseMessage_AppendageLength_Used = true;
        }

        internal static bool AccountQueryResponseMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class OrderRestatedAppendageWriter
    {
        internal static void OrderRestatedAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long OrderRestatedAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long orderRestatedAppendageOptionalFieldLengthDependencyReadPosition;
            byte orderRestatedAppendageOptionalFieldLengthValue;
            if (!cursor.OrderRestatedAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)orderRestatedAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var OrderRestatedAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderRestatedAppendageSizeRule1ArithmeticLeft2 => ((OrderRestatedAppendageSizeRule1Data2) switch { var OrderRestatedAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var OrderRestatedAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((OrderRestatedAppendageSizeRule1ArithmeticLeft4 - OrderRestatedAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.OrderRestatedAppendage_OptionalFieldLength_Used = true;
            }
            const int orderRestatedAppendageOptionalFieldLengthDependencyReadWidth = 1;
            orderRestatedAppendageOptionalFieldLengthDependencyReadPosition = orderRestatedAppendageOptionalFieldLengthDependencyWritePosition;
            if (orderRestatedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderRestatedAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || orderRestatedAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - orderRestatedAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedAppendageOptionalFieldLengthValue = bytes[(int)orderRestatedAppendageOptionalFieldLengthDependencyReadPosition];
            if (((orderRestatedAppendageOptionalFieldLengthValue) switch { var OrderRestatedAppendageSizeRule1ArithmeticLeft0 => ((OrderRestatedAppendageSizeRule1Data2) switch { var OrderRestatedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OptionalFieldLength_Used = true;
        }

        internal static void OrderRestatedOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderRestatedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.DisplayQuantityOrderRestatedOptionalField;
            long orderRestatedOptionalFieldDependencyReadPosition;
            byte orderRestatedOptionalFieldValue;
            if (!cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used)
            {
                bytes[(int)orderRestatedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderRestatedOptionalValueBranchRule0Data2);
                cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
            }
            const int orderRestatedOptionalFieldDependencyReadWidth = 1;
            orderRestatedOptionalFieldDependencyReadPosition = orderRestatedOptionalFieldDependencyWritePosition;
            if (orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > bytes.Length || orderRestatedOptionalFieldDependencyReadWidth > bytes.Length - orderRestatedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
            if (!(orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
        }

        internal static void OrderRestatedOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderRestatedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.DisplayPriceOrderRestatedOptionalField;
            long orderRestatedOptionalFieldDependencyReadPosition;
            byte orderRestatedOptionalFieldValue;
            if (!cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used)
            {
                bytes[(int)orderRestatedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderRestatedOptionalValueBranchRule1Data2);
                cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
            }
            const int orderRestatedOptionalFieldDependencyReadWidth = 1;
            orderRestatedOptionalFieldDependencyReadPosition = orderRestatedOptionalFieldDependencyWritePosition;
            if (orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > bytes.Length || orderRestatedOptionalFieldDependencyReadWidth > bytes.Length - orderRestatedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
            if (!(orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
        }

        internal static void OrderRestatedOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderRestatedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.SecondaryOrdRefNumOrderRestatedOptionalField;
            long orderRestatedOptionalFieldDependencyReadPosition;
            byte orderRestatedOptionalFieldValue;
            if (!cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used)
            {
                bytes[(int)orderRestatedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderRestatedOptionalValueBranchRule2Data2);
                cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
            }
            const int orderRestatedOptionalFieldDependencyReadWidth = 1;
            orderRestatedOptionalFieldDependencyReadPosition = orderRestatedOptionalFieldDependencyWritePosition;
            if (orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > bytes.Length || orderRestatedOptionalFieldDependencyReadWidth > bytes.Length - orderRestatedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
            if (!(orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
        }

        internal static void OrderRestatedOptionalValueBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderRestatedOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.UserRefIdxOrderRestatedOptionalField;
            long orderRestatedOptionalFieldDependencyReadPosition;
            byte orderRestatedOptionalFieldValue;
            if (!cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used)
            {
                bytes[(int)orderRestatedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderRestatedOptionalValueBranchRule3Data2);
                cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
            }
            const int orderRestatedOptionalFieldDependencyReadWidth = 1;
            orderRestatedOptionalFieldDependencyReadPosition = orderRestatedOptionalFieldDependencyWritePosition;
            if (orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > bytes.Length || orderRestatedOptionalFieldDependencyReadWidth > bytes.Length - orderRestatedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
            if (!(orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
        }

        internal static void OrderRestatedOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int DisplayQuantityCase = 1;
            const int DisplayPriceCase = 2;
            const int SecondaryOrdRefNumCase = 3;
            const int OrderRestatedOptionalValueUserRefIdxCase = 4;
            const byte OrderRestatedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.DisplayQuantityOrderRestatedOptionalField;
            const byte OrderRestatedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.DisplayPriceOrderRestatedOptionalField;
            const byte OrderRestatedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.SecondaryOrdRefNumOrderRestatedOptionalField;
            const byte OrderRestatedOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Value.UserRefIdxOrderRestatedOptionalField;
            int selected = Position.Absent;
            long orderRestatedOptionalFieldDependencyReadPosition;
            byte orderRestatedOptionalFieldValue;
            const int orderRestatedOptionalFieldDependencyReadWidth = 1;
            orderRestatedOptionalFieldDependencyReadPosition = orderRestatedOptionalFieldDependencyWritePosition;
            if (orderRestatedOptionalFieldDependencyReadPosition < Position.Minimum || orderRestatedOptionalFieldDependencyReadPosition > bytes.Length || orderRestatedOptionalFieldDependencyReadWidth > bytes.Length - orderRestatedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedOptionalFieldValue = bytes[(int)orderRestatedOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule0Data2)) selected = DisplayQuantityCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule1Data2)) selected = DisplayPriceCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule2Data2)) selected = SecondaryOrdRefNumCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderRestatedOptionalFieldValue == OrderRestatedOptionalValueBranchRule3Data2)) selected = OrderRestatedOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedAppendage_OrderRestatedOptionalField_Used = true;
        }
    }

    internal static class OrderRestatedMessageWriter
    {
        internal static void OrderRestatedAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderRestatedMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long orderRestatedMessageAppendageLengthDependencyReadPosition;
            ushort orderRestatedMessageAppendageLengthValue;
            if (!cursor.OrderRestatedMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)orderRestatedMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var OrderRestatedAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderRestatedAppendageCountRule0ArithmeticLeft2 => ((index) switch { var OrderRestatedAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticRight3)))) }) })) switch { var OrderRestatedAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((OrderRestatedAppendageCountRule0ArithmeticLeft4 - OrderRestatedAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.OrderRestatedMessage_AppendageLength_Used = true;
            }
            const int orderRestatedMessageAppendageLengthDependencyReadWidth = 2;
            orderRestatedMessageAppendageLengthDependencyReadPosition = orderRestatedMessageAppendageLengthDependencyWritePosition;
            if (orderRestatedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderRestatedMessageAppendageLengthDependencyReadPosition > bytes.Length || orderRestatedMessageAppendageLengthDependencyReadWidth > bytes.Length - orderRestatedMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderRestatedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderRestatedMessageAppendageLengthDependencyReadPosition)..]);
            if (((orderRestatedMessageAppendageLengthValue) switch { var OrderRestatedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderRestatedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderRestatedAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderRestatedMessage_AppendageLength_Used = true;
        }
    }

    internal static class OrderModifiedAppendageWriter
    {
        internal static void OrderModifiedAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long OrderModifiedAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long orderModifiedAppendageOptionalFieldLengthDependencyReadPosition;
            byte orderModifiedAppendageOptionalFieldLengthValue;
            if (!cursor.OrderModifiedAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)orderModifiedAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var OrderModifiedAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderModifiedAppendageSizeRule1ArithmeticLeft2 => ((OrderModifiedAppendageSizeRule1Data2) switch { var OrderModifiedAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var OrderModifiedAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((OrderModifiedAppendageSizeRule1ArithmeticLeft4 - OrderModifiedAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.OrderModifiedAppendage_OptionalFieldLength_Used = true;
            }
            const int orderModifiedAppendageOptionalFieldLengthDependencyReadWidth = 1;
            orderModifiedAppendageOptionalFieldLengthDependencyReadPosition = orderModifiedAppendageOptionalFieldLengthDependencyWritePosition;
            if (orderModifiedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderModifiedAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || orderModifiedAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - orderModifiedAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderModifiedAppendageOptionalFieldLengthValue = bytes[(int)orderModifiedAppendageOptionalFieldLengthDependencyReadPosition];
            if (((orderModifiedAppendageOptionalFieldLengthValue) switch { var OrderModifiedAppendageSizeRule1ArithmeticLeft0 => ((OrderModifiedAppendageSizeRule1Data2) switch { var OrderModifiedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedAppendage_OptionalFieldLength_Used = true;
        }

        internal static void OrderModifiedOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderModifiedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.UserRefIdxOrderModifiedOptionalField;
            long orderModifiedOptionalFieldDependencyReadPosition;
            byte orderModifiedOptionalFieldValue;
            if (!cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used)
            {
                bytes[(int)orderModifiedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderModifiedOptionalValueBranchRule0Data2);
                cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
            }
            const int orderModifiedOptionalFieldDependencyReadWidth = 1;
            orderModifiedOptionalFieldDependencyReadPosition = orderModifiedOptionalFieldDependencyWritePosition;
            if (orderModifiedOptionalFieldDependencyReadPosition < Position.Minimum || orderModifiedOptionalFieldDependencyReadPosition > bytes.Length || orderModifiedOptionalFieldDependencyReadWidth > bytes.Length - orderModifiedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderModifiedOptionalFieldValue = bytes[(int)orderModifiedOptionalFieldDependencyReadPosition];
            if (!(orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
        }

        internal static void OrderModifiedOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderModifiedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.SharesLocatedOrderModifiedOptionalField;
            long orderModifiedOptionalFieldDependencyReadPosition;
            byte orderModifiedOptionalFieldValue;
            if (!cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used)
            {
                bytes[(int)orderModifiedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderModifiedOptionalValueBranchRule1Data2);
                cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
            }
            const int orderModifiedOptionalFieldDependencyReadWidth = 1;
            orderModifiedOptionalFieldDependencyReadPosition = orderModifiedOptionalFieldDependencyWritePosition;
            if (orderModifiedOptionalFieldDependencyReadPosition < Position.Minimum || orderModifiedOptionalFieldDependencyReadPosition > bytes.Length || orderModifiedOptionalFieldDependencyReadWidth > bytes.Length - orderModifiedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderModifiedOptionalFieldValue = bytes[(int)orderModifiedOptionalFieldDependencyReadPosition];
            if (!(orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
        }

        internal static void OrderModifiedOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderModifiedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.LocateBrokerOrderModifiedOptionalField;
            long orderModifiedOptionalFieldDependencyReadPosition;
            byte orderModifiedOptionalFieldValue;
            if (!cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used)
            {
                bytes[(int)orderModifiedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderModifiedOptionalValueBranchRule2Data2);
                cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
            }
            const int orderModifiedOptionalFieldDependencyReadWidth = 1;
            orderModifiedOptionalFieldDependencyReadPosition = orderModifiedOptionalFieldDependencyWritePosition;
            if (orderModifiedOptionalFieldDependencyReadPosition < Position.Minimum || orderModifiedOptionalFieldDependencyReadPosition > bytes.Length || orderModifiedOptionalFieldDependencyReadWidth > bytes.Length - orderModifiedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderModifiedOptionalFieldValue = bytes[(int)orderModifiedOptionalFieldDependencyReadPosition];
            if (!(orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
        }

        internal static void OrderModifiedOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int OrderModifiedOptionalValueUserRefIdxCase = 1;
            const int OrderModifiedOptionalValueSharesLocatedCase = 2;
            const int LocateBrokerCase = 3;
            const byte OrderModifiedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.UserRefIdxOrderModifiedOptionalField;
            const byte OrderModifiedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.SharesLocatedOrderModifiedOptionalField;
            const byte OrderModifiedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value.LocateBrokerOrderModifiedOptionalField;
            int selected = Position.Absent;
            long orderModifiedOptionalFieldDependencyReadPosition;
            byte orderModifiedOptionalFieldValue;
            const int orderModifiedOptionalFieldDependencyReadWidth = 1;
            orderModifiedOptionalFieldDependencyReadPosition = orderModifiedOptionalFieldDependencyWritePosition;
            if (orderModifiedOptionalFieldDependencyReadPosition < Position.Minimum || orderModifiedOptionalFieldDependencyReadPosition > bytes.Length || orderModifiedOptionalFieldDependencyReadWidth > bytes.Length - orderModifiedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderModifiedOptionalFieldValue = bytes[(int)orderModifiedOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule0Data2)) selected = OrderModifiedOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule1Data2)) selected = OrderModifiedOptionalValueSharesLocatedCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderModifiedOptionalFieldValue == OrderModifiedOptionalValueBranchRule2Data2)) selected = LocateBrokerCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = true;
        }
    }

    internal static class OrderModifiedMessageWriter
    {
        internal static void OrderModifiedMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = OrderModifiedMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void OrderModifiedAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderModifiedMessageAppendageLengthDependencyWritePosition, bool orderModifiedMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long orderModifiedMessageAppendageLengthDependencyReadPosition = default;
            ushort orderModifiedMessageAppendageLengthValue = default;
            if (orderModifiedMessageAppendageLengthExists && !cursor.OrderModifiedMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)orderModifiedMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var OrderModifiedAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderModifiedAppendageCountRule0ArithmeticLeft2 => ((index) switch { var OrderModifiedAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticRight3)))) }) })) switch { var OrderModifiedAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((OrderModifiedAppendageCountRule0ArithmeticLeft4 - OrderModifiedAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.OrderModifiedMessage_AppendageLength_Used = true;
            }
            if (orderModifiedMessageAppendageLengthExists)
            {
                const int orderModifiedMessageAppendageLengthDependencyReadWidth = 2;
                orderModifiedMessageAppendageLengthDependencyReadPosition = orderModifiedMessageAppendageLengthDependencyWritePosition;
                if (orderModifiedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderModifiedMessageAppendageLengthDependencyReadPosition > bytes.Length || orderModifiedMessageAppendageLengthDependencyReadWidth > bytes.Length - orderModifiedMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                orderModifiedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderModifiedMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((orderModifiedMessageAppendageLengthValue) switch { var OrderModifiedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderModifiedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderModifiedAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderModifiedMessage_AppendageLength_Used = true;
        }

        internal static bool OrderModifiedMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class OrderPriorityUpdateAppendageWriter
    {
        internal static void OrderPriorityUpdateAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderPriorityUpdateAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long OrderPriorityUpdateAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition;
            byte orderPriorityUpdateAppendageOptionalFieldLengthValue;
            if (!cursor.OrderPriorityUpdateAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)orderPriorityUpdateAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft2 => ((OrderPriorityUpdateAppendageSizeRule1Data2) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft4 - OrderPriorityUpdateAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.OrderPriorityUpdateAppendage_OptionalFieldLength_Used = true;
            }
            const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth = 1;
            orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition = orderPriorityUpdateAppendageOptionalFieldLengthDependencyWritePosition;
            if (orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderPriorityUpdateAppendageOptionalFieldLengthValue = bytes[(int)orderPriorityUpdateAppendageOptionalFieldLengthDependencyReadPosition];
            if (((orderPriorityUpdateAppendageOptionalFieldLengthValue) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0 => ((OrderPriorityUpdateAppendageSizeRule1Data2) switch { var OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderPriorityUpdateAppendage_OptionalFieldLength_Used = true;
        }

        internal static void OrderPriorityUpdateOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderPriorityUpdateOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderPriorityUpdateOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Value.UserRefIdxOrderPriorityUpdateOptionalField;
            long orderPriorityUpdateOptionalFieldDependencyReadPosition;
            byte orderPriorityUpdateOptionalFieldValue;
            if (!cursor.OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used)
            {
                bytes[(int)orderPriorityUpdateOptionalFieldDependencyWritePosition] = unchecked((byte)OrderPriorityUpdateOptionalValueBranchRule0Data2);
                cursor.OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used = true;
            }
            const int orderPriorityUpdateOptionalFieldDependencyReadWidth = 1;
            orderPriorityUpdateOptionalFieldDependencyReadPosition = orderPriorityUpdateOptionalFieldDependencyWritePosition;
            if (orderPriorityUpdateOptionalFieldDependencyReadPosition < Position.Minimum || orderPriorityUpdateOptionalFieldDependencyReadPosition > bytes.Length || orderPriorityUpdateOptionalFieldDependencyReadWidth > bytes.Length - orderPriorityUpdateOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderPriorityUpdateOptionalFieldValue = bytes[(int)orderPriorityUpdateOptionalFieldDependencyReadPosition];
            if (!(orderPriorityUpdateOptionalFieldValue == OrderPriorityUpdateOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used = true;
        }

        internal static void OrderPriorityUpdateOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderPriorityUpdateOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int OrderPriorityUpdateOptionalValueUserRefIdxCase = 1;
            const byte OrderPriorityUpdateOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Value.UserRefIdxOrderPriorityUpdateOptionalField;
            int selected = Position.Absent;
            long orderPriorityUpdateOptionalFieldDependencyReadPosition;
            byte orderPriorityUpdateOptionalFieldValue;
            const int orderPriorityUpdateOptionalFieldDependencyReadWidth = 1;
            orderPriorityUpdateOptionalFieldDependencyReadPosition = orderPriorityUpdateOptionalFieldDependencyWritePosition;
            if (orderPriorityUpdateOptionalFieldDependencyReadPosition < Position.Minimum || orderPriorityUpdateOptionalFieldDependencyReadPosition > bytes.Length || orderPriorityUpdateOptionalFieldDependencyReadWidth > bytes.Length - orderPriorityUpdateOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderPriorityUpdateOptionalFieldValue = bytes[(int)orderPriorityUpdateOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((orderPriorityUpdateOptionalFieldValue == OrderPriorityUpdateOptionalValueBranchRule0Data2)) selected = OrderPriorityUpdateOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used = true;
        }
    }

    internal static class OrderPriorityUpdateMessageWriter
    {
        internal static void OrderPriorityUpdateMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = OrderPriorityUpdateMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void OrderPriorityUpdateAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderPriorityUpdateMessageAppendageLengthDependencyWritePosition, bool orderPriorityUpdateMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = default;
            ushort orderPriorityUpdateMessageAppendageLengthValue = default;
            if (orderPriorityUpdateMessageAppendageLengthExists && !cursor.OrderPriorityUpdateMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)orderPriorityUpdateMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticLeft2 => ((index) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticRight3)))) }) })) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((OrderPriorityUpdateAppendageCountRule0ArithmeticLeft4 - OrderPriorityUpdateAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.OrderPriorityUpdateMessage_AppendageLength_Used = true;
            }
            if (orderPriorityUpdateMessageAppendageLengthExists)
            {
                const int orderPriorityUpdateMessageAppendageLengthDependencyReadWidth = 2;
                orderPriorityUpdateMessageAppendageLengthDependencyReadPosition = orderPriorityUpdateMessageAppendageLengthDependencyWritePosition;
                if (orderPriorityUpdateMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderPriorityUpdateMessageAppendageLengthDependencyReadPosition > bytes.Length || orderPriorityUpdateMessageAppendageLengthDependencyReadWidth > bytes.Length - orderPriorityUpdateMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                orderPriorityUpdateMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderPriorityUpdateMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((orderPriorityUpdateMessageAppendageLengthValue) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderPriorityUpdateAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderPriorityUpdateAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderPriorityUpdateMessage_AppendageLength_Used = true;
        }

        internal static bool OrderPriorityUpdateMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class CancelRejectAppendageWriter
    {
        internal static void CancelRejectAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelRejectAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long CancelRejectAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long cancelRejectAppendageOptionalFieldLengthDependencyReadPosition;
            byte cancelRejectAppendageOptionalFieldLengthValue;
            if (!cursor.CancelRejectAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)cancelRejectAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var CancelRejectAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelRejectAppendageSizeRule1ArithmeticLeft2 => ((CancelRejectAppendageSizeRule1Data2) switch { var CancelRejectAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var CancelRejectAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((CancelRejectAppendageSizeRule1ArithmeticLeft4 - CancelRejectAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.CancelRejectAppendage_OptionalFieldLength_Used = true;
            }
            const int cancelRejectAppendageOptionalFieldLengthDependencyReadWidth = 1;
            cancelRejectAppendageOptionalFieldLengthDependencyReadPosition = cancelRejectAppendageOptionalFieldLengthDependencyWritePosition;
            if (cancelRejectAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelRejectAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || cancelRejectAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - cancelRejectAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelRejectAppendageOptionalFieldLengthValue = bytes[(int)cancelRejectAppendageOptionalFieldLengthDependencyReadPosition];
            if (((cancelRejectAppendageOptionalFieldLengthValue) switch { var CancelRejectAppendageSizeRule1ArithmeticLeft0 => ((CancelRejectAppendageSizeRule1Data2) switch { var CancelRejectAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelRejectAppendage_OptionalFieldLength_Used = true;
        }

        internal static void CancelRejectOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelRejectOptionalFieldDependencyWritePosition, int index)
        {
            const byte CancelRejectOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Value.UserRefIdxCancelRejectOptionalField;
            long cancelRejectOptionalFieldDependencyReadPosition;
            byte cancelRejectOptionalFieldValue;
            if (!cursor.CancelRejectAppendage_CancelRejectOptionalField_Used)
            {
                bytes[(int)cancelRejectOptionalFieldDependencyWritePosition] = unchecked((byte)CancelRejectOptionalValueBranchRule0Data2);
                cursor.CancelRejectAppendage_CancelRejectOptionalField_Used = true;
            }
            const int cancelRejectOptionalFieldDependencyReadWidth = 1;
            cancelRejectOptionalFieldDependencyReadPosition = cancelRejectOptionalFieldDependencyWritePosition;
            if (cancelRejectOptionalFieldDependencyReadPosition < Position.Minimum || cancelRejectOptionalFieldDependencyReadPosition > bytes.Length || cancelRejectOptionalFieldDependencyReadWidth > bytes.Length - cancelRejectOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelRejectOptionalFieldValue = bytes[(int)cancelRejectOptionalFieldDependencyReadPosition];
            if (!(cancelRejectOptionalFieldValue == CancelRejectOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelRejectAppendage_CancelRejectOptionalField_Used = true;
        }

        internal static void CancelRejectOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelRejectOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int CancelRejectOptionalValueUserRefIdxCase = 1;
            const byte CancelRejectOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Value.UserRefIdxCancelRejectOptionalField;
            int selected = Position.Absent;
            long cancelRejectOptionalFieldDependencyReadPosition;
            byte cancelRejectOptionalFieldValue;
            const int cancelRejectOptionalFieldDependencyReadWidth = 1;
            cancelRejectOptionalFieldDependencyReadPosition = cancelRejectOptionalFieldDependencyWritePosition;
            if (cancelRejectOptionalFieldDependencyReadPosition < Position.Minimum || cancelRejectOptionalFieldDependencyReadPosition > bytes.Length || cancelRejectOptionalFieldDependencyReadWidth > bytes.Length - cancelRejectOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelRejectOptionalFieldValue = bytes[(int)cancelRejectOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((cancelRejectOptionalFieldValue == CancelRejectOptionalValueBranchRule0Data2)) selected = CancelRejectOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelRejectAppendage_CancelRejectOptionalField_Used = true;
        }
    }

    internal static class CancelRejectMessageWriter
    {
        internal static void CancelRejectMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = CancelRejectMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void CancelRejectAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelRejectMessageAppendageLengthDependencyWritePosition, bool cancelRejectMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long cancelRejectMessageAppendageLengthDependencyReadPosition = default;
            ushort cancelRejectMessageAppendageLengthValue = default;
            if (cancelRejectMessageAppendageLengthExists && !cursor.CancelRejectMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)cancelRejectMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var CancelRejectAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelRejectAppendageCountRule0ArithmeticLeft2 => ((index) switch { var CancelRejectAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticRight3)))) }) })) switch { var CancelRejectAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((CancelRejectAppendageCountRule0ArithmeticLeft4 - CancelRejectAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.CancelRejectMessage_AppendageLength_Used = true;
            }
            if (cancelRejectMessageAppendageLengthExists)
            {
                const int cancelRejectMessageAppendageLengthDependencyReadWidth = 2;
                cancelRejectMessageAppendageLengthDependencyReadPosition = cancelRejectMessageAppendageLengthDependencyWritePosition;
                if (cancelRejectMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelRejectMessageAppendageLengthDependencyReadPosition > bytes.Length || cancelRejectMessageAppendageLengthDependencyReadWidth > bytes.Length - cancelRejectMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                cancelRejectMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelRejectMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((cancelRejectMessageAppendageLengthValue) switch { var CancelRejectAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelRejectAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelRejectAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelRejectMessage_AppendageLength_Used = true;
        }

        internal static bool CancelRejectMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class CancelPendingAppendageWriter
    {
        internal static void CancelPendingAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelPendingAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long CancelPendingAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long cancelPendingAppendageOptionalFieldLengthDependencyReadPosition;
            byte cancelPendingAppendageOptionalFieldLengthValue;
            if (!cursor.CancelPendingAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)cancelPendingAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var CancelPendingAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelPendingAppendageSizeRule1ArithmeticLeft2 => ((CancelPendingAppendageSizeRule1Data2) switch { var CancelPendingAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var CancelPendingAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((CancelPendingAppendageSizeRule1ArithmeticLeft4 - CancelPendingAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.CancelPendingAppendage_OptionalFieldLength_Used = true;
            }
            const int cancelPendingAppendageOptionalFieldLengthDependencyReadWidth = 1;
            cancelPendingAppendageOptionalFieldLengthDependencyReadPosition = cancelPendingAppendageOptionalFieldLengthDependencyWritePosition;
            if (cancelPendingAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelPendingAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || cancelPendingAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - cancelPendingAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelPendingAppendageOptionalFieldLengthValue = bytes[(int)cancelPendingAppendageOptionalFieldLengthDependencyReadPosition];
            if (((cancelPendingAppendageOptionalFieldLengthValue) switch { var CancelPendingAppendageSizeRule1ArithmeticLeft0 => ((CancelPendingAppendageSizeRule1Data2) switch { var CancelPendingAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelPendingAppendage_OptionalFieldLength_Used = true;
        }

        internal static void CancelPendingOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelPendingOptionalFieldDependencyWritePosition, int index)
        {
            const byte CancelPendingOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Value.UserRefIdxCancelPendingOptionalField;
            long cancelPendingOptionalFieldDependencyReadPosition;
            byte cancelPendingOptionalFieldValue;
            if (!cursor.CancelPendingAppendage_CancelPendingOptionalField_Used)
            {
                bytes[(int)cancelPendingOptionalFieldDependencyWritePosition] = unchecked((byte)CancelPendingOptionalValueBranchRule0Data2);
                cursor.CancelPendingAppendage_CancelPendingOptionalField_Used = true;
            }
            const int cancelPendingOptionalFieldDependencyReadWidth = 1;
            cancelPendingOptionalFieldDependencyReadPosition = cancelPendingOptionalFieldDependencyWritePosition;
            if (cancelPendingOptionalFieldDependencyReadPosition < Position.Minimum || cancelPendingOptionalFieldDependencyReadPosition > bytes.Length || cancelPendingOptionalFieldDependencyReadWidth > bytes.Length - cancelPendingOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelPendingOptionalFieldValue = bytes[(int)cancelPendingOptionalFieldDependencyReadPosition];
            if (!(cancelPendingOptionalFieldValue == CancelPendingOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelPendingAppendage_CancelPendingOptionalField_Used = true;
        }

        internal static void CancelPendingOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelPendingOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int CancelPendingOptionalValueUserRefIdxCase = 1;
            const byte CancelPendingOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Value.UserRefIdxCancelPendingOptionalField;
            int selected = Position.Absent;
            long cancelPendingOptionalFieldDependencyReadPosition;
            byte cancelPendingOptionalFieldValue;
            const int cancelPendingOptionalFieldDependencyReadWidth = 1;
            cancelPendingOptionalFieldDependencyReadPosition = cancelPendingOptionalFieldDependencyWritePosition;
            if (cancelPendingOptionalFieldDependencyReadPosition < Position.Minimum || cancelPendingOptionalFieldDependencyReadPosition > bytes.Length || cancelPendingOptionalFieldDependencyReadWidth > bytes.Length - cancelPendingOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelPendingOptionalFieldValue = bytes[(int)cancelPendingOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((cancelPendingOptionalFieldValue == CancelPendingOptionalValueBranchRule0Data2)) selected = CancelPendingOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelPendingAppendage_CancelPendingOptionalField_Used = true;
        }
    }

    internal static class CancelPendingMessageWriter
    {
        internal static void CancelPendingMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = CancelPendingMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void CancelPendingAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelPendingMessageAppendageLengthDependencyWritePosition, bool cancelPendingMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long cancelPendingMessageAppendageLengthDependencyReadPosition = default;
            ushort cancelPendingMessageAppendageLengthValue = default;
            if (cancelPendingMessageAppendageLengthExists && !cursor.CancelPendingMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)cancelPendingMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var CancelPendingAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelPendingAppendageCountRule0ArithmeticLeft2 => ((index) switch { var CancelPendingAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticRight3)))) }) })) switch { var CancelPendingAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((CancelPendingAppendageCountRule0ArithmeticLeft4 - CancelPendingAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.CancelPendingMessage_AppendageLength_Used = true;
            }
            if (cancelPendingMessageAppendageLengthExists)
            {
                const int cancelPendingMessageAppendageLengthDependencyReadWidth = 2;
                cancelPendingMessageAppendageLengthDependencyReadPosition = cancelPendingMessageAppendageLengthDependencyWritePosition;
                if (cancelPendingMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelPendingMessageAppendageLengthDependencyReadPosition > bytes.Length || cancelPendingMessageAppendageLengthDependencyReadWidth > bytes.Length - cancelPendingMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                cancelPendingMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelPendingMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((cancelPendingMessageAppendageLengthValue) switch { var CancelPendingAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelPendingAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelPendingAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelPendingMessage_AppendageLength_Used = true;
        }

        internal static bool CancelPendingMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class RejectedOrderAppendageWriter
    {
        internal static void RejectedOrderAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long rejectedOrderAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long RejectedOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition;
            byte rejectedOrderAppendageOptionalFieldLengthValue;
            if (!cursor.RejectedOrderAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)rejectedOrderAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var RejectedOrderAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var RejectedOrderAppendageSizeRule1ArithmeticLeft2 => ((RejectedOrderAppendageSizeRule1Data2) switch { var RejectedOrderAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var RejectedOrderAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((RejectedOrderAppendageSizeRule1ArithmeticLeft4 - RejectedOrderAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.RejectedOrderAppendage_OptionalFieldLength_Used = true;
            }
            const int rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
            rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition = rejectedOrderAppendageOptionalFieldLengthDependencyWritePosition;
            if (rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || rejectedOrderAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rejectedOrderAppendageOptionalFieldLengthValue = bytes[(int)rejectedOrderAppendageOptionalFieldLengthDependencyReadPosition];
            if (((rejectedOrderAppendageOptionalFieldLengthValue) switch { var RejectedOrderAppendageSizeRule1ArithmeticLeft0 => ((RejectedOrderAppendageSizeRule1Data2) switch { var RejectedOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.RejectedOrderAppendage_OptionalFieldLength_Used = true;
        }

        internal static void RejectedOrderOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long rejectedOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte RejectedOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Value.UserRefIdxRejectedOrderOptionalField;
            long rejectedOrderOptionalFieldDependencyReadPosition;
            byte rejectedOrderOptionalFieldValue;
            if (!cursor.RejectedOrderAppendage_RejectedOrderOptionalField_Used)
            {
                bytes[(int)rejectedOrderOptionalFieldDependencyWritePosition] = unchecked((byte)RejectedOrderOptionalValueBranchRule0Data2);
                cursor.RejectedOrderAppendage_RejectedOrderOptionalField_Used = true;
            }
            const int rejectedOrderOptionalFieldDependencyReadWidth = 1;
            rejectedOrderOptionalFieldDependencyReadPosition = rejectedOrderOptionalFieldDependencyWritePosition;
            if (rejectedOrderOptionalFieldDependencyReadPosition < Position.Minimum || rejectedOrderOptionalFieldDependencyReadPosition > bytes.Length || rejectedOrderOptionalFieldDependencyReadWidth > bytes.Length - rejectedOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rejectedOrderOptionalFieldValue = bytes[(int)rejectedOrderOptionalFieldDependencyReadPosition];
            if (!(rejectedOrderOptionalFieldValue == RejectedOrderOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.RejectedOrderAppendage_RejectedOrderOptionalField_Used = true;
        }

        internal static void RejectedOrderOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long rejectedOrderOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int RejectedOrderOptionalValueUserRefIdxCase = 1;
            const byte RejectedOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Value.UserRefIdxRejectedOrderOptionalField;
            int selected = Position.Absent;
            long rejectedOrderOptionalFieldDependencyReadPosition;
            byte rejectedOrderOptionalFieldValue;
            const int rejectedOrderOptionalFieldDependencyReadWidth = 1;
            rejectedOrderOptionalFieldDependencyReadPosition = rejectedOrderOptionalFieldDependencyWritePosition;
            if (rejectedOrderOptionalFieldDependencyReadPosition < Position.Minimum || rejectedOrderOptionalFieldDependencyReadPosition > bytes.Length || rejectedOrderOptionalFieldDependencyReadWidth > bytes.Length - rejectedOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rejectedOrderOptionalFieldValue = bytes[(int)rejectedOrderOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((rejectedOrderOptionalFieldValue == RejectedOrderOptionalValueBranchRule0Data2)) selected = RejectedOrderOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.RejectedOrderAppendage_RejectedOrderOptionalField_Used = true;
        }
    }

    internal static class RejectedOrderMessageWriter
    {
        internal static void RejectedOrderMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = RejectedOrderMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void RejectedOrderAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long rejectedOrderMessageAppendageLengthDependencyWritePosition, bool rejectedOrderMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long rejectedOrderMessageAppendageLengthDependencyReadPosition = default;
            ushort rejectedOrderMessageAppendageLengthValue = default;
            if (rejectedOrderMessageAppendageLengthExists && !cursor.RejectedOrderMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)rejectedOrderMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var RejectedOrderAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var RejectedOrderAppendageCountRule0ArithmeticLeft2 => ((index) switch { var RejectedOrderAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticRight3)))) }) })) switch { var RejectedOrderAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((RejectedOrderAppendageCountRule0ArithmeticLeft4 - RejectedOrderAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.RejectedOrderMessage_AppendageLength_Used = true;
            }
            if (rejectedOrderMessageAppendageLengthExists)
            {
                const int rejectedOrderMessageAppendageLengthDependencyReadWidth = 2;
                rejectedOrderMessageAppendageLengthDependencyReadPosition = rejectedOrderMessageAppendageLengthDependencyWritePosition;
                if (rejectedOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || rejectedOrderMessageAppendageLengthDependencyReadPosition > bytes.Length || rejectedOrderMessageAppendageLengthDependencyReadWidth > bytes.Length - rejectedOrderMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                rejectedOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)rejectedOrderMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((rejectedOrderMessageAppendageLengthValue) switch { var RejectedOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var RejectedOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(RejectedOrderAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.RejectedOrderMessage_AppendageLength_Used = true;
        }

        internal static bool RejectedOrderMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class BrokenTradeAppendageWriter
    {
        internal static void BrokenTradeAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long brokenTradeAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long BrokenTradeAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long brokenTradeAppendageOptionalFieldLengthDependencyReadPosition;
            byte brokenTradeAppendageOptionalFieldLengthValue;
            if (!cursor.BrokenTradeAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)brokenTradeAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var BrokenTradeAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var BrokenTradeAppendageSizeRule1ArithmeticLeft2 => ((BrokenTradeAppendageSizeRule1Data2) switch { var BrokenTradeAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var BrokenTradeAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((BrokenTradeAppendageSizeRule1ArithmeticLeft4 - BrokenTradeAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.BrokenTradeAppendage_OptionalFieldLength_Used = true;
            }
            const int brokenTradeAppendageOptionalFieldLengthDependencyReadWidth = 1;
            brokenTradeAppendageOptionalFieldLengthDependencyReadPosition = brokenTradeAppendageOptionalFieldLengthDependencyWritePosition;
            if (brokenTradeAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || brokenTradeAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || brokenTradeAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - brokenTradeAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            brokenTradeAppendageOptionalFieldLengthValue = bytes[(int)brokenTradeAppendageOptionalFieldLengthDependencyReadPosition];
            if (((brokenTradeAppendageOptionalFieldLengthValue) switch { var BrokenTradeAppendageSizeRule1ArithmeticLeft0 => ((BrokenTradeAppendageSizeRule1Data2) switch { var BrokenTradeAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.BrokenTradeAppendage_OptionalFieldLength_Used = true;
        }

        internal static void BrokenTradeOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long brokenTradeOptionalFieldDependencyWritePosition, int index)
        {
            const byte BrokenTradeOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Value.UserRefIdxBrokenTradeOptionalField;
            long brokenTradeOptionalFieldDependencyReadPosition;
            byte brokenTradeOptionalFieldValue;
            if (!cursor.BrokenTradeAppendage_BrokenTradeOptionalField_Used)
            {
                bytes[(int)brokenTradeOptionalFieldDependencyWritePosition] = unchecked((byte)BrokenTradeOptionalValueBranchRule0Data2);
                cursor.BrokenTradeAppendage_BrokenTradeOptionalField_Used = true;
            }
            const int brokenTradeOptionalFieldDependencyReadWidth = 1;
            brokenTradeOptionalFieldDependencyReadPosition = brokenTradeOptionalFieldDependencyWritePosition;
            if (brokenTradeOptionalFieldDependencyReadPosition < Position.Minimum || brokenTradeOptionalFieldDependencyReadPosition > bytes.Length || brokenTradeOptionalFieldDependencyReadWidth > bytes.Length - brokenTradeOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            brokenTradeOptionalFieldValue = bytes[(int)brokenTradeOptionalFieldDependencyReadPosition];
            if (!(brokenTradeOptionalFieldValue == BrokenTradeOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.BrokenTradeAppendage_BrokenTradeOptionalField_Used = true;
        }

        internal static void BrokenTradeOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long brokenTradeOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int BrokenTradeOptionalValueUserRefIdxCase = 1;
            const byte BrokenTradeOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Value.UserRefIdxBrokenTradeOptionalField;
            int selected = Position.Absent;
            long brokenTradeOptionalFieldDependencyReadPosition;
            byte brokenTradeOptionalFieldValue;
            const int brokenTradeOptionalFieldDependencyReadWidth = 1;
            brokenTradeOptionalFieldDependencyReadPosition = brokenTradeOptionalFieldDependencyWritePosition;
            if (brokenTradeOptionalFieldDependencyReadPosition < Position.Minimum || brokenTradeOptionalFieldDependencyReadPosition > bytes.Length || brokenTradeOptionalFieldDependencyReadWidth > bytes.Length - brokenTradeOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            brokenTradeOptionalFieldValue = bytes[(int)brokenTradeOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((brokenTradeOptionalFieldValue == BrokenTradeOptionalValueBranchRule0Data2)) selected = BrokenTradeOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.BrokenTradeAppendage_BrokenTradeOptionalField_Used = true;
        }
    }

    internal static class BrokenTradeMessageWriter
    {
        internal static void BrokenTradeMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = BrokenTradeMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void BrokenTradeAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long brokenTradeMessageAppendageLengthDependencyWritePosition, bool brokenTradeMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long brokenTradeMessageAppendageLengthDependencyReadPosition = default;
            ushort brokenTradeMessageAppendageLengthValue = default;
            if (brokenTradeMessageAppendageLengthExists && !cursor.BrokenTradeMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)brokenTradeMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var BrokenTradeAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var BrokenTradeAppendageCountRule0ArithmeticLeft2 => ((index) switch { var BrokenTradeAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticRight3)))) }) })) switch { var BrokenTradeAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((BrokenTradeAppendageCountRule0ArithmeticLeft4 - BrokenTradeAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.BrokenTradeMessage_AppendageLength_Used = true;
            }
            if (brokenTradeMessageAppendageLengthExists)
            {
                const int brokenTradeMessageAppendageLengthDependencyReadWidth = 2;
                brokenTradeMessageAppendageLengthDependencyReadPosition = brokenTradeMessageAppendageLengthDependencyWritePosition;
                if (brokenTradeMessageAppendageLengthDependencyReadPosition < Position.Minimum || brokenTradeMessageAppendageLengthDependencyReadPosition > bytes.Length || brokenTradeMessageAppendageLengthDependencyReadWidth > bytes.Length - brokenTradeMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                brokenTradeMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)brokenTradeMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((brokenTradeMessageAppendageLengthValue) switch { var BrokenTradeAppendageCountRule0ArithmeticLeft0 => ((index) switch { var BrokenTradeAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(BrokenTradeAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.BrokenTradeMessage_AppendageLength_Used = true;
        }

        internal static bool BrokenTradeMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class OrderExecutedAppendageWriter
    {
        internal static void OrderExecutedAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderExecutedAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long OrderExecutedAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long orderExecutedAppendageOptionalFieldLengthDependencyReadPosition;
            byte orderExecutedAppendageOptionalFieldLengthValue;
            if (!cursor.OrderExecutedAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)orderExecutedAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var OrderExecutedAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderExecutedAppendageSizeRule1ArithmeticLeft2 => ((OrderExecutedAppendageSizeRule1Data2) switch { var OrderExecutedAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var OrderExecutedAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((OrderExecutedAppendageSizeRule1ArithmeticLeft4 - OrderExecutedAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.OrderExecutedAppendage_OptionalFieldLength_Used = true;
            }
            const int orderExecutedAppendageOptionalFieldLengthDependencyReadWidth = 1;
            orderExecutedAppendageOptionalFieldLengthDependencyReadPosition = orderExecutedAppendageOptionalFieldLengthDependencyWritePosition;
            if (orderExecutedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderExecutedAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || orderExecutedAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - orderExecutedAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderExecutedAppendageOptionalFieldLengthValue = bytes[(int)orderExecutedAppendageOptionalFieldLengthDependencyReadPosition];
            if (((orderExecutedAppendageOptionalFieldLengthValue) switch { var OrderExecutedAppendageSizeRule1ArithmeticLeft0 => ((OrderExecutedAppendageSizeRule1Data2) switch { var OrderExecutedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderExecutedAppendage_OptionalFieldLength_Used = true;
        }

        internal static void OrderExecutedOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderExecutedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderExecutedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Value.UserRefIdxOrderExecutedOptionalField;
            long orderExecutedOptionalFieldDependencyReadPosition;
            byte orderExecutedOptionalFieldValue;
            if (!cursor.OrderExecutedAppendage_OrderExecutedOptionalField_Used)
            {
                bytes[(int)orderExecutedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderExecutedOptionalValueBranchRule0Data2);
                cursor.OrderExecutedAppendage_OrderExecutedOptionalField_Used = true;
            }
            const int orderExecutedOptionalFieldDependencyReadWidth = 1;
            orderExecutedOptionalFieldDependencyReadPosition = orderExecutedOptionalFieldDependencyWritePosition;
            if (orderExecutedOptionalFieldDependencyReadPosition < Position.Minimum || orderExecutedOptionalFieldDependencyReadPosition > bytes.Length || orderExecutedOptionalFieldDependencyReadWidth > bytes.Length - orderExecutedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderExecutedOptionalFieldValue = bytes[(int)orderExecutedOptionalFieldDependencyReadPosition];
            if (!(orderExecutedOptionalFieldValue == OrderExecutedOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderExecutedAppendage_OrderExecutedOptionalField_Used = true;
        }

        internal static void OrderExecutedOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderExecutedOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int OrderExecutedOptionalValueUserRefIdxCase = 1;
            const byte OrderExecutedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Value.UserRefIdxOrderExecutedOptionalField;
            int selected = Position.Absent;
            long orderExecutedOptionalFieldDependencyReadPosition;
            byte orderExecutedOptionalFieldValue;
            const int orderExecutedOptionalFieldDependencyReadWidth = 1;
            orderExecutedOptionalFieldDependencyReadPosition = orderExecutedOptionalFieldDependencyWritePosition;
            if (orderExecutedOptionalFieldDependencyReadPosition < Position.Minimum || orderExecutedOptionalFieldDependencyReadPosition > bytes.Length || orderExecutedOptionalFieldDependencyReadWidth > bytes.Length - orderExecutedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderExecutedOptionalFieldValue = bytes[(int)orderExecutedOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((orderExecutedOptionalFieldValue == OrderExecutedOptionalValueBranchRule0Data2)) selected = OrderExecutedOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderExecutedAppendage_OrderExecutedOptionalField_Used = true;
        }
    }

    internal static class OrderExecutedMessageWriter
    {
        internal static void OrderExecutedAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderExecutedMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long orderExecutedMessageAppendageLengthDependencyReadPosition;
            ushort orderExecutedMessageAppendageLengthValue;
            if (!cursor.OrderExecutedMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)orderExecutedMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var OrderExecutedAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderExecutedAppendageCountRule0ArithmeticLeft2 => ((index) switch { var OrderExecutedAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticRight3)))) }) })) switch { var OrderExecutedAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((OrderExecutedAppendageCountRule0ArithmeticLeft4 - OrderExecutedAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.OrderExecutedMessage_AppendageLength_Used = true;
            }
            const int orderExecutedMessageAppendageLengthDependencyReadWidth = 2;
            orderExecutedMessageAppendageLengthDependencyReadPosition = orderExecutedMessageAppendageLengthDependencyWritePosition;
            if (orderExecutedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderExecutedMessageAppendageLengthDependencyReadPosition > bytes.Length || orderExecutedMessageAppendageLengthDependencyReadWidth > bytes.Length - orderExecutedMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderExecutedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderExecutedMessageAppendageLengthDependencyReadPosition)..]);
            if (((orderExecutedMessageAppendageLengthValue) switch { var OrderExecutedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderExecutedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderExecutedAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderExecutedMessage_AppendageLength_Used = true;
        }
    }

    internal static class AiqCanceledAppendageWriter
    {
        internal static void AiqCanceledAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long aiqCanceledAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long AiqCanceledAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition;
            byte aiqCanceledAppendageOptionalFieldLengthValue;
            if (!cursor.AiqCanceledAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)aiqCanceledAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var AiqCanceledAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var AiqCanceledAppendageSizeRule1ArithmeticLeft2 => ((AiqCanceledAppendageSizeRule1Data2) switch { var AiqCanceledAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var AiqCanceledAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((AiqCanceledAppendageSizeRule1ArithmeticLeft4 - AiqCanceledAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.AiqCanceledAppendage_OptionalFieldLength_Used = true;
            }
            const int aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth = 1;
            aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition = aiqCanceledAppendageOptionalFieldLengthDependencyWritePosition;
            if (aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || aiqCanceledAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            aiqCanceledAppendageOptionalFieldLengthValue = bytes[(int)aiqCanceledAppendageOptionalFieldLengthDependencyReadPosition];
            if (((aiqCanceledAppendageOptionalFieldLengthValue) switch { var AiqCanceledAppendageSizeRule1ArithmeticLeft0 => ((AiqCanceledAppendageSizeRule1Data2) switch { var AiqCanceledAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AiqCanceledAppendage_OptionalFieldLength_Used = true;
        }

        internal static void AiqCanceledOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long aiqCanceledOptionalFieldDependencyWritePosition, int index)
        {
            const byte AiqCanceledOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Value.UserRefIdxAiqCanceledOptionalField;
            long aiqCanceledOptionalFieldDependencyReadPosition;
            byte aiqCanceledOptionalFieldValue;
            if (!cursor.AiqCanceledAppendage_AiqCanceledOptionalField_Used)
            {
                bytes[(int)aiqCanceledOptionalFieldDependencyWritePosition] = unchecked((byte)AiqCanceledOptionalValueBranchRule0Data2);
                cursor.AiqCanceledAppendage_AiqCanceledOptionalField_Used = true;
            }
            const int aiqCanceledOptionalFieldDependencyReadWidth = 1;
            aiqCanceledOptionalFieldDependencyReadPosition = aiqCanceledOptionalFieldDependencyWritePosition;
            if (aiqCanceledOptionalFieldDependencyReadPosition < Position.Minimum || aiqCanceledOptionalFieldDependencyReadPosition > bytes.Length || aiqCanceledOptionalFieldDependencyReadWidth > bytes.Length - aiqCanceledOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            aiqCanceledOptionalFieldValue = bytes[(int)aiqCanceledOptionalFieldDependencyReadPosition];
            if (!(aiqCanceledOptionalFieldValue == AiqCanceledOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AiqCanceledAppendage_AiqCanceledOptionalField_Used = true;
        }

        internal static void AiqCanceledOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long aiqCanceledOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int AiqCanceledOptionalValueUserRefIdxCase = 1;
            const byte AiqCanceledOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Value.UserRefIdxAiqCanceledOptionalField;
            int selected = Position.Absent;
            long aiqCanceledOptionalFieldDependencyReadPosition;
            byte aiqCanceledOptionalFieldValue;
            const int aiqCanceledOptionalFieldDependencyReadWidth = 1;
            aiqCanceledOptionalFieldDependencyReadPosition = aiqCanceledOptionalFieldDependencyWritePosition;
            if (aiqCanceledOptionalFieldDependencyReadPosition < Position.Minimum || aiqCanceledOptionalFieldDependencyReadPosition > bytes.Length || aiqCanceledOptionalFieldDependencyReadWidth > bytes.Length - aiqCanceledOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            aiqCanceledOptionalFieldValue = bytes[(int)aiqCanceledOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((aiqCanceledOptionalFieldValue == AiqCanceledOptionalValueBranchRule0Data2)) selected = AiqCanceledOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AiqCanceledAppendage_AiqCanceledOptionalField_Used = true;
        }
    }

    internal static class AiqCanceledMessageWriter
    {
        internal static void AiqCanceledMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = AiqCanceledMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void AiqCanceledAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long aiqCanceledMessageAppendageLengthDependencyWritePosition, bool aiqCanceledMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long aiqCanceledMessageAppendageLengthDependencyReadPosition = default;
            ushort aiqCanceledMessageAppendageLengthValue = default;
            if (aiqCanceledMessageAppendageLengthExists && !cursor.AiqCanceledMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)aiqCanceledMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var AiqCanceledAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var AiqCanceledAppendageCountRule0ArithmeticLeft2 => ((index) switch { var AiqCanceledAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticRight3)))) }) })) switch { var AiqCanceledAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((AiqCanceledAppendageCountRule0ArithmeticLeft4 - AiqCanceledAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.AiqCanceledMessage_AppendageLength_Used = true;
            }
            if (aiqCanceledMessageAppendageLengthExists)
            {
                const int aiqCanceledMessageAppendageLengthDependencyReadWidth = 2;
                aiqCanceledMessageAppendageLengthDependencyReadPosition = aiqCanceledMessageAppendageLengthDependencyWritePosition;
                if (aiqCanceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || aiqCanceledMessageAppendageLengthDependencyReadPosition > bytes.Length || aiqCanceledMessageAppendageLengthDependencyReadWidth > bytes.Length - aiqCanceledMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                aiqCanceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)aiqCanceledMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((aiqCanceledMessageAppendageLengthValue) switch { var AiqCanceledAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AiqCanceledAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AiqCanceledAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AiqCanceledMessage_AppendageLength_Used = true;
        }

        internal static bool AiqCanceledMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class CanceledMessageAppendageWriter
    {
        internal static void CanceledMessageAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long canceledMessageAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long CanceledMessageAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long canceledMessageAppendageOptionalFieldLengthDependencyReadPosition;
            byte canceledMessageAppendageOptionalFieldLengthValue;
            if (!cursor.CanceledMessageAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)canceledMessageAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var CanceledMessageAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var CanceledMessageAppendageSizeRule1ArithmeticLeft2 => ((CanceledMessageAppendageSizeRule1Data2) switch { var CanceledMessageAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var CanceledMessageAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((CanceledMessageAppendageSizeRule1ArithmeticLeft4 - CanceledMessageAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.CanceledMessageAppendage_OptionalFieldLength_Used = true;
            }
            const int canceledMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
            canceledMessageAppendageOptionalFieldLengthDependencyReadPosition = canceledMessageAppendageOptionalFieldLengthDependencyWritePosition;
            if (canceledMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || canceledMessageAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - canceledMessageAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            canceledMessageAppendageOptionalFieldLengthValue = bytes[(int)canceledMessageAppendageOptionalFieldLengthDependencyReadPosition];
            if (((canceledMessageAppendageOptionalFieldLengthValue) switch { var CanceledMessageAppendageSizeRule1ArithmeticLeft0 => ((CanceledMessageAppendageSizeRule1Data2) switch { var CanceledMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CanceledMessageAppendage_OptionalFieldLength_Used = true;
        }

        internal static void CanceledMessageOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long canceledMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte CanceledMessageOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Value.UserRefIdxCanceledMessageOptionalField;
            long canceledMessageOptionalFieldDependencyReadPosition;
            byte canceledMessageOptionalFieldValue;
            if (!cursor.CanceledMessageAppendage_CanceledMessageOptionalField_Used)
            {
                bytes[(int)canceledMessageOptionalFieldDependencyWritePosition] = unchecked((byte)CanceledMessageOptionalValueBranchRule0Data2);
                cursor.CanceledMessageAppendage_CanceledMessageOptionalField_Used = true;
            }
            const int canceledMessageOptionalFieldDependencyReadWidth = 1;
            canceledMessageOptionalFieldDependencyReadPosition = canceledMessageOptionalFieldDependencyWritePosition;
            if (canceledMessageOptionalFieldDependencyReadPosition < Position.Minimum || canceledMessageOptionalFieldDependencyReadPosition > bytes.Length || canceledMessageOptionalFieldDependencyReadWidth > bytes.Length - canceledMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            canceledMessageOptionalFieldValue = bytes[(int)canceledMessageOptionalFieldDependencyReadPosition];
            if (!(canceledMessageOptionalFieldValue == CanceledMessageOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CanceledMessageAppendage_CanceledMessageOptionalField_Used = true;
        }

        internal static void CanceledMessageOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long canceledMessageOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int CanceledMessageOptionalValueUserRefIdxCase = 1;
            const byte CanceledMessageOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Value.UserRefIdxCanceledMessageOptionalField;
            int selected = Position.Absent;
            long canceledMessageOptionalFieldDependencyReadPosition;
            byte canceledMessageOptionalFieldValue;
            const int canceledMessageOptionalFieldDependencyReadWidth = 1;
            canceledMessageOptionalFieldDependencyReadPosition = canceledMessageOptionalFieldDependencyWritePosition;
            if (canceledMessageOptionalFieldDependencyReadPosition < Position.Minimum || canceledMessageOptionalFieldDependencyReadPosition > bytes.Length || canceledMessageOptionalFieldDependencyReadWidth > bytes.Length - canceledMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            canceledMessageOptionalFieldValue = bytes[(int)canceledMessageOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((canceledMessageOptionalFieldValue == CanceledMessageOptionalValueBranchRule0Data2)) selected = CanceledMessageOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CanceledMessageAppendage_CanceledMessageOptionalField_Used = true;
        }
    }

    internal static class CanceledMessageWriter
    {
        internal static void CanceledMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = CanceledMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.SequencedDataPacket_SequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void CanceledMessageAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long canceledMessageAppendageLengthDependencyWritePosition, bool canceledMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long canceledMessageAppendageLengthDependencyReadPosition = default;
            ushort canceledMessageAppendageLengthValue = default;
            if (canceledMessageAppendageLengthExists && !cursor.CanceledMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)canceledMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var CanceledMessageAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var CanceledMessageAppendageCountRule0ArithmeticLeft2 => ((index) switch { var CanceledMessageAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticRight3)))) }) })) switch { var CanceledMessageAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((CanceledMessageAppendageCountRule0ArithmeticLeft4 - CanceledMessageAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.CanceledMessage_AppendageLength_Used = true;
            }
            if (canceledMessageAppendageLengthExists)
            {
                const int canceledMessageAppendageLengthDependencyReadWidth = 2;
                canceledMessageAppendageLengthDependencyReadPosition = canceledMessageAppendageLengthDependencyWritePosition;
                if (canceledMessageAppendageLengthDependencyReadPosition < Position.Minimum || canceledMessageAppendageLengthDependencyReadPosition > bytes.Length || canceledMessageAppendageLengthDependencyReadWidth > bytes.Length - canceledMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                canceledMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)canceledMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((canceledMessageAppendageLengthValue) switch { var CanceledMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CanceledMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CanceledMessageAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CanceledMessage_AppendageLength_Used = true;
        }

        internal static bool CanceledMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class ReplacedMessageAppendageWriter
    {
        internal static void ReplacedMessageAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long ReplacedMessageAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long replacedMessageAppendageOptionalFieldLengthDependencyReadPosition;
            byte replacedMessageAppendageOptionalFieldLengthValue;
            if (!cursor.ReplacedMessageAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)replacedMessageAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var ReplacedMessageAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var ReplacedMessageAppendageSizeRule1ArithmeticLeft2 => ((ReplacedMessageAppendageSizeRule1Data2) switch { var ReplacedMessageAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var ReplacedMessageAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((ReplacedMessageAppendageSizeRule1ArithmeticLeft4 - ReplacedMessageAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.ReplacedMessageAppendage_OptionalFieldLength_Used = true;
            }
            const int replacedMessageAppendageOptionalFieldLengthDependencyReadWidth = 1;
            replacedMessageAppendageOptionalFieldLengthDependencyReadPosition = replacedMessageAppendageOptionalFieldLengthDependencyWritePosition;
            if (replacedMessageAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || replacedMessageAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - replacedMessageAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageAppendageOptionalFieldLengthValue = bytes[(int)replacedMessageAppendageOptionalFieldLengthDependencyReadPosition];
            if (((replacedMessageAppendageOptionalFieldLengthValue) switch { var ReplacedMessageAppendageSizeRule1ArithmeticLeft0 => ((ReplacedMessageAppendageSizeRule1Data2) switch { var ReplacedMessageAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_OptionalFieldLength_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.FirmReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule0Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.MinQtyReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule1Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.MaxFloorReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule2Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.PriceTypeReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule3Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.PostOnlyReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule4Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.ExpireTimeReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule5Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.TradeNowReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule6Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.HandleInstReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule7Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.BboWeightIndicatorReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule8Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.UserRefIdxReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule9Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.AiqStrategyReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule10Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplacedMessageOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.AiqGroupIdReplacedMessageOptionalField;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            if (!cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used)
            {
                bytes[(int)replacedMessageOptionalFieldDependencyWritePosition] = unchecked((byte)ReplacedMessageOptionalValueBranchRule11Data2);
                cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
            }
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (!(replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }

        internal static void ReplacedMessageOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int ReplacedMessageOptionalValueFirmCase = 1;
            const int ReplacedMessageOptionalValueMinQtyCase = 2;
            const int ReplacedMessageOptionalValueMaxFloorCase = 3;
            const int ReplacedMessageOptionalValuePriceTypeCase = 4;
            const int ReplacedMessageOptionalValuePostOnlyCase = 5;
            const int ReplacedMessageOptionalValueExpireTimeCase = 6;
            const int ReplacedMessageOptionalValueTradeNowCase = 7;
            const int ReplacedMessageOptionalValueHandleInstCase = 8;
            const int ReplacedMessageOptionalValueBboWeightIndicatorCase = 9;
            const int ReplacedMessageOptionalValueUserRefIdxCase = 10;
            const int ReplacedMessageOptionalValueAiqStrategyCase = 11;
            const int ReplacedMessageOptionalValueAiqGroupIdCase = 12;
            const byte ReplacedMessageOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.FirmReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.MinQtyReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.MaxFloorReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.PriceTypeReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.PostOnlyReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.ExpireTimeReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.TradeNowReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.HandleInstReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.BboWeightIndicatorReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.UserRefIdxReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.AiqStrategyReplacedMessageOptionalField;
            const byte ReplacedMessageOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value.AiqGroupIdReplacedMessageOptionalField;
            int selected = Position.Absent;
            long replacedMessageOptionalFieldDependencyReadPosition;
            byte replacedMessageOptionalFieldValue;
            const int replacedMessageOptionalFieldDependencyReadWidth = 1;
            replacedMessageOptionalFieldDependencyReadPosition = replacedMessageOptionalFieldDependencyWritePosition;
            if (replacedMessageOptionalFieldDependencyReadPosition < Position.Minimum || replacedMessageOptionalFieldDependencyReadPosition > bytes.Length || replacedMessageOptionalFieldDependencyReadWidth > bytes.Length - replacedMessageOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageOptionalFieldValue = bytes[(int)replacedMessageOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule0Data2)) selected = ReplacedMessageOptionalValueFirmCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule1Data2)) selected = ReplacedMessageOptionalValueMinQtyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule2Data2)) selected = ReplacedMessageOptionalValueMaxFloorCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule3Data2)) selected = ReplacedMessageOptionalValuePriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule4Data2)) selected = ReplacedMessageOptionalValuePostOnlyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule5Data2)) selected = ReplacedMessageOptionalValueExpireTimeCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule6Data2)) selected = ReplacedMessageOptionalValueTradeNowCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule7Data2)) selected = ReplacedMessageOptionalValueHandleInstCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule8Data2)) selected = ReplacedMessageOptionalValueBboWeightIndicatorCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule9Data2)) selected = ReplacedMessageOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule10Data2)) selected = ReplacedMessageOptionalValueAiqStrategyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replacedMessageOptionalFieldValue == ReplacedMessageOptionalValueBranchRule11Data2)) selected = ReplacedMessageOptionalValueAiqGroupIdCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = true;
        }
    }

    internal static class ReplacedMessageWriter
    {
        internal static void ReplacedMessageAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long replacedMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long replacedMessageAppendageLengthDependencyReadPosition;
            ushort replacedMessageAppendageLengthValue;
            if (!cursor.ReplacedMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)replacedMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ReplacedMessageAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var ReplacedMessageAppendageCountRule0ArithmeticLeft2 => ((index) switch { var ReplacedMessageAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticRight3)))) }) })) switch { var ReplacedMessageAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((ReplacedMessageAppendageCountRule0ArithmeticLeft4 - ReplacedMessageAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.ReplacedMessage_AppendageLength_Used = true;
            }
            const int replacedMessageAppendageLengthDependencyReadWidth = 2;
            replacedMessageAppendageLengthDependencyReadPosition = replacedMessageAppendageLengthDependencyWritePosition;
            if (replacedMessageAppendageLengthDependencyReadPosition < Position.Minimum || replacedMessageAppendageLengthDependencyReadPosition > bytes.Length || replacedMessageAppendageLengthDependencyReadWidth > bytes.Length - replacedMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replacedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replacedMessageAppendageLengthDependencyReadPosition)..]);
            if (((replacedMessageAppendageLengthValue) switch { var ReplacedMessageAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplacedMessageAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplacedMessageAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplacedMessage_AppendageLength_Used = true;
        }
    }

    internal static class OrderAcceptedAppendageWriter
    {
        internal static void OrderAcceptedAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long OrderAcceptedAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition;
            byte orderAcceptedAppendageOptionalFieldLengthValue;
            if (!cursor.OrderAcceptedAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)orderAcceptedAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var OrderAcceptedAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderAcceptedAppendageSizeRule1ArithmeticLeft2 => ((OrderAcceptedAppendageSizeRule1Data2) switch { var OrderAcceptedAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var OrderAcceptedAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((OrderAcceptedAppendageSizeRule1ArithmeticLeft4 - OrderAcceptedAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.OrderAcceptedAppendage_OptionalFieldLength_Used = true;
            }
            const int orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth = 1;
            orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition = orderAcceptedAppendageOptionalFieldLengthDependencyWritePosition;
            if (orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || orderAcceptedAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedAppendageOptionalFieldLengthValue = bytes[(int)orderAcceptedAppendageOptionalFieldLengthDependencyReadPosition];
            if (((orderAcceptedAppendageOptionalFieldLengthValue) switch { var OrderAcceptedAppendageSizeRule1ArithmeticLeft0 => ((OrderAcceptedAppendageSizeRule1Data2) switch { var OrderAcceptedAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OptionalFieldLength_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.FirmOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule0Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.MinQtyOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule1Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.CustomerTypeOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule2Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.MaxFloorOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule3Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PriceTypeOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule4Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PegOffsetOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule5Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPriceOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule6Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPriceTypeOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule7Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPegOffsetOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule8Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PostOnlyOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule9Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.RandomReservesOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule10Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.RouteOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule11Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.ExpireTimeOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule12Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.TradeNowOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule13Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.HandleInstOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule14Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.BboWeightIndicatorOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule15Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.GroupIdOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule16Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.SharesLocatedOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule17Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule18(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule18Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.UserRefIdxOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule18Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule18Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule19(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule19Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.AiqStrategyOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule19Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule19Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueBranchRule20(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index)
        {
            const byte OrderAcceptedOptionalValueBranchRule20Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.AiqGroupIdOrderAcceptedOptionalField;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            if (!cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used)
            {
                bytes[(int)orderAcceptedOptionalFieldDependencyWritePosition] = unchecked((byte)OrderAcceptedOptionalValueBranchRule20Data2);
                cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
            }
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (!(orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule20Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }

        internal static void OrderAcceptedOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int OrderAcceptedOptionalValueFirmCase = 1;
            const int OrderAcceptedOptionalValueMinQtyCase = 2;
            const int CustomerTypeCase = 3;
            const int OrderAcceptedOptionalValueMaxFloorCase = 4;
            const int OrderAcceptedOptionalValuePriceTypeCase = 5;
            const int PegOffsetCase = 6;
            const int DiscretionPriceCase = 7;
            const int DiscretionPriceTypeCase = 8;
            const int DiscretionPegOffsetCase = 9;
            const int OrderAcceptedOptionalValuePostOnlyCase = 10;
            const int RandomReservesCase = 11;
            const int RouteCase = 12;
            const int OrderAcceptedOptionalValueExpireTimeCase = 13;
            const int OrderAcceptedOptionalValueTradeNowCase = 14;
            const int OrderAcceptedOptionalValueHandleInstCase = 15;
            const int OrderAcceptedOptionalValueBboWeightIndicatorCase = 16;
            const int OrderAcceptedOptionalValueGroupIdCase = 17;
            const int OrderAcceptedOptionalValueSharesLocatedCase = 18;
            const int OrderAcceptedOptionalValueUserRefIdxCase = 19;
            const int OrderAcceptedOptionalValueAiqStrategyCase = 20;
            const int OrderAcceptedOptionalValueAiqGroupIdCase = 21;
            const byte OrderAcceptedOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.FirmOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.MinQtyOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.CustomerTypeOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.MaxFloorOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PriceTypeOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PegOffsetOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPriceOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPriceTypeOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.DiscretionPegOffsetOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.PostOnlyOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.RandomReservesOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.RouteOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.ExpireTimeOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.TradeNowOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.HandleInstOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.BboWeightIndicatorOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.GroupIdOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.SharesLocatedOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule18Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.UserRefIdxOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule19Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.AiqStrategyOrderAcceptedOptionalField;
            const byte OrderAcceptedOptionalValueBranchRule20Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value.AiqGroupIdOrderAcceptedOptionalField;
            int selected = Position.Absent;
            long orderAcceptedOptionalFieldDependencyReadPosition;
            byte orderAcceptedOptionalFieldValue;
            const int orderAcceptedOptionalFieldDependencyReadWidth = 1;
            orderAcceptedOptionalFieldDependencyReadPosition = orderAcceptedOptionalFieldDependencyWritePosition;
            if (orderAcceptedOptionalFieldDependencyReadPosition < Position.Minimum || orderAcceptedOptionalFieldDependencyReadPosition > bytes.Length || orderAcceptedOptionalFieldDependencyReadWidth > bytes.Length - orderAcceptedOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedOptionalFieldValue = bytes[(int)orderAcceptedOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule0Data2)) selected = OrderAcceptedOptionalValueFirmCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule1Data2)) selected = OrderAcceptedOptionalValueMinQtyCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule2Data2)) selected = CustomerTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule3Data2)) selected = OrderAcceptedOptionalValueMaxFloorCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule4Data2)) selected = OrderAcceptedOptionalValuePriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule5Data2)) selected = PegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule6Data2)) selected = DiscretionPriceCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule7Data2)) selected = DiscretionPriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule8Data2)) selected = DiscretionPegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule9Data2)) selected = OrderAcceptedOptionalValuePostOnlyCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule10Data2)) selected = RandomReservesCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule11Data2)) selected = RouteCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule12Data2)) selected = OrderAcceptedOptionalValueExpireTimeCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule13Data2)) selected = OrderAcceptedOptionalValueTradeNowCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule14Data2)) selected = OrderAcceptedOptionalValueHandleInstCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule15Data2)) selected = OrderAcceptedOptionalValueBboWeightIndicatorCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule16Data2)) selected = OrderAcceptedOptionalValueGroupIdCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule17Data2)) selected = OrderAcceptedOptionalValueSharesLocatedCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule18Data2)) selected = OrderAcceptedOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule19Data2)) selected = OrderAcceptedOptionalValueAiqStrategyCase;
            }
            if (selected == Position.Absent)
            {
                if ((orderAcceptedOptionalFieldValue == OrderAcceptedOptionalValueBranchRule20Data2)) selected = OrderAcceptedOptionalValueAiqGroupIdCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = true;
        }
    }

    internal static class OrderAcceptedMessageWriter
    {
        internal static void OrderAcceptedAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long orderAcceptedMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long orderAcceptedMessageAppendageLengthDependencyReadPosition;
            ushort orderAcceptedMessageAppendageLengthValue;
            if (!cursor.OrderAcceptedMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)orderAcceptedMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var OrderAcceptedAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var OrderAcceptedAppendageCountRule0ArithmeticLeft2 => ((index) switch { var OrderAcceptedAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticRight3)))) }) })) switch { var OrderAcceptedAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((OrderAcceptedAppendageCountRule0ArithmeticLeft4 - OrderAcceptedAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.OrderAcceptedMessage_AppendageLength_Used = true;
            }
            const int orderAcceptedMessageAppendageLengthDependencyReadWidth = 2;
            orderAcceptedMessageAppendageLengthDependencyReadPosition = orderAcceptedMessageAppendageLengthDependencyWritePosition;
            if (orderAcceptedMessageAppendageLengthDependencyReadPosition < Position.Minimum || orderAcceptedMessageAppendageLengthDependencyReadPosition > bytes.Length || orderAcceptedMessageAppendageLengthDependencyReadWidth > bytes.Length - orderAcceptedMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            orderAcceptedMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)orderAcceptedMessageAppendageLengthDependencyReadPosition)..]);
            if (((orderAcceptedMessageAppendageLengthValue) switch { var OrderAcceptedAppendageCountRule0ArithmeticLeft0 => ((index) switch { var OrderAcceptedAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(OrderAcceptedAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.OrderAcceptedMessage_AppendageLength_Used = true;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.SystemEventMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderAcceptedMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.ReplacedMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CanceledMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.AiqCanceledMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderExecutedMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.BrokenTradeMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.RejectedOrderMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CancelPendingMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CancelRejectMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderPriorityUpdateMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderModifiedMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderRestatedMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.AccountQueryResponseMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.MassCancelResponseMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.DisableOrderEntryResponseMessageSequencedMessageType;
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
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.EnableOrderEntryResponseMessageSequencedMessageType;
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

        internal static void SequencedMessageCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long sequencedMessageTypeDependencyWritePosition, int index, int written)
        {
            const int SystemEventMessageCase = 1;
            const int OrderAcceptedMessageCase = 2;
            const int ReplacedMessageCase = 3;
            const int CanceledMessageCase = 4;
            const int AiqCanceledMessageCase = 5;
            const int OrderExecutedMessageCase = 6;
            const int BrokenTradeMessageCase = 7;
            const int RejectedOrderMessageCase = 8;
            const int CancelPendingMessageCase = 9;
            const int CancelRejectMessageCase = 10;
            const int OrderPriorityUpdateMessageCase = 11;
            const int OrderModifiedMessageCase = 12;
            const int OrderRestatedMessageCase = 13;
            const int AccountQueryResponseMessageCase = 14;
            const int MassCancelResponseMessageCase = 15;
            const int DisableOrderEntryResponseMessageCase = 16;
            const int EnableOrderEntryResponseMessageCase = 17;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.SystemEventMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderAcceptedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.ReplacedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CanceledMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.AiqCanceledMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderExecutedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.BrokenTradeMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.RejectedOrderMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CancelPendingMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.CancelRejectMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderPriorityUpdateMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderModifiedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.OrderRestatedMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.AccountQueryResponseMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.MassCancelResponseMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.DisableOrderEntryResponseMessageSequencedMessageType;
            global::System.ReadOnlySpan<byte> SequencedMessageBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Value.EnableOrderEntryResponseMessageSequencedMessageType;
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
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule2Data2)) selected = OrderAcceptedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule3Data2)) selected = ReplacedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule4Data2)) selected = CanceledMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule5Data2)) selected = AiqCanceledMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule6Data2)) selected = OrderExecutedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule7Data2)) selected = BrokenTradeMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule8Data2)) selected = RejectedOrderMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule9Data2)) selected = CancelPendingMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule10Data2)) selected = CancelRejectMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule11Data2)) selected = OrderPriorityUpdateMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule12Data2)) selected = OrderModifiedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule13Data2)) selected = OrderRestatedMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule14Data2)) selected = AccountQueryResponseMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule15Data2)) selected = MassCancelResponseMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule16Data2)) selected = DisableOrderEntryResponseMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (sequencedMessageTypeValue.SequenceEqual(SequencedMessageBranchRule17Data2)) selected = EnableOrderEntryResponseMessageCase;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.DebugPacketServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.LoginAcceptedPacketServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.LoginRejectedPacketServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.SequencedDataPacketServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.ServerHeartbeatServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.EndOfSessionServerPacketType;
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
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.DebugPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.LoginAcceptedPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.LoginRejectedPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.SequencedDataPacketServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.ServerHeartbeatServerPacketType;
            global::System.ReadOnlySpan<byte> ServerPayloadBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Value.EndOfSessionServerPacketType;
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

    internal static class EnableOrderEntryRequestAppendageWriter
    {
        internal static void EnableOrderEntryRequestAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long EnableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
            byte enableOrderEntryRequestAppendageOptionalFieldLengthValue;
            if (!cursor.EnableOrderEntryRequestAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)enableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft2 => ((EnableOrderEntryRequestAppendageSizeRule1Data2) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft4 - EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.EnableOrderEntryRequestAppendage_OptionalFieldLength_Used = true;
            }
            const int enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
            enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = enableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition;
            if (enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)enableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
            if (((enableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((EnableOrderEntryRequestAppendageSizeRule1Data2) switch { var EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryRequestAppendage_OptionalFieldLength_Used = true;
        }

        internal static void EnableOrderEntryRequestOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryRequestOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnableOrderEntryRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Value.UserRefIdxEnableOrderEntryRequestOptionalField;
            long enableOrderEntryRequestOptionalFieldDependencyReadPosition;
            byte enableOrderEntryRequestOptionalFieldValue;
            if (!cursor.EnableOrderEntryRequestAppendage_EnableOrderEntryRequestOptionalField_Used)
            {
                bytes[(int)enableOrderEntryRequestOptionalFieldDependencyWritePosition] = unchecked((byte)EnableOrderEntryRequestOptionalValueBranchRule0Data2);
                cursor.EnableOrderEntryRequestAppendage_EnableOrderEntryRequestOptionalField_Used = true;
            }
            const int enableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
            enableOrderEntryRequestOptionalFieldDependencyReadPosition = enableOrderEntryRequestOptionalFieldDependencyWritePosition;
            if (enableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryRequestOptionalFieldDependencyReadPosition > bytes.Length || enableOrderEntryRequestOptionalFieldDependencyReadWidth > bytes.Length - enableOrderEntryRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryRequestOptionalFieldValue = bytes[(int)enableOrderEntryRequestOptionalFieldDependencyReadPosition];
            if (!(enableOrderEntryRequestOptionalFieldValue == EnableOrderEntryRequestOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryRequestAppendage_EnableOrderEntryRequestOptionalField_Used = true;
        }

        internal static void EnableOrderEntryRequestOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryRequestOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int EnableOrderEntryRequestOptionalValueUserRefIdxCase = 1;
            const byte EnableOrderEntryRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Value.UserRefIdxEnableOrderEntryRequestOptionalField;
            int selected = Position.Absent;
            long enableOrderEntryRequestOptionalFieldDependencyReadPosition;
            byte enableOrderEntryRequestOptionalFieldValue;
            const int enableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
            enableOrderEntryRequestOptionalFieldDependencyReadPosition = enableOrderEntryRequestOptionalFieldDependencyWritePosition;
            if (enableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || enableOrderEntryRequestOptionalFieldDependencyReadPosition > bytes.Length || enableOrderEntryRequestOptionalFieldDependencyReadWidth > bytes.Length - enableOrderEntryRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryRequestOptionalFieldValue = bytes[(int)enableOrderEntryRequestOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((enableOrderEntryRequestOptionalFieldValue == EnableOrderEntryRequestOptionalValueBranchRule0Data2)) selected = EnableOrderEntryRequestOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryRequestAppendage_EnableOrderEntryRequestOptionalField_Used = true;
        }
    }

    internal static class EnableOrderEntryRequestMessageWriter
    {
        internal static void EnableOrderEntryRequestAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enableOrderEntryRequestMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
            ushort enableOrderEntryRequestMessageAppendageLengthValue;
            if (!cursor.EnableOrderEntryRequestMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)enableOrderEntryRequestMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft2 => ((index) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticRight3)))) }) })) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft4 - EnableOrderEntryRequestAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.EnableOrderEntryRequestMessage_AppendageLength_Used = true;
            }
            const int enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
            enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = enableOrderEntryRequestMessageAppendageLengthDependencyWritePosition;
            if (enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > bytes.Length || enableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > bytes.Length - enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
            if (((enableOrderEntryRequestMessageAppendageLengthValue) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnableOrderEntryRequestMessage_AppendageLength_Used = true;
        }
    }

    internal static class DisableOrderEntryRequestAppendageWriter
    {
        internal static void DisableOrderEntryRequestAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long DisableOrderEntryRequestAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition;
            byte disableOrderEntryRequestAppendageOptionalFieldLengthValue;
            if (!cursor.DisableOrderEntryRequestAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)disableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft2 => ((DisableOrderEntryRequestAppendageSizeRule1Data2) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft4 - DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.DisableOrderEntryRequestAppendage_OptionalFieldLength_Used = true;
            }
            const int disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
            disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition = disableOrderEntryRequestAppendageOptionalFieldLengthDependencyWritePosition;
            if (disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryRequestAppendageOptionalFieldLengthValue = bytes[(int)disableOrderEntryRequestAppendageOptionalFieldLengthDependencyReadPosition];
            if (((disableOrderEntryRequestAppendageOptionalFieldLengthValue) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0 => ((DisableOrderEntryRequestAppendageSizeRule1Data2) switch { var DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryRequestAppendage_OptionalFieldLength_Used = true;
        }

        internal static void DisableOrderEntryRequestOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryRequestOptionalFieldDependencyWritePosition, int index)
        {
            const byte DisableOrderEntryRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Value.UserRefIdxDisableOrderEntryRequestOptionalField;
            long disableOrderEntryRequestOptionalFieldDependencyReadPosition;
            byte disableOrderEntryRequestOptionalFieldValue;
            if (!cursor.DisableOrderEntryRequestAppendage_DisableOrderEntryRequestOptionalField_Used)
            {
                bytes[(int)disableOrderEntryRequestOptionalFieldDependencyWritePosition] = unchecked((byte)DisableOrderEntryRequestOptionalValueBranchRule0Data2);
                cursor.DisableOrderEntryRequestAppendage_DisableOrderEntryRequestOptionalField_Used = true;
            }
            const int disableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
            disableOrderEntryRequestOptionalFieldDependencyReadPosition = disableOrderEntryRequestOptionalFieldDependencyWritePosition;
            if (disableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryRequestOptionalFieldDependencyReadPosition > bytes.Length || disableOrderEntryRequestOptionalFieldDependencyReadWidth > bytes.Length - disableOrderEntryRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryRequestOptionalFieldValue = bytes[(int)disableOrderEntryRequestOptionalFieldDependencyReadPosition];
            if (!(disableOrderEntryRequestOptionalFieldValue == DisableOrderEntryRequestOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryRequestAppendage_DisableOrderEntryRequestOptionalField_Used = true;
        }

        internal static void DisableOrderEntryRequestOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryRequestOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int DisableOrderEntryRequestOptionalValueUserRefIdxCase = 1;
            const byte DisableOrderEntryRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Value.UserRefIdxDisableOrderEntryRequestOptionalField;
            int selected = Position.Absent;
            long disableOrderEntryRequestOptionalFieldDependencyReadPosition;
            byte disableOrderEntryRequestOptionalFieldValue;
            const int disableOrderEntryRequestOptionalFieldDependencyReadWidth = 1;
            disableOrderEntryRequestOptionalFieldDependencyReadPosition = disableOrderEntryRequestOptionalFieldDependencyWritePosition;
            if (disableOrderEntryRequestOptionalFieldDependencyReadPosition < Position.Minimum || disableOrderEntryRequestOptionalFieldDependencyReadPosition > bytes.Length || disableOrderEntryRequestOptionalFieldDependencyReadWidth > bytes.Length - disableOrderEntryRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryRequestOptionalFieldValue = bytes[(int)disableOrderEntryRequestOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((disableOrderEntryRequestOptionalFieldValue == DisableOrderEntryRequestOptionalValueBranchRule0Data2)) selected = DisableOrderEntryRequestOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryRequestAppendage_DisableOrderEntryRequestOptionalField_Used = true;
        }
    }

    internal static class DisableOrderEntryRequestMessageWriter
    {
        internal static void DisableOrderEntryRequestAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long disableOrderEntryRequestMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition;
            ushort disableOrderEntryRequestMessageAppendageLengthValue;
            if (!cursor.DisableOrderEntryRequestMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)disableOrderEntryRequestMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft2 => ((index) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticRight3)))) }) })) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft4 - DisableOrderEntryRequestAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.DisableOrderEntryRequestMessage_AppendageLength_Used = true;
            }
            const int disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth = 2;
            disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition = disableOrderEntryRequestMessageAppendageLengthDependencyWritePosition;
            if (disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition > bytes.Length || disableOrderEntryRequestMessageAppendageLengthDependencyReadWidth > bytes.Length - disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            disableOrderEntryRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)disableOrderEntryRequestMessageAppendageLengthDependencyReadPosition)..]);
            if (((disableOrderEntryRequestMessageAppendageLengthValue) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(DisableOrderEntryRequestAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.DisableOrderEntryRequestMessage_AppendageLength_Used = true;
        }
    }

    internal static class MassCancelRequestAppendageWriter
    {
        internal static void MassCancelRequestAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long MassCancelRequestAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition;
            byte massCancelRequestAppendageOptionalFieldLengthValue;
            if (!cursor.MassCancelRequestAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)massCancelRequestAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var MassCancelRequestAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var MassCancelRequestAppendageSizeRule1ArithmeticLeft2 => ((MassCancelRequestAppendageSizeRule1Data2) switch { var MassCancelRequestAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var MassCancelRequestAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((MassCancelRequestAppendageSizeRule1ArithmeticLeft4 - MassCancelRequestAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.MassCancelRequestAppendage_OptionalFieldLength_Used = true;
            }
            const int massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth = 1;
            massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition = massCancelRequestAppendageOptionalFieldLengthDependencyWritePosition;
            if (massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || massCancelRequestAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestAppendageOptionalFieldLengthValue = bytes[(int)massCancelRequestAppendageOptionalFieldLengthDependencyReadPosition];
            if (((massCancelRequestAppendageOptionalFieldLengthValue) switch { var MassCancelRequestAppendageSizeRule1ArithmeticLeft0 => ((MassCancelRequestAppendageSizeRule1Data2) switch { var MassCancelRequestAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestAppendage_OptionalFieldLength_Used = true;
        }

        internal static void MassCancelRequestOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.SideMassCancelRequestOptionalField;
            long massCancelRequestOptionalFieldDependencyReadPosition;
            byte massCancelRequestOptionalFieldValue;
            if (!cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used)
            {
                bytes[(int)massCancelRequestOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelRequestOptionalValueBranchRule0Data2);
                cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
            }
            const int massCancelRequestOptionalFieldDependencyReadWidth = 1;
            massCancelRequestOptionalFieldDependencyReadPosition = massCancelRequestOptionalFieldDependencyWritePosition;
            if (massCancelRequestOptionalFieldDependencyReadPosition < Position.Minimum || massCancelRequestOptionalFieldDependencyReadPosition > bytes.Length || massCancelRequestOptionalFieldDependencyReadWidth > bytes.Length - massCancelRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestOptionalFieldValue = bytes[(int)massCancelRequestOptionalFieldDependencyReadPosition];
            if (!(massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
        }

        internal static void MassCancelRequestOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelRequestOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.GroupIdMassCancelRequestOptionalField;
            long massCancelRequestOptionalFieldDependencyReadPosition;
            byte massCancelRequestOptionalFieldValue;
            if (!cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used)
            {
                bytes[(int)massCancelRequestOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelRequestOptionalValueBranchRule1Data2);
                cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
            }
            const int massCancelRequestOptionalFieldDependencyReadWidth = 1;
            massCancelRequestOptionalFieldDependencyReadPosition = massCancelRequestOptionalFieldDependencyWritePosition;
            if (massCancelRequestOptionalFieldDependencyReadPosition < Position.Minimum || massCancelRequestOptionalFieldDependencyReadPosition > bytes.Length || massCancelRequestOptionalFieldDependencyReadWidth > bytes.Length - massCancelRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestOptionalFieldValue = bytes[(int)massCancelRequestOptionalFieldDependencyReadPosition];
            if (!(massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
        }

        internal static void MassCancelRequestOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestOptionalFieldDependencyWritePosition, int index)
        {
            const byte MassCancelRequestOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.UserRefIdxMassCancelRequestOptionalField;
            long massCancelRequestOptionalFieldDependencyReadPosition;
            byte massCancelRequestOptionalFieldValue;
            if (!cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used)
            {
                bytes[(int)massCancelRequestOptionalFieldDependencyWritePosition] = unchecked((byte)MassCancelRequestOptionalValueBranchRule2Data2);
                cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
            }
            const int massCancelRequestOptionalFieldDependencyReadWidth = 1;
            massCancelRequestOptionalFieldDependencyReadPosition = massCancelRequestOptionalFieldDependencyWritePosition;
            if (massCancelRequestOptionalFieldDependencyReadPosition < Position.Minimum || massCancelRequestOptionalFieldDependencyReadPosition > bytes.Length || massCancelRequestOptionalFieldDependencyReadWidth > bytes.Length - massCancelRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestOptionalFieldValue = bytes[(int)massCancelRequestOptionalFieldDependencyReadPosition];
            if (!(massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
        }

        internal static void MassCancelRequestOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int MassCancelRequestOptionalValueSideCase = 1;
            const int MassCancelRequestOptionalValueGroupIdCase = 2;
            const int MassCancelRequestOptionalValueUserRefIdxCase = 3;
            const byte MassCancelRequestOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.SideMassCancelRequestOptionalField;
            const byte MassCancelRequestOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.GroupIdMassCancelRequestOptionalField;
            const byte MassCancelRequestOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value.UserRefIdxMassCancelRequestOptionalField;
            int selected = Position.Absent;
            long massCancelRequestOptionalFieldDependencyReadPosition;
            byte massCancelRequestOptionalFieldValue;
            const int massCancelRequestOptionalFieldDependencyReadWidth = 1;
            massCancelRequestOptionalFieldDependencyReadPosition = massCancelRequestOptionalFieldDependencyWritePosition;
            if (massCancelRequestOptionalFieldDependencyReadPosition < Position.Minimum || massCancelRequestOptionalFieldDependencyReadPosition > bytes.Length || massCancelRequestOptionalFieldDependencyReadWidth > bytes.Length - massCancelRequestOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestOptionalFieldValue = bytes[(int)massCancelRequestOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule0Data2)) selected = MassCancelRequestOptionalValueSideCase;
            }
            if (selected == Position.Absent)
            {
                if ((massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule1Data2)) selected = MassCancelRequestOptionalValueGroupIdCase;
            }
            if (selected == Position.Absent)
            {
                if ((massCancelRequestOptionalFieldValue == MassCancelRequestOptionalValueBranchRule2Data2)) selected = MassCancelRequestOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = true;
        }
    }

    internal static class MassCancelRequestMessageWriter
    {
        internal static void MassCancelRequestAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long massCancelRequestMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long massCancelRequestMessageAppendageLengthDependencyReadPosition;
            ushort massCancelRequestMessageAppendageLengthValue;
            if (!cursor.MassCancelRequestMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)massCancelRequestMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var MassCancelRequestAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var MassCancelRequestAppendageCountRule0ArithmeticLeft2 => ((index) switch { var MassCancelRequestAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticRight3)))) }) })) switch { var MassCancelRequestAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((MassCancelRequestAppendageCountRule0ArithmeticLeft4 - MassCancelRequestAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.MassCancelRequestMessage_AppendageLength_Used = true;
            }
            const int massCancelRequestMessageAppendageLengthDependencyReadWidth = 2;
            massCancelRequestMessageAppendageLengthDependencyReadPosition = massCancelRequestMessageAppendageLengthDependencyWritePosition;
            if (massCancelRequestMessageAppendageLengthDependencyReadPosition < Position.Minimum || massCancelRequestMessageAppendageLengthDependencyReadPosition > bytes.Length || massCancelRequestMessageAppendageLengthDependencyReadWidth > bytes.Length - massCancelRequestMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            massCancelRequestMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)massCancelRequestMessageAppendageLengthDependencyReadPosition)..]);
            if (((massCancelRequestMessageAppendageLengthValue) switch { var MassCancelRequestAppendageCountRule0ArithmeticLeft0 => ((index) switch { var MassCancelRequestAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(MassCancelRequestAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.MassCancelRequestMessage_AppendageLength_Used = true;
        }
    }

    internal static class AccountQueryAppendageWriter
    {
        internal static void AccountQueryAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long AccountQueryAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long accountQueryAppendageOptionalFieldLengthDependencyReadPosition;
            byte accountQueryAppendageOptionalFieldLengthValue;
            if (!cursor.AccountQueryAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)accountQueryAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var AccountQueryAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var AccountQueryAppendageSizeRule1ArithmeticLeft2 => ((AccountQueryAppendageSizeRule1Data2) switch { var AccountQueryAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var AccountQueryAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((AccountQueryAppendageSizeRule1ArithmeticLeft4 - AccountQueryAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.AccountQueryAppendage_OptionalFieldLength_Used = true;
            }
            const int accountQueryAppendageOptionalFieldLengthDependencyReadWidth = 1;
            accountQueryAppendageOptionalFieldLengthDependencyReadPosition = accountQueryAppendageOptionalFieldLengthDependencyWritePosition;
            if (accountQueryAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || accountQueryAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || accountQueryAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - accountQueryAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryAppendageOptionalFieldLengthValue = bytes[(int)accountQueryAppendageOptionalFieldLengthDependencyReadPosition];
            if (((accountQueryAppendageOptionalFieldLengthValue) switch { var AccountQueryAppendageSizeRule1ArithmeticLeft0 => ((AccountQueryAppendageSizeRule1Data2) switch { var AccountQueryAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryAppendage_OptionalFieldLength_Used = true;
        }

        internal static void AccountQueryOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryOptionalFieldDependencyWritePosition, int index)
        {
            const byte AccountQueryOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Value.UserRefIdxAccountQueryOptionalField;
            long accountQueryOptionalFieldDependencyReadPosition;
            byte accountQueryOptionalFieldValue;
            if (!cursor.AccountQueryAppendage_AccountQueryOptionalField_Used)
            {
                bytes[(int)accountQueryOptionalFieldDependencyWritePosition] = unchecked((byte)AccountQueryOptionalValueBranchRule0Data2);
                cursor.AccountQueryAppendage_AccountQueryOptionalField_Used = true;
            }
            const int accountQueryOptionalFieldDependencyReadWidth = 1;
            accountQueryOptionalFieldDependencyReadPosition = accountQueryOptionalFieldDependencyWritePosition;
            if (accountQueryOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryOptionalFieldDependencyReadPosition > bytes.Length || accountQueryOptionalFieldDependencyReadWidth > bytes.Length - accountQueryOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryOptionalFieldValue = bytes[(int)accountQueryOptionalFieldDependencyReadPosition];
            if (!(accountQueryOptionalFieldValue == AccountQueryOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryAppendage_AccountQueryOptionalField_Used = true;
        }

        internal static void AccountQueryOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int AccountQueryOptionalValueUserRefIdxCase = 1;
            const byte AccountQueryOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Value.UserRefIdxAccountQueryOptionalField;
            int selected = Position.Absent;
            long accountQueryOptionalFieldDependencyReadPosition;
            byte accountQueryOptionalFieldValue;
            const int accountQueryOptionalFieldDependencyReadWidth = 1;
            accountQueryOptionalFieldDependencyReadPosition = accountQueryOptionalFieldDependencyWritePosition;
            if (accountQueryOptionalFieldDependencyReadPosition < Position.Minimum || accountQueryOptionalFieldDependencyReadPosition > bytes.Length || accountQueryOptionalFieldDependencyReadWidth > bytes.Length - accountQueryOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            accountQueryOptionalFieldValue = bytes[(int)accountQueryOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((accountQueryOptionalFieldValue == AccountQueryOptionalValueBranchRule0Data2)) selected = AccountQueryOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryAppendage_AccountQueryOptionalField_Used = true;
        }
    }

    internal static class AccountQueryMessageWriter
    {
        internal static void AccountQueryMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = AccountQueryMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void AccountQueryAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long accountQueryMessageAppendageLengthDependencyWritePosition, bool accountQueryMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long accountQueryMessageAppendageLengthDependencyReadPosition = default;
            ushort accountQueryMessageAppendageLengthValue = default;
            if (accountQueryMessageAppendageLengthExists && !cursor.AccountQueryMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)accountQueryMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var AccountQueryAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var AccountQueryAppendageCountRule0ArithmeticLeft2 => ((index) switch { var AccountQueryAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticRight3)))) }) })) switch { var AccountQueryAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((AccountQueryAppendageCountRule0ArithmeticLeft4 - AccountQueryAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.AccountQueryMessage_AppendageLength_Used = true;
            }
            if (accountQueryMessageAppendageLengthExists)
            {
                const int accountQueryMessageAppendageLengthDependencyReadWidth = 2;
                accountQueryMessageAppendageLengthDependencyReadPosition = accountQueryMessageAppendageLengthDependencyWritePosition;
                if (accountQueryMessageAppendageLengthDependencyReadPosition < Position.Minimum || accountQueryMessageAppendageLengthDependencyReadPosition > bytes.Length || accountQueryMessageAppendageLengthDependencyReadWidth > bytes.Length - accountQueryMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                accountQueryMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)accountQueryMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((accountQueryMessageAppendageLengthValue) switch { var AccountQueryAppendageCountRule0ArithmeticLeft0 => ((index) switch { var AccountQueryAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(AccountQueryAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.AccountQueryMessage_AppendageLength_Used = true;
        }

        internal static bool AccountQueryMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class ModifyOrderAppendageWriter
    {
        internal static void ModifyOrderAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long ModifyOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long modifyOrderAppendageOptionalFieldLengthDependencyReadPosition;
            byte modifyOrderAppendageOptionalFieldLengthValue;
            if (!cursor.ModifyOrderAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)modifyOrderAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var ModifyOrderAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var ModifyOrderAppendageSizeRule1ArithmeticLeft2 => ((ModifyOrderAppendageSizeRule1Data2) switch { var ModifyOrderAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var ModifyOrderAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((ModifyOrderAppendageSizeRule1ArithmeticLeft4 - ModifyOrderAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.ModifyOrderAppendage_OptionalFieldLength_Used = true;
            }
            const int modifyOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
            modifyOrderAppendageOptionalFieldLengthDependencyReadPosition = modifyOrderAppendageOptionalFieldLengthDependencyWritePosition;
            if (modifyOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || modifyOrderAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || modifyOrderAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - modifyOrderAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            modifyOrderAppendageOptionalFieldLengthValue = bytes[(int)modifyOrderAppendageOptionalFieldLengthDependencyReadPosition];
            if (((modifyOrderAppendageOptionalFieldLengthValue) switch { var ModifyOrderAppendageSizeRule1ArithmeticLeft0 => ((ModifyOrderAppendageSizeRule1Data2) switch { var ModifyOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderAppendage_OptionalFieldLength_Used = true;
        }

        internal static void ModifyOrderOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ModifyOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.UserRefIdxModifyOrderOptionalField;
            long modifyOrderOptionalFieldDependencyReadPosition;
            byte modifyOrderOptionalFieldValue;
            if (!cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used)
            {
                bytes[(int)modifyOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ModifyOrderOptionalValueBranchRule0Data2);
                cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
            }
            const int modifyOrderOptionalFieldDependencyReadWidth = 1;
            modifyOrderOptionalFieldDependencyReadPosition = modifyOrderOptionalFieldDependencyWritePosition;
            if (modifyOrderOptionalFieldDependencyReadPosition < Position.Minimum || modifyOrderOptionalFieldDependencyReadPosition > bytes.Length || modifyOrderOptionalFieldDependencyReadWidth > bytes.Length - modifyOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            modifyOrderOptionalFieldValue = bytes[(int)modifyOrderOptionalFieldDependencyReadPosition];
            if (!(modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
        }

        internal static void ModifyOrderOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ModifyOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.SharesLocatedModifyOrderOptionalField;
            long modifyOrderOptionalFieldDependencyReadPosition;
            byte modifyOrderOptionalFieldValue;
            if (!cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used)
            {
                bytes[(int)modifyOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ModifyOrderOptionalValueBranchRule1Data2);
                cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
            }
            const int modifyOrderOptionalFieldDependencyReadWidth = 1;
            modifyOrderOptionalFieldDependencyReadPosition = modifyOrderOptionalFieldDependencyWritePosition;
            if (modifyOrderOptionalFieldDependencyReadPosition < Position.Minimum || modifyOrderOptionalFieldDependencyReadPosition > bytes.Length || modifyOrderOptionalFieldDependencyReadWidth > bytes.Length - modifyOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            modifyOrderOptionalFieldValue = bytes[(int)modifyOrderOptionalFieldDependencyReadPosition];
            if (!(modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
        }

        internal static void ModifyOrderOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ModifyOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.LocateBrokerModifyOrderOptionalField;
            long modifyOrderOptionalFieldDependencyReadPosition;
            byte modifyOrderOptionalFieldValue;
            if (!cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used)
            {
                bytes[(int)modifyOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ModifyOrderOptionalValueBranchRule2Data2);
                cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
            }
            const int modifyOrderOptionalFieldDependencyReadWidth = 1;
            modifyOrderOptionalFieldDependencyReadPosition = modifyOrderOptionalFieldDependencyWritePosition;
            if (modifyOrderOptionalFieldDependencyReadPosition < Position.Minimum || modifyOrderOptionalFieldDependencyReadPosition > bytes.Length || modifyOrderOptionalFieldDependencyReadWidth > bytes.Length - modifyOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            modifyOrderOptionalFieldValue = bytes[(int)modifyOrderOptionalFieldDependencyReadPosition];
            if (!(modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
        }

        internal static void ModifyOrderOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int ModifyOrderOptionalValueUserRefIdxCase = 1;
            const int ModifyOrderOptionalValueSharesLocatedCase = 2;
            const int ModifyOrderOptionalValueLocateBrokerCase = 3;
            const byte ModifyOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.UserRefIdxModifyOrderOptionalField;
            const byte ModifyOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.SharesLocatedModifyOrderOptionalField;
            const byte ModifyOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value.LocateBrokerModifyOrderOptionalField;
            int selected = Position.Absent;
            long modifyOrderOptionalFieldDependencyReadPosition;
            byte modifyOrderOptionalFieldValue;
            const int modifyOrderOptionalFieldDependencyReadWidth = 1;
            modifyOrderOptionalFieldDependencyReadPosition = modifyOrderOptionalFieldDependencyWritePosition;
            if (modifyOrderOptionalFieldDependencyReadPosition < Position.Minimum || modifyOrderOptionalFieldDependencyReadPosition > bytes.Length || modifyOrderOptionalFieldDependencyReadWidth > bytes.Length - modifyOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            modifyOrderOptionalFieldValue = bytes[(int)modifyOrderOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule0Data2)) selected = ModifyOrderOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule1Data2)) selected = ModifyOrderOptionalValueSharesLocatedCase;
            }
            if (selected == Position.Absent)
            {
                if ((modifyOrderOptionalFieldValue == ModifyOrderOptionalValueBranchRule2Data2)) selected = ModifyOrderOptionalValueLocateBrokerCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = true;
        }
    }

    internal static class ModifyOrderMessageWriter
    {
        internal static void ModifyOrderMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = ModifyOrderMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void ModifyOrderAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long modifyOrderMessageAppendageLengthDependencyWritePosition, bool modifyOrderMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long modifyOrderMessageAppendageLengthDependencyReadPosition = default;
            ushort modifyOrderMessageAppendageLengthValue = default;
            if (modifyOrderMessageAppendageLengthExists && !cursor.ModifyOrderMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)modifyOrderMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ModifyOrderAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var ModifyOrderAppendageCountRule0ArithmeticLeft2 => ((index) switch { var ModifyOrderAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticRight3)))) }) })) switch { var ModifyOrderAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((ModifyOrderAppendageCountRule0ArithmeticLeft4 - ModifyOrderAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.ModifyOrderMessage_AppendageLength_Used = true;
            }
            if (modifyOrderMessageAppendageLengthExists)
            {
                const int modifyOrderMessageAppendageLengthDependencyReadWidth = 2;
                modifyOrderMessageAppendageLengthDependencyReadPosition = modifyOrderMessageAppendageLengthDependencyWritePosition;
                if (modifyOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || modifyOrderMessageAppendageLengthDependencyReadPosition > bytes.Length || modifyOrderMessageAppendageLengthDependencyReadWidth > bytes.Length - modifyOrderMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                modifyOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)modifyOrderMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((modifyOrderMessageAppendageLengthValue) switch { var ModifyOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ModifyOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ModifyOrderAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ModifyOrderMessage_AppendageLength_Used = true;
        }

        internal static bool ModifyOrderMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class CancelOrderAppendageWriter
    {
        internal static void CancelOrderAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelOrderAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long CancelOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long cancelOrderAppendageOptionalFieldLengthDependencyReadPosition;
            byte cancelOrderAppendageOptionalFieldLengthValue;
            if (!cursor.CancelOrderAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)cancelOrderAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var CancelOrderAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelOrderAppendageSizeRule1ArithmeticLeft2 => ((CancelOrderAppendageSizeRule1Data2) switch { var CancelOrderAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var CancelOrderAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((CancelOrderAppendageSizeRule1ArithmeticLeft4 - CancelOrderAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.CancelOrderAppendage_OptionalFieldLength_Used = true;
            }
            const int cancelOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
            cancelOrderAppendageOptionalFieldLengthDependencyReadPosition = cancelOrderAppendageOptionalFieldLengthDependencyWritePosition;
            if (cancelOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || cancelOrderAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || cancelOrderAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - cancelOrderAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelOrderAppendageOptionalFieldLengthValue = bytes[(int)cancelOrderAppendageOptionalFieldLengthDependencyReadPosition];
            if (((cancelOrderAppendageOptionalFieldLengthValue) switch { var CancelOrderAppendageSizeRule1ArithmeticLeft0 => ((CancelOrderAppendageSizeRule1Data2) switch { var CancelOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelOrderAppendage_OptionalFieldLength_Used = true;
        }

        internal static void CancelOrderOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte CancelOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Value.UserRefIdxCancelOrderOptionalField;
            long cancelOrderOptionalFieldDependencyReadPosition;
            byte cancelOrderOptionalFieldValue;
            if (!cursor.CancelOrderAppendage_CancelOrderOptionalField_Used)
            {
                bytes[(int)cancelOrderOptionalFieldDependencyWritePosition] = unchecked((byte)CancelOrderOptionalValueBranchRule0Data2);
                cursor.CancelOrderAppendage_CancelOrderOptionalField_Used = true;
            }
            const int cancelOrderOptionalFieldDependencyReadWidth = 1;
            cancelOrderOptionalFieldDependencyReadPosition = cancelOrderOptionalFieldDependencyWritePosition;
            if (cancelOrderOptionalFieldDependencyReadPosition < Position.Minimum || cancelOrderOptionalFieldDependencyReadPosition > bytes.Length || cancelOrderOptionalFieldDependencyReadWidth > bytes.Length - cancelOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelOrderOptionalFieldValue = bytes[(int)cancelOrderOptionalFieldDependencyReadPosition];
            if (!(cancelOrderOptionalFieldValue == CancelOrderOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelOrderAppendage_CancelOrderOptionalField_Used = true;
        }

        internal static void CancelOrderOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelOrderOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int CancelOrderOptionalValueUserRefIdxCase = 1;
            const byte CancelOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Value.UserRefIdxCancelOrderOptionalField;
            int selected = Position.Absent;
            long cancelOrderOptionalFieldDependencyReadPosition;
            byte cancelOrderOptionalFieldValue;
            const int cancelOrderOptionalFieldDependencyReadWidth = 1;
            cancelOrderOptionalFieldDependencyReadPosition = cancelOrderOptionalFieldDependencyWritePosition;
            if (cancelOrderOptionalFieldDependencyReadPosition < Position.Minimum || cancelOrderOptionalFieldDependencyReadPosition > bytes.Length || cancelOrderOptionalFieldDependencyReadWidth > bytes.Length - cancelOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cancelOrderOptionalFieldValue = bytes[(int)cancelOrderOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((cancelOrderOptionalFieldValue == CancelOrderOptionalValueBranchRule0Data2)) selected = CancelOrderOptionalValueUserRefIdxCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelOrderAppendage_CancelOrderOptionalField_Used = true;
        }
    }

    internal static class CancelOrderMessageWriter
    {
        internal static void CancelOrderMessageAppendageLengthOptionalRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, bool written)
        {
            bool holds = CancelOrderMessageAppendageLengthOptionalRule0PresenceCondition(bytes, ref cursor, index, limit);
            if (!cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen) return;
            if (holds != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void CancelOrderAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long cancelOrderMessageAppendageLengthDependencyWritePosition, bool cancelOrderMessageAppendageLengthExists, int index, int end)
        {
            global::System.Int128 measure = end;
            long cancelOrderMessageAppendageLengthDependencyReadPosition = default;
            ushort cancelOrderMessageAppendageLengthValue = default;
            if (cancelOrderMessageAppendageLengthExists && !cursor.CancelOrderMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)cancelOrderMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var CancelOrderAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var CancelOrderAppendageCountRule0ArithmeticLeft2 => ((index) switch { var CancelOrderAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticRight3)))) }) })) switch { var CancelOrderAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((CancelOrderAppendageCountRule0ArithmeticLeft4 - CancelOrderAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.CancelOrderMessage_AppendageLength_Used = true;
            }
            if (cancelOrderMessageAppendageLengthExists)
            {
                const int cancelOrderMessageAppendageLengthDependencyReadWidth = 2;
                cancelOrderMessageAppendageLengthDependencyReadPosition = cancelOrderMessageAppendageLengthDependencyWritePosition;
                if (cancelOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || cancelOrderMessageAppendageLengthDependencyReadPosition > bytes.Length || cancelOrderMessageAppendageLengthDependencyReadWidth > bytes.Length - cancelOrderMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                cancelOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)cancelOrderMessageAppendageLengthDependencyReadPosition)..]);
            }
            if (((cancelOrderMessageAppendageLengthValue) switch { var CancelOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var CancelOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(CancelOrderAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.CancelOrderMessage_AppendageLength_Used = true;
        }

        internal static bool CancelOrderMessageAppendageLengthOptionalRule0PresenceCondition(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit)
        {
            return index < limit;
        }
    }

    internal static class ReplaceOrderAppendageWriter
    {
        internal static void ReplaceOrderAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long ReplaceOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long replaceOrderAppendageOptionalFieldLengthDependencyReadPosition;
            byte replaceOrderAppendageOptionalFieldLengthValue;
            if (!cursor.ReplaceOrderAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)replaceOrderAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var ReplaceOrderAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var ReplaceOrderAppendageSizeRule1ArithmeticLeft2 => ((ReplaceOrderAppendageSizeRule1Data2) switch { var ReplaceOrderAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var ReplaceOrderAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((ReplaceOrderAppendageSizeRule1ArithmeticLeft4 - ReplaceOrderAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.ReplaceOrderAppendage_OptionalFieldLength_Used = true;
            }
            const int replaceOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
            replaceOrderAppendageOptionalFieldLengthDependencyReadPosition = replaceOrderAppendageOptionalFieldLengthDependencyWritePosition;
            if (replaceOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || replaceOrderAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || replaceOrderAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - replaceOrderAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderAppendageOptionalFieldLengthValue = bytes[(int)replaceOrderAppendageOptionalFieldLengthDependencyReadPosition];
            if (((replaceOrderAppendageOptionalFieldLengthValue) switch { var ReplaceOrderAppendageSizeRule1ArithmeticLeft0 => ((ReplaceOrderAppendageSizeRule1Data2) switch { var ReplaceOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_OptionalFieldLength_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.MinQtyReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule0Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.MaxFloorReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule1Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PriceTypeReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule2Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PostOnlyReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule3Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.ExpireTimeReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule4Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.TradeNowReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule5Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.HandleInstReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule6Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.RandomReservesReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule7Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PegOffsetReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule8Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPriceReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule9Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPriceTypeReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule10Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPegOffsetReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule11Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.SharesLocatedReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule12Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.LocateBrokerReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule13Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.UserRefIdxReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule14Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.SideReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule15Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.AiqStrategyReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule16Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte ReplaceOrderOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.AiqGroupIdReplaceOrderOptionalField;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            if (!cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used)
            {
                bytes[(int)replaceOrderOptionalFieldDependencyWritePosition] = unchecked((byte)ReplaceOrderOptionalValueBranchRule17Data2);
                cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
            }
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (!(replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }

        internal static void ReplaceOrderOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int ReplaceOrderOptionalValueMinQtyCase = 1;
            const int ReplaceOrderOptionalValueMaxFloorCase = 2;
            const int ReplaceOrderOptionalValuePriceTypeCase = 3;
            const int ReplaceOrderOptionalValuePostOnlyCase = 4;
            const int ReplaceOrderOptionalValueExpireTimeCase = 5;
            const int ReplaceOrderOptionalValueTradeNowCase = 6;
            const int ReplaceOrderOptionalValueHandleInstCase = 7;
            const int ReplaceOrderOptionalValueRandomReservesCase = 8;
            const int ReplaceOrderOptionalValuePegOffsetCase = 9;
            const int ReplaceOrderOptionalValueDiscretionPriceCase = 10;
            const int ReplaceOrderOptionalValueDiscretionPriceTypeCase = 11;
            const int ReplaceOrderOptionalValueDiscretionPegOffsetCase = 12;
            const int ReplaceOrderOptionalValueSharesLocatedCase = 13;
            const int ReplaceOrderOptionalValueLocateBrokerCase = 14;
            const int ReplaceOrderOptionalValueUserRefIdxCase = 15;
            const int ReplaceOrderOptionalValueSideCase = 16;
            const int ReplaceOrderOptionalValueAiqStrategyCase = 17;
            const int ReplaceOrderOptionalValueAiqGroupIdCase = 18;
            const byte ReplaceOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.MinQtyReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.MaxFloorReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PriceTypeReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PostOnlyReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.ExpireTimeReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.TradeNowReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.HandleInstReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.RandomReservesReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.PegOffsetReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPriceReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPriceTypeReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.DiscretionPegOffsetReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.SharesLocatedReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.LocateBrokerReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.UserRefIdxReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.SideReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.AiqStrategyReplaceOrderOptionalField;
            const byte ReplaceOrderOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value.AiqGroupIdReplaceOrderOptionalField;
            int selected = Position.Absent;
            long replaceOrderOptionalFieldDependencyReadPosition;
            byte replaceOrderOptionalFieldValue;
            const int replaceOrderOptionalFieldDependencyReadWidth = 1;
            replaceOrderOptionalFieldDependencyReadPosition = replaceOrderOptionalFieldDependencyWritePosition;
            if (replaceOrderOptionalFieldDependencyReadPosition < Position.Minimum || replaceOrderOptionalFieldDependencyReadPosition > bytes.Length || replaceOrderOptionalFieldDependencyReadWidth > bytes.Length - replaceOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderOptionalFieldValue = bytes[(int)replaceOrderOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule0Data2)) selected = ReplaceOrderOptionalValueMinQtyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule1Data2)) selected = ReplaceOrderOptionalValueMaxFloorCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule2Data2)) selected = ReplaceOrderOptionalValuePriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule3Data2)) selected = ReplaceOrderOptionalValuePostOnlyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule4Data2)) selected = ReplaceOrderOptionalValueExpireTimeCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule5Data2)) selected = ReplaceOrderOptionalValueTradeNowCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule6Data2)) selected = ReplaceOrderOptionalValueHandleInstCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule7Data2)) selected = ReplaceOrderOptionalValueRandomReservesCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule8Data2)) selected = ReplaceOrderOptionalValuePegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule9Data2)) selected = ReplaceOrderOptionalValueDiscretionPriceCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule10Data2)) selected = ReplaceOrderOptionalValueDiscretionPriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule11Data2)) selected = ReplaceOrderOptionalValueDiscretionPegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule12Data2)) selected = ReplaceOrderOptionalValueSharesLocatedCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule13Data2)) selected = ReplaceOrderOptionalValueLocateBrokerCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule14Data2)) selected = ReplaceOrderOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule15Data2)) selected = ReplaceOrderOptionalValueSideCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule16Data2)) selected = ReplaceOrderOptionalValueAiqStrategyCase;
            }
            if (selected == Position.Absent)
            {
                if ((replaceOrderOptionalFieldValue == ReplaceOrderOptionalValueBranchRule17Data2)) selected = ReplaceOrderOptionalValueAiqGroupIdCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = true;
        }
    }

    internal static class ReplaceOrderMessageWriter
    {
        internal static void ReplaceOrderAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long replaceOrderMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long replaceOrderMessageAppendageLengthDependencyReadPosition;
            ushort replaceOrderMessageAppendageLengthValue;
            if (!cursor.ReplaceOrderMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)replaceOrderMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var ReplaceOrderAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var ReplaceOrderAppendageCountRule0ArithmeticLeft2 => ((index) switch { var ReplaceOrderAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticRight3)))) }) })) switch { var ReplaceOrderAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((ReplaceOrderAppendageCountRule0ArithmeticLeft4 - ReplaceOrderAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.ReplaceOrderMessage_AppendageLength_Used = true;
            }
            const int replaceOrderMessageAppendageLengthDependencyReadWidth = 2;
            replaceOrderMessageAppendageLengthDependencyReadPosition = replaceOrderMessageAppendageLengthDependencyWritePosition;
            if (replaceOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || replaceOrderMessageAppendageLengthDependencyReadPosition > bytes.Length || replaceOrderMessageAppendageLengthDependencyReadWidth > bytes.Length - replaceOrderMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            replaceOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)replaceOrderMessageAppendageLengthDependencyReadPosition)..]);
            if (((replaceOrderMessageAppendageLengthValue) switch { var ReplaceOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var ReplaceOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(ReplaceOrderAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.ReplaceOrderMessage_AppendageLength_Used = true;
        }
    }

    internal static class EnterOrderAppendageWriter
    {
        internal static void EnterOrderAppendageSizeRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderAppendageOptionalFieldLengthDependencyWritePosition, int index, int end)
        {
            const long EnterOrderAppendageSizeRule1Data2 = 1;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long enterOrderAppendageOptionalFieldLengthDependencyReadPosition;
            byte enterOrderAppendageOptionalFieldLengthValue;
            if (!cursor.EnterOrderAppendage_OptionalFieldLength_Used)
            {
                bytes[(int)enterOrderAppendageOptionalFieldLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var EnterOrderAppendageSizeRule1ArithmeticLeft4 => ((((RuleTextZero) switch { var EnterOrderAppendageSizeRule1ArithmeticLeft2 => ((EnterOrderAppendageSizeRule1Data2) switch { var EnterOrderAppendageSizeRule1ArithmeticRight3 => checked((checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticLeft2)) + checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticRight3)))) }) })) switch { var EnterOrderAppendageSizeRule1ArithmeticRight5 => checked((byte)(checked((EnterOrderAppendageSizeRule1ArithmeticLeft4 - EnterOrderAppendageSizeRule1ArithmeticRight5)))) }) }));
                cursor.EnterOrderAppendage_OptionalFieldLength_Used = true;
            }
            const int enterOrderAppendageOptionalFieldLengthDependencyReadWidth = 1;
            enterOrderAppendageOptionalFieldLengthDependencyReadPosition = enterOrderAppendageOptionalFieldLengthDependencyWritePosition;
            if (enterOrderAppendageOptionalFieldLengthDependencyReadPosition < Position.Minimum || enterOrderAppendageOptionalFieldLengthDependencyReadPosition > bytes.Length || enterOrderAppendageOptionalFieldLengthDependencyReadWidth > bytes.Length - enterOrderAppendageOptionalFieldLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderAppendageOptionalFieldLengthValue = bytes[(int)enterOrderAppendageOptionalFieldLengthDependencyReadPosition];
            if (((enterOrderAppendageOptionalFieldLengthValue) switch { var EnterOrderAppendageSizeRule1ArithmeticLeft0 => ((EnterOrderAppendageSizeRule1Data2) switch { var EnterOrderAppendageSizeRule1ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageSizeRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_OptionalFieldLength_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.FirmEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule0Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.MinQtyEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule1Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.CustomerTypeEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule2Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.MaxFloorEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule3Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PriceTypeEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule4Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PegOffsetEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule5Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPriceEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule6Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPriceTypeEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule7Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPegOffsetEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule8Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PostOnlyEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule9Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.RandomReservesEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule10Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule11(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.ExpireTimeEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule11Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule11Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule12(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.TradeNowEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule12Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule12Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule13(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.HandleInstEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule13Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule13Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule14(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.GroupIdEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule14Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule14Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule15(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.SharesLocatedEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule15Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule15Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.LocateBrokerEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule16Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule17(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.UserRefIdxEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule17Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule17Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule18(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule18Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.AiqStrategyEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule18Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule18Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueBranchRule19(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index)
        {
            const byte EnterOrderOptionalValueBranchRule19Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.AiqGroupIdEnterOrderOptionalField;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            if (!cursor.EnterOrderAppendage_EnterOrderOptionalField_Used)
            {
                bytes[(int)enterOrderOptionalFieldDependencyWritePosition] = unchecked((byte)EnterOrderOptionalValueBranchRule19Data2);
                cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
            }
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (!(enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule19Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }

        internal static void EnterOrderOptionalValueCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderOptionalFieldDependencyWritePosition, int index, int written)
        {
            const int EnterOrderOptionalValueFirmCase = 1;
            const int EnterOrderOptionalValueMinQtyCase = 2;
            const int CustomerTypeCase = 3;
            const int EnterOrderOptionalValueMaxFloorCase = 4;
            const int EnterOrderOptionalValuePriceTypeCase = 5;
            const int EnterOrderOptionalValuePegOffsetCase = 6;
            const int EnterOrderOptionalValueDiscretionPriceCase = 7;
            const int EnterOrderOptionalValueDiscretionPriceTypeCase = 8;
            const int EnterOrderOptionalValueDiscretionPegOffsetCase = 9;
            const int EnterOrderOptionalValuePostOnlyCase = 10;
            const int EnterOrderOptionalValueRandomReservesCase = 11;
            const int EnterOrderOptionalValueExpireTimeCase = 12;
            const int EnterOrderOptionalValueTradeNowCase = 13;
            const int EnterOrderOptionalValueHandleInstCase = 14;
            const int EnterOrderOptionalValueGroupIdCase = 15;
            const int EnterOrderOptionalValueSharesLocatedCase = 16;
            const int EnterOrderOptionalValueLocateBrokerCase = 17;
            const int EnterOrderOptionalValueUserRefIdxCase = 18;
            const int EnterOrderOptionalValueAiqStrategyCase = 19;
            const int EnterOrderOptionalValueAiqGroupIdCase = 20;
            const byte EnterOrderOptionalValueBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.FirmEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.MinQtyEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.CustomerTypeEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.MaxFloorEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PriceTypeEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PegOffsetEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPriceEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPriceTypeEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.DiscretionPegOffsetEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule9Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.PostOnlyEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule10Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.RandomReservesEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule11Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.ExpireTimeEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule12Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.TradeNowEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule13Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.HandleInstEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule14Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.GroupIdEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule15Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.SharesLocatedEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule16Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.LocateBrokerEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule17Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.UserRefIdxEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule18Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.AiqStrategyEnterOrderOptionalField;
            const byte EnterOrderOptionalValueBranchRule19Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value.AiqGroupIdEnterOrderOptionalField;
            int selected = Position.Absent;
            long enterOrderOptionalFieldDependencyReadPosition;
            byte enterOrderOptionalFieldValue;
            const int enterOrderOptionalFieldDependencyReadWidth = 1;
            enterOrderOptionalFieldDependencyReadPosition = enterOrderOptionalFieldDependencyWritePosition;
            if (enterOrderOptionalFieldDependencyReadPosition < Position.Minimum || enterOrderOptionalFieldDependencyReadPosition > bytes.Length || enterOrderOptionalFieldDependencyReadWidth > bytes.Length - enterOrderOptionalFieldDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderOptionalFieldValue = bytes[(int)enterOrderOptionalFieldDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule0Data2)) selected = EnterOrderOptionalValueFirmCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule1Data2)) selected = EnterOrderOptionalValueMinQtyCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule2Data2)) selected = CustomerTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule3Data2)) selected = EnterOrderOptionalValueMaxFloorCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule4Data2)) selected = EnterOrderOptionalValuePriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule5Data2)) selected = EnterOrderOptionalValuePegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule6Data2)) selected = EnterOrderOptionalValueDiscretionPriceCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule7Data2)) selected = EnterOrderOptionalValueDiscretionPriceTypeCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule8Data2)) selected = EnterOrderOptionalValueDiscretionPegOffsetCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule9Data2)) selected = EnterOrderOptionalValuePostOnlyCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule10Data2)) selected = EnterOrderOptionalValueRandomReservesCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule11Data2)) selected = EnterOrderOptionalValueExpireTimeCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule12Data2)) selected = EnterOrderOptionalValueTradeNowCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule13Data2)) selected = EnterOrderOptionalValueHandleInstCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule14Data2)) selected = EnterOrderOptionalValueGroupIdCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule15Data2)) selected = EnterOrderOptionalValueSharesLocatedCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule16Data2)) selected = EnterOrderOptionalValueLocateBrokerCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule17Data2)) selected = EnterOrderOptionalValueUserRefIdxCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule18Data2)) selected = EnterOrderOptionalValueAiqStrategyCase;
            }
            if (selected == Position.Absent)
            {
                if ((enterOrderOptionalFieldValue == EnterOrderOptionalValueBranchRule19Data2)) selected = EnterOrderOptionalValueAiqGroupIdCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = true;
        }
    }

    internal static class EnterOrderMessageWriter
    {
        internal static void EnterOrderAppendageCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long enterOrderMessageAppendageLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = end;
            long enterOrderMessageAppendageLengthDependencyReadPosition;
            ushort enterOrderMessageAppendageLengthValue;
            if (!cursor.EnterOrderMessage_AppendageLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt16BigEndian(bytes[(int)enterOrderMessageAppendageLengthDependencyWritePosition..], unchecked((ushort)((measure) switch { var EnterOrderAppendageCountRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var EnterOrderAppendageCountRule0ArithmeticLeft2 => ((index) switch { var EnterOrderAppendageCountRule0ArithmeticRight3 => checked((checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticLeft2)) + checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticRight3)))) }) })) switch { var EnterOrderAppendageCountRule0ArithmeticRight5 => checked((ushort)(checked((EnterOrderAppendageCountRule0ArithmeticLeft4 - EnterOrderAppendageCountRule0ArithmeticRight5)))) }) })));
                cursor.EnterOrderMessage_AppendageLength_Used = true;
            }
            const int enterOrderMessageAppendageLengthDependencyReadWidth = 2;
            enterOrderMessageAppendageLengthDependencyReadPosition = enterOrderMessageAppendageLengthDependencyWritePosition;
            if (enterOrderMessageAppendageLengthDependencyReadPosition < Position.Minimum || enterOrderMessageAppendageLengthDependencyReadPosition > bytes.Length || enterOrderMessageAppendageLengthDependencyReadWidth > bytes.Length - enterOrderMessageAppendageLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            enterOrderMessageAppendageLengthValue = BinaryPrimitives.ReadUInt16BigEndian(bytes[((int)enterOrderMessageAppendageLengthDependencyReadPosition)..]);
            if (((enterOrderMessageAppendageLengthValue) switch { var EnterOrderAppendageCountRule0ArithmeticLeft0 => ((index) switch { var EnterOrderAppendageCountRule0ArithmeticRight1 => checked((checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticLeft0)) + checked((global::System.Int128)(EnterOrderAppendageCountRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.EnterOrderMessage_AppendageLength_Used = true;
        }
    }

    internal static class UnsequencedMessageWriter
    {
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

        internal static void UnsequencedMessageBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.EnterOrderMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule1Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.ReplaceOrderMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule2Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule3(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.CancelOrderMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule3Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule3Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.ModifyOrderMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule4Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.AccountQueryMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule5Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.MassCancelRequestMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule6Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.DisableOrderEntryRequestMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule7Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index)
        {
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.EnableOrderEntryRequestMessageUnsequencedMessageType;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            if (!cursor.UnsequencedDataPacket_UnsequencedMessageType_Used)
            {
                global::System.ReadOnlySpan<byte> stored = UnsequencedMessageBranchRule8Data2;
                global::System.Span<byte> target = bytes.Slice((int)unsequencedMessageTypeDependencyWritePosition, SizeUnsequencedMessageType);
                if (stored.Length != target.Length) throw new global::System.ArgumentException(WriterValueDoesNotFit, WriterValue);
                stored.CopyTo(target);
                cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
            }
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (!unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
        }

        internal static void UnsequencedMessageCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long unsequencedMessageTypeDependencyWritePosition, int index, int written)
        {
            const int EnterOrderMessageCase = 1;
            const int ReplaceOrderMessageCase = 2;
            const int CancelOrderMessageCase = 3;
            const int ModifyOrderMessageCase = 4;
            const int AccountQueryMessageCase = 5;
            const int MassCancelRequestMessageCase = 6;
            const int DisableOrderEntryRequestMessageCase = 7;
            const int EnableOrderEntryRequestMessageCase = 8;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.EnterOrderMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.ReplaceOrderMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.CancelOrderMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.ModifyOrderMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule5Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.AccountQueryMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule6Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.MassCancelRequestMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule7Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.DisableOrderEntryRequestMessageUnsequencedMessageType;
            global::System.ReadOnlySpan<byte> UnsequencedMessageBranchRule8Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Value.EnableOrderEntryRequestMessageUnsequencedMessageType;
            int selected = Position.Absent;
            long unsequencedMessageTypeDependencyReadPosition;
            ReadOnlySpan<byte> unsequencedMessageTypeValue;
            const int unsequencedMessageTypeDependencyReadWidth = 1;
            unsequencedMessageTypeDependencyReadPosition = unsequencedMessageTypeDependencyWritePosition;
            if (unsequencedMessageTypeDependencyReadPosition < Position.Minimum || unsequencedMessageTypeDependencyReadPosition > bytes.Length || unsequencedMessageTypeDependencyReadWidth > bytes.Length - unsequencedMessageTypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            unsequencedMessageTypeValue = bytes.Slice((int)unsequencedMessageTypeDependencyReadPosition, unsequencedMessageTypeDependencyReadWidth);
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule1Data2)) selected = EnterOrderMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule2Data2)) selected = ReplaceOrderMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule3Data2)) selected = CancelOrderMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule4Data2)) selected = ModifyOrderMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule5Data2)) selected = AccountQueryMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule6Data2)) selected = MassCancelRequestMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule7Data2)) selected = DisableOrderEntryRequestMessageCase;
            }
            if (selected == Position.Absent)
            {
                if (unsequencedMessageTypeValue.SequenceEqual(UnsequencedMessageBranchRule8Data2)) selected = EnableOrderEntryRequestMessageCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.UnsequencedDataPacket_UnsequencedMessageType_Used = true;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.DebugPacketClientPacketType;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.LoginRequestPacketClientPacketType;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.UnsequencedDataPacketClientPacketType;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.ClientHeartbeatClientPacketType;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.LogoutRequestClientPacketType;
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
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule0Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.DebugPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule1Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.LoginRequestPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule2Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.UnsequencedDataPacketClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule3Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.ClientHeartbeatClientPacketType;
            global::System.ReadOnlySpan<byte> ClientPayloadBranchRule4Data2 = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Value.LogoutRequestClientPacketType;
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
