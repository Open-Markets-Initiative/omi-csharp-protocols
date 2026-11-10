using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  A Broken Trade Message informs you that an execution has been broken. The trade is no longer good and will not clear. The reason for the break is given.
/// </summary>
public static class BrokenTradeMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum;
        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MatchNumber MatchNumber;
        /// <summary>
        ///  The reason the trade was broken.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeReasonField BrokenTradeReason;
        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid;
    }

    /// <summary>
    ///  Reads the Broken Trade Message in place.
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
        ///  Gets the bytes of the Broken Trade Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureBrokenTradeMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Broken Trade Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public ref readonly Timestamp Timestamp => ref this.layout.Timestamp;

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public ref readonly UserRefNum UserRefNum => ref this.layout.UserRefNum;

        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public ref readonly MatchNumber MatchNumber => ref this.layout.MatchNumber;

        /// <summary>
        ///  The reason the trade was broken.
        /// </summary>
        public ref readonly BrokenTradeReasonField BrokenTradeReason => ref this.layout.BrokenTradeReason;

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public ref readonly Clordid Clordid => ref this.layout.Clordid;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        /// <remarks>
        ///  <see langword="null"/> when the bytes leave it out.
        /// </remarks>
        public AppendageLength? AppendageLength
        {
            get
            {
                int brokenTradeMessageAppendageLengthReaderPosition = Parser.Valid(Parser.BrokenTradeMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return brokenTradeMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[brokenTradeMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Broken Trade Appendage
        /// </summary>
        public BrokenTradeMessage.BrokenTradeAppendageEntries BrokenTradeAppendage
        {
            get
            {
                int brokenTradeAppendageReaderPosition = Parser.Valid(Parser.BrokenTradeMessageBrokenTradeAppendageStart(this.bytes, this.start, this.end, this.start, out var brokenTradeAppendageCursorLimit, out var brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End));
                return new(this.bytes, brokenTradeAppendageReaderPosition, brokenTradeAppendageCursorLimit, brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Broken Trade Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct BrokenTradeAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End;

        internal BrokenTradeAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End = brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly BrokenTradeAppendageEntries GetEnumerator() => this;

        /// <summary>
        ///  Moves to the next entry.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another entry; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="global::System.ArgumentOutOfRangeException">
        ///  The bytes end before the length that they state.
        /// </exception>
        public bool MoveNext()
        {
            int end = Parser.StepBrokenTradeMessageBrokenTradeAppendage(this.bytes, this.position, this.limit, this.brokenTradeAppendageCursorBrokenTradeAppendageCountRule0End);
            if (end == Parser.Position.Ended) return false;
            this.currentStart = this.position;
            this.currentEnd = Parser.Valid(end);
            this.position = this.currentEnd;
            this.index++;
            return true;
        }

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index;

        /// <summary>
        ///  Gets the Reader of the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Broken Trade Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum { get; init; }

        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MatchNumber MatchNumber { get; init; }

        /// <summary>
        ///  The reason the trade was broken.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeReasonField BrokenTradeReason { get; init; }

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Broken Trade Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Value> BrokenTradeAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Broken Trade Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMatchNumber + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeBrokenTradeReason + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeClordid;
    private const int brokenTradeMessageAppendageLengthDependencyWriteDistanceFromBrokenTradeMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Broken Trade Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <param name="sequencedMessageEnd">
    ///  The offset in destination where the enclosing Sequenced Message ends.
    /// </param>
    /// <returns>
    ///  The bytes of the Broken Trade Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value, int sequencedMessageEnd)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        cursor.SequencedDataPacket_SequencedMessage_End = sequencedMessageEnd;
        cursor.SequencedDataPacket_SequencedMessage_EndFrozen = true;
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int brokenTradeMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int brokenTradeMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int brokenTradeAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int brokenTradeAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int brokenTradeAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout brokenTradeMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        brokenTradeMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        brokenTradeMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        brokenTradeMessageTimestampWriterLayout.MatchNumber = value.MatchNumber;
        brokenTradeMessageTimestampWriterLayout.BrokenTradeReason = value.BrokenTradeReason;
        brokenTradeMessageTimestampWriterLayout.Clordid = value.Clordid;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } brokenTradeMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (brokenTradeMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeMessageAppendageLengthWriterStart = cursor.Index;
            brokenTradeMessageAppendageLengthWriterCount++;
            int brokenTradeMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.BrokenTradeMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int brokenTradeAppendageIndex = 0; brokenTradeAppendageIndex < value.BrokenTradeAppendage.Length; brokenTradeAppendageIndex++)
        {
            ref readonly var brokenTradeAppendageCurrent = ref value.BrokenTradeAppendage[brokenTradeAppendageIndex];
            if (brokenTradeMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeMessageAppendageLengthWriterStart = cursor.Index;
            if (brokenTradeAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeAppendageWriterStart = cursor.Index;
            brokenTradeAppendageWriterCount++;
            int brokenTradeAppendageWriting = cursor.Index;
            var brokenTradeAppendageWriteValue = brokenTradeAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Write(bytes, ref cursor, brokenTradeAppendageWriting, in brokenTradeAppendageWriteValue);
        }
        if (brokenTradeMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeMessageAppendageLengthWriterStart = cursor.Index;
        if (brokenTradeAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeAppendageWriterStart = cursor.Index;
        if (brokenTradeAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) brokenTradeAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.BrokenTradeMessageWriter.BrokenTradeAppendageCountRule0(bytes, ref cursor, brokenTradeMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : brokenTradeMessageAppendageLengthWriterStart + brokenTradeMessageAppendageLengthDependencyWriteDistanceFromBrokenTradeMessageAppendageLength, brokenTradeMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, brokenTradeAppendageWriterStart, brokenTradeAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.BrokenTradeMessageWriter.BrokenTradeMessageAppendageLengthOptionalRule0(bytes, ref cursor, brokenTradeMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, brokenTradeMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
