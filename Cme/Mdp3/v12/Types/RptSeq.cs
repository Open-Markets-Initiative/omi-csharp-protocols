using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Rpt Seq: Sequence number of the last Market Data entry processed for the instrument
/// </summary>

public struct RptSeq
{
    /// <summary>
    ///  Fix Tag for Rpt Seq
    /// </summary>
    public const ushort FixTag = 83;

    /// <summary>
    ///  Size of Rpt Seq in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Rpt Seq value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Rpt Seq
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Rpt Seq
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Rpt Seq as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
