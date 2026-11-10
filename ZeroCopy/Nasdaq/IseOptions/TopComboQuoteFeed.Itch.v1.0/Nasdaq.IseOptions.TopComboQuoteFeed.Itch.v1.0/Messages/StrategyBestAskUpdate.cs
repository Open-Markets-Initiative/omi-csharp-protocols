using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The options system will continuously calculate its best offer position for active strategies on the options market during the trading day. Whenever the best ask position changes on one side but not the other side, the options system will send its best ask update via this feed for the affected security
/// </summary>
public static class StrategyBestAskUpdate
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Strategy Best Ask Update in bytes.
        /// </summary>
        public const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeQuoteCondition + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizePrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeNttMarketSize;

        /// <summary>
        ///  Gets the Strategy Best Ask Update laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Strategy Best Ask Update.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Strategy Best Ask Update at the start of the buffer, writes its fixed values, and gets the Strategy Best Ask Update laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Strategy Best Ask Update.
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
        ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.QuoteConditionField QuoteCondition;
        /// <summary>
        ///  Best bid or ask price in fixed point format with 6 whole number places followed by 4 decimal digits, the side determined by Message Type.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Price Price;
        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Size Size;
        /// <summary>
        ///  Customer Quantity on the bid or ask side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.CustSize CustSize;
        /// <summary>
        ///  Customer professional quantity on the bid or ask side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ProCustSize ProCustSize;
        /// <summary>
        ///  Limit size that can’t be traded through
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.NttSize NttSize;
        /// <summary>
        ///  The aggregate market size.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MarketSize MarketSize;
        /// <summary>
        ///  The aggregated NTT Market Size.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.NttMarketSize NttMarketSize;
    }

    /// <summary>
    ///  The Strategy Best Ask Update fields to write.
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
        ///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.QuoteConditionField QuoteCondition { get; init; }

        /// <summary>
        ///  Best bid or ask price in fixed point format with 6 whole number places followed by 4 decimal digits, the side determined by Message Type.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Price Price { get; init; }

        /// <summary>
        ///  Aggregated number of contracts on the bid or ask side being displayed in the options market at the current time.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Size Size { get; init; }

        /// <summary>
        ///  Customer Quantity on the bid or ask side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.CustSize CustSize { get; init; }

        /// <summary>
        ///  Customer professional quantity on the bid or ask side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ProCustSize ProCustSize { get; init; }

        /// <summary>
        ///  Limit size that can’t be traded through
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.NttSize NttSize { get; init; }

        /// <summary>
        ///  The aggregate market size.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MarketSize MarketSize { get; init; }

        /// <summary>
        ///  The aggregated NTT Market Size.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.NttMarketSize NttMarketSize { get; init; }

        /// <summary>
        ///  Creates the value of the Strategy Best Ask Update, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeQuoteCondition + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizePrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeNttMarketSize;

    /// <summary>
    ///  Writes the Strategy Best Ask Update to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Strategy Best Ask Update.
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
        ref Layout strategyBestAskUpdateTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        strategyBestAskUpdateTimestampWriterLayout.Timestamp = value.Timestamp;
        strategyBestAskUpdateTimestampWriterLayout.StrategyId = value.StrategyId;
        strategyBestAskUpdateTimestampWriterLayout.QuoteCondition = value.QuoteCondition;
        strategyBestAskUpdateTimestampWriterLayout.Price = value.Price;
        strategyBestAskUpdateTimestampWriterLayout.Size = value.Size;
        strategyBestAskUpdateTimestampWriterLayout.CustSize = value.CustSize;
        strategyBestAskUpdateTimestampWriterLayout.ProCustSize = value.ProCustSize;
        strategyBestAskUpdateTimestampWriterLayout.NttSize = value.NttSize;
        strategyBestAskUpdateTimestampWriterLayout.MarketSize = value.MarketSize;
        strategyBestAskUpdateTimestampWriterLayout.NttMarketSize = value.NttMarketSize;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
