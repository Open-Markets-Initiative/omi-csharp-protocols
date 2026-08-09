using Pcap.CSharp;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Yields parsed Iex.IexEquities.Deep.IexTp packets from a pcap file.
/// </summary>
public static class Packets
{
    /// <summary>
    ///  Reads a pcap file and yields each successfully parsed packet.
    /// </summary>
    /// <param name="pcapPath">Path to the pcap file to read.</param>
    /// <returns>Each parsed Packet, in wire order. Check IsValid for parse success.</returns>
    public static IEnumerable<Packet> Read(string pcapPath)
    {
        foreach (var frame in PcapReader.ReadPackets(pcapPath))
        {
            if (!NetworkHeaders.TryGetUdpPayload(frame.Span, out var payload)) continue;
            var packet = Packet.Parse(payload);
            yield return packet;
        }
    }
}
