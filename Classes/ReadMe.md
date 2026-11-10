# Classes

Messages are plain mutable classes that you can parse, modify, and encode.

Organizations: [Databento][Databento.Directory], [Iex][Iex.Directory], [Nasdaq][Nasdaq.Directory]

## Projects

Each organization directory lists its protocols. Each protocol directory holds a solution, a ReadMe with usage, and these projects:

| Project | Description |
| --- | --- |
| `<Protocol>` | Parser library with the packet and message classes. |
| `Examples/Iterator` | Example to copy from: reads a pcap file and yields the packets of a feed. |
| `Examples/Viewer` | Console program that prints every packet and message of a pcap file, for any feed of the protocol. |
| `Examples/<Feed>Manager` | Starting point for your own program: a partial class with one partial method for each message, a `*.Handlers.cs` file that implements one of them, and a program that runs it over a pcap file and prints its counts. |
| `Tests/Test` | Console program that parses every packet of a pcap file and verifies that encoding each message gives back the captured bytes. |

## Omi.Pcap

[Omi.Pcap](Omi.Pcap) opens the capture files for the example and test programs. It reads pcap and pcapng files, plain or compressed with gzip, bzip2, XZ, LZ4, or Zstandard, takes each frame apart to the UDP or TCP payload, and reassembles TCP streams, so a program sees only protocol bytes. The parser libraries do not use it. `Classes` and `ZeroCopy` each carry a copy, so either directory builds on its own.

[Databento.Directory]: Databento "Databento"
[Iex.Directory]: Iex "Investors Exchange"
[Nasdaq.Directory]: Nasdaq "National Association of Securities Dealers Automated Quotations (Nasdaq)"
