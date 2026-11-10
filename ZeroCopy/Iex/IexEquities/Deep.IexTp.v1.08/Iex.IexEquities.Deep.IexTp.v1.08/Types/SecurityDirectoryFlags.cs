using System.Runtime.CompilerServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

/// <summary>
///  Security Directory Flags
/// </summary>
public struct SecurityDirectoryFlags
{
    /// <summary>
    ///  The bits of the Etp flag in <see cref="Value"/>.
    /// </summary>
    public const byte EtpMask = unchecked((byte)0x20UL);
    /// <summary>
    ///  Symbol is an ETP
    /// </summary>
    public bool Etp
    {
        readonly get => (Value & EtpMask) != 0;
        set => Value = value ? (byte)(Value | EtpMask) : (byte)(Value & ~EtpMask);
    }

    /// <summary>
    ///  The bits of the When Issued flag in <see cref="Value"/>.
    /// </summary>
    public const byte WhenIssuedMask = unchecked((byte)0x40UL);
    /// <summary>
    ///  Symbol is a when issued security
    /// </summary>
    public bool WhenIssued
    {
        readonly get => (Value & WhenIssuedMask) != 0;
        set => Value = value ? (byte)(Value | WhenIssuedMask) : (byte)(Value & ~WhenIssuedMask);
    }

    /// <summary>
    ///  The bits of the Test Security flag in <see cref="Value"/>.
    /// </summary>
    public const byte TestSecurityMask = unchecked((byte)0x80UL);
    /// <summary>
    ///  Symbol is a test security
    /// </summary>
    public bool TestSecurity
    {
        readonly get => (Value & TestSecurityMask) != 0;
        set => Value = value ? (byte)(Value | TestSecurityMask) : (byte)(Value & ~TestSecurityMask);
    }

    /// <summary>
    ///  The size of the Security Directory Flags field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Security Directory Flags
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
    ///  Writes a value into the Security Directory Flags field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(byte value)
        => Underlying = value;

    internal byte Underlying;
}
