using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  A statistics message; the record of the statistics schema
/// </summary>
public static class StatMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Stat Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRef + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeQuantity + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSequence + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStatType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeChannel + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUpdateAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStatFlags + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved18;

        /// <summary>
        ///  Gets the Stat Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Stat Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Stat Msg at the start of the buffer, writes its fixed values, and gets the Stat Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Stat Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v3.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv;
        /// <summary>
        ///  The reference timestamp of the statistic value expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRef TsRef;
        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Price Price;
        /// <summary>
        ///  The value for non-price statistics
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Quantity Quantity;
        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Sequence Sequence;
        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsInDelta TsInDelta;
        /// <summary>
        ///  The type of statistic value contained in the message
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StatTypeField StatType;
        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Channel Channel;
        /// <summary>
        ///  Indicates if the statistic is newly added or deleted
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.UpdateActionField UpdateAction;
        /// <summary>
        ///  Additional flags associated with certain stat types
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.StatFlags StatFlags;
        /// <summary>
        ///  Reserved, 18 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Reserved18 Reserved18;
    }

    /// <summary>
    ///  The Stat Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.TsRecv.Absent;

        /// <summary>
        ///  The reference timestamp of the statistic value expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.TsRef TsRef { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.TsRef.Absent;

        /// <summary>
        ///  The price where every 1 unit corresponds to 1e-9
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Price Price { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Price.Absent;

        /// <summary>
        ///  The value for non-price statistics
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v3.Quantity Quantity { get; init; } = global::Databento.MarketData.Normalized.Dbn.v3.Quantity.Absent;

        /// <summary>
        ///  The message sequence number assigned at the venue
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Sequence Sequence { get; init; }

        /// <summary>
        ///  The matching-engine-sending timestamp expressed as the number of nanoseconds before ts_recv
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.TsInDelta TsInDelta { get; init; }

        /// <summary>
        ///  The type of statistic value contained in the message
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.StatTypeField StatType { get; init; }

        /// <summary>
        ///  The channel ID assigned by Databento as an incrementing integer starting at zero
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.Channel Channel { get; init; }

        /// <summary>
        ///  Indicates if the statistic is newly added or deleted
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.UpdateActionField UpdateAction { get; init; }

        /// <summary>
        ///  Additional flags associated with certain stat types
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v3.StatFlags StatFlags { get; init; }

        /// <summary>
        ///  Creates the value of the Stat Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsRef + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizePrice + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeQuantity + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeSequence + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeTsInDelta + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStatType + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeChannel + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeUpdateAction + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeStatFlags + global::Databento.MarketData.Normalized.Dbn.v3.Parser.SizeReserved18;

    /// <summary>
    ///  Writes the Stat Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Stat Msg.
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
        ref Layout statMsgTsRecvWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        statMsgTsRecvWriterLayout.TsRecv = value.TsRecv;
        statMsgTsRecvWriterLayout.TsRef = value.TsRef;
        statMsgTsRecvWriterLayout.Price = value.Price;
        statMsgTsRecvWriterLayout.Quantity = value.Quantity;
        statMsgTsRecvWriterLayout.Sequence = value.Sequence;
        statMsgTsRecvWriterLayout.TsInDelta = value.TsInDelta;
        statMsgTsRecvWriterLayout.StatType = value.StatType;
        statMsgTsRecvWriterLayout.Channel = value.Channel;
        statMsgTsRecvWriterLayout.UpdateAction = value.UpdateAction;
        statMsgTsRecvWriterLayout.StatFlags = value.StatFlags;
        statMsgTsRecvWriterLayout.Reserved18 = default;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
