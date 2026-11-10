using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order Priority Update Appendage
/// </summary>
public static class OrderPriorityUpdateAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalFieldField OrderPriorityUpdateOptionalField;
    }

    /// <summary>
    ///  Reads the Order Priority Update Appendage in place.
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
        ///  Gets the bytes of the Order Priority Update Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Priority Update Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly OrderPriorityUpdateOptionalFieldField OrderPriorityUpdateOptionalField => ref this.layout.OrderPriorityUpdateOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public OrderPriorityUpdateOptionalValue.Reader OrderPriorityUpdateOptionalValue
        {
            get
            {
                int orderPriorityUpdateOptionalValueReaderPosition = Parser.Valid(Parser.OrderPriorityUpdateAppendageOrderPriorityUpdateOptionalValueStart(this.bytes, this.start, this.end, this.start, out var orderPriorityUpdateOptionalFieldValue));
                return new(this.bytes, orderPriorityUpdateOptionalValueReaderPosition, this.end, orderPriorityUpdateOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Order Priority Update Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Value OrderPriorityUpdateOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Order Priority Update Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderPriorityUpdateOptionalField;
    private const int orderPriorityUpdateAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderPriorityUpdateAppendage = 0;
    private const int orderPriorityUpdateOptionalFieldDependencyWriteDistanceFromOrderPriorityUpdateAppendage = 1;
    private const int OrderPriorityUpdateOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Order Priority Update Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Priority Update Appendage.
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
        int orderPriorityUpdateOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderPriorityUpdateOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.OrderPriorityUpdateAppendage_OptionalFieldLength_Used = false;
        cursor.OrderPriorityUpdateAppendage_OrderPriorityUpdateOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (orderPriorityUpdateOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateOptionalValueWriterStart = cursor.Index;
        int orderPriorityUpdateOptionalValueWriting = cursor.Index;
        var orderPriorityUpdateOptionalValueWriteValue = value.OrderPriorityUpdateOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Write(bytes, ref cursor, orderPriorityUpdateOptionalValueWriting, ref orderPriorityUpdateOptionalValueWriterCase, in orderPriorityUpdateOptionalValueWriteValue);
        if (orderPriorityUpdateOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderPriorityUpdateOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderPriorityUpdateAppendageWriter.OrderPriorityUpdateAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderPriorityUpdateAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderPriorityUpdateAppendage, start, cursor.Index);
        if (orderPriorityUpdateOptionalValueWriterCase == OrderPriorityUpdateOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderPriorityUpdateAppendageWriter.OrderPriorityUpdateOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderPriorityUpdateOptionalFieldDependencyWriteDistanceFromOrderPriorityUpdateAppendage, orderPriorityUpdateOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderPriorityUpdateAppendageWriter.OrderPriorityUpdateOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderPriorityUpdateOptionalFieldDependencyWriteDistanceFromOrderPriorityUpdateAppendage, orderPriorityUpdateOptionalValueWriterStart, orderPriorityUpdateOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : orderPriorityUpdateOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
