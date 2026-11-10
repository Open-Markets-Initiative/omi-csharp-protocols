using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  A trading status update message; the record of the status schema
/// </summary>
public static class StatusMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Status Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStatusAction + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStatusReason + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTradingEvent + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsTrading + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsQuoting + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsShortSellRestricted + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved7;

        /// <summary>
        ///  Gets the Status Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Status Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Status Msg at the start of the buffer, writes its fixed values, and gets the Status Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Status Msg.
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
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv;
        /// <summary>
        ///  The type of status change
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StatusActionField StatusAction;
        /// <summary>
        ///  Additional details about the cause of the status change
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StatusReasonField StatusReason;
        /// <summary>
        ///  Further information about a status update
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TradingEventField TradingEvent;
        /// <summary>
        ///  The state of trading in the instrument
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.IsTradingField IsTrading;
        /// <summary>
        ///  The state of quoting in the instrument
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.IsQuotingField IsQuoting;
        /// <summary>
        ///  The state of short sell restrictions for the instrument
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.IsShortSellRestrictedField IsShortSellRestricted;
        /// <summary>
        ///  Reserved, 7 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Reserved7 Reserved7;
    }

    /// <summary>
    ///  The Status Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.TsRecv.Absent;

        /// <summary>
        ///  The type of status change
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.StatusActionField StatusAction { get; init; }

        /// <summary>
        ///  Additional details about the cause of the status change
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.StatusReasonField StatusReason { get; init; }

        /// <summary>
        ///  Further information about a status update
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.TradingEventField TradingEvent { get; init; }

        /// <summary>
        ///  The state of trading in the instrument
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.IsTradingField IsTrading { get; init; }

        /// <summary>
        ///  The state of quoting in the instrument
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.IsQuotingField IsQuoting { get; init; }

        /// <summary>
        ///  The state of short sell restrictions for the instrument
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.IsShortSellRestrictedField IsShortSellRestricted { get; init; }

        /// <summary>
        ///  Creates the value of the Status Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStatusAction + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStatusReason + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTradingEvent + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsTrading + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsQuoting + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIsShortSellRestricted + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved7;

    /// <summary>
    ///  Writes the Status Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Status Msg.
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
        ref Layout statusMsgTsRecvWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        statusMsgTsRecvWriterLayout.TsRecv = value.TsRecv;
        statusMsgTsRecvWriterLayout.StatusAction = value.StatusAction;
        statusMsgTsRecvWriterLayout.StatusReason = value.StatusReason;
        statusMsgTsRecvWriterLayout.TradingEvent = value.TradingEvent;
        statusMsgTsRecvWriterLayout.IsTrading = value.IsTrading;
        statusMsgTsRecvWriterLayout.IsQuoting = value.IsQuoting;
        statusMsgTsRecvWriterLayout.IsShortSellRestricted = value.IsShortSellRestricted;
        statusMsgTsRecvWriterLayout.Reserved7 = default;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
