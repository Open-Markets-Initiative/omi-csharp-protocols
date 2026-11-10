namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026;

/// <summary>
///  Login Reject Codes
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum RejectReasonCode : byte
{
    /// <summary>
    ///  The Login Request Packet's username and password combination was invalid
    /// </summary>
    NotAuthorized = (byte)'A',

    /// <summary>
    ///  The Login Request Packet's requested session was invalid or not available
    /// </summary>
    SessionNotAvailable = (byte)'S',
}
