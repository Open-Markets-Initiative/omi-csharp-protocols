using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  Market by price implementation with a book depth of 0; equivalent to MBP-0, the record of the trades schema
/// </summary>
public static class TradeMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Trade Msg in bytes.
        /// </summary>
        public const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeDepth + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSequence;

        /// <summary>
        ///  Gets the Trade Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Trade Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Trade Msg at the start of the buffer, writes its fixed values, and gets the Trade Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Trade Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.WriterNoRoom, nameof(destination));
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v2.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Price Price;
        /// <summary>
        ///  The order quantity
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Size Size;
        /// <summary>
        ///  The event action
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.ActionField Action;
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.SideField Side;
        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Flags Flags;
        /// <summary>
        ///  The book level where the update event occurred
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Depth Depth;
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsRecv TsRecv;
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsInDelta TsInDelta;
        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Sequence Sequence;
    }

    /// <summary>
    ///  The Trade Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.Price Price { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.Price.Absent;

        /// <summary>
        ///  The order quantity
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Size Size { get; init; }

        /// <summary>
        ///  The event action
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.ActionField Action { get; init; }

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.SideField Side { get; init; }

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Flags Flags { get; init; }

        /// <summary>
        ///  The book level where the update event occurred
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Depth Depth { get; init; }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v2.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v2.TsRecv.Absent;

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.TsInDelta TsInDelta { get; init; }

        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v2.Sequence Sequence { get; init; }

        /// <summary>
        ///  Creates the value of the Trade Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeDepth + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v2.Parser.SizeSequence;

    /// <summary>
    ///  Writes the Trade Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Trade Msg.
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
        ref Layout tradeMsgPriceWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        tradeMsgPriceWriterLayout.Price = value.Price;
        tradeMsgPriceWriterLayout.Size = value.Size;
        tradeMsgPriceWriterLayout.Action = value.Action;
        tradeMsgPriceWriterLayout.Side = value.Side;
        tradeMsgPriceWriterLayout.Flags = value.Flags;
        tradeMsgPriceWriterLayout.Depth = value.Depth;
        tradeMsgPriceWriterLayout.TsRecv = value.TsRecv;
        tradeMsgPriceWriterLayout.TsInDelta = value.TsInDelta;
        tradeMsgPriceWriterLayout.Sequence = value.Sequence;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
