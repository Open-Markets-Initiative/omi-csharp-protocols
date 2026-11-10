using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates the anticipated IPO quotation release time of a security.
/// </summary>
public static class IpoQuotingPeriodUpdate
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Ipo Quoting Period Update in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoQuotationReleaseTime + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoQuotationReleaseQualifier + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoPrice;

        /// <summary>
        ///  Gets the Ipo Quoting Period Update laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Ipo Quoting Period Update.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Ipo Quoting Period Update at the start of the buffer, writes its fixed values, and gets the Ipo Quoting Period Update laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Ipo Quoting Period Update.
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
        ///  Denotes the IPO release time, in seconds since midnight, for quotation to the nearest second
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotationReleaseTime IpoQuotationReleaseTime;
        /// <summary>
        ///  IPO Quotation Release Qualifier
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotationReleaseQualifierField IpoQuotationReleaseQualifier;
        /// <summary>
        ///  Denotes the IPO price to be used for intraday net change calculations
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoPrice IpoPrice;
    }

    /// <summary>
    ///  The Ipo Quoting Period Update fields to write.
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
        ///  Denotes the IPO release time, in seconds since midnight, for quotation to the nearest second
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotationReleaseTime IpoQuotationReleaseTime { get; init; }

        /// <summary>
        ///  IPO Quotation Release Qualifier
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotationReleaseQualifierField IpoQuotationReleaseQualifier { get; init; }

        /// <summary>
        ///  Denotes the IPO price to be used for intraday net change calculations
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoPrice IpoPrice { get; init; }

        /// <summary>
        ///  Creates the value of the Ipo Quoting Period Update, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoQuotationReleaseTime + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoQuotationReleaseQualifier + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoPrice;

    /// <summary>
    ///  Writes the Ipo Quoting Period Update to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Ipo Quoting Period Update.
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
        ref Layout ipoQuotingPeriodUpdateStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        ipoQuotingPeriodUpdateStockLocateWriterLayout.StockLocate = value.StockLocate;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.Timestamp = value.Timestamp;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.Stock = value.Stock;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.IpoQuotationReleaseTime = value.IpoQuotationReleaseTime;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.IpoQuotationReleaseQualifier = value.IpoQuotationReleaseQualifier;
        ipoQuotingPeriodUpdateStockLocateWriterLayout.IpoPrice = value.IpoPrice;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
