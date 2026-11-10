using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Subsampled consolidated market by price with a known book depth of 1; the record of the cbbo-1s and cbbo-1m schemas
/// </summary>
public static class CbboMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Cbbo Msg in bytes.
        /// </summary>
        public const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSecondReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved8 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeConsolidatedBidAskPair;

        /// <summary>
        ///  Gets the Cbbo Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Cbbo Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Cbbo Msg at the start of the buffer, writes its fixed values, and gets the Cbbo Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Cbbo Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoRoom, nameof(destination));
            destination[..LayoutSize].Clear();
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Price Price;
        /// <summary>
        ///  The order quantity
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Size Size;
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Reserved1 Reserved1;
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SideField Side;
        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Flags Flags;
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SecondReserved1 SecondReserved1;
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv;
        /// <summary>
        ///  Reserved, 8 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Reserved8 Reserved8;
        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Layout ConsolidatedBidAskPair;
    }

    /// <summary>
    ///  The Cbbo Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Price Price { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.Price.Absent;

        /// <summary>
        ///  The order quantity
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.Size Size { get; init; }

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.SideField Side { get; init; }

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.Flags Flags { get; init; }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.TsRecv.Absent;

        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Value ConsolidatedBidAskPair { get; init; }

        /// <summary>
        ///  Creates the value of the Cbbo Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSecondReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved8 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeConsolidatedBidAskPair;

    /// <summary>
    ///  Writes the Cbbo Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cbbo Msg.
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
        ref Layout cbboMsgPriceWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        cbboMsgPriceWriterLayout.Price = value.Price;
        cbboMsgPriceWriterLayout.Size = value.Size;
        cbboMsgPriceWriterLayout.Reserved1 = default;
        cbboMsgPriceWriterLayout.Side = value.Side;
        cbboMsgPriceWriterLayout.Flags = value.Flags;
        cbboMsgPriceWriterLayout.SecondReserved1 = default;
        cbboMsgPriceWriterLayout.TsRecv = value.TsRecv;
        cbboMsgPriceWriterLayout.Reserved8 = default;
        cursor.Index += global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSecondReserved1 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved8;
        int cbboMsgConsolidatedBidAskPairWriting = cursor.Index;
        var cbboMsgConsolidatedBidAskPairWriteValue = value.ConsolidatedBidAskPair;
        global::Databento.MarketData.Normalized.Dbn.v1.ConsolidatedBidAskPair.Write(bytes, ref cursor, cbboMsgConsolidatedBidAskPairWriting, in cbboMsgConsolidatedBidAskPairWriteValue);
        return bytes[start..cursor.Index];
    }
}
