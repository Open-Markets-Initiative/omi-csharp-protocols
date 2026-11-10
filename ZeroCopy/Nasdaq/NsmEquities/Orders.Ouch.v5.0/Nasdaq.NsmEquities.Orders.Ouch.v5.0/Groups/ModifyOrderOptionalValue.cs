using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class ModifyOrderOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Modify Order Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Modify Order Optional Value.
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
    ///  Reads the Modify Order Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte modifyOrderOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte modifyOrderOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.modifyOrderOptionalFieldValue = modifyOrderOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case)Parser.Valid(Parser.SelectModifyOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.modifyOrderOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Modify Order Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Modify Order Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureModifyOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.modifyOrderOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LocateBroker>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Modify Order Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case @case;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxModifyOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Shares Located.
        /// </summary>
        public const byte SharesLocatedModifyOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalField.SharesLocated;

        /// <summary>
        ///  The value in the bytes that selects the Locate Broker.
        /// </summary>
        public const byte LocateBrokerModifyOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalField.LocateBroker;

        /// <summary>
        ///  Creates the value of the Modify Order Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int ModifyOrderOptionalValueUserRefIdxCase = 1;
    private const int ModifyOrderOptionalValueSharesLocatedCase = 2;
    private const int ModifyOrderOptionalValueLocateBrokerCase = 3;

    /// <summary>
    ///  Writes the Modify Order Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Modify Order Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int modifyOrderOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int modifyOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            modifyOrderOptionalValueWriterCase = ModifyOrderOptionalValueUserRefIdxCase;
            int modifyOrderOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[modifyOrderOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            modifyOrderOptionalValueWriterCase = ModifyOrderOptionalValueSharesLocatedCase;
            int modifyOrderOptionalValueSharesLocatedWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField>(bytes[modifyOrderOptionalValueSharesLocatedWriterPosition..]) = value.caseStorage.sharesLocated;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            modifyOrderOptionalValueWriterCase = ModifyOrderOptionalValueLocateBrokerCase;
            int modifyOrderOptionalValueLocateBrokerWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker>(bytes[modifyOrderOptionalValueLocateBrokerWriterPosition..]) = value.caseStorage.locateBroker;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref modifyOrderOptionalValueWriterCaseRecord))
        {
            int recorded = modifyOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : modifyOrderOptionalValueWriterCase;
            if (modifyOrderOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && modifyOrderOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            modifyOrderOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
