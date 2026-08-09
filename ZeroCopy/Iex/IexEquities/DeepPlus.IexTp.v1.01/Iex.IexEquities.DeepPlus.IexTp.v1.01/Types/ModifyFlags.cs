using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Modify Flags: Bitfield
/// </summary>

public struct ModifyFlags
{
    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte PriorityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Priority
    /// </summary>
    public readonly bool Priority => (Value & PriorityMask) != 0;

    /// <summary>
    ///  Size of ModifyFlags in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Modify Flags value
    /// </summary>
    public readonly byte Value
        => Underlying;

    /// <summary>
    ///  Modify Flags as string
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Encodes a Modify Flags value into the underlying bytes
    /// </summary>
    public void Encode(byte value)
        => Underlying = value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal byte Underlying;
}
