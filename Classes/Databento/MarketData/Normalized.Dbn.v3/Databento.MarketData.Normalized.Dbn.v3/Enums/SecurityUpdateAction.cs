namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  Indicates if the instrument definition has been added, modified, or deleted
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum SecurityUpdateAction : byte
{
    /// <summary>
    ///  Add
    /// </summary>
    Add = (byte)'A',

    /// <summary>
    ///  Delete
    /// </summary>
    Delete = (byte)'D',

    /// <summary>
    ///  Modify
    /// </summary>
    Modify = (byte)'M',

    /// <summary>
    ///  Invalid
    /// </summary>
    Invalid = (byte)'~',
}
