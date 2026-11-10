# Investors Exchange Equities Depth Of Book IexTp v1.08

Generated C# parser for Iex Equities Depth Of Book IexTp v1.08.

Messages are read and written through structs that overlay the packet bytes directly, without copying.

## Usage

Reference project `Iex.IexEquities.Deep.IexTp.v1.08` to read and write packets. Its namespace is `Iex.IexEquities.Deep.IexTp.v1_08`; each protocol version has its own namespace, so a program can reference two versions together.

Read the bytes with `Packet.TryRead`. The bytes are the UDP datagram body, without the Ethernet, IP, and UDP headers, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Iex.IexEquities.Deep.IexTp.v1_08;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!Packet.TryRead(bytes, out var packet)) return;
    Console.WriteLine(packet.IextpHeader.Version);
}
```

Write one Packet with `Packet.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Iex.IexEquities.Deep.IexTp.v1_08;

static int Write(Span<byte> buffer, in Packet.Value value)
    => Packet.Write(buffer, in value).Length;
```

Change one field of a captured Auction Information Message with `AuctionInformationMessage.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Iex.IexEquities.Deep.IexTp.v1_08;

static byte[] Change(ReadOnlySpan<byte> captured, AuctionTypeField value)
{
    var bytes = captured.ToArray();
    AuctionInformationMessage.Layout.Over(bytes).AuctionType = value;
    return bytes;
}
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Iex.IexEquities.Deep.IexTp.v1.08` |  | Parser library with the message structs that read and write packet bytes in place. |
| `Examples/Iterator` | UDP pcap | Library to copy from: a capture class for each packet type that reads a pcap file and opens a reading of the messages in each frame. |
| `Examples/Viewer` | UDP pcap | Console program that prints every message of a pcap file; optional arguments name the packet types to read. |
| `Examples/Manager` | UDP pcap | Starting point for your own program: one sealed partial class that passes each message, header and packet of a reading to a partial method of the same name, and a program that runs it over a pcap file. |
| `Examples/Counter` | UDP pcap | Console program that counts the messages, headers and packets of a pcap file and prints each count, the bytes of the capture file and the elapsed time. |

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | UDP pcap | Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Iex.IexEquities.Deep.IexTp.v1.08.slnx
```

