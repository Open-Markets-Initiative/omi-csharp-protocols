using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  IexTp message
/// </summary>
public static class Message
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  Gets the fixed fields over the start of the bytes without clearing or writing them, for changes in place.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the fixed fields.
        /// </exception>
        public static ref Layout Over(global::System.Span<byte> bytes)
        {
            if (global::Iex.IexEquities.Tops.IexTp.v1_66.Message.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  IexTp message header
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Layout MessageHeader;
    }

    /// <summary>
    ///  Reads the Message in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ulong firstMessageSequenceNumberSequenceSeed;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ulong firstMessageSequenceNumberSequenceSeed)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.firstMessageSequenceNumberSequenceSeed = firstMessageSequenceNumberSequenceSeed;
        }

        /// <summary>
        ///  Gets the bytes of the Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  IexTp message header
        /// </summary>
        public ref readonly MessageHeader.Layout MessageHeader => ref this.layout.MessageHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public MessageData.Reader MessageData
        {
            get
            {
                int messageDataReaderPosition = Parser.Valid(Parser.MessageMessageDataStart(this.bytes, this.start, this.end, this.start, out var messageTypeValue));
                return new(this.bytes, messageDataReaderPosition, this.end, messageTypeValue);
            }
        }

        /// <summary>
        ///  Gets the sequence number of the Message.
        /// </summary>
        public ulong SequenceNumber => this.firstMessageSequenceNumberSequenceSeed;
    }

    /// <summary>
    ///  The Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  IexTp message header
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Value MessageHeader { get; init; } = new();

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Value MessageData { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeMessageHeader;
    private const int messageLengthDependencyWriteDistanceFromMessageHeader = 0;
    private const int messageTypeDependencyWriteDistanceFromMessageHeader = 2;
    private const int SystemEventMessageCase = 1;
    private const int SecurityDirectoryMessageCase = 2;
    private const int TradingStatusMessageCase = 3;
    private const int RetailLiquidityIndicatorMessageCase = 4;
    private const int OperationalHaltStatusMessageCase = 5;
    private const int ShortSalePriceTestStatusMessageCase = 6;
    private const int QuoteUpdateMessageCase = 7;
    private const int TradeReportMessageCase = 8;
    private const int OfficialPriceMessageCase = 9;
    private const int TradeBreakMessageCase = 10;
    private const int AuctionInformationMessageCase = 11;

    /// <summary>
    ///  Writes the Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int messageHeaderWriterStart = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent;
        int messageDataWriterStart = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent;
        int messageDataWriterCase = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent;
        bool messageLengthDependencyFirstUsed = false;
        bool messageTypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterNoRoom, global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterDestination);
        if (messageHeaderWriterStart == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        int messageHeaderWriting = cursor.Index;
        var messageHeaderWriteValue = value.MessageHeader;
        global::Iex.IexEquities.Tops.IexTp.v1_66.MessageHeader.Write(bytes, ref cursor, messageHeaderWriting, ref messageHeaderWriterStart == messageHeaderWriting ? ref messageLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref messageHeaderWriterStart == messageHeaderWriting ? ref messageTypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in messageHeaderWriteValue);
        if (messageHeaderWriterStart == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (messageDataWriterStart == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent) messageDataWriterStart = cursor.Index;
        int messageDataWriting = cursor.Index;
        var messageDataWriteValue = value.MessageData;
        global::Iex.IexEquities.Tops.IexTp.v1_66.MessageData.Write(bytes, ref cursor, messageDataWriting, ref messageDataWriterCase, in messageDataWriteValue);
        if (messageHeaderWriterStart == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (messageDataWriterStart == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent) messageDataWriterStart = cursor.Index;
        global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageSizeRule1(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageLengthDependencyWriteDistanceFromMessageHeader, ref messageLengthDependencyFirstUsed, start, cursor.Index);
        if (messageDataWriterCase == SystemEventMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule0(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == SecurityDirectoryMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule1(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == TradingStatusMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule2(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == RetailLiquidityIndicatorMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule3(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == OperationalHaltStatusMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule4(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == ShortSalePriceTestStatusMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule5(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == QuoteUpdateMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule6(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == TradeReportMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule7(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == OfficialPriceMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule8(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == TradeBreakMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule9(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        if (messageDataWriterCase == AuctionInformationMessageCase) global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataBranchRule10(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart);
        global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.MessageWriter.MessageDataCases(bytes, ref cursor, messageHeaderWriterStart < global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, messageDataWriterStart, messageDataWriterCase == global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.NoCase ? global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Absent : messageDataWriterCase);
        return bytes[start..cursor.Index];
    }
}
