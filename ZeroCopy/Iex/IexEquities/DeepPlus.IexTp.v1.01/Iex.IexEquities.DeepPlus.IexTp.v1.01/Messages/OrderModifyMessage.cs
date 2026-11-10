using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
/// </summary>
public static class OrderModifyMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Order Modify Message in bytes.
        /// </summary>
        public const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeModifyFlags + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeOrderIdReference + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSize + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizePrice;

        /// <summary>
        ///  Gets the Order Modify Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Order Modify Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Order Modify Message at the start of the buffer, writes its fixed values, and gets the Order Modify Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Order Modify Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterNoRoom, nameof(destination));
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
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Modify Flags
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ModifyFlags ModifyFlags;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Symbol Symbol;
        /// <summary>
        ///  Order ID of the referenced order
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderIdReference OrderIdReference;
        /// <summary>
        ///  Quoted size
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Size Size;
        /// <summary>
        ///  Booking price on the IEX Order Book
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Price Price;
    }

    /// <summary>
    ///  The Order Modify Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Modify Flags
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ModifyFlags ModifyFlags { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Symbol Symbol { get; init; }

        /// <summary>
        ///  Order ID of the referenced order
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderIdReference OrderIdReference { get; init; }

        /// <summary>
        ///  Quoted size
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Size Size { get; init; }

        /// <summary>
        ///  Booking price on the IEX Order Book
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Price Price { get; init; }

        /// <summary>
        ///  Creates the value of the Order Modify Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeModifyFlags + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSymbol + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeOrderIdReference + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizeSize + global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.SizePrice;

    /// <summary>
    ///  Writes the Order Modify Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Modify Message.
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
        ref Layout modifyFlagsWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        modifyFlagsWriterLayout.ModifyFlags = value.ModifyFlags;
        modifyFlagsWriterLayout.Timestamp = value.Timestamp;
        modifyFlagsWriterLayout.Symbol = value.Symbol;
        modifyFlagsWriterLayout.OrderIdReference = value.OrderIdReference;
        modifyFlagsWriterLayout.Size = value.Size;
        modifyFlagsWriterLayout.Price = value.Price;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
