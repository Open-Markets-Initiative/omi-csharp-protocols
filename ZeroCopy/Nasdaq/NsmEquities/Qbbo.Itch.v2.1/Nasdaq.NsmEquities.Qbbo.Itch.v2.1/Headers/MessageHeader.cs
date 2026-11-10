using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Mold Udp 64 Message Header
/// </summary>
public static class MessageHeader
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Message Header in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMessageLength + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMessageType;

        /// <summary>
        ///  Gets the Message Header laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Message Header.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Message Header at the start of the buffer, writes its fixed values, and gets the Message Header laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Message Header.
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
        ///  Length of data message not including this field
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageLength MessageLength;
        /// <summary>
        ///  Code identifying this message type
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageTypeField MessageType;
    }

    /// <summary>
    ///  The Message Header fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Creates the value of the Message Header, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMessageLength + global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.SizeMessageType;

    /// <summary>
    ///  Writes the Message Header to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Message Header.
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
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped ref bool messageLengthDependencyWriteUsed, scoped ref bool messageTypeDependencyWriteUsed, scoped in Value value)
    {
        ref bool messageLengthDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageLengthDependencyWriteUsed) ? ref cursor.MessageHeader_MessageLength_Used : ref messageLengthDependencyWriteUsed;
        ref bool messageTypeDependencyParentUsed = ref global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageTypeDependencyWriteUsed) ? ref cursor.MessageHeader_MessageType_Used : ref messageTypeDependencyWriteUsed;
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterDestination);
        cursor.MessageHeader_MessageLength_Used = false;
        cursor.MessageHeader_MessageType_Used = false;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
