# Investors Exchange Depth Of Book 1.08

Generated C# parser for Investors Exchange Depth Of Book v1.08.

Classes parser — immutable classes with Parse methods, built for readability over raw throughput.

## Build

```
dotnet build Iex.IexEquities.Deep.IexTp.v1.08.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Test/Iex.IexEquities.Deep.IexTp.v1.08.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.Deep.IexTp.v1.08.slnx
```

Produces `Iex.IexEquities.Deep.IexTp.v1.08.Classes.1.08.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.Deep.IexTp.v1.08.Classes` is fully self-contained. The Test and example harness projects additionally reference the `Classes/Pcap.CSharp` support project by relative path.

