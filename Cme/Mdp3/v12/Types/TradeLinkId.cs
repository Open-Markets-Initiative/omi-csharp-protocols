using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Trade Link Id: Contains the workup ID; unique per instrument per day
/// </summary>

public struct TradeLinkId
{
    /// <summary>
    ///  Fix Tag for Trade Link Id
    /// </summary>
    public const ushort FixTag = 820;

    /// <summary>
    ///  Size of Trade Link Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Trade Link Id value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Trade Link Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Trade Link Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Trade Link Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
