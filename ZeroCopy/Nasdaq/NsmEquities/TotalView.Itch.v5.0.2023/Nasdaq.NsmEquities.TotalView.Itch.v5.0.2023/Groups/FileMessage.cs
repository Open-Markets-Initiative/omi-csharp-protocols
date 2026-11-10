using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Nasdaq historical file message: its length, then the message
/// </summary>
public static class FileMessage
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
            if (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Mold Udp 64 Message Header
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout MessageHeader;
    }

    /// <summary>
    ///  Reads the File Message in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        ///  Gets the bytes of the File Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the File Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Mold Udp 64 Message Header
        /// </summary>
        public ref readonly MessageHeader.Layout MessageHeader => ref this.layout.MessageHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public FilePayload.Reader FilePayload
        {
            get
            {
                int fileMessageFilePayloadReaderPosition = Parser.Valid(Parser.FileMessageFilePayloadStart(this.bytes, this.start, this.end, this.start, out var messageTypeValue, out var messageLengthValue));
                int fileMessageFilePayloadReaderEnd = Parser.Valid(Parser.FileMessageFilePayloadEnd(this.bytes, fileMessageFilePayloadReaderPosition, this.end, this.start, messageLengthValue));
                return new(this.bytes, fileMessageFilePayloadReaderPosition, fileMessageFilePayloadReaderEnd, messageTypeValue);
            }
        }
    }

    /// <summary>
    ///  The File Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Mold Udp 64 Message Header
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Value MessageHeader { get; init; } = new();

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Value FilePayload { get; init; } = new();

        /// <summary>
        ///  Creates the value of the File Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMessageHeader;
    private const int messageLengthDependencyWriteDistanceFromMessageHeader = 0;
    private const int messageTypeDependencyWriteDistanceFromMessageHeader = 2;
    private const int SystemEventMessageCase = 1;
    private const int StockDirectoryMessageCase = 2;
    private const int StockTradingActionMessageCase = 3;
    private const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
    private const int MarketParticipantPositionMessageCase = 5;
    private const int MwcbDeclineLevelMessageCase = 6;
    private const int MwcbStatusLevelMessageCase = 7;
    private const int IpoQuotingPeriodUpdateCase = 8;
    private const int LuldAuctionCollarMessageCase = 9;
    private const int OperationalHaltMessageCase = 10;
    private const int AddOrderNoMpidAttributionMessageCase = 11;
    private const int AddOrderWithMpidAttributionMessageCase = 12;
    private const int OrderExecutedMessageCase = 13;
    private const int OrderExecutedWithPriceMessageCase = 14;
    private const int OrderCancelMessageCase = 15;
    private const int OrderDeleteMessageCase = 16;
    private const int OrderReplaceMessageCase = 17;
    private const int NonCrossTradeMessageCase = 18;
    private const int CrossTradeMessageCase = 19;
    private const int BrokenTradeMessageCase = 20;
    private const int NetOrderImbalanceIndicatorMessageCase = 21;
    private const int RetailPriceImprovementIndicatorMessageCase = 22;
    private const int DirectListingWithCapitalRaisePriceDiscoveryMessageCase = 23;

    /// <summary>
    ///  Writes the File Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the File Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int messageHeaderWriterStart = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        int fileMessageFilePayloadWriterStart = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        int fileMessageFilePayloadWriterCase = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        bool messageLengthDependencyFirstUsed = false;
        bool messageTypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        int messageHeaderWriting = cursor.Index;
        var messageHeaderWriteValue = value.MessageHeader;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Write(bytes, ref cursor, messageHeaderWriting, ref messageHeaderWriterStart == messageHeaderWriting ? ref messageLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref messageHeaderWriterStart == messageHeaderWriting ? ref messageTypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in messageHeaderWriteValue);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (fileMessageFilePayloadWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent) fileMessageFilePayloadWriterStart = cursor.Index;
        int fileMessageFilePayloadWriting = cursor.Index;
        var fileMessageFilePayloadWriteValue = value.FilePayload;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Write(bytes, ref cursor, fileMessageFilePayloadWriting, ref messageLengthDependencyFirstUsed, ref fileMessageFilePayloadWriterCase, in fileMessageFilePayloadWriteValue);
        if (messageHeaderWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent) messageHeaderWriterStart = cursor.Index;
        if (fileMessageFilePayloadWriterStart == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent) fileMessageFilePayloadWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageReassembleRule1(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageLengthDependencyWriteDistanceFromMessageHeader, ref messageLengthDependencyFirstUsed, start, cursor.Index);
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageSizeRule2(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageLengthDependencyWriteDistanceFromMessageHeader, ref messageLengthDependencyFirstUsed, start, cursor.Index);
        if (fileMessageFilePayloadWriterCase == SystemEventMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule1(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == StockDirectoryMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule2(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == StockTradingActionMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule3(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == RegShoShortSalePriceTestRestrictedIndicatorMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule4(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == MarketParticipantPositionMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule5(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == MwcbDeclineLevelMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule6(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == MwcbStatusLevelMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule7(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == IpoQuotingPeriodUpdateCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule8(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == LuldAuctionCollarMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule9(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OperationalHaltMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule10(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == AddOrderNoMpidAttributionMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule11(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == AddOrderWithMpidAttributionMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule12(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OrderExecutedMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule13(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OrderExecutedWithPriceMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule14(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OrderCancelMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule15(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OrderDeleteMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule16(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == OrderReplaceMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule17(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == NonCrossTradeMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule18(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == CrossTradeMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule19(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == BrokenTradeMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule20(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == NetOrderImbalanceIndicatorMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule21(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == RetailPriceImprovementIndicatorMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule22(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        if (fileMessageFilePayloadWriterCase == DirectListingWithCapitalRaisePriceDiscoveryMessageCase) global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadBranchRule23(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart);
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.FileMessageWriter.FileMessageFilePayloadCases(bytes, ref cursor, messageHeaderWriterStart < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : messageHeaderWriterStart + messageTypeDependencyWriteDistanceFromMessageHeader, ref messageTypeDependencyFirstUsed, fileMessageFilePayloadWriterStart, fileMessageFilePayloadWriterCase == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : fileMessageFilePayloadWriterCase);
        return bytes[start..cursor.Index];
    }
}
