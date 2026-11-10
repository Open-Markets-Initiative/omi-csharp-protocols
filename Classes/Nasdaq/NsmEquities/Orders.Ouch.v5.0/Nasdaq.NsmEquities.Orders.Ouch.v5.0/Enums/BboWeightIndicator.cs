namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  BBO Weight Indicator
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum BboWeightIndicator : byte
{
    /// <summary>
    ///  0-0.2%
    /// </summary>
    Weight0To02Percent = (byte)'0',

    /// <summary>
    ///  0.2%-1%
    /// </summary>
    Weight02To1Percent = (byte)'1',

    /// <summary>
    ///  1%-2%
    /// </summary>
    Weight1To2Percent = (byte)'2',

    /// <summary>
    ///  greater than 2%
    /// </summary>
    GreaterThan2 = (byte)'3',

    /// <summary>
    ///  unspecified (default)
    /// </summary>
    Unspecified = (byte)' ',

    /// <summary>
    ///  Sets the QBBO while joining the NBBO
    /// </summary>
    SetsTheQbboWhileJoiningTheNbbo = (byte)'S',

    /// <summary>
    ///  Improves the NBBO upon entry
    /// </summary>
    ImprovesTheNbboUponEntry = (byte)'N',
}
