using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Replaced Message Appendage
/// </summary>
public static class ReplacedMessageAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalFieldField ReplacedMessageOptionalField;
    }

    /// <summary>
    ///  Reads the Replaced Message Appendage in place.
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
        ///  Gets the bytes of the Replaced Message Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Replaced Message Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly ReplacedMessageOptionalFieldField ReplacedMessageOptionalField => ref this.layout.ReplacedMessageOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public ReplacedMessageOptionalValue.Reader ReplacedMessageOptionalValue
        {
            get
            {
                int replacedMessageOptionalValueReaderPosition = Parser.Valid(Parser.ReplacedMessageAppendageReplacedMessageOptionalValueStart(this.bytes, this.start, this.end, this.start, out var replacedMessageOptionalFieldValue));
                return new(this.bytes, replacedMessageOptionalValueReaderPosition, this.end, replacedMessageOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Replaced Message Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Value ReplacedMessageOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Replaced Message Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeReplacedMessageOptionalField;
    private const int replacedMessageAppendageOptionalFieldLengthDependencyWriteDistanceFromReplacedMessageAppendage = 0;
    private const int replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage = 1;
    private const int ReplacedMessageOptionalValueFirmCase = 1;
    private const int ReplacedMessageOptionalValueMinQtyCase = 2;
    private const int ReplacedMessageOptionalValueMaxFloorCase = 3;
    private const int ReplacedMessageOptionalValuePriceTypeCase = 4;
    private const int ReplacedMessageOptionalValuePostOnlyCase = 5;
    private const int ReplacedMessageOptionalValueExpireTimeCase = 6;
    private const int ReplacedMessageOptionalValueTradeNowCase = 7;
    private const int ReplacedMessageOptionalValueHandleInstCase = 8;
    private const int ReplacedMessageOptionalValueBboWeightIndicatorCase = 9;
    private const int ReplacedMessageOptionalValueUserRefIdxCase = 10;
    private const int ReplacedMessageOptionalValueAiqStrategyCase = 11;
    private const int ReplacedMessageOptionalValueAiqGroupIdCase = 12;

    /// <summary>
    ///  Writes the Replaced Message Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replaced Message Appendage.
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
        int replacedMessageOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int replacedMessageOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.ReplacedMessageAppendage_OptionalFieldLength_Used = false;
        cursor.ReplacedMessageAppendage_ReplacedMessageOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (replacedMessageOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replacedMessageOptionalValueWriterStart = cursor.Index;
        int replacedMessageOptionalValueWriting = cursor.Index;
        var replacedMessageOptionalValueWriteValue = value.ReplacedMessageOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Write(bytes, ref cursor, replacedMessageOptionalValueWriting, ref replacedMessageOptionalValueWriterCase, in replacedMessageOptionalValueWriteValue);
        if (replacedMessageOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) replacedMessageOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageAppendageOptionalFieldLengthDependencyWriteDistanceFromReplacedMessageAppendage, start, cursor.Index);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueFirmCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueMinQtyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueMaxFloorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValuePriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule3(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValuePostOnlyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule4(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueExpireTimeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule5(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueTradeNowCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule6(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueHandleInstCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule7(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueBboWeightIndicatorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule8(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule9(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueAiqStrategyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule10(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        if (replacedMessageOptionalValueWriterCase == ReplacedMessageOptionalValueAiqGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueBranchRule11(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReplacedMessageAppendageWriter.ReplacedMessageOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + replacedMessageOptionalFieldDependencyWriteDistanceFromReplacedMessageAppendage, replacedMessageOptionalValueWriterStart, replacedMessageOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : replacedMessageOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
