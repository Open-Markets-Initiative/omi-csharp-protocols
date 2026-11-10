using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Disable Order Entry Response Appendage
/// </summary>
public static class DisableOrderEntryResponseAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalFieldField DisableOrderEntryResponseOptionalField;
    }

    /// <summary>
    ///  Reads the Disable Order Entry Response Appendage in place.
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
        ///  Gets the bytes of the Disable Order Entry Response Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Disable Order Entry Response Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly DisableOrderEntryResponseOptionalFieldField DisableOrderEntryResponseOptionalField => ref this.layout.DisableOrderEntryResponseOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public DisableOrderEntryResponseOptionalValue.Reader DisableOrderEntryResponseOptionalValue
        {
            get
            {
                int disableOrderEntryResponseOptionalValueReaderPosition = Parser.Valid(Parser.DisableOrderEntryResponseAppendageDisableOrderEntryResponseOptionalValueStart(this.bytes, this.start, this.end, this.start, out var disableOrderEntryResponseOptionalFieldValue));
                return new(this.bytes, disableOrderEntryResponseOptionalValueReaderPosition, this.end, disableOrderEntryResponseOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Disable Order Entry Response Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Value DisableOrderEntryResponseOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Disable Order Entry Response Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisableOrderEntryResponseOptionalField;
    private const int disableOrderEntryResponseAppendageOptionalFieldLengthDependencyWriteDistanceFromDisableOrderEntryResponseAppendage = 0;
    private const int disableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromDisableOrderEntryResponseAppendage = 1;
    private const int DisableOrderEntryResponseOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Disable Order Entry Response Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Disable Order Entry Response Appendage.
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
        int disableOrderEntryResponseOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int disableOrderEntryResponseOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.DisableOrderEntryResponseAppendage_OptionalFieldLength_Used = false;
        cursor.DisableOrderEntryResponseAppendage_DisableOrderEntryResponseOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (disableOrderEntryResponseOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) disableOrderEntryResponseOptionalValueWriterStart = cursor.Index;
        int disableOrderEntryResponseOptionalValueWriting = cursor.Index;
        var disableOrderEntryResponseOptionalValueWriteValue = value.DisableOrderEntryResponseOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Write(bytes, ref cursor, disableOrderEntryResponseOptionalValueWriting, ref disableOrderEntryResponseOptionalValueWriterCase, in disableOrderEntryResponseOptionalValueWriteValue);
        if (disableOrderEntryResponseOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) disableOrderEntryResponseOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.DisableOrderEntryResponseAppendageWriter.DisableOrderEntryResponseAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + disableOrderEntryResponseAppendageOptionalFieldLengthDependencyWriteDistanceFromDisableOrderEntryResponseAppendage, start, cursor.Index);
        if (disableOrderEntryResponseOptionalValueWriterCase == DisableOrderEntryResponseOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.DisableOrderEntryResponseAppendageWriter.DisableOrderEntryResponseOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + disableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromDisableOrderEntryResponseAppendage, disableOrderEntryResponseOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.DisableOrderEntryResponseAppendageWriter.DisableOrderEntryResponseOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + disableOrderEntryResponseOptionalFieldDependencyWriteDistanceFromDisableOrderEntryResponseAppendage, disableOrderEntryResponseOptionalValueWriterStart, disableOrderEntryResponseOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : disableOrderEntryResponseOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
