using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
/// </summary>
public static class ShortSalePriceTestStatusMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Short Sale Price Test Status Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeShortSalePriceTestStatus + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeDetail;

        /// <summary>
        ///  Gets the Short Sale Price Test Status Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Short Sale Price Test Status Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Short Sale Price Test Status Message at the start of the buffer, writes its fixed values, and gets the Short Sale Price Test Status Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Short Sale Price Test Status Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Reg. SHO short sale price test restriction status
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusField ShortSalePriceTestStatus;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Symbol Symbol;
        /// <summary>
        ///  Detail of the Reg. SHO short sale price test restriction status
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.DetailField Detail;
    }

    /// <summary>
    ///  The Short Sale Price Test Status Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Reg. SHO short sale price test restriction status
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusField ShortSalePriceTestStatus { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Symbol Symbol { get; init; }

        /// <summary>
        ///  Detail of the Reg. SHO short sale price test restriction status
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.DetailField Detail { get; init; }

        /// <summary>
        ///  Creates the value of the Short Sale Price Test Status Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeShortSalePriceTestStatus + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeDetail;

    /// <summary>
    ///  Writes the Short Sale Price Test Status Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Short Sale Price Test Status Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterNoRoom, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterDestination);
        ref Layout shortSalePriceTestStatusWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        shortSalePriceTestStatusWriterLayout.ShortSalePriceTestStatus = value.ShortSalePriceTestStatus;
        shortSalePriceTestStatusWriterLayout.Timestamp = value.Timestamp;
        shortSalePriceTestStatusWriterLayout.Symbol = value.Symbol;
        shortSalePriceTestStatusWriterLayout.Detail = value.Detail;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
