using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Trade Id: IEX Generated Identifier
/// </summary>

public struct TradeId
{
    /// <summary>
    ///  Size of Trade Id in bytes
    /// </summary>
    public const int Size = 8;

    /// <summary>
    ///  Trade Id value
    /// </summary>
    public readonly ulong Value
        => Decode();

    /// <summary>
    ///  Read Trade Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ulong Decode()
        => Underlying;

    /// <summary>
    ///  Write Trade Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ulong value)
        => Underlying = value;

    /// <summary>
    ///  Trade Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal ulong Underlying;
}
