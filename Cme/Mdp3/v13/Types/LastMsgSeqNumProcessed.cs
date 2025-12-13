using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Last Msg Seq Num Processed: Sequence number of the last Incremental feed packet processed. This value is used to synchronize the snapshot loop with the real-time feed
/// </summary>

public struct LastMsgSeqNumProcessed
{
    /// <summary>
    ///  Fix Tag for Last Msg Seq Num Processed
    /// </summary>
    public const ushort FixTag = 369;

    /// <summary>
    ///  Size of Last Msg Seq Num Processed in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Last Msg Seq Num Processed value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Last Msg Seq Num Processed
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Last Msg Seq Num Processed
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Last Msg Seq Num Processed as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
