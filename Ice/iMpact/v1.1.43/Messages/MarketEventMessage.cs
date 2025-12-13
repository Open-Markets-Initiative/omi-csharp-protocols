using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  Market Event Message: End of Day Market Summary Message
/// </summary>

public partial class MarketEventMessage
{
    /// <summary>
    ///  Market Id
    /// </summary>
    public int MarketId => Fields.MarketId.Value;

    /// <summary>
    ///  ‘A’ – Implication Disabled for the Market
    /// </summary>
    public EventType EventType => Fields.EventType.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MarketId MarketId;
        public EventType EventType;
        public MessageDateTime MessageDateTime;
    };

    protected Layout Fields;
};
