using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp.v1_56;

/// <summary>
///  Tops broadcasts a real-time Quote Update Message each time IEX's best bid or offer quotation is updated during the trading day
/// </summary>
public static class QuoteUpdateMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Quote Update Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeQuoteUpdateFlags + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeBidSize + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeBidPrice + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeAskPrice + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeAskSize;

        /// <summary>
        ///  Gets the Quote Update Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Quote Update Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Quote Update Message at the start of the buffer, writes its fixed values, and gets the Quote Update Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Quote Update Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Quote Update Flags
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateFlags QuoteUpdateFlags;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.Symbol Symbol;
        /// <summary>
        ///  Aggregate quoted best bid size
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.BidSize BidSize;
        /// <summary>
        ///  Best quoted bid price
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.BidPrice BidPrice;
        /// <summary>
        ///  Best quoted ask price
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.AskPrice AskPrice;
        /// <summary>
        ///  Aggregate quoted best ask size
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_56.AskSize AskSize;
    }

    /// <summary>
    ///  The Quote Update Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Quote Update Flags
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.QuoteUpdateFlags QuoteUpdateFlags { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.Symbol Symbol { get; init; }

        /// <summary>
        ///  Aggregate quoted best bid size
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.BidSize BidSize { get; init; }

        /// <summary>
        ///  Best quoted bid price
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.BidPrice BidPrice { get; init; }

        /// <summary>
        ///  Best quoted ask price
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.AskPrice AskPrice { get; init; }

        /// <summary>
        ///  Aggregate quoted best ask size
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_56.AskSize AskSize { get; init; }

        /// <summary>
        ///  Creates the value of the Quote Update Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeQuoteUpdateFlags + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeBidSize + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeBidPrice + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeAskPrice + global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.SizeAskSize;

    /// <summary>
    ///  Writes the Quote Update Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Quote Update Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterNoRoom, global::Iex.IexEquities.Tops.IexTp.v1_56.Parser.WriterDestination);
        ref Layout quoteUpdateFlagsWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        quoteUpdateFlagsWriterLayout.QuoteUpdateFlags = value.QuoteUpdateFlags;
        quoteUpdateFlagsWriterLayout.Timestamp = value.Timestamp;
        quoteUpdateFlagsWriterLayout.Symbol = value.Symbol;
        quoteUpdateFlagsWriterLayout.BidSize = value.BidSize;
        quoteUpdateFlagsWriterLayout.BidPrice = value.BidPrice;
        quoteUpdateFlagsWriterLayout.AskPrice = value.AskPrice;
        quoteUpdateFlagsWriterLayout.AskSize = value.AskSize;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
