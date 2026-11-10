using System.Runtime.CompilerServices;

namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The flow schedule type
/// </summary>
public struct FlowScheduleType
{
    /// <summary>
    ///  The size of the Flow Schedule Type field in bytes.
    /// </summary>
    public const int Size = 1;

    /// <summary>
    ///  The flow schedule type
    /// </summary>
    public sbyte Value
    {
        readonly get => Decode();
        set => Encode(value);
    }

    /// <summary>
    ///  Returns the wire value of the Flow Schedule Type field.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly sbyte Decode()
        => (sbyte)Byte;

    /// <summary>
    ///  Writes an unscaled wire value into the Flow Schedule Type field.
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
