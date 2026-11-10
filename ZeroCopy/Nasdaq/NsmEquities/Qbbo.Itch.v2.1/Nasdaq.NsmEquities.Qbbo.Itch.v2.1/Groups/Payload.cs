using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1;

/// <summary>
///  Branch
/// </summary>
public static class Payload
{
    /// <summary>
    ///  Identifies the case of the Payload that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Payload.
        /// </summary>
        None = 0,
        /// <summary>
        ///  The System Event message is used to signal key market or data feed control events.
        /// </summary>
        SystemEventMessage = 1,
        /// <summary>
        ///  At the start of each trading day, Nasdaq disseminates stock directory messages for all active symbols in its market center system.
        /// </summary>
        StockDirectoryMessage = 2,
        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        StockTradingActionMessage = 3,
        /// <summary>
        ///  Nasdaq introduced the Reg SHO Short Sale Price Test Restricted Indicator message format.
        /// </summary>
        RegShoRestrictionMessage = 4,
        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        MwcbDeclineLevelMessage = 5,
        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        MwcbBreachMessage = 6,
        /// <summary>
        ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
        /// </summary>
        OperationalHaltMessage = 7,
        /// <summary>
        ///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
        /// </summary>
        BboQuotationMessage = 8,
        /// <summary>
        ///  Indicates a retail interest indication of the Bid, Ask or both the Bid and Ask for Nasdaq-listed securities.
        /// </summary>
        PriceImprovementMessage = 9,
        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        IpoQuotingPeriodUpdateMessage = 10,
    }

    /// <summary>
    ///  Reads the Payload in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> messageTypeValue;
        private readonly global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case)Parser.Valid(Parser.SelectPayload(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Payload.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  The System Event message is used to signal key market or data feed control events.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemEventMessage.Layout SystemEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq disseminates stock directory messages for all active symbols in its market center system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockDirectoryMessage.Layout StockDirectoryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockDirectoryMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockTradingActionMessage.Layout StockTradingActionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockTradingActionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq introduced the Reg SHO Short Sale Price Test Restricted Indicator message format.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RegShoRestrictionMessage.Layout RegShoRestrictionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RegShoRestrictionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MwcbDeclineLevelMessage.Layout MwcbDeclineLevelMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbDeclineLevelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MwcbBreachMessage.Layout MwcbBreachMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbBreachMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OperationalHaltMessage.Layout OperationalHaltMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OperationalHaltMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly BboQuotationMessage.Layout BboQuotationMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BboQuotationMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates a retail interest indication of the Bid, Ask or both the Bid and Ask for Nasdaq-listed securities.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PriceImprovementMessage.Layout PriceImprovementMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PriceImprovementMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly IpoQuotingPeriodUpdateMessage.Layout IpoQuotingPeriodUpdateMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<IpoQuotingPeriodUpdateMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Payload fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Value stockDirectoryMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Value stockTradingActionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Value regShoRestrictionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Value mwcbDeclineLevelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Value mwcbBreachMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Value operationalHaltMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Value bboQuotationMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Value priceImprovementMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Value ipoQuotingPeriodUpdateMessage;
        }

        /// <summary>
        ///  The System Event message is used to signal key market or data feed control events.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq disseminates stock directory messages for all active symbols in its market center system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Value StockDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.stockDirectoryMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage;
            }
        }

        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Value StockTradingActionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.stockTradingActionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage;
            }
        }

        /// <summary>
        ///  Nasdaq introduced the Reg SHO Short Sale Price Test Restricted Indicator message format.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Value RegShoRestrictionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.regShoRestrictionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Value MwcbDeclineLevelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.mwcbDeclineLevelMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Value MwcbBreachMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.mwcbBreachMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage;
            }
        }

        /// <summary>
        ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Value OperationalHaltMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.operationalHaltMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage;
            }
        }

        /// <summary>
        ///  BBO products will broadcast a real-time update every time that the exchange's best bid and offer quote is updated during the trading day.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Value BboQuotationMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.bboQuotationMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage;
            }
        }

        /// <summary>
        ///  Indicates a retail interest indication of the Bid, Ask or both the Bid and Ask for Nasdaq-listed securities.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Value PriceImprovementMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.priceImprovementMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage;
            }
        }

        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Value IpoQuotingPeriodUpdateMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
                this.caseStorage.ipoQuotingPeriodUpdateMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Stock Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockDirectoryMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.StockDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Stock Trading Action Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockTradingActionMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.StockTradingActionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Reg Sho Restriction Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RegShoRestrictionMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.RegShoRestrictionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Decline Level Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbDeclineLevelMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.MwcbDeclineLevelMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Breach Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbBreachMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.MwcbBreachMessage];

        /// <summary>
        ///  The value in the bytes that selects the Operational Halt Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OperationalHaltMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.OperationalHaltMessage];

        /// <summary>
        ///  The value in the bytes that selects the Bbo Quotation Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> BboQuotationMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.BboQuotationMessage];

        /// <summary>
        ///  The value in the bytes that selects the Price Improvement Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> PriceImprovementMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.PriceImprovementMessage];

        /// <summary>
        ///  The value in the bytes that selects the Ipo Quoting Period Update Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> IpoQuotingPeriodUpdateMessageMessageType => [(byte)global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MessageType.IpoQuotingPeriodUpdateMessage];

        /// <summary>
        ///  Creates the value of the Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int messageLengthDependencyWriteDistanceFromPayload = -3;
    private const int SystemEventMessageCase = 1;
    private const int StockDirectoryMessageCase = 2;
    private const int StockTradingActionMessageCase = 3;
    private const int RegShoRestrictionMessageCase = 4;
    private const int MwcbDeclineLevelMessageCase = 5;
    private const int MwcbBreachMessageCase = 6;
    private const int OperationalHaltMessageCase = 7;
    private const int BboQuotationMessageCase = 8;
    private const int PriceImprovementMessageCase = 9;
    private const int IpoQuotingPeriodUpdateMessageCase = 10;

    /// <summary>
    ///  Writes the Payload to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Payload.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterCursor cursor, int start, scoped ref bool messageLengthDependencyWriteUsed, scoped ref int payloadWriterCaseRecord, scoped in Value value)
    {
        int payloadWriterCase = global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage or global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage)
        {
            payloadWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage)
        {
            payloadWriterCase = StockDirectoryMessageCase;
            int stockDirectoryMessageWriting = cursor.Index;
            var stockDirectoryMessageWriteValue = value.caseStorage.stockDirectoryMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockDirectoryMessage.Write(bytes, ref cursor, stockDirectoryMessageWriting, in stockDirectoryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage)
        {
            payloadWriterCase = StockTradingActionMessageCase;
            int stockTradingActionMessageWriting = cursor.Index;
            var stockTradingActionMessageWriteValue = value.caseStorage.stockTradingActionMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.StockTradingActionMessage.Write(bytes, ref cursor, stockTradingActionMessageWriting, in stockTradingActionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage)
        {
            payloadWriterCase = RegShoRestrictionMessageCase;
            int regShoRestrictionMessageWriting = cursor.Index;
            var regShoRestrictionMessageWriteValue = value.caseStorage.regShoRestrictionMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.RegShoRestrictionMessage.Write(bytes, ref cursor, regShoRestrictionMessageWriting, in regShoRestrictionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage)
        {
            payloadWriterCase = MwcbDeclineLevelMessageCase;
            int mwcbDeclineLevelMessageWriting = cursor.Index;
            var mwcbDeclineLevelMessageWriteValue = value.caseStorage.mwcbDeclineLevelMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbDeclineLevelMessage.Write(bytes, ref cursor, mwcbDeclineLevelMessageWriting, in mwcbDeclineLevelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage)
        {
            payloadWriterCase = MwcbBreachMessageCase;
            int mwcbBreachMessageWriting = cursor.Index;
            var mwcbBreachMessageWriteValue = value.caseStorage.mwcbBreachMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.MwcbBreachMessage.Write(bytes, ref cursor, mwcbBreachMessageWriting, in mwcbBreachMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage)
        {
            payloadWriterCase = OperationalHaltMessageCase;
            int operationalHaltMessageWriting = cursor.Index;
            var operationalHaltMessageWriteValue = value.caseStorage.operationalHaltMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.OperationalHaltMessage.Write(bytes, ref cursor, operationalHaltMessageWriting, in operationalHaltMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage)
        {
            payloadWriterCase = BboQuotationMessageCase;
            int bboQuotationMessageWriting = cursor.Index;
            var bboQuotationMessageWriteValue = value.caseStorage.bboQuotationMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.BboQuotationMessage.Write(bytes, ref cursor, bboQuotationMessageWriting, in bboQuotationMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage)
        {
            payloadWriterCase = PriceImprovementMessageCase;
            int priceImprovementMessageWriting = cursor.Index;
            var priceImprovementMessageWriteValue = value.caseStorage.priceImprovementMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PriceImprovementMessage.Write(bytes, ref cursor, priceImprovementMessageWriting, in priceImprovementMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage)
        {
            payloadWriterCase = IpoQuotingPeriodUpdateMessageCase;
            int ipoQuotingPeriodUpdateMessageWriting = cursor.Index;
            var ipoQuotingPeriodUpdateMessageWriteValue = value.caseStorage.ipoQuotingPeriodUpdateMessage;
            global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.IpoQuotingPeriodUpdateMessage.Write(bytes, ref cursor, ipoQuotingPeriodUpdateMessageWriting, in ipoQuotingPeriodUpdateMessageWriteValue);
        }
        global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.PayloadWriter.PayloadSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent : start + messageLengthDependencyWriteDistanceFromPayload, ref messageLengthDependencyWriteUsed, start, cursor.Index);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadWriterCaseRecord))
        {
            int recorded = payloadWriterCase == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.NoCase : payloadWriterCase;
            if (payloadWriterCaseRecord != global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.Position.Absent && payloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Parser.WriterValue);
            payloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
