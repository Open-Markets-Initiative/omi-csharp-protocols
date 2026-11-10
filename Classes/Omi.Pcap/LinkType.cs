namespace Omi.Pcap;

/// <summary>
///  Identifies the link-layer header at the start of each captured packet, using the LINKTYPE values that pcap and pcap-ng file headers store.
/// </summary>
public enum LinkType : ushort
{
    /// <summary>
    ///  BSD loopback: a 4-byte address family in the byte order of the capture host.
    /// </summary>
    Null = 0,

    /// <summary>
    ///  Ethernet II, with zero or more VLAN tags.
    /// </summary>
    Ethernet = 1,

    /// <summary>
    ///  Raw IP: the packet starts at the IPv4 or IPv6 header.
    /// </summary>
    Raw = 101,

    /// <summary>
    ///  Linux cooked capture, version 1 (SLL): a 16-byte header.
    /// </summary>
    LinuxCooked = 113,

    /// <summary>
    ///  Raw IPv4: the packet starts at the IPv4 header.
    /// </summary>
    IPv4 = 228,

    /// <summary>
    ///  Raw IPv6: the packet starts at the IPv6 header.
    /// </summary>
    IPv6 = 229,

    /// <summary>
    ///  Linux cooked capture, version 2 (SLL2): a 20-byte header.
    /// </summary>
    LinuxCooked2 = 276,
}
