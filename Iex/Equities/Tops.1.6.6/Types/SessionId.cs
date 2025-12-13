using System.Runtime.CompilerServices;

namespace Iex.Tops;

/// <summary>
///  Session Id: Identifies the session
/// </summary>

public struct SessionId
{
    /// <summary>
    ///  Size of Session Id in bytes
    /// </summary>
    public const int Size = 4;

    /// <summary>
    ///  Session Id value
    /// </summary>
    public readonly uint Value
        => Decode();

    /// <summary>
    ///  Read Session Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly uint Decode()
        => Underlying;

    /// <summary>
    ///  Write Session Id
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(uint value)
        => Underlying = value;

    /// <summary>
    ///  Session Id as string
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal uint Underlying;
}
