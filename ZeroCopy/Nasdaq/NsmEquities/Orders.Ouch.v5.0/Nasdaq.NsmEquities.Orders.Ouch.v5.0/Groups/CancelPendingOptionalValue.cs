using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class CancelPendingOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Cancel Pending Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Cancel Pending Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 1,
    }

    /// <summary>
    ///  Reads the Cancel Pending Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte cancelPendingOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte cancelPendingOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.cancelPendingOptionalFieldValue = cancelPendingOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case)Parser.Valid(Parser.SelectCancelPendingOptionalValue(this.bytes, this.start, this.end, this.start, this.cancelPendingOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Cancel Pending Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Cancel Pending Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureCancelPendingOptionalValue(this.bytes, this.start, this.end, this.start, this.cancelPendingOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Cancel Pending Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case @case;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxCancelPendingOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalField.UserRefIdx;

        /// <summary>
        ///  Creates the value of the Cancel Pending Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int CancelPendingOptionalValueUserRefIdxCase = 1;

    /// <summary>
    ///  Writes the Cancel Pending Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Cancel Pending Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int cancelPendingOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int cancelPendingOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            cancelPendingOptionalValueWriterCase = CancelPendingOptionalValueUserRefIdxCase;
            int cancelPendingOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[cancelPendingOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref cancelPendingOptionalValueWriterCaseRecord))
        {
            int recorded = cancelPendingOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : cancelPendingOptionalValueWriterCase;
            if (cancelPendingOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && cancelPendingOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            cancelPendingOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
