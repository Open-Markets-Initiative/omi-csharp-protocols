using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Leg information
/// </summary>
public static class LegInformation
{
    /// <summary>
    ///  The fixed fields in wire order, laid out as they are in the bytes.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Layout
    {
        /// <summary>
        ///  The length of the Leg Information in bytes.
        /// </summary>
        public const int Size = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOptionId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSecuritySymbol + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationYear + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationMonth + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationDay + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExplicitStrikePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOptionType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegSide + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegRatio;

        /// <summary>
        ///  Gets the Leg Information laid over the start of the bytes.
        /// </summary>
        /// <remarks>
        ///  Checks only that the bytes are long enough.
        /// </remarks>
        /// <exception cref="ArgumentException">
        ///  The bytes are shorter than the Leg Information.
        /// </exception>
        public static ref readonly Layout Read(ReadOnlySpan<byte> source)
        {
            if (Size > source.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(source));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(source);
        }

        /// <summary>
        ///  Clears the Leg Information at the start of the buffer, writes its fixed values, and gets the Leg Information laid over them.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The buffer has no room for the Leg Information.
        /// </exception>
        public static ref Layout Initialize(global::System.Span<byte> destination)
        {
            if (Size > destination.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoRoom, nameof(destination));
            destination[..Size].Clear();
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
            if (Size > bytes.Length) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.ReadTooShort, nameof(bytes));
            return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes);
        }

        /// <summary>
        ///  ISE’s Option ID for this leg, valid for the trading day. The same ID as the corresponding Option in the Options Directory Message. Zero (0) for Stock Leg.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OptionId OptionId;
        /// <summary>
        ///  Denotes the option root symbol (security symbol)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SecuritySymbol SecuritySymbol;
        /// <summary>
        ///  Leg identifier within this strategy. This is an exchange-assigned 0-based index. E.g. Nth leg has LegId=N-1.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegId LegId;
        /// <summary>
        ///  Last two digits of the year of the option expiration
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationYear ExpirationYear;
        /// <summary>
        ///  Expiration Month of the option (1-12)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationMonth ExpirationMonth;
        /// <summary>
        ///  Day of the Month of expiration (1-31)
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationDay ExpirationDay;
        /// <summary>
        ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExplicitStrikePrice ExplicitStrikePrice;
        /// <summary>
        ///  Option Type:
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OptionTypeField OptionType;
        /// <summary>
        ///  Indicates the side of the leg
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegSideField LegSide;
        /// <summary>
        ///  Leg Ratio
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegRatio LegRatio;
    }

    /// <summary>
    ///  The Leg Information fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  ISE’s Option ID for this leg, valid for the trading day. The same ID as the corresponding Option in the Options Directory Message. Zero (0) for Stock Leg.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OptionId OptionId { get; init; }

        /// <summary>
        ///  Denotes the option root symbol (security symbol)
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SecuritySymbol SecuritySymbol { get; init; }

        /// <summary>
        ///  Leg identifier within this strategy. This is an exchange-assigned 0-based index. E.g. Nth leg has LegId=N-1.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegId LegId { get; init; }

        /// <summary>
        ///  Last two digits of the year of the option expiration
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationYear ExpirationYear { get; init; }

        /// <summary>
        ///  Expiration Month of the option (1-12)
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationMonth ExpirationMonth { get; init; }

        /// <summary>
        ///  Day of the Month of expiration (1-31)
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExpirationDay ExpirationDay { get; init; }

        /// <summary>
        ///  Denotes the explicit strike price of the option. Refer to Data Types for field processing notes.
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ExplicitStrikePrice ExplicitStrikePrice { get; init; }

        /// <summary>
        ///  Option Type:
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.OptionTypeField OptionType { get; init; }

        /// <summary>
        ///  Indicates the side of the leg
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegSideField LegSide { get; init; }

        /// <summary>
        ///  Leg Ratio
        /// </summary>
        public required global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.LegRatio LegRatio { get; init; }

        /// <summary>
        ///  Creates the value of the Leg Information, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    internal const int LayoutSize = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOptionId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeSecuritySymbol + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegId + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationYear + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationMonth + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExpirationDay + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeExplicitStrikePrice + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeOptionType + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegSide + global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.SizeLegRatio;

    /// <summary>
    ///  Writes the Leg Information to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Leg Information.
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
        ref Layout optionIdWriterLayout = ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Layout>(bytes[cursor.Index..]);
        optionIdWriterLayout.OptionId = value.OptionId;
        optionIdWriterLayout.SecuritySymbol = value.SecuritySymbol;
        optionIdWriterLayout.LegId = value.LegId;
        optionIdWriterLayout.ExpirationYear = value.ExpirationYear;
        optionIdWriterLayout.ExpirationMonth = value.ExpirationMonth;
        optionIdWriterLayout.ExpirationDay = value.ExpirationDay;
        optionIdWriterLayout.ExplicitStrikePrice = value.ExplicitStrikePrice;
        optionIdWriterLayout.OptionType = value.OptionType;
        optionIdWriterLayout.LegSide = value.LegSide;
        optionIdWriterLayout.LegRatio = value.LegRatio;
        cursor.Index += LayoutSize;
        return bytes[start..cursor.Index];
    }
}
