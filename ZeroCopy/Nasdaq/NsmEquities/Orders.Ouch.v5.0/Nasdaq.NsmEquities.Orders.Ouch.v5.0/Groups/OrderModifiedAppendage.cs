using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Order Modified Appendage
/// </summary>
public static class OrderModifiedAppendage
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
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedAppendage.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OptionalFieldLength OptionalFieldLength;
        /// <summary>
        ///  Appendage Id
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalFieldField OrderModifiedOptionalField;
    }

    /// <summary>
    ///  Reads the Order Modified Appendage in place.
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
        ///  Gets the bytes of the Order Modified Appendage.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Order Modified Appendage.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Appendage Length Type
        /// </summary>
        public ref readonly OptionalFieldLength OptionalFieldLength => ref this.layout.OptionalFieldLength;

        /// <summary>
        ///  Appendage Id
        /// </summary>
        public ref readonly OrderModifiedOptionalFieldField OrderModifiedOptionalField => ref this.layout.OrderModifiedOptionalField;

        /// <summary>
        ///  Branch
        /// </summary>
        public OrderModifiedOptionalValue.Reader OrderModifiedOptionalValue
        {
            get
            {
                int orderModifiedOptionalValueReaderPosition = Parser.Valid(Parser.OrderModifiedAppendageOrderModifiedOptionalValueStart(this.bytes, this.start, this.end, this.start, out var orderModifiedOptionalFieldValue));
                return new(this.bytes, orderModifiedOptionalValueReaderPosition, this.end, orderModifiedOptionalFieldValue);
            }
        }
    }

    /// <summary>
    ///  The Order Modified Appendage fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Branch
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Value OrderModifiedOptionalValue { get; init; } = new();

        /// <summary>
        ///  Creates the value of the Order Modified Appendage, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOptionalFieldLength + global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeOrderModifiedOptionalField;
    private const int orderModifiedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderModifiedAppendage = 0;
    private const int orderModifiedOptionalFieldDependencyWriteDistanceFromOrderModifiedAppendage = 1;
    private const int OrderModifiedOptionalValueUserRefIdxCase = 1;
    private const int OrderModifiedOptionalValueSharesLocatedCase = 2;
    private const int LocateBrokerCase = 3;

    /// <summary>
    ///  Writes the Order Modified Appendage to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Modified Appendage.
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
        int orderModifiedOptionalValueWriterStart = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        int orderModifiedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
        cursor.OrderModifiedAppendage_OptionalFieldLength_Used = false;
        cursor.OrderModifiedAppendage_OrderModifiedOptionalField_Used = false;
        cursor.Index += LayoutSize;
        if (orderModifiedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderModifiedOptionalValueWriterStart = cursor.Index;
        int orderModifiedOptionalValueWriting = cursor.Index;
        var orderModifiedOptionalValueWriteValue = value.OrderModifiedOptionalValue;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Write(bytes, ref cursor, orderModifiedOptionalValueWriting, ref orderModifiedOptionalValueWriterCase, in orderModifiedOptionalValueWriteValue);
        if (orderModifiedOptionalValueWriterStart == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent) orderModifiedOptionalValueWriterStart = cursor.Index;
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderModifiedAppendageWriter.OrderModifiedAppendageSizeRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderModifiedAppendageOptionalFieldLengthDependencyWriteDistanceFromOrderModifiedAppendage, start, cursor.Index);
        if (orderModifiedOptionalValueWriterCase == OrderModifiedOptionalValueUserRefIdxCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderModifiedAppendageWriter.OrderModifiedOptionalValueBranchRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderModifiedOptionalFieldDependencyWriteDistanceFromOrderModifiedAppendage, orderModifiedOptionalValueWriterStart);
        if (orderModifiedOptionalValueWriterCase == OrderModifiedOptionalValueSharesLocatedCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderModifiedAppendageWriter.OrderModifiedOptionalValueBranchRule1(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderModifiedOptionalFieldDependencyWriteDistanceFromOrderModifiedAppendage, orderModifiedOptionalValueWriterStart);
        if (orderModifiedOptionalValueWriterCase == LocateBrokerCase) global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderModifiedAppendageWriter.OrderModifiedOptionalValueBranchRule2(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderModifiedOptionalFieldDependencyWriteDistanceFromOrderModifiedAppendage, orderModifiedOptionalValueWriterStart);
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.OrderModifiedAppendageWriter.OrderModifiedOptionalValueCases(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + orderModifiedOptionalFieldDependencyWriteDistanceFromOrderModifiedAppendage, orderModifiedOptionalValueWriterStart, orderModifiedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : orderModifiedOptionalValueWriterCase);
        return bytes[start..cursor.Index];
    }
}
