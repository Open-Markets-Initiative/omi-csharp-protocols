using System.Numerics;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

internal static class Padded
{
    internal static T ReadBigEndian<T>(ReadOnlySpan<byte> wire) where T : IBinaryInteger<T>, IMinMaxValue<T> => T.ReadBigEndian(wire, isUnsigned: !T.IsNegative(T.MinValue));

    internal static T ReadLittleEndian<T>(ReadOnlySpan<byte> wire) where T : IBinaryInteger<T>, IMinMaxValue<T> => T.ReadLittleEndian(wire, isUnsigned: !T.IsNegative(T.MinValue));

    internal static void WriteBigEndian<T>(Span<byte> wire, T value) where T : IBinaryInteger<T>
    {
        Span<byte> storage = stackalloc byte[value.GetByteCount()];
        value.WriteBigEndian(storage);
        storage[^wire.Length..].CopyTo(wire);
    }

    internal static void WriteLittleEndian<T>(Span<byte> wire, T value) where T : IBinaryInteger<T>
    {
        Span<byte> storage = stackalloc byte[value.GetByteCount()];
        value.WriteLittleEndian(storage);
        storage[..wire.Length].CopyTo(wire);
    }
}
