namespace Iex.IexEquities.Tops.IexTp.v1_66.PacketManager;

public sealed partial class IexIexEquitiesTopsIexTpPacketManager
{
    /// <summary>
    ///  Parses the packet in a captured frame and passes it to Receive.
    /// </summary>
    /// <param name="frame">The captured frame, from its link-layer header. The method keeps no reference to it.</param>
    /// <param name="linkType">The link type of the capture.</param>
    /// <remarks>
    ///  A frame with no UDP payload is ignored.
    /// </remarks>
    public void Process(ReadOnlySpan<byte> frame, LinkType linkType)
    {
        if (!NetworkHeaders.TryGetUdpPayload(frame, linkType, out var payload)) return;
        var packet = Packet.Parse(payload);
        Receive(packet);
    }
}
