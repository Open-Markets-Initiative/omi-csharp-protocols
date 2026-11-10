namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  The Nasdaq PSX cross session for which the message is being generated.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum CrossType : byte
{
    /// <summary>
    ///  NASDAQ PSX Opening Cross.
    /// </summary>
    OpeningCross = (byte)'O',

    /// <summary>
    ///  NASDAQ PSX Closing Cross.
    /// </summary>
    ClosingCross = (byte)'C',

    /// <summary>
    ///  Cross for IPO and halted / paused securities.
    /// </summary>
    CrossHaltedOrPaused = (byte)'H',

    /// <summary>
    ///  NASDAQ PSX Cross Network: Intraday Cross and Post-Close Cross
    /// </summary>
    IntradayCrossAndPostCloseCross = (byte)'I',
}
