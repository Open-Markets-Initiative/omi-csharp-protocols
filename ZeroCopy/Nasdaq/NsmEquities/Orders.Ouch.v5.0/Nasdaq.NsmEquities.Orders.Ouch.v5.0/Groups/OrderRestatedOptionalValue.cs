using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class OrderRestatedOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Order Restated Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Order Restated Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed quantity (i.e. an order with reserves).
        /// </summary>
        DisplayQuantity = 1,
        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed price.
        /// </summary>
        DisplayPrice = 2,
        /// <summary>
        ///  An alternative order reference number used when publishing the order on the Nasdaq market data feeds (identifying, for example, the displayed portion of a reserve order)
        /// </summary>
        SecondaryOrdRefNum = 3,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 4,
    }

    /// <summary>
    ///  Reads the Order Restated Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte orderRestatedOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte orderRestatedOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.orderRestatedOptionalFieldValue = orderRestatedOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case)Parser.Valid(Parser.SelectOrderRestatedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderRestatedOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Order Restated Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Order Restated Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureOrderRestatedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderRestatedOptionalFieldValue))];

        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed quantity (i.e. an order with reserves).
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DisplayQuantity DisplayQuantity
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DisplayQuantity>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed price.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DisplayPrice DisplayPrice
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DisplayPrice>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  An alternative order reference number used when publishing the order on the Nasdaq market data feeds (identifying, for example, the displayed portion of a reserve order)
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SecondaryOrdRefNum SecondaryOrdRefNum
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SecondaryOrdRefNum>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly UserRefIdx UserRefIdx
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Order Restated Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayQuantity displayQuantity;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayPrice displayPrice;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SecondaryOrdRefNum secondaryOrdRefNum;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
        }

        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed quantity (i.e. an order with reserves).
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayQuantity DisplayQuantity
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.displayQuantity = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity;
            }
        }

        /// <summary>
        ///  Used in the Order Restated Message only. Represents an update of an order's displayed price.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayPrice DisplayPrice
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.displayPrice = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice;
            }
        }

        /// <summary>
        ///  An alternative order reference number used when publishing the order on the Nasdaq market data feeds (identifying, for example, the displayed portion of a reserve order)
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SecondaryOrdRefNum SecondaryOrdRefNum
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.secondaryOrdRefNum = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum;
            }
        }

        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx UserRefIdx
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.userRefIdx = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Display Quantity.
        /// </summary>
        public const byte DisplayQuantityOrderRestatedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalField.DisplayQuantity;

        /// <summary>
        ///  The value in the bytes that selects the Display Price.
        /// </summary>
        public const byte DisplayPriceOrderRestatedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalField.DisplayPrice;

        /// <summary>
        ///  The value in the bytes that selects the Secondary Ord Ref Num.
        /// </summary>
        public const byte SecondaryOrdRefNumOrderRestatedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalField.SecondaryOrdRefNum;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxOrderRestatedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalField.UserRefIdx;

        /// <summary>
        ///  Creates the value of the Order Restated Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int DisplayQuantityCase = 1;
    private const int DisplayPriceCase = 2;
    private const int SecondaryOrdRefNumCase = 3;
    private const int OrderRestatedOptionalValueUserRefIdxCase = 4;

    /// <summary>
    ///  Writes the Order Restated Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Restated Optional Value.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int orderRestatedOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int orderRestatedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplayQuantity > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderRestatedOptionalValueWriterCase = DisplayQuantityCase;
            int displayQuantityWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayQuantity>(bytes[displayQuantityWriterPosition..]) = value.caseStorage.displayQuantity;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplayQuantity;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplayPrice > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderRestatedOptionalValueWriterCase = DisplayPriceCase;
            int displayPriceWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisplayPrice>(bytes[displayPriceWriterPosition..]) = value.caseStorage.displayPrice;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDisplayPrice;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSecondaryOrdRefNum > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderRestatedOptionalValueWriterCase = SecondaryOrdRefNumCase;
            int secondaryOrdRefNumWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SecondaryOrdRefNum>(bytes[secondaryOrdRefNumWriterPosition..]) = value.caseStorage.secondaryOrdRefNum;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSecondaryOrdRefNum;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderRestatedOptionalValueWriterCase = OrderRestatedOptionalValueUserRefIdxCase;
            int orderRestatedOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[orderRestatedOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref orderRestatedOptionalValueWriterCaseRecord))
        {
            int recorded = orderRestatedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : orderRestatedOptionalValueWriterCase;
            if (orderRestatedOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && orderRestatedOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            orderRestatedOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
