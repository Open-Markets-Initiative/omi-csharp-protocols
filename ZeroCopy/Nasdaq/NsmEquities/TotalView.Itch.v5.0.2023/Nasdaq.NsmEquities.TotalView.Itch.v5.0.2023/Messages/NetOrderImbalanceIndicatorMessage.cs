using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  NASDAQ disseminates NOII data
/// </summary>
public static class NetOrderImbalanceIndicatorMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Net Order Imbalance Indicator Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePairedShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeImbalanceShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeImbalanceDirection + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeFarPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeNearPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCurrentReferencePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossType + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePriceVariationIndicator;

        /// <summary>
        ///  Gets the Net Order Imbalance Indicator Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Net Order Imbalance Indicator Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Net Order Imbalance Indicator Message at the start of the buffer, writes its fixed values, and gets the Net Order Imbalance Indicator Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Net Order Imbalance Indicator Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockLocate StockLocate;
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Timestamp Timestamp;
        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PairedShares PairedShares;
        /// <summary>
        ///  The number of shares not paired at the Current Reference Price
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ImbalanceShares ImbalanceShares;
        /// <summary>
        ///  The market side of the order imbalance
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ImbalanceDirectionField ImbalanceDirection;
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock;
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FarPrice FarPrice;
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NearPrice NearPrice;
        /// <summary>
        ///  The price at which the NOII shares are being calculated
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CurrentReferencePrice CurrentReferencePrice;
        /// <summary>
        ///  The Nasdaq cross session for which the message is being generated
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTypeField CrossType;
        /// <summary>
        ///  Indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PriceVariationIndicatorField PriceVariationIndicator;
    }

    /// <summary>
    ///  The Net Order Imbalance Indicator Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Locate Code uniquely assigned to the security symbol for the day
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PairedShares PairedShares { get; init; }

        /// <summary>
        ///  The number of shares not paired at the Current Reference Price
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ImbalanceShares ImbalanceShares { get; init; }

        /// <summary>
        ///  The market side of the order imbalance
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ImbalanceDirectionField ImbalanceDirection { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock { get; init; }

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FarPrice FarPrice { get; init; }

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NearPrice NearPrice { get; init; }

        /// <summary>
        ///  The price at which the NOII shares are being calculated
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CurrentReferencePrice CurrentReferencePrice { get; init; }

        /// <summary>
        ///  The Nasdaq cross session for which the message is being generated
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTypeField CrossType { get; init; }

        /// <summary>
        ///  Indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PriceVariationIndicatorField PriceVariationIndicator { get; init; }

        /// <summary>
        ///  Creates the value of the Net Order Imbalance Indicator Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePairedShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeImbalanceShares + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeImbalanceDirection + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeFarPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeNearPrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCurrentReferencePrice + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeCrossType + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePriceVariationIndicator;

    /// <summary>
    ///  Writes the Net Order Imbalance Indicator Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Net Order Imbalance Indicator Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        ref Layout netOrderImbalanceIndicatorMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.PairedShares = value.PairedShares;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.ImbalanceShares = value.ImbalanceShares;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.ImbalanceDirection = value.ImbalanceDirection;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.Stock = value.Stock;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.FarPrice = value.FarPrice;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.NearPrice = value.NearPrice;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.CurrentReferencePrice = value.CurrentReferencePrice;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.CrossType = value.CrossType;
        netOrderImbalanceIndicatorMessageStockLocateWriterLayout.PriceVariationIndicator = value.PriceVariationIndicator;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
