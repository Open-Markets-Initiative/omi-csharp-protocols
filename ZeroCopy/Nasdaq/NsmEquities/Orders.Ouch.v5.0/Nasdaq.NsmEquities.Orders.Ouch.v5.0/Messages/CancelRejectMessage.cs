using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  A Cancel Reject Message is sent in response to a partial cancel request (with non-zero “intended order size”) for a cross order during a pre-cross late period signifying that it cannot be partially canceled at this time. No automatic cancel will be scheduled for this order. Clients could repeat their request for any unexecuted portion of the order after the cross completes.
/// </summary>
public static class CancelRejectMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
    }

    /// <summary>
    ///  Reads the Cancel Reject Message in place.
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
        ///  Gets the bytes of the Cancel Reject Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureCancelRejectMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Cancel Reject Message.
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
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        /// <remarks>
        ///  <see langword="null"/> when the bytes leave it out.
        /// </remarks>
        public AppendageLength? AppendageLength
        {
            get
            {
                int cancelRejectMessageAppendageLengthReaderPosition = Parser.Valid(Parser.CancelRejectMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return cancelRejectMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[cancelRejectMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Cancel Reject Appendage
        /// </summary>
        public CancelRejectMessage.CancelRejectAppendageEntries CancelRejectAppendage
        {
            get
            {
                int cancelRejectAppendageReaderPosition = Parser.Valid(Parser.CancelRejectMessageCancelRejectAppendageStart(this.bytes, this.start, this.end, this.start, out var cancelRejectAppendageCursorLimit, out var cancelRejectAppendageCursorCancelRejectAppendageCountRule0End));
                return new(this.bytes, cancelRejectAppendageReaderPosition, cancelRejectAppendageCursorLimit, cancelRejectAppendageCursorCancelRejectAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Cancel Reject Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct CancelRejectAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int cancelRejectAppendageCursorCancelRejectAppendageCountRule0End;

        internal CancelRejectAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int cancelRejectAppendageCursorCancelRejectAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.cancelRejectAppendageCursorCancelRejectAppendageCountRule0End = cancelRejectAppendageCursorCancelRejectAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly CancelRejectAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepCancelRejectMessageCancelRejectAppendage(this.bytes, this.position, this.limit, this.cancelRejectAppendageCursorCancelRejectAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Cancel Reject Message fields to write.
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
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Cancel Reject Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Value> CancelRejectAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Cancel Reject Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum;
    private const int cancelRejectMessageAppendageLengthDependencyWriteDistanceFromCancelRejectMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Cancel Reject Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Cancel Reject Message.
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
        int cancelRejectMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelRejectMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int cancelRejectAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelRejectAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelRejectAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout cancelRejectMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        cancelRejectMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        cancelRejectMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } cancelRejectMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (cancelRejectMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectMessageAppendageLengthWriterStart = cursor.Index;
            cancelRejectMessageAppendageLengthWriterCount++;
            int cancelRejectMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.CancelRejectMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int cancelRejectAppendageIndex = 0; cancelRejectAppendageIndex < value.CancelRejectAppendage.Length; cancelRejectAppendageIndex++)
        {
            ref readonly var cancelRejectAppendageCurrent = ref value.CancelRejectAppendage[cancelRejectAppendageIndex];
            if (cancelRejectMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectMessageAppendageLengthWriterStart = cursor.Index;
            if (cancelRejectAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectAppendageWriterStart = cursor.Index;
            cancelRejectAppendageWriterCount++;
            int cancelRejectAppendageWriting = cursor.Index;
            var cancelRejectAppendageWriteValue = cancelRejectAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Write(bytes, ref cursor, cancelRejectAppendageWriting, in cancelRejectAppendageWriteValue);
        }
        if (cancelRejectMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectMessageAppendageLengthWriterStart = cursor.Index;
        if (cancelRejectAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectAppendageWriterStart = cursor.Index;
        if (cancelRejectAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelRejectAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelRejectMessageWriter.CancelRejectAppendageCountRule0(bytes, ref cursor, cancelRejectMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : cancelRejectMessageAppendageLengthWriterStart + cancelRejectMessageAppendageLengthDependencyWriteDistanceFromCancelRejectMessageAppendageLength, cancelRejectMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, cancelRejectAppendageWriterStart, cancelRejectAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelRejectMessageWriter.CancelRejectMessageAppendageLengthOptionalRule0(bytes, ref cursor, cancelRejectMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, cancelRejectMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
