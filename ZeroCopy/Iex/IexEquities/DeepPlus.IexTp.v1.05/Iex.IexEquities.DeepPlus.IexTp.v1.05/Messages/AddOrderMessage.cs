using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  A displayed order that has been added to the IEX Book
/// </summary>
public static class AddOrderMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Add Order Message in bytes.
        /// </summary>
        public const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSide + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeOrderId + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSize + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizePrice;

        /// <summary>
        ///  Gets the Add Order Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Add Order Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Add Order Message at the start of the buffer, writes its fixed values, and gets the Add Order Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Add Order Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterNoRoom, nameof(destination));
            destination[..LayoutSize].Clear();
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Side of order
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SideField Side;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Symbol Symbol;
        /// <summary>
        ///  Order ID of new order
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderId OrderId;
        /// <summary>
        ///  Quoted size
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Size Size;
        /// <summary>
        ///  Booking price on the IEX Order Book
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Price Price;
    }

    /// <summary>
    ///  The Add Order Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Side of order
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.SideField Side { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Symbol Symbol { get; init; }

        /// <summary>
        ///  Order ID of new order
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OrderId OrderId { get; init; }

        /// <summary>
        ///  Quoted size
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Size Size { get; init; }

        /// <summary>
        ///  Booking price on the IEX Order Book
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Price Price { get; init; }

        /// <summary>
        ///  Creates the value of the Add Order Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSide + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeOrderId + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSize + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizePrice;

    /// <summary>
    ///  Writes the Add Order Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Add Order Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterNoRoom, global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterDestination);
        ref Layout sideWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        sideWriterLayout.Side = value.Side;
        sideWriterLayout.Timestamp = value.Timestamp;
        sideWriterLayout.Symbol = value.Symbol;
        sideWriterLayout.OrderId = value.OrderId;
        sideWriterLayout.Size = value.Size;
        sideWriterLayout.Price = value.Price;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
