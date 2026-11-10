using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  An Executed Order Message informs you that all or part of an order has been executed.
/// </summary>
public static class OrderExecutedMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity;
        /// <summary>
        ///  The price of the order.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Price Price;
        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LiquidityFlagField LiquidityFlag;
        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MatchNumber MatchNumber;
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength AppendageLength;
    }

    /// <summary>
    ///  Reads the Order Executed Message in place.
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
        ///  Gets the bytes of the Order Executed Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureOrderExecutedMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Executed Message.
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
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public ref readonly Quantity Quantity => ref this.layout.Quantity;

        /// <summary>
        ///  The price of the order.
        /// </summary>
        public ref readonly Price Price => ref this.layout.Price;

        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public ref readonly LiquidityFlagField LiquidityFlag => ref this.layout.LiquidityFlag;

        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public ref readonly MatchNumber MatchNumber => ref this.layout.MatchNumber;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Order Executed Appendage
        /// </summary>
        public OrderExecutedMessage.OrderExecutedAppendageEntries OrderExecutedAppendage
        {
            get
            {
                int orderExecutedAppendageReaderPosition = Parser.Valid(Parser.OrderExecutedMessageOrderExecutedAppendageStart(this.bytes, this.start, this.end, this.start, out var orderExecutedAppendageCursorLimit, out var orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End));
                return new(this.bytes, orderExecutedAppendageReaderPosition, orderExecutedAppendageCursorLimit, orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Order Executed Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct OrderExecutedAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End;

        internal OrderExecutedAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End = orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly OrderExecutedAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepOrderExecutedMessageOrderExecutedAppendage(this.bytes, this.position, this.limit, this.orderExecutedAppendageCursorOrderExecutedAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Order Executed Message fields to write.
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
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity { get; init; }

        /// <summary>
        ///  The price of the order.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Price Price { get; init; }

        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LiquidityFlagField LiquidityFlag { get; init; }

        /// <summary>
        ///  Assigned by the exchange to identify the trade. Both the buy and the sell executions participating in the trade will share the same match number.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MatchNumber MatchNumber { get; init; }

        /// <summary>
        ///  Order Executed Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Value> OrderExecutedAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Order Executed Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLiquidityFlag + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMatchNumber + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int orderExecutedMessageAppendageLengthDependencyWriteDistanceFromOrderExecutedMessage = 33;

    /// <summary>
    ///  Writes the Order Executed Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Executed Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int orderExecutedAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderExecutedAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderExecutedAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout orderExecutedMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        orderExecutedMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        orderExecutedMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        orderExecutedMessageTimestampWriterLayout.Quantity = value.Quantity;
        orderExecutedMessageTimestampWriterLayout.Price = value.Price;
        orderExecutedMessageTimestampWriterLayout.LiquidityFlag = value.LiquidityFlag;
        orderExecutedMessageTimestampWriterLayout.MatchNumber = value.MatchNumber;
        cursor.OrderExecutedMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int orderExecutedAppendageIndex = 0; orderExecutedAppendageIndex < value.OrderExecutedAppendage.Length; orderExecutedAppendageIndex++)
        {
            ref readonly var orderExecutedAppendageCurrent = ref value.OrderExecutedAppendage[orderExecutedAppendageIndex];
            if (orderExecutedAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderExecutedAppendageWriterStart = cursor.Index;
            orderExecutedAppendageWriterCount++;
            int orderExecutedAppendageWriting = cursor.Index;
            var orderExecutedAppendageWriteValue = orderExecutedAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Write(bytes, ref cursor, orderExecutedAppendageWriting, in orderExecutedAppendageWriteValue);
        }
        if (orderExecutedAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderExecutedAppendageWriterStart = cursor.Index;
        if (orderExecutedAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderExecutedAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderExecutedMessageWriter.OrderExecutedAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderExecutedMessageAppendageLengthDependencyWriteDistanceFromOrderExecutedMessage, orderExecutedAppendageWriterStart, orderExecutedAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
