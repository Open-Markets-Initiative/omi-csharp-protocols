using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Replace Order Message allows you to cancel and replace an existing order in a single message. This is more efficient than canceling an existing order and immediately succeeding it with a new order. Replacing an order always gives it a new timestamp for its time priority on the book. If you wish you simply partially cancel an order and retain its time priority, send a Cancel Order Message instead.
/// </summary>
public static class ReplaceOrderMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrigUserRefNum OrigUserRefNum;
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
        ///  Corresponds to TimeInForce (59) in Nasdaq FIX
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TimeInForceField TimeInForce;
        /// <summary>
        ///  Display
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayField Display;
        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.InterMarketSweepEligibilityField InterMarketSweepEligibility;
        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid;
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength AppendageLength;
    }

    /// <summary>
    ///  Reads the Replace Order Message in place.
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
        ///  Gets the bytes of the Replace Order Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureReplaceOrderMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Replace Order Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public ref readonly OrigUserRefNum OrigUserRefNum => ref this.layout.OrigUserRefNum;

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
        ///  Corresponds to TimeInForce (59) in Nasdaq FIX
        /// </summary>
        public ref readonly TimeInForceField TimeInForce => ref this.layout.TimeInForce;

        /// <summary>
        ///  Display
        /// </summary>
        public ref readonly DisplayField Display => ref this.layout.Display;

        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public ref readonly InterMarketSweepEligibilityField InterMarketSweepEligibility => ref this.layout.InterMarketSweepEligibility;

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public ref readonly Clordid Clordid => ref this.layout.Clordid;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Replace Order Appendage
        /// </summary>
        public ReplaceOrderMessage.ReplaceOrderAppendageEntries ReplaceOrderAppendage
        {
            get
            {
                int replaceOrderAppendageReaderPosition = Parser.Valid(Parser.ReplaceOrderMessageReplaceOrderAppendageStart(this.bytes, this.start, this.end, this.start, out var replaceOrderAppendageCursorLimit, out var replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End));
                return new(this.bytes, replaceOrderAppendageReaderPosition, replaceOrderAppendageCursorLimit, replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Replace Order Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct ReplaceOrderAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End;

        internal ReplaceOrderAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End = replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly ReplaceOrderAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepReplaceOrderMessageReplaceOrderAppendage(this.bytes, this.position, this.limit, this.replaceOrderAppendageCursorReplaceOrderAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Replace Order Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrigUserRefNum OrigUserRefNum { get; init; }

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
        ///  Corresponds to TimeInForce (59) in Nasdaq FIX
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TimeInForceField TimeInForce { get; init; }

        /// <summary>
        ///  Display
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayField Display { get; init; }

        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.InterMarketSweepEligibilityField InterMarketSweepEligibility { get; init; }

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid { get; init; }

        /// <summary>
        ///  Replace Order Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Value> ReplaceOrderAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Replace Order Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrigUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimeInForce + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplay + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeInterMarketSweepEligibility + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeClordid + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int replaceOrderMessageAppendageLengthDependencyWriteDistanceFromReplaceOrderMessage = 37;

    /// <summary>
    ///  Writes the Replace Order Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replace Order Message.
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
        int replaceOrderAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replaceOrderAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replaceOrderAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout origUserRefNumWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        origUserRefNumWriterLayout.OrigUserRefNum = value.OrigUserRefNum;
        origUserRefNumWriterLayout.UserRefNum = value.UserRefNum;
        origUserRefNumWriterLayout.Quantity = value.Quantity;
        origUserRefNumWriterLayout.Price = value.Price;
        origUserRefNumWriterLayout.TimeInForce = value.TimeInForce;
        origUserRefNumWriterLayout.Display = value.Display;
        origUserRefNumWriterLayout.InterMarketSweepEligibility = value.InterMarketSweepEligibility;
        origUserRefNumWriterLayout.Clordid = value.Clordid;
        cursor.ReplaceOrderMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int replaceOrderAppendageIndex = 0; replaceOrderAppendageIndex < value.ReplaceOrderAppendage.Length; replaceOrderAppendageIndex++)
        {
            ref readonly var replaceOrderAppendageCurrent = ref value.ReplaceOrderAppendage[replaceOrderAppendageIndex];
            if (replaceOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replaceOrderAppendageWriterStart = cursor.Index;
            replaceOrderAppendageWriterCount++;
            int replaceOrderAppendageWriting = cursor.Index;
            var replaceOrderAppendageWriteValue = replaceOrderAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Write(bytes, ref cursor, replaceOrderAppendageWriting, in replaceOrderAppendageWriteValue);
        }
        if (replaceOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replaceOrderAppendageWriterStart = cursor.Index;
        if (replaceOrderAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replaceOrderAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderMessageWriter.ReplaceOrderAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderMessageAppendageLengthDependencyWriteDistanceFromReplaceOrderMessage, replaceOrderAppendageWriterStart, replaceOrderAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
