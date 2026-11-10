# National Association of Securities Dealers Automated Quotations (Nasdaq) Stock Market Orders Ouch v5.0

Generated C# parser for Nasdaq Stock Market Orders Ouch v5.0.

Messages are read and written through structs that overlay the packet bytes directly, without copying.

## Usage

Reference project `Nasdaq.NsmEquities.Orders.Ouch.v5.0` to read and write packets. Its namespace is `Nasdaq.NsmEquities.Orders.Ouch.v5_0`; each protocol version has its own namespace, so a program can reference two versions together.

### Client Packet (TCP)

Read the bytes with `ClientPacket.TryRead`. The bytes are whole Client Packet bytes that one side sends on one TCP connection, in order, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!ClientPacket.TryRead(bytes, out var clientPacket)) return;
    foreach (var clientSoupBinTcpPacket in clientPacket.ClientSoupBinTcpPacket) Console.WriteLine(clientSoupBinTcpPacket.ClientPacketHeader.PacketLength);
}
```

`ClientPacket.TryGetFrameLength` gets the length of the Client Packet frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `ClientPacketStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;

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
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

static int Write(Span<byte> buffer, in ClientPacket.Value value)
    => ClientPacket.Write(buffer, in value).Length;
```

Change one field of a captured Login Request Packet with `LoginRequestPacket.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

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
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!ServerPacket.TryRead(bytes, out var serverPacket)) return;
    foreach (var serverSoupBinTcpPacket in serverPacket.ServerSoupBinTcpPacket) Console.WriteLine(serverSoupBinTcpPacket.ServerPacketHeader.PacketLength);
}
```

`ServerPacket.TryGetFrameLength` gets the length of the Server Packet frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `ServerPacketStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;
using Nasdaq.NsmEquities.Orders.Ouch.v5_0.Iterator;

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
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

static int Write(Span<byte> buffer, in ServerPacket.Value value)
    => ServerPacket.Write(buffer, in value).Length;
```

Change one field of a captured System Event Message with `SystemEventMessage.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

static byte[] Change(ReadOnlySpan<byte> captured, Timestamp value)
{
    var bytes = captured.ToArray();
    SystemEventMessage.Layout.Over(bytes).Timestamp = value;
    return bytes;
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Nasdaq.NsmEquities.Orders.Ouch.v5.0` |  | Parser library with the message structs that read and write packet bytes in place. |
| `Examples/Iterator` | TCP pcap | Library to copy from: a capture class for each packet type that reads a pcap file and opens a reading of the messages in each frame. |
| `Examples/Viewer` | TCP pcap | Console program that prints every message of a pcap file; optional arguments name the packet types to read. |
| `Examples/Manager` | TCP pcap | Starting point for your own program: one sealed partial class that passes each message, header and packet of a reading to a partial method of the same name, and a program that runs it over a pcap file. |
| `Examples/Counter` | TCP pcap | Console program that counts the messages, headers and packets of a pcap file and prints each count, the bytes of the capture file and the elapsed time. |

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | TCP pcap | Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Nasdaq.NsmEquities.Orders.Ouch.v5.0.slnx
```

