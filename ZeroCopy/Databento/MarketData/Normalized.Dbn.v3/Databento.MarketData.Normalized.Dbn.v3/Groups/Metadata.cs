using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  DBN metadata header, once at the start of a file or stream
/// </summary>
public static class Metadata
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
            if (global::Databento.MarketData.Normalized.Dbn.v3.Metadata.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The DBN magic string
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Magic Magic;
        /// <summary>
        ///  The DBN schema version number
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Version Version;
        /// <summary>
        ///  The length of the metadata after this field, in bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MetadataLength MetadataLength;
        /// <summary>
        ///  The dataset code
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Dataset Dataset;
        /// <summary>
        ///  The data record schema; no value for a file of mixed schemas
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SchemaField Schema;
        /// <summary>
        ///  The start time of query range in UNIX epoch nanoseconds
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Start Start;
        /// <summary>
        ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.End End;
        /// <summary>
        ///  The maximum number of records to return; no value when unlimited
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Limit Limit;
        /// <summary>
        ///  The input symbology type
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StypeInField StypeIn;
        /// <summary>
        ///  The output symbology type
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StypeOutField StypeOut;
        /// <summary>
        ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsOutField TsOut;
        /// <summary>
        ///  The number of bytes in fixed-length symbol strings, including a null terminator byte; 71 in version 3
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SymbolCstrLen SymbolCstrLen;
        /// <summary>
        ///  Reserved, 53 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MetadataReserved MetadataReserved;
        /// <summary>
        ///  The length of the schema definition; always 0
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SchemaDefinitionLength SchemaDefinitionLength;
    }

    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SymbolsCountLayout
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
        public static ref SymbolsCountLayout Over(global::System.Span<byte> bytes)
        {
            if (global::Databento.MarketData.Normalized.Dbn.v3.Metadata.SymbolsCountLayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SymbolsCountLayout>(bytes);
        }

        /// <summary>
        ///  The number of query input symbols
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SymbolsCount SymbolsCount;
    }

    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PartialCountLayout
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
        public static ref PartialCountLayout Over(global::System.Span<byte> bytes)
        {
            if (global::Databento.MarketData.Normalized.Dbn.v3.Metadata.PartialCountLayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PartialCountLayout>(bytes);
        }

        /// <summary>
        ///  The number of symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.PartialCount PartialCount;
    }

    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NotFoundCountLayout
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
        public static ref NotFoundCountLayout Over(global::System.Span<byte> bytes)
        {
            if (global::Databento.MarketData.Normalized.Dbn.v3.Metadata.NotFoundCountLayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NotFoundCountLayout>(bytes);
        }

        /// <summary>
        ///  The number of symbols that did not resolve for any day in the query time range
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.NotFoundCount NotFoundCount;
    }

    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MappingsCountLayout
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
        public static ref MappingsCountLayout Over(global::System.Span<byte> bytes)
        {
            if (global::Databento.MarketData.Normalized.Dbn.v3.Metadata.MappingsCountLayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MappingsCountLayout>(bytes);
        }

        /// <summary>
        ///  The number of symbol mappings
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.MappingsCount MappingsCount;
    }

    /// <summary>
    ///  Reads the Metadata in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);
        private ref readonly SymbolsCountLayout symbolsCountReaderRunLayout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SymbolsCountLayout>(this.bytes[Parser.Valid(Parser.MetadataSymbolsCountStart(this.bytes, this.start, this.end, this.start))..this.end]);
        private ref readonly PartialCountLayout partialCountReaderRunLayout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PartialCountLayout>(this.bytes[Parser.Valid(Parser.MetadataPartialCountStart(this.bytes, this.start, this.end, this.start))..this.end]);
        private ref readonly NotFoundCountLayout notFoundCountReaderRunLayout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NotFoundCountLayout>(this.bytes[Parser.Valid(Parser.MetadataNotFoundCountStart(this.bytes, this.start, this.end, this.start))..this.end]);
        private ref readonly MappingsCountLayout mappingsCountReaderRunLayout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MappingsCountLayout>(this.bytes[Parser.Valid(Parser.MetadataMappingsCountStart(this.bytes, this.start, this.end, this.start))..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
        }

        /// <summary>
        ///  Gets the bytes of the Metadata.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Metadata.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  The DBN magic string
        /// </summary>
        public ref readonly Magic Magic => ref this.layout.Magic;

        /// <summary>
        ///  The DBN schema version number
        /// </summary>
        public ref readonly Version Version => ref this.layout.Version;

        /// <summary>
        ///  The length of the metadata after this field, in bytes
        /// </summary>
        public ref readonly MetadataLength MetadataLength => ref this.layout.MetadataLength;

        /// <summary>
        ///  The dataset code
        /// </summary>
        public ref readonly Dataset Dataset => ref this.layout.Dataset;

        /// <summary>
        ///  The data record schema; no value for a file of mixed schemas
        /// </summary>
        public ref readonly SchemaField Schema => ref this.layout.Schema;

        /// <summary>
        ///  The start time of query range in UNIX epoch nanoseconds
        /// </summary>
        public ref readonly Start Start => ref this.layout.Start;

        /// <summary>
        ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
        /// </summary>
        public ref readonly End End => ref this.layout.End;

        /// <summary>
        ///  The maximum number of records to return; no value when unlimited
        /// </summary>
        public ref readonly Limit Limit => ref this.layout.Limit;

        /// <summary>
        ///  The input symbology type
        /// </summary>
        public ref readonly StypeInField StypeIn => ref this.layout.StypeIn;

        /// <summary>
        ///  The output symbology type
        /// </summary>
        public ref readonly StypeOutField StypeOut => ref this.layout.StypeOut;

        /// <summary>
        ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
        /// </summary>
        public ref readonly TsOutField TsOut => ref this.layout.TsOut;

        /// <summary>
        ///  The number of bytes in fixed-length symbol strings, including a null terminator byte; 71 in version 3
        /// </summary>
        public ref readonly SymbolCstrLen SymbolCstrLen => ref this.layout.SymbolCstrLen;

        /// <summary>
        ///  Reserved, 53 bytes
        /// </summary>
        public ref readonly MetadataReserved MetadataReserved => ref this.layout.MetadataReserved;

        /// <summary>
        ///  The length of the schema definition; always 0
        /// </summary>
        public ref readonly SchemaDefinitionLength SchemaDefinitionLength => ref this.layout.SchemaDefinitionLength;

        /// <summary>
        ///  The schema definition; always empty
        /// </summary>
        public ReadOnlySpan<byte> SchemaDefinition
        {
            get
            {
                int schemaDefinitionReaderPosition = Parser.Valid(Parser.MetadataSchemaDefinitionStart(this.bytes, this.start, this.end, this.start, out var schemaDefinitionLengthValue));
                int schemaDefinitionReaderEnd = Parser.Valid(Parser.MetadataSchemaDefinitionEnd(this.bytes, schemaDefinitionReaderPosition, this.end, this.start, schemaDefinitionLengthValue));
                return this.bytes[schemaDefinitionReaderPosition..schemaDefinitionReaderEnd];
            }
        }

        /// <summary>
        ///  The number of query input symbols
        /// </summary>
        public ref readonly SymbolsCount SymbolsCount => ref this.symbolsCountReaderRunLayout.SymbolsCount;

        /// <summary>
        ///  One of the query input symbols
        /// </summary>
        public Metadata.SymbolEntries Symbol
        {
            get
            {
                int symbolReaderPosition = Parser.Valid(Parser.MetadataSymbolStart(this.bytes, this.start, this.end, this.start, out var symbolReaderCount));
                return new(this.bytes, symbolReaderPosition, symbolReaderCount);
            }
        }

        /// <summary>
        ///  The number of symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public ref readonly PartialCount PartialCount => ref this.partialCountReaderRunLayout.PartialCount;

        /// <summary>
        ///  One of the symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public Metadata.PartialSymbolEntries PartialSymbol
        {
            get
            {
                int partialSymbolReaderPosition = Parser.Valid(Parser.MetadataPartialSymbolStart(this.bytes, this.start, this.end, this.start, out var partialSymbolReaderCount));
                return new(this.bytes, partialSymbolReaderPosition, partialSymbolReaderCount);
            }
        }

        /// <summary>
        ///  The number of symbols that did not resolve for any day in the query time range
        /// </summary>
        public ref readonly NotFoundCount NotFoundCount => ref this.notFoundCountReaderRunLayout.NotFoundCount;

        /// <summary>
        ///  One of the symbols that did not resolve for any day in the query time range
        /// </summary>
        public Metadata.NotFoundSymbolEntries NotFoundSymbol
        {
            get
            {
                int notFoundSymbolReaderPosition = Parser.Valid(Parser.MetadataNotFoundSymbolStart(this.bytes, this.start, this.end, this.start, out var notFoundSymbolReaderCount));
                return new(this.bytes, notFoundSymbolReaderPosition, notFoundSymbolReaderCount);
            }
        }

        /// <summary>
        ///  The number of symbol mappings
        /// </summary>
        public ref readonly MappingsCount MappingsCount => ref this.mappingsCountReaderRunLayout.MappingsCount;

        /// <summary>
        ///  The mappings of one raw symbol
        /// </summary>
        public Metadata.SymbolMappingEntries SymbolMapping
        {
            get
            {
                int symbolMappingReaderPosition = Parser.Valid(Parser.MetadataSymbolMappingStart(this.bytes, this.start, this.end, this.start, out var symbolMappingCursorLimit, out var symbolMappingCursorSymbolMappingCountRule0Entries));
                return new(this.bytes, symbolMappingReaderPosition, symbolMappingCursorLimit, symbolMappingCursorSymbolMappingCountRule0Entries);
            }
        }

        /// <summary>
        ///  Padding to an 8 byte boundary
        /// </summary>
        public ReadOnlySpan<byte> MetadataPadding
        {
            get
            {
                int metadataPaddingReaderPosition = Parser.Valid(Parser.MetadataMetadataPaddingStart(this.bytes, this.start, this.end, this.start, out var metadataLimit));
                int metadataPaddingReaderEnd = Parser.Valid(Parser.MetadataMetadataPaddingEnd(this.bytes, metadataPaddingReaderPosition, this.end, this.start, metadataLimit));
                return this.bytes[metadataPaddingReaderPosition..metadataPaddingReaderEnd];
            }
        }
    }

    /// <summary>
    ///  Steps through the Symbol entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own.
    /// </remarks>
    public ref struct SymbolEntries
    {
        private const int FirstEntry = 1;
        private readonly ReadOnlySpan<byte> source;
        private readonly int start;
        private readonly int count;
        private int index;

        internal SymbolEntries(ReadOnlySpan<byte> bytes, int start, int count)
        {
            this.source = bytes;
            this.start = start;
            this.count = count;
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly SymbolEntries GetEnumerator() => this;

        /// <summary>
        ///  Moves to the next entry.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another entry; <see langword="false"/> after the last entry.
        /// </returns>
        public bool MoveNext()
        {
            if (this.index > this.count) return false;
            this.index++;
            return this.index <= this.count;
        }
        /// <summary>
        ///  Gets the number of entries.
        /// </summary>
        public readonly long Count => this.count;

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index > this.count ? this.count : this.index;

        /// <summary>
        ///  Gets the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly ref readonly Symbol Current
        {
            get
            {
                if (this.index == Parser.Position.NoEntries || this.index > this.count) throw new global::System.InvalidOperationException(Parser.NoEntry);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Symbol>(this.source[(this.start + (this.index - FirstEntry) * Parser.SizeSymbol)..]);
            }
        }
    }

    /// <summary>
    ///  Steps through the Partial Symbol entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own.
    /// </remarks>
    public ref struct PartialSymbolEntries
    {
        private const int FirstEntry = 1;
        private readonly ReadOnlySpan<byte> source;
        private readonly int start;
        private readonly int count;
        private int index;

        internal PartialSymbolEntries(ReadOnlySpan<byte> bytes, int start, int count)
        {
            this.source = bytes;
            this.start = start;
            this.count = count;
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly PartialSymbolEntries GetEnumerator() => this;

        /// <summary>
        ///  Moves to the next entry.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another entry; <see langword="false"/> after the last entry.
        /// </returns>
        public bool MoveNext()
        {
            if (this.index > this.count) return false;
            this.index++;
            return this.index <= this.count;
        }
        /// <summary>
        ///  Gets the number of entries.
        /// </summary>
        public readonly long Count => this.count;

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index > this.count ? this.count : this.index;

        /// <summary>
        ///  Gets the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly ref readonly PartialSymbol Current
        {
            get
            {
                if (this.index == Parser.Position.NoEntries || this.index > this.count) throw new global::System.InvalidOperationException(Parser.NoEntry);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PartialSymbol>(this.source[(this.start + (this.index - FirstEntry) * Parser.SizePartialSymbol)..]);
            }
        }
    }

    /// <summary>
    ///  Steps through the Not Found Symbol entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own.
    /// </remarks>
    public ref struct NotFoundSymbolEntries
    {
        private const int FirstEntry = 1;
        private readonly ReadOnlySpan<byte> source;
        private readonly int start;
        private readonly int count;
        private int index;

        internal NotFoundSymbolEntries(ReadOnlySpan<byte> bytes, int start, int count)
        {
            this.source = bytes;
            this.start = start;
            this.count = count;
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly NotFoundSymbolEntries GetEnumerator() => this;

        /// <summary>
        ///  Moves to the next entry.
        /// </summary>
        /// <returns>
        ///  <see langword="true"/> when there is another entry; <see langword="false"/> after the last entry.
        /// </returns>
        public bool MoveNext()
        {
            if (this.index > this.count) return false;
            this.index++;
            return this.index <= this.count;
        }
        /// <summary>
        ///  Gets the number of entries.
        /// </summary>
        public readonly long Count => this.count;

        /// <summary>
        ///  Gets the one-based number of the current entry, or zero before the first entry.
        /// </summary>
        public readonly long Index => this.index > this.count ? this.count : this.index;

        /// <summary>
        ///  Gets the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly ref readonly NotFoundSymbol Current
        {
            get
            {
                if (this.index == Parser.Position.NoEntries || this.index > this.count) throw new global::System.InvalidOperationException(Parser.NoEntry);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NotFoundSymbol>(this.source[(this.start + (this.index - FirstEntry) * Parser.SizeNotFoundSymbol)..]);
            }
        }
    }

    /// <summary>
    ///  Steps through the Symbol Mapping entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct SymbolMappingEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly Parser.RuleCount total;
        private Parser.RuleCount symbolMappingCursorSymbolMappingCountRule0Entries;

        internal SymbolMappingEntries(ReadOnlySpan<byte> bytes, int start, int limit, Parser.RuleCount symbolMappingCursorSymbolMappingCountRule0Entries)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.total = symbolMappingCursorSymbolMappingCountRule0Entries;
            this.symbolMappingCursorSymbolMappingCountRule0Entries = symbolMappingCursorSymbolMappingCountRule0Entries;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly SymbolMappingEntries GetEnumerator() => this;

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
            int end = Parser.StepMetadataSymbolMapping(this.bytes, this.position, this.limit, ref this.symbolMappingCursorSymbolMappingCountRule0Entries);
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
        ///  Gets the Reader of the current entry.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The entries have no current entry.
        /// </exception>
        public readonly global::Databento.MarketData.Normalized.Dbn.v3.SymbolMapping.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd);
            }
        }
    }

    /// <summary>
    ///  The Metadata fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  The DBN magic string
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Magic Magic { get; init; }

        /// <summary>
        ///  The DBN schema version number
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Version Version { get; init; }

        /// <summary>
        ///  The dataset code
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Dataset Dataset { get; init; }

        /// <summary>
        ///  The data record schema; no value for a file of mixed schemas
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SchemaField Schema { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.SchemaField.Absent;

        /// <summary>
        ///  The start time of query range in UNIX epoch nanoseconds
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Start Start { get; init; }

        /// <summary>
        ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.End End { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.End.Absent;

        /// <summary>
        ///  The maximum number of records to return; no value when unlimited
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Limit Limit { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Limit.Absent;

        /// <summary>
        ///  The input symbology type
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StypeInField StypeIn { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.StypeInField.Absent;

        /// <summary>
        ///  The output symbology type
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.StypeOutField StypeOut { get; init; }

        /// <summary>
        ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.TsOutField TsOut { get; init; }

        /// <summary>
        ///  The number of bytes in fixed-length symbol strings, including a null terminator byte; 71 in version 3
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SymbolCstrLen SymbolCstrLen { get; init; }

        /// <summary>
        ///  The schema definition; always empty
        /// </summary>
        public required global::System.ReadOnlySpan<byte> SchemaDefinition { get; init; }

        /// <summary>
        ///  One of the query input symbols
        /// </summary>
        public global::System.ReadOnlySpan<global::Databento.MarketData.Normalized.Dbn.v3.Symbol> Symbol { get; init; }

        /// <summary>
        ///  One of the symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public global::System.ReadOnlySpan<global::Databento.MarketData.Normalized.Dbn.v3.PartialSymbol> PartialSymbol { get; init; }

        /// <summary>
        ///  One of the symbols that did not resolve for any day in the query time range
        /// </summary>
        public global::System.ReadOnlySpan<global::Databento.MarketData.Normalized.Dbn.v3.NotFoundSymbol> NotFoundSymbol { get; init; }

        /// <summary>
        ///  The mappings of one raw symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Entries<global::Databento.MarketData.Normalized.Dbn.v3.SymbolMapping.Value> SymbolMapping { get; init; }

        /// <summary>
        ///  Padding to an 8 byte boundary
        /// </summary>
        public required global::System.ReadOnlySpan<byte> MetadataPadding { get; init; }

        /// <summary>
        ///  Creates the value of the Metadata, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMagic + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeVersion + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMetadataLength + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeDataset + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSchema + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStart + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeEnd + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeLimit + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStypeIn + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStypeOut + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsOut + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSymbolCstrLen + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMetadataReserved + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSchemaDefinitionLength;
    private const int SymbolsCountLayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSymbolsCount;
    private const int PartialCountLayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePartialCount;
    private const int NotFoundCountLayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeNotFoundCount;
    private const int MappingsCountLayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeMappingsCount;
    private const int symbolsCountDependencyWriteDistanceFromSymbolsCount = 0;
    private const int partialCountDependencyWriteDistanceFromPartialCount = 0;
    private const int notFoundCountDependencyWriteDistanceFromNotFoundCount = 0;
    private const int mappingsCountDependencyWriteDistanceFromMappingsCount = 0;
    private const int metadataLengthDependencyWriteDistanceFromMetadata = 4;
    private const int schemaDefinitionLengthDependencyWriteDistanceFromMetadata = 108;

    /// <summary>
    ///  Writes the Metadata to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Metadata.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int symbolsCountWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int symbolWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int symbolWriterCount = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoEntries;
        int partialCountWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int partialSymbolWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int partialSymbolWriterCount = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoEntries;
        int notFoundCountWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int notFoundSymbolWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int notFoundSymbolWriterCount = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoEntries;
        int mappingsCountWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int symbolMappingWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int symbolMappingWriterCount = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoEntries;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        ref Layout magicWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        magicWriterLayout.Magic = value.Magic;
        magicWriterLayout.Version = value.Version;
        magicWriterLayout.Dataset = value.Dataset;
        magicWriterLayout.Schema = value.Schema;
        magicWriterLayout.Start = value.Start;
        magicWriterLayout.End = value.End;
        magicWriterLayout.Limit = value.Limit;
        magicWriterLayout.StypeIn = value.StypeIn;
        magicWriterLayout.StypeOut = value.StypeOut;
        magicWriterLayout.TsOut = value.TsOut;
        magicWriterLayout.SymbolCstrLen = value.SymbolCstrLen;
        magicWriterLayout.MetadataReserved = default;
        cursor.Metadata_MetadataLength_Used = false;
        cursor.Metadata_SchemaDefinitionLength_Used = false;
        cursor.Index += LayoutSize;
        int schemaDefinitionWriterLength = value.SchemaDefinition.Length;
        if (schemaDefinitionWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        int schemaDefinitionWriterPosition = cursor.Index;
        value.SchemaDefinition.CopyTo(bytes[schemaDefinitionWriterPosition..]);
        cursor.Index += schemaDefinitionWriterLength;
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.SchemaDefinitionSizeRule0(bytes, ref cursor, start < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : start + schemaDefinitionLengthDependencyWriteDistanceFromMetadata, schemaDefinitionWriterPosition, schemaDefinitionWriterPosition + schemaDefinitionWriterLength);
        if (SymbolsCountLayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        int symbolsCountWriterPosition = cursor.Index;
        cursor.Metadata_SymbolsCount_Used = false;
        cursor.Index += SymbolsCountLayoutSize;
        for (int symbolIndex = 0; symbolIndex < value.Symbol.Length; symbolIndex++)
        {
            ref readonly var symbolCurrent = ref value.Symbol[symbolIndex];
            if (global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSymbol > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
            if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
            if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
            symbolWriterCount++;
            int symbolWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Databento.MarketData.Normalized.Dbn.v3.Symbol>(bytes[symbolWriterPosition..]) = symbolCurrent;
            cursor.Index += global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSymbol;
        }
        if (PartialCountLayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
        if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
        int partialCountWriterPosition = cursor.Index;
        cursor.Metadata_PartialCount_Used = false;
        cursor.Index += PartialCountLayoutSize;
        for (int partialSymbolIndex = 0; partialSymbolIndex < value.PartialSymbol.Length; partialSymbolIndex++)
        {
            ref readonly var partialSymbolCurrent = ref value.PartialSymbol[partialSymbolIndex];
            if (global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePartialSymbol > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
            if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
            if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
            if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
            if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
            partialSymbolWriterCount++;
            int partialSymbolWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Databento.MarketData.Normalized.Dbn.v3.PartialSymbol>(bytes[partialSymbolWriterPosition..]) = partialSymbolCurrent;
            cursor.Index += global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePartialSymbol;
        }
        if (NotFoundCountLayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
        if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
        if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
        if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
        int notFoundCountWriterPosition = cursor.Index;
        cursor.Metadata_NotFoundCount_Used = false;
        cursor.Index += NotFoundCountLayoutSize;
        for (int notFoundSymbolIndex = 0; notFoundSymbolIndex < value.NotFoundSymbol.Length; notFoundSymbolIndex++)
        {
            ref readonly var notFoundSymbolCurrent = ref value.NotFoundSymbol[notFoundSymbolIndex];
            if (global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeNotFoundSymbol > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
            if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
            if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
            if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
            if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
            if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
            if (notFoundSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundSymbolWriterStart = cursor.Index;
            notFoundSymbolWriterCount++;
            int notFoundSymbolWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Databento.MarketData.Normalized.Dbn.v3.NotFoundSymbol>(bytes[notFoundSymbolWriterPosition..]) = notFoundSymbolCurrent;
            cursor.Index += global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeNotFoundSymbol;
        }
        if (MappingsCountLayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
        if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
        if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
        if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
        if (notFoundSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundSymbolWriterStart = cursor.Index;
        if (mappingsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) mappingsCountWriterStart = cursor.Index;
        int mappingsCountWriterPosition = cursor.Index;
        cursor.Metadata_MappingsCount_Used = false;
        cursor.Index += MappingsCountLayoutSize;
        for (int symbolMappingIndex = 0; symbolMappingIndex < value.SymbolMapping.Count; symbolMappingIndex++)
        {
            var symbolMappingCurrent = value.SymbolMapping[symbolMappingIndex];
            if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
            if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
            if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
            if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
            if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
            if (notFoundSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundSymbolWriterStart = cursor.Index;
            if (mappingsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) mappingsCountWriterStart = cursor.Index;
            if (symbolMappingWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolMappingWriterStart = cursor.Index;
            symbolMappingWriterCount++;
            int symbolMappingWriting = cursor.Index;
            var symbolMappingWriteValue = symbolMappingCurrent;
            global::Databento.MarketData.Normalized.Dbn.v3.SymbolMapping.Write(bytes, ref cursor, symbolMappingWriting, in symbolMappingWriteValue);
        }
        int metadataPaddingWriterLength = value.MetadataPadding.Length;
        if (metadataPaddingWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
        if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
        if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
        if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
        if (notFoundSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundSymbolWriterStart = cursor.Index;
        if (mappingsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) mappingsCountWriterStart = cursor.Index;
        if (symbolMappingWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolMappingWriterStart = cursor.Index;
        int metadataPaddingWriterPosition = cursor.Index;
        value.MetadataPadding.CopyTo(bytes[metadataPaddingWriterPosition..]);
        cursor.Index += metadataPaddingWriterLength;
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.MetadataPaddingSizeRule0(bytes, ref cursor, metadataPaddingWriterPosition, cursor.Index, metadataPaddingWriterPosition + metadataPaddingWriterLength);
        if (symbolsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolsCountWriterStart = cursor.Index;
        if (symbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolWriterStart = cursor.Index;
        if (partialCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialCountWriterStart = cursor.Index;
        if (partialSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) partialSymbolWriterStart = cursor.Index;
        if (notFoundCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundCountWriterStart = cursor.Index;
        if (notFoundSymbolWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) notFoundSymbolWriterStart = cursor.Index;
        if (mappingsCountWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) mappingsCountWriterStart = cursor.Index;
        if (symbolMappingWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) symbolMappingWriterStart = cursor.Index;
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.SymbolCountRule0(bytes, ref cursor, symbolsCountWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : symbolsCountWriterStart + symbolsCountDependencyWriteDistanceFromSymbolsCount, symbolWriterStart, symbolWriterCount);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.PartialSymbolCountRule0(bytes, ref cursor, partialCountWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : partialCountWriterStart + partialCountDependencyWriteDistanceFromPartialCount, partialSymbolWriterStart, partialSymbolWriterCount);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.NotFoundSymbolCountRule0(bytes, ref cursor, notFoundCountWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : notFoundCountWriterStart + notFoundCountDependencyWriteDistanceFromNotFoundCount, notFoundSymbolWriterStart, notFoundSymbolWriterCount);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.SymbolMappingCountRule0(bytes, ref cursor, mappingsCountWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : mappingsCountWriterStart + mappingsCountDependencyWriteDistanceFromMappingsCount, symbolMappingWriterStart, symbolMappingWriterCount);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.MetadataWriter.MetadataSizeRule0(bytes, ref cursor, start < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : start + metadataLengthDependencyWriteDistanceFromMetadata, start, cursor.Index);
        return bytes[start..cursor.Index];
    }
}
