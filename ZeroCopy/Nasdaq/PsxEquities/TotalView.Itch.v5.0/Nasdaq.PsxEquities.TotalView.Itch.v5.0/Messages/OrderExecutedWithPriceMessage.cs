using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price. Since the execution price is different than the display price of the original Add Order, Nasdaq PSX includes a price field within this execution message.
/// </summary>
public static class OrderExecutedWithPriceMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Order Executed With Price Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeExecutedShares + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeMatchNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizePrintable + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeExecutionPrice;

        /// <summary>
        ///  Gets the Order Executed With Price Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Order Executed With Price Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Order Executed With Price Message at the start of the buffer, writes its fixed values, and gets the Order Executed With Price Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Order Executed With Price Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination);
        }

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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Always 0
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.StockLocate StockLocate;
        /// <summary>
        ///  NASDAQ PSX internal tracking number
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  The unique reference number assigned to the new order at the time of receipt.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderReferenceNumber OrderReferenceNumber;
        /// <summary>
        ///  The number of shares executed.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.ExecutedShares ExecutedShares;
        /// <summary>
        ///  The Nasdaq PSX generated day unique Match Number of this execution
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MatchNumber MatchNumber;
        /// <summary>
        ///  Indicates if the execution should be reflected on time and sale displays and volume calculations.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.PrintableField Printable;
        /// <summary>
        ///  The price at which the order execution occurred. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.ExecutionPrice ExecutionPrice;
    }

    /// <summary>
    ///  The Order Executed With Price Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Always 0
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  NASDAQ PSX internal tracking number
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The unique reference number assigned to the new order at the time of receipt.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.OrderReferenceNumber OrderReferenceNumber { get; init; }

        /// <summary>
        ///  The number of shares executed.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.ExecutedShares ExecutedShares { get; init; }

        /// <summary>
        ///  The Nasdaq PSX generated day unique Match Number of this execution
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.MatchNumber MatchNumber { get; init; }

        /// <summary>
        ///  Indicates if the execution should be reflected on time and sale displays and volume calculations.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.PrintableField Printable { get; init; }

        /// <summary>
        ///  The price at which the order execution occurred. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.ExecutionPrice ExecutionPrice { get; init; }

        /// <summary>
        ///  Creates the value of the Order Executed With Price Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeExecutedShares + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeMatchNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizePrintable + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeExecutionPrice;

    /// <summary>
    ///  Writes the Order Executed With Price Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Executed With Price Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.WriterDestination);
        ref Layout orderExecutedWithPriceMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        orderExecutedWithPriceMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        orderExecutedWithPriceMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        orderExecutedWithPriceMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        orderExecutedWithPriceMessageStockLocateWriterLayout.OrderReferenceNumber = value.OrderReferenceNumber;
        orderExecutedWithPriceMessageStockLocateWriterLayout.ExecutedShares = value.ExecutedShares;
        orderExecutedWithPriceMessageStockLocateWriterLayout.MatchNumber = value.MatchNumber;
        orderExecutedWithPriceMessageStockLocateWriterLayout.Printable = value.Printable;
        orderExecutedWithPriceMessageStockLocateWriterLayout.ExecutionPrice = value.ExecutionPrice;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
