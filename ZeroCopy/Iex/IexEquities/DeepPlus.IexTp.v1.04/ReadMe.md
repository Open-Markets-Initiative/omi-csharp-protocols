# Investors Exchange DeepPlus 1.04

Generated C# parser for Investors Exchange DeepPlus v1.04.

Zero-copy sequential-layout parser — [StructLayout(LayoutKind.Sequential, Pack = 1)] unsafe structs overlaying packet bytes directly, no parsing loop.

## Build

```
dotnet build Iex.IexEquities.DeepPlus.IexTp.v1.04.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Iex.IexEquities.DeepPlus.IexTp.v1.04/Test/Iex.IexEquities.DeepPlus.IexTp.v1.04.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.DeepPlus.IexTp.v1.04.slnx
```

Produces `Iex.IexEquities.DeepPlus.IexTp.v1.04.ZeroCopy.1.04.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.DeepPlus.IexTp.v1.04.ZeroCopy` is fully self-contained. The Test and example harness projects additionally reference the `ZeroCopy/Pcap.CSharp` support project by relative path.

