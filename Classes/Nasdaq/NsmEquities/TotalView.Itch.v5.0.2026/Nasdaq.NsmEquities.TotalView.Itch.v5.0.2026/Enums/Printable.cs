namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Indicates if the execution should be reflected on time and sale displays and volume calculations
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Printable : byte
{
    /// <summary>
    ///  Non-printable
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Printable
    /// </summary>
    Yes = (byte)'Y',
}
