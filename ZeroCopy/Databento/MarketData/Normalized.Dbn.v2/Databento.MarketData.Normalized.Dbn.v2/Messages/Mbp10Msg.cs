using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Market by price implementation with a known book depth of 10
/// </summary>
public static class Mbp10Msg
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
            if (global::Databento.MarketData.Normalized.Dbn.v2.Mbp10Msg.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Price Price;
        /// <summary>
        ///  The order quantity
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Size Size;
        /// <summary>
        ///  The event action
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.ActionField Action;
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.SideField Side;
        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Flags Flags;
        /// <summary>
        ///  The book level where the update event occurred
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Depth Depth;
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsRecv TsRecv;
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsInDelta TsInDelta;
        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Sequence Sequence;
    }

    /// <summary>
    ///  Reads the Mbp 10 Msg in place.
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
        ///  Gets the bytes of the Mbp 10 Msg.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMbp10Msg(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Mbp 10 Msg.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public ref readonly Price Price => ref this.layout.Price;

        /// <summary>
        ///  The order quantity
        /// </summary>
        public ref readonly Size Size => ref this.layout.Size;

        /// <summary>
        ///  The event action
        /// </summary>
        public ref readonly ActionField Action => ref this.layout.Action;

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public ref readonly SideField Side => ref this.layout.Side;

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public ref readonly Flags Flags => ref this.layout.Flags;

        /// <summary>
        ///  The book level where the update event occurred
        /// </summary>
        public ref readonly Depth Depth => ref this.layout.Depth;

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public ref readonly TsRecv TsRecv => ref this.layout.TsRecv;

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public ref readonly TsInDelta TsInDelta => ref this.layout.TsInDelta;

        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public ref readonly Sequence Sequence => ref this.layout.Sequence;

        /// <summary>
        ///  A level
        /// </summary>
        public Mbp10Msg.BidAskPairEntries BidAskPair
        {
            get
            {
                int mbp10MsgBidAskPairReaderPosition = Parser.Valid(Parser.Mbp10MsgBidAskPairStart(this.bytes, this.start, this.end, this.start, out var mbp10MsgBidAskPairCursorLimit, out var mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries));
                return new(this.bytes, mbp10MsgBidAskPairReaderPosition, mbp10MsgBidAskPairCursorLimit, mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries);
            }
        }
    }

    /// <summary>
    ///  Steps through the Bid Ask Pair entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct BidAskPairEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries;

        internal BidAskPairEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries;
            this.mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries = mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly BidAskPairEntries GetEnumerator() => this;

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
            int end = Parser.StepMbp10MsgBidAskPair(this.bytes, this.position, this.limit, ref this.mbp10MsgBidAskPairCursorBidAskPairCountRule0Entries);
            if (end == Parser.Position.Ended) return false;
            this.currentStart = this.position;
            this.currentEnd = Parser.Valid(end);
            this.position = this.currentEnd;
            this.index++;
            return true;
        }

        /// <summary>
        ///  Gets the number of entries that the bytes state.
        /// </summary>
        /// <exception cref="global::System.OverflowException">
        ///  The number of entries is larger than an int can hold.
        /// </exception>
        public readonly long Count
        {
            get
            {
                if (!Parser.TryRuleCountValue(in this.total, out int count)) throw new global::System.OverflowException(Parser.CountTooLarge);
                return count;
            }
        }

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index;

        /// <summary>
        ///  Gets the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly ref readonly BidAskPair.Layout Current
        {
            get
            {
                Entry();
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BidAskPair.Layout>(this.bytes[this.currentStart..this.currentEnd]);
            }
        }
    }

    /// <summary>
    ///  The Mbp 10 Msg fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Price Price { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.Price.Absent;

        /// <summary>
        ///  The order quantity
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Size Size { get; init; }

        /// <summary>
        ///  The event action
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.ActionField Action { get; init; }

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.SideField Side { get; init; }

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Flags Flags { get; init; }

        /// <summary>
        ///  The book level where the update event occurred
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Depth Depth { get; init; }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.TsRecv.Absent;

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.TsInDelta TsInDelta { get; init; }

        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Sequence Sequence { get; init; }

        /// <summary>
        ///  A level
        /// </summary>
        public global::System.ReadOnlySpan<global::Databento.MarketData.Normalized.Dbn.v2.BidAskPair.Value> BidAskPair { get; init; }

        /// <summary>
        ///  Creates the value of the Mbp 10 Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeDepth + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSequence;

    /// <summary>
    ///  Writes the Mbp 10 Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Mbp 10 Msg.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int mbp10MsgBidAskPairWriterStart = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent;
        int mbp10MsgBidAskPairWriterCount = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterDestination);
        ref Layout mbp10MsgPriceWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        mbp10MsgPriceWriterLayout.Price = value.Price;
        mbp10MsgPriceWriterLayout.Size = value.Size;
        mbp10MsgPriceWriterLayout.Action = value.Action;
        mbp10MsgPriceWriterLayout.Side = value.Side;
        mbp10MsgPriceWriterLayout.Flags = value.Flags;
        mbp10MsgPriceWriterLayout.Depth = value.Depth;
        mbp10MsgPriceWriterLayout.TsRecv = value.TsRecv;
        mbp10MsgPriceWriterLayout.TsInDelta = value.TsInDelta;
        mbp10MsgPriceWriterLayout.Sequence = value.Sequence;
        cursor.Index += LayoutSize;
        for (int mbp10MsgBidAskPairIndex = 0; mbp10MsgBidAskPairIndex < value.BidAskPair.Length; mbp10MsgBidAskPairIndex++)
        {
            ref readonly var mbp10MsgBidAskPairCurrent = ref value.BidAskPair[mbp10MsgBidAskPairIndex];
            if (mbp10MsgBidAskPairWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) mbp10MsgBidAskPairWriterStart = cursor.Index;
            mbp10MsgBidAskPairWriterCount++;
            int mbp10MsgBidAskPairWriting = cursor.Index;
            var mbp10MsgBidAskPairWriteValue = mbp10MsgBidAskPairCurrent;
            global::Databento.MarketData.Normalized.Dbn.v2.BidAskPair.Write(bytes, ref cursor, mbp10MsgBidAskPairWriting, in mbp10MsgBidAskPairWriteValue);
        }
        if (mbp10MsgBidAskPairWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) mbp10MsgBidAskPairWriterStart = cursor.Index;
        {
            global::System.Int128 mbp10MsgBidAskPairCardinality = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Mbp10MsgWriter.Mbp10MsgBidAskPairCountRule0CountValue(bytes, ref cursor, mbp10MsgBidAskPairWriterStart);
            if (mbp10MsgBidAskPairWriterCount > mbp10MsgBidAskPairCardinality) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterRuleDisagrees, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterValue);
            while (mbp10MsgBidAskPairWriterCount < mbp10MsgBidAskPairCardinality)
            {
                global::Databento.MarketData.Normalized.Dbn.v2.BidAskPair.Value mbp10MsgBidAskPairWriterFilled = default;
                if (mbp10MsgBidAskPairWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) mbp10MsgBidAskPairWriterStart = cursor.Index;
                mbp10MsgBidAskPairWriterCount++;
                int mbp10MsgBidAskPairWriting = cursor.Index;
                var mbp10MsgBidAskPairWriteValue = mbp10MsgBidAskPairWriterFilled;
                global::Databento.MarketData.Normalized.Dbn.v2.BidAskPair.Write(bytes, ref cursor, mbp10MsgBidAskPairWriting, in mbp10MsgBidAskPairWriteValue);
            }
        }
        global::Databento.MarketData.Normalized.Dbn.v2.Parser.Mbp10MsgWriter.Mbp10MsgBidAskPairCountRule0(bytes, ref cursor, mbp10MsgBidAskPairWriterStart, mbp10MsgBidAskPairWriterCount);
        return bytes[start..cursor.Index];
    }
}
