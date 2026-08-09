using System.Runtime.CompilerServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Security event identifier
/// </summary>

public struct SecurityEvent
{
    /// <summary>
    ///  Opening Process Complete
    /// </summary>
    public const char OpeningProcessComplete = 'O';

    /// <summary>
    ///  Closing Process Complete
    /// </summary>
    public const char ClosingProcessComplete = 'C';

    /// <summary>
    ///  Size of SecurityEvent in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Security Event value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Security Event value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Security Event bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Security Event value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
