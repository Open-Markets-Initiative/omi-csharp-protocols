using System.Runtime.CompilerServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  AIQ strategy used on the cancelled order.
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum AiqStrategy : byte
{
    /// <summary>
    ///  use port default
    /// </summary>
    UsePortDefault = (byte)'*',

    /// <summary>
    ///  Disabled
    /// </summary>
    Disabled = (byte)'N',

    /// <summary>
    ///  Firm Level Decrement Both No Details
    /// </summary>
    FirmLevelDecrementBothNoDetails = (byte)'Y',

    /// <summary>
    ///  Firm Level Decrement Both
    /// </summary>
    FirmLevelDecrementBoth = (byte)'D',

    /// <summary>
    ///  Firm Level Cancel Oldest
    /// </summary>
    FirmLevelCancelOldest = (byte)'O',

    /// <summary>
    ///  Firm Level Cancel Newest
    /// </summary>
    FirmLevelCancelNewest = (byte)'W',

    /// <summary>
    ///  Firm Level Use Remover
    /// </summary>
    FirmLevelUseRemover = (byte)'R',

    /// <summary>
    ///  Organization Level Decrement Both No Details
    /// </summary>
    OrganizationLevelDecrementBothNoDetails = (byte)'y',

    /// <summary>
    ///  Organization Level Decrement Both
    /// </summary>
    OrganizationLevelDecrementBoth = (byte)'d',

    /// <summary>
    ///  Organization Level Cancel Oldest
    /// </summary>
    OrganizationLevelCancelOldest = (byte)'o',

    /// <summary>
    ///  Organization Level Cancel Newest
    /// </summary>
    OrganizationLevelCancelNewest = (byte)'w',

    /// <summary>
    ///  Organization Level Use Remover
    /// </summary>
    OrganizationLevelUseRemover = (byte)'r',

    /// <summary>
    ///  Affiliate Level Decrement Both No Details
    /// </summary>
    AffiliateLevelDecrementBothNoDetails = (byte)'Z',

    /// <summary>
    ///  Affiliate Level Decrement Both
    /// </summary>
    AffiliateLevelDecrementBoth = (byte)'E',

    /// <summary>
    ///  Affiliate Level Cancel Oldest
    /// </summary>
    AffiliateLevelCancelOldest = (byte)'P',

    /// <summary>
    ///  Affiliate Level Cancel Newest
    /// </summary>
    AffiliateLevelCancelNewest = (byte)'X',

    /// <summary>
    ///  Affiliate Level Use Remover
    /// </summary>
    AffiliateLevelUseRemover = (byte)'S',

    /// <summary>
    ///  Match Any Level Decrement Both No Details
    /// </summary>
    MatchAnyLevelDecrementBothNoDetails = (byte)'0',

    /// <summary>
    ///  Match Any Level Decrement Both
    /// </summary>
    MatchAnyLevelDecrementBoth = (byte)'1',

    /// <summary>
    ///  Match Any Level Cancel Oldest
    /// </summary>
    MatchAnyLevelCancelOldest = (byte)'2',

    /// <summary>
    ///  Match Any Level Cancel Newest
    /// </summary>
    MatchAnyLevelCancelNewest = (byte)'4',

    /// <summary>
    ///  Match Any Level Use Remover
    /// </summary>
    MatchAnyLevelUseRemover = (byte)'5',

}

/// <summary>
///  Reads and writes the Aiq Strategy field in place.
/// </summary>
public struct AiqStrategyField
{
    /// <summary>
    ///  The size of the Aiq Strategy field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  AIQ strategy used on the cancelled order.
    /// </summary>
    public AiqStrategy Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Aiq Strategy field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly AiqStrategy Decode()
        => (AiqStrategy)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Aiq Strategy field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(AiqStrategy value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
