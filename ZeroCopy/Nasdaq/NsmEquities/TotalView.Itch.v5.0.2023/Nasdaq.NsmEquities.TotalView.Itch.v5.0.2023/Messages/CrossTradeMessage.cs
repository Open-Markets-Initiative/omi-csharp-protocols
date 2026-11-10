using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Cross Trade message indicates that Nasdaq has completed its cross process for a specific security
/// </summary>
public static class CrossTradeMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Cross Trade Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMatchNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossType;

        /// <summary>
        ///  Gets the Cross Trade Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Cross Trade Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Cross Trade Message at the start of the buffer, writes its fixed values, and gets the Cross Trade Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Cross Trade Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockLocate StockLocate;
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Timestamp Timestamp;
        /// <summary>
        ///  The number of shares matched in the Nasdaq Cross
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossShares CrossShares;
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock;
        /// <summary>
        ///  The price at which the cross occurred
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossPrice CrossPrice;
        /// <summary>
        ///  The Nasdaq generated day-unique Match Number of this execution
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MatchNumber MatchNumber;
        /// <summary>
        ///  The Nasdaq cross session for which the message is being generated
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTypeField CrossType;
    }

    /// <summary>
    ///  The Cross Trade Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The number of shares matched in the Nasdaq Cross
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossShares CrossShares { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock { get; init; }

        /// <summary>
        ///  The price at which the cross occurred
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossPrice CrossPrice { get; init; }

        /// <summary>
        ///  The Nasdaq generated day-unique Match Number of this execution
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MatchNumber MatchNumber { get; init; }

        /// <summary>
        ///  The Nasdaq cross session for which the message is being generated
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTypeField CrossType { get; init; }

        /// <summary>
        ///  Creates the value of the Cross Trade Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMatchNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossType;

    /// <summary>
    ///  Writes the Cross Trade Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cross Trade Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        ref Layout crossTradeMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        crossTradeMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        crossTradeMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        crossTradeMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        crossTradeMessageStockLocateWriterLayout.CrossShares = value.CrossShares;
        crossTradeMessageStockLocateWriterLayout.Stock = value.Stock;
        crossTradeMessageStockLocateWriterLayout.CrossPrice = value.CrossPrice;
        crossTradeMessageStockLocateWriterLayout.MatchNumber = value.MatchNumber;
        crossTradeMessageStockLocateWriterLayout.CrossType = value.CrossType;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
