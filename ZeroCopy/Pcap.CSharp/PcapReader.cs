using System.Runtime.InteropServices;

namespace Pcap.CSharp;

/// <summary>
///  Pcap global file header (24 bytes)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PcapGlobalHeader
{
    public uint MagicNumber;
    public ushort VersionMajor;
    public ushort VersionMinor;
    public int ThisZone;
    public uint SigFigs;
    public uint SnapLen;
    public uint Network;

    public const uint ExpectedMagic = 0xA1B2C3D4;        // microsecond timestamps
    public const uint ExpectedMagicNsec = 0xA1B23C4D;    // nanosecond timestamps (same format)
    public const uint ExpectedMagicSwapped = 0xD4C3B2A1;
    public const uint LinkTypeEthernet = 1;

    public const int Size = 24;

    /// <summary>
    ///  Validates the pcap magic number and link type. Shared by <see cref="PcapReader"/> and
    ///  <see cref="PcapCursor"/> so both apply identical acceptance rules (native/nanosecond magic,
    ///  Ethernet link type only). Throws on byte-swapped captures, unknown magic, or non-Ethernet.
    /// </summary>
    internal readonly void Validate()
    {
        if (MagicNumber != ExpectedMagic && MagicNumber != ExpectedMagicNsec)
        {
            if (MagicNumber == ExpectedMagicSwapped)
                throw new NotSupportedException("Byte-swapped pcap files are not supported");

            throw new InvalidDataException($"Not a pcap file (magic: 0x{MagicNumber:X8})");
        }

        if (Network != LinkTypeEthernet)
            throw new NotSupportedException($"Only Ethernet link type is supported (got {Network})");
    }
}

/// <summary>
///  Pcap per-packet record header (16 bytes)
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 1)]
public struct PcapRecordHeader
{
    public uint TsSec;
    public uint TsUsec;
    public uint InclLen;
    public uint OrigLen;

    public const int Size = 16;
}

/// <summary>
///  Reads pcap files and yields raw packet payloads
/// </summary>
public static class PcapReader
{
    public static IEnumerable<ReadOnlyMemory<byte>> ReadPackets(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new BinaryReader(stream);

        // Read and validate global header
        var globalHeader = Read<PcapGlobalHeader>(reader);
        globalHeader.Validate();

        // Yield each packet
        while (stream.Position < stream.Length)
        {
            var recordHeader = Read<PcapRecordHeader>(reader);
            var data = reader.ReadBytes((int)recordHeader.InclLen);

            if (data.Length < recordHeader.InclLen)
                break; // truncated

            yield return data;
        }
    }

    private static unsafe T Read<T>(BinaryReader reader) where T : unmanaged
    {
        var bytes = reader.ReadBytes(sizeof(T));

        fixed (byte* ptr = bytes)
        {
            return *(T*)ptr;
        }
    }
}
