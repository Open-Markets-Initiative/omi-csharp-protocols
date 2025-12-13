using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Tot Num Reports: Total number of messages replayed in the loop
/// </summary>

public struct TotNumReports
{
    /// <summary>
    ///  Fix Tag for Tot Num Reports
    /// </summary>
    public const ushort FixTag = 911;

    /// <summary>
    ///  Size of Tot Num Reports in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Tot Num Reports value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Tot Num Reports
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Tot Num Reports
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Tot Num Reports as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
