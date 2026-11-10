using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  A Rejected Message may be sent in response to an Enter Order Message or Replace Order Message if the order or replace cannot be accepted at this time. The reason for the rejection is given.
/// </summary>
public static class RejectedOrderMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  The reason the order was rejected.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderReasonField RejectedOrderReason;
        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid;
    }

    /// <summary>
    ///  Reads the Rejected Order Message in place.
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
        ///  Gets the bytes of the Rejected Order Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureRejectedOrderMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Rejected Order Message.
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
        ///  The reason the order was rejected.
        /// </summary>
        public ref readonly RejectedOrderReasonField RejectedOrderReason => ref this.layout.RejectedOrderReason;

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
                int rejectedOrderMessageAppendageLengthReaderPosition = Parser.Valid(Parser.RejectedOrderMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return rejectedOrderMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[rejectedOrderMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Rejected Order Appendage
        /// </summary>
        public RejectedOrderMessage.RejectedOrderAppendageEntries RejectedOrderAppendage
        {
            get
            {
                int rejectedOrderAppendageReaderPosition = Parser.Valid(Parser.RejectedOrderMessageRejectedOrderAppendageStart(this.bytes, this.start, this.end, this.start, out var rejectedOrderAppendageCursorLimit, out var rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End));
                return new(this.bytes, rejectedOrderAppendageReaderPosition, rejectedOrderAppendageCursorLimit, rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Rejected Order Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct RejectedOrderAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End;

        internal RejectedOrderAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End = rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly RejectedOrderAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepRejectedOrderMessageRejectedOrderAppendage(this.bytes, this.position, this.limit, this.rejectedOrderAppendageCursorRejectedOrderAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Rejected Order Message fields to write.
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
        ///  The reason the order was rejected.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderReasonField RejectedOrderReason { get; init; }

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Rejected Order Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Value> RejectedOrderAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Rejected Order Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRejectedOrderReason + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeClordid;
    private const int rejectedOrderMessageAppendageLengthDependencyWriteDistanceFromRejectedOrderMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Rejected Order Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Rejected Order Message.
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
        int rejectedOrderMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int rejectedOrderMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int rejectedOrderAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int rejectedOrderAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int rejectedOrderAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout rejectedOrderMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        rejectedOrderMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        rejectedOrderMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        rejectedOrderMessageTimestampWriterLayout.RejectedOrderReason = value.RejectedOrderReason;
        rejectedOrderMessageTimestampWriterLayout.Clordid = value.Clordid;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } rejectedOrderMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (rejectedOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderMessageAppendageLengthWriterStart = cursor.Index;
            rejectedOrderMessageAppendageLengthWriterCount++;
            int rejectedOrderMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.RejectedOrderMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int rejectedOrderAppendageIndex = 0; rejectedOrderAppendageIndex < value.RejectedOrderAppendage.Length; rejectedOrderAppendageIndex++)
        {
            ref readonly var rejectedOrderAppendageCurrent = ref value.RejectedOrderAppendage[rejectedOrderAppendageIndex];
            if (rejectedOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderMessageAppendageLengthWriterStart = cursor.Index;
            if (rejectedOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderAppendageWriterStart = cursor.Index;
            rejectedOrderAppendageWriterCount++;
            int rejectedOrderAppendageWriting = cursor.Index;
            var rejectedOrderAppendageWriteValue = rejectedOrderAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Write(bytes, ref cursor, rejectedOrderAppendageWriting, in rejectedOrderAppendageWriteValue);
        }
        if (rejectedOrderMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderMessageAppendageLengthWriterStart = cursor.Index;
        if (rejectedOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderAppendageWriterStart = cursor.Index;
        if (rejectedOrderAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) rejectedOrderAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.RejectedOrderMessageWriter.RejectedOrderAppendageCountRule0(bytes, ref cursor, rejectedOrderMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : rejectedOrderMessageAppendageLengthWriterStart + rejectedOrderMessageAppendageLengthDependencyWriteDistanceFromRejectedOrderMessageAppendageLength, rejectedOrderMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, rejectedOrderAppendageWriterStart, rejectedOrderAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.RejectedOrderMessageWriter.RejectedOrderMessageAppendageLengthOptionalRule0(bytes, ref cursor, rejectedOrderMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, rejectedOrderMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
