using System.Runtime.InteropServices;

namespace Eurex.Eobi;

/// <summary>
///  Message: Eurex Eobi Message
/// </summary>

public partial class Message
{
    /// <summary>
    ///  Message Header Comp
    /// </summary>
    public string MessageHeaderComp => Fields.MessageHeaderComp.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public MessageHeaderComp MessageHeaderComp;
    };

    protected Layout Fields;
};
