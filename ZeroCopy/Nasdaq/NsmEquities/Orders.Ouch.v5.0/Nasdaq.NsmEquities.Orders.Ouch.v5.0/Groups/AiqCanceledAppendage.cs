using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  AIQ Canceled Appendage
/// </summary>
public static class AiqCanceledAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalFieldField AiqCanceledOptionalField;
    }

    /// <summary>
    ///  Reads the Aiq Canceled Appendage in place.
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
        ///  Gets the bytes of the Aiq Canceled Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Aiq Canceled Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly AiqCanceledOptionalFieldField AiqCanceledOptionalField => ref this.layout.AiqCanceledOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public AiqCanceledOptionalValue.Reader AiqCanceledOptionalValue
        {
            get
            {
                int aiqCanceledOptionalValueReaderPosition = Parser.Valid(Parser.AiqCanceledAppendageAiqCanceledOptionalValueStart(this.bytes, this.start, this.end, this.start, out var aiqCanceledOptionalFieldValue));
                return new(this.bytes, aiqCanceledOptionalValueReaderPosition, this.end, aiqCanceledOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Aiq Canceled Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Value AiqCanceledOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Aiq Canceled Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqCanceledOptionalField;
    private const int aiqCanceledAppendageOptionalFieldLengthDependencyWriteDistanceFromAiqCanceledAppendage = 0;
    private const int aiqCanceledOptionalFieldDependencyWriteDistanceFromAiqCanceledAppendage = 1;
    private const int AiqCanceledOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Aiq Canceled Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Aiq Canceled Appendage.
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
        int aiqCanceledOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int aiqCanceledOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.AiqCanceledAppendage_OptionalFieldLength_Used = false;
        cursor.AiqCanceledAppendage_AiqCanceledOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (aiqCanceledOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledOptionalValueWriterStart = cursor.Index;
        int aiqCanceledOptionalValueWriting = cursor.Index;
        var aiqCanceledOptionalValueWriteValue = value.AiqCanceledOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Write(bytes, ref cursor, aiqCanceledOptionalValueWriting, ref aiqCanceledOptionalValueWriterCase, in aiqCanceledOptionalValueWriteValue);
        if (aiqCanceledOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) aiqCanceledOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AiqCanceledAppendageWriter.AiqCanceledAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + aiqCanceledAppendageOptionalFieldLengthDependencyWriteDistanceFromAiqCanceledAppendage, start, cursor.Index);
        if (aiqCanceledOptionalValueWriterCase == AiqCanceledOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AiqCanceledAppendageWriter.AiqCanceledOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + aiqCanceledOptionalFieldDependencyWriteDistanceFromAiqCanceledAppendage, aiqCanceledOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.AiqCanceledAppendageWriter.AiqCanceledOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + aiqCanceledOptionalFieldDependencyWriteDistanceFromAiqCanceledAppendage, aiqCanceledOptionalValueWriterStart, aiqCanceledOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : aiqCanceledOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
