namespace Omi.Pcap;

using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Omi.Pcap.Decompressors;

/// <summary>
///  Reads the packets of a classic pcap or pcap-ng capture, one packet at a time.
/// </summary>
/// <remarks>
///  <see cref="Open"/> detects supported compression formats. A reader created from a stream reads from the current
///  position of the stream. Disposing the reader disposes its input stream. Instances are not thread-safe.
/// </remarks>
public sealed class PcapReader : IDisposable
{
    private const int ClassicHeaderSize = 24;
    private const int ClassicLinkTypeOffset = 20;
    private const int ClassicRecordHeaderSize = 16;
    private const uint SectionHeaderBlockType = 0x0a0d0d0a;
    private const uint InterfaceBlockType = 1;
    private const uint ObsoletePacketBlockType = 2;
    private const uint SimplePacketBlockType = 3;
    private const uint EnhancedPacketBlockType = 6;
    private const uint ByteOrderMagic = 0x1a2b3c4d;
    private const uint SwappedByteOrderMagic = 0x4d3c2b1a;
    private const int BlockHeaderSize = 8;
    private const int ByteOrderMagicOffset = 8;
    private const int BlockTrailerSize = 4;
    private const int MinimumBlockSize = BlockHeaderSize + BlockTrailerSize;
    private const int MinimumSectionHeaderSize = 28;
    private const int MinimumInterfaceSize = 20;
    private const int InterfaceLinkTypeOffset = 8;
    private const int InterfaceOptionsOffset = 16;
    private const int EnhancedPacketDataOffset = 28;
    private const int MinimumEnhancedPacketSize = EnhancedPacketDataOffset + BlockTrailerSize;
    private const uint LinkTypeMask = 0xFFFF;
    private const ushort EndOfOptionsCode = 0;
    private const ushort TimestampResolutionOptionCode = 9;
    private const ushort TimestampOffsetOptionCode = 14;
    private const int TimestampResolutionOptionLength = 1;
    private const int TimestampOffsetOptionLength = 8;
    private const byte MicrosecondResolution = 6;
    private const byte BinaryResolutionFlag = 0x80;
    private const byte ResolutionExponentMask = 0x7f;
    private const int NanosecondExponent = 9;
    private const ulong NanosecondsPerSecond = 1_000_000_000UL;

    private readonly Stream stream;
    private readonly PcapBuffer buffer;
    private readonly List<Interface> interfaces = new();

    private bool swap;
    private bool nanoseconds;
    private bool pcapng;
    private bool done;

    private int pendingConsume;

    private ulong timestampNanos;
    private LinkType linkType;
    private int packetOffset;
    private int packetLength;

    /// <summary>
    ///  Gets the capture time of the current packet, in nanoseconds since 1970-01-01 00:00:00 UTC.
    /// </summary>
    /// <remarks>
    ///  The value is zero before the first call to <see cref="Advance"/> and after <see cref="Advance"/> returns <see langword="false"/> or throws.
    ///  A pcap-ng timestamp that is not a whole number of nanoseconds is truncated to whole nanoseconds.
    /// </remarks>
    public ulong TimestampNanoseconds => timestampNanos;

    /// <summary>
    ///  Gets the link type that identifies the header at the start of <see cref="Data"/>.
    /// </summary>
    /// <remarks>
    ///  The value comes from the pcap global header, or from the pcap-ng interface of the current packet.
    ///  It is valid after <see cref="Advance"/> returns <see langword="true"/>.
    /// </remarks>
    public LinkType LinkType => linkType;

    /// <summary>
    ///  Gets the captured bytes of the current packet, starting at the link-layer header.
    /// </summary>
    /// <remarks>
    ///  The span is valid after <see cref="Advance"/> returns <see langword="true"/>, until the next call to
    ///  <see cref="Advance"/> or <see cref="Dispose"/>. The span can be shorter than the original packet when the
    ///  capture stores only part of it. The span is empty when there is no current packet: before the first call to
    ///  <see cref="Advance"/>, and after <see cref="Advance"/> returns <see langword="false"/> or throws.
    /// </remarks>
    public ReadOnlySpan<byte> Data
        => packetLength > 0 ? buffer.Span.Slice(packetOffset, packetLength) : default;

