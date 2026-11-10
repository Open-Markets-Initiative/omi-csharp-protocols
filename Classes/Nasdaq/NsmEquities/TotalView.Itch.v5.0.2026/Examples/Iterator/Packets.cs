using Omi.Pcap;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Reads a pcap file and yields each <see cref="Packet"/>.
/// </summary>
public static class Packets
{
    /// <summary>
    ///  Reads a pcap file and yields the packet in each UDP payload.
    /// </summary>
    /// <param name="pcapPath">The path of the pcap file to read.</param>
    /// <returns>The packets, in the order captured. Check <see cref="Packet.IsParsed"/> on each packet.</returns>
    /// <remarks>
    ///  A frame with no UDP payload is ignored.
    /// </remarks>
    public static IEnumerable<Packet> Read(string pcapPath)
    {
        using var reader = PcapReader.Open(pcapPath);
        while (reader.Advance())
        {
            var frame = reader.Data;
            if (!NetworkHeaders.TryGetUdpPayload(frame, reader.LinkType, out var payload)) continue;
            var packet = Packet.Parse(payload);
            yield return packet;
        }
    }
}
