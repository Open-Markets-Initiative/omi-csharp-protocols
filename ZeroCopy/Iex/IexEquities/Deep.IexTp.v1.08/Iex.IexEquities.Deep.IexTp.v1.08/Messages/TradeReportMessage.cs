using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
/// </summary>
public static class TradeReportMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Trade Report Message in bytes.
        /// </summary>
        public const int LayoutSize = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSaleConditionFlags + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSymbol + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSize + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTradeId;

        /// <summary>
        ///  Gets the Trade Report Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Trade Report Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Trade Report Message at the start of the buffer, writes its fixed values, and gets the Trade Report Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Trade Report Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, nameof(destination));
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Sale Condition Flags
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SaleConditionFlags SaleConditionFlags;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Symbol Symbol;
        /// <summary>
        ///  Aggregate quoted size
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Size Size;
        /// <summary>
        ///  Price level to add/update in the IEX Order Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Price Price;
        /// <summary>
        ///  IEX Generated Identifier. Trade ID is also
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.TradeId TradeId;
    }

    /// <summary>
    ///  The Trade Report Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Sale Condition Flags
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.SaleConditionFlags SaleConditionFlags { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Symbol Symbol { get; init; }

        /// <summary>
        ///  Aggregate quoted size
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Size Size { get; init; }

        /// <summary>
        ///  Price level to add/update in the IEX Order Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Price Price { get; init; }

        /// <summary>
        ///  IEX Generated Identifier. Trade ID is also
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.TradeId TradeId { get; init; }

        /// <summary>
        ///  Creates the value of the Trade Report Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSaleConditionFlags + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSymbol + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSize + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTradeId;

    /// <summary>
    ///  Writes the Trade Report Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Trade Report Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterDestination);
        ref Layout tradeReportMessageSaleConditionFlagsWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        tradeReportMessageSaleConditionFlagsWriterLayout.SaleConditionFlags = value.SaleConditionFlags;
        tradeReportMessageSaleConditionFlagsWriterLayout.Timestamp = value.Timestamp;
        tradeReportMessageSaleConditionFlagsWriterLayout.Symbol = value.Symbol;
        tradeReportMessageSaleConditionFlagsWriterLayout.Size = value.Size;
        tradeReportMessageSaleConditionFlagsWriterLayout.Price = value.Price;
        tradeReportMessageSaleConditionFlagsWriterLayout.TradeId = value.TradeId;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