    /// <summary>
    ///  Gets a pointer to the first byte of <see cref="Data"/>.
    /// </summary>
    /// <remarks>
    ///  The packet bytes do not move while the pointer is valid, so a caller can keep the pointer without a
    ///  <see langword="fixed"/> statement. The pointer is valid after <see cref="Advance"/> returns
    ///  <see langword="true"/>, until the next call to <see cref="Advance"/> or <see cref="Dispose"/>. A later packet
    ///  can be in a different buffer, so get the pointer again for each packet. The pointer is null when
    ///  <see cref="Data"/> is empty.
    /// </remarks>
    public unsafe byte* DataPointer
        => (byte*)Unsafe.AsPointer(ref MemoryMarshal.GetReference(Data));

    /// <summary>
    ///  Gets the position of the current packet in the capture, counted from 1.
    /// </summary>
    /// <remarks>
    ///  Each packet that is counted in <see cref="SkippedPackets"/> also takes a number, so the numbers of the
    ///  returned packets can have gaps. Pcap-ng blocks that are not packet blocks do not take a number.
    /// </remarks>
    public long PacketNumber { get; private set; }

    /// <summary>
    ///  Gets the number of packets that <see cref="Advance"/> skipped because this library cannot read them.
    /// </summary>
    /// <remarks>
    ///  A packet is skipped and counted when it is in a pcap-ng Simple Packet Block or obsolete Packet Block, when its
    ///  pcap-ng interface has an unsupported link type, or when it is an IPv4 fragment that does not hold a whole TCP
    ///  segment or UDP datagram. Other pcap-ng blocks, such as name resolution, statistics, custom, and unknown
    ///  blocks, are stepped over and are not counted.
    /// </remarks>
    public long SkippedPackets { get; private set; }

    /// <summary>
    ///  Opens a capture file, and decompresses it when it uses a supported compression format.
    /// </summary>
    /// <param name="path">The path of the capture file.</param>
    /// <returns>A reader that owns the opened file stream.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="path"/> is empty or contains a null character.</exception>
    /// <exception cref="InvalidDataException">The capture header is not valid or states an unsupported link type, or the compressed content is corrupt, truncated, or uses an unsupported feature.</exception>
    /// <exception cref="IOException">The file is not found, or an I/O error occurs while opening or reading the file.</exception>
    /// <exception cref="UnauthorizedAccessException">The caller does not have permission to read the file.</exception>
    /// <remarks>
    ///  The method identifies the format from the first bytes of the file. It decompresses gzip, bzip2, XZ, LZ4 frame,
    ///  and Zstandard content, and reads other content unchanged. For XZ content, only the first XZ stream is
    ///  decompressed.
    /// </remarks>
    public static PcapReader Open(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        return new(Decompressor.Open(path));
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="PcapReader"/> class that reads an uncompressed capture stream.
    /// </summary>
    /// <param name="stream">The readable capture stream, which the reader owns and disposes. If the constructor throws, it disposes this stream.</param>
    /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <see langword="null"/>.</exception>
    /// <exception cref="InvalidDataException">The capture header is not valid, or states an unsupported link type.</exception>
    /// <exception cref="IOException">An I/O error occurs while reading the stream.</exception>
    /// <remarks>
    ///  The reader starts at the current position of the stream. This constructor does not detect or decompress
    ///  compressed content: use <see cref="Open"/> for a compressed file, or pass a decompression stream.
    /// </remarks>
    public PcapReader(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        this.stream = stream;
        try
        {
            this.buffer = new PcapBuffer(stream);
            ParseFileHeader();
        }
        catch
        {
            stream.Dispose();
            throw;
        }
    }

    /// <summary>
    ///  Moves to the next captured packet that this library can read.
    /// </summary>
    /// <returns><see langword="true"/> if the reader moved to a packet; otherwise, <see langword="false"/> at the end of the capture.</returns>
    /// <exception cref="InvalidDataException">The capture is truncated or malformed, a pcap-ng packet timestamp plus its interface offset is outside the range of <see cref="TimestampNanoseconds"/>, or the compressed content of a reader from <see cref="Open"/> is corrupt, truncated, or uses an unsupported feature.</exception>
    /// <exception cref="IOException">An I/O error occurs while reading the input stream.</exception>
    /// <remarks>
    ///  The call ends the validity of <see cref="Data"/> and <see cref="DataPointer"/> for the previous packet.
    ///  The method returns packets from classic pcap records and pcap-ng Enhanced Packet Blocks. It skips a packet in a
    ///  pcap-ng Simple Packet Block or obsolete Packet Block, a packet whose pcap-ng interface has an unsupported link
    ///  type, and an IPv4 fragment that does not hold a whole TCP segment or UDP datagram; each skipped packet
    ///  increments <see cref="PacketNumber"/> and <see cref="SkippedPackets"/>. It steps over all other pcap-ng blocks
    ///  and does not count them.
    ///  A packet record or pcap-ng block that states a length of more than 16,777,216 bytes (16 MiB) is malformed.
    ///  After the method returns <see langword="false"/> or throws, later calls return <see langword="false"/>.
    /// </remarks>
    public bool Advance()
    {
        try
        {
            while (!done)
            {
                if (pendingConsume > 0)
                {
                    buffer.Consume(pendingConsume);
                    pendingConsume = 0;
                }

                if (!(pcapng ? AdvancePcapng() : AdvanceClassic()))
                {
                    End();
                    return false;
                }

                PacketNumber++;
                if (packetLength >= 0 && !NetworkHeaders.IsPartialFragment(Data, linkType))
                {
                    return true;
                }
                SkippedPackets++;
            }
        }
        catch
        {
            End();
            throw;
        }

        return false;
    }

    /// <summary>
    ///  Disposes the input stream of the reader.
    /// </summary>
    /// <remarks>
    ///  After this call, <see cref="Advance"/> returns <see langword="false"/> and <see cref="Data"/> is empty.
    /// </remarks>
    public void Dispose()
    {
        End();
        stream.Dispose();
    }

    private void End()
    {
        done = true;
        timestampNanos = 0;
        packetOffset = 0;
        packetLength = 0;
    }

    private void ParseFileHeader()
    {
        if (!buffer.Ensure(4)) { throw new InvalidDataException("file too short for pcap header"); }

        var magic = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Span);

        if (magic == SectionHeaderBlockType)
        {
            pcapng = true;
            ReadSectionHeader();
        }
        else
        {
            ParseClassicHeader(magic);
        }
    }

