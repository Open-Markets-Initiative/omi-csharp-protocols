using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A DBN record: the common header, the record its rtype selects, and the ts_out trailer when the metadata asks for it
/// </summary>
public static class Record
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
            if (global::Databento.MarketData.Normalized.Dbn.v3.Record.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  DBN common header of every record
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RecordHeader.Layout RecordHeader;
    }

    /// <summary>
    ///  Reads the Record in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte? tsOutStoredValue;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte? tsOutStoredValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.tsOutStoredValue = tsOutStoredValue;
        }

        /// <summary>
        ///  Gets the bytes of the Record.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Record.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  DBN common header of every record
        /// </summary>
        public ref readonly RecordHeader.Layout RecordHeader => ref this.layout.RecordHeader;

        /// <summary>
        ///  Branch
        /// </summary>
        public RecordBody.Reader RecordBody
        {
            get
            {
                int recordBodyReaderPosition = Parser.Valid(Parser.RecordRecordBodyStart(this.bytes, this.start, this.end, this.start, this.tsOutStoredValue, out var rtypeValue));
                return new(this.bytes, recordBodyReaderPosition, this.end, rtypeValue);
            }
        }

        /// <summary>
        ///  The live gateway send timestamp, present on every record when the metadata ts_out flag is set
        /// </summary>
        /// <remarks>
        ///  <see langword="null"/> when the bytes leave it out.
        /// </remarks>
        public TsOutTime? TsOutTime
        {
            get
            {
                int tsOutTimeReaderPosition = Parser.Valid(Parser.RecordTsOutTimeStart(this.bytes, this.start, this.end, this.start, this.tsOutStoredValue));
                return tsOutTimeReaderPosition == Parser.Position.Absent ? null : global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TsOutTime>(this.bytes[tsOutTimeReaderPosition..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Record fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  DBN common header of every record
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.RecordHeader.Value RecordHeader { get; init; }

        /// <summary>
        ///  Branch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Value RecordBody { get; init; } = new();

        /// <summary>
        ///  The live gateway send timestamp, present on every record when the metadata ts_out flag is set
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsOutTime? TsOutTime { get; init; }

        /// <summary>
        ///  Creates the value of the Record, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRecordHeader;
    private const int recordLengthDependencyWriteDistanceFromRecordHeader = 0;
    private const int rtypeDependencyWriteDistanceFromRecordHeader = 1;
    private const int TradeMsgCase = 1;
    private const int Mbp1MsgCase = 2;
    private const int Mbp10MsgCase = 3;
    private const int OhlcvMsgCase = 4;
    private const int StatusMsgCase = 5;
    private const int InstrumentDefMsgCase = 6;
    private const int ImbalanceMsgCase = 7;
    private const int ErrorMsgCase = 8;
    private const int SymbolMappingMsgCase = 9;
    private const int SystemMsgCase = 10;
    private const int StatMsgCase = 11;
    private const int MboMsgCase = 12;
    private const int Cmbp1MsgCase = 13;
    private const int CbboMsgCase = 14;
    private const int BboMsgCase = 15;

    /// <summary>
    ///  Writes the Record to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Record.
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
        int recordHeaderWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int recordBodyWriterStart = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int recordBodyWriterCase = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent;
        int tsOutTimeWriterCount = global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoEntries;
        bool recordLengthDependencyFirstUsed = false;
        bool rtypeDependencyFirstUsed = false;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        if (recordHeaderWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordHeaderWriterStart = cursor.Index;
        int recordHeaderWriting = cursor.Index;
        var recordHeaderWriteValue = value.RecordHeader;
        global::Databento.MarketData.Normalized.Dbn.v3.RecordHeader.Write(bytes, ref cursor, recordHeaderWriting, ref recordHeaderWriterStart == recordHeaderWriting ? ref recordLengthDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref recordHeaderWriterStart == recordHeaderWriting ? ref rtypeDependencyFirstUsed : ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in recordHeaderWriteValue);
        if (recordHeaderWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordHeaderWriterStart = cursor.Index;
        if (recordBodyWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordBodyWriterStart = cursor.Index;
        int recordBodyWriting = cursor.Index;
        var recordBodyWriteValue = value.RecordBody;
        global::Databento.MarketData.Normalized.Dbn.v3.RecordBody.Write(bytes, ref cursor, recordBodyWriting, ref recordBodyWriterCase, in recordBodyWriteValue);
        if (value.TsOutTime is { } tsOutTimeCurrent)
        {
            if (global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsOutTime > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
            if (recordHeaderWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordHeaderWriterStart = cursor.Index;
            if (recordBodyWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordBodyWriterStart = cursor.Index;
            tsOutTimeWriterCount++;
            int tsOutTimeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Databento.MarketData.Normalized.Dbn.v3.TsOutTime>(bytes[tsOutTimeWriterPosition..]) = tsOutTimeCurrent;
            cursor.Index += global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsOutTime;
        }
        if (recordHeaderWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordHeaderWriterStart = cursor.Index;
        if (recordBodyWriterStart == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent) recordBodyWriterStart = cursor.Index;
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordReassembleRule1(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + recordLengthDependencyWriteDistanceFromRecordHeader, ref recordLengthDependencyFirstUsed, start, cursor.Index);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordSizeRule2(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + recordLengthDependencyWriteDistanceFromRecordHeader, ref recordLengthDependencyFirstUsed, start, cursor.Index);
        if (recordBodyWriterCase == TradeMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule0(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == Mbp1MsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule1(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == Mbp10MsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule2(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == StatusMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule4(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == InstrumentDefMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule5(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == ImbalanceMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule6(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == ErrorMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule7(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == SymbolMappingMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule8(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == SystemMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule9(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == StatMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule10(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        if (recordBodyWriterCase == MboMsgCase) global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyBranchRule16(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart);
        global::Databento.MarketData.Normalized.Dbn.v3.Parser.RecordWriter.RecordBodyCases(bytes, ref cursor, recordHeaderWriterStart < global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordHeaderWriterStart + rtypeDependencyWriteDistanceFromRecordHeader, ref rtypeDependencyFirstUsed, recordBodyWriterStart, recordBodyWriterCase == global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.NoCase ? global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Absent : recordBodyWriterCase);
        return bytes[start..cursor.Index];
    }
}
