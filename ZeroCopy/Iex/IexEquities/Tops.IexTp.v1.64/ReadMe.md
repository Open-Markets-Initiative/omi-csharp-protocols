# Investors Exchange Top Of Book 1.64

Generated C# parser for Investors Exchange Top Of Book v1.64.

Zero-copy sequential-layout parser — [StructLayout(LayoutKind.Sequential, Pack = 1)] unsafe structs overlaying packet bytes directly, no parsing loop.

## Build

```
dotnet build Iex.IexEquities.Tops.IexTp.v1.64.slnx
```

## Tests

The Test harness walks a whole capture file (frame → payload → parse → counts). Captures are not included in this repository — point the harness at a local pcap:

```
dotnet run --project Iex.IexEquities.Tops.IexTp.v1.64/Test/Iex.IexEquities.Tops.IexTp.v1.64.Test.csproj -- <path-to-pcap>
```

## Releases

Pack the parser library into a NuGet package:

```
dotnet pack Iex.IexEquities.Tops.IexTp.v1.64.slnx
```

Produces `Iex.IexEquities.Tops.IexTp.v1.64.ZeroCopy.1.64.0.nupkg`.

## Portability

The packable library `Iex.IexEquities.Tops.IexTp.v1.64.ZeroCopy` is fully self-contained. The Test and example harness projects additionally reference the `ZeroCopy/Pcap.CSharp` support project by relative path.