    private void ParseClassicHeader(uint magic)
    {
        switch (magic)
        {
            case 0xa1b2c3d4: swap = false; nanoseconds = false; break;
            case 0xd4c3b2a1: swap = true;  nanoseconds = false; break;
            case 0xa1b23c4d: swap = false; nanoseconds = true;  break;
            case 0x4d3cb2a1: swap = true;  nanoseconds = true;  break;
            default: throw new InvalidDataException($"invalid pcap magic: 0x{magic:X8}");
        }

        if (!buffer.Ensure(ClassicHeaderSize)) { throw new InvalidDataException("truncated pcap global header"); }
        var value = Read32(buffer.Span[ClassicLinkTypeOffset..]) & LinkTypeMask;
        linkType = ToLinkType(value) ?? throw new InvalidDataException($"unsupported pcap link type {value}");
        buffer.Consume(ClassicHeaderSize);
    }

    private bool AdvanceClassic()
    {
        if (!Ensure(ClassicRecordHeaderSize, "packet record")) { return false; }

        var span = buffer.Span;
        var tsSec   = Read32(span);
        var tsUsec  = Read32(span[4..]);
        var capLen  = Read32(span[8..]);
        var size = CheckSize(capLen, "a packet record");
        buffer.Consume(ClassicRecordHeaderSize);

        if (!buffer.Ensure(size)) { throw new InvalidDataException("the file ends in the middle of a packet record"); }

        packetOffset = 0;
        packetLength = size;
        pendingConsume = size;

        timestampNanos = nanoseconds
            ? (ulong)tsSec * 1_000_000_000UL + tsUsec
            : (ulong)tsSec * 1_000_000_000UL + (ulong)tsUsec * 1_000UL;

        return true;
    }

