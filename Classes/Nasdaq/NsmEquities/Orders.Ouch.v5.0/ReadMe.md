# National Association of Securities Dealers Automated Quotations (Nasdaq) Stock Market Orders Ouch v5.0

Generated C# parser for Nasdaq Stock Market Orders Ouch v5.0.

Messages are plain mutable classes that you can parse, modify, and encode.

## Feeds

| Feed | Transport | Packet | Handler | Stream | Description |
| --- | --- | --- | --- | --- | --- |
| Client Packet | TCP | `ClientPacket` | `ClientPacketHandler` | `ClientPacketStream` | SoupBinTCP Packet sent by the client |
| Server Packet | TCP | `ServerPacket` | `ServerPacketHandler` | `ServerPacketStream` | SoupBinTCP Packet sent by the server |

## Usage

Reference project `Nasdaq.NsmEquities.Orders.Ouch.v5.0` to parse packets. Its namespace is `Nasdaq.NsmEquities.Orders.Ouch.v5_0`; each protocol version has its own namespace, so a program can reference two versions together.

Project `Examples/Iterator` is an example to copy from: it shows how to read a pcap file and get the packets of a feed. The examples below that read a capture file use its classes.

### Client Packet (TCP)

Read a capture file and print every message. `ClientPackets.Read` reassembles the TCP connections in the file and yields the packets of the flows that belong to this feed. The capture can hold both directions and several connections; a flow is matched to this feed from the TCP handshake, or from its first packets when the capture holds no handshake:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

foreach (var packet in ClientPackets.Read("capture.pcap"))
{
    if (!packet.IsParsed) continue;
    foreach (var message in packet.Messages)
    {
        Console.WriteLine(message);
    }
}
```

Parse the packets that the client sends on one TCP connection when you already have their bytes. Pass each part to `Append` of one `ClientPacketStream` as it arrives, in order, as a `ReadOnlySpan<byte>`:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

// stream: reads the packets that the client sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(ClientPacketStream stream, ReadOnlySpan<byte> bytes)
{
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        Console.WriteLine(packet);
    }
}
```

Handle messages by type: derive from `ClientPacketHandler` and override the method for each message:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

// stream: reads the packets that the client sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(ClientPacketStream stream, ReadOnlySpan<byte> bytes)
{
    var example = new Example();
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        example.Handle(packet);
    }
}

sealed class Example : ClientPacketHandler
{
    protected override void OnEnterOrderMessage(EnterOrderMessage message)
        => Console.WriteLine(message.UserRefNum);
}
```

### Server Packet (TCP)

Read a capture file and print every message. `ServerPackets.Read` reassembles the TCP connections in the file and yields the packets of the flows that belong to this feed. The capture can hold both directions and several connections; a flow is matched to this feed from the TCP handshake, or from its first packets when the capture holds no handshake:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

foreach (var packet in ServerPackets.Read("capture.pcap"))
{
    if (!packet.IsParsed) continue;
    foreach (var message in packet.Messages)
    {
        Console.WriteLine(message);
    }
}
```

Parse the packets that the server sends on one TCP connection when you already have their bytes. Pass each part to `Append` of one `ServerPacketStream` as it arrives, in order, as a `ReadOnlySpan<byte>`:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

// stream: reads the packets that the server sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(ServerPacketStream stream, ReadOnlySpan<byte> bytes)
{
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        Console.WriteLine(packet);
    }
}
```

Handle messages by type: derive from `ServerPacketHandler` and override the method for each message:

```csharp
using Nasdaq.NsmEquities.Orders.Ouch.v5_0;

// stream: reads the packets that the server sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(ServerPacketStream stream, ReadOnlySpan<byte> bytes)
{
    var example = new Example();
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        example.Handle(packet);
    }
}

sealed class Example : ServerPacketHandler
{
    protected override void OnSystemEventMessage(SystemEventMessage message)
        => Console.WriteLine(message.Timestamp);
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Nasdaq.NsmEquities.Orders.Ouch.v5.0` |  | Parser library with the packet and message classes. |
| `Examples/Iterator` | TCP pcap | Example to copy from: reads a pcap file and yields the packets of a feed. |
| `Examples/Viewer` | TCP pcap | Console program that prints every packet and message of a pcap file, for any feed of the protocol. |
| `Examples/ClientManager` | TCP pcap, Client Packet feed | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |
| `Examples/ServerManager` | TCP pcap, Server Packet feed | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |

Project `Examples/Viewer` holds one class for each feed, `ClientViewer`, `ServerViewer`, each of which handles every message of its feed and prints it. The manager project of a feed instead implements one message, in its `*.Handlers.cs` file, and prints counts.

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | TCP pcap | Console program that parses every packet of a pcap file and verifies that encoding each message gives back the captured bytes. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Nasdaq.NsmEquities.Orders.Ouch.v5.0.slnx
```

## Run

Print the packets of a capture file:

```
dotnet run --project Examples/Viewer/Nasdaq.NsmEquities.Orders.Ouch.v5.0.Viewer.csproj -- <pcap-file> [max-packets] [--feed Client | Server]
```

The viewer reads every feed of the protocol in the capture; the optional `[--feed Client | Server]` argument reads only the named feed.

The optional `[max-packets]` argument stops after that many packets. Captures are not included in this repository.

