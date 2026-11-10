using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

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
        ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
        /// </summary>
        StrategyTradingActionMessage = 3,
        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        StrategyOpenClosedMessage = 4,
        /// <summary>
        ///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
        /// </summary>
        ComplexStrategyOrderOnBookMessage = 5,
        /// <summary>
        ///  This message is used to announce strategy auctions. The start of auction is announced, followed by possible updates on the auction, and announcing the end of the auction.
        /// </summary>
        ComplexStrategyAuctionMessage = 6,
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
        private readonly global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case)Parser.Valid(Parser.SelectPayload(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case Case => this.selectedCase;

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
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int complexStrategyDirectoryMessageReaderPosition = this.start;
                return new(this.bytes, complexStrategyDirectoryMessageReaderPosition, this.end);
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
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyTradingActionMessage.Layout>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StrategyOpenClosedMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ComplexStrategyOrderOnBookMessage.Layout ComplexStrategyOrderOnBookMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ComplexStrategyOrderOnBookMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is used to announce strategy auctions. The start of auction is announced, followed by possible updates on the auction, and announcing the end of the auction.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ComplexStrategyAuctionMessage.Reader ComplexStrategyAuctionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int complexStrategyAuctionMessageReaderPosition = this.start;
                return new(this.bytes, complexStrategyAuctionMessageReaderPosition, this.end);
            }
        }
    }

    /// <summary>
    ///  The Payload fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case @case;
        internal readonly global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Value complexStrategyDirectoryMessage;
        internal readonly global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Value complexStrategyAuctionMessage;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Value strategyTradingActionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Value strategyOpenClosedMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Value complexStrategyOrderOnBookMessage;
        }

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Value ComplexStrategyDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.complexStrategyDirectoryMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage;
            }
        }

        /// <summary>
        ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Value StrategyTradingActionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.caseStorage.strategyTradingActionMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage;
            }
        }

        /// <summary>
        ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Value StrategyOpenClosedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.caseStorage.strategyOpenClosedMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage;
            }
        }

        /// <summary>
        ///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Value ComplexStrategyOrderOnBookMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.caseStorage.complexStrategyOrderOnBookMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage;
            }
        }

        /// <summary>
        ///  This message is used to announce strategy auctions. The start of auction is announced, followed by possible updates on the auction, and announcing the end of the auction.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Value ComplexStrategyAuctionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterTwoCases, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
                this.complexStrategyAuctionMessage = value;
                this.@case = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Complex Strategy Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ComplexStrategyDirectoryMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.ComplexStrategyDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Trading Action Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyTradingActionMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.StrategyTradingActionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Strategy Open Closed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StrategyOpenClosedMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.StrategyOpenClosedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Complex Strategy Order On Book Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ComplexStrategyOrderOnBookMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.ComplexStrategyOrderOnBookMessage];

        /// <summary>
        ///  The value in the bytes that selects the Complex Strategy Auction Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ComplexStrategyAuctionMessageMessageType => [(byte)global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.MessageType.ComplexStrategyAuctionMessage];

        /// <summary>
        ///  Creates the value of the Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int messageLengthDependencyWriteDistanceFromPayload = -3;
    private const int SystemEventMessageCase = 1;
    private const int ComplexStrategyDirectoryMessageCase = 2;
    private const int StrategyTradingActionMessageCase = 3;
    private const int StrategyOpenClosedMessageCase = 4;
    private const int ComplexStrategyOrderOnBookMessageCase = 5;
    private const int ComplexStrategyAuctionMessageCase = 6;

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
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped ref bool messageLengthDependencyWriteUsed, scoped ref int payloadWriterCaseRecord, scoped in Value value)
    {
        int payloadWriterCase = global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage or global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage or global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage or global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage or global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage or global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage)) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage)
        {
            payloadWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage)
        {
            payloadWriterCase = ComplexStrategyDirectoryMessageCase;
            int complexStrategyDirectoryMessageWriting = cursor.Index;
            var complexStrategyDirectoryMessageWriteValue = value.complexStrategyDirectoryMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Write(bytes, ref cursor, complexStrategyDirectoryMessageWriting, in complexStrategyDirectoryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage)
        {
            payloadWriterCase = StrategyTradingActionMessageCase;
            int strategyTradingActionMessageWriting = cursor.Index;
            var strategyTradingActionMessageWriteValue = value.caseStorage.strategyTradingActionMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyTradingActionMessage.Write(bytes, ref cursor, strategyTradingActionMessageWriting, in strategyTradingActionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage)
        {
            payloadWriterCase = StrategyOpenClosedMessageCase;
            int strategyOpenClosedMessageWriting = cursor.Index;
            var strategyOpenClosedMessageWriteValue = value.caseStorage.strategyOpenClosedMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.StrategyOpenClosedMessage.Write(bytes, ref cursor, strategyOpenClosedMessageWriting, in strategyOpenClosedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage)
        {
            payloadWriterCase = ComplexStrategyOrderOnBookMessageCase;
            int complexStrategyOrderOnBookMessageWriting = cursor.Index;
            var complexStrategyOrderOnBookMessageWriteValue = value.caseStorage.complexStrategyOrderOnBookMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyOrderOnBookMessage.Write(bytes, ref cursor, complexStrategyOrderOnBookMessageWriting, in complexStrategyOrderOnBookMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage)
        {
            payloadWriterCase = ComplexStrategyAuctionMessageCase;
            int complexStrategyAuctionMessageWriting = cursor.Index;
            var complexStrategyAuctionMessageWriteValue = value.complexStrategyAuctionMessage;
            global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Write(bytes, ref cursor, complexStrategyAuctionMessageWriting, in complexStrategyAuctionMessageWriteValue);
        }
        global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.PayloadWriter.PayloadSizeRule0(bytes, ref cursor, start < global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum ? global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent : start + messageLengthDependencyWriteDistanceFromPayload, ref messageLengthDependencyWriteUsed, start, cursor.Index);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadWriterCaseRecord))
        {
            int recorded = payloadWriterCase == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent ? global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.NoCase : payloadWriterCase;
            if (payloadWriterCaseRecord != global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Absent && payloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterRuleDisagrees, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterValue);
            payloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
