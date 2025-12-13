using System.Runtime.InteropServices;

namespace Cme.Mdp3;

/// <summary>
///  Channel Reset: ChannelReset
/// </summary>

public partial class ChannelReset
{
    /// <summary>
    ///  Start of event processing time in number of nanoseconds since Unix epoch
    /// </summary>
    public DateTime TransactTime => Fields.TransactTime.Value;

    /// <summary>
    ///  MatchEventIndicator bit set
    /// </summary>
    public string MatchEventIndicator => Fields.MatchEventIndicator.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TransactTime TransactTime;
        public MatchEventIndicator MatchEventIndicator;
    };

    protected Layout Fields;
};
