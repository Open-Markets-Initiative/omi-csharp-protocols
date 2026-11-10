using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
/// </summary>
public static class OfficialPriceMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Official Price Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizePriceType + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeOfficialPrice;

        /// <summary>
        ///  Gets the Official Price Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Official Price Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Official Price Message at the start of the buffer, writes its fixed values, and gets the Official Price Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Official Price Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Price type identifier
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.PriceTypeField PriceType;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.Symbol Symbol;
        /// <summary>
        ///  Official opening or closing price, as specified
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.OfficialPrice OfficialPrice;
    }

    /// <summary>
    ///  The Official Price Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Price type identifier
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.PriceTypeField PriceType { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.Symbol Symbol { get; init; }

        /// <summary>
        ///  Official opening or closing price, as specified
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.OfficialPrice OfficialPrice { get; init; }

        /// <summary>
        ///  Creates the value of the Official Price Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizePriceType + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeOfficialPrice;

    /// <summary>
    ///  Writes the Official Price Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Official Price Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterNoRoom, global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.WriterDestination);
        ref Layout priceTypeWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        priceTypeWriterLayout.PriceType = value.PriceType;
        priceTypeWriterLayout.Timestamp = value.Timestamp;
        priceTypeWriterLayout.Symbol = value.Symbol;
        priceTypeWriterLayout.OfficialPrice = value.OfficialPrice;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
