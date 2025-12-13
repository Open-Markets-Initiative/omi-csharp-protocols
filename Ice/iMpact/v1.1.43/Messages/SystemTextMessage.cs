using System.Runtime.InteropServices;

namespace Ice.iMpact;

/// <summary>
///  System Text Message: System Text Message
/// </summary>

public partial class SystemTextMessage
{
    /// <summary>
    ///  Text Message
    /// </summary>
    public string TextMessage => Fields.TextMessage.Value;

    /// <summary>
    ///  Date time the trade was investigated
    /// </summary>
    public DateTime MessageDateTime => Fields.MessageDateTime.Value;

    /// <summary>
    ///  Extra field for text message when TextMessage field is not big enough. This should be appended to TextMessage if it is not empty.
    /// </summary>
    public string TextMessageExtraFld => Fields.TextMessageExtraFld.Value;

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public unsafe struct Layout
    {
        public TextMessage TextMessage;
        public MessageDateTime MessageDateTime;
        public TextMessageExtraFld TextMessageExtraFld;
    };

    protected Layout Fields;
};
