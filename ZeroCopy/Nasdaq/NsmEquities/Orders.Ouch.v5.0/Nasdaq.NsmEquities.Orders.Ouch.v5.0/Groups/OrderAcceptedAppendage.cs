using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order Accepted Appendage
/// </summary>
public static class OrderAcceptedAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalFieldField OrderAcceptedOptionalField;
    }

    /// <summary>
    ///  Reads the Order Accepted Appendage in place.
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
        ///  Gets the bytes of the Order Accepted Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Accepted Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly OrderAcceptedOptionalFieldField OrderAcceptedOptionalField => ref this.layout.OrderAcceptedOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public OrderAcceptedOptionalValue.Reader OrderAcceptedOptionalValue
        {
            get
            {
                int orderAcceptedOptionalValueReaderPosition = Parser.Valid(Parser.OrderAcceptedAppendageOrderAcceptedOptionalValueStart(this.bytes, this.start, this.end, this.start, out var orderAcceptedOptionalFieldValue));
                return new(this.bytes, orderAcceptedOptionalValueReaderPosition, this.end, orderAcceptedOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Order Accepted Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Value OrderAcceptedOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Order Accepted Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderAcceptedOptionalField;
    private const int orderAcceptedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderAcceptedAppendage = 0;
    private const int orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage = 1;
    private const int OrderAcceptedOptionalValueFirmCase = 1;
    private const int OrderAcceptedOptionalValueMinQtyCase = 2;
    private const int CustomerTypeCase = 3;
    private const int OrderAcceptedOptionalValueMaxFloorCase = 4;
    private const int OrderAcceptedOptionalValuePriceTypeCase = 5;
    private const int PegOffsetCase = 6;
    private const int DiscretionPriceCase = 7;
    private const int DiscretionPriceTypeCase = 8;
    private const int DiscretionPegOffsetCase = 9;
    private const int OrderAcceptedOptionalValuePostOnlyCase = 10;
    private const int RandomReservesCase = 11;
    private const int RouteCase = 12;
    private const int OrderAcceptedOptionalValueExpireTimeCase = 13;
    private const int OrderAcceptedOptionalValueTradeNowCase = 14;
    private const int OrderAcceptedOptionalValueHandleInstCase = 15;
    private const int OrderAcceptedOptionalValueBboWeightIndicatorCase = 16;
    private const int OrderAcceptedOptionalValueGroupIdCase = 17;
    private const int OrderAcceptedOptionalValueSharesLocatedCase = 18;
    private const int OrderAcceptedOptionalValueUserRefIdxCase = 19;
    private const int OrderAcceptedOptionalValueAiqStrategyCase = 20;
    private const int OrderAcceptedOptionalValueAiqGroupIdCase = 21;

    /// <summary>
    ///  Writes the Order Accepted Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Accepted Appendage.
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
        int orderAcceptedOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderAcceptedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.OrderAcceptedAppendage_OptionalFieldLength_Used = false;
        cursor.OrderAcceptedAppendage_OrderAcceptedOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (orderAcceptedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderAcceptedOptionalValueWriterStart = cursor.Index;
        int orderAcceptedOptionalValueWriting = cursor.Index;
        var orderAcceptedOptionalValueWriteValue = value.OrderAcceptedOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Write(bytes, ref cursor, orderAcceptedOptionalValueWriting, ref orderAcceptedOptionalValueWriterCase, in orderAcceptedOptionalValueWriteValue);
        if (orderAcceptedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderAcceptedOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderAcceptedAppendage, start, cursor.Index);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueFirmCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueMinQtyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == CustomerTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueMaxFloorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule3(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValuePriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule4(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == PegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule5(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == DiscretionPriceCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule6(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == DiscretionPriceTypeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule7(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == DiscretionPegOffsetCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule8(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValuePostOnlyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule9(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == RandomReservesCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule10(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == RouteCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule11(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueExpireTimeCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule12(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueTradeNowCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule13(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueHandleInstCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule14(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueBboWeightIndicatorCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule15(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule16(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueSharesLocatedCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule17(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule18(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueAiqStrategyCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule19(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        if (orderAcceptedOptionalValueWriterCase == OrderAcceptedOptionalValueAiqGroupIdCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueBranchRule20(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderAcceptedAppendageWriter.OrderAcceptedOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderAcceptedOptionalFieldDependencyWriteDistanceFromOrderAcceptedAppendage, orderAcceptedOptionalValueWriterStart, orderAcceptedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : orderAcceptedOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
