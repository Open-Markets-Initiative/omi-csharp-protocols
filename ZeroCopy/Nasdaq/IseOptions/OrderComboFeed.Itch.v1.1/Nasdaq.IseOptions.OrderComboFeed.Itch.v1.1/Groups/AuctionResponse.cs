using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Auction Response information
/// </summary>
public static class AuctionResponse
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Auction Response in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeResponsePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeResponseSize;

        /// <summary>
        ///  Gets the Auction Response laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Auction Response.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Auction Response at the start of the buffer, writes its fixed values, and gets the Auction Response laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Auction Response.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Price of the auction response in fixed point format with 6 whole number places followed by 4 decimal digits. The response, if shown (zero if not shown) is the best response on the contra side.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ResponsePrice ResponsePrice;
        /// <summary>
        ///  Size of the auction response (zero if not shown).
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ResponseSize ResponseSize;
    }

    /// <summary>
    ///  The Auction Response fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Price of the auction response in fixed point format with 6 whole number places followed by 4 decimal digits. The response, if shown (zero if not shown) is the best response on the contra side.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ResponsePrice ResponsePrice { get; init; }

        /// <summary>
        ///  Size of the auction response (zero if not shown).
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ResponseSize ResponseSize { get; init; }

        /// <summary>
        ///  Creates the value of the Auction Response, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeResponsePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeResponseSize;

    /// <summary>
    ///  Writes the Auction Response to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Auction Response.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterDestination);
        ref Layout responsePriceWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        responsePriceWriterLayout.ResponsePrice = value.ResponsePrice;
        responsePriceWriterLayout.ResponseSize = value.ResponseSize;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
