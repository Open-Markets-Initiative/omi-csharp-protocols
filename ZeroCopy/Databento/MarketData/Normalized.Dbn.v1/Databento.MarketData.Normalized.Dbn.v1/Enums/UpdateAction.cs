using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Indicates if the statistic is newly added or deleted
/// </summary>
/// <remarks>
///  Each value is the number that represents it on the wire.
/// </remarks>
public enum UpdateAction : byte
{
    /// <summary>
    ///  New
    /// </summary>
    New = 1,

    /// <summary>
    ///  Delete
    /// </summary>
    Delete = 2,

}

/// <summary>
///  Reads and writes the Update Action field in place.
/// </summary>
public struct UpdateActionField
{
    /// <summary>
    ///  The size of the Update Action field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  Indicates if the statistic is newly added or deleted
    /// </summary>
    public UpdateAction Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Update Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly UpdateAction Decode()
        => (UpdateAction)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Update Action field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(UpdateAction value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
