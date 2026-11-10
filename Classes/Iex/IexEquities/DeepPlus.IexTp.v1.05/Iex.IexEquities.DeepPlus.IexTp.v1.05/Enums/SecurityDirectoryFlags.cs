namespace Iex.IexEquities.DeepPlus.IexTp.v1_05;

/// <summary>
///  Security Directory Flags
/// </summary>
public readonly record struct SecurityDirectoryFlags
{
    internal SecurityDirectoryFlags(byte value)
    {
        Value = value;
    }

    /// <summary>
    ///  Gets the integer that contains the bits.
    /// </summary>
    public byte Value { get; init; }

    /// <summary>
    ///  The bit mask of Etp.
    /// </summary>
    public const byte EtpMask = 0x20;

    /// <summary>
    ///  The position of the lowest bit of Etp.
    /// </summary>
    public const int EtpShift = 5;

    /// <summary>
    ///  The number of bits in Etp.
    /// </summary>
    public const int EtpWidth = 1;

    /// <summary>
    ///  Symbol is an ETP
    /// </summary>
    public bool Etp
    {
        get => (Value & EtpMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | EtpMask : (ulong)Value & ~(ulong)EtpMask);
    }

    /// <summary>
    ///  The bit mask of When Issued.
    /// </summary>
    public const byte WhenIssuedMask = 0x40;

    /// <summary>
    ///  The position of the lowest bit of When Issued.
    /// </summary>
    public const int WhenIssuedShift = 6;

    /// <summary>
    ///  The number of bits in When Issued.
    /// </summary>
    public const int WhenIssuedWidth = 1;

    /// <summary>
    ///  Symbol is a when issued security
    /// </summary>
    public bool WhenIssued
    {
        get => (Value & WhenIssuedMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | WhenIssuedMask : (ulong)Value & ~(ulong)WhenIssuedMask);
    }

    /// <summary>
    ///  The bit mask of Test Security.
    /// </summary>
    public const byte TestSecurityMask = 0x80;

    /// <summary>
    ///  The position of the lowest bit of Test Security.
    /// </summary>
    public const int TestSecurityShift = 7;

    /// <summary>
    ///  The number of bits in Test Security.
    /// </summary>
    public const int TestSecurityWidth = 1;

    /// <summary>
    ///  Symbol is a test security
    /// </summary>
    public bool TestSecurity
    {
        get => (Value & TestSecurityMask) != 0;
        init => Value = (byte)(value ? (ulong)Value | TestSecurityMask : (ulong)Value & ~(ulong)TestSecurityMask);
    }
}
