using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  A level
/// </summary>
public static class BidAskPair
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Bid Ask Pair in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidPx + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskPx + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidSz + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskSz + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidCt + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskCt;

        /// <summary>
        ///  Gets the Bid Ask Pair laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Bid Ask Pair.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Bid Ask Pair at the start of the buffer, writes its fixed values, and gets the Bid Ask Pair laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Bid Ask Pair.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The bid price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.BidPx BidPx;
        /// <summary>
        ///  The ask price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AskPx AskPx;
        /// <summary>
        ///  The bid size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.BidSz BidSz;
        /// <summary>
        ///  The ask size
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AskSz AskSz;
        /// <summary>
        ///  The number of bid orders
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.BidCt BidCt;
        /// <summary>
        ///  The number of ask orders
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AskCt AskCt;
    }

    /// <summary>
    ///  The Bid Ask Pair fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The bid price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.BidPx BidPx { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.BidPx.Absent;

        /// <summary>
        ///  The ask price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AskPx AskPx { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.AskPx.Absent;

        /// <summary>
        ///  The bid size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.BidSz BidSz { get; init; }

        /// <summary>
        ///  The ask size
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.AskSz AskSz { get; init; }

        /// <summary>
        ///  The number of bid orders
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.BidCt BidCt { get; init; }

        /// <summary>
        ///  The number of ask orders
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.AskCt AskCt { get; init; }

        /// <summary>
        ///  Creates the value of the Bid Ask Pair, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidPx + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskPx + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidSz + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskSz + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeBidCt + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAskCt;

    /// <summary>
    ///  Writes the Bid Ask Pair to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Bid Ask Pair.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterDestination);
        ref Layout bboMsgBidAskPairBidPxWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        bboMsgBidAskPairBidPxWriterLayout.BidPx = value.BidPx;
        bboMsgBidAskPairBidPxWriterLayout.AskPx = value.AskPx;
        bboMsgBidAskPairBidPxWriterLayout.BidSz = value.BidSz;
        bboMsgBidAskPairBidPxWriterLayout.AskSz = value.AskSz;
        bboMsgBidAskPairBidPxWriterLayout.BidCt = value.BidCt;
        bboMsgBidAskPairBidPxWriterLayout.AskCt = value.AskCt;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
