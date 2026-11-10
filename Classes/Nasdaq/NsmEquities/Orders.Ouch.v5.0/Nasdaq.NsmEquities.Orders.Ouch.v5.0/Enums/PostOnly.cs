namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  PostOnly
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum PostOnly : byte
{
    /// <summary>
    ///  Post Only (Mid-point Post Only not available on BX)
    /// </summary>
    PostOnlyValue = (byte)'P',

    /// <summary>
    ///  No (default)
    /// </summary>
    No = (byte)'N',
}
