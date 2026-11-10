using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v2;

/// <summary>
///  The error code
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ErrorCode : byte
{
    /// <summary>
    ///  Auth Failed
    /// </summary>
    AuthFailed = 1,

    /// <summary>
    ///  Api Key Deactivated
    /// </summary>
    ApiKeyDeactivated = 2,

    /// <summary>
    ///  Connection Limit Exceeded
    /// </summary>
    ConnectionLimitExceeded = 3,

    /// <summary>
    ///  Symbol Resolution Failed
    /// </summary>
    SymbolResolutionFailed = 4,

    /// <summary>
    ///  Invalid Subscription
    /// </summary>
    InvalidSubscription = 5,

    /// <summary>
    ///  Internal Error
    /// </summary>
    InternalError = 6,

    /// <summary>
    ///  Skipped Records After Slow Reading
    /// </summary>
    SkippedRecordsAfterSlowReading = 7,

    /// <summary>
    ///  Replay Data Aged Out
    /// </summary>
    ReplayDataAgedOut = 8,

    /// <summary>
    ///  Unset
    /// </summary>
    Unset = 255,

}

/// <summary>
///  Reads and writes the Error Code field in place.
/// </summary>
public struct ErrorCodeField
{
    /// <summary>
    ///  The size of the Error Code field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The error code
    /// </summary>
    public ErrorCode Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Error Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ErrorCode Decode()
        => (ErrorCode)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Error Code field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ErrorCode value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
