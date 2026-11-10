using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  AIQ Canceled Message
/// </summary>
public static class AiqCanceledMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DecrementShares DecrementShares;
        /// <summary>
        ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderCancelReason OrderCancelReason;
        /// <summary>
        ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.QuantityPreventedFromTrading QuantityPreventedFromTrading;
        /// <summary>
        ///  Price at which the trade would have occurred
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExecutionPrice ExecutionPrice;
        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LiquidityFlagField LiquidityFlag;
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField AiqStrategy;
    }

    /// <summary>
    ///  Reads the Aiq Canceled Message in place.
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
        ///  Gets the bytes of the Aiq Canceled Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureAiqCanceledMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Aiq Canceled Message.
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
        ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
        /// </summary>
        public ref readonly DecrementShares DecrementShares => ref this.layout.DecrementShares;

        /// <summary>
        ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
        /// </summary>
        public ref readonly OrderCancelReason OrderCancelReason => ref this.layout.OrderCancelReason;

        /// <summary>
        ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
        /// </summary>
        public ref readonly QuantityPreventedFromTrading QuantityPreventedFromTrading => ref this.layout.QuantityPreventedFromTrading;

        /// <summary>
        ///  Price at which the trade would have occurred
        /// </summary>
        public ref readonly ExecutionPrice ExecutionPrice => ref this.layout.ExecutionPrice;

        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public ref readonly LiquidityFlagField LiquidityFlag => ref this.layout.LiquidityFlag;

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        public ref readonly AiqStrategyField AiqStrategy => ref this.layout.AiqStrategy;

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
                int aiqCanceledMessageAppendageLengthReaderPosition = Parser.Valid(Parser.AiqCanceledMessageAppendageLengthStart(this.bytes, this.start, this.end, this.start));
                return aiqCanceledMessageAppendageLengthReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AppendageLength>(this.bytes[aiqCanceledMessageAppendageLengthReaderPosition..this.end]);
            }
        }

        /// <summary>
        ///  AIQ Canceled Appendage
        /// </summary>
        public AiqCanceledMessage.AiqCanceledAppendageEntries AiqCanceledAppendage
        {
            get
            {
                int aiqCanceledAppendageReaderPosition = Parser.Valid(Parser.AiqCanceledMessageAiqCanceledAppendageStart(this.bytes, this.start, this.end, this.start, out var aiqCanceledAppendageCursorLimit, out var aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End));
                return new(this.bytes, aiqCanceledAppendageReaderPosition, aiqCanceledAppendageCursorLimit, aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Aiq Canceled Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct AiqCanceledAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End;

        internal AiqCanceledAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End = aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly AiqCanceledAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepAiqCanceledMessageAiqCanceledAppendage(this.bytes, this.position, this.limit, this.aiqCanceledAppendageCursorAiqCanceledAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Aiq Canceled Message fields to write.
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
        ///  The number of shares just decremented from the order. This number is incremental, not cumulative.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DecrementShares DecrementShares { get; init; }

        /// <summary>
        ///  Reason the order was reduced or canceled. For an AIQ Cancel message, this value will always be “Q”.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderCancelReason OrderCancelReason { get; init; }

        /// <summary>
        ///  Shares that would have executed if the trade would have occurred. Depending on AIQ type and the sizes of orders in question the value for this field could either be the same or different than the Decrement Shares field. For “Decrement both” they are always the same. For “Cancel oldest” they will be different if the incoming order is smaller than the resting order.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.QuantityPreventedFromTrading QuantityPreventedFromTrading { get; init; }

        /// <summary>
        ///  Price at which the trade would have occurred
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExecutionPrice ExecutionPrice { get; init; }

        /// <summary>
        ///  Liquidity flag the order would have received.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LiquidityFlagField LiquidityFlag { get; init; }

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField AiqStrategy { get; init; }

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength? AppendageLength { get; init; }

        /// <summary>
        ///  AIQ Canceled Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Value> AiqCanceledAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Aiq Canceled Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDecrementShares + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderCancelReason + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantityPreventedFromTrading + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExecutionPrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLiquidityFlag + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy;
    private const int aiqCanceledMessageAppendageLengthDependencyWriteDistanceFromAiqCanceledMessageAppendageLength = 0;

    /// <summary>
    ///  Writes the Aiq Canceled Message to the start of destination and fills in each length, count and message type in it.
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
    ///  The bytes of the Aiq Canceled Message.
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
        int aiqCanceledMessageAppendageLengthWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int aiqCanceledMessageAppendageLengthWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        int aiqCanceledAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int aiqCanceledAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int aiqCanceledAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout aiqCanceledMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        aiqCanceledMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        aiqCanceledMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        aiqCanceledMessageTimestampWriterLayout.DecrementShares = value.DecrementShares;
        aiqCanceledMessageTimestampWriterLayout.OrderCancelReason = value.OrderCancelReason;
        aiqCanceledMessageTimestampWriterLayout.QuantityPreventedFromTrading = value.QuantityPreventedFromTrading;
        aiqCanceledMessageTimestampWriterLayout.ExecutionPrice = value.ExecutionPrice;
        aiqCanceledMessageTimestampWriterLayout.LiquidityFlag = value.LiquidityFlag;
        aiqCanceledMessageTimestampWriterLayout.AiqStrategy = value.AiqStrategy;
        cursor.Index += LayoutSize;
        if (value.AppendageLength is { } aiqCanceledMessageAppendageLengthCurrent)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            if (aiqCanceledMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledMessageAppendageLengthWriterStart = cursor.Index;
            aiqCanceledMessageAppendageLengthWriterCount++;
            int aiqCanceledMessageAppendageLengthWriterPosition = cursor.Index;
            cursor.AiqCanceledMessage_AppendageLength_Used = false;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
        }
        for (int aiqCanceledAppendageIndex = 0; aiqCanceledAppendageIndex < value.AiqCanceledAppendage.Length; aiqCanceledAppendageIndex++)
        {
            ref readonly var aiqCanceledAppendageCurrent = ref value.AiqCanceledAppendage[aiqCanceledAppendageIndex];
            if (aiqCanceledMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledMessageAppendageLengthWriterStart = cursor.Index;
            if (aiqCanceledAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledAppendageWriterStart = cursor.Index;
            aiqCanceledAppendageWriterCount++;
            int aiqCanceledAppendageWriting = cursor.Index;
            var aiqCanceledAppendageWriteValue = aiqCanceledAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Write(bytes, ref cursor, aiqCanceledAppendageWriting, in aiqCanceledAppendageWriteValue);
        }
        if (aiqCanceledMessageAppendageLengthWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledMessageAppendageLengthWriterStart = cursor.Index;
        if (aiqCanceledAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledAppendageWriterStart = cursor.Index;
        if (aiqCanceledAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AiqCanceledMessageWriter.AiqCanceledAppendageCountRule0(bytes, ref cursor, aiqCanceledMessageAppendageLengthWriterStart < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : aiqCanceledMessageAppendageLengthWriterStart + aiqCanceledMessageAppendageLengthDependencyWriteDistanceFromAiqCanceledMessageAppendageLength, aiqCanceledMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries, aiqCanceledAppendageWriterStart, aiqCanceledAppendageWriterEnd);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AiqCanceledMessageWriter.AiqCanceledMessageAppendageLengthOptionalRule0(bytes, ref cursor, aiqCanceledMessageAppendageLengthWriterStart, cursor.SequencedDataPacket_SequencedMessage_End, aiqCanceledMessageAppendageLengthWriterCount != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries);
        return bytes[start..cursor.Index];
    }
}
