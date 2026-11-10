namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

/// <summary>
///  &lt;space&gt; = regular quote/autox eligible “X” = Pre-Open/Halted
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum QuoteCondition : byte
{
    /// <summary>
    ///  Regular Quote Auto Execution Eligible
    /// </summary>
    RegularQuote = (byte)' ',

    /// <summary>
    ///  Halsted or Preopen
    /// </summary>
    Halted = (byte)'X',
}
