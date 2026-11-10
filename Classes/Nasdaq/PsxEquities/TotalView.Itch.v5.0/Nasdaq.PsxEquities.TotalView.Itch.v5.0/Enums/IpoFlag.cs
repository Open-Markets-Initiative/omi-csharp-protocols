namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates if the NASDAQ security is set up for IPO release. This field is intended to help NASDAQ market participant firms comply with FINRA Rule 5131(b).
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum IpoFlag : byte
{
    /// <summary>
    ///  Set up for IPO release
    /// </summary>
    Yes = (byte)'Y',

    /// <summary>
    ///  Not set up for IPO release
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Not available
    /// </summary>
    Na = (byte)' ',
}
