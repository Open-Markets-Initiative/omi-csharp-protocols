# Databento Normalized Dbn v1

Generated C# parser for Databento Normalized Dbn v1.

Messages are read and written through structs that overlay the packet bytes directly, without copying.

## Usage

Reference project `Databento.MarketData.Normalized.Dbn.v1` to read and write packets. Its namespace is `Databento.MarketData.Normalized.Dbn.v1`; each protocol version has its own namespace, so a program can reference two versions together.

### File

Read the bytes with `File.TryRead`. The bytes are the bytes of one File, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!File.TryRead(bytes, out var file)) return;
    Console.WriteLine(file.Metadata.Magic);
}
```

`File.TryGetFrameLength` gets the length of the File frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `FileStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;
using Databento.MarketData.Normalized.Dbn.v1.Iterator;

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
using Databento.MarketData.Normalized.Dbn.v1;

static int Write(Span<byte> buffer, in File.Value value)
    => File.Write(buffer, in value).Length;
```

### Gateway Stream (TCP)

Read the bytes with `GatewayStream.TryRead`. The bytes are whole Gateway Stream bytes that one side sends on one TCP connection, in order, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!GatewayStream.TryRead(bytes, out var gatewayStream)) return;
    Console.WriteLine(gatewayStream.Metadata.Magic);
}
```

`GatewayStream.TryGetFrameLength` gets the length of the Gateway Stream frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `GatewayStreamStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;
using Databento.MarketData.Normalized.Dbn.v1.Iterator;

static bool Whole(ReadOnlySpan<byte> bytes)
    => GatewayStream.TryGetFrameLength(bytes, out var length) && length <= bytes.Length;

static long Receive(GatewayStreamStream stream, ReadOnlySpan<byte> segment)
{
    long frames = 0;
    foreach (var gatewayStream in stream.Receive(segment)) frames++;
    return frames;
}
```

Write one Gateway Stream with `GatewayStream.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static int Write(Span<byte> buffer, in GatewayStream.Value value)
    => GatewayStream.Write(buffer, in value).Length;
```

Change one field of a captured Bid Ask Pair with `BidAskPair.Layout.Over`. It lays the fixed fields over the bytes without clearing them or writing their fixed values, so the other fields keep the captured values:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static byte[] Change(ReadOnlySpan<byte> captured, BidPx value)
{
    var bytes = captured.ToArray();
    BidAskPair.Layout.Over(bytes).BidPx = value;
    return bytes;
}
```

### Client Stream (TCP)

Read the bytes with `ClientStream.TryRead`. The bytes are whole Client Stream bytes that one side sends on one TCP connection, in order, as a `ReadOnlySpan<byte>`; the Reader borrows them and reads each member when you get it.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static void Read(ReadOnlySpan<byte> bytes)
{
    if (!ClientStream.TryRead(bytes, out var clientStream)) return;
}
```

`ClientStream.TryGetFrameLength` gets the length of the Client Stream frame at the start of the bytes as soon as the bytes hold the fields that state it. To read the segments of a TCP connection as they arrive, pass each one to `ClientStreamStream.Receive` in the `Examples/Iterator` project; it keeps the bytes of a frame that is not whole and gives each whole frame once.

```csharp
using Databento.MarketData.Normalized.Dbn.v1;
using Databento.MarketData.Normalized.Dbn.v1.Iterator;

static bool Whole(ReadOnlySpan<byte> bytes)
    => ClientStream.TryGetFrameLength(bytes, out var length) && length <= bytes.Length;

static long Receive(ClientStreamStream stream, ReadOnlySpan<byte> segment)
{
    long frames = 0;
    foreach (var clientStream in stream.Receive(segment)) frames++;
    return frames;
}
```

Write one Client Stream with `ClientStream.Write`. The value gives its fields; the write fills in its lengths, counts and message types, and returns the bytes that it wrote at the start of the buffer:

```csharp
using Databento.MarketData.Normalized.Dbn.v1;

static int Write(Span<byte> buffer, in ClientStream.Value value)
    => ClientStream.Write(buffer, in value).Length;
```

## Projects

| Project | Reads | Description |
| --- | --- | --- |
| `Databento.MarketData.Normalized.Dbn.v1` |  | Parser library with the message structs that read and write packet bytes in place. |
| `Examples/Iterator` | FILE or TCP pcap | Library to copy from: a capture class for each packet type that reads a pcap file and opens a reading of the messages in each frame. |
| `Examples/Viewer` | FILE or TCP pcap | Console program that prints every message of a pcap file; optional arguments name the packet types to read. |
| `Examples/Manager` | FILE or TCP pcap | Starting point for your own program: one sealed partial class that passes each message, header and packet of a reading to a partial method of the same name, and a program that runs it over a pcap file. |
| `Examples/Counter` | FILE or TCP pcap | Console program that counts the messages, headers and packets of a pcap file and prints each count, the bytes of the capture file and the elapsed time. |

## Verification

| Project | Reads | Description |
| --- | --- | --- |
| `Tests/Test` | FILE or TCP pcap | Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order. |

## Build

Requires the .NET 10 SDK.

```
dotnet build Databento.MarketData.Normalized.Dbn.v1.slnx
```

