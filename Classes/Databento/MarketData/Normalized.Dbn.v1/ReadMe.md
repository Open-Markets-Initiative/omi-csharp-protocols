# Databento Normalized Dbn v1

Generated C# parser for Databento Normalized Dbn v1.

Messages are plain mutable classes that you can parse, modify, and encode.

## Feeds

| Feed | Transport | Packet | Handler | Stream | Description |
| --- | --- | --- | --- | --- | --- |
| Gateway Stream | TCP | `GatewayStream` | `GatewayStreamHandler` | `GatewayStreamStream` | The gateway's control messages, then the DBN stream: its metadata, then records |
| Client Stream | TCP | `ClientStream` |  | `ClientStreamStream` | The client's control messages: authentication, subscriptions and the session start |

## Usage

Reference project `Databento.MarketData.Normalized.Dbn.v1` to parse packets. Its namespace is `Databento.MarketData.Normalized.Dbn.v1`; each protocol version has its own namespace, so a program can reference two versions together.

Project `Examples/Iterator` is an example to copy from: it shows how to read a pcap file and get the packets of a feed. The examples below that read a capture file use its classes.

### Gateway Stream (TCP)

Read a capture file and print every message. `GatewayStreams.Read` reassembles the TCP connections in the file and yields the packets of the flows that belong to this feed. The capture can hold both directions and several connections; a flow is matched to this feed from the TCP handshake, or from its first packets when the capture holds no handshake:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

foreach (var packet in GatewayStreams.Read("capture.pcap"))
{
    if (!packet.IsParsed) continue;
    foreach (var message in packet.Messages)
    {
        Console.WriteLine(message);
    }
}
```

Parse the packets that the server sends on one TCP connection when you already have their bytes. Pass each part to `Append` of one `GatewayStreamStream` as it arrives, in order, as a `ReadOnlySpan<byte>`:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

// stream: reads the packets that the server sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(GatewayStreamStream stream, ReadOnlySpan<byte> bytes)
{
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        Console.WriteLine(packet);
    }
}
```

Handle messages by type: derive from `GatewayStreamHandler` and override the method for each message:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

// stream: reads the packets that the server sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(GatewayStreamStream stream, ReadOnlySpan<byte> bytes)
{
    var example = new Example();
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        example.Handle(packet);
    }
}

sealed class Example : GatewayStreamHandler
{
    protected override void OnTradeMsg(TradeMsg message)
        => Console.WriteLine(message.Price);
}
```

### Client Stream (TCP)

Read a capture file and print every message. `ClientStreams.Read` reassembles the TCP connections in the file and yields the packets of the flows that belong to this feed. The capture can hold both directions and several connections; a flow is matched to this feed from the TCP handshake, or from its first packets when the capture holds no handshake:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

foreach (var packet in ClientStreams.Read("capture.pcap"))
{
    if (!packet.IsParsed) continue;
    foreach (var message in packet.Messages)
    {
        Console.WriteLine(message);
    }
}
```

Parse the packets that the client sends on one TCP connection when you already have their bytes. Pass each part to `Append` of one `ClientStreamStream` as it arrives, in order, as a `ReadOnlySpan<byte>`:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

// stream: reads the packets that the client sends on one TCP connection; bytes: the next of their bytes, in order.
static void Read(ClientStreamStream stream, ReadOnlySpan<byte> bytes)
{
    foreach (var packet in stream.Append(bytes))
    {
        if (!packet.IsParsed) continue;
        Console.WriteLine(packet);
    }
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Databento.MarketData.Normalized.Dbn.v1` |  | Parser library with the packet and message classes. |
| `Examples/Iterator` | TCP pcap | Example to copy from: reads a pcap file and yields the packets of a feed. |
| `Examples/Viewer` | TCP pcap | Console program that prints every packet and message of a pcap file, for any feed of the protocol. |
| `Examples/GatewayManager` | TCP pcap, Gateway Stream feed | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |
| `Examples/ClientManager` | TCP pcap, Client Stream feed | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |

Project `Examples/Viewer` holds one class for each feed, `GatewayViewer`, `ClientViewer`, each of which handles every message of its feed and prints it. The manager project of a feed instead implements one message, in its `*.Handlers.cs` file, and prints counts.

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | TCP pcap | Console program that parses every packet of a pcap file and verifies that encoding each message gives back the captured bytes. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Databento.MarketData.Normalized.Dbn.v1.slnx
```

## Run

Print the packets of a capture file:

```
dotnet run --project Examples/Viewer/Databento.MarketData.Normalized.Dbn.v1.Viewer.csproj -- <pcap-file> [max-packets] [--feed Gateway | Client]
```

The viewer reads every feed of the protocol in the capture; the optional `[--feed Gateway | Client]` argument reads only the named feed.

The optional `[max-packets]` argument stops after that many packets. Captures are not included in this repository.

