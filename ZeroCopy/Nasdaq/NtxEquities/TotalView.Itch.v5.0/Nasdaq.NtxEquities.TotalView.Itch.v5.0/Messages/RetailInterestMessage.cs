using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates price improving interest of the Bid, Ask or both the Bid and Ask for a security.
/// </summary>
public static class RetailInterestMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Retail Interest Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeInterestFlag;

        /// <summary>
        ///  Gets the Retail Interest Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Retail Interest Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Retail Interest Message at the start of the buffer, writes its fixed values, and gets the Retail Interest Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Retail Interest Message.
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
        ///  Denotes the security symbol for the issue in the Nasdaq Texas execution system.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Stock Stock;
        /// <summary>
        ///  Indicates the type of price improvement available
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.InterestFlagField InterestFlag;
    }

    /// <summary>
    ///  The Retail Interest Message fields to write.
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
        ///  Denotes the security symbol for the issue in the Nasdaq Texas execution system.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Stock Stock { get; init; }

        /// <summary>
        ///  Indicates the type of price improvement available
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.InterestFlagField InterestFlag { get; init; }

        /// <summary>
        ///  Creates the value of the Retail Interest Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeInterestFlag;

    /// <summary>
    ///  Writes the Retail Interest Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Retail Interest Message.
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
        ref Layout retailInterestMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        retailInterestMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        retailInterestMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        retailInterestMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        retailInterestMessageStockLocateWriterLayout.Stock = value.Stock;
        retailInterestMessageStockLocateWriterLayout.InterestFlag = value.InterestFlag;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
