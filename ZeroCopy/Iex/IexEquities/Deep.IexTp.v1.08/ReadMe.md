# Investors Exchange Depth Of Book 1.08

Generated C# parser for Investors Exchange Depth Of Book v1.08.

Zero-copy sequential-layout parser — [StructLayout(LayoutKind.Sequential, Pack = 1)] unsafe structs overlaying packet bytes directly, no parsing loop.

## Build

```
dotnet build Iex.IexEquities.Deep.IexTp.v1.08.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Iex.IexEquities.Deep.IexTp.v1.08/Test/Iex.IexEquities.Deep.IexTp.v1.08.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.Deep.IexTp.v1.08.slnx
```

Produces `Iex.IexEquities.Deep.IexTp.v1.08.ZeroCopy.1.08.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.Deep.IexTp.v1.08.ZeroCopy` is fully self-contained. The Test and example harness projects additionally reference the `ZeroCopy/Pcap.CSharp` support project by relative path.

