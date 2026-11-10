namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the directional relationship between the ETP and underlying index
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum InverseIndicator : byte
{
    /// <summary>
    ///  ETP is an Inverse ETP
    /// </summary>
    InverseEtp = (byte)'Y',

    /// <summary>
    ///  ETP is not an Inverse ETP
    /// </summary>
    NotInverseEtp = (byte)'N',
}
