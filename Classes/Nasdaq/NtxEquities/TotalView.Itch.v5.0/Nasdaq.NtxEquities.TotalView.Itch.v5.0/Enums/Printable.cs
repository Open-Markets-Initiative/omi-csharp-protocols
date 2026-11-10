namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the execution should be reflected on time and sales displays and volume calculations
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum Printable : byte
{
    /// <summary>
    ///  Non-Printable
    /// </summary>
    NonPrintable = (byte)'N',

    /// <summary>
    ///  Printable
    /// </summary>
    PrintableValue = (byte)'Y',
}
