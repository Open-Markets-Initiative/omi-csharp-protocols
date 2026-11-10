using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class MassCancelRequestOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Mass Cancel Request Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Mass Cancel Request Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Side
        /// </summary>
        Side = 1,
        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        GroupId = 2,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 3,
    }

    /// <summary>
    ///  Reads the Mass Cancel Request Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte massCancelRequestOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte massCancelRequestOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.massCancelRequestOptionalFieldValue = massCancelRequestOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case)Parser.Valid(Parser.SelectMassCancelRequestOptionalValue(this.bytes, this.start, this.end, this.start, this.massCancelRequestOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Mass Cancel Request Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Mass Cancel Request Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMassCancelRequestOptionalValue(this.bytes, this.start, this.end, this.start, this.massCancelRequestOptionalFieldValue))];

        /// <summary>
        ///  Side
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SideField Side
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SideField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly GroupId GroupId
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<GroupId>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Mass Cancel Request Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField side;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId groupId;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
        }

        /// <summary>
        ///  Side
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.side = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side;
            }
        }

        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId GroupId
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.groupId = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Side.
        /// </summary>
        public const byte SideMassCancelRequestOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalField.Side;

        /// <summary>
        ///  The value in the bytes that selects the Group Id.
        /// </summary>
        public const byte GroupIdMassCancelRequestOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalField.GroupId;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxMassCancelRequestOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalField.UserRefIdx;

        /// <summary>
        ///  Creates the value of the Mass Cancel Request Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int MassCancelRequestOptionalValueSideCase = 1;
    private const int MassCancelRequestOptionalValueGroupIdCase = 2;
    private const int MassCancelRequestOptionalValueUserRefIdxCase = 3;

    /// <summary>
    ///  Writes the Mass Cancel Request Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Mass Cancel Request Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int massCancelRequestOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int massCancelRequestOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            massCancelRequestOptionalValueWriterCase = MassCancelRequestOptionalValueSideCase;
            int massCancelRequestOptionalValueSideWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField>(bytes[massCancelRequestOptionalValueSideWriterPosition..]) = value.caseStorage.side;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            massCancelRequestOptionalValueWriterCase = MassCancelRequestOptionalValueGroupIdCase;
            int massCancelRequestOptionalValueGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId>(bytes[massCancelRequestOptionalValueGroupIdWriterPosition..]) = value.caseStorage.groupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            massCancelRequestOptionalValueWriterCase = MassCancelRequestOptionalValueUserRefIdxCase;
            int massCancelRequestOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[massCancelRequestOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref massCancelRequestOptionalValueWriterCaseRecord))
        {
            int recorded = massCancelRequestOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : massCancelRequestOptionalValueWriterCase;
            if (massCancelRequestOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && massCancelRequestOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            massCancelRequestOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
