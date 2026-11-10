using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  IEX disseminates a full pre-market spin of Security Directory Messages for all IEX-listed securities. After the pre-market spin, IEX will use the Security Directory Message to relay changes for an individual security.
/// </summary>
public static class SecurityDirectoryMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Security Directory Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSecurityDirectoryFlags + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeRoundLotSize + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeAdjustedPocPrice + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeLuldTier;

        /// <summary>
        ///  Gets the Security Directory Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Security Directory Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Security Directory Message at the start of the buffer, writes its fixed values, and gets the Security Directory Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Security Directory Message.
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
        ///  Security Directory Flags
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags SecurityDirectoryFlags;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.Symbol Symbol;
        /// <summary>
        ///  Number of shares that represent a round lot
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.RoundLotSize RoundLotSize;
        /// <summary>
        ///  Corporate action adjusted previous official closing price
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.AdjustedPocPrice AdjustedPocPrice;
        /// <summary>
        ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
        /// </summary>
        public global::Iex.IexEquities.Tops.IexTp.v1_66.LuldTierField LuldTier;
    }

    /// <summary>
    ///  The Security Directory Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Security Directory Flags
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.SecurityDirectoryFlags SecurityDirectoryFlags { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.Symbol Symbol { get; init; }

        /// <summary>
        ///  Number of shares that represent a round lot
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.RoundLotSize RoundLotSize { get; init; }

        /// <summary>
        ///  Corporate action adjusted previous official closing price
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.AdjustedPocPrice AdjustedPocPrice { get; init; }

        /// <summary>
        ///  Indicates which Limit Up-Limit Down price band calculation parameter is to be used
        /// </summary>
        public required global::Iex.IexEquities.Tops.IexTp.v1_66.LuldTierField LuldTier { get; init; }

        /// <summary>
        ///  Creates the value of the Security Directory Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSecurityDirectoryFlags + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeTimestamp + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeSymbol + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeRoundLotSize + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeAdjustedPocPrice + global::Iex.IexEquities.Tops.IexTp.v1_66.Parser.SizeLuldTier;

    /// <summary>
    ///  Writes the Security Directory Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Security Directory Message.
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
        ref Layout securityDirectoryFlagsWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        securityDirectoryFlagsWriterLayout.SecurityDirectoryFlags = value.SecurityDirectoryFlags;
        securityDirectoryFlagsWriterLayout.Timestamp = value.Timestamp;
        securityDirectoryFlagsWriterLayout.Symbol = value.Symbol;
        securityDirectoryFlagsWriterLayout.RoundLotSize = value.RoundLotSize;
        securityDirectoryFlagsWriterLayout.AdjustedPocPrice = value.AdjustedPocPrice;
        securityDirectoryFlagsWriterLayout.LuldTier = value.LuldTier;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
