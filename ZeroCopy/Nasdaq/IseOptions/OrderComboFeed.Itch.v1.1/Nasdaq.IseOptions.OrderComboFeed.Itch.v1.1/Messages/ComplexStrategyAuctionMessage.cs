using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  This message is used to announce strategy auctions. The start of auction is announced, followed by possible updates on the auction, and announcing the end of the auction.
/// </summary>
public static class ComplexStrategyAuctionMessage
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
            if (global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp;
        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId;
        /// <summary>
        ///  Integer which uniquely identifies the auction.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionId AuctionId;
        /// <summary>
        ///  Order Type
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderTypeField OrderType;
        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SideField Side;
        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Price Price;
        /// <summary>
        ///  Size of the order
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Size Size;
        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExecFlagField ExecFlag;
        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderCapacityField OrderCapacity;
        /// <summary>
        ///  Scope value
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ScopeField Scope;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OwnerId OwnerId;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Giveup Giveup;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Cmta Cmta;
        /// <summary>
        ///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionEventField AuctionEvent;
        /// <summary>
        ///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionTypeField AuctionType;
        /// <summary>
        ///  Number of auction Responses. Next two fields repeat that number of times. Allowable values for this field are 0 or 1.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.NumberOfResponses NumberOfResponses;
    }

    /// <summary>
    ///  Reads the Complex Strategy Auction Message in place.
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
        ///  Gets the bytes of the Complex Strategy Auction Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureComplexStrategyAuctionMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Complex Strategy Auction Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public ref readonly Timestamp Timestamp => ref this.layout.Timestamp;

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public ref readonly StrategyId StrategyId => ref this.layout.StrategyId;

        /// <summary>
        ///  Integer which uniquely identifies the auction.
        /// </summary>
        public ref readonly AuctionId AuctionId => ref this.layout.AuctionId;

        /// <summary>
        ///  Order Type
        /// </summary>
        public ref readonly OrderTypeField OrderType => ref this.layout.OrderType;

        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public ref readonly SideField Side => ref this.layout.Side;

        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public ref readonly Price Price => ref this.layout.Price;

        /// <summary>
        ///  Size of the order
        /// </summary>
        public ref readonly Size Size => ref this.layout.Size;

        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public ref readonly ExecFlagField ExecFlag => ref this.layout.ExecFlag;

        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public ref readonly OrderCapacityField OrderCapacity => ref this.layout.OrderCapacity;

        /// <summary>
        ///  Scope value
        /// </summary>
        public ref readonly ScopeField Scope => ref this.layout.Scope;

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public ref readonly OwnerId OwnerId => ref this.layout.OwnerId;

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public ref readonly Giveup Giveup => ref this.layout.Giveup;

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public ref readonly Cmta Cmta => ref this.layout.Cmta;

        /// <summary>
        ///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
        /// </summary>
        public ref readonly AuctionEventField AuctionEvent => ref this.layout.AuctionEvent;

        /// <summary>
        ///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
        /// </summary>
        public ref readonly AuctionTypeField AuctionType => ref this.layout.AuctionType;

        /// <summary>
        ///  Number of auction Responses. Next two fields repeat that number of times. Allowable values for this field are 0 or 1.
        /// </summary>
        public ref readonly NumberOfResponses NumberOfResponses => ref this.layout.NumberOfResponses;

        /// <summary>
        ///  Auction Response information
        /// </summary>
        public ComplexStrategyAuctionMessage.AuctionResponseEntries AuctionResponse
        {
            get
            {
                int auctionResponseReaderPosition = Parser.Valid(Parser.ComplexStrategyAuctionMessageAuctionResponseStart(this.bytes, this.start, this.end, this.start, out var auctionResponseCursorLimit, out var auctionResponseCursorAuctionResponseCountRule0Entries));
                return new(this.bytes, auctionResponseReaderPosition, auctionResponseCursorLimit, auctionResponseCursorAuctionResponseCountRule0Entries);
            }
        }
    }

    /// <summary>
    ///  Steps through the Auction Response entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct AuctionResponseEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount auctionResponseCursorAuctionResponseCountRule0Entries;

        internal AuctionResponseEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount auctionResponseCursorAuctionResponseCountRule0Entries)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = auctionResponseCursorAuctionResponseCountRule0Entries;
            this.auctionResponseCursorAuctionResponseCountRule0Entries = auctionResponseCursorAuctionResponseCountRule0Entries;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly AuctionResponseEntries GetEnumerator() => this;

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
            int end = Parser.StepComplexStrategyAuctionMessageAuctionResponse(this.bytes, this.position, this.limit, ref this.auctionResponseCursorAuctionResponseCountRule0Entries);
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
        public readonly ref readonly AuctionResponse.Layout Current
        {
            get
            {
                Entry();
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AuctionResponse.Layout>(this.bytes[this.currentStart..this.currentEnd]);
            }
        }
    }

    /// <summary>
    ///  The Complex Strategy Auction Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId { get; init; }

        /// <summary>
        ///  Integer which uniquely identifies the auction.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionId AuctionId { get; init; }

        /// <summary>
        ///  Order Type
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderTypeField OrderType { get; init; }

        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SideField Side { get; init; }

        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Price Price { get; init; }

        /// <summary>
        ///  Size of the order
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Size Size { get; init; }

        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExecFlagField ExecFlag { get; init; }

        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderCapacityField OrderCapacity { get; init; }

        /// <summary>
        ///  Scope value
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ScopeField Scope { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OwnerId OwnerId { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Giveup Giveup { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Cmta Cmta { get; init; }

        /// <summary>
        ///  ‘S’ = Start ‘U’ = Auction Update ‘E’ = End of Auction
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionEventField AuctionEvent { get; init; }

        /// <summary>
        ///  ‘E’ = Exposure ‘C’ = Facilitation ‘S’ = Solicitation ‘P’ = PIM
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionTypeField AuctionType { get; init; }

        /// <summary>
        ///  Auction Response information
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Value> AuctionResponse { get; init; }

        /// <summary>
        ///  Creates the value of the Complex Strategy Auction Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeStrategyId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeAuctionId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSide + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSize + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExecFlag + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderCapacity + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeScope + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOwnerId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeGiveup + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCmta + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeAuctionEvent + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeAuctionType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeNumberOfResponses;
    private const int numberOfResponsesDependencyWriteDistanceFromComplexStrategyAuctionMessage = 47;

    /// <summary>
    ///  Writes the Complex Strategy Auction Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Complex Strategy Auction Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int auctionResponseWriterStart = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent;
        int auctionResponseWriterCount = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterDestination);
        ref Layout complexStrategyAuctionMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        complexStrategyAuctionMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        complexStrategyAuctionMessageTimestampWriterLayout.StrategyId = value.StrategyId;
        complexStrategyAuctionMessageTimestampWriterLayout.AuctionId = value.AuctionId;
        complexStrategyAuctionMessageTimestampWriterLayout.OrderType = value.OrderType;
        complexStrategyAuctionMessageTimestampWriterLayout.Side = value.Side;
        complexStrategyAuctionMessageTimestampWriterLayout.Price = value.Price;
        complexStrategyAuctionMessageTimestampWriterLayout.Size = value.Size;
        complexStrategyAuctionMessageTimestampWriterLayout.ExecFlag = value.ExecFlag;
        complexStrategyAuctionMessageTimestampWriterLayout.OrderCapacity = value.OrderCapacity;
        complexStrategyAuctionMessageTimestampWriterLayout.Scope = value.Scope;
        complexStrategyAuctionMessageTimestampWriterLayout.OwnerId = value.OwnerId;
        complexStrategyAuctionMessageTimestampWriterLayout.Giveup = value.Giveup;
        complexStrategyAuctionMessageTimestampWriterLayout.Cmta = value.Cmta;
        complexStrategyAuctionMessageTimestampWriterLayout.AuctionEvent = value.AuctionEvent;
        complexStrategyAuctionMessageTimestampWriterLayout.AuctionType = value.AuctionType;
        cursor.ComplexStrategyAuctionMessage_NumberOfResponses_Used = false;
        cursor.Index += LayoutSize;
        for (int auctionResponseIndex = 0; auctionResponseIndex < value.AuctionResponse.Length; auctionResponseIndex++)
        {
            ref readonly var auctionResponseCurrent = ref value.AuctionResponse[auctionResponseIndex];
            if (auctionResponseWriterStart == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent) auctionResponseWriterStart = cursor.Index;
            auctionResponseWriterCount++;
            int auctionResponseWriting = cursor.Index;
            var auctionResponseWriteValue = auctionResponseCurrent;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.AuctionResponse.Write(bytes, ref cursor, auctionResponseWriting, in auctionResponseWriteValue);
        }
        if (auctionResponseWriterStart == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent) auctionResponseWriterStart = cursor.Index;
        global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ComplexStrategyAuctionMessageWriter.AuctionResponseCountRule0(bytes, ref cursor, start < global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum ? global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent : start + numberOfResponsesDependencyWriteDistanceFromComplexStrategyAuctionMessage, auctionResponseWriterStart, auctionResponseWriterCount);
        return bytes[start..cursor.Index];
    }
}
