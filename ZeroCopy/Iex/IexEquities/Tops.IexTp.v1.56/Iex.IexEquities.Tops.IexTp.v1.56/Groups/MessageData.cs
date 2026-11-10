using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Branch
/// </summary>
public static class MessageData
{
    /// <summary>
    ///  Identifies the case of the Message Data that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Message Data.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
        /// </summary>
        QuoteUpdateMessage = 1,
        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        TradeReportMessage = 2,
        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        TradeBreakMessage = 3,
    }

    /// <summary>
    ///  Reads the Message Data in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> messageTypeValue;
        private readonly global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case)Parser.Valid(Parser.SelectMessageData(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Message Data that the bytes select.
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Message Data.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMessageData(this.bytes, this.start, this.end, this.start, this.messageTypeValue))];

        /// <summary>
        ///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly QuoteUpdateMessage.Layout QuoteUpdateMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<QuoteUpdateMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeReportMessage.Layout TradeReportMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeReportMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeBreakMessage.Layout TradeBreakMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeBreakMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Message Data fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Value quoteUpdateMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Value tradeReportMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Value tradeBreakMessage;
        }

        /// <summary>
        ///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Value QuoteUpdateMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterTwoCases, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterValue);
                this.caseStorage.quoteUpdateMessage = value;
                this.@case = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage;
            }
        }

        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Value TradeReportMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterTwoCases, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterValue);
                this.caseStorage.tradeReportMessage = value;
                this.@case = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage;
            }
        }

        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Value TradeBreakMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterTwoCases, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterValue);
                this.caseStorage.tradeBreakMessage = value;
                this.@case = global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Quote Update Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> QuoteUpdateMessageMessageType => [(byte)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageType.QuoteUpdateMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Report Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeReportMessageMessageType => [(byte)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageType.TradeReportMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Break Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeBreakMessageMessageType => [(byte)global::Iex.IexEquities.Tops.IexTp.v1_56.MessageType.TradeBreakMessage];

        /// <summary>
        ///  Creates the value of the Message Data, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int QuoteUpdateMessageCase = 1;
    private const int TradeReportMessageCase = 2;
    private const int TradeBreakMessageCase = 3;

    /// <summary>
    ///  Writes the Message Data to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Message Data.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterCursor cursor, int start, scoped ref int messageDataWriterCaseRecord, scoped in Value value)
    {
        int messageDataWriterCase = global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.Absent;
        if (value.@case is not (global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage or global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage or global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage)) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage)
        {
            messageDataWriterCase = QuoteUpdateMessageCase;
            int quoteUpdateMessageWriting = cursor.Index;
            var quoteUpdateMessageWriteValue = value.caseStorage.quoteUpdateMessage;
            global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateMessage.Write(bytes, ref cursor, quoteUpdateMessageWriting, in quoteUpdateMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage)
        {
            messageDataWriterCase = TradeReportMessageCase;
            int tradeReportMessageWriting = cursor.Index;
            var tradeReportMessageWriteValue = value.caseStorage.tradeReportMessage;
            global::Iex.IexEquities.Tops.IexTp.v1_56.TradeReportMessage.Write(bytes, ref cursor, tradeReportMessageWriting, in tradeReportMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage)
        {
            messageDataWriterCase = TradeBreakMessageCase;
            int tradeBreakMessageWriting = cursor.Index;
            var tradeBreakMessageWriteValue = value.caseStorage.tradeBreakMessage;
            global::Iex.IexEquities.Tops.IexTp.v1_56.TradeBreakMessage.Write(bytes, ref cursor, tradeBreakMessageWriting, in tradeBreakMessageWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageDataWriterCaseRecord))
        {
            int recorded = messageDataWriterCase == global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.Absent ? global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.NoCase : messageDataWriterCase;
            if (messageDataWriterCaseRecord != global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.Absent && messageDataWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterRuleDisagrees, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterValue);
            messageDataWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
