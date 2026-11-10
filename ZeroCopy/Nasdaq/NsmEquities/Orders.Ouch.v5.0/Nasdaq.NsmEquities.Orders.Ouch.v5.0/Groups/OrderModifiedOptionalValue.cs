using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class OrderModifiedOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Order Modified Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Order Modified Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 1,
        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        SharesLocated = 2,
        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        LocateBroker = 3,
    }

    /// <summary>
    ///  Reads the Order Modified Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte orderModifiedOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte orderModifiedOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.orderModifiedOptionalFieldValue = orderModifiedOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case)Parser.Valid(Parser.SelectOrderModifiedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderModifiedOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Order Modified Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Order Modified Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureOrderModifiedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderModifiedOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SharesLocatedField SharesLocated
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SharesLocatedField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LocateBroker LocateBroker
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LocateBroker>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Order Modified Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField sharesLocated;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker locateBroker;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField SharesLocated
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.sharesLocated = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated;
            }
        }

        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker LocateBroker
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.locateBroker = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxOrderModifiedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Shares Located.
        /// </summary>
        public const byte SharesLocatedOrderModifiedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalField.SharesLocated;

        /// <summary>
        ///  The value in the bytes that selects the Locate Broker.
        /// </summary>
        public const byte LocateBrokerOrderModifiedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalField.LocateBroker;

        /// <summary>
        ///  Creates the value of the Order Modified Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int OrderModifiedOptionalValueUserRefIdxCase = 1;
    private const int OrderModifiedOptionalValueSharesLocatedCase = 2;
    private const int LocateBrokerCase = 3;

    /// <summary>
    ///  Writes the Order Modified Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Modified Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int orderModifiedOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int orderModifiedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderModifiedOptionalValueWriterCase = OrderModifiedOptionalValueUserRefIdxCase;
            int orderModifiedOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[orderModifiedOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderModifiedOptionalValueWriterCase = OrderModifiedOptionalValueSharesLocatedCase;
            int orderModifiedOptionalValueSharesLocatedWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField>(bytes[orderModifiedOptionalValueSharesLocatedWriterPosition..]) = value.caseStorage.sharesLocated;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderModifiedOptionalValueWriterCase = LocateBrokerCase;
            int locateBrokerWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker>(bytes[locateBrokerWriterPosition..]) = value.caseStorage.locateBroker;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref orderModifiedOptionalValueWriterCaseRecord))
        {
            int recorded = orderModifiedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : orderModifiedOptionalValueWriterCase;
            if (orderModifiedOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && orderModifiedOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            orderModifiedOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
