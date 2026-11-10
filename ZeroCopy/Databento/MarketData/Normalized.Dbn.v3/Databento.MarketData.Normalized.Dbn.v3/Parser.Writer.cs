using System;
using System.Buffers.Binary;
namespace Databento.MarketData.Normalized.Dbn.v3;
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
        internal bool Metadata_MetadataLength_Used;
        internal bool Metadata_SchemaDefinitionLength_Used;
        internal bool Metadata_SymbolsCount_Used;
        internal bool Metadata_PartialCount_Used;
        internal bool Metadata_NotFoundCount_Used;
        internal bool Metadata_MappingsCount_Used;
        internal bool SymbolMapping_IntervalCount_Used;
        internal bool RecordHeader_RecordLength_Used;
        internal bool RecordHeader_Rtype_Used;
    }

    internal static class ControlMessageWriter
    {
        internal static void ControlMessageReassembleRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int end)
        {
            const byte ControlMessageReassembleRule1Terminator0 = 10;
            global::System.Int128 measure = (global::System.Int128)end - index;
            int ControlMessageReassembleRule1Terminator0Bytes;
            int ControlMessageReassembleRule1Terminator0Offset;
            
            {
                ControlMessageReassembleRule1Terminator0Offset = bytes.Slice(index, end - index).IndexOf(ControlMessageReassembleRule1Terminator0);
                if (ControlMessageReassembleRule1Terminator0Offset < 0)
                {
                    throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                }
                ControlMessageReassembleRule1Terminator0Bytes = ControlMessageReassembleRule1Terminator0Offset + sizeof(byte);
            }
            if (checked((global::System.Int128)(ControlMessageReassembleRule1Terminator0Bytes)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static void ControlFieldsSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int end)
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            global::System.Int128 measure = (global::System.Int128)end - index;
            int ControlFieldsSizeRule0Terminator0Bytes;
            int ControlFieldsSizeRule0Terminator0Offset;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, end - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            if (checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class ClientStreamWriter
    {
        internal static void ControlMessagePayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = end;
            if (checked((global::System.Int128)(limit)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class Mbp10MsgWriter
    {
        internal static void Mbp10MsgBidAskPairCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int count)
        {
            const long BidAskPairCountRule0Data0 = 10;
            global::System.Int128 measure = count;
            if (checked((global::System.Int128)(BidAskPairCountRule0Data0)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }

        internal static global::System.Int128 Mbp10MsgBidAskPairCountRule0CountValue(global::System.Span<byte> bytes, ref WriterCursor cursor, int index)
        {
            const long BidAskPairCountRule0Data0 = 10;
            return checked((global::System.Int128)(BidAskPairCountRule0Data0));
        }
    }

    internal static class RecordWriter
    {
        internal static void RecordReassembleRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long recordLengthDependencyWritePosition, ref bool recordLengthDependencyWriteUsed, int index, int end)
        {
            const long RecordReassembleRule1Data2 = 4;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long recordLengthDependencyReadPosition;
            byte recordLengthValue;
            if (!recordLengthDependencyWriteUsed)
            {
                bytes[(int)recordLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var RecordReassembleRule1Inverse6 => checked((byte)(DivideRuleIntegerExact(RecordReassembleRule1Inverse6, ((RuleTextOne) switch { var RecordReassembleRule1ArithmeticLeft2 => ((RecordReassembleRule1Data2) switch { var RecordReassembleRule1ArithmeticRight3 => checked((checked((global::System.Int128)(RecordReassembleRule1ArithmeticLeft2)) * checked((global::System.Int128)(RecordReassembleRule1ArithmeticRight3)))) }) })))) }));
                recordLengthDependencyWriteUsed = true;
            }
            const int recordLengthDependencyReadWidth = 1;
            recordLengthDependencyReadPosition = recordLengthDependencyWritePosition;
            if (recordLengthDependencyReadPosition < Position.Minimum || recordLengthDependencyReadPosition > bytes.Length || recordLengthDependencyReadWidth > bytes.Length - recordLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            recordLengthValue = bytes[(int)recordLengthDependencyReadPosition];
            if (((recordLengthValue) switch { var RecordReassembleRule1ArithmeticLeft0 => ((RecordReassembleRule1Data2) switch { var RecordReassembleRule1ArithmeticRight1 => checked((checked((global::System.Int128)(RecordReassembleRule1ArithmeticLeft0)) * checked((global::System.Int128)(RecordReassembleRule1ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            recordLengthDependencyWriteUsed = true;
        }

        internal static void RecordSizeRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long recordLengthDependencyWritePosition, ref bool recordLengthDependencyWriteUsed, int index, int end)
        {
            const long RecordSizeRule2Data2 = 4;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long recordLengthDependencyReadPosition;
            byte recordLengthValue;
            if (!recordLengthDependencyWriteUsed)
            {
                bytes[(int)recordLengthDependencyWritePosition] = unchecked((byte)((measure) switch { var RecordSizeRule2Inverse6 => checked((byte)(DivideRuleIntegerExact(RecordSizeRule2Inverse6, ((RuleTextOne) switch { var RecordSizeRule2ArithmeticLeft2 => ((RecordSizeRule2Data2) switch { var RecordSizeRule2ArithmeticRight3 => checked((checked((global::System.Int128)(RecordSizeRule2ArithmeticLeft2)) * checked((global::System.Int128)(RecordSizeRule2ArithmeticRight3)))) }) })))) }));
                recordLengthDependencyWriteUsed = true;
            }
            const int recordLengthDependencyReadWidth = 1;
            recordLengthDependencyReadPosition = recordLengthDependencyWritePosition;
            if (recordLengthDependencyReadPosition < Position.Minimum || recordLengthDependencyReadPosition > bytes.Length || recordLengthDependencyReadWidth > bytes.Length - recordLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            recordLengthValue = bytes[(int)recordLengthDependencyReadPosition];
            if (((recordLengthValue) switch { var RecordSizeRule2ArithmeticLeft0 => ((RecordSizeRule2Data2) switch { var RecordSizeRule2ArithmeticRight1 => checked((checked((global::System.Int128)(RecordSizeRule2ArithmeticLeft0)) * checked((global::System.Int128)(RecordSizeRule2ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            recordLengthDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule0Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.TradeMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule0Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule0Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule1(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule1Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Mbp1MsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule1Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule1Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule2(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule2Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Mbp10MsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule2Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule2Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule4(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule4Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.StatusMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule4Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule4Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule5(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule5Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.InstrumentDefMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule5Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule5Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule6(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule6Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.ImbalanceMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule6Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule6Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule7(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule7Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.ErrorMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule7Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule7Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule8(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule8Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.SymbolMappingMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule8Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule8Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule9(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule9Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.SystemMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule9Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule9Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule10(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule10Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.StatMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule10Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule10Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyBranchRule16(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index)
        {
            const byte RecordBodyBranchRule16Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.MboMsgRtype;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            if (!rtypeDependencyWriteUsed)
            {
                bytes[(int)rtypeDependencyWritePosition] = unchecked((byte)RecordBodyBranchRule16Data2);
                rtypeDependencyWriteUsed = true;
            }
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (!(rtypeValue == RecordBodyBranchRule16Data2)) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }

        internal static void RecordBodyCases(global::System.Span<byte> bytes, ref WriterCursor cursor, long rtypeDependencyWritePosition, ref bool rtypeDependencyWriteUsed, int index, int written)
        {
            const int TradeMsgCase = 1;
            const int Mbp1MsgCase = 2;
            const int Mbp10MsgCase = 3;
            const int OhlcvMsgCase = 4;
            const int StatusMsgCase = 5;
            const int InstrumentDefMsgCase = 6;
            const int ImbalanceMsgCase = 7;
            const int ErrorMsgCase = 8;
            const int SymbolMappingMsgCase = 9;
            const int SystemMsgCase = 10;
            const int StatMsgCase = 11;
            const int MboMsgCase = 12;
            const int Cmbp1MsgCase = 13;
            const int CbboMsgCase = 14;
            const int BboMsgCase = 15;
            const byte RecordBodyBranchRule0Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.TradeMsgRtype;
            const byte RecordBodyBranchRule1Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Mbp1MsgRtype;
            const byte RecordBodyBranchRule2Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Mbp10MsgRtype;
            const byte RecordBodyBranchRule3Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype;
            const byte RecordBodyBranchRule4Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.StatusMsgRtype;
            const byte RecordBodyBranchRule5Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.InstrumentDefMsgRtype;
            const byte RecordBodyBranchRule6Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.ImbalanceMsgRtype;
            const byte RecordBodyBranchRule7Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.ErrorMsgRtype;
            const byte RecordBodyBranchRule8Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.SymbolMappingMsgRtype;
            const byte RecordBodyBranchRule9Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.SystemMsgRtype;
            const byte RecordBodyBranchRule10Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.StatMsgRtype;
            const byte RecordBodyBranchRule11Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype2;
            const byte RecordBodyBranchRule12Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype3;
            const byte RecordBodyBranchRule13Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype4;
            const byte RecordBodyBranchRule14Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype5;
            const byte RecordBodyBranchRule15Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.OhlcvMsgRtype6;
            const byte RecordBodyBranchRule16Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.MboMsgRtype;
            const byte RecordBodyBranchRule17Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Cmbp1MsgRtype;
            const byte RecordBodyBranchRule18Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.CbboMsgRtype;
            const byte RecordBodyBranchRule19Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.CbboMsgRtype2;
            const byte RecordBodyBranchRule20Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.Cmbp1MsgRtype2;
            const byte RecordBodyBranchRule21Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.BboMsgRtype;
            const byte RecordBodyBranchRule22Data2 = global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value.BboMsgRtype2;
            int selected = Position.Absent;
            long rtypeDependencyReadPosition;
            byte rtypeValue;
            const int rtypeDependencyReadWidth = 1;
            rtypeDependencyReadPosition = rtypeDependencyWritePosition;
            if (rtypeDependencyReadPosition < Position.Minimum || rtypeDependencyReadPosition > bytes.Length || rtypeDependencyReadWidth > bytes.Length - rtypeDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeValue = bytes[(int)rtypeDependencyReadPosition];
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule0Data2)) selected = TradeMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule1Data2)) selected = Mbp1MsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule2Data2)) selected = Mbp10MsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule3Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule4Data2)) selected = StatusMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule5Data2)) selected = InstrumentDefMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule6Data2)) selected = ImbalanceMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule7Data2)) selected = ErrorMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule8Data2)) selected = SymbolMappingMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule9Data2)) selected = SystemMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule10Data2)) selected = StatMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule11Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule12Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule13Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule14Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule15Data2)) selected = OhlcvMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule16Data2)) selected = MboMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule17Data2)) selected = Cmbp1MsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule18Data2)) selected = CbboMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule19Data2)) selected = CbboMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule20Data2)) selected = Cmbp1MsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule21Data2)) selected = BboMsgCase;
            }
            if (selected == Position.Absent)
            {
                if ((rtypeValue == RecordBodyBranchRule22Data2)) selected = BboMsgCase;
            }
            if (selected != written) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            rtypeDependencyWriteUsed = true;
        }
    }

    internal static class SymbolMappingWriter
    {
        internal static void MappingIntervalCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long intervalCountDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long intervalCountDependencyReadPosition;
            uint intervalCountValue;
            if (!cursor.SymbolMapping_IntervalCount_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)intervalCountDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.SymbolMapping_IntervalCount_Used = true;
            }
            const int intervalCountDependencyReadWidth = 4;
            intervalCountDependencyReadPosition = intervalCountDependencyWritePosition;
            if (intervalCountDependencyReadPosition < Position.Minimum || intervalCountDependencyReadPosition > bytes.Length || intervalCountDependencyReadWidth > bytes.Length - intervalCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            intervalCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)intervalCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(intervalCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.SymbolMapping_IntervalCount_Used = true;
        }
    }

    internal static class MetadataWriter
    {
        internal static void SymbolCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long symbolsCountDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long symbolsCountDependencyReadPosition;
            uint symbolsCountValue;
            if (!cursor.Metadata_SymbolsCount_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)symbolsCountDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.Metadata_SymbolsCount_Used = true;
            }
            const int symbolsCountDependencyReadWidth = 4;
            symbolsCountDependencyReadPosition = symbolsCountDependencyWritePosition;
            if (symbolsCountDependencyReadPosition < Position.Minimum || symbolsCountDependencyReadPosition > bytes.Length || symbolsCountDependencyReadWidth > bytes.Length - symbolsCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            symbolsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)symbolsCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(symbolsCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_SymbolsCount_Used = true;
        }

        internal static void PartialSymbolCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long partialCountDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long partialCountDependencyReadPosition;
            uint partialCountValue;
            if (!cursor.Metadata_PartialCount_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)partialCountDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.Metadata_PartialCount_Used = true;
            }
            const int partialCountDependencyReadWidth = 4;
            partialCountDependencyReadPosition = partialCountDependencyWritePosition;
            if (partialCountDependencyReadPosition < Position.Minimum || partialCountDependencyReadPosition > bytes.Length || partialCountDependencyReadWidth > bytes.Length - partialCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            partialCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)partialCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(partialCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_PartialCount_Used = true;
        }

        internal static void NotFoundSymbolCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long notFoundCountDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long notFoundCountDependencyReadPosition;
            uint notFoundCountValue;
            if (!cursor.Metadata_NotFoundCount_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)notFoundCountDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.Metadata_NotFoundCount_Used = true;
            }
            const int notFoundCountDependencyReadWidth = 4;
            notFoundCountDependencyReadPosition = notFoundCountDependencyWritePosition;
            if (notFoundCountDependencyReadPosition < Position.Minimum || notFoundCountDependencyReadPosition > bytes.Length || notFoundCountDependencyReadWidth > bytes.Length - notFoundCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            notFoundCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)notFoundCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(notFoundCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_NotFoundCount_Used = true;
        }

        internal static void SymbolMappingCountRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long mappingsCountDependencyWritePosition, int index, int count)
        {
            global::System.Int128 measure = count;
            long mappingsCountDependencyReadPosition;
            uint mappingsCountValue;
            if (!cursor.Metadata_MappingsCount_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)mappingsCountDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.Metadata_MappingsCount_Used = true;
            }
            const int mappingsCountDependencyReadWidth = 4;
            mappingsCountDependencyReadPosition = mappingsCountDependencyWritePosition;
            if (mappingsCountDependencyReadPosition < Position.Minimum || mappingsCountDependencyReadPosition > bytes.Length || mappingsCountDependencyReadWidth > bytes.Length - mappingsCountDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            mappingsCountValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)mappingsCountDependencyReadPosition)..]);
            if (checked((global::System.Int128)(mappingsCountValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_MappingsCount_Used = true;
        }

        internal static void MetadataSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long metadataLengthDependencyWritePosition, int index, int end)
        {
            const long MetadataSizeRule0Data2 = 8;
            global::System.Int128 measure = (global::System.Int128)end - index;
            long metadataLengthDependencyReadPosition;
            uint metadataLengthValue;
            if (!cursor.Metadata_MetadataLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)metadataLengthDependencyWritePosition..], unchecked((uint)((measure) switch { var MetadataSizeRule0ArithmeticLeft4 => ((((RuleTextZero) switch { var MetadataSizeRule0ArithmeticLeft2 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight3 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft2)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight3)))) }) })) switch { var MetadataSizeRule0ArithmeticRight5 => checked((uint)(checked((MetadataSizeRule0ArithmeticLeft4 - MetadataSizeRule0ArithmeticRight5)))) }) })));
                cursor.Metadata_MetadataLength_Used = true;
            }
            const int metadataLengthDependencyReadWidth = 4;
            metadataLengthDependencyReadPosition = metadataLengthDependencyWritePosition;
            if (metadataLengthDependencyReadPosition < Position.Minimum || metadataLengthDependencyReadPosition > bytes.Length || metadataLengthDependencyReadWidth > bytes.Length - metadataLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            metadataLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)metadataLengthDependencyReadPosition)..]);
            if (((metadataLengthValue) switch { var MetadataSizeRule0ArithmeticLeft0 => ((MetadataSizeRule0Data2) switch { var MetadataSizeRule0ArithmeticRight1 => checked((checked((global::System.Int128)(MetadataSizeRule0ArithmeticLeft0)) + checked((global::System.Int128)(MetadataSizeRule0ArithmeticRight1)))) }) }) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_MetadataLength_Used = true;
        }

        internal static void SchemaDefinitionSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, long schemaDefinitionLengthDependencyWritePosition, int index, int end)
        {
            global::System.Int128 measure = (global::System.Int128)end - index;
            long schemaDefinitionLengthDependencyReadPosition;
            uint schemaDefinitionLengthValue;
            if (!cursor.Metadata_SchemaDefinitionLength_Used)
            {
                global::System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes[(int)schemaDefinitionLengthDependencyWritePosition..], unchecked((uint)checked((uint)(measure))));
                cursor.Metadata_SchemaDefinitionLength_Used = true;
            }
            const int schemaDefinitionLengthDependencyReadWidth = 4;
            schemaDefinitionLengthDependencyReadPosition = schemaDefinitionLengthDependencyWritePosition;
            if (schemaDefinitionLengthDependencyReadPosition < Position.Minimum || schemaDefinitionLengthDependencyReadPosition > bytes.Length || schemaDefinitionLengthDependencyReadWidth > bytes.Length - schemaDefinitionLengthDependencyReadPosition) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            schemaDefinitionLengthValue = BinaryPrimitives.ReadUInt32LittleEndian(bytes[((int)schemaDefinitionLengthDependencyReadPosition)..]);
            if (checked((global::System.Int128)(schemaDefinitionLengthValue)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
            cursor.Metadata_SchemaDefinitionLength_Used = true;
        }

        internal static void MetadataPaddingSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = (global::System.Int128)end - index;
            if (checked((global::System.Int128)(((long)limit - index))) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class AuthenticationResponseWriter
    {
        internal static void AuthenticationResponseControlFieldsSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int end)
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            global::System.Int128 measure = (global::System.Int128)end - index;
            int ControlFieldsSizeRule0Terminator0Bytes;
            int ControlFieldsSizeRule0Terminator0Offset;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, end - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            if (checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class ChallengeRequestWriter
    {
        internal static void ChallengeRequestControlFieldsSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int end)
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            global::System.Int128 measure = (global::System.Int128)end - index;
            int ControlFieldsSizeRule0Terminator0Bytes;
            int ControlFieldsSizeRule0Terminator0Offset;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, end - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            if (checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class GreetingWriter
    {
        internal static void GreetingControlFieldsSizeRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int end)
        {
            const byte ControlFieldsSizeRule0Terminator0 = 10;
            global::System.Int128 measure = (global::System.Int128)end - index;
            int ControlFieldsSizeRule0Terminator0Bytes;
            int ControlFieldsSizeRule0Terminator0Offset;
            
            {
                ControlFieldsSizeRule0Terminator0Offset = bytes.Slice(index, end - index).IndexOf(ControlFieldsSizeRule0Terminator0);
                if (ControlFieldsSizeRule0Terminator0Offset < 0)
                {
                    throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
                }
                ControlFieldsSizeRule0Terminator0Bytes = ControlFieldsSizeRule0Terminator0Offset + sizeof(byte);
            }
            if (checked((global::System.Int128)(ControlFieldsSizeRule0Terminator0Bytes)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class GatewayStreamWriter
    {
        internal static void RecordPayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
        {
            global::System.Int128 measure = end;
            if (checked((global::System.Int128)(limit)) != measure) throw new global::System.ArgumentException(WriterRuleDisagrees, WriterValue);
        }
    }

    internal static class FileWriter
    {
        internal static void RecordPayloadRule0(global::System.Span<byte> bytes, ref WriterCursor cursor, int index, int limit, int end)
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
