using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Security Status: SecurityStatus
/// </summary>

public partial class SecurityStatus
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Security Group
    /// </summary>
    public string SecurityGroup => Fields.SecurityGroup.Value;

    /// <summary>
    ///  Product Code within Security Group specified
    /// </summary>
    public string Asset => Fields.Asset.Value;

    /// <summary>
    ///  If this tag is present, 35=f message is sent for the instrument
    /// </summary>
    public int SecurityIdOptional => Fields.SecurityIdOptional.Value;

    /// <summary>
    ///  Trade Session Date
    /// </summary>
    public ushort TradeDate => Fields.TradeDate.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  Identifies the trading status applicable to the instrument or Security Group
    /// </summary>
    public SecurityTradingStatus SecurityTradingStatus => Fields.SecurityTradingStatus.Value;

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
        public SecurityGroup SecurityGroup;
        public Asset Asset;
        public SecurityIdOptional SecurityIdOptional;
        public TradeDate TradeDate;
        public MatchEventIndicator MatchEventIndicator;
        public SecurityTradingStatus SecurityTradingStatus;
        public HaltReason HaltReason;
        public SecurityTradingEvent SecurityTradingEvent;
    };

    protected Layout Fields;
};
