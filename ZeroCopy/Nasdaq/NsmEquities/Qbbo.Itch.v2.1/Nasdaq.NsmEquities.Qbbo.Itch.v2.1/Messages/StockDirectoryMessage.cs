using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  At the start of each trading day, Nasdaq disseminates stock directory messages for all active symbols in its market center system.
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
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMarketCategory + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeFinancialStatusIndicator + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRoundLotSize + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRoundLotsOnly + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIssueClassification + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIssueSubType + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeAuthenticity + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeShortSaleThresholdIndicator + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoFlag + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLuldReferencePriceTier + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeEtpFlag + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeEtpLeverageFactor + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeInverseIndicator;

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
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
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
        ///  Indicates Listing market or listing market tier for the issue
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MarketCategoryField MarketCategory;
        /// <summary>
        ///  For Nasdaq-listed issues, this field indicates when a firm is not in compliance with Nasdaq continued listing requirements.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.FinancialStatusIndicatorField FinancialStatusIndicator;
        /// <summary>
        ///  Indicates the number of shares that represent a round lot for the issue
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RoundLotSize RoundLotSize;
        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RoundLotsOnlyField RoundLotsOnly;
        /// <summary>
        ///  Identifies the security class for the issue as assigned by Nasdaq. See Appendix for allowable values.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IssueClassificationField IssueClassification;
        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by Nasdaq. See Appendix for allowable values.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IssueSubType IssueSubType;
        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in Nasdaq systems in a live/production, test, or demo state.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.AuthenticityField Authenticity;
        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.ShortSaleThresholdIndicatorField ShortSaleThresholdIndicator;
        /// <summary>
        ///  Indicates if the Nasdaq security is set up for IPO release.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoFlagField IpoFlag;
        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.LuldReferencePriceTierField LuldReferencePriceTier;
        /// <summary>
        ///  Indicates whether the security is an exchange traded product (ETP).
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.EtpFlagField EtpFlag;
        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.EtpLeverageFactor EtpLeverageFactor;
        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.InverseIndicatorField InverseIndicator;
    }

    /// <summary>
    ///  The Stock Directory Message fields to write.
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
        ///  Indicates Listing market or listing market tier for the issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MarketCategoryField MarketCategory { get; init; }

        /// <summary>
        ///  For Nasdaq-listed issues, this field indicates when a firm is not in compliance with Nasdaq continued listing requirements.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.FinancialStatusIndicatorField FinancialStatusIndicator { get; init; }

        /// <summary>
        ///  Indicates the number of shares that represent a round lot for the issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RoundLotSize RoundLotSize { get; init; }

        /// <summary>
        ///  Indicates if Nasdaq system limits order entry for issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RoundLotsOnlyField RoundLotsOnly { get; init; }

        /// <summary>
        ///  Identifies the security class for the issue as assigned by Nasdaq. See Appendix for allowable values.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IssueClassificationField IssueClassification { get; init; }

        /// <summary>
        ///  Identifies the security sub-type for the issue as assigned by Nasdaq. See Appendix for allowable values.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IssueSubType IssueSubType { get; init; }

        /// <summary>
        ///  Denotes if an issue or quoting participant record is set-up in Nasdaq systems in a live/production, test, or demo state.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.AuthenticityField Authenticity { get; init; }

        /// <summary>
        ///  Indicates if a security is subject to mandatory close-out of short sales under SEC Rule 203(b)(3).
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.ShortSaleThresholdIndicatorField ShortSaleThresholdIndicator { get; init; }

        /// <summary>
        ///  Indicates if the Nasdaq security is set up for IPO release.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoFlagField IpoFlag { get; init; }

        /// <summary>
        ///  Indicates which Limit Up / Limit Down price band calculation parameter is to be used for the instrument.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.LuldReferencePriceTierField LuldReferencePriceTier { get; init; }

        /// <summary>
        ///  Indicates whether the security is an exchange traded product (ETP).
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.EtpFlagField EtpFlag { get; init; }

        /// <summary>
        ///  Tracks the integral relationship of the ETP to the underlying index.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.EtpLeverageFactor EtpLeverageFactor { get; init; }

        /// <summary>
        ///  Indicates the directional relationship between the ETP and underlying index.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.InverseIndicatorField InverseIndicator { get; init; }

        /// <summary>
        ///  Creates the value of the Stock Directory Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMarketCategory + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeFinancialStatusIndicator + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRoundLotSize + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeRoundLotsOnly + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIssueClassification + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIssueSubType + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeAuthenticity + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeShortSaleThresholdIndicator + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoFlag + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeLuldReferencePriceTier + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeEtpFlag + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeEtpLeverageFactor + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeInverseIndicator;

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
        var cursor = new global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterDestination);
        ref Layout stockDirectoryMessageTrackingNumberWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        stockDirectoryMessageTrackingNumberWriterLayout.TrackingNumber = value.TrackingNumber;
        stockDirectoryMessageTrackingNumberWriterLayout.TimeStamp = value.TimeStamp;
        stockDirectoryMessageTrackingNumberWriterLayout.Stock = value.Stock;
        stockDirectoryMessageTrackingNumberWriterLayout.MarketCategory = value.MarketCategory;
        stockDirectoryMessageTrackingNumberWriterLayout.FinancialStatusIndicator = value.FinancialStatusIndicator;
        stockDirectoryMessageTrackingNumberWriterLayout.RoundLotSize = value.RoundLotSize;
        stockDirectoryMessageTrackingNumberWriterLayout.RoundLotsOnly = value.RoundLotsOnly;
        stockDirectoryMessageTrackingNumberWriterLayout.IssueClassification = value.IssueClassification;
        stockDirectoryMessageTrackingNumberWriterLayout.IssueSubType = value.IssueSubType;
        stockDirectoryMessageTrackingNumberWriterLayout.Authenticity = value.Authenticity;
        stockDirectoryMessageTrackingNumberWriterLayout.ShortSaleThresholdIndicator = value.ShortSaleThresholdIndicator;
        stockDirectoryMessageTrackingNumberWriterLayout.IpoFlag = value.IpoFlag;
        stockDirectoryMessageTrackingNumberWriterLayout.LuldReferencePriceTier = value.LuldReferencePriceTier;
        stockDirectoryMessageTrackingNumberWriterLayout.EtpFlag = value.EtpFlag;
        stockDirectoryMessageTrackingNumberWriterLayout.EtpLeverageFactor = value.EtpLeverageFactor;
        stockDirectoryMessageTrackingNumberWriterLayout.InverseIndicator = value.InverseIndicator;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
