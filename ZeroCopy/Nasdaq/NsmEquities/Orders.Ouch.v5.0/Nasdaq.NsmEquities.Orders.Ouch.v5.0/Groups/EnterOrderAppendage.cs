using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Enter Order Appendage
/// </summary>
public static class EnterOrderAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalFieldField EnterOrderOptionalField;
    }

    /// <summary>
    ///  Reads the Enter Order Appendage in place.
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
        ///  Gets the bytes of the Enter Order Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Enter Order Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly EnterOrderOptionalFieldField EnterOrderOptionalField => ref this.layout.EnterOrderOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public EnterOrderOptionalValue.Reader EnterOrderOptionalValue
        {
            get
            {
                int enterOrderOptionalValueReaderPosition = Parser.Valid(Parser.EnterOrderAppendageEnterOrderOptionalValueStart(this.bytes, this.start, this.end, this.start, out var enterOrderOptionalFieldValue));
                return new(this.bytes, enterOrderOptionalValueReaderPosition, this.end, enterOrderOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Enter Order Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Value EnterOrderOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Enter Order Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeEnterOrderOptionalField;
    private const int enterOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromEnterOrderAppendage = 0;
    private const int enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage = 1;
    private const int EnterOrderOptionalValueFirmCase = 1;
    private const int EnterOrderOptionalValueMinQtyCase = 2;
    private const int CustomerTypeCase = 3;
    private const int EnterOrderOptionalValueMaxFloorCase = 4;
    private const int EnterOrderOptionalValuePriceTypeCase = 5;
    private const int EnterOrderOptionalValuePegOffsetCase = 6;
    private const int EnterOrderOptionalValueDiscretionPriceCase = 7;
    private const int EnterOrderOptionalValueDiscretionPriceTypeCase = 8;
    private const int EnterOrderOptionalValueDiscretionPegOffsetCase = 9;
    private const int EnterOrderOptionalValuePostOnlyCase = 10;
    private const int EnterOrderOptionalValueRandomReservesCase = 11;
    private const int EnterOrderOptionalValueExpireTimeCase = 12;
    private const int EnterOrderOptionalValueTradeNowCase = 13;
    private const int EnterOrderOptionalValueHandleInstCase = 14;
    private const int EnterOrderOptionalValueGroupIdCase = 15;
    private const int EnterOrderOptionalValueSharesLocatedCase = 16;
    private const int EnterOrderOptionalValueLocateBrokerCase = 17;
    private const int EnterOrderOptionalValueUserRefIdxCase = 18;
    private const int EnterOrderOptionalValueAiqStrategyCase = 19;
    private const int EnterOrderOptionalValueAiqGroupIdCase = 20;

    /// <summary>
    ///  Writes the Enter Order Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Enter Order Appendage.
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
        int enterOrderOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int enterOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.EnterOrderAppendage_OptionalFieldLength_Used = false;
        cursor.EnterOrderAppendage_EnterOrderOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (enterOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enterOrderOptionalValueWriterStart = cursor.Index;
        int enterOrderOptionalValueWriting = cursor.Index;
        var enterOrderOptionalValueWriteValue = value.EnterOrderOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Write(bytes, ref cursor, enterOrderOptionalValueWriting, ref enterOrderOptionalValueWriterCase, in enterOrderOptionalValueWriteValue);
        if (enterOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) enterOrderOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromEnterOrderAppendage, start, cursor.Index);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueFirmCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueMinQtyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == CustomerTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueMaxFloorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule3(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValuePriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule4(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValuePegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule5(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueDiscretionPriceCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule6(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueDiscretionPriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule7(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueDiscretionPegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule8(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValuePostOnlyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule9(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueRandomReservesCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule10(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueExpireTimeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule11(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueTradeNowCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule12(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueHandleInstCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule13(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule14(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueSharesLocatedCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule15(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueLocateBrokerCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule16(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule17(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueAiqStrategyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule18(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        if (enterOrderOptionalValueWriterCase == EnterOrderOptionalValueAiqGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueBranchRule19(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.EnterOrderAppendageWriter.EnterOrderOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + enterOrderOptionalFieldDependencyWriteDistanceFromEnterOrderAppendage, enterOrderOptionalValueWriterStart, enterOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : enterOrderOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
