namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0;

/// <summary>
///  Indicates the directional relationship between the ETP and underlying index. Example: An ETP Leverage Factor of 3 and an Inverse value of ‘Y’ indicates the ETP will decrease by a value of 3.
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
