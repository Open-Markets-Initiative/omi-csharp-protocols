# ZeroCopy

Messages are read and written through structs that overlay the packet bytes directly, without copying.

Organizations: [Databento][Databento.Directory], [Iex][Iex.Directory], [Nasdaq][Nasdaq.Directory]

## Projects

Each organization directory lists its protocols. Each protocol directory holds a solution, a ReadMe with usage, and these projects:

| Project | Description |
| --- | --- |
| `<Protocol>` | Parser library with the message structs that read and write packet bytes in place. |
| `Examples/Iterator` | Library to copy from: a capture class for each packet type that reads a pcap file and opens a reading of the messages in each frame. |
| `Examples/Viewer` | Console program that prints every message of a pcap file; optional arguments name the packet types to read. |
| `Examples/Manager` | Starting point for your own program: one sealed partial class that passes each message, header and packet of a reading to a partial method of the same name, and a program that runs it over a pcap file. |
| `Examples/Counter` | Console program that counts the messages, headers and packets of a pcap file and prints each count, the bytes of the capture file and the elapsed time. |
| `Tests/Test` | Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order. |

## Omi.Pcap

[Omi.Pcap](Omi.Pcap) opens the capture files for the example and test programs. It reads pcap and pcapng files, plain or compressed with gzip, bzip2, XZ, LZ4, or Zstandard, takes each frame apart to the UDP or TCP payload, and reassembles TCP streams, so a program sees only protocol bytes. The parser libraries do not use it. `Classes` and `ZeroCopy` each carry a copy, so either directory builds on its own.

[Databento.Directory]: Databento "Databento"
[Iex.Directory]: Iex "Investors Exchange"
[Nasdaq.Directory]: Nasdaq "National Association of Securities Dealers Automated Quotations (Nasdaq)"
