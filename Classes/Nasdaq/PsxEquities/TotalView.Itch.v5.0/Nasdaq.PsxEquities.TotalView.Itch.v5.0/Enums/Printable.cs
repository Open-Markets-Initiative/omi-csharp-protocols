namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the execution should be reflected on time and sale displays and volume calculations.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Printable : byte
{
    /// <summary>
    ///  non-printable
    /// </summary>
    NonPrintable = (byte)'N',

    /// <summary>
    ///  printable
    /// </summary>
    PrintableValue = (byte)'Y',
}
