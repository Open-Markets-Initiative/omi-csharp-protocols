using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
/// </summary>
public static class BboQuotationMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Bbo Quotation Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeSecurityClass + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestBidPrice + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestBidSize + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestOfferPrice + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestOfferSize;

        /// <summary>
        ///  Gets the Bbo Quotation Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Bbo Quotation Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Bbo Quotation Message at the start of the buffer, writes its fixed values, and gets the Bbo Quotation Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Bbo Quotation Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TrackingNumber TrackingNumber;
        /// <summary>
        ///  Timestamp
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp;
        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock;
        /// <summary>
        ///  Indicates the primary listing market for the issue.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SecurityClassField SecurityClass;
        /// <summary>
        ///  Denotes the best bid price, or the highest price for market buy order(s) in the execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestBidPrice BestBidPrice;
        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best bid price.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestBidSize BestBidSize;
        /// <summary>
        ///  Denotes the Nasdaq exchange's best offer price, or the lowest price for market sell order(s) in the execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestOfferPrice BestOfferPrice;
        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best offer price.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestOfferSize BestOfferSize;
    }

    /// <summary>
    ///  The Bbo Quotation Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TrackingNumber TrackingNumber { get; init; }

        /// <summary>
        ///  Timestamp
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock { get; init; }

        /// <summary>
        ///  Indicates the primary listing market for the issue.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SecurityClassField SecurityClass { get; init; }

        /// <summary>
        ///  Denotes the best bid price, or the highest price for market buy order(s) in the execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestBidPrice BestBidPrice { get; init; }

        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best bid price.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestBidSize BestBidSize { get; init; }

        /// <summary>
        ///  Denotes the Nasdaq exchange's best offer price, or the lowest price for market sell order(s) in the execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestOfferPrice BestOfferPrice { get; init; }

        /// <summary>
        ///  Denotes the aggregated number of shares available for display within the execution system at the best offer price.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BestOfferSize BestOfferSize { get; init; }

        /// <summary>
        ///  Creates the value of the Bbo Quotation Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeSecurityClass + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestBidPrice + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestBidSize + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestOfferPrice + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeBestOfferSize;

    /// <summary>
    ///  Writes the Bbo Quotation Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Bbo Quotation Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterDestination);
        ref Layout bboQuotationMessageTrackingNumberWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        bboQuotationMessageTrackingNumberWriterLayout.TrackingNumber = value.TrackingNumber;
        bboQuotationMessageTrackingNumberWriterLayout.TimeStamp = value.TimeStamp;
        bboQuotationMessageTrackingNumberWriterLayout.Stock = value.Stock;
        bboQuotationMessageTrackingNumberWriterLayout.SecurityClass = value.SecurityClass;
        bboQuotationMessageTrackingNumberWriterLayout.BestBidPrice = value.BestBidPrice;
        bboQuotationMessageTrackingNumberWriterLayout.BestBidSize = value.BestBidSize;
        bboQuotationMessageTrackingNumberWriterLayout.BestOfferPrice = value.BestOfferPrice;
        bboQuotationMessageTrackingNumberWriterLayout.BestOfferSize = value.BestOfferSize;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
