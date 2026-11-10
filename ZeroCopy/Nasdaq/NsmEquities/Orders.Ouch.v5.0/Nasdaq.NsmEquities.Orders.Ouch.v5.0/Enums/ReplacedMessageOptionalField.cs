using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Appendage Id
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum ReplacedMessageOptionalField : byte
{
    /// <summary>
    ///  Replaced Message Optional Firm Enum
    /// </summary>
    Firm = 2,

    /// <summary>
    ///  Replaced Message Optional MinQty Enum
    /// </summary>
    MinQty = 3,

    /// <summary>
    ///  Replaced Message Optional MaxFloor Enum
    /// </summary>
    MaxFloor = 5,

    /// <summary>
    ///  Replaced Message Optional PriceType Enum
    /// </summary>
    PriceType = 6,

    /// <summary>
    ///  Replaced Message Optional PostOnly Enum
    /// </summary>
    PostOnly = 12,

    /// <summary>
    ///  Replaced Message Optional ExpireTime Enum
    /// </summary>
    ExpireTime = 15,

    /// <summary>
    ///  Replaced Message Optional TradeNow Enum
    /// </summary>
    TradeNow = 16,

    /// <summary>
    ///  Replaced Message Optional HandleInst Enum
    /// </summary>
    HandleInst = 17,

    /// <summary>
    ///  Replaced Message Optional BBO Weight Indicator Enum
    /// </summary>
    BboWeightIndicator = 18,

    /// <summary>
    ///  Replaced Message Optional UserRefIdx Enum
    /// </summary>
    UserRefIdx = 28,

    /// <summary>
    ///  Replaced Message Optional AIQ Strategy Enum
    /// </summary>
    AiqStrategy = 29,

    /// <summary>
    ///  Replaced Message Optional AIQ Group ID Enum
    /// </summary>
    AiqGroupId = 30,

}

/// <summary>
///  Reads and writes the Replaced Message Optional Field field in place.
/// </summary>
public struct ReplacedMessageOptionalFieldField
{
    /// <summary>
    ///  The size of the Replaced Message Optional Field field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Appendage Id
    /// </summary>
    public ReplacedMessageOptionalField Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Replaced Message Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly ReplacedMessageOptionalField Decode()
        => (ReplacedMessageOptionalField)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Replaced Message Optional Field field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(ReplacedMessageOptionalField value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
