using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
/// </summary>
public static class OrderCancelMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Order Cancel Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCancelledShares;

        /// <summary>
        ///  Gets the Order Cancel Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Order Cancel Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Order Cancel Message at the start of the buffer, writes its fixed values, and gets the Order Cancel Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Order Cancel Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Always 0
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockLocate StockLocate;
        /// <summary>
        ///  Nasdaq Texas internal tracking number
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  The unique reference number assigned to the new order at the time of receipt.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReferenceNumber OrderReferenceNumber;
        /// <summary>
        ///  The number of shares being removed from the display size of the order as a result of a cancellation
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CancelledShares CancelledShares;
    }

    /// <summary>
    ///  The Order Cancel Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Always 0
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  Nasdaq Texas internal tracking number
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The unique reference number assigned to the new order at the time of receipt.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReferenceNumber OrderReferenceNumber { get; init; }

        /// <summary>
        ///  The number of shares being removed from the display size of the order as a result of a cancellation
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CancelledShares CancelledShares { get; init; }

        /// <summary>
        ///  Creates the value of the Order Cancel Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCancelledShares;

    /// <summary>
    ///  Writes the Order Cancel Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Cancel Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterDestination);
        ref Layout orderCancelMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        orderCancelMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        orderCancelMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        orderCancelMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        orderCancelMessageStockLocateWriterLayout.OrderReferenceNumber = value.OrderReferenceNumber;
        orderCancelMessageStockLocateWriterLayout.CancelledShares = value.CancelledShares;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
