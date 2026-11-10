using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  This message acknowledges the receipt and acceptance of a valid Replace Order Message. The data fields from the Replace Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields. You will receive one and only one of these two for each replacement.
/// </summary>
public static class ReplacedMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrigUserRefNum OrigUserRefNum;
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
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderReferenceNumber OrderReferenceNumber;
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
        ///  Order State
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderStateField OrderState;
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
    ///  Reads the Replaced Message in place.
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
        ///  Gets the bytes of the Replaced Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureReplacedMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Replaced Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public ref readonly Timestamp Timestamp => ref this.layout.Timestamp;

        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public ref readonly OrigUserRefNum OrigUserRefNum => ref this.layout.OrigUserRefNum;

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
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public ref readonly OrderReferenceNumber OrderReferenceNumber => ref this.layout.OrderReferenceNumber;

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
        ///  Order State
        /// </summary>
        public ref readonly OrderStateField OrderState => ref this.layout.OrderState;

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public ref readonly Clordid Clordid => ref this.layout.Clordid;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Replaced Message Appendage
        /// </summary>
        public ReplacedMessage.ReplacedMessageAppendageEntries ReplacedMessageAppendage
        {
            get
            {
                int replacedMessageAppendageReaderPosition = Parser.Valid(Parser.ReplacedMessageReplacedMessageAppendageStart(this.bytes, this.start, this.end, this.start, out var replacedMessageAppendageCursorLimit, out var replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End));
                return new(this.bytes, replacedMessageAppendageReaderPosition, replacedMessageAppendageCursorLimit, replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Replaced Message Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct ReplacedMessageAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End;

        internal ReplacedMessageAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End = replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly ReplacedMessageAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepReplacedMessageReplacedMessageAppendage(this.bytes, this.position, this.limit, this.replacedMessageAppendageCursorReplacedMessageAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Replaced Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Expressed as nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  This must be filled out with the Order UserRefNum sent on the Enter Order Message or last Replace Order Message.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrigUserRefNum OrigUserRefNum { get; init; }

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
        ///  The day-unique Order Reference Number assigned by NASDAQ to this order
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderReferenceNumber OrderReferenceNumber { get; init; }

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
        ///  Order State
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderStateField OrderState { get; init; }

        /// <summary>
        ///  Customer order identifier. ClOrdID will not be checked for day-uniqueness for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Clordid Clordid { get; init; }

        /// <summary>
        ///  Replaced Message Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Value> ReplacedMessageAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Replaced Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrigUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeQuantity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSymbol + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePrice + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimeInForce + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplay + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCapacity + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeInterMarketSweepEligibility + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCrossType + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderState + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeClordid + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int replacedMessageAppendageLengthDependencyWriteDistanceFromReplacedMessage = 65;

    /// <summary>
    ///  Writes the Replaced Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replaced Message.
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
        int replacedMessageAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replacedMessageAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replacedMessageAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout replacedMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        replacedMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        replacedMessageTimestampWriterLayout.OrigUserRefNum = value.OrigUserRefNum;
        replacedMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        replacedMessageTimestampWriterLayout.Side = value.Side;
        replacedMessageTimestampWriterLayout.Quantity = value.Quantity;
        replacedMessageTimestampWriterLayout.Symbol = value.Symbol;
        replacedMessageTimestampWriterLayout.Price = value.Price;
        replacedMessageTimestampWriterLayout.TimeInForce = value.TimeInForce;
        replacedMessageTimestampWriterLayout.Display = value.Display;
        replacedMessageTimestampWriterLayout.OrderReferenceNumber = value.OrderReferenceNumber;
        replacedMessageTimestampWriterLayout.Capacity = value.Capacity;
        replacedMessageTimestampWriterLayout.InterMarketSweepEligibility = value.InterMarketSweepEligibility;
        replacedMessageTimestampWriterLayout.CrossType = value.CrossType;
        replacedMessageTimestampWriterLayout.OrderState = value.OrderState;
        replacedMessageTimestampWriterLayout.Clordid = value.Clordid;
        cursor.ReplacedMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int replacedMessageAppendageIndex = 0; replacedMessageAppendageIndex < value.ReplacedMessageAppendage.Length; replacedMessageAppendageIndex++)
        {
            ref readonly var replacedMessageAppendageCurrent = ref value.ReplacedMessageAppendage[replacedMessageAppendageIndex];
            if (replacedMessageAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replacedMessageAppendageWriterStart = cursor.Index;
            replacedMessageAppendageWriterCount++;
            int replacedMessageAppendageWriting = cursor.Index;
            var replacedMessageAppendageWriteValue = replacedMessageAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Write(bytes, ref cursor, replacedMessageAppendageWriting, in replacedMessageAppendageWriteValue);
        }
        if (replacedMessageAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replacedMessageAppendageWriterStart = cursor.Index;
        if (replacedMessageAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replacedMessageAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageWriter.ReplacedMessageAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageAppendageLengthDependencyWriteDistanceFromReplacedMessage, replacedMessageAppendageWriterStart, replacedMessageAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
