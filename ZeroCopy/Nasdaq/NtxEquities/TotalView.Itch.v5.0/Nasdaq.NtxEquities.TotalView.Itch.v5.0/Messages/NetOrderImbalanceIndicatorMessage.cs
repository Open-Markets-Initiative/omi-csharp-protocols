using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Nasdaq Texas disseminates NOII data at 5-second intervals in the minutes leading up to the Nasdaq Opening Cross and Nasdaq Closing Cross.
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
        public const int Size = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizePairedShares + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeImbalanceShares + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeImbalanceDirection + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeFarPrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeNearPrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCurrentReferencePrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCrossType + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizePriceVariationIndicator;

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
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(source));
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
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Always 0
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockLocate StockLocate;
        /// <summary>
        ///  Nasdaq Texas internal tracking number
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Timestamp Timestamp;
        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PairedShares PairedShares;
        /// <summary>
        ///  The number of shares not paired at the Current Reference Price.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.ImbalanceShares ImbalanceShares;
        /// <summary>
        ///  The market side of the order imbalance.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.ImbalanceDirectionField ImbalanceDirection;
        /// <summary>
        ///  Denotes the security symbol for the issue in the Nasdaq Texas execution system.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Stock Stock;
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.FarPrice FarPrice;
        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NearPrice NearPrice;
        /// <summary>
        ///  The price at which the NOII shares are being calculated. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CurrentReferencePrice CurrentReferencePrice;
        /// <summary>
        ///  The Nasdaq Texas cross session for which the message is being generated.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTypeField CrossType;
        /// <summary>
        ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PriceVariationIndicatorField PriceVariationIndicator;
    }

    /// <summary>
    ///  The Net Order Imbalance Indicator Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Always 0
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockLocate StockLocate { get; init; }

        /// <summary>
        ///  Nasdaq Texas internal tracking number
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Nanoseconds since midnight
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  The total number of shares that are eligible to be matched at the Current Reference Price.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PairedShares PairedShares { get; init; }

        /// <summary>
        ///  The number of shares not paired at the Current Reference Price.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.ImbalanceShares ImbalanceShares { get; init; }

        /// <summary>
        ///  The market side of the order imbalance.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.ImbalanceDirectionField ImbalanceDirection { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the Nasdaq Texas execution system.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Stock Stock { get; init; }

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders only. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.FarPrice FarPrice { get; init; }

        /// <summary>
        ///  A hypothetical auction-clearing price for cross orders as well as continuous orders. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NearPrice NearPrice { get; init; }

        /// <summary>
        ///  The price at which the NOII shares are being calculated. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CurrentReferencePrice CurrentReferencePrice { get; init; }

        /// <summary>
        ///  The Nasdaq Texas cross session for which the message is being generated.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTypeField CrossType { get; init; }

        /// <summary>
        ///  This field indicates the absolute value of the percentage of deviation of the Near Indicative Clearing Price to the nearest Current Reference Price.
        /// </summary>
        public required global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.PriceVariationIndicatorField PriceVariationIndicator { get; init; }

        /// <summary>
        ///  Creates the value of the Net Order Imbalance Indicator Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStockLocate + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTrackingNumber + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeTimestamp + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizePairedShares + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeImbalanceShares + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeImbalanceDirection + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeStock + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeFarPrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeNearPrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCurrentReferencePrice + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizeCrossType + global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.SizePriceVariationIndicator;

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
        var cursor = new global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterDestination);
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
