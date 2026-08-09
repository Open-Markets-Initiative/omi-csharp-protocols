using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the TradingStatusMessage message from the DeepPlus protocol.
/// </summary>

public partial class TradingStatusMessage
{
    /// <summary>
    ///  Trading status identifier
    /// </summary>
    public char TradingStatus => Fields.TradingStatus.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    /// <summary>
    ///  Reason for the trading status change
    /// </summary>
    public string Reason => Fields.Reason.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TradingStatus TradingStatus;
        public Timestamp Timestamp;
        public Symbol Symbol;
        public Reason Reason;
    };

    protected Layout Fields;
};
