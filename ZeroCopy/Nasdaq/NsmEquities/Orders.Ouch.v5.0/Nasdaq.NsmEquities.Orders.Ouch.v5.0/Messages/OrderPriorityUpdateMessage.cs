using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  A Priority Update Message is sent whenever priority of the order has been changed by the system.
/// </summary>
public static class OrderPriorityUpdateMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  The price of the order.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Price Price;
        /// <summary>
        ///  Display
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayField Display;
        /// <summary>
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderReferenceNumber OrderReferenceNumber;
    }

    /// <summary>
    ///  Reads the Order Priority Update Message in place.
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
        ///  Gets the bytes of the Order Priority Update Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureOrderPriorityUpdateMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Priority Update Message.
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
        ///  The price of the order.
        /// </summary>
        public ref readonly Price Price => ref this.layout.Price;

        /// <summary>
        ///  Display
        /// </summary>
        public ref readonly DisplayField Display => ref this.layout.Display;

        /// <summary>
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public ref readonly OrderReferenceNumber OrderReferenceNumber => ref this.layout.OrderReferenceNumber;

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
                int orderPriorityUpdateMessageAppendageLengthReaderPosition = Parser.Valid(Parser.OrderPriorityUpdateMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return orderPriorityUpdateMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[orderPriorityUpdateMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  Order Priority Update Appendage
        /// </summary>
        public OrderPriorityUpdateMessage.OrderPriorityUpdateAppendageEntries OrderPriorityUpdateAppendage
        {
            get
            {
                int orderPriorityUpdateAppendageReaderPosition = Parser.Valid(Parser.OrderPriorityUpdateMessageOrderPriorityUpdateAppendageStart(this.bytes, this.start, this.end, this.start, out var orderPriorityUpdateAppendageCursorLimit, out var orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End));
                return new(this.bytes, orderPriorityUpdateAppendageReaderPosition, orderPriorityUpdateAppendageCursorLimit, orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Order Priority Update Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct OrderPriorityUpdateAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End;

        internal OrderPriorityUpdateAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End = orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly OrderPriorityUpdateAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepOrderPriorityUpdateMessageOrderPriorityUpdateAppendage(this.bytes, this.position, this.limit, this.orderPriorityUpdateAppendageCursorOrderPriorityUpdateAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Order Priority Update Message fields to write.
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
        ///  The price of the order.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Price Price { get; init; }

        /// <summary>
        ///  Display
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayField Display { get; init; }

        /// <summary>
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderReferenceNumber OrderReferenceNumber { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  Order Priority Update Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Value> OrderPriorityUpdateAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Order Priority Update Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplay + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderReferenceNumber;
    private const int orderPriorityUpdateMessageAppendageLengthDependencyWriteDistanceFromOrderPriorityUpdateMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Order Priority Update Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Order Priority Update Message.
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
        int orderPriorityUpdateMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderPriorityUpdateMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int orderPriorityUpdateAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderPriorityUpdateAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderPriorityUpdateAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout orderPriorityUpdateMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        orderPriorityUpdateMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        orderPriorityUpdateMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        orderPriorityUpdateMessageTimestampWriterLayout.Price = value.Price;
        orderPriorityUpdateMessageTimestampWriterLayout.Display = value.Display;
        orderPriorityUpdateMessageTimestampWriterLayout.OrderReferenceNumber = value.OrderReferenceNumber;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } orderPriorityUpdateMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (orderPriorityUpdateMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateMessageAppendageLengthWriterStart = cursor.Index;
            orderPriorityUpdateMessageAppendageLengthWriterCount++;
            int orderPriorityUpdateMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.OrderPriorityUpdateMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int orderPriorityUpdateAppendageIndex = 0; orderPriorityUpdateAppendageIndex < value.OrderPriorityUpdateAppendage.Length; orderPriorityUpdateAppendageIndex++)
        {
            ref readonly var orderPriorityUpdateAppendageCurrent = ref value.OrderPriorityUpdateAppendage[orderPriorityUpdateAppendageIndex];
            if (orderPriorityUpdateMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateMessageAppendageLengthWriterStart = cursor.Index;
            if (orderPriorityUpdateAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateAppendageWriterStart = cursor.Index;
            orderPriorityUpdateAppendageWriterCount++;
            int orderPriorityUpdateAppendageWriting = cursor.Index;
            var orderPriorityUpdateAppendageWriteValue = orderPriorityUpdateAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Write(bytes, ref cursor, orderPriorityUpdateAppendageWriting, in orderPriorityUpdateAppendageWriteValue);
        }
        if (orderPriorityUpdateMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateMessageAppendageLengthWriterStart = cursor.Index;
        if (orderPriorityUpdateAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateAppendageWriterStart = cursor.Index;
        if (orderPriorityUpdateAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderPriorityUpdateMessageWriter.OrderPriorityUpdateAppendageCountRule0(bytes, ref cursor, orderPriorityUpdateMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : orderPriorityUpdateMessageAppendageLengthWriterStart + orderPriorityUpdateMessageAppendageLengthDependencyWriteDistanceFromOrderPriorityUpdateMessageAppendageLength, orderPriorityUpdateMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, orderPriorityUpdateAppendageWriterStart, orderPriorityUpdateAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderPriorityUpdateMessageWriter.OrderPriorityUpdateMessageAppendageLengthOptionalRule0(bytes, ref cursor, orderPriorityUpdateMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, orderPriorityUpdateMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
