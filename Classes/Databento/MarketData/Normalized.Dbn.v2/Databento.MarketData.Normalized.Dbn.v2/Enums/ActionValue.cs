namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The event action
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum ActionValue : byte
{
    /// <summary>
    ///  A new order was added to the book
    /// </summary>
    Add = (byte)'A',

    /// <summary>
    ///  An order was fully or partially cancelled
    /// </summary>
    Cancel = (byte)'C',

    /// <summary>
    ///  An existing order was filled; does not affect the book
    /// </summary>
    Fill = (byte)'F',

    /// <summary>
    ///  An existing order was modified: price and/or size
    /// </summary>
    Modify = (byte)'M',

    /// <summary>
    ///  Has no effect on the book, but may carry flags or other information
    /// </summary>
    None = (byte)'N',

    /// <summary>
    ///  Reset the book; clear all orders for an instrument
    /// </summary>
    Clear = (byte)'R',

    /// <summary>
    ///  An aggressing order traded; does not affect the book
    /// </summary>
    Trade = (byte)'T',
}
