using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Enter Order Message
/// </summary>
public static class EnterOrderMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum;
        /// <summary>
        ///  Side
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side;
        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity;
        /// <summary>
        ///  Stock Symbol
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Symbol Symbol;
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
        ///  Capacity
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CapacityField Capacity;
        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.InterMarketSweepEligibilityField InterMarketSweepEligibility;
        /// <summary>
        ///  CrossType
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CrossTypeField CrossType;
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
    ///  Reads the Enter Order Message in place.
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
        ///  Gets the bytes of the Enter Order Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureEnterOrderMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Enter Order Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public ref readonly UserRefNum UserRefNum => ref this.layout.UserRefNum;

        /// <summary>
        ///  Side
        /// </summary>
        public ref readonly SideField Side => ref this.layout.Side;

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public ref readonly Quantity Quantity => ref this.layout.Quantity;

        /// <summary>
        ///  Stock Symbol
        /// </summary>
        public ref readonly Symbol Symbol => ref this.layout.Symbol;

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
        ///  Capacity
        /// </summary>
        public ref readonly CapacityField Capacity => ref this.layout.Capacity;

        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public ref readonly InterMarketSweepEligibilityField InterMarketSweepEligibility => ref this.layout.InterMarketSweepEligibility;

        /// <summary>
        ///  CrossType
        /// </summary>
        public ref readonly CrossTypeField CrossType => ref this.layout.CrossType;

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public ref readonly Clordid Clordid => ref this.layout.Clordid;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Enter Order Appendage
        /// </summary>
        public EnterOrderMessage.EnterOrderAppendageEntries EnterOrderAppendage
        {
            get
            {
                int enterOrderAppendageReaderPosition = Parser.Valid(Parser.EnterOrderMessageEnterOrderAppendageStart(this.bytes, this.start, this.end, this.start, out var enterOrderAppendageCursorLimit, out var enterOrderAppendageCursorEnterOrderAppendageCountRule0End));
                return new(this.bytes, enterOrderAppendageReaderPosition, enterOrderAppendageCursorLimit, enterOrderAppendageCursorEnterOrderAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Enter Order Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct EnterOrderAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int enterOrderAppendageCursorEnterOrderAppendageCountRule0End;

        internal EnterOrderAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int enterOrderAppendageCursorEnterOrderAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.enterOrderAppendageCursorEnterOrderAppendageCountRule0End = enterOrderAppendageCursorEnterOrderAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly EnterOrderAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepEnterOrderMessageEnterOrderAppendage(this.bytes, this.position, this.limit, this.enterOrderAppendageCursorEnterOrderAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Enter Order Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum { get; init; }

        /// <summary>
        ///  Side
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side { get; init; }

        /// <summary>
        ///  Total number of shares. Must be greater than zero and less than 1,000,000.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Quantity Quantity { get; init; }

        /// <summary>
        ///  Stock Symbol
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Symbol Symbol { get; init; }

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
        ///  Capacity
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CapacityField Capacity { get; init; }

        /// <summary>
        ///  InterMarket Sweep Eligibility
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.InterMarketSweepEligibilityField InterMarketSweepEligibility { get; init; }

        /// <summary>
        ///  CrossType
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CrossTypeField CrossType { get; init; }

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid { get; init; }

        /// <summary>
        ///  Enter Order Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Value> EnterOrderAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Enter Order Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSymbol + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimeInForce + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplay + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCapacity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeInterMarketSweepEligibility + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCrossType + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeClordid + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int enterOrderMessageAppendageLengthDependencyWriteDistanceFromEnterOrderMessage = 44;

    /// <summary>
    ///  Writes the Enter Order Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Enter Order Message.
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
        int enterOrderAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enterOrderAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enterOrderAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout enterOrderMessageUserRefNumWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        enterOrderMessageUserRefNumWriterLayout.UserRefNum = value.UserRefNum;
        enterOrderMessageUserRefNumWriterLayout.Side = value.Side;
        enterOrderMessageUserRefNumWriterLayout.Quantity = value.Quantity;
        enterOrderMessageUserRefNumWriterLayout.Symbol = value.Symbol;
        enterOrderMessageUserRefNumWriterLayout.Price = value.Price;
        enterOrderMessageUserRefNumWriterLayout.TimeInForce = value.TimeInForce;
        enterOrderMessageUserRefNumWriterLayout.Display = value.Display;
        enterOrderMessageUserRefNumWriterLayout.Capacity = value.Capacity;
        enterOrderMessageUserRefNumWriterLayout.InterMarketSweepEligibility = value.InterMarketSweepEligibility;
        enterOrderMessageUserRefNumWriterLayout.CrossType = value.CrossType;
        enterOrderMessageUserRefNumWriterLayout.Clordid = value.Clordid;
        cursor.EnterOrderMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int enterOrderAppendageIndex = 0; enterOrderAppendageIndex < value.EnterOrderAppendage.Length; enterOrderAppendageIndex++)
        {
            ref readonly var enterOrderAppendageCurrent = ref value.EnterOrderAppendage[enterOrderAppendageIndex];
            if (enterOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enterOrderAppendageWriterStart = cursor.Index;
            enterOrderAppendageWriterCount++;
            int enterOrderAppendageWriting = cursor.Index;
            var enterOrderAppendageWriteValue = enterOrderAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Write(bytes, ref cursor, enterOrderAppendageWriting, in enterOrderAppendageWriteValue);
        }
        if (enterOrderAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enterOrderAppendageWriterStart = cursor.Index;
        if (enterOrderAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enterOrderAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderMessageWriter.EnterOrderAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderMessageAppendageLengthDependencyWriteDistanceFromEnterOrderMessage, enterOrderAppendageWriterStart, enterOrderAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
