# Omi C# Protocols

Omi C# Protocols provide source generated C# parsing of common binary exchange protocols. Each top-level directory holds one kind of parser, with a directory for each organization, division, and protocol version:

- [Classes](Classes) — Messages are plain mutable classes that you can parse, modify, and encode.
- [ZeroCopy](ZeroCopy) — Messages are read and written through structs that overlay the packet bytes directly, without copying.

## Usage

Each protocol directory has its own solution:

```
dotnet build Classes/Databento/MarketData/Normalized.Dbn.v1/Databento.MarketData.Normalized.Dbn.v1.slnx
```

Each protocol has a Test program that parses every packet of a pcap file. Captures are not included in this repository. Run the program on a local pcap file:

Classes: Console program that parses every packet of a pcap file and verifies that encoding each message gives back the captured bytes.

```
dotnet run --project Classes/<organization>/<division>/<protocol>/Tests/Test -- <pcap-file>
```

ZeroCopy: Console program that parses every packet of a pcap file, prints the number of messages, headers and packets parsed and of invalid and incomplete datagrams or flows, and fails when any datagram or flow is invalid or incomplete. It also checks that each fixed-length message writes exactly its own bytes and that the entries of each repeated group are counted and numbered in order.

```
dotnet run --project ZeroCopy/<organization>/<division>/<protocol>/Tests/Test -- <pcap-file>
```

Every project that reads a pcap file references the one `Omi.Pcap` project of its top-level directory. The ReadMe of each protocol lists its projects.

## Development

Updates are greatly appreciated; however, this entire repository is source generated...including the words you are reading right now. If you wish to suggest parser updates, the recommended process is to create an issue with changes and explanation.  Time permitting, we will update the models and regenerate.

| Parser | Protocol Count | Generated Lines |
| --- | --- | --- |
| Classes | 18 | 596,125 |
| ZeroCopy | 18 | 354,286 |

## Testing

[![Build](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/actions/workflows/build.yml/badge.svg)](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/actions/workflows/build.yml)

Please report any parsing errors as an [issue](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/issues "Omi C# Protocols Issues").  Include a small note on the protocol and version, and a minimal capture demonstrating the problem. Also consider including a link or pdf specification documenting the correct behavior.

Production packet captures are required for protocol verification.  If your organization has the rights to packet captures, and you wish to make the world a better place, please post captures to this project.

## Open Markets Initiative

[![Omi](https://github.com/Open-Markets-Initiative/Open-Markets-Initiative/raw/main/About/Images/Logo.png)](https://github.com/Open-Markets-Initiative/Directory)  The Open Markets Initiative (Omi) is a group of technologists dedicated to enhancing the stability of electronic financial markets using modern development methods.

Other generated code can be found at [Omi Repositories](https://github.com/Open-Markets-Initiative/Directory/tree/main/Repositories "Open Markets Initiative Repositories"); for Omi rules and regulations, see [Omi Directory](https://github.com/Open-Markets-Initiative/Directory "Open Markets Initiative Directory").

## Protocols

### Classes

Organizations: [Databento][Classes.Databento.Directory], [Iex][Classes.Iex.Directory], [Nasdaq][Classes.Nasdaq.Directory]

Exchange divisions: [IexEquities][Classes.IexEquities.Exchange], [IseOptions][Classes.IseOptions.Exchange], [NsmEquities][Classes.NsmEquities.Exchange], [NtxEquities][Classes.NtxEquities.Exchange], [PsxEquities][Classes.PsxEquities.Exchange]

Redistributor divisions: [Databento][Classes.MarketData.Redistributor]

### ZeroCopy

Organizations: [Databento][ZeroCopy.Databento.Directory], [Iex][ZeroCopy.Iex.Directory], [Nasdaq][ZeroCopy.Nasdaq.Directory]

Exchange divisions: [IexEquities][ZeroCopy.IexEquities.Exchange], [IseOptions][ZeroCopy.IseOptions.Exchange], [NsmEquities][ZeroCopy.NsmEquities.Exchange], [NtxEquities][ZeroCopy.NtxEquities.Exchange], [PsxEquities][ZeroCopy.PsxEquities.Exchange]

Redistributor divisions: [Databento][ZeroCopy.MarketData.Redistributor]

## Disclaimer

Any similarities between existing people, places and/or protocols is purely incidental.

Enjoy.

[Classes.Databento.Directory]: Classes/Databento "Databento"
[Classes.Iex.Directory]: Classes/Iex "Investors Exchange"
[Classes.Nasdaq.Directory]: Classes/Nasdaq "National Association of Securities Dealers Automated Quotations (Nasdaq)"

[Classes.IexEquities.Exchange]: Classes/Iex/IexEquities "IEX Equities"
[Classes.IseOptions.Exchange]: Classes/Nasdaq/IseOptions "Nasdaq ISE"
[Classes.MarketData.Redistributor]: Classes/Databento/MarketData "Databento Market Data"
[Classes.NsmEquities.Exchange]: Classes/Nasdaq/NsmEquities "Nasdaq Stock Market"
[Classes.NtxEquities.Exchange]: Classes/Nasdaq/NtxEquities "Nasdaq Texas"
[Classes.PsxEquities.Exchange]: Classes/Nasdaq/PsxEquities "Nasdaq PSX"

[ZeroCopy.Databento.Directory]: ZeroCopy/Databento "Databento"
[ZeroCopy.Iex.Directory]: ZeroCopy/Iex "Investors Exchange"
[ZeroCopy.Nasdaq.Directory]: ZeroCopy/Nasdaq "National Association of Securities Dealers Automated Quotations (Nasdaq)"

[ZeroCopy.IexEquities.Exchange]: ZeroCopy/Iex/IexEquities "IEX Equities"
[ZeroCopy.IseOptions.Exchange]: ZeroCopy/Nasdaq/IseOptions "Nasdaq ISE"
[ZeroCopy.MarketData.Redistributor]: ZeroCopy/Databento/MarketData "Databento Market Data"
[ZeroCopy.NsmEquities.Exchange]: ZeroCopy/Nasdaq/NsmEquities "Nasdaq Stock Market"
[ZeroCopy.NtxEquities.Exchange]: ZeroCopy/Nasdaq/NtxEquities "Nasdaq Texas"
[ZeroCopy.PsxEquities.Exchange]: ZeroCopy/Nasdaq/PsxEquities "Nasdaq PSX"
