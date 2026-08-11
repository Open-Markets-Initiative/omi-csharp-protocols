using System.Runtime.InteropServices;

namespace Pcap.CSharp;

/// <summary>
///  Classic pcap global header.
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

    public const uint ExpectedMagic = 0xA1B2C3D4; // Microsecond timestamps.
    public const uint ExpectedMagicNsec = 0xA1B23C4D; // Nanosecond timestamps.
    public const uint ExpectedMagicSwapped = 0xD4C3B2A1;
    public const uint LinkTypeEthernet = 1;

    public const int Size = 24;

    /// <summary>
    ///  Validates the supported native-endian pcap magic numbers and Ethernet link type.
    ///  Throws for byte-swapped captures, unknown magic numbers, and other link types.
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
///  Classic pcap packet record header.
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
///  Reads classic pcap files and yields packet bytes.
/// </summary>
public static class PcapReader
{
    /// <summary>Reads packet bytes from a classic pcap file.</summary>
    /// <param name="path">Path to the pcap file.</param>
    /// <returns>Packet byte buffers in file order.</returns>
    public static IEnumerable<ReadOnlyMemory<byte>> ReadPackets(string path)
    {
        using var stream = File.OpenRead(path);
        using var reader = new BinaryReader(stream);

        var globalHeader = Read<PcapGlobalHeader>(reader);
        globalHeader.Validate();

        while (stream.Position < stream.Length)
        {
            var recordHeader = Read<PcapRecordHeader>(reader);
            var data = reader.ReadBytes((int)recordHeader.InclLen);

            if (data.Length < recordHeader.InclLen)
                break; // The final packet is truncated.

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
