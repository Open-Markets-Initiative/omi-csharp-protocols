using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NtxEquities.TotalView.Itch.v5_0;

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
        ///  At the start of each trading day, Nasdaq Texas disseminates stock directory messages for all active symbols in the Nasdaq Texas execution system.
        /// </summary>
        StockDirectoryMessage = 2,
        /// <summary>
        ///  Nasdaq Texas uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        StockTradingActionMessage = 3,
        /// <summary>
        ///  In February 2011, the Securities and Exchange Commission (SEC) implemented changes to Rule 201 of the Regulation SHO (Reg SHO).
        /// </summary>
        RegShoShortSalePriceTestRestrictedIndicatorMessage = 4,
        /// <summary>
        ///  At the start of each trading day, Nasdaq Texas disseminates a spin of market participant position messages.
        /// </summary>
        MarketParticipantPositionMessage = 5,
        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        MwcbDeclineLevelMessage = 6,
        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        MwcbStatusMessage = 7,
        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD Trading Pause
        /// </summary>
        LuldAuctionCollarMessage = 8,
        /// <summary>
        ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
        /// </summary>
        OperationalHaltMessage = 9,
        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq Texas system.
        /// </summary>
        AddOrderNoMpidAttributionMessage = 10,
        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq Texas system.
        /// </summary>
        AddOrderMpidAttributionMessage = 11,
        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part.
        /// </summary>
        OrderExecutedMessage = 12,
        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price.
        /// </summary>
        OrderExecutedWithPriceMessage = 13,
        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        OrderCancelMessage = 14,
        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled. All remaining shares are no longer accessible so the order must be removed from the book.
        /// </summary>
        OrderDeleteMessage = 15,
        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced.
        /// </summary>
        OrderReplaceMessage = 16,
        /// <summary>
        ///  The Trade Message is designed to provide execution details for normal match events involving non-displayable order types.
        /// </summary>
        NonCrossTradeMessage = 17,
        /// <summary>
        ///  Cross Trade message indicates that Nasdaq Texas has completed its cross process for a specific security.
        /// </summary>
        CrossTradeMessage = 18,
        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq Texas is broken.
        /// </summary>
        BrokenTradeMessage = 19,
        /// <summary>
        ///  Nasdaq Texas disseminates NOII data at 5-second intervals in the minutes leading up to the Nasdaq Opening Cross and Nasdaq Closing Cross.
        /// </summary>
        NetOrderImbalanceIndicatorMessage = 20,
        /// <summary>
        ///  Indicates price improving interest of the Bid, Ask or both the Bid and Ask for a security.
        /// </summary>
        RetailInterestMessage = 21,
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
        private readonly global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case)Parser.Valid(Parser.SelectPayload(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Payload that the bytes select.
        /// </summary>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case Case => this.selectedCase;

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
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq Texas disseminates stock directory messages for all active symbols in the Nasdaq Texas execution system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockDirectoryMessage.Layout StockDirectoryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockDirectoryMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq Texas uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockTradingActionMessage.Layout StockTradingActionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockTradingActionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  In February 2011, the Securities and Exchange Commission (SEC) implemented changes to Rule 201 of the Regulation SHO (Reg SHO).
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout RegShoShortSalePriceTestRestrictedIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq Texas disseminates a spin of market participant position messages.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MarketParticipantPositionMessage.Layout MarketParticipantPositionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MarketParticipantPositionMessage.Layout>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbDeclineLevelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MwcbStatusMessage.Layout MwcbStatusMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbStatusMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD Trading Pause
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LuldAuctionCollarMessage.Layout LuldAuctionCollarMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LuldAuctionCollarMessage.Layout>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OperationalHaltMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq Texas system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AddOrderNoMpidAttributionMessage.Layout AddOrderNoMpidAttributionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AddOrderNoMpidAttributionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq Texas system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AddOrderMpidAttributionMessage.Layout AddOrderMpidAttributionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AddOrderMpidAttributionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderExecutedMessage.Layout OrderExecutedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderExecutedMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderExecutedWithPriceMessage.Layout OrderExecutedWithPriceMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderExecutedWithPriceMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderCancelMessage.Layout OrderCancelMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderCancelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled. All remaining shares are no longer accessible so the order must be removed from the book.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderDeleteMessage.Layout OrderDeleteMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderDeleteMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderReplaceMessage.Layout OrderReplaceMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderReplaceMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Trade Message is designed to provide execution details for normal match events involving non-displayable order types.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly NonCrossTradeMessage.Layout NonCrossTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NonCrossTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Cross Trade message indicates that Nasdaq Texas has completed its cross process for a specific security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly CrossTradeMessage.Layout CrossTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<CrossTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq Texas is broken.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly BrokenTradeMessage.Layout BrokenTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BrokenTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq Texas disseminates NOII data at 5-second intervals in the minutes leading up to the Nasdaq Opening Cross and Nasdaq Closing Cross.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly NetOrderImbalanceIndicatorMessage.Layout NetOrderImbalanceIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NetOrderImbalanceIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates price improving interest of the Bid, Ask or both the Bid and Ask for a security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RetailInterestMessage.Layout RetailInterestMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RetailInterestMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Payload fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Value stockDirectoryMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Value stockTradingActionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Value regShoShortSalePriceTestRestrictedIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Value marketParticipantPositionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Value mwcbDeclineLevelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Value mwcbStatusMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Value luldAuctionCollarMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Value operationalHaltMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Value addOrderNoMpidAttributionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Value addOrderMpidAttributionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Value orderExecutedMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Value orderExecutedWithPriceMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Value orderCancelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Value orderDeleteMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Value orderReplaceMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Value nonCrossTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Value crossTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Value brokenTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Value netOrderImbalanceIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Value retailInterestMessage;
        }

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq Texas disseminates stock directory messages for all active symbols in the Nasdaq Texas execution system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Value StockDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.stockDirectoryMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage;
            }
        }

        /// <summary>
        ///  Nasdaq Texas uses this administrative message to indicate the current trading status of a security to the trading community.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Value StockTradingActionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.stockTradingActionMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage;
            }
        }

        /// <summary>
        ///  In February 2011, the Securities and Exchange Commission (SEC) implemented changes to Rule 201 of the Regulation SHO (Reg SHO).
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Value RegShoShortSalePriceTestRestrictedIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.regShoShortSalePriceTestRestrictedIndicatorMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage;
            }
        }

        /// <summary>
        ///  At the start of each trading day, Nasdaq Texas disseminates a spin of market participant position messages.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Value MarketParticipantPositionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.marketParticipantPositionMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Value MwcbDeclineLevelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.mwcbDeclineLevelMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Value MwcbStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.mwcbStatusMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage;
            }
        }

        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD Trading Pause
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Value LuldAuctionCollarMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.luldAuctionCollarMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage;
            }
        }

        /// <summary>
        ///  The Exchange uses this message to indicate the current Operational Status of a security to the trading community.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Value OperationalHaltMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.operationalHaltMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage;
            }
        }

        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq Texas system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Value AddOrderNoMpidAttributionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.addOrderNoMpidAttributionMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage;
            }
        }

        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq Texas system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Value AddOrderMpidAttributionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.addOrderMpidAttributionMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Value OrderExecutedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.orderExecutedMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Value OrderExecutedWithPriceMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.orderExecutedWithPriceMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Value OrderCancelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.orderCancelMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled. All remaining shares are no longer accessible so the order must be removed from the book.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Value OrderDeleteMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.orderDeleteMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Value OrderReplaceMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.orderReplaceMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage;
            }
        }

        /// <summary>
        ///  The Trade Message is designed to provide execution details for normal match events involving non-displayable order types.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Value NonCrossTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.nonCrossTradeMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage;
            }
        }

        /// <summary>
        ///  Cross Trade message indicates that Nasdaq Texas has completed its cross process for a specific security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Value CrossTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.crossTradeMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage;
            }
        }

        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq Texas is broken.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Value BrokenTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.brokenTradeMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage;
            }
        }

        /// <summary>
        ///  Nasdaq Texas disseminates NOII data at 5-second intervals in the minutes leading up to the Nasdaq Opening Cross and Nasdaq Closing Cross.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Value NetOrderImbalanceIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.netOrderImbalanceIndicatorMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage;
            }
        }

        /// <summary>
        ///  Indicates price improving interest of the Bid, Ask or both the Bid and Ask for a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Value RetailInterestMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
                this.caseStorage.retailInterestMessage = value;
                this.@case = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Stock Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockDirectoryMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.StockDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Stock Trading Action Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockTradingActionMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.StockTradingActionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Reg Sho Short Sale Price Test Restricted Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RegShoShortSalePriceTestRestrictedIndicatorMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.RegShoShortSalePriceTestRestrictedIndicatorMessage];

        /// <summary>
        ///  The value in the bytes that selects the Market Participant Position Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MarketParticipantPositionMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.MarketParticipantPositionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Decline Level Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbDeclineLevelMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.MwcbDeclineLevelMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbStatusMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.MwcbStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Luld Auction Collar Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LuldAuctionCollarMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.LuldAuctionCollarMessage];

        /// <summary>
        ///  The value in the bytes that selects the Operational Halt Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OperationalHaltMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OperationalHaltMessage];

        /// <summary>
        ///  The value in the bytes that selects the Add Order No Mpid Attribution Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AddOrderNoMpidAttributionMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.AddOrderNoMpidAttributionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Add Order Mpid Attribution Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AddOrderMpidAttributionMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.AddOrderMpidAttributionMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Executed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OrderExecutedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Executed With Price Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedWithPriceMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OrderExecutedWithPriceMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Cancel Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderCancelMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OrderCancelMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Delete Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderDeleteMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OrderDeleteMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Replace Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderReplaceMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.OrderReplaceMessage];

        /// <summary>
        ///  The value in the bytes that selects the Non Cross Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> NonCrossTradeMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.NonCrossTradeMessage];

        /// <summary>
        ///  The value in the bytes that selects the Cross Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CrossTradeMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.CrossTradeMessage];

        /// <summary>
        ///  The value in the bytes that selects the Broken Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> BrokenTradeMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.BrokenTradeMessage];

        /// <summary>
        ///  The value in the bytes that selects the Net Order Imbalance Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> NetOrderImbalanceIndicatorMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.NetOrderImbalanceIndicatorMessage];

        /// <summary>
        ///  The value in the bytes that selects the Retail Interest Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RetailInterestMessageMessageType => [(byte)global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MessageType.RetailInterestMessage];

        /// <summary>
        ///  Creates the value of the Payload, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int messageLengthDependencyWriteDistanceFromPayload = -3;
    private const int SystemEventMessageCase = 1;
    private const int StockDirectoryMessageCase = 2;
    private const int StockTradingActionMessageCase = 3;
    private const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
    private const int MarketParticipantPositionMessageCase = 5;
    private const int MwcbDeclineLevelMessageCase = 6;
    private const int MwcbStatusMessageCase = 7;
    private const int LuldAuctionCollarMessageCase = 8;
    private const int OperationalHaltMessageCase = 9;
    private const int AddOrderNoMpidAttributionMessageCase = 10;
    private const int AddOrderMpidAttributionMessageCase = 11;
    private const int OrderExecutedMessageCase = 12;
    private const int OrderExecutedWithPriceMessageCase = 13;
    private const int OrderCancelMessageCase = 14;
    private const int OrderDeleteMessageCase = 15;
    private const int OrderReplaceMessageCase = 16;
    private const int NonCrossTradeMessageCase = 17;
    private const int CrossTradeMessageCase = 18;
    private const int BrokenTradeMessageCase = 19;
    private const int NetOrderImbalanceIndicatorMessageCase = 20;
    private const int RetailInterestMessageCase = 21;

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
        var cursor = new global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterCursor cursor, int start, scoped ref bool messageLengthDependencyWriteUsed, scoped ref int payloadWriterCaseRecord, scoped in Value value)
    {
        int payloadWriterCase = global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage or global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage)) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage)
        {
            payloadWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage)
        {
            payloadWriterCase = StockDirectoryMessageCase;
            int stockDirectoryMessageWriting = cursor.Index;
            var stockDirectoryMessageWriteValue = value.caseStorage.stockDirectoryMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockDirectoryMessage.Write(bytes, ref cursor, stockDirectoryMessageWriting, in stockDirectoryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage)
        {
            payloadWriterCase = StockTradingActionMessageCase;
            int stockTradingActionMessageWriting = cursor.Index;
            var stockTradingActionMessageWriteValue = value.caseStorage.stockTradingActionMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.StockTradingActionMessage.Write(bytes, ref cursor, stockTradingActionMessageWriting, in stockTradingActionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            payloadWriterCase = RegShoShortSalePriceTestRestrictedIndicatorMessageCase;
            int regShoShortSalePriceTestRestrictedIndicatorMessageWriting = cursor.Index;
            var regShoShortSalePriceTestRestrictedIndicatorMessageWriteValue = value.caseStorage.regShoShortSalePriceTestRestrictedIndicatorMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RegShoShortSalePriceTestRestrictedIndicatorMessage.Write(bytes, ref cursor, regShoShortSalePriceTestRestrictedIndicatorMessageWriting, in regShoShortSalePriceTestRestrictedIndicatorMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage)
        {
            payloadWriterCase = MarketParticipantPositionMessageCase;
            int marketParticipantPositionMessageWriting = cursor.Index;
            var marketParticipantPositionMessageWriteValue = value.caseStorage.marketParticipantPositionMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MarketParticipantPositionMessage.Write(bytes, ref cursor, marketParticipantPositionMessageWriting, in marketParticipantPositionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage)
        {
            payloadWriterCase = MwcbDeclineLevelMessageCase;
            int mwcbDeclineLevelMessageWriting = cursor.Index;
            var mwcbDeclineLevelMessageWriteValue = value.caseStorage.mwcbDeclineLevelMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbDeclineLevelMessage.Write(bytes, ref cursor, mwcbDeclineLevelMessageWriting, in mwcbDeclineLevelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusMessage)
        {
            payloadWriterCase = MwcbStatusMessageCase;
            int mwcbStatusMessageWriting = cursor.Index;
            var mwcbStatusMessageWriteValue = value.caseStorage.mwcbStatusMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.MwcbStatusMessage.Write(bytes, ref cursor, mwcbStatusMessageWriting, in mwcbStatusMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage)
        {
            payloadWriterCase = LuldAuctionCollarMessageCase;
            int luldAuctionCollarMessageWriting = cursor.Index;
            var luldAuctionCollarMessageWriteValue = value.caseStorage.luldAuctionCollarMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.LuldAuctionCollarMessage.Write(bytes, ref cursor, luldAuctionCollarMessageWriting, in luldAuctionCollarMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage)
        {
            payloadWriterCase = OperationalHaltMessageCase;
            int operationalHaltMessageWriting = cursor.Index;
            var operationalHaltMessageWriteValue = value.caseStorage.operationalHaltMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OperationalHaltMessage.Write(bytes, ref cursor, operationalHaltMessageWriting, in operationalHaltMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage)
        {
            payloadWriterCase = AddOrderNoMpidAttributionMessageCase;
            int addOrderNoMpidAttributionMessageWriting = cursor.Index;
            var addOrderNoMpidAttributionMessageWriteValue = value.caseStorage.addOrderNoMpidAttributionMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderNoMpidAttributionMessage.Write(bytes, ref cursor, addOrderNoMpidAttributionMessageWriting, in addOrderNoMpidAttributionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderMpidAttributionMessage)
        {
            payloadWriterCase = AddOrderMpidAttributionMessageCase;
            int addOrderMpidAttributionMessageWriting = cursor.Index;
            var addOrderMpidAttributionMessageWriteValue = value.caseStorage.addOrderMpidAttributionMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.AddOrderMpidAttributionMessage.Write(bytes, ref cursor, addOrderMpidAttributionMessageWriting, in addOrderMpidAttributionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage)
        {
            payloadWriterCase = OrderExecutedMessageCase;
            int orderExecutedMessageWriting = cursor.Index;
            var orderExecutedMessageWriteValue = value.caseStorage.orderExecutedMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedMessage.Write(bytes, ref cursor, orderExecutedMessageWriting, in orderExecutedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage)
        {
            payloadWriterCase = OrderExecutedWithPriceMessageCase;
            int orderExecutedWithPriceMessageWriting = cursor.Index;
            var orderExecutedWithPriceMessageWriteValue = value.caseStorage.orderExecutedWithPriceMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderExecutedWithPriceMessage.Write(bytes, ref cursor, orderExecutedWithPriceMessageWriting, in orderExecutedWithPriceMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage)
        {
            payloadWriterCase = OrderCancelMessageCase;
            int orderCancelMessageWriting = cursor.Index;
            var orderCancelMessageWriteValue = value.caseStorage.orderCancelMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderCancelMessage.Write(bytes, ref cursor, orderCancelMessageWriting, in orderCancelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage)
        {
            payloadWriterCase = OrderDeleteMessageCase;
            int orderDeleteMessageWriting = cursor.Index;
            var orderDeleteMessageWriteValue = value.caseStorage.orderDeleteMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderDeleteMessage.Write(bytes, ref cursor, orderDeleteMessageWriting, in orderDeleteMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage)
        {
            payloadWriterCase = OrderReplaceMessageCase;
            int orderReplaceMessageWriting = cursor.Index;
            var orderReplaceMessageWriteValue = value.caseStorage.orderReplaceMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.OrderReplaceMessage.Write(bytes, ref cursor, orderReplaceMessageWriting, in orderReplaceMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NonCrossTradeMessage)
        {
            payloadWriterCase = NonCrossTradeMessageCase;
            int nonCrossTradeMessageWriting = cursor.Index;
            var nonCrossTradeMessageWriteValue = value.caseStorage.nonCrossTradeMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NonCrossTradeMessage.Write(bytes, ref cursor, nonCrossTradeMessageWriting, in nonCrossTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage)
        {
            payloadWriterCase = CrossTradeMessageCase;
            int crossTradeMessageWriting = cursor.Index;
            var crossTradeMessageWriteValue = value.caseStorage.crossTradeMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.CrossTradeMessage.Write(bytes, ref cursor, crossTradeMessageWriting, in crossTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage)
        {
            payloadWriterCase = BrokenTradeMessageCase;
            int brokenTradeMessageWriting = cursor.Index;
            var brokenTradeMessageWriteValue = value.caseStorage.brokenTradeMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.BrokenTradeMessage.Write(bytes, ref cursor, brokenTradeMessageWriting, in brokenTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage)
        {
            payloadWriterCase = NetOrderImbalanceIndicatorMessageCase;
            int netOrderImbalanceIndicatorMessageWriting = cursor.Index;
            var netOrderImbalanceIndicatorMessageWriteValue = value.caseStorage.netOrderImbalanceIndicatorMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.NetOrderImbalanceIndicatorMessage.Write(bytes, ref cursor, netOrderImbalanceIndicatorMessageWriting, in netOrderImbalanceIndicatorMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Payload.Case.RetailInterestMessage)
        {
            payloadWriterCase = RetailInterestMessageCase;
            int retailInterestMessageWriting = cursor.Index;
            var retailInterestMessageWriteValue = value.caseStorage.retailInterestMessage;
            global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.RetailInterestMessage.Write(bytes, ref cursor, retailInterestMessageWriting, in retailInterestMessageWriteValue);
        }
        global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.PayloadWriter.PayloadSizeRule0(bytes, ref cursor, start < global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Absent : start + messageLengthDependencyWriteDistanceFromPayload, ref messageLengthDependencyWriteUsed, start, cursor.Index);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref payloadWriterCaseRecord))
        {
            int recorded = payloadWriterCase == global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Absent ? global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.NoCase : payloadWriterCase;
            if (payloadWriterCaseRecord != global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.Position.Absent && payloadWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NtxEquities.TotalView.Itch.v5_0.Parser.WriterValue);
            payloadWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
