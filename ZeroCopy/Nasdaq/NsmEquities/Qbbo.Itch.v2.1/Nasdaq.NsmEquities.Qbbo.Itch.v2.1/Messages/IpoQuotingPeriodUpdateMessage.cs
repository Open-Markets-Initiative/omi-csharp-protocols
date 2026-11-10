using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Indicates the anticipated IPO quotation release time of a security.
/// </summary>
public static class IpoQuotingPeriodUpdateMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Ipo Quoting Period Update Message in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTracking + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoQuotationReleaseTime + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoQuotationReleaseQualifier + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoPrice;

        /// <summary>
        ///  Gets the Ipo Quoting Period Update Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Ipo Quoting Period Update Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Ipo Quoting Period Update Message at the start of the buffer, writes its fixed values, and gets the Ipo Quoting Period Update Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Ipo Quoting Period Update Message.
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
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Tracking Tracking;
        /// <summary>
        ///  Timestamp
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp;
        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock;
        /// <summary>
        ///  Denotes the IPO release time, in seconds since midnight, for quotation to the nearest second.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotationReleaseTime IpoQuotationReleaseTime;
        /// <summary>
        ///  Denotes the IPO release qualifier
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotationReleaseQualifierField IpoQuotationReleaseQualifier;
        /// <summary>
        ///  Denotes the IPO price to be used for intraday net change calculations.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoPrice IpoPrice;
    }

    /// <summary>
    ///  The Ipo Quoting Period Update Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Nasdaq internal tracking number
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Tracking Tracking { get; init; }

        /// <summary>
        ///  Timestamp
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.TimeStamp TimeStamp { get; init; }

        /// <summary>
        ///  Denotes the security symbol for the issue in the execution system.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Stock Stock { get; init; }

        /// <summary>
        ///  Denotes the IPO release time, in seconds since midnight, for quotation to the nearest second.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotationReleaseTime IpoQuotationReleaseTime { get; init; }

        /// <summary>
        ///  Denotes the IPO release qualifier
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotationReleaseQualifierField IpoQuotationReleaseQualifier { get; init; }

        /// <summary>
        ///  Denotes the IPO price to be used for intraday net change calculations.
        /// </summary>
        public required global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoPrice IpoPrice { get; init; }

        /// <summary>
        ///  Creates the value of the Ipo Quoting Period Update Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTracking + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeTimeStamp + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeStock + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoQuotationReleaseTime + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoQuotationReleaseQualifier + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeIpoPrice;

    /// <summary>
    ///  Writes the Ipo Quoting Period Update Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Ipo Quoting Period Update Message.
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
        ref Layout ipoQuotingPeriodUpdateMessageTrackingWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.Tracking = value.Tracking;
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.TimeStamp = value.TimeStamp;
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.Stock = value.Stock;
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.IpoQuotationReleaseTime = value.IpoQuotationReleaseTime;
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.IpoQuotationReleaseQualifier = value.IpoQuotationReleaseQualifier;
        ipoQuotingPeriodUpdateMessageTrackingWriterLayout.IpoPrice = value.IpoPrice;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
