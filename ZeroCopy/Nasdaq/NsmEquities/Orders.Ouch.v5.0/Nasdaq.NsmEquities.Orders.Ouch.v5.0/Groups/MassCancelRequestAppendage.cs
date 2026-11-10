using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Mass Cancel Request Appendage
/// </summary>
public static class MassCancelRequestAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalFieldField MassCancelRequestOptionalField;
    }

    /// <summary>
    ///  Reads the Mass Cancel Request Appendage in place.
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
        ///  Gets the bytes of the Mass Cancel Request Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Mass Cancel Request Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly MassCancelRequestOptionalFieldField MassCancelRequestOptionalField => ref this.layout.MassCancelRequestOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public MassCancelRequestOptionalValue.Reader MassCancelRequestOptionalValue
        {
            get
            {
                int massCancelRequestOptionalValueReaderPosition = Parser.Valid(Parser.MassCancelRequestAppendageMassCancelRequestOptionalValueStart(this.bytes, this.start, this.end, this.start, out var massCancelRequestOptionalFieldValue));
                return new(this.bytes, massCancelRequestOptionalValueReaderPosition, this.end, massCancelRequestOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Mass Cancel Request Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Value MassCancelRequestOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Mass Cancel Request Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMassCancelRequestOptionalField;
    private const int massCancelRequestAppendageOptionalFieldLengthDependencyWriteDistanceFromMassCancelRequestAppendage = 0;
    private const int massCancelRequestOptionalFieldDependencyWriteDistanceFromMassCancelRequestAppendage = 1;
    private const int MassCancelRequestOptionalValueSideCase = 1;
    private const int MassCancelRequestOptionalValueGroupIdCase = 2;
    private const int MassCancelRequestOptionalValueUserRefIdxCase = 3;

    /// <summary>
    ///  Writes the Mass Cancel Request Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Mass Cancel Request Appendage.
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
        int massCancelRequestOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int massCancelRequestOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.MassCancelRequestAppendage_OptionalFieldLength_Used = false;
        cursor.MassCancelRequestAppendage_MassCancelRequestOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (massCancelRequestOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) massCancelRequestOptionalValueWriterStart = cursor.Index;
        int massCancelRequestOptionalValueWriting = cursor.Index;
        var massCancelRequestOptionalValueWriteValue = value.MassCancelRequestOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Write(bytes, ref cursor, massCancelRequestOptionalValueWriting, ref massCancelRequestOptionalValueWriterCase, in massCancelRequestOptionalValueWriteValue);
        if (massCancelRequestOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) massCancelRequestOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestAppendageWriter.MassCancelRequestAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestAppendageOptionalFieldLengthDependencyWriteDistanceFromMassCancelRequestAppendage, start, cursor.Index);
        if (massCancelRequestOptionalValueWriterCase == MassCancelRequestOptionalValueSideCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestAppendageWriter.MassCancelRequestOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestOptionalFieldDependencyWriteDistanceFromMassCancelRequestAppendage, massCancelRequestOptionalValueWriterStart);
        if (massCancelRequestOptionalValueWriterCase == MassCancelRequestOptionalValueGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestAppendageWriter.MassCancelRequestOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestOptionalFieldDependencyWriteDistanceFromMassCancelRequestAppendage, massCancelRequestOptionalValueWriterStart);
        if (massCancelRequestOptionalValueWriterCase == MassCancelRequestOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestAppendageWriter.MassCancelRequestOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestOptionalFieldDependencyWriteDistanceFromMassCancelRequestAppendage, massCancelRequestOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.MassCancelRequestAppendageWriter.MassCancelRequestOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + massCancelRequestOptionalFieldDependencyWriteDistanceFromMassCancelRequestAppendage, massCancelRequestOptionalValueWriterStart, massCancelRequestOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : massCancelRequestOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
