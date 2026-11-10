# National Association of Securities Dealers Automated Quotations (Nasdaq) Stock Market TotalView Itch v5.0.2026

Generated C# parser for Nasdaq Stock Market TotalView Itch v5.0.2026.

Messages are read and written through structs that overlay the packet bytes directly, without copying.

## Usage

Reference project `Nasdaq.NsmEquities.TotalView.Itch.v5.0.2026` to read and write packets. Its namespace is `Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026`; each protocol version has its own namespace, so a program can reference two versions together.

### Client Packet (TCP)

Read the bytes with `ClientPacket.TryRead`. The bytes are whole Client Packet bytes that one side sends on one TCP connection, in order, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!ClientPacket.TryRead(bytes, out var clientPacket)) return;
    foreach (var clientSoupBinTcpPacket in clientPacket.ClientSoupBinTcpPacket) Console.WriteLine(clientSoupBinTcpPacket.ClientPacketHeader.PacketLength);
}
```

`ClientPacket.TryGetFrameLength` gets the length of the Client Packet frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `ClientPacketStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Iterator;

static bool Whole(ReadOnlySpan<byte> bytes)
    => ClientPacket.TryGetFrameLength(bytes, out var length) && length <= bytes.Length;

static long Receive(ClientPacketStream stream, ReadOnlySpan<byte> segment)
{
    long frames = 0;
    foreach (var clientPacket in stream.Receive(segment)) frames++;
    return frames;
}
```

Write one Client Packet with `ClientPacket.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static int Write(Span<byte> buffer, in ClientPacket.Value value)
    => ClientPacket.Write(buffer, in value).Length;
```

Change one field of a captured Login Request Packet with `LoginRequestPacket.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static byte[] Change(ReadOnlySpan<byte> captured, Username value)
{
    var bytes = captured.ToArray();
    LoginRequestPacket.Layout.Over(bytes).Username = value;
    return bytes;
}
```

### Server Packet (TCP)

Read the bytes with `ServerPacket.TryRead`. The bytes are whole Server Packet bytes that one side sends on one TCP connection, in order, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!ServerPacket.TryRead(bytes, out var serverPacket)) return;
    foreach (var serverSoupBinTcpPacket in serverPacket.ServerSoupBinTcpPacket) Console.WriteLine(serverSoupBinTcpPacket.ServerPacketHeader.PacketLength);
}
```

`ServerPacket.TryGetFrameLength` gets the length of the Server Packet frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `ServerPacketStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Iterator;

static bool Whole(ReadOnlySpan<byte> bytes)
    => ServerPacket.TryGetFrameLength(bytes, out var length) && length <= bytes.Length;

static long Receive(ServerPacketStream stream, ReadOnlySpan<byte> segment)
{
    long frames = 0;
    foreach (var serverPacket in stream.Receive(segment)) frames++;
    return frames;
}
```

Write one Server Packet with `ServerPacket.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static int Write(Span<byte> buffer, in ServerPacket.Value value)
    => ServerPacket.Write(buffer, in value).Length;
```

Change one field of a captured Login Rejected Packet with `LoginRejectedPacket.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static byte[] Change(ReadOnlySpan<byte> captured, RejectReasonCodeField value)
{
    var bytes = captured.ToArray();
    LoginRejectedPacket.Layout.Over(bytes).RejectReasonCode = value;
    return bytes;
}
```

### Packet (UDP)

Read the bytes with `Packet.TryRead`. The bytes are the UDP datagram body, without the Ethernet, IP, and UDP headers, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!Packet.TryRead(bytes, out var packet)) return;
    Console.WriteLine(packet.PacketHeader.Session);
}
```

Write one Packet with `Packet.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static int Write(Span<byte> buffer, in Packet.Value value)
    => Packet.Write(buffer, in value).Length;
```

Change one field of a captured Packet Header with `PacketHeader.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static byte[] Change(ReadOnlySpan<byte> captured, Session value)
{
    var bytes = captured.ToArray();
    PacketHeader.Layout.Over(bytes).Session = value;
    return bytes;
}
```

### File

Read the bytes with `File.TryRead`. The bytes are the bytes of one File, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!File.TryRead(bytes, out var file)) return;
    foreach (var fileMessage in file.FileMessage) Console.WriteLine(fileMessage.MessageHeader.MessageLength);
}
```

`File.TryGetFrameLength` gets the length of the File frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `FileStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Iterator;

static bool Whole(ReadOnlySpan<byte> bytes)
    => File.TryGetFrameLength(bytes, out var length) && length <= bytes.Length;

static long Receive(FileStream stream, ReadOnlySpan<byte> segment)
{
    long frames = 0;
    foreach (var file in stream.Receive(segment)) frames++;
    return frames;
}
```

Write one File with `File.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static int Write(Span<byte> buffer, in File.Value value)
    => File.Write(buffer, in value).Length;
```

Change one field of a captured Direct Listing With Capital Raise Price Discovery Message with `DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

static byte[] Change(ReadOnlySpan<byte> captured, StockLocate value)
{
    var bytes = captured.ToArray();
    DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Over(bytes).StockLocate = value;
    return bytes;
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Nasdaq.NsmEquities.TotalView.Itch.v5.0.2026` |  | Parser library with the message structs that read and write packet bytes in place. |
| `Examples/Iterator` | TCP or UDP or FILE pcap | Library to copy from: a capture class for each packet type that reads a pcap file and opens a reading of the messages in each frame. |
| `Examples/Viewer` | TCP or UDP or FILE pcap | Console program that prints every message of a pcap file; optional arguments name the packet types to read. |
| `Examples/Manager` | TCP or UDP or FILE pcap | Starting point for your own program: one sealed partial class that passes each message, header and packet of a reading to a partial method of the same name, and a program that runs it over a pcap file. |
| `Examples/Counter` | TCP or UDP or FILE pcap | Console program that counts the messages, headers and packets of a pcap file and prints each count, the bytes of the capture file and the elapsed time. |

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | TCP or UDP or FILE pcap | Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Nasdaq.NsmEquities.TotalView.Itch.v5.0.2026.slnx
```

