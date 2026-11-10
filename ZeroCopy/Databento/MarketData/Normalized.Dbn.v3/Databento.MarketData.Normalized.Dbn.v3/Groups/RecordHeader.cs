using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  DBN common header of every record
/// </summary>
public static class RecordHeader
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Record Header in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRecordLength + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRtype + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePublisherId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsEvent;

        /// <summary>
        ///  Gets the Record Header laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Record Header.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Record Header at the start of the buffer, writes its fixed values, and gets the Record Header laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Record Header.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination);
        }

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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The length of the record in 32-bit words, including this header and any ts_out trailer
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RecordLength RecordLength;
        /// <summary>
        ///  The record type, which selects the record that follows the header
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.RtypeField Rtype;
        /// <summary>
        ///  The publisher ID assigned by Databento, which denotes the dataset and venue
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.PublisherId PublisherId;
        /// <summary>
        ///  The numeric instrument ID
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.InstrumentId InstrumentId;
        /// <summary>
        ///  The matching-engine-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsEvent TsEvent;
    }

    /// <summary>
    ///  The Record Header fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The publisher ID assigned by Databento, which denotes the dataset and venue
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.PublisherId PublisherId { get; init; }

        /// <summary>
        ///  The numeric instrument ID
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.InstrumentId InstrumentId { get; init; }

        /// <summary>
        ///  The matching-engine-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsEvent TsEvent { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.TsEvent.Absent;

        /// <summary>
        ///  Creates the value of the Record Header, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRecordLength + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeRtype + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePublisherId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeInstrumentId + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsEvent;

    /// <summary>
    ///  Writes the Record Header to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Record Header.
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
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor cursor, int start, scoped ref bool recordLengthDependencyWriteUsed, scoped ref bool rtypeDependencyWriteUsed, scoped in Value value)
    {
        ref bool recordLengthDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref recordLengthDependencyWriteUsed) ? ref cursor.RecordHeader_RecordLength_Used : ref recordLengthDependencyWriteUsed;
        ref bool rtypeDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref rtypeDependencyWriteUsed) ? ref cursor.RecordHeader_Rtype_Used : ref rtypeDependencyWriteUsed;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        ref Layout recordLengthWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        recordLengthWriterLayout.PublisherId = value.PublisherId;
        recordLengthWriterLayout.InstrumentId = value.InstrumentId;
        recordLengthWriterLayout.TsEvent = value.TsEvent;
        cursor.RecordHeader_RecordLength_Used = false;
        cursor.RecordHeader_Rtype_Used = false;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
