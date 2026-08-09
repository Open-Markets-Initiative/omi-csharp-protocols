# Investors Exchange DeepPlus 1.04

Generated C# parser for Investors Exchange DeepPlus v1.04.

Classes parser — immutable classes with Parse methods, built for readability over raw throughput.

## Build

```
dotnet build Iex.IexEquities.DeepPlus.IexTp.v1.04.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Test/Iex.IexEquities.DeepPlus.IexTp.v1.04.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.DeepPlus.IexTp.v1.04.slnx
```

Produces `Iex.IexEquities.DeepPlus.IexTp.v1.04.Classes.1.04.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.DeepPlus.IexTp.v1.04.Classes` is fully self-contained. The Test and example harness projects additionally reference the `Classes/Pcap.CSharp` support project by relative path.

