using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  Nasdaq will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue
/// </summary>
public static class MarketParticipantPositionMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Market Participant Position Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMpid + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePrimaryMarketMaker + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMarketMakerMode + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMarketParticipantState;

        /// <summary>
        ///  Gets the Market Participant Position Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Market Participant Position Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Market Participant Position Message at the start of the buffer, writes its fixed values, and gets the Market Participant Position Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Market Participant Position Message.
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
        ///  Denotes the market participant identifier for which the position message is being generated
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Mpid Mpid;
        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock;
        /// <summary>
        ///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PrimaryMarketMakerField PrimaryMarketMaker;
        /// <summary>
        ///  Indicates the quoting participant's registration status in relation to SEC Rules 101 and 104 of Regulation M
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketMakerModeField MarketMakerMode;
        /// <summary>
        ///  Indicates the market participant's current registration status in the issue
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantStateField MarketParticipantState;
    }

    /// <summary>
    ///  The Market Participant Position Message fields to write.
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
        ///  Denotes the market participant identifier for which the position message is being generated
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Mpid Mpid { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the NASDAQ execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Stock Stock { get; init; }

        /// <summary>
        ///  Indicates if the market participant firm qualifies as a Primary Market Maker in accordance with NASDAQ marketplace rules
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PrimaryMarketMakerField PrimaryMarketMaker { get; init; }

        /// <summary>
        ///  Indicates the quoting participant's registration status in relation to SEC Rules 101 and 104 of Regulation M
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketMakerModeField MarketMakerMode { get; init; }

        /// <summary>
        ///  Indicates the market participant's current registration status in the issue
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantStateField MarketParticipantState { get; init; }

        /// <summary>
        ///  Creates the value of the Market Participant Position Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStockLocate + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTrackingNumber + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeTimestamp + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMpid + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeStock + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizePrimaryMarketMaker + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMarketMakerMode + global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeMarketParticipantState;

    /// <summary>
    ///  Writes the Market Participant Position Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Market Participant Position Message.
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
        ref Layout marketParticipantPositionMessageStockLocateWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        marketParticipantPositionMessageStockLocateWriterLayout.StockLocate = value.StockLocate;
        marketParticipantPositionMessageStockLocateWriterLayout.TrackingNumber = value.TrackingNumber;
        marketParticipantPositionMessageStockLocateWriterLayout.Timestamp = value.Timestamp;
        marketParticipantPositionMessageStockLocateWriterLayout.Mpid = value.Mpid;
        marketParticipantPositionMessageStockLocateWriterLayout.Stock = value.Stock;
        marketParticipantPositionMessageStockLocateWriterLayout.PrimaryMarketMaker = value.PrimaryMarketMaker;
        marketParticipantPositionMessageStockLocateWriterLayout.MarketMakerMode = value.MarketMakerMode;
        marketParticipantPositionMessageStockLocateWriterLayout.MarketParticipantState = value.MarketParticipantState;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
