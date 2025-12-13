namespace Iex.Deep;

using System.Runtime.CompilerServices;

/// <summary>
///  Security Directory Flags: Bitfield
/// </summary>

public sealed class SecurityDirectoryFlags
{
    /// <summary>
    ///  Unused 5
    /// </summary>
    public const byte Unused5 = 1 << 0;

    /// <summary>
    ///  Etp
    /// </summary>
    public const byte Etp = 1 << 5;

    /// <summary>
    ///  When Issued
    /// </summary>
    public const byte WhenIssued = 1 << 6;

    /// <summary>
    ///  Test Security
    /// </summary>
    public const byte TestSecurity = 1 << 7;

    /// <summary>
    ///  Length of Security Directory Flags in bytes
    /// </summary>
    public const int Length = 1;
}
