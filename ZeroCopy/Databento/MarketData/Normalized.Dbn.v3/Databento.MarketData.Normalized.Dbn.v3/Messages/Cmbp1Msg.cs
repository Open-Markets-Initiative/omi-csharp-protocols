using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Consolidated market by price implementation with a known book depth of 1; the record of the cmbp-1 and tcbbo schemas
/// </summary>
public static class Cmbp1Msg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Cmbp 1 Msg in bytes.
        /// </summary>
        public const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved4 + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeConsolidatedBidAskPair;

        /// <summary>
        ///  Gets the Cmbp 1 Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Cmbp 1 Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Cmbp 1 Msg at the start of the buffer, writes its fixed values, and gets the Cmbp 1 Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Cmbp 1 Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, nameof(destination));
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Price Price;
        /// <summary>
        ///  The order quantity
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Size Size;
        /// <summary>
        ///  The event action
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.ActionField Action;
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.SideField Side;
        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Flags Flags;
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Reserved1 Reserved1;
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv;
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsInDelta TsInDelta;
        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Reserved4 Reserved4;
        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.ConsolidatedBidAskPair.Layout ConsolidatedBidAskPair;
    }

    /// <summary>
    ///  The Cmbp 1 Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Price Price { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Price.Absent;

        /// <summary>
        ///  The order quantity
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Size Size { get; init; }

        /// <summary>
        ///  The event action
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.ActionField Action { get; init; }

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.SideField Side { get; init; }

        /// <summary>
        ///  A bit field indicating event end, message characteristics, and data quality; declared least significant bit first
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Flags Flags { get; init; }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.TsRecv.Absent;

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.TsInDelta TsInDelta { get; init; }

        /// <summary>
        ///  A price level consolidated from multiple venues
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.ConsolidatedBidAskPair.Value ConsolidatedBidAskPair { get; init; }

        /// <summary>
        ///  Creates the value of the Cmbp 1 Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved4 + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeConsolidatedBidAskPair;

    /// <summary>
    ///  Writes the Cmbp 1 Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cmbp 1 Msg.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v3.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterDestination);
        ref Layout cmbp1MsgPriceWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        cmbp1MsgPriceWriterLayout.Price = value.Price;
        cmbp1MsgPriceWriterLayout.Size = value.Size;
        cmbp1MsgPriceWriterLayout.Action = value.Action;
        cmbp1MsgPriceWriterLayout.Side = value.Side;
        cmbp1MsgPriceWriterLayout.Flags = value.Flags;
        cmbp1MsgPriceWriterLayout.Reserved1 = default;
        cmbp1MsgPriceWriterLayout.TsRecv = value.TsRecv;
        cmbp1MsgPriceWriterLayout.TsInDelta = value.TsInDelta;
        cmbp1MsgPriceWriterLayout.Reserved4 = default;
        cursor.Index += global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSize + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeFlags + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved1 + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved4;
        int cmbp1MsgConsolidatedBidAskPairWriting = cursor.Index;
        var cmbp1MsgConsolidatedBidAskPairWriteValue = value.ConsolidatedBidAskPair;
        global::Databento.MarketData.Normalized.Dbn.v3.ConsolidatedBidAskPair.Write(bytes, ref cursor, cmbp1MsgConsolidatedBidAskPairWriting, in cmbp1MsgConsolidatedBidAskPairWriteValue);
        return bytes[start..cursor.Index];
    }
}
