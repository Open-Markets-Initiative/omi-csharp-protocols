using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Nasdaq PSX uses this administrative message to indicate the current trading status of a security to the trading community.
/// </summary>
public static class StockTradingActionMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Stock Trading Action Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTradingState + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeReserved + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeReason;

        /// <summary>
        ///  Gets the Stock Trading Action Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Stock Trading Action Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Stock Trading Action Message at the start of the buffer, writes its fixed values, and gets the Stock Trading Action Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Stock Trading Action Message.
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
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Stock Stock;
        /// <summary>
        ///  Indicates the current trading state for the stock.
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.TradingStateField TradingState;
        /// <summary>
        ///  Reserved
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Reserved Reserved;
        /// <summary>
        ///  Trading Action reason
        /// </summary>
        public global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Reason Reason;
    }

    /// <summary>
    ///  The Stock Trading Action Message fields to write.
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
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Stock Stock { get; init; }

        /// <summary>
        ///  Indicates the current trading state for the stock.
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.TradingStateField TradingState { get; init; }

        /// <summary>
        ///  Reserved
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Reserved Reserved { get; init; }

        /// <summary>
        ///  Trading Action reason
        /// </summary>
        public required global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Reason Reason { get; init; }

        /// <summary>
        ///  Creates the value of the Stock Trading Action Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeTradingState + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeReserved + global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Parser.SizeReason;

    /// <summary>
    ///  Writes the Stock Trading Action Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Stock Trading Action Message.
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
        ref Layout stockTradingActionMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        stockTradingActionMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        stockTradingActionMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        stockTradingActionMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        stockTradingActionMessageStockLocateWriterLayout.Stock = value.Stock;
        stockTradingActionMessageStockLocateWriterLayout.TradingState = value.TradingState;
        stockTradingActionMessageStockLocateWriterLayout.Reserved = value.Reserved;
        stockTradingActionMessageStockLocateWriterLayout.Reason = value.Reason;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
