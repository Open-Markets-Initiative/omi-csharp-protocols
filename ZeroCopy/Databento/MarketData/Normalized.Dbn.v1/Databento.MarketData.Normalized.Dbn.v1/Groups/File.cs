using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  A DBN file: the metadata, then records to its end
/// </summary>
public static class File
{
    /// <summary>
    ///  Reads the File in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte? tsOutStoredValue;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte? tsOutStoredValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.tsOutStoredValue = tsOutStoredValue;
        }

        /// <summary>
        ///  Gets the bytes of the File.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  DBN metadata header, once at the start of a file or stream
        /// </summary>
        public Metadata.Reader Metadata
        {
            get
            {
                int metadataReaderPosition = Parser.Valid(Parser.FileMetadataStart(this.bytes, this.start, this.end, this.start, this.tsOutStoredValue));
                int metadataReaderEnd = Parser.Valid(Parser.FileMetadataEnd(this.bytes, metadataReaderPosition, this.end, this.start));
                return new(this.bytes, metadataReaderPosition, metadataReaderEnd);
            }
        }

        /// <summary>
        ///  A DBN record: the common header, the record its rtype selects, and the ts_out trailer when the metadata asks for it
        /// </summary>
        public File.RecordEntries Record
        {
            get
            {
                int recordReaderPosition = Parser.Valid(Parser.FileRecordStart(this.bytes, this.start, this.end, this.start, this.tsOutStoredValue, out var recordCursorLimit, out var recordCursorRecordPayloadRule0End, out var recordCursorDependency0));
                return new(this.bytes, recordReaderPosition, recordCursorLimit, recordCursorRecordPayloadRule0End, recordCursorDependency0);
            }
        }
    }

    /// <summary>
    ///  Reads one File from the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the File.
    /// </param>
    /// <param name="tsOutStoredValue">
    ///  The last Ts Out that earlier bytes stored, or null before they store one.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the File, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the File; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryRead(ReadOnlySpan<byte> source, byte? tsOutStoredValue, out Reader reader)
    {
        reader = new Reader(source, Parser.Position.Minimum, source.Length, tsOutStoredValue);
        return true;
    }

    /// <summary>
    ///  Gets the number of bytes before the first Record of the File.
    /// </summary>
    /// <param name="source">
    ///  The bytes, from the start of the File.
    /// </param>
    /// <param name="tsOutStoredValue">
    ///  The last Ts Out that earlier bytes stored, or null before they store one.
    /// </param>
    /// <param name="length">
    ///  The number of bytes before the first Record.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold every byte before the first Record; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryGetOpeningLength(ReadOnlySpan<byte> source, byte? tsOutStoredValue, out int length)
    {
        length = Parser.FileRecordStart(source, Parser.Position.Minimum, source.Length, Parser.Position.Minimum, tsOutStoredValue, out _, out _, out _);
        return length != Parser.Position.Failure;
    }

    /// <summary>
    ///  Reads one Record from the bytes of its frame.
    /// </summary>
    /// <param name="source">
    ///  The bytes of the frame of the Record.
    /// </param>
    /// <param name="tsOutStoredValue">
    ///  The last Ts Out that earlier bytes stored, or null before they store one.
    /// </param>
    /// <param name="reader">
    ///  The Reader of the Record, which borrows the bytes.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fixed fields at the start of the Record; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryReadRecord(ReadOnlySpan<byte> source, byte? tsOutStoredValue, out global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader reader)
    {
        if (global::Databento.MarketData.Normalized.Dbn.v1.Record.LayoutSize > source.Length)
        {
            reader = default;
            return false;
        }
        reader = new global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader(source, Parser.Position.Minimum, source.Length, tsOutStoredValue);
        return true;
    }

    /// <summary>
    ///  Gets the length of the File frame at the start of the bytes.
    /// </summary>
    /// <param name="source">
    ///  The bytes, from the start of the frame.
    /// </param>
    /// <param name="tsOutStoredValue">
    ///  The last Ts Out that earlier bytes stored, or null before they store one.
    /// </param>
    /// <param name="length">
    ///  The number of bytes of the frame, which can be more than the bytes hold.
    /// </param>
    /// <returns>
    ///  <see langword="true"/> when the bytes hold the fields that state the length; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="global::System.ArgumentOutOfRangeException">
    ///  The bytes state a frame length that is not valid.
    /// </exception>
    public static bool TryGetFrameLength(ReadOnlySpan<byte> source, byte? tsOutStoredValue, out int length)
    {
        int available = Parser.RecordReassembleRule1BytesRemaining(source, Parser.Position.Minimum, source.Length, out length, tsOutStoredValue);
        if (length < Parser.Position.Minimum || available >= Parser.Position.Minimum && length == Parser.Position.Minimum) throw new global::System.ArgumentOutOfRangeException(nameof(source), Parser.FrameLength);
        return length > Parser.Position.Minimum;
    }

    /// <summary>
    ///  Steps through the Record entries in wire order.
    /// </summary>
    /// <remarks>
    ///  A copy of the entries steps on its own. Each step measures its entry from the bytes.
    /// </remarks>
    public ref struct RecordEntries
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int limit;
        private int position;
        private long index;
        private int currentStart;
        private int currentEnd;
        private readonly int recordCursorRecordPayloadRule0End;
        private readonly byte? recordCursorDependency0;

        internal RecordEntries(ReadOnlySpan<byte> bytes, int start, int limit, int recordCursorRecordPayloadRule0End, byte? recordCursorDependency0)
        {
            this.bytes = bytes;
            this.limit = limit;
            this.position = start;
            this.recordCursorRecordPayloadRule0End = recordCursorRecordPayloadRule0End;
            this.recordCursorDependency0 = recordCursorDependency0;
        }
        private readonly void Entry()
        {
            if (this.index == Parser.Position.NoEntries) throw new global::System.InvalidOperationException(Parser.NoEntry);
        }
        /// <summary>
        ///  Gets the enumerator over the entries.
        /// </summary>
        public readonly RecordEntries GetEnumerator() => this;

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
            int end = Parser.StepFileRecord(this.bytes, this.position, this.limit, this.recordCursorRecordPayloadRule0End, this.recordCursorDependency0);
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
        public readonly global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader Current
        {
            get
            {
                Entry();
                return new(this.bytes, this.currentStart, this.currentEnd, this.recordCursorDependency0);
            }
        }
    }

    /// <summary>
    ///  The File fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  DBN metadata header, once at the start of a file or stream
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Value Metadata { get; init; }

        /// <summary>
        ///  A DBN record: the common header, the record its rtype selects, and the ts_out trailer when the metadata asks for it
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Entries<global::Databento.MarketData.Normalized.Dbn.v1.Record.Value> Record { get; init; }

        /// <summary>
        ///  Creates the value of the File, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    /// <summary>
    ///  Writes the File to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the File.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        int recordWriterStart = global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent;
        int recordWriterEnd = global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent;
        int recordWriterCount = global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.NoEntries;
        int metadataWriting = cursor.Index;
        var metadataWriteValue = value.Metadata;
        global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Write(bytes, ref cursor, metadataWriting, in metadataWriteValue);
        for (int recordIndex = 0; recordIndex < value.Record.Count; recordIndex++)
        {
            var recordCurrent = value.Record[recordIndex];
            if (recordWriterStart == global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent) recordWriterStart = cursor.Index;
            recordWriterCount++;
            int recordWriting = cursor.Index;
            var recordWriteValue = recordCurrent;
            global::Databento.MarketData.Normalized.Dbn.v1.Record.Write(bytes, ref cursor, recordWriting, in recordWriteValue);
        }
        if (recordWriterStart == global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent) recordWriterStart = cursor.Index;
        if (recordWriterEnd == global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent) recordWriterEnd = cursor.Index;
        global::Databento.MarketData.Normalized.Dbn.v1.Parser.FileWriter.RecordPayloadRule0(bytes, ref cursor, recordWriterStart, cursor.Index, recordWriterEnd);
        return bytes[start..cursor.Index];
    }
}
