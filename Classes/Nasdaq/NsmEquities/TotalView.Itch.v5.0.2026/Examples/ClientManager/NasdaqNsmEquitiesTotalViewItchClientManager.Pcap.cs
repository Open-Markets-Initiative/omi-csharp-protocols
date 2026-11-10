namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientManager;

public sealed partial class NasdaqNsmEquitiesTotalViewItchClientManager
{
    private readonly TcpFlowReader flows = new([new TcpFlowReader.Feed("Client", TcpSide.Client, () => new ClientPacketStream().Append)]);

    /// <summary>
    ///  Gets the number of TCP flows that were not read because their first packets were not valid.
    /// </summary>
    public int UnreadFlows => this.flows.UnreadFlows;

    /// <summary>
    ///  Gets the number of TCP flows that stopped before their end because the capture misses some of their bytes or a frame has a length that is not valid.
    /// </summary>
    public int StoppedFlows => this.flows.StoppedFlows;

    /// <summary>
    ///  Adds the TCP segment of a captured frame to its flow and passes each completed packet to Receive.
    /// </summary>
    /// <param name="frame">The captured frame, from its link-layer header. The method keeps no reference to it.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <remarks>
    ///  The bytes of each TCP flow are read in sequence order. The flows that the client sends, as the TCP handshake shows, are read. Without a handshake in the capture, a flow is read only if its first packets are valid.
    /// </remarks>
    public void Process(ReadOnlySpan<byte> frame, LinkType linkType)
    {
        if (flows.TryAdd(frame, linkType, out var flow, out var data) && flows.Read(flow, data, out var packets) is not null)
        {
            foreach (ClientPacket packet in packets)
            {
                Receive(packet);
            }
        }
    }
}
