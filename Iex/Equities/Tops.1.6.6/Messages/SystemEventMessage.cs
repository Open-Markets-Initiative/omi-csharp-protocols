using System.Runtime.InteropServices;

namespace Iex.Tops;

/// <summary>
///  System Event Message: The System Event Message is used to indicate events that apply to the market or the data feed
/// </summary>

public partial class SystemEventMessage
{
    /// <summary>
    ///  System event identifier
    /// </summary>
    public SystemEvent SystemEvent => Fields.SystemEvent.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public SystemEvent SystemEvent;
        public Timestamp Timestamp;
    };

    protected Layout Fields;
};
