using System.Runtime.InteropServices;

namespace Iex.IexEquities.Tops.IexTp;

/// <summary>
///  Represents the OperationalHaltStatusMessage message from the Tops protocol.
/// </summary>

public partial class OperationalHaltStatusMessage
{
    /// <summary>
    ///  Operational halt status identifier
    /// </summary>
    public char OperationalHaltStatus => Fields.OperationalHaltStatus.Value;

    /// <summary>
    ///  Time stamp of the system event
    /// </summary>
    public DateTime Timestamp => Fields.Timestamp.Value;

    /// <summary>
    ///  Security identifier
    /// </summary>
    public string Symbol => Fields.Symbol.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public OperationalHaltStatus OperationalHaltStatus;
        public Timestamp Timestamp;
        public Symbol Symbol;
    };

    protected Layout Fields;
};
