using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp;

/// <summary>
///  Represents the ClearBookMessage message from the DeepPlus protocol.
/// </summary>

public partial class ClearBookMessage
{
    /// <summary>
    ///  Reserved for future use
    /// </summary>
    public char Reserved1 => Fields.Reserved1.Value;

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
        public Reserved1 Reserved1;
        public Timestamp Timestamp;
        public Symbol Symbol;
    };

    protected Layout Fields;
};
