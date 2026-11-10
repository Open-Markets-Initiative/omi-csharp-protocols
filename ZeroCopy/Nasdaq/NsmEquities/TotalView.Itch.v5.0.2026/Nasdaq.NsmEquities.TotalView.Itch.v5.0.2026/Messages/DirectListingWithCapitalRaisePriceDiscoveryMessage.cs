using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
/// </summary>
public static class DirectListingWithCapitalRaisePriceDiscoveryMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Direct Listing With Capital Raise Price Discovery Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeOpenEligibilityStatus + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMinimumAllowablePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMaximumAllowablePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeNearExecutionPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeNearExecutionTime + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeLowerPriceRangeCollar + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeUpperPriceRangeCollar;

        /// <summary>
        ///  Gets the Direct Listing With Capital Raise Price Discovery Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Direct Listing With Capital Raise Price Discovery Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Direct Listing With Capital Raise Price Discovery Message at the start of the buffer, writes its fixed values, and gets the Direct Listing With Capital Raise Price Discovery Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Direct Listing With Capital Raise Price Discovery Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockLocate StockLocate;
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Timestamp Timestamp;
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Stock Stock;
        /// <summary>
        ///  Indicates if the security is eligible to be released for trading
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OpenEligibilityStatusField OpenEligibilityStatus;
        /// <summary>
        ///  20% below Registration Statement Lower Price
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MinimumAllowablePrice MinimumAllowablePrice;
        /// <summary>
        ///  80% above Registration Statement Highest Price
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MaximumAllowablePrice MaximumAllowablePrice;
        /// <summary>
        ///  The current reference price when the DLCR volatility test has successfully passed
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NearExecutionPrice NearExecutionPrice;
        /// <summary>
        ///  The time at which the Near Execution Price was determined
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NearExecutionTime NearExecutionTime;
        /// <summary>
        ///  Indicates the price of the Lower Auction Collar Threshold
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LowerPriceRangeCollar LowerPriceRangeCollar;
        /// <summary>
        ///  Indicates the price of the Upper Auction Collar Threshold
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.UpperPriceRangeCollar UpperPriceRangeCollar;
    }

    /// <summary>
    ///  The Direct Listing With Capital Raise Price Discovery Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Stock Stock { get; init; }

        /// <summary>
        ///  Indicates if the security is eligible to be released for trading
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OpenEligibilityStatusField OpenEligibilityStatus { get; init; }

        /// <summary>
        ///  20% below Registration Statement Lower Price
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MinimumAllowablePrice MinimumAllowablePrice { get; init; }

        /// <summary>
        ///  80% above Registration Statement Highest Price
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MaximumAllowablePrice MaximumAllowablePrice { get; init; }

        /// <summary>
        ///  The current reference price when the DLCR volatility test has successfully passed
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NearExecutionPrice NearExecutionPrice { get; init; }

        /// <summary>
        ///  The time at which the Near Execution Price was determined
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NearExecutionTime NearExecutionTime { get; init; }

        /// <summary>
        ///  Indicates the price of the Lower Auction Collar Threshold
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LowerPriceRangeCollar LowerPriceRangeCollar { get; init; }

        /// <summary>
        ///  Indicates the price of the Upper Auction Collar Threshold
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.UpperPriceRangeCollar UpperPriceRangeCollar { get; init; }

        /// <summary>
        ///  Creates the value of the Direct Listing With Capital Raise Price Discovery Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeOpenEligibilityStatus + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMinimumAllowablePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMaximumAllowablePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeNearExecutionPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeNearExecutionTime + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeLowerPriceRangeCollar + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeUpperPriceRangeCollar;

    /// <summary>
    ///  Writes the Direct Listing With Capital Raise Price Discovery Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Direct Listing With Capital Raise Price Discovery Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.WriterDestination);
        ref Layout directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.Stock = value.Stock;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.OpenEligibilityStatus = value.OpenEligibilityStatus;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.MinimumAllowablePrice = value.MinimumAllowablePrice;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.MaximumAllowablePrice = value.MaximumAllowablePrice;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.NearExecutionPrice = value.NearExecutionPrice;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.NearExecutionTime = value.NearExecutionTime;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.LowerPriceRangeCollar = value.LowerPriceRangeCollar;
        directListingWithCapitalRaisePriceDiscoveryMessageStockLocateWriterLayout.UpperPriceRangeCollar = value.UpperPriceRangeCollar;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
