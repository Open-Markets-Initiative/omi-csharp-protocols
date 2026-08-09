using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Represents the SystemEventMessage message from the Deep protocol.
/// </summary>

public partial class SystemEventMessage
{
    /// <summary>
    ///  System event identifier
    /// </summary>
    public char SystemEvent => Fields.SystemEvent.Value;

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
