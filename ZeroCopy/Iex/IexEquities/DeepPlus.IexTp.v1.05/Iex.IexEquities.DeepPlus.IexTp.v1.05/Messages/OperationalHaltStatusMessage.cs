using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
/// </summary>
public static class OperationalHaltStatusMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Operational Halt Status Message in bytes.
        /// </summary>
        public const int Size = global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeOperationalHaltStatus + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSymbol;

        /// <summary>
        ///  Gets the Operational Halt Status Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Operational Halt Status Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Operational Halt Status Message at the start of the buffer, writes its fixed values, and gets the Operational Halt Status Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Operational Halt Status Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.WriterNoRoom, nameof(destination));
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Operational halt status identifier
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusField OperationalHaltStatus;
        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Timestamp Timestamp;
        /// <summary>
        ///  Security identifier
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Symbol Symbol;
    }

    /// <summary>
    ///  The Operational Halt Status Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Operational halt status identifier
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.OperationalHaltStatusField OperationalHaltStatus { get; init; }

        /// <summary>
        ///  Time stamp of the system event
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  Security identifier
        /// </summary>
        public required global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Symbol Symbol { get; init; }

        /// <summary>
        ///  Creates the value of the Operational Halt Status Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeOperationalHaltStatus + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeTimestamp + global::Iex.IexEquities.DeepPlus.IexTp.v1_05.Parser.SizeSymbol;

    /// <summary>
    ///  Writes the Operational Halt Status Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Operational Halt Status Message.
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
        ref Layout operationalHaltStatusWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        operationalHaltStatusWriterLayout.OperationalHaltStatus = value.OperationalHaltStatus;
        operationalHaltStatusWriterLayout.Timestamp = value.Timestamp;
        operationalHaltStatusWriterLayout.Symbol = value.Symbol;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
