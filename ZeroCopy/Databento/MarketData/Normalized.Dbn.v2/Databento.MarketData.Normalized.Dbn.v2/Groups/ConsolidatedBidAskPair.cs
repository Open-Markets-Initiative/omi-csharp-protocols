using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  A price level consolidated from multiple venues
/// </summary>
public static class ConsolidatedBidAskPair
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Consolidated Bid Ask Pair in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidPx + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskPx + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidSz + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskSz + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidPb + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeReserved2 + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskPb + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSecondReserved2;

        /// <summary>
        ///  Gets the Consolidated Bid Ask Pair laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Consolidated Bid Ask Pair.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Consolidated Bid Ask Pair at the start of the buffer, writes its fixed values, and gets the Consolidated Bid Ask Pair laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Consolidated Bid Ask Pair.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The bid price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.BidPx BidPx;
        /// <summary>
        ///  The ask price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.AskPx AskPx;
        /// <summary>
        ///  The bid size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.BidSz BidSz;
        /// <summary>
        ///  The ask size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.AskSz AskSz;
        /// <summary>
        ///  The publisher ID indicating the venue containing the best bid
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.BidPb BidPb;
        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Reserved2 Reserved2;
        /// <summary>
        ///  The publisher ID indicating the venue containing the best ask
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.AskPb AskPb;
        /// <summary>
        ///  Reserved, 2 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.SecondReserved2 SecondReserved2;
    }

    /// <summary>
    ///  The Consolidated Bid Ask Pair fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The bid price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.BidPx BidPx { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.BidPx.Absent;

        /// <summary>
        ///  The ask price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.AskPx AskPx { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.AskPx.Absent;

        /// <summary>
        ///  The bid size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.BidSz BidSz { get; init; }

        /// <summary>
        ///  The ask size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.AskSz AskSz { get; init; }

        /// <summary>
        ///  The publisher ID indicating the venue containing the best bid
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.BidPb BidPb { get; init; }

        /// <summary>
        ///  The publisher ID indicating the venue containing the best ask
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.AskPb AskPb { get; init; }

        /// <summary>
        ///  Creates the value of the Consolidated Bid Ask Pair, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidPx + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskPx + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidSz + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskSz + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeBidPb + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeReserved2 + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAskPb + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSecondReserved2;

    /// <summary>
    ///  Writes the Consolidated Bid Ask Pair to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Consolidated Bid Ask Pair.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v2.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterDestination);
        ref Layout cbboMsgConsolidatedBidAskPairBidPxWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.BidPx = value.BidPx;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.AskPx = value.AskPx;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.BidSz = value.BidSz;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.AskSz = value.AskSz;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.BidPb = value.BidPb;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.Reserved2 = default;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.AskPb = value.AskPb;
        cbboMsgConsolidatedBidAskPairBidPxWriterLayout.SecondReserved2 = default;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
