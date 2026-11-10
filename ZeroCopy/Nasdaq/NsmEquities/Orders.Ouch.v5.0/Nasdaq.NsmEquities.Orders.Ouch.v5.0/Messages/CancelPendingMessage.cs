using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  A Cancel Pending Message is sent in response to a cancel request for a cross order during a pre-cross late period signifying that it cannot be canceled at this time, but any unexecuted portion of this order will automatically be canceled immediately after the cross completes.
/// </summary>
public static class CancelPendingMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
    ///  Reads the Cancel Pending Message in place.
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
        ///  Gets the bytes of the Cancel Pending Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureCancelPendingMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Cancel Pending Message.
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
                int cancelPendingMessageAppendageLengthReaderPosition = Parser.Valid(Parser.CancelPendingMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return cancelPendingMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[cancelPendingMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Cancel Pending Appendage
        /// </summary>
        public CancelPendingMessage.CancelPendingAppendageEntries CancelPendingAppendage
        {
            get
            {
                int cancelPendingAppendageReaderPosition = Parser.Valid(Parser.CancelPendingMessageCancelPendingAppendageStart(this.bytes, this.start, this.end, this.start, out var cancelPendingAppendageCursorLimit, out var cancelPendingAppendageCursorCancelPendingAppendageCountRule0End));
                return new(this.bytes, cancelPendingAppendageReaderPosition, cancelPendingAppendageCursorLimit, cancelPendingAppendageCursorCancelPendingAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Cancel Pending Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct CancelPendingAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int cancelPendingAppendageCursorCancelPendingAppendageCountRule0End;

        internal CancelPendingAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int cancelPendingAppendageCursorCancelPendingAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.cancelPendingAppendageCursorCancelPendingAppendageCountRule0End = cancelPendingAppendageCursorCancelPendingAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly CancelPendingAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepCancelPendingMessageCancelPendingAppendage(this.bytes, this.position, this.limit, this.cancelPendingAppendageCursorCancelPendingAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Cancel Pending Message fields to write.
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
        ///  Cancel Pending Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Value> CancelPendingAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Cancel Pending Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum;
    private const int cancelPendingMessageAppendageLengthDependencyWriteDistanceFromCancelPendingMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Cancel Pending Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Cancel Pending Message.
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
        int cancelPendingMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelPendingMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int cancelPendingAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelPendingAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelPendingAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout cancelPendingMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        cancelPendingMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        cancelPendingMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } cancelPendingMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (cancelPendingMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingMessageAppendageLengthWriterStart = cursor.Index;
            cancelPendingMessageAppendageLengthWriterCount++;
            int cancelPendingMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.CancelPendingMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int cancelPendingAppendageIndex = 0; cancelPendingAppendageIndex < value.CancelPendingAppendage.Length; cancelPendingAppendageIndex++)
        {
            ref readonly var cancelPendingAppendageCurrent = ref value.CancelPendingAppendage[cancelPendingAppendageIndex];
            if (cancelPendingMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingMessageAppendageLengthWriterStart = cursor.Index;
            if (cancelPendingAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingAppendageWriterStart = cursor.Index;
            cancelPendingAppendageWriterCount++;
            int cancelPendingAppendageWriting = cursor.Index;
            var cancelPendingAppendageWriteValue = cancelPendingAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Write(bytes, ref cursor, cancelPendingAppendageWriting, in cancelPendingAppendageWriteValue);
        }
        if (cancelPendingMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingMessageAppendageLengthWriterStart = cursor.Index;
        if (cancelPendingAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingAppendageWriterStart = cursor.Index;
        if (cancelPendingAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelPendingMessageWriter.CancelPendingAppendageCountRule0(bytes, ref cursor, cancelPendingMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : cancelPendingMessageAppendageLengthWriterStart + cancelPendingMessageAppendageLengthDependencyWriteDistanceFromCancelPendingMessageAppendageLength, cancelPendingMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, cancelPendingAppendageWriterStart, cancelPendingAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelPendingMessageWriter.CancelPendingMessageAppendageLengthOptionalRule0(bytes, ref cursor, cancelPendingMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, cancelPendingMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
