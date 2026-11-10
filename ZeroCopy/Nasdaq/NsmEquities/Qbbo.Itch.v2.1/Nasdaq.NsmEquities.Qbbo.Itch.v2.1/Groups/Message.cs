using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Mold Udp64 Message
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
            if (global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Message.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Mold Udp 64 Message Header
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Layout MessageHeader;
    }

    /// <summary>
    ///  Reads the Message in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ulong sequenceNumberSequenceSeed;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ulong sequenceNumberSequenceSeed)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.sequenceNumberSequenceSeed = sequenceNumberSequenceSeed;
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
        ///  Mold Udp 64 Message Header
        /// </summary>
        public ref readonly MessageHeader.Layout MessageHeader => ref this.layout.MessageHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public Payload.Reader Payload
        {
            get
            {
                int payloadReaderPosition = Parser.Valid(Parser.MessagePayloadStart(this.bytes, this.start, this.end, this.start, out var messageTypeValue, out var messageLengthValue));
                int payloadReaderEnd = Parser.Valid(Parser.MessagePayloadEnd(this.bytes, payloadReaderPosition, this.end, this.start, messageLengthValue));
                return new(this.bytes, payloadReaderPosition, payloadReaderEnd, messageTypeValue);
            }
        }

        /// <summary>
        ///  Gets the sequence number of the Message.
        /// </summary>
        public ulong SequenceNumber => this.sequenceNumberSequenceSeed;
    }

    /// <summary>
    ///  The Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Mold Udp 64 Message Header
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Value MessageHeader { get; init; } = new();

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Value Payload { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMessageHeader;
    private const int messageLengthDependencyWriteDistanceFromMessageHeader = 0;
    private const int messageTypeDependencyWriteDistanceFromMessageHeader = 2;
    private const int SystemEventMessageCase = 1;
    private const int StockDirectoryMessageCase = 2;
    private const int StockTradingActionMessageCase = 3;
    private const int RegShoRestrictionMessageCase = 4;
    private const int MwcbDeclineLevelMessageCase = 5;
    private const int MwcbBreachMessageCase = 6;
    private const int OperationalHaltMessageCase = 7;
    private const int BboQuotationMessageCase = 8;
    private const int PriceImprovementMessageCase = 9;
    private const int IpoQuotingPeriodUpdateMessageCase = 10;

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
        var cursor = new global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int messageHeaderWriterStart = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent;
        int payloadWriterStart = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent;
        int payloadWriterCase = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent;
        bool messageLengthDependencyFirstUsed = false;
        bool messageTypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterDestination);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        int messageHeaderWriting = cursor.Index;
        var messageHeaderWriteValue = value.MessageHeader;
        global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageHeader.Write(bytes, ref cursor, messageHeaderWriting, ref messageHeaderWriterStart == messageHeaderWriting ? ref messageLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref messageHeaderWriterStart == messageHeaderWriting ? ref messageTypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in messageHeaderWriteValue);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (payloadWriterStart == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent) payloadWriterStart = cursor.Index;
        int payloadWriting = cursor.Index;
        var payloadWriteValue = value.Payload;
        global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Write(bytes, ref cursor, payloadWriting, ref messageLengthDependencyFirstUsed, ref payloadWriterCase, in payloadWriteValue);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (payloadWriterStart == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent) payloadWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.MessageSizeRule1(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageLengthDependencyWriteDistanceFromMessageHeader, ref messageLengthDependencyFirstUsed, start, cursor.Index);
        if (payloadWriterCase == SystemEventMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule1(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == StockDirectoryMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule2(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == StockTradingActionMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule3(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == RegShoRestrictionMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule4(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == MwcbDeclineLevelMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule5(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == MwcbBreachMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule6(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == OperationalHaltMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule7(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == BboQuotationMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule8(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == PriceImprovementMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule9(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        if (payloadWriterCase == IpoQuotingPeriodUpdateMessageCase) global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadBranchRule10(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart);
        global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.MessageWriter.PayloadCases(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, payloadWriterStart, payloadWriterCase == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : payloadWriterCase);
        return bytes[start..cursor.Index];
    }
}
