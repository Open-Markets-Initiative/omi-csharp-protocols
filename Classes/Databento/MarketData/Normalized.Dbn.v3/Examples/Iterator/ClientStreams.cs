using Omi.Pcap;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Reads a pcap file and yields each <see cref="ClientStream"/>.
/// </summary>
public static class ClientStreams
{
    /// <summary>
    ///  Reads a pcap file and yields the packets of each TCP flow that the client sends.
    /// </summary>
    /// <param name="pcapPath">The path of the pcap file to read.</param>
    /// <returns>The packets, in the order captured. Check <see cref="ClientStream.IsParsed"/> on each packet.</returns>
    /// <remarks>
    ///  The bytes of each TCP flow are read in sequence order. The flows that the client sends, as the TCP handshake shows, are read. Without a handshake in the capture, a flow is read only if its first packets are valid. The packets of all flows are yielded in the order that they complete.
    /// </remarks>
    public static IEnumerable<ClientStream> Read(string pcapPath)
    {
        var flows = new TcpFlowReader([new TcpFlowReader.Feed("Client", TcpSide.Client, () => new ClientStreamStream().Append)]);
        using var reader = PcapReader.Open(pcapPath);
        while (reader.Advance())
        {
            var frame = reader.Data;
            if (flows.TryAdd(frame, reader.LinkType, out var flow, out var data) && flows.Read(flow, data, out var packets) is not null)
            {
                foreach (ClientStream packet in packets)
                {
                    yield return packet;
                }
            }
        }
    }
}
