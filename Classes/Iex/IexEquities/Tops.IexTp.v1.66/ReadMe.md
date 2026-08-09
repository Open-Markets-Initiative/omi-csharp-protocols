# Investors Exchange Top Of Book 1.66

Generated C# parser for Investors Exchange Top Of Book v1.66.

Classes parser — immutable classes with Parse methods, built for readability over raw throughput.

## Build

```
dotnet build Iex.IexEquities.Tops.IexTp.v1.66.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Test/Iex.IexEquities.Tops.IexTp.v1.66.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.Tops.IexTp.v1.66.slnx
```

Produces `Iex.IexEquities.Tops.IexTp.v1.66.Classes.1.66.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.Tops.IexTp.v1.66.Classes` is fully self-contained. The Test and example harness projects additionally reference the `Classes/Pcap.CSharp` support project by relative path.

