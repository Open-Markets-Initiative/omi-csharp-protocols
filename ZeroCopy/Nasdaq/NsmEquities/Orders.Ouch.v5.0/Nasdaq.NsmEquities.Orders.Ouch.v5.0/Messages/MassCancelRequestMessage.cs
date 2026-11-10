using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The Mass Cancel Request message allows firms to initiate the cancellation of one or more orders that are currently booked at the exchange if they match the criteria specified in the request. If no optional criteria are specified then all orders for the specified firm on the account will be cancelled. Optional criteria can be specified in the Appendage field.
/// </summary>
public static class MassCancelRequestMessage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  Stock Symbol
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Symbol Symbol;
        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AppendageLength AppendageLength;
    }

    /// <summary>
    ///  Reads the Mass Cancel Request Message in place.
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
        ///  Gets the bytes of the Mass Cancel Request Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMassCancelRequestMessage(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Mass Cancel Request Message.
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
        ///  Stock Symbol
        /// </summary>
        public ref readonly Symbol Symbol => ref this.layout.Symbol;

        /// <summary>
        ///  The length of the remaining Optional Appendage field.
        /// </summary>
        public ref readonly AppendageLength AppendageLength => ref this.layout.AppendageLength;

        /// <summary>
        ///  Mass Cancel Request Appendage
        /// </summary>
        public MassCancelRequestMessage.MassCancelRequestAppendageEntries MassCancelRequestAppendage
        {
            get
            {
                int massCancelRequestAppendageReaderPosition = Parser.Valid(Parser.MassCancelRequestMessageMassCancelRequestAppendageStart(this.bytes, this.start, this.end, this.start, out var massCancelRequestAppendageCursorLimit, out var massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End));
                return new(this.bytes, massCancelRequestAppendageReaderPosition, massCancelRequestAppendageCursorLimit, massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End);
            }
        }
    }

    /// <summary>
    ///  Steps through the Mass Cancel Request Appendage entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct MassCancelRequestAppendageEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End;

        internal MassCancelRequestAppendageEntries(ReadOnlySpan<byte> bytes, int start, int limit, int massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End = massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly MassCancelRequestAppendageEntries GetEnumerator() => this;

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
            int end = Parser.StepMassCancelRequestMessageMassCancelRequestAppendage(this.bytes, this.position, this.limit, this.massCancelRequestAppendageCursorMassCancelRequestAppendageCountRule0End);
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
        public readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Mass Cancel Request Message fields to write.
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
        ///  Stock Symbol
        /// </summary>
        public required global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Symbol Symbol { get; init; }

        /// <summary>
        ///  Mass Cancel Request Appendage
        /// </summary>
        public global::System.ReadOnlySpan<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Value> MassCancelRequestAppendage { get; init; }

        /// <summary>
        ///  Creates the value of the Mass Cancel Request Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefNum + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSymbol + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAppendageLength;
    private const int massCancelRequestMessageAppendageLengthDependencyWriteDistanceFromMassCancelRequestMessage = 16;

    /// <summary>
    ///  Writes the Mass Cancel Request Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Mass Cancel Request Message.
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
        int massCancelRequestAppendageWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int massCancelRequestAppendageWriterEnd = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int massCancelRequestAppendageWriterCount = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        ref Layout massCancelRequestMessageUserRefNumWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        massCancelRequestMessageUserRefNumWriterLayout.UserRefNum = value.UserRefNum;
        massCancelRequestMessageUserRefNumWriterLayout.Firm = value.Firm;
        massCancelRequestMessageUserRefNumWriterLayout.Symbol = value.Symbol;
        cursor.MassCancelRequestMessage_AppendageLength_Used = false;
        cursor.Index += LayoutSize;
        for (int massCancelRequestAppendageIndex = 0; massCancelRequestAppendageIndex < value.MassCancelRequestAppendage.Length; massCancelRequestAppendageIndex++)
        {
            ref readonly var massCancelRequestAppendageCurrent = ref value.MassCancelRequestAppendage[massCancelRequestAppendageIndex];
            if (massCancelRequestAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) massCancelRequestAppendageWriterStart = cursor.Index;
            massCancelRequestAppendageWriterCount++;
            int massCancelRequestAppendageWriting = cursor.Index;
            var massCancelRequestAppendageWriteValue = massCancelRequestAppendageCurrent;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Write(bytes, ref cursor, massCancelRequestAppendageWriting, in massCancelRequestAppendageWriteValue);
        }
        if (massCancelRequestAppendageWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) massCancelRequestAppendageWriterStart = cursor.Index;
        if (massCancelRequestAppendageWriterEnd == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) massCancelRequestAppendageWriterEnd = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestMessageWriter.MassCancelRequestAppendageCountRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestMessageAppendageLengthDependencyWriteDistanceFromMassCancelRequestMessage, massCancelRequestAppendageWriterStart, massCancelRequestAppendageWriterEnd);
        return bytes[start..cursor.Index];
    }
}
