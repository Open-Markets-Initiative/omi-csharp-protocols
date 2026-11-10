using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

/// <summary>
///  The Unsequenced Data Packets act as an envelope to carry the actual data messages that are transferred from the client to the server
/// </summary>
public static class UnsequencedDataPacket
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
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
            if (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  Value identifying unsequenced message type
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedMessageType UnsequencedMessageType;
    }

    /// <summary>
    ///  Reads the Unsequenced Data Packet in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ushort packetLengthValue;
        private ref readonly Layout layout => ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(this.bytes[this.start..this.end]);

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ushort packetLengthValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.packetLengthValue = packetLengthValue;
        }

        /// <summary>
        ///  Gets the bytes of the Unsequenced Data Packet.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Gets the fixed fields at the start of the Unsequenced Data Packet.
        /// </summary>
        public ref readonly Layout Layout => ref this.layout;

        /// <summary>
        ///  Value identifying unsequenced message type
        /// </summary>
        public ref readonly UnsequencedMessageType UnsequencedMessageType => ref this.layout.UnsequencedMessageType;

        /// <summary>
        ///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        public ReadOnlySpan<byte> UnsequencedMessage
        {
            get
            {
                int unsequencedMessageReaderPosition = Parser.Valid(Parser.UnsequencedDataPacketUnsequencedMessageStart(this.bytes, this.start, this.end, this.start, this.packetLengthValue, out var packetLengthValue));
                int unsequencedMessageReaderEnd = Parser.Valid(Parser.UnsequencedDataPacketUnsequencedMessageEnd(this.bytes, unsequencedMessageReaderPosition, this.end, this.start, packetLengthValue));
                return this.bytes[unsequencedMessageReaderPosition..unsequencedMessageReaderEnd];
            }
        }
    }

    /// <summary>
    ///  The Unsequenced Data Packet fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        /// <summary>
        ///  Value identifying unsequenced message type
        /// </summary>
        public required global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedMessageType UnsequencedMessageType { get; init; }

        /// <summary>
        ///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
        /// </summary>
        public required global::System.ReadOnlySpan<byte> UnsequencedMessage { get; init; }

        /// <summary>
        ///  Creates the value of the Unsequenced Data Packet, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SizeUnsequencedMessageType;
    private const int packetLengthDependencyWriteDistanceFromUnsequencedDataPacket = -3;
    private const int packetLengthDependencyWriteDistanceFromUnsequencedMessage = -4;

    /// <summary>
    ///  Writes the Unsequenced Data Packet to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Unsequenced Data Packet.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        ref Layout unsequencedMessageTypeWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        unsequencedMessageTypeWriterLayout.UnsequencedMessageType = value.UnsequencedMessageType;
        cursor.Index += LayoutSize;
        int unsequencedMessageWriterLength = value.UnsequencedMessage.Length;
        if (unsequencedMessageWriterLength > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterDestination);
        int unsequencedMessageWriterPosition = cursor.Index;
        value.UnsequencedMessage.CopyTo(bytes[unsequencedMessageWriterPosition..]);
        cursor.Index += unsequencedMessageWriterLength;
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.UnsequencedDataPacketWriter.UnsequencedMessageSizeRule0(bytes, ref cursor, unsequencedMessageWriterPosition < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : unsequencedMessageWriterPosition + packetLengthDependencyWriteDistanceFromUnsequencedMessage, ref packetLengthDependencyWriteUsed, unsequencedMessageWriterPosition, unsequencedMessageWriterPosition + unsequencedMessageWriterLength);
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.UnsequencedDataPacketWriter.UnsequencedDataPacketSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : start + packetLengthDependencyWriteDistanceFromUnsequencedDataPacket, ref packetLengthDependencyWriteUsed, start, cursor.Index);
        return bytes[start..cursor.Index];
    }
}
