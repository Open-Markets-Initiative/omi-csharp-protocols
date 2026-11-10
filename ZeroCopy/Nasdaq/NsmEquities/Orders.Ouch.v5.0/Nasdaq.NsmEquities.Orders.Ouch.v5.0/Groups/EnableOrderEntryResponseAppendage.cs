using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Enable Order Entry Response Appendage
/// </summary>
public static class EnableOrderEntryResponseAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalFieldField EnableOrderEntryResponseOptionalField;
    }

    /// <summary>
    ///  Reads the Enable Order Entry Response Appendage in place.
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
        ///  Gets the bytes of the Enable Order Entry Response Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Enable Order Entry Response Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly EnableOrderEntryResponseOptionalFieldField EnableOrderEntryResponseOptionalField => ref this.layout.EnableOrderEntryResponseOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public EnableOrderEntryResponseOptionalValue.Reader EnableOrderEntryResponseOptionalValue
        {
            get
            {
                int enableOrderEntryResponseOptionalValueReaderPosition = Parser.Valid(Parser.EnableOrderEntryResponseAppendageEnableOrderEntryResponseOptionalValueStart(this.bytes, this.start, this.end, this.start, out var enableOrderEntryResponseOptionalFieldValue));
                return new(this.bytes, enableOrderEntryResponseOptionalValueReaderPosition, this.end, enableOrderEntryResponseOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Enable Order Entry Response Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Value EnableOrderEntryResponseOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Enable Order Entry Response Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeEnableOrderEntryResponseOptionalField;
    private const int enableOrderEntryResponseAppendageOptionalFieldLengthDependencyWriteDistanceFromEnableOrderEntryResponseAppendage = 0;
    private const int enableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromEnableOrderEntryResponseAppendage = 1;
    private const int EnableOrderEntryResponseOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Enable Order Entry Response Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Enable Order Entry Response Appendage.
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
        int enableOrderEntryResponseOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enableOrderEntryResponseOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.EnableOrderEntryResponseAppendage_OptionalFieldLength_Used = false;
        cursor.EnableOrderEntryResponseAppendage_EnableOrderEntryResponseOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (enableOrderEntryResponseOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enableOrderEntryResponseOptionalValueWriterStart = cursor.Index;
        int enableOrderEntryResponseOptionalValueWriting = cursor.Index;
        var enableOrderEntryResponseOptionalValueWriteValue = value.EnableOrderEntryResponseOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Write(bytes, ref cursor, enableOrderEntryResponseOptionalValueWriting, ref enableOrderEntryResponseOptionalValueWriterCase, in enableOrderEntryResponseOptionalValueWriteValue);
        if (enableOrderEntryResponseOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enableOrderEntryResponseOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnableOrderEntryResponseAppendageWriter.EnableOrderEntryResponseAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enableOrderEntryResponseAppendageOptionalFieldLengthDependencyWriteDistanceFromEnableOrderEntryResponseAppendage, start, cursor.Index);
        if (enableOrderEntryResponseOptionalValueWriterCase == EnableOrderEntryResponseOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnableOrderEntryResponseAppendageWriter.EnableOrderEntryResponseOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromEnableOrderEntryResponseAppendage, enableOrderEntryResponseOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnableOrderEntryResponseAppendageWriter.EnableOrderEntryResponseOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromEnableOrderEntryResponseAppendage, enableOrderEntryResponseOptionalValueWriterStart, enableOrderEntryResponseOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : enableOrderEntryResponseOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
