using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  A symbol mapping message, which maps a symbol of one SType to another
/// </summary>
public static class SymbolMappingMsg
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Symbol Mapping Msg in bytes.
        /// </summary>
        public const int Size = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStypeInSymbol + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStypeOutSymbol + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeDummy4 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStartTs + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeEndTs;

        /// <summary>
        ///  Gets the Symbol Mapping Msg laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Symbol Mapping Msg.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Symbol Mapping Msg at the start of the buffer, writes its fixed values, and gets the Symbol Mapping Msg laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Symbol Mapping Msg.
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
        ///  The input symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StypeInSymbol StypeInSymbol;
        /// <summary>
        ///  The output symbol
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StypeOutSymbol StypeOutSymbol;
        /// <summary>
        ///  Reserved, 4 bytes
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.Dummy4 Dummy4;
        /// <summary>
        ///  The start of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StartTs StartTs;
        /// <summary>
        ///  The end of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.EndTs EndTs;
    }

    /// <summary>
    ///  The Symbol Mapping Msg fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The input symbol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.StypeInSymbol StypeInSymbol { get; init; }

        /// <summary>
        ///  The output symbol
        /// </summary>
        public required global::Databento.MarketData.Normalized.Dbn.v1.StypeOutSymbol StypeOutSymbol { get; init; }

        /// <summary>
        ///  The start of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.StartTs StartTs { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.StartTs.Absent;

        /// <summary>
        ///  The end of the mapping interval expressed as the number of nanoseconds since the UNIX epoch
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.EndTs EndTs { get; init; } = global::Databento.MarketData.Normalized.Dbn.v1.EndTs.Absent;

        /// <summary>
        ///  Creates the value of the Symbol Mapping Msg, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStypeInSymbol + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStypeOutSymbol + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeDummy4 + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeStartTs + global::Databento.MarketData.Normalized.Dbn.v1.Parser.SizeEndTs;

    /// <summary>
    ///  Writes the Symbol Mapping Msg to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Symbol Mapping Msg.
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
        ref Layout stypeInSymbolWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        stypeInSymbolWriterLayout.StypeInSymbol = value.StypeInSymbol;
        stypeInSymbolWriterLayout.StypeOutSymbol = value.StypeOutSymbol;
        stypeInSymbolWriterLayout.Dummy4 = default;
        stypeInSymbolWriterLayout.StartTs = value.StartTs;
        stypeInSymbolWriterLayout.EndTs = value.EndTs;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
