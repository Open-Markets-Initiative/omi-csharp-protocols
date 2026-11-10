using System.Runtime.CompilerServices;

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

/// <summary>
///  Reads and writes the Match Algorithm field in place.
/// </summary>
public struct MatchAlgorithmField
{
    /// <summary>
    ///  The size of the Match Algorithm field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The matching algorithm used for the instrument, typically FIFO
    /// </summary>
    public MatchAlgorithm Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Match Algorithm field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly MatchAlgorithm Decode()
        => (MatchAlgorithm)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Match Algorithm field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(MatchAlgorithm value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
