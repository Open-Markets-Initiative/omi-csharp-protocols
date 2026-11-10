using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
/// </summary>
public static class StrategyBestBidAndAskUpdate
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Strategy Best Bid And Ask Update in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeQuoteCondition + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidNttMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskNttMarketSize;

        /// <summary>
        ///  Gets the Strategy Best Bid And Ask Update laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Strategy Best Bid And Ask Update.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Strategy Best Bid And Ask Update at the start of the buffer, writes its fixed values, and gets the Strategy Best Bid And Ask Update laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Strategy Best Bid And Ask Update.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.ReadTooShort, nameof(bytes));
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
        ///  Best bid price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidPrice BidPrice;
        /// <summary>
        ///  Aggregated number of contracts on the bid side being displayed in the options market at the current time.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidSize BidSize;
        /// <summary>
        ///  Customer quantity on the bid side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidCustSize BidCustSize;
        /// <summary>
        ///  Customer professional quantity on the bid side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidProCustSize BidProCustSize;
        /// <summary>
        ///  Limit Bid size that can’t be traded through.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidNttSize BidNttSize;
        /// <summary>
        ///  The aggregate market size on Bid side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidMarketSize BidMarketSize;
        /// <summary>
        ///  The aggregated NTT Market Size on Bid Size.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidNttMarketSize BidNttMarketSize;
        /// <summary>
        ///  Best ask price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskPrice AskPrice;
        /// <summary>
        ///  Aggregated number of contracts on the ask side being displayed in the options market at the current time.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskSize AskSize;
        /// <summary>
        ///  Customer quantity on the ask side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskCustSize AskCustSize;
        /// <summary>
        ///  Customer professional quantity on the ask side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskProCustSize AskProCustSize;
        /// <summary>
        ///  Limit Ask size that can’t be traded through.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskNttSize AskNttSize;
        /// <summary>
        ///  Aggregate market quantity on Ask side.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskMarketSize AskMarketSize;
        /// <summary>
        ///  The aggregated NTT Market Size on Ask Size.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskNttMarketSize AskNttMarketSize;
    }

    /// <summary>
    ///  The Strategy Best Bid And Ask Update fields to write.
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
        ///  Best bid price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidPrice BidPrice { get; init; }

        /// <summary>
        ///  Aggregated number of contracts on the bid side being displayed in the options market at the current time.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidSize BidSize { get; init; }

        /// <summary>
        ///  Customer quantity on the bid side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidCustSize BidCustSize { get; init; }

        /// <summary>
        ///  Customer professional quantity on the bid side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidProCustSize BidProCustSize { get; init; }

        /// <summary>
        ///  Limit Bid size that can’t be traded through.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidNttSize BidNttSize { get; init; }

        /// <summary>
        ///  The aggregate market size on Bid side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidMarketSize BidMarketSize { get; init; }

        /// <summary>
        ///  The aggregated NTT Market Size on Bid Size.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.BidNttMarketSize BidNttMarketSize { get; init; }

        /// <summary>
        ///  Best ask price in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskPrice AskPrice { get; init; }

        /// <summary>
        ///  Aggregated number of contracts on the ask side being displayed in the options market at the current time.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskSize AskSize { get; init; }

        /// <summary>
        ///  Customer quantity on the ask side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskCustSize AskCustSize { get; init; }

        /// <summary>
        ///  Customer professional quantity on the ask side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskProCustSize AskProCustSize { get; init; }

        /// <summary>
        ///  Limit Ask size that can’t be traded through.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskNttSize AskNttSize { get; init; }

        /// <summary>
        ///  Aggregate market quantity on Ask side.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskMarketSize AskMarketSize { get; init; }

        /// <summary>
        ///  The aggregated NTT Market Size on Ask Size.
        /// </summary>
        public required global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.AskNttMarketSize AskNttMarketSize { get; init; }

        /// <summary>
        ///  Creates the value of the Strategy Best Bid And Ask Update, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeTimestamp + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeStrategyId + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeQuoteCondition + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeBidNttMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskPrice + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskProCustSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskNttSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskMarketSize + global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.SizeAskNttMarketSize;

    /// <summary>
    ///  Writes the Strategy Best Bid And Ask Update to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Strategy Best Bid And Ask Update.
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
        ref Layout strategyBestBidAndAskUpdateTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        strategyBestBidAndAskUpdateTimestampWriterLayout.Timestamp = value.Timestamp;
        strategyBestBidAndAskUpdateTimestampWriterLayout.StrategyId = value.StrategyId;
        strategyBestBidAndAskUpdateTimestampWriterLayout.QuoteCondition = value.QuoteCondition;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidPrice = value.BidPrice;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidSize = value.BidSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidCustSize = value.BidCustSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidProCustSize = value.BidProCustSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidNttSize = value.BidNttSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidMarketSize = value.BidMarketSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.BidNttMarketSize = value.BidNttMarketSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskPrice = value.AskPrice;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskSize = value.AskSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskCustSize = value.AskCustSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskProCustSize = value.AskProCustSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskNttSize = value.AskNttSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskMarketSize = value.AskMarketSize;
        strategyBestBidAndAskUpdateTimestampWriterLayout.AskNttMarketSize = value.AskNttMarketSize;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
