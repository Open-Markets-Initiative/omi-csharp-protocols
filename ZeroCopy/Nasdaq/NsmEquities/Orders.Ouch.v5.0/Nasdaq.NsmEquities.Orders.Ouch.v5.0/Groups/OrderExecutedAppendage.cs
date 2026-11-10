using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order Executed Appendage
/// </summary>
public static class OrderExecutedAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalFieldField OrderExecutedOptionalField;
    }

    /// <summary>
    ///  Reads the Order Executed Appendage in place.
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
        ///  Gets the bytes of the Order Executed Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Executed Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly OrderExecutedOptionalFieldField OrderExecutedOptionalField => ref this.layout.OrderExecutedOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public OrderExecutedOptionalValue.Reader OrderExecutedOptionalValue
        {
            get
            {
                int orderExecutedOptionalValueReaderPosition = Parser.Valid(Parser.OrderExecutedAppendageOrderExecutedOptionalValueStart(this.bytes, this.start, this.end, this.start, out var orderExecutedOptionalFieldValue));
                return new(this.bytes, orderExecutedOptionalValueReaderPosition, this.end, orderExecutedOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Order Executed Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Value OrderExecutedOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Order Executed Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderExecutedOptionalField;
    private const int orderExecutedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderExecutedAppendage = 0;
    private const int orderExecutedOptionalFieldDependencyWriteDistanceFromOrderExecutedAppendage = 1;
    private const int OrderExecutedOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Order Executed Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Executed Appendage.
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
        int orderExecutedOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderExecutedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.OrderExecutedAppendage_OptionalFieldLength_Used = false;
        cursor.OrderExecutedAppendage_OrderExecutedOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (orderExecutedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderExecutedOptionalValueWriterStart = cursor.Index;
        int orderExecutedOptionalValueWriting = cursor.Index;
        var orderExecutedOptionalValueWriteValue = value.OrderExecutedOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Write(bytes, ref cursor, orderExecutedOptionalValueWriting, ref orderExecutedOptionalValueWriterCase, in orderExecutedOptionalValueWriteValue);
        if (orderExecutedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderExecutedOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderExecutedAppendageWriter.OrderExecutedAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderExecutedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderExecutedAppendage, start, cursor.Index);
        if (orderExecutedOptionalValueWriterCase == OrderExecutedOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderExecutedAppendageWriter.OrderExecutedOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderExecutedOptionalFieldDependencyWriteDistanceFromOrderExecutedAppendage, orderExecutedOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderExecutedAppendageWriter.OrderExecutedOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderExecutedOptionalFieldDependencyWriteDistanceFromOrderExecutedAppendage, orderExecutedOptionalValueWriterStart, orderExecutedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : orderExecutedOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
