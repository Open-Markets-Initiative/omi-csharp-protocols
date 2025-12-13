using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Md Incremental Refresh Order Book: MDIncrementalRefreshOrderBook
/// </summary>

public partial class MdIncrementalRefreshOrderBook
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    /// <summary>
    ///  2 bytes padding
    /// </summary>
    public string Padding2 => Fields.Padding2.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public MatchEventIndicator MatchEventIndicator;
        public Padding2 Padding2;
    };

    protected Layout Fields;
};
