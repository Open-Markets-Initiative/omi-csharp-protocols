using System.Runtime.InteropServices;

namespace Pcap.CSharp;

/// <summary>
///  Reads a classic pcap file into one reusable packet buffer.
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

    /// <summary>Opens <paramref name="path"/> and validates its pcap header.</summary>
    /// <param name="path">Path to the pcap file.</param>
    /// <returns>A cursor for the file.</returns>
    public static PcapCursor Open(string path)
        => new(File.OpenRead(path));

    /// <summary>
    ///  Gets the current packet bytes. The span is valid until the next <see cref="Advance"/> call.
    /// </summary>
    public ReadOnlySpan<byte> Data => buffer.AsSpan(0, length);

    /// <summary>
    ///  Reads the next packet into <see cref="Data"/>.
    ///  Returns <c>false</c> at end of file or when a record is truncated.
    /// </summary>
    public bool Advance()
    {
        Span<byte> headerBytes = stackalloc byte[PcapRecordHeader.Size];
        if (!ReadExactly(headerBytes))
            return false; // No complete record header remains.

        var recordHeader = MemoryMarshal.Read<PcapRecordHeader>(headerBytes);
        var inclLen = (int)recordHeader.InclLen;
        if (inclLen < 0)
            return false;

        if (buffer.Length < inclLen)
            buffer = new byte[Math.Max(inclLen, buffer.Length * 2)];

        if (!ReadExactly(buffer.AsSpan(0, inclLen)))
            return false; // The final packet is truncated.

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
    ///  Reads all bytes into <paramref name="destination"/>.
    ///  Returns <c>false</c> when the stream ends before the destination is full.
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
