using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Cancel Pending Appendage
/// </summary>
public static class CancelPendingAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalFieldField CancelPendingOptionalField;
    }

    /// <summary>
    ///  Reads the Cancel Pending Appendage in place.
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
        ///  Gets the bytes of the Cancel Pending Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Cancel Pending Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly CancelPendingOptionalFieldField CancelPendingOptionalField => ref this.layout.CancelPendingOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public CancelPendingOptionalValue.Reader CancelPendingOptionalValue
        {
            get
            {
                int cancelPendingOptionalValueReaderPosition = Parser.Valid(Parser.CancelPendingAppendageCancelPendingOptionalValueStart(this.bytes, this.start, this.end, this.start, out var cancelPendingOptionalFieldValue));
                return new(this.bytes, cancelPendingOptionalValueReaderPosition, this.end, cancelPendingOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Cancel Pending Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Value CancelPendingOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Cancel Pending Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCancelPendingOptionalField;
    private const int cancelPendingAppendageOptionalFieldLengthDependencyWriteDistanceFromCancelPendingAppendage = 0;
    private const int cancelPendingOptionalFieldDependencyWriteDistanceFromCancelPendingAppendage = 1;
    private const int CancelPendingOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Cancel Pending Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cancel Pending Appendage.
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
        int cancelPendingOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int cancelPendingOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.CancelPendingAppendage_OptionalFieldLength_Used = false;
        cursor.CancelPendingAppendage_CancelPendingOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (cancelPendingOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingOptionalValueWriterStart = cursor.Index;
        int cancelPendingOptionalValueWriting = cursor.Index;
        var cancelPendingOptionalValueWriteValue = value.CancelPendingOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Write(bytes, ref cursor, cancelPendingOptionalValueWriting, ref cancelPendingOptionalValueWriterCase, in cancelPendingOptionalValueWriteValue);
        if (cancelPendingOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) cancelPendingOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelPendingAppendageWriter.CancelPendingAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelPendingAppendageOptionalFieldLengthDependencyWriteDistanceFromCancelPendingAppendage, start, cursor.Index);
        if (cancelPendingOptionalValueWriterCase == CancelPendingOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelPendingAppendageWriter.CancelPendingOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelPendingOptionalFieldDependencyWriteDistanceFromCancelPendingAppendage, cancelPendingOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.CancelPendingAppendageWriter.CancelPendingOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + cancelPendingOptionalFieldDependencyWriteDistanceFromCancelPendingAppendage, cancelPendingOptionalValueWriterStart, cancelPendingOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : cancelPendingOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
