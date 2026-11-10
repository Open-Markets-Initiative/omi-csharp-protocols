namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Indicates if the instrument is user defined
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum UserDefinedInstrument : byte
{
    /// <summary>
    ///  No
    /// </summary>
    No = (byte)'N',

    /// <summary>
    ///  Yes
    /// </summary>
    Yes = (byte)'Y',
}
