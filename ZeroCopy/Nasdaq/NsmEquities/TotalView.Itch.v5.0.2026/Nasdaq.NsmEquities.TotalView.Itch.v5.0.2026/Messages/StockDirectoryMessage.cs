using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
/// </summary>
public static class StockDirectoryMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Stock Directory Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMarketCategory + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeFinancialStatusIndicator + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeRoundLotSize + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeRoundLotsOnly + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIssueClassification + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIssueSubType + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeAuthenticity + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeShortSaleThresholdIndicator + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoFlag + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeLuldReferencePriceTier + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeEtpFlag + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeEtpLeverageFactor + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeInverseIndicator;

        /// <summary>
        ///  Gets the Stock Directory Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Stock Directory Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Stock Directory Message at the start of the buffer, writes its fixed values, and gets the Stock Directory Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Stock Directory Message.
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
        ///  Indicates listing market or listing market tier for the issue
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketCategoryField MarketCategory;
        /// <summary>
        ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FinancialStatusIndicatorField FinancialStatusIndicator;
        /// <summary>
        ///  Denotes the number of shares that represent a round lot for the issue
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RoundLotSize RoundLotSize;
        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RoundLotsOnlyField RoundLotsOnly;
        /// <summary>
        ///  Identifies the security class for the issue as assigned by NASDAQ
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IssueClassificationField IssueClassification;
        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by NASDAQ
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IssueSubType IssueSubType;
        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AuthenticityField Authenticity;
        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ShortSaleThresholdIndicatorField ShortSaleThresholdIndicator;
        /// <summary>
        ///  Indicates if the NASDAQ security is set up for IPO release
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoFlagField IpoFlag;
        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldReferencePriceTierField LuldReferencePriceTier;
        /// <summary>
        ///  Indicates whether the security is an exchange traded product
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.EtpFlagField EtpFlag;
        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.EtpLeverageFactor EtpLeverageFactor;
        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of 'Y' indicates the ETP will decrease by a value of 3.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.InverseIndicatorField InverseIndicator;
    }

    /// <summary>
    ///  The Stock Directory Message fields to write.
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
        ///  Indicates listing market or listing market tier for the issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketCategoryField MarketCategory { get; init; }

        /// <summary>
        ///  Indicates when a firm is not in compliance with NASDAQ continued listing requirements
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FinancialStatusIndicatorField FinancialStatusIndicator { get; init; }

        /// <summary>
        ///  Denotes the number of shares that represent a round lot for the issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RoundLotSize RoundLotSize { get; init; }

        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RoundLotsOnlyField RoundLotsOnly { get; init; }

        /// <summary>
        ///  Identifies the security class for the issue as assigned by NASDAQ
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IssueClassificationField IssueClassification { get; init; }

        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by NASDAQ
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IssueSubType IssueSubType { get; init; }

        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in NASDAQ systems in a live/production, test, or demo state
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AuthenticityField Authenticity { get; init; }

        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ShortSaleThresholdIndicatorField ShortSaleThresholdIndicator { get; init; }

        /// <summary>
        ///  Indicates if the NASDAQ security is set up for IPO release
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoFlagField IpoFlag { get; init; }

        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldReferencePriceTierField LuldReferencePriceTier { get; init; }

        /// <summary>
        ///  Indicates whether the security is an exchange traded product
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.EtpFlagField EtpFlag { get; init; }

        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.EtpLeverageFactor EtpLeverageFactor { get; init; }

        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of 'Y' indicates the ETP will decrease by a value of 3.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.InverseIndicatorField InverseIndicator { get; init; }

        /// <summary>
        ///  Creates the value of the Stock Directory Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeMarketCategory + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeFinancialStatusIndicator + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeRoundLotSize + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeRoundLotsOnly + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIssueClassification + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIssueSubType + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeAuthenticity + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeShortSaleThresholdIndicator + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeIpoFlag + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeLuldReferencePriceTier + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeEtpFlag + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeEtpLeverageFactor + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Parser.SizeInverseIndicator;

    /// <summary>
    ///  Writes the Stock Directory Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Stock Directory Message.
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
        ref Layout stockDirectoryMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        stockDirectoryMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        stockDirectoryMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        stockDirectoryMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        stockDirectoryMessageStockLocateWriterLayout.Stock = value.Stock;
        stockDirectoryMessageStockLocateWriterLayout.MarketCategory = value.MarketCategory;
        stockDirectoryMessageStockLocateWriterLayout.FinancialStatusIndicator = value.FinancialStatusIndicator;
        stockDirectoryMessageStockLocateWriterLayout.RoundLotSize = value.RoundLotSize;
        stockDirectoryMessageStockLocateWriterLayout.RoundLotsOnly = value.RoundLotsOnly;
        stockDirectoryMessageStockLocateWriterLayout.IssueClassification = value.IssueClassification;
        stockDirectoryMessageStockLocateWriterLayout.IssueSubType = value.IssueSubType;
        stockDirectoryMessageStockLocateWriterLayout.Authenticity = value.Authenticity;
        stockDirectoryMessageStockLocateWriterLayout.ShortSaleThresholdIndicator = value.ShortSaleThresholdIndicator;
        stockDirectoryMessageStockLocateWriterLayout.IpoFlag = value.IpoFlag;
        stockDirectoryMessageStockLocateWriterLayout.LuldReferencePriceTier = value.LuldReferencePriceTier;
        stockDirectoryMessageStockLocateWriterLayout.EtpFlag = value.EtpFlag;
        stockDirectoryMessageStockLocateWriterLayout.EtpLeverageFactor = value.EtpLeverageFactor;
        stockDirectoryMessageStockLocateWriterLayout.InverseIndicator = value.InverseIndicator;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
