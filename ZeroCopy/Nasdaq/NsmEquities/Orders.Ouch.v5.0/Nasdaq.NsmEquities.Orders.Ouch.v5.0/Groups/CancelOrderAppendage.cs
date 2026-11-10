using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Cancel Order Appendage
/// </summary>
public static class CancelOrderAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalFieldField CancelOrderOptionalField;
    }

    /// <summary>
    ///  Reads the Cancel Order Appendage in place.
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
        ///  Gets the bytes of the Cancel Order Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Cancel Order Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly CancelOrderOptionalFieldField CancelOrderOptionalField => ref this.layout.CancelOrderOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public CancelOrderOptionalValue.Reader CancelOrderOptionalValue
        {
            get
            {
                int cancelOrderOptionalValueReaderPosition = Parser.Valid(Parser.CancelOrderAppendageCancelOrderOptionalValueStart(this.bytes, this.start, this.end, this.start, out var cancelOrderOptionalFieldValue));
                return new(this.bytes, cancelOrderOptionalValueReaderPosition, this.end, cancelOrderOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Cancel Order Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Value CancelOrderOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Cancel Order Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCancelOrderOptionalField;
    private const int cancelOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromCancelOrderAppendage = 0;
    private const int cancelOrderOptionalFieldDependencyWriteDistanceFromCancelOrderAppendage = 1;
    private const int CancelOrderOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Cancel Order Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cancel Order Appendage.
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
        int cancelOrderOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.CancelOrderAppendage_OptionalFieldLength_Used = false;
        cursor.CancelOrderAppendage_CancelOrderOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (cancelOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelOrderOptionalValueWriterStart = cursor.Index;
        int cancelOrderOptionalValueWriting = cursor.Index;
        var cancelOrderOptionalValueWriteValue = value.CancelOrderOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Write(bytes, ref cursor, cancelOrderOptionalValueWriting, ref cancelOrderOptionalValueWriterCase, in cancelOrderOptionalValueWriteValue);
        if (cancelOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelOrderOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelOrderAppendageWriter.CancelOrderAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromCancelOrderAppendage, start, cursor.Index);
        if (cancelOrderOptionalValueWriterCase == CancelOrderOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelOrderAppendageWriter.CancelOrderOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelOrderOptionalFieldDependencyWriteDistanceFromCancelOrderAppendage, cancelOrderOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelOrderAppendageWriter.CancelOrderOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelOrderOptionalFieldDependencyWriteDistanceFromCancelOrderAppendage, cancelOrderOptionalValueWriterStart, cancelOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : cancelOrderOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
