namespace Omi.Pcap;

using Omi.Pcap.Decompressors;

/// <summary>
///  Opens a file that a protocol delivers as a file rather than as packets, such as a Databento DBN file, for reading its bytes
///  in order.
/// </summary>
/// <remarks>
///  <see cref="Open"/> detects the same compression formats as <see cref="PcapReader.Open"/> from the first bytes of the file,
///  and reads the uncompressed bytes. Compression is a property of the stored file, not of the protocol.
/// </remarks>
public static class DataFile
{
    /// <summary>
    ///  Opens the file at a path for reading.
    /// </summary>
    /// <returns>A stream of the uncompressed bytes of the file. Disposing the stream closes the file.</returns>
    public static Stream Open(string path)
        => Decompressor.Open(path);
}
