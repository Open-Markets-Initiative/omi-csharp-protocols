using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Replace Order Appendage
/// </summary>
public static class ReplaceOrderAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalFieldField ReplaceOrderOptionalField;
    }

    /// <summary>
    ///  Reads the Replace Order Appendage in place.
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
        ///  Gets the bytes of the Replace Order Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Replace Order Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly ReplaceOrderOptionalFieldField ReplaceOrderOptionalField => ref this.layout.ReplaceOrderOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public ReplaceOrderOptionalValue.Reader ReplaceOrderOptionalValue
        {
            get
            {
                int replaceOrderOptionalValueReaderPosition = Parser.Valid(Parser.ReplaceOrderAppendageReplaceOrderOptionalValueStart(this.bytes, this.start, this.end, this.start, out var replaceOrderOptionalFieldValue));
                return new(this.bytes, replaceOrderOptionalValueReaderPosition, this.end, replaceOrderOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Replace Order Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Value ReplaceOrderOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Replace Order Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeReplaceOrderOptionalField;
    private const int replaceOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromReplaceOrderAppendage = 0;
    private const int replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage = 1;
    private const int ReplaceOrderOptionalValueMinQtyCase = 1;
    private const int ReplaceOrderOptionalValueMaxFloorCase = 2;
    private const int ReplaceOrderOptionalValuePriceTypeCase = 3;
    private const int ReplaceOrderOptionalValuePostOnlyCase = 4;
    private const int ReplaceOrderOptionalValueExpireTimeCase = 5;
    private const int ReplaceOrderOptionalValueTradeNowCase = 6;
    private const int ReplaceOrderOptionalValueHandleInstCase = 7;
    private const int ReplaceOrderOptionalValueRandomReservesCase = 8;
    private const int ReplaceOrderOptionalValuePegOffsetCase = 9;
    private const int ReplaceOrderOptionalValueDiscretionPriceCase = 10;
    private const int ReplaceOrderOptionalValueDiscretionPriceTypeCase = 11;
    private const int ReplaceOrderOptionalValueDiscretionPegOffsetCase = 12;
    private const int ReplaceOrderOptionalValueSharesLocatedCase = 13;
    private const int ReplaceOrderOptionalValueLocateBrokerCase = 14;
    private const int ReplaceOrderOptionalValueUserRefIdxCase = 15;
    private const int ReplaceOrderOptionalValueSideCase = 16;
    private const int ReplaceOrderOptionalValueAiqStrategyCase = 17;
    private const int ReplaceOrderOptionalValueAiqGroupIdCase = 18;

    /// <summary>
    ///  Writes the Replace Order Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replace Order Appendage.
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
        int replaceOrderOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replaceOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.ReplaceOrderAppendage_OptionalFieldLength_Used = false;
        cursor.ReplaceOrderAppendage_ReplaceOrderOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (replaceOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replaceOrderOptionalValueWriterStart = cursor.Index;
        int replaceOrderOptionalValueWriting = cursor.Index;
        var replaceOrderOptionalValueWriteValue = value.ReplaceOrderOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Write(bytes, ref cursor, replaceOrderOptionalValueWriting, ref replaceOrderOptionalValueWriterCase, in replaceOrderOptionalValueWriteValue);
        if (replaceOrderOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replaceOrderOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderAppendageOptionalFieldLengthDependencyWriteDistanceFromReplaceOrderAppendage, start, cursor.Index);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueMinQtyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueMaxFloorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValuePriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValuePostOnlyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule3(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueExpireTimeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule4(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueTradeNowCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule5(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueHandleInstCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule6(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueRandomReservesCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule7(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValuePegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule8(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueDiscretionPriceCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule9(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueDiscretionPriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule10(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueDiscretionPegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule11(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueSharesLocatedCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule12(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueLocateBrokerCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule13(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule14(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueSideCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule15(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueAiqStrategyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule16(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        if (replaceOrderOptionalValueWriterCase == ReplaceOrderOptionalValueAiqGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueBranchRule17(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplaceOrderAppendageWriter.ReplaceOrderOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replaceOrderOptionalFieldDependencyWriteDistanceFromReplaceOrderAppendage, replaceOrderOptionalValueWriterStart, replaceOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : replaceOrderOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
