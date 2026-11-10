using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Open, high, low, close, and volume; the record of the ohlcv schemas
/// </summary>
public static class OhlcvMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Ohlcv Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeOpen + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeHigh + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeLow + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeClose + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeVolume;

        /// <summary>
        ///  Gets the Ohlcv Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Ohlcv Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Ohlcv Msg at the start of the buffer, writes its fixed values, and gets the Ohlcv Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Ohlcv Msg.
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
        ///  The open price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Open Open;
        /// <summary>
        ///  The high price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.High High;
        /// <summary>
        ///  The low price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Low Low;
        /// <summary>
        ///  The close price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Close Close;
        /// <summary>
        ///  The total volume traded during the aggregation period
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Volume Volume;
    }

    /// <summary>
    ///  The Ohlcv Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The open price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Open Open { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.Open.Absent;

        /// <summary>
        ///  The high price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.High High { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.High.Absent;

        /// <summary>
        ///  The low price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Low Low { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.Low.Absent;

        /// <summary>
        ///  The close price for the bar
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Close Close { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.Close.Absent;

        /// <summary>
        ///  The total volume traded during the aggregation period
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.Volume Volume { get; init; }

        /// <summary>
        ///  Creates the value of the Ohlcv Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeOpen + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeHigh + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeLow + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeClose + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeVolume;

    /// <summary>
    ///  Writes the Ohlcv Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Ohlcv Msg.
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
        ref Layout openWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        openWriterLayout.Open = value.Open;
        openWriterLayout.High = value.High;
        openWriterLayout.Low = value.Low;
        openWriterLayout.Close = value.Close;
        openWriterLayout.Volume = value.Volume;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
