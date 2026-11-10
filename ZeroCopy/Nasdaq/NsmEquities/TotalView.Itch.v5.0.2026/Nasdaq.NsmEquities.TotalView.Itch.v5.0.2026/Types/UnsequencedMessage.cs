namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
/// </summary>
public struct UnsequencedMessage
{
    /// <summary>
    ///  The size of the Unsequenced Message field in bytes.
    /// </summary>
    public const int Size = 0;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => $"Data[0]";
}
