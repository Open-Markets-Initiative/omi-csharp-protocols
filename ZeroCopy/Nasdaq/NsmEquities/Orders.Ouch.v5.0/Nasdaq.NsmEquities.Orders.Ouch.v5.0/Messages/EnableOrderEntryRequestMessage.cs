using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Enable Order Entry Request allows firms the ability to unblock submission of new orders on a particular account.
/// </summary>
public static class EnableOrderEntryRequestMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

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
    ///  Reads the Enable Order Entry Request Message in place.
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
        ///  Gets the bytes of the Enable Order Entry Request Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureEnableOrderEntryRequestMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Enable Order Entry Request Message.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

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
        ///  Enable Order Entry Request Appendage
        /// </summary>
        public EnableOrderEntryRequestMessage.EnableOrderEntryRequestAppendageEntries EnableOrderEntryRequestAppendage
        {
            get
            {
                int enableOrderEntryRequestAppendageReaderPosition = Parser.Valid(Parser.EnableOrderEntryRequestMessageEnableOrderEntryRequestAppendageStart(this.bytes, this.start, this.end, this.start, out var enableOrderEntryRequestAppendageCursorLimit, out var enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End));
                return new(this.bytes, enableOrderEntryRequestAppendageReaderPosition, enableOrderEntryRequestAppendageCursorLimit, enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Enable Order Entry Request Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct EnableOrderEntryRequestAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End;

        internal EnableOrderEntryRequestAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End = enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly EnableOrderEntryRequestAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepEnableOrderEntryRequestMessageEnableOrderEntryRequestAppendage(this.bytes, this.position, this.limit, this.enableOrderEntryRequestAppendageCursorEnableOrderEntryRequestAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Enable Order Entry Request Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  UserRefNum must be day-unique and strictly increasing for each OUCH account.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefNum UserRefNum { get; init; }

        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm Firm { get; init; }

        /// <summary>
        ///  Enable Order Entry Request Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Value> EnableOrderEntryRequestAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Enable Order Entry Request Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int enableOrderEntryRequestMessageAppendageLengthDependencyWriteDistanceFromEnableOrderEntryRequestMessage = 8;

    /// <summary>
    ///  Writes the Enable Order Entry Request Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Enable Order Entry Request Message.
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
        int enableOrderEntryRequestAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enableOrderEntryRequestAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enableOrderEntryRequestAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout enableOrderEntryRequestMessageUserRefNumWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        enableOrderEntryRequestMessageUserRefNumWriterLayout.UserRefNum = value.UserRefNum;
        enableOrderEntryRequestMessageUserRefNumWriterLayout.Firm = value.Firm;
        cursor.EnableOrderEntryRequestMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int enableOrderEntryRequestAppendageIndex = 0; enableOrderEntryRequestAppendageIndex < value.EnableOrderEntryRequestAppendage.Length; enableOrderEntryRequestAppendageIndex++)
        {
            ref readonly var enableOrderEntryRequestAppendageCurrent = ref value.EnableOrderEntryRequestAppendage[enableOrderEntryRequestAppendageIndex];
            if (enableOrderEntryRequestAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enableOrderEntryRequestAppendageWriterStart = cursor.Index;
            enableOrderEntryRequestAppendageWriterCount++;
            int enableOrderEntryRequestAppendageWriting = cursor.Index;
            var enableOrderEntryRequestAppendageWriteValue = enableOrderEntryRequestAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Write(bytes, ref cursor, enableOrderEntryRequestAppendageWriting, in enableOrderEntryRequestAppendageWriteValue);
        }
        if (enableOrderEntryRequestAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enableOrderEntryRequestAppendageWriterStart = cursor.Index;
        if (enableOrderEntryRequestAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enableOrderEntryRequestAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnableOrderEntryRequestMessageWriter.EnableOrderEntryRequestAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enableOrderEntryRequestMessageAppendageLengthDependencyWriteDistanceFromEnableOrderEntryRequestMessage, enableOrderEntryRequestAppendageWriterStart, enableOrderEntryRequestAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
