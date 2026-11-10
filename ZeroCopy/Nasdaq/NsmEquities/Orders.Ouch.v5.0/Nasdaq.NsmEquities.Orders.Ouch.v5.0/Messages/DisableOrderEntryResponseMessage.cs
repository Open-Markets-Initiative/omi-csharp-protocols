using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  This message acknowledges the receipt of the Disable Order Entry Request message. The data fields from the Disable Order Entry Request message are echoed back in this message.
/// </summary>
public static class DisableOrderEntryResponseMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm Firm;
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength AppendageLength;
    }

    /// <summary>
    ///  Reads the Disable Order Entry Response Message in place.
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
        ///  Gets the bytes of the Disable Order Entry Response Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureDisableOrderEntryResponseMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Disable Order Entry Response Message.
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
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        public ref readonly Firm Firm => ref this.layout.Firm;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Disable Order Entry Response Appendage
        /// </summary>
        public DisableOrderEntryResponseMessage.DisableOrderEntryResponseAppendageEntries DisableOrderEntryResponseAppendage
        {
            get
            {
                int disableOrderEntryResponseAppendageReaderPosition = Parser.Valid(Parser.DisableOrderEntryResponseMessageDisableOrderEntryResponseAppendageStart(this.bytes, this.start, this.end, this.start, out var disableOrderEntryResponseAppendageCursorLimit, out var disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End));
                return new(this.bytes, disableOrderEntryResponseAppendageReaderPosition, disableOrderEntryResponseAppendageCursorLimit, disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Disable Order Entry Response Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct DisableOrderEntryResponseAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End;

        internal DisableOrderEntryResponseAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End = disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly DisableOrderEntryResponseAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepDisableOrderEntryResponseMessageDisableOrderEntryResponseAppendage(this.bytes, this.position, this.limit, this.disableOrderEntryResponseAppendageCursorDisableOrderEntryResponseAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Disable Order Entry Response Message fields to write.
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
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm Firm { get; init; }

        /// <summary>
        ///  Disable Order Entry Response Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Value> DisableOrderEntryResponseAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Disable Order Entry Response Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int disableOrderEntryResponseMessageAppendageLengthDependencyWriteDistanceFromDisableOrderEntryResponseMessage = 16;

    /// <summary>
    ///  Writes the Disable Order Entry Response Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Disable Order Entry Response Message.
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
        int disableOrderEntryResponseAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int disableOrderEntryResponseAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int disableOrderEntryResponseAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout disableOrderEntryResponseMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        disableOrderEntryResponseMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        disableOrderEntryResponseMessageTimestampWriterLayout.UserRefNum = value.UserRefNum;
        disableOrderEntryResponseMessageTimestampWriterLayout.Firm = value.Firm;
        cursor.DisableOrderEntryResponseMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int disableOrderEntryResponseAppendageIndex = 0; disableOrderEntryResponseAppendageIndex < value.DisableOrderEntryResponseAppendage.Length; disableOrderEntryResponseAppendageIndex++)
        {
            ref readonly var disableOrderEntryResponseAppendageCurrent = ref value.DisableOrderEntryResponseAppendage[disableOrderEntryResponseAppendageIndex];
            if (disableOrderEntryResponseAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) disableOrderEntryResponseAppendageWriterStart = cursor.Index;
            disableOrderEntryResponseAppendageWriterCount++;
            int disableOrderEntryResponseAppendageWriting = cursor.Index;
            var disableOrderEntryResponseAppendageWriteValue = disableOrderEntryResponseAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Write(bytes, ref cursor, disableOrderEntryResponseAppendageWriting, in disableOrderEntryResponseAppendageWriteValue);
        }
        if (disableOrderEntryResponseAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) disableOrderEntryResponseAppendageWriterStart = cursor.Index;
        if (disableOrderEntryResponseAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) disableOrderEntryResponseAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.DisableOrderEntryResponseMessageWriter.DisableOrderEntryResponseAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + disableOrderEntryResponseMessageAppendageLengthDependencyWriteDistanceFromDisableOrderEntryResponseMessage, disableOrderEntryResponseAppendageWriterStart, disableOrderEntryResponseAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
