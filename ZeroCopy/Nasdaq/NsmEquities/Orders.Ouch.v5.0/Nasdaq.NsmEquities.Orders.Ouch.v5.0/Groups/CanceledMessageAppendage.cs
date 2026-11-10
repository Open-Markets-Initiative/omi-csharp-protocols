using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Canceled Message Appendage
/// </summary>
public static class CanceledMessageAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalFieldField CanceledMessageOptionalField;
    }

    /// <summary>
    ///  Reads the Canceled Message Appendage in place.
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
        ///  Gets the bytes of the Canceled Message Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Canceled Message Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly CanceledMessageOptionalFieldField CanceledMessageOptionalField => ref this.layout.CanceledMessageOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public CanceledMessageOptionalValue.Reader CanceledMessageOptionalValue
        {
            get
            {
                int canceledMessageOptionalValueReaderPosition = Parser.Valid(Parser.CanceledMessageAppendageCanceledMessageOptionalValueStart(this.bytes, this.start, this.end, this.start, out var canceledMessageOptionalFieldValue));
                return new(this.bytes, canceledMessageOptionalValueReaderPosition, this.end, canceledMessageOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Canceled Message Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Value CanceledMessageOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Canceled Message Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCanceledMessageOptionalField;
    private const int canceledMessageAppendageOptionalFieldLengthDependencyWriteDistanceFromCanceledMessageAppendage = 0;
    private const int canceledMessageOptionalFieldDependencyWriteDistanceFromCanceledMessageAppendage = 1;
    private const int CanceledMessageOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Canceled Message Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Canceled Message Appendage.
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
        int canceledMessageOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int canceledMessageOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.CanceledMessageAppendage_OptionalFieldLength_Used = false;
        cursor.CanceledMessageAppendage_CanceledMessageOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (canceledMessageOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) canceledMessageOptionalValueWriterStart = cursor.Index;
        int canceledMessageOptionalValueWriting = cursor.Index;
        var canceledMessageOptionalValueWriteValue = value.CanceledMessageOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Write(bytes, ref cursor, canceledMessageOptionalValueWriting, ref canceledMessageOptionalValueWriterCase, in canceledMessageOptionalValueWriteValue);
        if (canceledMessageOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) canceledMessageOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CanceledMessageAppendageWriter.CanceledMessageAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + canceledMessageAppendageOptionalFieldLengthDependencyWriteDistanceFromCanceledMessageAppendage, start, cursor.Index);
        if (canceledMessageOptionalValueWriterCase == CanceledMessageOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CanceledMessageAppendageWriter.CanceledMessageOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + canceledMessageOptionalFieldDependencyWriteDistanceFromCanceledMessageAppendage, canceledMessageOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CanceledMessageAppendageWriter.CanceledMessageOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + canceledMessageOptionalFieldDependencyWriteDistanceFromCanceledMessageAppendage, canceledMessageOptionalValueWriterStart, canceledMessageOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : canceledMessageOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
