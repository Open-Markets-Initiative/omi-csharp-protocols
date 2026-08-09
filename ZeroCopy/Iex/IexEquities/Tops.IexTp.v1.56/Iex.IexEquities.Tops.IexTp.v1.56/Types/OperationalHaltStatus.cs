using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Operational halt status identifier
/// </summary>

public struct OperationalHaltStatus
{
    /// <summary>
    ///  Iex Specific Operational Trading Halt
    /// </summary>
    public const char IexSpecificOperationalTradingHalt = 'O';

    /// <summary>
    ///  Not Operationally Halted On Iex
    /// </summary>
    public const char NotOperationallyHaltedOnIex = 'N';

    /// <summary>
    ///  Size of OperationalHaltStatus in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Gets the decoded Operational Halt Status value.
    /// </summary>
    public readonly char Value
        => (char)Byte;

    /// <summary>
    ///  Decodes the raw bytes into the Operational Halt Status value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly char Decode()
        => (char)Byte;

    /// <summary>
    ///  Encodes a value into the underlying Operational Halt Status bytes.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(char value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the string representation of the Operational Halt Status value.
    /// </summary>
    public readonly override string ToString()
        => $"{Value}";

    /// <summary>
    ///  Underlying byte
    /// </summary>
    internal byte Byte;
}
