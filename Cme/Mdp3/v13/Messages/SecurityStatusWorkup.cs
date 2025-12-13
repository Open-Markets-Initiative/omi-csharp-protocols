using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Security Status Workup: SecurityStatusWorkup
/// </summary>

public partial class SecurityStatusWorkup
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Workup price
    /// </summary>
    public long MdEntryPxOptionalEx => Fields.MdEntryPxOptionalEx.Value;

    /// <summary>
    ///  Security ID
    /// </summary>
    public int SecurityId => Fields.SecurityId.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  Trade Session Date
    /// </summary>
    public ushort TradeDate => Fields.TradeDate.Value;

    /// <summary>
    ///  Contains the workup ID; unique per instrument per day
    /// </summary>
    public uint TradeLinkId => Fields.TradeLinkId.Value;

    /// <summary>
    ///  Identifies the trading status applicable to the instrument
    /// </summary>
    public WorkupTradingStatus WorkupTradingStatus => Fields.WorkupTradingStatus.Value;

    /// <summary>
    ///  Identifies the reason for the status change
    /// </summary>
    public HaltReason HaltReason => Fields.HaltReason.Value;

    /// <summary>
    ///  Identifies an additional event or a rule related to the status
    /// </summary>
    public SecurityTradingEvent SecurityTradingEvent => Fields.SecurityTradingEvent.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public MdEntryPxOptionalEx MdEntryPxOptionalEx;
        public SecurityId SecurityId;
        public MatchEventIndicator MatchEventIndicator;
        public TradeDate TradeDate;
        public TradeLinkId TradeLinkId;
        public WorkupTradingStatus WorkupTradingStatus;
        public HaltReason HaltReason;
        public SecurityTradingEvent SecurityTradingEvent;
    };

    protected Layout Fields;
};