    /// <summary>
    ///  The block length can be read only after the byte order magic that follows it.
    /// </summary>
    private void ReadSectionHeader()
    {
        if (!buffer.Ensure(ByteOrderMagicOffset + 4)) { throw new InvalidDataException("the file ends in the middle of a pcap-ng block"); }

        var bom = BinaryPrimitives.ReadUInt32LittleEndian(buffer.Span[ByteOrderMagicOffset..]);
        if (bom == ByteOrderMagic) { swap = false; }
        else if (bom == SwappedByteOrderMagic) { swap = true; }
        else { throw new InvalidDataException("invalid pcap-ng byte order magic"); }

        var size = CheckSize(Read32(buffer.Span[4..]), "a pcap-ng block");
        if (size < MinimumSectionHeaderSize) { throw new InvalidDataException($"a pcap-ng Section Header Block states {size} bytes"); }
        if (!buffer.Ensure(size)) { throw new InvalidDataException("the file ends in the middle of a pcap-ng block"); }

        interfaces.Clear();
        buffer.Consume(size);
    }

    private bool AdvancePcapng()
    {
        while (true)
        {
            if (!Ensure(BlockHeaderSize, "pcap-ng block")) { return false; }

            var blockType = Read32(buffer.Span);
            if (blockType == SectionHeaderBlockType)
            {
                ReadSectionHeader();
                continue;
            }

            var blockLen = CheckSize(Read32(buffer.Span[4..]), "a pcap-ng block");
            if (blockLen < MinimumBlockSize) { throw new InvalidDataException($"a pcap-ng block states {blockLen} bytes"); }
            if (!buffer.Ensure(blockLen)) { throw new InvalidDataException("the file ends in the middle of a pcap-ng block"); }

            var block = buffer.Span[..blockLen];

            switch (blockType)
            {
                case InterfaceBlockType:
                    ParsePcapngInterface(block);
                    break;
                case EnhancedPacketBlockType:
                    ParsePcapngEnhancedPacket(block);
                    pendingConsume = blockLen;
                    return true;
                case SimplePacketBlockType:
                case ObsoletePacketBlockType:
                    packetLength = -1;
                    pendingConsume = blockLen;
                    return true;
            }

            buffer.Consume(blockLen);
        }
    }

    private void ParsePcapngInterface(ReadOnlySpan<byte> block)
    {
        if (block.Length < MinimumInterfaceSize) { throw new InvalidDataException($"a pcap-ng Interface Description Block states {block.Length} bytes"); }

        var iface = new Interface
        {
            LinkType = ToLinkType(Read16(block[InterfaceLinkTypeOffset..])),
            TimestampResolution = MicrosecondResolution,
        };

        ParseInterfaceOptions(block[InterfaceOptionsOffset..^BlockTrailerSize], ref iface);
        interfaces.Add(iface);
    }

    private void ParseInterfaceOptions(ReadOnlySpan<byte> options, ref Interface iface)
    {
        var position = 0;
        while (position + 4 <= options.Length)
        {
            var code = Read16(options[position..]);
            var len  = Read16(options[(position + 2)..]);
            position += 4;

            if (code == EndOfOptionsCode) { break; }
            if (position + len > options.Length) { break; }

            if (code == TimestampResolutionOptionCode && len == TimestampResolutionOptionLength)
            {
                iface.TimestampResolution = options[position];
            }
            else if (code == TimestampOffsetOptionCode && len == TimestampOffsetOptionLength)
            {
                iface.TimestampOffsetSeconds = Read64(options[position..]);
            }

            position += len;
            position = (position + 3) & ~3;
        }
    }

    /// <summary>
    ///  Sets a negative packet length when the interface has an unsupported link type.
    /// </summary>
    private void ParsePcapngEnhancedPacket(ReadOnlySpan<byte> block)
    {
        if (block.Length < MinimumEnhancedPacketSize) { throw new InvalidDataException($"a pcap-ng Enhanced Packet Block states {block.Length} bytes"); }

        var ifaceId = Read32(block[8..]);
        var tsHigh  = Read32(block[12..]);
        var tsLow   = Read32(block[16..]);
        var capLen  = Read32(block[20..]);

        if (capLen > (uint)(block.Length - MinimumEnhancedPacketSize))
        {
            throw new InvalidDataException($"a pcap-ng Enhanced Packet Block of {block.Length} bytes states {capLen} captured bytes");
        }

        if (ifaceId >= interfaces.Count)
        {
            throw new InvalidDataException($"pcap-ng packet refers to undeclared interface {ifaceId}");
        }

        var iface = interfaces[(int)ifaceId];
        packetOffset = EnhancedPacketDataOffset;
        if (iface.LinkType is not { } known)
        {
            packetLength = -1;
            return;
        }

        linkType = known;
        packetLength = (int)capLen;
        timestampNanos = ToNanoseconds(((ulong)tsHigh << 32) | tsLow, iface);
    }

