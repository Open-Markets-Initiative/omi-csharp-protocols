namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Modify Flags
/// </summary>
public readonly record struct ModifyFlags
{
    internal ModifyFlags(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///  Gets the integer that contains the bits.
    /// </summary>
    public byte Value { get; init; }

    /// <summary>
    ///  The bit mask of Priority.
    /// </summary>
    public const byte PriorityMask = 0x80;

    /// <summary>
    ///  The position of the lowest bit of Priority.
    /// </summary>
    public const int PriorityShift = 7;

    /// <summary>
    ///  The number of bits in Priority.
    /// </summary>
    public const int PriorityWidth = 1;

    /// <summary>
    ///  Order Priority
    /// </summary>
    public bool Priority
    {
        get => (Value & PriorityMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | PriorityMask : (ulong)Value & ~(ulong)PriorityMask);
    }
}
