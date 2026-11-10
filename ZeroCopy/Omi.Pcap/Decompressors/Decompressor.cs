namespace Omi.Pcap.Decompressors;

using System.IO.Compression;
using K4os.Compression.LZ4.Streams;
using SharpCompress.Compressors.BZip2;
using SharpCompress.Compressors.Xz;
using ZstdSharp;

/// <summary>
///  Identifies the compression format of a capture from its first bytes, and returns a stream of its uncompressed content.
/// </summary>
internal static class Decompressor
{
    public static Stream Open(string path)
        => Wrap(File.OpenRead(path));

    public static Stream Wrap(Stream source)
    {
        try
        {
            var prefix = new byte[6];
            var total = 0;
            while (total < prefix.Length)
            {
                var read = source.Read(prefix, total, prefix.Length - total);
                if (read == 0) { break; }
                total += read;
            }

            var replay = new PrependStream(prefix, total, source);

            if (total >= 2 && prefix[0] == 0x1f && prefix[1] == 0x8b)
            {
                return new DecoderStream(replay, input => new GZipStream(input, CompressionMode.Decompress));
            }

            if (total >= 2 && prefix[0] == 0x42 && prefix[1] == 0x5a)
            {
                return new DecoderStream(replay, input => BZip2Stream.Create(input, SharpCompress.Compressors.CompressionMode.Decompress, decompressConcatenated: true));
            }

            if (total >= 6 && prefix[0] == 0xfd && prefix[1] == 0x37 && prefix[2] == 0x7a && prefix[3] == 0x58 && prefix[4] == 0x5a && prefix[5] == 0x00)
            {
                return new DecoderStream(replay, input => new XZStream(input));
            }

            if (total >= 4 && prefix[0] == 0x04 && prefix[1] == 0x22 && prefix[2] == 0x4d && prefix[3] == 0x18)
            {
                return new DecoderStream(replay, input => LZ4Stream.Decode(input));
            }

            if (total >= 4 && prefix[0] == 0x28 && prefix[1] == 0xb5 && prefix[2] == 0x2f && prefix[3] == 0xfd)
            {
                return new DecoderStream(replay, input => new DecompressionStream(input, leaveOpen: false));
            }

            return replay;
        }
        catch
        {
            source.Dispose();
            throw;
        }
    }
}
