using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
/// </summary>
public static class ComplexStrategyOrderOnBookMessage
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Complex Strategy Order On Book Message in bytes.
        /// </summary>
        public const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeStrategyId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSide + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSize + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExecFlag + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderCapacity + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeScope + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOwnerId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeGiveup + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCmta;

        /// <summary>
        ///  Gets the Complex Strategy Order On Book Message laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Complex Strategy Order On Book Message.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (LayoutSize > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Complex Strategy Order On Book Message at the start of the buffer, writes its fixed values, and gets the Complex Strategy Order On Book Message laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Complex Strategy Order On Book Message.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (LayoutSize > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, nameof(destination));
            destination[..LayoutSize].Clear();
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(destination);
        }

        /// <summary>
        ///  Gets the fixed fields over the start of the bytes without clearing or writing them, for changes in place.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the fixed fields.
        /// </exception>
        public static ref Layout Over(global::System.Span<byte> bytes)
        {
            if (LayoutSize > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp;
        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId;
        /// <summary>
        ///  Order Type
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderTypeField OrderType;
        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SideField Side;
        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Price Price;
        /// <summary>
        ///  Size of the order
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Size Size;
        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExecFlagField ExecFlag;
        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderCapacityField OrderCapacity;
        /// <summary>
        ///  Scope value
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ScopeField Scope;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OwnerId OwnerId;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Giveup Giveup;
        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Cmta Cmta;
    }

    /// <summary>
    ///  The Complex Strategy Order On Book Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  The time, expressed as the number of nanoseconds after midnight.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Timestamp Timestamp { get; init; }

        /// <summary>
        ///  ISE’s Strategy ID assigned daily, valid while there are any open complex orders for the day
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyId StrategyId { get; init; }

        /// <summary>
        ///  Order Type
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderTypeField OrderType { get; init; }

        /// <summary>
        ///  ‘B’ = Bid ‘A’ = Offer (Ask) ‘ ’ (blank) = not set (hidden)
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SideField Side { get; init; }

        /// <summary>
        ///  Price of the order in fixed point format with 6 whole number places followed by 4 decimal digits.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Price Price { get; init; }

        /// <summary>
        ///  Size of the order
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Size Size { get; init; }

        /// <summary>
        ///  ‘N’ = None ‘A’ = AON
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExecFlagField ExecFlag { get; init; }

        /// <summary>
        ///  Order Capacity value
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OrderCapacityField OrderCapacity { get; init; }

        /// <summary>
        ///  Scope value
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ScopeField Scope { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OwnerId OwnerId { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Giveup Giveup { get; init; }

        /// <summary>
        ///  Spaces when not set
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Cmta Cmta { get; init; }

        /// <summary>
        ///  Creates the value of the Complex Strategy Order On Book Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeTimestamp + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeStrategyId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSide + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSize + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExecFlag + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOrderCapacity + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeScope + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOwnerId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeGiveup + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeCmta;

    /// <summary>
    ///  Writes the Complex Strategy Order On Book Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Complex Strategy Order On Book Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        if (LayoutSize > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterDestination);
        ref Layout complexStrategyOrderOnBookMessageTimestampWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Timestamp = value.Timestamp;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.StrategyId = value.StrategyId;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.OrderType = value.OrderType;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Side = value.Side;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Price = value.Price;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Size = value.Size;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.ExecFlag = value.ExecFlag;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.OrderCapacity = value.OrderCapacity;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Scope = value.Scope;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.OwnerId = value.OwnerId;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Giveup = value.Giveup;
        complexStrategyOrderOnBookMessageTimestampWriterLayout.Cmta = value.Cmta;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
