using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The ticker message is used to send real time trade information
/// </summary>
public static class ComplexStrategyTickerMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Complex Strategy Ticker Message in bytes.
        /// </summary>
        public const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeLastPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeVolume + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeHigh + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeLow + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeFirst + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTradeCondition;

        /// <summary>
        ///  Gets the Complex Strategy Ticker Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Complex Strategy Ticker Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Complex Strategy Ticker Message at the start of the buffer, writes its fixed values, and gets the Complex Strategy Ticker Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Complex Strategy Ticker Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, nameof(destination));
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Timestamp Timestamp;
        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyId StrategyId;
        /// <summary>
        ///  Most recent price.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LastPrice LastPrice;
        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Size Size;
        /// <summary>
        ///  Total traded quantity.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Volume Volume;
        /// <summary>
        ///  High price for the day.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.High High;
        /// <summary>
        ///  Low price for the day.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Low Low;
        /// <summary>
        ///  Opening price for the day
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.First First;
        /// <summary>
        ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.TradeCondition TradeCondition;
    }

    /// <summary>
    ///  The Complex Strategy Ticker Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyId StrategyId { get; init; }

        /// <summary>
        ///  Most recent price.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.LastPrice LastPrice { get; init; }

        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Size Size { get; init; }

        /// <summary>
        ///  Total traded quantity.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Volume Volume { get; init; }

        /// <summary>
        ///  High price for the day.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.High High { get; init; }

        /// <summary>
        ///  Low price for the day.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Low Low { get; init; }

        /// <summary>
        ///  Opening price for the day
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.First First { get; init; }

        /// <summary>
        ///  Same value as the Trade Condition sent to OPRA for this trade. To obtain a list of Trade Conditions, refer to the NOTES below.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.TradeCondition TradeCondition { get; init; }

        /// <summary>
        ///  Creates the value of the Complex Strategy Ticker Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeLastPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeVolume + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeHigh + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeLow + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeFirst + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTradeCondition;

    /// <summary>
    ///  Writes the Complex Strategy Ticker Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Complex Strategy Ticker Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterDestination);
        ref Layout complexStrategyTickerMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        complexStrategyTickerMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        complexStrategyTickerMessageTimestampWriterLayout.StrategyId = value.StrategyId;
        complexStrategyTickerMessageTimestampWriterLayout.LastPrice = value.LastPrice;
        complexStrategyTickerMessageTimestampWriterLayout.Size = value.Size;
        complexStrategyTickerMessageTimestampWriterLayout.Volume = value.Volume;
        complexStrategyTickerMessageTimestampWriterLayout.High = value.High;
        complexStrategyTickerMessageTimestampWriterLayout.Low = value.Low;
        complexStrategyTickerMessageTimestampWriterLayout.First = value.First;
        complexStrategyTickerMessageTimestampWriterLayout.TradeCondition = value.TradeCondition;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
