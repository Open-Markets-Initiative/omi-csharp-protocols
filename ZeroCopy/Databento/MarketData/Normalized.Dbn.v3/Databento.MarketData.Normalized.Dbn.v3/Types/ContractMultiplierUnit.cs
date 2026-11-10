using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The contract multiplier unit
/// </summary>
public struct ContractMultiplierUnit
{
    /// <summary>
    ///  The size of the Contract Multiplier Unit field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The contract multiplier unit
    /// </summary>
    public sbyte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Contract Multiplier Unit field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly sbyte Decode()
        => (sbyte)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Contract Multiplier Unit field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(sbyte value)
        => Byte = (byte)value;

    /// <summary>
    ///  Returns the value of the field as text.
    /// </summary>
    public readonly override string ToString()
        => string.Create(global::System.Globalization.CultureInfo.InvariantCulture, $"{Value}");

    internal byte Byte;
}
