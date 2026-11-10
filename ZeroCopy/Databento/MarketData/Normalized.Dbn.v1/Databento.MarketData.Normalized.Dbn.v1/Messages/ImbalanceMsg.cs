using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  An auction imbalance message; the record of the imbalance schema
/// </summary>
public static class ImbalanceMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Imbalance Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeRefPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionTime + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeContBookClrPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctInterestClrPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSsrFillingPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIndMatchPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUpperCollar + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeLowerCollar + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizePairedQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTotalImbalanceQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeMarketImbalanceQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUnpairedQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionType + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionStatus + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeFreezeStatus + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeNumExtensions + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUnpairedSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSignificantImbalance + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved1;

        /// <summary>
        ///  Gets the Imbalance Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Imbalance Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Imbalance Msg at the start of the buffer, writes its fixed values, and gets the Imbalance Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Imbalance Msg.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv;
        /// <summary>
        ///  The price at which the imbalance shares are calculated
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.RefPrice RefPrice;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctionTime AuctionTime;
        /// <summary>
        ///  The hypothetical auction-clearing price for both cross and continuous orders
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.ContBookClrPrice ContBookClrPrice;
        /// <summary>
        ///  The hypothetical auction-clearing price for cross orders only
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctInterestClrPrice AuctInterestClrPrice;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SsrFillingPrice SsrFillingPrice;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.IndMatchPrice IndMatchPrice;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.UpperCollar UpperCollar;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.LowerCollar LowerCollar;
        /// <summary>
        ///  The quantity of shares that are eligible to be matched at ref_price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.PairedQty PairedQty;
        /// <summary>
        ///  The quantity of shares that are not paired at ref_price
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TotalImbalanceQty TotalImbalanceQty;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.MarketImbalanceQty MarketImbalanceQty;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.UnpairedQty UnpairedQty;
        /// <summary>
        ///  Venue-specific character code indicating the auction type; ~ when unused
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctionType AuctionType;
        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SideField Side;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctionStatus AuctionStatus;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.FreezeStatus FreezeStatus;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.NumExtensions NumExtensions;
        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.UnpairedSideField UnpairedSide;
        /// <summary>
        ///  Venue-specific character code; for Nasdaq, contains the raw Price Variation Indicator
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SignificantImbalance SignificantImbalance;
        /// <summary>
        ///  Reserved, 1 byte
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Reserved1 Reserved1;
    }

    /// <summary>
    ///  The Imbalance Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The capture-server-received timestamp expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.TsRecv TsRecv { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.TsRecv.Absent;

        /// <summary>
        ///  The price at which the imbalance shares are calculated
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.RefPrice RefPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.RefPrice.Absent;

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctionTime AuctionTime { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.AuctionTime.Absent;

        /// <summary>
        ///  The hypothetical auction-clearing price for both cross and continuous orders
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.ContBookClrPrice ContBookClrPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.ContBookClrPrice.Absent;

        /// <summary>
        ///  The hypothetical auction-clearing price for cross orders only
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.AuctInterestClrPrice AuctInterestClrPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.AuctInterestClrPrice.Absent;

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.SsrFillingPrice SsrFillingPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.SsrFillingPrice.Absent;

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.IndMatchPrice IndMatchPrice { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.IndMatchPrice.Absent;

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.UpperCollar UpperCollar { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.UpperCollar.Absent;

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.LowerCollar LowerCollar { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.LowerCollar.Absent;

        /// <summary>
        ///  The quantity of shares that are eligible to be matched at ref_price
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.PairedQty PairedQty { get; init; }

        /// <summary>
        ///  The quantity of shares that are not paired at ref_price
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.TotalImbalanceQty TotalImbalanceQty { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.MarketImbalanceQty MarketImbalanceQty { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.UnpairedQty UnpairedQty { get; init; }

        /// <summary>
        ///  Venue-specific character code indicating the auction type; ~ when unused
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.AuctionType AuctionType { get; init; }

        /// <summary>
        ///  The side that initiates the event
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.SideField Side { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.AuctionStatus AuctionStatus { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.FreezeStatus FreezeStatus { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.NumExtensions NumExtensions { get; init; }

        /// <summary>
        ///  Reserved for future use
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.UnpairedSideField UnpairedSide { get; init; }

        /// <summary>
        ///  Venue-specific character code; for Nasdaq, contains the raw Price Variation Indicator
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.SignificantImbalance SignificantImbalance { get; init; }

        /// <summary>
        ///  Creates the value of the Imbalance Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTsRecv + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeRefPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionTime + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeContBookClrPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctInterestClrPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSsrFillingPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeIndMatchPrice + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUpperCollar + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeLowerCollar + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizePairedQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeTotalImbalanceQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeMarketImbalanceQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUnpairedQty + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionType + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeAuctionStatus + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeFreezeStatus + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeNumExtensions + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeUnpairedSide + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeSignificantImbalance + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeReserved1;

    /// <summary>
    ///  Writes the Imbalance Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Imbalance Msg.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoRoom, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterDestination);
        ref Layout imbalanceMsgTsRecvWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        imbalanceMsgTsRecvWriterLayout.TsRecv = value.TsRecv;
        imbalanceMsgTsRecvWriterLayout.RefPrice = value.RefPrice;
        imbalanceMsgTsRecvWriterLayout.AuctionTime = value.AuctionTime;
        imbalanceMsgTsRecvWriterLayout.ContBookClrPrice = value.ContBookClrPrice;
        imbalanceMsgTsRecvWriterLayout.AuctInterestClrPrice = value.AuctInterestClrPrice;
        imbalanceMsgTsRecvWriterLayout.SsrFillingPrice = value.SsrFillingPrice;
        imbalanceMsgTsRecvWriterLayout.IndMatchPrice = value.IndMatchPrice;
        imbalanceMsgTsRecvWriterLayout.UpperCollar = value.UpperCollar;
        imbalanceMsgTsRecvWriterLayout.LowerCollar = value.LowerCollar;
        imbalanceMsgTsRecvWriterLayout.PairedQty = value.PairedQty;
        imbalanceMsgTsRecvWriterLayout.TotalImbalanceQty = value.TotalImbalanceQty;
        imbalanceMsgTsRecvWriterLayout.MarketImbalanceQty = value.MarketImbalanceQty;
        imbalanceMsgTsRecvWriterLayout.UnpairedQty = value.UnpairedQty;
        imbalanceMsgTsRecvWriterLayout.AuctionType = value.AuctionType;
        imbalanceMsgTsRecvWriterLayout.Side = value.Side;
        imbalanceMsgTsRecvWriterLayout.AuctionStatus = value.AuctionStatus;
        imbalanceMsgTsRecvWriterLayout.FreezeStatus = value.FreezeStatus;
        imbalanceMsgTsRecvWriterLayout.NumExtensions = value.NumExtensions;
        imbalanceMsgTsRecvWriterLayout.UnpairedSide = value.UnpairedSide;
        imbalanceMsgTsRecvWriterLayout.SignificantImbalance = value.SignificantImbalance;
        imbalanceMsgTsRecvWriterLayout.Reserved1 = default;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
