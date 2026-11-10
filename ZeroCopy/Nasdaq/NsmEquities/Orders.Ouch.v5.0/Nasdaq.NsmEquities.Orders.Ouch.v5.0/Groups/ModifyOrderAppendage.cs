using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Modify Order Appendage
/// </summary>
public static class ModifyOrderAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalFieldField ModifyOrderOptionalField;
    }

    /// <summary>
    ///  Reads the Modify Order Appendage in place.
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
        ///  Gets the bytes of the Modify Order Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Modify Order Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly ModifyOrderOptionalFieldField ModifyOrderOptionalField => ref this.layout.ModifyOrderOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public ModifyOrderOptionalValue.Reader ModifyOrderOptionalValue
        {
            get
            {
                int modifyOrderOptionalValueReaderPosition = Parser.Valid(Parser.ModifyOrderAppendageModifyOrderOptionalValueStart(this.bytes, this.start, this.end, this.start, out var modifyOrderOptionalFieldValue));
                return new(this.bytes, modifyOrderOptionalValueReaderPosition, this.end, modifyOrderOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Modify Order Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Value ModifyOrderOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Modify Order Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeModifyOrderOptionalField;
    private const int modifyOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromModifyOrderAppendage = 0;
    private const int modifyOrderOptionalFieldDependencyWriteDistanceFromModifyOrderAppendage = 1;
    private const int ModifyOrderOptionalValueUserRefIdxCase = 1;
    private const int ModifyOrderOptionalValueSharesLocatedCase = 2;
    private const int ModifyOrderOptionalValueLocateBrokerCase = 3;

    /// <summary>
    ///  Writes the Modify Order Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Modify Order Appendage.
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
        int modifyOrderOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int modifyOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.ModifyOrderAppendage_OptionalFieldLength_Used = false;
        cursor.ModifyOrderAppendage_ModifyOrderOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (modifyOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderOptionalValueWriterStart = cursor.Index;
        int modifyOrderOptionalValueWriting = cursor.Index;
        var modifyOrderOptionalValueWriteValue = value.ModifyOrderOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Write(bytes, ref cursor, modifyOrderOptionalValueWriting, ref modifyOrderOptionalValueWriterCase, in modifyOrderOptionalValueWriteValue);
        if (modifyOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) modifyOrderOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderAppendageWriter.ModifyOrderAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + modifyOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromModifyOrderAppendage, start, cursor.Index);
        if (modifyOrderOptionalValueWriterCase == ModifyOrderOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderAppendageWriter.ModifyOrderOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + modifyOrderOptionalFieldDependencyWriteDistanceFromModifyOrderAppendage, modifyOrderOptionalValueWriterStart);
        if (modifyOrderOptionalValueWriterCase == ModifyOrderOptionalValueSharesLocatedCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderAppendageWriter.ModifyOrderOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + modifyOrderOptionalFieldDependencyWriteDistanceFromModifyOrderAppendage, modifyOrderOptionalValueWriterStart);
        if (modifyOrderOptionalValueWriterCase == ModifyOrderOptionalValueLocateBrokerCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderAppendageWriter.ModifyOrderOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + modifyOrderOptionalFieldDependencyWriteDistanceFromModifyOrderAppendage, modifyOrderOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ModifyOrderAppendageWriter.ModifyOrderOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + modifyOrderOptionalFieldDependencyWriteDistanceFromModifyOrderAppendage, modifyOrderOptionalValueWriterStart, modifyOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : modifyOrderOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
