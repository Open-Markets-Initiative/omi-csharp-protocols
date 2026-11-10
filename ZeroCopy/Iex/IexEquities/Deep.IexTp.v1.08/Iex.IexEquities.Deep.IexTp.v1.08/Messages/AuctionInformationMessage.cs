using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
/// </summary>
public static class AuctionInformationMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Auction Information Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeAuctionType + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSymbol + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePairedShares + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeReferencePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeIndicativeClearingPrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeImbalanceShares + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeImbalanceSide + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeExtensionNumber + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeScheduledAuctionTime + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeAuctionBookClearingPrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeCollarReferencePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeLowerAuctionCollar + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeUpperAuctionCollar;

        /// <summary>
        ///  Gets the Auction Information Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Auction Information Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Auction Information Message at the start of the buffer, writes its fixed values, and gets the Auction Information Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Auction Information Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Auction type identifier
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionTypeField AuctionType;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.Symbol Symbol;
        /// <summary>
        ///  Number of shares paired at the Reference Price using orders on the Auction Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.PairedShares PairedShares;
        /// <summary>
        ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ReferencePrice ReferencePrice;
        /// <summary>
        ///  Clearing price using Eligible Auction Orders
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.IndicativeClearingPrice IndicativeClearingPrice;
        /// <summary>
        ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ImbalanceShares ImbalanceShares;
        /// <summary>
        ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ImbalanceSideField ImbalanceSide;
        /// <summary>
        ///  Number of extensions an auction received
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ExtensionNumber ExtensionNumber;
        /// <summary>
        ///  Projected time of the auction match
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ScheduledAuctionTime ScheduledAuctionTime;
        /// <summary>
        ///  Clearing price using orders on the Auction Book
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionBookClearingPrice AuctionBookClearingPrice;
        /// <summary>
        ///  Reference priced used for the auction collar, if any
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.CollarReferencePrice CollarReferencePrice;
        /// <summary>
        ///  Lower threshold price of the auction collar, if any
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.LowerAuctionCollar LowerAuctionCollar;
        /// <summary>
        ///  Upper threshold price of the auction collar, if any
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.UpperAuctionCollar UpperAuctionCollar;
    }

    /// <summary>
    ///  The Auction Information Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Auction type identifier
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionTypeField AuctionType { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.Symbol Symbol { get; init; }

        /// <summary>
        ///  Number of shares paired at the Reference Price using orders on the Auction Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.PairedShares PairedShares { get; init; }

        /// <summary>
        ///  Clearing price at or within the Reference Price Range using orders on the Auction Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.ReferencePrice ReferencePrice { get; init; }

        /// <summary>
        ///  Clearing price using Eligible Auction Orders
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.IndicativeClearingPrice IndicativeClearingPrice { get; init; }

        /// <summary>
        ///  Number of unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.ImbalanceShares ImbalanceShares { get; init; }

        /// <summary>
        ///  Side of the unpaired shares at the Reference Price using orders on the Auction Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.ImbalanceSideField ImbalanceSide { get; init; }

        /// <summary>
        ///  Number of extensions an auction received
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.ExtensionNumber ExtensionNumber { get; init; }

        /// <summary>
        ///  Projected time of the auction match
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.ScheduledAuctionTime ScheduledAuctionTime { get; init; }

        /// <summary>
        ///  Clearing price using orders on the Auction Book
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionBookClearingPrice AuctionBookClearingPrice { get; init; }

        /// <summary>
        ///  Reference priced used for the auction collar, if any
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.CollarReferencePrice CollarReferencePrice { get; init; }

        /// <summary>
        ///  Lower threshold price of the auction collar, if any
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.LowerAuctionCollar LowerAuctionCollar { get; init; }

        /// <summary>
        ///  Upper threshold price of the auction collar, if any
        /// </summary>
        public required global::Iex.IexEquities.Deep.IexTp.v1_08.UpperAuctionCollar UpperAuctionCollar { get; init; }

        /// <summary>
        ///  Creates the value of the Auction Information Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeAuctionType + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeTimestamp + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeSymbol + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizePairedShares + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeReferencePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeIndicativeClearingPrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeImbalanceShares + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeImbalanceSide + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeExtensionNumber + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeScheduledAuctionTime + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeAuctionBookClearingPrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeCollarReferencePrice + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeLowerAuctionCollar + global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.SizeUpperAuctionCollar;

    /// <summary>
    ///  Writes the Auction Information Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Auction Information Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoRoom, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterDestination);
        ref Layout auctionTypeWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        auctionTypeWriterLayout.AuctionType = value.AuctionType;
        auctionTypeWriterLayout.Timestamp = value.Timestamp;
        auctionTypeWriterLayout.Symbol = value.Symbol;
        auctionTypeWriterLayout.PairedShares = value.PairedShares;
        auctionTypeWriterLayout.ReferencePrice = value.ReferencePrice;
        auctionTypeWriterLayout.IndicativeClearingPrice = value.IndicativeClearingPrice;
        auctionTypeWriterLayout.ImbalanceShares = value.ImbalanceShares;
        auctionTypeWriterLayout.ImbalanceSide = value.ImbalanceSide;
        auctionTypeWriterLayout.ExtensionNumber = value.ExtensionNumber;
        auctionTypeWriterLayout.ScheduledAuctionTime = value.ScheduledAuctionTime;
        auctionTypeWriterLayout.AuctionBookClearingPrice = value.AuctionBookClearingPrice;
        auctionTypeWriterLayout.CollarReferencePrice = value.CollarReferencePrice;
        auctionTypeWriterLayout.LowerAuctionCollar = value.LowerAuctionCollar;
        auctionTypeWriterLayout.UpperAuctionCollar = value.UpperAuctionCollar;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
