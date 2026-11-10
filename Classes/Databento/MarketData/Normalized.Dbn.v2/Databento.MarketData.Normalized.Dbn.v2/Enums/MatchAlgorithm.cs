namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The matching algorithm used for the instrument, typically FIFO
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MatchAlgorithm : byte
{
    /// <summary>
    ///  Undefined
    /// </summary>
    Undefined = (byte)' ',

    /// <summary>
    ///  Allocation
    /// </summary>
    Allocation = (byte)'A',

    /// <summary>
    ///  Pro Rata
    /// </summary>
    ProRata = (byte)'C',

    /// <summary>
    ///  Fifo
    /// </summary>
    Fifo = (byte)'F',

    /// <summary>
    ///  Configurable
    /// </summary>
    Configurable = (byte)'K',

    /// <summary>
    ///  Threshold Pro Rata
    /// </summary>
    ThresholdProRata = (byte)'O',

    /// <summary>
    ///  Time Pro Rata
    /// </summary>
    TimeProRata = (byte)'P',

    /// <summary>
    ///  Threshold Pro Rata Lmm
    /// </summary>
    ThresholdProRataLmm = (byte)'Q',

    /// <summary>
    ///  Fifo Top Lmm
    /// </summary>
    FifoTopLmm = (byte)'S',

    /// <summary>
    ///  Fifo Lmm
    /// </summary>
    FifoLmm = (byte)'T',

    /// <summary>
    ///  Institutional Prioritization
    /// </summary>
    InstitutionalPrioritization = (byte)'V',

    /// <summary>
    ///  Eurodollar Futures
    /// </summary>
    EurodollarFutures = (byte)'Y',
}
