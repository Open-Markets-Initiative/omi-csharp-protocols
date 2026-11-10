using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  This message will be generated for attributed orders and quotations accepted by the NASDAQ PSX system.
/// </summary>
public static class AddOrderWithMpidAttributionMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Add Order With Mpid Attribution Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeBuySellIndicator + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeShares + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizePrice + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeAttribution;

        /// <summary>
        ///  Gets the Add Order With Mpid Attribution Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Add Order With Mpid Attribution Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Add Order With Mpid Attribution Message at the start of the buffer, writes its fixed values, and gets the Add Order With Mpid Attribution Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Add Order With Mpid Attribution Message.
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
        ///  The type of order being added.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.BuySellIndicatorField BuySellIndicator;
        /// <summary>
        ///  The total number of shares associated with the order being added to the book.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Shares Shares;
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Stock Stock;
        /// <summary>
        ///  The display price of the new order. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Price Price;
        /// <summary>
        ///  NASDAQ market participant identifier associated with the entered order.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Attribution Attribution;
    }

    /// <summary>
    ///  The Add Order With Mpid Attribution Message fields to write.
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
        ///  The type of order being added.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.BuySellIndicatorField BuySellIndicator { get; init; }

        /// <summary>
        ///  The total number of shares associated with the order being added to the book.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Shares Shares { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Stock Stock { get; init; }

        /// <summary>
        ///  The display price of the new order. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Price Price { get; init; }

        /// <summary>
        ///  NASDAQ market participant identifier associated with the entered order.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Attribution Attribution { get; init; }

        /// <summary>
        ///  Creates the value of the Add Order With Mpid Attribution Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeOrderReferenceNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeBuySellIndicator + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeShares + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizePrice + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeAttribution;

    /// <summary>
    ///  Writes the Add Order With Mpid Attribution Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Add Order With Mpid Attribution Message.
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
        ref Layout addOrderWithMpidAttributionMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.OrderReferenceNumber = value.OrderReferenceNumber;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.BuySellIndicator = value.BuySellIndicator;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.Shares = value.Shares;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.Stock = value.Stock;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.Price = value.Price;
        addOrderWithMpidAttributionMessageStockLocateWriterLayout.Attribution = value.Attribution;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
