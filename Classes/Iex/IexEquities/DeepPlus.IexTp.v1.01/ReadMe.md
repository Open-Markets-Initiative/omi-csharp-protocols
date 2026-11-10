# Investors Exchange Equities DeepPlus IexTp v1.01

Generated C# parser for Iex Equities DeepPlus IexTp v1.01.

Messages are plain mutable classes that you can parse, modify, and encode.

## Usage

Reference project `Iex.IexEquities.DeepPlus.IexTp.v1.01` to parse packets. Its namespace is `Iex.IexEquities.DeepPlus.IexTp.v1_01`; each protocol version has its own namespace, so a program can reference two versions together.

Project `Examples/Iterator` is an example to copy from: it shows how to read a pcap file and get the packets of a feed. The examples below that read a capture file use its classes.

Read a capture file and print every message. `Packets.Read` parses every UDP payload in the file as a packet of this feed, so the capture should hold only this feed:

```csharp
using Iex.IexEquities.DeepPlus.IexTp.v1_01;

foreach (var packet in Packets.Read("capture.pcap"))
{
    if (!packet.IsParsed) continue;
    foreach (var message in packet.Messages)
    {
        Console.WriteLine(message);
    }
}
```

Parse one payload. The payload is the UDP datagram body, without the Ethernet, IP, and UDP headers, as a `ReadOnlySpan<byte>`:

```csharp
using Iex.IexEquities.DeepPlus.IexTp.v1_01;

// payload: the UDP datagram body, without the Ethernet, IP, and UDP headers.
static void Read(ReadOnlySpan<byte> payload)
{
    var packet = Packet.Parse(payload);
    if (!packet.IsParsed) return;
    Console.WriteLine(packet);
}
```

Handle messages by type: derive from `PacketHandler` and override the method for each message:

```csharp
using Iex.IexEquities.DeepPlus.IexTp.v1_01;

// payload: the UDP datagram body, without the Ethernet, IP, and UDP headers.
static void Read(ReadOnlySpan<byte> payload)
{
    var packet = Packet.Parse(payload);
    if (!packet.IsParsed) return;
    new Example().Handle(packet);
}

sealed class Example : PacketHandler
{
    protected override void OnSystemEventMessage(SystemEventMessage message)
        => Console.WriteLine(message.SystemEvent);
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Iex.IexEquities.DeepPlus.IexTp.v1.01` |  | Parser library with the packet and message classes. |
| `Examples/Iterator` | UDP pcap | Example to copy from: reads a pcap file and yields the packets of a feed. |
| `Examples/Viewer` | UDP pcap | Console program that prints every packet and message of a pcap file, for any feed of the protocol. |
| `Examples/PacketManager` | UDP pcap, Packet feed | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |

Project `Examples/Viewer` holds class `PacketViewer`, which handles every message of the feed and prints it. The manager project of a feed instead implements one message, in its `*.Handlers.cs` file, and prints counts.

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | UDP pcap | Console program that parses every packet of a pcap file and verifies that encoding each message gives back the captured bytes. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Iex.IexEquities.DeepPlus.IexTp.v1.01.slnx
```

## Run

Print the packets of a capture file:

```
dotnet run --project Examples/Viewer/Iex.IexEquities.DeepPlus.IexTp.v1.01.Viewer.csproj -- <pcap-file> [max-packets]
```

The optional `[max-packets]` argument stops after that many packets. Captures are not included in this repository.