    /// <summary>
    ///  Throws when the result does not fit in a <see cref="ulong"/>.
    /// </summary>
    private static ulong ToNanoseconds(ulong ticks, in Interface iface)
    {
        var exponent = iface.TimestampResolution & ResolutionExponentMask;
        Int128 total;
        if ((iface.TimestampResolution & BinaryResolutionFlag) != 0)
        {
            total = (Int128)((UInt128)ticks * NanosecondsPerSecond >> exponent);
        }
        else if (exponent <= NanosecondExponent)
        {
            total = (Int128)ticks * PowersOfTen[NanosecondExponent - exponent];
        }
        else if (exponent - NanosecondExponent < PowersOfTen.Length)
        {
            total = ticks / PowersOfTen[exponent - NanosecondExponent];
        }
        else
        {
            total = 0;
        }

        total += (Int128)iface.TimestampOffsetSeconds * NanosecondsPerSecond;
        return total >= 0 && total <= ulong.MaxValue
            ? (ulong)total
            : throw new InvalidDataException($"a pcap-ng packet timestamp of {ticks} ticks with an offset of {iface.TimestampOffsetSeconds} seconds is outside the range of 0 to {ulong.MaxValue} nanoseconds since 1970-01-01 00:00:00 UTC");
    }

    private static ReadOnlySpan<ulong> PowersOfTen =>
    [
        1UL, 10UL, 100UL, 1_000UL, 10_000UL, 100_000UL, 1_000_000UL, 10_000_000UL, 100_000_000UL, 1_000_000_000UL,
        10_000_000_000UL, 100_000_000_000UL, 1_000_000_000_000UL, 10_000_000_000_000UL, 100_000_000_000_000UL,
        1_000_000_000_000_000UL, 10_000_000_000_000_000UL, 100_000_000_000_000_000UL, 1_000_000_000_000_000_000UL,
        10_000_000_000_000_000_000UL,
    ];

    /// <summary>
    ///  Returns <see langword="false"/> at a clean end of the file, and throws when the file ends inside the header.
    /// </summary>
    private bool Ensure(int count, string record)
    {
        if (buffer.Ensure(count)) { return true; }
        if (buffer.Available == 0) { return false; }
        throw new InvalidDataException($"the file ends in the middle of a {record}");
    }

    private static int CheckSize(uint size, string record)
        => size <= PcapBuffer.MaxSize
            ? (int)size
            : throw new InvalidDataException($"{record} states {size} bytes, more than the limit of {PcapBuffer.MaxSize} bytes");

    private static LinkType? ToLinkType(uint value)
        => value <= ushort.MaxValue && Enum.IsDefined((LinkType)value) ? (LinkType)value : null;

    private ushort Read16(ReadOnlySpan<byte> bytes)
    {
        var v = BinaryPrimitives.ReadUInt16LittleEndian(bytes);
        return swap ? BinaryPrimitives.ReverseEndianness(v) : v;
    }

    private uint Read32(ReadOnlySpan<byte> bytes)
    {
        var v = BinaryPrimitives.ReadUInt32LittleEndian(bytes);
        return swap ? BinaryPrimitives.ReverseEndianness(v) : v;
    }

    private long Read64(ReadOnlySpan<byte> bytes)
    {
        var v = BinaryPrimitives.ReadInt64LittleEndian(bytes);
        return swap ? BinaryPrimitives.ReverseEndianness(v) : v;
    }

    /// <summary>
    ///  Holds the values of the if_tsresol and if_tsoffset options as the Interface Description Block states them.
    /// </summary>
    private struct Interface
    {
        public LinkType? LinkType;
        public byte TimestampResolution;
        public long TimestampOffsetSeconds;
    }
}
