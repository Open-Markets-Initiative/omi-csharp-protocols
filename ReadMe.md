# Omi C# Protocols

Omi C# Protocols provide source generated C# parsing of common binary exchange protocols. Each parser flavor is a self-contained top-level directory holding one slice per protocol version.

- [Classes](Classes) — Classes parser — immutable classes with Parse methods, built for readability over raw throughput.
- [ZeroCopy](ZeroCopy) — Zero-copy sequential-layout parser — [StructLayout(LayoutKind.Sequential, Pack = 1)] unsafe structs overlaying packet bytes directly, no parsing loop.

## Usage

Each protocol directory is a self-contained slice with its own solution and NuGet packaging:

```
dotnet build Classes/Iex/IexEquities/Tops.IexTp.v1.56/Iex.IexEquities.Tops.IexTp.v1.56.slnx
```
Each slice ships a pcap-driven Test harness that walks a whole capture (frame → payload → parse → counts). Captures are not included in this repository — point a harness at a local pcap:

```
dotnet run --project <flavor>/<organization>/<division>/<protocol>/Test -- <path-to-pcap>
```
Every flavor directory carries its own `Pcap.CSharp` project providing pcap traversal for its harnesses.

## Development

Updates are greatly appreciated; however, this entire repository is source generated...including the words you are reading right now. If you wish to suggest parser updates, the recommended process is to create an issue with changes and explanation.  Time permitting, we will update the models and regenerate.

| Protocol Count | Generated Lines |
| --- | --- |
| 14 | 120,625 |

## Testing

[![Build](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/actions/workflows/build.yml/badge.svg)](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/actions/workflows/build.yml)

Please report any parsing errors as an [issue](https://github.com/Open-Markets-Initiative/omi-csharp-protocols/issues "Omi C# Protocols Issues").  Include a small note on the protocol and version, and a minimal capture demonstrating the problem. Also consider including a link or pdf specification documenting the correct behavior.

Production packet captures are required for protocol verification.  If your organization has the rights to packet captures, and you wish to make the world a better place, please post captures to this project.

## Open Markets Initiative

[![Omi](https://github.com/Open-Markets-Initiative/Directory/blob/main/About/Images/Logo.png)](https://github.com/Open-Markets-Initiative/Directory)  The Open Markets Initiative (Omi) is a group of technologists dedicated to enhancing the stability of electronic financial markets using modern development methods.

For a list of Omi Hft projects: [Omi Projects](https://github.com/Open-Markets-Initiative/Directory/tree/main/Projects "Open Markets Initiative Projects")

For details of Omi rules and regulations: [Omi Directory](https://github.com/Open-Markets-Initiative/Directory "Open Markets Initiative Directory")
## Protocols

Parsers by Organization: [Iex][Iex.Directory]

Parsers by Exchange: [IexEquities][IexEquities.Exchange]

## Disclaimer

Any similarities between existing people, places and/or protocols is purely incidental.

Enjoy.

[Omi Projects]: https://github.com/Open-Markets-Initiative/Directory/tree/main/Projects "Open Markets Initiative Projects"
[Omi Rules and Regulations]: https://github.com/Open-Markets-Initiative/Directory/tree/main/License "Open Markets Initiative Rules and Regulations"

[Omi.Glossary.Testing]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Protocol Testing Status"
[Omi.Glossary.Testing.Verified]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Testing Status: Protocol has been tested on live data"
[Omi.Glossary.Testing.Incomplete]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Testing Status: Protocol has been tested on live data but contains known issues"
[Omi.Glossary.Testing.Beta]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Testing Status: Protocol has not been tested and structure is speculative"
[Omi.Glossary.Testing.Untested]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Testing Status: Protocol has not been tested on live data"
[Omi.Glossary.Testing.Unavailable]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Glossary/Testing.md "Testing Status: Protocol does not state a testing status"
[Omi.Encoding.Definitions]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Protocols/ReadMe.md "Encoding Directory"

[Omi.Encoding.IexTp]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Protocols/IexTp.md "IexTp Encoding"

[Iex.IexEquities.Tops]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Organizations/Iex/Protocols/IexEquities/Tops.md "Top Of Book"
[Iex.IexEquities.Deep]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Organizations/Iex/Protocols/IexEquities/Deep.md "Depth Of Book"
[Iex.IexEquities.DeepPlus]: https://github.com/Open-Markets-Initiative/Directory/blob/main/Organizations/Iex/Protocols/IexEquities/DeepPlus.md "DeepPlus"

[Iex.Directory]: https://github.com/Open-Markets-Initiative/omi-csharp-protocols/tree/main/Classes/Iex "Investors Exchange"

[IexEquities.Exchange]: https://github.com/Open-Markets-Initiative/omi-csharp-protocols/tree/main/Classes/Iex/IexEquities "IEX Equities"
