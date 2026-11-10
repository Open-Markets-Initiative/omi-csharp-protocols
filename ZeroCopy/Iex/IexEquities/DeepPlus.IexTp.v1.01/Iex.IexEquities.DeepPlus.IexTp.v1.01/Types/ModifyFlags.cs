using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Modify Flags
/// </summary>
public struct ModifyFlags
{
    /// <summary>
    ///  The bits of the Priority flag in <see cref="Value"/>.
    /// </summary>
    public const byte PriorityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Order Priority
    /// </summary>
    public bool Priority
    {
        readonly get => (Value & PriorityMask) != 0;
        set => Value = value ? (byte)(Value | PriorityMask) : (byte)(Value & ~PriorityMask);
    }

    /// <summary>
    ///  The size of the Modify Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Modify Flags
    /// </summary>
    public byte Value
    {
        readonly get => Underlying;
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Writes a value into the Modify Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Underlying = value;

    internal byte Underlying;
}
