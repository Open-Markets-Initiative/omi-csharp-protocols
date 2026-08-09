using System.Runtime.InteropServices;

namespace Pcap.CSharp;

/// <summary>
///  Allocation-free forward cursor over a classic pcap file. Each packet is read into one
///  reusable buffer and exposed as a <see cref="ReadOnlySpan{T}"/> via <see cref="Data"/>, so
///  walking a whole capture allocates nothing per packet.
///
///  <para>
///  Companion to <see cref="PcapReader.ReadPackets"/>: that iterator yields
///  <c>ReadOnlyMemory&lt;byte&gt;</c> for callers (e.g. the test harness) that must hold a packet
///  across a <c>yield</c>/<c>await</c>; this cursor is for the zero-copy/zero-alloc hot path
///  (the generated managers) and parses identically (same magic validation, same Ethernet
///  link-type requirement).
///  </para>
///
///  <para>
///  Contract: the span returned by <see cref="Data"/> is valid only until the next
///  <see cref="Advance"/> — the buffer is overwritten in place. The <c>ReadOnlySpan</c> type
///  enforces this; it cannot be stored, boxed, or held across the next read.
///  </para>
/// </summary>
public sealed class PcapCursor : IDisposable
{
    private readonly Stream stream;
    private byte[] buffer;
    private int length;

    private PcapCursor(Stream stream)
    {
        this.stream = stream;
        this.buffer = new byte[2048];
        length = 0;
        ReadGlobalHeader();
    }

    /// <summary>Opens <paramref name="path"/> and validates the pcap global header.</summary>
    public static PcapCursor Open(string path)
        => new(File.OpenRead(path));

    /// <summary>
    ///  The current packet's captured bytes. Valid only until the next <see cref="Advance"/>.
    /// </summary>
    public ReadOnlySpan<byte> Data => buffer.AsSpan(0, length);

    /// <summary>
    ///  Advances to the next packet. Returns <c>true</c> if a packet was read, or <c>false</c>
    ///  at end of file or on a truncated record.
    /// </summary>
    public bool Advance()
    {
        Span<byte> headerBytes = stackalloc byte[PcapRecordHeader.Size];
        if (!ReadExactly(headerBytes))
            return false; // clean EOF (no more record headers)

        var recordHeader = MemoryMarshal.Read<PcapRecordHeader>(headerBytes);
        var inclLen = (int)recordHeader.InclLen;
        if (inclLen < 0)
            return false;

        if (buffer.Length < inclLen)
            buffer = new byte[Math.Max(inclLen, buffer.Length * 2)];

        if (!ReadExactly(buffer.AsSpan(0, inclLen)))
            return false; // truncated final packet

        length = inclLen;
        return true;
    }

    public void Dispose()
        => stream.Dispose();

    private void ReadGlobalHeader()
    {
        Span<byte> headerBytes = stackalloc byte[PcapGlobalHeader.Size];
        if (!ReadExactly(headerBytes))
            throw new InvalidDataException("Not a pcap file (truncated global header)");

        var globalHeader = MemoryMarshal.Read<PcapGlobalHeader>(headerBytes);
        globalHeader.Validate();
    }

    /// <summary>
    ///  Fills <paramref name="destination"/> completely, looping over partial reads. Returns
    ///  false if the stream ends before the buffer is full (EOF or truncation).
    /// </summary>
    private bool ReadExactly(Span<byte> destination)
    {
        var total = 0;
        while (total < destination.Length)
        {
            var read = stream.Read(destination[total..]);
            if (read == 0)
                return false;
            total += read;
        }

        return true;
    }
}
