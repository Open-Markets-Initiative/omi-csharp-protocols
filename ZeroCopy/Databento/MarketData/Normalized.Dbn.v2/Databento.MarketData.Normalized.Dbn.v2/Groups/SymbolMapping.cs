using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The mappings of one raw symbol
/// </summary>
public static class SymbolMapping
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
            if (global::Databento.MarketData.Normalized.Dbn.v2.SymbolMapping.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The symbol assigned by publisher
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.RawSymbolIn RawSymbolIn;
        /// <summary>
        ///  The number of mapping intervals of the symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.IntervalCount IntervalCount;
    }

    /// <summary>
    ///  Reads the Symbol Mapping in place.
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
        ///  Gets the bytes of the Symbol Mapping.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureSymbolMapping(this.bytes, this.start, this.end, this.start))];

        /// <summary>
        ///  Gets the fixed fields at the start of the Symbol Mapping.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  The symbol assigned by publisher
        /// </summary>
        public ref readonly RawSymbolIn RawSymbolIn => ref this.layout.RawSymbolIn;

        /// <summary>
        ///  The number of mapping intervals of the symbol
        /// </summary>
        public ref readonly IntervalCount IntervalCount => ref this.layout.IntervalCount;

        /// <summary>
        ///  The resolution of a symbol for a date range
        /// </summary>
        public SymbolMapping.MappingIntervalEntries MappingInterval
        {
            get
            {
                int mappingIntervalReaderPosition = Parser.Valid(Parser.SymbolMappingMappingIntervalStart(this.bytes, this.start, this.end, this.start, out var mappingIntervalCursorLimit, out var mappingIntervalCursorMappingIntervalCountRule0Entries));
                return new(this.bytes, mappingIntervalReaderPosition, mappingIntervalCursorLimit, mappingIntervalCursorMappingIntervalCountRule0Entries);
            }
        }
    }

    /// <summary>
    ///  Steps through the Mapping Interval entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct MappingIntervalEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount mappingIntervalCursorMappingIntervalCountRule0Entries;

        internal MappingIntervalEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount mappingIntervalCursorMappingIntervalCountRule0Entries)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = mappingIntervalCursorMappingIntervalCountRule0Entries;
            this.mappingIntervalCursorMappingIntervalCountRule0Entries = mappingIntervalCursorMappingIntervalCountRule0Entries;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly MappingIntervalEntries GetEnumerator() => this;

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
            int end = Parser.StepSymbolMappingMappingInterval(this.bytes, this.position, this.limit, ref this.mappingIntervalCursorMappingIntervalCountRule0Entries);
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
        public readonly ref readonly MappingInterval.Layout Current
        {
            get
            {
                Entry();
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MappingInterval.Layout>(this.bytes[this.currentStart..this.currentEnd]);
            }
        }
    }

    /// <summary>
    ///  The Symbol Mapping fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The symbol assigned by publisher
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.RawSymbolIn RawSymbolIn { get; init; }

        /// <summary>
        ///  The resolution of a symbol for a date range
        /// </summary>
        public global::System.ReadOnlySpan<global::Databento.MarketData.Normalized.Dbn.v2.MappingInterval.Value> MappingInterval { get; init; }

        /// <summary>
        ///  Creates the value of the Symbol Mapping, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeRawSymbolIn + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeIntervalCount;
    private const int intervalCountDependencyWriteDistanceFromSymbolMapping = 71;

    /// <summary>
    ///  Writes the Symbol Mapping to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Symbol Mapping.
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
        int mappingIntervalWriterStart = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent;
        int mappingIntervalWriterCount = global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterDestination);
        ref Layout rawSymbolInWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        rawSymbolInWriterLayout.RawSymbolIn = value.RawSymbolIn;
        cursor.SymbolMapping_IntervalCount_Used = false;
        cursor.Index += LayoutSize;
        for (int mappingIntervalIndex = 0; mappingIntervalIndex < value.MappingInterval.Length; mappingIntervalIndex++)
        {
            ref readonly var mappingIntervalCurrent = ref value.MappingInterval[mappingIntervalIndex];
            if (mappingIntervalWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) mappingIntervalWriterStart = cursor.Index;
            mappingIntervalWriterCount++;
            int mappingIntervalWriting = cursor.Index;
            var mappingIntervalWriteValue = mappingIntervalCurrent;
            global::Databento.MarketData.Normalized.Dbn.v2.MappingInterval.Write(bytes, ref cursor, mappingIntervalWriting, in mappingIntervalWriteValue);
        }
        if (mappingIntervalWriterStart == global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent) mappingIntervalWriterStart = cursor.Index;
        global::Databento.MarketData.Normalized.Dbn.v2.Parser.SymbolMappingWriter.MappingIntervalCountRule0(bytes, ref cursor, start < global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Absent : start + intervalCountDependencyWriteDistanceFromSymbolMapping, mappingIntervalWriterStart, mappingIntervalWriterCount);
        return bytes[start..cursor.Index];
    }
}
