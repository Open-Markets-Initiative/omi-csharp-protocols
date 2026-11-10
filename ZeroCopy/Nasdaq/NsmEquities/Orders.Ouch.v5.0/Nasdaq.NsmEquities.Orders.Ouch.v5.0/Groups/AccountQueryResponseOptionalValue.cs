using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class AccountQueryResponseOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Account Query Response Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Account Query Response Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 1,
    }

    /// <summary>
    ///  Reads the Account Query Response Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte accountQueryResponseOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte accountQueryResponseOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.accountQueryResponseOptionalFieldValue = accountQueryResponseOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case)Parser.Valid(Parser.SelectAccountQueryResponseOptionalValue(this.bytes, this.start, this.end, this.start, this.accountQueryResponseOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Account Query Response Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Account Query Response Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureAccountQueryResponseOptionalValue(this.bytes, this.start, this.end, this.start, this.accountQueryResponseOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Account Query Response Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxAccountQueryResponseOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalField.UserRefIdx;

        /// <summary>
        ///  Creates the value of the Account Query Response Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int AccountQueryResponseOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Account Query Response Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Account Query Response Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int accountQueryResponseOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int accountQueryResponseOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            accountQueryResponseOptionalValueWriterCase = AccountQueryResponseOptionalValueUserRefIdxCase;
            int accountQueryResponseOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[accountQueryResponseOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref accountQueryResponseOptionalValueWriterCaseRecord))
        {
            int recorded = accountQueryResponseOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : accountQueryResponseOptionalValueWriterCase;
            if (accountQueryResponseOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && accountQueryResponseOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            accountQueryResponseOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
