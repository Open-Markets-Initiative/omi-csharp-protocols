using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Security Directory Flags: Bitfield
/// </summary>

public struct SecurityDirectoryFlags
{
    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte EtpMask = unchecked((byte)0x20UL);
    /// <summary>
    ///  Etp
    /// </summary>
    public readonly bool Etp => (Value & EtpMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte WhenIssuedMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  When Issued
    /// </summary>
    public readonly bool WhenIssued => (Value & WhenIssuedMask) != 0;

    /// <summary>
    ///  Applies to the container's normalized Value, not the raw underlying bytes.
    /// </summary>
    public const byte TestSecurityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Test Security
    /// </summary>
    public readonly bool TestSecurity => (Value & TestSecurityMask) != 0;

    /// <summary>
    ///  Size of SecurityDirectoryFlags in bytes
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Security Directory Flags value
    /// </summary>
    public readonly byte Value
        => Underlying;

    /// <summary>
    ///  Security Directory Flags as string
    /// </summary>
    public readonly override string ToString()
        => $"0x{Value:X}";

    /// <summary>
    ///  Encodes a Security Directory Flags value into the underlying bytes
    /// </summary>
    public void Encode(byte value)
        => Underlying = value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal byte Underlying;
}
