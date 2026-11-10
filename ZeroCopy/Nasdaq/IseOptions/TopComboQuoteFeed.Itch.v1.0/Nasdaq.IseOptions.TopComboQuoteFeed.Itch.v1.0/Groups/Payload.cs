using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0;

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
        ///  The system event message type is used to signal a market or data feed handler event.
        /// </summary>
        SystemEventMessage = 1,
        /// <summary>
        ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
        /// </summary>
        ComplexStrategyDirectoryMessage = 2,
        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        StrategyOpenClosedMessage = 3,
        /// <summary>
        ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
        /// </summary>
        StrategyTradingActionMessage = 4,
        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        StrategyBestBidAndAskUpdate = 5,
        /// <summary>
        ///  The options system will continuously calculate its best bid position for active strategies on the options market during the trading day. Whenever the best bid position changes on one side but not the other side, the options system will send its best bid update via this feed for the affected security.
        /// </summary>
        StrategyBestBidUpdate = 6,
        /// <summary>
        ///  The options system will continuously calculate its best offer position for active strategies on the options market during the trading day. Whenever the best ask position changes on one side but not the other side, the options system will send its best ask update via this feed for the affected security
        /// </summary>
        StrategyBestAskUpdate = 7,
        /// <summary>
        ///  The ticker message is used to send real time trade information
        /// </summary>
        ComplexStrategyTickerMessage = 8,
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
        private readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case)Parser.Valid(Parser.SelectPayload(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Payload.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemEventMessage.Layout SystemEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ComplexStrategyDirectoryMessage.Reader ComplexStrategyDirectoryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int complexStrategyDirectoryMessageReaderPosition = this.start;
                return new(this.bytes, complexStrategyDirectoryMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StrategyOpenClosedMessage.Layout StrategyOpenClosedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyOpenClosedMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StrategyTradingActionMessage.Layout StrategyTradingActionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyTradingActionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StrategyBestBidAndAskUpdate.Layout StrategyBestBidAndAskUpdate
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyBestBidAndAskUpdate.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The options system will continuously calculate its best bid position for active strategies on the options market during the trading day. Whenever the best bid position changes on one side but not the other side, the options system will send its best bid update via this feed for the affected security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StrategyBestBidUpdate.Layout StrategyBestBidUpdate
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyBestBidUpdate.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The options system will continuously calculate its best offer position for active strategies on the options market during the trading day. Whenever the best ask position changes on one side but not the other side, the options system will send its best ask update via this feed for the affected security
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StrategyBestAskUpdate.Layout StrategyBestAskUpdate
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyBestAskUpdate.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The ticker message is used to send real time trade information
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ComplexStrategyTickerMessage.Layout ComplexStrategyTickerMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ComplexStrategyTickerMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Payload fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case @case;
        internal readonly global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Value complexStrategyDirectoryMessage;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Value strategyOpenClosedMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Value strategyTradingActionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Value strategyBestBidAndAskUpdate;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Value strategyBestBidUpdate;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Value strategyBestAskUpdate;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Value complexStrategyTickerMessage;
        }

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Value ComplexStrategyDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.complexStrategyDirectoryMessage = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage;
            }
        }

        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Value StrategyOpenClosedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.strategyOpenClosedMessage = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage;
            }
        }

        /// <summary>
        ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Value StrategyTradingActionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.strategyTradingActionMessage = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage;
            }
        }

        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Value StrategyBestBidAndAskUpdate
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.strategyBestBidAndAskUpdate = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate;
            }
        }

        /// <summary>
        ///  The options system will continuously calculate its best bid position for active strategies on the options market during the trading day. Whenever the best bid position changes on one side but not the other side, the options system will send its best bid update via this feed for the affected security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Value StrategyBestBidUpdate
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.strategyBestBidUpdate = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate;
            }
        }

        /// <summary>
        ///  The options system will continuously calculate its best offer position for active strategies on the options market during the trading day. Whenever the best ask position changes on one side but not the other side, the options system will send its best ask update via this feed for the affected security
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Value StrategyBestAskUpdate
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.strategyBestAskUpdate = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate;
            }
        }

        /// <summary>
        ///  The ticker message is used to send real time trade information
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Value ComplexStrategyTickerMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterTwoCases, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
                this.caseStorage.complexStrategyTickerMessage = value;
                this.@case = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Complex Strategy Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ComplexStrategyDirectoryMessageMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.ComplexStrategyDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Open Closed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyOpenClosedMessageMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.StrategyOpenClosedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Trading Action Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyTradingActionMessageMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.StrategyTradingActionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Best Bid And Ask Update.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyBestBidAndAskUpdateMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.StrategyBestBidAndAskUpdate];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Best Bid Update.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyBestBidUpdateMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.StrategyBestBidUpdate];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Best Ask Update.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyBestAskUpdateMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.StrategyBestAskUpdate];

        /// <summary>
        ///  The value in the bytes that selects the Complex Strategy Ticker Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ComplexStrategyTickerMessageMessageType => [(byte)global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.MessageType.ComplexStrategyTickerMessage];

        /// <summary>
        ///  Creates the value of the Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int messageLengthDependencyWriteDistanceFromPayload = -3;
    private const int SystemEventMessageCase = 1;
    private const int ComplexStrategyDirectoryMessageCase = 2;
    private const int StrategyOpenClosedMessageCase = 3;
    private const int StrategyTradingActionMessageCase = 4;
    private const int StrategyBestBidAndAskUpdateCase = 5;
    private const int StrategyBestBidUpdateCase = 6;
    private const int StrategyBestAskUpdateCase = 7;
    private const int ComplexStrategyTickerMessageCase = 8;

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
        var cursor = new global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterCursor cursor, int start, scoped ref bool messageLengthDependencyWriteUsed, scoped ref int payloadWriterCaseRecord, scoped in Value value)
    {
        int payloadWriterCase = global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate or global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage)) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage)
        {
            payloadWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage)
        {
            payloadWriterCase = ComplexStrategyDirectoryMessageCase;
            int complexStrategyDirectoryMessageWriting = cursor.Index;
            var complexStrategyDirectoryMessageWriteValue = value.complexStrategyDirectoryMessage;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Write(bytes, ref cursor, complexStrategyDirectoryMessageWriting, in complexStrategyDirectoryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage)
        {
            payloadWriterCase = StrategyOpenClosedMessageCase;
            int strategyOpenClosedMessageWriting = cursor.Index;
            var strategyOpenClosedMessageWriteValue = value.caseStorage.strategyOpenClosedMessage;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyOpenClosedMessage.Write(bytes, ref cursor, strategyOpenClosedMessageWriting, in strategyOpenClosedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage)
        {
            payloadWriterCase = StrategyTradingActionMessageCase;
            int strategyTradingActionMessageWriting = cursor.Index;
            var strategyTradingActionMessageWriteValue = value.caseStorage.strategyTradingActionMessage;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyTradingActionMessage.Write(bytes, ref cursor, strategyTradingActionMessageWriting, in strategyTradingActionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate)
        {
            payloadWriterCase = StrategyBestBidAndAskUpdateCase;
            int strategyBestBidAndAskUpdateWriting = cursor.Index;
            var strategyBestBidAndAskUpdateWriteValue = value.caseStorage.strategyBestBidAndAskUpdate;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidAndAskUpdate.Write(bytes, ref cursor, strategyBestBidAndAskUpdateWriting, in strategyBestBidAndAskUpdateWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate)
        {
            payloadWriterCase = StrategyBestBidUpdateCase;
            int strategyBestBidUpdateWriting = cursor.Index;
            var strategyBestBidUpdateWriteValue = value.caseStorage.strategyBestBidUpdate;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestBidUpdate.Write(bytes, ref cursor, strategyBestBidUpdateWriting, in strategyBestBidUpdateWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate)
        {
            payloadWriterCase = StrategyBestAskUpdateCase;
            int strategyBestAskUpdateWriting = cursor.Index;
            var strategyBestAskUpdateWriteValue = value.caseStorage.strategyBestAskUpdate;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.StrategyBestAskUpdate.Write(bytes, ref cursor, strategyBestAskUpdateWriting, in strategyBestAskUpdateWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage)
        {
            payloadWriterCase = ComplexStrategyTickerMessageCase;
            int complexStrategyTickerMessageWriting = cursor.Index;
            var complexStrategyTickerMessageWriteValue = value.caseStorage.complexStrategyTickerMessage;
            global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyTickerMessage.Write(bytes, ref cursor, complexStrategyTickerMessageWriting, in complexStrategyTickerMessageWriteValue);
        }
        global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.PayloadWriter.PayloadSizeRule0(bytes, ref cursor, start < global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Minimum ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent : start + messageLengthDependencyWriteDistanceFromPayload, ref messageLengthDependencyWriteUsed, start, cursor.Index);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadWriterCaseRecord))
        {
            int recorded = payloadWriterCase == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent ? global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.NoCase : payloadWriterCase;
            if (payloadWriterCaseRecord != global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.Position.Absent && payloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterRuleDisagrees, global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Parser.WriterValue);
            payloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
