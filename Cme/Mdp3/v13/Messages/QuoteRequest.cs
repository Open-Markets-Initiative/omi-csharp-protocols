using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Quote Request: QuoteRequest
/// </summary>

public partial class QuoteRequest
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  Quote Request ID defined by the exchange
    /// </summary>
    public string QuoteReqId => Fields.QuoteReqId.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  3 bytes padding
    /// </summary>
    public string Padding3 => Fields.Padding3.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public QuoteReqId QuoteReqId;
        public MatchEventIndicator MatchEventIndicator;
        public Padding3 Padding3;
    };

    protected Layout Fields;
};
